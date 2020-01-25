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
    public class TipoHorasController : Controller
    {
        private ERP_GMEDINAEntities db = null;

        // GET: Habilidades
        public ActionResult Index()
        {
            tbTipoHoras tbTipoHoras = new tbTipoHoras {tiho_Estado=true };
            bool Admin = (bool)Session["Admin"];
       
            return View(tbTipoHoras);
        }
        [HttpPost]
        public JsonResult llenarTabla()
        {
            try
            {
                db = new ERP_GMEDINAEntities();
                var tbTipoHoras = db.tbTipoHoras
                    .Select(
                       x => new {
                           
                           tiho_Id = x.tiho_Id,
                    tiho_Descripcion = x.tiho_Descripcion,
                    tiho_Recargo = x.tiho_Recargo,
                           tiho_Estado = x.tiho_Estado
                       }
                )
                .ToList();
                return Json(tbTipoHoras, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ex.ToString();
                return Json(-2, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Habilidades/Create
        [HttpPost]
        public JsonResult Create(string tiho_Descripcion, int tiho_Recargo)
        {
            string msj = "";
            tbTipoHoras tbTipoHoras = new tbTipoHoras();
            tbTipoHoras.tiho_Descripcion = tiho_Descripcion;
            tbTipoHoras.tiho_Recargo = tiho_Recargo;
            if (tbTipoHoras.tiho_Descripcion != "" && tbTipoHoras.tiho_Recargo != 0)
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbTipoHoras_Insert(tbTipoHoras.tiho_Descripcion, tbTipoHoras.tiho_Recargo, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbTipoHoras_Insert_Result item in list)
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

        // GET: Habilidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbTipoHoras tbTipoHoras = null;
            try
            {
                db = new ERP_GMEDINAEntities();
                tbTipoHoras = db.tbTipoHoras.Find(id);
                if (tbTipoHoras == null)
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
            var TipoHoras = new tbTipoHoras
            {
                tiho_Id = tbTipoHoras.tiho_Id,
                tiho_Descripcion = tbTipoHoras.tiho_Descripcion,
                tiho_Recargo = tbTipoHoras.tiho_Recargo,
                tiho_Estado = tbTipoHoras.tiho_Estado,
                tiho_RazonInactivo = tbTipoHoras.tiho_RazonInactivo,
                tiho_UsuarioCrea = tbTipoHoras.tiho_UsuarioCrea,
                tiho_FechaCrea = tbTipoHoras.tiho_FechaCrea,
                tiho_UsuarioModifica = tbTipoHoras.tiho_UsuarioModifica,
                tiho_FechaModifica = tbTipoHoras.tiho_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoHoras.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoHoras.tbUsuario1).usu_NombreUsuario }
            };
            return Json(TipoHoras, JsonRequestBehavior.AllowGet);
        }

        // POST: Habilidades/Edit/5
        [HttpPost]
        public JsonResult Edit(string tiho_Descripcion, int tiho_Recargo)
        {
            string msj = "";
            tbTipoHoras tbTipoHoras = new tbTipoHoras();
            //tbTipoHoras.tiho_Id = id;
            tbTipoHoras.tiho_Descripcion = tiho_Descripcion;
            tbTipoHoras.tiho_Recargo = tiho_Recargo;
            if ( tbTipoHoras.tiho_Descripcion != "" && tbTipoHoras.tiho_Recargo != 0)
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbTipoHora_Update(id, tbTipoHoras.tiho_Descripcion, tbTipoHoras.tiho_Recargo, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbTipoHora_Update_Result item in list)
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

        // GET: Habilidades/Delete/5
        [HttpPost]
        public ActionResult Delete(string tiho_RazonInactivo)
        {
            string msj = "";
            string RazonInactivo = "Se ha Inhabilitado este Registro";

            tbTipoHoras tbTipoHoras = new tbTipoHoras();
            //tbTipoHoras.tiho_Id = id;
            tbTipoHoras.tiho_RazonInactivo = tiho_RazonInactivo;

            if ( tbTipoHoras.tiho_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbTipoHoras_Delete(id, RazonInactivo, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbTipoHoras_Delete_Result item in list)
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
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {

                    var list = db.UDP_RRHH_tbTipoHoras_Restore(id, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbTipoHoras_Restore_Result item in list)
                    {
                        result = item.MensajeError;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                result = "-2";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}