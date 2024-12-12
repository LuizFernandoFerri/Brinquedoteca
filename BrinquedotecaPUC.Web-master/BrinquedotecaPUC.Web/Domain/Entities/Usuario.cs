namespace BrinquedotecaPUC.Web.Domain.Entities
{
    public class Usuario : EntityBase
    {
        public string CodUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Situacao { get; set; }
        public bool? SenhaInvalida { get; set; }
    }
}
