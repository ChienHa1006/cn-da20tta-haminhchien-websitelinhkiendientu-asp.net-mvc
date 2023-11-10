using WebSiteLKDT.Models;
using Microsoft.AspNetCore.Mvc;
using WebSiteLKDT.Repository;

namespace WebSiteLKDT.ViewComponents
{
	[ViewComponent(Name = "LoaiSPMenu")]
	public class LoaiSPMenuViewComponents: ViewComponent
	{
		private readonly LoaiSPRepository _loaiSP;

		public  LoaiSPMenuViewComponents(LoaiSPRepository ILoaiSPRepository) {

			_loaiSP = ILoaiSPRepository;

		}
		public IViewComponentResult Invoke()
		{
			var loaisp = _loaiSP.GetAllDanhMuc().OrderBy(x=>x.TenDanhMuc);
			return View(loaisp);
		}
	}
}
