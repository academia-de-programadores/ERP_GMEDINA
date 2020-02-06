using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace ERP_GMEDINA.Helpers
{
    public class GuardarEnHistorialDePago
    {
        public static int GuardarHistorialDePago(DateTime fechaInicio, DateTime fechaFin, List<ViewModelListaErrores> listaErrores, ref int errores, string codigoDePlanillaGenerada, ERP_GMEDINAEntities db, ref int contador, ref int idDetalleDeduccionHisotorialesContador, ref int idDetalleIngresoHisotorialesContador, tbEmpleados empleadoActual, decimal totalSalario, decimal? totalComisiones, int horasExtrasTrabajadas, int cantidadUnidadesBonos, decimal? totalHorasExtras, decimal? totalHorasPermiso, decimal? totalBonificaciones, decimal? totalIngresosIndivuales, decimal? totalVacaciones, decimal totalISR, decimal totalAFP, decimal? adelantosSueldo, decimal? totalDeduccionesIndividuales, decimal? netoAPagarColaborador, ref IEnumerable<object> listHistorialPago, ref string MensajeError, List<tbHistorialDeduccionPago> lisHistorialDeducciones, List<tbHistorialDeIngresosPago> lisHistorialIngresos, V_InformacionColaborador InformacionDelEmpleadoActual, decimal resultSeptimoDia)
        {
            #region guardar en el historial de pago

            int idHistorialPago = db.tbHistorialDePago
                              .Select(x => x.hipa_IdHistorialDePago)
                              .DefaultIfEmpty(0)
                              .Max();
            int idUser = (int)HttpContext.Current.Session["UserLogin"];

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

                oHistorialPagoEncabezado.hipa_UsuarioCrea = idUser;
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
                        hisorialDeduccioneIterado.hidp_UsuarioCrea = idUser;
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
                        hisorialIngresosIterado.hip_UsuarioCrea = idUser;
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
            return idHistorialPago;
        }
    }
}