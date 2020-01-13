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
    public class IngresosIndividualesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        #region Index Ingresos Individuales
        // GET: IngresosIndividuales
        public ActionResult Index()
        {
            var tbIngresosIndividuales = db.tbIngresosIndividuales.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbEmpleados);
            return View(tbIngresosIndividuales.ToList());
        }

        // GET: OBTENER LA DATA Y ENVIARLA A LA VISTA EN FORMATO JSON
        public ActionResult GetData()
        {
            //Variable para Guardar el Select List que llamará el js en el FrontEnd
            var tbIngresosIndividualesD = db.tbIngresosIndividuales
                .Select(d => new
                {
                    ini_IdIngresosIndividuales = d.ini_IdIngresosIndividuales,
                    ini_Motivo = d.ini_Motivo,
                    emp_Id = d.emp_Id,
                    per_Nombres = d.tbEmpleados.tbPersonas.per_Nombres,
                    per_Apellidos = d.tbEmpleados.tbPersonas.per_Apellidos,
                    ini_Monto = d.ini_Monto,
                    ini_PagaSiempre = d.ini_PagaSiempre,
                    ini_Activo = d.ini_Activo,
                    ini_UsuarioCrea = d.ini_UsuarioCrea,
                    ini_FechaCrea = d.ini_FechaCrea,
                    ini_UsuarioModifica = d.ini_UsuarioModifica,
                    ini_FechaModifica = d.ini_FechaModifica
                }).ToList();

            //Retornamos un Json en el FrontEnd
            return new JsonResult { Data = tbIngresosIndividualesD, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region Crear Ingresos Individuales

        // POST: IngresosIndividuales/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ini_Motivo,emp_Id,ini_Monto,ini_PagaSiempre,ini_UsuarioCrea,ini_FechaCrea")] tbIngresosIndividuales tbIngresosIndividuales)
        {
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbIngresosIndividuales.ini_UsuarioCrea = 1;
            tbIngresosIndividuales.ini_FechaCrea = DateTime.Now;
            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
            string response = String.Empty;
            IEnumerable<object> listIngresosIndividuales = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listIngresosIndividuales = db.UDP_Plani_tbIngresosIndividuales_Insert(tbIngresosIndividuales.ini_Motivo,
                                                                                              tbIngresosIndividuales.emp_Id,
                                                                                              tbIngresosIndividuales.ini_Monto,
                                                                                              tbIngresosIndividuales.ini_PagaSiempre,
                                                                                              tbIngresosIndividuales.ini_UsuarioCrea,
                                                                                              tbIngresosIndividuales.ini_FechaCrea);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbIngresosIndividuales_Insert_Result Resultado in listIngresosIndividuales)
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
        public JsonResult EditGetEmpleadoDDL()
        {
            //OBTENER LA DATA QUE NECESITAMOS, HACIENDOLO DE ESTA FORMA SE EVITA LA EXCEPCION POR "REFERENCIAS CIRCULARES"
            var DDL =
            from Emp in db.tbEmpleados
            join Per in db.tbPersonas on Emp.per_Id equals Per.per_Id
            where Emp.emp_Estado == true
            select new
            {
                Id = Emp.emp_Id,
                Descripcion = Per.per_Nombres + " " + Per.per_Apellidos
            };
            //RETORNAR LA DATA EN FORMATO JSON AL CLIENTE 
            return Json(DDL, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Editar Ingresos Individuales
        // GET: IngresosIndividuales/Edit/5
        public ActionResult Edit(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbIngresosIndividuales tbIngresosIndividualesJSON = db.tbIngresosIndividuales.Find(id);
            return Json(tbIngresosIndividualesJSON, JsonRequestBehavior.AllowGet);
        }

        // POST: IngresosIndividuales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ini_IdIngresosIndividuales,ini_Motivo,emp_Id,ini_Monto,ini_PagaSiempre,ini_UsuarioModifica,ini_FechaModifica")] tbIngresosIndividuales tbIngresosIndividuales)
        {
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbIngresosIndividuales.ini_UsuarioModifica = 1;
            tbIngresosIndividuales.ini_FechaModifica = DateTime.Now;
            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
            string response = String.Empty;
            IEnumerable<object> listIngresosIndividuales = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listIngresosIndividuales = db.UDP_Plani_tbIngresosIndividuales_Update(tbIngresosIndividuales.ini_IdIngresosIndividuales,
                                                                                          tbIngresosIndividuales.ini_Motivo,
                                                                                          tbIngresosIndividuales.emp_Id,
                                                                                          tbIngresosIndividuales.ini_Monto,
                                                                                          tbIngresosIndividuales.ini_PagaSiempre,
                                                                                          tbIngresosIndividuales.ini_UsuarioModifica,
                                                                                          tbIngresosIndividuales.ini_FechaModifica);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbIngresosIndividuales_Update_Result Resultado in listIngresosIndividuales)
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

        #region Detalles Ingresos Individuales
        // GET: IngresosIndividuales/Details/5
        public ActionResult Details(int? id)
        {
            var tbIngresosIndividualesJSON = from tbIngrIndi in db.tbIngresosIndividuales
                                                where tbIngrIndi.ini_Activo == true && tbIngrIndi.ini_IdIngresosIndividuales == id
                                                select new
                                                {
                                                    tbIngrIndi.ini_IdIngresosIndividuales,
                                                    tbIngrIndi.ini_Motivo,
                                                    tbIngrIndi.emp_Id,
                                                    tbIngrIndi.tbEmpleados.tbPersonas.per_Nombres,
                                                    tbIngrIndi.tbEmpleados.tbPersonas.per_Apellidos,
                                                    tbIngrIndi.ini_Monto,
                                                    tbIngrIndi.ini_PagaSiempre,
                                                    tbIngrIndi.ini_Pagado,
                                                    tbIngrIndi.ini_UsuarioCrea,
                                                    UsuCrea = tbIngrIndi.tbUsuario.usu_NombreUsuario,
                                                    tbIngrIndi.ini_FechaCrea,
                                                    tbIngrIndi.ini_UsuarioModifica,
                                                    UsuModifica = tbIngrIndi.tbUsuario1.usu_NombreUsuario,
                                                    tbIngrIndi.ini_FechaModifica
                                                };
            db.Configuration.ProxyCreationEnabled = false;
            return Json(tbIngresosIndividualesJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Inhabilitar Ingresos Individuales
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inactivar(int ini_IdIngresosIndividuales)
        {
            //LLENAR DATA DE AUDITORIA
            int ini_UsuarioModifica = 1;
            DateTime ini_FechaModifica = DateTime.Now;
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            IEnumerable<object> listIngresosIndividuales = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listIngresosIndividuales = db.UDP_Plani_tbIngresosIndividuales_Inactivar(ini_IdIngresosIndividuales,
                                                                                             ini_UsuarioModifica,
                                                                                             ini_FechaModifica);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbIngresosIndividuales_Inactivar_Result Resultado in listIngresosIndividuales)
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

        #region Activar Ingresos Individuales
        [HttpPost]
        public ActionResult Activar(int id)
        {
            //LLENAR DATA DE AUDITORIA
            int ini_UsuarioModifica = 1;
            DateTime ini_FechaModifica = DateTime.Now;
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            IEnumerable<object> listIngresosIndividuales = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listIngresosIndividuales = db.UDP_Plani_tbIngresosIndividuales_Activar(id,
                                                                                           ini_UsuarioModifica,
                                                                                           ini_FechaModifica);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbIngresosIndividuales_Activar_Result Resultado in listIngresosIndividuales)
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
                ModelState.AddModelError("", "No se pudo activar el registro, contacte al administrador.");
                response = "error";
            }
            //ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion", tbCatalogoDeDeducciones.tde_IdTipoDedu);

            //RETORNAR MENSAJE AL LADO DEL CLIENTE
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Ejecutable Ingresos Individuales
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
