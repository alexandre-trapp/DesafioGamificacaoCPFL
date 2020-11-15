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
        public int QuantidadePontosAtual { get; internal set; }
        public int QuantidadeXP { get; internal set; }
        public int QuantidadeNovosPontos { get; set; }
        public int QuantidadePontosXpNecessariosParaAtingirProximoNivel { get; internal set; }
        public int QuantidadePontosDeBonusRecebidosCadaNivel { get; internal set; }
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

    public class AdicionarPontuacaoClienteRequest
    {
        public string ClienteId { get; set; }
        public int QuantidadeNovosPontos { get; set; }
    }

    public class PontuacaoClienteResponse
    {
        public int PontosGanhosBonusPorAtingirNovoNivel { get; set; }
        public int TotalPontosCliente { get; set; }
        public string Mensagem { get; set; }
        public int QuantidadePontosXpNecessariosParaAtingirProximoNivel { get; internal set; }
    }
}
