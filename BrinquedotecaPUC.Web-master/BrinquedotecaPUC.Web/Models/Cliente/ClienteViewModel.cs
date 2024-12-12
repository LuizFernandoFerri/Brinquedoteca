using BrinquedotecaPUC.Web.CrossCutting.Enumerators;

namespace BrinquedotecaPUC.Web.Models.Cliente
{
    public class ClienteViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string? CodUsuario { get; set; }
        public string? Nome { get; set; }
        private string? _cpf;
        public string? CpfFormatado { get; set; }
        public string? Cpf
        {
            get => _cpf;
            set
            {
                _cpf = value;
                CpfFormatado = value?.Insert(3, ".").Insert(7, ".").Insert(11, "-");
            }
        }
        public DateTime? DataNascimento { get; set; }
        public string? Telefone { get; set; }
        public string? Celular { get; set; }
        public string? Cep { get; set; }
        public string? Logradouro { get; set; }
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public bool? Situacao { get; set; }

        #region .: Propriedades da entidade :.

        public bool IsEdit { get; set; } = false;

        #endregion
    }
}
