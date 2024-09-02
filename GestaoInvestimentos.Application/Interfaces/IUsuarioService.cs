using GestaoInvestimentos.Application.DTOs.Usuario.Request;
using GestaoInvestimentos.Domain.Entitites;

namespace GestaoInvestimentos.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<string> AutenticarUsuario(string email, string senha);
        Task CadastrarUsuario(UsuarioRequest usuario);
        Task<Usuario> BuscarUsuario(Guid id);
        Task<IEnumerable<Usuario>> ListarUsuario();
        Task RemoverUsuarioPorId(Guid id);
    }
}
