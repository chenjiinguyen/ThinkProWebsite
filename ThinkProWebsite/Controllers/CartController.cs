using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThinkProWebsite.Models;

namespace ThinkProWebsite.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Card/
        ThinkProDataContext db = new ThinkProDataContext();

        public ActionResult Index()
        {
            return View();
        }

        public List<Cart> getCart() {
            List<Cart> lstCart = Session["Cart"] as List<Cart>;
            if (lstCart == null)
            {
                lstCart = new List<Cart>();
                Session["Cart"] = lstCart;
            }
            return lstCart;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idP"></param>
        /// <param name="strURL"></param>
        /// <returns></returns>
        public ActionResult AddCart(string idP, string strURL)
        {
            //Lấy ra giỏ hàng
            List<Cart> lstCart = getCart();

            //Kiểm tra SP này có tồn tại trong giỏ hàng chưa?
            Cart product = lstCart.Find(sp => sp.idProduct == idP);
            if (product == null)
            {
                product = new Cart(idP);
                lstCart.Add(product);
                return Redirect(strURL);
            }
            else
            { //đã có sản phẩm này trong giỏ
                product.iQuantity++;
                return Redirect(strURL);
            }
        }

        /// <summary>
        /// Sub Total products in Cart
        /// </summary>
        /// <returns></returns>
        private double SubTotal()
        {
            double sub = 0;
            List<Cart> lstCart = Session["Cart"] as List<Cart>;
            if (lstCart != null)
            {
                sub = lstCart.Sum(sp => sp.dSubtotal);
            }
            return sub;
        }

        private int CountCart()
        {
            
            List<Cart> lstCart = Session["Cart"] as List<Cart>;

            return lstCart.Count;
        }

        public ActionResult Cart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Cart> lstCart = getCart();
            if (lstCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.SubTotal = SubTotal();
            return View(lstCart);
        }

        public ActionResult CartPartial()
        {
            List<Cart> lstCart = getCart();
            ViewBag.CountCart = CountCart();
            ViewBag.SubTotal = SubTotal();
            return PartialView(lstCart);
        }

        public ActionResult DeleteCart(string idP)
        {
            List<Cart> lstCart = getCart();
            Cart product = lstCart.Single(s => s.idProduct == idP);
            if (product != null)
            {
                lstCart.RemoveAll(s => s.idProduct == idP);
                return RedirectToAction("Cart", "Cart");
            }
            if (lstCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Cart", "Cart");
        }
    }
}
