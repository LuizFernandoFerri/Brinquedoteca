using AutoMapper;
using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Models.Cliente;
using BrinquedotecaPUC.Web.Models.Produto;
using BrinquedotecaPUC.Web.Models.Usuario;

namespace BrinquedotecaPUC.Web.Profiles
{
    public class BrinquedotecaPUCProfiles : Profile
    {
        public BrinquedotecaPUCProfiles()
        {
            CreateMap<Usuario, UsuarioViewModel>();
            CreateMap<UsuarioViewModel, Usuario>();
            CreateMap<Usuario, UsuarioGridViewModel>();

            CreateMap<Cliente, ClienteViewModel>();
            CreateMap<ClienteViewModel, Cliente>();
            CreateMap<Cliente, ClienteGridViewModel>();

            CreateMap<Produto, ProdutoViewModel>();
            CreateMap<ProdutoViewModel, Produto>();
            CreateMap<Produto, ProdutoGridViewModel>();
        }
    }
}
