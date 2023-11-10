using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebSiteLKDT.Models;
using X.PagedList;

namespace WebSiteLKDT.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		WslkdtContext db = new WslkdtContext();
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult TrangChu(int? page)
		{
			int pageSize = 8;
			int pageNumber=page==null||page<0?1:page.Value;
			var listsanpham = db.SanPhams.AsNoTracking().OrderBy(x=>x.TenSanPham);
			PagedList<SanPham> lst = new PagedList<SanPham>(listsanpham,pageNumber,pageSize);
			return View(lst);
		}
		public IActionResult TrangGioiThieu()
		{
			return View();
		}

		[HttpGet]
		public IActionResult TrangDangNhap()
		{

			if (HttpContext.Session.GetString("Username") == null)
			{
				return View();
			}
			else
			{
				return RedirectToAction("TrangChu", "Home");
			}
		}
		[HttpPost]
		public IActionResult TrangDangNhap(LbUser user)
		{
			if (HttpContext.Session.GetString("Username") == null)
			{
				var u = db.LbUsers.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
				if (u != null)
				{
					if (u.RoleId == 0)
					{

						HttpContext.Session.SetString("Username", u.Username.ToString());
						HttpContext.Session.SetString("Role", "0");
						return RedirectToAction("TrangChu", "Home");
					}
					else
					{
						HttpContext.Session.SetString("Username", u.Username.ToString());
						HttpContext.Session.SetString("Role", "1");
						return RedirectToAction("TrangChu", "Home");
					}

				}
			}
			return View();
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("Username");
			return RedirectToAction("TrangChu", "Home");
		}

		public IActionResult TrangDangKy()
		{
			return View();
		}

		
	
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}