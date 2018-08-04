using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThucHanhWebTMDT.Models;
namespace ThucHanhWebTMDT.Controllers
{
    public class SanPhamController : Controller
    {
        MyEStoreEntities db = new MyEStoreEntities();
        // GET: SanPham
        public ActionResult Index()
        {
            var dsHH = db.HangHoas.OrderByDescending(p => p.GiamGia).Take(12).ToList();
            return View(dsHH);
        }

        public ActionResult ChiTiet(int? id)
        {
            var hh = db.HangHoas.SingleOrDefault(p => p.MaHH == id);
            return View(hh);
        }
    }
}