using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OwnerPasswordCredentialsGrant.Controllers
{
    [RoutePrefix("api/v1/test")]
    [Authorize]
    public class TestController : ApiController
    {
        [Route("value")]
        public string GetValue()
        {
            return "123";
        }
    }
}
