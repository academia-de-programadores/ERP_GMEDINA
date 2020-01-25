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
        private ERP_GMEDINAEntities db = null;

        // GET: TipoMonedas
        public ActionResult Index()
        {
            if (Session["Admin"] == null && Session["Usuario"] == null)
            {
                Response.Redirect("~/Inicio/index");
                return null;
            }
            tbTipoMonedas tbTipoMonedas = new tbTipoMonedas { };
            return View(tbTipoMonedas);
        }

        [HttpPost]
        public JsonResult llenarTabla()
        {
            try
            {
                db = new ERP_GMEDINAEntities();
                var tbTipoMonedas = db.tbTipoMonedas
                    .Select(
                    t => new
                    {
                        tmon_Id = t.tmon_Id,
                        tmon_Estado = t.tmon_Estado,
                        tmon_Descripcion = t.tmon_Descripcion
                    }
                    )
                    .ToList();
                return Json(tbTipoMonedas, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        //{
        //    List<tbTipoMonedas> tbTipoMonedas =
        //        new List<Models.tbTipoMonedas> { };
        //    foreach (tbTipoMonedas x in db.tbTipoMonedas.ToList().Where(x => x.tmon_Estado == true))
        //    {
        //        tbTipoMonedas.Add(new tbTipoMonedas
        //        {
        //            tmon_Id = x.tmon_Id,
        //            tmon_Descripcion = x.tmon_Descripcion
        //        });
        //    }
        //    return Json(tbTipoMonedas, JsonRequestBehavior.AllowGet);
        //}

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
        public JsonResult Create(tbTipoMonedas tbTipoMonedas)
        {
            string msj = "...";
            if (tbTipoMonedas.tmon_Descripcion != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbTipoMonedas_Insert(tbTipoMonedas.tmon_Descripcion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbTipoMonedas_Insert_Result item in list)
                    {
                        msj = item.MensajeError;
                        //return Json(msj, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                    //return Json(msj,JsonRequestBehavior.AllowGet);
                }
            }

            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }
        // GET: TipoMonedas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbTipoMonedas tbTipoMonedas = null;
            try
            {
                db = new ERP_GMEDINAEntities();
                tbTipoMonedas = db.tbTipoMonedas.Find(id);
                if (tbTipoMonedas == null)
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
            var TipoMonedas = new tbTipoMonedas
            {
                tmon_Id = tbTipoMonedas.tmon_Id,
                tmon_Descripcion = tbTipoMonedas.tmon_Descripcion,
                tmon_Estado = tbTipoMonedas.tmon_Estado,
                tmon_RazonInactivo = tbTipoMonedas.tmon_RazonInactivo,
                tmon_UsuarioCrea = tbTipoMonedas.tmon_UsuarioCrea,
                tmon_FechaCrea = tbTipoMonedas.tmon_FechaCrea,
                tmon_UsuarioModifica = tbTipoMonedas.tmon_UsuarioModifica,
                tmon_FechaModifica = tbTipoMonedas.tmon_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoMonedas.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoMonedas.tbUsuario1).usu_NombreUsuario }
            };
            return Json(TipoMonedas, JsonRequestBehavior.AllowGet);
        }
        // POST: TipoMonedas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Edit(tbTipoMonedas tbTipoMonedas)
        {
            string msj = "";
            if (tbTipoMonedas.tmon_Id != 0 && tbTipoMonedas.tmon_Descripcion != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbTipoMoneda_Update(id, tbTipoMonedas.tmon_Descripcion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbTipoMoneda_Update_Result item in list)
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
        // GET: TipoMonedas/Delete/5
        public ActionResult Delete(tbTipoMonedas tbTipoMonedas)
        {
            string msj = "...";
            string RazonInactivo = "Se ha Inhabilitado este Registro";

            if (tbTipoMonedas.tmon_Id != 0)
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbTipoMonedas_Delete(id, RazonInactivo, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbTipoMonedas_Delete_Result item in list)
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
        //// POST: TipoMonedas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tbTipoMonedas tbTipoMonedas = db.tbTipoMonedas.Find(id);
        //    db.tbTipoMonedas.Remove(tbTipoMonedas);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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


        [HttpPost]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var list = db.UDP_RRHH_tbTipoMonedas_Restore(id, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbTipoMonedas_Restore_Result item in list)
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

    }
}

//// GET: TipoMonedas/Details/5
//public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            tbTipoMonedas tbTipoMonedas = db.tbTipoMonedas.Find(id);
//            if (tbTipoMonedas == null)
//            {
//                return HttpNotFound();
//            }
//            return View(tbTipoMonedas);
//        }

//        // GET: TipoMonedas/Create
//        public ActionResult Create()
//        {
//            ViewBag.tmon_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
//            ViewBag.tmon_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
//            return View();
//        }

//        // POST: TipoMonedas/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "tmon_Id,tmon_Descripcion,tmon_Estado,tmon_RazonInactivo,tmon_UsuarioCrea,tmon_FechaCrea,tmon_UsuarioModifica,tmon_FechaModifica")] tbTipoMonedas tbTipoMonedas)
//        {
//            if (ModelState.IsValid)
//            {
//                db.tbTipoMonedas.Add(tbTipoMonedas);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.tmon_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoMonedas.tmon_UsuarioCrea);
//            ViewBag.tmon_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoMonedas.tmon_UsuarioModifica);
//            return View(tbTipoMonedas);
//        }

//        // GET: TipoMonedas/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            tbTipoMonedas tbTipoMonedas = db.tbTipoMonedas.Find(id);
//            if (tbTipoMonedas == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.tmon_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoMonedas.tmon_UsuarioCrea);
//            ViewBag.tmon_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoMonedas.tmon_UsuarioModifica);
//            return View(tbTipoMonedas);
//        }

//        // POST: TipoMonedas/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "tmon_Id,tmon_Descripcion,tmon_Estado,tmon_RazonInactivo,tmon_UsuarioCrea,tmon_FechaCrea,tmon_UsuarioModifica,tmon_FechaModifica")] tbTipoMonedas tbTipoMonedas)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(tbTipoMonedas).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.tmon_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoMonedas.tmon_UsuarioCrea);
//            ViewBag.tmon_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoMonedas.tmon_UsuarioModifica);
//            return View(tbTipoMonedas);
//        }

//        // GET: TipoMonedas/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            tbTipoMonedas tbTipoMonedas = db.tbTipoMonedas.Find(id);
//            if (tbTipoMonedas == null)
//            {
//                return HttpNotFound();
//            }
//            return View(tbTipoMonedas);
//        }

//        // POST: TipoMonedas/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            tbTipoMonedas tbTipoMonedas = db.tbTipoMonedas.Find(id);
//            db.tbTipoMonedas.Remove(tbTipoMonedas);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}

    
    //Nota: Los parametros y nombres de funciones, dependen de la tabla trabajada
  

