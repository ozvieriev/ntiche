using System;

namespace Site.Data.Entities.Test
{
    public class vExamResultReport : BaseEntity
    {
        public string AccountFirstName { get; set; }
        public string AccountLastName { get; set; }
        public string AccountPharmacistLicense { get; set; }
        public string AccountPharmacySettingName { get; set; }
        public string AccoutnProvinceName { get; set; }
        public string AccountEmail { get; set; }
        public string AccountOcupation { get; set; }
        public string AccountCountryName { get; set; }
        public string AccountCity { get; set; }
        //public bool AccountIsActivated { get; set; }
        public bool AccountIsOptin { get; set; }
        public DateTime AccountCreateDateUtc { get; set; }

        public string ExamName { get; set; }

        public byte ExamResultAnswer0 { get; set; }
        public byte ExamResultAnswer1 { get; set; }
        public byte ExamResultAnswer2 { get; set; }
        public byte ExamResultAnswer3 { get; set; }
        public byte ExamResultAnswer4 { get; set; }
        public byte ExamResultAnswer5 { get; set; }
        public byte ExamResultAnswer6 { get; set; }
        public byte ExamResultPercentCorrect { get; set; }
        public DateTime ExamResultCreateDateUtc { get; set; }
    }
}
