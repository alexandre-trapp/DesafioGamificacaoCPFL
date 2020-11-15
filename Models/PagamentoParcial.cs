using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DesafioGamificacaoCPFL.Models
{
    public class PagamentoParcial
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; internal set; }
        public string ClienteId { get; set; }
        public decimal ValorTotalFatura { get; set; }
        public decimal ValorPagoParcialmente { get; set; }
        public decimal ValorRestante { get; internal set; }
        public decimal QuantidadeParcelas { get; set; }
        public decimal ValorDaParcela { get; internal set; }
        public MetodoPagamento MetodoPagamento { internal get; set; }
    }    

    public class PagamentoParcialResponse
    { 
        public decimal ValorRestante { get; internal set; }
        public decimal ValorDaParcela { get; internal set; }
        public decimal QuantidadeParcelas { get; set; }
        public string Mensagem { get; internal set; }
    }

    public class MetodoPagamento
    {
        public bool Fatura { get; set; } = false;
        public CartaoCredito CartaoCredito { get; set; }
        public DadosBancarios DadosBancarios { get; set; }
    }

    public class CartaoCredito
    {
        public int NumeroCartao { get; set; }
        public string DataVencimento { get; set; }
        public int NumeroVerso { get; set; }
        public string NomeNoCartao { get; set; }
    }

    public class DadosBancarios
    {
        public int CodigoBanco { get; set; }
        public int AgenciaNumero { get; set; }
        public int ContaNumero { get; set; }
        public string ContaDigito { get; set; }
    }
}
