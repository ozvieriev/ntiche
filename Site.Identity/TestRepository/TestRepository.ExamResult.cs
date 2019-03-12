using Site.Data.Entities.Test;
using Site.Identity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class TestRepository
    {
        public async Task<ExamResult> ExamResultInsertAsync(ExamResult entity)
        {
            _context.ExamResults.Add(entity);

            var saveChangesAsync = await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<vExamResultReport>> ExamResultReportAsync(vExamResultReportViewModel model = null)
        {
            return await _context.ExamResultReportAsync(model);
        }
    }
}