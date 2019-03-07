using Site.Data.Entities.Test;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class AuthDbContext
    {
        internal async Task<vExam> vExamGetByLanguageIso2Async(string languageIso2)
        {
            var sqlParams = new SqlParameter[]
            {
                languageIso2.ToSql("languageIso2")
            };

            using (var cmd = Database.Connection.CreateCommand())
            {
                cmd.CommandText = sqlParams.CommandText("test.vExamGetByLanguageIso2");

                cmd.Parameters.AddRange(sqlParams);

                await Database.Connection.OpenAsync();

                var reader = await cmd.ExecuteReaderAsync();

                var questions = ((IObjectContextAdapter)this)
                    .ObjectContext
                    .Translate<vQuestion>(reader)
                    .ToList();

                var exam = new vExam { Questions = questions };
                if (await reader.NextResultAsync())
                {
                    var answers = ((IObjectContextAdapter)this)
                        .ObjectContext
                        .Translate<vAnswer>(reader)
                        .ToList();

                    foreach (var question in questions)
                    {
                        question.Answers = answers
                            .Where(answer => answer.QuestionId == question.Id)
                            .ToList();
                    }
                }

                Database.Connection.Close();

                return exam;
            }
        }
    }
}
