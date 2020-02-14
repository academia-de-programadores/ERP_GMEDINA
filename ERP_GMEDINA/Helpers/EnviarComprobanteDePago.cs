using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ERP_GMEDINA.Helpers
{
    public class EnviarComprobanteDePago
    {
        public static void EnviarComprobanteDePagoColaborador(string moneda, bool? enviarEmail, DateTime fechaInicio, DateTime fechaFin, General utilities, ref List<IngresosDeduccionesVoucher> ListaIngresosVoucher, ref List<IngresosDeduccionesVoucher> ListaDeduccionesVoucher, ComprobantePagoModel oComprobantePagoModel, List<ViewModelListaErrores> listaErrores, ref int errores, ERP_GMEDINAEntities db, tbEmpleados empleadoActual, decimal? totalIngresosEmpleado, decimal? totalDeduccionesEmpleado, decimal? netoAPagarColaborador, V_InformacionColaborador InformacionDelEmpleadoActual)
        {
            #region Enviar comprobante de pago por email
            if (enviarEmail != null && enviarEmail == true)
            {
                oComprobantePagoModel.moneda = moneda;
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

                // enviar comprobante de pago
                try
                {
                    if (!utilities.SendEmail(oComprobantePagoModel))
                    {
                        listaErrores.Add(new ViewModelListaErrores
                        {
                            Identidad = InformacionDelEmpleadoActual.per_Identidad,
                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                        NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
                        Error = "Error al Enviar comprobante de pago.",
                        PosibleSolucion = "Verifique que la información del perfil del colaborador esté completa y/o correcta."

                    });
                    errores++;
                }
            }
            #endregion
        }
    }
}