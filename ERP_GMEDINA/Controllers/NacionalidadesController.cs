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
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Nacionalidades
        public ActionResult Index()
        {
            List<tbNacionalidades> tbNacionalidades = new List<Models.tbNacionalidades> { };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            try
            {
                tbNacionalidades = db.tbNacionalidades.Where(x => x.nac_Estado == true).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).ToList();
                //tbNacionalidades.Add(new tbNacionalidades { nac_Id = 0, nac_Descripcion = "fallo la conexion" });
                return View(tbNacionalidades);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                tbNacionalidades.Add(new tbNacionalidades { nac_Id = 0, nac_Descripcion = "falló la conexion" });
            }
            return View(tbNacionalidades);
        }

        [HttpPost]
        public JsonResult llenarTabla()
        {
            List<tbNacionalidades> tbNacionalidades =
                new List<Models.tbNacionalidades> { };
            foreach (tbNacionalidades x in db.tbNacionalidades.ToList().Where(x => x.nac_Estado == true))
            {
                tbNacionalidades.Add(new tbNacionalidades
                {
                    nac_Id = x.nac_Id,
                    nac_Descripcion = x.nac_Descripcion
                });
            }
            return Json(tbNacionalidades, JsonRequestBehavior.AllowGet);
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
                tbNacionalidades = db.tbNacionalidades.Find(id);
                if (tbNacionalidades == null || !tbNacionalidades.nac_Estado)
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
            var habilidad = new tbNacionalidades
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
            return Json(habilidad, JsonRequestBehavior.AllowGet);
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

        // GET: Nacionalidades/Delete/5
        [HttpPost]
        public ActionResult Delete(tbNacionalidades tbNacionalidades)
        {
            string msj = "";
            if (tbNacionalidades.nac_Id != 0 && tbNacionalidades.nac_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbNacionalidades_Delete(id, tbNacionalidades.nac_RazonInactivo, Usuario.usu_Id, DateTime.Now);
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
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
