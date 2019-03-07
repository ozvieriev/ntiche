using Site.Data.Entities.Test;
using System;
using System.Threading.Tasks;

namespace Site.Identity
{
    public interface ITestRepository : IDisposable
    {
        Task<vExam> vExamGetByLanguageIso2Async(string languageIso2);
    }
}