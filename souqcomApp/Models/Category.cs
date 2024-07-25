using System;
using System.Collections.Generic;

namespace souqcomApp.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? CategoryDescription { get; set; }

    public string? CategoryPhoto { get; set; }
}
