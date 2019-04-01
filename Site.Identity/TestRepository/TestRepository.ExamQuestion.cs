using Site.Data.Entities.Test;
using Site.Identity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class TestRepository
    {
        public async Task<ExamQuestion> ExamQuestionInsertAsync(ExamQuestion entity)
        {
            _context.ExamQuestions.Add(entity);

            var saveChangesAsync = await _context.SaveChangesAsync();

            return entity;
        }
        public async Task<List<vExamQuestionReport>> ExamQuestionReportAsync(vExamQuestionReportViewModel model = null)
        {
            return await _context.ExamQuestionReportAsync(model);
        }
    }
}