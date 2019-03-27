using System;

namespace Site.UI.Models
{
    public class ExamPostResponseViewModel : DescriptionViewModel
    {
        public ExamPostResponseViewModel(string description) :
            base(description)
        {

        }
        public Guid ExamResultId { get; set; }
        public int? TotalFeedbacks { get; set; }
        public int? TotalFailedPostTests { get; set; }
        public bool IsSuccess { get; set; }
    }
}