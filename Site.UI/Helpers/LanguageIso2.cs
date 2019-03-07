using System.Linq;
using System.Net.Http.Headers;

namespace Site.UI.Helpers
{
    public static class LanguageIso2
    {
        public static string GetLanguageIso2(this HttpRequestHeaders headers)
        {
            if (object.Equals(headers, null))
                return null;

            var header = headers.AcceptLanguage.First();

            return header.Value.Substring(0,2);
        }
    }
}