using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Site.UI.Controllers.Api
{
    [RoutePrefix("api/ping")]
    public class PingController : ApiController
    {
        [HttpGet, Route("")]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}