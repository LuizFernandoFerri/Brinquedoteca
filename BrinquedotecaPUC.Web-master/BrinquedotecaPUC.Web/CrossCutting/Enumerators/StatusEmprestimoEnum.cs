using System.ComponentModel;

namespace BrinquedotecaPUC.Web.CrossCutting.Enumerators
{
    public enum StatusEmprestimoEnum
    {
        [Description("Atrasado")]
        Atrasado = 1,
        [Description("Cancelado")]
        Cancelado = 2,
        [Description("Devolvido")]
        Devolvido = 3,
        [Description("Emprestado")]
        Emprestado = 4,
        [Description("Reservado")]
        Reservado = 5,
        [Description("Aguardando Retirada")]
        AguardandoRetirada = 6,
        [Description("Disponível")]
        Disponivel = 7
    }
}
