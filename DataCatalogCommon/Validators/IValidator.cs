using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCatalogCommon.Validators
{
    public interface IValidator
    {
        string Message { get; set; }
        bool Validate();
        object ObjectToValidate { get; set; }
    }
}
