using GestaoInvestimentos.Domain.Enums;

namespace GestaoInvestimentos.Domain.Entitites
{
    public class Usuario : EntityBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public EnumRoles Role { get; set; }

    }
}
