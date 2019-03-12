using Site.Data.Entities.Test;
using System;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class TestRepository
    {
        public async Task<vAccountActivity> AccountActivityGetAsync(Guid accoundId)
        {
            return await _context.AccountActivityGetAsync(accoundId);
        }
    }
}