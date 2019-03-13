﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DateTime AccountCreateDate { get; set; }

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
        public DateTime FeedbackCreateDate { get; set; }

        [NotMapped]
        public int HumanFeedbackEnhancedRating { get { return FeedbackEnhancedRating + 1; } }

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
    }
}