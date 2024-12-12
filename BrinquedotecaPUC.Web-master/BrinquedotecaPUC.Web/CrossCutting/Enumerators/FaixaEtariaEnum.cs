using System.ComponentModel;

namespace BrinquedotecaPUC.Web.CrossCutting.Enumerators
{
    public enum FaixaEtariaEnum
    {
        [Description("0 a 2 anos")]
        ZeroADoisAnos = 1,
        [Description("3 a 5 anos")]
        TresACincoAnos = 2,
        [Description("6 a 8 anos")]
        SeisAOitoAnos = 3,
        [Description("9 a 12 anos")]
        NoveA12Anos = 4,
        [Description("13 anos ou mais")]
        TrezeAnosOuMais = 5,
    }
}
