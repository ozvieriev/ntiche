using Site.Data.Entities.Test;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class TestRepository
    {
        public async Task ExamResultInsertAsync(string name, Guid accountId, IEnumerable<Guid> answers)
        {
            await _context.ExamResultInsertAsync(name, accountId, answers);
        }

        public async Task<vExam> vExamGetByNameAsync(string name, string languageIso2)
        {
            return await _context.vExamGetByNameAsync(name, languageIso2);
        }
        public async Task<vExam> vExamGetByExamResultIdAsync(Guid examResultId, string languageIso2)
        {
            return await _context.vExamGetByExamResultIdAsync(examResultId, languageIso2);
        }
        public async Task<List<vExamResult>> vExamResultByAccountIdAsync(Guid accountId, string name = null)
        {
            return await _context.vExamResultByAccountIdAsync(accountId);
        }
        
    }
}