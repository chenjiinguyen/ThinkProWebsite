using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThinkProWebsite.Models;

namespace ThinkProWebsite.Controllers
{
    public class MenuController : Controller
    {
        ThinkProDataContext db = new ThinkProDataContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuPatial()
        {
            ViewBag.Loai = db.LOAIs.OrderBy(t => t.TENLOAI).ToList();
            ViewBag.ThuongHieu = db.THUONGHIEUs.OrderBy(t=> t.TENTH).ToList();
            return View();
        }
    }
}