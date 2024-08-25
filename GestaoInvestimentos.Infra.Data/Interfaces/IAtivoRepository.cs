using GestaoInvestimentos.Domain.Entitites;

namespace GestaoInvestimentos.Infra.Data.Interfaces
{
    public interface IAtivoRepository
    {
        Task<IEnumerable<Ativo>> ListarAtivos();
        Task CadastrarAtivo(Ativo ativo);
    }
}
