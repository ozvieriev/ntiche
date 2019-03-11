using AutoMapper;
using CsvHelper;
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
    [RoutePrefix("api/feedback")]
    public class FeedbackController : ApiController
    {
        private ITestRepository _test;

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
            var dbModel = new vFeedbackReportViewModel { };

            dbModel = Mapper.Map(model, dbModel);

            var feedbacks = await _test.FeedbackReportAsync(dbModel);

            var response = new HttpResponseMessage(HttpStatusCode.OK);

            response.Content = GetFeedbacksContent(feedbacks);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "feedbacks.csv"
            };

            return response;
        }

        private StringContent GetFeedbacksContent(IEnumerable<vFeedbackReport> feedbacks)
        {
            using (var memoryStream = new MemoryStream())
            using (var streamReader = new StreamReader(memoryStream))
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                var feedbackWriter = new FeedbackCsvWriter();
                feedbackWriter.Write(streamWriter, feedbacks);
                streamWriter.Flush();
                memoryStream.Position = 0;

                string csv = streamReader.ReadToEnd();
                return new StringContent(csv, Encoding.UTF8, "text/csv");
            }
        }

        private class FeedbackCsvWriter
        {
            public FeedbackCsvWriter() { }

            public void Write(StreamWriter stream, IEnumerable<vFeedbackReport> feedbacks)
            {
                var csvSerializer = new CsvSerializer(stream);
                var writer = new CsvWriter(csvSerializer);

                //writer.Configuration.RegisterClassMap<FeedbackCsvMap>();
                writer.WriteRecords(feedbacks);
            }
        }

        private class FeedbackCsvMap
        {

        }
    }
}