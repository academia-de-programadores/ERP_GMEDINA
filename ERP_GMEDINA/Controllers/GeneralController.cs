using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_GMEDINA.Controllers
{
    public class GeneralController : Controller
    {
        // GET: General
        public ActionResult General()
        {
            return View();
        }
        public ActionResult Cargos()
        {
            return View();
        }
        //public ActionResult General()
        //{
        //    return View();
        //}
    }
}