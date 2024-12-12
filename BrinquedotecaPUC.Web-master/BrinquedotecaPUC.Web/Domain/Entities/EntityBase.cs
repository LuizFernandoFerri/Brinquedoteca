using BrinquedotecaPUC.Web.Domain.ValueObjects;

namespace BrinquedotecaPUC.Web.Domain.Entities
{
    public class EntityBase
    {
        public EntityBase()
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
