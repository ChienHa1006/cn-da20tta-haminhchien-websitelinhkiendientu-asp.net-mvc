using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSiteLKDT.Models;

public partial class SanPham
{
	public int MaSanPham { get; set; }

    public string? TenSanPham { get; set; }

    public string? Anh { get; set; }

    public int? SoLuong { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Mota { get; set; }

    public string? MaDanhMuc { get; set; }

    public int? MaNhaCungCap { get; set; }

    public int? GiaBan { get; set; }

    public int? GiaKhuyenMai { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual DanhMucSanPham? MaDanhMucNavigation { get; set; }

    public virtual NhaCungCap? MaNhaCungCapNavigation { get; set; }
}
