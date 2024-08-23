using System;
using System.Collections.Generic;

namespace ProjectApi.Data;

public partial class Question
{
    public int QuestionId { get; set; }

    public string Subject { get; set; } = null!;

    public string Topic { get; set; } = null!;

    public string DifficultyLevel { get; set; } = null!;

    public string QuestionText { get; set; } = null!;

    public string OptionA { get; set; } = null!;

    public string OptionB { get; set; } = null!;

    public string OptionC { get; set; } = null!;

    public string OptionD { get; set; } = null!;

    public string CorrectAnswer { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<TestPaper> TestPapers { get; set; } = new List<TestPaper>();
}
