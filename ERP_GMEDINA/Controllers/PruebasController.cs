using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Controllers
{
    public class PruebasController : Controller
    {
        //ERP_NominaEntities db = new ERP_NominaEntities();
                
        // GET: Pruebas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Datatable()
        {
            return View();
        }
        public ActionResult TemplateDatatable()
        {
            return View(/*db.tbCatalogoDeDeducciones*/);
        }

        public ActionResult TemplateCreate()
        {
            //ViewBag.cde_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

    }
}