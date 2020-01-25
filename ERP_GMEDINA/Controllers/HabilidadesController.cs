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
    public class HabilidadesController : Controller
    {
        private ERP_GMEDINAEntities db = null;

        // GET: Habilidades
        public ActionResult Index()
        {
            bool Admin = (bool)Session["Admin"];
            tbHabilidades tbHabilidades = new tbHabilidades {habi_Estado= Admin };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            return View(tbHabilidades);
        }
        [HttpPost]
        public JsonResult llenarTabla()
        {
            try
            {
                db = new ERP_GMEDINAEntities();
                //Aqui codigo llenarTabla
                    var lista = db.tbHabilidades
                        .Select(
                            t =>
                            new
                            {
                                habi_Id = t.habi_Id,
                                habi_Descripcion = t.habi_Descripcion,
                                habi_Estado = t.habi_Estado
                            }
                        )
                        .ToList();
                    return Json(lista, JsonRequestBehavior.AllowGet);

                //aqui termina llenarTabla
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        // POST: Habilidades/Create
        [HttpPost]
        public JsonResult Create(tbHabilidades tbHabilidades)
        {
            string msj = "";
            if (tbHabilidades.habi_Descripcion != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbHabilidades_Insert(tbHabilidades.habi_Descripcion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbHabilidades_Insert_Result item in list)
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
        [HttpPost]
        [ActionName("Datos")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbHabilidades tbHabilidades = null;
            try
            {
                db = new ERP_GMEDINAEntities();
                tbHabilidades = db.tbHabilidades.Find(id);
                if (tbHabilidades == null )
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
            var habilidad = new tbHabilidades
            {
                habi_Id = tbHabilidades.habi_Id,
                habi_Descripcion = tbHabilidades.habi_Descripcion,
                habi_Estado = tbHabilidades.habi_Estado,
                habi_RazonInactivo = tbHabilidades.habi_RazonInactivo,
                habi_UsuarioCrea = tbHabilidades.habi_UsuarioCrea,
                habi_FechaCrea = tbHabilidades.habi_FechaCrea,
                habi_UsuarioModifica = tbHabilidades.habi_UsuarioModifica,
                habi_FechaModifica = tbHabilidades.habi_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbHabilidades.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbHabilidades.tbUsuario1).usu_NombreUsuario }
            };
            return Json(habilidad, JsonRequestBehavior.AllowGet);
        }
        // POST: Habilidades/Edit/5
        [HttpPost]
        public JsonResult Edit(tbHabilidades tbHabilidades)
        {
            string msj = "";
            if (tbHabilidades.habi_Id != 0 && tbHabilidades.habi_Descripcion != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbHabilidades_Update(id, tbHabilidades.habi_Descripcion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbHabilidades_Update_Result item in list)
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
        public ActionResult Delete(tbHabilidades tbHabilidades)
        {
            string msj = "";

            string RazonInactivo = "Se ha Inhabilitado este Registro";
            if (tbHabilidades.habi_Id != 0 && tbHabilidades.habi_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbHabilidades_Delete(id, RazonInactivo, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbHabilidades_Delete_Result item in list)
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
        [HttpPost]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbHabilidades_Restore(id, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbHabilidades_Restore_Result item in list)
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
