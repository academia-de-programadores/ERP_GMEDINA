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
    public class RazonSalidasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: RazonSalidas
        public ActionResult Index()
        {
            tbRazonSalidas tbRazonSalidas = new tbRazonSalidas { rsal_Estado = true };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            try
            {              
                return View(tbRazonSalidas);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();            
            }
            return View(tbRazonSalidas);
        }

        [HttpPost]
        public JsonResult llenarTabla()
        {
            try
            {
                var tbRazonSalidas = db.tbRazonSalidas
                    .Select(
                    x => new {
                        rsal_Id = x.rsal_Id,
                        rsal_Descripcion = x.rsal_Descripcion,
                        rsal_Estado = x.rsal_Estado
                    }

                    ).ToList();
                return Json(tbRazonSalidas, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        // POST: RazonSalidas/Create
        [HttpPost]
        public JsonResult Create(tbRazonSalidas tbRazonSalidas)
        {
            string msj = "";
            if (tbRazonSalidas.rsal_Descripcion != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbRazonSalidas_Insert(tbRazonSalidas.rsal_Descripcion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbRazonSalidas_Insert_Result item in list)
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

        // GET: RazonSalidas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbRazonSalidas tbRazonSalidas = null;
            try
            {
                tbRazonSalidas = db.tbRazonSalidas.Find(id);
                if (tbRazonSalidas == null )
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
            var habilidad = new tbRazonSalidas
            {
                rsal_Id = tbRazonSalidas.rsal_Id,
                rsal_Descripcion = tbRazonSalidas.rsal_Descripcion,
                rsal_Estado = tbRazonSalidas.rsal_Estado,
                rsal_RazonInactivo = tbRazonSalidas.rsal_RazonInactivo,
                rsal_UsuarioCrea = tbRazonSalidas.rsal_UsuarioCrea,
                rsal_FechaCrea = tbRazonSalidas.rsal_FechaCrea,
                rsal_UsuarioModifica = tbRazonSalidas.rsal_UsuarioModifica,
                rsal_FechaModifica = tbRazonSalidas.rsal_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbRazonSalidas.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbRazonSalidas.tbUsuario1).usu_NombreUsuario }
            };
            return Json(habilidad, JsonRequestBehavior.AllowGet);
        }

        // POST: RazonSalidas/Edit/5
        [HttpPost]
        public JsonResult Edit(tbRazonSalidas tbRazonSalidas)
        {
            string msj = "";
            if (tbRazonSalidas.rsal_Id != 0 && tbRazonSalidas.rsal_Descripcion != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbRazonSalida_Update(id, tbRazonSalidas.rsal_Descripcion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbRazonSalida_Update_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
                //Session.Remove("id");
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }

        // GET: RazonSalidas/Delete/5
        [HttpPost]
        public ActionResult Delete(tbRazonSalidas tbRazonSalidas)
        {
            string msj = "";


            string RazonInactivo = "Se ha Inhabilitado este Registro";
            if (tbRazonSalidas.rsal_Id != 0 && tbRazonSalidas.rsal_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbRazonSalidas_Delete(id, RazonInactivo, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbRazonSalidas_Delete_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
                //Session.Remove("id");
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }

        protected tbUsuario IsNull(tbUsuario valor)
        {
            if (valor != null)
            {
                return valor;
            }
            else
            {
                return new tbUsuario { usu_NombreUsuario = "" };
            }
        }

        [HttpPost]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    var list = db.UDP_RRHH_tbRazonSalidas_Restore (id, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbRazonSalidas_Restore_Result item in list)
                    {
                        result = item.MensajeError;
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    result = "-2";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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
