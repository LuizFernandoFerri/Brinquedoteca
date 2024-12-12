using BrinquedotecaPUC.Web.CrossCutting.Enumerators;

namespace BrinquedotecaPUC.Web.Models
{
    public class MensagemAlertaViewModel
    {
        public string Mensagem { get; set; }
        public TipoAlerta Tipo { get; set; }
    }
}
