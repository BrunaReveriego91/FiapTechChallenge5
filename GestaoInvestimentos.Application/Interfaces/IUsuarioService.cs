using GestaoInvestimentos.Domain.Entitites;

namespace GestaoInvestimentos.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task CadastrarUsuario(Usuario usuario);
    }
}
