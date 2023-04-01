﻿using Comfy.Domain.Base;

namespace Comfy.Domain
{
    public class QuestionAnswer : Auditable
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public string Text { get; set; } = null!;
        public int UsefulAnswerCount { get; set; }
        public int NeedlessAnswerCount { get; set; }
        public bool IsActive { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }
}
