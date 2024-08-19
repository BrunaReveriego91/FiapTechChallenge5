using GestaoInvestimentos.Application.DTOs.Usuario.Request;
using GestaoInvestimentos.Domain.Entitites;

namespace GestaoInvestimentos.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task CadastrarUsuario(UsuarioRequest usuario);
    } 
}
