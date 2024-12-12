namespace BrinquedotecaPUC.Web.Domain.Entities
{
    public class DetalheEmprestimo : EntityBase
    {
        public int IdDetalhePedido { get; set; }
        public int IdPedido { get; set; }
        public int IdProduto { get; set; }
        public DateTime DataPedido { get; set; }
        public DateTime DataDevolucao { get; set; }
        public int Status { get; set; }

        public virtual Emprestimo Emprestimo { get; set; }
        //public virtual Produto Produto { get; set; }
    }
}
