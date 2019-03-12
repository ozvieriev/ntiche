using System;

namespace Site.Identity.Models
{
    public class vExamResultReportViewModel
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
        public DateTime? AccountFromUtc { get; set; }
        public DateTime? AccountToUtc { get; set; }
    }
}
