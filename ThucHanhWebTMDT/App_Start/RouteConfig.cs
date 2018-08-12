using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ThucHanhWebTMDT
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                 name: "ChiTietHangHoa",
                 url: "san-pham/{tenHHSEO}",
                 defaults: new
                 {
                     controller = "SanPham",
                     action = "SEOUrl"
                 }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "SanPham", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
