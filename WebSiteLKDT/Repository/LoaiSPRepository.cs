using WebSiteLKDT.Models;

namespace WebSiteLKDT.Repository
{
	public interface LoaiSPRepository
	{
		DanhMucSanPham Add(DanhMucSanPham TenDanhMuc);
		DanhMucSanPham Update(DanhMucSanPham TenDanhMuc);
		DanhMucSanPham Delete(int MaDanhMuc);
		DanhMucSanPham GetDanhMuc(int MaDanhMuc);

		IEnumerable<DanhMucSanPham> GetAllDanhMuc();
	}
}
