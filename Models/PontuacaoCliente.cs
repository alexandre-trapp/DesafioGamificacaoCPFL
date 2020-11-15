using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DesafioGamificacaoCPFL.Models
{
    public class PontuacaoCliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ClienteId { get; set; }
        public int QuantidadePontos { get; set; }
    }
}
