using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSiteLKDT.Models;

public partial class LbUser
{
    public int MaUser { get; set; }

    public string? Username { get; set; }

	[Column(TypeName = "nvarchar(max)")]
	public string? Password { get; set; }


    public string? Email { get; set; }

    public string? DiaChi { get; set; }

    public string? DienThoai { get; set; }

    public int? RoleId { get; set; }

    public DateTime? NgayTao { get; set; }

    public string? TrangThai { get; set; }

	public string? HoTen { get; set; }
	public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();
}
