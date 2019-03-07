using Site.Data.Entities.Test;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.Identity
{
    public interface ITestRepository : IDisposable
    {
        Task ExamResultInsertAsync(Guid accountId, IEnumerable<Guid> answers);

        Task<vExam> vExamGetByNameAsync(string name, string languageIso2);
        Task<vExam> vExamGetByExamResultIdAsync(Guid examResultId, string languageIso2);
    }
}