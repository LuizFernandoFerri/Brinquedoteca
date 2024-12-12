using BrinquedotecaPUC.Web.Domain.Entities;

namespace BrinquedotecaPUC.Web.Domain.Interfaces.Repositories
{
    public interface IProdutoRepositoryLeitura : IRepositoryLeituraBase<Produto>
    {
        List<Produto> ProdutoList(int pageNumber, int pageSize, string codProduto, string descricao, int? idEstConservao, int? idProduto);
        Produto? GetProductByKey(int idProduto);
    }
}
