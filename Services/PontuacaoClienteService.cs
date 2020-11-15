using System;
using MongoDB.Driver;
using System.Threading.Tasks;
using DesafioGamificacaoCPFL.Models;
using DesafioGamificacaoCPFL.Infra.Database.Repositories;

namespace DesafioGamificacaoCPFL.Services
{
    public class PontuacaoClienteService
    {
        private const decimal MULTIPLICADOR_BONUS_PROXIMO_NIVEL = 1.2m;
        private const decimal MULTIPLICADOR_PONTUACAO_PARA_ATINGIR_PROXIMO_NIVEL = 1.8m;
        private const int PONTUACAO_BONUS_INICIAL = 7;
        public const int PONTUACAO_INICIAL_PARA_PASSAR_PRIMEIRO_NIVEL = 32;

        private readonly IPontuacaoClienteRepository _pontuacaoClienteRepository;
        private PontuacaoClienteResponse _pontuacaoClienteResponse;

        public PontuacaoClienteService(IPontuacaoClienteRepository pontuacaoClienteRepository) =>
            _pontuacaoClienteRepository = pontuacaoClienteRepository;

        public async Task<PontuacaoClienteResponse> AdicionarPontosAoClienteConformeRegraDeGamificacao(AdicionarPontuacaoClienteRequest pontuacaoClienteRequest)
        {
            string mensagemAtingiuProximoNivel = string.Empty;

            var pontuacaoCliente = await _pontuacaoClienteRepository.Get(pontuacaoClienteRequest.ClienteId);
            if (pontuacaoCliente == null)
            {
                throw new OperationCanceledException("Não é possível adicionar pontos ao cliente, pois ele ainda não possui pontos, " +
                    "deve-se chamar a api '/pontuacaoCliente/cadastrar' para cadastro inicial da pontuação.");
            }

            pontuacaoCliente.QuantidadeNovosPontos = pontuacaoClienteRequest.QuantidadeNovosPontos;

            _pontuacaoClienteResponse = new PontuacaoClienteResponse();

            if (ClienteAtingiuProximoNivel(pontuacaoCliente))
            {
                mensagemAtingiuProximoNivel = "Parabéns, você atingiu o próximo nível!" + Environment.NewLine;

                CalcularQuantidadeDePontosGanhosPorAtingirProximoNivel(pontuacaoCliente);
                CalcularQuantosPontosPrecisaParaAtingirProximoNivel(pontuacaoCliente);
            }

            CalcularQuantidadeDePontosTotalDoCliente(pontuacaoCliente);

            await AtualizarPontuacaoCliente(pontuacaoCliente);

            _pontuacaoClienteResponse.Mensagem = $"{mensagemAtingiuProximoNivel}Pontuação atualizada com sucesso, sua nova pontuação é {pontuacaoCliente.QuantidadePontosAtual}" +
                $" e {pontuacaoCliente.QuantidadeXP} de XP, você precisa somar {pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel} " +
                $"pontos para atingir o próximo nível e conseguir ainda mais descontos!";

            return _pontuacaoClienteResponse;
        }

        private bool ClienteAtingiuProximoNivel(PontuacaoCliente pontuacaoCliente) =>
            (pontuacaoCliente.QuantidadePontosAtual + pontuacaoCliente.QuantidadeNovosPontos) >= pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel;

        private void CalcularQuantidadeDePontosGanhosPorAtingirProximoNivel(PontuacaoCliente pontuacaoCliente)
        {
            if (pontuacaoCliente.QuantidadePontosDeBonusRecebidosCadaNivel == 0)
                _pontuacaoClienteResponse.PontosGanhosBonusPorAtingirNovoNivel = (int)(PONTUACAO_BONUS_INICIAL * 1.2);
            else
                _pontuacaoClienteResponse.PontosGanhosBonusPorAtingirNovoNivel = (int)(pontuacaoCliente.QuantidadePontosDeBonusRecebidosCadaNivel * 1.2);
        }

        private void CalcularQuantosPontosPrecisaParaAtingirProximoNivel(PontuacaoCliente pontuacaoCliente)
        {
            var quantidadePontosAtualSomadoComNovosPontos = pontuacaoCliente.QuantidadePontosAtual + pontuacaoCliente.QuantidadeNovosPontos;

            if (quantidadePontosAtualSomadoComNovosPontos < PONTUACAO_INICIAL_PARA_PASSAR_PRIMEIRO_NIVEL)
                pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel = PONTUACAO_INICIAL_PARA_PASSAR_PRIMEIRO_NIVEL;

            else if (quantidadePontosAtualSomadoComNovosPontos == PONTUACAO_INICIAL_PARA_PASSAR_PRIMEIRO_NIVEL)
                pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel = (int)(PONTUACAO_INICIAL_PARA_PASSAR_PRIMEIRO_NIVEL *
                                                                                            MULTIPLICADOR_PONTUACAO_PARA_ATINGIR_PROXIMO_NIVEL);

            else if (quantidadePontosAtualSomadoComNovosPontos >= pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel)
                pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel = (int)(pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel *
                                                                                            MULTIPLICADOR_PONTUACAO_PARA_ATINGIR_PROXIMO_NIVEL);
        }

        private void CalcularQuantidadeDePontosTotalDoCliente(PontuacaoCliente pontuacaoCliente)
        {
            _pontuacaoClienteResponse.TotalPontosCliente = (pontuacaoCliente.QuantidadePontosAtual +

                                                             (_pontuacaoClienteResponse.PontosGanhosBonusPorAtingirNovoNivel > 0
                                                                ? pontuacaoCliente.QuantidadeNovosPontos + _pontuacaoClienteResponse.PontosGanhosBonusPorAtingirNovoNivel
                                                                : pontuacaoCliente.QuantidadeNovosPontos));
        }

        private async Task AtualizarPontuacaoCliente(PontuacaoCliente pontuacaoCliente)
        {
            pontuacaoCliente.QuantidadePontosAtual = _pontuacaoClienteResponse.TotalPontosCliente;

            if (_pontuacaoClienteResponse.PontosGanhosBonusPorAtingirNovoNivel > 0)
                pontuacaoCliente.QuantidadePontosDeBonusRecebidosCadaNivel = _pontuacaoClienteResponse.PontosGanhosBonusPorAtingirNovoNivel ;

            pontuacaoCliente.QuantidadeXP = _pontuacaoClienteResponse.PontosGanhosBonusPorAtingirNovoNivel > 0

                                              ? pontuacaoCliente.QuantidadeXP +
                                                pontuacaoCliente.QuantidadeNovosPontos +
                                                _pontuacaoClienteResponse.PontosGanhosBonusPorAtingirNovoNivel

                                              : pontuacaoCliente.QuantidadeXP + pontuacaoCliente.QuantidadeNovosPontos;

            await _pontuacaoClienteRepository.AtualizarPontosCliente(pontuacaoCliente);
        }

        public async Task<ResgatePontosResponse> ResgatarPontosDoCliente(ResgatePontosRequest resgatePontos)
        {
            var pontuacaoAtual = await _pontuacaoClienteRepository.Get(resgatePontos.ClienteId);

            await AtualizarQuantidadePontosClientesAoEfetuarResgate(resgatePontos, pontuacaoAtual);

            return new ResgatePontosResponse
            {
                TotalPontosCliente = pontuacaoAtual.QuantidadePontosAtual,
                Mensagem = $"Resgate efetuado com sucesso, seu saldo atual é de {pontuacaoAtual.QuantidadePontosAtual} pontos."
            };
        }

        private async Task AtualizarQuantidadePontosClientesAoEfetuarResgate(ResgatePontosRequest resgatePontos, 
            PontuacaoCliente pontuacaoAtual)
        {
            if (pontuacaoAtual.QuantidadePontosAtual < resgatePontos.QuantidadePontosResgatados)
                throw new OperationCanceledException("Não é possível efetuar o resgate dos pontos, seu saldo de pontos é inferior a quantidade de resgate solicitado");

            pontuacaoAtual.QuantidadeNovosPontos = 0;
            pontuacaoAtual.QuantidadePontosAtual = pontuacaoAtual.QuantidadePontosAtual - resgatePontos.QuantidadePontosResgatados;

            await _pontuacaoClienteRepository.AtualizarPontosCliente(pontuacaoAtual);
        }
    }
}
