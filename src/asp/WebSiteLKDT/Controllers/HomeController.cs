using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Text.Json;
using WebSiteLKDT.Models;
using X.PagedList;
using System.Configuration;
using System.Linq;
using Microsoft.CodeAnalysis;
using WebSiteLKDT.Repository;

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

		public IActionResult TrangChu(string selectedDanhMuc)
		{
			var danhMucs = db.SanPhams.Select(item => item.MaDanhMuc).Distinct().ToList();

			var model = new List<SanPham>();  
			foreach (var danhMuc in danhMucs)
			{
				var productsInDanhMuc = db.SanPhams.Where(sp => sp.MaDanhMuc == danhMuc).ToList();
				model.AddRange(productsInDanhMuc);
			}

			if (!string.IsNullOrEmpty(selectedDanhMuc))
			{
				model = model.Where(sp => sp.MaDanhMuc == selectedDanhMuc).ToList();
			}

			return View(model);
		}

        public IActionResult TrangTatcaSanPham(string selectedDanhMuc)
        {
			var danhMucs = db.SanPhams.Select(item => item.MaDanhMuc).Distinct().ToList();

			var model = new List<SanPham>();  
			foreach (var danhMuc in danhMucs)
			{
				var productsInDanhMuc = db.SanPhams.Where(sp => sp.MaDanhMuc == danhMuc).ToList();
				model.AddRange(productsInDanhMuc);
			}

			if (!string.IsNullOrEmpty(selectedDanhMuc))
			{
				model = model.Where(sp => sp.MaDanhMuc == selectedDanhMuc).ToList();
			}

			return View(model);
		}


		public IActionResult TrangDanhMuc(string masanpham)
		{
            var sanPhams = db.SanPhams.Where(sp => sp.MaDanhMuc == masanpham).ToList();
            return View(sanPhams);
        }


		public IActionResult TrangSanPham(int MaSP)
		{
			
			var sanPham = db.SanPhams.FirstOrDefault(sp => sp.MaSanPham == MaSP);
			ViewBag.MaNhaCungCap = new SelectList(db.NhaCungCaps.ToList(), "MaNhaCungCap", "TenNhaCungCap");
			
			if (sanPham == null)
			{
				return NotFound(); 
			}

			return View(sanPham); 
		}

		[HttpGet]
		public IActionResult TimKiemSanPham(string timkiem)
        {
           
            if (string.IsNullOrEmpty(timkiem))
            {
                return RedirectToAction("TrangChu", "Home");
            }

            var ketQuaTimKiem = db.SanPhams
                .Where(sp => sp.TenSanPham.Contains(timkiem))
                .ToList();

        
            return View(ketQuaTimKiem);
        }


        public IActionResult LocSanPham(double? giaMin, double? giaMax, int? nhaCungCap)
        {
           
            var sanPhams = db.SanPhams.AsQueryable();
			var danhSachNhaCungCap = db.NhaCungCaps.ToList();

			ViewData["DanhSachNhaCungCap"] = danhSachNhaCungCap;

			if (giaMin.HasValue)
            {
                sanPhams = sanPhams.Where(sp => sp.GiaBan >= giaMin);
            }

            if (giaMax.HasValue)
            {
                sanPhams = sanPhams.Where(sp => sp.GiaBan <= giaMax);
            }
            if (nhaCungCap.HasValue)
            {
                var tenNhaCungCap = GetTenNhaCungCap(nhaCungCap.Value);
                
            }

			
			return View(sanPhams.ToList());
		}

        public string GetTenNhaCungCap(int maNhaCungCap)
        {
            var nhaCungCap = db.NhaCungCaps.FirstOrDefault(ncp => ncp.MaNhaCungCap == maNhaCungCap);
            return nhaCungCap?.TenNhaCungCap ?? "N/A"; 
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

				
				ViewBag.AlertMessage = "Đăng nhập thành công.";
				return RedirectToAction("TrangChu", "Home");
			}
		}

		[HttpPost]
		public IActionResult TrangDangNhap(LbUser user)
		{
			if (HttpContext.Session.GetString("Username") == null)
			{
				string hashedPassword = GetMd5Hash(user.Password);
				var u = db.LbUsers.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(hashedPassword)).FirstOrDefault();
				if (u != null)
				{
					if (u.RoleId == 0)
					{
						if (HttpContext.Session.GetString("Username") == null)
						{
							HttpContext.Session.SetString("Username", u.Username.ToString());
							HttpContext.Session.SetString("Role", "0");
						}	
						ViewBag.Message = "Đăng nhập thành công!";
						return RedirectToAction("TrangChu", "Home");
					}
					else
					{
						if (HttpContext.Session.GetString("Username") == null)
						{
							HttpContext.Session.SetString("Username", u.Username.ToString());
							HttpContext.Session.SetString("Role", "1");
						}

						ViewBag.Message = "Đăng nhập thành công!";
						return RedirectToAction("TrangChu", "Home");
					}

				}
			}
			ViewBag.ErrorMessage = "Đăng nhập không thành công!";
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
		public IActionResult TrangDangKy(LbUser newUser)
		{
			if (HttpContext.Session.GetString("Username") == null)
			{
				if (IsUsernameExists(newUser.Username))
				{
					ModelState.AddModelError("Username", "Tên người dùng đã tồn tại");
					ViewBag.AlertMessage = "Tên người dùng đã tồn tại.";
					return View(newUser);
				}
				else
				{
					// Mã hóa mật khẩu bằng MD5
					string hashedPassword = GetMd5Hash(newUser.Password);

					newUser.RoleId = 0;
					newUser.MaUser = newUser.MaUser + 0;
					int maxMaUser = db.LbUsers.Max(u => u.MaUser);
					newUser.MaUser = maxMaUser + 1;

					// Sử dụng mật khẩu đã được mã hóa
					newUser.Password = hashedPassword;

					ViewBag.AlertMessage = "Đăng ký thành công.";
					db.LbUsers.Add(newUser);
					db.SaveChanges();

					HttpContext.Session.SetString("Username", newUser.Username.ToString());
					HttpContext.Session.SetString("Role", "0");

					return RedirectToAction("TrangChu", "Home");
				}
			}

			return View();
		}

		private string GetMd5Hash(string input)
		{
			using (MD5 md5Hash = MD5.Create())
			{
				byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

				StringBuilder sBuilder = new StringBuilder();

				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				return sBuilder.ToString();
			}
		}


		private bool IsUsernameExists(string username)
        {
            return db.LbUsers.Any(u => u.Username.Equals(username));
        }

        [HttpGet]
        public IActionResult EditUser()
        {
           
            if (HttpContext.Session.GetString("Username") != null)
            {
                string username = HttpContext.Session.GetString("Username");

                
                var user = db.LbUsers.FirstOrDefault(u => u.Username == username);

               
                if (user != null)
                {
                    return View(user);
                }
                else
                {
                    ViewBag.ErrorMessage = "Người dùng không tồn tại.";
                    return View("Error"); 
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Bạn không có quyền truy cập trang này.";
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(LbUser user)
        {
            if (ModelState.IsValid)
            {
                db.Update(user);
                db.SaveChanges();
                return RedirectToAction("TrangChu", "Home"); 
            }
            return View(user);
        }



        [HttpGet]
        public IActionResult EditPassword()
        {
            
            if (HttpContext.Session.GetString("Username") != null)
            {
                string username = HttpContext.Session.GetString("Username");

               
                var user = db.LbUsers.FirstOrDefault(u => u.Username == username);

                
                if (user != null)
                {
                   

                    
                    ViewBag.HashedPassword = user.Password;

                    return View(user); 
                }
                else
                {
                    ViewBag.ErrorMessage = "Người dùng không tồn tại.";
                    return View("Error"); 
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Bạn không có quyền truy cập trang này.";
                return View("Error"); 
            }
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditPassword(LbUser user)
		{
			if (ModelState.IsValid)
			{
				
				string hashedPassword = GetMd5Hash(user.Password);

				
				user.Password = hashedPassword;
				db.LbUsers.Attach(user);
				db.Entry(user).Property(u => u.Password).IsModified = true;
				db.SaveChanges();

				return RedirectToAction("Logout", "Home"); 
			}
			return View(user);
		}


	



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}