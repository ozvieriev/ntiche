using Site.Data.Entities.Test;
using Site.Identity.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.Identity
{
    public interface ITestRepository : IDisposable
    {
        Task<Exam> ExamGetAsync(string name);
        Task<ExamResult> ExamResultInsertAsync(ExamResult entity);

        Task<Feedback> FeedbackInsertAsync(Feedback feedback);
        Task<List<vFeedbackReport>> FeedbackReportAsync(vFeedbackReportViewModel model = null);
        Task<List<vExamResultReport>> ExamResultReportAsync(vExamResultReportViewModel model = null);
    }
}