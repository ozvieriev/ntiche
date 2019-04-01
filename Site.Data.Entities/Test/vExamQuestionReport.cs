using System;

namespace Site.Data.Entities.Test
{
    public class vExamQuestionReport : BaseEntity
    {
        public string AccountFirstName { get; set; }
        public string AccountLastName { get; set; }
        public string AccountPharmacistLicense { get; set; }
        public string AccountPharmacySetting { get; set; }
        public string AccoutnProvinceName { get; set; }
        public string AccountEmail { get; set; }
        public string AccountSpecialty { get; set; }
        public string AccountCountryName { get; set; }
        public string AccountCity { get; set; }
        //public bool AccountIsActivated { get; set; }
        public bool AccountIsOptin { get; set; }
        public DateTime AccountCreateDate { get; set; }

        public string ExamQuestion { get; set; }
        public DateTime ExamQuestionCreateDate { get; set; }
    }
}
