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
        private readonly IPontuacaoClienteRepository _pontuacaoClienteRepository;
        private PontuacaoClienteResponse _pontuacaoClienteResponse;

        public PontuacaoClienteService(IPontuacaoClienteRepository pontuacaoClienteRepository) =>
            _pontuacaoClienteRepository = pontuacaoClienteRepository;

        public async Task<PontuacaoClienteResponse> AdicionarPontosAoClienteConformeRegraDeGamificacao(PontuacaoCliente pontuacaoCliente)
        {
            string mensagemAtingiuProximoNivel = string.Empty;
            _pontuacaoClienteResponse = new PontuacaoClienteResponse();

            CalcularQuantosPontosPrecisaParaAtingirProximoNivel(pontuacaoCliente);

            if (ClienteAtingiuProximoNivel(pontuacaoCliente))
            {
                mensagemAtingiuProximoNivel = "Parabéns, você atingiu o próximo nível!" + Environment.NewLine;

                ResetarQuantidadePontosNecessariosParaAtingirProximoNivel(pontuacaoCliente);
                CalcularQuantidadeDePontosGanhosPorAtingirProximoNivel(pontuacaoCliente);
            }

            CalcularQuantidadeDePontosTotalDoCliente(pontuacaoCliente);
            pontuacaoCliente.QuantidadePontosAtual = _pontuacaoClienteResponse.TotalPontosCliente;

            await _pontuacaoClienteRepository.AtualizarPontosCliente(pontuacaoCliente);

            _pontuacaoClienteResponse.Mensagem = $"{mensagemAtingiuProximoNivel}Pontuação atualizada com sucesso, sua nova pontuação é {pontuacaoCliente.QuantidadePontosAtual}, " +
                $"agora falta só mais {pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel} pontos para atingir o próximo nível e conseguir mais descontos!";

            return _pontuacaoClienteResponse;
        }

        private void CalcularQuantosPontosPrecisaParaAtingirProximoNivel(PontuacaoCliente pontuacaoCliente)
        {
            if (pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel > 0)
                pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel = pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel - pontuacaoCliente.QuantidadeNovosPontos;

            else if (pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel == 0)
                pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel = pontuacaoCliente.QuantidadePontosPadraoParaProximoNivel;

            else
                pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel = pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel * -1;
        }

        private bool ClienteAtingiuProximoNivel(PontuacaoCliente pontuacaoCliente) =>
            pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel <= 0;

        private void CalcularQuantidadeDePontosGanhosPorAtingirProximoNivel(PontuacaoCliente pontuacaoCliente)
        {
            _pontuacaoClienteResponse.PontosGanhosBonusPorAtingirNovoNivel = Convert.ToInt32(pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel * 1.2);
        }

        private void ResetarQuantidadePontosNecessariosParaAtingirProximoNivel(PontuacaoCliente pontuacaoCliente) =>
            CalcularQuantosPontosPrecisaParaAtingirProximoNivel(pontuacaoCliente);

        private void CalcularQuantidadeDePontosTotalDoCliente(PontuacaoCliente pontuacaoCliente) =>

            _pontuacaoClienteResponse.TotalPontosCliente = (pontuacaoCliente.QuantidadePontosAtual +

                                                             (_pontuacaoClienteResponse.PontosGanhosBonusPorAtingirNovoNivel > 0
                                                                ? _pontuacaoClienteResponse.PontosGanhosBonusPorAtingirNovoNivel
                                                                : pontuacaoCliente.QuantidadeNovosPontos) );

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
