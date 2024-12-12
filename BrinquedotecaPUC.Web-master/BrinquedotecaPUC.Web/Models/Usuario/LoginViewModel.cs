using System.ComponentModel.DataAnnotations;


namespace BrinquedotecaPUC.Web.Models.Usuario
{
    public class LoginViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Código do usuário ou e-mail, é obrigatório.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A quantidade mínima é 6 caracteres para a senha.")]
        [MaxLength(8, ErrorMessage = "A quantidade máxima é 8 caracteres para a senha.")]
        public string? Password { get; set; }
    }
}
