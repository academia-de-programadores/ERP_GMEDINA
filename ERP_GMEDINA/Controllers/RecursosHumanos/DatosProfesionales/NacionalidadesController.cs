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
    public class NacionalidadesController : Controller
    {
        private ERP_GMEDINAEntities db = null;
        Models.Helpers Function = new Models.Helpers();
        // GET: Nacionalidades
        [SessionManager("Nacionalidades/Index")]
        public ActionResult Index()
        {
            tbNacionalidades tbNacionalidades = new tbNacionalidades { nac_Estado = true };
            bool Admin = (bool)Session["Admin"];
            return View(tbNacionalidades);
        }

        [HttpPost]
        [SessionManager("Nacionalidades/Index")]
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
                return Json(-2, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Nacionalidades/Create
        [HttpPost]
        [SessionManager("Nacionalidades/Create")]
        public JsonResult Create(tbNacionalidades tbNacionalidades)
        {
            string msj = "";
            tbNacionalidades tbNacionalidad = new tbNacionalidades();
            if (tbNacionalidades.nac_Descripcion != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbNacionalidades_Insert(tbNacionalidades.nac_Descripcion, (int)Session["UserLogin"], Function.DatetimeNow());
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
        [SessionManager("Nacionalidades/Edit")]
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
        [SessionManager("Nacionalidades/Edit")]
        public JsonResult Edit(tbNacionalidades tbNacionalidades)
        {
            string msj = "";
            tbNacionalidades tbNacionalidad = new tbNacionalidades();
            if (tbNacionalidades.nac_Id != 0 && tbNacionalidades.nac_Descripcion != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbNacionalidades_Update(id, tbNacionalidades.nac_Descripcion, (int)Session["UserLogin"], Function.DatetimeNow());
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
                // Session.Remove("id");
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
        [SessionManager("Nacionalidades/habilitar")]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
              
                using (db = new ERP_GMEDINAEntities())
                {
                    var list = db.UDP_RRHH_tbNacionalidades_Restore(id,(int)Session["UserLogin"], Function.DatetimeNow());
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
        [SessionManager("Nacionalidades/Delete")]
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
                    var list = db.UDP_RRHH_tbNacionalidades_Delete(id, RazonInactivo,(int)Session["UserLogin"], Function.DatetimeNow());
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
