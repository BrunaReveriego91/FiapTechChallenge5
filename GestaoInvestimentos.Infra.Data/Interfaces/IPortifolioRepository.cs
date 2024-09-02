using GestaoInvestimentos.Domain.Entitites;

namespace GestaoInvestimentos.Infra.Data.Interfaces
{
    public interface IPortifolioRepository
    {
        Task<IEnumerable<Portifolio>> ListarPortifolios();
        Task CadastrarPortifolio(Portifolio portifolio);
        Task<Portifolio> BuscarPortifolio(Guid id);
        Task AlterarPortifolio(Portifolio portifolio);
        Task RemoverPortifolio(Guid id);
    }
}
