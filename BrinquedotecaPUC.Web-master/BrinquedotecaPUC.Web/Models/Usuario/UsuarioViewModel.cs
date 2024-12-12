using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BrinquedotecaPUC.Web.Models.Usuario
{
    public class UsuarioViewModel : BaseViewModel
    {
        public string? CodUsuario { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(3, ErrorMessage = "A quantidade mínima é 3 caracteres para o nome")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Informe um e-mail válido")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage =
            "A senha deve conter pelo menos 8 caracteres, incluindo pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial.")]
        
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public bool? Situacao { get; set; }
        public bool? SenhaInvalida { get; set; }
        public bool? IsEdit { get; set; } = false;
        public bool? IsDelete { get; set; } = false;
    }
}
