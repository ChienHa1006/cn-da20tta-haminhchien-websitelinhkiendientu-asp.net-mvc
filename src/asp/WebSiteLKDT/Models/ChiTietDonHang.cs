using System;
using System.Collections.Generic;

namespace WebSiteLKDT.Models;

public partial class ChiTietDonHang
{
    public int MaChiTietDonHang { get; set; }

    public int? MaDonHang { get; set; }

    public int? MaSanPham { get; set; }

    public int? SoLuong { get; set; }

    public double? DonGia { get; set; }

    public double? ThanhTien { get; set; }

    public virtual DonHang? MaDonHangNavigation { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
