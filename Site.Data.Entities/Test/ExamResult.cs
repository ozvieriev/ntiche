using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site.Data.Entities.Test
{
    public class ExamResult : Entity<Guid>
    {
        public Guid AccountId { get; set; }
        public Guid ExamId { get; set; }

        public byte Answer0 { get; set; }
        public byte Answer1 { get; set; }
        public byte Answer2 { get; set; }
        public byte Answer3 { get; set; }
        public byte Answer4 { get; set; }
        public byte Answer5 { get; set; }
        public byte Answer6 { get; set; }

        public byte PercentCorrect { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed), DataType(DataType.DateTime)]
        public DateTime CreateDateUtc { get; set; }
    }
}
