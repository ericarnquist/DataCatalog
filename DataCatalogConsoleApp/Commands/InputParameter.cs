using System;
using System.Collections.Generic;
using DataCatalogCommon.Validators;

namespace DataCatalogConsoleApp.Commands
{
    /// <summary>
    /// Base implementation of the IInputParameter interface. Additional
    /// implementations can be created if different functionality is
    /// required by the console application.
    /// </summary>
    public class InputParameter : IInputParameter
    {
        /// <summary>
        /// Default constructor taking in the only required field
        /// as a parameter.
        /// </summary>
        /// <param name="prompt"></param>
        public InputParameter(string prompt)
        { 
            Prompt = prompt;
        }

        // Base properties
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

        /// <summary>
        /// Adds the validation item to the collection of 
        /// validations for the parameter.
        /// </summary>
        /// <param name="validation"></param>
        public void AddValidation(IValidator validation)
        {
            this.Validations.Add(validation);
        }

        /// <summary>
        /// Gets the parameter from the user.
        /// </summary>
        /// <returns>true - valid, false - invalid</returns>
        public bool GetParameter()
        {
            // Write out a prompt to the user and then collect the input
            Console.WriteLine(Prompt);
            Input = Console.ReadLine();

            // Run each validation and ensure it passes
            foreach(var validation in Validations)
            {
                //Add the input to the validation and run it
                validation.ObjectToValidate = Input;
                if(!validation.Validate())
                {
                    Console.WriteLine("The parameter entered was invalid because: " + validation.Message);
                    return false;
                }
            }
            return true;
        }
    }
}
