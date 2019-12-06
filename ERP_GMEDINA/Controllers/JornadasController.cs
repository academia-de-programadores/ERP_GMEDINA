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
    public class JornadasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Jornadas
        public ActionResult Index()
        {
            //var tbJornadas = db.tbJornadas.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(new List<tbJornadas> { });
        }

        // GET: Jornadas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbJornadas tbJornadas = db.tbJornadas.Find(id);
            if (tbJornadas == null)
            {
                return HttpNotFound();
            }
            return View(tbJornadas);
        }

        // GET: Jornadas/Create
        public ActionResult Create()
        {
            ViewBag.jor_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.jor_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }


        public ActionResult ChildRowData(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            //List<tbHorarios> lista = new List<tbHorarios> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    var lista = db.V_HorariosDetalles.Where(x => x.jor_Id == id)
                        .Select(tabla=>new {hor_HoraInicio=tabla.hor_HoraInicio, hor_HoraFin = tabla.hor_HoraFin, hor_descripcion = tabla.hor_Descripcion }).ToList();
                     return Json(lista, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                }
            }
            return Json("-2", JsonRequestBehavior.AllowGet);
        }

        public ActionResult llenarTabla()
        {
            try
            {
                //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                //posteriormente es destruida.
                using (db = new ERP_GMEDINAEntities())
                {
                    var tbJornadas = db.tbJornadas
                        .Select(
                        t => new
                        {
                            jor_Id = t.jor_Id,
                            jor_Descripcion = t.jor_Descripcion
                        }
                        )
                        .ToList();
                    return Json(tbJornadas, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Jornadas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "jor_Id,jor_Descripcion,jor_Estado,jor_RazonInactivo,jor_UsuarioCrea,jor_FechaCrea,jor_UsuarioModifica,jor_FechaModifica")] tbJornadas tbJornadas)
        {
            if (ModelState.IsValid)
            {
                db.tbJornadas.Add(tbJornadas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.jor_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbJornadas.jor_UsuarioCrea);
            ViewBag.jor_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbJornadas.jor_UsuarioModifica);
            return View(tbJornadas);
        }

        //public ActionResult ChildRowData(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
            
        //    try
        //    {
        //        var Horarios = db.tbHorarios.Where(x => x.jor_Id == id).ToList();
        //        List<tbHorarios> ListHorarios = new List<tbHorarios> { };
        //        foreach (var item in Horarios)
        //        {
        //            ListHorarios.Add(new tbHorarios {
        //                hor_Descripcion = item.hor_Descripcion,
        //                hor_HoraInicio = item.hor_HoraInicio,
        //                hor_HoraFin = item.hor_HoraFin,
        //                hor_CantidadHoras = item.hor_CantidadHoras
        //            });
        //        }

        //        return Json(ListHorarios, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //        return HttpNotFound();
        //    }
        //}

        // GET: Jornadas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbJornadas tbJornadas = db.tbJornadas.Find(id);
            if (tbJornadas == null)
            {
                return HttpNotFound();
            }
            ViewBag.jor_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbJornadas.jor_UsuarioCrea);
            ViewBag.jor_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbJornadas.jor_UsuarioModifica);
            return View(tbJornadas);
        }

        // POST: Jornadas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "jor_Id,jor_Descripcion,jor_Estado,jor_RazonInactivo,jor_UsuarioCrea,jor_FechaCrea,jor_UsuarioModifica,jor_FechaModifica")] tbJornadas tbJornadas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbJornadas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.jor_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbJornadas.jor_UsuarioCrea);
            ViewBag.jor_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbJornadas.jor_UsuarioModifica);
            return View(tbJornadas);
        }

        // GET: Jornadas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbJornadas tbJornadas = db.tbJornadas.Find(id);
            if (tbJornadas == null)
            {
                return HttpNotFound();
            }
            return View(tbJornadas);
        }

        // POST: Jornadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbJornadas tbJornadas = db.tbJornadas.Find(id);
            db.tbJornadas.Remove(tbJornadas);
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
