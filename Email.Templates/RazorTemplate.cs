using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Templates
{
    public class RazorTemplate
    {
        private EmailTemplate _template;
        private static string _templateFolder;

        static RazorTemplate()
        {
            _templateFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "EmailTemplates");
        }

        public RazorTemplate(EmailTemplate template)
        {
            _template = template;
        }

        public string Compile(Notification notification)
        {
            string templatePath = Path.Combine(_templateFolder, $"{_template}.cshtml");

            return RunCompile(templatePath, notification);
        }

        private string RunCompile(string templatePath, Notification notification)
        {
            var templateKey = Enum.GetName(typeof(EmailTemplate), _template);

            var template = File.ReadAllText(templatePath);
            var templateSource = new LoadedTemplateSource(template, templatePath);

            return Engine.Razor.RunCompile(templateSource, templateKey, typeof(Notification), notification);
        }
    }
}
