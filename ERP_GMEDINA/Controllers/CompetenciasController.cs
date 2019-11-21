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
            List<tbCompetencias> tbCompetencias = new List<Models.tbCompetencias> { };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            try
            {
                tbCompetencias = db.tbCompetencias.Where(x => x.comp_Estado).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).ToList();
                return View(tbCompetencias);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                tbCompetencias.Add(new tbCompetencias { comp_Id = 0, comp_Descripcion = "fallo la conexion" });
            }
            return View(tbCompetencias);
        }
        [HttpPost]
        public JsonResult llenarTabla()
        {
            List<tbCompetencias> tbCompetencias = new List<Models.tbCompetencias> { };
            foreach (tbCompetencias x in db.tbCompetencias.ToList().Where(x => x.comp_Estado == true))
            {
                tbCompetencias.Add(new tbCompetencias
                {
                    comp_Id = x.comp_Id,
                    comp_Descripcion = x.comp_Descripcion
                });
            }
            return Json(tbCompetencias, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Create(tbCompetencias tbCompetencias)
        {
            string msj = "";
            if (tbCompetencias.comp_Descripcion != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbCompetencias_Insert(
                        tbCompetencias.comp_Descripcion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbCompetencias_Insert_Result item in list)
                    {
                        msj = item.MensajeError + "";
                    }
                }
                catch(Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }





        

        // POST: Competencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "comp_Id, comp_Descripcion, comp_Estado, comp_RazonInactivo, comp_UsuarioCrea, comp_FechaCrea, comp_UsuarioModifica, comp_FechaModifica")] tbCompetencias tbCompetencias)
        {
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbCompetencias.comp_UsuarioCrea = 1;
            tbCompetencias.comp_FechaCrea = DateTime.Now;
            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
            string response = String.Empty;
            IEnumerable<object> listCompetencias = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listCompetencias = db.UDP_RRHH_tbCompetencias_Insert(tbCompetencias.comp_Descripcion,
                                                                                            tbCompetencias.comp_UsuarioCrea,
                                                                                            tbCompetencias.comp_FechaCrea);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_RRHH_tbCompetencias_Insert_Result Resultado in listCompetencias)
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







        // GET: Competencias/Edit/5
        public JsonResult Edit(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbCompetencias tbJSON = db.tbCompetencias.Find(ID);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }

        // POST: Competencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "comp_Id,comp_Descripcion,comp_Estado,comp_RazonInactivo,comp_UsuarioCrea,comp_FechaCrea,comp_UsuarioModifica,comp_FechaModifica")] tbCompetencias tbCompetencias)
        {
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbCompetencias.comp_UsuarioModifica = 2;
            tbCompetencias.comp_FechaModifica = DateTime.Now;
            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
            string response = String.Empty;
            IEnumerable<object> listCompetencias = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listCompetencias = db.UDP_RRHH_tbCompetencias_Update(tbCompetencias.comp_Id,
                                                                                            tbCompetencias.comp_Descripcion,
                                                                                            tbCompetencias.comp_UsuarioModifica,
                                                                                            tbCompetencias.comp_FechaModifica);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_RRHH_tbCompetencias_Update_Result Resultado in listCompetencias)
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
