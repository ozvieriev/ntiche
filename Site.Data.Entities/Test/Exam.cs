using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Data.Entities.Test
{
    public class Exam : Entity<Guid>
    {
        [Required]
        public string Name { get; set; }
    }
}
