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
    public class RazonSalidasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: RazonSalidas
        public ActionResult Index()
        {
            var tbRazonSalidas = db.tbRazonSalidas.Where(d => d.rsal_Estado == true);
            return View(tbRazonSalidas.ToList());
        }
        public ActionResult GetData()
        {
            //SI SE LLEGA A DAR PROBLEMAS DE "REFERENCIAS CIRCULARES", OBTENER LA DATA DE ESTA FORMA
            //SELECCIONANDO UNO POR UNO LOS CAMPOS QUE NECESITAREMOS
            //DE LO CONTRARIO, HACERLO DE LA FORMA CONVENCIONAL (EJEMPLO: db.tbCatalogoDeDeducciones.ToList(); )
            var tbRazonSalidas1 = db.tbRazonSalidas
                        .Select(c => new {
                            rsal_Id = c.rsal_Id,
                            rsal_Descripcionn = c.rsal_Descripcion,
                            rsal_Estado = c.rsal_Estado,
                            rsal_RazonInactivo = c.rsal_RazonInactivo,
                            rsal_UsuarioModifica = c.rsal_UsuarioModifica,
                            rsal_UsuarioCrea = c.rsal_UsuarioCrea,                           
                            rsal_FechaCrea = c.rsal_FechaCrea,
                            rsal_FechaModifica = c.rsal_FechaModifica
                        }).Where(c => c.rsal_Estado == true)
                        .ToList();
            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = tbRazonSalidas1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // GET: RazonSalidas/Details/5
        public ActionResult Details(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbRazonSalidas tbJSON = db.tbRazonSalidas.Find(ID);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }

        // GET: RazonSalidas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RazonSalidas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "rsal_Id,rsal_Descripcion,rsal_UsuarioCrea,rsal_FechaCrea")] tbRazonSalidas tbRazonSalidas)
        {
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbRazonSalidas.rsal_UsuarioCrea = 1;
            tbRazonSalidas.rsal_FechaCrea = DateTime.Now;
            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
            string response = String.Empty;
            IEnumerable<object> list = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)

            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO

                    //list = db.UDP_rrhh_tbRazonSalidas_Insert( tbRazonSalidas.rsal_Descripcion,
                    //                                          tbRazonSalidas.rsal_UsuarioCrea,
                    //                                          tbRazonSalidas.rsal_FechaCrea);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                   // foreach (UDP_rrhh_tbRazonSalidas_Insert Resultado in list)
                    //    MensajeError = Resultado.MensajeError;

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

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // GET: RazonSalidas/Edit/5
        public ActionResult Edit(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbRazonSalidas tbJSON = db.tbRazonSalidas.Find(ID);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }

        // POST: RazonSalidas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "rsal_Id,rsal_Descripcion,rsal_UsuarioCrea,rsal_FechaCrea")] tbRazonSalidas tbRazonSalidas)
        { 
        //DATA DE AUDIOTIRIA DE CREACIÓN, PUESTA UNICAMENTE PARA QUE NO CAIGA EN EL CATCH
        //EN EL PROCEDIMIENTO ALMACENADO, ESTOS DOS CAMPOS NO SE DEBEN MODIFICAR
            tbRazonSalidas.rsal_UsuarioCrea = 1;
            tbRazonSalidas.rsal_FechaCrea = DateTime.Now;


            //LLENAR DATA DE AUDITORIA
            tbRazonSalidas.rsal_UsuarioModifica = 1;
            tbRazonSalidas.rsal_FechaModifica = DateTime.Now;
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            IEnumerable<object> list = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    //list = db.UDP_rrhh_tbRazonSalidas_Update(   tbRazonSalidas.rsal_Id,
                    //                                            tbRazonSalidas.rsal_Descripcion,
                    //                                            tbRazonSalidas.rsal_UsuarioModifica,
                    //                                            tbRazonSalidas.rsal_FechaModifica);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    //foreach (UDP_rrhh_tbRazonSalidas_Update Resultado in list)
                    //    MensajeError = Resultado.MensajeError;

                    //if (MensajeError.StartsWith("-1"))
                    //{
                    //    //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    //    ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                    //    response = "error";
                    //}

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
            else {
                // SI EL MODELO NO ES CORRECTO, RETORNAR ERROR
                ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                response = "error";
            }
            
            //RETORNAR MENSAJE AL LADO DEL CLIENTE
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Inactivar(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbRazonSalidas tbJSON = db.tbRazonSalidas.Find(ID);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inactivar([Bind(Include = "rsal_Id,rsal_UsuarioModifica,rsal_FechaModifica")] tbRazonSalidas tbRazonSalidas)
        {
            //DATA DE AUDIOTIRIA DE CREACIÓN, PUESTA UNICAMENTE PARA QUE NO CAIGA EN EL CATCH
            //EN EL PROCEDIMIENTO ALMACENADO, ESTOS DOS CAMPOS NO SE DEBEN MODIFICAR
            //tbCatalogoDeDeducciones.rsal_UsuarioCrea = 1;
            //tbCatalogoDeDeducciones.rsal_FechaCrea = DateTime.Now;


            //LLENAR DATA DE AUDITORIA
            tbRazonSalidas.rsal_UsuarioModifica = 1;
            tbRazonSalidas.rsal_FechaModifica = DateTime.Now;
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            IEnumerable<object> listCatalogoDeDeducciones = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                //try
                //{
                //    //EJECUTAR PROCEDIMIENTO ALMACENADO
                //    list = db.UDP_Plani_tbCatalogoDeDeducciones_Inactivar( tbRazonSalidas.rsal_Id,
                //                                                           tbRazonSalidas.rsal_UsuarioModifica,
                //                                                           tbRazonSalidas.rsal_FechaModifica);
                //    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                //    foreach (UDP_Plani_tbCatalogoDeDeducciones_Inactivar_Result Resultado in listCatalogoDeDeducciones)
                //        MensajeError = Resultado.MensajeError;

                //    if (MensajeError.StartsWith("-1"))
                //    {
                //        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                //        ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador");
                //        response = "error";
                //    }

                //}
                //catch (Exception Ex)
                //{
                //    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                //    response = Ex.Message.ToString();
                //}
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
