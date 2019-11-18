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
    public class HabilidadesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Habilidades
        public ActionResult Index()
        {
            List< tbHabilidades> tbHabilidades = new List<Models.tbHabilidades> { };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            try
            {
                tbHabilidades = db.tbHabilidades.Where(x=>x.habi_Estado==true).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).ToList();
                return View(tbHabilidades);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                tbHabilidades.Add(new tbHabilidades {habi_Id=1,habi_Descripcion="fallo la conexion" });
            }
            return View(tbHabilidades);
        }
        [HttpPost]
        public JsonResult llenarTabla()
        {
            List<tbHabilidades> tbHabilidades =
                new List<Models.tbHabilidades> { };
            foreach (tbHabilidades x in db.tbHabilidades.ToList().Where(x=>x.habi_Estado==true))
            {
                tbHabilidades.Add( new tbHabilidades
                {
                    habi_Id = x.habi_Id,
                    habi_Descripcion = x.habi_Descripcion,
                    habi_Estado = x.habi_Estado,
                    habi_RazonInactivo = x.habi_RazonInactivo,
                    habi_UsuarioCrea = x.habi_UsuarioCrea,
                    habi_FechaCrea = x.habi_FechaCrea,
                    habi_UsuarioModifica = x.habi_UsuarioModifica,
                    habi_FechaModifica = x.habi_FechaModifica
                });
            }
            //tbHabilidades Habilidad = new tbHabilidades {habi_Descripcion="hola", habi_Id=1 };
            //List<tbHabilidades> tbHabilidades = new List<Models.tbHabilidades> { };
            ////tbHabilidades.Add(Habilidad);
            return Json(tbHabilidades, JsonRequestBehavior.AllowGet);
        }
        
        // POST: Habilidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Bind(Include = "habi_Id,habi_Descripcion,habi_Estado,habi_RazonInactivo,habi_UsuarioCrea,habi_FechaCrea,habi_UsuarioModifica,habi_FechaModifica")]
        [HttpPost]
        public JsonResult Create(tbHabilidades tbHabilidades)
        {
            string msj = "";
            var Usuario=(tbUsuario)Session["Usuario"];
            try
            {
                var list= db.UDP_RRHH_tbHabilidades_Insert(tbHabilidades.habi_Descripcion, Usuario.usu_Id, DateTime.Now);
                foreach (UDP_RRHH_tbHabilidades_Insert_Result item in list)
                {
                    msj = item.MensajeError;
                }
                if (msj.Length > 2)
                {
                    msj = msj.Substring(0, 2);
                }
            }
            catch (Exception ex)
            {
                msj = "-2";
                ex.Message.ToString();
            }
            return Json(msj,JsonRequestBehavior.AllowGet);
        }

        // GET: Habilidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHabilidades tbHabilidades = db.tbHabilidades.Find(id);
            if (tbHabilidades == null || !tbHabilidades.habi_Estado)
            {
                return HttpNotFound();
            }
            Session["id"] = id;
            var habilidad = new tbHabilidades
            {
                habi_Id = tbHabilidades.habi_Id,
                habi_Descripcion = tbHabilidades.habi_Descripcion,
                habi_Estado = tbHabilidades.habi_Estado,
                habi_RazonInactivo = tbHabilidades.habi_RazonInactivo,
                habi_UsuarioCrea = tbHabilidades.habi_UsuarioCrea,
                habi_FechaCrea = tbHabilidades.habi_FechaCrea,
                habi_UsuarioModifica = tbHabilidades.habi_UsuarioModifica,
                habi_FechaModifica = tbHabilidades.habi_FechaModifica
            };
            if (tbHabilidades.tbUsuario!=null)
            {
                habilidad.tbUsuario = new tbUsuario { usu_NombreUsuario = tbHabilidades.tbUsuario.usu_NombreUsuario };
            }
            else
            {
                habilidad.tbUsuario = new tbUsuario { usu_NombreUsuario = "" };
            }
            if (tbHabilidades.tbUsuario1 != null)
            {
                habilidad.tbUsuario1 = new tbUsuario { usu_NombreUsuario = tbHabilidades.tbUsuario1.usu_NombreUsuario };
            }
            else
            {
                habilidad.tbUsuario1 = new tbUsuario { usu_NombreUsuario = "" };
            }
            return Json(habilidad, JsonRequestBehavior.AllowGet);
        }

        // POST: Habilidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Edit(tbHabilidades tbHabilidades)
        {
            var id =(int) Session["id"];
            string msj = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                var list = db.UDP_RRHH_tbHabilidades_Update(id, tbHabilidades.habi_Descripcion, Usuario.usu_Id, DateTime.Now);
                foreach (UDP_RRHH_tbHabilidades_Update_Result item in list)
                {
                    msj = item.MensajeError;
                }
                if (msj.Length>2)
                {
                    msj = msj.Substring(0, 2);
                }
            }
            catch (Exception ex)
            {
                msj = "-2";
                ex.Message.ToString();
            }
            return Json(msj);
        }

        // GET: Habilidades/Delete/5
        [HttpPost]
        public ActionResult Delete(tbHabilidades tbHabilidades)
        {
            var id = (int)Session["id"];
            string msj = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                var list = db.UDP_RRHH_tbHabilidades_Delete(id, tbHabilidades.habi_RazonInactivo, Usuario.usu_Id, DateTime.Now);
                foreach (UDP_RRHH_tbHabilidades_Delete_Result item in list)
                {
                    msj = item.MensajeError;
                }
                if (msj.Length > 2)
                {
                    msj = msj.Substring(0, 2);
                }
            }
            catch (Exception ex)
            {
                msj = "-2";
                ex.Message.ToString();
            }
            return Json(msj);
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
