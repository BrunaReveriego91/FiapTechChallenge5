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

        public async Task AlterarAtivo(Ativo ativo)
        {
            try
            {
                var filter = Builders<Ativo>.Filter.Eq(e => e.Id, ativo.Id);
                var update = Builders<Ativo>.Update
                .Set(e => e.Nome, ativo.Nome)
                    .Set(e => e.Codigo, ativo.Codigo);

                await _context.Ativos.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Ativo> BuscarAtivo(Guid id)
        {
            try
            {
                var filter = Builders<Ativo>.Filter.Eq(e => e.Id, id);
                var ativo = await _context.Ativos.Find(filter).FirstOrDefaultAsync();

                return ativo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        public async Task RemoverAtivo(Guid id)
        {
            try
            {
                var filter = Builders<Ativo>.Filter.Eq(e => e.Id, id);
                await _context.Ativos.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
