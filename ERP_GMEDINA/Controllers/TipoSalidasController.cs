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
    public class TipoSalidasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: TipoSalidas
        public ActionResult Index()
        {
            List<tbTipoSalidas> tbTipoSalidas = new List<Models.tbTipoSalidas> { };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            try
            {
                tbTipoSalidas = db.tbTipoSalidas.Where(x => x.tsal_Estado == true).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).ToList();
                return View(tbTipoSalidas);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                tbTipoSalidas.Add(new tbTipoSalidas { tsal_Id = 0, tsal_Descripcion = "fallo la conexion" });
            }
            return View(tbTipoSalidas);
        }

        [HttpPost]
        public JsonResult llenarTabla()
        {
            List<tbTipoSalidas> tbTipoSalidas =
                new List<Models.tbTipoSalidas> { };
            foreach (tbTipoSalidas x in db.tbTipoSalidas.ToList().Where(x => x.tsal_Estado == true))
            {
                tbTipoSalidas.Add(new tbTipoSalidas
                {
                    tsal_Id = x.tsal_Id,
                    tsal_Descripcion = x.tsal_Descripcion
                });
            }
            return Json(tbTipoSalidas, JsonRequestBehavior.AllowGet);
        }
        // GET: TipoSalidas/Create
        [HttpPost]
        public ActionResult Create(tbTipoSalidas tbTipoSalidas)
        {
            string msj = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                var list = db.UDP_RRHH_tbTipoSalidas_Insert(
                    tbTipoSalidas.tsal_Descripcion, 
                    Usuario.usu_Id, 
                    DateTime.Now);
                foreach (UDP_RRHH_tbTipoSalidas_Insert_Result item in list)
                {
                    msj = item.MensajeError.ToString()+" ";
                }
            }
            catch (Exception ex)
            {
                msj = "-2";
                ex.Message.ToString();
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }

        // GET: TipoSalidas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoSalidas tbTipoSalidas = db.tbTipoSalidas.Find(id);
            if (tbTipoSalidas == null || !tbTipoSalidas.tsal_Estado)
            {
                return HttpNotFound();
            }
            Session["id"] = id;
            var TipoSalidas = new tbTipoSalidas
            {
                tsal_Id = tbTipoSalidas.tsal_Id,
                tsal_Descripcion = tbTipoSalidas.tsal_Descripcion,
                tsal_Estado = tbTipoSalidas.tsal_Estado,
                tsal_RazonInactivo = tbTipoSalidas.tsal_RazonInactivo,
                tsal_UsuarioCrea = tbTipoSalidas.tsal_UsuarioCrea,
                tsal_FechaCrea = tbTipoSalidas.tsal_FechaCrea,
                tsal_UsuarioModifica = tbTipoSalidas.tsal_UsuarioModifica,
                tsal_FechaModifica = tbTipoSalidas.tsal_FechaModifica,
                tbUsuario=new tbUsuario { usu_NombreUsuario= tbTipoSalidas.tbUsuario.usu_NombreUsuario},
                tbUsuario1=new tbUsuario {usu_NombreUsuario= tbTipoSalidas.tbUsuario1.usu_NombreUsuario }
            };
            return Json(TipoSalidas, JsonRequestBehavior.AllowGet);
        }

        // POST: TipoSalidas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(tbTipoSalidas tbTipoSalidas)
        {
            var id = (int)Session["id"];
            string msj = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                var list = db.UDP_RRHH_tbTipoSalidas_Update(id, tbTipoSalidas.tsal_Descripcion, Usuario.usu_Id, DateTime.Now);
                foreach (UDP_RRHH_tbTipoSalidas_Update_Result item in list)
                {
                    msj = item.MensajeError.ToString()+" ";
                }
            }
            catch (Exception ex)
            {
                msj = "-2";
                ex.Message.ToString();
            }
            Session.Remove("Usuario");
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }

        // POST: TipoSalidas/Delete/5
        [HttpPost]
        public ActionResult Delete(tbTipoSalidas tbTipoSalidas)
        {
            var id = (int)Session["id"];
            string msj = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                var list = db.UDP_RRHH_tbTipoSalidas_Delete(
                    id,
                    tbTipoSalidas.tsal_Descripcion, 
                    Usuario.usu_Id, 
                    DateTime.Now);
                foreach (UDP_RRHH_tbTipoSalidas_Delete_Result item in list)
                {
                    msj = item.MensajeError.ToString()+" ";
                }
            }
            catch (Exception ex)
            {
                msj = "-2";
                ex.Message.ToString();
            }
            Session.Remove("Usuario");
            return Json(msj.Substring(0, 2),JsonRequestBehavior.AllowGet);
        }

        protected object IsNull(tbUsuario valor)
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
