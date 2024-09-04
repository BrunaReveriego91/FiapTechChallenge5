using GestaoInvestimentos.Domain.Entitites;
using GestaoInvestimentos.Infra.Data.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GestaoInvestimentos.Infra.Data.Context
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _db;

        public MongoContext(IOptions<MongoConfiguration> config)
        {
            var settings = MongoClientSettings.FromConnectionString(config.Value.Host);
         
            var client = new MongoClient(settings);
            _db = client.GetDatabase(config.Value.Database);

            //CriaCollectionSeNaoExistir<Usuario>("Usuarios").Wait();
            //CriaCollectionSeNaoExistir<Ativo>("Ativos").Wait();
            //CriaCollectionSeNaoExistir<Portifolio>("Portifolios").Wait();
            //CriaCollectionSeNaoExistir<Transacao>("Transacoes").Wait();

        }

        private async Task CriaCollectionSeNaoExistir<T>(string nomeCollection)
        {
            //await Task.Run(() =>
            //{
                try
                {
                    var filter = new BsonDocument("name", nomeCollection);
                    var collections = _db.ListCollections(new ListCollectionsOptions { Filter = filter });

                    if (!collections.Any())
                        _db.CreateCollection(nomeCollection);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            //});
        }

        public IMongoCollection<Usuario> Usuarios => _db.GetCollection<Usuario>("Usuarios");

        public IMongoCollection<Ativo> Ativos => _db.GetCollection<Ativo>("Ativos");
        public IMongoCollection<Portifolio> Portifolios => _db.GetCollection<Portifolio>("Portifolios");
        public IMongoCollection<Transacao> Transacoes => _db.GetCollection<Transacao>("Transacoes");
    }
}
