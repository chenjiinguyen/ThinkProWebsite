using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThinkProWebsite.Models;

namespace ThinkProWebsite.Controllers
{
    public class BrandController : Controller
    {
        ThinkProDataContext db = new ThinkProDataContext();
        // GET: Brand
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BrandFilterPatial(string idBrand = "", string idLoai = "")
        {
            List<string> _ListLoai = (idLoai == "") ? new List<string>() : idLoai.Split(',').ToList();
            List<string> _ListBrand = (idBrand == "") ? new List<string>() : idBrand.Split(',').ToList();

            var ListBrand = db.THUONGHIEUs.ToList();
            List<int> CountBrand = new List<int>();
            foreach(var Brand in ListBrand)
            {
               
                if (_ListLoai.Count == 0)
                    CountBrand.Add(db.SANPHAMs.Where(t => Brand.ID_BRAND == t.ID_BRAND).Count());
                else
                    CountBrand.Add(db.SANPHAMs.Where(t => Brand.ID_BRAND == t.ID_BRAND && _ListLoai.Contains(t.ID_LOAI)).Count());
            }
            ViewBag.ListBrand = ListBrand;
            ViewBag.CountBrand = CountBrand;
            ViewBag._ListBrand = _ListBrand;
            return View();
        }
    }
}