using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCatalogCommon.Validators
{
    public class MatchValidator : AbstractValidator
    {
        private string _characters;
        public MatchValidator(string charactersToMatch)
        {
            _characters = charactersToMatch;
        }

        public override bool Validate()
        {
            try
            {
                string characterToMatch = ObjectToValidate as string;
                if(characterToMatch.Length > 1)
                {
                    Message = "The character provided was too long, only single characters are allowed";
                    return false;
                }

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
                return HandleInvalidObjectTypeException(cExcp);
            }
        }
    }
}
