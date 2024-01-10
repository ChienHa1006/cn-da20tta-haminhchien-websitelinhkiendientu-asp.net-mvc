using System;
using System.Collections.Generic;

namespace WebSiteLKDT.Models;

public partial class GioHang
{
    public int MaGioHang { get; set; }

    public int? MaUser { get; set; }

    public virtual LbUser? MaUserNavigation { get; set; }

    
}
