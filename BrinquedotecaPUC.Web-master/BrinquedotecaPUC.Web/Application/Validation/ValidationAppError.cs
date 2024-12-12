using System.Collections.Generic;

namespace BrinquedotecaPUC.Web.Application.Validation
{
    public class ValidationAppError
    {
        public string Message { get; set; }
        public ValidationAppError(string message)
        {
            this.Message = message;
        }
    }
}
