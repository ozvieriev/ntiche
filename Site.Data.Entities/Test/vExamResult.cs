﻿using System;

namespace Site.Data.Entities.Test
{
    public class vExamResult : Entity<Guid>
    {
        public string AccountFirstName { get; set; }
        public string AccountLastName { get; set; }
        public string AccountEmail { get; set; }
        public string AccountPharmacistLicense { get; set; }

        public bool ExamResultIsSuccess { get; set; }
        public DateTime ExamResultCreateDate { get; set; }
        public string ExamName { get; set; }
    }
}
