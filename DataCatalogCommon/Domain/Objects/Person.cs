using DataCatalogCommon.Domain.Enums;
using DataCatalogCommon.Domain.Interfaces;
using System;

namespace DataCatalogCommon.Domain.Objects
{
    /// <summary>
    /// Person data object containing properties and methods
    /// associated with a person for use across all the projects within 
    /// the Data Catalog application.
    /// </summary>
    public class Person : IDataObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string FavoriteColor { get; set; }

        public override bool Equals(object obj)
        {
            try
            {
                Person personObj = obj as Person;
                if (personObj.FirstName == FirstName
                    && personObj.LastName == LastName
                    && personObj.Gender == Gender
                    && personObj.BirthDate == BirthDate
                    && personObj.FavoriteColor == FavoriteColor)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(InvalidCastException excp)
            {
                // Call the base object Equals method if the object can't be cast to a Person
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
