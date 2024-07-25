using System;
using System.Collections.Generic;

namespace souqcomApp.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int CartUserId { get; set; }

    public int CartItemId { get; set; }

    public int CartQuantity { get; set; }

    public virtual Item CartItem { get; set; } = null!;

    public virtual User CartUser { get; set; } = null!;
}
