using System;
using System.Collections.Generic;

namespace ProjectApi.Data;

public partial class Result
{
    public int ResultId { get; set; }

    public int? TestId { get; set; }

    public int? TraineeId { get; set; }

    public decimal Score { get; set; }

    public decimal Percentage { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Test? Test { get; set; }

    public virtual User? Trainee { get; set; }
}
