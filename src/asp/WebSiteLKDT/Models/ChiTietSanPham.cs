using System;
using System.Collections.Generic;

namespace WebSiteLKDT.Models;

public partial class ChiTietSanPham
{
    public int MaChiTietSanPham { get; set; }

    public int? MaSanPham { get; set; }

    public string? MoTa { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
