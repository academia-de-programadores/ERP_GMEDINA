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
    public class TipoPermisosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: CatalogoDeDeducciones editado
        public ActionResult Index()
        {
            var tbTipoPermisos = db.tbTipoPermisos.Where(d => d.tper_Estado == true);
            return View(tbTipoPermisos.ToList());
        }

        // GET: OBTENER LA DATA Y ENVIARLA A LA VISTA EN FORMATO JSON
        public ActionResult GetData()
        {
            //SI SE LLEGA A DAR PROBLEMAS DE "REFERENCIAS CIRCULARES", OBTENER LA DATA DE ESTA FORMA
            //SELECCIONANDO UNO POR UNO LOS CAMPOS QUE NECESITAREMOS
            //DE LO CONTRARIO, HACERLO DE LA FORMA CONVENCIONAL (EJEMPLO: db.tbCatalogoDeDeducciones.ToList(); )
            var tbTipoPermisos1 = db.tbCatalogoDeDeducciones
                        .Select(c => new { tde_Descripcion = c.tbTipoDeduccion.tde_Descripcion, tde_IdTipoDedu = c.tbTipoDeduccion.tde_IdTipoDedu, cde_UsuarioModifica = c.cde_UsuarioModifica, cde_UsuarioCrea = c.cde_UsuarioCrea, cde_PorcentajeEmpresa = c.cde_PorcentajeEmpresa, cde_PorcentajeColaborador = c.cde_PorcentajeColaborador, cde_IdDeducciones = c.cde_IdDeducciones, cde_DescripcionDeduccion = c.cde_DescripcionDeduccion, cde_Activo = c.cde_Activo, cde_FechaCrea = c.cde_FechaCrea, cde_FechaModifica = c.cde_FechaModifica }).Where(c => c.cde_Activo == true)
                        .ToList();
            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = tbTipoPermisos1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



        // POST: CatalogoDeDeducciones/Create REALIZAR LA INSERCIÓN
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "tper_Descripcion,tper_Estado,tper_RazonInactivo,tper_UsuarioCrea,tper_FechaCrea,tper_UsuarioModifica,tper_FechaModifica")] tbTipoPermisos tbTipoPermisos)
        {
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbTipoPermisos.tper_UsuarioCrea = 1;
            tbTipoPermisos.tper_FechaCrea = DateTime.Now;
            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
            string response = String.Empty;
            IEnumerable<object> listTipoPermisos = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //[tper_Descripcion],[tper_UsuarioCrea],[tper_FechaCrea]
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listTipoPermisos = db.UDP_RRHH_tbTipoPermisos_Insert( tbTipoPermisos.tper_Descripcion,
                                                                        tbTipoPermisos.tper_UsuarioCrea,
                                                                        tbTipoPermisos.tper_FechaCrea   );
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_RRHH_tbTipoPermisos_Insert_Result Resultado in listTipoPermisos)
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

            //ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion", tbCatalogoDeDeducciones.tde_IdTipoDedu);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        // GET: CatalogoDeDeducciones/Edit/5
        public JsonResult Edit(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false; /*ESTO*/
            tbTipoPermisos tbTipoPermisosJSON = db.tbTipoPermisos.Find(ID);
            return Json(tbTipoPermisosJSON, JsonRequestBehavior.AllowGet);
        }

        // POST: CatalogoDeDeducciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "cde_IdDeducciones,cde_DescripcionDeduccion,tde_IdTipoDedu,cde_PorcentajeColaborador,cde_PorcentajeEmpresa,cde_UsuarioCrea,cde_FechaCrea")]*/ tbTipoPermisos tbTipoPermisos)
        {
            //DATA DE AUDIOTIRIA DE CREACIÓN, PUESTA UNICAMENTE PARA QUE NO CAIGA EN EL CATCH
            //EN EL PROCEDIMIENTO ALMACENADO, ESTOS DOS CAMPOS NO SE DEBEN MODIFICAR
            
            
            tbTipoPermisos.tper_UsuarioCrea = 1;
            tbTipoPermisos.tper_FechaCrea = DateTime.Now;


            //LLENAR DATA DE AUDITORIA
            tbTipoPermisos.tper_UsuarioModifica = 1;
            tbTipoPermisos.tper_FechaModifica = DateTime.Now;
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            IEnumerable<object> ListTipoPermisos = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    ListTipoPermisos = db.UDP_RRHH_tbTipoPermisos_Update(   tbTipoPermisos.tper_Id,
                                                                            tbTipoPermisos.tper_Descripcion,
                                                                            tbTipoPermisos.tper_UsuarioModifica,
                                                                            tbTipoPermisos.tper_FechaModifica);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_RRHH_tbTipoPermisos_Update_Result Resultado in ListTipoPermisos)
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
                // SI EL MODELO NO ES CORRECTO, RETORNAR ERROR
                ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                response = "error";
            }
            /*aqui*///ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion", tbCatalogoDeDeducciones.tde_IdTipoDedu);

            //RETORNAR MENSAJE AL LADO DEL CLIENTE
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        //FUNCIÓN: OBETENER LA DATA PARA LLENAR LOS DROPDOWNLIST DE EDICIÓN Y CREACIÓN
        public JsonResult EditGetDDL() /*LINQ*/
        {
            //OBTENER LA DATA QUE NECESITAMOS, HACIENDOLO DE ESTA FORMA SE EVITA LA EXCEPCION POR "REFERENCIAS CIRCULARES"
            var DDL =
            from TipoPer in db.tbTipoPermisos
            //join CatDeduc in db.tbCatalogoDeDeducciones on TipoDedu.tde_IdTipoDedu equals CatDeduc.tde_IdTipoDedu into prodGroup
            select new { Id = TipoPer.tper_Id, Descripcion = TipoPer.tper_Descripcion};
            //RETORNAR LA DATA EN FORMATO JSON AL CLIENTE 
            return Json(DDL, JsonRequestBehavior.AllowGet);
        }




        public JsonResult Details(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbTipoPermisos tbTipoPermisosJSON = db.tbTipoPermisos.Find(ID);
            return Json(tbTipoPermisosJSON, JsonRequestBehavior.AllowGet);
        }

        // GET: CatalogoDeDeducciones/Details/5
        //public ActionResult Details2(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tbCatalogoDeDeducciones tbCatalogoDeDeducciones = db.tbCatalogoDeDeducciones.Find(id);
        //    if (tbCatalogoDeDeducciones == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tbCatalogoDeDeducciones);
        //}

        // GET: CatalogoDeDeducciones/Create
        public ActionResult Create()
        {
            ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoPermisos, "tper_Id", "tper_Descripcion");
            return View();
        }

        public JsonResult Inactivar(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbTipoPermisos tbTipoPermisosJSON = db.tbTipoPermisos.Find(ID);
            return Json(tbTipoPermisosJSON, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inactivar([Bind(Include = "tper_Id,tper_RazonInactivo,tper_UsuarioModifica,tper_FechaModifica")] tbTipoPermisos tbTipoPermisos)
        {
            //DATA DE AUDIOTIRIA DE CREACIÓN, PUESTA UNICAMENTE PARA QUE NO CAIGA EN EL CATCH
            //EN EL PROCEDIMIENTO ALMACENADO, ESTOS DOS CAMPOS NO SE DEBEN MODIFICAR
            //tbCatalogoDeDeducciones.cde_UsuarioCrea = 1;
            //tbCatalogoDeDeducciones.cde_FechaCrea = DateTime.Now;


            //LLENAR DATA DE AUDITORIA
            tbTipoPermisos.tper_UsuarioModifica = 1;
            tbTipoPermisos.tper_FechaModifica = DateTime.Now;
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            IEnumerable<object> listTipoPermisos = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listTipoPermisos = db.UDP_RRHH_tbTipoPermisos_Delete(   tbCatalogoDeDeducciones.cde_IdDeducciones,
                                                                            tbCatalogoDeDeducciones.tper_RazonInactivo,
                                                                            tbCatalogoDeDeducciones.cde_UsuarioModifica,
                                                                            tbCatalogoDeDeducciones.cde_FechaModifica);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_RRHH_tbTipoPermisos_Delete_Result Resultado in listTipoPermisos)
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
                //SI LA EJECUCIÓN LLEGA A ESTE PUNTO SIGNIFICA QUE NO OCURRIÓ NINGÚN ERROR Y EL PROCESO FUE EXITOSO
                //IGUALAMOS LA VARIABLE "RESPONSE" A "BIEN" PARA VALIDARLO EN EL CLIENTE
                response = "bien";
            }
            else
            {
                // SI EL MODELO NO ES CORRECTO, RETORNAR ERROR
                ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador.");
                response = "error";
            }
            //ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion", tbCatalogoDeDeducciones.tde_IdTipoDedu);

            //RETORNAR MENSAJE AL LADO DEL CLIENTE
            return Json(response, JsonRequestBehavior.AllowGet);
        }


























        // GET: CatalogoDeDeducciones/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tbCatalogoDeDeducciones tbCatalogoDeDeducciones = db.tbCatalogoDeDeducciones.Find(id);
        //    if (tbCatalogoDeDeducciones == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tbCatalogoDeDeducciones);
        //}

        //// POST: CatalogoDeDeducciones/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tbCatalogoDeDeducciones tbCatalogoDeDeducciones = db.tbCatalogoDeDeducciones.Find(id);
        //    db.tbCatalogoDeDeducciones.Remove(tbCatalogoDeDeducciones);
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
