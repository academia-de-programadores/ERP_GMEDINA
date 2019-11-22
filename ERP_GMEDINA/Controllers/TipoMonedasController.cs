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
    public class TipoMonedasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: TipoMonedas
        public ActionResult Index()
        {
            var tbTipoMonedas = db.tbTipoMonedas.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbTipoMonedas.ToList());
        }

        // GET: TipoMonedas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoMonedas tbTipoMonedas = db.tbTipoMonedas.Find(id);
            if (tbTipoMonedas == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoMonedas);
        }

        // GET: TipoMonedas/Create
        public ActionResult Create()
        {
            ViewBag.tmon_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.tmon_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: TipoMonedas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tmon_Id,tmon_Descripcion,tmon_Estado,tmon_RazonInactivo,tmon_UsuarioCrea,tmon_FechaCrea,tmon_UsuarioModifica,tmon_FechaModifica")] tbTipoMonedas tbTipoMonedas)
        {
            tbTipoMonedas.tmon_UsuarioCrea = 1;
            tbTipoMonedas.tmon_FechaCrea = DateTime.Now;

            string response = String.Empty;
            IEnumerable<Object> listaTipoMonedas = null;
            string MensajeError = "";


            if (ModelState.IsValid)
            {
                try
                {
                    listaTipoMonedas = db.UDP_RRHH_tbTipoMonedas_Insert(tbTipoMonedas.tmon_Descripcion,
                                                                    tbTipoMonedas.tmon_UsuarioCrea,
                                                                    tbTipoMonedas.tmon_FechaCrea);
                    foreach (UDP_RRHH_tbTipoMonedas_Insert_Result Resultado in listaTipoMonedas)
                    {
                        MensajeError = Resultado.MensajeError;
                    }

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
                response = "Bien";
            }

            else
            {
                response = "Error";
            }

            ViewBag.empr_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoMonedas.tmon_UsuarioCrea);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // GET: TipoMonedas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoMonedas tbTipoMonedas = db.tbTipoMonedas.Find(id);
            if (tbTipoMonedas == null)
            {
                return HttpNotFound();
            }
            ViewBag.tmon_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoMonedas.tmon_UsuarioCrea);
            ViewBag.tmon_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoMonedas.tmon_UsuarioModifica);
            return View(tbTipoMonedas);
        }

        // POST: TipoMonedas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tmon_Id,tmon_Descripcion,tmon_Estado,tmon_RazonInactivo,tmon_UsuarioCrea,tmon_FechaCrea,tmon_UsuarioModifica,tmon_FechaModifica")] tbTipoMonedas tbTipoMonedas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTipoMonedas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tmon_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoMonedas.tmon_UsuarioCrea);
            ViewBag.tmon_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoMonedas.tmon_UsuarioModifica);
            return View(tbTipoMonedas);
        }

        // GET: TipoMonedas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoMonedas tbTipoMonedas = db.tbTipoMonedas.Find(id);
            if (tbTipoMonedas == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoMonedas);
        }

        // POST: TipoMonedas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoMonedas tbTipoMonedas = db.tbTipoMonedas.Find(id);
            db.tbTipoMonedas.Remove(tbTipoMonedas);
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
