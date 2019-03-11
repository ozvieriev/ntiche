using System;

namespace Site.Data.Entities.Test
{
    public class vFeedbackReport : BaseEntity
    {
        public string AccountFirstName { get; set; }
        public string AccountLastName { get; set; }
        public string AccountPharmacistLicense { get; set; }
        public string AccountPharmacySettingName { get; set; }
        public string AccoutnProvinceName { get; set; }
        public string AccountEmail { get; set; }
        public string AccountOcupation { get; set; }
        public string AccountCountryName { get; set; }
        public string AccountCity { get; set; }
        //public bool AccountIsActivated { get; set; }
        public bool AccountIsOptin { get; set; }
        public DateTime AccountCreateDateUtc { get; set; }

        public byte FeedbackEnhancedRating { get; set; }
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
        public bool FeedbackIsAppreciateDelivery { get; set; }
        public bool FeedbackIsPerceiveDegree { get; set; }
        public string FeedbackPerceiveDegreeComments { get; set; }
        public string FeedbackChangesComments { get; set; }
        public string FeedbackTopicsComments { get; set; }
        public string FeedbackAdditionalComments { get; set; }
        public DateTime FeedbackCreateDateUtc { get; set; }
    }
}
