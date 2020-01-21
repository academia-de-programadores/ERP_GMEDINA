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
    public class AcumuladosISRController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: AcumuladosISR
        public ActionResult Index()
        {
            var tbAcumuladosISR = db.tbAcumuladosISR.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).OrderByDescending(x => x.aisr_FechaCrea);
            return View(tbAcumuladosISR.ToList());
        }
        [HttpGet]
        // GET: OBTENER LA DATA Y ENVIARLA A LA VISTA EN FORMATO JSON
        public ActionResult GetData()
        {
            var otbAcumuladosISR = db.tbAcumuladosISR
                        .Select(c => new { aisr_Descripcion = c.aisr_Descripcion, aisr_Id = c.aisr_Id, aisr_Monto = c.aisr_Monto, aisr_Activo = c.aisr_Activo, aisr_FechaCrea = c.aisr_FechaCrea, aisr_UsuarioCrea = c.aisr_UsuarioCrea, aisr_UsuarioModifica = c.aisr_UsuarioModifica, aisr_FechaModifica = c.aisr_FechaModifica })
                        //.OrderByDescending(c => c.aisr_FechaCrea)
                        .ToList();

            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = otbAcumuladosISR, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        // GET: AcumuladosISR/Details/5
        public ActionResult Details(int? id)
        {
            var tbAcumuladosISRJSON = from tbAcumuladosISR in db.tbAcumuladosISR
                                      where tbAcumuladosISR.aisr_Activo == true && tbAcumuladosISR.aisr_Id == id
                                      select new
                                      {
                                          tbAcumuladosISR.aisr_Id,
                                          tbAcumuladosISR.aisr_Descripcion,
                                          tbAcumuladosISR.aisr_Monto,
                                          tbAcumuladosISR.aisr_Activo,

                                          tbAcumuladosISR.aisr_UsuarioCrea,
                                          UsuCrea = tbAcumuladosISR.tbUsuario.usu_NombreUsuario,
                                          tbAcumuladosISR.aisr_FechaCrea,

                                          tbAcumuladosISR.aisr_UsuarioModifica,
                                          UsuModifica = tbAcumuladosISR.tbUsuario1.usu_NombreUsuario,
                                          tbAcumuladosISR.aisr_FechaModifica
                                      };

            db.Configuration.ProxyCreationEnabled = false;

            return Json(tbAcumuladosISRJSON, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "aisr_Id,aisr_Descripcion,aisr_Monto,aisr_UsuarioCrea,aisr_FechaCrea,aisr_UsuarioModifica,aisr_FechaModifica,aisr_Activo")] tbAcumuladosISR tbAcumuladosISR)
        {
            #region declaracion de variables 
            tbAcumuladosISR.aisr_UsuarioCrea = 1;
            tbAcumuladosISR.aisr_FechaCrea = DateTime.Now;
            //Variable para almacenar el resultado del proceso y enviarlo al lado del cliente
            string response = String.Empty;
            IEnumerable<object> listAcumuladosISR = null;
            string MensajeError = "";
            #endregion

            if (ModelState.IsValid)
            {
                try
                {
                    //Ejecutar el procedimiento almacenado
                    listAcumuladosISR = db.UDP_Plani_tbAcumuladosISR_Insert(tbAcumuladosISR.aisr_Descripcion,
                                                                                     tbAcumuladosISR.aisr_Monto,
                                                                                     tbAcumuladosISR.aisr_UsuarioCrea,
                                                                                     tbAcumuladosISR.aisr_FechaCrea);

                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbAcumuladosISR_Insert_Result Resultado in listAcumuladosISR)
                        MensajeError = Resultado.MensajeError;

                    response = "bien";
                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo ingresar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    response = Ex.Message.ToString();
                }

            }
            else
            {
                //SI EL MODELO NO ES VÁLIDO, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                response = "error";
            }
            //RETORNAMOS LA VARIABLE RESPONSE AL CLIENTE PARA EVALUARLA
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Edit(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbAcumuladosISR tbAcumuladosISRJSON = db.tbAcumuladosISR.Find(ID);
            return Json(tbAcumuladosISRJSON, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Edit([Bind(Include = "aisr_Id,aisr_Descripcion,aisr_Monto,aisr_Activo")] tbAcumuladosISR tbAcumuladosISR)
        {
            //DATA DE AUDIOTIRIA DE CREACIÓN, PUESTA UNICAMENTE PARA QUE NO CAIGA EN EL CATCH
            //EN EL PROCEDIMIENTO ALMACENADO, ESTOS DOS CAMPOS NO SE DEBEN MODIFICAR
            tbAcumuladosISR.aisr_UsuarioCrea = 1;
            tbAcumuladosISR.aisr_FechaCrea = DateTime.Now;
            tbAcumuladosISR.aisr_Monto = (decimal)((tbAcumuladosISR.aisr_Monto % 1 == 0) ? Convert.ToDecimal(tbAcumuladosISR.aisr_Monto + ".00") : Convert.ToDecimal(tbAcumuladosISR.aisr_Monto) );


            //LLENAR DATA DE AUDITORIA
            tbAcumuladosISR.aisr_UsuarioModifica = 1;
            tbAcumuladosISR.aisr_FechaModifica = DateTime.Now;
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            IEnumerable<object> listAcumuladosISR = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listAcumuladosISR = db.UDP_Plani_tbAcumuladosISR_Update(tbAcumuladosISR.aisr_Id,
                                                                                            tbAcumuladosISR.aisr_Descripcion,
                                                                                            tbAcumuladosISR.aisr_Monto,
                                                                                            tbAcumuladosISR.aisr_UsuarioModifica,
                                                                                            tbAcumuladosISR.aisr_FechaModifica);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbAcumuladosISR_Update_Result Resultado in listAcumuladosISR)
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
            else {
                // SI EL MODELO NO ES CORRECTO, RETORNAR ERROR
                ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                response = "error";
            }
            //ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion", tbCatalogoDeDeducciones.tde_IdTipoDedu);
            
            //RETORNAR MENSAJE AL LADO DEL CLIENTE
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Inactivar(int id)
        {
            IEnumerable<object> listAcumuladosISR = null;
            string MensajeError = "";
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    listAcumuladosISR = db.UDP_Plani_tbAcumuladosISR_Inactivar(id,
                                                                                1,
                                                                                DateTime.Now);

                    foreach (UDP_Plani_tbAcumuladosISR_Inactivar_Result Resultado in listAcumuladosISR)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo inactivar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception)
                {
                    response = "error";
                }
                response = "bien";
            }
            else
            {
                //Se devuelve un mensaje de error en caso de que el modelo no sea válido
                response = "error";
            }

            return Json(JsonRequestBehavior.AllowGet);
        }

        // GET: TechosDeducciones/Activar/5    
        public ActionResult Activar(int id)
        {
            IEnumerable<object> listAcumuladosISR = null;
            string MensajeError = "";
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    listAcumuladosISR = db.UDP_Plani_tbAcumuladosISR_Activar(id,
                                                                                    1,
                                                                                    DateTime.Now);

                    foreach (UDP_Plani_tbAcumuladosISR_Activar_Result Resultado in listAcumuladosISR)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo activar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception)
                {
                    response = "error";
                }
                response = "bien";
            }
            else
            {
                //Se devuelve un mensaje de error en caso de que el modelo no sea válido
                response = "error";
            }

            return Json(JsonRequestBehavior.AllowGet);
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
