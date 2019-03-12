using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.AspNet.Identity;
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
    public class ExamController : ApiController
    {
        private ITestRepository _test;
        private IAppSettings _appSettings;

        public ExamController(ITestRepository test, IAppSettings appSettings)
        {
            _test = test;
            _appSettings = appSettings;
        }

        [HttpGet, Route("report")]
        public async Task<HttpResponseMessage> GetExamReport([FromUri] ExamResultReportGetViewModel model)
        {
            var dbModel = new vExamResultReportViewModel { };

            dbModel = Mapper.Map(model, dbModel);

            var report = await _test.ExamResultReportAsync(dbModel);

            var response = new HttpResponseMessage(HttpStatusCode.OK);

            response.Content = GetContent(report);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = $"{report.Count}-examResults.csv"
            };

            return response;
        }

        [HttpPost, Route("{name:regex(pre-test|post-test)}"), ValidateNullModel, ValidateModel, Authorize]
        public async Task<IHttpActionResult> PostResult(string name, [FromBody] ExamPostViewModel model)
        {
            var exam = await _test.ExamGetAsync(name);
            var accountId = Guid.Parse(User.Identity.GetUserId());
            
            var entity = new ExamResult { AccountId = accountId, ExamId = exam.Id };
            entity = Mapper.Map(model, entity);
            entity = await _test.ExamResultInsertAsync(entity);            

            return Ok(new DescriptionViewModel("Thank you for your answers. Your test results have been saved."));
        }

        private StringContent GetContent(IEnumerable<vExamResultReport> report)
        {
            using (var memoryStream = new MemoryStream())
            using (var streamReader = new StreamReader(memoryStream))
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                var csvWriter = new ExamResultReportCsvWriter();
                csvWriter.Write(streamWriter, report);
                streamWriter.Flush();
                memoryStream.Position = 0;

                string csv = streamReader.ReadToEnd();
                return new StringContent(csv, Encoding.UTF8, "text/csv");
            }
        }

        private class ExamResultReportCsvWriter
        {
            public ExamResultReportCsvWriter() { }

            public void Write(StreamWriter stream, IEnumerable<vExamResultReport> report)
            {
                var csvSerializer = new CsvSerializer(stream);
                var writer = new CsvWriter(csvSerializer);

                writer.Configuration.RegisterClassMap<ExamResultCsvClassMap>();
                writer.WriteRecords(report);
            }
        }

        private class ExamResultCsvClassMap : ClassMap<vExamResultReport>
        {
            public ExamResultCsvClassMap()
            {
                Map(m => m.AccountFirstName).Name("First name");
                Map(m => m.AccountLastName).Name("Last name");
                Map(m => m.AccountEmail).Name("Email");
                Map(m => m.AccountPharmacistLicense).Name("Pharmacist license");
                Map(m => m.AccountPharmacySettingName).Name("Pharmacy setting");
                Map(m => m.AccountOcupation).Name("Ocupation");
                Map(m => m.AccountCountryName).Name("Country");
                Map(m => m.AccoutnProvinceName).Name("Province");
                Map(m => m.AccountCity).Name("City");
                Map(m => m.AccountIsOptin).Name("Optin").TypeConverter<CsvBooleanConverter>();
                Map(m => m.AccountCreateDateUtc).Name("Create Date(Utc)");
            }
        }

        private class CsvBooleanConverter : DefaultTypeConverter
        {
            public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
            {
                if (value == null)
                    return string.Empty;

                return (bool)value ? "yes" : "no";
            }
        }
    }
}