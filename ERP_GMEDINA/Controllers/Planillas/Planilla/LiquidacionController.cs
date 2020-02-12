using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Helpers;
using ERP_GMEDINA.Models;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class LiquidacionController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        Models.Helpers Function = new Models.Helpers();
        public decimal total { get; set; }

        // GET: Liquidacion
        [SessionManager("Liquidacion/Index")]
        public ActionResult Index()
        {
            Session["Liquidaciones"] = null;
            return View();
        }
        
        [HttpGet]
        public string GetEmpleadosAreas()
        {
            return General.ObtenerEmpleados();
        }

        //OBTENER LA INFORMACION DE EMPLEADOS
        [HttpPost]
        public JsonResult Obtener_Informacion_Empleado(int IdEmpleado, DateTime fechaFin, int IdMotivo)
        {
            int anios = 0, meses = 0, dias = 0;
            object json, salarios;
            Session["Liquidaciones"] = "";
            using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
            {
                try
                {
                    var consulta = (from te in db.tbEmpleados
                                    join ta in db.tbAreas on te.area_Id equals ta.area_Id into ta_join
                                    from ta in ta_join.DefaultIfEmpty()
                                    join td in db.tbDepartamentos on te.depto_Id equals td.depto_Id into td_join
                                    from td in td_join.DefaultIfEmpty()
                                    join tp in db.tbPersonas on te.per_Id equals tp.per_Id into tp_join
                                    from tp in tp_join.DefaultIfEmpty()
                                    join tc in db.tbCargos on te.car_Id equals tc.car_Id into tc_join
                                    from tc in tc_join.DefaultIfEmpty()
                                    join ts in db.tbSueldos on te.emp_Id equals ts.emp_Id into ts_join
                                    from ts in ts_join.DefaultIfEmpty()
                                    join ttm in db.tbTipoMonedas on ts.tmon_Id equals ttm.tmon_Id into ttm_join
                                    from ttm in ttm_join.DefaultIfEmpty()
                                    where
                                      te.emp_Id == IdEmpleado &&
                                      te.emp_Estado == true &&
                                      ta.area_Estado == true &&
                                      td.depto_Estado == true &&
                                      tp.per_Estado == true &&
                                      tc.car_Estado == true &&
                                      ts.sue_Estado == true &&
                                      ttm.tmon_Estado == true
									select new
                                    {
                                        NúmeroIdentidad = tp.per_Identidad.Substring(0, 4) + "-" + tp.per_Identidad.Substring(4, 4) + "-" + tp.per_Identidad.Substring(9, tp.per_Identidad.Length - 9),
                                        nombreEmpleado = tp.per_Nombres,
                                        apellidoEmpleado = tp.per_Apellidos,
                                        fechaIngreso = (te.emp_Fechaingreso.Year + "/" +
                                                       ((te.emp_Fechaingreso.Month > 9) ? te.emp_Fechaingreso.Month.ToString() : "0" + te.emp_Fechaingreso.Month.ToString()) + "/" +
                                                       ((te.emp_Fechaingreso.Day > 9) ? te.emp_Fechaingreso.Day.ToString() : "0" + te.emp_Fechaingreso.Day.ToString())),
                                        sexoEmpleado = (tp.per_Sexo == "m" || tp.per_Sexo == "M") ? "Masculino" : "Femenino",
                                        edadEmpleado = (int?)tp.per_Edad,
                                        descripcionDepartamento = td.depto_Descripcion,
                                        descripcionCargo = tc.car_Descripcion,
                                        cantidadSueldo = (decimal?)ts.sue_Cantidad,
                                        descripcionMoneda = ttm.tmon_Descripcion
                                    }).ToList();
                    //OBTENER: ANTIGUEDAD
                    int TotalDiasLaborados = (int)Liquidacion.Dias360Mes(fechaFin, IdEmpleado);

                    int residuo = 0;
                    //OBTENER AÑOS
                    anios = (TotalDiasLaborados > 360) ? TotalDiasLaborados / 360 : 0;
                    //SETEAR LA VARIABLE DE RESIDUO
                    residuo = (anios > 0) ? TotalDiasLaborados - (anios * 360) : TotalDiasLaborados;
                    //OBTENER MESES
                    meses = (residuo > 30) ? (residuo / 30) : 0;
                    //SETEAR LA VARIABLE DE RESIDUO
                    residuo = (meses > 0) ? residuo - (meses * 30) : residuo;
                    //OBTENER DIAS
                    dias = residuo;

                    salarios = Helpers.Liquidacion.EjecutarCalculosSalarios(IdEmpleado);
                    //OBTENER: ABSTRACCION - SALARIO ORDINARIO DIARIO 
                    decimal SalarioOrdinarioMensual = Helpers.Liquidacion.Calculo_SalarioOrdinarioMensual(IdEmpleado);
                    //ALMACENAMIENTO DE VALORES DE RETORNO EN METODOS UTILITARIOS
                    decimal Monto_Preaviso = Helpers.Liquidacion.Calculo_PagoDePreaviso(IdEmpleado, SalarioOrdinarioMensual, TotalDiasLaborados);
                    decimal Monto_Cesantia = Helpers.Liquidacion.Calculo_PagoDeCesantia(IdEmpleado, SalarioOrdinarioMensual, TotalDiasLaborados);
                    decimal Monto_DecimoCuartoProporcional = Helpers.Liquidacion.Calculo_DecimoCuartoMesProporcional(IdEmpleado, fechaFin, SalarioOrdinarioMensual);
                    decimal Monto_DecimoTercerProporcional = Helpers.Liquidacion.Calculo_DecimoTercerMesProporcional(IdEmpleado, fechaFin, SalarioOrdinarioMensual);
                    decimal Monto_VacacionesProporcionales = Helpers.Liquidacion.Calculo_VacacionesProporcionales(IdEmpleado, fechaFin, SalarioOrdinarioMensual, TotalDiasLaborados);
                    

                    //CALCULAR EL PAGO DE CONCEPTOS PROPORCIONAL AL MOTIVO DE LIQUIDACION
                    var PorcentajeLiqudiacion = from Porcentajes in db.tbPorcentajeMotivoLiquidacion
                                                join Motivos in db.tbMotivoLiquidacion on Porcentajes.moli_IdMotivo equals Motivos.moli_IdMotivo
                                                where Motivos.moli_IdMotivo == IdMotivo
                                                select new
                                                {
                                                    Cesantia = Porcentajes.pml_Cesantia,
                                                    Preaviso = Porcentajes.pml_Preaviso,
                                                    DecimoTercerProp = Porcentajes.pml_DecimoTercerProp,
                                                    DecimoCuartoProp = Porcentajes.pml_DecimoCuartoProp,
                                                    Vacaciones = Porcentajes.pml_Vacaciones
                                                };
                    //ITERAR LA LISTA CON LOS PORCENTAJES DE LIQUIDACION CORRESPONDIENTES
                    foreach (var iter in PorcentajeLiqudiacion)
                    {
                        Monto_Preaviso = Math.Round(((decimal)(Monto_Preaviso * iter.Preaviso) / 100), 2);
                        Monto_Cesantia = Math.Round(((decimal)(Monto_Cesantia * iter.Cesantia) / 100), 2);
                        Monto_DecimoCuartoProporcional = Math.Round(((decimal)(Monto_DecimoCuartoProporcional * iter.DecimoCuartoProp) / 100), 2);
                        Monto_DecimoTercerProporcional = Math.Round(((decimal)(Monto_DecimoTercerProporcional * iter.DecimoTercerProp) / 100), 2);
                        Monto_VacacionesProporcionales = Math.Round(((decimal)(Monto_VacacionesProporcionales * iter.Vacaciones) / 100), 2);
                    }

                    //SUMATORIA DEL TOTAL INICIAL SIN CONCEPTOS ADICIONALES 
                    decimal Total_Liquidacion = Monto_Preaviso + Monto_Cesantia + Monto_DecimoCuartoProporcional + Monto_DecimoTercerProporcional + Monto_VacacionesProporcionales;
                    //INICIALIZAR LA CONSTRUCCION DEL OBJETO EN SESSION
                    LiquidacionViewModel LiquidacionVM = new LiquidacionViewModel();
                    LiquidacionVM.emp_Id = IdEmpleado;
                    LiquidacionVM.FechaLiquidacion = fechaFin;
                    LiquidacionVM.SalarioOrdinarioMensual_Liq = SalarioOrdinarioMensual;
                    LiquidacionVM.SalarioPromedioMensual_Liq = (SalarioOrdinarioMensual * 14) / 12;
                    LiquidacionVM.SalarioOrdinarioDiario_Liq = (SalarioOrdinarioMensual) / 30 ;
                    LiquidacionVM.SalarioPromedioDiario_Liq = (SalarioOrdinarioMensual * 14) / 360;
                    LiquidacionVM.Preaviso_Liq = Monto_Preaviso;
                    LiquidacionVM.Cesantia_Liq = Monto_Cesantia;
                    LiquidacionVM.DecimoTercerMesProporcional_Liq = Monto_DecimoTercerProporcional;
                    LiquidacionVM.DecimoCuartoMesProporcional_Liq = Monto_DecimoCuartoProporcional;
                    LiquidacionVM.VacacionesPendientes_Liq = Monto_VacacionesProporcionales;
                    LiquidacionVM.MontoTotalLiquidacion = Math.Round(Total_Liquidacion, 2);
                    LiquidacionVM.moli_Id = IdMotivo;
                    //ALMACENAMIENTO EN LA SESIÓN
                    LiquidacionViewModel sessionLiquidacion = new LiquidacionViewModel();
                    sessionLiquidacion = LiquidacionVM;
                    Session["Liquidaciones"] = sessionLiquidacion;
                    json = new
                    {
                        Preaviso = Monto_Preaviso,
                        Cesantia = Monto_Cesantia,
                        DecimoCuarto = Monto_DecimoCuartoProporcional,
                        DecimoTercer = Monto_DecimoTercerProporcional,
                        Vacaciones = Monto_VacacionesProporcionales,
                        Total_Liquidacion = Total_Liquidacion,
                        consulta,
                        anios,
                        meses,
                        dias,
                        salarios
                    };
                    return Json(json, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        //GET: MotivosLiquidacion
        public JsonResult GetMotivoLiquidacion()
        {
            //using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
            //{
                try
                {
                    var listaMotivo = from motivoLiq in db.tbMotivoLiquidacion
                                        select new { Id = motivoLiq.moli_IdMotivo, Descripcion = motivoLiq.moli_Descripcion };
                    return Json(listaMotivo, JsonRequestBehavior.AllowGet);
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    return Json("Error");
                }
            //}
        }


        //GET: CALCULAR LIQUIDACION
        [HttpPost]
        public JsonResult CalcularLiquidacion(LiquidacionViewModel Liquidacion)
        {
            try
            {
                LiquidacionViewModel list = (LiquidacionViewModel)Session["Liquidaciones"];
                //OBTENER: ABSTRACCION - SALARIO ORDINARIO DIARIO 
                decimal SalarioOrdinarioMensual = Helpers.Liquidacion.Calculo_SalarioOrdinarioMensual((int)list.emp_Id);
                //SETEO DE VARIABLES EN EL OBJETO DE LIQUIDACION
                list.SalariosAdeudados = Liquidacion.SalariosAdeudados;
                list.OtrosPagos = Liquidacion.OtrosPagos;
                list.PagoHEPendiente = Liquidacion.PagoHEPendiente;
                list.ValorBonoEducativo = Liquidacion.ValorBonoEducativo;
                list.PagoSeptimoDia = Liquidacion.PagoSeptimoDia;
                list.BonoPorVacaciones = Liquidacion.BonoPorVacaciones;
                list.ReajusteSalarial = Liquidacion.ReajusteSalarial;
                list.DecimoTercerMesAdeudado = Liquidacion.DecimoTercerMesAdeudado;
                list.DecimoCuartoMesAdeudado = Liquidacion.DecimoCuartoMesAdeudado;
                list.BonificacionVacaciones = (Liquidacion.BonificacionVacaciones) * ((SalarioOrdinarioMensual * 14) / 360);
                list.PagoPorEmbarazo = (Liquidacion.PagoPorEmbarazo) * ((SalarioOrdinarioMensual * 14) / 360);
                list.PagoPorLactancia = (Liquidacion.PagoPorLactancia) * ((((SalarioOrdinarioMensual * 14) / 360) / 9) * 8);
                list.PrePosNatal = (Liquidacion.PrePosNatal) * ((SalarioOrdinarioMensual * 14) / 360);
                list.PagoPorDiasFeriado = (Liquidacion.PagoPorDiasFeriado) * ((SalarioOrdinarioMensual * 14) / 360);
                //Validar las entradas nulas
                list.Preaviso_Liq = (list.Preaviso_Liq == null) ? 0 : list.Preaviso_Liq;
                list.Cesantia_Liq = (list.Cesantia_Liq == null) ? 0 : list.Cesantia_Liq;
                list.DecimoTercerMesProporcional_Liq = (list.DecimoTercerMesProporcional_Liq == null) ? 0 : list.DecimoTercerMesProporcional_Liq;
                list.DecimoCuartoMesProporcional_Liq = (list.DecimoCuartoMesProporcional_Liq == null) ? 0 : list.DecimoCuartoMesProporcional_Liq;
                list.VacacionesPendientes_Liq = (list.VacacionesPendientes_Liq == null) ? 0 : list.VacacionesPendientes_Liq;
                list.SalariosAdeudados = (list.SalariosAdeudados == null) ? 0 : list.SalariosAdeudados;
                list.OtrosPagos = (list.OtrosPagos == null) ? 0 : list.OtrosPagos;
                list.PagoHEPendiente = (list.PagoHEPendiente == null) ? 0 : list.PagoHEPendiente;
                list.ValorBonoEducativo = (list.ValorBonoEducativo == null) ? 0 : list.ValorBonoEducativo;
                list.PagoSeptimoDia = (list.PagoSeptimoDia == null) ? 0 : list.PagoSeptimoDia;
                list.BonoPorVacaciones = (list.BonoPorVacaciones == null) ? 0 : list.BonoPorVacaciones;
                list.ReajusteSalarial = (list.ReajusteSalarial == null) ? 0 : list.ReajusteSalarial;
                list.DecimoTercerMesAdeudado = (list.DecimoTercerMesAdeudado == null) ? 0 : list.DecimoTercerMesAdeudado;
                list.DecimoCuartoMesAdeudado = (list.DecimoCuartoMesAdeudado == null) ? 0 : list.DecimoCuartoMesAdeudado;
                list.BonificacionVacaciones = (list.BonificacionVacaciones == null) ? 0 : list.BonificacionVacaciones;
                list.PagoPorEmbarazo = (list.PagoPorEmbarazo == null) ? 0 : list.PagoPorEmbarazo;
                list.PagoPorLactancia = (list.PagoPorLactancia == null) ? 0 : list.PagoPorLactancia;
                list.PrePosNatal = (list.PrePosNatal == null) ? 0 : list.PrePosNatal;
                list.PagoPorDiasFeriado = (list.PagoPorDiasFeriado == null) ? 0 : list.PagoPorDiasFeriado;
                //Total
                list.MontoTotalLiquidacion = list.Preaviso_Liq +
                list.Cesantia_Liq + 
                list.DecimoTercerMesProporcional_Liq +
                list.DecimoCuartoMesProporcional_Liq +
                list.VacacionesPendientes_Liq +
                list.SalariosAdeudados +
                list.OtrosPagos +
                list.PagoHEPendiente +
                list.ValorBonoEducativo +
                list.PagoSeptimoDia +
                list.BonoPorVacaciones +
                list.ReajusteSalarial +
                list.DecimoTercerMesAdeudado +
                list.DecimoCuartoMesAdeudado +
                list.BonificacionVacaciones +
                list.PagoPorEmbarazo +
                list.PagoPorLactancia +
                list.PrePosNatal +
                list.PagoPorDiasFeriado;
                list.MontoTotalLiquidacion = Math.Round((decimal)list.MontoTotalLiquidacion, 2);
                Session["Liquidaciones"] = list;
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        //REGISTRAR LA LIQUIDACION
        [HttpGet]
        public JsonResult RegistrarLiquidacion()
        {
            //Instancia del objeto tbLiquidaciones
            LiquidacionViewModel tbLiquidaciones = (LiquidacionViewModel)Session["Liquidaciones"];
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            int idUser = (int)Session["UserLogin"];
            tbLiquidaciones.UsuarioCrea = idUser;
            tbLiquidaciones.FechaCrea = Function.DatetimeNow();
            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
            string response = "bien";
            IEnumerable<object> listLiquidacion = null;
            string MensajeError = "bien";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listLiquidacion = db.UDP_Plani_tbHistorialDeLiquidaciones_Insert(tbLiquidaciones.emp_Id,
                                                                                     tbLiquidaciones.FechaLiquidacion,
                                                                                     tbLiquidaciones.SalarioOrdinarioMensual_Liq,
                                                                                     tbLiquidaciones.SalarioPromedioMensual_Liq,
                                                                                     tbLiquidaciones.SalarioOrdinarioDiario_Liq,
                                                                                     tbLiquidaciones.SalarioPromedioDiario_Liq,
                                                                                     tbLiquidaciones.Preaviso_Liq,
                                                                                     tbLiquidaciones.Cesantia_Liq,
                                                                                     tbLiquidaciones.DecimoTercerMesProporcional_Liq,
                                                                                     tbLiquidaciones.DecimoCuartoMesProporcional_Liq,
                                                                                     tbLiquidaciones.VacacionesPendientes_Liq,
                                                                                     tbLiquidaciones.SalariosAdeudados,
                                                                                     tbLiquidaciones.OtrosPagos,
                                                                                     tbLiquidaciones.PagoHEPendiente,
                                                                                     tbLiquidaciones.ValorBonoEducativo,
                                                                                     tbLiquidaciones.PagoSeptimoDia,
                                                                                     tbLiquidaciones.BonoPorVacaciones,
                                                                                     tbLiquidaciones.ReajusteSalarial,
                                                                                     tbLiquidaciones.DecimoTercerMesAdeudado,
                                                                                     tbLiquidaciones.DecimoCuartoMesAdeudado,
                                                                                     tbLiquidaciones.BonificacionVacaciones,
                                                                                     tbLiquidaciones.PagoPorEmbarazo,
                                                                                     tbLiquidaciones.PagoPorLactancia,
                                                                                     tbLiquidaciones.PrePosNatal,
                                                                                     tbLiquidaciones.PagoPorDiasFeriado,
                                                                                     tbLiquidaciones.MontoTotalLiquidacion,
                                                                                     tbLiquidaciones.UsuarioCrea,
                                                                                     tbLiquidaciones.FechaCrea,
                                                                                     tbLiquidaciones.moli_Id);

                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbHistorialDeLiquidaciones_Insert_Result Resultado in listLiquidacion)
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
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                //SI EL MODELO NO ES VÁLIDO, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                response = "error";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}