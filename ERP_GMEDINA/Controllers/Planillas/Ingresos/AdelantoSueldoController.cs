using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Collections.Generic;
using System.Web.Mvc.Html;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class AdelantoSueldoController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        Models.Helpers Function = new Models.Helpers();
        // GET: AdelantoSueldo

        [SessionManager("AdelantoSueldo/Index")]
        public ActionResult Index()
        {
            try
            {
                var tbAdelantoSueldo = db.tbAdelantoSueldo.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbEmpleados);
                //.OrderBy(t => t.adsu_IdAdelantoSueldo).OrderByDescending(t => t.adsu_IdAdelantoSueldo);
                //.Where(t => t.adsu_Activo == true);
                return View(tbAdelantoSueldo.ToList());
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return View(db.tbAdelantoSueldo.ToList());
            }
        }

        //FUNCION: OBTENER NUEVAMENTE LOS REGISTROS DE LA BASE DE DATOS PARA RECARGAR LA PAGINA Y LLENAR LA TABLA
        public ActionResult GetData()
        {
            //SI SE LLEGA A DAR PROBLEMAS DE "REFERENCIAS CIRCULARES", OBTENER LA DATA DE ESTA FORMA
            //SELECCIONANDO UNO POR UNO LOS CAMPOS QUE NECESITAREMOS
            //DE LO CONTRARIO, HACERLO DE LA FORMA CONVENCIONAL (EJEMPLO: db.tbCatalogoDeDeducciones.ToList(); )
            var tbAdelantoSueldo = db.tbAdelantoSueldo
                        .Select(c => new
                        {
                            adsu_IdAdelantoSueldo = c.adsu_IdAdelantoSueldo,
                            adsu_RazonAdelanto = c.adsu_RazonAdelanto,
                            adsu_Monto = c.adsu_Monto,
                            adsu_FechaAdelanto = c.adsu_FechaAdelanto.Day + "/" + (((c.adsu_FechaAdelanto.Month).ToString().Length > 1) ? c.adsu_FechaAdelanto.Month.ToString() : "0" + c.adsu_FechaAdelanto.Month) + "/" + c.adsu_FechaAdelanto.Year,
                            adsu_Deducido = c.adsu_Deducido,
                            adsu_UsuarioCrea = c.tbUsuario.usu_NombreUsuario,
                            adsu_FechaCrea = c.adsu_FechaCrea,
                            adsu_UsuarioModifica = c.tbUsuario1.usu_NombreUsuario,
                            adsu_FechaModifica = c.adsu_FechaModifica,
                            adsu_Activo = c.adsu_Activo,
                            empleadoNombre = c.tbEmpleados.tbPersonas.per_Nombres + " " + c.tbEmpleados.tbPersonas.per_Apellidos
                        })
                        //.OrderBy(t => t.adsu_IdAdelantoSueldo)
                        //.OrderByDescending(x => x.adsu_IdAdelantoSueldo)
                        .ToList();

            //.Where(p => p.adsu_Activo == true);
            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = tbAdelantoSueldo, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //OBTENER INFORMACION DE LOS REGISTROS DE LOS EMPLEADOS PARA LLENAR EL MODAL DE INSERTAR, SELECCIONA LOS QUE NO TIENEN
        //UN ADELANTO ACTIVO
        public string EmpleadoGetDDL()
        {
            return Helpers.General.ObtenerEmpleados();
        }

        [HttpPost]
        //OBTENER EL SUELDO NETO PROMEDIO
        public JsonResult GetSueldoNetoProm(int? id)
        {
            //Captura de SueldoNetoPromedio
            decimal SueldoNetoPromedio = 0;
            try
            {
                if (id == null)
                    return Json("Id_Vacio", JsonRequestBehavior.AllowGet);
                //LA CONSULTA DEVUELVE LOS REGISTROS QUE NO TENGAN ADELANTOS ACTIVOS
                DateTime FechaHistorialPago = (Function.DatetimeNow()).AddMonths(-6);
                //METODO MEDIANTE HISTORIAL DE PAGO
                List<tbHistorialDePago> HistorialDePago = (List<tbHistorialDePago>)db.tbHistorialDePago.OrderByDescending(x => x.hipa_FechaPago).Where(x => x.emp_Id == id).ToList();
                //Contador
                int Cont = 1;
                foreach (tbHistorialDePago iter in HistorialDePago)
                {
                    SueldoNetoPromedio += (decimal)iter.hipa_SueldoNeto;
                    if (Cont == 6)
                        break;
                    Cont++;
                }
                SueldoNetoPromedio = SueldoNetoPromedio / Cont;
                if (SueldoNetoPromedio == 0)
                {
                    IQueryable<decimal> Sueldo = (IQueryable<decimal>)db.tbSueldos.Where(p => p.emp_Id == id).Select(x => x.sue_Cantidad).Take(1);
                    SueldoNetoPromedio = Sueldo.Average();
                }
                //List<decimal> SalariosUlt6Meses = (List<decimal>)HistorialDePago.Where(p => p.emp_Id == id
                //                                                            && p.hipa_FechaCrea >= FechaHistorialPago)
                //                                                            .Select(x => (decimal)x.hipa_SueldoNeto).Take(6);
                ////CAPTURA DEL SALARIO PROMEDIO Y CONVERSIÓN A STRING PARA EL RETORNO 
                //SueldoNetoPromedio = (SalariosUlt6Meses.Count() >= 1) ? SalariosUlt6Meses.Average() : 0;
                //if (SueldoNetoPromedio == 0)
                //{
                //    var Salario = db.tbSueldos.OrderByDescending(x => x.sue_FechaCrea).ToList();
                //    List<decimal> Sueldo = (List<decimal>)db.tbSueldos.Where(p => p.emp_Id == id).Select(x => x.sue_Cantidad).Take(1);
                //    SueldoNetoPromedio = Sueldo.Average();
                //}
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            //RETORNAR LA DATA EN FORMATO JSON AL CLIENTE
            return Json(Math.Round(SueldoNetoPromedio, 2), JsonRequestBehavior.AllowGet);
        }

        //FUNCION: CREAR UN NUEVO REGISTRO
        [HttpPost]
        [SessionManager("AdelantoSueldo/Create")]
        public ActionResult Create([Bind(Include = "emp_Id, adsu_FechaAdelanto, adsu_RazonAdelanto, adsu_Monto")] tbAdelantoSueldo tbAdelantoSueldo)
        {
            //Para llenar los campos de auditoría
            tbAdelantoSueldo.adsu_UsuarioCrea = Function.GetUser();
            tbAdelantoSueldo.adsu_FechaCrea = Function.DatetimeNow();

            //Variable para enviarla al lado del Cliente
            string Response = String.Empty;
            IEnumerable<object> listAdelantoSueldo = null;
            string MensajeError = "";

            if (ModelState.IsValid)
            {
                try
                {
                    //Ejecutar Procedimiento Almacenado
                    listAdelantoSueldo = db.UDP_Plani_tbAdelantoSueldo_Insert(tbAdelantoSueldo.emp_Id,
                                                                              tbAdelantoSueldo.adsu_FechaAdelanto,
                                                                              tbAdelantoSueldo.adsu_RazonAdelanto,
                                                                              tbAdelantoSueldo.adsu_Monto,
                                                                              tbAdelantoSueldo.adsu_UsuarioCrea,
                                                                              tbAdelantoSueldo.adsu_FechaCrea);

                    //El tipo complejo del Procedimiento Almacenado
                    foreach (UDP_Plani_tbAdelantoSueldo_Insert_Result Resultado in listAdelantoSueldo)
                    {
                        MensajeError = Resultado.MensajeError;
                    }

                    if (MensajeError.StartsWith("-1"))
                    {
                        //En caso de un error igualamos la variable Response a "Error" para validar en el lado del Cliente
                        ModelState.AddModelError("", "No se pudo Registrar. Contacte al Administrador!");
                        Response = "Error";
                    }
                }
                catch (Exception Ex)
                {
                    Response = Ex.Message.ToString();
                }
                //Si llega aqui significa que todo salio correctamente. Solo igualamos Response a "Exito" para validar en el lado del Cliente
                Response = "Exito";
                return RedirectToAction("Index");

            }
            else
            {
                //Si el modelo no es valido. Igualamos Response a "Error" para validar en el lado del Cliente
                Response = "Error";
            }
            return Json(Response, JsonRequestBehavior.AllowGet);
        }


        //EDITAR

        //OBTENER REGISTRO PARA EDITAR
        public ActionResult Edit(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbAdelantoSueldo tbAdelantoSueldoJSON = db.tbAdelantoSueldo.Find(id);
            return Json(tbAdelantoSueldoJSON, JsonRequestBehavior.AllowGet);
        }

        //FUNCION: EDITAR UN REGISTRO
        [SessionManager("AdelantoSueldo/Edit")]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "adsu_IdAdelantoSueldo,emp_Id,adsu_RazonAdelanto,adsu_Monto,adsu_UsuarioModifica,adsu_FechaModifica")] tbAdelantoSueldo tbAdelantoSueldo)
        {
            tbAdelantoSueldo.adsu_UsuarioModifica = Function.GetUser();
            tbAdelantoSueldo.adsu_FechaModifica = Function.DatetimeNow();

            string response = "bien";
            IEnumerable<object> listAdelantoSueldo = null;
            string MensajeError = "";


            if (ModelState.IsValid)
            {
                try
                {

                    listAdelantoSueldo = db.UDP_Plani_tbAdelantoSueldo_Update(tbAdelantoSueldo.adsu_IdAdelantoSueldo,
                                                                              tbAdelantoSueldo.emp_Id,
                                                                              tbAdelantoSueldo.adsu_RazonAdelanto,
                                                                              tbAdelantoSueldo.adsu_Monto,
                                                                              tbAdelantoSueldo.adsu_UsuarioModifica,
                                                                              tbAdelantoSueldo.adsu_FechaModifica);

                    foreach (UDP_Plani_tbAdelantoSueldo_Update_Result resultado in listAdelantoSueldo)
                        MensajeError = resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    //MENSAJE DE ERROR
                    Ex.Message.ToString();
                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                    response = "error";
                }
            }

            else
            {
                // SI EL MODELO NO ES CORRECTO, RETORNAR ERROR
                ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador");
                response = "error";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [SessionManager("AdelantoSueldo/Details")]
        //DETALLES
        public JsonResult Details(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            V_tbAdelantoSueldo V_tbAdelantoSueldoJSON = db.V_tbAdelantoSueldo.SingleOrDefault(m => m.adsu_IdAdelantoSueldo == id);
            return Json(V_tbAdelantoSueldoJSON, JsonRequestBehavior.AllowGet);
        }

        //INACTIVAR
        [SessionManager("AdelantoSueldo/Inactivar")]
        [HttpPost]
        public ActionResult Inactivar(int? Id)
        {
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = "bien";
            IEnumerable<object> listAdelantoSueldo = null;
            string MensajeError = "";
            //Validar que el Id no sea null
            if (Id == null)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            //LLENAR DATA DE AUDITORIA
            tbAdelantoSueldo tbAdelantoSueldo = new tbAdelantoSueldo();
            tbAdelantoSueldo.adsu_IdAdelantoSueldo = (int)Id;
            tbAdelantoSueldo.adsu_UsuarioModifica = Function.GetUser();
            tbAdelantoSueldo.adsu_FechaModifica = Function.DatetimeNow();
            try
            {
                //EJECUTAR PROCEDIMIENTO ALMACENADO
                listAdelantoSueldo = db.UDP_Plani_tbAdelantoSueldo_Inactivar(tbAdelantoSueldo.adsu_IdAdelantoSueldo,
                                                                            tbAdelantoSueldo.adsu_UsuarioModifica,
                                                                            tbAdelantoSueldo.adsu_FechaModifica);

                //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                foreach (UDP_Plani_tbAdelantoSueldo_Inactivar_Result Resultado in listAdelantoSueldo)
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

        //ACTIVAR
        [SessionManager("AdelantoSueldo/Activar")]
        [HttpPost]
        public ActionResult Activar(int? Id)
        {
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = "bien";
            IEnumerable<object> listAdelantoSueldo = null;
            string MensajeError = "";
            //Validar que el Id no sea null
            if (Id == null)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            //LLENAR DATA DE AUDITORIA
            tbAdelantoSueldo tbAdelantoSueldo = new tbAdelantoSueldo();
            tbAdelantoSueldo.adsu_IdAdelantoSueldo = (int)Id;
            tbAdelantoSueldo.adsu_UsuarioModifica = Function.GetUser();
            tbAdelantoSueldo.adsu_FechaModifica = Function.DatetimeNow();
            try
            {
                //EJECUTAR PROCEDIMIENTO ALMACENADO
                listAdelantoSueldo = db.UDP_Plani_tbAdelantoSueldo_Activar(tbAdelantoSueldo.adsu_IdAdelantoSueldo,
                                                                            tbAdelantoSueldo.adsu_UsuarioModifica,
                                                                            tbAdelantoSueldo.adsu_FechaModifica);

                //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                foreach (UDP_Plani_tbAdelantoSueldo_Activar_Result Resultado in listAdelantoSueldo)
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