using System.Threading.Tasks;
using DesafioGamificacaoCPFL.Models;
using DesafioGamificacaoCPFL.Infra.Database.Repositories;

namespace DesafioGamificacaoCPFL.Services
{
    public static class PagamentoParcialService
    {
        public static async Task<PagamentoParcialResponse> CadastrarNovoPagamentoParcial(IPagamentoParcialRepository pagamentoParcialRepository,
            IClienteRepository clienteRepository,
            PagamentoParcial pagamentoParcial)
        {
            CalcularValorRestanteDaFatura(pagamentoParcial);

            CalcularValorDaParcela(pagamentoParcial);

            await pagamentoParcialRepository.CadastrarNovoPagamentoParcial(pagamentoParcial);

            return new PagamentoParcialResponse
            {
                ValorRestante = pagamentoParcial.ValorRestante,
                ValorDaParcela = pagamentoParcial.ValorDaParcela,
                Mensagem = $"Pagamento parcial para o cliente {(await clienteRepository.Get(pagamentoParcial.ClienteId)).Nome} cadastrado com sucesso"
            };
        }

        private static void CalcularValorRestanteDaFatura(PagamentoParcial pagamentoParcial) => 
            pagamentoParcial.ValorRestante = pagamentoParcial.ValorTotalFatura - pagamentoParcial.ValorPagoParcialmente;

        private static void CalcularValorDaParcela(PagamentoParcial pagamentoParcial) =>
            pagamentoParcial.ValorDaParcela = pagamentoParcial.ValorRestante / pagamentoParcial.QuantidadeParcelas;
    }
}
