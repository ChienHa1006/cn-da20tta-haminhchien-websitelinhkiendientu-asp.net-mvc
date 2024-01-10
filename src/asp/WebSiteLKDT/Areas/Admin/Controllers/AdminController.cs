using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebSiteLKDT.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace WebSiteLKDT.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin")]
	public class AdminController : Controller
	{
		WslkdtContext db = new WslkdtContext();

		[Route("")]
		public IActionResult Index()
		{
			return View();
		}

		[Route("Danhmucsanpham")]
		public IActionResult Danhmucsanpham()
		{
			var lstSanPham = db.SanPhams.ToList();
			return View(lstSanPham);
		}

        [Route("Themsanpham")]
        [HttpGet]
        public IActionResult Themsanpham()
        {
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucSanPhams.ToList(), "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaNhaCungCap = new SelectList(db.NhaCungCaps.ToList(), "MaNhaCungCap", "TenNhaCungCap");

            return View();
        }
        [Route("Themsanpham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Themsanpham(SanPham sanpham)
        {
            if (ModelState.IsValid)
            {
				int maxMaSanPham = db.SanPhams.Max(sp => (int?)sp.MaSanPham) ?? 0;

				// Cộng thêm 1 để tạo mã sản phẩm mới
				sanpham.MaSanPham = maxMaSanPham + 1;
				db.SanPhams.Add(sanpham);
                db.SaveChanges();
                return RedirectToAction("Danhmucsanpham");
            }
            return View(sanpham);
        }

        [Route("Suasanpham")]
        [HttpGet]
        public IActionResult Suasanpham(int maSanPham)
        {
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucSanPhams.ToList(), "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaNhaCungCap = new SelectList(db.NhaCungCaps.ToList(), "MaNhaCungCap", "TenNhaCungCap");
            var sanPham = db.SanPhams.Find(maSanPham);
            return View(sanPham);
        }
        [Route("Suasanpham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Suasanpham(SanPham sanpham)
        {
            try
            {
                db.Update(sanpham);
                db.SaveChanges();
                return RedirectToAction("Danhmucsanpham", "Admin");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        

            return View(sanpham);
        }

        [Route("Xoasanpham")]
        [HttpGet]
        public IActionResult Xoasanpham(int maSanPham)
        {
            db.Remove(db.SanPhams.Find(maSanPham));
            db.SaveChanges();
            return RedirectToAction("Danhmucsanpham", "Admin");

        }

        [Route("Danhmuc")]
        public IActionResult Danhmuc()
        {
            var lstdanhmuc = db.DanhMucSanPhams.ToList();
            return View(lstdanhmuc);
        }

        [Route("Themdanhmuc")]
        [HttpGet]
        public IActionResult Themdanhmuc()
        {
            return View();
        }
        [Route("Themdanhmuc")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Themdanhmuc(DanhMucSanPham danhmuc)
        {
            if (ModelState.IsValid)
            {
                db.DanhMucSanPhams.Add(danhmuc);
                db.SaveChanges();
                return RedirectToAction("Danhmuc");
            }
            return View(danhmuc);
        }


        [Route("Xoadanhmuc")]
        [HttpGet]
        public IActionResult Xoadanhmuc(string madanhmuc)
        {
            if (string.IsNullOrEmpty(madanhmuc))
            {
               
                return RedirectToAction("Danhmuc", "Admin");
            }

            var danhMuc = db.DanhMucSanPhams.Find(madanhmuc);

            if (danhMuc != null)
            {
                db.Remove(danhMuc);
                db.SaveChanges();
            }

            return RedirectToAction("Danhmuc", "Admin");
        }


        [Route("Taikhoan")]
        public IActionResult Taikhoan()
        {
            var taikhoan = db.LbUsers.ToList();
            return View(taikhoan);
        }

        [Route("Suataikhoan")]
        [HttpGet]
        public IActionResult Suataikhoan(int MaUser)
        { 
            var user = db.LbUsers.Find(MaUser);
            return View(user);
        }
        [Route("Suataikhoan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Suataikhoan(LbUser user)
        {
            try
            {
                db.Update(user);
                db.SaveChanges();
                return RedirectToAction("Taikhoan", "Admin");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }


            return View(user);
        }


        [Route("Xoataikhoan")]
        [HttpGet]
        public IActionResult Xoataikhoan(int MaUser)
        {
            var user = db.LbUsers.Find(MaUser);

                db.Remove(user);
                db.SaveChanges();

            return RedirectToAction("Taikhoan", "Admin");
        }


        [Route("Donhang")]
        public IActionResult Donhang()
        {
            var lstdonhang = db.DonHangs.ToList();
            return View(lstdonhang);

        }

		[Route("Chitietdonhang")]
		public IActionResult Chitietdonhang(int maDonHang)
		{
			var username = HttpContext.Session.GetString("Username");

			if (username != null)
			{

				var currentUser = db.LbUsers.SingleOrDefault(u => u.Username == username);

				if (currentUser != null)
				{

					var danhsachchitiet = db.ChiTietDonHangs
						.Where(d => d.MaDonHang == maDonHang)
						.ToList();

					return View(danhsachchitiet);
				}
			}


			return RedirectToAction("TrangChu", "Home");

		}



	}
}
