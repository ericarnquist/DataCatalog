using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCatalogCommon.Validators
{
    /// <summary>
    /// Validator that determines if a string provided matches against
    /// any of the characters provided to this implementation at 
    /// construction.
    /// </summary>
    public class MatchValidator : AbstractValidator
    {
        private string _characters;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="charactersToMatch"></param>
        public MatchValidator(string charactersToMatch)
        {
            _characters = charactersToMatch;
        }

        /// <summary>
        /// Validation gets the character to match and uses built in
        /// string functions to determine if it exists.
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            try
            {
                // Cast the object, handle exception below
                string characterToMatch = (string) ObjectToValidate;

                // Only support a single character to match
                if(characterToMatch.Length > 1)
                {
                    Message = "The character provided was too long, only single characters are allowed";
                    return false;
                }

                // Check for the index of the character and respond
                if(_characters.IndexOf(characterToMatch) > -1)
                {
                    return true;
                }
                else
                {
                    Message = "The character provided was not found, provide a valid character";
                    return false;
                }
            }
            catch(InvalidCastException cExcp)
            {
                // Handle an unsupported object
                return HandleInvalidObjectTypeException(cExcp);
            }
        }
    }
}
