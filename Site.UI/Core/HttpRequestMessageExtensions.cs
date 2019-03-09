using NLog;
using System;
using System.Net;
using System.Net.Http;

namespace Site.UI.Core
{
    public static class HttpRequestMessageExtensions
    {
        private static Logger _oauthLogger = LogManager.GetLogger("oauth-logger");

        public static HttpResponseMessage CreateExceptionResponse(this HttpRequestMessage request,
            Exception exc,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            var response = request.CreateExceptionResponse(error_description: "Sorry, the server is busy. Please try again later.", statusCode: statusCode);

            if (!object.Equals(exc, null))
            {
                _oauthLogger.Error(exc);

                response.Headers.Add("Exc-Message", exc.Message.Replace(Environment.NewLine, string.Empty));
            }
            return response;
        }
        public static HttpResponseMessage CreateExceptionResponse(this HttpRequestMessage request,
            string error = "invalid_request",
            string error_description = null,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            var response = request.CreateResponse(statusCode, new { error, error_description });

            return response;
        }

        public static HttpExceptionResult HttpExceptionResult(this HttpRequestMessage request, Exception exc)
        {
            return new HttpExceptionResult(exc, request);
        }
    }
}