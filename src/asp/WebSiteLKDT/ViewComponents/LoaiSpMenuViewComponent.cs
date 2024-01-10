using WebSiteLKDT.Models;
using Microsoft.AspNetCore.Mvc;
using WebSiteLKDT.Repository;


namespace WebSiteLKDT.ViewComponents
{
    [ViewComponent(Name = "LoaiSPMenu")]
    public class LoaiSpMenuViewComponent : ViewComponent
    {
        private readonly ILoaiSpRepository _loaiSP;

        public LoaiSpMenuViewComponent(ILoaiSpRepository LoaiSpRepository)
        {

            _loaiSP = LoaiSpRepository;

        }
        public IViewComponentResult Invoke()
        {
            var loaisp = _loaiSP.GetALLDanhMuc().OrderBy(x => x.TenDanhMuc);
            return View(loaisp);
        }
    }
}
