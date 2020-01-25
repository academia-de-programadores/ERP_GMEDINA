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
    public class NacionalidadesController : Controller
    {
        private ERP_GMEDINAEntities db = null;

        // GET: Nacionalidades
        public ActionResult Index()
        {
            if (Session["Admin"] == null && Session["Usuario"] == null)
            {
                Response.Redirect("~/Inicio/index");
                return null;
            }
            tbNacionalidades tbNacionalidades = new tbNacionalidades { };
            return View(tbNacionalidades);

        }

        [HttpPost]
        public JsonResult llenarTabla()
        {

            try
            {
                db = new ERP_GMEDINAEntities();
                var tbNacionalidades = db.tbNacionalidades
                       .Select(
                       t => new
                       {
                           nac_Id = t.nac_Id,
                           nac_Descripcion = t.nac_Descripcion,
                           nac_Estado = t.nac_Estado
                       }
                       )
                       .ToList();
                return Json(tbNacionalidades, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        // POST: Nacionalidades/Create
        [HttpPost]
        public JsonResult Create(tbNacionalidades tbNacionalidades)
        {
            string msj = "";
            if (tbNacionalidades.nac_Descripcion != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbNacionalidades_Insert(tbNacionalidades.nac_Descripcion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbNacionalidades_Insert_Result item in list)
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

        // GET: Nacionalidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbNacionalidades tbNacionalidades = null;
            try
            {
                db = new ERP_GMEDINAEntities();
                tbNacionalidades = db.tbNacionalidades.Find(id);
                if (tbNacionalidades == null)
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
            var tbNacionalidad = new tbNacionalidades
            {
                nac_Id = tbNacionalidades.nac_Id,
                nac_Descripcion = tbNacionalidades.nac_Descripcion,
                nac_Estado = tbNacionalidades.nac_Estado,
                nac_RazonInactivo = tbNacionalidades.nac_RazonInactivo,
                nac_UsuarioCrea = tbNacionalidades.nac_UsuarioCrea,
                nac_FechaCrea = tbNacionalidades.nac_FechaCrea,
                nac_UsuarioModifica = tbNacionalidades.nac_UsuarioModifica,
                nac_FechaModifica = tbNacionalidades.nac_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbNacionalidades.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbNacionalidades.tbUsuario1).usu_NombreUsuario }
            };
            return Json(tbNacionalidad, JsonRequestBehavior.AllowGet);
        }

        // POST: Nacionalidades/Edit/5
        [HttpPost]
        public JsonResult Edit(tbNacionalidades tbNacionalidades)
        {
            string msj = "";
            if (tbNacionalidades.nac_Id != 0 && tbNacionalidades.nac_Descripcion != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbNacionalidades_Update(id, tbNacionalidades.nac_Descripcion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbNacionalidades_Update_Result item in list)
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

        //-------------------------------------------------------------------------------------//
        //Código para poder habilitar / activar el registro.

        [HttpPost]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                db = new ERP_GMEDINAEntities();
                //using (db = new ERP_GMEDINAEntities())
                {
                    var list = db.UDP_RRHH_tbNacionalidades_Restore(id, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbNacionalidades_Restore_Result item in list)
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

        //-------------------------------------------------------------------------------------------//


        // GET: Nacionalidades/Delete/5
        [HttpPost]
        public ActionResult Delete(tbNacionalidades tbNacionalidades)
        {
            string msj = "";
            string RazonInactivo = "Se ha Inhabilitado este Registro";


            if (tbNacionalidades.nac_Id != 0)
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];

                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbNacionalidades_Delete(id, RazonInactivo, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbNacionalidades_Delete_Result item in list)
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
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
