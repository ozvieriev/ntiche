using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site.Data.Entities.Test
{
    public class vFeedbackReport : BaseEntity
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

        public byte FeedbackOverallLearningObjectives1Before { get; set; }
        public byte FeedbackOverallLearningObjectives1After { get; set; }
        public byte FeedbackOverallLearningObjectives1Relevance { get; set; }
        public byte FeedbackOverallLearningObjectives2Before { get; set; }
        public byte FeedbackOverallLearningObjectives2After { get; set; }
        public byte FeedbackOverallLearningObjectives2Relevance { get; set; }
        public byte FeedbackOverallLearningObjectives3Before { get; set; }
        public byte FeedbackOverallLearningObjectives3After { get; set; }
        public byte FeedbackOverallLearningObjectives3Relevance { get; set; }
        public byte FeedbackProgramRating { get; set; }
        public byte FeedbackProgramRatingEducational { get; set; }
        public byte? FeedBackMeetStatedLearningObjectives { get; set; }
        public bool FeedbackIsSufficientTime { get; set; }
        public bool FeedbackIsAppreciateDelivery { get; set; }
        public bool? FeedbackIsPerceiveDegree { get; set; }
        public string FeedbackPerceiveDegreeComments { get; set; }
        public bool FeedbackIsDisclosureStatement { get; set; }
        public string FeedbackChangesComments { get; set; }
        public string FeedbackAdditionalComments { get; set; }
        public DateTime FeedbackCreateDate { get; set; }

        [NotMapped]
        public int HumanFeedbackOverallLearningObjectives1Before { get { return FeedbackOverallLearningObjectives1Before + 1; } }

        [NotMapped]
        public int HumanFeedbackOverallLearningObjectives1After { get { return FeedbackOverallLearningObjectives1After + 1; } }

        [NotMapped]
        public int HumanFeedbackOverallLearningObjectives1Relevance { get { return FeedbackOverallLearningObjectives1Relevance + 1; } }

        [NotMapped]
        public int HumanFeedbackOverallLearningObjectives2Before { get { return FeedbackOverallLearningObjectives2Before + 1; } }

        [NotMapped]
        public int HumanFeedbackOverallLearningObjectives2After { get { return FeedbackOverallLearningObjectives2After + 1; } }

        [NotMapped]
        public int HumanFeedbackOverallLearningObjectives2Relevance { get { return FeedbackOverallLearningObjectives2Relevance + 1; } }

        [NotMapped]
        public int HumanFeedbackOverallLearningObjectives3Before { get { return FeedbackOverallLearningObjectives3Before + 1; } }

        [NotMapped]
        public int HumanFeedbackOverallLearningObjectives3After { get { return FeedbackOverallLearningObjectives3After + 1; } }

        [NotMapped]
        public int HumanFeedbackOverallLearningObjectives3Relevance { get { return FeedbackOverallLearningObjectives3Relevance + 1; } }

        [NotMapped]
        public int HumanFeedBackMeetStatedLearningObjectives { get { return FeedBackMeetStatedLearningObjectives.HasValue ? FeedBackMeetStatedLearningObjectives.Value + 1 : 0; } }

        [NotMapped]
        public int HumanFeedbackProgramRating { get { return FeedbackProgramRating + 1; } }

        [NotMapped]
        public int HumanFeedbackProgramRatingEducational { get { return FeedbackProgramRatingEducational + 1; } }
    }
}
