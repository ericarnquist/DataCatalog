using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCatalogCommon.Validators
{
    /// <summary>
    /// Contract specification that all validator implementations
    /// should follow.
    /// </summary>
    public interface IValidator
    {
        #region Properties

        /// <summary>
        /// Message that can be set and used to display information back to the
        /// caller, mainly in the case when validation does not pass.
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// An object that is run through the vaidation routine.
        /// </summary>
        object ObjectToValidate { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Validation method which returns true or false based on the outcome.
        /// </summary>
        /// <returns></returns>
        bool Validate();
        
        #endregion
    }
}
