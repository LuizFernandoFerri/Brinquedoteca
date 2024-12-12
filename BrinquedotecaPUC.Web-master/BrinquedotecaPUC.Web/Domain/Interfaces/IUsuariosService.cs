using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Models.Usuario;

namespace BrinquedotecaPUC.Web.Domain.Interfaces
{
    public interface IUsuariosService : IServiceBase<Usuario>
    {
        UsuarioViewModel SalvarUsuario(UsuarioViewModel usuarioViewModel);
        Task<List<UsuarioGridViewModel>?> GetAllUsers();
        Task<UsuarioViewModel>? GetByKeyReadOnly(string codUsuario);
        Task<UsuarioViewModel?> Login(string userName, string userPassaword);
    }
}
