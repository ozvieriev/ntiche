using NLog;
using System;
using System.Net;
using System.Net.Http;

namespace Site.UI.Core
{
    public static class HttpRequestMessageExtensions
    {
        private static Logger _oauthLogLogger = LogManager.GetLogger("oauth-log");

        public static HttpResponseMessage CreateExceptionResponse(this HttpRequestMessage request,
            Exception exc,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            var response = request.CreateExceptionResponse(error_description: exc.Message, statusCode: statusCode);

            if (!object.Equals(exc, null))
                _oauthLogLogger.Error(exc);

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