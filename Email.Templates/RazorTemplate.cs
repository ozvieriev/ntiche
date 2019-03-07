using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Reflection;

namespace Email.Templates
{
    public class RazorTemplate
    {
        private EmailTemplate _template;
        private static Type _resourcesType = typeof(Resources);

        public RazorTemplate(EmailTemplate template)
        {
            _template = template;
        }

        public string Compile(Notification notification)
        {
            return RunCompile($"{_template}.cshtml", notification);
        }

        private string RunCompile(string templatePath, Notification notification)
        {
            var templateKey = Enum.GetName(typeof(EmailTemplate), _template);
            var property = _resourcesType.GetProperty(templateKey, (BindingFlags.Public | BindingFlags.Static));

            var template = (string)property.GetValue(null, null);
            var templateSource = new LoadedTemplateSource(template, templatePath);

            return Engine.Razor.RunCompile(templateSource, templateKey, typeof(Notification), notification);
        }
    }
}
