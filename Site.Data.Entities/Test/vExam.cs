using System;
using System.Collections.Generic;

namespace Site.Data.Entities.Test
{
    public class vExam : Entity<Guid?>
    {
        public List<vQuestion> Questions { get; set; }
    }
}