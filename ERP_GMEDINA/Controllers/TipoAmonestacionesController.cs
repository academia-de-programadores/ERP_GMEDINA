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
    public class TipoAmonestacionesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: TipoAmonestaciones
        public ActionResult Index()
        {
            var tbTipoAmonestaciones = db.tbTipoAmonestaciones.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbTipoAmonestaciones.ToList());
        }
        //LO AGREGUE
        public ActionResult GetData()
        {
            var tbTipoAmonestaciones1 = db.tbTipoAmonestaciones.ToList();
            return new JsonResult { Data = tbTipoAmonestaciones1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
     
        // GET: TipoAmonestaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoAmonestaciones tbTipoAmonestaciones = db.tbTipoAmonestaciones.Find(id);
            if (tbTipoAmonestaciones == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoAmonestaciones);
        }

        // GET: TipoAmonestaciones/Create
        //LO AGREGE
        public ActionResult Create()
        {
            ViewBag.tamo_Id = new SelectList(db.tbTipoAmonestaciones, "tamo_Id", "tamo_Descripcion");
            //ViewBag.tamo_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            //ViewBag.tamo_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: TipoAmonestaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

            //LO AGREGE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tamo_Id,tamo_Descripcion,tamo_Estado,tamo_RazonInactivo,tamo_UsuarioCrea,tamo_FechaCrea,tamo_UsuarioModifica,tamo_FechaModifica")] tbTipoAmonestaciones tbTipoAmonestaciones)
        {
            tbTipoAmonestaciones.tamo_UsuarioCrea = 1;
            tbTipoAmonestaciones.tamo_FechaCrea = DateTime.Now;
            string response = String.Empty;
            IEnumerable<object> listTipoAmonestacion = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {

                    listTipoAmonestacion = db.UDP_RRHH_tbTipoAmonestaciones_Insert(tbTipoAmonestaciones.tamo_Descripcion,
                                                                                   tbTipoAmonestaciones.tamo_UsuarioCrea,
                                                                                   tbTipoAmonestaciones.tamo_FechaCrea);
                    foreach (UDP_RRHH_tbTipoAmonestaciones_Insert_Result Resultado in listTipoAmonestacion)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    response = Ex.Message.ToString();
                }
                response="Exito";
            }
            else
            {
                response = "Falló";
            }
            ViewBag.tamo_Id = new SelectList(db.tbTipoAmonestaciones, "tamo_Id", "tamo_Descripcion", tbTipoAmonestaciones.tamo_Id);
            return Json(response, JsonRequestBehavior.AllowGet);

        }
        
        // GET: TipoAmonestaciones/Edit/5
        //LO AGREGE
        public ActionResult Edit(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbTipoAmonestaciones tbTipoAmonestacionesJSON = db.tbTipoAmonestaciones.Find(id);
            return Json(tbTipoAmonestacionesJSON,JsonRequestBehavior.AllowGet);
        }

        // POST: TipoAmonestaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //PROCESO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tamo_Id,tamo_Descripcion,tamo_Estado,tamo_RazonInactivo,tamo_UsuarioCrea,tamo_FechaCrea,tamo_UsuarioModifica,tamo_FechaModifica")] tbTipoAmonestaciones tbTipoAmonestaciones)
        {
            //traer los campo de auditorioa de Usuario y Fecha Crea
            tbTipoAmonestaciones.tamo_UsuarioCrea = 1;
            tbTipoAmonestaciones.tamo_FechaCrea = DateTime.Now;

            //Llenar los campo de auditoria
            tbTipoAmonestaciones.tamo_UsuarioModifica = 1;
            tbTipoAmonestaciones.tamo_FechaModifica = DateTime.Now;

            //Variable donde se alamacenara los resultados
            string response = String.Empty;
            IEnumerable<object> listTipoAmonestaciones = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
              try
                {
                    //PROCEDIMIENTO
                    listTipoAmonestaciones = db.UDP_RRHH_tbTipoAmonestaciones_Update(tbTipoAmonestaciones.tamo_Id,
                                                                             tbTipoAmonestaciones.tamo_Descripcion,
                                                                                     tbTipoAmonestaciones.tamo_UsuarioModifica,
                                                                                     tbTipoAmonestaciones.tamo_FechaModifica);

                    foreach (UDP_RRHH_tbTipoAmonestaciones_Update_Result Resultado in listTipoAmonestaciones)
                        MensajeError = Resultado.MensajeError;
                    if(MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo encontrar");
                        response = "error";
                    }
                }
                catch (Exception EX)
                {
                    EX.Message.ToString();
                }
                response = "Bien";
            }
            else
            {

                ModelState.AddModelError("", "No se pudo");
                response = "mal";
            }

            ViewBag.tamo_Id = new SelectList(db.tbTipoAmonestaciones, "tamo_Id", "tamo_Descripcion", tbTipoAmonestaciones.tamo_Id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // GET: TipoAmonestaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoAmonestaciones tbTipoAmonestaciones = db.tbTipoAmonestaciones.Find(id);
            if (tbTipoAmonestaciones == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoAmonestaciones);
        }

        // POST: TipoAmonestaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoAmonestaciones tbTipoAmonestaciones = db.tbTipoAmonestaciones.Find(id);
            db.tbTipoAmonestaciones.Remove(tbTipoAmonestaciones);
            db.SaveChanges();
            return RedirectToAction("Index");
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
