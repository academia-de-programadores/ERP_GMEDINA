using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using ERP_GMEDINA.Attribute;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Controllers
{
    public class TipoIncapacidadesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        Models.Helpers Function = new Models.Helpers();

        [SessionManager("TipoIncapacidades/Index")]
        public ActionResult Index()
        {
            tbTipoIncapacidades tbTipoIncapacidades = new tbTipoIncapacidades { ticn_Estado = true };
           
            try
            {
                //tbTipoIncapacidades = db.tbTipoIncapacidades.Where(x => x.ticn_Estado == true).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).ToList();
                return View(tbTipoIncapacidades);
            }
            catch (Exception ex)
            {


                ex.Message.ToString();
                //tbTipoIncapacidades.Add(new tbTipoIncapacidades { ticn_Id = 0, ticn_Descripcion = "Fallo la conexión" });
            }
            return View(tbTipoIncapacidades);
        }

        [HttpPost]
        [SessionManager("TipoIncapacidades/Index")]
        public JsonResult llenarTabla()
        {
            try
            {
                var tbTipoIncapacidades = db.tbTipoIncapacidades
                    .Select(
                    x => new {

                        ticn_Id = x.ticn_Id,
                        ticn_Descripcion = x.ticn_Descripcion,
                        ticn_Estado = x.ticn_Estado
                    }

                    ).ToList();
                return Json(tbTipoIncapacidades, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.ToString();
                //throw;
                return Json(-2, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        [SessionManager("TipoIncapacidades/Create")]
        public JsonResult Create(tbTipoIncapacidades tbTipoIncapacidades)
        {
            string msj = "";
            if (tbTipoIncapacidades.ticn_Descripcion != "")
            {
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbTipoIncapacidades_Insert(tbTipoIncapacidades.ticn_Descripcion, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbTipoIncapacidades_Insert_Result item in list)
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
        [SessionManager("TipoIncapacidades/Edit")]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoIncapacidades tbTipoIncapacidades = null;


            try
            {
                tbTipoIncapacidades = db.tbTipoIncapacidades.Find(id);
                if (tbTipoIncapacidades == null)
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
            var TipoIncapacidades = new tbTipoIncapacidades
            {
                ticn_Id = tbTipoIncapacidades.ticn_Id,
                ticn_Descripcion = tbTipoIncapacidades.ticn_Descripcion,
                ticn_Estado = tbTipoIncapacidades.ticn_Estado,
                ticn_RazonInactivo = tbTipoIncapacidades.ticn_RazonInactivo,
                ticn_UsuarioCrea = tbTipoIncapacidades.ticn_UsuarioCrea,
                ticn_FechaCrea = tbTipoIncapacidades.ticn_FechaCrea,
                ticn_UsuarioModifica = tbTipoIncapacidades.ticn_UsuarioModifica,
                ticn_FechaModifica = tbTipoIncapacidades.ticn_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoIncapacidades.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoIncapacidades.tbUsuario1).usu_NombreUsuario }
            };
            return Json(TipoIncapacidades, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [SessionManager("TipoIncapacidades/Edit")]
        public JsonResult Edit(tbTipoIncapacidades tbTipoIncapacidades)
        {
            string msj = "";
            if (tbTipoIncapacidades.ticn_Descripcion != "")
            {
                var id = (int)Session["id"];
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbTipoIncapacidades_Update(id, tbTipoIncapacidades.ticn_Descripcion, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbTipoIncapacidades_Update_Result item in list)
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
        [SessionManager("TipoIncapacidades/Delete")]
        public ActionResult Delete(tbTipoIncapacidades tbTipoIncapacidades)
        {
            string msj = "";

            string RazonInactivo = "Se ha Inhabilitado este Registro";

            if (tbTipoIncapacidades.ticn_Id != 0)
            {
                var id = (int)Session["id"];
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbTipoIncapacidades_Delete(id, RazonInactivo, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbTipoIncapacidades_Delete_Result item in list)
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
        [SessionManager("TipoIncapacidades/hablilitar")]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    var list = db.UDP_RRHH_tbTipoIncapacidades_Restore(id, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbTipoIncapacidades_Restore_Result item in list)
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
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
