using System;
using System.Collections.Generic;

namespace WebSiteLKDT.Models;

public partial class DanhMucSanPham
{
    public string MaDanhMuc { get; set; } = null!;

    public string? TenDanhMuc { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
