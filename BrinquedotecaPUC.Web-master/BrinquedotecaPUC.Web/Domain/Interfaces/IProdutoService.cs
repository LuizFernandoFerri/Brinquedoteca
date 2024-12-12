using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Models.Produto;

namespace BrinquedotecaPUC.Web.Domain.Interfaces
{
    public interface IProdutoService : IServiceBase<Produto>
    {
        ProdutoViewModel GetProductById(int idProduto);
        ProdutoGridViewModel ProdutoList(int pageNumber, int pageSize, ProdutoFiltroViewModel filtroViewModel);
        ProdutoViewModel Salvar(ProdutoViewModel prodViewModel);
    }
}
