using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace DataCatalogCommon.Validators
{
    public abstract class AbstractValidator : IValidator
    {
        protected Logger logger = LogManager.GetCurrentClassLogger();
        protected bool HandleInvalidObjectTypeException(Exception excp)
        {
            logger.Error("The file path " + ObjectToValidate.ToString() + " provided was not valid");
            logger.Error(excp.Message);
            Message = "The path provided was of an unsupported type please provide a different type of path";
            return false;
        }

        public object ObjectToValidate { get; set; }

        public string Message { get; set; }
        public abstract bool Validate();

    }
}
