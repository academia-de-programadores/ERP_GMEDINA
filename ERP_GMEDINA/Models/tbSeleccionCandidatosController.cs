using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ERP_GMEDINA.Models
{				
	public class tbSeleccionCandidatosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: /tbSeleccionCandidatos/
        public ActionResult Index()        
		{           
		    List<tbSeleccionCandidatos> tbSeleccionCandidatos = new List<Models.tbSeleccionCandidatos> { };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            return View(tbSeleccionCandidatos);
        }
		[HttpPost]
        public JsonResult llenarTabla()
        {
			List<tbSeleccionCandidatos> tbSeleccionCandidatos = new List<Models.tbSeleccionCandidatos> { };
            var lista = db.tbSeleccionCandidatos.Where(x => x.scan_Estado).ToList();
            foreach (tbSeleccionCandidatos x in db.tbSeleccionCandidatos.ToList().Where(x=>x.scan_Estado))
            {
                tbSeleccionCandidatos.Add( new tbSeleccionCandidatos
                {
					scan_Id = x.scan_Id,
					per_Id = x.per_Id,
					fare_Id = x.fare_Id,
					scan_Fecha = x.scan_Fecha,
                    req_Id = x.req_Id
				});
            }
            return Json(tbSeleccionCandidatos, JsonRequestBehavior.AllowGet);
        }


  //      // POST: /tbSeleccionCandidatos/Create
  //      [HttpPost]
  //      public JsonResult Create(tbSeleccionCandidatos tbSeleccionCandidatos)
  //      {
  //          string msj = "";
  //          if (tbSeleccionCandidatos.per_Id != 0 && tbSeleccionCandidatos.fare_Id != 0 && tbSeleccionCandidatos.scan_Fecha != "" && tbSeleccionCandidatos.rper_Id != 0)
  //          { 
  //              var Usuario = (tbUsuario)Session["Usuario"];
  //              try
  //              {
  //                  var list = db.UDP_RRHH_tbSeleccionCandidatos_Insert(tbSeleccionCandidatos.per_Id, tbSeleccionCandidatos.fare_Id, tbSeleccionCandidatos.scan_Fecha, tbSeleccionCandidatos.rper_Id, Usuario.usu_Id, DateTime.Now);
  //                  foreach (UDP_RRHH_tbSeleccionCandidatos_Insert_Result item in list)
  //                  {
  //                      msj = item.MensajeError + " ";
  //                  }
  //              }
  //              catch (Exception ex)
  //              {
  //                  msj = "-2";
  //                  ex.Message.ToString();
  //              }
  //          }
  //          else
  //          {
  //              msj = "-3";
  //          }
  //          return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
  //      }
		//// GET: /tbSeleccionCandidatos//Edit/5
    //    public ActionResult Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }

    //        tbSeleccionCandidatos tbSeleccionCandidatos = null;
    //        try
    //        {
    //            tbSeleccionCandidatos = db.tbSeleccionCandidatos.Find(id);
    //            if (tbSeleccionCandidatos == null || !tbSeleccionCandidatos.scan_Estado)
    //            {
    //                return HttpNotFound();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            ex.Message.ToString();
    //            return HttpNotFound();
    //        }            
    //        Session["id"] = id;
    //        var tabla = new tbSeleccionCandidatos
    //        {
				//scan_Id = tbSeleccionCandidatos.scan_Id,
				//per_Id = tbSeleccionCandidatos.per_Id,
				//fare_Id = tbSeleccionCandidatos.fare_Id,
				//scan_Fecha = tbSeleccionCandidatos.scan_Fecha,
				//rper_Id = tbSeleccionCandidatos.rper_Id,
				//scan_Estado = tbSeleccionCandidatos.scan_Estado,
				//scan_RazonInactivo = tbSeleccionCandidatos.scan_RazonInactivo,
				//scan_UsuarioCrea = tbSeleccionCandidatos.scan_UsuarioCrea,
				//scan_FechaCrea = tbSeleccionCandidatos.scan_FechaCrea,
				//scan_UsuarioModifica = tbSeleccionCandidatos.scan_UsuarioModifica,
				//scan_FechaModifica = tbSeleccionCandidatos.scan_FechaModifica,
				//tbUsuario = new tbUsuario { usu_NombreUsuario= IsNull(tbSeleccionCandidatos.tbUsuario).usu_NombreUsuario },
    //            tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbSeleccionCandidatos.tbUsuario1).usu_NombreUsuario }
    //        };
    //        return Json(tabla, JsonRequestBehavior.AllowGet);
    //    }
    //    // POST: /tbSeleccionCandidatos/Edit/5
  //      [HttpPost]
  //      public JsonResult Edit(tbSeleccionCandidatos tbSeleccionCandidatos)
  //      {
  //          string msj = "";
  //          if (tbSeleccionCandidatos.scan_Id != 0 && tbSeleccionCandidatos.per_Id != 0 && tbSeleccionCandidatos.fare_Id != 0 && tbSeleccionCandidatos.scan_Fecha != "" && tbSeleccionCandidatos.rper_Id != 0)            
		//	{
  //              var id = (int)Session["id"];
  //              var Usuario = (tbUsuario)Session["Usuario"];
  //              try
  //              {
  //                  var list = db.UDP_RRHH_tbSeleccionCandidatos_Update(id, tbSeleccionCandidatos.per_Id, tbSeleccionCandidatos.fare_Id, tbSeleccionCandidatos.scan_Fecha, tbSeleccionCandidatos.rper_Id, Usuario.usu_Id, DateTime.Now);
  //                  foreach (UDP_RRHH_tbSeleccionCandidatos_Update_Result item in list)
  //                  {
  //                      msj = item.MensajeError + " ";
  //                  }
  //              }
  //              catch (Exception ex)
  //              {
  //                  msj = "-2";
  //                  ex.Message.ToString();
  //              }
  //              Session.Remove("id");
  //          }
  //          else
  //          {
  //              msj = "-3";
  //          }            
  //          return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
  //      }
		//// GET: /tbSeleccionCandidatos//Delete/5
        //[HttpPost]
        //public ActionResult Delete(tbSeleccionCandidatos tbSeleccionCandidatos)
        //{
        //    string msj = "";
        //    if (tbSeleccionCandidatos.scan_Id != 0 && tbSeleccionCandidatos.scan_RazonInactivo != "")
        //    {
        //        var id = (int)Session["id"];
        //        var Usuario = (tbUsuario)Session["Usuario"];
        //        try
        //        {
        //            var list = db.UDP_RRHH_tbSeleccionCandidatos_Delete(id, tbSeleccionCandidatos.scan_RazonInactivo, Usuario.usu_Id, DateTime.Now);
        //            foreach (UDP_RRHH_tbSeleccionCandidatos_Delete_Result item in list)
        //            {
        //                msj = item.MensajeError + " ";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            msj = "-2";
        //            ex.Message.ToString();
        //        }
        //        Session.Remove("id");
        //    }
        //    else
        //    {
        //        msj = "-3";
        //    }            
        //    return Json(msj.Substring(0, 2),JsonRequestBehavior.AllowGet);
        //}
        //protected tbUsuario IsNull(tbUsuario valor)
        //{
        //    if (valor!=null)
        //    {
        //        return valor;
        //    }
        //    else
        //    {
        //        return new tbUsuario {usu_NombreUsuario="" };
        //    }
        //}
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
