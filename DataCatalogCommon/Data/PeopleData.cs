using DataCatalogCommon.Domain.Enums;
using DataCatalogCommon.Domain.Objects;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace DataCatalogCommon.Data
{
    /// <summary>
    /// NOT FOR USE IN PRODUCTION SYSTEM
    /// This is a singleton class used to store data globally for testing purposes
    /// only between the DataCatalogApi and the DataCatalogConsoleApp. A typical
    /// production system would have a persistent data store on the backend which would 
    /// be accessed via the API.
    /// NOT FOR USE IN PRODUCTION SYSTEM
    /// </summary>
    public sealed class PeopleData
    {
        
        private static readonly PeopleData instance = new PeopleData();
        
        public static PeopleData Instance
        {
            get
            {
                return instance;
            }
        }

        private readonly ConcurrentBag<Person> _people = new ConcurrentBag<Person>();

        private PeopleData()
        {
        }

        public void Add(Person person)
        {
            _people.Add(person);
        }

        public IQueryable<Person> GetData()
        {
            return _people.AsQueryable();
        }
    }
}
