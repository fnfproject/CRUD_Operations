using System;
using System.Collections.Generic;

namespace ProjectApi.Data;

public partial class TestPaper
{
    public int TestPaperId { get; set; }

    public int? TestId { get; set; }

    public int? QuestionId { get; set; }

    public virtual Question? Question { get; set; }

    public virtual Test? Test { get; set; }
}
