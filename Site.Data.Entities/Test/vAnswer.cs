﻿using System;

namespace Site.Data.Entities.Test
{
    public class vAnswer : Entity<Guid>
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}