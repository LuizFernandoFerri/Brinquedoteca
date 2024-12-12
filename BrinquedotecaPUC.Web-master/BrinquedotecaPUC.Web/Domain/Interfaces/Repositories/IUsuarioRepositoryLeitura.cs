using BrinquedotecaPUC.Web.Domain.Entities;

namespace BrinquedotecaPUC.Web.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepositoryLeitura : IRepositoryLeituraBase<Usuario>
    {
        List<Usuario> ListarUsuarios();
        Usuario LocalizaUsuario(string userName, string userMail);
        Usuario? GetByKeyReadOnly(string codUsuario);
    }
}
