using System;
using System.Collections.Generic;

namespace ProjectApi.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool? Is2Faenabled { get; set; }

    public bool? IsAdminApproved { get; set; }

    public string? TwoFactorSecretKey { get; set; }

    public DateTime? CreactedAt { get; set; }

    public bool? IsVerified { get; set; }

    public virtual ICollection<PracticePaper> PracticePapers { get; set; } = new List<PracticePaper>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual ICollection<TestTrainee> TestTrainees { get; set; } = new List<TestTrainee>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
