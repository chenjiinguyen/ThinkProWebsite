using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThinkProWebsite.Models
{
    public class Cart
    {

        ThinkProDataContext db = new ThinkProDataContext();

        public string idProduct { set; get; }
        public string nName { set; get; }
        public string iImage { set; get; }
        public double pPrice { set; get; }
        public int iQuantity { set; get; }
        public double dSubtotal
        {
            get { return iQuantity * pPrice; }
        }

        // Khởi tạo giỏ hàng
        public Cart(string idPro)
        {
            idProduct = idPro;
            SANPHAM product = db.SANPHAMs.Single(s => s.ID_SP == idProduct);
            nName = product.TENSP;
            iImage = product.ANH_SP;
            pPrice = double.Parse(product.GIATIEN.ToString());
            iQuantity = 1;
        }
    }
}