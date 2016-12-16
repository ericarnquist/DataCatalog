using DataCatalogCommon.Domain.Objects;
using DataCatalogCommon.Domain.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataCatalogApi.Models
{
    public static class ModelToDomainObjectFactory
    {
        public static Person CreatePersonFromModel(PersonModel model)
        {
            try
            {
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