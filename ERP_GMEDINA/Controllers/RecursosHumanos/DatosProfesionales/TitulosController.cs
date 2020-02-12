using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using ERP_GMEDINA.Attribute;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Controllers
{
    public class TitulosController : Controller
    {
        private ERP_GMEDINAEntities db = null;
        Models.Helpers Function = new Models.Helpers();


        // GET: Titulos
        [SessionManager("Titulos/Index")]
        public ActionResult Index()
        {     
               var  tbtitulos = new tbTitulos { };
               return View(tbtitulos);
          
        }

        [HttpPost]
        [SessionManager("Titulos/Index")]
        public JsonResult llenarTabla()
        {
            try
            {
                db = new ERP_GMEDINAEntities();

                var tbTitulos = db.tbTitulos
                    .Select(
                    t => new
                    {
                        titu_Id = t.titu_Id,
                        titu_Descripcion = t.titu_Descripcion,
                        titu_Estado = t.titu_Estado,

                    }
                    ).ToList();

                return Json(tbTitulos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public ActionResult Create()
        {
            ViewBag.titu_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.titu_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }


        [HttpPost]
        [SessionManager("Titulos/Create")]

        public JsonResult Create(tbTitulos tbtitulos)
        {
            string msj = "";
            if (tbtitulos.titu_Descripcion != "")
            {
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbTitulos_Insert(tbtitulos.titu_Descripcion, (int)Session["UserLogin"],Function.DatetimeNow());
                    foreach (UDP_RRHH_tbTitulos_Insert_Result item in list)
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
        [HttpGet]
        [SessionManager("Titulos/Edit")]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTitulos tbtitulos = null;

            try
            {
                db = new ERP_GMEDINAEntities();
                tbtitulos = db.tbTitulos.Find(id);
                if (tbtitulos == null)
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
            var titulos = new tbTitulos
            {
                titu_Id = tbtitulos.titu_Id,
                titu_Descripcion = tbtitulos.titu_Descripcion,
                titu_Estado = tbtitulos.titu_Estado,
                titu_RazonInactivo = tbtitulos.titu_RazonInactivo,
                titu_UsuarioCrea = tbtitulos.titu_UsuarioCrea,
                titu_FechaCrea = tbtitulos.titu_FechaCrea,
                titu_UsuarioModifica = tbtitulos.titu_UsuarioModifica,
                titu_FechaModifica = tbtitulos.titu_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbtitulos.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbtitulos.tbUsuario1).usu_NombreUsuario }
            };
            return Json(titulos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SessionManager("Titulos/Edit")]
        public JsonResult Edit(tbTitulos tbtitulos)
        {
            string msj = "";
            if (tbtitulos.titu_Id != 0 && tbtitulos.titu_Descripcion != "")
            {
                var id = (int)Session["id"];
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbTitulos_Update(id, tbtitulos.titu_Descripcion, (int)Session["UserLogin"],Function.DatetimeNow());
                    foreach (UDP_RRHH_tbTitulos_Update_Result item in list)
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

        [SessionManager("Titulos/Delete")]
        public ActionResult Delete(tbTitulos tbtitulos)
        {
            string msj = "";
            string RazonInactivo = "Se ha Inhabilitado este Registro";

            if (tbtitulos.titu_Id != 0)
            {
                var id = (int)Session["id"];
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbTitulos_Delete(id, RazonInactivo, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbTitulos_Delete_Result item in list)
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
        [SessionManager("Titulos/habilitar")]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbTitulos_Restore(id, (int)Session["UserLogin"],Function.DatetimeNow());
                    foreach (UDP_RRHH_tbTitulos_Restore_Result item in list)
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




















