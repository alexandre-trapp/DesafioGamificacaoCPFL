using System.Threading.Tasks;
using DesafioGamificacaoCPFL.Models;
using DesafioGamificacaoCPFL.Infra.Database.Repositories;
using System.Globalization;
using System;
using System.Diagnostics;

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
                QuantidadeParcelas = pagamentoParcial.QuantidadeParcelas,
                Mensagem = $"Pagamento parcial em {pagamentoParcial.QuantidadeParcelas} parcelas no valor de " + 
                           string.Format(new CultureInfo("pt-BR"), "{0:C}", pagamentoParcial.ValorDaParcela) +
                           $" para o cliente {(await clienteRepository.Get(pagamentoParcial.ClienteId)).Nome} cadastrado com sucesso," +
                           $" com a forma de pagamento { BuscaFormaPagamento(pagamentoParcial) }"
            };
        }

        private static void CalcularValorRestanteDaFatura(PagamentoParcial pagamentoParcial) => 
            pagamentoParcial.ValorRestante = pagamentoParcial.ValorTotalFatura - pagamentoParcial.ValorPagoParcialmente;

        private static void CalcularValorDaParcela(PagamentoParcial pagamentoParcial) =>
            pagamentoParcial.ValorDaParcela = pagamentoParcial.ValorRestante / pagamentoParcial.QuantidadeParcelas;

        private static string BuscaFormaPagamento(PagamentoParcial pagamentoParcial)
        {
            if (pagamentoParcial.MetodoPagamento.CartaoCredito != null)
                return "Cartão de crédito";

            if (pagamentoParcial.MetodoPagamento.DadosBancarios != null)
                return "Dados bancários";

            return "fatura de energia";
        }
    }
}
