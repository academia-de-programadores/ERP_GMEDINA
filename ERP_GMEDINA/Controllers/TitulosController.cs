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
    public class TitulosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Titulos
        public ActionResult Index()
        {

            if (Session["Admin"] == null && Session["Usuario"] == null)
            {
                Response.Redirect("~/Inicio/index");
                return null;
            }
            try
            {
                
            tbTitulos tbtitulos = new tbTitulos { };                   
            return View(tbtitulos);
           }
            catch
               {
                return View();
                }
        }

        [HttpPost]
        public JsonResult llenarTabla()
        {
            try
            {
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
        public JsonResult Create(tbTitulos tbtitulos)
        {
            string msj = "";
            if (tbtitulos.titu_Descripcion != "")
            {
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbTitulos_Insert(tbtitulos.titu_Descripcion, usuario.usu_Id, DateTime.Now);
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
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTitulos tbtitulos = null;

            try
            {
                tbtitulos = db.tbTitulos.Find(id);
                if (tbtitulos == null )
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
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbtitulos.tbUsuario).usu_NombreUsuario }
            };
            return Json(titulos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(tbTitulos tbtitulos)
        {
            string msj = "";
            if (tbtitulos.titu_Id != 0 && tbtitulos.titu_Descripcion != "")
            {
                var id = (int)Session["id"];
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbTitulos_Update(id, tbtitulos.titu_Descripcion, usuario.usu_Id, DateTime.Now);
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

        public ActionResult Delete(tbTitulos tbtitulos)
        {
            string msj = "";
            string RazonInactivo = "Se ha Inhabilitado este Registro";

            if (tbtitulos.titu_Id != 0 )
            {
                var id = (int)Session["id"];
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbTitulos_Delete(id, RazonInactivo, usuario.usu_Id, DateTime.Now);
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
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    var list = db.UDP_RRHH_tbTitulos_Restore(id, Usuario.usu_Id, DateTime.Now);
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
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}




















