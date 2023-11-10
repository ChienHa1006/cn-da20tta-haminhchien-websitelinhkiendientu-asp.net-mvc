using WebSiteLKDT.Models;

namespace WebSiteLKDT.Repository
{
	public class ILoaiSPRepository : LoaiSPRepository
	{
		private readonly WslkdtContext _context;
		public ILoaiSPRepository(WslkdtContext context)
		{
			_context = context;
		}
		public DanhMucSanPham Add(DanhMucSanPham TenDanhMuc)
		{
			_context.DanhMucSanPhams.Add(TenDanhMuc);
			_context.SaveChanges();
			return TenDanhMuc;
		}

		public DanhMucSanPham Delete(int MaDanhMuc)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<DanhMucSanPham> GetAllDanhMuc()
		{
			return _context.DanhMucSanPhams;
		}

		public DanhMucSanPham GetDanhMuc(int MaDanhMuc)
		{
			return _context.DanhMucSanPhams.Find(MaDanhMuc);
		}

		public DanhMucSanPham Update(DanhMucSanPham TenDanhMuc)
		{
			_context.Update(TenDanhMuc);
			_context.SaveChanges();
			return TenDanhMuc;
		}
	}
}
