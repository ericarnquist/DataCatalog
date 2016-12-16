using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCatalogCommon.Domain.Objects
{
    public class People
    {
        private List<Person> _items;
        public List<Person> Items
        {
            get
            {
                if(_items == null)
                {
                    _items = new List<Person>();
                }
                return _items;
            }
        }

        public void AddPerson(Person person)
        {
            Items.Add(person);
        }

        public List<Person> OrderByGender()
        {
            return new List<Person>(Items.OrderBy(p => p.Gender).OrderBy(p => p.LastName));
        }

        public List<Person> OrderByBirthDate()
        {
            return new List<Person>(Items.OrderBy(p => p.BirthDate));
        }

        public List<Person> OrderByLastName()
        {
            return new List<Person>(Items.OrderByDescending(p => p.LastName));
        }
    }
}
