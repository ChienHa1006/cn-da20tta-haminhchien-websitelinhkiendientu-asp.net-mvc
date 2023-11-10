using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebSiteLKDT.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace WSLKDT.Areas.Admin.Controllers
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
            if (ModelState.IsValid)
            {
                db.Update(sanpham);
                db.SaveChanges();
                return RedirectToAction("Danhmucsanpham", "Admin");
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
    }

}

