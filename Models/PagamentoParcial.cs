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
    }

    public class PagamentoParcialResponse
    {
        public decimal ValorRestante { get; internal set; }
        public decimal ValorDaParcela { get; internal set; }
        public decimal QuantidadeParcelas { get; set; }
        public string Mensagem { get; internal set; }
    }
}
