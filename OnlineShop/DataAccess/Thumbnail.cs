using System;
using System.Collections.Generic;

namespace OnlineShop.DataAccess;

public partial class Thumbnail
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? ColorId { get; set; }

    public string Thumbnail1 { get; set; }

    public virtual Color Color { get; set; }
}
