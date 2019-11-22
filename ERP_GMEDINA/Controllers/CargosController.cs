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
    public class CargosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Cargos
        public ActionResult Index()
        {
            List<tbCargos> tbCargos = new List<Models.tbCargos> { };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            try
            {
                tbCargos = db.tbCargos.Where(x => x.car_Estado == true).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).ToList();
             
                return View(tbCargos);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                tbCargos.Add(new tbCargos { car_Id = 0, car_Descripcion = "fallo la conexion" });
            }
            return View(tbCargos);
        }


        [HttpPost]
        public JsonResult llenarTabla()
        {
            List<tbCargos> tbCargos =
                new List<Models.tbCargos> { };
            foreach (tbCargos x in db.tbCargos.ToList().Where(x => x.car_Estado == true))
            {
                tbCargos.Add(new tbCargos
                {
                    car_Id = x.car_Id,
                    car_Descripcion = x.car_Descripcion
                });
            }
            return Json(tbCargos, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public JsonResult Create(tbCargos tbCargos)
        {
            string msj = "";
            if (tbCargos.car_Descripcion != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbCargos_Insert(tbCargos.car_Descripcion, Usuario.usu_Id, DateTime.Now);
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



        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbCargos tbCargos = null;
            try
            {
                tbCargos = db.tbCargos.Find(id);
                if (tbCargos == null || !tbCargos.car_Estado)
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
        public JsonResult Edit(tbCargos tbCargos)
        {
            string msj = "";
            if (tbCargos.car_Id != 0 && tbCargos.car_Descripcion != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbCargos_Update(id, tbCargos.car_Descripcion, Usuario.usu_Id, DateTime.Now);
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
        public ActionResult Delete(tbCargos tbCargos)
        {
            string msj = "";
            if (tbCargos.car_Id != 0 && tbCargos.car_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbCargos_Delete(id, tbCargos.car_RazonInactivo, Usuario.usu_Id, DateTime.Now);
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
