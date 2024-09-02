using GestaoInvestimentos.Application.Interfaces;
using GestaoInvestimentos.Domain.Entitites;
using GestaoInvestimentos.Infra.Data.Interfaces;

namespace GestaoInvestimentos.Application.Services
{
    public class PortifolioService : IPortifolioService
    {
        private readonly IPortifolioRepository _portifolioRepository;
        private readonly IUsuarioService _usuarioService;

        public PortifolioService(IPortifolioRepository portifolioRepository, IUsuarioService usuarioService)
        {
            _portifolioRepository = portifolioRepository;
            _usuarioService = usuarioService;
        }

        public async Task<IEnumerable<Portifolio>> ListarPortifolios()
        {
            return await _portifolioRepository.ListarPortifolios();
        }

        public async Task CadastrarPortifolio(Portifolio portifolio)
        {
            var usuario = await _usuarioService.BuscarUsuario(portifolio.UsuarioId);

            if (usuario == null)
                throw new Exception("Não foi possível realizar o cadastro do portifólio, usuário não localizado.");

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
