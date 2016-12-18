using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DataCatalogApi.Controllers
{
    /// <summary>
    /// Base controller that all other controllers should extend from. Includes
    /// basic logging facility.
    /// </summary>
    public class BaseController : ApiController
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
