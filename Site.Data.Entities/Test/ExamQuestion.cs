using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site.Data.Entities.Test
{
    public class ExamQuestion : Entity<Guid>
    {
        public Guid AccountId { get; set; }
        public Guid ExamId { get; set; }

        public string Question { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed), DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
    }
}
