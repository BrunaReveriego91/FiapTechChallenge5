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

        public async Task AlterarAtivo(Ativo ativo)
        {
            await _ativoRepository.AlterarAtivo(ativo);
        }

        public async Task RemoverAtivo(Guid id)
        {
            var ativoExistente = await _ativoRepository.BuscarAtivo(id);

            if (ativoExistente == null)
                throw new Exception("Cadastro ativo não localizado.");

            await _ativoRepository.RemoverAtivo(id);
        }

        public async Task<Ativo> BuscarAtivo(Guid id)
        {
            return await _ativoRepository.BuscarAtivo(id);
        }
    }
}
