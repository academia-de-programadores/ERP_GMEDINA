using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class CompetenciasController : Controller
    {
        private ERP_GMEDINAEntities db = null;
        Models.Helpers Function = new Models.Helpers();

        // GET: Competencias
        [SessionManager("Competencias/Index")]
        public ActionResult Index()
        {
            tbCompetencias tbCompetencias = new tbCompetencias { comp_Estado = true };
            return View(tbCompetencias);
        }
        [HttpPost]
        [SessionManager("Competencias/Index")]
        public JsonResult llenarTabla()
        {
            using (db = new ERP_GMEDINAEntities())
                try
                {
                    var tbCompetencias = db.tbCompetencias
                        .Select(
                        t => new
                        {
                            comp_Id = t.comp_Id,
                            comp_Descripcion = t.comp_Descripcion,
                            comp_Estado = t.comp_Estado,
                        }
                        ).ToList();
                    return Json(tbCompetencias, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    throw;
                }
        }
        [HttpPost]
        [SessionManager("Competencias/Create")]
        public JsonResult Create(tbCompetencias tbCompetencias)
        {
            string msj = "";

            if (tbCompetencias.comp_Descripcion != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                using (db = new ERP_GMEDINAEntities())
                    try
                    {
                        var list = db.UDP_RRHH_tbCompetencias_Insert(
                                                                     tbCompetencias.comp_Descripcion,
                                                                     (int)Session["UserLogin"],
                                                                     Function.DatetimeNow());
                        foreach (UDP_RRHH_tbCompetencias_Insert_Result item in list)
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
        [SessionManager("Competencias/Edit")]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbCompetencias tbCompetencias = null;
            // using (db = new ERP_GMEDINAEntities())
            try
            {
                db = new ERP_GMEDINAEntities();
                tbCompetencias = db.tbCompetencias.Find(id);
                if(tbCompetencias == null )
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
            var competencias = new tbCompetencias
            {
                comp_Id = tbCompetencias.comp_Id,
                comp_Descripcion = tbCompetencias.comp_Descripcion,
                comp_Estado = tbCompetencias.comp_Estado,
                comp_RazonInactivo = tbCompetencias.comp_RazonInactivo,
                comp_UsuarioCrea = tbCompetencias.comp_UsuarioCrea,
                comp_FechaCrea = tbCompetencias.comp_FechaCrea,
                comp_UsuarioModifica = tbCompetencias.comp_UsuarioModifica,
                comp_FechaModifica = tbCompetencias.comp_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbCompetencias.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbCompetencias.tbUsuario1).usu_NombreUsuario }
            };
            return Json(competencias, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [SessionManager("Competencias/Edit")]
        public JsonResult Edit(tbCompetencias tbCompetencias)
        {
            string msj = "";
            if (tbCompetencias.comp_Id != 0 && tbCompetencias.comp_Descripcion != "")
            {
                var id = (int)Session["id"];
                var usuario = (tbUsuario)Session["Usuario"];

                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbCompetencias_Update(tbCompetencias.comp_Id,
                                                                 tbCompetencias.comp_Descripcion,
                                                                 (int)Session["UserLogin"],
                                                                 Function.DatetimeNow());
                    foreach (UDP_RRHH_tbCompetencias_Update_Result item in list)
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
        [HttpPost]
        [SessionManager("Competencias/Delete")]
        public ActionResult Delete(tbCompetencias tbCompetencias)
        {
            string msj = "...";
            string RazonInactivo = "Se ha Inhabilitado este Registro";
            if (tbCompetencias.comp_Id != 0 && tbCompetencias.comp_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];

                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbCompetencias_Delete(tbCompetencias.comp_Id,
                                                                 RazonInactivo,
                                                                 (int)Session["UserLogin"],
                                                                 Function.DatetimeNow());
                    foreach (UDP_RRHH_tbCompetencias_Delete_Result item in list)
                    {
                        msj = item.MensajeError = " ";
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
            return Json(msj, JsonRequestBehavior.AllowGet);
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
        [SessionManager("Competencias/hablilitar")]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    var list = db.UDP_RRHH_tbCompetencias_Restore(id, 
                                                                  (int)Session["UserLogin"],
                                                                  Function.DatetimeNow());
                    foreach (UDP_RRHH_tbCompetencias_Restore_Result item in list)
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
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
