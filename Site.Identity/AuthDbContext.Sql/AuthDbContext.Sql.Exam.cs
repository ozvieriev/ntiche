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
        internal async Task<vExamResult> ExamResultGetAsync(Guid examResultId)
        {
            var sqlParams = new SqlParameter[] {
                examResultId.ToSql("id")
            };

            return await ExecuteReaderAsync<vExamResult>("[test].[vExamResultById]", sqlParams);
        }
        internal async Task<Exam> ExamGetAsync(string name)
        {
            var sqlParams = new SqlParameter[] {
                name.ToSql("name")
            };

            return await ExecuteReaderAsync<Exam>("[test].[pExamGetByName]", sqlParams);
        }

        internal async Task<List<vExamResultReport>> ExamResultReportAsync(vExamResultReportViewModel model = null)
        {
            model = model ?? new vExamResultReportViewModel();

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
                model.ExamName.ToSql("examName"),
                model.ExamResultIsSuccess.ToSql("examResultIsSuccess"),
            };

            return await ExecuteReaderCollectionAsync<vExamResultReport>("test.vExamResultReport", sqlParams);
        }

        internal async Task<List<vExamQuestionReport>> ExamQuestionReportAsync(vExamQuestionReportViewModel model = null)
        {
            model = model ?? new vExamQuestionReportViewModel();

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
                model.AccountTo.ToSql("accountTo")
            };

            return await ExecuteReaderCollectionAsync<vExamQuestionReport>("test.vExamQuestionReport", sqlParams);
        }
    }
}