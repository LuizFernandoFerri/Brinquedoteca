using BrinquedotecaPUC.Web.Application.Validation;
using BrinquedotecaPUC.Web.Domain.ValueObjects;

namespace BrinquedotecaPUC.Web.Application.AppService
{
    public class ApplicationService
    {
        protected ValidationAppResult DomainToApplicationResult(ValidationResult result)
        {
            var validationAppResult = new ValidationAppResult();

            foreach (var validationError in result.Erros)
            {
                validationAppResult.Erros.Add(new ValidationAppError(validationError.Message));
            }
            validationAppResult.IsValid = result.IsValid;
            validationAppResult.Mensagem = result.Mensagem;

            return validationAppResult;
        }
    }
}
