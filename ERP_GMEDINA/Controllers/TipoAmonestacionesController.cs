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
    public class TipoAmonestacionesController : Controller
    {
        private ERP_GMEDINAEntities1 db = new ERP_GMEDINAEntities1();

        // GET: TipoAmonestaciones
        public ActionResult Index()
        {
            List<tbTipoAmonestaciones> tbTipoAmonestaciones = new List<Models.tbTipoAmonestaciones> { };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            try
            {
                tbTipoAmonestaciones = db.tbTipoAmonestaciones.Where(x => x.tamo_Estado == true).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).ToList();
                //tbHabilidades.Add(new tbHabilidades { habi_Id = 0, habi_Descripcion = "fallo la conexion" });
                return View(tbTipoAmonestaciones);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                tbTipoAmonestaciones.Add(new tbTipoAmonestaciones { tamo_Id = 0, tamo_Descripcion = "fallo la conexion" });
            }
            return View(tbTipoAmonestaciones);
        }

        [HttpPost]
        public JsonResult llenarTabla()
        {
            List<tbTipoAmonestaciones> tbTipoAmonestaciones =
                new List<Models.tbTipoAmonestaciones> { };
            foreach (tbTipoAmonestaciones x in db.tbTipoAmonestaciones.ToList().Where(x => x.tamo_Estado == true))
            {
                tbTipoAmonestaciones.Add(new tbTipoAmonestaciones
                {
                    tamo_Id = x.tamo_Id,
                    tamo_Descripcion = x.tamo_Descripcion
                });
            }
            return Json(tbTipoAmonestaciones, JsonRequestBehavior.AllowGet);
        }
        // POST: TipoAmonestaciones/Create
        [HttpPost]
        public JsonResult Create(tbTipoAmonestaciones tbTipoAmonestaciones)
        {
            string msj = "";
            if (tbTipoAmonestaciones.tamo_Descripcion != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbTipoAmonestaciones_Insert(tbTipoAmonestaciones.tamo_Descripcion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbTipoAmonestaciones_Insert_Result item in list)
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

        // GET: TipoAmonestaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbTipoAmonestaciones tbTipoAmonestaciones = null;
            try
            {
                tbTipoAmonestaciones = db.tbTipoAmonestaciones.Find(id);
                if (tbTipoAmonestaciones == null || !tbTipoAmonestaciones.tamo_Estado)
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
            var tipoAmonestaciones = new tbTipoAmonestaciones
            {
                tamo_Id = tbTipoAmonestaciones.tamo_Id,
                tamo_Descripcion = tbTipoAmonestaciones.tamo_Descripcion,
                tamo_Estado = tbTipoAmonestaciones.tamo_Estado,
                tamo_RazonInactivo = tbTipoAmonestaciones.tamo_RazonInactivo,
                tamo_UsuarioCrea = tbTipoAmonestaciones.tamo_UsuarioCrea,
                tamo_FechaCrea = tbTipoAmonestaciones.tamo_FechaCrea,
                tamo_UsuarioModifica = tbTipoAmonestaciones.tamo_UsuarioModifica,
                tamo_FechaModifica = tbTipoAmonestaciones.tamo_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoAmonestaciones.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoAmonestaciones.tbUsuario1).usu_NombreUsuario }
            };
            return Json(tipoAmonestaciones, JsonRequestBehavior.AllowGet);
        }

        // POST: TipoAmonestaciones/Edit/5
        [HttpPost]
        public JsonResult Edit(tbTipoAmonestaciones tbTipoAmonestaciones)
        {
            string msj = "";
            if (tbTipoAmonestaciones.tamo_Id != 0 && tbTipoAmonestaciones.tamo_Descripcion != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbTipoAmonestaciones_Update(id, tbTipoAmonestaciones.tamo_Descripcion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbTipoAmonestaciones_Update_Result item in list)
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



        // POST: TipoAmonestaciones/Delete/5
        [HttpPost]
        public ActionResult Delete(tbTipoAmonestaciones tbTipoAmonestaciones)
        {
            string msj = "";
            if (tbTipoAmonestaciones.tamo_Id != 0 && tbTipoAmonestaciones.tamo_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbTipoAmonestaciones_Delete(id, tbTipoAmonestaciones.tamo_RazonInactivo, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbTipoAmonestaciones_Delete_Result item in list)
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
