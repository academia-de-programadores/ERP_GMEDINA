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
    public class AuxilioDeCesantiasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: tbAuxilioDeCesantias
        public ActionResult Index()
        {
            var tbAuxilioDeCesantias = db.tbAuxilioDeCesantias.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Where(x => x.aces_Activo == true);
            return View(tbAuxilioDeCesantias.ToList());
        }

        public ActionResult GetData()
        {
            var tbAuxilioCesantia1 = db.tbAuxilioDeCesantias
                        .Select(c => new {
                            aces_IdAuxilioCesantia = c.aces_IdAuxilioCesantia,
                            aces_RangoInicioMeses = c.aces_RangoInicioMeses,
                            aces_RangoFinMeses = c.aces_RangoFinMeses,
                            aces_DiasAuxilioCesantia = c.aces_DiasAuxilioCesantia,
                            aces_UsuarioCrea = c.aces_UsuarioCrea,
                            aces_FechaCrea = c.aces_FechaCrea,
                            aces_UsuarioModifica = c.aces_UsuarioModifica,
                            aces_FechaModifica = c.aces_FechaModifica,
                            aces_Activo = c.aces_Activo
                        })
                                           .OrderByDescending(x => x.aces_FechaCrea)
                                           .Where(x => x.aces_Activo == true).ToList();
            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = tbAuxilioCesantia1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult Details(int? ID)
        {
            var tbAuxCesanJSON = from tbAuxilioDeCesantias in db.tbAuxilioDeCesantias
                                           where tbAuxilioDeCesantias.aces_Activo == true && tbAuxilioDeCesantias.aces_IdAuxilioCesantia == ID
                                           orderby tbAuxilioDeCesantias.aces_FechaCrea descending
                                           select new
                                           {
                                               tbAuxilioDeCesantias.aces_IdAuxilioCesantia,
                                               tbAuxilioDeCesantias.aces_RangoInicioMeses,
                                               tbAuxilioDeCesantias.aces_RangoFinMeses,
                                               tbAuxilioDeCesantias.aces_DiasAuxilioCesantia,
                                               UsuCrea = tbAuxilioDeCesantias.tbUsuario.usu_NombreUsuario,
                                               tbAuxilioDeCesantias.aces_FechaCrea,
                                               tbAuxilioDeCesantias.aces_UsuarioModifica,
                                               UsuModifica = tbAuxilioDeCesantias.tbUsuario1.usu_NombreUsuario,
                                               tbAuxilioDeCesantias.aces_FechaModifica
                                           };


            db.Configuration.ProxyCreationEnabled = false;
            return Json(tbAuxCesanJSON, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(tbAuxilioDeCesantias tbAuxilioDeCesantias)
        {
            #region declaracion de variables
            //Auditoria
            tbAuxilioDeCesantias.aces_UsuarioCrea = 1;
            tbAuxilioDeCesantias.aces_FechaCrea = DateTime.Now;
            tbAuxilioDeCesantias.aces_Activo = true;

            string response = String.Empty;
            IEnumerable<object> listAuxCesantias = null;
            string MensajeError = "";
            #endregion

            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listAuxCesantias = db.UDP_Plani_tbAuxilioDeCesantias_Insert(tbAuxilioDeCesantias.aces_RangoInicioMeses,
                                                                                tbAuxilioDeCesantias.aces_RangoFinMeses,
                                                                                tbAuxilioDeCesantias.aces_DiasAuxilioCesantia,
                                                                                         tbAuxilioDeCesantias.aces_UsuarioCrea,
                                                                                         tbAuxilioDeCesantias.aces_FechaCrea,tbAuxilioDeCesantias.aces_Activo);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbAuxilioDeCesantias_Insert_Result Resultado in listAuxCesantias)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        response = "error";
                    }

                }
                catch (Exception Ex)
                {
                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    response = Ex.Message.ToString();
                }
                response = "bien";
            }
            else
            {
                //SI EL MODELO NO ES VÁLIDO, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                response = "error";
            }

            ViewBag.aces_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbAuxilioDeCesantias.aces_UsuarioCrea);
            ViewBag.aces_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbAuxilioDeCesantias.aces_UsuarioModifica);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

      

        // GET: tbAuxilioDeCesantias/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tbAuxilioDeCesantias tbAuxilioDeCesantias = db.tbAuxilioDeCesantias.Find(id);
        //    if (tbAuxilioDeCesantias == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.aces_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbAuxilioDeCesantias.aces_UsuarioCrea);
        //    ViewBag.aces_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbAuxilioDeCesantias.aces_UsuarioModifica);
        //    return View(tbAuxilioDeCesantias);
        //}

        // POST: tbAuxilioDeCesantias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "aces_IdAuxilioCesantia,aces_RangoInicioMeses,aces_RangoFinMeses,aces_DiasAuxilioCesantia,aces_UsuarioCrea,aces_FechaCrea,aces_UsuarioModifica,aces_FechaModifica,aces_Activo")] tbAuxilioDeCesantias tbAuxilioDeCesantias)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tbAuxilioDeCesantias).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.aces_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbAuxilioDeCesantias.aces_UsuarioCrea);
        //    ViewBag.aces_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbAuxilioDeCesantias.aces_UsuarioModifica);
        //    return View(tbAuxilioDeCesantias);
        //}

        // GET: tbAuxilioDeCesantias/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tbAuxilioDeCesantias tbAuxilioDeCesantias = db.tbAuxilioDeCesantias.Find(id);
        //    if (tbAuxilioDeCesantias == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tbAuxilioDeCesantias);
        //}

        // POST: tbAuxilioDeCesantias/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tbAuxilioDeCesantias tbAuxilioDeCesantias = db.tbAuxilioDeCesantias.Find(id);
        //    db.tbAuxilioDeCesantias.Remove(tbAuxilioDeCesantias);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
