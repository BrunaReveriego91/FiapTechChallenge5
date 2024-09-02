using GestaoInvestimentos.Domain.Entitites;
using GestaoInvestimentos.Infra.Data.Context;
using GestaoInvestimentos.Infra.Data.Interfaces;
using MongoDB.Driver;

namespace GestaoInvestimentos.Infra.Data.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly IMongoContext _context;

        public TransacaoRepository(IMongoContext context)
        {
            _context = context;
        }

        public async Task AlterarTransacao(Transacao transacao)
        {
            try
            {
                var filter = Builders<Transacao>.Filter.Eq(e => e.Id, transacao.Id);
                var update = Builders<Transacao>.Update
                .Set(e => e.Quantidade, transacao.Quantidade)
                    .Set(e => e.Preco, transacao.Preco);

                await _context.Transacoes.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Transacao> BuscarTransacao(Guid id)
        {
            try
            {
                var filter = Builders<Transacao>.Filter.Eq(e => e.Id, id);
                var transacao = await _context.Transacoes.Find(filter).FirstOrDefaultAsync();

                return transacao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task CadastrarTransacao(Transacao transacao)
        {
            try
            {
                await _context.Transacoes.InsertOneAsync(transacao);
            }
            catch (MongoException ex)
            {
                throw new MongoException(ex.Message);
            }
        }

        public async Task<IEnumerable<Transacao>> ListarTransacoes()
        {
            try
            {
                return await _context.Transacoes.Find(e => true).ToListAsync();
            }
            catch (MongoException ex)
            {
                throw new MongoException(ex.Message);
            }
        }

        public async Task RemoverTransacao(Guid id)
        {
            try
            {
                var filter = Builders<Transacao>.Filter.Eq(e => e.Id, id);
                await _context.Transacoes.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
