using WebSiteLKDT.Models;

namespace WebSiteLKDT.Repository
{
	public interface ILoaiSpRepository
	{
		DanhMucSanPham Add(DanhMucSanPham TenDanhMuc);
		DanhMucSanPham Update(DanhMucSanPham TenDanhMuc);
		DanhMucSanPham Delete(string MaDanhMuc);
		DanhMucSanPham GetDanhMuc(string MaDanhMuc);

		IEnumerable<DanhMucSanPham>GetALLDanhMuc();
	}
}
