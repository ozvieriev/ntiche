using System;

namespace Site.Identity.Models
{
    public class vExamResultViewModel
    {
        public Guid AccountId { get; set; }
        public string ExamName { get; set; }
        public byte Answer0 { get; set; }
        public byte Answer1 { get; set; }
        public byte Answer2 { get; set; }
        public byte Answer3 { get; set; }
        public byte Answer4 { get; set; }
        public byte Answer5 { get; set; }
        public byte Answer6 { get; set; }

        public byte PercentCorrect { get; set; }
    }
}
