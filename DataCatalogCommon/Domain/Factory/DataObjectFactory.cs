using DataCatalogCommon.Common;
using DataCatalogCommon.Domain.Enums;
using DataCatalogCommon.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCatalogCommon.Domain.Factory
{
    public static class DataObjectFactory
    {
        public static Person CreatePerson(string firstName, string lastName, Gender gender, string favoriteColor, DateTime birthDate)
        {
            Person person = new Person();
            person.FirstName = firstName;
            person.LastName = lastName;
            person.Gender = gender;
            person.FavoriteColor = favoriteColor;
            person.BirthDate = birthDate;
            return person;
        }

        public static Person CreatePerson(string[] fields)
        {
            Person person = new Person();

            //Check the number of fields matches the object we're populating
            if (fields.Length != typeof(Person).GetProperties().Count())
            {
                throw new ApplicationException("The record provided does not contain the right amount of fields");
            }

            //Validate the gender can be found
            Gender gender;
            if (!Enum.TryParse(fields[2], out gender))
            {
                throw new ApplicationException("The gender provided for the record is not valid");
            }
            person.Gender = gender;

            //Validate the birth date can be parsed
            DateTime birthDate;

            if(!DateTime.TryParseExact(fields[4], DataCatalogApplicationConstants.STANDARD_ACCEPTABLE_DATE_FORMAT, 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate))
            {
                throw new ApplicationException("The birth date supplied for the record does not follow the valid format: " 
                    + DataCatalogApplicationConstants.STANDARD_ACCEPTABLE_DATE_FORMAT);
            }
            person.BirthDate = birthDate;

            //Add the additional string fields
            person.FirstName = fields[0];
            person.LastName = fields[1];
            person.FavoriteColor = fields[3];

            return person;
        }
    }
}
