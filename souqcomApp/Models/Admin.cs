using System;
using System.Collections.Generic;

namespace souqcomApp.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string AdminUserName { get; set; } = null!;

    public string AdminPassword { get; set; } = null!;
}
