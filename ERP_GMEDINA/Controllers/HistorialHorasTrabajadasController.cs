using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Controllers
{				
	public class HistorialHorasTrabajadasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: /HistorialHorasTrabajadas/
        public ActionResult Index()        
		{           
		    List<tbHistorialHorasTrabajadas> tbHistorialHorasTrabajadas = new List<Models.tbHistorialHorasTrabajadas> { };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            return View(tbHistorialHorasTrabajadas);
        }
		[HttpPost]
        public JsonResult llenarTabla()
        {
			List<tbHistorialHorasTrabajadas> tbHistorialHorasTrabajadas = new List<Models.tbHistorialHorasTrabajadas> { };
            var lista = db.tbHistorialHorasTrabajadas.Where(x => x.htra_Estado).ToList();
            foreach (tbHistorialHorasTrabajadas x in db.tbHistorialHorasTrabajadas.ToList().Where(x=>x.htra_Estado))
            {
                tbHistorialHorasTrabajadas.Add( new tbHistorialHorasTrabajadas
                {
					htra_Id = x.htra_Id,
					emp_Id = x.emp_Id,
					tiho_Id = x.tiho_Id,
					jor_Id = x.jor_Id,
					htra_CantidadHoras = x.htra_CantidadHoras,
					htra_Fecha = x.htra_Fecha
				});
            }
            return Json(tbHistorialHorasTrabajadas, JsonRequestBehavior.AllowGet);
        }
        // POST: /HistorialHorasTrabajadas/Create
     
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
