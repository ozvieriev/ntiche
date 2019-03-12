namespace Site.UI.Models
{
    public class ExamResultReportGetViewModel
    {
        public string AccountFirstName { get; set; }
        public string AccountLastName { get; set; }
        public string AccountPharmacistLicense { get; set; }
        public int? AccountPharmacySettingId { get; set; }
        public string AccountProvinceId { get; set; }
        public string AccountEmail { get; set; }
        public string AccountCity { get; set; }
        //public bool? AccountIsActivated { get; set; }
        public bool? AccountIsOptin { get; set; }
        public int? AccountFrom { get; set; }
        public int? AccountTo { get; set; }

        public string ExamName { get; set; }
        public bool? ExamResultIsSuccess { get; set; }
    }
}