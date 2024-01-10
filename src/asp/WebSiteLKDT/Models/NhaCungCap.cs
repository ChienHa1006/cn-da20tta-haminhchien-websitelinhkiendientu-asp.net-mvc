using System;
using System.Collections.Generic;

namespace WebSiteLKDT.Models;

public partial class NhaCungCap
{
    public int MaNhaCungCap { get; set; }

    public string? TenNhaCungCap { get; set; }

    public string? DiaChi { get; set; }

    public string? DienThoai { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
