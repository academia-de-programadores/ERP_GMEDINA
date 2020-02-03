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

        #region GET: Planilla
        public ActionResult Index()
        {
            List<V_ColaboradoresPorPlanilla> colaboradoresPlanillas = db.V_ColaboradoresPorPlanilla.Where(x => x.CantidadColaboradores > 0).ToList();
            ViewBag.PlanillasColaboradores = colaboradoresPlanillas;
            ViewBag.colaboradoresGeneral = db.V_PreviewPlanilla.Count().ToString();
            return View(db.V_PreviewPlanilla.ToList());
        }
        #endregion

        #region GET: GetPlanilla
        // cargar en el datatable una planilla en específico
        public ActionResult GetPlanilla(int? ID)
        {
            List<V_PreviewPlanilla> PreviewPlanilla = new List<V_PreviewPlanilla>();

            if (ID != null)
                PreviewPlanilla = db.V_PreviewPlanilla.Where(x => x.cpla_IdPlanilla == ID).ToList();
            else
                PreviewPlanilla = db.V_PreviewPlanilla.ToList();
            return Json(PreviewPlanilla, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GenerarPlanilla
        public ActionResult GenerarPlanilla(int? ID, bool? enviarEmail, DateTime fechaInicio, DateTime fechaFin)
        {
            #region declaracion de instancias

            // helper
            General utilities = new General();

            // instancias para el comprobante de pago
            List<IngresosDeduccionesVoucher> ListaIngresosVoucher = new List<IngresosDeduccionesVoucher>();
            List<IngresosDeduccionesVoucher> ListaDeduccionesVoucher = new List<IngresosDeduccionesVoucher>();
            ComprobantePagoModel oComprobantePagoModel = new ComprobantePagoModel();
            IngresosDeduccionesVoucher ingresosColaborador = new IngresosDeduccionesVoucher();
            IngresosDeduccionesVoucher deduccionesColaborador = new IngresosDeduccionesVoucher();

            // instancias para el reporte final
            ReportePlanillaViewModel oPlanillaEmpleado;
            List<ReportePlanillaViewModel> reporte = new List<ReportePlanillaViewModel>();
            ViewModelListaErrores oError;
            List<ViewModelListaErrores> listaErrores = new List<ViewModelListaErrores>();

            // instancia para resultado del proceso en izitoast
            iziToast response = new iziToast();
            int errores = 0;
            string codigoDePlanillaGenerada = String.Empty;
            #endregion

            #region inicia proceso de generación de planilla
            try
            {
                using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
                {
                    List<tbCatalogoDePlanillas> oIDSPlanillas = new List<tbCatalogoDePlanillas>();

                    // seleccionar las planillas que se van a generar
                    if (ID != null)
                        oIDSPlanillas = db.tbCatalogoDePlanillas
                                          .Where(X => X.cpla_IdPlanilla == ID)
                                          .ToList();
                    else
                        oIDSPlanillas = db.tbCatalogoDePlanillas
                                          .Where(x => x.cpla_Activo == true)
                                          .ToList();

                    if (oIDSPlanillas != null)
                    {
                        // procesar todas las planillas seleccionadas
                        foreach (var iter in oIDSPlanillas)
                        {
                            codigoDePlanillaGenerada = $"PLANI_{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}_{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}";
                            try
                            {
                                // planilla actual del foreach
                                tbCatalogoDePlanillas oPlanilla = db.tbCatalogoDePlanillas
                                                                    .Find(iter.cpla_IdPlanilla);

                                // ingresos de la planilla actual
                                List<V_PlanillaIngresos> oIngresos = db.V_PlanillaIngresos
                                                                       .Where(x => x.cpla_IdPlanilla == oPlanilla.cpla_IdPlanilla)
                                                                       .ToList();

                                // deducciones de la planilla actual
                                List<V_PlanillaDeducciones> oDeducciones = db.V_PlanillaDeducciones
                                                                             .Where(x => x.cpla_IdPlanilla == oPlanilla.cpla_IdPlanilla)
                                                                             .ToList();

                                // empleados de la planilla actual
                                List<tbEmpleados> oEmpleados = db.tbEmpleados
                                                                 .Where(emp => emp.cpla_IdPlanilla == oPlanilla.cpla_IdPlanilla &&
                                                                        emp.emp_Estado == true)
                                                                .ToList();

                                int contador = 1;
                                int idHistorialPago = 0;
                                int idDetalleDeduccionHisotorialesContador = 1;
                                int idDetalleIngresoHisotorialesContador = 1;
                                string identidadEmpleado = string.Empty;
                                string NombresEmpleado = string.Empty;

                                // procesar planilla empleado por empleado
                                foreach (var empleadoActual in oEmpleados)
                                {
                                    using (var dbContextTransaccion = db.Database.BeginTransaction())
                                    {
                                        try
                                        {
                                            #region variables reporte view model

                                            string codColaborador = string.Empty;
                                            string nombreColaborador = string.Empty;
                                            decimal SalarioBase = 0;
                                            int horasTrabajadas = 0;
                                            decimal salarioHora = 0;
                                            decimal totalSalario = 0;
                                            string tipoPlanilla = string.Empty;
                                            decimal? porcentajeComision = 0;
                                            decimal? totalVentas = 0;
                                            decimal? totalComisiones = 0;
                                            int horasExtrasTrabajadas = 0;
                                            int cantidadUnidadesBonos = 0;
                                            decimal? totalHorasExtras = 0;
                                            decimal? totalHorasPermiso = 0;
                                            decimal? totalBonificaciones = 0;
                                            decimal? totalIngresosIndivuales = 0;
                                            decimal? totalVacaciones = 0;
                                            decimal? totalIngresosEmpleado = 0;
                                            decimal totalISR = 0;
                                            decimal? colaboradorDeducciones = 0;
                                            decimal totalAFP = 0;
                                            decimal? totalInstitucionesFinancieras = 0;
                                            decimal? totalOtrasDeducciones = 0;
                                            decimal? adelantosSueldo = 0;
                                            decimal? totalDeduccionesEmpleado = 0;
                                            decimal? totalDeduccionesIndividuales = 0;
                                            decimal? netoAPagarColaborador = 0;
                                            oPlanillaEmpleado = new ReportePlanillaViewModel();
                                            oError = new ViewModelListaErrores();

                                            // variables para insertar en los historiales de pago
                                            IEnumerable<object> listHistorialPago = null;
                                            string MensajeError = "";
                                            List<tbHistorialDeduccionPago> lisHistorialDeducciones = new List<tbHistorialDeduccionPago>();
                                            List<tbHistorialDeIngresosPago> lisHistorialIngresos = new List<tbHistorialDeIngresosPago>();


                                            #endregion

                                            #region Procesar ingresos

                                            // informacion del colaborador actual
                                            V_InformacionColaborador InformacionDelEmpleadoActual = db.V_InformacionColaborador
                                                                                                      .Where(x => x.emp_Id == empleadoActual.emp_Id)
                                                                                                      .FirstOrDefault();

                                            // salario base del colaborador actual
                                            try
                                            {
                                                SalarioBase = Math.Round(InformacionDelEmpleadoActual.SalarioBase.Value, 2);
                                            }
                                            catch (Exception ex)
                                            {
                                                listaErrores.Add(new ViewModelListaErrores
                                                {
                                                    Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                    NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                    Error = "Error al recuperar sueldo. Perfil del colaborador incompleto o incorrecto",
                                                    PosibleSolucion = "Verifique que la información del perfil del colaborador esté completa y/o correcta."

                                                });
                                                errores++;
                                            }

                                            // horas normales trabajadas
                                            horasTrabajadas = db.tbHistorialHorasTrabajadas
                                                                .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                       x.htra_Estado == true &&
                                                                       x.tbTipoHoras.tiho_Recargo == 0 &&
                                                                       x.htra_Fecha >= fechaInicio &&
                                                                       x.htra_Fecha <= fechaFin)
                                                                .Select(x => x.htra_CantidadHoras)
                                                                .DefaultIfEmpty(0)
                                                                .Sum();


                                            // salario por hora
                                            try
                                            {
                                                salarioHora = SalarioBase / 240;
                                            }
                                            catch (Exception ex)
                                            {
                                                listaErrores.Add(new ViewModelListaErrores
                                                {
                                                    Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                    NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                    Error = "Error al calcular sueldo por hora. Perfil del colaborador incompleto o incorrecto",
                                                    PosibleSolucion = "Verifique que perfil del colaborador (sueldo) esté completo y/o correcto."

                                                });
                                                errores++;
                                            }


                                            // total salario o sueldo bruto
                                            try
                                            {
                                                totalSalario = Math.Round((Decimal)salarioHora * horasTrabajadas, 2);

                                                // agregar total salario al voucher
                                                ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                {
                                                    concepto = "Salario ordinario",
                                                    monto = totalSalario
                                                });
                                            }
                                            catch (Exception ex)
                                            {
                                                listaErrores.Add(new ViewModelListaErrores
                                                {
                                                    Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                    NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                    Error = "Error al calcular sueldo bruto.",
                                                    PosibleSolucion = "Verifique que el sueldo del colaborador esté completo y/o correcto."

                                                });
                                                errores++;
                                            }




                                            // horas con permiso justificado
                                            List<tbHistorialPermisos> horasConPermiso = db.tbHistorialPermisos
                                                                                          .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                                 x.hper_Estado == true &&
                                                                                                 x.hper_fechaInicio >= fechaInicio &&
                                                                                                 x.hper_fechaFin <= fechaFin)
                                                                                          .ToList();

                                            if (horasConPermiso.Count > 0)
                                            {
                                                // sumar todas las horas extras
                                                try
                                                {
                                                    int CantidadHorasPermisoActual = 0;
                                                    foreach (var iterHorasPermiso in horasConPermiso)
                                                    {
                                                        CantidadHorasPermisoActual = iterHorasPermiso.hper_Duracion;

                                                        totalHorasPermiso += CantidadHorasPermisoActual * (((iterHorasPermiso.hper_PorcentajeIndemnizado * salarioHora) / 100));

                                                        // para el voucher
                                                        ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                        {
                                                            concepto = $"{CantidadHorasPermisoActual} horas al {iterHorasPermiso.hper_PorcentajeIndemnizado} %",
                                                            monto = Math.Round(CantidadHorasPermisoActual * (((iterHorasPermiso.hper_PorcentajeIndemnizado * salarioHora) / 100)), 2)
                                                        });

                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    listaErrores.Add(new ViewModelListaErrores
                                                    {
                                                        Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                        NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                        Error = "Error al procesar horas con permiso.",
                                                        PosibleSolucion = "Verifique que las horas con permiso sean correctas."

                                                    });
                                                    errores++;
                                                }

                                            }

                                            // comisiones
                                            List<tbEmpleadoComisiones> oComisionesColaboradores = db.tbEmpleadoComisiones
                                                                                                    .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                                           x.cc_Activo == true &&
                                                                                                           x.cc_Pagado == false &&
                                                                                                           x.cc_FechaRegistro >= fechaInicio &&
                                                                                                           x.cc_FechaRegistro <= fechaFin)
                                                                                                    .ToList();
                                            if (oComisionesColaboradores.Count > 0)
                                            {

                                                // sumar todas las comisiones
                                                try
                                                {
                                                    foreach (var oComisionesColaboradoresIterador in oComisionesColaboradores)
                                                    {
                                                        try
                                                        {
                                                            totalComisiones += oComisionesColaboradoresIterador.cc_TotalComision;

                                                            // pasar el estado de las comisiones a pagadas
                                                            oComisionesColaboradoresIterador.cc_Pagado = true;
                                                            oComisionesColaboradoresIterador.cc_FechaPagado = DateTime.Now;
                                                            db.Entry(oComisionesColaboradoresIterador).State = EntityState.Modified;

                                                            // agregarlas al vocher
                                                            ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                            {
                                                                concepto = oComisionesColaboradoresIterador.tbCatalogoDeIngresos.cin_DescripcionIngreso,
                                                                monto = Math.Round(oComisionesColaboradoresIterador.cc_TotalComision, 2)
                                                            });

                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            listaErrores.Add(new ViewModelListaErrores
                                                            {
                                                                Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                                NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                                Error = $"Error al procesar comisión número {oComisionesColaboradoresIterador.cc_Id}, con total venta: {oComisionesColaboradoresIterador.cc_TotalVenta}, total comsión: {oComisionesColaboradoresIterador.cc_TotalComision}.",
                                                                PosibleSolucion = "Verifique que laa comisión fue registradaa al colaborador de forma correcta."

                                                            });
                                                            errores++;
                                                        }

                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    listaErrores.Add(new ViewModelListaErrores
                                                    {
                                                        Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                        NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                        Error = "Error al procesar las comisiones del colaborador.",
                                                        PosibleSolucion = "Verifique que las comisiones registradas al colaborador sean las correctas."

                                                    });
                                                    errores++;
                                                }
                                            }

                                            // horas extras
                                            horasExtrasTrabajadas = db.tbHistorialHorasTrabajadas
                                                                    .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                            x.htra_Estado == true &&
                                                                            x.tbTipoHoras.tiho_Recargo > 0 &&
                                                                            x.htra_Fecha >= fechaInicio &&
                                                                            x.htra_Fecha <= fechaFin)
                                                                    .Select(x => x.htra_CantidadHoras)
                                                                    .DefaultIfEmpty(0)
                                                                    .Sum();

                                            if (horasExtrasTrabajadas > 0)
                                            {
                                                // para el voucer
                                                ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                {
                                                    concepto = "Horas extras",
                                                    monto = horasExtrasTrabajadas
                                                });
                                            }

                                            // total ingresos horas extras
                                            List<tbHistorialHorasTrabajadas> oHorasExtras = db.tbHistorialHorasTrabajadas
                                                                                            .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                                   x.htra_Estado == true &&
                                                                                                   x.tbTipoHoras.tiho_Recargo > 0 &&
                                                                                                   x.htra_Fecha >= fechaInicio &&
                                                                                                   x.htra_Fecha <= fechaFin)
                                                                                            .ToList();
                                            if (oHorasExtras.Count > 0)
                                            {
                                                int CantidadHorasExtrasActual = 0;

                                                try
                                                {
                                                    // sumar todas las horas extras
                                                    foreach (var iterHorasExtras in oHorasExtras)
                                                    {
                                                        try
                                                        {
                                                            CantidadHorasExtrasActual = db.tbHistorialHorasTrabajadas
                                                                                    .Where(x => x.emp_Id == empleadoActual.emp_Id && x.htra_Estado == true && x.htra_Id == iterHorasExtras.htra_Id)
                                                                                    .Select(x => x.htra_CantidadHoras)
                                                                                    .DefaultIfEmpty(0)
                                                                                    .Sum();

                                                            totalHorasExtras += Math.Round(CantidadHorasExtrasActual * (salarioHora + ((iterHorasExtras.tbTipoHoras.tiho_Recargo * salarioHora) / 100)), 2);


                                                            // para el voucher
                                                            ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                            {
                                                                concepto = $"{CantidadHorasExtrasActual} horas {iterHorasExtras.tbTipoHoras.tiho_Descripcion} al {iterHorasExtras.tbTipoHoras.tiho_Recargo} %",
                                                                monto = Math.Round(CantidadHorasExtrasActual * (salarioHora + ((iterHorasExtras.tbTipoHoras.tiho_Recargo * salarioHora) / 100)), 2)
                                                            });
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            listaErrores.Add(new ViewModelListaErrores
                                                            {
                                                                Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                                NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                                Error = $"Error al cargar horas extras registradas el dia {iterHorasExtras.htra_Fecha}, cantidad: {iterHorasExtras.htra_CantidadHoras}.",
                                                                PosibleSolucion = "Verifique que las horas registradas al colaborador sean las correctas."

                                                            });
                                                            errores++;
                                                        }

                                                    }
                                                    // para el voucher
                                                    ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                    {
                                                        concepto = "Total horas extras",
                                                        monto = totalHorasExtras
                                                    });
                                                }
                                                catch (Exception ex)
                                                {
                                                    listaErrores.Add(new ViewModelListaErrores
                                                    {
                                                        Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                        NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                        Error = "Error al calcular horas extras.",
                                                        PosibleSolucion = "Verifique que las horas registradas al colaborador sean las correctas."

                                                    });
                                                    errores++;
                                                }
                                            }

                                            // bonos del colaborador
                                            List<tbEmpleadoBonos> oBonosColaboradores = db.tbEmpleadoBonos
                                                                                        .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                               x.cb_Activo == true &&
                                                                                               x.cb_Pagado == false &&
                                                                                               x.cb_FechaRegistro >= fechaInicio &&
                                                                                               x.cb_FechaRegistro <= fechaFin)
                                                                                        .ToList();


                                            if (oBonosColaboradores.Count > 0)
                                            {

                                                try
                                                {
                                                    cantidadUnidadesBonos = oBonosColaboradores.Count;

                                                    // iterar los bonos
                                                    foreach (var oBonosColaboradoresIterador in oBonosColaboradores)
                                                    {
                                                        try
                                                        {
                                                            totalBonificaciones += Math.Round(oBonosColaboradoresIterador.cb_Monto.Value, 2);

                                                            // pasar el bono a pagado
                                                            oBonosColaboradoresIterador.cb_Pagado = true;
                                                            oBonosColaboradoresIterador.cb_FechaPagado = DateTime.Now;
                                                            db.Entry(oBonosColaboradoresIterador).State = EntityState.Modified;

                                                            // agregarlo al voucher
                                                            ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                            {
                                                                concepto = oBonosColaboradoresIterador.tbCatalogoDeIngresos.cin_DescripcionIngreso,
                                                                monto = Math.Round(oBonosColaboradoresIterador.cb_Monto.Value, 2)
                                                            });

                                                            // Historial de ingresos (bonos)
                                                            lisHistorialIngresos.Add(new tbHistorialDeIngresosPago
                                                            {
                                                                hip_UnidadesPagar = 1,
                                                                hip_MedidaUnitaria = 1,
                                                                hip_TotalPagar = Math.Round(oBonosColaboradoresIterador.cb_Monto.Value, 2),
                                                                cin_IdIngreso = oBonosColaboradoresIterador.cin_IdIngreso
                                                            });
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            listaErrores.Add(new ViewModelListaErrores
                                                            {
                                                                Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                                NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                                Error = $"Error al cargar bono número {oBonosColaboradoresIterador.cb_Id}, con monto: {oBonosColaboradoresIterador.cb_Monto}.",
                                                                PosibleSolucion = "Verifique que el bono registrado al colaborador esté completo y/o correcto."

                                                            });
                                                            errores++;
                                                        }

                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    listaErrores.Add(new ViewModelListaErrores
                                                    {
                                                        Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                        NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                        Error = "Error al calcular bonos.",
                                                        PosibleSolucion = "Verifique que los bonos registrados al colaborador esté completos y/o correctos."

                                                    });
                                                    errores++;
                                                }
                                            }

                                            // vacaciones
                                            List<tbHistorialVacaciones> oVacacionesColaboradores = db.tbHistorialVacaciones
                                                                                                    .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                                           x.hvac_DiasPagados == false &&
                                                                                                           x.hvac_Estado == true &&
                                                                                                           x.hvac_FechaInicio >= fechaInicio &&
                                                                                                           x.hvac_FechaFin <= fechaFin)
                                                                                                    .ToList();
                                            if (oVacacionesColaboradores.Count > 0)
                                            {

                                                try
                                                {
                                                    // sumar todas las comisiones
                                                    foreach (var oVacacionesColaboradoresIterador in oVacacionesColaboradores)
                                                    {
                                                        try
                                                        {
                                                            int cantidadDias = 0;
                                                            DateTime VacacionesfechaInicio;
                                                            DateTime VacacionesfechaFin;

                                                            VacacionesfechaInicio = (from tbEmpVac in db.tbHistorialVacaciones
                                                                                     where tbEmpVac.hvac_Id == oVacacionesColaboradoresIterador.hvac_Id
                                                                                     select tbEmpVac.hvac_FechaInicio).FirstOrDefault();

                                                            VacacionesfechaFin = (from tbEmpVac in db.tbHistorialVacaciones
                                                                                  where tbEmpVac.hvac_Id == oVacacionesColaboradoresIterador.hvac_Id
                                                                                  select tbEmpVac.hvac_FechaFin).FirstOrDefault();

                                                            TimeSpan restaFechas = VacacionesfechaFin - VacacionesfechaInicio;
                                                            cantidadDias = restaFechas.Days;

                                                            totalVacaciones += Math.Round((salarioHora * 8) * cantidadDias, 2);

                                                            // cambiar el estado de las vacaciones a pagadas
                                                            oVacacionesColaboradoresIterador.hvac_DiasPagados = true;
                                                            db.Entry(oVacacionesColaboradoresIterador).State = EntityState.Modified;

                                                            // agregarlas al vocher
                                                            ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                            {
                                                                concepto = $"{cantidadDias} dias de vacaciones",
                                                                monto = Math.Round((salarioHora * 8) * cantidadDias, 2)
                                                            });
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            listaErrores.Add(new ViewModelListaErrores
                                                            {
                                                                Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                                NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                                Error = $"Error al cargar vaciones registradas {oVacacionesColaboradoresIterador.hvac_FechaCrea}, con fecha de inicio: {oVacacionesColaboradoresIterador.hvac_FechaInicio} y fecha fin {oVacacionesColaboradoresIterador.hvac_FechaFin}.",
                                                                PosibleSolucion = "Verifique que los rangos de fecha registrados son correctos."

                                                            });
                                                            errores++;
                                                        }

                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    listaErrores.Add(new ViewModelListaErrores
                                                    {
                                                        Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                        NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                        Error = "Error al calcular pago de vaciones.",
                                                        PosibleSolucion = "Verifique que los rangos de fecha registrados son correctos."

                                                    });
                                                    errores++;
                                                }
                                            }

                                            // ingresos individuales
                                            List<tbIngresosIndividuales> oIngresosIndiColaboradores = db.tbIngresosIndividuales
                                                                                                        .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                                               x.ini_Activo == true &&
                                                                                                               x.ini_Pagado != true &&
                                                                                                               x.ini_FechaCrea >= fechaInicio &&
                                                                                                               x.ini_FechaCrea <= fechaFin)
                                                                                                        .ToList();

                                            if (oIngresosIndiColaboradores.Count > 0)
                                            {
                                                try
                                                {
                                                    //iterar los bonos
                                                    foreach (var oIngresosIndiColaboradoresIterador in oIngresosIndiColaboradores)
                                                    {
                                                        try
                                                        {
                                                            totalIngresosIndivuales += Math.Round(oIngresosIndiColaboradoresIterador.ini_Monto.Value, 2);

                                                            //pasar el bono a pagado
                                                            oIngresosIndiColaboradoresIterador.ini_Pagado = true;
                                                            oIngresosIndiColaboradoresIterador.ini_FechaModifica = DateTime.Now;
                                                            db.Entry(oIngresosIndiColaboradoresIterador).State = EntityState.Modified;

                                                            //agregarlo al voucher
                                                            ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                            {
                                                                concepto = oIngresosIndiColaboradoresIterador.ini_Motivo,
                                                                monto = Math.Round(oIngresosIndiColaboradoresIterador.ini_Monto.Value, 2)
                                                            });
                                                        }
                                                        catch(Exception ex)
                                                        {
                                                            listaErrores.Add(new ViewModelListaErrores
                                                            {
                                                                Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                                NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                                Error = $"Error al procesar ingreso inidividual número {oIngresosIndiColaboradoresIterador.ini_IdIngresosIndividuales}, con motivo {oIngresosIndiColaboradoresIterador.ini_Motivo} y monto: {oIngresosIndiColaboradoresIterador.ini_Monto}.",
                                                                PosibleSolucion = "Verifique la información de dichos ingresos sea correcta."

                                                            });
                                                            errores++;
                                                        }
                                                    }
                                                }
                                                catch (Exception ex)
                                                {

                                                }
                                            }

                                            #region Septimo Dia
                                            DateTime inicioFecha = fechaInicio;
                                            DateTime finFecha = fechaFin;
                                            TimeSpan restaFechasSeptimo = finFecha - inicioFecha;
                                            int cantidadDiasSeptimo = restaFechasSeptimo.Days + 1;
                                            DateTime fechaIterador = inicioFecha;
                                            int cantHoras = 0;
                                            int cantHorasPermiso = 0;
                                            int cantidadSeptimosDias = 0;
                                            int contadorSeptimosDias = 1;
                                            decimal resultSeptimoDia = 0;

                                            try
                                            {
                                                for (int i = 1; i <= cantidadDiasSeptimo; i++)
                                                {
                                                    if (fechaIterador.DayOfWeek.ToString() != "Sunday")
                                                    {
                                                        cantHoras += db.tbHistorialHorasTrabajadas
                                                                    .Where(x => x.htra_Fecha == fechaIterador &&
                                                                           x.emp_Id == empleadoActual.emp_Id &&
                                                                           x.htra_Estado == true)
                                                                    .Select(x => x.htra_CantidadHoras)
                                                                    .FirstOrDefault();

                                                        cantHorasPermiso += db.tbHistorialPermisos
                                                                            .Where(x => x.hper_fechaInicio <= fechaIterador &&
                                                                                   x.hper_fechaFin >= fechaIterador &&
                                                                                   x.emp_Id == empleadoActual.emp_Id)
                                                                            .Select(x => x.hper_Duracion)
                                                                            .FirstOrDefault();

                                                        if ((cantHoras + (cantHorasPermiso * 8)) >= 48 && contadorSeptimosDias == 7)
                                                        {
                                                            cantidadSeptimosDias++;
                                                            contadorSeptimosDias = 0;
                                                            cantHoras = 0;
                                                        }
                                                    }
                                                    if (contadorSeptimosDias == 7)
                                                    {
                                                        cantHoras = 0;
                                                        contadorSeptimosDias = 0;
                                                    }
                                                    fechaIterador = fechaIterador.Add(new TimeSpan(1, 0, 0, 0, 0));
                                                    contadorSeptimosDias++;
                                                }

                                                resultSeptimoDia = (salarioHora * 8) * cantidadSeptimosDias;
                                                if (resultSeptimoDia > 0)
                                                {
                                                    // agregarlas al vocher
                                                    ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                                                    {
                                                        concepto = $"{cantidadSeptimosDias}x séptimo día",
                                                        monto = Math.Round(resultSeptimoDia, 2)
                                                    });
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                listaErrores.Add(new ViewModelListaErrores
                                                {
                                                    Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                    NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                    Error = "Error al calcular séptimo día.",
                                                    PosibleSolucion = "Verifique que la información en el historial de horas trabajadas del colaborador esté correcta."

                                                });
                                                errores++;
                                            }


                                            #endregion

                                            // total ingresos
                                            totalIngresosEmpleado = totalIngresosIndivuales + totalSalario + totalComisiones + totalHorasExtras + totalBonificaciones + totalVacaciones + totalHorasPermiso + resultSeptimoDia;

                                            #endregion

                                            #region Procesar deducciones

                                           if (oDeducciones.Count > 0)
                                            {
                                                // deducciones de la planilla
                                                foreach (var iterDeducciones in oDeducciones)
                                                {
                                                    decimal? porcentajeColaborador = iterDeducciones.cde_PorcentajeColaborador;
                                                    decimal? porcentajeEmpresa = iterDeducciones.cde_PorcentajeEmpresa;
                                                    decimal? montoDeduccionColaborador = SalarioBase;

                                                    try
                                                    {
                                                        // verificar techos deducciones
                                                        List<tbTechosDeducciones> oTechosDeducciones = db.tbTechosDeducciones
                                                                                                         .Where(x => x.cde_IdDeducciones == iterDeducciones.cde_IdDeducciones &&
                                                                                                                x.tddu_Activo == true)
                                                                                                         .OrderBy(x => x.tddu_Techo)
                                                                                                         .ToList();
                                                        if (oTechosDeducciones.Count() > 0)
                                                        {
                                                            foreach (var techosDeduccionesIter in oTechosDeducciones)
                                                            {
                                                                if (SalarioBase > techosDeduccionesIter.tddu_Techo)
                                                                {
                                                                    montoDeduccionColaborador = techosDeduccionesIter.tddu_Techo;
                                                                    porcentajeColaborador = techosDeduccionesIter.tddu_PorcentajeColaboradores;
                                                                    porcentajeEmpresa = techosDeduccionesIter.tddu_PorcentajeEmpresa;
                                                                }
                                                            }
                                                        }
                                                        //sumar las deducciones
                                                        colaboradorDeducciones += Math.Round((decimal)(montoDeduccionColaborador * porcentajeColaborador) / 100, 2);

                                                        //Voucher
                                                        ListaDeduccionesVoucher.Add(new IngresosDeduccionesVoucher
                                                        {
                                                            concepto = iterDeducciones.cde_DescripcionDeduccion,
                                                            monto = Math.Round((decimal)(SalarioBase * porcentajeColaborador) / 100, 2)
                                                        });

                                                        //Historial de deducciones
                                                        lisHistorialDeducciones.Add(new tbHistorialDeduccionPago
                                                        {
                                                            cde_IdDeducciones = iterDeducciones.cde_IdDeducciones,
                                                            hidp_Total = Math.Round((decimal)(SalarioBase * porcentajeColaborador) / 100, 2)
                                                        });
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        listaErrores.Add(new ViewModelListaErrores
                                                        {
                                                            Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                            Error = $"Error al procesar deducción {iterDeducciones.cde_DescripcionDeduccion}.",
                                                            PosibleSolucion = "Verifique que la información de esta deducción y sus respectivos techos (si los tiene) esté completa y/o correcta."

                                                        });
                                                        errores++;
                                                    }
                                                }
                                            }


                                            //instituciones financieras
                                            List<tbDeduccionInstitucionFinanciera> oDeduInstiFinancieras = db.tbDeduccionInstitucionFinanciera
                                                                                                            .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                                                   x.deif_Activo == true &&
                                                                                                                   x.deif_Pagado == false &&
                                                                                                                   x.deif_FechaCrea >= fechaInicio &&
                                                                                                                   x.deif_FechaCrea <= fechaFin)
                                                                                                            .ToList();

                                            if (oDeduInstiFinancieras.Count > 0)
                                            {
                                                // sumarlas todas
                                                foreach (var oDeduInstiFinancierasIterador in oDeduInstiFinancieras)
                                                {

                                                    try
                                                    {
                                                        totalInstitucionesFinancieras += Math.Round((decimal)oDeduInstiFinancierasIterador.deif_Monto, 2);
                                                        // pasar el estado de la deduccion a pagada
                                                        oDeduInstiFinancierasIterador.deif_Pagado = true;
                                                        db.Entry(oDeduInstiFinancierasIterador).State = EntityState.Modified;

                                                        // agregar al voucher
                                                        ListaDeduccionesVoucher.Add(new IngresosDeduccionesVoucher
                                                        {
                                                            concepto = $"{oDeduInstiFinancierasIterador.tbInstitucionesFinancieras.insf_DescInstitucionFinanc} {oDeduInstiFinancierasIterador.deif_Comentarios}",
                                                            monto = Math.Round((decimal)oDeduInstiFinancierasIterador.deif_Monto, 2)
                                                        });

                                                        // Historial de deducciones
                                                        lisHistorialDeducciones.Add(new tbHistorialDeduccionPago
                                                        {
                                                            cde_IdDeducciones = oDeduInstiFinancierasIterador.cde_IdDeducciones,
                                                            hidp_Total = Math.Round((decimal)oDeduInstiFinancierasIterador.deif_Monto, 2)
                                                        });
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        listaErrores.Add(new ViewModelListaErrores
                                                        {
                                                            Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                            Error = $"Error al procesar deduccion por institución financiera {oDeduInstiFinancierasIterador.tbInstitucionesFinancieras.insf_DescInstitucionFinanc} - {oDeduInstiFinancierasIterador.deif_Comentarios} - {oDeduInstiFinancierasIterador.deif_Monto}.",
                                                            PosibleSolucion = "Verifique que la información de esta deducción sea correcta de acuerdo al formato leído por el sistema."

                                                        });
                                                        errores++;
                                                    }
                                                }
                                            }
                                            // deducciones afp
                                            List<tbDeduccionAFP> oDeduccionAfp = db.tbDeduccionAFP
                                                                                .Where(af => af.emp_Id == empleadoActual.emp_Id &&
                                                                                       af.dafp_Pagado != true &&
                                                                                       af.dafp_Activo == true &&
                                                                                       af.dafp_FechaCrea >= fechaInicio &&
                                                                                       af.dafp_FechaCrea <= fechaFin)
                                                                                .ToList();

                                            if (oDeduccionAfp.Count > 0)
                                            {
                                                // sumarlas todas
                                                foreach (var oDeduccionAfpIter in oDeduccionAfp)
                                                {
                                                    try
                                                    {
                                                        totalAFP += Math.Round(oDeduccionAfpIter.dafp_AporteLps, 2);

                                                        //pasar el estado del aporte a pagado
                                                        oDeduccionAfpIter.dafp_Pagado = true;
                                                        db.Entry(oDeduccionAfpIter).State = EntityState.Modified;

                                                        ListaDeduccionesVoucher.Add(new IngresosDeduccionesVoucher
                                                        {
                                                            concepto = oDeduccionAfpIter.tbAFP.afp_Descripcion,
                                                            monto = Math.Round(oDeduccionAfpIter.dafp_AporteLps, 2)
                                                        });
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        listaErrores.Add(new ViewModelListaErrores
                                                        {
                                                            Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                            Error = $"Error al procesar deduccion AFP. {oDeduccionAfpIter.tbAFP.afp_Descripcion} - {oDeduccionAfpIter.dafp_AporteLps}",
                                                            PosibleSolucion = "Verifique que la información de esta deducción esté correcta y/o completa."

                                                        });
                                                        errores++;
                                                    }

                                                }
                                            }

                                            // deducciones extras
                                            List<tbDeduccionesExtraordinarias> oDeduccionesExtrasColaborador = db.tbDeduccionesExtraordinarias
                                                                                                                .Where(DEX => DEX.tbEquipoEmpleados.emp_Id == empleadoActual.emp_Id &&
                                                                                                                       DEX.dex_MontoRestante > 0 &&
                                                                                                                       DEX.dex_Activo == true)
                                                                                                                .ToList();

                                            if (oDeduccionesExtrasColaborador.Count > 0)
                                            {
                                                // sumarlas todas
                                                foreach (var oDeduccionesExtrasColaboradorIterador in oDeduccionesExtrasColaborador)
                                                {
                                                    try
                                                    {
                                                        totalOtrasDeducciones += oDeduccionesExtrasColaboradorIterador.dex_MontoRestante <= oDeduccionesExtrasColaboradorIterador.dex_Cuota ? oDeduccionesExtrasColaboradorIterador.dex_MontoRestante : oDeduccionesExtrasColaboradorIterador.dex_Cuota;

                                                        // agregar al comprobante de pago
                                                        ListaDeduccionesVoucher.Add(new IngresosDeduccionesVoucher
                                                        {
                                                            concepto = oDeduccionesExtrasColaboradorIterador.dex_ObservacionesComentarios,
                                                            monto = Math.Round((decimal)oDeduccionesExtrasColaboradorIterador.dex_Cuota, 2)
                                                        });

                                                        // restar la cuota al monto restante
                                                        oDeduccionesExtrasColaboradorIterador.dex_MontoRestante = oDeduccionesExtrasColaboradorIterador.dex_MontoRestante <= oDeduccionesExtrasColaboradorIterador.dex_Cuota ? oDeduccionesExtrasColaboradorIterador.dex_MontoRestante - oDeduccionesExtrasColaboradorIterador.dex_MontoRestante : oDeduccionesExtrasColaboradorIterador.dex_MontoRestante - oDeduccionesExtrasColaboradorIterador.dex_Cuota;
                                                        db.Entry(oDeduccionesExtrasColaboradorIterador).State = EntityState.Modified;

                                                        // Historial de deducciones
                                                        lisHistorialDeducciones.Add(new tbHistorialDeduccionPago
                                                        {
                                                            cde_IdDeducciones = oDeduccionesExtrasColaboradorIterador.cde_IdDeducciones,
                                                            hidp_Total = Math.Round((decimal)oDeduccionesExtrasColaboradorIterador.dex_Cuota, 2)
                                                        });
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        listaErrores.Add(new ViewModelListaErrores
                                                        {
                                                            Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                            Error = $"Error al procesar deduccion extra. {oDeduccionesExtrasColaboradorIterador.dex_ObservacionesComentarios} - {oDeduccionesExtrasColaboradorIterador.dex_Cuota}",
                                                            PosibleSolucion = "Verifique que la información de esta deducción esté correcta y/o completa."

                                                        });
                                                        errores++;
                                                    }
                                                }
                                            }

                                            // adelantos de sueldo
                                            List<tbAdelantoSueldo> oAdelantosSueldo = db.tbAdelantoSueldo
                                                                                        .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                               x.adsu_Activo == true && x.adsu_Deducido == false &&
                                                                                               x.adsu_FechaAdelanto >= fechaInicio &&
                                                                                               x.adsu_FechaAdelanto <= fechaFin)
                                                                                        .ToList();

                                            if (oAdelantosSueldo.Count > 0)
                                            {
                                                // sumarlas todas
                                                foreach (var oAdelantosSueldoIterador in oAdelantosSueldo)
                                                {

                                                    try
                                                    {
                                                        adelantosSueldo += Math.Round((decimal)oAdelantosSueldoIterador.adsu_Monto, 2);
                                                        // pasar el estado del adelanto a deducido
                                                        oAdelantosSueldoIterador.adsu_Deducido = true;
                                                        db.Entry(oAdelantosSueldoIterador).State = EntityState.Modified;

                                                        ListaDeduccionesVoucher.Add(new IngresosDeduccionesVoucher
                                                        {
                                                            concepto = $"Adelanto sueldo ({oAdelantosSueldoIterador.adsu_RazonAdelanto})",
                                                            monto = Math.Round((decimal)oAdelantosSueldoIterador.adsu_Monto, 2)
                                                        });
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        listaErrores.Add(new ViewModelListaErrores
                                                        {
                                                            Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                            Error = $"Error al procesar adelanto de sueldo. {oAdelantosSueldoIterador.adsu_RazonAdelanto} - {oAdelantosSueldoIterador.adsu_Monto}",
                                                            PosibleSolucion = "Verifique que la información de dicho adelanto esté completa y/o correcta."

                                                        });
                                                        errores++;
                                                    }
                                                }
                                            }

                                            // deducciones individuales
                                            List<tbDeduccionesIndividuales> oDeduccionesIndiColaborador = db.tbDeduccionesIndividuales
                                                                                                            .Where(DEX => DEX.emp_Id == empleadoActual.emp_Id &&
                                                                                                                   DEX.dei_Monto > 0 &&
                                                                                                                   DEX.dei_Pagado != true &&
                                                                                                                   DEX.dei_Activo == true)
                                                                                                            .ToList();

                                            if (oDeduccionesIndiColaborador.Count > 0)
                                            {
                                                // sumarlas todas
                                                foreach (var oDeduccionesIndiColaboradorIterador in oDeduccionesIndiColaborador)
                                                {
                                                    try
                                                    {
                                                        totalDeduccionesIndividuales += oDeduccionesIndiColaboradorIterador.dei_Monto <= oDeduccionesIndiColaboradorIterador.dei_MontoCuota ? oDeduccionesIndiColaboradorIterador.dei_MontoCuota : oDeduccionesIndiColaboradorIterador.dei_MontoCuota;

                                                        // agregar al comprobante de pago
                                                        ListaDeduccionesVoucher.Add(new IngresosDeduccionesVoucher
                                                        {
                                                            concepto = oDeduccionesIndiColaboradorIterador.dei_Motivo,
                                                            monto = oDeduccionesIndiColaboradorIterador.dei_Monto <= oDeduccionesIndiColaboradorIterador.dei_Monto ? Math.Round((decimal)oDeduccionesIndiColaboradorIterador.dei_MontoCuota, 2) : Math.Round((decimal)oDeduccionesIndiColaboradorIterador.dei_MontoCuota, 2)
                                                        });

                                                        // restar la cuota al monto restante
                                                        oDeduccionesIndiColaboradorIterador.dei_Monto = oDeduccionesIndiColaboradorIterador.dei_Monto <= oDeduccionesIndiColaboradorIterador.dei_Monto ? oDeduccionesIndiColaboradorIterador.dei_Monto - oDeduccionesIndiColaboradorIterador.dei_MontoCuota : oDeduccionesIndiColaboradorIterador.dei_MontoCuota - oDeduccionesIndiColaboradorIterador.dei_MontoCuota;
                                                        db.Entry(oDeduccionesIndiColaboradorIterador).State = EntityState.Modified;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        listaErrores.Add(new ViewModelListaErrores
                                                        {
                                                            Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                            Error = $"Error al procesar deducción individual. {oDeduccionesIndiColaboradorIterador.dei_Motivo} - {oDeduccionesIndiColaboradorIterador.dei_MontoCuota}",
                                                            PosibleSolucion = "Verifique que la de dicha deducción esté completa y/o correcta."

                                                        });
                                                        errores++;
                                                    }
                                                }
                                            }


                                            // totales
                                            totalDeduccionesEmpleado = Math.Round((decimal)totalOtrasDeducciones, 2) + totalInstitucionesFinancieras + colaboradorDeducciones + totalAFP + adelantosSueldo + totalDeduccionesIndividuales;
                                            netoAPagarColaborador = totalIngresosEmpleado - totalDeduccionesEmpleado;

											#endregion

											#region Cálculo de ISR

											#region Declaracion de Variables
											int AnioActual = DateTime.Now.Year;
											decimal? TotalBonos = 0;
											decimal? TotalHrsExtra = 0;
											decimal? TotalComisiones = 0;
											decimal? TotalIngresosExtras = 0;
											decimal? TotalDeduccionesEquipoTrabajo = 0;
											decimal? TotalDeduccionesExtras = 0;
											decimal? TotalDeduccionesAFP = 0;
											decimal? TotalDeduccionesInstitucionesFinancieras = 0;
											decimal? ExcesoDecimoTercer = 0;
											decimal? ExcesoVacaciones = 0;
											decimal? ExcesoDecimoCuarto = 0;
											decimal Exceso = 0;
											decimal SueldoAnual = 0;
											decimal? AcumuladosISR = 0;
											decimal TotalIngresosGravables = 0;
											decimal TotalDeduccionesGravables = 0;
											decimal RentaNetaGravable = 0;
											decimal SalarioMinimo = db.tbEmpresas.Select(x => x.empr_SalarioMinimo).FirstOrDefault() ?? 0;
											var tablaEmp = db.tbSueldos.Where(x => x.emp_Id == empleadoActual.emp_Id).OrderBy(x => x.sue_FechaCrea);
											#endregion

											#region Sueldo Promedio Anual
											//Sueldo redondeado del Colaborador
											DateTime AnioActualEnero = new DateTime(DateTime.Now.Year, 1, 1);

											//Obtener los pagos mensuales totales
											var mesesPago = (db.tbHistorialDePago
												.Where(x => x.emp_Id == empleadoActual.emp_Id && x.hipa_Anio == AnioActualEnero.Year)
												.OrderBy(x => x.hipa_Mes)
												.GroupBy(x => x.hipa_Mes)
												.Select(x => x.Sum(y => (Decimal)y.hipa_TotalSueldoBruto))).ToList();

											DateTime FechaIngresoEmpleado = db.tbEmpleados
																			.Where(x => x.emp_Id == empleadoActual.emp_Id)
																			.Select(x => x.emp_Fechaingreso).FirstOrDefault();
											bool esMensual = true;

											TimeSpan diferencia = AnioActualEnero - FechaIngresoEmpleado;

											if (TimeSpan.Zero > diferencia)
												esMensual = true;


											//Saber que mes entro
											int mes = FechaIngresoEmpleado.Month;
											decimal SalarioPromedioAnualPagadoAlAnio = 0;
											decimal salarioPromedioAnualPagadoAlMes = 0;
											decimal TotalSalarioAnual = SalarioPromedioAnualISR(totalSalario,
											mesesPago,
											esMensual,
											ref SalarioPromedioAnualPagadoAlAnio,
											ref salarioPromedioAnualPagadoAlMes);
											#endregion

											#region Excesos
											////-----------------------------------------------------------------------------------------------------------------------------
											//Exceso Décimo Tercer Mes
											//Variable para los empleados con Décimo Tercer Mes
											var DecimoTercer = db.V_DecimoTercerMes_Pagados.Where(x => x.emp_Id == empleadoActual.emp_Id && x.dtm_FechaPago.Year == AnioActual).FirstOrDefault();

											//Validar primero si es en el año actual el proceso de este calculo
											if (DecimoTercer != null)
											{
												//Salario Mínimo Mensual por 10 Meses (Según SAR)
												Exceso = SalarioMinimo * 10;

												//Validar si el Décimo Tercer es mayor al Exceso
												if (DecimoTercer.dtm_Monto > Exceso)
												{
													ExcesoDecimoTercer = DecimoTercer.dtm_Monto - Exceso;
												}
												else
												{
													ExcesoDecimoTercer = 0;
												}
											}
											////-----------------------------------------------------------------------------------------------------------------------------


											////-----------------------------------------------------------------------------------------------------------------------------
											//Exceso Décimo Cuarto Mes
											//Variable para los empleados con Décimo Cuarto Mes
											var DecimoCuarto = db.V_DecimoCuartoMes_Pagados.Where(x => x.emp_Id == empleadoActual.emp_Id && x.dcm_FechaPago.Year == AnioActual).FirstOrDefault();

											//Validar primero si es en el año actual el proceso de este calculo
											if (DecimoCuarto != null)
											{
												//Salario Mínimo Mensual por 10 Meses (Según SAR)
												Exceso = SalarioMinimo * 10;

												//Validar si el Décimo Cuarto es mayor al Exceso
												if (DecimoCuarto.dcm_Monto > Exceso)
												{
													ExcesoDecimoCuarto = DecimoCuarto.dcm_Monto - Exceso;
												}
												else
												{
													ExcesoDecimoCuarto = 0;
												}
											}
											////-----------------------------------------------------------------------------------------------------------------------------


											////-----------------------------------------------------------------------------------------------------------------------------
											//Exceso Vacaciones
											//Variable para las Vacaciones Pagadas del Historial de Ingresos de Pago
											var objVacaciones = db.tbHistorialVacaciones.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hvac_AnioVacaciones && x.hvac_DiasPagados == true).Select(x => x.hvac_CantDias).FirstOrDefault();

											//Validar si los dias a Pagar es mayor a 30 dias 
											if (objVacaciones > 30)
											{
												ExcesoVacaciones = ((objVacaciones - 30) * (SueldoAnual / 360));
											}
											else
											{
												ExcesoVacaciones = 0;
											}

											#endregion

											#region Acumulados ISR

											//Variable para obtener los registros de los Acumulados ISR del empleado actual
											AcumuladosISR = db.tbAcumuladosISR.Where(x => x.aisr_Activo == true && x.emp_Id == empleadoActual.emp_Id).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.aisr_Monto)).FirstOrDefault();

											#endregion

											#region Otros Ingresos

											TotalBonos = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_TotalBonos)).FirstOrDefault();
											TotalHrsExtra = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_TotalHorasExtras)).FirstOrDefault();
											TotalComisiones = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_TotalComisiones)).FirstOrDefault();
											TotalIngresosExtras = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_TotalIngresosIndividuales)).FirstOrDefault();

											if (TotalBonos == null)
												TotalBonos = 0;

											if (TotalHrsExtra == null)
												TotalHrsExtra = 0;

											if (TotalComisiones == null)
												TotalComisiones = 0;

											if (TotalIngresosExtras == null)
												TotalIngresosExtras = 0;

											#endregion

											#region Otras Deducciones (Posible cálculo para el ISR)

											#region Deducciones por Equipo de Trabajo
											//DET = Deducción por Equipo de Trabajo
											decimal? MontoInicialDET = 0;
											decimal? MontoRestanteDET = 0;
											List<tbDeduccionesExtraordinarias> objDeduccionesExtraordinarias = db.tbDeduccionesExtraordinarias.Where(x => x.tbEquipoEmpleados.emp_Id == empleadoActual.emp_Id && x.dex_Activo == true && x.dex_DeducirISR == true)
																																			  .ToList();
											if (objDeduccionesExtraordinarias.Count() > 0)
											{
												foreach (var oDET in objDeduccionesExtraordinarias)
												{
													MontoInicialDET = MontoInicialDET + oDET.dex_MontoInicial;
													MontoRestanteDET = MontoRestanteDET + oDET.dex_MontoRestante;
												}
											}
											if (MontoInicialDET > 0 && MontoRestanteDET > 0)
											{
												TotalDeduccionesEquipoTrabajo = MontoInicialDET - MontoRestanteDET;
											}
											else
											{
												TotalDeduccionesEquipoTrabajo = 0;
											}
											#endregion

											#region Deducciones Extras
											List<tbDeduccionesIndividuales> objDeduccionIndividual = db.tbDeduccionesIndividuales.Where(x => x.emp_Id == empleadoActual.emp_Id && x.dei_Activo == true && x.dei_DeducirISR == true)
																																 .ToList();
											if (objDeduccionIndividual.Count() > 0)
											{
												foreach (var oDeduIndi in objDeduccionIndividual)
												{
													if (oDeduIndi.dei_DeducirISR == true)
													{
														TotalDeduccionesExtras = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_TotalDeduccionesIndividuales)).FirstOrDefault();
													}
												}
											}
											#endregion

											#region Deducciones AFP
											List<tbDeduccionAFP> objDeduccionAFP = db.tbDeduccionAFP.Where(x => x.emp_Id == empleadoActual.emp_Id && x.dafp_Activo == true && x.dafp_DeducirISR == true)
																									.ToList();
											if (objDeduccionAFP.Count() > 0)
											{
												foreach (var oDeduAFP in objDeduccionAFP)
												{
													if (oDeduAFP.dafp_DeducirISR == true)
													{
														TotalDeduccionesAFP = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_AFP)).FirstOrDefault();
													}
												}
											}
											#endregion

											#region Deducciones Instituciones Financieras
											//DIF = Deducción Institución Financiera
											List<tbDeduccionInstitucionFinanciera> objDeduccionInstitucionFinanciera = db.tbDeduccionInstitucionFinanciera.Where(x => x.emp_Id == empleadoActual.emp_Id && x.deif_Activo == true && x.deif_DeducirISR == true)
																																						  .ToList();
											if (objDeduccionInstitucionFinanciera.Count() > 0)
											{
												foreach (var oDIF in objDeduccionInstitucionFinanciera)
												{
													if (oDIF.deif_Monto > 0)
													{
														TotalDeduccionesInstitucionesFinancieras = TotalDeduccionesInstitucionesFinancieras + oDIF.deif_Monto;
													}
													else
													{
														TotalDeduccionesInstitucionesFinancieras = 0;
													}
												}
											}
											#endregion

											if (TotalDeduccionesEquipoTrabajo == null)
												TotalDeduccionesEquipoTrabajo = 0;

											if (TotalDeduccionesExtras == null)
												TotalDeduccionesExtras = 0;

											if (TotalDeduccionesAFP == null)
												TotalDeduccionesAFP = 0;

											if (TotalDeduccionesInstitucionesFinancieras == null)
												TotalDeduccionesInstitucionesFinancieras = 0;

											#endregion

											#region Calculo del ISR
											//-----------------------------------------------------------------------------------------------------------------------------
											//Total Ingresos Gravables
											TotalIngresosGravables = TotalSalarioAnual + (Decimal)ExcesoDecimoTercer + (Decimal)ExcesoDecimoCuarto + (Decimal)ExcesoVacaciones + (Decimal)TotalBonos + (Decimal)TotalHrsExtra + (Decimal)TotalComisiones + (Decimal)TotalIngresosExtras;

											//Total Deducciones Gravables
											TotalDeduccionesGravables = (Decimal)AcumuladosISR + (Decimal)TotalDeduccionesEquipoTrabajo + (Decimal)TotalDeduccionesExtras + (Decimal)TotalDeduccionesAFP + (Decimal)TotalDeduccionesInstitucionesFinancieras;

											//Renta Neta Gravable
											RentaNetaGravable = TotalIngresosGravables - TotalDeduccionesGravables;


											#region Tabla Progresiva ISR
											//Tabla Progresiva ISR

											//Variable para validar que entre primero en la primera parte de la fórmula del ISR y luego en la segunda parte
											string VI = "FirstRange";

											//Variable de tipo Lista para obtener los registros de la tabla progresiva de mayor a menor
											List<tbISR> objDeduccionISR = db.tbISR.Where(x => x.isr_Activo == true)
																				  .OrderByDescending(x => x.isr_RangoInicial)
																				  .ToList();

											foreach (var oISR in objDeduccionISR)
											{
												if (objDeduccionISR.Count() > 0)
												{
													if (RentaNetaGravable >= oISR.isr_RangoInicial)
													{
														if (VI == "FirstRange")
														{
															totalISR = totalISR + (RentaNetaGravable - oISR.isr_RangoInicial) * (oISR.isr_Porcentaje / 100);
															VI = "No";
														}
														else if (VI == "No")
														{
															VI = "SecondRange";
														}
														if (VI == "SecondRange")
														{
															totalISR = totalISR + (oISR.isr_RangoFinal - oISR.isr_RangoInicial) * (oISR.isr_Porcentaje / 100);
															VI = "No";
														}
													}
													else
													{
														totalISR = 0;
													}
												}
											}
											if (totalISR > 0)
											{
												totalISR = totalISR / 12;
											}
											#endregion

											#endregion

											#endregion

											#region guardar en el historial de pago

											idHistorialPago = db.tbHistorialDePago
                                                              .Select(x => x.hipa_IdHistorialDePago)
                                                              .DefaultIfEmpty(0)
                                                              .Max();


                                            try
                                            {
                                                tbHistorialDePago oHistorialPagoEncabezado = new tbHistorialDePago();
                                                oHistorialPagoEncabezado.hipa_IdHistorialDePago = idHistorialPago + contador;
                                                oHistorialPagoEncabezado.emp_Id = empleadoActual.emp_Id;
                                                oHistorialPagoEncabezado.hipa_SueldoNeto = Math.Round((decimal)netoAPagarColaborador, 2);
                                                oHistorialPagoEncabezado.hipa_FechaInicio = fechaInicio;
                                                oHistorialPagoEncabezado.hipa_FechaFin = fechaFin;
                                                oHistorialPagoEncabezado.hipa_FechaPago = DateTime.Now;
                                                oHistorialPagoEncabezado.hipa_Anio = DateTime.Now.Year;
                                                oHistorialPagoEncabezado.hipa_Mes = DateTime.Now.Month;
                                                oHistorialPagoEncabezado.peri_IdPeriodo = 1;
                                                oHistorialPagoEncabezado.hipa_UsuarioCrea = 1;
                                                oHistorialPagoEncabezado.hipa_FechaCrea = DateTime.Now;
                                                oHistorialPagoEncabezado.hipa_TotalISR = totalISR;
                                                oHistorialPagoEncabezado.hipa_ISRPendiente = true;
                                                oHistorialPagoEncabezado.hipa_AFP = totalAFP;

                                                //Nuevos campos
                                                oHistorialPagoEncabezado.hipa_TotalHorasConPermisoJustificado = totalHorasPermiso;
                                                oHistorialPagoEncabezado.hipa_TotalComisiones = totalComisiones;
                                                oHistorialPagoEncabezado.hipa_TotalHorasExtras = totalHorasExtras;
                                                oHistorialPagoEncabezado.hipa_TotalVacaciones = totalVacaciones;
                                                oHistorialPagoEncabezado.hipa_TotalSeptimoDia = resultSeptimoDia;
                                                oHistorialPagoEncabezado.hipa_AdelantoSueldo = adelantosSueldo;
                                                oHistorialPagoEncabezado.hipa_TotalSalario = totalSalario;

                                                // nuevos campos
                                                oHistorialPagoEncabezado.hipa_TotalDeduccionesIndividuales = totalDeduccionesIndividuales;
                                                oHistorialPagoEncabezado.hipa_TotalIngresosIndividuales = totalIngresosIndivuales;
                                                oHistorialPagoEncabezado.hipa_TotalSueldoBruto = totalSalario;
                                                oHistorialPagoEncabezado.hipa_CantidadUnidadesHorasExtras = horasExtrasTrabajadas;

                                                oHistorialPagoEncabezado.hipa_CantidadUnidadesBonos = cantidadUnidadesBonos;

                                                oHistorialPagoEncabezado.hipa_TotalBonos = totalBonificaciones;

                                                //Ejecutar el procedimiento almacenado
                                                listHistorialPago = db.UDP_Plani_tbHistorialDePago_Insert(oHistorialPagoEncabezado.emp_Id,
                                                                                                        oHistorialPagoEncabezado.hipa_SueldoNeto,
                                                                                                        oHistorialPagoEncabezado.hipa_FechaInicio,
                                                                                                        oHistorialPagoEncabezado.hipa_FechaFin,
                                                                                                        oHistorialPagoEncabezado.hipa_FechaPago,
                                                                                                        oHistorialPagoEncabezado.hipa_Anio,
                                                                                                        oHistorialPagoEncabezado.hipa_Mes,
                                                                                                        oHistorialPagoEncabezado.peri_IdPeriodo,
                                                                                                        oHistorialPagoEncabezado.hipa_UsuarioCrea,
                                                                                                        oHistorialPagoEncabezado.hipa_FechaCrea,
                                                                                                        oHistorialPagoEncabezado.hipa_TotalISR,
                                                                                                        oHistorialPagoEncabezado.hipa_ISRPendiente,
                                                                                                        oHistorialPagoEncabezado.hipa_AFP,
                                                                                                        // nuevos campos
                                                                                                        oHistorialPagoEncabezado.hipa_TotalHorasConPermisoJustificado,
                                                                                                        oHistorialPagoEncabezado.hipa_TotalComisiones,
                                                                                                        oHistorialPagoEncabezado.hipa_TotalHorasExtras,
                                                                                                        oHistorialPagoEncabezado.hipa_TotalVacaciones,
                                                                                                        oHistorialPagoEncabezado.hipa_TotalSeptimoDia,
                                                                                                        oHistorialPagoEncabezado.hipa_AdelantoSueldo,
                                                                                                        oHistorialPagoEncabezado.hipa_TotalSalario,
                                                                                                        // nuevos campos

                                                                                                        oHistorialPagoEncabezado.hipa_TotalDeduccionesIndividuales,// deducciones individuales
                                                                                                        oHistorialPagoEncabezado.hipa_TotalIngresosIndividuales,// ingresos individuales
                                                                                                        oHistorialPagoEncabezado.hipa_TotalSueldoBruto,// sueldo bruto
                                                                                                        oHistorialPagoEncabezado.hipa_CantidadUnidadesHorasExtras,// cantidad unidades horas extras
                                                                                                        oHistorialPagoEncabezado.hipa_CantidadUnidadesBonos,// cantidad unidades bonos
                                                                                                        oHistorialPagoEncabezado.hipa_TotalBonos, // total bonos
                                                                                                        codigoDePlanillaGenerada // codigo identificador de la planilla
                                                                                                        );

                                                // obtener resultado del procedimiento almacenado
                                                foreach (UDP_Plani_tbHistorialDePago_Insert_Result Resultado in listHistorialPago)
                                                    MensajeError = Resultado.MensajeError;


                                                if (MensajeError.StartsWith("-1"))
                                                {
                                                    // el procedimiento almacenado falló
                                                    errores++;
                                                }
                                                // si el encabezado del historial de pago se registró correctamente, guardar los detalles
                                                else
                                                {
                                                    // guardar en el detalle de deducciones del historial de pago
                                                    foreach (var hisorialDeduccioneIterado in lisHistorialDeducciones)
                                                    {
                                                        int idDetalle = db.tbHistorialDeduccionPago
                                                                          .Select(x => x.hidp_IdHistorialdeDeduPago)
                                                                          .DefaultIfEmpty(0)
                                                                          .Max();

                                                        hisorialDeduccioneIterado.hidp_IdHistorialdeDeduPago = idDetalle + idDetalleDeduccionHisotorialesContador;
                                                        hisorialDeduccioneIterado.hipa_IdHistorialDePago = int.Parse(MensajeError);
                                                        hisorialDeduccioneIterado.hidp_UsuarioCrea = 1;
                                                        hisorialDeduccioneIterado.hidp_FechaCrea = DateTime.Now;
                                                        db.tbHistorialDeduccionPago.Add(hisorialDeduccioneIterado);
                                                        idDetalleDeduccionHisotorialesContador++;

                                                    }
                                                    // guardar en el detalle de ingresos del historial de pago
                                                    foreach (var hisorialIngresosIterado in lisHistorialIngresos)
                                                    {
                                                        int idDetalle = db.tbHistorialDeIngresosPago
                                                                          .Select(x => x.hip_IdHistorialDeIngresosPago)
                                                                          .DefaultIfEmpty(0)
                                                                          .Max();

                                                        hisorialIngresosIterado.hip_IdHistorialDeIngresosPago = idDetalle + idDetalleIngresoHisotorialesContador;
                                                        hisorialIngresosIterado.hipa_IdHistorialDePago = int.Parse(MensajeError);
                                                        hisorialIngresosIterado.hip_FechaInicio = fechaInicio;
                                                        hisorialIngresosIterado.hip_FechaFinal = fechaFin;
                                                        hisorialIngresosIterado.hip_UsuarioCrea = 1;
                                                        hisorialIngresosIterado.hip_FechaCrea = DateTime.Now;
                                                        db.tbHistorialDeIngresosPago.Add(hisorialIngresosIterado);
                                                        idDetalleIngresoHisotorialesContador++;
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                listaErrores.Add(new ViewModelListaErrores
                                                {
                                                    Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                    NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                    Error = "Error al guardar en el historial de pago",
                                                    PosibleSolucion = "Verifique que la información del perfil del colaborador esté completa y/o correcta."

                                                });
                                                errores++;
                                            }

                                            contador++;
                                            #endregion

                                            // guardar cambios en la bbdd
                                            db.SaveChanges();
                                            dbContextTransaccion.Commit();

                                            #region Enviar comprobante de pago por email
                                            if (enviarEmail != null && enviarEmail == true)
                                            {
                                                oComprobantePagoModel.moneda = db.tbSueldos.Where(x => x.emp_Id == empleadoActual.emp_Id).Select(x => x.tbTipoMonedas.tmon_Descripcion).FirstOrDefault();
                                                oComprobantePagoModel.EmailAsunto = "Comprobante de pago";
                                                oComprobantePagoModel.NombreColaborador = empleadoActual.tbPersonas.per_Nombres + " " + empleadoActual.tbPersonas.per_Apellidos;
                                                oComprobantePagoModel.idColaborador = empleadoActual.emp_Id;
                                                oComprobantePagoModel.EmailDestino = empleadoActual.tbPersonas.per_CorreoElectronico;
                                                oComprobantePagoModel.PeriodoPago = $"{fechaInicio.ToString("dd/MM/yyyy")}- {fechaFin.ToString("dd/MM/yyyy")}";
                                                oComprobantePagoModel.Ingresos = ListaIngresosVoucher;
                                                oComprobantePagoModel.Deducciones = ListaDeduccionesVoucher;
                                                oComprobantePagoModel.totalIngresos = totalIngresosEmpleado;
                                                oComprobantePagoModel.totalDeducciones = totalDeduccionesEmpleado;
                                                oComprobantePagoModel.NetoPagar = netoAPagarColaborador;
                                                oComprobantePagoModel.moneda = db.tbSueldos.Where(x=>x.emp_Id == InformacionDelEmpleadoActual.emp_Id).Select(x=>x.tbTipoMonedas.tmon_Descripcion).FirstOrDefault();

                                                // enviar comprobante de pago
                                                try
                                                {
                                                    if (!utilities.SendEmail(oComprobantePagoModel))
                                                    {
                                                        listaErrores.Add(new ViewModelListaErrores
                                                        {
                                                            Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                            Error = "Error al Enviar comprobante de pago.",
                                                            PosibleSolucion = "Verifique que la información del perfil del colaborador esté completa y/o correcta."

                                                        });
                                                        errores++;
                                                    }
                                                    else
                                                    {
                                                        ListaDeduccionesVoucher = new List<IngresosDeduccionesVoucher>();
                                                        ListaIngresosVoucher = new List<IngresosDeduccionesVoucher>();
                                                    }

                                                }
                                                catch (Exception ex)
                                                {
                                                    listaErrores.Add(new ViewModelListaErrores
                                                    {
                                                        Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                                        NombreColaborador = InformacionDelEmpleadoActual.per_Nombres,
                                                        Error = "Error al Enviar comprobante de pago.",
                                                        PosibleSolucion = "Verifique que la información del perfil del colaborador esté completa y/o correcta."

                                                    });
                                                    errores++;
                                                }
                                            }
                                            #endregion

                                            #region crear registro de la planilla del colaborador para el reporte
                                            oPlanillaEmpleado.CodColaborador = InformacionDelEmpleadoActual.emp_Id.ToString();
                                            oPlanillaEmpleado.NombresColaborador = $"{empleadoActual.tbPersonas.per_Nombres} {empleadoActual.tbPersonas.per_Apellidos}";
                                            oPlanillaEmpleado.SalarioBase = SalarioBase;
                                            oPlanillaEmpleado.horasTrabajadas = horasTrabajadas;
                                            oPlanillaEmpleado.SalarioHora = salarioHora;
                                            oPlanillaEmpleado.totalSalario = totalSalario;
                                            oPlanillaEmpleado.tipoPlanilla = empleadoActual.tbCatalogoDePlanillas.cpla_DescripcionPlanilla;
                                            oPlanillaEmpleado.totalComisiones = totalComisiones;
                                            oPlanillaEmpleado.horasExtras = horasExtrasTrabajadas;
                                            oPlanillaEmpleado.totalHorasPermiso = totalHorasPermiso;
                                            oPlanillaEmpleado.TotalIngresosHorasExtras = totalHorasExtras;
                                            oPlanillaEmpleado.totalBonificaciones = totalBonificaciones;
                                            oPlanillaEmpleado.totalIngresosIndivuales = totalIngresosIndivuales;
                                            oPlanillaEmpleado.totalVacaciones = totalVacaciones;
                                            oPlanillaEmpleado.totalIngresos = Math.Round((decimal)totalIngresosEmpleado, 2);
                                            oPlanillaEmpleado.totalISR = totalISR;
                                            oPlanillaEmpleado.totalDeduccionesColaborador = colaboradorDeducciones;
                                            oPlanillaEmpleado.totalAFP = totalAFP;
                                            oPlanillaEmpleado.totalInstitucionesFinancieras = totalInstitucionesFinancieras;
                                            oPlanillaEmpleado.otrasDeducciones = Math.Round((decimal)totalOtrasDeducciones, 2);
                                            oPlanillaEmpleado.adelantosSueldo = Math.Round((decimal)adelantosSueldo, 2);
                                            oPlanillaEmpleado.totalDeduccionesIndividuales = totalDeduccionesIndividuales;
                                            oPlanillaEmpleado.totalDeducciones = Math.Round((decimal)totalDeduccionesEmpleado, 2);
                                            oPlanillaEmpleado.totalAPagar = Math.Round((decimal)netoAPagarColaborador, 2);
                                            reporte.Add(oPlanillaEmpleado);
                                            oPlanillaEmpleado = null;
                                            #endregion
                                            identidadEmpleado = InformacionDelEmpleadoActual.per_Identidad;
                                            NombresEmpleado = InformacionDelEmpleadoActual.per_Nombres;

                                        }
                                        // catch por si hubo un error al generar la planilla de un empleado
                                        catch (Exception ex)
                                        {
                                            // si hay un error, hacer un rollback
                                            dbContextTransaccion.Rollback();

                                            // mensaje del error en el registro del colaborador
                                            errores++;

                                            listaErrores.Add(new ViewModelListaErrores
                                            {
                                                Identidad = identidadEmpleado,
                                                NombreColaborador = NombresEmpleado,
                                                Error = "Error al procesar planilla del colaborador.",
                                                PosibleSolucion = "Verifique información registrada al colaborador y vuelva a intentarlo."

                                            });
                                        }
                                    } // termina transaccion
                                }

                            }
                            // catch si se produjo un error al procesar una sola planilla
                            catch (Exception ex)
                            {
                                listaErrores.Add(new ViewModelListaErrores
                                {
                                    Identidad = $"Planilla {iter.cpla_DescripcionPlanilla}",
                                    NombreColaborador = "",
                                    Error = $"Ocurrió un error al procesar la planilla {iter.cpla_DescripcionPlanilla}"
                                });
                                errores++;
                            }
                        } // for each idsPlanillas

                    } // if idsPlanilla != null

                } // using entities model

                // enviar resultado al cliente
                response.Response = $"El proceso de generación de planilla se realizó, con {errores} errores";
                response.Encabezado = "Exito";
                response.Tipo = errores == 0 ? "success" : "warning";

            }
            // catch se produjo un error fatal en el proceso generar planilla
            catch (Exception ex)
            {
                response.Response = "";
                response.Encabezado = "Error";
                response.Tipo = "error";
                listaErrores.Add(new ViewModelListaErrores
                {
                    Identidad = $"Planilla",
                    NombreColaborador = "",
                    Error = $"El proceso de generación de planillas falló, contacte al adminstrador.",
                    PosibleSolucion = "Vuelva a intentarlo"
                });
            }
            #endregion

            // retornar resultado del proceso
            return Json(new { Data = reporte, listaDeErrores = listaErrores, Response = response }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region previsualizar
        public ActionResult PrevisualizarPlanilla(int? ID, bool? enviarEmail, DateTime fechaInicio, DateTime fechaFin)
        {
            #region declaracion de instancias

            // helper
            General utilities = new General();

            // instancias para el reporte final
            ReportePlanillaViewModel oPlanillaEmpleado;
            List<ReportePlanillaViewModel> reporte = new List<ReportePlanillaViewModel>();

            // instancia para resultado del proceso en izitoast
            iziToast response = new iziToast();
            int errores = 0;
            #endregion

            #region inicia proceso de previsualizacion de planilla
            try
            {
                using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
                {
                    List<tbCatalogoDePlanillas> oIDSPlanillas = new List<tbCatalogoDePlanillas>();

                    // seleccionar las planillas que se van a generar
                    if (ID != null)
                        oIDSPlanillas = db.tbCatalogoDePlanillas
                                          .Where(X => X.cpla_IdPlanilla == ID)
                                          .ToList();
                    else
                        oIDSPlanillas = db.tbCatalogoDePlanillas
                                          .Where(x => x.cpla_Activo == true)
                                          .ToList();

                    // procesar todas las planillas seleccionadas
                    foreach (var iter in oIDSPlanillas)
                    {
                        try
                        {
                            // planilla actual del foreach
                            tbCatalogoDePlanillas oPlanilla = db.tbCatalogoDePlanillas
                                                                .Find(iter.cpla_IdPlanilla);

                            // ingresos de la planilla actual
                            List<V_PlanillaIngresos> oIngresos = db.V_PlanillaIngresos
                                                                   .Where(x => x.cpla_IdPlanilla == oPlanilla.cpla_IdPlanilla)
                                                                   .ToList();

                            // deducciones de la planilla actual
                            List<V_PlanillaDeducciones> oDeducciones = db.V_PlanillaDeducciones
                                                                         .Where(x => x.cpla_IdPlanilla == oPlanilla.cpla_IdPlanilla)
                                                                         .ToList();

                            // empleados de la planilla actual
                            List<tbEmpleados> oEmpleados = db.tbEmpleados
                                                             .Where(emp => emp.cpla_IdPlanilla == oPlanilla.cpla_IdPlanilla &&
                                                                    emp.emp_Estado == true)
                                                            .ToList();


                            // procesar planilla empleado por empleado
                            foreach (var empleadoActual in oEmpleados)
                            {
                                using (var dbContextTransaccion = db.Database.BeginTransaction())
                                {
                                    try
                                    {
                                        #region variables reporte view model

                                        string codColaborador = string.Empty;
                                        string nombreColaborador = string.Empty;
                                        decimal SalarioBase = 0;
                                        int horasTrabajadas = 0;
                                        decimal salarioHora = 0;
                                        decimal totalSalario = 0;
                                        string tipoPlanilla = string.Empty;
                                        decimal? porcentajeComision = 0;
                                        decimal? totalVentas = 0;
                                        decimal? totalComisiones = 0;
                                        int horasExtrasTrabajadas = 0;
                                        int cantidadUnidadesBonos = 0;
                                        decimal? totalHorasExtras = 0;
                                        decimal? totalHorasPermiso = 0;
                                        decimal? totalBonificaciones = 0;
                                        decimal? totalIngresosIndivuales = 0;
                                        decimal? totalVacaciones = 0;
                                        decimal? totalIngresosEmpleado = 0;
                                        decimal totalISR = 0;
                                        decimal? colaboradorDeducciones = 0;
                                        decimal totalAFP = 0;
                                        decimal? totalInstitucionesFinancieras = 0;
                                        decimal? totalOtrasDeducciones = 0;
                                        decimal? adelantosSueldo = 0;
                                        decimal? totalDeduccionesEmpleado = 0;
                                        decimal? totalDeduccionesIndividuales = 0;
                                        decimal? netoAPagarColaborador = 0;
                                        oPlanillaEmpleado = new ReportePlanillaViewModel();


                                        #endregion

                                        #region Procesar ingresos

                                        // informacion del colaborador actual
                                        V_InformacionColaborador InformacionDelEmpleadoActual = db.V_InformacionColaborador
                                                                                                  .Where(x => x.emp_Id == empleadoActual.emp_Id)
                                                                                                  .FirstOrDefault();

                                        // salario base del colaborador actual
                                        SalarioBase = InformacionDelEmpleadoActual.SalarioBase.Value;


                                        // horas normales trabajadas
                                        horasTrabajadas = db.tbHistorialHorasTrabajadas
                                                            .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                   x.htra_Estado == true &&
                                                                   x.tbTipoHoras.tiho_Recargo == 0 &&
                                                                   x.htra_Fecha >= fechaInicio &&
                                                                   x.htra_Fecha <= fechaFin)
                                                            .Select(x => x.htra_CantidadHoras)
                                                            .DefaultIfEmpty(0)
                                                            .Sum();


                                        // salario por hora
                                        salarioHora = SalarioBase / 240;


                                        // total salario o salario bruto
                                        totalSalario = salarioHora * horasTrabajadas;


                                        // horas con permiso justificado
                                        List<tbHistorialPermisos> horasConPermiso = db.tbHistorialPermisos
                                                                                      .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                             x.hper_Estado == true &&
                                                                                             x.hper_fechaInicio >= fechaInicio &&
                                                                                             x.hper_fechaFin <= fechaFin)
                                                                                      .ToList();

                                        if (horasConPermiso.Count > 0)
                                        {
                                            int CantidadHorasPermisoActual = 0;
                                            // sumar todas las horas extras
                                            foreach (var iterHorasPermiso in horasConPermiso)
                                            {
                                                CantidadHorasPermisoActual = iterHorasPermiso.hper_Duracion;

                                                totalHorasPermiso += CantidadHorasPermisoActual * (((iterHorasPermiso.hper_PorcentajeIndemnizado * salarioHora) / 100));

                                            }
                                        }

                                        // comisiones
                                        List<tbEmpleadoComisiones> oComisionesColaboradores = db.tbEmpleadoComisiones
                                                                                                .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                                       x.cc_Activo == true &&
                                                                                                       x.cc_Pagado == false &&
                                                                                                       x.cc_FechaRegistro >= fechaInicio &&
                                                                                                       x.cc_FechaRegistro <= fechaFin)
                                                                                                .ToList();
                                        if (oComisionesColaboradores.Count > 0)
                                        {
                                            // sumar todas las comisiones
                                            foreach (var oComisionesColaboradoresIterador in oComisionesColaboradores)
                                            {
                                                totalComisiones += oComisionesColaboradoresIterador.cc_TotalComision;
                                            }
                                        }

                                        // horas extras
                                        horasExtrasTrabajadas = db.tbHistorialHorasTrabajadas
                                                                .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                        x.htra_Estado == true &&
                                                                        x.tbTipoHoras.tiho_Recargo > 0 &&
                                                                        x.htra_Fecha >= fechaInicio &&
                                                                        x.htra_Fecha <= fechaFin)
                                                                .Select(x => x.htra_CantidadHoras)
                                                                .DefaultIfEmpty(0)
                                                                .Sum();

                                        // total ingresos horas extras
                                        List<tbHistorialHorasTrabajadas> oHorasExtras = db.tbHistorialHorasTrabajadas
                                                                                        .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                               x.htra_Estado == true &&
                                                                                               x.tbTipoHoras.tiho_Recargo > 0 &&
                                                                                               x.htra_Fecha >= fechaInicio &&
                                                                                               x.htra_Fecha <= fechaFin)
                                                                                        .ToList();
                                        if (oHorasExtras.Count > 0)
                                        {
                                            int CantidadHorasExtrasActual = 0;
                                            // sumar todas las horas extras
                                            foreach (var iterHorasExtras in oHorasExtras)
                                            {
                                                CantidadHorasExtrasActual = db.tbHistorialHorasTrabajadas
                                                                            .Where(x => x.emp_Id == empleadoActual.emp_Id && x.htra_Estado == true && x.htra_Id == iterHorasExtras.htra_Id)
                                                                            .Select(x => x.htra_CantidadHoras)
                                                                            .DefaultIfEmpty(0)
                                                                            .Sum();

                                                totalHorasExtras += CantidadHorasExtrasActual * (salarioHora + ((iterHorasExtras.tbTipoHoras.tiho_Recargo * salarioHora) / 100));

                                            }
                                        }

                                        // bonos del colaborador
                                        List<tbEmpleadoBonos> oBonosColaboradores = db.tbEmpleadoBonos
                                                                                    .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                           x.cb_Activo == true &&
                                                                                           x.cb_Pagado == false &&
                                                                                           x.cb_FechaRegistro >= fechaInicio &&
                                                                                           x.cb_FechaRegistro <= fechaFin)
                                                                                    .ToList();


                                        if (oBonosColaboradores.Count > 0)
                                        {
                                            cantidadUnidadesBonos = oBonosColaboradores.Count;

                                            // iterar los bonos
                                            foreach (var oBonosColaboradoresIterador in oBonosColaboradores)
                                            {
                                                totalBonificaciones += oBonosColaboradoresIterador.cb_Monto;
                                            }
                                        }

                                        // vacaciones
                                        List<tbHistorialVacaciones> oVacacionesColaboradores = db.tbHistorialVacaciones
                                                                                                .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                                       x.hvac_DiasPagados == false &&
                                                                                                       x.hvac_Estado == true &&
                                                                                                       x.hvac_FechaInicio >= fechaInicio &&
                                                                                                       x.hvac_FechaFin <= fechaFin)
                                                                                                .ToList();
                                        if (oVacacionesColaboradores.Count > 0)
                                        {
                                            // sumar todas las comisiones
                                            foreach (var oVacacionesColaboradoresIterador in oVacacionesColaboradores)
                                            {
                                                int cantidadDias = 0;
                                                DateTime VacacionesfechaInicio;
                                                DateTime VacacionesfechaFin;

                                                VacacionesfechaInicio = (from tbEmpVac in db.tbHistorialVacaciones
                                                                         where tbEmpVac.hvac_Id == oVacacionesColaboradoresIterador.hvac_Id
                                                                         select tbEmpVac.hvac_FechaInicio).FirstOrDefault();

                                                VacacionesfechaFin = (from tbEmpVac in db.tbHistorialVacaciones
                                                                      where tbEmpVac.hvac_Id == oVacacionesColaboradoresIterador.hvac_Id
                                                                      select tbEmpVac.hvac_FechaFin).FirstOrDefault();

                                                TimeSpan restaFechas = VacacionesfechaFin - VacacionesfechaInicio;
                                                cantidadDias = restaFechas.Days;

                                                totalVacaciones += (salarioHora * 8) * cantidadDias;

                                            }
                                        }

                                        // ingresos individuales
                                        List<tbIngresosIndividuales> oIngresosIndiColaboradores = db.tbIngresosIndividuales
                                                                                                    .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                                           x.ini_Activo == true &&
                                                                                                           x.ini_Pagado != true &&
                                                                                                           x.ini_FechaCrea >= fechaInicio &&
                                                                                                           x.ini_FechaCrea <= fechaFin)
                                                                                                    .ToList();

                                        if (oIngresosIndiColaboradores.Count > 0)
                                        {
                                            //iterar los bonos
                                            foreach (var oIngresosIndiColaboradoresIterador in oIngresosIndiColaboradores)
                                            {
                                                totalIngresosIndivuales += oIngresosIndiColaboradoresIterador.ini_Monto;

                                            }
                                        }

                                        #region Septimo Dia
                                        DateTime inicioFecha = fechaInicio;
                                        DateTime finFecha = fechaFin;
                                        TimeSpan restaFechasSeptimo = finFecha - inicioFecha;
                                        int cantidadDiasSeptimo = restaFechasSeptimo.Days + 1;
                                        DateTime fechaIterador = inicioFecha;
                                        int cantHoras = 0;
                                        int cantHorasPermiso = 0;
                                        int cantidadSeptimosDias = 0;
                                        int contadorSeptimosDias = 1;

                                        for (int i = 1; i <= cantidadDiasSeptimo; i++)
                                        {
                                            if (fechaIterador.DayOfWeek.ToString() != "Sunday")
                                            {
                                                cantHoras += db.tbHistorialHorasTrabajadas
                                                            .Where(x => x.htra_Fecha == fechaIterador &&
                                                                   x.emp_Id == empleadoActual.emp_Id &&
                                                                   x.htra_Estado == true)
                                                            .Select(x => x.htra_CantidadHoras)
                                                            .FirstOrDefault();

                                                cantHorasPermiso += db.tbHistorialPermisos
                                                                    .Where(x => x.hper_fechaInicio <= fechaIterador &&
                                                                           x.hper_fechaFin >= fechaIterador &&
                                                                           x.emp_Id == empleadoActual.emp_Id)
                                                                    .Select(x => x.hper_Duracion)
                                                                    .FirstOrDefault();

                                                if ((cantHoras + (cantHorasPermiso * 8)) >= 48 && contadorSeptimosDias == 7)
                                                {
                                                    cantidadSeptimosDias++;
                                                    contadorSeptimosDias = 0;
                                                    cantHoras = 0;
                                                }
                                            }
                                            if (contadorSeptimosDias == 7)
                                            {
                                                cantHoras = 0;
                                                contadorSeptimosDias = 0;
                                            }
                                            fechaIterador = fechaIterador.Add(new TimeSpan(1, 0, 0, 0, 0));
                                            contadorSeptimosDias++;
                                        }

                                        decimal resultSeptimoDia = (salarioHora * 8) * cantidadSeptimosDias;
                                        #endregion

                                        // total ingresos
                                        totalIngresosEmpleado = totalIngresosIndivuales + totalSalario + totalComisiones + totalHorasExtras + totalBonificaciones + totalVacaciones + totalHorasPermiso + resultSeptimoDia;

                                        #endregion

                                        #region Procesar deducciones

                                        // deducciones de la planilla
                                        foreach (var iterDeducciones in oDeducciones)
                                        {
                                            decimal? porcentajeColaborador = iterDeducciones.cde_PorcentajeColaborador;
                                            decimal? porcentajeEmpresa = iterDeducciones.cde_PorcentajeEmpresa;
                                            decimal? montoDeduccionColaborador = SalarioBase;

                                            // verificar techos deducciones
                                            List<tbTechosDeducciones> oTechosDeducciones = db.tbTechosDeducciones
                                                                                             .Where(x => x.cde_IdDeducciones == iterDeducciones.cde_IdDeducciones &&
                                                                                                    x.tddu_Activo == true)
                                                                                             .OrderBy(x => x.tddu_Techo)
                                                                                             .ToList();
                                            if (oTechosDeducciones.Count() > 0)
                                            {
                                                foreach (var techosDeduccionesIter in oTechosDeducciones)
                                                {
                                                    if (SalarioBase > techosDeduccionesIter.tddu_Techo)
                                                    {
                                                        montoDeduccionColaborador = techosDeduccionesIter.tddu_Techo;
                                                        porcentajeColaborador = techosDeduccionesIter.tddu_PorcentajeColaboradores;
                                                        porcentajeEmpresa = techosDeduccionesIter.tddu_PorcentajeEmpresa;
                                                    }
                                                }
                                            }
                                            //sumar las deducciones
                                            colaboradorDeducciones += (montoDeduccionColaborador * porcentajeColaborador) / 100;

                                        }

                                        //instituciones financieras
                                        List<tbDeduccionInstitucionFinanciera> oDeduInstiFinancieras = db.tbDeduccionInstitucionFinanciera
                                                                                                        .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                                               x.deif_Activo == true &&
                                                                                                               x.deif_Pagado == false &&
                                                                                                               x.deif_FechaCrea >= fechaInicio &&
                                                                                                               x.deif_FechaCrea <= fechaFin)
                                                                                                        .ToList();

                                        if (oDeduInstiFinancieras.Count > 0)
                                        {
                                            // sumarlas todas
                                            foreach (var oDeduInstiFinancierasIterador in oDeduInstiFinancieras)
                                            {
                                                totalInstitucionesFinancieras += oDeduInstiFinancierasIterador.deif_Monto;

                                            }
                                        }
                                        // deducciones afp
                                        List<tbDeduccionAFP> oDeduccionAfp = db.tbDeduccionAFP
                                                                            .Where(af => af.emp_Id == empleadoActual.emp_Id &&
                                                                                   af.dafp_Pagado != true &&
                                                                                   af.dafp_Activo == true &&
                                                                                   af.dafp_FechaCrea >= fechaInicio &&
                                                                                   af.dafp_FechaCrea <= fechaFin)
                                                                            .ToList();

                                        if (oDeduccionAfp.Count > 0)
                                        {
                                            // sumarlas todas
                                            foreach (var oDeduccionAfpIter in oDeduccionAfp)
                                            {
                                                totalAFP += oDeduccionAfpIter.dafp_AporteLps;
                                            }
                                        }

                                        // deducciones extras
                                        List<tbDeduccionesExtraordinarias> oDeduccionesExtrasColaborador = db.tbDeduccionesExtraordinarias
                                                                                                            .Where(DEX => DEX.tbEquipoEmpleados.emp_Id == empleadoActual.emp_Id &&
                                                                                                                   DEX.dex_MontoRestante > 0 &&
                                                                                                                   DEX.dex_Activo == true)
                                                                                                            .ToList();

                                        if (oDeduccionesExtrasColaborador.Count > 0)
                                        {
                                            // sumarlas todas
                                            foreach (var oDeduccionesExtrasColaboradorIterador in oDeduccionesExtrasColaborador)
                                            {
                                                totalOtrasDeducciones += oDeduccionesExtrasColaboradorIterador.dex_MontoRestante <= oDeduccionesExtrasColaboradorIterador.dex_Cuota ? oDeduccionesExtrasColaboradorIterador.dex_MontoRestante : oDeduccionesExtrasColaboradorIterador.dex_Cuota;

                                            }
                                        }

                                        // adelantos de sueldo
                                        List<tbAdelantoSueldo> oAdelantosSueldo = db.tbAdelantoSueldo
                                                                                    .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                                                                           x.adsu_Activo == true && x.adsu_Deducido == false &&
                                                                                           x.adsu_FechaAdelanto >= fechaInicio &&
                                                                                           x.adsu_FechaAdelanto <= fechaFin)
                                                                                    .ToList();

                                        if (oAdelantosSueldo.Count > 0)
                                        {
                                            // sumarlas todas
                                            foreach (var oAdelantosSueldoIterador in oAdelantosSueldo)
                                            {
                                                adelantosSueldo += oAdelantosSueldoIterador.adsu_Monto;
                                            }
                                        }

                                        // deducciones individuales
                                        List<tbDeduccionesIndividuales> oDeduccionesIndiColaborador = db.tbDeduccionesIndividuales
                                                                                                        .Where(DEX => DEX.emp_Id == empleadoActual.emp_Id &&
                                                                                                               DEX.dei_Monto > 0 &&
                                                                                                               DEX.dei_Pagado != true &&
                                                                                                               DEX.dei_Activo == true)
                                                                                                        .ToList();

                                        if (oDeduccionesIndiColaborador.Count > 0)
                                        {
                                            // sumarlas todas
                                            foreach (var oDeduccionesIndiColaboradorIterador in oDeduccionesIndiColaborador)
                                            {
                                                totalDeduccionesIndividuales += oDeduccionesIndiColaboradorIterador.dei_Monto <= oDeduccionesIndiColaboradorIterador.dei_MontoCuota ? oDeduccionesIndiColaboradorIterador.dei_MontoCuota : oDeduccionesIndiColaboradorIterador.dei_MontoCuota;

                                            }
                                        }


                                        // totales
                                        totalDeduccionesEmpleado = totalOtrasDeducciones + totalInstitucionesFinancieras + colaboradorDeducciones + totalAFP + adelantosSueldo + totalDeduccionesIndividuales;
                                        netoAPagarColaborador = Math.Round((decimal)totalIngresosEmpleado.Value - totalDeduccionesEmpleado.Value, 2);

										#endregion

										#region Cálculo de ISR

										#region Declaracion de Variables
										int AnioActual = DateTime.Now.Year;
										decimal? TotalBonos = 0;
										decimal? TotalHrsExtra = 0;
										decimal? TotalComisiones = 0;
										decimal? TotalIngresosExtras = 0;
										decimal? TotalDeduccionesEquipoTrabajo = 0;
										decimal? TotalDeduccionesExtras = 0;
										decimal? TotalDeduccionesAFP = 0;
										decimal? TotalDeduccionesInstitucionesFinancieras = 0;
										decimal? ExcesoDecimoTercer = 0;
										decimal? ExcesoVacaciones = 0;
										decimal? ExcesoDecimoCuarto = 0;
										decimal Exceso = 0;
										decimal SueldoAnual = 0;
										decimal? AcumuladosISR = 0;
										decimal TotalIngresosGravables = 0;
										decimal TotalDeduccionesGravables = 0;
										decimal RentaNetaGravable = 0;
										decimal SalarioMinimo = db.tbEmpresas.Select(x => x.empr_SalarioMinimo).FirstOrDefault() ?? 0;
										var tablaEmp = db.tbSueldos.Where(x => x.emp_Id == empleadoActual.emp_Id).OrderBy(x => x.sue_FechaCrea);
										#endregion

										#region Sueldo Promedio Anual
										//Sueldo redondeado del Colaborador
										DateTime AnioActualEnero = new DateTime(DateTime.Now.Year, 1, 1);

										//Obtener los pagos mensuales totales
										var mesesPago = (db.tbHistorialDePago
											.Where(x => x.emp_Id == empleadoActual.emp_Id && x.hipa_Anio == AnioActualEnero.Year)
											.OrderBy(x => x.hipa_Mes)
											.GroupBy(x => x.hipa_Mes)
											.Select(x => x.Sum(y => (Decimal)y.hipa_TotalSueldoBruto))).ToList();

										DateTime FechaIngresoEmpleado = db.tbEmpleados
																		.Where(x => x.emp_Id == empleadoActual.emp_Id)
																		.Select(x => x.emp_Fechaingreso).FirstOrDefault();
										bool esMensual = true;

										TimeSpan diferencia = AnioActualEnero - FechaIngresoEmpleado;

										if (TimeSpan.Zero > diferencia)
											esMensual = true;


										//Saber que mes entro
										int mes = FechaIngresoEmpleado.Month;
										decimal SalarioPromedioAnualPagadoAlAnio = 0;
										decimal salarioPromedioAnualPagadoAlMes = 0;
										decimal TotalSalarioAnual = SalarioPromedioAnualISR(totalSalario,
										mesesPago,
										esMensual,
										ref SalarioPromedioAnualPagadoAlAnio,
										ref salarioPromedioAnualPagadoAlMes);
										#endregion

										#region Excesos
										////-----------------------------------------------------------------------------------------------------------------------------
										//Exceso Décimo Tercer Mes
										//Variable para los empleados con Décimo Tercer Mes
										var DecimoTercer = db.V_DecimoTercerMes_Pagados.Where(x => x.emp_Id == empleadoActual.emp_Id && x.dtm_FechaPago.Year == AnioActual).FirstOrDefault();

										//Validar primero si es en el año actual el proceso de este calculo
										if (DecimoTercer != null)
										{
											//Salario Mínimo Mensual por 10 Meses (Según SAR)
											Exceso = SalarioMinimo * 10;

											//Validar si el Décimo Tercer es mayor al Exceso
											if (DecimoTercer.dtm_Monto > Exceso)
											{
												ExcesoDecimoTercer = DecimoTercer.dtm_Monto - Exceso;
											}
											else
											{
												ExcesoDecimoTercer = 0;
											}
										}
										////-----------------------------------------------------------------------------------------------------------------------------


										////-----------------------------------------------------------------------------------------------------------------------------
										//Exceso Décimo Cuarto Mes
										//Variable para los empleados con Décimo Cuarto Mes
										var DecimoCuarto = db.V_DecimoCuartoMes_Pagados.Where(x => x.emp_Id == empleadoActual.emp_Id && x.dcm_FechaPago.Year == AnioActual).FirstOrDefault();

										//Validar primero si es en el año actual el proceso de este calculo
										if (DecimoCuarto != null)
										{
											//Salario Mínimo Mensual por 10 Meses (Según SAR)
											Exceso = SalarioMinimo * 10;

											//Validar si el Décimo Cuarto es mayor al Exceso
											if (DecimoCuarto.dcm_Monto > Exceso)
											{
												ExcesoDecimoCuarto = DecimoCuarto.dcm_Monto - Exceso;
											}
											else
											{
												ExcesoDecimoCuarto = 0;
											}
										}
										////-----------------------------------------------------------------------------------------------------------------------------


										////-----------------------------------------------------------------------------------------------------------------------------
										//Exceso Vacaciones
										//Variable para las Vacaciones Pagadas del Historial de Ingresos de Pago
										var objVacaciones = db.tbHistorialVacaciones.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hvac_AnioVacaciones && x.hvac_DiasPagados == true).Select(x => x.hvac_CantDias).FirstOrDefault();

										//Validar si los dias a Pagar es mayor a 30 dias 
										if (objVacaciones > 30)
										{
											ExcesoVacaciones = ((objVacaciones - 30) * (SueldoAnual / 360));
										}
										else
										{
											ExcesoVacaciones = 0;
										}

										#endregion

										#region Acumulados ISR

										//Variable para obtener los registros de los Acumulados ISR del empleado actual
										AcumuladosISR = db.tbAcumuladosISR.Where(x => x.aisr_Activo == true && x.emp_Id == empleadoActual.emp_Id).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.aisr_Monto)).FirstOrDefault();

										#endregion

										#region Otros Ingresos

										TotalBonos = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_TotalBonos)).FirstOrDefault();
										TotalHrsExtra = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_TotalHorasExtras)).FirstOrDefault();
										TotalComisiones = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_TotalComisiones)).FirstOrDefault();
										TotalIngresosExtras = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_TotalIngresosIndividuales)).FirstOrDefault();

										if (TotalBonos == null)
											TotalBonos = 0;

										if (TotalHrsExtra == null)
											TotalHrsExtra = 0;

										if (TotalComisiones == null)
											TotalComisiones = 0;

										if (TotalIngresosExtras == null)
											TotalIngresosExtras = 0;

										#endregion

										#region Otras Deducciones (Posible cálculo para el ISR)

										#region Deducciones por Equipo de Trabajo
										//DET = Deducción por Equipo de Trabajo
										decimal? MontoInicialDET = 0;
										decimal? MontoRestanteDET = 0;
										List<tbDeduccionesExtraordinarias> objDeduccionesExtraordinarias = db.tbDeduccionesExtraordinarias.Where(x => x.tbEquipoEmpleados.emp_Id == empleadoActual.emp_Id && x.dex_Activo == true && x.dex_DeducirISR == true)
																																		  .ToList();
										if (objDeduccionesExtraordinarias.Count() > 0)
										{
											foreach (var oDET in objDeduccionesExtraordinarias)
											{
												MontoInicialDET = MontoInicialDET + oDET.dex_MontoInicial;
												MontoRestanteDET = MontoRestanteDET + oDET.dex_MontoRestante;
											}
										}
										if (MontoInicialDET > 0 && MontoRestanteDET > 0)
										{
											TotalDeduccionesEquipoTrabajo = MontoInicialDET - MontoRestanteDET;
										}
										else
										{
											TotalDeduccionesEquipoTrabajo = 0;
										}
										#endregion

										#region Deducciones Extras
										List<tbDeduccionesIndividuales> objDeduccionIndividual = db.tbDeduccionesIndividuales.Where(x => x.emp_Id == empleadoActual.emp_Id && x.dei_Activo == true && x.dei_DeducirISR == true)
																															 .ToList();
										if (objDeduccionIndividual.Count() > 0)
										{
											foreach (var oDeduIndi in objDeduccionIndividual)
											{
												if (oDeduIndi.dei_DeducirISR == true)
												{
													TotalDeduccionesExtras = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_TotalDeduccionesIndividuales)).FirstOrDefault();
												}
											}
										}
										#endregion

										#region Deducciones AFP
										List<tbDeduccionAFP> objDeduccionAFP = db.tbDeduccionAFP.Where(x => x.emp_Id == empleadoActual.emp_Id && x.dafp_Activo == true && x.dafp_DeducirISR == true)
																								.ToList();
										if (objDeduccionAFP.Count() > 0)
										{
											foreach (var oDeduAFP in objDeduccionAFP)
											{
												if (oDeduAFP.dafp_DeducirISR == true)
												{
													TotalDeduccionesAFP = db.tbHistorialDePago.Where(x => x.emp_Id == empleadoActual.emp_Id && AnioActual == x.hipa_Anio).GroupBy(x => x.emp_Id).Select(x => x.Sum(y => y.hipa_AFP)).FirstOrDefault();
												}
											}
										}
										#endregion

										#region Deducciones Instituciones Financieras
										//DIF = Deducción Institución Financiera
										List<tbDeduccionInstitucionFinanciera> objDeduccionInstitucionFinanciera = db.tbDeduccionInstitucionFinanciera.Where(x => x.emp_Id == empleadoActual.emp_Id && x.deif_Activo == true && x.deif_DeducirISR == true)
																																					  .ToList();
										if (objDeduccionInstitucionFinanciera.Count() > 0)
										{
											foreach (var oDIF in objDeduccionInstitucionFinanciera)
											{
												if (oDIF.deif_Monto > 0)
												{
													TotalDeduccionesInstitucionesFinancieras = TotalDeduccionesInstitucionesFinancieras + oDIF.deif_Monto;
												}
												else
												{
													TotalDeduccionesInstitucionesFinancieras = 0;
												}
											}
										}
										#endregion

										#endregion

										#region Calculo del ISR
										//-----------------------------------------------------------------------------------------------------------------------------
										//Total Ingresos Gravables
										TotalIngresosGravables = TotalSalarioAnual + (Decimal)ExcesoDecimoTercer + (Decimal)ExcesoDecimoCuarto + (Decimal)ExcesoVacaciones + (Decimal)TotalBonos + (Decimal)TotalHrsExtra + (Decimal)TotalComisiones + (Decimal)TotalIngresosExtras;

										//Total Deducciones Gravables
										TotalDeduccionesGravables = (Decimal)AcumuladosISR + (Decimal)TotalDeduccionesEquipoTrabajo + (Decimal)TotalDeduccionesExtras + (Decimal)TotalDeduccionesAFP + (Decimal)TotalDeduccionesInstitucionesFinancieras;

										//Renta Neta Gravable
										RentaNetaGravable = TotalIngresosGravables - TotalDeduccionesGravables;


										#region Tabla Progresiva ISR
										//Tabla Progresiva ISR

										//Variable para validar que entre primero en la primera parte de la fórmula del ISR y luego en la segunda parte
										string VI = "FirstRange";

										//Variable de tipo Lista para obtener los registros de la tabla progresiva de mayor a menor
										List<tbISR> objDeduccionISR = db.tbISR.Where(x => x.isr_Activo == true)
																			  .OrderByDescending(x => x.isr_RangoInicial)
																			  .ToList();

										foreach (var oISR in objDeduccionISR)
										{
											if (objDeduccionISR.Count() > 0)
											{
												if (RentaNetaGravable >= oISR.isr_RangoInicial)
												{
													if (VI == "FirstRange")
													{
														totalISR = totalISR + (RentaNetaGravable - oISR.isr_RangoInicial) * (oISR.isr_Porcentaje / 100);
														VI = "No";
													}
													else if (VI == "No")
													{
														VI = "SecondRange";
													}
													if (VI == "SecondRange")
													{
														totalISR = totalISR + (oISR.isr_RangoFinal - oISR.isr_RangoInicial) * (oISR.isr_Porcentaje / 100);
														VI = "No";
													}
												}
												else
												{
													totalISR = 0;
												}
											}
										}
										if (totalISR > 0)
										{
											totalISR = totalISR / 12;
										}
										#endregion

										#endregion

										#endregion

										#region crear registro de la planilla del colaborador para el reporte
										oPlanillaEmpleado.CodColaborador = InformacionDelEmpleadoActual.emp_Id.ToString();
                                        oPlanillaEmpleado.NombresColaborador = $"{empleadoActual.tbPersonas.per_Nombres} {empleadoActual.tbPersonas.per_Apellidos}";
                                        oPlanillaEmpleado.SalarioBase = SalarioBase;
                                        oPlanillaEmpleado.horasTrabajadas = horasTrabajadas;
                                        oPlanillaEmpleado.SalarioHora = salarioHora;
                                        oPlanillaEmpleado.totalSalario = totalSalario;
                                        oPlanillaEmpleado.tipoPlanilla = empleadoActual.tbCatalogoDePlanillas.cpla_DescripcionPlanilla;
                                        oPlanillaEmpleado.procentajeComision = porcentajeComision;
                                        oPlanillaEmpleado.totalVentas = totalVentas;
                                        oPlanillaEmpleado.totalComisiones = totalComisiones;
                                        oPlanillaEmpleado.horasExtras = horasExtrasTrabajadas;
                                        oPlanillaEmpleado.totalHorasPermiso = totalHorasPermiso;
                                        oPlanillaEmpleado.TotalIngresosHorasExtras = totalHorasExtras;
                                        oPlanillaEmpleado.totalBonificaciones = totalBonificaciones;
                                        oPlanillaEmpleado.totalIngresosIndivuales = totalIngresosIndivuales;
                                        oPlanillaEmpleado.totalVacaciones = totalVacaciones;
                                        oPlanillaEmpleado.totalIngresos = totalIngresosEmpleado;
                                        oPlanillaEmpleado.totalISR = totalISR;
                                        oPlanillaEmpleado.totalDeduccionesColaborador = colaboradorDeducciones;
                                        oPlanillaEmpleado.totalAFP = totalAFP;
                                        oPlanillaEmpleado.totalInstitucionesFinancieras = totalInstitucionesFinancieras;
                                        oPlanillaEmpleado.otrasDeducciones = totalOtrasDeducciones;
                                        oPlanillaEmpleado.adelantosSueldo = adelantosSueldo;
                                        oPlanillaEmpleado.totalDeduccionesIndividuales = totalDeduccionesIndividuales;
                                        oPlanillaEmpleado.totalDeducciones = totalDeduccionesEmpleado;
                                        oPlanillaEmpleado.totalAPagar = netoAPagarColaborador;
                                        reporte.Add(oPlanillaEmpleado);
                                        oPlanillaEmpleado = null;
                                        #endregion

                                    }
                                    // catch por si hubo un error al generar la planilla de un empleado
                                    catch (Exception ex)
                                    {
                                        // mensaje del error en el registro del colaborador
                                        errores++;
                                    }
                                }
                            }

                        }
                        // catch si se produjo un error al procesar una sola planilla
                        catch (Exception ex)
                        {
                            errores++;
                        }
                    }
                }

                // enviar resultado al cliente
                response.Response = $"El proceso de generación de planilla se realizó, con {errores} errores";
                response.Encabezado = "Exito";
                response.Tipo = errores == 0 ? "success" : "warning";

            }
            // catch se produjo un error fatal en el proceso generar planilla
            catch (Exception ex)
            {
                response.Response = "El proceso de generación de planillas falló, contacte al adminstrador.";
                response.Encabezado = "Error";
                response.Tipo = "error";
            }
            #endregion

            // retornar resultado del proceso
            return Json(new { Data = reporte, Response = response }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SalarioPromedioAnualISR
        private static decimal SalarioPromedioAnualISR(decimal? sueldoBruto, List<decimal> mesesPago, bool esMensual, ref decimal SalarioPromedioAnualPagadoAlAnio, ref decimal salarioPromedioAnualPagadoAlMes)
        {
            decimal sueldoProyeccion = 0;
            if (esMensual)
            {
                //Si es el primer mes a cobrar
                if (mesesPago.Count == 0)
				{
					salarioPromedioAnualPagadoAlMes = ((sueldoBruto * 12)) ?? 0;
				}
				else
				{
					int cantidadMesesPagados = mesesPago.Count;
					//if (netoAPagarColaborador == 0)
					//    netoAPagarColaborador = 37350.66M;
					mesesPago.Add((Decimal)sueldoBruto);

					decimal promedioMesesPago = mesesPago.Average();

					for (int i = cantidadMesesPagados; i <= 12; i++)
					{
						sueldoProyeccion += promedioMesesPago;
					}

					salarioPromedioAnualPagadoAlMes = sueldoProyeccion;
				}
            }
            else
            {
                if (DateTime.Now.Month == 12)
                    //Calcular todas las fechas de este año, aunque haya entrado
                    SalarioPromedioAnualPagadoAlAnio = mesesPago.Sum();
            }


            return (salarioPromedioAnualPagadoAlMes > 0) ? salarioPromedioAnualPagadoAlMes : SalarioPromedioAnualPagadoAlAnio;
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

    class iziToast
    {
        public string Response { get; set; }
        public string Encabezado { get; set; }
        public string Tipo { get; set; }
    }
}
