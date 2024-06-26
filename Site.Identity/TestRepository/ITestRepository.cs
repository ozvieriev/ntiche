﻿using Site.Data.Entities.Test;
using Site.Identity.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.Identity
{
    public interface ITestRepository : IDisposable
    {
        Task<int> FeedbackCountGetAsync(Guid accountId);
        Task<vAccountActivity> AccountActivityGetAsync(Guid accoundId);

        Task<Exam> ExamGetAsync(string name);
        Task<ExamQuestion> ExamQuestionInsertAsync(ExamQuestion entity);
        Task<ExamResult> ExamResultInsertAsync(ExamResult entity);
        Task<vExamResult> ExamResultGetAsync(Guid examResultId);

        Task<Feedback> FeedbackInsertAsync(Feedback feedback);
        Task<List<vFeedbackReport>> FeedbackReportAsync(vFeedbackReportViewModel model = null);
        Task<List<vExamQuestionReport>> ExamQuestionReportAsync(vExamQuestionReportViewModel model = null);
        Task<List<vExamResultReport>> ExamResultReportAsync(vExamResultReportViewModel model = null);
    }
}