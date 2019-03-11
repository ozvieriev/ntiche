using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site.Data.Entities.Test
{
    public class vExamResult : BaseEntity
    {
        public string ExamName { get; set; }
        public DateTime CreateDateUtc { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalCorrectAnswers { get; set; }

        [NotMapped]
        public bool IsSuccess
        {
            get { return (decimal)(TotalCorrectAnswers / TotalQuestions) * 100 >= 70; }
        }
    }
}