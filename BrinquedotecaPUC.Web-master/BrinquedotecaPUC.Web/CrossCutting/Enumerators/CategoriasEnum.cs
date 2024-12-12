using System.ComponentModel;

namespace BrinquedotecaPUC.Web.CrossCutting.Enumerators
{
    public enum CategoriasEnum
    {
        [Description("Educativos")]
        Educativos = 1,
        [Description("Criativos")]
        Criativos = 2,
        [Description("Interativos e Tecnológicos")]
        InterativosTecnologicos = 3,
        [Description("De Construção")]
        DeConstrucao = 4,
        [Description("Esportivos")]
        Esportivos = 5,
        [Description("Fantasias e Roupas")]
        FantasiasRoupas = 6,
        [Description("Jogos de Tabuleiro e Cartas")]
        JogosTabuleiroCartas = 7,
        [Description("Brinquedos de Imitação")]
        BrinquedosImitacao = 8,
        [Description("Ao ar livre")]
        ArLivre = 9,
    }
}
