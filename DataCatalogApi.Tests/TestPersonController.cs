using DataCatalogApi.Controllers;
using DataCatalogApi.Models;
using DataCatalogCommon.Data;
using DataCatalogCommon.Domain.Objects;
using NUnit.Framework;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace DataCatalogApi.Tests
{
    [TestFixture]
    public class TestPersonController
    {
        PeopleData _data;
        PersonModel _testPerson1;
        PersonModel _testPerson2;
        PersonModel _testPerson3;
        PersonModel _testPerson4;

        [OneTimeSetUp]
        public void Init()
        {
            _data = PeopleData.Instance;
            _testPerson1 = new PersonModel(
                "Mary",
                "Marie",
                "Female",
                "Blue",
                 "01/01/2001"
            );
            _data.Add(ModelToDomainObjectFactory.CreatePersonFromModel(_testPerson1));

            _testPerson2 = new PersonModel(
                "Tommy",
                "Toons",
                "Male",
                "Red",
                "12/31/2000"
            );
            _data.Add(ModelToDomainObjectFactory.CreatePersonFromModel(_testPerson2));

            _testPerson3 = new PersonModel(
                "Jenny",
                "Juniper",
                "Female",
                "White",
                "03/12/2001"
            );
            _data.Add(ModelToDomainObjectFactory.CreatePersonFromModel(_testPerson3));

            _testPerson4 = new PersonModel(
                "Mikey",
                "Minute",
                "Male",
                "Black",
                "07/07/2003"
            );
            _data.Add(ModelToDomainObjectFactory.CreatePersonFromModel(_testPerson4));
        }

        [Test]
        public void TestGetAllPeople()
        {
            PersonController controllerUnderTest = new PersonController();
            IHttpActionResult result = controllerUnderTest.GetAll("");
            var contentResult = result as OkNegotiatedContentResult<IQueryable<Person>>;
            Assert.AreEqual(_data.GetData().Count(), contentResult.Content.Count());
        }

        [Test]
        public void TestGetPeopleByGenderAndLastName()
        {
            PersonController controllerUnderTest = new PersonController();
            IHttpActionResult result = controllerUnderTest.GetAll("Gender,-LastName");
            var contentResult = result as OkNegotiatedContentResult<IQueryable<Person>>;
            IQueryable<Person> expected = _data.GetData()
                                                .OrderBy(p => p.Gender)
                                                .ThenByDescending(p => p.LastName);

            Assert.That(contentResult.Content, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetPeopleByBirthDateAsc()
        {
            PersonController controllerUnderTest = new PersonController();
            IHttpActionResult result = controllerUnderTest.GetAll("BirthDate");
            var contentResult = result as OkNegotiatedContentResult<IQueryable<Person>>;
            IQueryable<Person> expected = _data.GetData()
                                                .OrderBy(p => p.BirthDate);

            Assert.That(contentResult.Content, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetPeopleByLastNameDesc()
        {
            PersonController controllerUnderTest = new PersonController();
            IHttpActionResult result = controllerUnderTest.GetAll("-LastName");
            var contentResult = result as OkNegotiatedContentResult<IQueryable<Person>>;
            IQueryable<Person> expected = _data.GetData()
                                                .OrderByDescending(p => p.LastName);

            Assert.That(contentResult.Content, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetInvalidSearchParameterNoFields()
        {
            PersonController controllerUnderTest = new PersonController();
            IHttpActionResult result = controllerUnderTest.GetAll("-");
            var contentResult = result as OkNegotiatedContentResult<IQueryable<Person>>;
            // Will return with no sort so no sort applied to the expected
            IQueryable<Person> expected = _data.GetData();

            Assert.That(contentResult.Content, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetInvalidSearchParameterMissingFields()
        {
            PersonController controllerUnderTest = new PersonController();
            IHttpActionResult result = controllerUnderTest.GetAll("-MiddleName,City");
            var contentResult = result as OkNegotiatedContentResult<IQueryable<Person>>;
            // Will return with no sort so no sort applied to the expected
            IQueryable<Person> expected = _data.GetData();

            Assert.That(contentResult.Content, Is.EqualTo(expected));
        }

        [Test]
        public void TestPostPerson()
        {
            PersonController controllerUnderTest = new PersonController();
            PersonModel model = new PersonModel(
                "PostFirst",
                "PostLast",
                "Male",
                "Blue",
                "01/01/2012"
            );
            Person expected = ModelToDomainObjectFactory.CreatePersonFromModel(model);
            IHttpActionResult actual = controllerUnderTest.PostPerson(model);
            CollectionAssert.Contains(_data.GetData(), expected);
        }

        [Test]
        public void TestPostInvalidModelDate()
        {
            PersonController controllerUnderTest = new PersonController();
            PersonModel model = new PersonModel(
                "PostFirst",
                "PostLast",
                "Male",
                "Blue",
                "January 1st 2012"
            );
            IHttpActionResult actual = controllerUnderTest.PostPerson(model);
            Assert.IsInstanceOf<BadRequestErrorMessageResult>(actual);
        }

        [Test]
        public void TestPostInvalidModelGender()
        {
            PersonController controllerUnderTest = new PersonController();
            PersonModel model = new PersonModel(
                "PostFirst",
                "PostLast",
                "Femalee",
                "Blue",
                "10/12/1997"
            );
            IHttpActionResult actual = controllerUnderTest.PostPerson(model);
            Assert.IsInstanceOf<BadRequestErrorMessageResult>(actual);
        }
    }
}
