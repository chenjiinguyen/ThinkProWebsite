using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThinkProWebsite.Models;

namespace ThinkProWebsite.Controllers
{
    public class ProductController : Controller
    {
        ThinkProDataContext db = new ThinkProDataContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult View(string id)
        {
            var Product = db.SANPHAMs.Single(u => u.ID_SP == id);
            if (Product != null)
            {
                ViewBag.ListReview = db.DANHGIAs.Where(t => t.ID_SP == id).OrderByDescending(t => t.NGAYGIO).ToList();
                ViewBag.AvgReview = db.DANHGIAs.Where(t => t.ID_SP == id).Select(t => t.XEPHANG).Average();
                return View(Product);
                
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ListProduct(string loai = "",string brand = "")
        {
            ViewBag.loai = loai;
            ViewBag.brand = brand;
            List<string> _ListBrand = (brand == "")? new List<string>() : brand.Split(',').ToList();

            var ListProduct = new List<SANPHAM>();
            if (loai == "" && _ListBrand.Count == 0)
            {
                ListProduct = db.SANPHAMs.ToList();
                ViewBag.Name = "Danh Sách Sản Phẩm";
            }
            else
            {
                if (loai != "") {
                    ListProduct = db.SANPHAMs.Where(t => t.ID_LOAI == loai).ToList();
                    var Loai = db.LOAIs.Single(t => t.ID_LOAI == loai);
                    ViewBag.Name = Loai.TENLOAI;
                }
                else if(_ListBrand.Count > 0)
                {
                    ListProduct = db.SANPHAMs.Where(t => _ListBrand.Contains(t.ID_BRAND)).ToList();
                    ViewBag.Name = "Danh Sách Sản Phẩm";
                }
                else
                {
                    ListProduct = db.SANPHAMs.Where(t => t.ID_LOAI == loai && _ListBrand.Contains(t.ID_BRAND)).ToList();
                    var Loai = db.LOAIs.Single(t => t.ID_LOAI == loai);
                    ViewBag.Name = Loai.TENLOAI;
                }
            }
            
               
            
            return View(ListProduct);
        }
    }
}