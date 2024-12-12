using BrinquedotecaPUC.Web.Domain.Interfaces;
using BrinquedotecaPUC.Web.Domain.Interfaces.Repositories;
using BrinquedotecaPUC.Web.Domain.Services;
using BrinquedotecaPUC.Web.Infra.Data;
using BrinquedotecaPUC.Web.Infra.Data.Repositories;

namespace BrinquedotecaPUC.Web.IoC
{
    public static class InjectionDependency
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Services
            services.AddScoped<IUsuariosService, UsuariosService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IEmprestimoService, EmprestimoService>();

            // Repositories
            services.AddScoped<IClienteRepositoryLeitura, ClienteRepositoryLeitura>();
            services.AddScoped<IClienteRepositoryEscrita, ClienteRepositoryEscrita>();
            services.AddScoped<IUsuarioRepositoryLeitura, UsuarioRepositoryLeitura>();
            services.AddScoped<IUsuarioRepositoryEscrita, UsuarioRepositoryEscrita>();
            services.AddScoped<IProdutoRepositoryLeitura, ProdutoRepositoryLeitura>();
            services.AddScoped<IProdutoRepositoryEscrita, ProdutoRepositoryEscrita>();
            services.AddScoped<IEmprestimoRepositoryEscrita, EmprestimoRepositoryEscrita>();
            services.AddScoped<IEmprestimoRepositoryLeitura, EmprestimoRepositoryLeitura>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            // Services and Repositories base
            services.AddSingleton(typeof(IServiceBase<>), typeof(ServiceBase<>));
            services.AddSingleton(typeof(IRepositoryLeituraBase<>), typeof(RepositoryLeituraBase<>));
            services.AddSingleton(typeof(IRepositoryEscritaBase<>), typeof(RepositoryEscritaBase<>));

            return services;
        }
    }
}
