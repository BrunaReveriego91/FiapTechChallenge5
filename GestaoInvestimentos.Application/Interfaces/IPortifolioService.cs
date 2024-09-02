using GestaoInvestimentos.Domain.Entitites;

namespace GestaoInvestimentos.Application.Interfaces
{
    public interface IPortifolioService
    {
        Task CadastrarPortifolio(Portifolio portifolio);
        Task<IEnumerable<Portifolio>> ListarPortifolios();        
        Task AlterarPortifolio(Portifolio portifolio);
        Task RemoverPortifolio(Guid id);
        Task<Portifolio> BuscarPortifolio(Guid id);
    }
}
