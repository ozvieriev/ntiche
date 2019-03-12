using Site.Data.Entities.Test;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class AuthDbContext
    {
        internal async Task<vAccountActivity> AccountActivityGetAsync(Guid accountId)
        {
            var sqlParams = new SqlParameter[] {
                accountId.ToSql("accountId")
            };

            return await ExecuteReaderAsync<vAccountActivity>("[test].[vAccountActivityByAccountId]", sqlParams);
        }
    }
}