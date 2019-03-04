using Microsoft.AspNet.Identity;
using Site.Data.Entities.Oauth;
using System;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class AuthRepository
    {
        public async Task<Account> AccountGetAsync(Guid accountId)
        {
            return await _context.AccountGetAsync(accountId);
        }
        public async Task<Account> AccountGetAsync(string email)
        {
            return await _context.AccountGetAsync(email);
        }
        public async Task<Account> AccountGetAsync(string email, string password)
        {
            return await _context.AccountGetAsync(email, password);
        }
        public async Task AccountActivateAsync(Guid accountId)
        {
            await _context.AccountActivateAsync(accountId);
        }

        public async Task<IdentityResult> AccountChangePasswordAsync(Guid accountId, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(accountId, oldPassword, newPassword);
        }
    }
}
