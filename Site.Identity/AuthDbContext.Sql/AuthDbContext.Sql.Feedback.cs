using Site.Data.Entities.Test;
using Site.Identity.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class AuthDbContext
    {

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
                model.AccountFromUtc.ToSql("accountFromUtc"),
                model.AccountToUtc.ToSql("accountToUtc"),
            };

            return await ExecuteReaderCollectionAsync<vFeedbackReport>("test.vFeedbackReport", sqlParams);
        }
    }
}