using System;
using System.IO;

namespace DataCatalogCommon.Validators
{
    /// <summary>
    /// Implementation of the IValidator class which takes a 
    /// file path and determines if it is a valid and accessible 
    /// path for the local machine that this running on.
    /// </summary>
    /// <typeparam name="T">the type of the path to validate</typeparam>
    public class FilePathValidator : AbstractValidator
    {
        //Default Constructor
        public FilePathValidator()
        {

        }

        public FilePathValidator(string pathToValidate)
        {
            ObjectToValidate = pathToValidate;
        }

        public override bool Validate()
        {
            string _path = "";

            try
            {
                _path = ObjectToValidate as string;

                string fullPath = Path.GetFullPath(ObjectToValidate as string);
                if(!string.IsNullOrEmpty(fullPath))
                {
                    return true;
                }

                Message = "The path provided was null or empty";
                return false;
            }
            catch (InvalidCastException cExcp)
            {
                return HandleInvalidObjectTypeException(cExcp);
            }
            catch (Exception excp)
            {
                //TODO: Log the exception
                logger.Warn("The file path " + _path + " provided was not valid");
                logger.Warn(excp.Message);
                Message = "The path provided was invalid please provide a different path";
                return false;
            }
        }
    }
}
