using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Site.Data.Entities.Oauth;
using System;

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

        public void Dispose()
        {
            _context.Dispose();
            _userManager.Dispose();
        }
    }
}