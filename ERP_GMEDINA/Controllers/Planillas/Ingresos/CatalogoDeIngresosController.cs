using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Data.Entity.Core.Objects;
using ERP_GMEDINA.Attribute;
using ERP_GMEDINA.Helpers;

namespace ERP_GMEDINA.Controllers
{
    public class CatalogoDeIngresosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        Models.Helpers Function = new Models.Helpers();

        #region Index Catalogo de Ingresos
        // GET: tbCatalogoDeIngresos
        [SessionManager("CatalogoDeIngresos/Index")]
        public ActionResult Index()
        {
            var tbCatalogoDeIngresos = db.tbCatalogoDeIngresos.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            //.OrderByDescending(x => x.cin_IdIngreso);
            //.Where(x => x.cin_Activo == true);
            return View(tbCatalogoDeIngresos.ToList());
        }

        public ActionResult GetData()
        {
            var tbCatalogoDeIngresos1 = db.tbCatalogoDeIngresos
                        .Select(c => new
                        {
                            cin_IdIngreso = c.cin_IdIngreso,
                            cin_DescripcionIngreso = c.cin_DescripcionIngreso,
                            cin_Activo = c.cin_Activo,
                            cin_UsuarioCrea = c.cin_UsuarioCrea,
                            cin_FechaCrea = c.cin_FechaCrea,
                            cin_TipoIngreso = c.cin_TipoIngreso,
                            cin_UsuarioModifica = c.cin_UsuarioModifica,
                            cin_FechaModifica = c.cin_FechaModifica
                        })
                        .ToList();
            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = tbCatalogoDeIngresos1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region Crear Catalogo de Ingresos
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("CatalogoDeIngresos/Create")]
        public ActionResult Create([Bind(Include = "cin_IdIngreso,cin_DescripcionIngreso,cin_UsuarioCrea,cin_FechaCrea,cin_TipoIngreso,cin_UsuarioModifica,cin_FechaModifica,cin_Activo")] tbCatalogoDeIngresos tbCatalogoDeIngresos)
        {
            #region declaracion de variables
            //Auditoria
            tbCatalogoDeIngresos.cin_UsuarioCrea = (Session["UserLogin"] as int?) ?? 1;
            tbCatalogoDeIngresos.cin_FechaCrea = General.DateTimeNow;

            string response = String.Empty;
            IEnumerable<object> listCatalogoDeIngresos = null;
            string MensajeError = "";
            #endregion

            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listCatalogoDeIngresos = db.UDP_Plani_tbCatalogoDeIngresos_Insert(tbCatalogoDeIngresos.cin_DescripcionIngreso,
                                                                                      tbCatalogoDeIngresos.cin_TipoIngreso,
                                                                                      tbCatalogoDeIngresos.cin_UsuarioCrea,
                                                                                      tbCatalogoDeIngresos.cin_FechaCrea);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbCatalogoDeIngresos_Insert_Result Resultado in listCatalogoDeIngresos)
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

            ViewBag.cin_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCatalogoDeIngresos.cin_UsuarioCrea);
            ViewBag.cin_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCatalogoDeIngresos.cin_UsuarioModifica);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Editar Catalogo de Ingresos
        // GET: CatalogoDeIngresos/Edit/5
        [SessionManager("CatalogoDeIngresos/Edit")]
        public JsonResult Edit(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbCatalogoDeIngresos tbCatalogoDeIngresosJSON = db.tbCatalogoDeIngresos.Find(ID);
            return Json(tbCatalogoDeIngresosJSON, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SessionManager("CatalogoDeIngresos/Edit")]
        public ActionResult Edit(int id, string cin_DescripcionIngreso, int cin_TipoIngreso)
        {
            tbCatalogoDeIngresos tbCatalogoDeIngresos = new Models.tbCatalogoDeIngresos { cin_DescripcionIngreso = cin_DescripcionIngreso, cin_IdIngreso = id, cin_TipoIngreso = cin_TipoIngreso };
            #region declaracion de variables
            //LLENAR DATA DE AUDITORIA
            tbCatalogoDeIngresos.cin_UsuarioModifica = Session["UserLogin"] as int?;
            tbCatalogoDeIngresos.cin_FechaModifica = General.DateTimeNow;
            string response = String.Empty;
            IEnumerable<object> listCatalogoDeIngresos = null;
            string MensajeError = "";
            #endregion

            try
            {
                //EJECUTAR PROCEDIMIENTO ALMACENADO
                listCatalogoDeIngresos = db.UDP_Plani_tbCatalogoDeIngresos_Update(tbCatalogoDeIngresos.cin_IdIngreso,
                                                                                  tbCatalogoDeIngresos.cin_DescripcionIngreso,
                                                                                  tbCatalogoDeIngresos.cin_TipoIngreso,
                                                                                  tbCatalogoDeIngresos.cin_UsuarioModifica,
                                                                                  tbCatalogoDeIngresos.cin_FechaModifica);
                //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                foreach (UDP_Plani_tbCatalogoDeIngresos_Update_Result Resultado in listCatalogoDeIngresos)
                    MensajeError = Resultado.MensajeError;



                if (MensajeError.StartsWith("-1"))
                {
                    //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                    response = "error";
                }
            }
            catch (Exception)
            {
                //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                response = "error";
            }
            //RETORNAR MENSAJE AL LADO DEL CLIENTE
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Detalles Catalogo de Ingresos
        [SessionManager("CatalogoDeIngresos/Details")]
        public JsonResult Details(int? ID)
        {
            var tbCatalogoDeIngresosJSON = from tbCatIngreso in db.tbCatalogoDeIngresos
                                           where tbCatIngreso.cin_IdIngreso == ID
                                           orderby tbCatIngreso.cin_FechaCrea descending
                                           select new
                                           {
                                               tbCatIngreso.cin_IdIngreso,
                                               tbCatIngreso.cin_DescripcionIngreso,
                                               tbCatIngreso.cin_Activo,
                                               tbCatIngreso.cin_UsuarioCrea,
                                               tbCatIngreso.cin_TipoIngreso,
                                               UsuCrea = tbCatIngreso.tbUsuario.usu_NombreUsuario,
                                               tbCatIngreso.cin_FechaCrea,
                                               tbCatIngreso.cin_UsuarioModifica,
                                               UsuModifica = tbCatIngreso.tbUsuario1.usu_NombreUsuario,
                                               tbCatIngreso.cin_FechaModifica
                                           };


            db.Configuration.ProxyCreationEnabled = false;
            return Json(tbCatalogoDeIngresosJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Inactivar Catalogo de Ingresos
        [SessionManager("CatalogoDeIngresos/Inactivar")]
        public JsonResult Inactivar(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbCatalogoDeIngresos tbCatalogoDeIngresosJSON = db.tbCatalogoDeIngresos.Find(ID);
            return Json(tbCatalogoDeIngresosJSON, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("CatalogoDeIngresos/Inactivar")]
        public ActionResult Inactivar(int ID)
        {
            string response = String.Empty;
            IEnumerable<object> listCatalogoDeIngresos = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listCatalogoDeIngresos = db.UDP_Plani_tbCatalogoDeIngresos_Inactivar(ID,
                                                                                        Session["UserLogin"] as int?,
                                                                                        General.DateTimeNow
                                                                                        );
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbCatalogoDeIngresos_Inactivar_Result Resultado in listCatalogoDeIngresos)
                        MensajeError = Resultado.MensajeError;


                    //RETORNAR MENSAJE DE CONFIRMACIÓN EN CASO QUE NO HAYA CAIDO EN EL CATCH
                    response = "bien";
                }
                catch (Exception)
                {
                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                    response = "error";
                }
            }
            else
            {
                // SI EL MODELO NO ES CORRECTO, RETORNAR ERROR
                ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                response = "error";
            }
            //RETORNAR MENSAJE AL LADO DEL CLIENTE
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Activar Catalogo de Ingresos
        [SessionManager("CatalogoDeIngresos/Activar")]
        public ActionResult Activar(int? ID)
        {
            string response = String.Empty;
            IEnumerable<object> listCatalogoDeIngresos = null;
            string MensajeError = "";

            if (ID == null)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            //LLENAR DATA DE AUDITORIA
            tbCatalogoDeIngresos tbCatalogoDeIngresos = new tbCatalogoDeIngresos();
            tbCatalogoDeIngresos.cin_IdIngreso = (int)ID;
            tbCatalogoDeIngresos.cin_UsuarioModifica = Session["UserLogin"] as int?;
            tbCatalogoDeIngresos.cin_FechaModifica = General.DateTimeNow;
            try
            {
                //EJECUTAR PROCEDIMIENTO ALMACENADO
                listCatalogoDeIngresos = db.UDP_Plani_tbCatalogoDeIngresos_Activar(tbCatalogoDeIngresos.cin_IdIngreso,
                                                                                   tbCatalogoDeIngresos.cin_UsuarioModifica,
                                                                                   tbCatalogoDeIngresos.cin_FechaModifica);

                //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                foreach (UDP_Plani_tbCatalogoDeIngresos_Activar_Result Resultado in listCatalogoDeIngresos)
                    MensajeError = Resultado.MensajeError;

                if (MensajeError.StartsWith("-1"))
                {
                    //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador");
                    response = "error";
                }

            }
            catch (Exception Ex)
            {
                //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                response = Ex.Message.ToString();
            }
            //RETORNAR MENSAJE AL LADO DEL CLIENTE
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete Catalogo de Ingresos
        // GET: CatalogoDeDeducciones/Delete/5
        [SessionManager("CatalogoDeIngresos/Inactivar")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCatalogoDeIngresos tbCatalogoDeIngresos = db.tbCatalogoDeIngresos.Find(id);
            if (tbCatalogoDeIngresos == null)
            {
                return HttpNotFound();
            }
            return View(tbCatalogoDeIngresos);
        }

        // POST: CatalogoDeDeducciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [SessionManager("CatalogoDeIngresos/Inactivar")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCatalogoDeIngresos tbCatalogoDeIngresos = db.tbCatalogoDeIngresos.Find(id);
            db.tbCatalogoDeIngresos.Remove(tbCatalogoDeIngresos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

    }
}
