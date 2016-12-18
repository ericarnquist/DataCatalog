using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace DataCatalogCommon.Validators
{
    /// <summary>
    /// Abstract implementation of the IValidator interface which
    /// includes base functionality for reuse across concrete
    /// implementations.
    /// </summary>
    public abstract class AbstractValidator : IValidator
    {
        protected Logger logger = LogManager.GetCurrentClassLogger();
        protected bool HandleInvalidObjectTypeException(Exception excp)
        {
            logger.Error("The type of object " + ObjectToValidate.ToString() + " provided was not valid");
            logger.Error(excp.Message);
            Message = "The path provided was of an unsupported type please provide a different type of path";
            return false;
        }

        // Properties
        public object ObjectToValidate { get; set; }
        public string Message { get; set; }
        
        // Abstract methods to be implemented by concrete classes
        public abstract bool Validate();
    }
}
