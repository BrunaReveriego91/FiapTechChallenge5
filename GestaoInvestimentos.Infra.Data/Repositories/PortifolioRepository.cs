using GestaoInvestimentos.Domain.Entitites;
using GestaoInvestimentos.Infra.Data.Context;
using GestaoInvestimentos.Infra.Data.Interfaces;
using MongoDB.Driver;

namespace GestaoInvestimentos.Infra.Data.Repositories
{
    public class PortifolioRepository : IPortifolioRepository
    {
        private readonly IMongoContext _context;

        public PortifolioRepository(IMongoContext context)
        {
            _context = context;
        }

        public async Task AlterarPortifolio(Portifolio portifolio)
        {
            try
            {
                var filter = Builders<Portifolio>.Filter.Eq(e => e.Id, portifolio.Id);
                var update = Builders<Portifolio>.Update
                .Set(e => e.Nome, portifolio.Nome)
                    .Set(e => e.Descricao, portifolio.Descricao);

                await _context.Portifolios.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Portifolio> BuscarPortifolio(Guid id)
        {
            try
            {
                var filter = Builders<Portifolio>.Filter.Eq(e => e.Id, id);
                var portifolio = await _context.Portifolios.Find(filter).FirstOrDefaultAsync();

                return portifolio;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task CadastrarPortifolio(Portifolio portifolio)
        {
            try
            {
                await _context.Portifolios.InsertOneAsync(portifolio);
            }
            catch (MongoException ex)
            {
                throw new MongoException(ex.Message);
            }
        }

        public async Task<IEnumerable<Portifolio>> ListarPortifolios()
        {
            try
            {
                return await _context.Portifolios.Find(e => true).ToListAsync();
            }
            catch (MongoException ex)
            {
                throw new MongoException(ex.Message);
            }
        }

        public async Task RemoverPortifolio(Guid id)
        {
            try
            {
                var filter = Builders<Portifolio>.Filter.Eq(e => e.Id, id);
                await _context.Portifolios.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
