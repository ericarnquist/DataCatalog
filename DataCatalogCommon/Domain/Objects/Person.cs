using DataCatalogCommon.Domain.Enums;
using DataCatalogCommon.Domain.Interfaces;
using System;

namespace DataCatalogCommon.Domain.Objects
{
    public class Person : IDataObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string FavoriteColor { get; set; }
    }
}
