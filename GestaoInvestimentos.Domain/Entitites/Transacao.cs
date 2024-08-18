using GestaoInvestimentos.Domain.Enums;

namespace GestaoInvestimentos.Domain.Entitites
{
    public class Transacao : Base
    {
        public int PortfolioId { get; set; }
        public int AtivoId { get; set; }
        public EnumTipoTransacao TipoTransacao { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }
        public DateTime DataTransacao { get; set; }

    }
}
