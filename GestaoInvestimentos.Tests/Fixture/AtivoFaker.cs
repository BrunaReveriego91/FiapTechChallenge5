using Bogus;
using GestaoInvestimentos.Domain.Entitites;
using GestaoInvestimentos.Domain.Enums;

namespace GestaoInvestimentos.Tests.Fixture
{
    public class AtivoFaker : Faker<Ativo>
    {
        public AtivoFaker()
        {
            RuleFor(c => c.Id, f => Guid.NewGuid());
            RuleFor(c => c.TipoAtivo, f => f.PickRandom<EnumTipoAtivo>());
            RuleFor(c => c.Nome, f => f.Random.String(20));
            RuleFor(c => c.Codigo, f => f.Random.String(20));
        }
    }
}
