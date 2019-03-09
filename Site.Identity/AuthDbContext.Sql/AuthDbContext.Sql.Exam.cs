using Site.Data.Entities.Test;
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
        //create type test.examResultSet as table
        //(
        //    [answerId] [uniqueidentifier] NOT NULL
        //)

        public async Task ExamResultInsertAsync(Guid accountId, IEnumerable<Guid> answers)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("answerId", typeof(Guid));

            foreach (var answer in answers)
                dataTable.Rows.Add(answer);

            var _answers = dataTable.ToSql("answers");
            _answers.SqlDbType = SqlDbType.Structured;
            _answers.TypeName = "test.examResultSet";

            var sqlParams = new SqlParameter[]
            {
                accountId.ToSql("accountId"),
                _answers
            };
                       
            await ExecuteNonQueryAsync("test.pExamResultInsert", sqlParams);
        }

        internal async Task<vExam> vExamGetByNameAsync(string name, string languageIso2)
        {
            var sqlParams = new SqlParameter[]
            {
                name.ToSql("name"),
                languageIso2.ToSql("languageIso2")
            };

            using (var cmd = Database.Connection.CreateCommand())
            {
                cmd.CommandText = sqlParams.CommandText("test.vExamGetByName");

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
        internal async Task<vExam> vExamGetByExamResultIdAsync(Guid examResultId, string languageIso2)
        {
            var sqlParams = new SqlParameter[]
            {
                examResultId.ToSql("examResultId"),
                languageIso2.ToSql("languageIso2")
            };

            using (var cmd = Database.Connection.CreateCommand())
            {
                cmd.CommandText = sqlParams.CommandText("test.vExamGetByExamResultId");

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