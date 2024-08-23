using System;
using System.Collections.Generic;

namespace ProjectApi.Data;

public partial class TestTrainee
{
    public int TestTraineeId { get; set; }

    public int? TestId { get; set; }

    public int? TraineeId { get; set; }

    public string? Status { get; set; }

    public decimal? Score { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Test? Test { get; set; }

    public virtual User? Trainee { get; set; }
}
