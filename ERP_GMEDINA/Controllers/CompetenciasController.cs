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
    public class CompetenciasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Competencias
        public ActionResult Index()
        {
            var tbCompetencias = db.tbCompetencias.Where(t => t.comp_Estado == true);
            return View(tbCompetencias.ToList());
        }

        // GET: Competencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCompetencias tbCompetencias = db.tbCompetencias.Find(id);
            if (tbCompetencias == null)
            {
                return HttpNotFound();
            }
            return View(tbCompetencias);
        }

        // GET: Competencias/Create
        public ActionResult Create()
        {
            ViewBag.comp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.comp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: Competencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "comp_Id,comp_Descripcion,comp_Estado,comp_RazonInactivo,comp_UsuarioCrea,comp_FechaCrea,comp_UsuarioModifica,comp_FechaModifica")] tbCompetencias tbCompetencias)
        {
            tbCompetencias.comp_FechaCrea = DateTime.Now;
            tbCompetencias.comp_UsuarioCrea = 1;
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> listCompetencias = null;
                    string MensajeError = "";
                    listCompetencias = db.UDP_RRHH_tbCompetencias_Insert(
                                                                            tbCompetencias.comp_Descripcion,
                                                                            tbCompetencias.comp_UsuarioCrea,
                                                                            tbCompetencias.comp_FechaCrea);
                    foreach (UDP_RRHH_tbCompetencias_Insert_Result com in listCompetencias)
                    {
                        MensajeError = com.MensajeError;
                    }
                    if (!string.IsNullOrEmpty(MensajeError))
                    {
                        if (MensajeError.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "1.hubo un problema");
                            return View(tbCompetencias);
                        }
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    ModelState.AddModelError("", "2. no se pudo insertar el registro");
                    return View(tbCompetencias);
                }
              
            }

            //ViewBag.comp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCompetencias.comp_UsuarioCrea);
            //ViewBag.comp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCompetencias.comp_UsuarioModifica);
            return View(tbCompetencias);
        }

        // GET: Competencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCompetencias tbCompetencias = db.tbCompetencias.Find(id);
            if (tbCompetencias == null)
            {
                return HttpNotFound();
            }
            ViewBag.comp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCompetencias.comp_UsuarioCrea);
            ViewBag.comp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCompetencias.comp_UsuarioModifica);
            return View(tbCompetencias);
        }

        // POST: Competencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "comp_Id,comp_Descripcion,comp_Estado,comp_RazonInactivo,comp_UsuarioCrea,comp_FechaCrea,comp_UsuarioModifica,comp_FechaModifica")] tbCompetencias tbCompetencias)
        {
            tbCompetencias.comp_FechaModifica = DateTime.Now;
            tbCompetencias.comp_UsuarioModifica = 2;

            if (ModelState.IsValid)
            {
                try { 
                        IEnumerable<object> listCompetencias = null;
                        string MensajeError = "";
                        listCompetencias = db.UDP_RRHH_tbCompetencias_Update(
                                                                                tbCompetencias.comp_Id,
                                                                                tbCompetencias.comp_Descripcion,
                                                                                tbCompetencias.comp_UsuarioModifica,
                                                                                tbCompetencias.comp_FechaModifica);
                foreach (UDP_RRHH_tbCompetencias_Update_Result Com in listCompetencias)
                {
                    MensajeError = Com.MensajeError;
                }
                if (!string.IsNullOrEmpty(MensajeError))
                {
                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "1. hubo un error");
                        return View(tbCompetencias);
                    }
                }
                return RedirectToAction("Index");
            }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    ModelState.AddModelError("", "2. Nose pudo actualizar");
                    return View(tbCompetencias);
                }
            }
            //ViewBag.comp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCompetencias.comp_UsuarioCrea);
            //ViewBag.comp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCompetencias.comp_UsuarioModifica);
            return View(tbCompetencias);
        }

        // GET: Competencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCompetencias tbCompetencias = db.tbCompetencias.Find(id);
            if (tbCompetencias == null)
            {
                return HttpNotFound();
            }
            return View(tbCompetencias);
        }

        // POST: Competencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCompetencias tbCompetencias = db.tbCompetencias.Find(id);
            db.tbCompetencias.Remove(tbCompetencias);
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
