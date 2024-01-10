using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace WebSiteLKDT.Models;

public partial class ChiTietGioHang
{

	public int? MaGioHang { get; set; }

    public int? MaSanPham { get; set; }

    public int? TongSoLuong { get; set; }

    public double? TongGiaTien { get; set; }

    public double Total => (TongSoLuong ?? 0) + (TongGiaTien ?? 0);


	public SanPham SanPham { get;set; }

	public virtual GioHang? MaGioHangNavigation { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }

}
