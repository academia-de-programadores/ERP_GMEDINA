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
    public class TipoSalidasController : Controller
    {
        private ERP_GMEDINAEntities db = null;
        Models.Helpers Function = new Models.Helpers();
        // GET: Habilidades
        [SessionManager("TipoSalidas/Index")]
        public ActionResult Index()
        {
            if (Session["Admin"] == null && Session["Usuario"] == null)
            {
                Response.Redirect("~/Inicio/index");
                return null;
            }
            tbTipoSalidas tbTipoSalidas = new tbTipoSalidas {  };
            return View(tbTipoSalidas);
        }

        [HttpPost]
        public JsonResult llenarTabla()
        {
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var lista = db.tbTipoSalidas
                .Select(
                t =>
                new
                {
                    tsal_Id = t.tsal_Id,
                    tsal_Descripcion = t.tsal_Descripcion,
                    tsal_Estado = t.tsal_Estado
                })
                .ToList();
                    return Json(lista, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Habilidades/Create
        [HttpPost]
        [SessionManager("TipoSalidas/Create")]
        public JsonResult Create(tbTipoSalidas tbTipoSalidas)
        {
            string msj = "";
            try
            {
                if (tbTipoSalidas.tsal_Descripcion != "")
                {
                    using (db = new ERP_GMEDINAEntities())
                    {
                        var Usuario = (tbUsuario)Session["Usuario"];
                        var list = db.UDP_RRHH_tbTipoSalidas_Insert(tbTipoSalidas.tsal_Descripcion, (int)Session["UserLogin"], Function.DatetimeNow());
                        foreach (UDP_RRHH_tbTipoSalidas_Insert_Result item in list)
                        {
                            msj = item.MensajeError + " ";
                        }
                        return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("-3", JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Habilidades/Edit/5
        [HttpPost]
        [ActionName("Datos")]
        [SessionManager("TipoSalidas/Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbTipoSalidas tbTipoSalidas = null;
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    tbTipoSalidas = db.tbTipoSalidas.Find(id);
                    if (tbTipoSalidas == null )
                    {
                        return HttpNotFound();
                    }
                    Session["id"] = id;
                    var TipoSalida = new tbTipoSalidas
                    {
                        tsal_Id = tbTipoSalidas.tsal_Id,
                        tsal_Descripcion = tbTipoSalidas.tsal_Descripcion,
                        tsal_Estado = tbTipoSalidas.tsal_Estado,
                        tsal_RazonInactivo = tbTipoSalidas.tsal_RazonInactivo,
                        tsal_UsuarioCrea = tbTipoSalidas.tsal_UsuarioCrea,
                        tsal_FechaCrea = tbTipoSalidas.tsal_FechaCrea,
                        tsal_UsuarioModifica = tbTipoSalidas.tsal_UsuarioModifica,
                        tsal_FechaModifica = tbTipoSalidas.tsal_FechaModifica,
                        tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoSalidas.tbUsuario).usu_NombreUsuario },
                        tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoSalidas.tbUsuario1).usu_NombreUsuario }
                    };
                    return Json(TipoSalida, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [SessionManager("TipoSalidas/Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbTipoSalidas tbTipoSalidas = null;
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    tbTipoSalidas = db.tbTipoSalidas.Find(id);
                    if (tbTipoSalidas == null)
                    {
                        return HttpNotFound();
                    }
                    Session["id"] = id;
                    var TipoSalida = new tbTipoSalidas
                    {
                        tsal_Id = tbTipoSalidas.tsal_Id,
                        tsal_Descripcion = tbTipoSalidas.tsal_Descripcion,
                        tsal_Estado = tbTipoSalidas.tsal_Estado,
                        tsal_RazonInactivo = tbTipoSalidas.tsal_RazonInactivo,
                        tsal_UsuarioCrea = tbTipoSalidas.tsal_UsuarioCrea,
                        tsal_FechaCrea = tbTipoSalidas.tsal_FechaCrea,
                        tsal_UsuarioModifica = tbTipoSalidas.tsal_UsuarioModifica,
                        tsal_FechaModifica = tbTipoSalidas.tsal_FechaModifica,
                        tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoSalidas.tbUsuario).usu_NombreUsuario },
                        tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoSalidas.tbUsuario1).usu_NombreUsuario }
                    };
                    return Json(TipoSalida, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Habilidades/Edit/5
        [HttpPost]
        [SessionManager("TipoSalidas/Edit")]
        public JsonResult Edit(tbTipoSalidas tbTipoSalidas)
        {
            string msj = "";
            if (tbTipoSalidas.tsal_Id != 0 && tbTipoSalidas.tsal_Descripcion != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    using (db = new ERP_GMEDINAEntities())
                    {
                        var list = db.UDP_RRHH_tbTipoSalidas_Update(id, tbTipoSalidas.tsal_Descripcion, (int)Session["UserLogin"], Function.DatetimeNow());
                        foreach (UDP_RRHH_tbTipoSalidas_Update_Result item in list)
                        {
                            msj = item.MensajeError + " ";
                        }
                        return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
                    }
                }
                catch
                {
                    return Json("-2", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("-3", JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Habilidades/Delete/5
        [HttpPost]
        [SessionManager("TipoSalidas/Delete")]
        public ActionResult Delete(tbTipoSalidas tbTipoSalidas)
        {
            string msj = "";
            string RazonInactivo = "Se ha Inhabilitado este Registro";

            if (tbTipoSalidas.tsal_Id != 0)
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    using (db = new ERP_GMEDINAEntities())
                    {
                        var list = db.UDP_RRHH_tbTipoSalidas_Delete(id, RazonInactivo, (int)Session["UserLogin"], Function.DatetimeNow());
                        foreach (UDP_RRHH_tbTipoSalidas_Delete_Result item in list)
                        {
                            msj = item.MensajeError + " ";
                        }
                        return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
                    }
                }
                catch 
                {
                    return Json("-2", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("-3", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [SessionManager("TipoSalidas/hablilitar")]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    var list = db.UDP_RRHH_tbTipoSalidas_Restore(id, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbTipoSalidas_Restore_Result item in list)
                    {
                        result = item.MensajeError.ToString();
                    }
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json("-2", JsonRequestBehavior.AllowGet);
                }
            }
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
