namespace GestaoInvestimentos.Domain.Entitites
{
    public class Portfolio: Base
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
