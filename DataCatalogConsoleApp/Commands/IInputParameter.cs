using DataCatalogCommon.Validators;
using System.Collections.Generic;

namespace DataCatalogConsoleApp.Commands
{
    /// <summary>
    /// Contract specification for all Input Parameters which
    /// are used as part of a command to be made available through the
    /// console application.
    /// </summary>
    public interface IInputParameter
    {
        #region Properties
        
        /// <summary>
        /// Text to prompt the user during the input process.
        /// </summary>
        string Prompt { get; set; }

        /// <summary>
        /// Value of the input parameter received from the user.
        /// </summary>
        string Input { get; set; }

        /// <summary>
        /// A collection of validations which the input needs to
        /// pass when being collected in order to be valid input.
        /// </summary>
        IList<IValidator> Validations { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Add a new validation object to the list for use during
        /// the process for validating user supplied input.
        /// </summary>
        /// <param name="validation"></param>
        void AddValidation(IValidator validation);

        /// <summary>
        /// Prompts the user for the input and ensures it passes all
        /// provided validations.
        /// </summary>
        /// <returns></returns>
        bool GetParameter();
        
        #endregion
    }
}
