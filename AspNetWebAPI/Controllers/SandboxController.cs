using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWebAPI.Controllers
{
    public class SandboxController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK,
                new List<string> { "Go Hawks", "Super Bowl 2021" });
            return response;
        }
        public List<string> Get(int id)
        {
            return new List<string> { "Have some data", "Have some more" };
        }
    }
}
