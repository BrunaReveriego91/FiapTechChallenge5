using Bogus;
using GestaoInvestimentos.Domain.Entitites;

namespace GestaoInvestimentos.Tests.Fixture
{
    public class PortfolioFaker : Faker<Portifolio>
    {
        public PortfolioFaker()
        {
            RuleFor(c => c.Id, f => Guid.NewGuid());
            RuleFor(c => c.UsuarioId, f => Guid.NewGuid());
            RuleFor(c => c.Nome, f => f.Random.String(10));
            RuleFor(c => c.Descricao, f => f.Random.String(10));
        }
    }
}
