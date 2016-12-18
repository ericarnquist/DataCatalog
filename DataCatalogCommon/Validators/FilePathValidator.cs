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

        /// <summary>
        /// Constructor passing in a string path to validate.
        /// </summary>
        /// <param name="pathToValidate"></param>
        public FilePathValidator(string pathToValidate)
        {
            ObjectToValidate = pathToValidate;
        }

        /// <summary>
        /// Validation method which uses the IO Path object to determine
        /// if the passed in object is accessible on the local machine.
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            string _path = "";

            try
            {
                _path = (string)ObjectToValidate;

                // Get the full path, this will throw exceptions if invalid
                string fullPath = Path.GetFullPath(_path);
                if(!string.IsNullOrEmpty(fullPath))
                {
                    return true;
                }

                Message = "The path provided was null or empty";
                return false;
            }
            catch (InvalidCastException cExcp)
            {
                // Catch an exception of the object is not a string
                return HandleInvalidObjectTypeException(cExcp);
            }
            catch (Exception excp)
            {
                logger.Warn("The file path " + _path + " provided was not valid");
                logger.Warn(excp.Message);
                Message = "The path provided was invalid please provide a different path";
                return false;
            }
        }
    }
}
