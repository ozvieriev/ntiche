using Site.Data.Entities.Oauth;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Site.UI.Oauth
{
    public static class ClaimsIdentityHelper
    {
        public static void TryRemoveClaim(this ClaimsIdentity identity, string type)
        {
            var claims = identity.FindAll(type).ToArray();

            foreach (var claim in claims)
                identity.TryRemoveClaim(claim);
        }

        public static void AddClaims(this ClaimsIdentity identity, Account account)
        {
            identity.TryRemoveClaim(ClaimTypes.NameIdentifier);
            identity.TryRemoveClaim(ClaimTypes.Name);

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, account.Id.ToString("N")));
            identity.AddClaim(new Claim(ClaimTypes.Name, account.Email));
        }

        public static void AddClaims(this ClaimsIdentity identity, IList<Role> roles)
        {
            identity.TryRemoveClaim(ClaimTypes.Role);

            foreach (var role in roles)
                identity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
        }
    }
}