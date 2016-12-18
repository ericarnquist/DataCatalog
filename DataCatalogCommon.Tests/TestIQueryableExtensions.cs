using DataCatalogCommon.Domain.Objects;
using DataCatalogCommon.Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCatalogCommon.Tests
{
    [TestFixture]
    public class TestIQueryableExtensions
    {
        IQueryable<Person> _data;
        [OneTimeSetUp]
        public void Init()
        {
            List<Person> testData = new List<Person>();
            testData.Add(new Person
            {
                FirstName = "FirstTest1",
                LastName = "LastTest1",
                Gender = Domain.Enums.Gender.Male,
                FavoriteColor = "Blue",
                BirthDate = new DateTime(2010, 1, 1)
            });

            testData.Add(new Person
            {
                FirstName = "FirstTest2",
                LastName = "LastTest2",
                Gender = Domain.Enums.Gender.Female,
                FavoriteColor = "Blue",
                BirthDate = new DateTime(2007, 2, 12)
            });

            testData.Add(new Person
            {
                FirstName = "Mike",
                LastName = "Jones",
                Gender = Domain.Enums.Gender.Male,
                FavoriteColor = "Geen",
                BirthDate = new DateTime(1996, 12, 10)
            });

            testData.Add(new Person
            {
                FirstName = "Sally",
                LastName = "Smith",
                Gender = Domain.Enums.Gender.Female,
                FavoriteColor = "Yellow",
                BirthDate = new DateTime(1991, 3, 30)
            });

            _data = testData.AsQueryable(); 
        }

        [Test]
        public void TestNullSource()
        {
            IQueryable<Person> expected = null;
            string sortParameters = "FirstName,LastName";
            Assert.Throws<ArgumentNullException>(() => expected.SortByExpression(sortParameters));
        }

        [Test]
        public void TestEmptySortParameters()
        {
            string sortParameters = "";
            IQueryable<Person> actual = _data.SortByExpression(sortParameters);
            Assert.AreEqual(_data, actual);
        }

        [Test]
        public void TestNullSortParameters()
        {
            IQueryable<Person> actual = _data.SortByExpression(null);
            Assert.AreEqual(_data, actual);
        }

        [Test]
        public void TestSortDescending()
        {
            IQueryable<Person> actual = _data.SortByExpression("-FirstName");
            IQueryable<Person> expected = _data.OrderByDescending(p => p.FirstName);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestSortAscending()
        {
            IQueryable<Person> actual = _data.SortByExpression("FirstName");
            IQueryable<Person> expected = _data.OrderBy(p => p.FirstName);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMultipleSortParameters()
        {
            IQueryable<Person> actual = _data.SortByExpression("Gender,BirthDate");
            IQueryable<Person> expected = _data.OrderBy(p => p.Gender).ThenBy(p => p.BirthDate);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestSortByMissingField()
        {
            IQueryable<Person> actual = _data.SortByExpression("Gender,MiddleName");
            IQueryable<Person> expected = _data;
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
