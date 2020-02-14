using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Helpers
{
    public static class Deducciones
    {
        public static void ProcesarDeducciones(int monedaDeducciones, List<ViewModelTasasDeCambio> objMonedas, int userId,DateTime fechaInicio, DateTime fechaFin, List<IngresosDeduccionesVoucher> ListaDeduccionesVoucher, List<ViewModelListaErrores> listaErrores, ref int errores, ERP_GMEDINAEntities db, List<V_PlanillaDeducciones> oDeducciones, tbEmpleados empleadoActual, decimal SalarioBase, decimal? totalIngresosEmpleado, ref decimal? colaboradorDeducciones, ref decimal totalAFP, ref decimal? totalInstitucionesFinancieras, ref decimal? totalOtrasDeducciones, ref decimal? adelantosSueldo, out decimal? totalDeduccionesEmpleado, ref decimal? totalDeduccionesIndividuales, out decimal? netoAPagarColaborador, List<tbHistorialDeduccionPago> lisHistorialDeducciones, V_InformacionColaborador InformacionDelEmpleadoActual)
        {
            #region Procesar deducciones

            // instancia de los helpers
            Models.Helpers objHelpers = new Models.Helpers();

            // id del usuario logueado
            int idUser = userId; 

            // validar que la planilla tenga deducciones
            if (oDeducciones.Count > 0)
            {

                // obtener id del tipo de moneda del sueldo del colaborador
                int idMonedaColaborador = db.tbSueldos.Where(x => x.emp_Id == InformacionDelEmpleadoActual.emp_Id && x.sue_Estado == true).Select(x => x.tmon_Id).FirstOrDefault();

                ViewModelTasasDeCambio tasaDeCambio = new ViewModelTasasDeCambio();

                // si el tipo de moneda del colaborador es distinto de la moneda de las deducciones seleccionada en el frontend, hacer la conversion 
                if (idMonedaColaborador != monedaDeducciones)
                {
                    // no tengo idea de que estaba haciendo aquí gg  
                    tasaDeCambio = objMonedas.Where(x => x.tmon_Id == idMonedaColaborador).FirstOrDefault();
                }

                // deducciones de la planilla
                foreach (var iterDeducciones in oDeducciones)
                {
                    decimal? porcentajeColaborador = iterDeducciones.cde_PorcentajeColaborador;
                    decimal? porcentajeEmpresa = iterDeducciones.cde_PorcentajeEmpresa;
                    decimal? montoDeduccionColaborador = SalarioBase;
                    decimal? montoDeduccionVoucher = 0;

                    // si el tipo de moneda del colaborador es distinto de la moneda de las deducciones seleccionada en el frontend, hacer la conversion 
                    if (idMonedaColaborador != monedaDeducciones)
                    {
                        montoDeduccionColaborador = (SalarioBase * tasaDeCambio.tmon_Cambio);
                    }

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
                                if (montoDeduccionColaborador > techosDeduccionesIter.tddu_Techo)
                                {
                                    montoDeduccionColaborador = techosDeduccionesIter.tddu_Techo;
                                    porcentajeColaborador = techosDeduccionesIter.tddu_PorcentajeColaboradores;
                                    porcentajeEmpresa = techosDeduccionesIter.tddu_PorcentajeEmpresa;
                                }
                            }
                        }
                        //sumar las deducciones
                        colaboradorDeducciones += Math.Round((decimal)(montoDeduccionColaborador * porcentajeColaborador) / 100, 2);
                        montoDeduccionVoucher = colaboradorDeducciones;

                        // si el tipo de moneda del colaborador es distinto de la moneda de las deducciones seleccionada en el frontend, hacer la conversion 
                        if (idMonedaColaborador != monedaDeducciones)
                        {
                            montoDeduccionVoucher = Math.Round((decimal)( ( (montoDeduccionColaborador * porcentajeColaborador /100) ) / tasaDeCambio.tmon_Cambio),2);
                        }


                        //Voucher
                        ListaDeduccionesVoucher.Add(new IngresosDeduccionesVoucher
                        {
                            concepto = iterDeducciones.cde_DescripcionDeduccion,
                            monto = montoDeduccionVoucher
                        });

                        //Historial de deducciones
                        lisHistorialDeducciones.Add(new tbHistorialDeduccionPago
                        {
                            cde_IdDeducciones = iterDeducciones.cde_IdDeducciones,
                            hidp_Total = montoDeduccionVoucher
                        });
                    }
                    catch (Exception ex)
                    {
                        listaErrores.Add(new ViewModelListaErrores
                        {
                            Identidad = InformacionDelEmpleadoActual.per_Identidad,
                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
                            Error = $"Error al procesar deducción {iterDeducciones.cde_DescripcionDeduccion}.",
                            PosibleSolucion = "Verifique que la información de esta deducción y sus respectivos techos (si los tiene) esté completa y/o correcta."

                        });
                        errores++;
                    }
                }

                // si la moneda de las deducciones es distintas al tipo de moneda del colaborador, convertir a la moneda del colaborador
                // antes de agregarlo al reporte final
                
                if (idMonedaColaborador != monedaDeducciones)
                {
                    colaboradorDeducciones = Math.Round(((decimal)colaboradorDeducciones) / tasaDeCambio.tmon_Cambio, 2);
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
                        oDeduInstiFinancierasIterador.deif_UsuarioModifica = idUser;
                        oDeduInstiFinancierasIterador.deif_FechaModifica = objHelpers.DatetimeNow();
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
                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                                                       af.dafp_Activo == true
                                                       )
                                                .ToList();

            // respaldo de where's de deduccion afp
            //af.dafp_FechaCrea >= fechaInicio &&
            //af.dafp_FechaCrea <= fechaFin

            if (oDeduccionAfp.Count > 0)
            {
                // sumarlas todas
                foreach (var oDeduccionAfpIter in oDeduccionAfp)
                {
                    try
                    {
                        totalAFP += Math.Round(oDeduccionAfpIter.dafp_AporteLps, 2);

                        //pasar el estado del aporte a pagado
                        //oDeduccionAfpIter.dafp_Pagado = true;
                        oDeduccionAfpIter.dafp_UsuarioModifica = idUser;
                        oDeduccionAfpIter.dafp_FechaModifica = objHelpers.DatetimeNow();
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
                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                        oDeduccionesExtrasColaboradorIterador.dex_UsuarioModifica = idUser;
                        oDeduccionesExtrasColaboradorIterador.dex_FechaModifica = objHelpers.DatetimeNow();
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
                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                        oAdelantosSueldoIterador.adsu_UsuarioModifica = idUser;
                        oAdelantosSueldoIterador.adsu_FechaModifica = objHelpers.DatetimeNow();
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
                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                                                                                   DEX.dei_Activo == true ||
                                                                                   (DEX.dei_PagaSiempre == true && DEX.dei_Activo == true)
                                                                                   )
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

                        if (oDeduccionesIndiColaboradorIterador.dei_Monto == 0 && oDeduccionesIndiColaboradorIterador.dei_PagaSiempre == false)
                        {
                            oDeduccionesIndiColaboradorIterador.dei_Pagado = true;
                        }
                        oDeduccionesIndiColaboradorIterador.dei_UsuarioModifica = idUser;
                        oDeduccionesIndiColaboradorIterador.dei_FechaModifica = objHelpers.DatetimeNow();
                        db.Entry(oDeduccionesIndiColaboradorIterador).State = EntityState.Modified;
                                                
                    }
                    catch (Exception ex)
                    {
                        listaErrores.Add(new ViewModelListaErrores
                        {
                            Identidad = InformacionDelEmpleadoActual.per_Identidad,
                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
        }

        // se usa este actualmente
        public static void PrevisualizarProcesarDeducciones(int monedaDeducciones, List<ViewModelTasasDeCambio> objMonedas, int userId,DateTime fechaInicio, DateTime fechaFin, List<ViewModelListaErrores> listaErrores, ref int errores, ERP_GMEDINAEntities db, List<V_PlanillaDeducciones> oDeducciones, tbEmpleados empleadoActual, decimal SalarioBase, decimal? totalIngresosEmpleado, ref decimal? colaboradorDeducciones, ref decimal totalAFP, ref decimal? totalInstitucionesFinancieras, ref decimal? totalOtrasDeducciones, ref decimal? adelantosSueldo, out decimal? totalDeduccionesEmpleado, ref decimal? totalDeduccionesIndividuales, out decimal? netoAPagarColaborador, V_InformacionColaborador InformacionDelEmpleadoActual)
        {
            #region Procesar deducciones

            // instancia del helper
            Models.Helpers objHelpers = new Models.Helpers();

            // id del usuario logueado
            int idUser = userId;

            // validar que la planilla tenga deducciones
            if (oDeducciones.Count > 0)
            {
                // obtener id del tipo de moneda del sueldo del colaborador
                int idMonedaColaborador = db.tbSueldos.Where(x => x.emp_Id == InformacionDelEmpleadoActual.emp_Id && x.sue_Estado == true).Select(x => x.tmon_Id).FirstOrDefault();
                
                ViewModelTasasDeCambio tasaDeCambio = new ViewModelTasasDeCambio();

                // si el tipo de moneda del colaborador es distinto de la moneda de las deducciones seleccionada en el frontend, hacer la conversion 
                if (idMonedaColaborador != monedaDeducciones)
                {
                    // no tengo idea de que estaba haciendo aquí gg  
                    tasaDeCambio = objMonedas.Where(x => x.tmon_Id == idMonedaColaborador).FirstOrDefault();
                }

                // deducciones de la planilla
                foreach (var iterDeducciones in oDeducciones)
                {
                    try
                    {
                        decimal? porcentajeColaborador = iterDeducciones.cde_PorcentajeColaborador;
                        decimal? porcentajeEmpresa = iterDeducciones.cde_PorcentajeEmpresa;
                        decimal? montoDeduccionColaborador = SalarioBase;

                        // si el tipo de moneda del colaborador es distinto de la moneda de las deducciones seleccionada en el frontend, hacer la conversion 
                        if (idMonedaColaborador != monedaDeducciones)
                        {
                            montoDeduccionColaborador = (SalarioBase * tasaDeCambio.tmon_Cambio);
                        }

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
                                    try
                                    {
                                        if (SalarioBase > techosDeduccionesIter.tddu_Techo)
                                        {
                                            montoDeduccionColaborador = techosDeduccionesIter.tddu_Techo;
                                            porcentajeColaborador = techosDeduccionesIter.tddu_PorcentajeColaboradores;
                                            porcentajeEmpresa = techosDeduccionesIter.tddu_PorcentajeEmpresa;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        listaErrores.Add(new ViewModelListaErrores
                                        {
                                            Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
                                            Error = $"Error al cargar techo de la deducción: {iterDeducciones.cde_DescripcionDeduccion}. Detalles del techo: Techo {techosDeduccionesIter.tddu_Techo}, porcentaje colaborador : {techosDeduccionesIter.tddu_PorcentajeColaboradores}, porcentaje empresa: {techosDeduccionesIter.tddu_PorcentajeEmpresa}.",
                                            PosibleSolucion = "Verifique que la información de esta deducción y sus respectivos techos esté completa y/o correcta."

                                        });
                                        errores++;

                                    }
                                }
                                // si la moneda de las deducciones es distintas al tipo de moneda del colaborador, convertir a la moneda del colaborador
                                // antes de agregarlo al reporte final

                                if (idMonedaColaborador != monedaDeducciones)
                                {
                                    colaboradorDeducciones = Math.Round(((decimal)colaboradorDeducciones) / tasaDeCambio.tmon_Cambio, 2);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            listaErrores.Add(new ViewModelListaErrores
                            {
                                Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
                                Error = $"Error al cargar techos de la deducción: {iterDeducciones.cde_DescripcionDeduccion}.",
                                PosibleSolucion = "Verifique que la información de esta deducción y sus respectivos techos esté completa y/o correcta."

                            });
                            errores++;
                        }

                        //sumar las deducciones
                        colaboradorDeducciones += Math.Round((decimal)(montoDeduccionColaborador * porcentajeColaborador) / 100, 2);
                        
                    }
                    catch (Exception ex)
                    {
                        listaErrores.Add(new ViewModelListaErrores
                        {
                            Identidad = InformacionDelEmpleadoActual.per_Identidad,
                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                    }
                    catch (Exception ex)
                    {
                        listaErrores.Add(new ViewModelListaErrores
                        {
                            Identidad = InformacionDelEmpleadoActual.per_Identidad,
                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                                                       af.dafp_Activo == true)
                                                .ToList();

            // respaldo de where's comentados de deduccion afp
            // af.dafp_Pagado != true &&
            // af.dafp_FechaCrea >= fechaInicio &&
            // af.dafp_FechaCrea <= fechaFin

            if (oDeduccionAfp.Count > 0)
            {
                // sumarlas todas
                foreach (var oDeduccionAfpIter in oDeduccionAfp)
                {
                    try
                    {
                        totalAFP += Math.Round(oDeduccionAfpIter.dafp_AporteLps, 2);
                    }
                    catch (Exception ex)
                    {
                        listaErrores.Add(new ViewModelListaErrores
                        {
                            Identidad = InformacionDelEmpleadoActual.per_Identidad,
                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
                            Error = $"Error al procesar deduccion AFP. {oDeduccionAfpIter.tbAFP.afp_Descripcion} - {oDeduccionAfpIter.dafp_AporteLps}",
                            PosibleSolucion = "Verifique que la información de esta deducción esté correcta y/o completa."

                        });
                        errores++;
                    }

                }
            }

            // deducciones por equipo de trabajo
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

                    }
                    catch (Exception ex)
                    {
                        listaErrores.Add(new ViewModelListaErrores
                        {
                            Identidad = InformacionDelEmpleadoActual.per_Identidad,
                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                    }
                    catch (Exception ex)
                    {
                        listaErrores.Add(new ViewModelListaErrores
                        {
                            Identidad = InformacionDelEmpleadoActual.per_Identidad,
                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                    }
                    catch (Exception ex)
                    {
                        listaErrores.Add(new ViewModelListaErrores
                        {
                            Identidad = InformacionDelEmpleadoActual.per_Identidad,
                            NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
        }

        public static void ProcesarDeduccionesParaPrevisualizacion(DateTime fechaInicio, DateTime fechaFin, ERP_GMEDINAEntities db, List<V_PlanillaDeducciones> oDeducciones, tbEmpleados empleadoActual, decimal SalarioBase, decimal? totalIngresosEmpleado, ref decimal? colaboradorDeducciones, ref decimal totalAFP, ref decimal? totalInstitucionesFinancieras, ref decimal? totalOtrasDeducciones, ref decimal? adelantosSueldo, out decimal? totalDeduccionesEmpleado, ref decimal? totalDeduccionesIndividuales, out decimal? netoAPagarColaborador)
        {
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
        }

    }
}