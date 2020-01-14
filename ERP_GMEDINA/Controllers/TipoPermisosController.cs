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
    public class TipoPermisosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Competencias

        public ActionResult Index()
        {
            List<tbTipoPermisos> tbTipoPermisos = new List<Models.tbTipoPermisos> { };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            try
            {
                tbTipoPermisos = db.tbTipoPermisos.Where(x => x.tper_Estado).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).ToList();
                return View(tbTipoPermisos);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                tbTipoPermisos.Add(new tbTipoPermisos { tper_Id = 0, tper_Descripcion = "fallo la conexion" });
            }
            return View(tbTipoPermisos);
        }





        //[HttpPost]
        //public JsonResult llenarTabla()
        //{
        //    var lista = db.tbTipoSalidas
        //        .Where(x => x.tsal_Estado == true)
        //        .Select(
        //        t =>
        //        new
        //        {
        //            tsal_Id = t.tsal_Id,
        //            tsal_Descripcion = t.tsal_Descripcion
        //        })
        //        .ToList();
        //    return Json(lista, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult llenarTabla()
        {
            var lista = db.tbTipoPermisos.Where(X => X.tper_Estado == true).Select(
                t=>
                new
                {
                    tper_Id = t.tper_Id,
                    tper_Descripcion = t.tper_Descripcion
                })
                .ToList();
                return Json(lista, JsonRequestBehavior.AllowGet);
            }




        [HttpPost]
        public JsonResult Create(tbTipoPermisos tbTipoPermisos)
        {
            string msj = "";
            if (tbTipoPermisos.tper_Descripcion != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbTipoPermisos_Insert(
                        tbTipoPermisos.tper_Descripcion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbTipoPermisos_Insert_Result item in list)
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

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbTipoPermisos tbTipoPermisos = null;
            try
            {
                tbTipoPermisos = db.tbTipoPermisos.Find(id);
                if (tbTipoPermisos == null || !tbTipoPermisos.tper_Estado)
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
            var competencias = new tbTipoPermisos
            {
                tper_Id = tbTipoPermisos.tper_Id,
                tper_Descripcion = tbTipoPermisos.tper_Descripcion,
                tper_Estado = tbTipoPermisos.tper_Estado,
                tper_RazonInactivo = tbTipoPermisos.tper_RazonInactivo,
                tper_UsuarioCrea = tbTipoPermisos.tper_UsuarioCrea,
                tper_FechaCrea = tbTipoPermisos.tper_FechaCrea,
                tper_UsuarioModifica = tbTipoPermisos.tper_UsuarioModifica,
                tper_FechaModifica = tbTipoPermisos.tper_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoPermisos.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoPermisos.tbUsuario1).usu_NombreUsuario }
            };
            return Json(competencias, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Edit(tbTipoPermisos tbTipoPermisos)
        {
            string msj = "";
            if (tbTipoPermisos.tper_Id != 0 && tbTipoPermisos.tper_Descripcion != "")
            {
                var id = (int)Session["id"];
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbTipoPermisos_Update(id,
                        tbTipoPermisos.tper_Descripcion, usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbTipoPermisos_Update_Result item in list)
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
        public ActionResult Delete(tbTipoPermisos tbTipoPermisos)
        {
            string msj = "...";
            if (tbTipoPermisos.tper_Id != 0 && tbTipoPermisos.tper_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbTipoPermisos_Delete(id, tbTipoPermisos.tper_RazonInactivo, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbTipoPermisos_Delete_Result item in list)
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