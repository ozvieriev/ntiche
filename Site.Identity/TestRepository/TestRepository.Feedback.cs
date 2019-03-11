using Site.Data.Entities.Test;
using Site.Identity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class TestRepository
    {
        public async Task<Feedback> FeedbackInsertAsync(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);

            var saveChangesAsync = await _context.SaveChangesAsync();

            return feedback;
        }

        public async Task<List<vFeedbackReport>> FeedbackReportAsync(vFeedbackReportViewModel model = null)
        {
            return await _context.FeedbackReportAsync(model);
        }
    }
}