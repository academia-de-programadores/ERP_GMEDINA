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
    public class NacionalidadesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Nacionalidades

        public ActionResult Index()
        {
            var tbNacionalidades = db.tbNacionalidades.Where(t => t.nac_Estado == true);
            return View(tbNacionalidades.ToList());
        }

        // GET: OBTENER LA DATA Y ENVIARLA A LA VISTA EN FORMATO JSON
        public ActionResult GetData()
        {
            //SI SE LLEGA A DAR PROBLEMAS DE "REFERENCIAS CIRCULARES", OBTENER LA DATA DE ESTA FORMA
            //SELECCIONANDO UNO POR UNO LOS CAMPOS QUE NECESITAREMOS
            //DE LO CONTRARIO, HACERLO DE LA FORMA CONVENCIONAL (EJEMPLO: db.tbnacetencias.ToList(); )
            var tbNacionalidades1 = db.tbNacionalidades
                        .Select(c => new {
                            nac_Id = c.nac_Id,
                            nac_Descripcion = c.nac_Descripcion,
                            nac_Estado = c.nac_Estado,
                            nac_RazonInactivo = c.nac_RazonInactivo,
                            nac_UsuarioModifica = c.nac_UsuarioModifica,
                            nac_UsuarioCrea = c.nac_UsuarioCrea,
                            nac_FechaCrea = c.nac_FechaCrea,
                            nac_FechaModifica = c.nac_FechaModifica
                        }).Where(c => c.nac_Estado == true)
                        .ToList();
            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = tbNacionalidades1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: Nacionalidades/Details/5
        public JsonResult Details(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbNacionalidades tbJSON = db.tbNacionalidades.Find(ID);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }

        // GET: Nacionalidades/Create
        public ActionResult Create()
        {
            return View();

        }


        //// GET: Nacionalidades/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tbNacionalidades tbNacionalidades = db.tbNacionalidades.Find(id);
        //    if (tbNacionalidades == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tbNacionalidades);
        //}


        // POST: Nacionalidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nac_Id, nac_Descripcion, nac_Estado, nac_RazonInactivo, nac_UsuarioCrea, nac_FechaCrea, nac_UsuarioModifica, nac_FechaModifica")] tbNacionalidades tbNacionalidades)
        {
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbNacionalidades.nac_UsuarioCrea = 1;
            tbNacionalidades.nac_FechaCrea = DateTime.Now;
            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
            string response = String.Empty;
            IEnumerable<object> listNacionalidades = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listNacionalidades = db.UDP_RRHH_tbNacionalidades_Insert(tbNacionalidades.nac_Descripcion,
                                                                                            tbNacionalidades.nac_UsuarioCrea,
                                                                                            tbNacionalidades.nac_FechaCrea);

                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_RRHH_tbNacionalidades_Insert_Result Resultado in listNacionalidades)
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

                //SI LA EJECUCIÓN LLEGA A ESTE PUNTO SIGNIFICA QUE NO OCURRIÓ NINGÚN ERROR Y EL PROCESO FUE EXITOSO
                //IGUALAMOS LA VARIABLE "RESPONSE" A "BIEN" PARA VALIDARLO EN EL CLIENTE
                response = "bien";
            }
            else
            {
                //SI EL MODELO NO ES VÁLIDO, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                response = "error";
            }
            //RETORNAMOS LA VARIABLE RESPONSE AL CLIENTE PARA EVALUARLA
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        // GET: Nacionalidades/Edit/5
        public JsonResult Edit(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbNacionalidades tbJSON = db.tbNacionalidades.Find(ID);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }

        // POST: Nacionalidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nac_Id, nac_Descripcion, nac_Estado, nac_RazonInactivo, nac_UsuarioCrea, nac_FechaCrea, nac_UsuarioModifica, nac_FechaModifica")] tbNacionalidades tbNacionalidades)
        {
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbNacionalidades.nac_UsuarioModifica = 2;
            tbNacionalidades.nac_FechaModifica = DateTime.Now;

            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
            string response = String.Empty;
            IEnumerable<object> listNacionalidades = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listNacionalidades = db.UDP_RRHH_tbNacionalidades_Update(tbNacionalidades.nac_Id,
                                                                                            tbNacionalidades.nac_Descripcion,
                                                                                            tbNacionalidades.nac_UsuarioModifica,
                                                                                            tbNacionalidades.nac_FechaModifica);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_RRHH_tbNacionalidades_Update_Result Resultado in listNacionalidades)
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
                //SI LA EJECUCIÓN LLEGA A ESTE PUNTO SIGNIFICA QUE NO OCURRIÓ NINGÚN ERROR Y EL PROCESO FUE EXITOSO
                //IGUALAMOS LA VARIABLE "RESPONSE" A "BIEN" PARA VALIDARLO EN EL CLIENTE
                response = "bien";
            }
            else
            {
                //SI EL MODELO NO ES VÁLIDO, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                response = "error";
            }
            //RETORNAMOS LA VARIABLE RESPONSE AL CLIENTE PARA EVALUARLA
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        // GET: Nacionalidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbNacionalidades tbNacionalidades = db.tbNacionalidades.Find(id);
            if (tbNacionalidades == null)
            {
                return HttpNotFound();
            }
            return View(tbNacionalidades);
        }

        // POST: Nacionalidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbNacionalidades tbNacionalidades = db.tbNacionalidades.Find(id);
            db.tbNacionalidades.Remove(tbNacionalidades);
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

































        //// GET: Nacionalidades/Create
        //public ActionResult Create()
        //{
        //    ViewBag.nac_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
        //    ViewBag.nac_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
        //    return View();
        //}

        //// POST: Nacionalidades/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "nac_Id,nac_Descripcion,nac_Estado,nac_RazonInactivo,nac_UsuarioCrea,nac_FechaCrea,nac_UsuarioModifica,nac_FechaModifica")] tbNacionalidades tbNacionalidades)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.tbNacionalidades.Add(tbNacionalidades);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.nac_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNacionalidades.nac_UsuarioCrea);
        //    ViewBag.nac_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNacionalidades.nac_UsuarioModifica);
        //    return View(tbNacionalidades);
        //}

        //// GET: Nacionalidades/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tbNacionalidades tbNacionalidades = db.tbNacionalidades.Find(id);
        //    if (tbNacionalidades == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.nac_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNacionalidades.nac_UsuarioCrea);
        //    ViewBag.nac_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNacionalidades.nac_UsuarioModifica);
        //    return View(tbNacionalidades);
        //}

        //// POST: Nacionalidades/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "nac_Id,nac_Descripcion,nac_Estado,nac_RazonInactivo,nac_UsuarioCrea,nac_FechaCrea,nac_UsuarioModifica,nac_FechaModifica")] tbNacionalidades tbNacionalidades)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tbNacionalidades).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.nac_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNacionalidades.nac_UsuarioCrea);
        //    ViewBag.nac_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbNacionalidades.nac_UsuarioModifica);
        //    return View(tbNacionalidades);
        //}

        //// GET: Nacionalidades/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tbNacionalidades tbNacionalidades = db.tbNacionalidades.Find(id);
        //    if (tbNacionalidades == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tbNacionalidades);
        //}

        //// POST: Nacionalidades/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tbNacionalidades tbNacionalidades = db.tbNacionalidades.Find(id);
        //    db.tbNacionalidades.Remove(tbNacionalidades);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
