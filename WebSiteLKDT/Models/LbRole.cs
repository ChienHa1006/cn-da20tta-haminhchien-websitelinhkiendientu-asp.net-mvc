using System;
using System.Collections.Generic;

namespace WebSiteLKDT.Models;

public partial class LbRole
{
    public int RoleId { get; set; }

    public string? TenVaiTro { get; set; }

    public virtual ICollection<LbUser> LbUsers { get; set; } = new List<LbUser>();
}
