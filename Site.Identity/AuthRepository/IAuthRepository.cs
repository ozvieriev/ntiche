using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.DataProtection;
using Site.Data.Entities.Oauth;
using System;
using System.Threading.Tasks;

namespace Site.Identity
{
    public interface IAuthRepository : IDisposable
    {
        void SetDataProtectorProvider(IDataProtectionProvider dataProtectorProvider);
        Task<Uri> GenerateEmailConfirmationTokenLinkAsync(Account account, Uri uri);

        /*******************************************Account*************************************************/
        Task<Account> AccountGetAsync(Guid accountId);
        Task<Account> AccountGetAsync(string userName);
        Task<Account> AccountGetAsync(string userName, string password);
        Task AccountActivateAsync(Guid accountId);

        Task<IdentityResult> AccountCreateAsync(Account account, string password);
        Task<IdentityResult> AccountChangePasswordAsync(Guid accountId, string oldPassword, string newPassword);
        Task<IdentityResult> AccountConfirmEmailAsync(Guid accountId, string emailConfirmationToken);

        /*******************************************SessionToken********************************************/
        //Task<SessionToken> SessionTokenGetAsync(Guid id);
        //Task SessionTokenInsertAsync(SessionToken token);
    }
}
