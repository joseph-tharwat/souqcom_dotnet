using System;
using System.Collections.Generic;

namespace souqcomApp.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public string? ItemDescription { get; set; }

    public string? ItemPhoto { get; set; }

    public int ItemPrice { get; set; }

    public int ItemCategoryId { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
