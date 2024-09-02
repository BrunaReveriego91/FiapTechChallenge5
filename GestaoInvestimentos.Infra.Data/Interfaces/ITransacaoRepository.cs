using GestaoInvestimentos.Domain.Entitites;

namespace GestaoInvestimentos.Infra.Data.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<IEnumerable<Transacao>> ListarTransacoes();
        Task CadastrarTransacao(Transacao transacao);
        Task<Transacao> BuscarTransacao(Guid id);
        Task AlterarTransacao(Transacao transacao);
        Task RemoverTransacao(Guid id);
    }
}
