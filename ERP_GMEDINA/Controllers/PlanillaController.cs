using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using SpreadsheetLight;
using DocumentFormat.OpenXml;
using ERP_GMEDINA.Helpers;

namespace ERP_GMEDINA.Controllers
{
    public class PlanillaController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Planilla
        public ActionResult Index()
        {
            List<V_ColaboradoresPorPlanilla> colaboradoresPlanillas = db.V_ColaboradoresPorPlanilla.Where(x => x.CantidadColaboradores > 0).ToList();
            ViewBag.PlanillasColaboradores = colaboradoresPlanillas;
            ViewBag.colaboradoresGeneral = db.tbEmpleados.Count().ToString();
            return View(db.V_PreviewPlanilla.ToList());
        }

        public ActionResult GetPlanilla(int? ID)
        {
            List<V_PreviewPlanilla> PreviewPlanilla = new List<V_PreviewPlanilla>();

            if (ID != null)
                PreviewPlanilla = db.V_PreviewPlanilla.Where(x => x.cpla_IdPlanilla == ID).ToList();
            else
                PreviewPlanilla = db.V_PreviewPlanilla.ToList();
            return Json(PreviewPlanilla, JsonRequestBehavior.AllowGet);
        }

        // GET: Planilla/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            V_PreviewPlanilla v_PreviewPlanilla = db.V_PreviewPlanilla.Find(id);
            if (v_PreviewPlanilla == null)
            {
                return HttpNotFound();
            }
            return View(v_PreviewPlanilla);
        }

        public ActionResult GenerarPlanilla(int? ID)
        {
            #region declaracion de variables

            //correo electronico
            General utilities = new General();
            List<IngresosDeduccionesVoucher> ListaIngresosVoucher = new List<IngresosDeduccionesVoucher>();
            List<IngresosDeduccionesVoucher> ListaDeduccionesVoucher = new List<IngresosDeduccionesVoucher>();
            ComprobantePagoModel oComprobantePagoModel = new ComprobantePagoModel();
            IngresosDeduccionesVoucher ingresosColaborador = new IngresosDeduccionesVoucher();
            IngresosDeduccionesVoucher deduccionesColaborador = new IngresosDeduccionesVoucher();

            //enviar resultados al lado del cliente
            iziToast response = new iziToast();
            int errores = 0;

            //procesar planilla empleados
            decimal totalPagarEmpleado = 0;
            decimal totalIngresosEmpleado = 0;
            decimal totalDeduccionesEmpleado = 0;
            decimal SalarioBase = 0;
            decimal bonos = 0, comisiones = 0, otrosIngresos = 0, deduccionesPorInstitucionesFinancieras = 0, DeduccionesExtraordinarias = 0;
            #endregion

            // INCIA PROCESO DE GENERACIÓN DE PLANILLAS
            try
            {

                #region CREAR ARCHIVO EXCEL DE LA PLANILLA
                tbCatalogoDePlanillas oNombrePlanilla = ID != null? db.tbCatalogoDePlanillas.Where(X => X.cpla_IdPlanilla == ID).FirstOrDefault() : null;
                string nombrePlanilla = oNombrePlanilla != null ? oNombrePlanilla.cpla_DescripcionPlanilla : "General";
                string nombreDocumento = $"Planilla {nombrePlanilla} {Convert.ToString(DateTime.Now.Year)}-{Convert.ToString(DateTime.Now.Month)}-{Convert.ToString(DateTime.Now.Day)} {Convert.ToString(DateTime.Now.Hour)}-{Convert.ToString(DateTime.Now.Minute)}.xlsx";
                string nombreDocumento2 = nombreDocumento;
                string pathFile = AppDomain.CurrentDomain.BaseDirectory + nombreDocumento2;
                string direccion = pathFile;
                SLDocument oSLDocument = new SLDocument();
                System.Data.DataTable dt = new System.Data.DataTable();

                dt.Columns.Add("Nombres", typeof(string));
                dt.Columns.Add("Apellidos", typeof(string));
                dt.Columns.Add("Sueldo base", typeof(decimal));
                dt.Columns.Add("Bonos", typeof(decimal));
                dt.Columns.Add("Comisiones", typeof(decimal));
                dt.Columns.Add("Deducciones extras", typeof(decimal));
                dt.Columns.Add("Deducciones Cooperativas", typeof(decimal));
                dt.Columns.Add("IHSS", typeof(decimal));
                dt.Columns.Add("ISR", typeof(decimal));
                dt.Columns.Add("AFP", typeof(decimal));
                dt.Columns.Add("RAP", typeof(decimal));
                dt.Columns.Add("TOTAL A PAGAR", typeof(decimal));
                #endregion


                using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
                {
                    List<tbCatalogoDePlanillas> oIDSPlanillas = new List<tbCatalogoDePlanillas>();

                    using (var dbContextTransaccion = db.Database.BeginTransaction())
                    {

                        //ID DE TODAS LAS PLANILLAS, PARA PROCESARLAS 1 POR 1
                        if (ID != null)
                            oIDSPlanillas = db.tbCatalogoDePlanillas.Where(X => X.cpla_IdPlanilla == ID).ToList();
                        else
                            oIDSPlanillas = db.tbCatalogoDePlanillas.ToList();

                        //PROCESAR LAS PLANILLAS 1 POR 1
                        foreach (var iter in oIDSPlanillas)
                        {
                            try
                            {
                                //OBTENER PLANILLA ACTUAL
                                tbCatalogoDePlanillas oPlanilla = db.tbCatalogoDePlanillas.Find(iter.cpla_IdPlanilla);

                                //OBTENER LOS INGRESOS DE LA PLANILLA ACTUAL
                                List<V_PlanillaIngresos> oIngresos = db.V_PlanillaIngresos.Where(x => x.cpla_IdPlanilla == oPlanilla.cpla_IdPlanilla).ToList();

                                //OBTENER LAS DEDUCCIONES DE LA PLANILLA ACTUAL
                                List<V_PlanillaDeducciones> oDeducciones = db.V_PlanillaDeducciones.Where(x => x.cpla_IdPlanilla == oPlanilla.cpla_IdPlanilla).ToList();

                                //OBTNER LA LISTA DE EMPLEADOS QUE PERTENECEN A LA PLANILLA ACTUAL
                                List<tbEmpleados> oEmpleados = db.tbEmpleados.Where(emp => emp.cpla_IdPlanilla == oPlanilla.cpla_IdPlanilla).ToList();

                                //================== PROCESAR PLANILLA COLABORADOR POR COLABORADOR ==============================
                                foreach (var empleadoActual in oEmpleados)
                                {
                                    try
                                    {

                                        //INFORMACION DEL COLABORADOR ACTUAL
                                        V_InformacionColaborador InformacionDelEmpleadoActual = db.V_InformacionColaborador.Where(x => x.emp_Id == empleadoActual.emp_Id).FirstOrDefault();

                                        //EL SALARIO BASE 
                                        SalarioBase = InformacionDelEmpleadoActual.SalarioBase;
                                        ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                        {
                                            concepto = "Salario base",
                                            monto = SalarioBase
                                        });


                                        //VARIABLES NECESARIAS PARA PROCESAR LA PLANILLA DEL COLABORADOR ACTUAL
                                        bonos = 0; comisiones = 0; otrosIngresos = 0; deduccionesPorInstitucionesFinancieras = 0; DeduccionesExtraordinarias = 0;

                                        //OBTENER LAS DEDUCCIONES EXTRAS DEL COLABORADOR ACTUAL (SI LAS TIENE Y NO HA TERMINADO DE PAGARLAS)
                                        List<V_DeduccionesExtrasColaboradores> oDeduccionesExtrasColaborador = db.V_DeduccionesExtrasColaboradores.Where(DEX => DEX.emp_Id == empleadoActual.emp_Id && DEX.dex_MontoRestante > 0 && DEX.dex_Activo == true).ToList();

                                        //VERIFICAR SI LA LISTA DE DEDUCCIONES EXTRAS DEL COLABORADOR NO VIENE NULA
                                        if (oDeduccionesExtrasColaborador.Count > 0)
                                        {
                                            //SI TIENE DEDUCCIONES EXTRAS, HAY QUE IR SUMANDOLAS TODAS
                                            foreach (var oDeduccionesExtrasColaboradorIterador in oDeduccionesExtrasColaborador)
                                            {
                                                DeduccionesExtraordinarias += oDeduccionesExtrasColaboradorIterador.dex_Cuota;
                                                //CÓDIGO PARA RESTAR LA CUOTA PAGADA DE LA CANTIDAD RESTANTE DE LA DEDUCCIÓN
                                                oDeduccionesExtrasColaboradorIterador.dex_MontoRestante = oDeduccionesExtrasColaboradorIterador.dex_MontoRestante - oDeduccionesExtrasColaboradorIterador.dex_Cuota;
                                                //db.Entry(oDeduccionesExtrasColaboradorIterador).State = EntityState.Modified;           

                                                ListaDeduccionesVoucher.Add(new IngresosDeduccionesVoucher
                                                {
                                                    concepto = oDeduccionesExtrasColaboradorIterador.dex_ObservacionesComentarios,
                                                    monto = oDeduccionesExtrasColaboradorIterador.dex_Cuota
                                                });
                                            }
                                            //db.SaveChanges();
                                        }

                                        //OBTENER LAS DEDUCCIONES POR INSTITUCIONES FINANCIERAS DEL COLABORADOR ACTUAL
                                        List<V_DeduccionesInstitucionesFinancierasColaboradres> oDeduInstiFinancieras = db.V_DeduccionesInstitucionesFinancierasColaboradres.Where(x => x.emp_Id == empleadoActual.emp_Id && x.deif_Activo == true).ToList();

                                        //VERIFICAR SI LA LISTA DE DEDUCCIONES POR INSTITUCIONES FINANCIERAS DEL COLABORADOR NO VIENE NULA
                                        if (oDeduInstiFinancieras.Count > 0)
                                        {
                                            //SI TIENE DEDUCCIONES DE INSTITUCIONES FINANCIERAS, HAY QUE IR SUMANDOLAS TODAS
                                            foreach (var oDeduInstiFinancierasIterador in oDeduInstiFinancieras)
                                            {
                                                deduccionesPorInstitucionesFinancieras += oDeduInstiFinancierasIterador.deif_Monto;
                                                //CÓDIGO PARA RESTAR LA CUOTA PAGADA DE LA CANTIDAD RESTANTE DE LA DEDUCCIÓN
                                                oDeduInstiFinancierasIterador.deif_Activo = false;
                                                //db.Entry(oDeduInstiFinancierasIterador).State = EntityState.Modified;

                                                ListaDeduccionesVoucher.Add(new IngresosDeduccionesVoucher
                                                {
                                                    concepto = $"{oDeduInstiFinancierasIterador.insf_DescInstitucionFinanc} {oDeduInstiFinancierasIterador.deif_Comentarios}",
                                                    monto = oDeduInstiFinancierasIterador.deif_Monto
                                                });

                                            }
                                            //db.SaveChanges();
                                        }

                                        //OBTENER LOS BONOS DEL COLABORADOR SI LOS TIENE
                                        List<V_BonosColaborador> oBonosColaboradores = db.V_BonosColaborador.Where(x => x.emp_Id == empleadoActual.emp_Id && x.cb_Activo == true && x.cb_Pagado == false).ToList();

                                        if (oBonosColaboradores.Count > 0)
                                        {
                                            //SI TIENE BONOS, HAY QUE IR SUMANDOLAS TODAS
                                            foreach (var oBonosColaboradoresIterador in oBonosColaboradores)
                                            {
                                                bonos += oBonosColaboradoresIterador.cb_Monto;
                                                //CÓDIGO PARA RESTAR LA CUOTA PAGADA DE LA CANTIDAD RESTANTE DE LA DEDUCCIÓN
                                                oBonosColaboradoresIterador.cb_Pagado = true;
                                                //db.Entry(oBonosColaboradoresIterador).State = EntityState.Modified;

                                                ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                {
                                                    concepto = oBonosColaboradoresIterador.cin_DescripcionIngreso,
                                                    monto = oBonosColaboradoresIterador.cb_Monto
                                                });
                                            }
                                            //db.SaveChanges();
                                        }

                                        //OBTENER LAS COMISIONES DEL COLABORADOR SI LOS TIENE
                                        List<V_ComisionesColaborador> oComisionesColaboradores = db.V_ComisionesColaborador.Where(x => x.emp_Id == empleadoActual.emp_Id && x.cc_Activo == true && x.cc_Pagado == false).ToList();

                                        if (oComisionesColaboradores.Count > 0)
                                        {
                                            //SI TIENE BONOS, HAY QUE IR SUMANDOLAS TODAS
                                            foreach (var oComisionesColaboradoresIterador in oComisionesColaboradores)
                                            {
                                                comisiones += oComisionesColaboradoresIterador.cc_Monto;
                                                //CÓDIGO PARA RESTAR LA CUOTA PAGADA DE LA CANTIDAD RESTANTE DE LA DEDUCCIÓN
                                                oComisionesColaboradoresIterador.cc_Pagado = true;
                                                //db.Entry(oComisionesColaboradoresIterador).State = EntityState.Modified;

                                                ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                {
                                                    concepto = oComisionesColaboradoresIterador.cin_DescripcionIngreso,
                                                    monto = oComisionesColaboradoresIterador.cc_Monto
                                                });

                                            }
                                            //db.SaveChanges();
                                        }


                                        totalIngresosEmpleado = SalarioBase + bonos + comisiones;
                                        totalDeduccionesEmpleado = DeduccionesExtraordinarias - deduccionesPorInstitucionesFinancieras;
                                        totalPagarEmpleado = totalIngresosEmpleado - totalDeduccionesEmpleado;
                                        //REGISTRO EN LA HOJA DE EXCEL
                                        dt.Rows.Add(empleadoActual.tbPersonas.per_Nombres,
                                                    empleadoActual.tbPersonas.per_Apellidos,
                                                    SalarioBase,
                                                    bonos,
                                                    comisiones,
                                                    DeduccionesExtraordinarias,
                                                    deduccionesPorInstitucionesFinancieras,
                                                    0,
                                                    0,
                                                    0,
                                                    0,
                                                    totalPagarEmpleado);

                                        #region Enviar comprobante de pago por email
                                        oComprobantePagoModel.EmailAsunto = "Comprobante de pago";
                                        oComprobantePagoModel.NombreColaborador = empleadoActual.tbPersonas.per_Nombres + " " + empleadoActual.tbPersonas.per_Apellidos;
                                        oComprobantePagoModel.idColaborador = empleadoActual.emp_Id;
                                        oComprobantePagoModel.EmailDestino = empleadoActual.tbPersonas.per_CorreoElectronico;
                                        oComprobantePagoModel.PeriodoPago = "1/11/2019 - 30/11/2019";
                                        oComprobantePagoModel.Ingresos = ListaIngresosVoucher;
                                        oComprobantePagoModel.Deducciones = ListaDeduccionesVoucher;
                                        oComprobantePagoModel.totalIngresos = totalIngresosEmpleado;
                                        oComprobantePagoModel.totalDeducciones = totalDeduccionesEmpleado;
                                        oComprobantePagoModel.NetoPagar = totalPagarEmpleado;

                                        //Enviar comprobante de pago
                                        try
                                        {
                                            if (!utilities.SendEmail(oComprobantePagoModel))
                                                errores++;
                                            else
                                            {
                                                ListaDeduccionesVoucher = new List<IngresosDeduccionesVoucher>();
                                                ListaIngresosVoucher = new List<IngresosDeduccionesVoucher>();
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            errores++;
                                        }
                                        #endregion
                                    }
                                    //CATCH FOR EACH EMPLEADO POR EMPLEADO
                                    catch (Exception ex)
                                    {
                                        // AGREGAR EL EMPLEADO ACTUAL AL QUE FALLÓ LA GENERACIÓN DE SU PLANILLA
                                        dt.Rows.Add(empleadoActual.tbPersonas.per_Nombres + ' ' + empleadoActual.tbPersonas.per_Apellidos,
                                                    "Ocurrió un error al generar la planilla de este empleado."
                                           );
                                        errores++;

                                    }
                                }
                                db.SaveChanges();
                                dbContextTransaccion.Commit();
                            }

                            //CATCH FOR EACH DE LAS PLANILLAS
                            catch (Exception ex)
                            {
                                // SI ALGO FALLA, HACER UN ROLLBACK
                                dbContextTransaccion.Rollback();
                                errores++;
                                dt.Rows.Add("Ocurrió un error al generar la planilla de xxyy.");
                            }
                        }
                    }
                }
                response.Response = $"El proceso de generación de planilla se realizó, con {errores} errores";
                response.Encabezado = "Exito";
                response.Tipo = "success";
                try
                {
                    oSLDocument.ImportDataTable(1, 1, dt, true);
                    oSLDocument.SaveAs(direccion);
                }
                catch (Exception ex)
                {
                    response.Response = "Planilla generada, error al crear documento excel.";
                    response.Encabezado = "Advertencia";
                    response.Tipo = "warning";
                }
                //===========================================================================

            }
            // CATCH DEL PROCESO DE GENERACIÓN DE PLANILLAS
            catch (Exception ex)
            {
                response.Response = "El proceso de generación de planillas falló, contacte al adminstrador.";
                response.Encabezado = "Error";
                response.Tipo = "error";
            }
            return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "emp_Id,Nombres,per_Identidad,per_Sexo,per_Edad,per_Direccion,per_Telefono,per_CorreoElectronico,per_EstadoCivil,salarioBase,tmon_Id,tmon_Descripcion,cpla_IdPlanilla,cpla_DescripcionPlanilla")] V_PreviewPlanilla v_PreviewPlanilla)
        {
            if (ModelState.IsValid)
            {
                db.V_PreviewPlanilla.Add(v_PreviewPlanilla);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(v_PreviewPlanilla);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "emp_Id,Nombres,per_Identidad,per_Sexo,per_Edad,per_Direccion,per_Telefono,per_CorreoElectronico,per_EstadoCivil,salarioBase,tmon_Id,tmon_Descripcion,cpla_IdPlanilla,cpla_DescripcionPlanilla")] V_PreviewPlanilla v_PreviewPlanilla)
        {
            if (ModelState.IsValid)
            {
                db.Entry(v_PreviewPlanilla).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(v_PreviewPlanilla);
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
    class iziToast
    {
        public string Response { get; set; }
        public string Encabezado { get; set; }
        public string Tipo { get; set; }
    }
}