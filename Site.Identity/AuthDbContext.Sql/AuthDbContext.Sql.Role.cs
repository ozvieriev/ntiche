using Site.Data.Entities.Oauth;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class AuthDbContext
    {
        internal async Task<List<Role>> RoleGetByAccountIdAsync(Guid accountId)
        {
            var sqlParams = new SqlParameter[]
            {
                accountId.ToSql("accountId")
            };

            return await ExecuteReaderCollectionAsync<Role>("oauth.pRoleGetByAccountId", sqlParams);
        }
    }
}
