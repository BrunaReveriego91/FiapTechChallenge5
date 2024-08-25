using GestaoInvestimentos.Domain.Entitites;

namespace GestaoInvestimentos.Application.Interfaces
{
    public interface IAtivoService
    {
        Task CadastrarAtivo(Ativo ativo);
        Task<IEnumerable<Ativo>> ListarAtivos();
    }
}
