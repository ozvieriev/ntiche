using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Site.UI.Handlers
{
    public class LanguageHandler : DelegatingHandler
    {

        private static readonly StringComparer _stringComparer = StringComparer.InvariantCultureIgnoreCase;
        private static readonly Dictionary<string, CultureInfo> _cultures = new Dictionary<string, CultureInfo>(_stringComparer) { };

        static LanguageHandler()
        {
            _cultures.Add("en", new CultureInfo("en-US"));
            _cultures.Add("fr", new CultureInfo("fr-FR"));
        }

        private static string GetQueryString(HttpRequestMessage request, string key)
        {
            var queryStrings = request.GetQueryNameValuePairs();
            if (object.Equals(queryStrings, null))
                return null;

            var match = queryStrings.FirstOrDefault(kv => string.Compare(kv.Key, key, true) == 0);
            if (string.IsNullOrEmpty(match.Value))
                return null;

            return match.Value;
        }

        private CultureInfo QueryCultureInfo(HttpRequestMessage request)
        {
            var lang = GetQueryString(request, "lang");

            if (string.IsNullOrEmpty(lang))
                return null;

            if (_cultures.ContainsKey(lang))
                return _cultures[lang];

            return null;
        }

        private CultureInfo HeaderCultureInfo(HttpRequestMessage request)
        {
            foreach (var lang in request.Headers.AcceptLanguage)
            {
                if (string.IsNullOrEmpty(lang.Value) || lang.Value.Length < 2)
                    continue;

                var language = lang.Value.Substring(0, 2);
                if (_cultures.ContainsKey(language))
                    return _cultures[language];
            }

            return null;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var culture = QueryCultureInfo(request) ??
                HeaderCultureInfo(request) ??
                _cultures.First().Value;

            request.Headers.AcceptLanguage.Clear();
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(culture.Name));

            Thread.CurrentThread.CurrentUICulture = culture;

            return await base.SendAsync(request, cancellationToken);
        }
    }
}