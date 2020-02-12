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
    public class IdiomasController : Controller
    {
        private ERP_GMEDINAEntities db = null;
        Models.Helpers Function = new Models.Helpers();
        // GET: Idiomas Index
        //public ActionResult Index()
        //{
        //    List<tbIdiomas> tbIdiomas = new List<Models.tbIdiomas> { };
        //    Session["Usuario"] = new tbUsuario { usu_Id = 1 };
        //    try
        //    {
        //        tbIdiomas = db.tbIdiomas.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).ToList();
        //        return View(tbIdiomas);
        //    }
        //    catch(Exception ex)
        //    {
        //        ex.Message.ToString();
        //        tbIdiomas.Add(new tbIdiomas { idi_Id = 0, idi_Descripcion = "Fallo la conexión" });
        //    }
        //    return View(tbIdiomas);
        //}
        [SessionManager("Idiomas/Index")]
        public ActionResult Index()
        {
            db = new ERP_GMEDINAEntities();
            tbIdiomas tbIdiomas = new tbIdiomas { idi_Estado = true };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            try
            {
                return View(tbIdiomas);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return View(tbIdiomas);
        }
        //Llenar la tabla de algun lado
        [SessionManager("Idiomas/Index")]
        [HttpPost]
        public JsonResult llenarTabla()
        {
            db = new ERP_GMEDINAEntities();
            List<tbIdiomas> tbIdiomas = new List<Models.tbIdiomas> { };
            foreach (tbIdiomas x in db.tbIdiomas.ToList())
            {
                tbIdiomas.Add(new tbIdiomas
                {
                    idi_Id = x.idi_Id,
                    idi_Descripcion = x.idi_Descripcion,
                    idi_Estado = x.idi_Estado
                });
            }
            return Json(tbIdiomas, JsonRequestBehavior.AllowGet);
        }

        // POST: Idiomas/Create
        [SessionManager("Idiomas/Details")]
        public JsonResult Details(int? ID)
        {
            db = new ERP_GMEDINAEntities();
            db.Configuration.ProxyCreationEnabled = false;
            tbIdiomas tbJSON = db.tbIdiomas.Find(ID);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }

        // POST: Idiomas/Create
        [HttpPost]
        [SessionManager("Idiomas/Create")]
        public JsonResult Create(tbIdiomas tbIdiomas)
        {

            string msj = "...";
            if (tbIdiomas.idi_Descripcion != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbIdiomas_Insert(tbIdiomas.idi_Descripcion,
                                                            (int)Session["UserLogin"],
                                                            Function.DatetimeNow());
                    foreach (UDP_RRHH_tbIdiomas_Insert_Result item in list)
                    {
                        msj = item.MensajeError + "";
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
        [SessionManager("Idiomas/Edit")]
        public ActionResult Edit(int? ID)
        {

            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbIdiomas tbIdiomas = null;
            try
            {
                db = new ERP_GMEDINAEntities();
                tbIdiomas = db.tbIdiomas.Find(ID);

                if(tbIdiomas == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return HttpNotFound();
            }
            Session["id"] = ID;
            var idiomas = new tbIdiomas
            {
                idi_Id = tbIdiomas.idi_Id,
                idi_Descripcion = tbIdiomas.idi_Descripcion,
                idi_Estado = tbIdiomas.idi_Estado,
                idi_RazonInactivo = tbIdiomas.idi_RazonInactivo,
                idi_UsuarioCrea = tbIdiomas.idi_UsuarioCrea,
                idi_FechaCrea = tbIdiomas.idi_FechaCrea,
                idi_UsuarioModifica = tbIdiomas.idi_UsuarioModifica,
                idi_FechaModifica = tbIdiomas.idi_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbIdiomas.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbIdiomas.tbUsuario1).usu_NombreUsuario }
            };
            return Json(idiomas, JsonRequestBehavior.AllowGet);
        }
        //POST : Idiomas/Edit

        [HttpPost]
        [SessionManager("Idiomas/Edit")]
        public JsonResult Edit(tbIdiomas tbIdiomas)
        {

            string msj = "";
            if (tbIdiomas.idi_Id != 0 && tbIdiomas.idi_Descripcion != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbIdiomas_Update(id,
                                                            tbIdiomas.idi_Descripcion,
                                                            (int)Session["UserLogin"],
                                                            Function.DatetimeNow());
                    foreach (UDP_RRHH_tbIdiomas_Update_Result item in list)
                    {
                        msj = item.MensajeError + "";
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
            return Json(msj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [SessionManager("Idiomas/hablilitar")]
        public JsonResult hablilitar(int id)
        {

            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbIdiomas_Restore(id, (int)Session["UserLogin"],Function.DatetimeNow());
                    foreach (UDP_RRHH_tbIdiomas_Restore_Result item in list)
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
        // Idiomas/Delete
        [HttpPost]
        [SessionManager("Idiomas/Delete")]
        public ActionResult Delete(tbIdiomas tbIdiomas)
        {

            string msj = "...";

            string RazonInactivo = "Se ha Inhabilitado este Registro";
            if (tbIdiomas.idi_Id != 0)
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbIdiomas_Delete(id,
                                                            RazonInactivo,
                                                            (int)Session["UserLogin"],
                                                            Function.DatetimeNow());
                    foreach (UDP_RRHH_tbIdiomas_Delete_Result item in list)
                    {
                        msj = item.MensajeError + "";
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
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
