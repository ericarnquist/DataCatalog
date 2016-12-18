using DataCatalogCommon.Domain.Factory;
using DataCatalogCommon.Domain.Objects;
using NUnit.Framework;
using System;
using System.Linq;

namespace DataCatalogCommon.Tests
{
    [TestFixture]
    public class TestDataObjectFactory
    {
        [Test]
        public void TestCreatePersonValidData()
        {
            string[] arrFields = new string[5];
            arrFields[0] = "TestFirst1";
            arrFields[1] = "TestFirst2";
            arrFields[2] = "Male";
            arrFields[3] = "Black";
            arrFields[4] = "12/12/1912";
            Person expected = new Person
            {
                FirstName = "TestFirst1",
                LastName = "TestFirst2",
                Gender = Domain.Enums.Gender.Male,
                FavoriteColor = "Black",
                BirthDate = new DateTime(1912, 12, 12)
            };
            Person actual = DataObjectFactory.CreatePerson(arrFields);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCreatePersonTooManyFields()
        {
            string[] arrFields = new string[typeof(Person).GetProperties().Count()+1];
            arrFields[0] = "TestFirst1";
            arrFields[1] = "TestFirst2";
            arrFields[2] = "Male";
            arrFields[3] = "Black";
            arrFields[4] = "12/12/1912";
            arrFields[5] = "TestMiddle";
            Assert.Throws<ApplicationException>(() => DataObjectFactory.CreatePerson(arrFields));
        }

        [Test]
        public void TestCreatePersonNotEnoughFields()
        {
            string[] arrFields = new string[typeof(Person).GetProperties().Count()-1];
            arrFields[0] = "TestFirst1";
            arrFields[1] = "TestFirst2";
            arrFields[2] = "Male";
            arrFields[3] = "Black";
            Assert.Throws<ApplicationException>(() => DataObjectFactory.CreatePerson(arrFields));
        }

        [Test]
        public void TestCreatePersonInvalidGender()
        {
            string[] arrFields = new string[typeof(Person).GetProperties().Count()];
            arrFields[0] = "TestFirst1";
            arrFields[1] = "TestFirst2";
            arrFields[2] = "Mail"; // Must be Male or Female
            arrFields[3] = "Black";
            arrFields[4] = "12/12/1912";
            Assert.Throws<ApplicationException>(() => DataObjectFactory.CreatePerson(arrFields));
        }

        [Test]
        public void TestCreatePersonInvalidBirthDateMonth()
        {
            string[] arrFields = new string[typeof(Person).GetProperties().Count()];
            arrFields[0] = "TestFirst1";
            arrFields[1] = "TestFirst2";
            arrFields[2] = "Male"; 
            arrFields[3] = "Black";
            arrFields[4] = "1/12/1913"; // Must be MM/dd/yyyy
            Assert.Throws<ApplicationException>(() => DataObjectFactory.CreatePerson(arrFields));
        }

        [Test]
        public void TestCreatePersonInvalidBirthDateDay()
        {
            string[] arrFields = new string[typeof(Person).GetProperties().Count()];
            arrFields[0] = "TestFirst1";
            arrFields[1] = "TestFirst2";
            arrFields[2] = "Male";
            arrFields[3] = "Black";
            arrFields[4] = "12/1/1913"; // Must be MM/dd/yyyy
            Assert.Throws<ApplicationException>(() => DataObjectFactory.CreatePerson(arrFields));
        }

        [Test]
        public void TestCreatePersonInvalidBirthDateYear()
        {
            string[] arrFields = new string[typeof(Person).GetProperties().Count()];
            arrFields[0] = "TestFirst1";
            arrFields[1] = "TestFirst2";
            arrFields[2] = "Male";
            arrFields[3] = "Black";
            arrFields[4] = "12/01/191"; // Must be MM/dd/yyyy
            Assert.Throws<ApplicationException>(() => DataObjectFactory.CreatePerson(arrFields));
        }
    }
}
