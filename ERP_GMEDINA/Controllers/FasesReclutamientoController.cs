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
    public class FasesReclutamientoController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: FasesReclutamiento
        public ActionResult Index()
        {
            var tbFasesReclutamiento = db.tbFasesReclutamiento.Where(d=> d.fare_Estado == true);
            return View(tbFasesReclutamiento.ToList());
        }

        public ActionResult GetData()
        {
            //SI SE LLEGA A DAR PROBLEMAS DE "REFERENCIAS CIRCULARES", OBTENER LA DATA DE ESTA FORMA
            //SELECCIONANDO UNO POR UNO LOS CAMPOS QUE NECESITAREMOS
            //DE LO CONTRARIO, HACERLO DE LA FORMA CONVENCIONAL (EJEMPLO: db.tbCatalogoDeDeducciones.ToList(); )
            var tbFasesReclutamiento1 = db.tbFasesReclutamiento
                        .Select(c => new {
                            fare_Descripcion = c.fare_Descripcion,
                            fare_UsuarioCrea = c.fare_FechaCrea,
                            fare_FechaCrea = c.fare_FechaCrea,
                            fare_UsuarioModifica = c.fare_UsuarioModifica ,
                            fare_FechaModifica = c.fare_FechaModifica,
                            fare_Estado = c.fare_Estado,
                            fare_RazonInactivo = c.fare_RazonInactivo
                        }).Where(c => c.fare_Estado == true)
                        .ToList();
            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = tbFasesReclutamiento1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // GET: FasesReclutamiento/Details/5
        public ActionResult Details(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbFasesReclutamiento tbJSON = db.tbFasesReclutamiento.Find(id);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }

        // GET: FasesReclutamiento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FasesReclutamiento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fare_Id,fare_Descripcion,fare_Estado,fare_RazonInactivo,fare_UsuarioCrea,fare_FechaCrea,fare_UsuarioModifica,fare_FechaModifica")] tbFasesReclutamiento tbFasesReclutamiento)
        {
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbFasesReclutamiento.fare_UsuarioCrea = 1;
            tbFasesReclutamiento.fare_FechaCrea = DateTime.Now;
            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
            string response = String.Empty;
            IEnumerable<object> list = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        response = "error";
                    }
                }
                catch(Exception Ex)
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

        // GET: FasesReclutamiento/Edit/5
        public ActionResult Edit(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbFasesReclutamiento tbJSON = db.tbFasesReclutamiento.Find(id);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }

        // POST: FasesReclutamiento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fare_Id,fare_Descripcion,fare_Estado,fare_RazonInactivo,fare_UsuarioCrea,fare_FechaCrea,fare_UsuarioModifica,fare_FechaModifica")] tbFasesReclutamiento tbFasesReclutamiento)
        {
            tbFasesReclutamiento.fare_UsuarioCrea = 1;
            tbFasesReclutamiento.fare_FechaCrea = DateTime.Now;

            //LLENAR DATA DE AUDITORIA
            tbFasesReclutamiento.fare_UsuarioModifica = 1;
            tbFasesReclutamiento.fare_FechaModifica = DateTime.Now;
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            IEnumerable<object> list = null;
            string MensajeError = "";

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
            else
            {
                // SI EL MODELO NO ES CORRECTO, RETORNAR ERROR
                ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                response = "error";
            }

            //RETORNAR MENSAJE AL LADO DEL CLIENTE
            return Json(response, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Inactivar(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbFasesReclutamiento tbJSON = db.tbFasesReclutamiento.Find(id);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inactivar([Bind(Include = "fare_Id,fare_Descripcion,fare_Estado,fare_RazonInactivo,fare_UsuarioCrea,fare_FechaCrea,fare_UsuarioModifica,fare_FechaModifica")] tbFasesReclutamiento tbFasesReclutamiento)
        {
            //DATA DE AUDIOTIRIA DE CREACIÓN, PUESTA UNICAMENTE PARA QUE NO CAIGA EN EL CATCH
            //EN EL PROCEDIMIENTO ALMACENADO, ESTOS DOS CAMPOS NO SE DEBEN MODIFICAR
            //tbCatalogoDeDeducciones.rsal_UsuarioCrea = 1;
            //tbCatalogoDeDeducciones.rsal_FechaCrea = DateTime.Now;


            //LLENAR DATA DE AUDITORIA
            tbFasesReclutamiento.fare_UsuarioModifica = 1;
            tbFasesReclutamiento.fare_FechaModifica = DateTime.Now;
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            IEnumerable<object> list = null;
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
