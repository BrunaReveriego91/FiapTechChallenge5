using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GestaoInvestimentos.Domain.Entitites
{
    public class EntityBase
    {
        [BsonId]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    }
}
