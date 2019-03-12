using Site.Data.Entities.Test;
using Site.Identity.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class AuthDbContext
    {
        internal async Task<Exam> ExamGetAsync(string name)
        {
            var sqlParams = new SqlParameter[] {
                name.ToSql("name")
            };

            return await ExecuteReaderAsync<Exam>("[test].[pExamGetByName]", sqlParams);
        }
        public async Task ExamResultInsertAsync(vExamResultViewModel model)
        {
            var sqlParams = new SqlParameter[]
            {
                model.ExamName.ToSql("name"),
                model.AccountId.ToSql("accountId"),
                model.Answer0.ToSql("answer0"),
                model.Answer1.ToSql("answer1"),
                model.Answer2.ToSql("answer2"),
                model.Answer3.ToSql("answer3"),
                model.Answer4.ToSql("answer4"),
                model.Answer5.ToSql("answer5"),
                model.Answer6.ToSql("answer6"),
                model.PercentCorrect.ToSql("percentCorrect")
            };

            await ExecuteNonQueryAsync("test.pExamResultInsert", sqlParams);
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
                model.AccountFromUtc.ToSql("accountFromUtc"),
                model.AccountToUtc.ToSql("accountToUtc"),
            };

            return await ExecuteReaderCollectionAsync<vExamResultReport>("test.vExamResultReport", sqlParams);
        }
    }
}