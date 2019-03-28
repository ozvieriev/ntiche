using System.ComponentModel.DataAnnotations;

namespace Site.UI.Models
{
    public class ExamPostTestQuestionViewModel
    {
        [Required, MaxLength(5000)]
        public string Question { get; set; }
    }
}