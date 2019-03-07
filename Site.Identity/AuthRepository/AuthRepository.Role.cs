using Site.Data.Entities.Oauth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class AuthRepository
    {
        public async Task<List<Role>> RoleGetByAccountIdAsync(Guid accountId)
        {
            return await _context.RoleGetByAccountIdAsync(accountId);
        }
    }
}
