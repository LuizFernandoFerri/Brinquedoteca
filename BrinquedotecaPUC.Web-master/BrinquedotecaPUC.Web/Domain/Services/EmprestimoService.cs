using AutoMapper;
using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Domain.Interfaces;
using BrinquedotecaPUC.Web.Domain.Interfaces.Repositories;

namespace BrinquedotecaPUC.Web.Domain.Services
{
    public class EmprestimoService : ServiceBase<Emprestimo>, IEmprestimoService
    {
        private readonly IEmprestimoRepositoryEscrita _repositoryEscrita;
        private readonly IEmprestimoRepositoryLeitura _repositoryLeitura;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmprestimoService(IEmprestimoRepositoryLeitura repositoryLeitura, IEmprestimoRepositoryEscrita repositoryEscrita,
            IUnitOfWork unitOfWork, IMapper mapper) : base(repositoryLeitura, repositoryEscrita)
        {
            _repositoryLeitura = repositoryLeitura;
            _repositoryEscrita = repositoryEscrita;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        
    }
}
