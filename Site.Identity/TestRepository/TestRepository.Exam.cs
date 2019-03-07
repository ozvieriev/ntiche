using Site.Data.Entities.Test;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class TestRepository
    {
        public async Task<vExam> vExamGetByLanguageIso2Async(string languageIso2)
        {
            return await _context.vExamGetByLanguageIso2Async(languageIso2);
        }
    }
}