using Site.Data.Entities.Test;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class TestRepository
    {
        public async Task ExamResultInsertAsync(Guid accountId, IEnumerable<Guid> answers)
        {
            await _context.ExamResultInsertAsync(accountId, answers);
        }

        public async Task<vExam> vExamGetByNameAsync(string name, string languageIso2)
        {
            return await _context.vExamGetByNameAsync(name, languageIso2);
        }
        public async Task<vExam> vExamGetByExamResultIdAsync(Guid examResultId, string languageIso2)
        {
            return await _context.vExamGetByExamResultIdAsync(examResultId, languageIso2);
        }
    }
}