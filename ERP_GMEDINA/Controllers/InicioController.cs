using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio
        public ActionResult Index(int? idmenu)
        {
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            Session["Admin"] = true;
            Session["sesionIdMenu"] = idmenu;
            return View();
        }
    }
}
