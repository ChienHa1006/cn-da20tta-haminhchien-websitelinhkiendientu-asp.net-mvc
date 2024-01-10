namespace WebSiteLKDT.Models
{
	public class CartItem
	{
		public int MaSanPham { get; set; }
		public string Hinh { get; set; }
		public string TenSp { get; set; }
		public double DonGia { get; set; }

		public int SoLuong {  get; set; } 

		public double ThanhTien  => SoLuong * DonGia ;

	}
}
