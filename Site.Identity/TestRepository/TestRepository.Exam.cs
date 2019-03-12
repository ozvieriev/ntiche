using Site.Data.Entities.Test;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class TestRepository
    {
        public async Task<Exam> ExamGetAsync(string name)
        {
            return await _context.ExamGetAsync(name);
        }
    }
}