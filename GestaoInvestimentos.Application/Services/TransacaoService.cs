using GestaoInvestimentos.Application.Interfaces;
using GestaoInvestimentos.Domain.Entitites;
using GestaoInvestimentos.Infra.Data.Interfaces;

namespace GestaoInvestimentos.Application.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;

        public TransacaoService(ITransacaoRepository transacaoRepository)
        {
            _transacaoRepository = transacaoRepository;
        }

        public async Task<IEnumerable<Transacao>> ListarTransacoes()
        {
            return await _transacaoRepository.ListarTransacoes();
        }

        public async Task CadastrarTransacao(Transacao transacao)
        {
            await _transacaoRepository.CadastrarTransacao(transacao);
        }

        public Task AlterarTransacao(Transacao transacao)
        {
            throw new NotImplementedException();
        }

        public async Task RemoverTransacao(Guid id)
        {
            var transacaoExistente = await _transacaoRepository.BuscarTransacao(id);

            if (transacaoExistente == null)
                throw new Exception("Cadastro transacao não localizado.");

            await _transacaoRepository.RemoverTransacao(id);
        }

        public async Task<Transacao> BuscarTransacao(Guid id)
        {
            return await _transacaoRepository.BuscarTransacao(id);
        }
    }
}
