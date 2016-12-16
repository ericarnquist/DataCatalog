using DataCatalogCommon.Validators;
using System.Collections.Generic;

namespace DataCatalogConsoleApp.Commands
{
    public interface IInputParameter
    {
        string Prompt { get; set; }
        string Input { get; set; }
        IList<IValidator> Validations { get; }
        void AddValidation(IValidator validation);
        bool GetParameter();
    }
}
