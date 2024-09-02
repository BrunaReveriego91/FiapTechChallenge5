namespace GestaoInvestimentos.Domain.Entitites
{
    public class Portifolio: EntityBase
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
