using Site.Data.Entities.Oauth;
using System;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class AuthRepository
    {
        public async Task<SessionToken> SessionTokenGetAsync(Guid id)
        {
            return await _context.SessionTokenGetAsync(id);
        }
        public async Task SessionTokenInsertAsync(SessionToken token)
        {
            await _context.SessionTokenInsertAsync(token);
        }
    }
}
