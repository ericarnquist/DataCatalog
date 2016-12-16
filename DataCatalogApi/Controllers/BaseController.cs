using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DataCatalogApi.Controllers
{
    public class BaseController : ApiController
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
