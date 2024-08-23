using System;
using System.Collections.Generic;

namespace ProjectApi.Models;

public partial class Result
{
    public int ResultId { get; set; }

    public int? TestId { get; set; }

    public int? UserId { get; set; }

    public decimal Score { get; set; }

    public int NoOfRightAnswers { get; set; }

    public int NoOfWrongAnswers { get; set; }

    public decimal Percentage { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Test? Test { get; set; }

    public virtual User? User { get; set; }
}
