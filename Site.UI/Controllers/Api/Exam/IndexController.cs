using AutoMapper;
using Certificate.Templates;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNet.Identity;
using Site.Data.Entities.Test;
using Site.Identity;
using Site.Identity.Models;
using Site.UI.Core;
using Site.UI.Core.Csv;
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
        private ILasyCertificateGenerator _lasyCertificateGenerator;

        private DateTime ToTime(long seconds)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(seconds)
                .ToLocalTime();
        }

        public ExamController(ITestRepository test, IAppSettings appSettings, LasyCertificateGenerator lasyCertificateGenerator)
        {
            _test = test;
            _appSettings = appSettings;
            _lasyCertificateGenerator = lasyCertificateGenerator;
        }

        [HttpGet, Route("report")]
        public async Task<HttpResponseMessage> GetExamReport([FromUri] ExamResultReportViewModel model)
        {
            model = model ?? new ExamResultReportViewModel();

            var dbModel = new vExamResultReportViewModel { };

            dbModel = Mapper.Map(model, dbModel);

            if (model.AccountFrom.HasValue)
                dbModel.AccountFrom = ToTime(model.AccountFrom.Value);

            if (model.AccountTo.HasValue)
                dbModel.AccountTo = ToTime(model.AccountTo.Value);

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
            entity.IsSuccess = entity.PercentCorrect >= 70;

            entity = await _test.ExamResultInsertAsync(entity);

            var response = new ExamPostResponseViewModel("Thank you for your answers. Your test results have been saved.")
            {
                ExamResultId = entity.Id,
                IsSuccess = entity.IsSuccess
            };

            if ("post-test".Equals(name, StringComparison.InvariantCultureIgnoreCase))
            {
                response.TotalFeedbacks = await _test.FeedbackCountGetAsync(accountId);

                var examResult = await _test.ExamResultGetAsync(entity.Id);

                if (!object.Equals(examResult, null))
                    _lasyCertificateGenerator.Save(new LasyCertificateGeneratorViewModel(examResult, CertificateTemplate.LetterOfAttendance));
            }
            return Ok(response);
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
                Map(m => m.AccountPharmacySetting).Name("Pharmacy setting");
                Map(m => m.AccountSpecialty).Name("Specialty");
                Map(m => m.AccountCountryName).Name("Country");
                Map(m => m.AccoutnProvinceName).Name("Province");
                Map(m => m.AccountCity).Name("City");
                Map(m => m.AccountIsOptin).Name("Optin").TypeConverter<CsvBooleanConverter>();
                Map(m => m.AccountCreateDate).Name("Create Date").TypeConverter<CsvDateTimeConverter>();

                Map(m => m.ExamName).Name("Test");
                Map(m => m.HumanExamResultAnswer0).Name("Q1");
                Map(m => m.HumanExamResultAnswer1).Name("Q2");
                Map(m => m.HumanExamResultAnswer2).Name("Q3");
                Map(m => m.HumanExamResultAnswer3).Name("Q4");
                Map(m => m.HumanExamResultAnswer4).Name("Q5");
                Map(m => m.HumanExamResultAnswer5).Name("Q6");
                Map(m => m.HumanExamResultAnswer6).Name("Q7");

                Map(m => m.ExamResultPercentCorrect).Name("Result %");
                Map(m => m.ExamResultCreateDate).Name("Create Date").TypeConverter<CsvDateTimeConverter>();
            }
        }
    }
}