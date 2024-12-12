using BrinquedotecaPUC.Web.CrossCutting;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BrinquedotecaPUC.Web.ValidationAttibutes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CNPJAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            return value != null && UtilsClient.IsValidCNPJ(value.ToString());
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && UtilsClient.IsValidCNPJ(value.ToString()))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(this.ErrorMessage);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule mvr = new ModelClientValidationRule();
            mvr.ErrorMessage = this.ErrorMessage;
            mvr.ValidationType = "cnpj";
            return new[] { mvr };
        }
    }
}
