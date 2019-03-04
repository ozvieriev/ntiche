using Site.Data.Entities.Oauth;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class AuthDbContext
    {
        internal async Task<SessionToken> SessionTokenGetAsync(Guid id)
        {
            var sqlParams = new SqlParameter[] { id.ToSql("id") };

            return await ExecuteReaderAsync<SessionToken>("[oauth].[pSessionTokenTake]", sqlParams);
        }
        internal async Task SessionTokenInsertAsync(SessionToken token)
        {
            var sqlParams = new SqlParameter[]
            {
                token.Id.ToSql("id"),
                token.AccountId.ToSql("accountId"),
                token.IssuedUtc.ToSql("issuedUtc"),
                token.ExpiresUtc.ToSql("expiresUtc"),
                token.ProtectedTicket.ToSql("protectedTicket")
            };

            await ExecuteNonQueryAsync("[oauth].[pSessionTokenInsert]", sqlParams);
        }
    }
}
