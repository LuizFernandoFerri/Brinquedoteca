using System.ComponentModel.DataAnnotations;

namespace BrinquedotecaPUC.Web.Models.Usuario
{
    public class UsuarioGridViewModel : BaseViewModel
    {
        public string? CodUsuario { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public bool? Situacao { get; set; }
        public bool? SenhaInvalida { get; set; }
        public bool? IsUpdate { get; set; } = false;
        public bool? IsDelete { get; set; } = false;
    }
}
