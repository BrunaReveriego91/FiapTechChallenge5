using GestaoInvestimentos.Domain.Entitites;

namespace GestaoInvestimentos.Application.Interfaces
{
    public interface IAtivoService
    {
        Task CadastrarAtivo(Ativo ativo);
        Task<IEnumerable<Ativo>> ListarAtivos();        
        Task AlterarAtivo(Ativo ativo);
        Task RemoverAtivo(Guid id);
        Task<Ativo> BuscarAtivo(Guid id);
    }
}
