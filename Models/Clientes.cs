using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DesafioGamificacaoCPFL
{
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; internal set; }

        public string Nome{ get; internal set; }

        public string Cpf { get; internal set; }
    }
}
