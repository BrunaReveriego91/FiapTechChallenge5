using GestaoInvestimentos.Application.Interfaces;
using GestaoInvestimentos.Domain.Entitites;
using GestaoInvestimentos.Infra.Data.Interfaces;

namespace GestaoInvestimentos.Application.Services
{
    public class AtivoService : IAtivoService
    {
        private readonly IAtivoRepository _ativoRepository;

        public AtivoService(IAtivoRepository ativoRepository)
        {
            _ativoRepository = ativoRepository;
        }

        public async Task<IEnumerable<Ativo>> ListarAtivos()
        {
            return await _ativoRepository.ListarAtivos();
        }

        public async Task CadastrarAtivo(Ativo ativo)
        {
            await _ativoRepository.CadastrarAtivo(ativo);
        }
    }
}
