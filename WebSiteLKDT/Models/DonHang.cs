using System;
using System.Collections.Generic;

namespace WebSiteLKDT.Models;

public partial class DonHang
{
    public int MaDonHang { get; set; }

    public DateTime? NgayLap { get; set; }

    public int? MaUser { get; set; }

    public int? TongTien { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual LbUser? MaUserNavigation { get; set; }
}
