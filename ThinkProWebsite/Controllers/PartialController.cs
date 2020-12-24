using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThinkProWebsite.Models;

namespace ThinkProWebsite.Controllers
{
    public class PartialController : Controller
    {
        ThinkProDataContext db = new ThinkProDataContext();

        public ActionResult MenuPartial()
        {
            ViewBag.Loai = db.LOAIs.OrderBy(t => t.TENLOAI).ToList();
            ViewBag.ThuongHieu = db.THUONGHIEUs.OrderBy(t => t.TENTH).ToList();
            return PartialView();
        }

        public ActionResult BrandFilterPartial(string idBrand = "", string idLoai = "")
        {
            List<string> _ListLoai = (idLoai == "") ? new List<string>() : idLoai.Split(',').ToList();
            List<string> _ListBrand = (idBrand == "") ? new List<string>() : idBrand.Split(',').ToList();

            var ListBrand = db.THUONGHIEUs.ToList();
            List<int> CountBrand = new List<int>();
            foreach (var Brand in ListBrand)
            {

                if (_ListLoai.Count == 0)
                    CountBrand.Add(db.SANPHAMs.Where(t => Brand.ID_BRAND == t.ID_BRAND).Count());
                else
                    CountBrand.Add(db.SANPHAMs.Where(t => Brand.ID_BRAND == t.ID_BRAND && _ListLoai.Contains(t.ID_LOAI)).Count());
            }
            ViewBag.ListBrand = ListBrand;
            ViewBag.CountBrand = CountBrand;
            ViewBag._ListBrand = _ListBrand;
            return PartialView();
        }

        public ActionResult LoaiFilterPartial(string idLoai = null, string idBrand = null)
        {
            List<string> _ListLoai = (idLoai == null) ? new List<string>() : idLoai.Split(',').ToList();
            List<string> _ListBrand = (idBrand == null) ? new List<string>() : idBrand.Split(',').ToList();

            var ListLoai = db.LOAIs.ToList();
            List<int> CountLoai = new List<int>();
            foreach (var Loai in ListLoai)
            {
                if (_ListBrand.Count == 0)
                    CountLoai.Add(db.SANPHAMs.Where(t => t.ID_LOAI == Loai.ID_LOAI).Count());
                else
                    CountLoai.Add(db.SANPHAMs.Where(t => t.ID_LOAI == Loai.ID_LOAI && _ListBrand.Contains(t.ID_BRAND)).Count());
            }

            ViewBag.ListLoai = ListLoai;
            ViewBag.CountLoai = CountLoai;
            ViewBag._ListLoai = _ListLoai;
            return PartialView();
        }

        public ActionResult ProductRelatedPartial(string loai, string brand)
        {
            var listProducts = db.SANPHAMs.Where(t => t.ID_LOAI == loai || t.ID_BRAND == brand).OrderBy(x => Guid.NewGuid()).Take(5).ToList();
            return PartialView(listProducts);
        }

        public ActionResult ProductYouCanMayLikePartial(string loai, string brand)
        {
            var listProducts = db.SANPHAMs.OrderBy(x => Guid.NewGuid()).Take(5).ToList();
            return PartialView(listProducts);
        }

        public ActionResult ProductNewPartial()
        {
            var listProducts = db.SANPHAMs.OrderByDescending(t=> t.ID_SP).Take(8).ToList();
            return PartialView(listProducts);
        }
    }
}
