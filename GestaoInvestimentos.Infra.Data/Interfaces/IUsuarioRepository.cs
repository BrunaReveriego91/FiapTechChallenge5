using GestaoInvestimentos.Domain.Entitites;

namespace GestaoInvestimentos.Infra.Data.Interfaces
{
    public interface IUsuarioRepository
    {
        Task CadastrarUsuario(Usuario usuario);
        Task<Usuario> BuscarUsuarioPorEmail(string email);
        Task DesabilitarUsuario(int idUsuario);
    }
}
