using System.ComponentModel;

namespace BrinquedotecaPUC.Web.CrossCutting.Enumerators
{
    public enum StatusProduto
    {
        [Description("Disponível")]
        Disponivel = 1,
        [Description("Indisponível")]
        Indisponivel = 2,
        [Description("Desativado")]
        Desativado = 3
    }
}
