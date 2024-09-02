using GestaoInvestimentos.Domain.Enums;

namespace GestaoInvestimentos.Application.DTOs.Usuario.Request
{
    public class UsuarioRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public EnumRoles Role { get; set; }

    }
}
