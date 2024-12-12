using BrinquedotecaPUC.Web.Domain.Entities;

namespace BrinquedotecaPUC.Web.Domain.Interfaces.Repositories
{
    public interface IEmprestimoRepositoryLeitura : IRepositoryLeituraBase<Emprestimo>
    {
        List<Emprestimo> ListarEmprestimos(int pageNumber, int pageSize, string codUsuario, int? idEmprestimo, DateTime? dataEmprestimo, string codProduto, string descricao);
    }
}
