using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site.Data.Entities.Test
{
    public class Feedback : Entity<int>
    {
        public Guid AccountId { get; set; }

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
        public byte ProgramRatingEducational { get; set; }

        public bool IsAppreciateDelivery { get; set; }

        public byte? MeetStatedLearningObjectives { get; set; }

        public bool? IsPerceiveDegree { get; set; }

        [MaxLength(4000)]
        public string PerceiveDegreeComments { get; set; }

        [Required, MaxLength(4000)]
        public string ChangesComments { get; set; }

        [MaxLength(4000)]
        public string TopicsComments { get; set; }

        [MaxLength(4000)]
        public string AdditionalComments { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed), DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
    }
}
