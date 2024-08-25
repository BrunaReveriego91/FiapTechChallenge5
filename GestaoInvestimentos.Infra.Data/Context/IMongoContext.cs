using GestaoInvestimentos.Domain.Entitites;
using MongoDB.Driver;

namespace GestaoInvestimentos.Infra.Data.Context
{
    public interface IMongoContext
    {
        IMongoCollection<Usuario> Usuarios { get; }
        IMongoCollection<Ativo> Ativos { get; }

    }
}
