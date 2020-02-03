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

        #region GET: AcumuladosISR
        public ActionResult Index()
        {
            var tbAcumuladosISR = db.tbAcumuladosISR.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).OrderByDescending(x => x.aisr_FechaCrea);
            return View(tbAcumuladosISR.ToList());
        }
        #endregion

        #region GET: OBTENER LA DATA Y ENVIARLA A LA VISTA EN FORMATO JSON
        [HttpGet]
        public ActionResult GetData()
        {
            var otbAcumuladosISR = db.tbAcumuladosISR
                        .Select(c => new {
                            aisr_Descripcion = c.aisr_Descripcion,
                            aisr_Id = c.aisr_Id,
                            aisr_Monto = c.aisr_Monto,
                            aisr_Activo = c.aisr_Activo,
                            aisr_FechaCrea = c.aisr_FechaCrea,
                            aisr_UsuarioCrea = c.aisr_UsuarioCrea,
                            aisr_UsuarioModifica = c.aisr_UsuarioModifica,
                            aisr_FechaModifica = c.aisr_FechaModifica,
                            aisr_DeducirISR = c.aisr_DeducirISR,
                            emp_Id = c.emp_Id,
                            per_Nombres = c.tbEmpleados.tbPersonas.per_Nombres,
                            per_Apellidos = c.tbEmpleados.tbPersonas.per_Apellidos
                        })
                        //.OrderByDescending(c => c.aisr_FechaCrea)
                        .ToList();

            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = otbAcumuladosISR, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region GET: Details
        public ActionResult Details(int? id)
        {
            var tbAcumuladosISRJSON = from tbAcumuladosISR in db.tbAcumuladosISR
                                      where tbAcumuladosISR.aisr_Id == id
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
                                          tbAcumuladosISR.aisr_FechaModifica,

                                          tbAcumuladosISR.aisr_DeducirISR,

                                          tbAcumuladosISR.emp_Id,
                                          tbAcumuladosISR.tbEmpleados.tbPersonas.per_Nombres,
                                          tbAcumuladosISR.tbEmpleados.tbPersonas.per_Apellidos
                                      };

            db.Configuration.ProxyCreationEnabled = false;

            return Json(tbAcumuladosISRJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "aisr_Id,aisr_Descripcion,aisr_Monto,aisr_UsuarioCrea,aisr_FechaCrea,aisr_UsuarioModifica,aisr_FechaModifica,aisr_Activo,aisr_DeducirISR,emp_Id")] tbAcumuladosISR tbAcumuladosISR)
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
                                                                                     tbAcumuladosISR.aisr_FechaCrea,
                                                                                     tbAcumuladosISR.aisr_DeducirISR,
                                                                                     tbAcumuladosISR.emp_Id);

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
        #endregion

        public string EditGetEmpleadoDDL()
        {
            //RETORNAR LA DATA EN FORMATO JSON AL CLIENTE 
            return Helpers.General.ObtenerEmpleados();
        }

        #region GET: edit
        public JsonResult Edit(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbAcumuladosISR tbAcumuladosISRJSON = db.tbAcumuladosISR.Find(ID);
            return Json(tbAcumuladosISRJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: Editar
        [HttpPost]
        public ActionResult Edit([Bind(Include = "aisr_Id,aisr_Descripcion,aisr_Monto,aisr_Activo,aisr_DeducirISR,emp_Id")] tbAcumuladosISR tbAcumuladosISR)
        {
            // data de auditoria
            tbAcumuladosISR.aisr_UsuarioModifica = 1;
            tbAcumuladosISR.aisr_FechaModifica = DateTime.Now;


            string response = String.Empty;
            IEnumerable<object> listAcumuladosISR = null;
            string MensajeError = "";


            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar procedimiento almacenado
                    listAcumuladosISR = db.UDP_Plani_tbAcumuladosISR_Update(tbAcumuladosISR.aisr_Id,
                                                                                            tbAcumuladosISR.aisr_Descripcion,
                                                                                            tbAcumuladosISR.aisr_Monto,
                                                                                            tbAcumuladosISR.aisr_UsuarioModifica,
                                                                                            tbAcumuladosISR.aisr_FechaModifica,
                                                                                            tbAcumuladosISR.aisr_DeducirISR,
                                                                                            tbAcumuladosISR.emp_Id);
                    // verificar resultado del procedimiento almacenado
                    foreach (UDP_Plani_tbAcumuladosISR_Update_Result Resultado in listAcumuladosISR)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // error: falló el PA
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        response = "error";
                    }

                }
                catch (Exception Ex)
                {
                    // error : se generó una excepción
                    response = "error";
                }

                // el resultado del proceso fue exitoso
                response = "bien";
            }
            else
            {

                // error: el modelo no es válido
                ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                response = "error";
            }

            // retornar resultado
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Inactivar
        public ActionResult Inactivar(int id)
        {
            IEnumerable<object> listAcumuladosISR = null;
            string MensajeError = "";
            string response = String.Empty;

            //validar estado del modelo
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listAcumuladosISR = db.UDP_Plani_tbAcumuladosISR_Inactivar(id,
                                                                                1,
                                                                                DateTime.Now);

                    // verificar resultado del PA
                    foreach (UDP_Plani_tbAcumuladosISR_Inactivar_Result Resultado in listAcumuladosISR)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo inactivar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception)
                {
                    // error: se generó una excepción
                    response = "error";
                }
                // el proceso fue exitoso
                response = "bien";
            }
            else
            {
                // el modelo no es válido
                response = "error";
            }

            // retornar resultado del proceso
            return Json(JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Activar  
        public ActionResult Activar(int id)
        {
            IEnumerable<object> listAcumuladosISR = null;
            string MensajeError = "";
            string response = String.Empty;

            // validar estado del modelo
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listAcumuladosISR = db.UDP_Plani_tbAcumuladosISR_Activar(id,
                                                                                    1,
                                                                                    DateTime.Now);

                    // verificar resultado del PA
                    foreach (UDP_Plani_tbAcumuladosISR_Activar_Result Resultado in listAcumuladosISR)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo activar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception)
                {
                    // se generó una excepción
                    response = "error";
                }

                // el proceso fue exitoso
                response = "bien";
            }
            else
            {
                // modelo inválido
                response = "error";
            }

            // retornar resultado del proceso
            return Json(JsonRequestBehavior.AllowGet);
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
