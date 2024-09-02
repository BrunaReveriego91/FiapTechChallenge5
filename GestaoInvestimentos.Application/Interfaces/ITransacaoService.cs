using GestaoInvestimentos.Domain.Entitites;

namespace GestaoInvestimentos.Application.Interfaces
{
    public interface ITransacaoService
    {
        Task CadastrarTransacao(Transacao transacao);
        Task<IEnumerable<Transacao>> ListarTransacoes();        
        Task AlterarTransacao(Transacao transacao);
        Task RemoverTransacao(Guid id);
        Task<Transacao> BuscarTransacao(Guid id);
    }
}
