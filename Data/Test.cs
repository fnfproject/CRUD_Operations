using System;
using System.Collections.Generic;

namespace ProjectApi.Data;

public partial class Test
{
    public int TestId { get; set; }

    public string TestName { get; set; } = null!;

    public int? TestMaxMarks { get; set; }

    public int? TestNoOfQuestions { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime ExpiryTime { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual ICollection<TestPaper> TestPapers { get; set; } = new List<TestPaper>();

    public virtual ICollection<TestTrainee> TestTrainees { get; set; } = new List<TestTrainee>();
}
