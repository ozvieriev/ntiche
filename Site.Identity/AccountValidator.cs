using Microsoft.AspNet.Identity;
using Site.Data.Entities.Oauth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Identity
{
    public class AccountValidator<TUser> : IIdentityValidator<TUser> where TUser : Account
    {
        private readonly UserManager<TUser, Guid> _manager;

        public AccountValidator() { }

        public AccountValidator(UserManager<TUser, Guid> manager)
        {
            _manager = manager;
        }

        public async Task<IdentityResult> ValidateAsync(TUser account)
        {
            var errors = new List<string>();

            if (_manager != null)
            {
                var otherAccount = await _manager.FindByEmailAsync(account.Email);
                if (!object.Equals(otherAccount,null) && otherAccount.Id != account.Id)
                    errors.Add("Select a different email address. An account has already been created with this email address.");

                otherAccount = await _manager.FindByNameAsync(account.UserName);
                if (!object.Equals(otherAccount, null) && otherAccount.Id != account.Id)
                    errors.Add("Select a different user name address. An account has already been created with this user name.");
            }

            return errors.Any()
                ? IdentityResult.Failed(errors.ToArray())
                : IdentityResult.Success;
        }
    }
}
