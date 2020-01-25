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
    public class FasesReclutamientoController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        public ActionResult Index()
        {
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };

            tbFasesReclutamiento tbFasesReclutamiento = new tbFasesReclutamiento { fare_Estado = true };
            return View(tbFasesReclutamiento);
        }

        [HttpPost]
        public JsonResult llenarTabla()
        {
            try
            {
                db = new ERP_GMEDINAEntities();
                List<tbFasesReclutamiento> tbFasesReclutamiento =
                new List<Models.tbFasesReclutamiento> { };
                foreach (tbFasesReclutamiento x in db.tbFasesReclutamiento.ToList())
                {
                    tbFasesReclutamiento.Add(new tbFasesReclutamiento
                    {
                        fare_Id = x.fare_Id,
                        fare_Descripcion = x.fare_Descripcion,
                        fare_Estado = x.fare_Estado
                    });
                }
                return Json(tbFasesReclutamiento, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }

        // POST: FasesReclutamiento/Create
        [HttpPost]
        public JsonResult Create(tbFasesReclutamiento tbFasesReclutamiento)
        {
            string msj = "";
            if (tbFasesReclutamiento.fare_Descripcion != "")
            {
                db = new ERP_GMEDINAEntities();
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbFasesReclutamiento_Insert(tbFasesReclutamiento.fare_Descripcion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbFasesReclutamiento_Insert_Result item in list)
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

            tbFasesReclutamiento tbFasesReclutamiento = null;
            try
            {
                db = new ERP_GMEDINAEntities();
                tbFasesReclutamiento = db.tbFasesReclutamiento.Find(id);
                if (tbFasesReclutamiento == null)
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
            var FaseReclutamiento = new tbFasesReclutamiento
            {
                fare_Id = tbFasesReclutamiento.fare_Id,
                fare_Descripcion = tbFasesReclutamiento.fare_Descripcion,
                fare_Estado = tbFasesReclutamiento.fare_Estado,
                fare_RazonInactivo = tbFasesReclutamiento.fare_RazonInactivo,
                fare_UsuarioCrea = tbFasesReclutamiento.fare_UsuarioCrea,
                fare_FechaCrea = tbFasesReclutamiento.fare_FechaCrea,
                fare_UsuarioModifica = tbFasesReclutamiento.fare_UsuarioModifica,
                fare_FechaModifica = tbFasesReclutamiento.fare_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbFasesReclutamiento.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbFasesReclutamiento.tbUsuario1).usu_NombreUsuario }
            };
            return Json(FaseReclutamiento, JsonRequestBehavior.AllowGet);
        }

        // POST: Habilidades/Edit/5
        [HttpPost]
        public JsonResult Edit(tbFasesReclutamiento tbFasesReclutamiento)
        {
            string msj = "";
            if ( tbFasesReclutamiento.fare_Descripcion != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbFasesReclutamiento_Update(id, tbFasesReclutamiento.fare_Descripcion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbFasesReclutamiento_Update_Result item in list)
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
        public ActionResult Delete(tbFasesReclutamiento tbFasesReclutamiento)
        {
            string msj = "";
            string RazonInactivo = "Se ha Inhabilitado este Registro";
            if (tbFasesReclutamiento.fare_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbfasesReclutamiento_Delete(id, RazonInactivo, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbfasesReclutamiento_Delete_Result item in list)
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
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var list = db.UDP_RRHH_tbfasesReclutamiento_Restore(id, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbfasesReclutamiento_Restore_Result item in list)
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
