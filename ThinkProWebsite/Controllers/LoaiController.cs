using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThinkProWebsite.Models;

namespace ThinkProWebsite.Controllers
{
    public class LoaiController : Controller
    {
        ThinkProDataContext db = new ThinkProDataContext();
        // GET: Loai
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoaiFilterPatial(string idLoai = null,string idBrand = null)
        {
            List<string> _ListLoai = (idLoai == null)? new List<string>(): idLoai.Split(',').ToList();
            List<string> _ListBrand = (idBrand == null) ? new List<string>() : idBrand.Split(',').ToList();

            var ListLoai = db.LOAIs.ToList();
            List<int> CountLoai = new List<int>();
            foreach (var Loai in ListLoai)
            {
                if(_ListBrand.Count == 0)
                    CountLoai.Add(db.SANPHAMs.Where(t => t.ID_LOAI == Loai.ID_LOAI).Count());
                else
                    CountLoai.Add(db.SANPHAMs.Where(t => t.ID_LOAI == Loai.ID_LOAI && _ListBrand.Contains(t.ID_BRAND)).Count());
            }

            ViewBag.ListLoai = ListLoai;
            ViewBag.CountLoai = CountLoai;
            ViewBag._ListLoai = _ListLoai;
            return View();
        }
    }
}