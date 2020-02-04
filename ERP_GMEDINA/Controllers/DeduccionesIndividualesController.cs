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
    public class DeduccionesIndividualesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        #region Index Deducciones Individuales
        // GET: DeduccionesIndividuales
        public ActionResult Index()
        {
            var tbDeduccionesIndividuales = db.tbDeduccionesIndividuales.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbEmpleados);
            return View(tbDeduccionesIndividuales.ToList());
        }

        // GET: OBTENER LA DATA Y ENVIARLA A LA VISTA EN FORMATO JSON
        public ActionResult GetData()
        {
            //Variable para Guardar el Select List que llamará el js en el FrontEnd
            var tbDeduccionesIndividualesD = db.tbDeduccionesIndividuales
                .Select(d => new
                {
                    dei_IdDeduccionesIndividuales = d.dei_IdDeduccionesIndividuales,
                    dei_Motivo = d.dei_Motivo,
                    emp_Id = d.emp_Id,
                    per_Nombres = d.tbEmpleados.tbPersonas.per_Nombres,
                    per_Apellidos = d.tbEmpleados.tbPersonas.per_Apellidos,
                    dei_Monto = d.dei_Monto,
                    dei_NumeroCuotas = d.dei_NumeroCuotas,
                    dei_MontoCuota = d.dei_MontoCuota,
                    dei_PagaSiempre = d.dei_PagaSiempre,
                    dei_Activo = d.dei_Activo,
                    dei_UsuarioCrea = d.dei_UsuarioCrea,
                    dei_FechaCrea = d.dei_FechaCrea,
                    dei_UsuarioModifica = d.dei_UsuarioModifica,
                    dei_FechaModifica = d.dei_FechaModifica,
                    dei_DeducirISR = d.dei_DeducirISR
                })
                .OrderBy(d => d.dei_FechaCrea)
                .ToList();

            //Retornamos un Json en el FrontEnd
            return new JsonResult { Data = tbDeduccionesIndividualesD, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region Crear Deducciones Individuales
        // GET: DeduccionesIndividuales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeduccionesIndividuales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(string dei_Motivo, int emp_Id, decimal dei_Monto, int dei_NumeroCuotas, decimal dei_MontoCuota, bool dei_PagaSiempre, bool dei_DeducirISR)
        {
            tbDeduccionesIndividuales tbDeduccionesIndividuales = new tbDeduccionesIndividuales
            {
                dei_Motivo = dei_Motivo,
                emp_Id = emp_Id,
                dei_Monto = dei_Monto,
                dei_NumeroCuotas = dei_NumeroCuotas,
                dei_MontoCuota = dei_MontoCuota,
                dei_PagaSiempre = dei_PagaSiempre,
                dei_DeducirISR = dei_DeducirISR
            };
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbDeduccionesIndividuales.dei_UsuarioCrea = 1;
            tbDeduccionesIndividuales.dei_FechaCrea = DateTime.Now;
            //tbDeduccionesIndividuales.dei_Pagado = false;
            //tbDeduccionesIndividuales.dei_DeducirISR = false;
            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
            string response = String.Empty;
            IEnumerable<object> listDeduccionIndividuales = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    //(string dei_Motivo, Nullable<int> emp_Id, Nullable<decimal> dei_Monto, Nullable<int> dei_NumeroCuotas, Nullable<decimal> dei_MontoCuotas, Nullable<bool> dei_PagaSiempre, Nullable<int> dei_UsuarioCrea, Nullable<System.DateTime> dei_FechaCrea)
                    listDeduccionIndividuales = db.UDP_Plani_tbDeduccionesIndividuales_Insert(tbDeduccionesIndividuales.dei_Motivo,
                                                                                              tbDeduccionesIndividuales.emp_Id,
                                                                                              tbDeduccionesIndividuales.dei_Monto,
                                                                                              tbDeduccionesIndividuales.dei_NumeroCuotas,
                                                                                              tbDeduccionesIndividuales.dei_MontoCuota,
                                                                                              tbDeduccionesIndividuales.dei_PagaSiempre,
                                                                                              tbDeduccionesIndividuales.dei_Pagado,
                                                                                              tbDeduccionesIndividuales.dei_UsuarioCrea,
                                                                                              tbDeduccionesIndividuales.dei_FechaCrea,
                                                                                              tbDeduccionesIndividuales.dei_DeducirISR);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbDeduccionesIndividuales_Insert_Result Resultado in listDeduccionIndividuales)
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
        #endregion

        #region Dropdownlist
        //FUNCIÓN: OBETENER LA DATA PARA LLENAR LOS DROPDOWNLIST DE EDICIÓN Y CREACIÓN
        public string EditGetEmpleadoDDL()
        {
            return Helpers.General.ObtenerEmpleados();
        }
        #endregion

        #region Editar Deducciones Individuales
        // GET: DeduccionesIndividuales/Edit/5
        public ActionResult Edit(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbDeduccionesIndividuales tbDeduccionesIndividualesJSON = db.tbDeduccionesIndividuales.Find(id);
            return Json(tbDeduccionesIndividualesJSON, JsonRequestBehavior.AllowGet);
        }

        // POST: DeduccionesIndividuales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(int dei_IdDeduccionesIndividuales, string dei_Motivo, int emp_Id, decimal dei_Monto, int dei_NumeroCuotas, decimal dei_MontoCuota, bool dei_PagaSiempre, bool dei_DeducirISR)
        {
            tbDeduccionesIndividuales tbDeduccionesIndividuales = new tbDeduccionesIndividuales
            {
                dei_IdDeduccionesIndividuales = dei_IdDeduccionesIndividuales,
                dei_Motivo = dei_Motivo,
                emp_Id = emp_Id,
                dei_Monto = dei_Monto,
                dei_NumeroCuotas = dei_NumeroCuotas,
                dei_MontoCuota = dei_MontoCuota,
                dei_PagaSiempre = dei_PagaSiempre,
                dei_DeducirISR = dei_DeducirISR
            };
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbDeduccionesIndividuales.dei_UsuarioModifica = 1;
            tbDeduccionesIndividuales.dei_FechaModifica = DateTime.Now;
            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
            string response = String.Empty;
            IEnumerable<object> listDeduccionIndividuales = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listDeduccionIndividuales = db.UDP_Plani_tbDeduccionesIndividuales_Update(tbDeduccionesIndividuales.dei_IdDeduccionesIndividuales,
                                                                                              tbDeduccionesIndividuales.dei_Motivo,
                                                                                              tbDeduccionesIndividuales.emp_Id,
                                                                                              tbDeduccionesIndividuales.dei_Monto,
                                                                                              tbDeduccionesIndividuales.dei_NumeroCuotas,
                                                                                              tbDeduccionesIndividuales.dei_MontoCuota,
                                                                                              tbDeduccionesIndividuales.dei_PagaSiempre,
                                                                                              tbDeduccionesIndividuales.dei_UsuarioModifica,
                                                                                              tbDeduccionesIndividuales.dei_FechaModifica,
                                                                                              tbDeduccionesIndividuales.dei_DeducirISR);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbDeduccionesIndividuales_Update_Result Resultado in listDeduccionIndividuales)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo editar el registro, contacte al administrador");
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
        #endregion

        #region Detalles Deducciones Individuales
        // GET: DeduccionesIndividuales/Details/5
        public ActionResult Details(int? id)
        {
            var tbDeduccionesIndividualesJSON = from tbDeduIndi in db.tbDeduccionesIndividuales
                                                where tbDeduIndi.dei_IdDeduccionesIndividuales == id
                                                select new
                                                {
                                                    tbDeduIndi.dei_IdDeduccionesIndividuales,
                                                    tbDeduIndi.dei_Motivo,
                                                    tbDeduIndi.emp_Id,
                                                    tbDeduIndi.tbEmpleados.tbPersonas.per_Nombres,
                                                    tbDeduIndi.tbEmpleados.tbPersonas.per_Apellidos,
                                                    tbDeduIndi.dei_Monto,
                                                    tbDeduIndi.dei_NumeroCuotas,
                                                    tbDeduIndi.dei_MontoCuota,
                                                    tbDeduIndi.dei_PagaSiempre,
                                                    tbDeduIndi.dei_UsuarioCrea,
                                                    UsuCrea = tbDeduIndi.tbUsuario.usu_NombreUsuario,
                                                    tbDeduIndi.dei_FechaCrea,
                                                    tbDeduIndi.dei_UsuarioModifica,
                                                    UsuModifica = tbDeduIndi.tbUsuario1.usu_NombreUsuario,
                                                    tbDeduIndi.dei_FechaModifica
                                                };
            db.Configuration.ProxyCreationEnabled = false;
            return Json(tbDeduccionesIndividualesJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Inhabilitar Deducciones Individuales
        [HttpPost]
        public ActionResult Inactivar(int dei_IdDeduccionesIndividuales)
        {
            //LLENAR DATA DE AUDITORIA
            int dei_UsuarioModifica = 1;
            DateTime dei_FechaModifica = DateTime.Now;
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            IEnumerable<object> listDeduccionesIndividuales = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listDeduccionesIndividuales = db.UDP_Plani_tbDeduccionesIndividuales_Inactivar(dei_IdDeduccionesIndividuales,
                                                                                                   dei_UsuarioModifica,
                                                                                                   dei_FechaModifica);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbDeduccionesIndividuales_Inactivar_Result Resultado in listDeduccionesIndividuales)
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
        #endregion

        #region Activar Deducciones Individuales
        [HttpPost]
        public ActionResult Activar(int id)
        {
            //LLENAR DATA DE AUDITORIA
            int dei_UsuarioModifica = 1;
            DateTime dei_FechaModifica = DateTime.Now;
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            IEnumerable<object> listDeduccionesIndividuales = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listDeduccionesIndividuales = db.UDP_Plani_tbDeduccionesIndividuales_Activar(id,
                                                                                                 dei_UsuarioModifica,
                                                                                                 dei_FechaModifica);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbDeduccionesIndividuales_Activar_Result Resultado in listDeduccionesIndividuales)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo activar el registro, contacte al administrador");
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
        #endregion

        #region Ejecutable Deducciones Individuales
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
