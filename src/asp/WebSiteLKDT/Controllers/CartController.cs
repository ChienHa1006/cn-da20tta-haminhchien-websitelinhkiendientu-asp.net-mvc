using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using WebSiteLKDT.Helpers;
using WebSiteLKDT.Models;

namespace WebSiteLKDT.Controllers
{
	public class CartController : Controller
	{
        private readonly WslkdtContext db;

        public CartController(WslkdtContext context)
        {
            db = context;
        }

		public void LuuDonHang(DonHang donHang)
		{
			db.DonHangs.Add(donHang);
			db.SaveChanges();
		}

		public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();

        public IActionResult Index()
		{
			return View(Cart);
		}

        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaSanPham == id);
            if (item == null)
            {
                var hangHoa = db.SanPhams.SingleOrDefault(p => p.MaSanPham == id);
                if (hangHoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy hàng hóa có mã {id}";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    MaSanPham = hangHoa.MaSanPham,
                    TenSp = hangHoa.TenSanPham,
                    DonGia = hangHoa.GiaBan ?? 0,
                    Hinh = hangHoa.Anh ?? string.Empty,
                    SoLuong = quantity
                };
                gioHang.Add(item);
            }
            else
            {   
                item.SoLuong += quantity;
            }

            HttpContext.Session.Set(MySetting.CART_KEY, gioHang);

            return RedirectToAction("Index");
        }

		public IActionResult DeleteToCart(int id, int quantity = 1)
		{
			var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.MaSanPham == id);
			if (item == null)
			{
				var hangHoa = db.SanPhams.SingleOrDefault(p => p.MaSanPham == id);
				if (hangHoa == null)
				{
					TempData["Message"] = $"Không tìm thấy hàng hóa có mã {id}";
					return Redirect("/404");
				}
				item = new CartItem
				{
					MaSanPham = hangHoa.MaSanPham,
					TenSp = hangHoa.TenSanPham,
					DonGia = hangHoa.GiaBan ?? 0,
					Hinh = hangHoa.Anh ?? string.Empty,
					SoLuong = quantity
				};
				gioHang.Add(item);
			}
			else
			{
				item.SoLuong -= quantity;
			}

			HttpContext.Session.Set(MySetting.CART_KEY, gioHang);

			return RedirectToAction("Index");
		}

		public IActionResult RemoveCart(int id)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaSanPham == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DonHang()
        {
            if (Cart.Count == 0) 
            {
                return Redirect("/");   
            }

			var madonhang = db.DonHangs.Max(d => (int?)d.MaDonHang) ?? 0;
			madonhang++;

            var machitietdonhang = db.ChiTietDonHangs.Max(d => (int?)d.MaChiTietDonHang) ?? 0;
            machitietdonhang++;

            var cart = Cart;

			
			var username = HttpContext.Session.GetString("Username");

			if (username != null)
			{
				
				var currentUser = db.LbUsers.SingleOrDefault(u => u.Username == username);

				if (currentUser != null)
				{
					
					var order = new DonHang
					{
						MaDonHang = madonhang,
						NgayLap = DateTime.Now,
						TongTien = cart.Sum(item => item.DonGia * item.SoLuong),
						MaUser = currentUser.MaUser, 
					};

                    if (ModelState.IsValid)
                    {
                        db.DonHangs.Add(order);
                        db.SaveChanges();

						foreach (var cartItem in cart)
						{
							var chiTietDonHang = new ChiTietDonHang
							{
								MaDonHang = madonhang,
								MaChiTietDonHang = machitietdonhang++,
								MaSanPham = cartItem.MaSanPham,
								SoLuong = cartItem.SoLuong,
								DonGia = cartItem.DonGia,
								ThanhTien = cartItem.SoLuong * cartItem.DonGia
							};

							db.ChiTietDonHangs.Add(chiTietDonHang);

						}

						db.SaveChanges();



						ClearCart();

						return View(new List<DonHang> { order });


					}

               
				}
			}
			return RedirectToAction("TrangDangNhap", "Home");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DonHang(DonHang donhang)
		{
			var order = new DonHang
			{
				MaDonHang = donhang.MaDonHang,
				NgayLap = donhang.NgayLap,
				TongTien = donhang.TongTien,
				MaUser = donhang.MaUser
				
			};

			if (ModelState.IsValid)
			{
				
				db.DonHangs.Add(order);
				db.SaveChanges();
			}
			return Redirect("Index");
		}


		[HttpGet]
		public IActionResult DanhSachDonHang()
		{
		
			var username = HttpContext.Session.GetString("Username");

			if (username != null)
			{
				
				var currentUser = db.LbUsers.SingleOrDefault(u => u.Username == username);

				if (currentUser != null)
				{
				
					var danhSachDonHang = db.DonHangs
						.Where(d => d.MaUser == currentUser.MaUser)
						.ToList();

					return View(danhSachDonHang);
				}
			}

			
			return RedirectToAction("TrangChu", "Home");
		}

		[HttpGet]
		public IActionResult ChiTietDonHang(int maDonHang)
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

		public IActionResult ClearCart()
		{
			
			HttpContext.Session.Remove(MySetting.CART_KEY);

			return Redirect("Index"); 
		}

	}
}
