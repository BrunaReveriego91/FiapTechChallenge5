using GestaoInvestimentos.Application.Interfaces;
using GestaoInvestimentos.Domain.Entitites;
using GestaoInvestimentos.Infra.Data.Interfaces;

namespace GestaoInvestimentos.Application.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IAtivoService _ativoService;
        private readonly IPortifolioService _portifolioService;

        public TransacaoService(ITransacaoRepository transacaoRepository, IAtivoService ativoService, IPortifolioService portifolioService)
        {
            _transacaoRepository = transacaoRepository;
            _ativoService = ativoService;
            _portifolioService = portifolioService;
        }

        public async Task<IEnumerable<Transacao>> ListarTransacoes()
        {
            return await _transacaoRepository.ListarTransacoes();
        }

        public async Task CadastrarTransacao(Transacao transacao)
        {

            var ativo = await _ativoService.BuscarAtivo(transacao.AtivoId);

            if (ativo == null)
                throw new Exception("Não foi possível realizar o cadastro da transação, ativo não localizado.");

            var portifolio = await _portifolioService.BuscarPortifolio(transacao.PortfolioId);

            if (portifolio == null)
                throw new Exception("Não foi possível realizar o cadastro da transação, portifolio não localizado.");

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
