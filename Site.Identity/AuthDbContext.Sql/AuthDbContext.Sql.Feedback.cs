using Site.Data.Entities.Test;
using Site.Identity.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class AuthDbContext
    {
        internal async Task<int> FeedbackCountGetAsync(Guid accountId)
        {
            var sqlParams = new SqlParameter[] {
                accountId.ToSql("accountId")
            };

            return await ExecuteReaderAsync<int>("test.pFeedbackCountGetByAccountId", sqlParams);
        }

        internal async Task<List<vFeedbackReport>> FeedbackReportAsync(vFeedbackReportViewModel model = null)
        {
            model = model ?? new vFeedbackReportViewModel();

            var sqlParams = new SqlParameter[] {
                model.AccountFirstName.ToSql("accountFirstName"),
                model.AccountLastName.ToSql("accountLastName"),
                model.AccountPharmacistLicense.ToSql("accountPharmacistLicense"),
                model.AccountPharmacySettingId.ToSql("accountPharmacySettingId"),
                model.AccountProvinceId.ToSql("accountProvinceId"),
                model.AccountEmail.ToSql("accountEmail"),
                model.AccountCity.ToSql("accountCity"),
                //model.AccountIsActivated.ToSql("accountIsActivated"),
                model.AccountIsOptin.ToSql("accountIsOptin"),
                model.AccountFrom.ToSql("accountFrom"),
                model.AccountTo.ToSql("accountTo"),
            };

            return await ExecuteReaderCollectionAsync<vFeedbackReport>("test.vFeedbackReport", sqlParams);
        }
    }
}