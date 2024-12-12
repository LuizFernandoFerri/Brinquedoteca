namespace BrinquedotecaPUC.Web.Domain.Entities
{
    public class Cliente : EntityBase
    {
        public int Id { get; set; }
        public string? CodUsuario { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? Telefone { get; set; }
        public string? Celular { get; set; }
        public string? Cep { get; set; }
        public string? Logradouro { get; set; }
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public bool Situacao { get; set; }

        public virtual ICollection<Emprestimo> Emprestimos { get; set; }
    }
}
