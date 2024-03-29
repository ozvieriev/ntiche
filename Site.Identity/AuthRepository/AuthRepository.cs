﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Site.Data.Entities.Oauth;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Site.Identity
{
    public partial class AuthRepository : IAuthRepository
    {
        protected readonly AuthDbContext _context;
        protected readonly UserManager<Account, Guid> _userManager;

        public AuthRepository()
        {
            _context = new AuthDbContext();

            _userManager = new UserManager<Account, Guid>(new IdentityStore<Account>(_context));
            _userManager.UserValidator = new AccountValidator<Account>(_userManager);
            _userManager.PasswordHasher = new PasswordHasher();
        }

        public void SetDataProtectorProvider(IDataProtectionProvider dataProtectorProvider)
        {
            var dataProtector = dataProtectorProvider.Create("101-14311");
            _userManager.UserTokenProvider = new DataProtectorTokenProvider<Account, Guid>(dataProtector)
            {
                TokenLifespan = TimeSpan.FromDays(1),
            };
        }

        public async Task<Uri> GenerateEmailConfirmationTokenLinkAsync(Account account, Uri uri)
        {
            var uriBuilder = new UriBuilder(uri);
            var emailConfirmationLinkPasswordToken = await _userManager.GenerateEmailConfirmationTokenAsync(account.Id);

            var bytes = Encoding.UTF8.GetBytes(emailConfirmationLinkPasswordToken);
            emailConfirmationLinkPasswordToken = Convert.ToBase64String(bytes);
            emailConfirmationLinkPasswordToken = HttpUtility.UrlEncode(emailConfirmationLinkPasswordToken);

            var fragment = uriBuilder.Fragment.TrimStart('#');
            var accountId = account.Id.ToString("N");

            uriBuilder.Fragment = $"{fragment}/{accountId}/{emailConfirmationLinkPasswordToken}";
            return uriBuilder.Uri;
        }
        public async Task<Uri> GenerateRecoverPasswordTokenLinkAsync(Account account, Uri uri)
        {
            var uriBuilder = new UriBuilder(uri);
            var recoverPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(account.Id);

            var bytes = Encoding.UTF8.GetBytes(recoverPasswordToken);
            recoverPasswordToken = Convert.ToBase64String(bytes);
            recoverPasswordToken = HttpUtility.UrlEncode(recoverPasswordToken);

            var fragment = uriBuilder.Fragment.TrimStart('#');
            var accountId = account.Id.ToString("N");

            uriBuilder.Fragment = $"{fragment}/{accountId}/{recoverPasswordToken}";
            return uriBuilder.Uri;
        }

        public void Dispose()
        {
            _context.Dispose();
            _userManager.Dispose();
        }
    }
}