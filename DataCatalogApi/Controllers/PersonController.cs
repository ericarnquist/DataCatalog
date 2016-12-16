using DataCatalogCommon.Domain.Objects;
using DataCatalogCommon.Common;
using System.Web.Http;
using DataCatalogCommon.Data;
using System.Web.Http.Description;
using DataCatalogApi.Models;
using System;

namespace DataCatalogApi.Controllers
{
    /// <summary>
    /// Controller containing RESTful methods to get and post
    /// data about a Person or multiple people.
    /// </summary>
    public class PersonController : BaseController
    {
        /// <summary>
        /// Get all the people that are stored in the data store
        /// </summary>
        /// <param name="sort"></param>
        /// <returns>Sorted list of people in JSON</returns>
        public IHttpActionResult GetAll(string sort = "-FirstName")
        {
            var sortedPeople = PeopleData.Instance.GetData();
            sortedPeople = sortedPeople.SortByExpression(sort);
            return Ok(sortedPeople);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [ResponseType(typeof(Person))]
        public IHttpActionResult PostPerson(PersonModel personModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var person = ModelToDomainObjectFactory.CreatePersonFromModel(personModel);

                PeopleData.Instance.Add(person);

                return CreatedAtRoute("DefaultApi", null, personModel);
            }
            catch(ApplicationException aExcp)
            {
                logger.Error("Error occurred while saving the person in PersonController", aExcp);
                return BadRequest(string.Format("Error occurred while saving the Person due to {0}", aExcp.Message));
            }
        }
    }
}
