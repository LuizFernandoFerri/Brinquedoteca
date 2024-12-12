namespace BrinquedotecaPUC.Web.Domain.Entities
{
    public class EstadoConservacao //: EntityBase
    {
        public int IdEstConservacao { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();

    }
}
