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
    public class TitulosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Titulos
        public ActionResult Index()
        {
            var tbTitulos = db.tbTitulos.Where(t => t.titu_Estado == true);
            return View(tbTitulos.ToList());
        }





        // GET: Titulos/Details/5
        /*public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTitulos tbTitulos = db.tbTitulos.Find(id);
            if (tbTitulos == null)
            {
                return HttpNotFound();
            }
            return View(tbTitulos);
        }
        */


        // GET: Titulos/Create
    

        // POST: Titulos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "titu_Id,titu_Descripcion,titu_Estado,titu_RazonInactivo,titu_UsuarioCrea,titu_FechaCrea,titu_UsuarioModifica,titu_FechaModifica")] tbTitulos tbtitulos)
        {
            tbtitulos.titu_UsuarioCrea = 1;
            tbtitulos.titu_FechaCrea = DateTime.Now;

            string response = String.Empty;
            IEnumerable<object> listatitulos = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listatitulos = db.UDP_RRHH_tbTitulos_Insert(tbtitulos.titu_Descripcion,
                                                                tbtitulos.titu_UsuarioCrea,
                                                                tbtitulos.titu_FechaCrea
                                                                );
                    foreach (UDP_RRHH_tbTitulos_Insert_Result Resultado in listatitulos)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registro");
                        response = "error";
                    }

                }
                catch (Exception ex)
                {
                    response = ex.Message.ToString();
                }
                response = "bien";

            }
            else
            {
                response = "error";
            }


            ViewBag.titu_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbtitulos.titu_UsuarioCrea);
            ViewBag.titu_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbtitulos.titu_UsuarioModifica);
            return Json(response,JsonRequestBehavior.AllowGet);
        }



        // GET: Titulos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTitulos tbTitulos = db.tbTitulos.Find(id);
            if (tbTitulos == null)
            {
                return HttpNotFound();
            }
            ViewBag.titu_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTitulos.titu_UsuarioCrea);
            ViewBag.titu_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTitulos.titu_UsuarioModifica);
            return View(tbTitulos);
        }



        // POST: Titulos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "titu_Id,titu_Descripcion,titu_Estado,titu_RazonInactivo,titu_UsuarioCrea,titu_FechaCrea,titu_UsuarioModifica,titu_FechaModifica")] tbTitulos tbtitulos)
        {
            tbtitulos.titu_UsuarioCrea = 1;
            tbtitulos.titu_FechaCrea = DateTime.Now;

            tbtitulos.titu_UsuarioModifica = 1;
            tbtitulos.titu_FechaModifica = DateTime.Now;

            string response = String.Empty;
            IEnumerable<object> listatitulos = null;
            string MensajeError = "";

            if (ModelState.IsValid)
            {
                try
                {
                    listatitulos = db.UDP_RRHH_tbTitulos_Update(tbtitulos.titu_Id,
                                                                tbtitulos.titu_Descripcion,
                                                                tbtitulos.titu_UsuarioModifica,
                                                                tbtitulos.titu_FechaModifica
                                                                );
                    foreach (UDP_RRHH_tbTitulos_Update_Result Resultado in listatitulos)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo ingresar el registron,conecte al administrador");
                    }
                }
                catch (Exception ex)
                {
                    response = ex.Message.ToString();
                }
                response = "bien";

            } 
            else
            {
                ModelState.AddModelError("", "no se pudo modificar el registro, conecte con el administrador");
                response = "error";
            }
            ViewBag.titu_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbtitulos.titu_UsuarioCrea);
            ViewBag.titu_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbtitulos.titu_UsuarioModifica);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // GET: Titulos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTitulos tbTitulos = db.tbTitulos.Find(id);
            if (tbTitulos == null)
            {
                return HttpNotFound();
            }
            return View(tbTitulos);
        }

        // POST: Titulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTitulos tbTitulos = db.tbTitulos.Find(id);
            db.tbTitulos.Remove(tbTitulos);
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
