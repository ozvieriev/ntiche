using Microsoft.AspNet.Identity;
using Site.Data.Entities.Oauth;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Site.Identity
{
    public class IdentityStore<TUser> : IUserStore<TUser, Guid>, IUserEmailStore<TUser, Guid>, IUserPasswordStore<TUser, Guid>, IUserClaimStore<TUser, Guid>
        where TUser : Account
    {
        private readonly AuthDbContext _context;

        public IdentityStore(AuthDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TUser account)
        {
            if (object.Equals(account, null))
                throw new ArgumentNullException("account");

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(TUser account)
        {
            throw new NotImplementedException();
        }

        public async Task<TUser> FindByIdAsync(Guid accountId)
        {
            return (TUser)(await _context.AccountGetAsync(accountId));
        }
        public async Task<TUser> FindByEmailAsync(string email)
        {
            return (TUser)(await _context.AccountGetByEmailAsync(email));
        }
        public Task<TUser> FindByEmailAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
        public async Task<TUser> FindByNameAsync(string userName)
        {
            throw new Exception();
            //return (TUser)(await _context.AccountGetAsync(userName));
        }

        public async Task UpdateAsync(TUser account)
        {
            if (object.Equals(account, null))
                throw new ArgumentNullException("account");

            _context.Accounts.AddOrUpdate(account);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<string> GetPasswordHashAsync(TUser user)
        {
            return Task.FromResult<string>(user.Password);
        }
        public Task<bool> HasPasswordAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException("account");

            user.Password = passwordHash;

            return Task.FromResult<object>(null);
        }

        public Task<string> GetEmailAsync(TUser account)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(TUser account)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(TUser account, string email)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(TUser account, bool confirmed)
        {
            return Task.FromResult<object>(null);
        }

        public Task AddClaimAsync(TUser account, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Claim>> GetClaimsAsync(TUser account)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(TUser account, Claim claim)
        {
            throw new NotImplementedException();
        }
    }
}
