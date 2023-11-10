using System;
using System.Collections.Generic;

namespace WebSiteLKDT.Models;

public partial class LbUser
{
    public int MaUser { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? DiaChi { get; set; }

    public string? DienThoai { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    public virtual LbRole? Role { get; set; }
}
