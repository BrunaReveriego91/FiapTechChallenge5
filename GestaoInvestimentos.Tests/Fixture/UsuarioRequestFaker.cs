using Bogus;
using GestaoInvestimentos.Application.DTOs.Usuario.Request;
using GestaoInvestimentos.Domain.Enums;

namespace GestaoInvestimentos.Tests.Fixture
{
    public class UsuarioRequestFaker: Faker<UsuarioRequest>
    {
        public UsuarioRequestFaker()
        {
            RuleFor(c => c.Nome, c => c.Person.FullName);
            RuleFor(c => c.Email, c => c.Person.Email);
            RuleFor(c => c.Senha, c => c.Internet.Password());
            RuleFor(c => c.Role, c => c.PickRandom<EnumRoles>());
        }
    }
}
