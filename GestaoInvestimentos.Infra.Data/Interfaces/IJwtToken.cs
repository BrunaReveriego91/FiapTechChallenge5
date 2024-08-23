using GestaoInvestimentos.Domain.Entitites;

namespace GestaoInvestimentos.Infra.Data.Interfaces
{
    public interface IJwtToken
    {
        public Task<string> GenerateToken(Usuario usuario);
    }
}
