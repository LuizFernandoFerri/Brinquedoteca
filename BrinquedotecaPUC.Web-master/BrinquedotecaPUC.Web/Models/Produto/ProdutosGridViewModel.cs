using BrinquedotecaPUC.Web.Domain.ValueObjects;

namespace BrinquedotecaPUC.Web.Models.Produto
{
    public class ProdutoGridViewModel : ProdutoFiltroViewModel
    {
        public List<ProdutoViewModel> Produtos { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalProdutos { get; set; }

        // Calcular o número total de páginas
        public int TotalPages => (int)Math.Ceiling((double)TotalProdutos / PageSize);

        // Determinar se há uma página anterior
        public bool HasPreviousPage => PageNumber > 1;

        // Determinar se há uma próxima página
        public bool HasNextPage => PageNumber < TotalPages;

        public virtual ValidationResult ResultValidadion { get; set; }
    }
}
