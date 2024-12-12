namespace BrinquedotecaPUC.Web.Domain.Entities
{
    public class Produto : EntityBase
    {
        public int? IdProduto { get; set; }
        public string? CodProduto { get; set; }
        public string? Descricao { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdFaixaEtaria { get; set; }
        public int? IdEstConservacao { get; set; }
        public int? Quantidade { get; set; }
        public int? Status { get; set; }
        public string? UrlImagem { get; set; }
        public string? Observacao { get; set; }

        public virtual Categoria? Categoria { get; set; } = new Categoria();
        public virtual EstadoConservacao? EstadoConservacao { get; set; } = new EstadoConservacao();
        //public virtual DetalheEmprestimo DetalheEmprestimo { get; set; } = new DetalheEmprestimo();
    }
}
