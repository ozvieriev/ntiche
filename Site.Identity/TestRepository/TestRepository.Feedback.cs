using Site.Data.Entities.Test;
using Site.Identity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class TestRepository
    {
        public async Task<Feedback> FeedbackInsertAsync(Feedback entity)
        {
            _context.Feedbacks.Add(entity);

            var saveChangesAsync = await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<vFeedbackReport>> FeedbackReportAsync(vFeedbackReportViewModel model = null)
        {
            return await _context.FeedbackReportAsync(model);
        }
    }
}