using System;
using System.Collections.Generic;

namespace ProjectApi.Data;

public partial class PracticePaper
{
    public int PaperId { get; set; }

    public string PaperName { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }
}
