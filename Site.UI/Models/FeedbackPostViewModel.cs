using System.ComponentModel.DataAnnotations;

namespace Site.UI.Models
{
    public class FeedbackPostViewModel : DescriptionViewModel
    {
        public FeedbackPostViewModel(string description)
            : base(description)
        {
        }

        public byte EnhancedRating { get; set; }
        public byte OverallLearningObjectives1Before { get; set; }
        public byte OverallLearningObjectives1After { get; set; }
        public byte OverallLearningObjectives1Relevance { get; set; }
        public byte OverallLearningObjectives2Before { get; set; }
        public byte OverallLearningObjectives2After { get; set; }
        public byte OverallLearningObjectives2Relevance { get; set; }
        public byte OverallLearningObjectives3Before { get; set; }
        public byte OverallLearningObjectives3After { get; set; }
        public byte OverallLearningObjectives3Relevance { get; set; }
        public byte ProgramRating { get; set; }

        public bool IsAppreciateDelivery { get; set; }
        public bool IsPerceiveDegree { get; set; }

        [Required, MaxLength(4000)]
        public string PerceiveDegreeComments { get; set; }

        [Required, MaxLength(4000)]
        public string ChangesComments { get; set; }

        [Required, MaxLength(4000)]
        public string TopicsComments { get; set; }

        [Required, MaxLength(4000)]
        public string AdditionalComments { get; set; }
    }
}