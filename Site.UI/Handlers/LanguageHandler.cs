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

        private CultureInfo CultureInfo(HttpRequestMessage request)
        {
            foreach (var lang in request.Headers.AcceptLanguage)
            {
                if (string.IsNullOrEmpty(lang.Value) || lang.Value.Length < 2)
                    continue;

                var language = lang.Value.Substring(0, 2);
                if (_cultures.ContainsKey(language))
                    return _cultures[language];
            }

            return _cultures.First().Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var culture = CultureInfo(request);

            request.Headers.AcceptLanguage.Clear();
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(culture.Name));

            return await base.SendAsync(request, cancellationToken);
        }
    }
}