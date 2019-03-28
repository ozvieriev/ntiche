﻿using Certificate.Templates;
using Email.Templates;
using Microsoft.AspNet.Identity;
using Site.Identity;
using Site.UI.Core;
using Site.UI.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Site.UI.Controllers.Api
{
    [RoutePrefix("api/exam")]
    public class ExamResultController : ApiController
    {
        private IAuthRepository _auth;
        private ITestRepository _test;
        private IAppSettings _appSettings;
        private ILasyEmailSender _lasyEmailSender;
        private ILasyCertificateGenerator _lasyCertificateGenerator;

        public ExamResultController(IAuthRepository auth, ITestRepository test, IAppSettings appSettings, ILasyEmailSender lasyEmailSender, ILasyCertificateGenerator lasyCertificateGenerator)
        {
            _auth = auth;
            _test = test;
            _appSettings = appSettings;
            _lasyEmailSender = lasyEmailSender;
            _lasyCertificateGenerator = lasyCertificateGenerator;
        }

        [HttpGet, Route("post-test/download")]
        public async Task<HttpResponseMessage> PostTestDownloadGet(Guid examResultId)
        {
            var examResult = await _test.ExamResultGetAsync(examResultId);
            if (object.Equals(examResult, null) || !examResult.ExamResultIsSuccess || !"post-test".Equals(examResult.ExamName))
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            var lasyCertificateGeneratorViewModel = new LasyCertificateGeneratorViewModel(examResult, CertificateTemplate.LetterOfAttendance);
            var certificate = _lasyCertificateGenerator.GetCertificate(lasyCertificateGeneratorViewModel);
            var bytes = File.ReadAllBytes(certificate.FullName);

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(bytes);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = $"{examResult.AccountFirstName} {examResult.AccountLastName}.pdf"
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            return response;
        }

        [HttpPost, Route("post-test/send"), Authorize, ValidateModel, ValidateNullModel]
        public async Task<IHttpActionResult> PostTestSendPost(ExamResultViewModel model)
        {
            var examResult = await _test.ExamResultGetAsync(model.ExamResultId);
            if (object.Equals(examResult, null) || !examResult.ExamResultIsSuccess || !"post-test".Equals(examResult.ExamName))
                return NotFound();

            var lasyCertificateGeneratorViewModel = new LasyCertificateGeneratorViewModel(examResult, CertificateTemplate.LetterOfAttendance);
            var certificate = _lasyCertificateGenerator.GetCertificate(lasyCertificateGeneratorViewModel);
            var attachment = new LasyEmailAttachment($"{examResult.AccountFirstName} {examResult.AccountLastName}.pdf", certificate);

            var lasyEmailViewModel = new LasyEmailViewModel("Congratulations", examResult.AccountEmail, EmailTemplate.PostTestCertificate,
                new Email.Templates.Notification(new
                {
                    firstName = examResult.AccountFirstName,
                    lastName = examResult.AccountLastName
                }), attachment);

            _lasyEmailSender.Send(lasyEmailViewModel);

            return Ok(new DescriptionViewModel("An email has been sent to your account."));
        }

        [HttpPost, Route("post-test/question"), Authorize, ValidateModel, ValidateNullModel]
        public async Task<IHttpActionResult> PostTestQuestionPost(ExamPostTestQuestionViewModel model)
        {
            var accountId = Guid.Parse(User.Identity.GetUserId());
            var account = await _auth.AccountGetAsync(accountId);

            var subject = $"[ntiche request]";
            var lasyEmailViewModel = new LasyEmailViewModel(subject, _appSettings.Email.Admin, EmailTemplate.PostTestQuestion,
                new Email.Templates.Notification(new
                {
                    account.FirstName,
                    account.LastName,
                    account.Email,
                    model.Question
                }));

            _lasyEmailSender.Send(lasyEmailViewModel);

            return Ok(new DescriptionViewModel("A question has been saved. You will contact you as soon as possible."));
        }
    }
}