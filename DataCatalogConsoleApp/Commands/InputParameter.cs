using System;
using System.Collections.Generic;
using DataCatalogCommon.Validators;

namespace DataCatalogConsoleApp.Commands
{
    public class InputParameter : IInputParameter
    {
        public InputParameter(string prompt)
        { 
            Prompt = prompt;
        }

        public string Prompt { get; set; }
        public string Input { get; set; }

        private IList<IValidator> _validations;
        public IList<IValidator> Validations
        {
            get
            {
                if(_validations == null)
                {
                    _validations = new List<IValidator>();
                }
                return _validations;
            }
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public void AddValidation(IValidator validation)
        {
            this.Validations.Add(validation);
        }

        public bool GetParameter()
        {
            Console.WriteLine(Prompt);
            Input = Console.ReadLine();
            foreach(var validation in Validations)
            {
                //Add the input to the validation and run it
                validation.ObjectToValidate = Input;
                if(!validation.Validate())
                {
                    Console.WriteLine("The parameter entered was not valid because: " + validation.Message);
                    return false;
                }
            }
            return true;
        }
    }
}
