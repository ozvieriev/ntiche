namespace Site.UI.Models
{
    public class ExamPostViewModel : DescriptionViewModel
    {
        public ExamPostViewModel(string description, bool isSuccess)
            : base(description)
        {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; set; }
    }
}