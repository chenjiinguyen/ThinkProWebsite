using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using ThinkProWebsite.Models;

namespace ThinkProWebsite.Controllers
{
    public class HomeController : Controller
    {
        ThinkProDataContext db = new ThinkProDataContext();
        private static Random random = new Random();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Product(string id)
        {
            var Product = db.SANPHAMs.SingleOrDefault(u => u.ID_SP == id);
            if (Product != null)
            {
                ViewBag.ListReview = db.DANHGIAs.Where(t => t.ID_SP == id).OrderByDescending(t => t.NGAYGIO).ToList();
                ViewBag.AvgReview = db.DANHGIAs.Where(t => t.ID_SP == id).Average(t => (int?)t.XEPHANG);
                ViewBag.InfoProduct = db.THONGTINs.SingleOrDefault(t => t.ID_SP == id);
                return View(Product);

            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Products(string loai = "", string brand = "", int paging = 1)
        {
            int PageSize = 12;
            ViewBag.loai = loai;
            ViewBag.brand = brand;
            List<string> _ListBrand = (brand == "") ? new List<string>() : brand.Split(',').ToList();


            var ListProduct = new List<SANPHAM>();
            if (loai == "" && _ListBrand.Count == 0)
            {
                ListProduct = db.SANPHAMs.ToList();
                ViewBag.Name = "Danh Sách Sản Phẩm";
            }
            else
            {
                if (loai != "" && _ListBrand.Count == 0)
                {
                    ListProduct = db.SANPHAMs.Where(t => t.ID_LOAI == loai).ToList();
                    var Loai = db.LOAIs.Single(t => t.ID_LOAI == loai);
                    ViewBag.Name = Loai.TENLOAI;
                }
                else if (_ListBrand.Count > 0 && loai == "")
                {
                    ListProduct = db.SANPHAMs.Where(t =>  _ListBrand.Contains(t.ID_BRAND)).ToList();
                    ViewBag.Name = "Danh Sách Sản Phẩm";
                }
                else
                {
                    ListProduct = db.SANPHAMs.Where(t => t.ID_LOAI == loai && _ListBrand.Contains(t.ID_BRAND)).ToList();
                    var Loai = db.LOAIs.Single(t => t.ID_LOAI == loai);
                    ViewBag.Name = Loai.TENLOAI;
                }
            }
            int count = ListProduct.Count;
            ViewBag.MaxPage = Math.Ceiling((double)count / PageSize);
            ViewBag.Page = paging;
            ListProduct = ListProduct.Skip((paging - 1) * PageSize).Take(PageSize).ToList();
            return View(ListProduct);
        }

        public ActionResult Search(string q, string brand = "", int paging = 1)
        {
            int PageSize = 12;
            ViewBag.Name = "Tìm Kiếm " + q;
            ViewBag.brand = brand;
            ViewBag.Search = q;
            List<string> _ListBrand = (brand == "") ? new List<string>() : brand.Split(',').ToList();


            var ListProduct = new List<SANPHAM>();

            if (_ListBrand.Count == 0)
            {
                ListProduct = db.SANPHAMs.Where(t => t.TENSP.Replace(" ", "").ToLower().Contains(q.Replace(" ", "").ToLower())).ToList();

            }
            else
            {
                ListProduct = db.SANPHAMs.Where(t => _ListBrand.Contains(t.ID_BRAND) && t.TENSP.Replace(" ", "").ToLower().Contains(q.Replace(" ", "").ToLower())).ToList();

            }

            int count = ListProduct.Count;
            ViewBag.MaxPage = Math.Ceiling((double)count / PageSize);
            ViewBag.Page = paging;
            ListProduct = ListProduct.Skip((paging - 1) * PageSize).Take(PageSize).ToList();
            return View(ListProduct);
        }


        public ActionResult Review(string id)
        {
            var Product = db.SANPHAMs.SingleOrDefault(u => u.ID_SP == id);
            if (Product != null)
            {
                return View(Product);

            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Review(string product, int danhgia, string review_tieude, string review_noidung)
        {
            var Product = db.SANPHAMs.SingleOrDefault(u => u.ID_SP == product);
            if (Product != null)
            {
                DANHGIA Review = new DANHGIA();
                Review.ID_DG = RandomString(8);
                Review.HOTEN = "Anonymous";
                Review.ID_SP = product;
                Review.NGAYGIO = DateTime.Now;
                Review.TIEUDE = review_tieude;
                Review.NOIDUNG = review_noidung;
                Review.XEPHANG = danhgia;
                db.DANHGIAs.InsertOnSubmit(Review);
                db.SubmitChanges();
                return RedirectToAction("Product", "Home", new { id = product });
            }
            return RedirectToAction("Index", "Home");
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="idP"></param>
        /// <param name="strURL"></param>
        /// <returns></returns>
        public ActionResult AddCart(string id, string strURL)
        {
            //Lấy ra giỏ hàng
            List<Cart> lstCart = getCart();

            //Kiểm tra SP này có tồn tại trong giỏ hàng chưa?
            Cart product = lstCart.Find(sp => sp.idProduct == id);
            if (product == null)
            {
                product = new Cart(id);
                lstCart.Add(product);
                return Redirect(strURL);
            }
            else
            { //đã có sản phẩm này trong giỏ
                product.iQuantity++;
                return Redirect(strURL);
            }
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

        [HttpPost]
        public ActionResult AddCart(string id, int quatity,string strURL)
        {
            try
            {

                List<Cart> lstCart = getCart();
                Cart product = lstCart.Find(sp => sp.idProduct == id);
                if (product == null)
                {
                    product = new Cart(id);
                    product.iQuantity = quatity;
                    lstCart.Add(product);
                }
                else
                { 
                    product.iQuantity += quatity;
                    
                }
                return Redirect(strURL);
            }
            catch
            {
                return Redirect(strURL);
            }
        }

        public ActionResult UpdateCart(string id, int iQuantity)
        {
            try
            {
                List<Cart> lstCart = getCart();
                Cart product = lstCart.Find(sp => sp.idProduct == id);
                if (product == null)
                {
                    product = new Cart(id);
                    product.iQuantity = iQuantity;
                    lstCart.Add(product);
                }
                else
                {
                    if(iQuantity <= 0)
                    {
                        lstCart.RemoveAll(s => s.idProduct == id);
                    }
                    else
                    {
                        product.iQuantity = iQuantity;
                    }
                    

                }
                return View();
            }
            catch
            {
                return View();
            }
        }


        public ActionResult DeleteCart(string idP)
        {
            List<Cart> lstCart = getCart();
            Cart product = lstCart.Single(s => s.idProduct == idP);
            if (product != null)
            {
                lstCart.RemoveAll(s => s.idProduct == idP);
                return RedirectToAction("Cart", "Home");
            }
            if (lstCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Cart", "Home");
        }

        public ActionResult CartDone() {
            List<Cart> lstCart = getCart();
            if (lstCart.Count > 0) {
                HOADON hd = new HOADON();
                hd.ID_HD = GeneratorID();
                hd.ID_USER = ((NGUOIDUNG)Session["Account"]).ID_USER;
                hd.NGAYLAPHD = DateTime.Now;
                hd.GHICHU = "";
                hd.ID_NV = "ONLINE";
                db.HOADONs.InsertOnSubmit(hd);
                db.SubmitChanges();
                foreach(Cart CTHD in lstCart)
                {
                    CHITIETHD cthd = new CHITIETHD();
                    cthd.ID_HD = hd.ID_HD;
                    cthd.ID_SP = CTHD.idProduct;
                    cthd.SOLUONG = CTHD.iQuantity;
                    db.CHITIETHDs.InsertOnSubmit(cthd);
                    db.SubmitChanges();
                }
                Session["Cart"] = null;
            } 
            return RedirectToAction("Index", "Home");
        } 

        //public ActionResult index()
        //{
        //    return View();
        //}

        public ActionResult DangKy()
        {
            return View();
        }

        public ActionResult DangNhap()
        {
            if(Session["Account"] == null)
            return View();
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult DangKy(NGUOIDUNG user, FormCollection f)
        {
            var email = f["email"];
            var hoten = f["hoten"];
            var matkhau = f["matkhau"];
            var nhaplaimatkhau = f["nhaplaimatkhau"];
            var dienthoai = f["dienthoai"];
            var diachi = f["diachi"];
            var gioitinh = f["gioitinh"];

            if (string.IsNullOrEmpty(email))
            {
                ViewData["Loi1"] = "Email không được để trống";
            }
            else
            {
                if(IsValidEmail(email))
                    @ViewData["Loi1"] = "Email không đúng định dạng";
            }
            if (string.IsNullOrEmpty(hoten))
            {
                ViewData["Loi2"] = "Vui lòng nhập họ tên";
            }
            if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Vui lòng nhập mật khẩu";
            }
            if (string.IsNullOrEmpty(nhaplaimatkhau))
            {
                ViewData["Loi4"] = "Vui lòng nhập lại mật khẩu";
            }
            if (string.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi5"] = "Vui lòng nhập số điện thoại";
            }
            if (string.IsNullOrEmpty(diachi))
            {
                ViewData["Loi6"] = "Vui lòng nhập địa chỉ";
            }
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(hoten) && !string.IsNullOrEmpty(matkhau) && !string.IsNullOrEmpty(nhaplaimatkhau) && !string.IsNullOrEmpty(dienthoai) && !string.IsNullOrEmpty(dienthoai) && !string.IsNullOrEmpty(diachi))
            {
                if (matkhau != nhaplaimatkhau)
                {
                    ViewBag.LoiRePass = "Mật khẩu không trùng khớp";
                    return View(user);
                }
                else
                {
                    if(db.NGUOIDUNGs.SingleOrDefault(x => x.EMAIL == email) == null)
                    {
                        Random rd = new Random();
                        user.ID_USER = "USER" + rd.Next(0, 999);

                        while (db.NGUOIDUNGs.SingleOrDefault(x => x.ID_USER == user.ID_USER) != null)
                        {
                            user.ID_USER = "USER" + rd.Next(0, 999);
                        }

                        user.EMAIL = email;
                        user.HOTEN = hoten;
                        user.MATKHAU = MD5Password(matkhau);
                        user.DIENTHOAI = dienthoai;
                        user.DIACHI = diachi;
                        user.GIOITINH = gioitinh;
                        user.ID_VAITRO = "U";
                        user.AVATAR = "https://i.imgur.com/yIRoqu8.png";

                        db.NGUOIDUNGs.InsertOnSubmit(user);
                        db.SubmitChanges();
                        return RedirectToAction("DangNhap", "Home");
                    }
                    else
                    {
                        @ViewData["Loi1"] = "Email đã tồn tại";
                        return View(user);
                    }
                    
                }
            }
            else
            {
                return View(user);
            }

        }

        [HttpPost]
        public ActionResult DangNhap(NGUOIDUNG user, FormCollection f)
        {
            Session["Account"] = null;
            var email = f["email"];
            var password = f["password"];
            //kiem tra rong
            if (string.IsNullOrEmpty(email))
            {
                ViewData["Loi7"] = "Phải nhập tên đăng nhập";
            }
            else
            {
                if (IsValidEmail(email))
                    @ViewData["Loi7"] = "Email không đúng định dạng";
            }
            if (string.IsNullOrEmpty(password))
            {
                ViewData["Loi8"] = "Phải nhập mật khẩu";
            }

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                var count = db.NGUOIDUNGs.SingleOrDefault(e => e.EMAIL == email && e.MATKHAU == MD5Password(password));
                if (count != null)
                {
                    Session["Account"] = count;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.LoiDN = "Tên đăng nhập hoặc mật khẩu không đúng";
                    return View();
                }
            }

            else
            {
                return View();
            }
        }
        public ActionResult DangXuat()
        {
            Session["Account"] = null;
            return RedirectToAction("Index", "Home");
        }

        public List<Cart> getCart()
        {
            List<Cart> lstCart = Session["Cart"] as List<Cart>;
            if (lstCart == null)
            {
                lstCart = new List<Cart>();
                Session["Cart"] = lstCart;
            }
            return lstCart;
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


        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private string MD5Password(string chuoi)
        {
            string str_md5 = "";
            byte[] mang = System.Text.Encoding.UTF8.GetBytes(chuoi);

            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);

            foreach (byte b in mang)
            {
                str_md5 += b.ToString("X2");
            }

            return str_md5;
        }

        public string GeneratorID()
        {
            DateTime DateTimeNow = DateTime.Now.Date.Add(new TimeSpan(0, 0, 0));
            List<HOADON> rowList = db.HOADONs.Where(r=> r.NGAYLAPHD >= DateTimeNow).OrderByDescending(x => x.NGAYLAPHD).ToList();
            int Int_Old_ID = 0;
            if (rowList.Count() > 0)
            {
                HOADON row = rowList[0];
                if (row != null)
                {
                    string Old_ID = row.ID_HD.ToString().Substring(10, 4);
                    Int_Old_ID = int.Parse(Old_ID);
                }
            }

            string prefixValue = "HD";
            string prefixDate = DateTimeNow.ToString("yyyyMMdd");
            string prefixNumber = (Int_Old_ID + 1).ToString("D" + 4);

            return prefixValue + prefixDate + prefixNumber;
        }
    }
}