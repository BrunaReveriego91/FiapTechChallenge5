using Bogus;
using GestaoInvestimentos.Domain.Entitites;
using GestaoInvestimentos.Domain.Enums;

namespace GestaoInvestimentos.Tests.Fixture
{
    public class TransacaoFaker : Faker<Transacao>
    {
        public TransacaoFaker()
        {
            RuleFor(c => c.Id, f => Guid.NewGuid());
            RuleFor(c => c.PortfolioId, f => Guid.NewGuid());
            RuleFor(c => c.AtivoId, f => Guid.NewGuid());
            RuleFor(c => c.TipoTransacao, f => f.PickRandom<EnumTipoTransacao>());
            RuleFor(c => c.Quantidade, f => f.Random.Int(1, 30));
            RuleFor(c => c.Preco, f => f.Random.Double(1, 30));
            RuleFor(c => c.DataTransacao, f => f.Date.Soon(10));
        }
    }
}
