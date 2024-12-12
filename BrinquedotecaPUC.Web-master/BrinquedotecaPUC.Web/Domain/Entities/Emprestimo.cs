namespace BrinquedotecaPUC.Web.Domain.Entities
{
    public class Emprestimo : EntityBase
    {
        public int IdPedido { get; set; }
        public int? idCliente { get; set; }
        public string? CodUsuario { get; set; }
        public int? Status { get; set; }
        public DateTime? DataEmprestimo { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<DetalheEmprestimo> DetalhesEmprestimo { get; set; }
    }
}
