using System;
using System.Collections.Generic;

namespace ProjectApi.Data;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool? TwoFactorEnabled { get; set; }

    public bool? AdminPermission { get; set; }

    public string? TwoFactorSecretKey { get; set; }

    public DateTime? TwoFactorExpiryTime { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<PracticePaper> PracticePapers { get; set; } = new List<PracticePaper>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual ICollection<TestTrainee> TestTrainees { get; set; } = new List<TestTrainee>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
