using AutoMapper;
using Certificate.Templates;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.AspNet.Identity;
using Site.Data.Entities.Oauth;
using Site.Data.Entities.Test;
using Site.Identity;
using Site.Identity.Models;
using Site.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Site.UI.Controllers.Api
{
    [RoutePrefix("api/exam")]
    public class ExamDownloadController : ApiController
    {
        private ITestRepository _test;
        private IAppSettings _appSettings;

        public ExamDownloadController(ITestRepository test, IAppSettings appSettings)
        {
            _test = test;
            _appSettings = appSettings;
        }

        [HttpGet, Route("download")]
        public async Task<HttpResponseMessage> DownloadGet()
        {
            var account = new Account
            {
                FirstName = "Oleksandr",
                LastName = "Zvieriev",
                PharmacistLicense = "-"
            };

            var bytes = PdfCertificate.CreatePdf(CertificateTemplate.LetterOfAttendance, new Notification(new
            {
                accountFullName = $"{account.FirstName} {account.LastName}",
                accountPharmacistLicense = account.PharmacistLicense,
                examResultCreateDate = DateTime.Now.ToShortDateString()
            }));
            var memoryStream = new MemoryStream(bytes);

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(memoryStream);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = $"{account.FirstName} {account.LastName}.pdf"
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            return response;
        }
    }
}