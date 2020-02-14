using ERP_GMEDINA.Attribute;
using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_GMEDINA.Controllers
{
    public class HomeController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        [SessionManager("Home/Index")]
        public ActionResult Index(int idmenu)
        {
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            Session["Admin"] = true;
            Session["sesionIdMenu"] = idmenu;
            return View();
        }

        public ActionResult Minor()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }
    }
}
