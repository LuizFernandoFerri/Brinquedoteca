namespace BrinquedotecaPUC.Web.Domain.Entities
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string NomeCategoria { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
