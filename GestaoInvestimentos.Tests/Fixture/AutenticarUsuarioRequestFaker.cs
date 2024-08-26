using Bogus;
using GestaoInvestimentos.Application.DTOs.Usuario.Request;

namespace GestaoInvestimentos.Tests.Fixture
{
    public class AutenticarUsuarioRequestFaker : Faker<AutenticarUsuarioRequest>
    {
        public AutenticarUsuarioRequestFaker()
        {
            RuleFor(c => c.Email, f => f.Internet.Email());
            RuleFor(c => c.Senha, f => f.Internet.Password());
        }
    }
}
