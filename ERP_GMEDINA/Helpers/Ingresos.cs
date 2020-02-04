using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace ERP_GMEDINA.Helpers
{
    public static class Ingresos
    {
        public static void ProcesarIngresos(DateTime fechaInicio, DateTime fechaFin, List<IngresosDeduccionesVoucher> ListaIngresosVoucher, List<ViewModelListaErrores> listaErrores, ref int errores, ERP_GMEDINAEntities db, tbEmpleados empleadoActual, ref decimal SalarioBase, out int horasTrabajadas, ref decimal salarioHora, ref decimal totalSalario, ref decimal? totalComisiones, out int horasExtrasTrabajadas, ref int cantidadUnidadesBonos, ref decimal? totalHorasExtras, ref decimal? totalHorasPermiso, ref decimal? totalBonificaciones, ref decimal? totalIngresosIndivuales, ref decimal? totalVacaciones, out decimal? totalIngresosEmpleado, List<tbHistorialDeIngresosPago> lisHistorialIngresos, out V_InformacionColaborador InformacionDelEmpleadoActual, out decimal resultSeptimoDia)
        {
            #region Procesar ingresos

            // informacion del colaborador actual
            InformacionDelEmpleadoActual = db.V_InformacionColaborador
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
                    NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                salarioHora = Math.Round(SalarioBase / 240,2);
            }
            catch (Exception ex)
            {
                listaErrores.Add(new ViewModelListaErrores
                {
                    Identidad = InformacionDelEmpleadoActual.per_Identidad,
                    NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                    NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                    totalHorasPermiso = Math.Round(totalHorasPermiso.Value, 2);
                }
                catch (Exception ex)
                {
                    listaErrores.Add(new ViewModelListaErrores
                    {
                        Identidad = InformacionDelEmpleadoActual.per_Identidad,
                        NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                                NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                        NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                                NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                        NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                                NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                        NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                                NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                        NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                            if (oIngresosIndiColaboradoresIterador.ini_PagaSiempre == false)
                            {
                                oIngresosIndiColaboradoresIterador.ini_Pagado = true;
                                oIngresosIndiColaboradoresIterador.ini_FechaModifica = DateTime.Now;
                                db.Entry(oIngresosIndiColaboradoresIterador).State = EntityState.Modified;
                            }

                            //agregarlo al voucher
                            ListaIngresosVoucher.Add(new IngresosDeduccionesVoucher
                            {
                                concepto = oIngresosIndiColaboradoresIterador.ini_Motivo,
                                monto = Math.Round(oIngresosIndiColaboradoresIterador.ini_Monto.Value, 2)
                            });
                        }
                        catch (Exception ex)
                        {
                            listaErrores.Add(new ViewModelListaErrores
                            {
                                Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
                                Error = $"Error al procesar ingreso inidividual número {oIngresosIndiColaboradoresIterador.ini_IdIngresosIndividuales}, con motivo {oIngresosIndiColaboradoresIterador.ini_Motivo}, con monto: {oIngresosIndiColaboradoresIterador.ini_Monto}.",
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
            resultSeptimoDia = 0;
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
                    NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
                    Error = "Error al calcular séptimo día.",
                    PosibleSolucion = "Verifique que la información en el historial de horas trabajadas del colaborador esté correcta."

                });
                errores++;
            }


            #endregion

            // total ingresos
            totalIngresosEmpleado =Math.Round(totalIngresosIndivuales.Value + totalSalario + totalComisiones.Value + totalHorasExtras.Value + totalBonificaciones.Value + totalVacaciones.Value + totalHorasPermiso.Value + resultSeptimoDia,2);

            #endregion
        }

        public static void ProcesarIngresosParaPrevisualizacion(DateTime fechaInicio, DateTime fechaFin, ERP_GMEDINAEntities db, tbEmpleados empleadoActual, out decimal SalarioBase, out int horasTrabajadas, out decimal salarioHora, out decimal totalSalario, ref decimal? totalComisiones, out int horasExtrasTrabajadas, ref int cantidadUnidadesBonos, ref decimal? totalHorasExtras, ref decimal? totalHorasPermiso, ref decimal? totalBonificaciones, ref decimal? totalIngresosIndivuales, ref decimal? totalVacaciones, out decimal? totalIngresosEmpleado, out V_InformacionColaborador InformacionDelEmpleadoActual)
        {
            #region Procesar ingresos

            // informacion del colaborador actual
            InformacionDelEmpleadoActual = db.V_InformacionColaborador
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
        }


        #region  procesar ingresos previsualización

        public static void PrevisualizarProcesarIngresos(DateTime fechaInicio, DateTime fechaFin,  List<ViewModelListaErrores> listaErrores, ref int errores, ERP_GMEDINAEntities db, tbEmpleados empleadoActual, ref decimal SalarioBase, out int horasTrabajadas, ref decimal salarioHora, ref decimal totalSalario, ref decimal? totalComisiones, out int horasExtrasTrabajadas, ref int cantidadUnidadesBonos, ref decimal? totalHorasExtras, ref decimal? totalHorasPermiso, ref decimal? totalBonificaciones, ref decimal? totalIngresosIndivuales, ref decimal? totalVacaciones, out decimal? totalIngresosEmpleado, out V_InformacionColaborador InformacionDelEmpleadoActual, out decimal resultSeptimoDia)
        {
            #region Procesar ingresos

            // informacion del colaborador actual
            InformacionDelEmpleadoActual = db.V_InformacionColaborador
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
                    NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                salarioHora = Math.Round(SalarioBase / 240);
            }
            catch (Exception ex)
            {
                listaErrores.Add(new ViewModelListaErrores
                {
                    Identidad = InformacionDelEmpleadoActual.per_Identidad,
                    NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
                    Error = "Error al calcular sueldo por hora. Perfil del colaborador incompleto o incorrecto",
                    PosibleSolucion = "Verifique que perfil del colaborador (sueldo) esté completo y/o correcto."

                });
                errores++;
            }


            // total salario o sueldo bruto
            try
            {
                totalSalario = Math.Round((Decimal)salarioHora * horasTrabajadas, 2);
            }
            catch (Exception ex)
            {
                listaErrores.Add(new ViewModelListaErrores
                {
                    Identidad = InformacionDelEmpleadoActual.per_Identidad,
                    NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                // sumar todas las horas con permiso
                try
                {
                    int CantidadHorasPermisoActual = 0;
                    foreach (var iterHorasPermiso in horasConPermiso)
                    {
                        try
                        {
                            CantidadHorasPermisoActual = iterHorasPermiso.hper_Duracion;

                            totalHorasPermiso += CantidadHorasPermisoActual * (((iterHorasPermiso.hper_PorcentajeIndemnizado * salarioHora) / 100));
                        }
                        catch (Exception ex)
                        {
                            listaErrores.Add(new ViewModelListaErrores
                            {
                                Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
                                Error = $"Error al procesa hora con permiso registrada el: {iterHorasPermiso.hper_FechaCrea}, con duracion: {iterHorasPermiso.hper_Duracion}, inicio: {iterHorasPermiso.hper_fechaInicio}, finaliza: {iterHorasPermiso.hper_fechaFin}",
                                PosibleSolucion = "Verifique que dichas horas con permiso hayan sido registradas de forma correcta."

                            });
                            errores++;
                        }

                    }
                    totalHorasPermiso = Math.Round(totalHorasPermiso.Value,2);
                }
                catch (Exception ex)
                {
                    listaErrores.Add(new ViewModelListaErrores
                    {
                        Identidad = InformacionDelEmpleadoActual.per_Identidad,
                        NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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

                        }
                        catch (Exception ex)
                        {
                            listaErrores.Add(new ViewModelListaErrores
                            {
                                Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                        NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
                        Error = "Error al procesar las comisiones del colaborador.",
                        PosibleSolucion = "Verifique que las comisiones registradas al colaborador sean las correctas."

                    });
                    errores++;
                }
            }

            // horas extras
            horasExtrasTrabajadas = 0;
            try
            {
                horasExtrasTrabajadas = db.tbHistorialHorasTrabajadas
                                    .Where(x => x.emp_Id == empleadoActual.emp_Id &&
                                            x.htra_Estado == true &&
                                            x.tbTipoHoras.tiho_Recargo > 0 &&
                                            x.htra_Fecha >= fechaInicio &&
                                            x.htra_Fecha <= fechaFin)
                                    .Select(x => x.htra_CantidadHoras)
                                    .DefaultIfEmpty(0)
                                    .Sum();
            }
            catch (Exception ex)
            {
                listaErrores.Add(new ViewModelListaErrores
                {
                    Identidad = InformacionDelEmpleadoActual.per_Identidad,
                    NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
                    Error = "Error al cargar las horas extras del colaborador.",
                    PosibleSolucion = "Verifique que las horas extras hayan sido registradas al colaborador de forma correcta."

                });
                errores++;
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

                        }
                        catch (Exception ex)
                        {
                            listaErrores.Add(new ViewModelListaErrores
                            {
                                Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
                                Error = $"Error al cargar horas extras registradas: {iterHorasExtras.htra_Fecha}, cantidad: {iterHorasExtras.htra_CantidadHoras}.",
                                PosibleSolucion = "Verifique que las horas registradas al colaborador sean las correctas."

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
                        NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
                        Error = "Error al procesar horas extras.",
                        PosibleSolucion = "Verifique que las horas extras hayan sido registradas al colaborador de forma correcta."

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
                        }
                        catch (Exception ex)
                        {
                            listaErrores.Add(new ViewModelListaErrores
                            {
                                Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                        NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                        }
                        catch (Exception ex)
                        {
                            listaErrores.Add(new ViewModelListaErrores
                            {
                                Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
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
                        NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
                        Error = "Error al procesar pago de vaciones.",
                        PosibleSolucion = "Verifique que los rangos de fecha registrados son correctos. Verifique que las vacacionese se hayan registrado de forma correcta."

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
                        }
                        catch (Exception ex)
                        {
                            listaErrores.Add(new ViewModelListaErrores
                            {
                                Identidad = InformacionDelEmpleadoActual.per_Identidad,
                                NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
                                Error = $"Error al procesar ingreso inidividual número {oIngresosIndiColaboradoresIterador.ini_IdIngresosIndividuales}, con motivo {oIngresosIndiColaboradoresIterador.ini_Motivo} y monto: {oIngresosIndiColaboradoresIterador.ini_Monto}.",
                                PosibleSolucion = "Verifique la información de dichos ingresos sea correcta."

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
                        NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
                        Error = $"Error al procesar ingresos individuales del colaborador.",
                        PosibleSolucion = "Verifique que la información de dichos ingresos se hayan registrado de forma correcta."

                    });
                    errores++;

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
            resultSeptimoDia = 0;
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
            }
            catch (Exception ex)
            {
                listaErrores.Add(new ViewModelListaErrores
                {
                    Identidad = InformacionDelEmpleadoActual.per_Identidad,
                    NombreColaborador = InformacionDelEmpleadoActual.per_Nombres + " " + InformacionDelEmpleadoActual.per_Apellidos,
                    Error = "Error al calcular séptimo día.",
                    PosibleSolucion = "Verifique que la información en el historial de horas trabajadas del colaborador esté correcta."

                });
                errores++;
            }


            #endregion

            // total ingresos
            totalIngresosEmpleado = Math.Round(totalIngresosIndivuales.Value + totalSalario + totalComisiones.Value + totalHorasExtras.Value + totalBonificaciones.Value + totalVacaciones.Value + totalHorasPermiso.Value + resultSeptimoDia,2);

            #endregion
        }
        #endregion
    }
}