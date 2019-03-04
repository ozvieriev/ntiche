using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Site.UI.Core
{
    public class HttpExceptionResult : IHttpActionResult
    {
        private Exception _exc;
        private HttpRequestMessage _request;

        public HttpExceptionResult(Exception exc, HttpRequestMessage request)
        {
            _exc = exc;
            _request = request;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = _request.CreateExceptionResponse(_exc);

            return Task.FromResult(response);
        }
    }
}