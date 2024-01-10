using WebSiteLKDT.Models;

namespace WebSiteLKDT.Repository
{
	public class LoaiSpRepository : ILoaiSpRepository
	{
		private readonly WslkdtContext _context;

		public LoaiSpRepository(WslkdtContext context)
		{
			_context = context;
		}

		public DanhMucSanPham Add(DanhMucSanPham TenDanhMuc)
		{
			_context.DanhMucSanPhams.Add(TenDanhMuc);
			_context.SaveChanges();
			return TenDanhMuc;
		}

		public DanhMucSanPham Delete(string MaDanhMuc)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<DanhMucSanPham> GetALLDanhMuc()
		{
			return _context.DanhMucSanPhams;
		}

		public DanhMucSanPham GetDanhMuc(string MaDanhMuc)
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
