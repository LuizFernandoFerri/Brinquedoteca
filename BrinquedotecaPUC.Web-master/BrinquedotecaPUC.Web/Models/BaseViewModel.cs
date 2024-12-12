using BrinquedotecaPUC.Web.Domain.ValueObjects;

namespace BrinquedotecaPUC.Web.Models
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            ResultValidation = new ValidationResult();
        }

        public string? RecCreatedBy { get; set; }

        public DateTime? RecCreatedOn { get; set; }

        public string? RecModifiedBy { get; set; }

        public DateTime? RecModifiedOn { get; set; }

        public virtual ValidationResult ResultValidation { get; set; }

    }
}
