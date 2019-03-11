using Site.Data.Entities.Test;
using Site.Identity.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.Identity
{
    public interface ITestRepository : IDisposable
    {
        Task ExamResultInsertAsync(string name, Guid accountId, IEnumerable<Guid> answers);

        Task<vExam> vExamGetByNameAsync(string name, string languageIso2);
        Task<vExam> vExamGetByExamResultIdAsync(Guid examResultId, string languageIso2);
        Task<List<vExamResult>> vExamResultByAccountIdAsync(Guid accountId, string name = null);

        Task<Feedback> FeedbackInsertAsync(Feedback feedback);
        Task<List<vFeedbackReport>> FeedbackReportAsync(vFeedbackReportViewModel model = null);
    }
}