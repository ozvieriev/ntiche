using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Site.UI.Core
{
    public class HttpErrorResult : IHttpActionResult
    {
        private string _errorDescription;
        private HttpRequestMessage _request;

        public HttpErrorResult(string errorDescription, HttpRequestMessage request)
        {
            _errorDescription = errorDescription;
            _request = request;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = _request.CreateErrorMessageResponse(error_description: _errorDescription);

            return Task.FromResult(response);
        }
    }
}