using DataCatalogCommon.Domain.Objects;
using DataCatalogCommon.Domain.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataCatalogApi.Models
{
    /// <summary>
    /// Class has factory methods used transfer model classes into
    /// domain classes for use by the back end application.
    /// </summary>
    public static class ModelToDomainObjectFactory
    {
        /// <summary>
        /// Creates a Person domain object based on the model passed into it. This method
        /// will catch and rethrow an ApplicationException if there are any data format
        /// issues with the model being passed in.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Person CreatePersonFromModel(PersonModel model)
        {
            try
            {
                // Convert the model into an array and pass into the object factory
                string[] arrFields = new string[model.GetType().GetProperties().Length];
                arrFields[0] = model.FirstName;
                arrFields[1] = model.LastName;
                arrFields[2] = model.Gender;
                arrFields[3] = model.FavoriteColor;
                arrFields[4] = model.BirthDate;
                return DataObjectFactory.CreatePerson(arrFields);
            }
            catch (ApplicationException aExcp)
            {
                throw new ApplicationException("Error occurred converting PersonModel to Person", aExcp);
            }
        }
    }
}