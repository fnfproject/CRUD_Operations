using System;
using System.Collections.Generic;

namespace ProjectApi.Models;

public partial class TestTrainee
{
    public int TestTraineeId { get; set; }

    public int? TestId { get; set; }

    public int? UserId { get; set; }

    public string? Status { get; set; }

    public decimal? Score { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Test? Test { get; set; }

    public virtual User? User { get; set; }
}
