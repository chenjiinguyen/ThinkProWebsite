using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ThinkProWebsite.Models;
namespace ThinkProWebsite
{
    /// <summary>
    /// Summary description for ProductWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ProductWebService : System.Web.Services.WebService
    {
        ThinkProDataContext db = new ThinkProDataContext();

        [WebMethod]
        public string add(SANPHAM sp)
        {
            try
            {
                db.SANPHAMs.InsertOnSubmit(sp);
                db.SubmitChanges();
                return "Thêm Thành Công";
            }
            catch
            {
                return "Thêm Thất Bại";
            }
           
        }

        [WebMethod]
        public string update(SANPHAM sp)
        {
            try
            {
                var update_sp = db.SANPHAMs.Single(s => s.ID_SP == sp.ID_SP);
                update_sp.ID_BRAND = sp.ID_BRAND;
                update_sp.GIATIEN = sp.GIATIEN;
                update_sp.ANH_SP = sp.ANH_SP;
                update_sp.DONVITINH = sp.DONVITINH;
                update_sp.ID_TINHTRANG = sp.ID_TINHTRANG;
                update_sp.TENSP = sp.TENSP;
                db.SubmitChanges();
                return "Sửa Thành Công";
            }
            catch
            {
                return "Sửa Thất Bại";
            }

        }

        [WebMethod]
        public string delete(SANPHAM sp)
        {
            try
            {
                var update_sp = db.SANPHAMs.Single(s => s.ID_SP == sp.ID_SP);
                db.SANPHAMs.DeleteOnSubmit(update_sp);
                db.SubmitChanges();
                return "Xóa Thành Công";
            }
            catch
            {
                return "Xóa Thất Bại";
            }

        }
    }
}
