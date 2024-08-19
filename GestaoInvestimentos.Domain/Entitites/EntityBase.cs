using MongoDB.Bson.Serialization.Attributes;

namespace GestaoInvestimentos.Domain.Entitites
{
    public class EntityBase
    {
        [BsonId]
        public int Id { get; set; }
    }
}
