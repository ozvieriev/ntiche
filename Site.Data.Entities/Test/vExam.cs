using System;
using System.Collections.Generic;

namespace Site.Data.Entities.Test
{
    public class vExam : BaseEntity
    {
        public List<vQuestion> Questions { get; set; }
    }
}