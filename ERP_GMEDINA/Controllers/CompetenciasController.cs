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
    public class CompetenciasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Competencias
        public ActionResult Index()
        {
            List<tbCompetencias> tbCompetencias = new List<Models.tbCompetencias> { };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            try
            {
                tbCompetencias = db.tbCompetencias.Where(x => x.comp_Estado).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).ToList();
                return View(tbCompetencias);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                tbCompetencias.Add(new tbCompetencias { comp_Id = 0, comp_Descripcion = "fallo la conexion" });
            }
            return View(tbCompetencias);
        }
        [HttpPost]
        public JsonResult llenarTabla()
        {
            List<tbCompetencias> tbCompetencias = new List<Models.tbCompetencias> { };
            foreach (tbCompetencias x in db.tbCompetencias.ToList().Where(x => x.comp_Estado == true))
            {
                tbCompetencias.Add(new tbCompetencias
                {
                    comp_Id = x.comp_Id,
                    comp_Descripcion = x.comp_Descripcion
                });
            }
            return Json(tbCompetencias, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Create(tbCompetencias tbCompetencias)
        {
            string msj = "";
            if (tbCompetencias.comp_Descripcion != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbCompetencias_Insert(
                        tbCompetencias.comp_Descripcion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbCompetencias_Insert_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                }
                catch(Exception ex)
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

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbCompetencias tbCompetencias = null;
            try
            {
                tbCompetencias = db.tbCompetencias.Find(id);
                if(tbCompetencias == null || !tbCompetencias.comp_Estado)
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
        public JsonResult Edit (tbCompetencias tbCompetencias)
        {
            string msj = "";
            if (tbCompetencias.comp_Id != 0 && tbCompetencias.comp_Descripcion != "")
            {
                var id = (int)Session["id"];
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbCompetencias_Update(id,
                        tbCompetencias.comp_Descripcion, usuario.usu_Id, DateTime.Now);
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
        public ActionResult Delete(tbCompetencias tbCompetencias)
        {
            string msj = "...";
            if (tbCompetencias.comp_Id != 0 && tbCompetencias.comp_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbCompetencias_Delete(id, tbCompetencias.comp_RazonInactivo, Usuario.usu_Id, DateTime.Now);
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
            return Json(msj.Substring(0,2), JsonRequestBehavior.AllowGet);
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
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}



