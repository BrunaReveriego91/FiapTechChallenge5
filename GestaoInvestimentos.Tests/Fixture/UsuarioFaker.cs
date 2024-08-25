using Bogus;
using GestaoInvestimentos.Domain.Entitites;
using MongoDB.Bson;

namespace GestaoInvestimentos.Tests.Fixture
{
    public class UsuarioFaker : Faker<Usuario>
    {
        public UsuarioFaker()
        {
            RuleFor(c => c.Id, f => new ObjectId(f.Random.Bytes(12)));
            RuleFor(c => c.Nome, f => f.Random.String(10));
            RuleFor(c => c.Email, f => f.Person.Email);
            RuleFor(c => c.Senha, f => f.Internet.Password());

        }
    }
}
