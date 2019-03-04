using Site.Data.Entities.Oauth;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class AuthDbContext
    {
        internal bool PasswordIsEqual(string source, string password)
        {
            return string.Equals(source, password.GetMD5Hash(), StringComparison.InvariantCultureIgnoreCase);
        }

        internal async Task<Account> AccountGetAsync(Guid accountId)
        {
            var sqlParams = new SqlParameter[] { accountId.ToSql("id") };

            return await ExecuteReaderAsync<Account>("[oauth].[pAccountGetById]", sqlParams);
        }
        internal async Task<Account> AccountGetAsync(string userName)
        {
            var sqlParams = new SqlParameter[] { userName.ToSql("userName") };

            return await ExecuteReaderAsync<Account>("[oauth].[pAccountGetByUserName]", sqlParams);
        }

        internal async Task<Account> AccountGetByEmailAsync(string email)
        {
            var sqlParams = new SqlParameter[] { email.ToSql("email") };

            return await ExecuteReaderAsync<Account>("[oauth].[pAccountGetByEmail]", sqlParams);
        }
        internal async Task<Account> AccountGetAsync(string userName, string password)
        {
            var account = await AccountGetAsync(userName);

            if (!object.Equals(account, null) && !PasswordIsEqual(account.Password, password))
                account = null;

            return account;
        }

        internal async Task AccountActivateAsync(Guid accountId)
        {
            var sqlParams = new SqlParameter[] { accountId.ToSql("id") };

            await ExecuteNonQueryAsync("[oauth].[pAccountActivate]", sqlParams);
        }
    }
}
