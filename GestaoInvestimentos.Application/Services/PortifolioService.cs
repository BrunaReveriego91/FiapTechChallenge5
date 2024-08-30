using GestaoInvestimentos.Application.Interfaces;
using GestaoInvestimentos.Domain.Entitites;
using GestaoInvestimentos.Infra.Data.Interfaces;

namespace GestaoInvestimentos.Application.Services
{
    public class PortifolioService : IPortifolioService
    {
        private readonly IPortifolioRepository _portifolioRepository;

        public PortifolioService(IPortifolioRepository portifolioRepository)
        {
            _portifolioRepository = portifolioRepository;
        }

        public async Task<IEnumerable<Portifolio>> ListarPortifolios()
        {
            return await _portifolioRepository.ListarPortifolios();
        }

        public async Task CadastrarPortifolio(Portifolio portifolio)
        {
            await _portifolioRepository.CadastrarPortifolio(portifolio);
        }

        public Task AlterarPortifolio(Portifolio portifolio)
        {
            throw new NotImplementedException();
        }

        public async Task RemoverPortifolio(Guid id)
        {
            var portifolioExistente = await _portifolioRepository.BuscarPortifolio(id);

            if (portifolioExistente == null)
                throw new Exception("Cadastro portifolio não localizado.");

            await _portifolioRepository.RemoverPortifolio(id);
        }

        public async Task<Portifolio> BuscarPortifolio(Guid id)
        {
            return await _portifolioRepository.BuscarPortifolio(id);
        }
    }
}
