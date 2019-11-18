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
    public class CargosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Cargos
        public ActionResult Index()
        {
            var tbCargos = db.tbCargos.Where(t => t.car_Estado==true);
            return View(tbCargos.ToList());
        }

        // GET: Cargos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCargos tbCargos = db.tbCargos.Find(id);
            if (tbCargos == null)
            {
                return HttpNotFound();
            }
            return View(tbCargos);
        }

        // GET: Cargos/Create
        public ActionResult Create()
        {
            ViewBag.car_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.car_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: Cargos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "car_Id,car_Descripcion,car_Estado,car_RazonInactivo,car_UsuarioCrea,car_FechaCrea,car_UsuarioModifica,car_FechaModifica")] tbCargos tbCargos)
        {
            tbCargos.car_UsuarioCrea = 1;
            tbCargos.car_FechaCrea = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> listcargos = null;
                    string MensajeError = "";
                    listcargos = db.UDP_RRHH_tbCargos_Insert(tbCargos.car_Descripcion,
                                                           tbCargos.car_UsuarioCrea,
                                                           tbCargos.car_FechaCrea);
                    foreach (UDP_RRHH_tbCargos_Insert_Result car in listcargos)
                    {
                        MensajeError = car.MensajeError;
                    }
                    if (!string.IsNullOrEmpty(MensajeError))
                    {
                        if (MensajeError.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "1. No se pudo insertar el registro");
                            return View(tbCargos);
                        }
                    }
               
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ex.Message.ToString();
                    ModelState.AddModelError("", "2. No se pudo insertar el registro");
                    return View(tbCargos);
                }
            }

            //ViewBag.car_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCargos.car_UsuarioCrea);
            //ViewBag.car_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCargos.car_UsuarioModifica);
            return View(tbCargos);
        }

        // GET: Cargos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCargos tbCargos = db.tbCargos.Find(id);
            if (tbCargos == null)
            {
                return HttpNotFound();
            }
            ViewBag.car_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCargos.car_UsuarioCrea);
            ViewBag.car_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCargos.car_UsuarioModifica);
            return View(tbCargos);
        }

        // POST: Cargos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "car_Id,car_Descripcion,car_Estado,car_RazonInactivo,car_UsuarioCrea,car_FechaCrea,car_UsuarioModifica,car_FechaModifica")] tbCargos tbCargos)
        {
            tbCargos.car_UsuarioModifica = 2;
            tbCargos.car_FechaModifica = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> listcargos = null;
                    string MensajeError = "";
                    listcargos = db.UDP_RRHH_tbCargos_Update(tbCargos.car_Id,
                                                             tbCargos.car_Descripcion,
                                                             tbCargos.car_UsuarioModifica,
                                                             tbCargos.car_FechaModifica);

                    foreach(UDP_RRHH_tbCargos_Update_Result car in listcargos)
                    {
                        MensajeError = car.MensajeError;
                    }
                    if (!string.IsNullOrEmpty(MensajeError))
                    {
                        if (MensajeError.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "1. No se pudo editar el registro");
                            return View(tbCargos);
                        }
                    }
                    return RedirectToAction("Index");

                }
                catch(Exception ex)
                {
                    ex.Message.ToString();
                    ModelState.AddModelError("", "2. No se pudo insertar el registro");
                    return View(tbCargos);
                }

                //db.Entry(tbCargos).State = EntityState.Modified;
                //db.SaveChanges();
                
            }
            //ViewBag.car_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCargos.car_UsuarioCrea);
            //ViewBag.car_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCargos.car_UsuarioModifica);
            return View(tbCargos);
        }

        // GET: Cargos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCargos tbCargos = db.tbCargos.Find(id);
            if (tbCargos == null)
            {
                return HttpNotFound();
            }
            return View(tbCargos);
        }

        // POST: Cargos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCargos tbCargos = db.tbCargos.Find(id);
            db.tbCargos.Remove(tbCargos);
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
