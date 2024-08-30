using GestaoInvestimentos.Domain.Entitites;

namespace GestaoInvestimentos.Infra.Data.Interfaces
{
    public interface IAtivoRepository
    {
        Task<IEnumerable<Ativo>> ListarAtivos();
        Task CadastrarAtivo(Ativo ativo);
        Task<Ativo> BuscarAtivo(Guid id);
        Task AlterarAtivo(Ativo ativo);
        Task RemoverAtivo(Guid id);
    }
}
