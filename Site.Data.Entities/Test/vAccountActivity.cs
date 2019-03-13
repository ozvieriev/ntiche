using System;

namespace Site.Data.Entities.Test
{
    public class vAccountActivity
    {
        public int TotalPreTests { get; set; }
        public int TotalPostTests { get; set; }
        public int TotalFeedbacks { get; set; }
        public Guid? BestPostExamResultId { get; set; }
        public bool BestPostExamResultIsSuccess { get; set; }
    }
}