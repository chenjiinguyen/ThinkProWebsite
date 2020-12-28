using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using THINKPROResource;
using Newtonsoft.Json;

namespace ThinkProWebsite.Controllers
{
    public class productController : ApiController
    {
        ThinkProDataContext db = new ThinkProDataContext();
 
        public IEnumerable<SANPHAM> Get()
        {
            return db.SANPHAMs.ToList();
        }

        public SANPHAM get(string id) {
            return db.SANPHAMs.Single(sp => sp.ID_SP == id);
        } 
    }
}
