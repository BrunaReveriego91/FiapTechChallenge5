using MongoDB.Bson.Serialization.Attributes;

namespace GestaoInvestimentos.Domain.Entitites
{
    public class EntityBase
    {
        [BsonId]
        public Guid Id { get; set; }
        public EntityBase()
        {
            Id = new Guid();
        }
    }
}
