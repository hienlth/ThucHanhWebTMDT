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

        public ActionResult SEOUrl(string tenHHSEO)
        {
            var id = int.Parse(tenHHSEO.Substring(0, tenHHSEO.IndexOf("-")));
            var hh = db.HangHoas.SingleOrDefault(p => p.MaHH == id);
            return View("ChiTiet", hh);
        }

        /// <summary>
        /// Covert correct data
        /// </summary>
        /// <returns></returns>
        public string ConvertUrlFriendly()
        {
            foreach (HangHoa hh in db.HangHoas)
            {
                hh.TenAlias = MyTools.ToURLFriendly(hh.MaHH, hh.TenHH);
            }
            db.SaveChanges();
            return "OK";
        }
    }
}