using System;
using System.Collections.Generic;

namespace Site.Data.Entities.Test
{
    public class vQuestion: Entity<Guid>
    {
        public string Text { get; set; }

        public List<vAnswer> Answers { get; set; }
    }
}
