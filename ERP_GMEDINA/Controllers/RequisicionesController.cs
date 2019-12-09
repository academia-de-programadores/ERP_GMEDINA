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
	public class RequisicionesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: /tbRequisiciones/
        public ActionResult Index()        
		{           
		    List<tbRequisiciones> tbRequisiciones = new List<Models.tbRequisiciones> { };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            return View(tbRequisiciones);
        }
		[HttpPost]
        public JsonResult llenarTabla()
        {
			List<tbRequisiciones> tbRequisiciones = new List<Models.tbRequisiciones> { };
            var lista = db.tbRequisiciones.Where(x => x.req_Estado).ToList();
            foreach (tbRequisiciones x in db.tbRequisiciones.ToList().Where(x => x.req_Estado))
            {
                tbRequisiciones.Add(new tbRequisiciones
                {
                    req_Id = x.req_Id,
                    req_Experiencia = x.req_Experiencia,
                    req_Sexo = x.req_Sexo,
                    req_Descripcion = x.req_Descripcion,
                    req_EdadMinima = x.req_EdadMinima,
                    req_EdadMaxima = x.req_EdadMaxima,
                    req_EstadoCivil = x.req_EstadoCivil,
                    req_EducacionSuperior = x.req_EducacionSuperior,
                    req_Permanente = x.req_Permanente,
                    req_Duracion = x.req_Duracion,
                    req_Vacantes = x.req_Vacantes,
                    req_FechaRequisicion = x.req_FechaRequisicion,
                    req_FechaContratacion = x.req_FechaContratacion
                });
            }
            return Json(tbRequisiciones, JsonRequestBehavior.AllowGet);
        }
        // POST: /tbRequisiciones/Create
        [HttpPost]
        public JsonResult Create(tbRequisiciones tbRequisiciones)
        {
            string msj = "";
            if (tbRequisiciones.req_Experiencia != "" && tbRequisiciones.req_Sexo != "" && tbRequisiciones.req_Descripcion != "" && tbRequisiciones.req_EstadoCivil != "" &&  tbRequisiciones.req_Duracion != "" && tbRequisiciones.req_Vacantes != "")
            { 
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbRequisiciones_Insert(tbRequisiciones.req_Experiencia, tbRequisiciones.req_Sexo, tbRequisiciones.req_Descripcion, tbRequisiciones.req_EdadMinima, tbRequisiciones.req_EdadMaxima, tbRequisiciones.req_EstadoCivil, tbRequisiciones.req_EducacionSuperior, tbRequisiciones.req_Permanente, tbRequisiciones.req_Duracion, tbRequisiciones.req_Vacantes, tbRequisiciones.req_FechaRequisicion, tbRequisiciones.req_FechaContratacion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbRequisiciones_Insert_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }
		// GET: /tbRequisiciones//Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbRequisiciones tbRequisiciones = null;
            try
            {
                tbRequisiciones = db.tbRequisiciones.Find(id);
                if (tbRequisiciones == null || !tbRequisiciones.req_Estado)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return HttpNotFound();
            }            
            Session["id"] = id;
            var tabla = new tbRequisiciones
            {
				req_Id = tbRequisiciones.req_Id,
				req_Experiencia = tbRequisiciones.req_Experiencia,
				req_Sexo = tbRequisiciones.req_Sexo,
				req_Descripcion = tbRequisiciones.req_Descripcion,
				req_EdadMinima = tbRequisiciones.req_EdadMinima,
				req_EdadMaxima = tbRequisiciones.req_EdadMaxima,
				req_EstadoCivil = tbRequisiciones.req_EstadoCivil,
				req_EducacionSuperior = tbRequisiciones.req_EducacionSuperior,
				req_Permanente = tbRequisiciones.req_Permanente,
				req_Duracion = tbRequisiciones.req_Duracion,
				req_Estado = tbRequisiciones.req_Estado,
				req_RazonInactivo = tbRequisiciones.req_RazonInactivo,
				req_Vacantes = tbRequisiciones.req_Vacantes,
				req_FechaRequisicion = tbRequisiciones.req_FechaRequisicion,
				req_FechaContratacion = tbRequisiciones.req_FechaContratacion,
				req_UsuarioCrea = tbRequisiciones.req_UsuarioCrea,
				req_FechaCrea = tbRequisiciones.req_FechaCrea,
				req_UsuarioModifica = tbRequisiciones.req_UsuarioModifica,
				req_FechaModifica = tbRequisiciones.req_FechaModifica,
				tbUsuario = new tbUsuario { usu_NombreUsuario= IsNull(tbRequisiciones.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbRequisiciones.tbUsuario1).usu_NombreUsuario }
            };
            return Json(tabla, JsonRequestBehavior.AllowGet);
        }
        // POST: /tbRequisiciones/Edit/5
        [HttpPost]
        public JsonResult Edit(tbRequisiciones tbRequisiciones)
        {
            string msj = "";
            if (tbRequisiciones.req_Id != 0 && tbRequisiciones.req_Experiencia != "" && tbRequisiciones.req_Sexo != "" && tbRequisiciones.req_Descripcion != "" && tbRequisiciones.req_EdadMinima > 0 && tbRequisiciones.req_EstadoCivil != "" && tbRequisiciones.req_Duracion != "" && tbRequisiciones.req_Vacantes != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbRequisiciones_Update(id, tbRequisiciones.req_Experiencia, tbRequisiciones.req_Sexo, tbRequisiciones.req_Descripcion, tbRequisiciones.req_EdadMinima, tbRequisiciones.req_EdadMaxima, tbRequisiciones.req_EstadoCivil, tbRequisiciones.req_EducacionSuperior, tbRequisiciones.req_Permanente, tbRequisiciones.req_Duracion, tbRequisiciones.req_Vacantes, tbRequisiciones.req_FechaRequisicion, tbRequisiciones.req_FechaContratacion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbRequisiciones_Update_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
                Session.Remove("id");
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }
        // GET: /tbRequisiciones//Delete/5
        [HttpPost]
        public ActionResult Delete(tbRequisiciones tbRequisiciones)
        {
            string msj = "";
            if (tbRequisiciones.req_Id != 0 && tbRequisiciones.req_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbRequisiciones_Delete(id, tbRequisiciones.req_RazonInactivo, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbRequisiciones_Delete_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
                Session.Remove("id");
            }
            else
            {
                msj = "-3";
            }            
            return Json(msj.Substring(0, 2),JsonRequestBehavior.AllowGet);
        }
        protected tbUsuario IsNull(tbUsuario valor)
        {
            if (valor!=null)
            {
                return valor;
            }
            else
            {
                return new tbUsuario {usu_NombreUsuario="" };
            }
        }
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
