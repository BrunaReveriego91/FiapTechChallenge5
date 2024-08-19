using GestaoInvestimentos.Application.Interfaces;
using GestaoInvestimentos.Domain.Entitites;
using GestaoInvestimentos.Infra.Data.Interfaces;

namespace GestaoInvestimentos.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task CadastrarUsuario(Usuario usuario)
        {
            var usuarioExistente = await _usuarioRepository.BuscarUsuarioPorEmail(usuario.Email);

            if (usuarioExistente != null)
                throw new Exception("E-mail já está em uso.");

            string senhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

            var usuarioHash = new Usuario
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = senhaHash
            };

            await _usuarioRepository.CadastrarUsuario(usuarioHash);
        }
    }
}
