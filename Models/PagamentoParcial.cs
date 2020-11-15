using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DesafioGamificacaoCPFL.Models
{
    public class PagamentoParcial
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ClienteId { get; set; }
        public decimal ValorTotalFatura { get; set; }
        public decimal ValorPagoParcialmente { get; set; }
        public decimal ValorRestante { get; set; }
        public decimal QuantidadeParcelas { get; set; }
        public decimal ValorDaParcela { get; set; }
    }
}
