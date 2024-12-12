using BrinquedotecaPUC.Web.Domain.ValueObjects;
using BrinquedotecaPUC.Web.Domain.Interfaces;
using BrinquedotecaPUC.Web.Domain.Interfaces.Repositories;
using BrinquedotecaPUC.Web.Domain.Entities;
using Microsoft.Extensions.Options;

namespace BrinquedotecaPUC.Web.Domain.Services
{
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : EntityBase
    {

        protected readonly IRepositoryLeituraBase<TEntity> _LeituraRepository;
        protected readonly IRepositoryEscritaBase<TEntity> _EscritaRepository;
        

        public ServiceBase(IRepositoryLeituraBase<TEntity> leituraRepository, IRepositoryEscritaBase<TEntity> escritaRepository)
        {
            _LeituraRepository = leituraRepository;
            _EscritaRepository = escritaRepository;
        }

        public virtual TEntity Add(TEntity obj)
        {
            obj = ValidateToSave(obj);

            if (obj.ResultValidation.IsValid)
                _EscritaRepository.Add(obj);

            return obj;
        }

        public virtual TEntity Update(TEntity obj)
        {
            obj = ValidateToSave(obj);

            if (obj.ResultValidation.IsValid)
                _EscritaRepository.Update(obj);

            return obj;
        }

        public virtual TEntity ValidateToSave(TEntity obj)
        {
            obj.ResultValidation = new ValidationResult();
            return obj;
        }

        public virtual ValidationResult Remove(TEntity obj)
        {
            var resultValidation = ValidateToDelete(obj);

            if (resultValidation.IsValid)
            {
                try
                {
                    _EscritaRepository.Remove(obj);
                }
                catch (Exception ex)
                {
                    if (resultValidation == null)
                        resultValidation = new ValidationResult();
                    resultValidation.AdicionarErro(new ValidationError(ex.Message));
                }
            }
            return resultValidation;
        }

        public virtual ValidationResult ValidateToDelete(TEntity obj)
        {
            obj.ResultValidation = new ValidationResult();
            return obj.ResultValidation;
        }

        public void Dispose()
        {
            _EscritaRepository.Dispose();
            _LeituraRepository.Dispose();
        }
    }
}
