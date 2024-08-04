using System;
using System.Collections.Generic;

namespace souqcomApp.Models;

public partial class AspNetRole
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? ConcurrencyStamp { get; set; }

    public string? NormalizedName { get; set; }

    public virtual ICollection<AspNetUser> Users { get; set; } = new List<AspNetUser>();
}
