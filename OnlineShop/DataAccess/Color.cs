using System;
using System.Collections.Generic;

namespace OnlineShop.DataAccess;

public partial class Color
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public string Name { get; set; }

    public string ProductImg { get; set; }

    public int? Quantity { get; set; }

    public virtual Product Product { get; set; }

    public virtual ICollection<Thumbnail> Thumbnails { get; set; } = new List<Thumbnail>();
}
