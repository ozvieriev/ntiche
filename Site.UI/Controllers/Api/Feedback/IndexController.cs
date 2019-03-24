using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNet.Identity;
using Site.Data.Entities.Test;
using Site.Identity;
using Site.Identity.Models;
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
    [RoutePrefix("api/feedback")]
    public class FeedbackController : ApiController
    {
        private ITestRepository _test;

        private DateTime ToTime(long seconds)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(seconds)
                .ToLocalTime();
        }

        public FeedbackController(ITestRepository test)
        {
            _test = test;
        }
        [HttpPost, Route(""), ValidateNullModel, ValidateModel, Authorize]
        public async Task<IHttpActionResult> PostFeedback(FeedbackPostViewModel model)
        {
            var accountId = Guid.Parse(User.Identity.GetUserId());

            var feedback = new Feedback { AccountId = accountId };
            feedback = Mapper.Map(model, feedback);
            feedback = await _test.FeedbackInsertAsync(feedback);

            return Ok();
        }
        [HttpGet, Route("report")]
        public async Task<HttpResponseMessage> GetFeedbackReport([FromUri] FeedbackReportGetViewModel model)
        {
            model = model ?? new FeedbackReportGetViewModel();

            var dbModel = new vFeedbackReportViewModel { };

            dbModel = Mapper.Map(model, dbModel);

            if (model.AccountFrom.HasValue)
                dbModel.AccountFrom = ToTime(model.AccountFrom.Value);

            if (model.AccountTo.HasValue)
                dbModel.AccountTo = ToTime(model.AccountTo.Value);


            var report = await _test.FeedbackReportAsync(dbModel);

            var response = new HttpResponseMessage(HttpStatusCode.OK);

            response.Content = GetContent(report);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = $"{report.Count}-feedbacks.csv"
            };

            return response;
        }

        private StringContent GetContent(IEnumerable<vFeedbackReport> report)
        {
            using (var memoryStream = new MemoryStream())
            using (var streamReader = new StreamReader(memoryStream))
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                var csvWriter = new FeedbackCsvWriter();
                csvWriter.Write(streamWriter, report);
                streamWriter.Flush();
                memoryStream.Position = 0;

                string csv = streamReader.ReadToEnd();
                return new StringContent(csv, Encoding.UTF8, "text/csv");
            }
        }

        private class FeedbackCsvWriter
        {
            public FeedbackCsvWriter() { }

            public void Write(StreamWriter stream, IEnumerable<vFeedbackReport> report)
            {
                var csvSerializer = new CsvSerializer(stream);
                var writer = new CsvWriter(csvSerializer);

                writer.Configuration.RegisterClassMap<FeedbackCsvClassMap>();
                writer.WriteRecords(report);
            }
        }

        private class FeedbackCsvClassMap : ClassMap<vFeedbackReport>
        {
            public FeedbackCsvClassMap()
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

                Map(m => m.HumanFeedbackEnhancedRating).Name("Q1");
                Map(m => m.FeedbackOverallLearningObjectives1Before).Name("Q2 1.Before");
                Map(m => m.FeedbackOverallLearningObjectives1After).Name("Q2 1.After");
                Map(m => m.FeedbackOverallLearningObjectives1Relevance).Name("Q2 1.Relevance");
                Map(m => m.FeedbackOverallLearningObjectives2Before).Name("Q2 2.Before");
                Map(m => m.FeedbackOverallLearningObjectives2After).Name("Q2 2.After");
                Map(m => m.FeedbackOverallLearningObjectives2Relevance).Name(" Q2 2.Relevance");
                Map(m => m.FeedbackOverallLearningObjectives3Before).Name("Q2 3.Before");
                Map(m => m.FeedbackOverallLearningObjectives3After).Name("Q2 3.After");
                Map(m => m.FeedbackOverallLearningObjectives3Relevance).Name("Q2 3.Relevance");
                Map(m => m.FeedbackProgramRating).Name("Q3");
                Map(m => m.FeedbackIsAppreciateDelivery).Name("Q4").TypeConverter<CsvBooleanConverter>();
                Map(m => m.FeedbackIsPerceiveDegree).Name("Q5").TypeConverter<CsvBooleanConverter>();
                Map(m => m.FeedbackPerceiveDegreeComments).Name("Q5 Comments");
                Map(m => m.FeedbackChangesComments).Name("Q6 Comments");
                Map(m => m.FeedbackTopicsComments).Name("Q7 Comments");
                Map(m => m.FeedbackAdditionalComments).Name("Q8 Comments");
                Map(m => m.FeedbackCreateDate).Name("Create Date").TypeConverter<CsvDateTimeConverter>();
            }
        }
    }
}