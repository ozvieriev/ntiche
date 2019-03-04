﻿using Microsoft.AspNet.Identity;
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
        public async Task<Account> AccountGetAsync(string userName)
        {
            return await _context.AccountGetAsync(userName);
        }
        public async Task<Account> AccountGetAsync(string userName, string password)
        {
            return await _context.AccountGetAsync(userName, password);
        }
        public async Task AccountActivateAsync(Guid accountId)
        {
            await _context.AccountActivateAsync(accountId);
        }

        public async Task<IdentityResult> AccountCreateAsync(Account account, string password)
        {
            return await _userManager.CreateAsync(account, password);
        }
        public async Task<IdentityResult> AccountChangePasswordAsync(Guid accountId, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(accountId, oldPassword, newPassword);
        }
    }
}
