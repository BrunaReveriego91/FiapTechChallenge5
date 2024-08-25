using GestaoInvestimentos.Domain.Enums;

namespace GestaoInvestimentos.Domain.Entitites
{
    public class Ativo : EntityBase
    {
        public EnumTipoAtivo TipoAtivo { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
    }
}
