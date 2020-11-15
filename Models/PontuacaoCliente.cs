using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DesafioGamificacaoCPFL.Models
{
    public class PontuacaoCliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; internal set; }
        public string ClienteId { get; set; }
        public int QuantidadePontosAtual { get; set; }
        public int QuantidadeXP { get; set; }
        public int QuantidadeNovosPontos { get; set; }
        public int QuantidadePontosNecessariosParaAtingirProximoNivel { get; set; }
        internal int QuantidadePontosDeBonusRecebidosCadaNivel { get; set; }
    }

    public class ResgatePontosRequest
    {
        public string ClienteId { get; set; }
        public int QuantidadePontosResgatados { get; set; }
    }

    public class ResgatePontosResponse
    {
        public int TotalPontosCliente { get; set; }
        public string Mensagem { get; set; }
    }

    public class PontuacaoClienteResponse
    {
        public int PontosGanhosBonusPorAtingirNovoNivel { get; set; }
        public int TotalPontosCliente { get; set; }
        public string Mensagem { get; set; }
    }
}
