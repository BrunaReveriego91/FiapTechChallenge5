using GestaoInvestimentos.Domain.Entitites;
using GestaoInvestimentos.Infra.Data.Context;
using GestaoInvestimentos.Infra.Data.Interfaces;
using MongoDB.Driver;

namespace GestaoInvestimentos.Infra.Data.Repositories
{
    public class AtivoRepository : IAtivoRepository
    {
        private readonly IMongoContext _context;

        public AtivoRepository(IMongoContext context)
        {
            _context = context;
        }

        public async Task CadastrarAtivo(Ativo ativo)
        {
            try
            {
                await _context.Ativos.InsertOneAsync(ativo);
            }
            catch (MongoException ex)
            {
                throw new MongoException(ex.Message);
            }
        }

        public async Task<IEnumerable<Ativo>> ListarAtivos()
        {
            try
            {
                return await _context.Ativos.Find(e => true).ToListAsync();
            }
            catch (MongoException ex)
            {
                throw new MongoException(ex.Message);
            }
        }
    }
}
