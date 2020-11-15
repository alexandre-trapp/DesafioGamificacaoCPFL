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

        public async Task<PontuacaoClienteResponse> AtualizarPontuacaoClienteConformeRegraDeGamificacao(PontuacaoCliente pontuacaoCliente)
        {
            _pontuacaoClienteResponse = new PontuacaoClienteResponse();

            CalcularQuantosPontosPrecisaParaAtingirProximoNivel(pontuacaoCliente);

            if (ClienteAtingiuProximoNivel(pontuacaoCliente))
            {
                CalcularQuantidadeDePontosGanhosPorAtingirProximoNivel(pontuacaoCliente);
                ResetarQuantidadePontosNecessariosParaAtingirProximoNivel(pontuacaoCliente);
            }

            CalcularQuantidadeDePontosTotalDoCliente(pontuacaoCliente);
            pontuacaoCliente.QuantidadePontosAtual = _pontuacaoClienteResponse.TotalPontosCliente;

            await _pontuacaoClienteRepository.AtualizarPontosCliente(pontuacaoCliente);

            _pontuacaoClienteResponse.Mensagem = $"Pontuação atualizada com sucesso, sua nova pontuação é {pontuacaoCliente.QuantidadePontosAtual}, " +
                $"faltando {pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel} pontos para atingir o próximo nível " +
                $"({pontuacaoCliente.QuantidadePontosAtual + pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel} pontos ou mais.)";

            return _pontuacaoClienteResponse;
        }

        private void CalcularQuantosPontosPrecisaParaAtingirProximoNivel(PontuacaoCliente pontuacaoCliente)
        {
            if (pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel > 0)
                pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel = pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel - pontuacaoCliente.QuantidadeNovosPontos;

            else if (pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel == 0)
                pontuacaoCliente.QuantidadePontosNecessariosParaAtingirProximoNivel = pontuacaoCliente.QuantidadeNovosPontos;

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
    }
}
