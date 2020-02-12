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
    public class CargosController : Controller
    {
        private ERP_GMEDINAEntities db = null;
        Models.Helpers Function = new Models.Helpers();
        [SessionManager("Cargos/Index")]
        //GET: Cargos
        public ActionResult Index()
        {
            tbCargos tbCargos =new tbCargos {car_Estado=true };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            try
            {
                db = new ERP_GMEDINAEntities();
                 tbCargos = db.tbCargos.Where(x => x.car_Estado == true).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).ToList()[0];

                return View(tbCargos);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                tbCargos = new tbCargos { car_Id = 0, car_Descripcion = "fallo la conexion" };
            }
            return View(tbCargos);
        }

      
        [HttpPost]
        [SessionManager("Cargos/Index")]
        public JsonResult llenarTabla()
        {
            try
            {
                db = new ERP_GMEDINAEntities();
                var tbCargos = db.tbCargos
                .Select(
                   t => new {
                        car_Id = t.car_Id,
                        car_Descripcion = t.car_Descripcion,
                        car_Estado = t.car_Estado
                    }
                )
                .ToList();
            return Json(tbCargos, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        [HttpPost]
        [SessionManager("Cargos/Create")]
        public JsonResult Create(tbCargos tbCargos)
        {
            string msj = "";
            if (tbCargos.car_Descripcion != "")
            {
                db = new ERP_GMEDINAEntities();
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbCargos_Insert(tbCargos.car_Descripcion, tbCargos.car_SueldoMinimo, tbCargos.car_SueldoMaximo, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbCargos_Insert_Result item in list)
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

        [SessionManager("Cargos/Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbCargos tbCargos = null;
            try
            {
                db = new ERP_GMEDINAEntities();
                tbCargos = db.tbCargos.Find(id);
                if (tbCargos == null )
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
            var Cargos = new tbCargos
            {
                car_Id = tbCargos.car_Id,
                car_Descripcion = tbCargos.car_Descripcion,
                car_SueldoMinimo = tbCargos.car_SueldoMinimo,
                car_SueldoMaximo = tbCargos.car_SueldoMaximo,
                car_Estado = tbCargos.car_Estado,
                car_RazonInactivo = tbCargos.car_RazonInactivo,
                car_UsuarioCrea = tbCargos.car_UsuarioCrea,
                car_FechaCrea = tbCargos.car_FechaCrea,
                car_UsuarioModifica = tbCargos.car_UsuarioModifica,
                car_FechaModifica = tbCargos.car_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbCargos.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbCargos.tbUsuario1).usu_NombreUsuario }
            };

            return Json(Cargos, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [SessionManager("Cargos/Edit")]
        public JsonResult Edit(tbCargos tbCargos)
        {
            string msj = "";
            if (tbCargos.car_Id != 0 && tbCargos.car_Descripcion != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbCargos_Update(id, tbCargos.car_Descripcion, tbCargos.car_SueldoMinimo, tbCargos.car_SueldoMaximo, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbCargos_Update_Result item in list)
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
        [SessionManager("Cargos/Delete")]
        public ActionResult Delete(tbCargos tbCargos)
        {
            string msj = "";
            if (tbCargos.car_Id != 0 && tbCargos.car_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbCargos_Delete(id, tbCargos.car_RazonInactivo, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbCargos_Delete_Result item in list)
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


        [HttpPost]
        [SessionManager("Cargos/hablilitar")]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbCargos_Restore(id,(int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbCargos_Restore_Result item in list)
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
