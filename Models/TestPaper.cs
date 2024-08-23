using System;
using System.Collections.Generic;

namespace ProjectApi.Models;

public partial class TestPaper
{
    public int TestPaperId { get; set; }

    public int? TestId { get; set; }

    public string TestPath { get; set; } = null!;

    public virtual Test? Test { get; set; }
}
