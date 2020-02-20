namespace ERP_GMEDINA.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ERP_GMEDINAEntities : DbContext
    {
        public ERP_GMEDINAEntities()
            : base("name=ERP_GMEDINAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbAccesoRol> tbAccesoRol { get; set; }
        public virtual DbSet<tbBitacoraErrores> tbBitacoraErrores { get; set; }
        public virtual DbSet<tbObjeto> tbObjeto { get; set; }
        public virtual DbSet<tbRol> tbRol { get; set; }
        public virtual DbSet<tbRolesUsuario> tbRolesUsuario { get; set; }
        public virtual DbSet<tbUsuario> tbUsuario { get; set; }
        public virtual DbSet<tbActividadEconomica> tbActividadEconomica { get; set; }
        public virtual DbSet<tbBanco> tbBanco { get; set; }
        public virtual DbSet<tbCuentasBanco> tbCuentasBanco { get; set; }
        public virtual DbSet<tbDenominacion> tbDenominacion { get; set; }
        public virtual DbSet<tbDepartamento> tbDepartamento { get; set; }
        public virtual DbSet<tbEmpleado> tbEmpleado { get; set; }
        public virtual DbSet<tbIdentificacionCliente> tbIdentificacionCliente { get; set; }
        public virtual DbSet<tbMoneda> tbMoneda { get; set; }
        public virtual DbSet<tbMunicipio> tbMunicipio { get; set; }
        public virtual DbSet<tbParametro> tbParametro { get; set; }
        public virtual DbSet<tbTipoIdentificacion> tbTipoIdentificacion { get; set; }
        public virtual DbSet<tbUnidadMedida> tbUnidadMedida { get; set; }
        public virtual DbSet<tbBodega> tbBodega { get; set; }
        public virtual DbSet<tbBodegaDetalle> tbBodegaDetalle { get; set; }
        public virtual DbSet<tbBox> tbBox { get; set; }
        public virtual DbSet<tbBoxDetalle> tbBoxDetalle { get; set; }
        public virtual DbSet<tbEntrada> tbEntrada { get; set; }
        public virtual DbSet<tbEntradaDetalle> tbEntradaDetalle { get; set; }
        public virtual DbSet<tbEstadoInventarioFisico> tbEstadoInventarioFisico { get; set; }
        public virtual DbSet<tbEstadoMovimiento> tbEstadoMovimiento { get; set; }
        public virtual DbSet<tbInventarioFisico> tbInventarioFisico { get; set; }
        public virtual DbSet<tbInventarioFisicoDetalle> tbInventarioFisicoDetalle { get; set; }
        public virtual DbSet<tbProducto> tbProducto { get; set; }
        public virtual DbSet<tbProductoCategoria> tbProductoCategoria { get; set; }
        public virtual DbSet<tbProductoSubcategoria> tbProductoSubcategoria { get; set; }
        public virtual DbSet<tbProveedor> tbProveedor { get; set; }
        public virtual DbSet<tbSalida> tbSalida { get; set; }
        public virtual DbSet<tbSalidaDetalle> tbSalidaDetalle { get; set; }
        public virtual DbSet<tbTipoDevolucion> tbTipoDevolucion { get; set; }
        public virtual DbSet<tbTipoEntrada> tbTipoEntrada { get; set; }
        public virtual DbSet<tbTipoSalida> tbTipoSalida { get; set; }
        public virtual DbSet<tbAcumuladosISR> tbAcumuladosISR { get; set; }
        public virtual DbSet<tbAdelantoSueldo> tbAdelantoSueldo { get; set; }
        public virtual DbSet<tbAFP> tbAFP { get; set; }
        public virtual DbSet<tbAuxilioDeCesantias> tbAuxilioDeCesantias { get; set; }
        public virtual DbSet<tbCatalogoDeDeducciones> tbCatalogoDeDeducciones { get; set; }
        public virtual DbSet<tbCatalogoDeIngresos> tbCatalogoDeIngresos { get; set; }
        public virtual DbSet<tbCatalogoDePlanillas> tbCatalogoDePlanillas { get; set; }
        public virtual DbSet<tbDecimoCuartoMes> tbDecimoCuartoMes { get; set; }
        public virtual DbSet<tbDecimoTercerMes> tbDecimoTercerMes { get; set; }
        public virtual DbSet<tbDeduccionAFP> tbDeduccionAFP { get; set; }
        public virtual DbSet<tbDeduccionesExtraordinarias> tbDeduccionesExtraordinarias { get; set; }
        public virtual DbSet<tbDeduccionesIndividuales> tbDeduccionesIndividuales { get; set; }
        public virtual DbSet<tbDeduccionImpuestoVecinal> tbDeduccionImpuestoVecinal { get; set; }
        public virtual DbSet<tbDeduccionInstitucionFinanciera> tbDeduccionInstitucionFinanciera { get; set; }
        public virtual DbSet<tbEmpleadoBonos> tbEmpleadoBonos { get; set; }
        public virtual DbSet<tbEmpleadoComisiones> tbEmpleadoComisiones { get; set; }
        public virtual DbSet<tbFormaPago> tbFormaPago { get; set; }
        public virtual DbSet<tbHistorialDeduccionPago> tbHistorialDeduccionPago { get; set; }
        public virtual DbSet<tbHistorialDeIngresosPago> tbHistorialDeIngresosPago { get; set; }
        public virtual DbSet<tbHistorialDePago> tbHistorialDePago { get; set; }
        public virtual DbSet<tbIngresosIndividuales> tbIngresosIndividuales { get; set; }
        public virtual DbSet<tbInstitucionesFinancieras> tbInstitucionesFinancieras { get; set; }
        public virtual DbSet<tbISR> tbISR { get; set; }
        public virtual DbSet<tbLiquidacionVacaciones> tbLiquidacionVacaciones { get; set; }
        public virtual DbSet<tbMotivoLiquidacion> tbMotivoLiquidacion { get; set; }
        public virtual DbSet<tbPagoDeCesantiaDetalle> tbPagoDeCesantiaDetalle { get; set; }
        public virtual DbSet<tbPagoDeCesantiaEncabezado> tbPagoDeCesantiaEncabezado { get; set; }
        public virtual DbSet<tbPeriodos> tbPeriodos { get; set; }
        public virtual DbSet<tbPorcentajeMotivoLiquidacion> tbPorcentajeMotivoLiquidacion { get; set; }
        public virtual DbSet<tbPreaviso> tbPreaviso { get; set; }
        public virtual DbSet<tbTechoImpuestoVecinal> tbTechoImpuestoVecinal { get; set; }
        public virtual DbSet<tbTechosComisiones> tbTechosComisiones { get; set; }
        public virtual DbSet<tbTechosDeducciones> tbTechosDeducciones { get; set; }
        public virtual DbSet<tbTipoDeduccion> tbTipoDeduccion { get; set; }
        public virtual DbSet<tbTipoPlanillaDetalleDeduccion> tbTipoPlanillaDetalleDeduccion { get; set; }
        public virtual DbSet<tbTipoPlanillaDetalleIngreso> tbTipoPlanillaDetalleIngreso { get; set; }
        public virtual DbSet<tbAreas> tbAreas { get; set; }
        public virtual DbSet<tbCargos> tbCargos { get; set; }
        public virtual DbSet<tbCompetencias> tbCompetencias { get; set; }
        public virtual DbSet<tbCompetenciasPersona> tbCompetenciasPersona { get; set; }
        public virtual DbSet<tbCompetenciasRequisicion> tbCompetenciasRequisicion { get; set; }
        public virtual DbSet<tbDepartamentos> tbDepartamentos { get; set; }
        public virtual DbSet<tbDirectoriosEmpleados> tbDirectoriosEmpleados { get; set; }
        public virtual DbSet<tbEmpleados> tbEmpleados { get; set; }
        public virtual DbSet<tbEmpresas> tbEmpresas { get; set; }
        public virtual DbSet<tbEquipoEmpleados> tbEquipoEmpleados { get; set; }
        public virtual DbSet<tbEquipoTrabajo> tbEquipoTrabajo { get; set; }
        public virtual DbSet<tbFaseSeleccion> tbFaseSeleccion { get; set; }
        public virtual DbSet<tbFasesReclutamiento> tbFasesReclutamiento { get; set; }
        public virtual DbSet<tbHabilidades> tbHabilidades { get; set; }
        public virtual DbSet<tbHabilidadesPersona> tbHabilidadesPersona { get; set; }
        public virtual DbSet<tbHabilidadesRequisicion> tbHabilidadesRequisicion { get; set; }
        public virtual DbSet<tbHistorialAmonestaciones> tbHistorialAmonestaciones { get; set; }
        public virtual DbSet<tbHistorialAudienciaDescargo> tbHistorialAudienciaDescargo { get; set; }
        public virtual DbSet<tbHistorialCargos> tbHistorialCargos { get; set; }
        public virtual DbSet<tbHistorialContrataciones> tbHistorialContrataciones { get; set; }
        public virtual DbSet<tbHistorialHorasTrabajadas> tbHistorialHorasTrabajadas { get; set; }
        public virtual DbSet<tbHistorialIncapacidades> tbHistorialIncapacidades { get; set; }
        public virtual DbSet<tbHistorialPermisos> tbHistorialPermisos { get; set; }
        public virtual DbSet<tbHistorialRefrendamientos> tbHistorialRefrendamientos { get; set; }
        public virtual DbSet<tbHistorialSalidas> tbHistorialSalidas { get; set; }
        public virtual DbSet<tbHistorialVacaciones> tbHistorialVacaciones { get; set; }
        public virtual DbSet<tbHorarios> tbHorarios { get; set; }
        public virtual DbSet<tbIdiomaPersona> tbIdiomaPersona { get; set; }
        public virtual DbSet<tbIdiomas> tbIdiomas { get; set; }
        public virtual DbSet<tbIdiomasRequisicion> tbIdiomasRequisicion { get; set; }
        public virtual DbSet<tbJornadas> tbJornadas { get; set; }
        public virtual DbSet<tbNacionalidades> tbNacionalidades { get; set; }
        public virtual DbSet<tbPersonas> tbPersonas { get; set; }
        public virtual DbSet<tbRazonSalidas> tbRazonSalidas { get; set; }
        public virtual DbSet<tbRequerimientosEspeciales> tbRequerimientosEspeciales { get; set; }
        public virtual DbSet<tbRequerimientosEspecialesPersona> tbRequerimientosEspecialesPersona { get; set; }
        public virtual DbSet<tbRequerimientosEspecialesRequisicion> tbRequerimientosEspecialesRequisicion { get; set; }
        public virtual DbSet<tbRequisiciones> tbRequisiciones { get; set; }
        public virtual DbSet<tbSeleccionCandidatos> tbSeleccionCandidatos { get; set; }
        public virtual DbSet<tbSucursales> tbSucursales { get; set; }
        public virtual DbSet<tbSueldos> tbSueldos { get; set; }
        public virtual DbSet<tbTipoAmonestaciones> tbTipoAmonestaciones { get; set; }
        public virtual DbSet<tbTipoHoras> tbTipoHoras { get; set; }
        public virtual DbSet<tbTipoIncapacidades> tbTipoIncapacidades { get; set; }
        public virtual DbSet<tbTipoMonedas> tbTipoMonedas { get; set; }
        public virtual DbSet<tbTipoPermisos> tbTipoPermisos { get; set; }
        public virtual DbSet<tbTipoSalidas> tbTipoSalidas { get; set; }
        public virtual DbSet<tbTitulos> tbTitulos { get; set; }
        public virtual DbSet<tbTitulosPersona> tbTitulosPersona { get; set; }
        public virtual DbSet<tbTitulosRequisicion> tbTitulosRequisicion { get; set; }
        public virtual DbSet<DatosConsumidorFinal> DatosConsumidorFinal { get; set; }
        public virtual DbSet<tbCaja> tbCaja { get; set; }
        public virtual DbSet<tbCliente> tbCliente { get; set; }
        public virtual DbSet<tbCliente_copy1> tbCliente_copy1 { get; set; }
        public virtual DbSet<tbCuponDescuento> tbCuponDescuento { get; set; }
        public virtual DbSet<tbDenominacionArqueo> tbDenominacionArqueo { get; set; }
        public virtual DbSet<tbDevolucion> tbDevolucion { get; set; }
        public virtual DbSet<tbDevolucionDetalle> tbDevolucionDetalle { get; set; }
        public virtual DbSet<tbDocumentoFiscal> tbDocumentoFiscal { get; set; }
        public virtual DbSet<tbEstadoFactura> tbEstadoFactura { get; set; }
        public virtual DbSet<tbEstadoPedido> tbEstadoPedido { get; set; }
        public virtual DbSet<tbEstadoSolicitudCredito> tbEstadoSolicitudCredito { get; set; }
        public virtual DbSet<tbExoneracion> tbExoneracion { get; set; }
        public virtual DbSet<tbFactura> tbFactura { get; set; }
        public virtual DbSet<tbFacturaDetalle> tbFacturaDetalle { get; set; }
        public virtual DbSet<tbFacturaHistorica> tbFacturaHistorica { get; set; }
        public virtual DbSet<tbListadoPrecioDetalle> tbListadoPrecioDetalle { get; set; }
        public virtual DbSet<tbListaPrecio> tbListaPrecio { get; set; }
        public virtual DbSet<tbMovimientoCaja> tbMovimientoCaja { get; set; }
        public virtual DbSet<tbNotaCredito> tbNotaCredito { get; set; }
        public virtual DbSet<tbPago> tbPago { get; set; }
        public virtual DbSet<tbPagosArqueo> tbPagosArqueo { get; set; }
        public virtual DbSet<tbPedido> tbPedido { get; set; }
        public virtual DbSet<tbPedidoDetalle> tbPedidoDetalle { get; set; }
        public virtual DbSet<tbPuntoEmision> tbPuntoEmision { get; set; }
        public virtual DbSet<tbPuntoEmisionDetalle> tbPuntoEmisionDetalle { get; set; }
        public virtual DbSet<tbSolicitudCredito> tbSolicitudCredito { get; set; }
        public virtual DbSet<tbSolicitudEfectivo> tbSolicitudEfectivo { get; set; }
        public virtual DbSet<tbSolicitudEfectivoDetalle> tbSolicitudEfectivoDetalle { get; set; }
        public virtual DbSet<tbTipoPago> tbTipoPago { get; set; }
        public virtual DbSet<UDV_Acce_Usuario_Roles> UDV_Acce_Usuario_Roles { get; set; }
        public virtual DbSet<V_Objetos> V_Objetos { get; set; }
        public virtual DbSet<UDV_Inv_Nombre_Empleado> UDV_Inv_Nombre_Empleado { get; set; }
        public virtual DbSet<UDP_ListadoDeProveedores> UDP_ListadoDeProveedores { get; set; }
        public virtual DbSet<UDV_Inv_CatalogoProductos> UDV_Inv_CatalogoProductos { get; set; }
        public virtual DbSet<UDV_Inv_Consultar_Existencias_Productos> UDV_Inv_Consultar_Existencias_Productos { get; set; }
        public virtual DbSet<UDV_Inv_ConsultarInventarioFisicoSobrantes> UDV_Inv_ConsultarInventarioFisicoSobrantes { get; set; }
        public virtual DbSet<UDV_Inv_EntradaPorId> UDV_Inv_EntradaPorId { get; set; }
        public virtual DbSet<UDV_INV_Frecuencia_Movimiento_Productos> UDV_INV_Frecuencia_Movimiento_Productos { get; set; }
        public virtual DbSet<UDV_INV_Frecuencia_Movimiento_Productos_Prueba> UDV_INV_Frecuencia_Movimiento_Productos_Prueba { get; set; }
        public virtual DbSet<UDV_Inv_Inventario_Historico_Por_Fechas> UDV_Inv_Inventario_Historico_Por_Fechas { get; set; }
        public virtual DbSet<UDV_Inv_Movimiento_Entre_Fechas> UDV_Inv_Movimiento_Entre_Fechas { get; set; }
        public virtual DbSet<UDV_Inv_PackingList> UDV_Inv_PackingList { get; set; }
        public virtual DbSet<UDV_Inv_PackingList_Caja> UDV_Inv_PackingList_Caja { get; set; }
        public virtual DbSet<UDV_Inv_ProductosSolicitadosPorEntregar> UDV_Inv_ProductosSolicitadosPorEntregar { get; set; }
        public virtual DbSet<UDV_Inv_Proyeccion_de_Movimiento_de_Producto> UDV_Inv_Proyeccion_de_Movimiento_de_Producto { get; set; }
        public virtual DbSet<UDV_Inv_Salida_Imprimir_Reporte> UDV_Inv_Salida_Imprimir_Reporte { get; set; }
        public virtual DbSet<UDV_inv_tbinventariofisico_reporteFaltantes> UDV_inv_tbinventariofisico_reporteFaltantes { get; set; }
        public virtual DbSet<UDV_Inv_tbProducto> UDV_Inv_tbProducto { get; set; }
        public virtual DbSet<UDV_Inv_TipoEntrada> UDV_Inv_TipoEntrada { get; set; }
        public virtual DbSet<UDV_InventarioActual> UDV_InventarioActual { get; set; }
        public virtual DbSet<UDV_TBInventarioFisico_ImprimirConciliacion> UDV_TBInventarioFisico_ImprimirConciliacion { get; set; }
        public virtual DbSet<VistaInventarioHistorico> VistaInventarioHistorico { get; set; }
        public virtual DbSet<EmpleadosVendedores> EmpleadosVendedores { get; set; }
        public virtual DbSet<V_AFP_RPT> V_AFP_RPT { get; set; }
        public virtual DbSet<V_BonosColaborador> V_BonosColaborador { get; set; }
        public virtual DbSet<V_CatalogoDeIngresos> V_CatalogoDeIngresos { get; set; }
        public virtual DbSet<V_CatalogoDePlanillasConIngresosYDeducciones> V_CatalogoDePlanillasConIngresosYDeducciones { get; set; }
        public virtual DbSet<V_ColaboradoresPorPlanilla> V_ColaboradoresPorPlanilla { get; set; }
        public virtual DbSet<V_DecimoCuartoMes> V_DecimoCuartoMes { get; set; }
        public virtual DbSet<V_DecimoCuartoMes_Pagados> V_DecimoCuartoMes_Pagados { get; set; }
        public virtual DbSet<V_DecimoCuartoMes_RPT> V_DecimoCuartoMes_RPT { get; set; }
        public virtual DbSet<V_DecimoCuartoMesFE> V_DecimoCuartoMesFE { get; set; }
        public virtual DbSet<V_DecimoTercerMes> V_DecimoTercerMes { get; set; }
        public virtual DbSet<V_DecimoTercerMes_Pagados> V_DecimoTercerMes_Pagados { get; set; }
        public virtual DbSet<V_DecimoTercerMes_RPT> V_DecimoTercerMes_RPT { get; set; }
        public virtual DbSet<V_DecimoTercerMesFE> V_DecimoTercerMesFE { get; set; }
        public virtual DbSet<V_Deducciones_RPT> V_Deducciones_RPT { get; set; }
        public virtual DbSet<V_DeduccionesExtraordinarias> V_DeduccionesExtraordinarias { get; set; }
        public virtual DbSet<V_DeduccionesExtraordinarias_Detalles> V_DeduccionesExtraordinarias_Detalles { get; set; }
        public virtual DbSet<V_DeduccionesExtraordinarias_Empleados> V_DeduccionesExtraordinarias_Empleados { get; set; }
        public virtual DbSet<V_DeduccionesExtraordinarias_EquipoEmpleado> V_DeduccionesExtraordinarias_EquipoEmpleado { get; set; }
        public virtual DbSet<V_DeduccionesExtrasColaboradores> V_DeduccionesExtrasColaboradores { get; set; }
        public virtual DbSet<V_DeduccionesInstitucionesFinancierasColaboradres> V_DeduccionesInstitucionesFinancierasColaboradres { get; set; }
        public virtual DbSet<V_EmpleadoBonos> V_EmpleadoBonos { get; set; }
        public virtual DbSet<V_FormaDePago> V_FormaDePago { get; set; }
        public virtual DbSet<V_HistorialEmpleadosLiquidados> V_HistorialEmpleadosLiquidados { get; set; }
        public virtual DbSet<V_IHSS_RPT> V_IHSS_RPT { get; set; }
        public virtual DbSet<V_INFOP_RPT> V_INFOP_RPT { get; set; }
        public virtual DbSet<V_InformacionColaborador> V_InformacionColaborador { get; set; }
        public virtual DbSet<V_Ingresos_RPT> V_Ingresos_RPT { get; set; }
        public virtual DbSet<V_InstitucionesFinancieras_RPT> V_InstitucionesFinancieras_RPT { get; set; }
        public virtual DbSet<V_ISR_RPT> V_ISR_RPT { get; set; }
        public virtual DbSet<V_Plani_AnioPlanilla> V_Plani_AnioPlanilla { get; set; }
        public virtual DbSet<V_Plani_DecimoCuarto> V_Plani_DecimoCuarto { get; set; }
        public virtual DbSet<V_Plani_DecimoTercer> V_Plani_DecimoTercer { get; set; }
        public virtual DbSet<V_Plani_DesplegableHistorialPlanilla> V_Plani_DesplegableHistorialPlanilla { get; set; }
        public virtual DbSet<V_Plani_EncabezadoHistorialPlanilla> V_Plani_EncabezadoHistorialPlanilla { get; set; }
        public virtual DbSet<V_Plani_HistorialDeducciones> V_Plani_HistorialDeducciones { get; set; }
        public virtual DbSet<V_Plani_HistorialIngreso> V_Plani_HistorialIngreso { get; set; }
        public virtual DbSet<V_Plani_HistorialPlanilla> V_Plani_HistorialPlanilla { get; set; }
        public virtual DbSet<V_Plani_Prueba> V_Plani_Prueba { get; set; }
        public virtual DbSet<V_Plani_TipoPlani> V_Plani_TipoPlani { get; set; }
        public virtual DbSet<V_PlanillaDeducciones> V_PlanillaDeducciones { get; set; }
        public virtual DbSet<V_PlanillaIngresos> V_PlanillaIngresos { get; set; }
        public virtual DbSet<V_PreviewPlanilla> V_PreviewPlanilla { get; set; }
        public virtual DbSet<V_RAP_RPT> V_RAP_RPT { get; set; }
        public virtual DbSet<V_ReporteInstitucionesFinancieras_RPT> V_ReporteInstitucionesFinancieras_RPT { get; set; }
        public virtual DbSet<V_ReportesVarios> V_ReportesVarios { get; set; }
        public virtual DbSet<V_tbAdelantoSueldo> V_tbAdelantoSueldo { get; set; }
        public virtual DbSet<V_tbCatalogoDeDeducciones> V_tbCatalogoDeDeducciones { get; set; }
        public virtual DbSet<V_tbCatalogoDeIngresos> V_tbCatalogoDeIngresos { get; set; }
        public virtual DbSet<V_tbEmpleadoComisiones> V_tbEmpleadoComisiones { get; set; }
        public virtual DbSet<V_tbPagoDeCesantiaDetalle> V_tbPagoDeCesantiaDetalle { get; set; }
        public virtual DbSet<V_tbPagoDeCesantiaDetalle_Preview> V_tbPagoDeCesantiaDetalle_Preview { get; set; }
        public virtual DbSet<V_tbTechosComisiones> V_tbTechosComisiones { get; set; }
        public virtual DbSet<V_TechoImpuestoVecinal> V_TechoImpuestoVecinal { get; set; }
        public virtual DbSet<V_TipoDeduccion> V_TipoDeduccion { get; set; }
        public virtual DbSet<V_Datos_Empleado> V_Datos_Empleado { get; set; }
        public virtual DbSet<V_DatosProfesionales> V_DatosProfesionales { get; set; }
        public virtual DbSet<V_DatosProfesionalesP> V_DatosProfesionalesP { get; set; }
        public virtual DbSet<V_DatosRequisicion> V_DatosRequisicion { get; set; }
        public virtual DbSet<V_Departamentos> V_Departamentos { get; set; }
        public virtual DbSet<V_EmpleadoAmonestaciones> V_EmpleadoAmonestaciones { get; set; }
        public virtual DbSet<V_EmpleadoIncapacidades> V_EmpleadoIncapacidades { get; set; }
        public virtual DbSet<V_Empleados> V_Empleados { get; set; }
        public virtual DbSet<V_Empleados_Sueldos> V_Empleados_Sueldos { get; set; }
        public virtual DbSet<V_EquipoTrabajoDetalles> V_EquipoTrabajoDetalles { get; set; }
        public virtual DbSet<V_FaseSeleccion> V_FaseSeleccion { get; set; }
        public virtual DbSet<V_HistorialAmonestacion> V_HistorialAmonestacion { get; set; }
        public virtual DbSet<V_HistorialAudienciaDescargo> V_HistorialAudienciaDescargo { get; set; }
        public virtual DbSet<V_HistorialCargos> V_HistorialCargos { get; set; }
        public virtual DbSet<V_HistorialContrataciones> V_HistorialContrataciones { get; set; }
        public virtual DbSet<V_HistorialHorasTrabajadas> V_HistorialHorasTrabajadas { get; set; }
        public virtual DbSet<V_HistorialIncapacidades> V_HistorialIncapacidades { get; set; }
        public virtual DbSet<V_HistorialPermisos> V_HistorialPermisos { get; set; }
        public virtual DbSet<V_HistorialPermisos_Empleados> V_HistorialPermisos_Empleados { get; set; }
        public virtual DbSet<V_HistorialPermisosEmpleados> V_HistorialPermisosEmpleados { get; set; }
        public virtual DbSet<V_HistorialSalidas_Empleados> V_HistorialSalidas_Empleados { get; set; }
        public virtual DbSet<V_Historialvacaciones> V_Historialvacaciones { get; set; }
        public virtual DbSet<V_HorariosDetalles> V_HorariosDetalles { get; set; }
        public virtual DbSet<V_HVacacionesEmpleados> V_HVacacionesEmpleados { get; set; }
        public virtual DbSet<V_RPT_EmpleadoCurriculum> V_RPT_EmpleadoCurriculum { get; set; }
        public virtual DbSet<V_RPT_EmpleadoCurriculum_Personas> V_RPT_EmpleadoCurriculum_Personas { get; set; }
        public virtual DbSet<V_RPT_EquipoEmpleado> V_RPT_EquipoEmpleado { get; set; }
        public virtual DbSet<V_RPT_FaseSeleccion> V_RPT_FaseSeleccion { get; set; }
        public virtual DbSet<V_RPT_HistorialAmonestaciones> V_RPT_HistorialAmonestaciones { get; set; }
        public virtual DbSet<V_RPT_HistorialAmonestaciones_Empleados> V_RPT_HistorialAmonestaciones_Empleados { get; set; }
        public virtual DbSet<V_RPT_HistorialAudienciaDescargo> V_RPT_HistorialAudienciaDescargo { get; set; }
        public virtual DbSet<V_RPT_HistorialAudienciaDescargo_empleados> V_RPT_HistorialAudienciaDescargo_empleados { get; set; }
        public virtual DbSet<V_RPT_HistorialCargos> V_RPT_HistorialCargos { get; set; }
        public virtual DbSet<V_RPT_HistorialContrataciones> V_RPT_HistorialContrataciones { get; set; }
        public virtual DbSet<V_RPT_HistorialIncapacidad> V_RPT_HistorialIncapacidad { get; set; }
        public virtual DbSet<V_RPT_HistorialPermisos> V_RPT_HistorialPermisos { get; set; }
        public virtual DbSet<V_RPT_HistorialSalidas> V_RPT_HistorialSalidas { get; set; }
        public virtual DbSet<V_RPT_HistorialSueldos> V_RPT_HistorialSueldos { get; set; }
        public virtual DbSet<V_RPT_HistorialVacaciones> V_RPT_HistorialVacaciones { get; set; }
        public virtual DbSet<V_RPT_HorasTrabajadas> V_RPT_HorasTrabajadas { get; set; }
        public virtual DbSet<V_RPT_Requisiciones> V_RPT_Requisiciones { get; set; }
        public virtual DbSet<V_RPT_RequisicionesDatos> V_RPT_RequisicionesDatos { get; set; }
        public virtual DbSet<V_SeleccionCandidatos> V_SeleccionCandidatos { get; set; }
        public virtual DbSet<V_Sueldos> V_Sueldos { get; set; }
        public virtual DbSet<V_tbEmpleados> V_tbEmpleados { get; set; }
        public virtual DbSet<V_tbHistorialPermisos_completa> V_tbHistorialPermisos_completa { get; set; }
        public virtual DbSet<V_tbHistorialSalidas> V_tbHistorialSalidas { get; set; }
        public virtual DbSet<V_tbHistorialSalidas_completa> V_tbHistorialSalidas_completa { get; set; }
        public virtual DbSet<V_tbPersonas> V_tbPersonas { get; set; }
        public virtual DbSet<V_tbtiposalidas> V_tbtiposalidas { get; set; }
        public virtual DbSet<UDP_Vent_listExoneracion_Select> UDP_Vent_listExoneracion_Select { get; set; }
        public virtual DbSet<UDP_Vent_PagosPorFechas> UDP_Vent_PagosPorFechas { get; set; }
        public virtual DbSet<UDP_Vent_SolicituEfectivo_Select> UDP_Vent_SolicituEfectivo_Select { get; set; }
        public virtual DbSet<UDV_Vent_AnalisisDeMora> UDV_Vent_AnalisisDeMora { get; set; }
        public virtual DbSet<UDV_Vent_ArqueoCaja_Select> UDV_Vent_ArqueoCaja_Select { get; set; }
        public virtual DbSet<UDV_Vent_Busqueda_Clientes> UDV_Vent_Busqueda_Clientes { get; set; }
        public virtual DbSet<UDV_Vent_Busqueda_Factura> UDV_Vent_Busqueda_Factura { get; set; }
        public virtual DbSet<UDV_Vent_CuponDescuentoPorFecha> UDV_Vent_CuponDescuentoPorFecha { get; set; }
        public virtual DbSet<UDV_Vent_DescuentosporFecha> UDV_Vent_DescuentosporFecha { get; set; }
        public virtual DbSet<UDV_Vent_DevolucionesPorFecha> UDV_Vent_DevolucionesPorFecha { get; set; }
        public virtual DbSet<UDV_Vent_EstadoCuenta> UDV_Vent_EstadoCuenta { get; set; }
        public virtual DbSet<UDV_Vent_Factura_VentasporFecha> UDV_Vent_Factura_VentasporFecha { get; set; }
        public virtual DbSet<UDV_Vent_FacturaPagoSelect> UDV_Vent_FacturaPagoSelect { get; set; }
        public virtual DbSet<UDV_Vent_FacturaPuntoEmision> UDV_Vent_FacturaPuntoEmision { get; set; }
        public virtual DbSet<UDV_Vent_FacturasPendientesDePago> UDV_Vent_FacturasPendientesDePago { get; set; }
        public virtual DbSet<UDV_Vent_InventarioNumeraciones> UDV_Vent_InventarioNumeraciones { get; set; }
        public virtual DbSet<UDV_Vent_ListadodePrecios> UDV_Vent_ListadodePrecios { get; set; }
        public virtual DbSet<UDV_Vent_MovimientoCaja_Apertura> UDV_Vent_MovimientoCaja_Apertura { get; set; }
        public virtual DbSet<UDV_Vent_NotaCreditoPorFecha> UDV_Vent_NotaCreditoPorFecha { get; set; }
        public virtual DbSet<UDV_Vent_PagosPorFechas> UDV_Vent_PagosPorFechas { get; set; }
        public virtual DbSet<UDV_Vent_Pedido_EntreFechas> UDV_Vent_Pedido_EntreFechas { get; set; }
        public virtual DbSet<UDV_Vent_prueba> UDV_Vent_prueba { get; set; }
        public virtual DbSet<UDV_Vent_SolicituEfectivo_Index> UDV_Vent_SolicituEfectivo_Index { get; set; }
        public virtual DbSet<UDV_Vent_tbSolicituEfectivo_EntreFechas> UDV_Vent_tbSolicituEfectivo_EntreFechas { get; set; }
        public virtual DbSet<UDV_Vent_VentasConsumidorFinal> UDV_Vent_VentasConsumidorFinal { get; set; }
        public virtual DbSet<UDV_Vent_VentasExoneradas> UDV_Vent_VentasExoneradas { get; set; }
        public virtual DbSet<UDV_Vent_VentasPorCaja_EntreFechas> UDV_Vent_VentasPorCaja_EntreFechas { get; set; }
        public virtual DbSet<UDV_Vent_SolicitudCredito_SolicitudesPorAprobarReporte> UDV_Vent_SolicitudCredito_SolicitudesPorAprobarReporte { get; set; }
    
        public virtual ObjectResult<SDP_Acce_GetObjetos_Result> SDP_Acce_GetObjetos()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Acce_GetObjetos_Result>("SDP_Acce_GetObjetos");
        }
    
        public virtual ObjectResult<SDP_Acce_GetObjetosAsignados_Result> SDP_Acce_GetObjetosAsignados(Nullable<int> rol_Id)
        {
            var rol_IdParameter = rol_Id.HasValue ?
                new ObjectParameter("rol_Id", rol_Id) :
                new ObjectParameter("rol_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Acce_GetObjetosAsignados_Result>("SDP_Acce_GetObjetosAsignados", rol_IdParameter);
        }
    
        public virtual ObjectResult<SDP_Acce_GetObjetosDisponibles_Result> SDP_Acce_GetObjetosDisponibles(Nullable<int> rol_Id)
        {
            var rol_IdParameter = rol_Id.HasValue ?
                new ObjectParameter("rol_Id", rol_Id) :
                new ObjectParameter("rol_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Acce_GetObjetosDisponibles_Result>("SDP_Acce_GetObjetosDisponibles", rol_IdParameter);
        }
    
        public virtual ObjectResult<SDP_Acce_GetReportes_Result> SDP_Acce_GetReportes()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Acce_GetReportes_Result>("SDP_Acce_GetReportes");
        }
    
        public virtual ObjectResult<SDP_Acce_GetRoles_Result> SDP_Acce_GetRoles()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Acce_GetRoles_Result>("SDP_Acce_GetRoles");
        }
    
        public virtual ObjectResult<SDP_Acce_GetRolesAsignados_Result> SDP_Acce_GetRolesAsignados(Nullable<int> usu_Id)
        {
            var usu_IdParameter = usu_Id.HasValue ?
                new ObjectParameter("usu_Id", usu_Id) :
                new ObjectParameter("usu_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Acce_GetRolesAsignados_Result>("SDP_Acce_GetRolesAsignados", usu_IdParameter);
        }
    
        public virtual ObjectResult<SDP_Acce_GetRolesDisponibles_Result> SDP_Acce_GetRolesDisponibles(Nullable<int> usu_Id)
        {
            var usu_IdParameter = usu_Id.HasValue ?
                new ObjectParameter("usu_Id", usu_Id) :
                new ObjectParameter("usu_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Acce_GetRolesDisponibles_Result>("SDP_Acce_GetRolesDisponibles", usu_IdParameter);
        }
    
        public virtual ObjectResult<SDP_Acce_GetUserRols_Result> SDP_Acce_GetUserRols(Nullable<int> usu_Id, string obj_Pantalla)
        {
            var usu_IdParameter = usu_Id.HasValue ?
                new ObjectParameter("usu_Id", usu_Id) :
                new ObjectParameter("usu_Id", typeof(int));
    
            var obj_PantallaParameter = obj_Pantalla != null ?
                new ObjectParameter("obj_Pantalla", obj_Pantalla) :
                new ObjectParameter("obj_Pantalla", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Acce_GetUserRols_Result>("SDP_Acce_GetUserRols", usu_IdParameter, obj_PantallaParameter);
        }
    
        public virtual ObjectResult<string> UDP_Acce_EJEMPLO(string rol_Descripcion, Nullable<bool> rol_Estado, Nullable<int> rol_UsuarioCrea, Nullable<System.DateTime> rol_FechaCrea)
        {
            var rol_DescripcionParameter = rol_Descripcion != null ?
                new ObjectParameter("rol_Descripcion", rol_Descripcion) :
                new ObjectParameter("rol_Descripcion", typeof(string));
    
            var rol_EstadoParameter = rol_Estado.HasValue ?
                new ObjectParameter("rol_Estado", rol_Estado) :
                new ObjectParameter("rol_Estado", typeof(bool));
    
            var rol_UsuarioCreaParameter = rol_UsuarioCrea.HasValue ?
                new ObjectParameter("rol_UsuarioCrea", rol_UsuarioCrea) :
                new ObjectParameter("rol_UsuarioCrea", typeof(int));
    
            var rol_FechaCreaParameter = rol_FechaCrea.HasValue ?
                new ObjectParameter("rol_FechaCrea", rol_FechaCrea) :
                new ObjectParameter("rol_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("UDP_Acce_EJEMPLO", rol_DescripcionParameter, rol_EstadoParameter, rol_UsuarioCreaParameter, rol_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Acce_Login_Result> UDP_Acce_Login(string usuarioId, string password)
        {
            var usuarioIdParameter = usuarioId != null ?
                new ObjectParameter("UsuarioId", usuarioId) :
                new ObjectParameter("UsuarioId", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Acce_Login_Result>("UDP_Acce_Login", usuarioIdParameter, passwordParameter);
        }
    
        public virtual int UDP_Acce_tbAccesoRol_Delete(Nullable<int> rol_Id, Nullable<int> obj_Id)
        {
            var rol_IdParameter = rol_Id.HasValue ?
                new ObjectParameter("rol_Id", rol_Id) :
                new ObjectParameter("rol_Id", typeof(int));
    
            var obj_IdParameter = obj_Id.HasValue ?
                new ObjectParameter("obj_Id", obj_Id) :
                new ObjectParameter("obj_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UDP_Acce_tbAccesoRol_Delete", rol_IdParameter, obj_IdParameter);
        }
    
        public virtual ObjectResult<string> UDP_Acce_tbBitacoraErrores_Insert(string bite_Pantalla, string bite_Usuario, Nullable<System.DateTime> bite_Fecha, string bite_MensajeError, string bite_Accion)
        {
            var bite_PantallaParameter = bite_Pantalla != null ?
                new ObjectParameter("bite_Pantalla", bite_Pantalla) :
                new ObjectParameter("bite_Pantalla", typeof(string));
    
            var bite_UsuarioParameter = bite_Usuario != null ?
                new ObjectParameter("bite_Usuario", bite_Usuario) :
                new ObjectParameter("bite_Usuario", typeof(string));
    
            var bite_FechaParameter = bite_Fecha.HasValue ?
                new ObjectParameter("bite_Fecha", bite_Fecha) :
                new ObjectParameter("bite_Fecha", typeof(System.DateTime));
    
            var bite_MensajeErrorParameter = bite_MensajeError != null ?
                new ObjectParameter("bite_MensajeError", bite_MensajeError) :
                new ObjectParameter("bite_MensajeError", typeof(string));
    
            var bite_AccionParameter = bite_Accion != null ?
                new ObjectParameter("bite_Accion", bite_Accion) :
                new ObjectParameter("bite_Accion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("UDP_Acce_tbBitacoraErrores_Insert", bite_PantallaParameter, bite_UsuarioParameter, bite_FechaParameter, bite_MensajeErrorParameter, bite_AccionParameter);
        }
    
        public virtual int UDP_Acce_tbRolesUsuario_Delete(Nullable<int> usu_Id, Nullable<int> rol_Id)
        {
            var usu_IdParameter = usu_Id.HasValue ?
                new ObjectParameter("usu_Id", usu_Id) :
                new ObjectParameter("usu_Id", typeof(int));
    
            var rol_IdParameter = rol_Id.HasValue ?
                new ObjectParameter("rol_Id", rol_Id) :
                new ObjectParameter("rol_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UDP_Acce_tbRolesUsuario_Delete", usu_IdParameter, rol_IdParameter);
        }
    
        public virtual int UDP_Acce_tbRolesUsuario_Update(Nullable<int> rolu_Id, Nullable<int> rol_Id, Nullable<int> usu_Id, Nullable<int> rolu_UsuarioCrea, Nullable<System.DateTime> rolu_FechaCrea)
        {
            var rolu_IdParameter = rolu_Id.HasValue ?
                new ObjectParameter("rolu_Id", rolu_Id) :
                new ObjectParameter("rolu_Id", typeof(int));
    
            var rol_IdParameter = rol_Id.HasValue ?
                new ObjectParameter("rol_Id", rol_Id) :
                new ObjectParameter("rol_Id", typeof(int));
    
            var usu_IdParameter = usu_Id.HasValue ?
                new ObjectParameter("usu_Id", usu_Id) :
                new ObjectParameter("usu_Id", typeof(int));
    
            var rolu_UsuarioCreaParameter = rolu_UsuarioCrea.HasValue ?
                new ObjectParameter("rolu_UsuarioCrea", rolu_UsuarioCrea) :
                new ObjectParameter("rolu_UsuarioCrea", typeof(int));
    
            var rolu_FechaCreaParameter = rolu_FechaCrea.HasValue ?
                new ObjectParameter("rolu_FechaCrea", rolu_FechaCrea) :
                new ObjectParameter("rolu_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UDP_Acce_tbRolesUsuario_Update", rolu_IdParameter, rol_IdParameter, usu_IdParameter, rolu_UsuarioCreaParameter, rolu_FechaCreaParameter);
        }
    
        public virtual ObjectResult<string> UDP_Acce_tbUsuario_Estado(Nullable<int> usu_Id, Nullable<bool> usu_EsActivo, string usu_RazonInactivo)
        {
            var usu_IdParameter = usu_Id.HasValue ?
                new ObjectParameter("usu_Id", usu_Id) :
                new ObjectParameter("usu_Id", typeof(int));
    
            var usu_EsActivoParameter = usu_EsActivo.HasValue ?
                new ObjectParameter("usu_EsActivo", usu_EsActivo) :
                new ObjectParameter("usu_EsActivo", typeof(bool));
    
            var usu_RazonInactivoParameter = usu_RazonInactivo != null ?
                new ObjectParameter("usu_RazonInactivo", usu_RazonInactivo) :
                new ObjectParameter("usu_RazonInactivo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("UDP_Acce_tbUsuario_Estado", usu_IdParameter, usu_EsActivoParameter, usu_RazonInactivoParameter);
        }
    
        public virtual ObjectResult<string> UDP_Acce_tbUsuario_Select()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("UDP_Acce_tbUsuario_Select");
        }
    
        public virtual ObjectResult<SDP_Gral_tbEmpleado_Select_Result> SDP_Gral_tbEmpleado_Select(Nullable<short> emp_Id)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Gral_tbEmpleado_Select_Result>("SDP_Gral_tbEmpleado_Select", emp_IdParameter);
        }
    
        public virtual ObjectResult<SDP_tbEmpleado_Consulta_Result> SDP_tbEmpleado_Consulta(Nullable<int> emp_Id)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_tbEmpleado_Consulta_Result>("SDP_tbEmpleado_Consulta", emp_IdParameter);
        }
    
        public virtual ObjectResult<SDP_tbEmpleado_Select_Result> SDP_tbEmpleado_Select()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_tbEmpleado_Select_Result>("SDP_tbEmpleado_Select");
        }
    
        public virtual ObjectResult<SDP_tbMunicipio_Select_Result> SDP_tbMunicipio_Select(string mun_Codigo)
        {
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_tbMunicipio_Select_Result>("SDP_tbMunicipio_Select", mun_CodigoParameter);
        }
    
        public virtual ObjectResult<spGetDenominacionesMoneda_Result> spGetDenominacionesMoneda(Nullable<int> codMoneda)
        {
            var codMonedaParameter = codMoneda.HasValue ?
                new ObjectParameter("CodMoneda", codMoneda) :
                new ObjectParameter("CodMoneda", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetDenominacionesMoneda_Result>("spGetDenominacionesMoneda", codMonedaParameter);
        }
    
        public virtual int UDP_Gral_Padres_Insert(string nombresPadre, string apellidosPadre)
        {
            var nombresPadreParameter = nombresPadre != null ?
                new ObjectParameter("NombresPadre", nombresPadre) :
                new ObjectParameter("NombresPadre", typeof(string));
    
            var apellidosPadreParameter = apellidosPadre != null ?
                new ObjectParameter("ApellidosPadre", apellidosPadre) :
                new ObjectParameter("ApellidosPadre", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UDP_Gral_Padres_Insert", nombresPadreParameter, apellidosPadreParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbActividadEconomica_Insert_Result> UDP_Gral_tbActividadEconomica_Insert(string acte_Descripcion, Nullable<int> acte_UsuarioCrea, Nullable<System.DateTime> acte_FechaCrea)
        {
            var acte_DescripcionParameter = acte_Descripcion != null ?
                new ObjectParameter("acte_Descripcion", acte_Descripcion) :
                new ObjectParameter("acte_Descripcion", typeof(string));
    
            var acte_UsuarioCreaParameter = acte_UsuarioCrea.HasValue ?
                new ObjectParameter("acte_UsuarioCrea", acte_UsuarioCrea) :
                new ObjectParameter("acte_UsuarioCrea", typeof(int));
    
            var acte_FechaCreaParameter = acte_FechaCrea.HasValue ?
                new ObjectParameter("acte_FechaCrea", acte_FechaCrea) :
                new ObjectParameter("acte_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbActividadEconomica_Insert_Result>("UDP_Gral_tbActividadEconomica_Insert", acte_DescripcionParameter, acte_UsuarioCreaParameter, acte_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbActividadEconomica_Update_Result> UDP_Gral_tbActividadEconomica_Update(Nullable<short> acte_Id, string acte_Descripcion, Nullable<int> acte_UsuarioCrea, Nullable<System.DateTime> acte_FechaCrea, Nullable<int> acte_UsuarioModifica, Nullable<System.DateTime> acte_FechaModifica)
        {
            var acte_IdParameter = acte_Id.HasValue ?
                new ObjectParameter("acte_Id", acte_Id) :
                new ObjectParameter("acte_Id", typeof(short));
    
            var acte_DescripcionParameter = acte_Descripcion != null ?
                new ObjectParameter("acte_Descripcion", acte_Descripcion) :
                new ObjectParameter("acte_Descripcion", typeof(string));
    
            var acte_UsuarioCreaParameter = acte_UsuarioCrea.HasValue ?
                new ObjectParameter("acte_UsuarioCrea", acte_UsuarioCrea) :
                new ObjectParameter("acte_UsuarioCrea", typeof(int));
    
            var acte_FechaCreaParameter = acte_FechaCrea.HasValue ?
                new ObjectParameter("acte_FechaCrea", acte_FechaCrea) :
                new ObjectParameter("acte_FechaCrea", typeof(System.DateTime));
    
            var acte_UsuarioModificaParameter = acte_UsuarioModifica.HasValue ?
                new ObjectParameter("acte_UsuarioModifica", acte_UsuarioModifica) :
                new ObjectParameter("acte_UsuarioModifica", typeof(int));
    
            var acte_FechaModificaParameter = acte_FechaModifica.HasValue ?
                new ObjectParameter("acte_FechaModifica", acte_FechaModifica) :
                new ObjectParameter("acte_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbActividadEconomica_Update_Result>("UDP_Gral_tbActividadEconomica_Update", acte_IdParameter, acte_DescripcionParameter, acte_UsuarioCreaParameter, acte_FechaCreaParameter, acte_UsuarioModificaParameter, acte_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbBanco_Insert_Result> UDP_Gral_tbBanco_Insert(string ban_Nombre, string ban_NombreContacto, string ban_TelefonoContacto, Nullable<int> ban_UsuarioCrea, Nullable<System.DateTime> ban_FechaCrea)
        {
            var ban_NombreParameter = ban_Nombre != null ?
                new ObjectParameter("ban_Nombre", ban_Nombre) :
                new ObjectParameter("ban_Nombre", typeof(string));
    
            var ban_NombreContactoParameter = ban_NombreContacto != null ?
                new ObjectParameter("ban_NombreContacto", ban_NombreContacto) :
                new ObjectParameter("ban_NombreContacto", typeof(string));
    
            var ban_TelefonoContactoParameter = ban_TelefonoContacto != null ?
                new ObjectParameter("ban_TelefonoContacto", ban_TelefonoContacto) :
                new ObjectParameter("ban_TelefonoContacto", typeof(string));
    
            var ban_UsuarioCreaParameter = ban_UsuarioCrea.HasValue ?
                new ObjectParameter("ban_UsuarioCrea", ban_UsuarioCrea) :
                new ObjectParameter("ban_UsuarioCrea", typeof(int));
    
            var ban_FechaCreaParameter = ban_FechaCrea.HasValue ?
                new ObjectParameter("ban_FechaCrea", ban_FechaCrea) :
                new ObjectParameter("ban_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbBanco_Insert_Result>("UDP_Gral_tbBanco_Insert", ban_NombreParameter, ban_NombreContactoParameter, ban_TelefonoContactoParameter, ban_UsuarioCreaParameter, ban_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbBanco_Update_Result> UDP_Gral_tbBanco_Update(Nullable<short> ban_Id, string ban_Nombre, string ban_NombreContacto, string ban_TelefonoContacto, Nullable<int> ban_UsuarioCrea, Nullable<System.DateTime> ban_FechaCrea, Nullable<int> ban_UsuarioModifica, Nullable<System.DateTime> ban_FechaModifica)
        {
            var ban_IdParameter = ban_Id.HasValue ?
                new ObjectParameter("ban_Id", ban_Id) :
                new ObjectParameter("ban_Id", typeof(short));
    
            var ban_NombreParameter = ban_Nombre != null ?
                new ObjectParameter("ban_Nombre", ban_Nombre) :
                new ObjectParameter("ban_Nombre", typeof(string));
    
            var ban_NombreContactoParameter = ban_NombreContacto != null ?
                new ObjectParameter("ban_NombreContacto", ban_NombreContacto) :
                new ObjectParameter("ban_NombreContacto", typeof(string));
    
            var ban_TelefonoContactoParameter = ban_TelefonoContacto != null ?
                new ObjectParameter("ban_TelefonoContacto", ban_TelefonoContacto) :
                new ObjectParameter("ban_TelefonoContacto", typeof(string));
    
            var ban_UsuarioCreaParameter = ban_UsuarioCrea.HasValue ?
                new ObjectParameter("ban_UsuarioCrea", ban_UsuarioCrea) :
                new ObjectParameter("ban_UsuarioCrea", typeof(int));
    
            var ban_FechaCreaParameter = ban_FechaCrea.HasValue ?
                new ObjectParameter("ban_FechaCrea", ban_FechaCrea) :
                new ObjectParameter("ban_FechaCrea", typeof(System.DateTime));
    
            var ban_UsuarioModificaParameter = ban_UsuarioModifica.HasValue ?
                new ObjectParameter("ban_UsuarioModifica", ban_UsuarioModifica) :
                new ObjectParameter("ban_UsuarioModifica", typeof(int));
    
            var ban_FechaModificaParameter = ban_FechaModifica.HasValue ?
                new ObjectParameter("ban_FechaModifica", ban_FechaModifica) :
                new ObjectParameter("ban_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbBanco_Update_Result>("UDP_Gral_tbBanco_Update", ban_IdParameter, ban_NombreParameter, ban_NombreContactoParameter, ban_TelefonoContactoParameter, ban_UsuarioCreaParameter, ban_FechaCreaParameter, ban_UsuarioModificaParameter, ban_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbCuentasBanco_Insert_Result> UDP_Gral_tbCuentasBanco_Insert(Nullable<short> ban_Id, Nullable<short> mnda_Id, Nullable<byte> bcta_TipoCuenta, Nullable<decimal> bcta_TotalCredito, Nullable<decimal> bcta_TotalDebito, Nullable<System.DateTime> bcta_FechaApertura, string bcta_Numero, Nullable<int> bcta_UsuarioCrea, Nullable<System.DateTime> bcta_FechaCrea)
        {
            var ban_IdParameter = ban_Id.HasValue ?
                new ObjectParameter("ban_Id", ban_Id) :
                new ObjectParameter("ban_Id", typeof(short));
    
            var mnda_IdParameter = mnda_Id.HasValue ?
                new ObjectParameter("mnda_Id", mnda_Id) :
                new ObjectParameter("mnda_Id", typeof(short));
    
            var bcta_TipoCuentaParameter = bcta_TipoCuenta.HasValue ?
                new ObjectParameter("bcta_TipoCuenta", bcta_TipoCuenta) :
                new ObjectParameter("bcta_TipoCuenta", typeof(byte));
    
            var bcta_TotalCreditoParameter = bcta_TotalCredito.HasValue ?
                new ObjectParameter("bcta_TotalCredito", bcta_TotalCredito) :
                new ObjectParameter("bcta_TotalCredito", typeof(decimal));
    
            var bcta_TotalDebitoParameter = bcta_TotalDebito.HasValue ?
                new ObjectParameter("bcta_TotalDebito", bcta_TotalDebito) :
                new ObjectParameter("bcta_TotalDebito", typeof(decimal));
    
            var bcta_FechaAperturaParameter = bcta_FechaApertura.HasValue ?
                new ObjectParameter("bcta_FechaApertura", bcta_FechaApertura) :
                new ObjectParameter("bcta_FechaApertura", typeof(System.DateTime));
    
            var bcta_NumeroParameter = bcta_Numero != null ?
                new ObjectParameter("bcta_Numero", bcta_Numero) :
                new ObjectParameter("bcta_Numero", typeof(string));
    
            var bcta_UsuarioCreaParameter = bcta_UsuarioCrea.HasValue ?
                new ObjectParameter("bcta_UsuarioCrea", bcta_UsuarioCrea) :
                new ObjectParameter("bcta_UsuarioCrea", typeof(int));
    
            var bcta_FechaCreaParameter = bcta_FechaCrea.HasValue ?
                new ObjectParameter("bcta_FechaCrea", bcta_FechaCrea) :
                new ObjectParameter("bcta_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbCuentasBanco_Insert_Result>("UDP_Gral_tbCuentasBanco_Insert", ban_IdParameter, mnda_IdParameter, bcta_TipoCuentaParameter, bcta_TotalCreditoParameter, bcta_TotalDebitoParameter, bcta_FechaAperturaParameter, bcta_NumeroParameter, bcta_UsuarioCreaParameter, bcta_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbCuentasBanco_Update_Result> UDP_Gral_tbCuentasBanco_Update(Nullable<short> bcta_Id, Nullable<short> ban_Id, Nullable<short> mnda_Id, Nullable<byte> bcta_TipoCuenta, Nullable<decimal> bcta_TotalCredito, Nullable<decimal> bcta_TotalDebito, Nullable<System.DateTime> bcta_FechaApertura, string bcta_Numero, Nullable<int> bcta_UsuarioCrea, Nullable<System.DateTime> bcta_FechaCrea, Nullable<int> bcta_UsuarioModifica, Nullable<System.DateTime> bcta_FechaModifica)
        {
            var bcta_IdParameter = bcta_Id.HasValue ?
                new ObjectParameter("bcta_Id", bcta_Id) :
                new ObjectParameter("bcta_Id", typeof(short));
    
            var ban_IdParameter = ban_Id.HasValue ?
                new ObjectParameter("ban_Id", ban_Id) :
                new ObjectParameter("ban_Id", typeof(short));
    
            var mnda_IdParameter = mnda_Id.HasValue ?
                new ObjectParameter("mnda_Id", mnda_Id) :
                new ObjectParameter("mnda_Id", typeof(short));
    
            var bcta_TipoCuentaParameter = bcta_TipoCuenta.HasValue ?
                new ObjectParameter("bcta_TipoCuenta", bcta_TipoCuenta) :
                new ObjectParameter("bcta_TipoCuenta", typeof(byte));
    
            var bcta_TotalCreditoParameter = bcta_TotalCredito.HasValue ?
                new ObjectParameter("bcta_TotalCredito", bcta_TotalCredito) :
                new ObjectParameter("bcta_TotalCredito", typeof(decimal));
    
            var bcta_TotalDebitoParameter = bcta_TotalDebito.HasValue ?
                new ObjectParameter("bcta_TotalDebito", bcta_TotalDebito) :
                new ObjectParameter("bcta_TotalDebito", typeof(decimal));
    
            var bcta_FechaAperturaParameter = bcta_FechaApertura.HasValue ?
                new ObjectParameter("bcta_FechaApertura", bcta_FechaApertura) :
                new ObjectParameter("bcta_FechaApertura", typeof(System.DateTime));
    
            var bcta_NumeroParameter = bcta_Numero != null ?
                new ObjectParameter("bcta_Numero", bcta_Numero) :
                new ObjectParameter("bcta_Numero", typeof(string));
    
            var bcta_UsuarioCreaParameter = bcta_UsuarioCrea.HasValue ?
                new ObjectParameter("bcta_UsuarioCrea", bcta_UsuarioCrea) :
                new ObjectParameter("bcta_UsuarioCrea", typeof(int));
    
            var bcta_FechaCreaParameter = bcta_FechaCrea.HasValue ?
                new ObjectParameter("bcta_FechaCrea", bcta_FechaCrea) :
                new ObjectParameter("bcta_FechaCrea", typeof(System.DateTime));
    
            var bcta_UsuarioModificaParameter = bcta_UsuarioModifica.HasValue ?
                new ObjectParameter("bcta_UsuarioModifica", bcta_UsuarioModifica) :
                new ObjectParameter("bcta_UsuarioModifica", typeof(int));
    
            var bcta_FechaModificaParameter = bcta_FechaModifica.HasValue ?
                new ObjectParameter("bcta_FechaModifica", bcta_FechaModifica) :
                new ObjectParameter("bcta_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbCuentasBanco_Update_Result>("UDP_Gral_tbCuentasBanco_Update", bcta_IdParameter, ban_IdParameter, mnda_IdParameter, bcta_TipoCuentaParameter, bcta_TotalCreditoParameter, bcta_TotalDebitoParameter, bcta_FechaAperturaParameter, bcta_NumeroParameter, bcta_UsuarioCreaParameter, bcta_FechaCreaParameter, bcta_UsuarioModificaParameter, bcta_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbDenominacion_Insert_Result> UDP_Gral_tbDenominacion_Insert(string deno_Descripcion, Nullable<byte> deno_Tipo, Nullable<decimal> deno_valor, Nullable<int> mnda_Id, Nullable<int> deno_UsuarioCrea, Nullable<System.DateTime> deno_FechaCrea)
        {
            var deno_DescripcionParameter = deno_Descripcion != null ?
                new ObjectParameter("deno_Descripcion", deno_Descripcion) :
                new ObjectParameter("deno_Descripcion", typeof(string));
    
            var deno_TipoParameter = deno_Tipo.HasValue ?
                new ObjectParameter("deno_Tipo", deno_Tipo) :
                new ObjectParameter("deno_Tipo", typeof(byte));
    
            var deno_valorParameter = deno_valor.HasValue ?
                new ObjectParameter("deno_valor", deno_valor) :
                new ObjectParameter("deno_valor", typeof(decimal));
    
            var mnda_IdParameter = mnda_Id.HasValue ?
                new ObjectParameter("mnda_Id", mnda_Id) :
                new ObjectParameter("mnda_Id", typeof(int));
    
            var deno_UsuarioCreaParameter = deno_UsuarioCrea.HasValue ?
                new ObjectParameter("deno_UsuarioCrea", deno_UsuarioCrea) :
                new ObjectParameter("deno_UsuarioCrea", typeof(int));
    
            var deno_FechaCreaParameter = deno_FechaCrea.HasValue ?
                new ObjectParameter("deno_FechaCrea", deno_FechaCrea) :
                new ObjectParameter("deno_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbDenominacion_Insert_Result>("UDP_Gral_tbDenominacion_Insert", deno_DescripcionParameter, deno_TipoParameter, deno_valorParameter, mnda_IdParameter, deno_UsuarioCreaParameter, deno_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbDenominacion_Select_Result1> UDP_Gral_tbDenominacion_Select()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbDenominacion_Select_Result1>("UDP_Gral_tbDenominacion_Select");
        }
    
        public virtual ObjectResult<UDP_Gral_tbDenominacion_Update_Result> UDP_Gral_tbDenominacion_Update(Nullable<short> deno_Id, string deno_Descripcion, Nullable<byte> deno_Tipo, Nullable<decimal> deno_valor, Nullable<short> mnda_Id, Nullable<int> deno_UsuarioCrea, Nullable<System.DateTime> deno_FechaCrea, Nullable<int> deno_UsuarioModifica, Nullable<System.DateTime> deno_FechaModifica)
        {
            var deno_IdParameter = deno_Id.HasValue ?
                new ObjectParameter("deno_Id", deno_Id) :
                new ObjectParameter("deno_Id", typeof(short));
    
            var deno_DescripcionParameter = deno_Descripcion != null ?
                new ObjectParameter("deno_Descripcion", deno_Descripcion) :
                new ObjectParameter("deno_Descripcion", typeof(string));
    
            var deno_TipoParameter = deno_Tipo.HasValue ?
                new ObjectParameter("deno_Tipo", deno_Tipo) :
                new ObjectParameter("deno_Tipo", typeof(byte));
    
            var deno_valorParameter = deno_valor.HasValue ?
                new ObjectParameter("deno_valor", deno_valor) :
                new ObjectParameter("deno_valor", typeof(decimal));
    
            var mnda_IdParameter = mnda_Id.HasValue ?
                new ObjectParameter("mnda_Id", mnda_Id) :
                new ObjectParameter("mnda_Id", typeof(short));
    
            var deno_UsuarioCreaParameter = deno_UsuarioCrea.HasValue ?
                new ObjectParameter("deno_UsuarioCrea", deno_UsuarioCrea) :
                new ObjectParameter("deno_UsuarioCrea", typeof(int));
    
            var deno_FechaCreaParameter = deno_FechaCrea.HasValue ?
                new ObjectParameter("deno_FechaCrea", deno_FechaCrea) :
                new ObjectParameter("deno_FechaCrea", typeof(System.DateTime));
    
            var deno_UsuarioModificaParameter = deno_UsuarioModifica.HasValue ?
                new ObjectParameter("deno_UsuarioModifica", deno_UsuarioModifica) :
                new ObjectParameter("deno_UsuarioModifica", typeof(int));
    
            var deno_FechaModificaParameter = deno_FechaModifica.HasValue ?
                new ObjectParameter("deno_FechaModifica", deno_FechaModifica) :
                new ObjectParameter("deno_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbDenominacion_Update_Result>("UDP_Gral_tbDenominacion_Update", deno_IdParameter, deno_DescripcionParameter, deno_TipoParameter, deno_valorParameter, mnda_IdParameter, deno_UsuarioCreaParameter, deno_FechaCreaParameter, deno_UsuarioModificaParameter, deno_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbDenominacion_using_Result> UDP_Gral_tbDenominacion_using(Nullable<int> dENO_ID)
        {
            var dENO_IDParameter = dENO_ID.HasValue ?
                new ObjectParameter("DENO_ID", dENO_ID) :
                new ObjectParameter("DENO_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbDenominacion_using_Result>("UDP_Gral_tbDenominacion_using", dENO_IDParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbDepartamento_Insert_Result> UDP_Gral_tbDepartamento_Insert(string dep_Codigo, string dep_Nombre, Nullable<int> dep_UsuarioCrea, Nullable<System.DateTime> dep_FechaCrea)
        {
            var dep_CodigoParameter = dep_Codigo != null ?
                new ObjectParameter("dep_Codigo", dep_Codigo) :
                new ObjectParameter("dep_Codigo", typeof(string));
    
            var dep_NombreParameter = dep_Nombre != null ?
                new ObjectParameter("dep_Nombre", dep_Nombre) :
                new ObjectParameter("dep_Nombre", typeof(string));
    
            var dep_UsuarioCreaParameter = dep_UsuarioCrea.HasValue ?
                new ObjectParameter("dep_UsuarioCrea", dep_UsuarioCrea) :
                new ObjectParameter("dep_UsuarioCrea", typeof(int));
    
            var dep_FechaCreaParameter = dep_FechaCrea.HasValue ?
                new ObjectParameter("dep_FechaCrea", dep_FechaCrea) :
                new ObjectParameter("dep_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbDepartamento_Insert_Result>("UDP_Gral_tbDepartamento_Insert", dep_CodigoParameter, dep_NombreParameter, dep_UsuarioCreaParameter, dep_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbDepartamento_Update_Result> UDP_Gral_tbDepartamento_Update(string dep_Codigo, string dep_Nombre, Nullable<int> dep_UsuarioCrea, Nullable<System.DateTime> dep_FechaCrea, Nullable<int> dep_UsuarioModifica, Nullable<System.DateTime> dep_FechaModifica)
        {
            var dep_CodigoParameter = dep_Codigo != null ?
                new ObjectParameter("dep_Codigo", dep_Codigo) :
                new ObjectParameter("dep_Codigo", typeof(string));
    
            var dep_NombreParameter = dep_Nombre != null ?
                new ObjectParameter("dep_Nombre", dep_Nombre) :
                new ObjectParameter("dep_Nombre", typeof(string));
    
            var dep_UsuarioCreaParameter = dep_UsuarioCrea.HasValue ?
                new ObjectParameter("dep_UsuarioCrea", dep_UsuarioCrea) :
                new ObjectParameter("dep_UsuarioCrea", typeof(int));
    
            var dep_FechaCreaParameter = dep_FechaCrea.HasValue ?
                new ObjectParameter("dep_FechaCrea", dep_FechaCrea) :
                new ObjectParameter("dep_FechaCrea", typeof(System.DateTime));
    
            var dep_UsuarioModificaParameter = dep_UsuarioModifica.HasValue ?
                new ObjectParameter("dep_UsuarioModifica", dep_UsuarioModifica) :
                new ObjectParameter("dep_UsuarioModifica", typeof(int));
    
            var dep_FechaModificaParameter = dep_FechaModifica.HasValue ?
                new ObjectParameter("dep_FechaModifica", dep_FechaModifica) :
                new ObjectParameter("dep_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbDepartamento_Update_Result>("UDP_Gral_tbDepartamento_Update", dep_CodigoParameter, dep_NombreParameter, dep_UsuarioCreaParameter, dep_FechaCreaParameter, dep_UsuarioModificaParameter, dep_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbEmpleado_Update_Estado_Result> UDP_Gral_tbEmpleado_Update_Estado(Nullable<int> emp_Id, Nullable<bool> emp_Estado, Nullable<int> emp_UsuarioModifica, Nullable<System.DateTime> emp_FechaModifica)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var emp_EstadoParameter = emp_Estado.HasValue ?
                new ObjectParameter("emp_Estado", emp_Estado) :
                new ObjectParameter("emp_Estado", typeof(bool));
    
            var emp_UsuarioModificaParameter = emp_UsuarioModifica.HasValue ?
                new ObjectParameter("emp_UsuarioModifica", emp_UsuarioModifica) :
                new ObjectParameter("emp_UsuarioModifica", typeof(int));
    
            var emp_FechaModificaParameter = emp_FechaModifica.HasValue ?
                new ObjectParameter("emp_FechaModifica", emp_FechaModifica) :
                new ObjectParameter("emp_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbEmpleado_Update_Estado_Result>("UDP_Gral_tbEmpleado_Update_Estado", emp_IdParameter, emp_EstadoParameter, emp_UsuarioModificaParameter, emp_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbEmpleado_Update_RazonInactivacion_Result> UDP_Gral_tbEmpleado_Update_RazonInactivacion(Nullable<int> emp_Id, Nullable<bool> emp_Estado, string emp_RazonInactivacion, Nullable<int> obj_UsuarioModifica, Nullable<System.DateTime> obj_FechaModifica)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var emp_EstadoParameter = emp_Estado.HasValue ?
                new ObjectParameter("emp_Estado", emp_Estado) :
                new ObjectParameter("emp_Estado", typeof(bool));
    
            var emp_RazonInactivacionParameter = emp_RazonInactivacion != null ?
                new ObjectParameter("emp_RazonInactivacion", emp_RazonInactivacion) :
                new ObjectParameter("emp_RazonInactivacion", typeof(string));
    
            var obj_UsuarioModificaParameter = obj_UsuarioModifica.HasValue ?
                new ObjectParameter("obj_UsuarioModifica", obj_UsuarioModifica) :
                new ObjectParameter("obj_UsuarioModifica", typeof(int));
    
            var obj_FechaModificaParameter = obj_FechaModifica.HasValue ?
                new ObjectParameter("obj_FechaModifica", obj_FechaModifica) :
                new ObjectParameter("obj_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbEmpleado_Update_RazonInactivacion_Result>("UDP_Gral_tbEmpleado_Update_RazonInactivacion", emp_IdParameter, emp_EstadoParameter, emp_RazonInactivacionParameter, obj_UsuarioModificaParameter, obj_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbEmpleado_Update_RazonSalida_Result> UDP_Gral_tbEmpleado_Update_RazonSalida(Nullable<int> emp_Id, Nullable<bool> emp_Estado, string emp_RazonSalida, Nullable<int> emp_UsuarioModifica, Nullable<System.DateTime> emp_FechaModifica)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var emp_EstadoParameter = emp_Estado.HasValue ?
                new ObjectParameter("emp_Estado", emp_Estado) :
                new ObjectParameter("emp_Estado", typeof(bool));
    
            var emp_RazonSalidaParameter = emp_RazonSalida != null ?
                new ObjectParameter("emp_RazonSalida", emp_RazonSalida) :
                new ObjectParameter("emp_RazonSalida", typeof(string));
    
            var emp_UsuarioModificaParameter = emp_UsuarioModifica.HasValue ?
                new ObjectParameter("emp_UsuarioModifica", emp_UsuarioModifica) :
                new ObjectParameter("emp_UsuarioModifica", typeof(int));
    
            var emp_FechaModificaParameter = emp_FechaModifica.HasValue ?
                new ObjectParameter("emp_FechaModifica", emp_FechaModifica) :
                new ObjectParameter("emp_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbEmpleado_Update_RazonSalida_Result>("UDP_Gral_tbEmpleado_Update_RazonSalida", emp_IdParameter, emp_EstadoParameter, emp_RazonSalidaParameter, emp_UsuarioModificaParameter, emp_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbEmpleados_Insert_Result> UDP_Gral_tbEmpleados_Insert(string emp_Nombres, string emp_Apellidos, string emp_Sexo, Nullable<System.DateTime> emp_FechaNacimiento, Nullable<byte> tpi_Id, string emp_Identificacion, string emp_Telefono, string emp_Correoelectronico, string emp_TipoSangre, string emp_Puesto, Nullable<System.DateTime> emp_FechaIngreso, string emp_Direccion, Nullable<int> emp_UsuarioCrea, Nullable<System.DateTime> emp_FechaCrea)
        {
            var emp_NombresParameter = emp_Nombres != null ?
                new ObjectParameter("emp_Nombres", emp_Nombres) :
                new ObjectParameter("emp_Nombres", typeof(string));
    
            var emp_ApellidosParameter = emp_Apellidos != null ?
                new ObjectParameter("emp_Apellidos", emp_Apellidos) :
                new ObjectParameter("emp_Apellidos", typeof(string));
    
            var emp_SexoParameter = emp_Sexo != null ?
                new ObjectParameter("emp_Sexo", emp_Sexo) :
                new ObjectParameter("emp_Sexo", typeof(string));
    
            var emp_FechaNacimientoParameter = emp_FechaNacimiento.HasValue ?
                new ObjectParameter("emp_FechaNacimiento", emp_FechaNacimiento) :
                new ObjectParameter("emp_FechaNacimiento", typeof(System.DateTime));
    
            var tpi_IdParameter = tpi_Id.HasValue ?
                new ObjectParameter("tpi_Id", tpi_Id) :
                new ObjectParameter("tpi_Id", typeof(byte));
    
            var emp_IdentificacionParameter = emp_Identificacion != null ?
                new ObjectParameter("emp_Identificacion", emp_Identificacion) :
                new ObjectParameter("emp_Identificacion", typeof(string));
    
            var emp_TelefonoParameter = emp_Telefono != null ?
                new ObjectParameter("emp_Telefono", emp_Telefono) :
                new ObjectParameter("emp_Telefono", typeof(string));
    
            var emp_CorreoelectronicoParameter = emp_Correoelectronico != null ?
                new ObjectParameter("emp_Correoelectronico", emp_Correoelectronico) :
                new ObjectParameter("emp_Correoelectronico", typeof(string));
    
            var emp_TipoSangreParameter = emp_TipoSangre != null ?
                new ObjectParameter("emp_TipoSangre", emp_TipoSangre) :
                new ObjectParameter("emp_TipoSangre", typeof(string));
    
            var emp_PuestoParameter = emp_Puesto != null ?
                new ObjectParameter("emp_Puesto", emp_Puesto) :
                new ObjectParameter("emp_Puesto", typeof(string));
    
            var emp_FechaIngresoParameter = emp_FechaIngreso.HasValue ?
                new ObjectParameter("emp_FechaIngreso", emp_FechaIngreso) :
                new ObjectParameter("emp_FechaIngreso", typeof(System.DateTime));
    
            var emp_DireccionParameter = emp_Direccion != null ?
                new ObjectParameter("emp_Direccion", emp_Direccion) :
                new ObjectParameter("emp_Direccion", typeof(string));
    
            var emp_UsuarioCreaParameter = emp_UsuarioCrea.HasValue ?
                new ObjectParameter("emp_UsuarioCrea", emp_UsuarioCrea) :
                new ObjectParameter("emp_UsuarioCrea", typeof(int));
    
            var emp_FechaCreaParameter = emp_FechaCrea.HasValue ?
                new ObjectParameter("emp_FechaCrea", emp_FechaCrea) :
                new ObjectParameter("emp_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbEmpleados_Insert_Result>("UDP_Gral_tbEmpleados_Insert", emp_NombresParameter, emp_ApellidosParameter, emp_SexoParameter, emp_FechaNacimientoParameter, tpi_IdParameter, emp_IdentificacionParameter, emp_TelefonoParameter, emp_CorreoelectronicoParameter, emp_TipoSangreParameter, emp_PuestoParameter, emp_FechaIngresoParameter, emp_DireccionParameter, emp_UsuarioCreaParameter, emp_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbEmpleados_Update_Result> UDP_Gral_tbEmpleados_Update(Nullable<short> emp_Id, string emp_Nombres, string emp_Apellidos, string emp_Sexo, Nullable<System.DateTime> emp_FechaNacimiento, Nullable<byte> tpi_Id, string emp_Identificacion, string emp_Telefono, string emp_Correoelectronico, string emp_TipoSangre, string emp_Puesto, Nullable<System.DateTime> emp_FechaIngreso, string emp_Direccion, string emp_RazonInactivacion, Nullable<int> emp_UsuarioCrea, Nullable<System.DateTime> emp_FechaCrea, string emp_RazonSalida, Nullable<System.DateTime> emp_fechaSalida, Nullable<int> emp_UsuarioModifica, Nullable<System.DateTime> emp_FechaModifica)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(short));
    
            var emp_NombresParameter = emp_Nombres != null ?
                new ObjectParameter("emp_Nombres", emp_Nombres) :
                new ObjectParameter("emp_Nombres", typeof(string));
    
            var emp_ApellidosParameter = emp_Apellidos != null ?
                new ObjectParameter("emp_Apellidos", emp_Apellidos) :
                new ObjectParameter("emp_Apellidos", typeof(string));
    
            var emp_SexoParameter = emp_Sexo != null ?
                new ObjectParameter("emp_Sexo", emp_Sexo) :
                new ObjectParameter("emp_Sexo", typeof(string));
    
            var emp_FechaNacimientoParameter = emp_FechaNacimiento.HasValue ?
                new ObjectParameter("emp_FechaNacimiento", emp_FechaNacimiento) :
                new ObjectParameter("emp_FechaNacimiento", typeof(System.DateTime));
    
            var tpi_IdParameter = tpi_Id.HasValue ?
                new ObjectParameter("tpi_Id", tpi_Id) :
                new ObjectParameter("tpi_Id", typeof(byte));
    
            var emp_IdentificacionParameter = emp_Identificacion != null ?
                new ObjectParameter("emp_Identificacion", emp_Identificacion) :
                new ObjectParameter("emp_Identificacion", typeof(string));
    
            var emp_TelefonoParameter = emp_Telefono != null ?
                new ObjectParameter("emp_Telefono", emp_Telefono) :
                new ObjectParameter("emp_Telefono", typeof(string));
    
            var emp_CorreoelectronicoParameter = emp_Correoelectronico != null ?
                new ObjectParameter("emp_Correoelectronico", emp_Correoelectronico) :
                new ObjectParameter("emp_Correoelectronico", typeof(string));
    
            var emp_TipoSangreParameter = emp_TipoSangre != null ?
                new ObjectParameter("emp_TipoSangre", emp_TipoSangre) :
                new ObjectParameter("emp_TipoSangre", typeof(string));
    
            var emp_PuestoParameter = emp_Puesto != null ?
                new ObjectParameter("emp_Puesto", emp_Puesto) :
                new ObjectParameter("emp_Puesto", typeof(string));
    
            var emp_FechaIngresoParameter = emp_FechaIngreso.HasValue ?
                new ObjectParameter("emp_FechaIngreso", emp_FechaIngreso) :
                new ObjectParameter("emp_FechaIngreso", typeof(System.DateTime));
    
            var emp_DireccionParameter = emp_Direccion != null ?
                new ObjectParameter("emp_Direccion", emp_Direccion) :
                new ObjectParameter("emp_Direccion", typeof(string));
    
            var emp_RazonInactivacionParameter = emp_RazonInactivacion != null ?
                new ObjectParameter("emp_RazonInactivacion", emp_RazonInactivacion) :
                new ObjectParameter("emp_RazonInactivacion", typeof(string));
    
            var emp_UsuarioCreaParameter = emp_UsuarioCrea.HasValue ?
                new ObjectParameter("emp_UsuarioCrea", emp_UsuarioCrea) :
                new ObjectParameter("emp_UsuarioCrea", typeof(int));
    
            var emp_FechaCreaParameter = emp_FechaCrea.HasValue ?
                new ObjectParameter("emp_FechaCrea", emp_FechaCrea) :
                new ObjectParameter("emp_FechaCrea", typeof(System.DateTime));
    
            var emp_RazonSalidaParameter = emp_RazonSalida != null ?
                new ObjectParameter("emp_RazonSalida", emp_RazonSalida) :
                new ObjectParameter("emp_RazonSalida", typeof(string));
    
            var emp_fechaSalidaParameter = emp_fechaSalida.HasValue ?
                new ObjectParameter("emp_fechaSalida", emp_fechaSalida) :
                new ObjectParameter("emp_fechaSalida", typeof(System.DateTime));
    
            var emp_UsuarioModificaParameter = emp_UsuarioModifica.HasValue ?
                new ObjectParameter("emp_UsuarioModifica", emp_UsuarioModifica) :
                new ObjectParameter("emp_UsuarioModifica", typeof(int));
    
            var emp_FechaModificaParameter = emp_FechaModifica.HasValue ?
                new ObjectParameter("emp_FechaModifica", emp_FechaModifica) :
                new ObjectParameter("emp_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbEmpleados_Update_Result>("UDP_Gral_tbEmpleados_Update", emp_IdParameter, emp_NombresParameter, emp_ApellidosParameter, emp_SexoParameter, emp_FechaNacimientoParameter, tpi_IdParameter, emp_IdentificacionParameter, emp_TelefonoParameter, emp_CorreoelectronicoParameter, emp_TipoSangreParameter, emp_PuestoParameter, emp_FechaIngresoParameter, emp_DireccionParameter, emp_RazonInactivacionParameter, emp_UsuarioCreaParameter, emp_FechaCreaParameter, emp_RazonSalidaParameter, emp_fechaSalidaParameter, emp_UsuarioModificaParameter, emp_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbMoneda_Insert_Result> UDP_Gral_tbMoneda_Insert(string mnda_Abreviatura, string mnda_Nombre, Nullable<int> mnda_UsuarioCrea, Nullable<System.DateTime> mnda_FechaCrea)
        {
            var mnda_AbreviaturaParameter = mnda_Abreviatura != null ?
                new ObjectParameter("mnda_Abreviatura", mnda_Abreviatura) :
                new ObjectParameter("mnda_Abreviatura", typeof(string));
    
            var mnda_NombreParameter = mnda_Nombre != null ?
                new ObjectParameter("mnda_Nombre", mnda_Nombre) :
                new ObjectParameter("mnda_Nombre", typeof(string));
    
            var mnda_UsuarioCreaParameter = mnda_UsuarioCrea.HasValue ?
                new ObjectParameter("mnda_UsuarioCrea", mnda_UsuarioCrea) :
                new ObjectParameter("mnda_UsuarioCrea", typeof(int));
    
            var mnda_FechaCreaParameter = mnda_FechaCrea.HasValue ?
                new ObjectParameter("mnda_FechaCrea", mnda_FechaCrea) :
                new ObjectParameter("mnda_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbMoneda_Insert_Result>("UDP_Gral_tbMoneda_Insert", mnda_AbreviaturaParameter, mnda_NombreParameter, mnda_UsuarioCreaParameter, mnda_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbMoneda_Update_Result> UDP_Gral_tbMoneda_Update(Nullable<short> mnda_Id, string mnda_Abreviatura, string mnda_Nombre, Nullable<int> mnda_UsuarioCrea, Nullable<System.DateTime> mnda_FechaCrea, Nullable<int> mnda_UsuarioModifica, Nullable<System.DateTime> mnda_FechaModifica)
        {
            var mnda_IdParameter = mnda_Id.HasValue ?
                new ObjectParameter("mnda_Id", mnda_Id) :
                new ObjectParameter("mnda_Id", typeof(short));
    
            var mnda_AbreviaturaParameter = mnda_Abreviatura != null ?
                new ObjectParameter("mnda_Abreviatura", mnda_Abreviatura) :
                new ObjectParameter("mnda_Abreviatura", typeof(string));
    
            var mnda_NombreParameter = mnda_Nombre != null ?
                new ObjectParameter("mnda_Nombre", mnda_Nombre) :
                new ObjectParameter("mnda_Nombre", typeof(string));
    
            var mnda_UsuarioCreaParameter = mnda_UsuarioCrea.HasValue ?
                new ObjectParameter("mnda_UsuarioCrea", mnda_UsuarioCrea) :
                new ObjectParameter("mnda_UsuarioCrea", typeof(int));
    
            var mnda_FechaCreaParameter = mnda_FechaCrea.HasValue ?
                new ObjectParameter("mnda_FechaCrea", mnda_FechaCrea) :
                new ObjectParameter("mnda_FechaCrea", typeof(System.DateTime));
    
            var mnda_UsuarioModificaParameter = mnda_UsuarioModifica.HasValue ?
                new ObjectParameter("mnda_UsuarioModifica", mnda_UsuarioModifica) :
                new ObjectParameter("mnda_UsuarioModifica", typeof(int));
    
            var mnda_FechaModificaParameter = mnda_FechaModifica.HasValue ?
                new ObjectParameter("mnda_FechaModifica", mnda_FechaModifica) :
                new ObjectParameter("mnda_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbMoneda_Update_Result>("UDP_Gral_tbMoneda_Update", mnda_IdParameter, mnda_AbreviaturaParameter, mnda_NombreParameter, mnda_UsuarioCreaParameter, mnda_FechaCreaParameter, mnda_UsuarioModificaParameter, mnda_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbMunicipio_Delete_Result> UDP_Gral_tbMunicipio_Delete(string mun_Codigo)
        {
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbMunicipio_Delete_Result>("UDP_Gral_tbMunicipio_Delete", mun_CodigoParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbMunicipio_Insert_Result> UDP_Gral_tbMunicipio_Insert(string mun_Codigo, string dep_Codigo, string mun_Nombre, Nullable<int> mun_UsuarioCrea, Nullable<System.DateTime> mun_FechaCrea)
        {
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            var dep_CodigoParameter = dep_Codigo != null ?
                new ObjectParameter("dep_Codigo", dep_Codigo) :
                new ObjectParameter("dep_Codigo", typeof(string));
    
            var mun_NombreParameter = mun_Nombre != null ?
                new ObjectParameter("mun_Nombre", mun_Nombre) :
                new ObjectParameter("mun_Nombre", typeof(string));
    
            var mun_UsuarioCreaParameter = mun_UsuarioCrea.HasValue ?
                new ObjectParameter("mun_UsuarioCrea", mun_UsuarioCrea) :
                new ObjectParameter("mun_UsuarioCrea", typeof(int));
    
            var mun_FechaCreaParameter = mun_FechaCrea.HasValue ?
                new ObjectParameter("mun_FechaCrea", mun_FechaCrea) :
                new ObjectParameter("mun_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbMunicipio_Insert_Result>("UDP_Gral_tbMunicipio_Insert", mun_CodigoParameter, dep_CodigoParameter, mun_NombreParameter, mun_UsuarioCreaParameter, mun_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbMunicipio_Update_Result> UDP_Gral_tbMunicipio_Update(string mun_Codigo, string dep_Codigo, string mun_Nombre, Nullable<int> mun_UsuarioCrea, Nullable<System.DateTime> mun_FechaCrea, Nullable<int> mun_UsuarioModifica, Nullable<System.DateTime> mun_FechaModifica)
        {
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            var dep_CodigoParameter = dep_Codigo != null ?
                new ObjectParameter("dep_Codigo", dep_Codigo) :
                new ObjectParameter("dep_Codigo", typeof(string));
    
            var mun_NombreParameter = mun_Nombre != null ?
                new ObjectParameter("mun_Nombre", mun_Nombre) :
                new ObjectParameter("mun_Nombre", typeof(string));
    
            var mun_UsuarioCreaParameter = mun_UsuarioCrea.HasValue ?
                new ObjectParameter("mun_UsuarioCrea", mun_UsuarioCrea) :
                new ObjectParameter("mun_UsuarioCrea", typeof(int));
    
            var mun_FechaCreaParameter = mun_FechaCrea.HasValue ?
                new ObjectParameter("mun_FechaCrea", mun_FechaCrea) :
                new ObjectParameter("mun_FechaCrea", typeof(System.DateTime));
    
            var mun_UsuarioModificaParameter = mun_UsuarioModifica.HasValue ?
                new ObjectParameter("mun_UsuarioModifica", mun_UsuarioModifica) :
                new ObjectParameter("mun_UsuarioModifica", typeof(int));
    
            var mun_FechaModificaParameter = mun_FechaModifica.HasValue ?
                new ObjectParameter("mun_FechaModifica", mun_FechaModifica) :
                new ObjectParameter("mun_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbMunicipio_Update_Result>("UDP_Gral_tbMunicipio_Update", mun_CodigoParameter, dep_CodigoParameter, mun_NombreParameter, mun_UsuarioCreaParameter, mun_FechaCreaParameter, mun_UsuarioModificaParameter, mun_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbParametro_Insert_Result> UDP_Gral_tbParametro_Insert(Nullable<byte> par_Id, string par_NombreEmpresa, string par_TelefonoEmpresa, string par_CorreoEmpresa, string par_PathLogo, Nullable<short> mnda_Id, Nullable<int> par_RolGerenteTienda, Nullable<int> par_RolCreditoCobranza, Nullable<int> par_RolSupervisorCaja, Nullable<int> par_RolCajero, Nullable<int> par_RolAuditor, Nullable<int> par_SucursalPrincipal, Nullable<decimal> par_PorcentajeDescuentoTE, Nullable<int> par_IdConsumidorFinal, Nullable<int> par_UsuarioCrea, Nullable<System.DateTime> par_FechaCrea)
        {
            var par_IdParameter = par_Id.HasValue ?
                new ObjectParameter("par_Id", par_Id) :
                new ObjectParameter("par_Id", typeof(byte));
    
            var par_NombreEmpresaParameter = par_NombreEmpresa != null ?
                new ObjectParameter("par_NombreEmpresa", par_NombreEmpresa) :
                new ObjectParameter("par_NombreEmpresa", typeof(string));
    
            var par_TelefonoEmpresaParameter = par_TelefonoEmpresa != null ?
                new ObjectParameter("par_TelefonoEmpresa", par_TelefonoEmpresa) :
                new ObjectParameter("par_TelefonoEmpresa", typeof(string));
    
            var par_CorreoEmpresaParameter = par_CorreoEmpresa != null ?
                new ObjectParameter("par_CorreoEmpresa", par_CorreoEmpresa) :
                new ObjectParameter("par_CorreoEmpresa", typeof(string));
    
            var par_PathLogoParameter = par_PathLogo != null ?
                new ObjectParameter("par_PathLogo", par_PathLogo) :
                new ObjectParameter("par_PathLogo", typeof(string));
    
            var mnda_IdParameter = mnda_Id.HasValue ?
                new ObjectParameter("mnda_Id", mnda_Id) :
                new ObjectParameter("mnda_Id", typeof(short));
    
            var par_RolGerenteTiendaParameter = par_RolGerenteTienda.HasValue ?
                new ObjectParameter("par_RolGerenteTienda", par_RolGerenteTienda) :
                new ObjectParameter("par_RolGerenteTienda", typeof(int));
    
            var par_RolCreditoCobranzaParameter = par_RolCreditoCobranza.HasValue ?
                new ObjectParameter("par_RolCreditoCobranza", par_RolCreditoCobranza) :
                new ObjectParameter("par_RolCreditoCobranza", typeof(int));
    
            var par_RolSupervisorCajaParameter = par_RolSupervisorCaja.HasValue ?
                new ObjectParameter("par_RolSupervisorCaja", par_RolSupervisorCaja) :
                new ObjectParameter("par_RolSupervisorCaja", typeof(int));
    
            var par_RolCajeroParameter = par_RolCajero.HasValue ?
                new ObjectParameter("par_RolCajero", par_RolCajero) :
                new ObjectParameter("par_RolCajero", typeof(int));
    
            var par_RolAuditorParameter = par_RolAuditor.HasValue ?
                new ObjectParameter("par_RolAuditor", par_RolAuditor) :
                new ObjectParameter("par_RolAuditor", typeof(int));
    
            var par_SucursalPrincipalParameter = par_SucursalPrincipal.HasValue ?
                new ObjectParameter("par_SucursalPrincipal", par_SucursalPrincipal) :
                new ObjectParameter("par_SucursalPrincipal", typeof(int));
    
            var par_PorcentajeDescuentoTEParameter = par_PorcentajeDescuentoTE.HasValue ?
                new ObjectParameter("par_PorcentajeDescuentoTE", par_PorcentajeDescuentoTE) :
                new ObjectParameter("par_PorcentajeDescuentoTE", typeof(decimal));
    
            var par_IdConsumidorFinalParameter = par_IdConsumidorFinal.HasValue ?
                new ObjectParameter("par_IdConsumidorFinal", par_IdConsumidorFinal) :
                new ObjectParameter("par_IdConsumidorFinal", typeof(int));
    
            var par_UsuarioCreaParameter = par_UsuarioCrea.HasValue ?
                new ObjectParameter("par_UsuarioCrea", par_UsuarioCrea) :
                new ObjectParameter("par_UsuarioCrea", typeof(int));
    
            var par_FechaCreaParameter = par_FechaCrea.HasValue ?
                new ObjectParameter("par_FechaCrea", par_FechaCrea) :
                new ObjectParameter("par_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbParametro_Insert_Result>("UDP_Gral_tbParametro_Insert", par_IdParameter, par_NombreEmpresaParameter, par_TelefonoEmpresaParameter, par_CorreoEmpresaParameter, par_PathLogoParameter, mnda_IdParameter, par_RolGerenteTiendaParameter, par_RolCreditoCobranzaParameter, par_RolSupervisorCajaParameter, par_RolCajeroParameter, par_RolAuditorParameter, par_SucursalPrincipalParameter, par_PorcentajeDescuentoTEParameter, par_IdConsumidorFinalParameter, par_UsuarioCreaParameter, par_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbParametro_Update_Result> UDP_Gral_tbParametro_Update(Nullable<byte> par_Id, string par_NombreEmpresa, string par_TelefonoEmpresa, string par_CorreoEmpresa, string par_PathLogo, Nullable<short> mnda_Id, Nullable<int> par_RolGerenteTienda, Nullable<int> par_RolCreditoCobranza, Nullable<int> par_RolSupervisorCaja, Nullable<int> par_RolCajero, Nullable<int> par_RolAuditor, Nullable<int> par_SucursalPrincipal, Nullable<int> par_UsuarioCrea, Nullable<System.DateTime> par_FechaCrea, Nullable<decimal> par_PorcentajeDescuentoTE, Nullable<int> par_IdConsumidorFinal, Nullable<int> par_UsuarioModifica, Nullable<System.DateTime> par_FechaModifica)
        {
            var par_IdParameter = par_Id.HasValue ?
                new ObjectParameter("par_Id", par_Id) :
                new ObjectParameter("par_Id", typeof(byte));
    
            var par_NombreEmpresaParameter = par_NombreEmpresa != null ?
                new ObjectParameter("par_NombreEmpresa", par_NombreEmpresa) :
                new ObjectParameter("par_NombreEmpresa", typeof(string));
    
            var par_TelefonoEmpresaParameter = par_TelefonoEmpresa != null ?
                new ObjectParameter("par_TelefonoEmpresa", par_TelefonoEmpresa) :
                new ObjectParameter("par_TelefonoEmpresa", typeof(string));
    
            var par_CorreoEmpresaParameter = par_CorreoEmpresa != null ?
                new ObjectParameter("par_CorreoEmpresa", par_CorreoEmpresa) :
                new ObjectParameter("par_CorreoEmpresa", typeof(string));
    
            var par_PathLogoParameter = par_PathLogo != null ?
                new ObjectParameter("par_PathLogo", par_PathLogo) :
                new ObjectParameter("par_PathLogo", typeof(string));
    
            var mnda_IdParameter = mnda_Id.HasValue ?
                new ObjectParameter("mnda_Id", mnda_Id) :
                new ObjectParameter("mnda_Id", typeof(short));
    
            var par_RolGerenteTiendaParameter = par_RolGerenteTienda.HasValue ?
                new ObjectParameter("par_RolGerenteTienda", par_RolGerenteTienda) :
                new ObjectParameter("par_RolGerenteTienda", typeof(int));
    
            var par_RolCreditoCobranzaParameter = par_RolCreditoCobranza.HasValue ?
                new ObjectParameter("par_RolCreditoCobranza", par_RolCreditoCobranza) :
                new ObjectParameter("par_RolCreditoCobranza", typeof(int));
    
            var par_RolSupervisorCajaParameter = par_RolSupervisorCaja.HasValue ?
                new ObjectParameter("par_RolSupervisorCaja", par_RolSupervisorCaja) :
                new ObjectParameter("par_RolSupervisorCaja", typeof(int));
    
            var par_RolCajeroParameter = par_RolCajero.HasValue ?
                new ObjectParameter("par_RolCajero", par_RolCajero) :
                new ObjectParameter("par_RolCajero", typeof(int));
    
            var par_RolAuditorParameter = par_RolAuditor.HasValue ?
                new ObjectParameter("par_RolAuditor", par_RolAuditor) :
                new ObjectParameter("par_RolAuditor", typeof(int));
    
            var par_SucursalPrincipalParameter = par_SucursalPrincipal.HasValue ?
                new ObjectParameter("par_SucursalPrincipal", par_SucursalPrincipal) :
                new ObjectParameter("par_SucursalPrincipal", typeof(int));
    
            var par_UsuarioCreaParameter = par_UsuarioCrea.HasValue ?
                new ObjectParameter("par_UsuarioCrea", par_UsuarioCrea) :
                new ObjectParameter("par_UsuarioCrea", typeof(int));
    
            var par_FechaCreaParameter = par_FechaCrea.HasValue ?
                new ObjectParameter("par_FechaCrea", par_FechaCrea) :
                new ObjectParameter("par_FechaCrea", typeof(System.DateTime));
    
            var par_PorcentajeDescuentoTEParameter = par_PorcentajeDescuentoTE.HasValue ?
                new ObjectParameter("par_PorcentajeDescuentoTE", par_PorcentajeDescuentoTE) :
                new ObjectParameter("par_PorcentajeDescuentoTE", typeof(decimal));
    
            var par_IdConsumidorFinalParameter = par_IdConsumidorFinal.HasValue ?
                new ObjectParameter("par_IdConsumidorFinal", par_IdConsumidorFinal) :
                new ObjectParameter("par_IdConsumidorFinal", typeof(int));
    
            var par_UsuarioModificaParameter = par_UsuarioModifica.HasValue ?
                new ObjectParameter("par_UsuarioModifica", par_UsuarioModifica) :
                new ObjectParameter("par_UsuarioModifica", typeof(int));
    
            var par_FechaModificaParameter = par_FechaModifica.HasValue ?
                new ObjectParameter("par_FechaModifica", par_FechaModifica) :
                new ObjectParameter("par_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbParametro_Update_Result>("UDP_Gral_tbParametro_Update", par_IdParameter, par_NombreEmpresaParameter, par_TelefonoEmpresaParameter, par_CorreoEmpresaParameter, par_PathLogoParameter, mnda_IdParameter, par_RolGerenteTiendaParameter, par_RolCreditoCobranzaParameter, par_RolSupervisorCajaParameter, par_RolCajeroParameter, par_RolAuditorParameter, par_SucursalPrincipalParameter, par_UsuarioCreaParameter, par_FechaCreaParameter, par_PorcentajeDescuentoTEParameter, par_IdConsumidorFinalParameter, par_UsuarioModificaParameter, par_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbTipoIdentificacion_Insert_Result> UDP_Gral_tbTipoIdentificacion_Insert(string tpi_Descripcion, Nullable<int> tpi_UsuarioCrea, Nullable<System.DateTime> tpi_FechaCrea)
        {
            var tpi_DescripcionParameter = tpi_Descripcion != null ?
                new ObjectParameter("tpi_Descripcion", tpi_Descripcion) :
                new ObjectParameter("tpi_Descripcion", typeof(string));
    
            var tpi_UsuarioCreaParameter = tpi_UsuarioCrea.HasValue ?
                new ObjectParameter("tpi_UsuarioCrea", tpi_UsuarioCrea) :
                new ObjectParameter("tpi_UsuarioCrea", typeof(int));
    
            var tpi_FechaCreaParameter = tpi_FechaCrea.HasValue ?
                new ObjectParameter("tpi_FechaCrea", tpi_FechaCrea) :
                new ObjectParameter("tpi_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbTipoIdentificacion_Insert_Result>("UDP_Gral_tbTipoIdentificacion_Insert", tpi_DescripcionParameter, tpi_UsuarioCreaParameter, tpi_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbTipoIdentificacion_Update_Result> UDP_Gral_tbTipoIdentificacion_Update(Nullable<byte> tpi_Id, string tpi_Descripcion, Nullable<int> tpi_UsuarioCrea, Nullable<System.DateTime> tpi_FechaCrea, Nullable<int> tpi_UsuarioModifica, Nullable<System.DateTime> tpi_FechaModifica)
        {
            var tpi_IdParameter = tpi_Id.HasValue ?
                new ObjectParameter("tpi_Id", tpi_Id) :
                new ObjectParameter("tpi_Id", typeof(byte));
    
            var tpi_DescripcionParameter = tpi_Descripcion != null ?
                new ObjectParameter("tpi_Descripcion", tpi_Descripcion) :
                new ObjectParameter("tpi_Descripcion", typeof(string));
    
            var tpi_UsuarioCreaParameter = tpi_UsuarioCrea.HasValue ?
                new ObjectParameter("tpi_UsuarioCrea", tpi_UsuarioCrea) :
                new ObjectParameter("tpi_UsuarioCrea", typeof(int));
    
            var tpi_FechaCreaParameter = tpi_FechaCrea.HasValue ?
                new ObjectParameter("tpi_FechaCrea", tpi_FechaCrea) :
                new ObjectParameter("tpi_FechaCrea", typeof(System.DateTime));
    
            var tpi_UsuarioModificaParameter = tpi_UsuarioModifica.HasValue ?
                new ObjectParameter("tpi_UsuarioModifica", tpi_UsuarioModifica) :
                new ObjectParameter("tpi_UsuarioModifica", typeof(int));
    
            var tpi_FechaModificaParameter = tpi_FechaModifica.HasValue ?
                new ObjectParameter("tpi_FechaModifica", tpi_FechaModifica) :
                new ObjectParameter("tpi_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbTipoIdentificacion_Update_Result>("UDP_Gral_tbTipoIdentificacion_Update", tpi_IdParameter, tpi_DescripcionParameter, tpi_UsuarioCreaParameter, tpi_FechaCreaParameter, tpi_UsuarioModificaParameter, tpi_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbUnidadMedida_Insert_Result> UDP_Gral_tbUnidadMedida_Insert(string uni_Descripcion, string uni_Abreviatura, Nullable<int> uni_UsuarioCrea, Nullable<System.DateTime> uni_FechaCrea)
        {
            var uni_DescripcionParameter = uni_Descripcion != null ?
                new ObjectParameter("uni_Descripcion", uni_Descripcion) :
                new ObjectParameter("uni_Descripcion", typeof(string));
    
            var uni_AbreviaturaParameter = uni_Abreviatura != null ?
                new ObjectParameter("uni_Abreviatura", uni_Abreviatura) :
                new ObjectParameter("uni_Abreviatura", typeof(string));
    
            var uni_UsuarioCreaParameter = uni_UsuarioCrea.HasValue ?
                new ObjectParameter("uni_UsuarioCrea", uni_UsuarioCrea) :
                new ObjectParameter("uni_UsuarioCrea", typeof(int));
    
            var uni_FechaCreaParameter = uni_FechaCrea.HasValue ?
                new ObjectParameter("uni_FechaCrea", uni_FechaCrea) :
                new ObjectParameter("uni_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbUnidadMedida_Insert_Result>("UDP_Gral_tbUnidadMedida_Insert", uni_DescripcionParameter, uni_AbreviaturaParameter, uni_UsuarioCreaParameter, uni_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Gral_tbUnidadMedida_Update_Result> UDP_Gral_tbUnidadMedida_Update(Nullable<int> uni_Id, string uni_Descripcion, string uni_Abreviatura, Nullable<int> uni_UsuarioCrea, Nullable<System.DateTime> uni_FechaCrea, Nullable<int> uni_UsuarioModifica, Nullable<System.DateTime> uni_FechaModifica)
        {
            var uni_IdParameter = uni_Id.HasValue ?
                new ObjectParameter("uni_Id", uni_Id) :
                new ObjectParameter("uni_Id", typeof(int));
    
            var uni_DescripcionParameter = uni_Descripcion != null ?
                new ObjectParameter("uni_Descripcion", uni_Descripcion) :
                new ObjectParameter("uni_Descripcion", typeof(string));
    
            var uni_AbreviaturaParameter = uni_Abreviatura != null ?
                new ObjectParameter("uni_Abreviatura", uni_Abreviatura) :
                new ObjectParameter("uni_Abreviatura", typeof(string));
    
            var uni_UsuarioCreaParameter = uni_UsuarioCrea.HasValue ?
                new ObjectParameter("uni_UsuarioCrea", uni_UsuarioCrea) :
                new ObjectParameter("uni_UsuarioCrea", typeof(int));
    
            var uni_FechaCreaParameter = uni_FechaCrea.HasValue ?
                new ObjectParameter("uni_FechaCrea", uni_FechaCrea) :
                new ObjectParameter("uni_FechaCrea", typeof(System.DateTime));
    
            var uni_UsuarioModificaParameter = uni_UsuarioModifica.HasValue ?
                new ObjectParameter("uni_UsuarioModifica", uni_UsuarioModifica) :
                new ObjectParameter("uni_UsuarioModifica", typeof(int));
    
            var uni_FechaModificaParameter = uni_FechaModifica.HasValue ?
                new ObjectParameter("uni_FechaModifica", uni_FechaModifica) :
                new ObjectParameter("uni_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Gral_tbUnidadMedida_Update_Result>("UDP_Gral_tbUnidadMedida_Update", uni_IdParameter, uni_DescripcionParameter, uni_AbreviaturaParameter, uni_UsuarioCreaParameter, uni_FechaCreaParameter, uni_UsuarioModificaParameter, uni_FechaModificaParameter);
        }
    
        public virtual ObjectResult<string> ACtualizar_Cantidad_BOdegaDetalle(Nullable<int> iDBodega, string iDProducto, Nullable<decimal> cantidadSolicitadad, Nullable<System.DateTime> fechaElaboracion, Nullable<int> bodegaDestino)
        {
            var iDBodegaParameter = iDBodega.HasValue ?
                new ObjectParameter("IDBodega", iDBodega) :
                new ObjectParameter("IDBodega", typeof(int));
    
            var iDProductoParameter = iDProducto != null ?
                new ObjectParameter("IDProducto", iDProducto) :
                new ObjectParameter("IDProducto", typeof(string));
    
            var cantidadSolicitadadParameter = cantidadSolicitadad.HasValue ?
                new ObjectParameter("CantidadSolicitadad", cantidadSolicitadad) :
                new ObjectParameter("CantidadSolicitadad", typeof(decimal));
    
            var fechaElaboracionParameter = fechaElaboracion.HasValue ?
                new ObjectParameter("FechaElaboracion", fechaElaboracion) :
                new ObjectParameter("FechaElaboracion", typeof(System.DateTime));
    
            var bodegaDestinoParameter = bodegaDestino.HasValue ?
                new ObjectParameter("BodegaDestino", bodegaDestino) :
                new ObjectParameter("BodegaDestino", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("ACtualizar_Cantidad_BOdegaDetalle", iDBodegaParameter, iDProductoParameter, cantidadSolicitadadParameter, fechaElaboracionParameter, bodegaDestinoParameter);
        }
    
        public virtual ObjectResult<ConteoParametro_Result> ConteoParametro(Nullable<int> conteo)
        {
            var conteoParameter = conteo.HasValue ?
                new ObjectParameter("conteo", conteo) :
                new ObjectParameter("conteo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ConteoParametro_Result>("ConteoParametro", conteoParameter);
        }
    
        public virtual ObjectResult<crud_tbProductoSelect_Result> crud_tbProductoSelect(string prod_Codigo)
        {
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<crud_tbProductoSelect_Result>("crud_tbProductoSelect", prod_CodigoParameter);
        }
    
        public virtual ObjectResult<ObtenerRol_Result> ObtenerRol(Nullable<int> usu_Id)
        {
            var usu_IdParameter = usu_Id.HasValue ?
                new ObjectParameter("usu_Id", usu_Id) :
                new ObjectParameter("usu_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ObtenerRol_Result>("ObtenerRol", usu_IdParameter);
        }
    
        public virtual ObjectResult<SDP_Inv_Cantidad_Salida_Emitida_Result> SDP_Inv_Cantidad_Salida_Emitida(string prod_Codigo)
        {
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Inv_Cantidad_Salida_Emitida_Result>("SDP_Inv_Cantidad_Salida_Emitida", prod_CodigoParameter);
        }
    
        public virtual ObjectResult<SDP_Inv_CatalogoProductos_Result> SDP_Inv_CatalogoProductos(string prod_Codigo)
        {
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Inv_CatalogoProductos_Result>("SDP_Inv_CatalogoProductos", prod_CodigoParameter);
        }
    
        public virtual ObjectResult<SDP_Inv_PackingList_Result> SDP_Inv_PackingList(string box_Codigo)
        {
            var box_CodigoParameter = box_Codigo != null ?
                new ObjectParameter("box_Codigo", box_Codigo) :
                new ObjectParameter("box_Codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Inv_PackingList_Result>("SDP_Inv_PackingList", box_CodigoParameter);
        }
    
        public virtual ObjectResult<SDP_Inv_Proyeccion_de_Movimiento_de_Producto_Result> SDP_Inv_Proyeccion_de_Movimiento_de_Producto(string prod_Codigo)
        {
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("Prod_Codigo", prod_Codigo) :
                new ObjectParameter("Prod_Codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Inv_Proyeccion_de_Movimiento_de_Producto_Result>("SDP_Inv_Proyeccion_de_Movimiento_de_Producto", prod_CodigoParameter);
        }
    
        public virtual ObjectResult<SDP_Inv_Salida_Imprimir_Result> SDP_Inv_Salida_Imprimir(Nullable<int> sal_Id)
        {
            var sal_IdParameter = sal_Id.HasValue ?
                new ObjectParameter("sal_Id", sal_Id) :
                new ObjectParameter("sal_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Inv_Salida_Imprimir_Result>("SDP_Inv_Salida_Imprimir", sal_IdParameter);
        }
    
        public virtual ObjectResult<SDP_Inv_Salida_Imprimir_Reporte_Result> SDP_Inv_Salida_Imprimir_Reporte(Nullable<int> tsal_Id, Nullable<System.DateTime> fecha)
        {
            var tsal_IdParameter = tsal_Id.HasValue ?
                new ObjectParameter("tsal_Id", tsal_Id) :
                new ObjectParameter("tsal_Id", typeof(int));
    
            var fechaParameter = fecha.HasValue ?
                new ObjectParameter("Fecha", fecha) :
                new ObjectParameter("Fecha", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Inv_Salida_Imprimir_Reporte_Result>("SDP_Inv_Salida_Imprimir_Reporte", tsal_IdParameter, fechaParameter);
        }
    
        public virtual ObjectResult<SDP_Inv_tbBodegaDetalle_Select_Producto_Result> SDP_Inv_tbBodegaDetalle_Select_Producto(Nullable<int> bod_Id)
        {
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Inv_tbBodegaDetalle_Select_Producto_Result>("SDP_Inv_tbBodegaDetalle_Select_Producto", bod_IdParameter);
        }
    
        public virtual ObjectResult<SDP_Inv_tbBoxDetalle_Select_Result> SDP_Inv_tbBoxDetalle_Select(Nullable<int> boxd_Id)
        {
            var boxd_IdParameter = boxd_Id.HasValue ?
                new ObjectParameter("boxd_Id", boxd_Id) :
                new ObjectParameter("boxd_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Inv_tbBoxDetalle_Select_Result>("SDP_Inv_tbBoxDetalle_Select", boxd_IdParameter);
        }
    
        public virtual ObjectResult<SDP_Inv_tbProducto_Select_Result> SDP_Inv_tbProducto_Select()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Inv_tbProducto_Select_Result>("SDP_Inv_tbProducto_Select");
        }
    
        public virtual ObjectResult<SDP_Inv_tbProducto_Select_CodBar_Result> SDP_Inv_tbProducto_Select_CodBar(string prod_CodigoBarras, Nullable<int> bod_Id)
        {
            var prod_CodigoBarrasParameter = prod_CodigoBarras != null ?
                new ObjectParameter("prod_CodigoBarras", prod_CodigoBarras) :
                new ObjectParameter("prod_CodigoBarras", typeof(string));
    
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Inv_tbProducto_Select_CodBar_Result>("SDP_Inv_tbProducto_Select_CodBar", prod_CodigoBarrasParameter, bod_IdParameter);
        }
    
        public virtual ObjectResult<SDP_Inv_tbProducto_Select_OUT_Result> SDP_Inv_tbProducto_Select_OUT(string prod_Codigo)
        {
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Inv_tbProducto_Select_OUT_Result>("SDP_Inv_tbProducto_Select_OUT", prod_CodigoParameter);
        }
    
        public virtual ObjectResult<Nullable<long>> SDP_Inv_tbSalida_Factura_Exist(string fact_Codigo)
        {
            var fact_CodigoParameter = fact_Codigo != null ?
                new ObjectParameter("fact_Codigo", fact_Codigo) :
                new ObjectParameter("fact_Codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<long>>("SDP_Inv_tbSalida_Factura_Exist", fact_CodigoParameter);
        }
    
        public virtual ObjectResult<SDP_Inv_tbSalidaDetalle_Edit_Select_Result> SDP_Inv_tbSalidaDetalle_Edit_Select(Nullable<int> sald_Id)
        {
            var sald_IdParameter = sald_Id.HasValue ?
                new ObjectParameter("sald_Id", sald_Id) :
                new ObjectParameter("sald_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Inv_tbSalidaDetalle_Edit_Select_Result>("SDP_Inv_tbSalidaDetalle_Edit_Select", sald_IdParameter);
        }
    
        public virtual ObjectResult<SDP_Inv_tbSalidaDetalle_Select_Result> SDP_Inv_tbSalidaDetalle_Select(Nullable<int> sald_Id)
        {
            var sald_IdParameter = sald_Id.HasValue ?
                new ObjectParameter("sald_Id", sald_Id) :
                new ObjectParameter("sald_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Inv_tbSalidaDetalle_Select_Result>("SDP_Inv_tbSalidaDetalle_Select", sald_IdParameter);
        }
    
        public virtual ObjectResult<SDP_tbBodega_Listado_Result> SDP_tbBodega_Listado(Nullable<int> bod_Id)
        {
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_tbBodega_Listado_Result>("SDP_tbBodega_Listado", bod_IdParameter);
        }
    
        public virtual ObjectResult<SDP_tbentradadetalle_Select_Result> SDP_tbentradadetalle_Select(Nullable<int> entd_Id)
        {
            var entd_IdParameter = entd_Id.HasValue ?
                new ObjectParameter("entd_Id", entd_Id) :
                new ObjectParameter("entd_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_tbentradadetalle_Select_Result>("SDP_tbentradadetalle_Select", entd_IdParameter);
        }
    
        public virtual ObjectResult<SDP_tbentradaImprimir_Select_Result> SDP_tbentradaImprimir_Select(Nullable<int> tipoEntrada, Nullable<System.DateTime> fechaElaboracion, Nullable<byte> estado, Nullable<int> bodega)
        {
            var tipoEntradaParameter = tipoEntrada.HasValue ?
                new ObjectParameter("TipoEntrada", tipoEntrada) :
                new ObjectParameter("TipoEntrada", typeof(int));
    
            var fechaElaboracionParameter = fechaElaboracion.HasValue ?
                new ObjectParameter("FechaElaboracion", fechaElaboracion) :
                new ObjectParameter("FechaElaboracion", typeof(System.DateTime));
    
            var estadoParameter = estado.HasValue ?
                new ObjectParameter("estado", estado) :
                new ObjectParameter("estado", typeof(byte));
    
            var bodegaParameter = bodega.HasValue ?
                new ObjectParameter("bodega", bodega) :
                new ObjectParameter("bodega", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_tbentradaImprimir_Select_Result>("SDP_tbentradaImprimir_Select", tipoEntradaParameter, fechaElaboracionParameter, estadoParameter, bodegaParameter);
        }
    
        public virtual ObjectResult<SDP_tbentradaImprimirCompra_Select_Result> SDP_tbentradaImprimirCompra_Select(Nullable<int> tipoEntrada)
        {
            var tipoEntradaParameter = tipoEntrada.HasValue ?
                new ObjectParameter("TipoEntrada", tipoEntrada) :
                new ObjectParameter("TipoEntrada", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_tbentradaImprimirCompra_Select_Result>("SDP_tbentradaImprimirCompra_Select", tipoEntradaParameter);
        }
    
        public virtual ObjectResult<SDP_tbentradaImprimirPorID_Select_Result> SDP_tbentradaImprimirPorID_Select(Nullable<int> ent_id)
        {
            var ent_idParameter = ent_id.HasValue ?
                new ObjectParameter("ent_id", ent_id) :
                new ObjectParameter("ent_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_tbentradaImprimirPorID_Select_Result>("SDP_tbentradaImprimirPorID_Select", ent_idParameter);
        }
    
        public virtual ObjectResult<SDP_tbInventarioFisicoDetalle_Select_Result> SDP_tbInventarioFisicoDetalle_Select(Nullable<int> invfd_Id)
        {
            var invfd_IdParameter = invfd_Id.HasValue ?
                new ObjectParameter("invfd_Id", invfd_Id) :
                new ObjectParameter("invfd_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_tbInventarioFisicoDetalle_Select_Result>("SDP_tbInventarioFisicoDetalle_Select", invfd_IdParameter);
        }
    
        public virtual ObjectResult<SDP_tbProductoCategoria_tbProductoSubCategoria_Select_Result> SDP_tbProductoCategoria_tbProductoSubCategoria_Select(Nullable<int> pcat_Id)
        {
            var pcat_IdParameter = pcat_Id.HasValue ?
                new ObjectParameter("pcat_Id", pcat_Id) :
                new ObjectParameter("pcat_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_tbProductoCategoria_tbProductoSubCategoria_Select_Result>("SDP_tbProductoCategoria_tbProductoSubCategoria_Select", pcat_IdParameter);
        }
    
        public virtual ObjectResult<SDP_tbProductoSubcategoria_Select_Result> SDP_tbProductoSubcategoria_Select(Nullable<int> pscat_Id)
        {
            var pscat_IdParameter = pscat_Id.HasValue ?
                new ObjectParameter("pscat_Id", pscat_Id) :
                new ObjectParameter("pscat_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_tbProductoSubcategoria_Select_Result>("SDP_tbProductoSubcategoria_Select", pscat_IdParameter);
        }
    
        public virtual ObjectResult<SP_tbEntrada_ProductosRepetidos_Result> SP_tbEntrada_ProductosRepetidos(string prod_CodigoBarras)
        {
            var prod_CodigoBarrasParameter = prod_CodigoBarras != null ?
                new ObjectParameter("prod_CodigoBarras", prod_CodigoBarras) :
                new ObjectParameter("prod_CodigoBarras", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_tbEntrada_ProductosRepetidos_Result>("SP_tbEntrada_ProductosRepetidos", prod_CodigoBarrasParameter);
        }
    
        public virtual ObjectResult<SP_tbInventariofisico_ProductosRepetidos_Result> SP_tbInventariofisico_ProductosRepetidos(string prod_CodigoBarras)
        {
            var prod_CodigoBarrasParameter = prod_CodigoBarras != null ?
                new ObjectParameter("prod_CodigoBarras", prod_CodigoBarras) :
                new ObjectParameter("prod_CodigoBarras", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_tbInventariofisico_ProductosRepetidos_Result>("SP_tbInventariofisico_ProductosRepetidos", prod_CodigoBarrasParameter);
        }
    
        public virtual ObjectResult<spGetActividadEconomica_Result> spGetActividadEconomica(Nullable<short> acte_Id)
        {
            var acte_IdParameter = acte_Id.HasValue ?
                new ObjectParameter("acte_Id", acte_Id) :
                new ObjectParameter("acte_Id", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetActividadEconomica_Result>("spGetActividadEconomica", acte_IdParameter);
        }
    
        public virtual ObjectResult<spGetCategoria_Result> spGetCategoria(Nullable<int> pcat_id)
        {
            var pcat_idParameter = pcat_id.HasValue ?
                new ObjectParameter("pcat_id", pcat_id) :
                new ObjectParameter("pcat_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetCategoria_Result>("spGetCategoria", pcat_idParameter);
        }
    
        public virtual ObjectResult<spGetProducto_Result> spGetProducto(Nullable<int> bod_Id, string prod_CodigoBarras)
        {
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var prod_CodigoBarrasParameter = prod_CodigoBarras != null ?
                new ObjectParameter("prod_CodigoBarras", prod_CodigoBarras) :
                new ObjectParameter("prod_CodigoBarras", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetProducto_Result>("spGetProducto", bod_IdParameter, prod_CodigoBarrasParameter);
        }
    
        public virtual ObjectResult<spGetProducto_BodegaDetalle_Result> spGetProducto_BodegaDetalle(string prod_CodigoBarras)
        {
            var prod_CodigoBarrasParameter = prod_CodigoBarras != null ?
                new ObjectParameter("prod_CodigoBarras", prod_CodigoBarras) :
                new ObjectParameter("prod_CodigoBarras", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetProducto_BodegaDetalle_Result>("spGetProducto_BodegaDetalle", prod_CodigoBarrasParameter);
        }
    
        public virtual ObjectResult<SPGetResponsableBodega_Result> SPGetResponsableBodega(Nullable<int> bod_id)
        {
            var bod_idParameter = bod_id.HasValue ?
                new ObjectParameter("bod_id", bod_id) :
                new ObjectParameter("bod_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPGetResponsableBodega_Result>("SPGetResponsableBodega", bod_idParameter);
        }
    
        public virtual ObjectResult<string> SPGetRTNproveedor(Nullable<int> prov_Id)
        {
            var prov_IdParameter = prov_Id.HasValue ?
                new ObjectParameter("prov_Id", prov_Id) :
                new ObjectParameter("prov_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("SPGetRTNproveedor", prov_IdParameter);
        }
    
        public virtual ObjectResult<string> spGetSubCategoria(Nullable<int> pcat_Id)
        {
            var pcat_IdParameter = pcat_Id.HasValue ?
                new ObjectParameter("pcat_Id", pcat_Id) :
                new ObjectParameter("pcat_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("spGetSubCategoria", pcat_IdParameter);
        }
    
        public virtual ObjectResult<spGetSuCategoria_Result> spGetSuCategoria(Nullable<int> pscat_Id)
        {
            var pscat_IdParameter = pscat_Id.HasValue ?
                new ObjectParameter("pscat_Id", pscat_Id) :
                new ObjectParameter("pscat_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetSuCategoria_Result>("spGetSuCategoria", pscat_IdParameter);
        }
    
        public virtual ObjectResult<tbInventarioFisico_ImprimirConciliacion_Result> tbInventarioFisico_ImprimirConciliacion(Nullable<int> invf_Id)
        {
            var invf_IdParameter = invf_Id.HasValue ?
                new ObjectParameter("invf_Id", invf_Id) :
                new ObjectParameter("invf_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<tbInventarioFisico_ImprimirConciliacion_Result>("tbInventarioFisico_ImprimirConciliacion", invf_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_CantidadExistente_Result> UDP_Inv_CantidadExistente(Nullable<int> bod_Id, string prod_Codigo)
        {
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_CantidadExistente_Result>("UDP_Inv_CantidadExistente", bod_IdParameter, prod_CodigoParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_Consultar_Existencias_Productos_Result> UDP_Inv_Consultar_Existencias_Productos(Nullable<int> bod_Id, Nullable<int> suc_Id)
        {
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_Consultar_Existencias_Productos_Result>("UDP_Inv_Consultar_Existencias_Productos", bod_IdParameter, suc_IdParameter);
        }
    
        public virtual int UDP_Inv_ConsultarExistenciaProductos(string sucursal, string bodega, string producto, string marca)
        {
            var sucursalParameter = sucursal != null ?
                new ObjectParameter("sucursal", sucursal) :
                new ObjectParameter("sucursal", typeof(string));
    
            var bodegaParameter = bodega != null ?
                new ObjectParameter("bodega", bodega) :
                new ObjectParameter("bodega", typeof(string));
    
            var productoParameter = producto != null ?
                new ObjectParameter("Producto", producto) :
                new ObjectParameter("Producto", typeof(string));
    
            var marcaParameter = marca != null ?
                new ObjectParameter("Marca", marca) :
                new ObjectParameter("Marca", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UDP_Inv_ConsultarExistenciaProductos", sucursalParameter, bodegaParameter, productoParameter, marcaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_SolicitarProducto_ValidacionPorBodega_Result> UDP_Inv_SolicitarProducto_ValidacionPorBodega(Nullable<int> iDBodega, string iDProducto)
        {
            var iDBodegaParameter = iDBodega.HasValue ?
                new ObjectParameter("IDBodega", iDBodega) :
                new ObjectParameter("IDBodega", typeof(int));
    
            var iDProductoParameter = iDProducto != null ?
                new ObjectParameter("IDProducto", iDProducto) :
                new ObjectParameter("IDProducto", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_SolicitarProducto_ValidacionPorBodega_Result>("UDP_Inv_SolicitarProducto_ValidacionPorBodega", iDBodegaParameter, iDProductoParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbBodega_Insert_Result> UDP_Inv_tbBodega_Insert(string bod_Nombre, Nullable<int> bod_ResponsableBodega, string bod_Direccion, string bod_Correo, string bod_Telefono, string mun_Codigo, Nullable<int> bod_UsuarioCrea, Nullable<System.DateTime> bod_FechaCrea)
        {
            var bod_NombreParameter = bod_Nombre != null ?
                new ObjectParameter("bod_Nombre", bod_Nombre) :
                new ObjectParameter("bod_Nombre", typeof(string));
    
            var bod_ResponsableBodegaParameter = bod_ResponsableBodega.HasValue ?
                new ObjectParameter("bod_ResponsableBodega", bod_ResponsableBodega) :
                new ObjectParameter("bod_ResponsableBodega", typeof(int));
    
            var bod_DireccionParameter = bod_Direccion != null ?
                new ObjectParameter("bod_Direccion", bod_Direccion) :
                new ObjectParameter("bod_Direccion", typeof(string));
    
            var bod_CorreoParameter = bod_Correo != null ?
                new ObjectParameter("bod_Correo", bod_Correo) :
                new ObjectParameter("bod_Correo", typeof(string));
    
            var bod_TelefonoParameter = bod_Telefono != null ?
                new ObjectParameter("bod_Telefono", bod_Telefono) :
                new ObjectParameter("bod_Telefono", typeof(string));
    
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            var bod_UsuarioCreaParameter = bod_UsuarioCrea.HasValue ?
                new ObjectParameter("bod_UsuarioCrea", bod_UsuarioCrea) :
                new ObjectParameter("bod_UsuarioCrea", typeof(int));
    
            var bod_FechaCreaParameter = bod_FechaCrea.HasValue ?
                new ObjectParameter("bod_FechaCrea", bod_FechaCrea) :
                new ObjectParameter("bod_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbBodega_Insert_Result>("UDP_Inv_tbBodega_Insert", bod_NombreParameter, bod_ResponsableBodegaParameter, bod_DireccionParameter, bod_CorreoParameter, bod_TelefonoParameter, mun_CodigoParameter, bod_UsuarioCreaParameter, bod_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbBodega_Update_Result> UDP_Inv_tbBodega_Update(Nullable<int> bod_Id, string bod_Nombre, Nullable<int> bod_ResponsableBodega, string bod_Direccion, string bod_Correo, string bod_Telefono, string mun_Codigo, Nullable<int> bod_UsuarioCrea, Nullable<System.DateTime> bod_Fechacrea, Nullable<int> bod_UsuarioModifica, Nullable<System.DateTime> bod_FechaModifica)
        {
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var bod_NombreParameter = bod_Nombre != null ?
                new ObjectParameter("bod_Nombre", bod_Nombre) :
                new ObjectParameter("bod_Nombre", typeof(string));
    
            var bod_ResponsableBodegaParameter = bod_ResponsableBodega.HasValue ?
                new ObjectParameter("bod_ResponsableBodega", bod_ResponsableBodega) :
                new ObjectParameter("bod_ResponsableBodega", typeof(int));
    
            var bod_DireccionParameter = bod_Direccion != null ?
                new ObjectParameter("bod_Direccion", bod_Direccion) :
                new ObjectParameter("bod_Direccion", typeof(string));
    
            var bod_CorreoParameter = bod_Correo != null ?
                new ObjectParameter("bod_Correo", bod_Correo) :
                new ObjectParameter("bod_Correo", typeof(string));
    
            var bod_TelefonoParameter = bod_Telefono != null ?
                new ObjectParameter("bod_Telefono", bod_Telefono) :
                new ObjectParameter("bod_Telefono", typeof(string));
    
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            var bod_UsuarioCreaParameter = bod_UsuarioCrea.HasValue ?
                new ObjectParameter("bod_UsuarioCrea", bod_UsuarioCrea) :
                new ObjectParameter("bod_UsuarioCrea", typeof(int));
    
            var bod_FechacreaParameter = bod_Fechacrea.HasValue ?
                new ObjectParameter("bod_Fechacrea", bod_Fechacrea) :
                new ObjectParameter("bod_Fechacrea", typeof(System.DateTime));
    
            var bod_UsuarioModificaParameter = bod_UsuarioModifica.HasValue ?
                new ObjectParameter("bod_UsuarioModifica", bod_UsuarioModifica) :
                new ObjectParameter("bod_UsuarioModifica", typeof(int));
    
            var bod_FechaModificaParameter = bod_FechaModifica.HasValue ?
                new ObjectParameter("bod_FechaModifica", bod_FechaModifica) :
                new ObjectParameter("bod_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbBodega_Update_Result>("UDP_Inv_tbBodega_Update", bod_IdParameter, bod_NombreParameter, bod_ResponsableBodegaParameter, bod_DireccionParameter, bod_CorreoParameter, bod_TelefonoParameter, mun_CodigoParameter, bod_UsuarioCreaParameter, bod_FechacreaParameter, bod_UsuarioModificaParameter, bod_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbBodega_Update_Estado_Result> UDP_Inv_tbBodega_Update_Estado(Nullable<int> bod_Id, Nullable<byte> bod_EsActiva, Nullable<int> bodd_UsuarioModifica, Nullable<System.DateTime> bodd_FechaModifica)
        {
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var bod_EsActivaParameter = bod_EsActiva.HasValue ?
                new ObjectParameter("bod_EsActiva", bod_EsActiva) :
                new ObjectParameter("bod_EsActiva", typeof(byte));
    
            var bodd_UsuarioModificaParameter = bodd_UsuarioModifica.HasValue ?
                new ObjectParameter("bodd_UsuarioModifica", bodd_UsuarioModifica) :
                new ObjectParameter("bodd_UsuarioModifica", typeof(int));
    
            var bodd_FechaModificaParameter = bodd_FechaModifica.HasValue ?
                new ObjectParameter("bodd_FechaModifica", bodd_FechaModifica) :
                new ObjectParameter("bodd_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbBodega_Update_Estado_Result>("UDP_Inv_tbBodega_Update_Estado", bod_IdParameter, bod_EsActivaParameter, bodd_UsuarioModificaParameter, bodd_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbBodega_Update_Estado_Validacion_Result> UDP_Inv_tbBodega_Update_Estado_Validacion(Nullable<int> bod_Id, Nullable<byte> bod_EsActiva, Nullable<int> bodd_UsuarioModifica, Nullable<System.DateTime> bodd_FechaModifica)
        {
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var bod_EsActivaParameter = bod_EsActiva.HasValue ?
                new ObjectParameter("bod_EsActiva", bod_EsActiva) :
                new ObjectParameter("bod_EsActiva", typeof(byte));
    
            var bodd_UsuarioModificaParameter = bodd_UsuarioModifica.HasValue ?
                new ObjectParameter("bodd_UsuarioModifica", bodd_UsuarioModifica) :
                new ObjectParameter("bodd_UsuarioModifica", typeof(int));
    
            var bodd_FechaModificaParameter = bodd_FechaModifica.HasValue ?
                new ObjectParameter("bodd_FechaModifica", bodd_FechaModifica) :
                new ObjectParameter("bodd_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbBodega_Update_Estado_Validacion_Result>("UDP_Inv_tbBodega_Update_Estado_Validacion", bod_IdParameter, bod_EsActivaParameter, bodd_UsuarioModificaParameter, bodd_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbBodegaDetalle_Delete_Result> UDP_Inv_tbBodegaDetalle_Delete(Nullable<int> bodd_Id)
        {
            var bodd_IdParameter = bodd_Id.HasValue ?
                new ObjectParameter("bodd_Id", bodd_Id) :
                new ObjectParameter("bodd_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbBodegaDetalle_Delete_Result>("UDP_Inv_tbBodegaDetalle_Delete", bodd_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbBodegaDetalle_Insert_Result> UDP_Inv_tbBodegaDetalle_Insert(string prod_Codigo, Nullable<int> bod_Id, Nullable<decimal> bodd_CantidadMinima, Nullable<decimal> bodd_CantidadMaxima, Nullable<decimal> bodd_PuntoReorden, Nullable<decimal> bodd_Costo, Nullable<decimal> bodd_CostoPromedio, Nullable<int> bodd_UsuarioCrea, Nullable<System.DateTime> bodd_FechaCrea)
        {
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var bodd_CantidadMinimaParameter = bodd_CantidadMinima.HasValue ?
                new ObjectParameter("bodd_CantidadMinima", bodd_CantidadMinima) :
                new ObjectParameter("bodd_CantidadMinima", typeof(decimal));
    
            var bodd_CantidadMaximaParameter = bodd_CantidadMaxima.HasValue ?
                new ObjectParameter("bodd_CantidadMaxima", bodd_CantidadMaxima) :
                new ObjectParameter("bodd_CantidadMaxima", typeof(decimal));
    
            var bodd_PuntoReordenParameter = bodd_PuntoReorden.HasValue ?
                new ObjectParameter("bodd_PuntoReorden", bodd_PuntoReorden) :
                new ObjectParameter("bodd_PuntoReorden", typeof(decimal));
    
            var bodd_CostoParameter = bodd_Costo.HasValue ?
                new ObjectParameter("bodd_Costo", bodd_Costo) :
                new ObjectParameter("bodd_Costo", typeof(decimal));
    
            var bodd_CostoPromedioParameter = bodd_CostoPromedio.HasValue ?
                new ObjectParameter("bodd_CostoPromedio", bodd_CostoPromedio) :
                new ObjectParameter("bodd_CostoPromedio", typeof(decimal));
    
            var bodd_UsuarioCreaParameter = bodd_UsuarioCrea.HasValue ?
                new ObjectParameter("bodd_UsuarioCrea", bodd_UsuarioCrea) :
                new ObjectParameter("bodd_UsuarioCrea", typeof(int));
    
            var bodd_FechaCreaParameter = bodd_FechaCrea.HasValue ?
                new ObjectParameter("bodd_FechaCrea", bodd_FechaCrea) :
                new ObjectParameter("bodd_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbBodegaDetalle_Insert_Result>("UDP_Inv_tbBodegaDetalle_Insert", prod_CodigoParameter, bod_IdParameter, bodd_CantidadMinimaParameter, bodd_CantidadMaximaParameter, bodd_PuntoReordenParameter, bodd_CostoParameter, bodd_CostoPromedioParameter, bodd_UsuarioCreaParameter, bodd_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbBodegaDetalle_Update_Result> UDP_Inv_tbBodegaDetalle_Update(Nullable<int> bodd_Id, string prod_Codigo, Nullable<int> bod_Id, Nullable<decimal> bodd_CantidadMinima, Nullable<decimal> bodd_CantidadMaxima, Nullable<decimal> bodd_PuntoReorden, Nullable<int> bodd_UsuarioCrea, Nullable<System.DateTime> bodd_FechaCrea, Nullable<int> bodd_UsuarioModifica, Nullable<System.DateTime> bodd_FechaModifica, Nullable<decimal> bodd_Costo, Nullable<decimal> bodd_CostoPromedio)
        {
            var bodd_IdParameter = bodd_Id.HasValue ?
                new ObjectParameter("bodd_Id", bodd_Id) :
                new ObjectParameter("bodd_Id", typeof(int));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var bodd_CantidadMinimaParameter = bodd_CantidadMinima.HasValue ?
                new ObjectParameter("bodd_CantidadMinima", bodd_CantidadMinima) :
                new ObjectParameter("bodd_CantidadMinima", typeof(decimal));
    
            var bodd_CantidadMaximaParameter = bodd_CantidadMaxima.HasValue ?
                new ObjectParameter("bodd_CantidadMaxima", bodd_CantidadMaxima) :
                new ObjectParameter("bodd_CantidadMaxima", typeof(decimal));
    
            var bodd_PuntoReordenParameter = bodd_PuntoReorden.HasValue ?
                new ObjectParameter("bodd_PuntoReorden", bodd_PuntoReorden) :
                new ObjectParameter("bodd_PuntoReorden", typeof(decimal));
    
            var bodd_UsuarioCreaParameter = bodd_UsuarioCrea.HasValue ?
                new ObjectParameter("bodd_UsuarioCrea", bodd_UsuarioCrea) :
                new ObjectParameter("bodd_UsuarioCrea", typeof(int));
    
            var bodd_FechaCreaParameter = bodd_FechaCrea.HasValue ?
                new ObjectParameter("bodd_FechaCrea", bodd_FechaCrea) :
                new ObjectParameter("bodd_FechaCrea", typeof(System.DateTime));
    
            var bodd_UsuarioModificaParameter = bodd_UsuarioModifica.HasValue ?
                new ObjectParameter("bodd_UsuarioModifica", bodd_UsuarioModifica) :
                new ObjectParameter("bodd_UsuarioModifica", typeof(int));
    
            var bodd_FechaModificaParameter = bodd_FechaModifica.HasValue ?
                new ObjectParameter("bodd_FechaModifica", bodd_FechaModifica) :
                new ObjectParameter("bodd_FechaModifica", typeof(System.DateTime));
    
            var bodd_CostoParameter = bodd_Costo.HasValue ?
                new ObjectParameter("bodd_Costo", bodd_Costo) :
                new ObjectParameter("bodd_Costo", typeof(decimal));
    
            var bodd_CostoPromedioParameter = bodd_CostoPromedio.HasValue ?
                new ObjectParameter("bodd_CostoPromedio", bodd_CostoPromedio) :
                new ObjectParameter("bodd_CostoPromedio", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbBodegaDetalle_Update_Result>("UDP_Inv_tbBodegaDetalle_Update", bodd_IdParameter, prod_CodigoParameter, bod_IdParameter, bodd_CantidadMinimaParameter, bodd_CantidadMaximaParameter, bodd_PuntoReordenParameter, bodd_UsuarioCreaParameter, bodd_FechaCreaParameter, bodd_UsuarioModificaParameter, bodd_FechaModificaParameter, bodd_CostoParameter, bodd_CostoPromedioParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbBox_Delete_Result> UDP_Inv_tbBox_Delete(string box_Codigo)
        {
            var box_CodigoParameter = box_Codigo != null ?
                new ObjectParameter("box_Codigo", box_Codigo) :
                new ObjectParameter("box_Codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbBox_Delete_Result>("UDP_Inv_tbBox_Delete", box_CodigoParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbBox_Insert_Result> UDP_Inv_tbBox_Insert(string box_Codigo, string box_Descripcion, Nullable<int> bod_Id, Nullable<int> box_UsuarioCrea, Nullable<System.DateTime> box_FechaCrea)
        {
            var box_CodigoParameter = box_Codigo != null ?
                new ObjectParameter("box_Codigo", box_Codigo) :
                new ObjectParameter("box_Codigo", typeof(string));
    
            var box_DescripcionParameter = box_Descripcion != null ?
                new ObjectParameter("box_Descripcion", box_Descripcion) :
                new ObjectParameter("box_Descripcion", typeof(string));
    
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var box_UsuarioCreaParameter = box_UsuarioCrea.HasValue ?
                new ObjectParameter("box_UsuarioCrea", box_UsuarioCrea) :
                new ObjectParameter("box_UsuarioCrea", typeof(int));
    
            var box_FechaCreaParameter = box_FechaCrea.HasValue ?
                new ObjectParameter("box_FechaCrea", box_FechaCrea) :
                new ObjectParameter("box_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbBox_Insert_Result>("UDP_Inv_tbBox_Insert", box_CodigoParameter, box_DescripcionParameter, bod_IdParameter, box_UsuarioCreaParameter, box_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbBox_Update_Result> UDP_Inv_tbBox_Update(string box_Codigo, string box_Descripcion, Nullable<int> bod_Id, Nullable<byte> box_Estado, Nullable<int> box_UsuarioCrea, Nullable<System.DateTime> box_FechaCrea, Nullable<int> box_UsuarioModifica, Nullable<System.DateTime> box_FechaModifica)
        {
            var box_CodigoParameter = box_Codigo != null ?
                new ObjectParameter("box_Codigo", box_Codigo) :
                new ObjectParameter("box_Codigo", typeof(string));
    
            var box_DescripcionParameter = box_Descripcion != null ?
                new ObjectParameter("box_Descripcion", box_Descripcion) :
                new ObjectParameter("box_Descripcion", typeof(string));
    
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var box_EstadoParameter = box_Estado.HasValue ?
                new ObjectParameter("box_Estado", box_Estado) :
                new ObjectParameter("box_Estado", typeof(byte));
    
            var box_UsuarioCreaParameter = box_UsuarioCrea.HasValue ?
                new ObjectParameter("box_UsuarioCrea", box_UsuarioCrea) :
                new ObjectParameter("box_UsuarioCrea", typeof(int));
    
            var box_FechaCreaParameter = box_FechaCrea.HasValue ?
                new ObjectParameter("box_FechaCrea", box_FechaCrea) :
                new ObjectParameter("box_FechaCrea", typeof(System.DateTime));
    
            var box_UsuarioModificaParameter = box_UsuarioModifica.HasValue ?
                new ObjectParameter("box_UsuarioModifica", box_UsuarioModifica) :
                new ObjectParameter("box_UsuarioModifica", typeof(int));
    
            var box_FechaModificaParameter = box_FechaModifica.HasValue ?
                new ObjectParameter("box_FechaModifica", box_FechaModifica) :
                new ObjectParameter("box_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbBox_Update_Result>("UDP_Inv_tbBox_Update", box_CodigoParameter, box_DescripcionParameter, bod_IdParameter, box_EstadoParameter, box_UsuarioCreaParameter, box_FechaCreaParameter, box_UsuarioModificaParameter, box_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbBoxDetalle_Delete_Result> UDP_Inv_tbBoxDetalle_Delete(Nullable<int> boxd_Id)
        {
            var boxd_IdParameter = boxd_Id.HasValue ?
                new ObjectParameter("boxd_Id", boxd_Id) :
                new ObjectParameter("boxd_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbBoxDetalle_Delete_Result>("UDP_Inv_tbBoxDetalle_Delete", boxd_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbBoxDetalle_Insert_Result> UDP_Inv_tbBoxDetalle_Insert(string box_Codigo, string prod_Codigo, Nullable<decimal> boxd_Cantidad, Nullable<int> boxd_UsuarioCrea, Nullable<System.DateTime> boxd_FechaCrea)
        {
            var box_CodigoParameter = box_Codigo != null ?
                new ObjectParameter("box_Codigo", box_Codigo) :
                new ObjectParameter("box_Codigo", typeof(string));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var boxd_CantidadParameter = boxd_Cantidad.HasValue ?
                new ObjectParameter("boxd_Cantidad", boxd_Cantidad) :
                new ObjectParameter("boxd_Cantidad", typeof(decimal));
    
            var boxd_UsuarioCreaParameter = boxd_UsuarioCrea.HasValue ?
                new ObjectParameter("boxd_UsuarioCrea", boxd_UsuarioCrea) :
                new ObjectParameter("boxd_UsuarioCrea", typeof(int));
    
            var boxd_FechaCreaParameter = boxd_FechaCrea.HasValue ?
                new ObjectParameter("boxd_FechaCrea", boxd_FechaCrea) :
                new ObjectParameter("boxd_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbBoxDetalle_Insert_Result>("UDP_Inv_tbBoxDetalle_Insert", box_CodigoParameter, prod_CodigoParameter, boxd_CantidadParameter, boxd_UsuarioCreaParameter, boxd_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbBoxDetalle_Update_Result> UDP_Inv_tbBoxDetalle_Update(Nullable<int> boxd_Id, string box_Codigo, string prod_Codigo, Nullable<decimal> boxd_Cantidad, Nullable<int> boxd_UsuarioCrea, Nullable<System.DateTime> boxd_FechaCrea, Nullable<int> boxd_UsuarioModifica, Nullable<System.DateTime> boxd_FechaModifica)
        {
            var boxd_IdParameter = boxd_Id.HasValue ?
                new ObjectParameter("boxd_Id", boxd_Id) :
                new ObjectParameter("boxd_Id", typeof(int));
    
            var box_CodigoParameter = box_Codigo != null ?
                new ObjectParameter("box_Codigo", box_Codigo) :
                new ObjectParameter("box_Codigo", typeof(string));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var boxd_CantidadParameter = boxd_Cantidad.HasValue ?
                new ObjectParameter("boxd_Cantidad", boxd_Cantidad) :
                new ObjectParameter("boxd_Cantidad", typeof(decimal));
    
            var boxd_UsuarioCreaParameter = boxd_UsuarioCrea.HasValue ?
                new ObjectParameter("boxd_UsuarioCrea", boxd_UsuarioCrea) :
                new ObjectParameter("boxd_UsuarioCrea", typeof(int));
    
            var boxd_FechaCreaParameter = boxd_FechaCrea.HasValue ?
                new ObjectParameter("boxd_FechaCrea", boxd_FechaCrea) :
                new ObjectParameter("boxd_FechaCrea", typeof(System.DateTime));
    
            var boxd_UsuarioModificaParameter = boxd_UsuarioModifica.HasValue ?
                new ObjectParameter("boxd_UsuarioModifica", boxd_UsuarioModifica) :
                new ObjectParameter("boxd_UsuarioModifica", typeof(int));
    
            var boxd_FechaModificaParameter = boxd_FechaModifica.HasValue ?
                new ObjectParameter("boxd_FechaModifica", boxd_FechaModifica) :
                new ObjectParameter("boxd_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbBoxDetalle_Update_Result>("UDP_Inv_tbBoxDetalle_Update", boxd_IdParameter, box_CodigoParameter, prod_CodigoParameter, boxd_CantidadParameter, boxd_UsuarioCreaParameter, boxd_FechaCreaParameter, boxd_UsuarioModificaParameter, boxd_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbEntrada_Insert_Result> UDP_Inv_tbEntrada_Insert(Nullable<System.DateTime> ent_FechaElaboracion, Nullable<int> bod_Id, Nullable<byte> estm_Id, Nullable<int> prov_Id, string ent_FacturaCompra, Nullable<System.DateTime> ent_FechaCompra, string fact_Id, Nullable<int> ent_RazonDevolucion, Nullable<int> ent_BodegaDestino, Nullable<byte> tent_Id, Nullable<int> ent_UsuarioCrea, Nullable<System.DateTime> ent_FechaCrea)
        {
            var ent_FechaElaboracionParameter = ent_FechaElaboracion.HasValue ?
                new ObjectParameter("ent_FechaElaboracion", ent_FechaElaboracion) :
                new ObjectParameter("ent_FechaElaboracion", typeof(System.DateTime));
    
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var estm_IdParameter = estm_Id.HasValue ?
                new ObjectParameter("estm_Id", estm_Id) :
                new ObjectParameter("estm_Id", typeof(byte));
    
            var prov_IdParameter = prov_Id.HasValue ?
                new ObjectParameter("prov_Id", prov_Id) :
                new ObjectParameter("prov_Id", typeof(int));
    
            var ent_FacturaCompraParameter = ent_FacturaCompra != null ?
                new ObjectParameter("ent_FacturaCompra", ent_FacturaCompra) :
                new ObjectParameter("ent_FacturaCompra", typeof(string));
    
            var ent_FechaCompraParameter = ent_FechaCompra.HasValue ?
                new ObjectParameter("ent_FechaCompra", ent_FechaCompra) :
                new ObjectParameter("ent_FechaCompra", typeof(System.DateTime));
    
            var fact_IdParameter = fact_Id != null ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(string));
    
            var ent_RazonDevolucionParameter = ent_RazonDevolucion.HasValue ?
                new ObjectParameter("ent_RazonDevolucion", ent_RazonDevolucion) :
                new ObjectParameter("ent_RazonDevolucion", typeof(int));
    
            var ent_BodegaDestinoParameter = ent_BodegaDestino.HasValue ?
                new ObjectParameter("ent_BodegaDestino", ent_BodegaDestino) :
                new ObjectParameter("ent_BodegaDestino", typeof(int));
    
            var tent_IdParameter = tent_Id.HasValue ?
                new ObjectParameter("tent_Id", tent_Id) :
                new ObjectParameter("tent_Id", typeof(byte));
    
            var ent_UsuarioCreaParameter = ent_UsuarioCrea.HasValue ?
                new ObjectParameter("ent_UsuarioCrea", ent_UsuarioCrea) :
                new ObjectParameter("ent_UsuarioCrea", typeof(int));
    
            var ent_FechaCreaParameter = ent_FechaCrea.HasValue ?
                new ObjectParameter("ent_FechaCrea", ent_FechaCrea) :
                new ObjectParameter("ent_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbEntrada_Insert_Result>("UDP_Inv_tbEntrada_Insert", ent_FechaElaboracionParameter, bod_IdParameter, estm_IdParameter, prov_IdParameter, ent_FacturaCompraParameter, ent_FechaCompraParameter, fact_IdParameter, ent_RazonDevolucionParameter, ent_BodegaDestinoParameter, tent_IdParameter, ent_UsuarioCreaParameter, ent_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbEntrada_Update_Result> UDP_Inv_tbEntrada_Update(Nullable<int> ent_Id, string ent_NumeroFormato, Nullable<System.DateTime> ent_FechaElaboracion, Nullable<int> bod_Id, Nullable<int> prov_Id, string ent_FacturaCompra, Nullable<System.DateTime> ent_FechaCompra, string fact_Id, Nullable<int> ent_RazonDevolucion, Nullable<int> ent_BodegaDestino, Nullable<byte> tent_Id, Nullable<int> ent_UsuarioCrea, Nullable<System.DateTime> ent_FechaCrea, Nullable<int> ent_UsuarioModifica, Nullable<System.DateTime> ent_FechaModifica)
        {
            var ent_IdParameter = ent_Id.HasValue ?
                new ObjectParameter("ent_Id", ent_Id) :
                new ObjectParameter("ent_Id", typeof(int));
    
            var ent_NumeroFormatoParameter = ent_NumeroFormato != null ?
                new ObjectParameter("ent_NumeroFormato", ent_NumeroFormato) :
                new ObjectParameter("ent_NumeroFormato", typeof(string));
    
            var ent_FechaElaboracionParameter = ent_FechaElaboracion.HasValue ?
                new ObjectParameter("ent_FechaElaboracion", ent_FechaElaboracion) :
                new ObjectParameter("ent_FechaElaboracion", typeof(System.DateTime));
    
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var prov_IdParameter = prov_Id.HasValue ?
                new ObjectParameter("prov_Id", prov_Id) :
                new ObjectParameter("prov_Id", typeof(int));
    
            var ent_FacturaCompraParameter = ent_FacturaCompra != null ?
                new ObjectParameter("ent_FacturaCompra", ent_FacturaCompra) :
                new ObjectParameter("ent_FacturaCompra", typeof(string));
    
            var ent_FechaCompraParameter = ent_FechaCompra.HasValue ?
                new ObjectParameter("ent_FechaCompra", ent_FechaCompra) :
                new ObjectParameter("ent_FechaCompra", typeof(System.DateTime));
    
            var fact_IdParameter = fact_Id != null ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(string));
    
            var ent_RazonDevolucionParameter = ent_RazonDevolucion.HasValue ?
                new ObjectParameter("ent_RazonDevolucion", ent_RazonDevolucion) :
                new ObjectParameter("ent_RazonDevolucion", typeof(int));
    
            var ent_BodegaDestinoParameter = ent_BodegaDestino.HasValue ?
                new ObjectParameter("ent_BodegaDestino", ent_BodegaDestino) :
                new ObjectParameter("ent_BodegaDestino", typeof(int));
    
            var tent_IdParameter = tent_Id.HasValue ?
                new ObjectParameter("tent_Id", tent_Id) :
                new ObjectParameter("tent_Id", typeof(byte));
    
            var ent_UsuarioCreaParameter = ent_UsuarioCrea.HasValue ?
                new ObjectParameter("ent_UsuarioCrea", ent_UsuarioCrea) :
                new ObjectParameter("ent_UsuarioCrea", typeof(int));
    
            var ent_FechaCreaParameter = ent_FechaCrea.HasValue ?
                new ObjectParameter("ent_FechaCrea", ent_FechaCrea) :
                new ObjectParameter("ent_FechaCrea", typeof(System.DateTime));
    
            var ent_UsuarioModificaParameter = ent_UsuarioModifica.HasValue ?
                new ObjectParameter("ent_UsuarioModifica", ent_UsuarioModifica) :
                new ObjectParameter("ent_UsuarioModifica", typeof(int));
    
            var ent_FechaModificaParameter = ent_FechaModifica.HasValue ?
                new ObjectParameter("ent_FechaModifica", ent_FechaModifica) :
                new ObjectParameter("ent_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbEntrada_Update_Result>("UDP_Inv_tbEntrada_Update", ent_IdParameter, ent_NumeroFormatoParameter, ent_FechaElaboracionParameter, bod_IdParameter, prov_IdParameter, ent_FacturaCompraParameter, ent_FechaCompraParameter, fact_IdParameter, ent_RazonDevolucionParameter, ent_BodegaDestinoParameter, tent_IdParameter, ent_UsuarioCreaParameter, ent_FechaCreaParameter, ent_UsuarioModificaParameter, ent_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbEntrada_Update_Anular_Result> UDP_Inv_tbEntrada_Update_Anular(Nullable<int> ent_Id, Nullable<byte> entd_EsAnulada, string entd_RazonAnulada, Nullable<int> entd_UsuarioModifica, Nullable<System.DateTime> entd_FechaModifica)
        {
            var ent_IdParameter = ent_Id.HasValue ?
                new ObjectParameter("ent_Id", ent_Id) :
                new ObjectParameter("ent_Id", typeof(int));
    
            var entd_EsAnuladaParameter = entd_EsAnulada.HasValue ?
                new ObjectParameter("entd_EsAnulada", entd_EsAnulada) :
                new ObjectParameter("entd_EsAnulada", typeof(byte));
    
            var entd_RazonAnuladaParameter = entd_RazonAnulada != null ?
                new ObjectParameter("entd_RazonAnulada", entd_RazonAnulada) :
                new ObjectParameter("entd_RazonAnulada", typeof(string));
    
            var entd_UsuarioModificaParameter = entd_UsuarioModifica.HasValue ?
                new ObjectParameter("entd_UsuarioModifica", entd_UsuarioModifica) :
                new ObjectParameter("entd_UsuarioModifica", typeof(int));
    
            var entd_FechaModificaParameter = entd_FechaModifica.HasValue ?
                new ObjectParameter("entd_FechaModifica", entd_FechaModifica) :
                new ObjectParameter("entd_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbEntrada_Update_Anular_Result>("UDP_Inv_tbEntrada_Update_Anular", ent_IdParameter, entd_EsAnuladaParameter, entd_RazonAnuladaParameter, entd_UsuarioModificaParameter, entd_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbEntrada_Update_Estado_Result> UDP_Inv_tbEntrada_Update_Estado(Nullable<int> ent_Id, Nullable<byte> estm_Id, Nullable<int> entd_UsuarioModifica, Nullable<System.DateTime> entd_FechaModifica)
        {
            var ent_IdParameter = ent_Id.HasValue ?
                new ObjectParameter("ent_Id", ent_Id) :
                new ObjectParameter("ent_Id", typeof(int));
    
            var estm_IdParameter = estm_Id.HasValue ?
                new ObjectParameter("estm_Id", estm_Id) :
                new ObjectParameter("estm_Id", typeof(byte));
    
            var entd_UsuarioModificaParameter = entd_UsuarioModifica.HasValue ?
                new ObjectParameter("entd_UsuarioModifica", entd_UsuarioModifica) :
                new ObjectParameter("entd_UsuarioModifica", typeof(int));
    
            var entd_FechaModificaParameter = entd_FechaModifica.HasValue ?
                new ObjectParameter("entd_FechaModifica", entd_FechaModifica) :
                new ObjectParameter("entd_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbEntrada_Update_Estado_Result>("UDP_Inv_tbEntrada_Update_Estado", ent_IdParameter, estm_IdParameter, entd_UsuarioModificaParameter, entd_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbEntradaDetalle_Insert_Result> UDP_Inv_tbEntradaDetalle_Insert(Nullable<int> ent_Id, string prod_Codigo, Nullable<decimal> entd_Cantidad, string box_Codigo, Nullable<int> ent_UsuarioCrea, Nullable<System.DateTime> ent_FechaCrea)
        {
            var ent_IdParameter = ent_Id.HasValue ?
                new ObjectParameter("ent_Id", ent_Id) :
                new ObjectParameter("ent_Id", typeof(int));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var entd_CantidadParameter = entd_Cantidad.HasValue ?
                new ObjectParameter("entd_Cantidad", entd_Cantidad) :
                new ObjectParameter("entd_Cantidad", typeof(decimal));
    
            var box_CodigoParameter = box_Codigo != null ?
                new ObjectParameter("box_Codigo", box_Codigo) :
                new ObjectParameter("box_Codigo", typeof(string));
    
            var ent_UsuarioCreaParameter = ent_UsuarioCrea.HasValue ?
                new ObjectParameter("ent_UsuarioCrea", ent_UsuarioCrea) :
                new ObjectParameter("ent_UsuarioCrea", typeof(int));
    
            var ent_FechaCreaParameter = ent_FechaCrea.HasValue ?
                new ObjectParameter("ent_FechaCrea", ent_FechaCrea) :
                new ObjectParameter("ent_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbEntradaDetalle_Insert_Result>("UDP_Inv_tbEntradaDetalle_Insert", ent_IdParameter, prod_CodigoParameter, entd_CantidadParameter, box_CodigoParameter, ent_UsuarioCreaParameter, ent_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbEntradaDetalle_Update_Result> UDP_Inv_tbEntradaDetalle_Update(Nullable<int> entd_Id, Nullable<int> ent_Id, string prod_Codigo, Nullable<decimal> entd_Cantidad, string box_Codigo, Nullable<int> entd_UsuarioCrea, Nullable<System.DateTime> entd_FechaCrea, Nullable<int> entd_UsuarioModifica, Nullable<System.DateTime> entd_FechaModifica)
        {
            var entd_IdParameter = entd_Id.HasValue ?
                new ObjectParameter("entd_Id", entd_Id) :
                new ObjectParameter("entd_Id", typeof(int));
    
            var ent_IdParameter = ent_Id.HasValue ?
                new ObjectParameter("ent_Id", ent_Id) :
                new ObjectParameter("ent_Id", typeof(int));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var entd_CantidadParameter = entd_Cantidad.HasValue ?
                new ObjectParameter("entd_Cantidad", entd_Cantidad) :
                new ObjectParameter("entd_Cantidad", typeof(decimal));
    
            var box_CodigoParameter = box_Codigo != null ?
                new ObjectParameter("box_Codigo", box_Codigo) :
                new ObjectParameter("box_Codigo", typeof(string));
    
            var entd_UsuarioCreaParameter = entd_UsuarioCrea.HasValue ?
                new ObjectParameter("entd_UsuarioCrea", entd_UsuarioCrea) :
                new ObjectParameter("entd_UsuarioCrea", typeof(int));
    
            var entd_FechaCreaParameter = entd_FechaCrea.HasValue ?
                new ObjectParameter("entd_FechaCrea", entd_FechaCrea) :
                new ObjectParameter("entd_FechaCrea", typeof(System.DateTime));
    
            var entd_UsuarioModificaParameter = entd_UsuarioModifica.HasValue ?
                new ObjectParameter("entd_UsuarioModifica", entd_UsuarioModifica) :
                new ObjectParameter("entd_UsuarioModifica", typeof(int));
    
            var entd_FechaModificaParameter = entd_FechaModifica.HasValue ?
                new ObjectParameter("entd_FechaModifica", entd_FechaModifica) :
                new ObjectParameter("entd_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbEntradaDetalle_Update_Result>("UDP_Inv_tbEntradaDetalle_Update", entd_IdParameter, ent_IdParameter, prod_CodigoParameter, entd_CantidadParameter, box_CodigoParameter, entd_UsuarioCreaParameter, entd_FechaCreaParameter, entd_UsuarioModificaParameter, entd_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbEstadoInventarioFisico_Insert_Result> UDP_Inv_tbEstadoInventarioFisico_Insert(Nullable<byte> estif_Id, string estif_Descripcion, Nullable<int> estif_UsuarioCrea, Nullable<System.DateTime> estif_FechaCrea)
        {
            var estif_IdParameter = estif_Id.HasValue ?
                new ObjectParameter("estif_Id", estif_Id) :
                new ObjectParameter("estif_Id", typeof(byte));
    
            var estif_DescripcionParameter = estif_Descripcion != null ?
                new ObjectParameter("estif_Descripcion", estif_Descripcion) :
                new ObjectParameter("estif_Descripcion", typeof(string));
    
            var estif_UsuarioCreaParameter = estif_UsuarioCrea.HasValue ?
                new ObjectParameter("estif_UsuarioCrea", estif_UsuarioCrea) :
                new ObjectParameter("estif_UsuarioCrea", typeof(int));
    
            var estif_FechaCreaParameter = estif_FechaCrea.HasValue ?
                new ObjectParameter("estif_FechaCrea", estif_FechaCrea) :
                new ObjectParameter("estif_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbEstadoInventarioFisico_Insert_Result>("UDP_Inv_tbEstadoInventarioFisico_Insert", estif_IdParameter, estif_DescripcionParameter, estif_UsuarioCreaParameter, estif_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbEstadoInventarioFisico_Update_Result> UDP_Inv_tbEstadoInventarioFisico_Update(Nullable<byte> estif_Id, string estif_Descripcion, Nullable<int> estif_UsuarioCrea, Nullable<System.DateTime> estif_FechaCrea)
        {
            var estif_IdParameter = estif_Id.HasValue ?
                new ObjectParameter("estif_Id", estif_Id) :
                new ObjectParameter("estif_Id", typeof(byte));
    
            var estif_DescripcionParameter = estif_Descripcion != null ?
                new ObjectParameter("estif_Descripcion", estif_Descripcion) :
                new ObjectParameter("estif_Descripcion", typeof(string));
    
            var estif_UsuarioCreaParameter = estif_UsuarioCrea.HasValue ?
                new ObjectParameter("estif_UsuarioCrea", estif_UsuarioCrea) :
                new ObjectParameter("estif_UsuarioCrea", typeof(int));
    
            var estif_FechaCreaParameter = estif_FechaCrea.HasValue ?
                new ObjectParameter("estif_FechaCrea", estif_FechaCrea) :
                new ObjectParameter("estif_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbEstadoInventarioFisico_Update_Result>("UDP_Inv_tbEstadoInventarioFisico_Update", estif_IdParameter, estif_DescripcionParameter, estif_UsuarioCreaParameter, estif_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbEstadoMovimiento_Delete_Result> UDP_Inv_tbEstadoMovimiento_Delete(Nullable<int> estm_Id)
        {
            var estm_IdParameter = estm_Id.HasValue ?
                new ObjectParameter("estm_Id", estm_Id) :
                new ObjectParameter("estm_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbEstadoMovimiento_Delete_Result>("UDP_Inv_tbEstadoMovimiento_Delete", estm_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbEstadoMovimiento_Insert_Result> UDP_Inv_tbEstadoMovimiento_Insert(Nullable<byte> estm_Id, string estm_Descripcion, Nullable<int> estm_UsuarioCrea, Nullable<System.DateTime> estm_FechaCrea)
        {
            var estm_IdParameter = estm_Id.HasValue ?
                new ObjectParameter("estm_Id", estm_Id) :
                new ObjectParameter("estm_Id", typeof(byte));
    
            var estm_DescripcionParameter = estm_Descripcion != null ?
                new ObjectParameter("estm_Descripcion", estm_Descripcion) :
                new ObjectParameter("estm_Descripcion", typeof(string));
    
            var estm_UsuarioCreaParameter = estm_UsuarioCrea.HasValue ?
                new ObjectParameter("estm_UsuarioCrea", estm_UsuarioCrea) :
                new ObjectParameter("estm_UsuarioCrea", typeof(int));
    
            var estm_FechaCreaParameter = estm_FechaCrea.HasValue ?
                new ObjectParameter("estm_FechaCrea", estm_FechaCrea) :
                new ObjectParameter("estm_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbEstadoMovimiento_Insert_Result>("UDP_Inv_tbEstadoMovimiento_Insert", estm_IdParameter, estm_DescripcionParameter, estm_UsuarioCreaParameter, estm_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbEstadoMovimiento_Update_Result> UDP_Inv_tbEstadoMovimiento_Update(Nullable<byte> estm_Id, string estm_Descripcion, Nullable<int> estm_UsuarioCrea, Nullable<System.DateTime> estm_FechaCrea)
        {
            var estm_IdParameter = estm_Id.HasValue ?
                new ObjectParameter("estm_Id", estm_Id) :
                new ObjectParameter("estm_Id", typeof(byte));
    
            var estm_DescripcionParameter = estm_Descripcion != null ?
                new ObjectParameter("estm_Descripcion", estm_Descripcion) :
                new ObjectParameter("estm_Descripcion", typeof(string));
    
            var estm_UsuarioCreaParameter = estm_UsuarioCrea.HasValue ?
                new ObjectParameter("estm_UsuarioCrea", estm_UsuarioCrea) :
                new ObjectParameter("estm_UsuarioCrea", typeof(int));
    
            var estm_FechaCreaParameter = estm_FechaCrea.HasValue ?
                new ObjectParameter("estm_FechaCrea", estm_FechaCrea) :
                new ObjectParameter("estm_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbEstadoMovimiento_Update_Result>("UDP_Inv_tbEstadoMovimiento_Update", estm_IdParameter, estm_DescripcionParameter, estm_UsuarioCreaParameter, estm_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbInventarioFisico_Insert_Result> UDP_Inv_tbInventarioFisico_Insert(string invf_Descripcion, string invf_ResponsableBodega, Nullable<int> bod_Id, Nullable<byte> estif_Id, Nullable<System.DateTime> invf_FechaInventario, Nullable<int> invf_UsuarioCrea, Nullable<System.DateTime> invf_FechaCrea)
        {
            var invf_DescripcionParameter = invf_Descripcion != null ?
                new ObjectParameter("invf_Descripcion", invf_Descripcion) :
                new ObjectParameter("invf_Descripcion", typeof(string));
    
            var invf_ResponsableBodegaParameter = invf_ResponsableBodega != null ?
                new ObjectParameter("invf_ResponsableBodega", invf_ResponsableBodega) :
                new ObjectParameter("invf_ResponsableBodega", typeof(string));
    
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var estif_IdParameter = estif_Id.HasValue ?
                new ObjectParameter("estif_Id", estif_Id) :
                new ObjectParameter("estif_Id", typeof(byte));
    
            var invf_FechaInventarioParameter = invf_FechaInventario.HasValue ?
                new ObjectParameter("invf_FechaInventario", invf_FechaInventario) :
                new ObjectParameter("invf_FechaInventario", typeof(System.DateTime));
    
            var invf_UsuarioCreaParameter = invf_UsuarioCrea.HasValue ?
                new ObjectParameter("invf_UsuarioCrea", invf_UsuarioCrea) :
                new ObjectParameter("invf_UsuarioCrea", typeof(int));
    
            var invf_FechaCreaParameter = invf_FechaCrea.HasValue ?
                new ObjectParameter("invf_FechaCrea", invf_FechaCrea) :
                new ObjectParameter("invf_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbInventarioFisico_Insert_Result>("UDP_Inv_tbInventarioFisico_Insert", invf_DescripcionParameter, invf_ResponsableBodegaParameter, bod_IdParameter, estif_IdParameter, invf_FechaInventarioParameter, invf_UsuarioCreaParameter, invf_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbInventarioFisico_ListaProductos_Result> UDP_Inv_tbInventarioFisico_ListaProductos(Nullable<int> bod_Id)
        {
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbInventarioFisico_ListaProductos_Result>("UDP_Inv_tbInventarioFisico_ListaProductos", bod_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbInventarioFisico_ReporteFaltantes_Result> UDP_Inv_tbInventarioFisico_ReporteFaltantes(Nullable<int> bod_Id, Nullable<System.DateTime> fecha1, Nullable<System.DateTime> fecha2, string prod_Codigo)
        {
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var fecha1Parameter = fecha1.HasValue ?
                new ObjectParameter("Fecha1", fecha1) :
                new ObjectParameter("Fecha1", typeof(System.DateTime));
    
            var fecha2Parameter = fecha2.HasValue ?
                new ObjectParameter("Fecha2", fecha2) :
                new ObjectParameter("Fecha2", typeof(System.DateTime));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbInventarioFisico_ReporteFaltantes_Result>("UDP_Inv_tbInventarioFisico_ReporteFaltantes", bod_IdParameter, fecha1Parameter, fecha2Parameter, prod_CodigoParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbInventarioFisico_Update_Result> UDP_Inv_tbInventarioFisico_Update(Nullable<int> invf_Id, string invf_Descripcion, string invf_ResponsableBodega, Nullable<int> bod_Id, Nullable<byte> estif_Id, Nullable<System.DateTime> invf_FechaInventario, Nullable<int> invf_UsuarioCrea, Nullable<System.DateTime> invf_FechaCrea, Nullable<int> invf_UsuarioModifica, Nullable<System.DateTime> invf_FechaModifica)
        {
            var invf_IdParameter = invf_Id.HasValue ?
                new ObjectParameter("invf_Id", invf_Id) :
                new ObjectParameter("invf_Id", typeof(int));
    
            var invf_DescripcionParameter = invf_Descripcion != null ?
                new ObjectParameter("invf_Descripcion", invf_Descripcion) :
                new ObjectParameter("invf_Descripcion", typeof(string));
    
            var invf_ResponsableBodegaParameter = invf_ResponsableBodega != null ?
                new ObjectParameter("invf_ResponsableBodega", invf_ResponsableBodega) :
                new ObjectParameter("invf_ResponsableBodega", typeof(string));
    
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var estif_IdParameter = estif_Id.HasValue ?
                new ObjectParameter("estif_Id", estif_Id) :
                new ObjectParameter("estif_Id", typeof(byte));
    
            var invf_FechaInventarioParameter = invf_FechaInventario.HasValue ?
                new ObjectParameter("invf_FechaInventario", invf_FechaInventario) :
                new ObjectParameter("invf_FechaInventario", typeof(System.DateTime));
    
            var invf_UsuarioCreaParameter = invf_UsuarioCrea.HasValue ?
                new ObjectParameter("invf_UsuarioCrea", invf_UsuarioCrea) :
                new ObjectParameter("invf_UsuarioCrea", typeof(int));
    
            var invf_FechaCreaParameter = invf_FechaCrea.HasValue ?
                new ObjectParameter("invf_FechaCrea", invf_FechaCrea) :
                new ObjectParameter("invf_FechaCrea", typeof(System.DateTime));
    
            var invf_UsuarioModificaParameter = invf_UsuarioModifica.HasValue ?
                new ObjectParameter("invf_UsuarioModifica", invf_UsuarioModifica) :
                new ObjectParameter("invf_UsuarioModifica", typeof(int));
    
            var invf_FechaModificaParameter = invf_FechaModifica.HasValue ?
                new ObjectParameter("invf_FechaModifica", invf_FechaModifica) :
                new ObjectParameter("invf_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbInventarioFisico_Update_Result>("UDP_Inv_tbInventarioFisico_Update", invf_IdParameter, invf_DescripcionParameter, invf_ResponsableBodegaParameter, bod_IdParameter, estif_IdParameter, invf_FechaInventarioParameter, invf_UsuarioCreaParameter, invf_FechaCreaParameter, invf_UsuarioModificaParameter, invf_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbInventarioFisico_Update_Estado_Result> UDP_Inv_tbInventarioFisico_Update_Estado(Nullable<int> invf_Id, Nullable<byte> estif_Id, Nullable<int> invf_UsuarioModifica, Nullable<System.DateTime> invf_FechaModifica)
        {
            var invf_IdParameter = invf_Id.HasValue ?
                new ObjectParameter("invf_Id", invf_Id) :
                new ObjectParameter("invf_Id", typeof(int));
    
            var estif_IdParameter = estif_Id.HasValue ?
                new ObjectParameter("estif_Id", estif_Id) :
                new ObjectParameter("estif_Id", typeof(byte));
    
            var invf_UsuarioModificaParameter = invf_UsuarioModifica.HasValue ?
                new ObjectParameter("invf_UsuarioModifica", invf_UsuarioModifica) :
                new ObjectParameter("invf_UsuarioModifica", typeof(int));
    
            var invf_FechaModificaParameter = invf_FechaModifica.HasValue ?
                new ObjectParameter("invf_FechaModifica", invf_FechaModifica) :
                new ObjectParameter("invf_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbInventarioFisico_Update_Estado_Result>("UDP_Inv_tbInventarioFisico_Update_Estado", invf_IdParameter, estif_IdParameter, invf_UsuarioModificaParameter, invf_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbInventarioFisicoDetalle_Insert_Result> UDP_Inv_tbInventarioFisicoDetalle_Insert(Nullable<int> invf_Id, string prod_Codigo, Nullable<decimal> invfd_Cantidad, Nullable<decimal> invfd_CantidadSistema, Nullable<int> uni_Id, Nullable<int> invfd_UsuarioCrea, Nullable<System.DateTime> invfd_FechaCrea)
        {
            var invf_IdParameter = invf_Id.HasValue ?
                new ObjectParameter("invf_Id", invf_Id) :
                new ObjectParameter("invf_Id", typeof(int));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var invfd_CantidadParameter = invfd_Cantidad.HasValue ?
                new ObjectParameter("invfd_Cantidad", invfd_Cantidad) :
                new ObjectParameter("invfd_Cantidad", typeof(decimal));
    
            var invfd_CantidadSistemaParameter = invfd_CantidadSistema.HasValue ?
                new ObjectParameter("invfd_CantidadSistema", invfd_CantidadSistema) :
                new ObjectParameter("invfd_CantidadSistema", typeof(decimal));
    
            var uni_IdParameter = uni_Id.HasValue ?
                new ObjectParameter("uni_Id", uni_Id) :
                new ObjectParameter("uni_Id", typeof(int));
    
            var invfd_UsuarioCreaParameter = invfd_UsuarioCrea.HasValue ?
                new ObjectParameter("invfd_UsuarioCrea", invfd_UsuarioCrea) :
                new ObjectParameter("invfd_UsuarioCrea", typeof(int));
    
            var invfd_FechaCreaParameter = invfd_FechaCrea.HasValue ?
                new ObjectParameter("invfd_FechaCrea", invfd_FechaCrea) :
                new ObjectParameter("invfd_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbInventarioFisicoDetalle_Insert_Result>("UDP_Inv_tbInventarioFisicoDetalle_Insert", invf_IdParameter, prod_CodigoParameter, invfd_CantidadParameter, invfd_CantidadSistemaParameter, uni_IdParameter, invfd_UsuarioCreaParameter, invfd_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbInventarioFisicoDetalle_Update_Result> UDP_Inv_tbInventarioFisicoDetalle_Update(Nullable<int> invfd_Id, Nullable<int> invf_Id, string prod_Codigo, Nullable<decimal> invfd_Cantidad, Nullable<decimal> invfd_CantidadSistema, Nullable<int> uni_Id, Nullable<int> invfd_UsuarioModifica, Nullable<System.DateTime> invfd_FechaModifica)
        {
            var invfd_IdParameter = invfd_Id.HasValue ?
                new ObjectParameter("invfd_Id", invfd_Id) :
                new ObjectParameter("invfd_Id", typeof(int));
    
            var invf_IdParameter = invf_Id.HasValue ?
                new ObjectParameter("invf_Id", invf_Id) :
                new ObjectParameter("invf_Id", typeof(int));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var invfd_CantidadParameter = invfd_Cantidad.HasValue ?
                new ObjectParameter("invfd_Cantidad", invfd_Cantidad) :
                new ObjectParameter("invfd_Cantidad", typeof(decimal));
    
            var invfd_CantidadSistemaParameter = invfd_CantidadSistema.HasValue ?
                new ObjectParameter("invfd_CantidadSistema", invfd_CantidadSistema) :
                new ObjectParameter("invfd_CantidadSistema", typeof(decimal));
    
            var uni_IdParameter = uni_Id.HasValue ?
                new ObjectParameter("uni_Id", uni_Id) :
                new ObjectParameter("uni_Id", typeof(int));
    
            var invfd_UsuarioModificaParameter = invfd_UsuarioModifica.HasValue ?
                new ObjectParameter("invfd_UsuarioModifica", invfd_UsuarioModifica) :
                new ObjectParameter("invfd_UsuarioModifica", typeof(int));
    
            var invfd_FechaModificaParameter = invfd_FechaModifica.HasValue ?
                new ObjectParameter("invfd_FechaModifica", invfd_FechaModifica) :
                new ObjectParameter("invfd_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbInventarioFisicoDetalle_Update_Result>("UDP_Inv_tbInventarioFisicoDetalle_Update", invfd_IdParameter, invf_IdParameter, prod_CodigoParameter, invfd_CantidadParameter, invfd_CantidadSistemaParameter, uni_IdParameter, invfd_UsuarioModificaParameter, invfd_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProducto_Consulta_Result> UDP_Inv_tbProducto_Consulta(string prod_Codigo)
        {
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProducto_Consulta_Result>("UDP_Inv_tbProducto_Consulta", prod_CodigoParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProducto_Estado_Result> UDP_Inv_tbProducto_Estado(string prod_Codigo, Nullable<bool> prod_EsActivo, string prod_Razon_Inactivacion, Nullable<int> prod_UsuarioModifica, Nullable<System.DateTime> prod_FechaModifica)
        {
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var prod_EsActivoParameter = prod_EsActivo.HasValue ?
                new ObjectParameter("prod_EsActivo", prod_EsActivo) :
                new ObjectParameter("prod_EsActivo", typeof(bool));
    
            var prod_Razon_InactivacionParameter = prod_Razon_Inactivacion != null ?
                new ObjectParameter("prod_Razon_Inactivacion", prod_Razon_Inactivacion) :
                new ObjectParameter("prod_Razon_Inactivacion", typeof(string));
    
            var prod_UsuarioModificaParameter = prod_UsuarioModifica.HasValue ?
                new ObjectParameter("prod_UsuarioModifica", prod_UsuarioModifica) :
                new ObjectParameter("prod_UsuarioModifica", typeof(int));
    
            var prod_FechaModificaParameter = prod_FechaModifica.HasValue ?
                new ObjectParameter("prod_FechaModifica", prod_FechaModifica) :
                new ObjectParameter("prod_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProducto_Estado_Result>("UDP_Inv_tbProducto_Estado", prod_CodigoParameter, prod_EsActivoParameter, prod_Razon_InactivacionParameter, prod_UsuarioModificaParameter, prod_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProducto_Estado_Prueba_Result> UDP_Inv_tbProducto_Estado_Prueba(string prod_Codigo, Nullable<bool> prod_EsActivo)
        {
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var prod_EsActivoParameter = prod_EsActivo.HasValue ?
                new ObjectParameter("prod_EsActivo", prod_EsActivo) :
                new ObjectParameter("prod_EsActivo", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProducto_Estado_Prueba_Result>("UDP_Inv_tbProducto_Estado_Prueba", prod_CodigoParameter, prod_EsActivoParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProducto_Existencia_Result> UDP_Inv_tbProducto_Existencia(string prod_Codigo)
        {
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProducto_Existencia_Result>("UDP_Inv_tbProducto_Existencia", prod_CodigoParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProducto_GetCategoria_Result> UDP_Inv_tbProducto_GetCategoria(Nullable<int> codsubcategoria)
        {
            var codsubcategoriaParameter = codsubcategoria.HasValue ?
                new ObjectParameter("codsubcategoria", codsubcategoria) :
                new ObjectParameter("codsubcategoria", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProducto_GetCategoria_Result>("UDP_Inv_tbProducto_GetCategoria", codsubcategoriaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProducto_GetSubCategoia_Result> UDP_Inv_tbProducto_GetSubCategoia(Nullable<int> codCategoria)
        {
            var codCategoriaParameter = codCategoria.HasValue ?
                new ObjectParameter("CodCategoria", codCategoria) :
                new ObjectParameter("CodCategoria", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProducto_GetSubCategoia_Result>("UDP_Inv_tbProducto_GetSubCategoia", codCategoriaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProducto_GetSubCategoria_Result> UDP_Inv_tbProducto_GetSubCategoria(Nullable<int> codCategoria)
        {
            var codCategoriaParameter = codCategoria.HasValue ?
                new ObjectParameter("CodCategoria", codCategoria) :
                new ObjectParameter("CodCategoria", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProducto_GetSubCategoria_Result>("UDP_Inv_tbProducto_GetSubCategoria", codCategoriaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProducto_Insert_Result> UDP_Inv_tbProducto_Insert(string prod_Codigo, string prod_Descripcion, string prod_Marca, string prod_Modelo, string prod_Talla, string prod_Color, Nullable<int> pscat_Id, Nullable<int> uni_Id, Nullable<bool> prod_EsActivo, string prod_CodigoBarras, Nullable<int> prov_Id, Nullable<int> prod_UsuarioCrea, Nullable<System.DateTime> prod_FechaCrea)
        {
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var prod_DescripcionParameter = prod_Descripcion != null ?
                new ObjectParameter("prod_Descripcion", prod_Descripcion) :
                new ObjectParameter("prod_Descripcion", typeof(string));
    
            var prod_MarcaParameter = prod_Marca != null ?
                new ObjectParameter("prod_Marca", prod_Marca) :
                new ObjectParameter("prod_Marca", typeof(string));
    
            var prod_ModeloParameter = prod_Modelo != null ?
                new ObjectParameter("prod_Modelo", prod_Modelo) :
                new ObjectParameter("prod_Modelo", typeof(string));
    
            var prod_TallaParameter = prod_Talla != null ?
                new ObjectParameter("prod_Talla", prod_Talla) :
                new ObjectParameter("prod_Talla", typeof(string));
    
            var prod_ColorParameter = prod_Color != null ?
                new ObjectParameter("prod_Color", prod_Color) :
                new ObjectParameter("prod_Color", typeof(string));
    
            var pscat_IdParameter = pscat_Id.HasValue ?
                new ObjectParameter("pscat_Id", pscat_Id) :
                new ObjectParameter("pscat_Id", typeof(int));
    
            var uni_IdParameter = uni_Id.HasValue ?
                new ObjectParameter("uni_Id", uni_Id) :
                new ObjectParameter("uni_Id", typeof(int));
    
            var prod_EsActivoParameter = prod_EsActivo.HasValue ?
                new ObjectParameter("prod_EsActivo", prod_EsActivo) :
                new ObjectParameter("prod_EsActivo", typeof(bool));
    
            var prod_CodigoBarrasParameter = prod_CodigoBarras != null ?
                new ObjectParameter("prod_CodigoBarras", prod_CodigoBarras) :
                new ObjectParameter("prod_CodigoBarras", typeof(string));
    
            var prov_IdParameter = prov_Id.HasValue ?
                new ObjectParameter("prov_Id", prov_Id) :
                new ObjectParameter("prov_Id", typeof(int));
    
            var prod_UsuarioCreaParameter = prod_UsuarioCrea.HasValue ?
                new ObjectParameter("prod_UsuarioCrea", prod_UsuarioCrea) :
                new ObjectParameter("prod_UsuarioCrea", typeof(int));
    
            var prod_FechaCreaParameter = prod_FechaCrea.HasValue ?
                new ObjectParameter("prod_FechaCrea", prod_FechaCrea) :
                new ObjectParameter("prod_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProducto_Insert_Result>("UDP_Inv_tbProducto_Insert", prod_CodigoParameter, prod_DescripcionParameter, prod_MarcaParameter, prod_ModeloParameter, prod_TallaParameter, prod_ColorParameter, pscat_IdParameter, uni_IdParameter, prod_EsActivoParameter, prod_CodigoBarrasParameter, prov_IdParameter, prod_UsuarioCreaParameter, prod_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProducto_Update_Result> UDP_Inv_tbProducto_Update(string prod_Codigo, string prod_Descripcion, string prod_Marca, string prod_Modelo, string prod_Talla, string prod_Color, Nullable<int> pscat_Id, Nullable<int> uni_Id, string prod_Razon_Inactivacion, string prod_CodigoBarras, Nullable<int> prov_Id, Nullable<int> prod_UsuarioCrea, Nullable<System.DateTime> prod_FechaCrea, Nullable<int> prod_UsuarioModifica, Nullable<System.DateTime> prod_FechaModifica)
        {
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var prod_DescripcionParameter = prod_Descripcion != null ?
                new ObjectParameter("prod_Descripcion", prod_Descripcion) :
                new ObjectParameter("prod_Descripcion", typeof(string));
    
            var prod_MarcaParameter = prod_Marca != null ?
                new ObjectParameter("prod_Marca", prod_Marca) :
                new ObjectParameter("prod_Marca", typeof(string));
    
            var prod_ModeloParameter = prod_Modelo != null ?
                new ObjectParameter("prod_Modelo", prod_Modelo) :
                new ObjectParameter("prod_Modelo", typeof(string));
    
            var prod_TallaParameter = prod_Talla != null ?
                new ObjectParameter("prod_Talla", prod_Talla) :
                new ObjectParameter("prod_Talla", typeof(string));
    
            var prod_ColorParameter = prod_Color != null ?
                new ObjectParameter("prod_Color", prod_Color) :
                new ObjectParameter("prod_Color", typeof(string));
    
            var pscat_IdParameter = pscat_Id.HasValue ?
                new ObjectParameter("pscat_Id", pscat_Id) :
                new ObjectParameter("pscat_Id", typeof(int));
    
            var uni_IdParameter = uni_Id.HasValue ?
                new ObjectParameter("uni_Id", uni_Id) :
                new ObjectParameter("uni_Id", typeof(int));
    
            var prod_Razon_InactivacionParameter = prod_Razon_Inactivacion != null ?
                new ObjectParameter("prod_Razon_Inactivacion", prod_Razon_Inactivacion) :
                new ObjectParameter("prod_Razon_Inactivacion", typeof(string));
    
            var prod_CodigoBarrasParameter = prod_CodigoBarras != null ?
                new ObjectParameter("prod_CodigoBarras", prod_CodigoBarras) :
                new ObjectParameter("prod_CodigoBarras", typeof(string));
    
            var prov_IdParameter = prov_Id.HasValue ?
                new ObjectParameter("prov_Id", prov_Id) :
                new ObjectParameter("prov_Id", typeof(int));
    
            var prod_UsuarioCreaParameter = prod_UsuarioCrea.HasValue ?
                new ObjectParameter("prod_UsuarioCrea", prod_UsuarioCrea) :
                new ObjectParameter("prod_UsuarioCrea", typeof(int));
    
            var prod_FechaCreaParameter = prod_FechaCrea.HasValue ?
                new ObjectParameter("prod_FechaCrea", prod_FechaCrea) :
                new ObjectParameter("prod_FechaCrea", typeof(System.DateTime));
    
            var prod_UsuarioModificaParameter = prod_UsuarioModifica.HasValue ?
                new ObjectParameter("prod_UsuarioModifica", prod_UsuarioModifica) :
                new ObjectParameter("prod_UsuarioModifica", typeof(int));
    
            var prod_FechaModificaParameter = prod_FechaModifica.HasValue ?
                new ObjectParameter("prod_FechaModifica", prod_FechaModifica) :
                new ObjectParameter("prod_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProducto_Update_Result>("UDP_Inv_tbProducto_Update", prod_CodigoParameter, prod_DescripcionParameter, prod_MarcaParameter, prod_ModeloParameter, prod_TallaParameter, prod_ColorParameter, pscat_IdParameter, uni_IdParameter, prod_Razon_InactivacionParameter, prod_CodigoBarrasParameter, prov_IdParameter, prod_UsuarioCreaParameter, prod_FechaCreaParameter, prod_UsuarioModificaParameter, prod_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_INV_tbProducto_Update_EstadoActivar_Result> UDP_INV_tbProducto_Update_EstadoActivar(string prod_Codigo, Nullable<bool> prod_EsActivo, Nullable<int> prod_UsuarioModifica, Nullable<System.DateTime> prod_FechaModifica)
        {
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var prod_EsActivoParameter = prod_EsActivo.HasValue ?
                new ObjectParameter("prod_EsActivo", prod_EsActivo) :
                new ObjectParameter("prod_EsActivo", typeof(bool));
    
            var prod_UsuarioModificaParameter = prod_UsuarioModifica.HasValue ?
                new ObjectParameter("prod_UsuarioModifica", prod_UsuarioModifica) :
                new ObjectParameter("prod_UsuarioModifica", typeof(int));
    
            var prod_FechaModificaParameter = prod_FechaModifica.HasValue ?
                new ObjectParameter("prod_FechaModifica", prod_FechaModifica) :
                new ObjectParameter("prod_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_INV_tbProducto_Update_EstadoActivar_Result>("UDP_INV_tbProducto_Update_EstadoActivar", prod_CodigoParameter, prod_EsActivoParameter, prod_UsuarioModificaParameter, prod_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProducto_Update_RazonInactivacion_Result> UDP_Inv_tbProducto_Update_RazonInactivacion(string prod_Codigo, Nullable<bool> prod_EsActivo, string prod_Razon_Inactivacion, Nullable<int> prod_Usuario_Crea, Nullable<System.DateTime> prod_Fecha_Crea, Nullable<int> prod_UsuarioModifica, Nullable<System.DateTime> prod_FechaModifica)
        {
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var prod_EsActivoParameter = prod_EsActivo.HasValue ?
                new ObjectParameter("prod_EsActivo", prod_EsActivo) :
                new ObjectParameter("prod_EsActivo", typeof(bool));
    
            var prod_Razon_InactivacionParameter = prod_Razon_Inactivacion != null ?
                new ObjectParameter("prod_Razon_Inactivacion", prod_Razon_Inactivacion) :
                new ObjectParameter("prod_Razon_Inactivacion", typeof(string));
    
            var prod_Usuario_CreaParameter = prod_Usuario_Crea.HasValue ?
                new ObjectParameter("prod_Usuario_Crea", prod_Usuario_Crea) :
                new ObjectParameter("prod_Usuario_Crea", typeof(int));
    
            var prod_Fecha_CreaParameter = prod_Fecha_Crea.HasValue ?
                new ObjectParameter("prod_Fecha_Crea", prod_Fecha_Crea) :
                new ObjectParameter("prod_Fecha_Crea", typeof(System.DateTime));
    
            var prod_UsuarioModificaParameter = prod_UsuarioModifica.HasValue ?
                new ObjectParameter("prod_UsuarioModifica", prod_UsuarioModifica) :
                new ObjectParameter("prod_UsuarioModifica", typeof(int));
    
            var prod_FechaModificaParameter = prod_FechaModifica.HasValue ?
                new ObjectParameter("prod_FechaModifica", prod_FechaModifica) :
                new ObjectParameter("prod_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProducto_Update_RazonInactivacion_Result>("UDP_Inv_tbProducto_Update_RazonInactivacion", prod_CodigoParameter, prod_EsActivoParameter, prod_Razon_InactivacionParameter, prod_Usuario_CreaParameter, prod_Fecha_CreaParameter, prod_UsuarioModificaParameter, prod_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProducto_ValorCodigo_Result> UDP_Inv_tbProducto_ValorCodigo(Nullable<int> pcat_Id, Nullable<int> pscat_Id, ObjectParameter prod_Codigo)
        {
            var pcat_IdParameter = pcat_Id.HasValue ?
                new ObjectParameter("pcat_Id", pcat_Id) :
                new ObjectParameter("pcat_Id", typeof(int));
    
            var pscat_IdParameter = pscat_Id.HasValue ?
                new ObjectParameter("pscat_Id", pscat_Id) :
                new ObjectParameter("pscat_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProducto_ValorCodigo_Result>("UDP_Inv_tbProducto_ValorCodigo", pcat_IdParameter, pscat_IdParameter, prod_Codigo);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProducto_ValorCodigo1_Result> UDP_Inv_tbProducto_ValorCodigo1(Nullable<int> pcat_Id, Nullable<int> pscat_Id, ObjectParameter prod_Codigo)
        {
            var pcat_IdParameter = pcat_Id.HasValue ?
                new ObjectParameter("pcat_Id", pcat_Id) :
                new ObjectParameter("pcat_Id", typeof(int));
    
            var pscat_IdParameter = pscat_Id.HasValue ?
                new ObjectParameter("pscat_Id", pscat_Id) :
                new ObjectParameter("pscat_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProducto_ValorCodigo1_Result>("UDP_Inv_tbProducto_ValorCodigo1", pcat_IdParameter, pscat_IdParameter, prod_Codigo);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProductoCategoria_Delete_Result> UDP_Inv_tbProductoCategoria_Delete(Nullable<int> pcat_Id)
        {
            var pcat_IdParameter = pcat_Id.HasValue ?
                new ObjectParameter("pcat_Id", pcat_Id) :
                new ObjectParameter("pcat_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProductoCategoria_Delete_Result>("UDP_Inv_tbProductoCategoria_Delete", pcat_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProductoCategoria_Insert_Result> UDP_Inv_tbProductoCategoria_Insert(string pcat_Nombre, Nullable<int> pcat_UsuarioCrea, Nullable<System.DateTime> pcat_FechaCrea)
        {
            var pcat_NombreParameter = pcat_Nombre != null ?
                new ObjectParameter("pcat_Nombre", pcat_Nombre) :
                new ObjectParameter("pcat_Nombre", typeof(string));
    
            var pcat_UsuarioCreaParameter = pcat_UsuarioCrea.HasValue ?
                new ObjectParameter("pcat_UsuarioCrea", pcat_UsuarioCrea) :
                new ObjectParameter("pcat_UsuarioCrea", typeof(int));
    
            var pcat_FechaCreaParameter = pcat_FechaCrea.HasValue ?
                new ObjectParameter("pcat_FechaCrea", pcat_FechaCrea) :
                new ObjectParameter("pcat_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProductoCategoria_Insert_Result>("UDP_Inv_tbProductoCategoria_Insert", pcat_NombreParameter, pcat_UsuarioCreaParameter, pcat_FechaCreaParameter);
        }
    
        public virtual int UDP_Inv_tbProductoCategoria_Update_Estado(Nullable<int> pcat_Id, Nullable<byte> eSTADO)
        {
            var pcat_IdParameter = pcat_Id.HasValue ?
                new ObjectParameter("pcat_Id", pcat_Id) :
                new ObjectParameter("pcat_Id", typeof(int));
    
            var eSTADOParameter = eSTADO.HasValue ?
                new ObjectParameter("ESTADO", eSTADO) :
                new ObjectParameter("ESTADO", typeof(byte));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UDP_Inv_tbProductoCategoria_Update_Estado", pcat_IdParameter, eSTADOParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProductoSubcategoria_Insert_Result> UDP_Inv_tbProductoSubcategoria_Insert(string pscat_Descripcion, Nullable<int> pcat_Id, Nullable<int> pscat_UsuarioCrea, Nullable<System.DateTime> pscat_FechaCrea, Nullable<decimal> pscat_ISV)
        {
            var pscat_DescripcionParameter = pscat_Descripcion != null ?
                new ObjectParameter("pscat_Descripcion", pscat_Descripcion) :
                new ObjectParameter("pscat_Descripcion", typeof(string));
    
            var pcat_IdParameter = pcat_Id.HasValue ?
                new ObjectParameter("pcat_Id", pcat_Id) :
                new ObjectParameter("pcat_Id", typeof(int));
    
            var pscat_UsuarioCreaParameter = pscat_UsuarioCrea.HasValue ?
                new ObjectParameter("pscat_UsuarioCrea", pscat_UsuarioCrea) :
                new ObjectParameter("pscat_UsuarioCrea", typeof(int));
    
            var pscat_FechaCreaParameter = pscat_FechaCrea.HasValue ?
                new ObjectParameter("pscat_FechaCrea", pscat_FechaCrea) :
                new ObjectParameter("pscat_FechaCrea", typeof(System.DateTime));
    
            var pscat_ISVParameter = pscat_ISV.HasValue ?
                new ObjectParameter("pscat_ISV", pscat_ISV) :
                new ObjectParameter("pscat_ISV", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProductoSubcategoria_Insert_Result>("UDP_Inv_tbProductoSubcategoria_Insert", pscat_DescripcionParameter, pcat_IdParameter, pscat_UsuarioCreaParameter, pscat_FechaCreaParameter, pscat_ISVParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProductoSubcategoria_Update_Result> UDP_Inv_tbProductoSubcategoria_Update(Nullable<int> pscat_Id, string pscat_Descripcion, Nullable<int> pcat_Id, Nullable<int> pscat_UsuarioCrea, Nullable<System.DateTime> pscat_FechaCrea, Nullable<int> pscat_UsuarioModifica, Nullable<System.DateTime> pscat_FechaModifica, Nullable<decimal> pscat_ISV)
        {
            var pscat_IdParameter = pscat_Id.HasValue ?
                new ObjectParameter("pscat_Id", pscat_Id) :
                new ObjectParameter("pscat_Id", typeof(int));
    
            var pscat_DescripcionParameter = pscat_Descripcion != null ?
                new ObjectParameter("pscat_Descripcion", pscat_Descripcion) :
                new ObjectParameter("pscat_Descripcion", typeof(string));
    
            var pcat_IdParameter = pcat_Id.HasValue ?
                new ObjectParameter("pcat_Id", pcat_Id) :
                new ObjectParameter("pcat_Id", typeof(int));
    
            var pscat_UsuarioCreaParameter = pscat_UsuarioCrea.HasValue ?
                new ObjectParameter("pscat_UsuarioCrea", pscat_UsuarioCrea) :
                new ObjectParameter("pscat_UsuarioCrea", typeof(int));
    
            var pscat_FechaCreaParameter = pscat_FechaCrea.HasValue ?
                new ObjectParameter("pscat_FechaCrea", pscat_FechaCrea) :
                new ObjectParameter("pscat_FechaCrea", typeof(System.DateTime));
    
            var pscat_UsuarioModificaParameter = pscat_UsuarioModifica.HasValue ?
                new ObjectParameter("pscat_UsuarioModifica", pscat_UsuarioModifica) :
                new ObjectParameter("pscat_UsuarioModifica", typeof(int));
    
            var pscat_FechaModificaParameter = pscat_FechaModifica.HasValue ?
                new ObjectParameter("pscat_FechaModifica", pscat_FechaModifica) :
                new ObjectParameter("pscat_FechaModifica", typeof(System.DateTime));
    
            var pscat_ISVParameter = pscat_ISV.HasValue ?
                new ObjectParameter("pscat_ISV", pscat_ISV) :
                new ObjectParameter("pscat_ISV", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProductoSubcategoria_Update_Result>("UDP_Inv_tbProductoSubcategoria_Update", pscat_IdParameter, pscat_DescripcionParameter, pcat_IdParameter, pscat_UsuarioCreaParameter, pscat_FechaCreaParameter, pscat_UsuarioModificaParameter, pscat_FechaModificaParameter, pscat_ISVParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProductoSubCategoria_Update_Estado_Result> UDP_Inv_tbProductoSubCategoria_Update_Estado(Nullable<int> pscat_Id, Nullable<byte> pscat_EsActiva)
        {
            var pscat_IdParameter = pscat_Id.HasValue ?
                new ObjectParameter("pscat_Id", pscat_Id) :
                new ObjectParameter("pscat_Id", typeof(int));
    
            var pscat_EsActivaParameter = pscat_EsActiva.HasValue ?
                new ObjectParameter("pscat_EsActiva", pscat_EsActiva) :
                new ObjectParameter("pscat_EsActiva", typeof(byte));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProductoSubCategoria_Update_Estado_Result>("UDP_Inv_tbProductoSubCategoria_Update_Estado", pscat_IdParameter, pscat_EsActivaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProductoSubCategoria_Update_Estado_Validacion_Result> UDP_Inv_tbProductoSubCategoria_Update_Estado_Validacion(Nullable<int> pscat_Id, Nullable<byte> pscat_EsActiva, Nullable<int> pscat_UsuarioModifica, Nullable<System.DateTime> pscat_FechaModifica)
        {
            var pscat_IdParameter = pscat_Id.HasValue ?
                new ObjectParameter("pscat_Id", pscat_Id) :
                new ObjectParameter("pscat_Id", typeof(int));
    
            var pscat_EsActivaParameter = pscat_EsActiva.HasValue ?
                new ObjectParameter("pscat_EsActiva", pscat_EsActiva) :
                new ObjectParameter("pscat_EsActiva", typeof(byte));
    
            var pscat_UsuarioModificaParameter = pscat_UsuarioModifica.HasValue ?
                new ObjectParameter("pscat_UsuarioModifica", pscat_UsuarioModifica) :
                new ObjectParameter("pscat_UsuarioModifica", typeof(int));
    
            var pscat_FechaModificaParameter = pscat_FechaModifica.HasValue ?
                new ObjectParameter("pscat_FechaModifica", pscat_FechaModifica) :
                new ObjectParameter("pscat_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProductoSubCategoria_Update_Estado_Validacion_Result>("UDP_Inv_tbProductoSubCategoria_Update_Estado_Validacion", pscat_IdParameter, pscat_EsActivaParameter, pscat_UsuarioModificaParameter, pscat_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProveedor_Insert_Result> UDP_Inv_tbProveedor_Insert(string prov_Nombre, string prov_NombreContacto, string prov_Direccion, string prov_Email, string prov_Telefono, string prov_RTN, Nullable<short> acte_Id, Nullable<int> prov_UsuarioCrea, Nullable<System.DateTime> prov_FechaCrea)
        {
            var prov_NombreParameter = prov_Nombre != null ?
                new ObjectParameter("prov_Nombre", prov_Nombre) :
                new ObjectParameter("prov_Nombre", typeof(string));
    
            var prov_NombreContactoParameter = prov_NombreContacto != null ?
                new ObjectParameter("prov_NombreContacto", prov_NombreContacto) :
                new ObjectParameter("prov_NombreContacto", typeof(string));
    
            var prov_DireccionParameter = prov_Direccion != null ?
                new ObjectParameter("prov_Direccion", prov_Direccion) :
                new ObjectParameter("prov_Direccion", typeof(string));
    
            var prov_EmailParameter = prov_Email != null ?
                new ObjectParameter("prov_Email", prov_Email) :
                new ObjectParameter("prov_Email", typeof(string));
    
            var prov_TelefonoParameter = prov_Telefono != null ?
                new ObjectParameter("prov_Telefono", prov_Telefono) :
                new ObjectParameter("prov_Telefono", typeof(string));
    
            var prov_RTNParameter = prov_RTN != null ?
                new ObjectParameter("prov_RTN", prov_RTN) :
                new ObjectParameter("prov_RTN", typeof(string));
    
            var acte_IdParameter = acte_Id.HasValue ?
                new ObjectParameter("acte_Id", acte_Id) :
                new ObjectParameter("acte_Id", typeof(short));
    
            var prov_UsuarioCreaParameter = prov_UsuarioCrea.HasValue ?
                new ObjectParameter("prov_UsuarioCrea", prov_UsuarioCrea) :
                new ObjectParameter("prov_UsuarioCrea", typeof(int));
    
            var prov_FechaCreaParameter = prov_FechaCrea.HasValue ?
                new ObjectParameter("prov_FechaCrea", prov_FechaCrea) :
                new ObjectParameter("prov_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProveedor_Insert_Result>("UDP_Inv_tbProveedor_Insert", prov_NombreParameter, prov_NombreContactoParameter, prov_DireccionParameter, prov_EmailParameter, prov_TelefonoParameter, prov_RTNParameter, acte_IdParameter, prov_UsuarioCreaParameter, prov_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProveedor_Update_Result> UDP_Inv_tbProveedor_Update(Nullable<int> prov_Id, string prov_Nombre, string prov_NombreContacto, string prov_Direccion, string prov_Email, string prov_Telefono, string prov_RTN, Nullable<short> acte_Id, Nullable<int> prov_UsuarioCrea, Nullable<System.DateTime> prov_FechaCrea, Nullable<int> prov_UsuarioModifica, Nullable<System.DateTime> prov_FechaModifica)
        {
            var prov_IdParameter = prov_Id.HasValue ?
                new ObjectParameter("prov_Id", prov_Id) :
                new ObjectParameter("prov_Id", typeof(int));
    
            var prov_NombreParameter = prov_Nombre != null ?
                new ObjectParameter("prov_Nombre", prov_Nombre) :
                new ObjectParameter("prov_Nombre", typeof(string));
    
            var prov_NombreContactoParameter = prov_NombreContacto != null ?
                new ObjectParameter("prov_NombreContacto", prov_NombreContacto) :
                new ObjectParameter("prov_NombreContacto", typeof(string));
    
            var prov_DireccionParameter = prov_Direccion != null ?
                new ObjectParameter("prov_Direccion", prov_Direccion) :
                new ObjectParameter("prov_Direccion", typeof(string));
    
            var prov_EmailParameter = prov_Email != null ?
                new ObjectParameter("prov_Email", prov_Email) :
                new ObjectParameter("prov_Email", typeof(string));
    
            var prov_TelefonoParameter = prov_Telefono != null ?
                new ObjectParameter("prov_Telefono", prov_Telefono) :
                new ObjectParameter("prov_Telefono", typeof(string));
    
            var prov_RTNParameter = prov_RTN != null ?
                new ObjectParameter("prov_RTN", prov_RTN) :
                new ObjectParameter("prov_RTN", typeof(string));
    
            var acte_IdParameter = acte_Id.HasValue ?
                new ObjectParameter("acte_Id", acte_Id) :
                new ObjectParameter("acte_Id", typeof(short));
    
            var prov_UsuarioCreaParameter = prov_UsuarioCrea.HasValue ?
                new ObjectParameter("prov_UsuarioCrea", prov_UsuarioCrea) :
                new ObjectParameter("prov_UsuarioCrea", typeof(int));
    
            var prov_FechaCreaParameter = prov_FechaCrea.HasValue ?
                new ObjectParameter("prov_FechaCrea", prov_FechaCrea) :
                new ObjectParameter("prov_FechaCrea", typeof(System.DateTime));
    
            var prov_UsuarioModificaParameter = prov_UsuarioModifica.HasValue ?
                new ObjectParameter("prov_UsuarioModifica", prov_UsuarioModifica) :
                new ObjectParameter("prov_UsuarioModifica", typeof(int));
    
            var prov_FechaModificaParameter = prov_FechaModifica.HasValue ?
                new ObjectParameter("prov_FechaModifica", prov_FechaModifica) :
                new ObjectParameter("prov_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProveedor_Update_Result>("UDP_Inv_tbProveedor_Update", prov_IdParameter, prov_NombreParameter, prov_NombreContactoParameter, prov_DireccionParameter, prov_EmailParameter, prov_TelefonoParameter, prov_RTNParameter, acte_IdParameter, prov_UsuarioCreaParameter, prov_FechaCreaParameter, prov_UsuarioModificaParameter, prov_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbSalida_Insert_Result> UDP_Inv_tbSalida_Insert(Nullable<int> bod_Id, Nullable<long> fact_Id, Nullable<System.DateTime> sal_FechaElaboracion, Nullable<byte> estm_Id, Nullable<byte> tsal_Id, Nullable<int> sal_BodDestino, Nullable<bool> sal_EsAnulada, Nullable<int> tdev_Id, string sal_RazonAnulada, Nullable<int> sal_UsuarioCrea, Nullable<System.DateTime> sal_FechaCrea)
        {
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var fact_IdParameter = fact_Id.HasValue ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(long));
    
            var sal_FechaElaboracionParameter = sal_FechaElaboracion.HasValue ?
                new ObjectParameter("sal_FechaElaboracion", sal_FechaElaboracion) :
                new ObjectParameter("sal_FechaElaboracion", typeof(System.DateTime));
    
            var estm_IdParameter = estm_Id.HasValue ?
                new ObjectParameter("estm_Id", estm_Id) :
                new ObjectParameter("estm_Id", typeof(byte));
    
            var tsal_IdParameter = tsal_Id.HasValue ?
                new ObjectParameter("tsal_Id", tsal_Id) :
                new ObjectParameter("tsal_Id", typeof(byte));
    
            var sal_BodDestinoParameter = sal_BodDestino.HasValue ?
                new ObjectParameter("sal_BodDestino", sal_BodDestino) :
                new ObjectParameter("sal_BodDestino", typeof(int));
    
            var sal_EsAnuladaParameter = sal_EsAnulada.HasValue ?
                new ObjectParameter("sal_EsAnulada", sal_EsAnulada) :
                new ObjectParameter("sal_EsAnulada", typeof(bool));
    
            var tdev_IdParameter = tdev_Id.HasValue ?
                new ObjectParameter("tdev_Id", tdev_Id) :
                new ObjectParameter("tdev_Id", typeof(int));
    
            var sal_RazonAnuladaParameter = sal_RazonAnulada != null ?
                new ObjectParameter("sal_RazonAnulada", sal_RazonAnulada) :
                new ObjectParameter("sal_RazonAnulada", typeof(string));
    
            var sal_UsuarioCreaParameter = sal_UsuarioCrea.HasValue ?
                new ObjectParameter("sal_UsuarioCrea", sal_UsuarioCrea) :
                new ObjectParameter("sal_UsuarioCrea", typeof(int));
    
            var sal_FechaCreaParameter = sal_FechaCrea.HasValue ?
                new ObjectParameter("sal_FechaCrea", sal_FechaCrea) :
                new ObjectParameter("sal_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbSalida_Insert_Result>("UDP_Inv_tbSalida_Insert", bod_IdParameter, fact_IdParameter, sal_FechaElaboracionParameter, estm_IdParameter, tsal_IdParameter, sal_BodDestinoParameter, sal_EsAnuladaParameter, tdev_IdParameter, sal_RazonAnuladaParameter, sal_UsuarioCreaParameter, sal_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbsalida_InsertarEntradaPorPrestamo_Result> UDP_Inv_tbsalida_InsertarEntradaPorPrestamo(Nullable<int> bod_Id, Nullable<byte> estm_Id, string prod_Codigo, Nullable<decimal> entd_Cantidad)
        {
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var estm_IdParameter = estm_Id.HasValue ?
                new ObjectParameter("estm_Id", estm_Id) :
                new ObjectParameter("estm_Id", typeof(byte));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var entd_CantidadParameter = entd_Cantidad.HasValue ?
                new ObjectParameter("entd_Cantidad", entd_Cantidad) :
                new ObjectParameter("entd_Cantidad", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbsalida_InsertarEntradaPorPrestamo_Result>("UDP_Inv_tbsalida_InsertarEntradaPorPrestamo", bod_IdParameter, estm_IdParameter, prod_CodigoParameter, entd_CantidadParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbSalida_InsertEntradaDetallePorPrestamo_Result> UDP_Inv_tbSalida_InsertEntradaDetallePorPrestamo(Nullable<int> ent_Id, string prod_Codigo, Nullable<decimal> entd_Cantidad)
        {
            var ent_IdParameter = ent_Id.HasValue ?
                new ObjectParameter("ent_Id", ent_Id) :
                new ObjectParameter("ent_Id", typeof(int));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var entd_CantidadParameter = entd_Cantidad.HasValue ?
                new ObjectParameter("entd_Cantidad", entd_Cantidad) :
                new ObjectParameter("entd_Cantidad", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbSalida_InsertEntradaDetallePorPrestamo_Result>("UDP_Inv_tbSalida_InsertEntradaDetallePorPrestamo", ent_IdParameter, prod_CodigoParameter, entd_CantidadParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbSalida_Update_Result> UDP_Inv_tbSalida_Update(Nullable<int> sal_Id, Nullable<int> bod_Id, Nullable<long> fact_Id, Nullable<System.DateTime> sal_FechaElaboracion, Nullable<byte> estm_Id, Nullable<byte> tsal_Id, Nullable<int> sal_BodDestino, Nullable<bool> sal_EsAnulada, Nullable<int> tdev_Id, string sal_RazonAnulada, Nullable<int> sal_UsuarioCrea, Nullable<System.DateTime> sal_FechaCrea, Nullable<int> sal_UsuarioModifica, Nullable<System.DateTime> sal_FechaModifica)
        {
            var sal_IdParameter = sal_Id.HasValue ?
                new ObjectParameter("sal_Id", sal_Id) :
                new ObjectParameter("sal_Id", typeof(int));
    
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var fact_IdParameter = fact_Id.HasValue ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(long));
    
            var sal_FechaElaboracionParameter = sal_FechaElaboracion.HasValue ?
                new ObjectParameter("sal_FechaElaboracion", sal_FechaElaboracion) :
                new ObjectParameter("sal_FechaElaboracion", typeof(System.DateTime));
    
            var estm_IdParameter = estm_Id.HasValue ?
                new ObjectParameter("estm_Id", estm_Id) :
                new ObjectParameter("estm_Id", typeof(byte));
    
            var tsal_IdParameter = tsal_Id.HasValue ?
                new ObjectParameter("tsal_Id", tsal_Id) :
                new ObjectParameter("tsal_Id", typeof(byte));
    
            var sal_BodDestinoParameter = sal_BodDestino.HasValue ?
                new ObjectParameter("sal_BodDestino", sal_BodDestino) :
                new ObjectParameter("sal_BodDestino", typeof(int));
    
            var sal_EsAnuladaParameter = sal_EsAnulada.HasValue ?
                new ObjectParameter("sal_EsAnulada", sal_EsAnulada) :
                new ObjectParameter("sal_EsAnulada", typeof(bool));
    
            var tdev_IdParameter = tdev_Id.HasValue ?
                new ObjectParameter("tdev_Id", tdev_Id) :
                new ObjectParameter("tdev_Id", typeof(int));
    
            var sal_RazonAnuladaParameter = sal_RazonAnulada != null ?
                new ObjectParameter("sal_RazonAnulada", sal_RazonAnulada) :
                new ObjectParameter("sal_RazonAnulada", typeof(string));
    
            var sal_UsuarioCreaParameter = sal_UsuarioCrea.HasValue ?
                new ObjectParameter("sal_UsuarioCrea", sal_UsuarioCrea) :
                new ObjectParameter("sal_UsuarioCrea", typeof(int));
    
            var sal_FechaCreaParameter = sal_FechaCrea.HasValue ?
                new ObjectParameter("sal_FechaCrea", sal_FechaCrea) :
                new ObjectParameter("sal_FechaCrea", typeof(System.DateTime));
    
            var sal_UsuarioModificaParameter = sal_UsuarioModifica.HasValue ?
                new ObjectParameter("sal_UsuarioModifica", sal_UsuarioModifica) :
                new ObjectParameter("sal_UsuarioModifica", typeof(int));
    
            var sal_FechaModificaParameter = sal_FechaModifica.HasValue ?
                new ObjectParameter("sal_FechaModifica", sal_FechaModifica) :
                new ObjectParameter("sal_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbSalida_Update_Result>("UDP_Inv_tbSalida_Update", sal_IdParameter, bod_IdParameter, fact_IdParameter, sal_FechaElaboracionParameter, estm_IdParameter, tsal_IdParameter, sal_BodDestinoParameter, sal_EsAnuladaParameter, tdev_IdParameter, sal_RazonAnuladaParameter, sal_UsuarioCreaParameter, sal_FechaCreaParameter, sal_UsuarioModificaParameter, sal_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbSalidaBodegaDestino_Insert_Result> UDP_Inv_tbSalidaBodegaDestino_Insert(string prod_Codigo, Nullable<int> bod_Id, Nullable<decimal> bodd_CantidadMinima, Nullable<decimal> bodd_CantidadMaxima, Nullable<decimal> bodd_PuntoReorden, Nullable<decimal> bodd_Costo, Nullable<decimal> bodd_CostoPromedio, Nullable<int> bodd_UsuarioCrea, Nullable<System.DateTime> bodd_FechaCrea, Nullable<decimal> cANTIDAD_EXISTENTE)
        {
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var bodd_CantidadMinimaParameter = bodd_CantidadMinima.HasValue ?
                new ObjectParameter("bodd_CantidadMinima", bodd_CantidadMinima) :
                new ObjectParameter("bodd_CantidadMinima", typeof(decimal));
    
            var bodd_CantidadMaximaParameter = bodd_CantidadMaxima.HasValue ?
                new ObjectParameter("bodd_CantidadMaxima", bodd_CantidadMaxima) :
                new ObjectParameter("bodd_CantidadMaxima", typeof(decimal));
    
            var bodd_PuntoReordenParameter = bodd_PuntoReorden.HasValue ?
                new ObjectParameter("bodd_PuntoReorden", bodd_PuntoReorden) :
                new ObjectParameter("bodd_PuntoReorden", typeof(decimal));
    
            var bodd_CostoParameter = bodd_Costo.HasValue ?
                new ObjectParameter("bodd_Costo", bodd_Costo) :
                new ObjectParameter("bodd_Costo", typeof(decimal));
    
            var bodd_CostoPromedioParameter = bodd_CostoPromedio.HasValue ?
                new ObjectParameter("bodd_CostoPromedio", bodd_CostoPromedio) :
                new ObjectParameter("bodd_CostoPromedio", typeof(decimal));
    
            var bodd_UsuarioCreaParameter = bodd_UsuarioCrea.HasValue ?
                new ObjectParameter("bodd_UsuarioCrea", bodd_UsuarioCrea) :
                new ObjectParameter("bodd_UsuarioCrea", typeof(int));
    
            var bodd_FechaCreaParameter = bodd_FechaCrea.HasValue ?
                new ObjectParameter("bodd_FechaCrea", bodd_FechaCrea) :
                new ObjectParameter("bodd_FechaCrea", typeof(System.DateTime));
    
            var cANTIDAD_EXISTENTEParameter = cANTIDAD_EXISTENTE.HasValue ?
                new ObjectParameter("CANTIDAD_EXISTENTE", cANTIDAD_EXISTENTE) :
                new ObjectParameter("CANTIDAD_EXISTENTE", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbSalidaBodegaDestino_Insert_Result>("UDP_Inv_tbSalidaBodegaDestino_Insert", prod_CodigoParameter, bod_IdParameter, bodd_CantidadMinimaParameter, bodd_CantidadMaximaParameter, bodd_PuntoReordenParameter, bodd_CostoParameter, bodd_CostoPromedioParameter, bodd_UsuarioCreaParameter, bodd_FechaCreaParameter, cANTIDAD_EXISTENTEParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbSalidaDetalle_Delete_Result> UDP_Inv_tbSalidaDetalle_Delete(Nullable<int> sald_Id)
        {
            var sald_IdParameter = sald_Id.HasValue ?
                new ObjectParameter("sald_Id", sald_Id) :
                new ObjectParameter("sald_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbSalidaDetalle_Delete_Result>("UDP_Inv_tbSalidaDetalle_Delete", sald_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbSalidaDetalle_Insert_Result> UDP_Inv_tbSalidaDetalle_Insert(Nullable<int> sal_Id, string prod_Codigo, Nullable<decimal> sald_Cantidad, string box_Codigo, Nullable<int> sald_UsuarioCrea, Nullable<System.DateTime> sald_FechaCrea)
        {
            var sal_IdParameter = sal_Id.HasValue ?
                new ObjectParameter("sal_Id", sal_Id) :
                new ObjectParameter("sal_Id", typeof(int));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var sald_CantidadParameter = sald_Cantidad.HasValue ?
                new ObjectParameter("sald_Cantidad", sald_Cantidad) :
                new ObjectParameter("sald_Cantidad", typeof(decimal));
    
            var box_CodigoParameter = box_Codigo != null ?
                new ObjectParameter("box_Codigo", box_Codigo) :
                new ObjectParameter("box_Codigo", typeof(string));
    
            var sald_UsuarioCreaParameter = sald_UsuarioCrea.HasValue ?
                new ObjectParameter("sald_UsuarioCrea", sald_UsuarioCrea) :
                new ObjectParameter("sald_UsuarioCrea", typeof(int));
    
            var sald_FechaCreaParameter = sald_FechaCrea.HasValue ?
                new ObjectParameter("sald_FechaCrea", sald_FechaCrea) :
                new ObjectParameter("sald_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbSalidaDetalle_Insert_Result>("UDP_Inv_tbSalidaDetalle_Insert", sal_IdParameter, prod_CodigoParameter, sald_CantidadParameter, box_CodigoParameter, sald_UsuarioCreaParameter, sald_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbSalidaDetalle_Update_Result> UDP_Inv_tbSalidaDetalle_Update(Nullable<int> sald_Id, Nullable<int> sal_Id, string prod_Codigo, Nullable<decimal> sald_Cantidad, string box_Codigo, Nullable<int> sald_UsuarioCrea, Nullable<System.DateTime> sald_FechaCrea, Nullable<int> sald_UsuarioModifica, Nullable<System.DateTime> sald_FechaModifica)
        {
            var sald_IdParameter = sald_Id.HasValue ?
                new ObjectParameter("sald_Id", sald_Id) :
                new ObjectParameter("sald_Id", typeof(int));
    
            var sal_IdParameter = sal_Id.HasValue ?
                new ObjectParameter("sal_Id", sal_Id) :
                new ObjectParameter("sal_Id", typeof(int));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var sald_CantidadParameter = sald_Cantidad.HasValue ?
                new ObjectParameter("sald_Cantidad", sald_Cantidad) :
                new ObjectParameter("sald_Cantidad", typeof(decimal));
    
            var box_CodigoParameter = box_Codigo != null ?
                new ObjectParameter("box_Codigo", box_Codigo) :
                new ObjectParameter("box_Codigo", typeof(string));
    
            var sald_UsuarioCreaParameter = sald_UsuarioCrea.HasValue ?
                new ObjectParameter("sald_UsuarioCrea", sald_UsuarioCrea) :
                new ObjectParameter("sald_UsuarioCrea", typeof(int));
    
            var sald_FechaCreaParameter = sald_FechaCrea.HasValue ?
                new ObjectParameter("sald_FechaCrea", sald_FechaCrea) :
                new ObjectParameter("sald_FechaCrea", typeof(System.DateTime));
    
            var sald_UsuarioModificaParameter = sald_UsuarioModifica.HasValue ?
                new ObjectParameter("sald_UsuarioModifica", sald_UsuarioModifica) :
                new ObjectParameter("sald_UsuarioModifica", typeof(int));
    
            var sald_FechaModificaParameter = sald_FechaModifica.HasValue ?
                new ObjectParameter("sald_FechaModifica", sald_FechaModifica) :
                new ObjectParameter("sald_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbSalidaDetalle_Update_Result>("UDP_Inv_tbSalidaDetalle_Update", sald_IdParameter, sal_IdParameter, prod_CodigoParameter, sald_CantidadParameter, box_CodigoParameter, sald_UsuarioCreaParameter, sald_FechaCreaParameter, sald_UsuarioModificaParameter, sald_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbTipoDevolucion_Insert_Result> UDP_Inv_tbTipoDevolucion_Insert(string tdev_Descripcion, Nullable<int> tdev_UsuarioCrea, Nullable<System.DateTime> tdev_FechaCrea)
        {
            var tdev_DescripcionParameter = tdev_Descripcion != null ?
                new ObjectParameter("tdev_Descripcion", tdev_Descripcion) :
                new ObjectParameter("tdev_Descripcion", typeof(string));
    
            var tdev_UsuarioCreaParameter = tdev_UsuarioCrea.HasValue ?
                new ObjectParameter("tdev_UsuarioCrea", tdev_UsuarioCrea) :
                new ObjectParameter("tdev_UsuarioCrea", typeof(int));
    
            var tdev_FechaCreaParameter = tdev_FechaCrea.HasValue ?
                new ObjectParameter("tdev_FechaCrea", tdev_FechaCrea) :
                new ObjectParameter("tdev_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbTipoDevolucion_Insert_Result>("UDP_Inv_tbTipoDevolucion_Insert", tdev_DescripcionParameter, tdev_UsuarioCreaParameter, tdev_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbTipoDevolucion_Update_Result> UDP_Inv_tbTipoDevolucion_Update(Nullable<int> tdev_Id, string tdev_Descripcion, Nullable<int> tdev_UsuarioCrea, Nullable<System.DateTime> tdev_FechaCrea, Nullable<int> tdev_UsuarioModifica, Nullable<System.DateTime> tdev_FechaModifica)
        {
            var tdev_IdParameter = tdev_Id.HasValue ?
                new ObjectParameter("tdev_Id", tdev_Id) :
                new ObjectParameter("tdev_Id", typeof(int));
    
            var tdev_DescripcionParameter = tdev_Descripcion != null ?
                new ObjectParameter("tdev_Descripcion", tdev_Descripcion) :
                new ObjectParameter("tdev_Descripcion", typeof(string));
    
            var tdev_UsuarioCreaParameter = tdev_UsuarioCrea.HasValue ?
                new ObjectParameter("tdev_UsuarioCrea", tdev_UsuarioCrea) :
                new ObjectParameter("tdev_UsuarioCrea", typeof(int));
    
            var tdev_FechaCreaParameter = tdev_FechaCrea.HasValue ?
                new ObjectParameter("tdev_FechaCrea", tdev_FechaCrea) :
                new ObjectParameter("tdev_FechaCrea", typeof(System.DateTime));
    
            var tdev_UsuarioModificaParameter = tdev_UsuarioModifica.HasValue ?
                new ObjectParameter("tdev_UsuarioModifica", tdev_UsuarioModifica) :
                new ObjectParameter("tdev_UsuarioModifica", typeof(int));
    
            var tdev_FechaModificaParameter = tdev_FechaModifica.HasValue ?
                new ObjectParameter("tdev_FechaModifica", tdev_FechaModifica) :
                new ObjectParameter("tdev_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbTipoDevolucion_Update_Result>("UDP_Inv_tbTipoDevolucion_Update", tdev_IdParameter, tdev_DescripcionParameter, tdev_UsuarioCreaParameter, tdev_FechaCreaParameter, tdev_UsuarioModificaParameter, tdev_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbTipoEntrada_Insert_Result> UDP_Inv_tbTipoEntrada_Insert(string tent_Descripcion, Nullable<int> tent_UsuarioCrea, Nullable<System.DateTime> tent_FechaCrea)
        {
            var tent_DescripcionParameter = tent_Descripcion != null ?
                new ObjectParameter("tent_Descripcion", tent_Descripcion) :
                new ObjectParameter("tent_Descripcion", typeof(string));
    
            var tent_UsuarioCreaParameter = tent_UsuarioCrea.HasValue ?
                new ObjectParameter("tent_UsuarioCrea", tent_UsuarioCrea) :
                new ObjectParameter("tent_UsuarioCrea", typeof(int));
    
            var tent_FechaCreaParameter = tent_FechaCrea.HasValue ?
                new ObjectParameter("tent_FechaCrea", tent_FechaCrea) :
                new ObjectParameter("tent_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbTipoEntrada_Insert_Result>("UDP_Inv_tbTipoEntrada_Insert", tent_DescripcionParameter, tent_UsuarioCreaParameter, tent_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbTipoEntrada_Update_Result> UDP_Inv_tbTipoEntrada_Update(Nullable<byte> tent_Id, string tent_Descripcion, Nullable<int> tent_UsuarioCrea, Nullable<System.DateTime> tent_FechaCrea, Nullable<int> tent_UsuarioModifica, Nullable<System.DateTime> tent_FechaModifica)
        {
            var tent_IdParameter = tent_Id.HasValue ?
                new ObjectParameter("tent_Id", tent_Id) :
                new ObjectParameter("tent_Id", typeof(byte));
    
            var tent_DescripcionParameter = tent_Descripcion != null ?
                new ObjectParameter("tent_Descripcion", tent_Descripcion) :
                new ObjectParameter("tent_Descripcion", typeof(string));
    
            var tent_UsuarioCreaParameter = tent_UsuarioCrea.HasValue ?
                new ObjectParameter("tent_UsuarioCrea", tent_UsuarioCrea) :
                new ObjectParameter("tent_UsuarioCrea", typeof(int));
    
            var tent_FechaCreaParameter = tent_FechaCrea.HasValue ?
                new ObjectParameter("tent_FechaCrea", tent_FechaCrea) :
                new ObjectParameter("tent_FechaCrea", typeof(System.DateTime));
    
            var tent_UsuarioModificaParameter = tent_UsuarioModifica.HasValue ?
                new ObjectParameter("tent_UsuarioModifica", tent_UsuarioModifica) :
                new ObjectParameter("tent_UsuarioModifica", typeof(int));
    
            var tent_FechaModificaParameter = tent_FechaModifica.HasValue ?
                new ObjectParameter("tent_FechaModifica", tent_FechaModifica) :
                new ObjectParameter("tent_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbTipoEntrada_Update_Result>("UDP_Inv_tbTipoEntrada_Update", tent_IdParameter, tent_DescripcionParameter, tent_UsuarioCreaParameter, tent_FechaCreaParameter, tent_UsuarioModificaParameter, tent_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbTipoSalida_Insert_Result> UDP_Inv_tbTipoSalida_Insert(string tsal_Descripcion, Nullable<int> tsal_UsuarioCrea, Nullable<System.DateTime> tsal_FechaCrea)
        {
            var tsal_DescripcionParameter = tsal_Descripcion != null ?
                new ObjectParameter("tsal_Descripcion", tsal_Descripcion) :
                new ObjectParameter("tsal_Descripcion", typeof(string));
    
            var tsal_UsuarioCreaParameter = tsal_UsuarioCrea.HasValue ?
                new ObjectParameter("tsal_UsuarioCrea", tsal_UsuarioCrea) :
                new ObjectParameter("tsal_UsuarioCrea", typeof(int));
    
            var tsal_FechaCreaParameter = tsal_FechaCrea.HasValue ?
                new ObjectParameter("tsal_FechaCrea", tsal_FechaCrea) :
                new ObjectParameter("tsal_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbTipoSalida_Insert_Result>("UDP_Inv_tbTipoSalida_Insert", tsal_DescripcionParameter, tsal_UsuarioCreaParameter, tsal_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbTipoSalida_Update_Result> UDP_Inv_tbTipoSalida_Update(Nullable<byte> tsal_Id, string tsal_Descripcion, Nullable<int> tsal_UsuarioCrea, Nullable<System.DateTime> tsal_FechaCrea, Nullable<int> tsal_UsuarioModifica, Nullable<System.DateTime> tsal_FechaModifica)
        {
            var tsal_IdParameter = tsal_Id.HasValue ?
                new ObjectParameter("tsal_Id", tsal_Id) :
                new ObjectParameter("tsal_Id", typeof(byte));
    
            var tsal_DescripcionParameter = tsal_Descripcion != null ?
                new ObjectParameter("tsal_Descripcion", tsal_Descripcion) :
                new ObjectParameter("tsal_Descripcion", typeof(string));
    
            var tsal_UsuarioCreaParameter = tsal_UsuarioCrea.HasValue ?
                new ObjectParameter("tsal_UsuarioCrea", tsal_UsuarioCrea) :
                new ObjectParameter("tsal_UsuarioCrea", typeof(int));
    
            var tsal_FechaCreaParameter = tsal_FechaCrea.HasValue ?
                new ObjectParameter("tsal_FechaCrea", tsal_FechaCrea) :
                new ObjectParameter("tsal_FechaCrea", typeof(System.DateTime));
    
            var tsal_UsuarioModificaParameter = tsal_UsuarioModifica.HasValue ?
                new ObjectParameter("tsal_UsuarioModifica", tsal_UsuarioModifica) :
                new ObjectParameter("tsal_UsuarioModifica", typeof(int));
    
            var tsal_FechaModificaParameter = tsal_FechaModifica.HasValue ?
                new ObjectParameter("tsal_FechaModifica", tsal_FechaModifica) :
                new ObjectParameter("tsal_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbTipoSalida_Update_Result>("UDP_Inv_tbTipoSalida_Update", tsal_IdParameter, tsal_DescripcionParameter, tsal_UsuarioCreaParameter, tsal_FechaCreaParameter, tsal_UsuarioModificaParameter, tsal_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_ValidacionCantidadExistente_Result> UDP_Inv_ValidacionCantidadExistente(Nullable<decimal> cantidadSolicitadad, Nullable<int> iDBodega, string iDProducto, Nullable<System.DateTime> fechaElaboracion, Nullable<int> bodegaDestino, Nullable<decimal> cantidadDisponible, Nullable<int> usuarioCrea, Nullable<System.DateTime> fechaCrea)
        {
            var cantidadSolicitadadParameter = cantidadSolicitadad.HasValue ?
                new ObjectParameter("CantidadSolicitadad", cantidadSolicitadad) :
                new ObjectParameter("CantidadSolicitadad", typeof(decimal));
    
            var iDBodegaParameter = iDBodega.HasValue ?
                new ObjectParameter("IDBodega", iDBodega) :
                new ObjectParameter("IDBodega", typeof(int));
    
            var iDProductoParameter = iDProducto != null ?
                new ObjectParameter("IDProducto", iDProducto) :
                new ObjectParameter("IDProducto", typeof(string));
    
            var fechaElaboracionParameter = fechaElaboracion.HasValue ?
                new ObjectParameter("FechaElaboracion", fechaElaboracion) :
                new ObjectParameter("FechaElaboracion", typeof(System.DateTime));
    
            var bodegaDestinoParameter = bodegaDestino.HasValue ?
                new ObjectParameter("BodegaDestino", bodegaDestino) :
                new ObjectParameter("BodegaDestino", typeof(int));
    
            var cantidadDisponibleParameter = cantidadDisponible.HasValue ?
                new ObjectParameter("CantidadDisponible", cantidadDisponible) :
                new ObjectParameter("CantidadDisponible", typeof(decimal));
    
            var usuarioCreaParameter = usuarioCrea.HasValue ?
                new ObjectParameter("UsuarioCrea", usuarioCrea) :
                new ObjectParameter("UsuarioCrea", typeof(int));
    
            var fechaCreaParameter = fechaCrea.HasValue ?
                new ObjectParameter("FechaCrea", fechaCrea) :
                new ObjectParameter("FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_ValidacionCantidadExistente_Result>("UDP_Inv_ValidacionCantidadExistente", cantidadSolicitadadParameter, iDBodegaParameter, iDProductoParameter, fechaElaboracionParameter, bodegaDestinoParameter, cantidadDisponibleParameter, usuarioCreaParameter, fechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_tbTipoPlanillaDetalleDeduccion_Insert_Result> UDP_tbTipoPlanillaDetalleDeduccion_Insert(Nullable<int> cde_IdDeducciones, Nullable<int> cpla_IdPlanilla, Nullable<int> tpdd_UsuarioCrea, Nullable<System.DateTime> tpdd_FechaCrea)
        {
            var cde_IdDeduccionesParameter = cde_IdDeducciones.HasValue ?
                new ObjectParameter("cde_IdDeducciones", cde_IdDeducciones) :
                new ObjectParameter("cde_IdDeducciones", typeof(int));
    
            var cpla_IdPlanillaParameter = cpla_IdPlanilla.HasValue ?
                new ObjectParameter("cpla_IdPlanilla", cpla_IdPlanilla) :
                new ObjectParameter("cpla_IdPlanilla", typeof(int));
    
            var tpdd_UsuarioCreaParameter = tpdd_UsuarioCrea.HasValue ?
                new ObjectParameter("tpdd_UsuarioCrea", tpdd_UsuarioCrea) :
                new ObjectParameter("tpdd_UsuarioCrea", typeof(int));
    
            var tpdd_FechaCreaParameter = tpdd_FechaCrea.HasValue ?
                new ObjectParameter("tpdd_FechaCrea", tpdd_FechaCrea) :
                new ObjectParameter("tpdd_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_tbTipoPlanillaDetalleDeduccion_Insert_Result>("UDP_tbTipoPlanillaDetalleDeduccion_Insert", cde_IdDeduccionesParameter, cpla_IdPlanillaParameter, tpdd_UsuarioCreaParameter, tpdd_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_tbTipoPlanillaDetalleDeduccion_Update_Result> UDP_tbTipoPlanillaDetalleDeduccion_Update(Nullable<int> cpla_IdPlanilla, Nullable<int> cde_IdDeducciones)
        {
            var cpla_IdPlanillaParameter = cpla_IdPlanilla.HasValue ?
                new ObjectParameter("cpla_IdPlanilla", cpla_IdPlanilla) :
                new ObjectParameter("cpla_IdPlanilla", typeof(int));
    
            var cde_IdDeduccionesParameter = cde_IdDeducciones.HasValue ?
                new ObjectParameter("cde_IdDeducciones", cde_IdDeducciones) :
                new ObjectParameter("cde_IdDeducciones", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_tbTipoPlanillaDetalleDeduccion_Update_Result>("UDP_tbTipoPlanillaDetalleDeduccion_Update", cpla_IdPlanillaParameter, cde_IdDeduccionesParameter);
        }
    
        public virtual ObjectResult<rrhh_tbCompetenciasRequisicion_Delete_Result> rrhh_tbCompetenciasRequisicion_Delete(Nullable<int> creq_Id, Nullable<int> creq_UsuarioModifica, Nullable<System.DateTime> creq_FechaModifica)
        {
            var creq_IdParameter = creq_Id.HasValue ?
                new ObjectParameter("creq_Id", creq_Id) :
                new ObjectParameter("creq_Id", typeof(int));
    
            var creq_UsuarioModificaParameter = creq_UsuarioModifica.HasValue ?
                new ObjectParameter("creq_UsuarioModifica", creq_UsuarioModifica) :
                new ObjectParameter("creq_UsuarioModifica", typeof(int));
    
            var creq_FechaModificaParameter = creq_FechaModifica.HasValue ?
                new ObjectParameter("creq_FechaModifica", creq_FechaModifica) :
                new ObjectParameter("creq_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rrhh_tbCompetenciasRequisicion_Delete_Result>("rrhh_tbCompetenciasRequisicion_Delete", creq_IdParameter, creq_UsuarioModificaParameter, creq_FechaModificaParameter);
        }
    
        public virtual ObjectResult<rrhh_tbCompetenciasRequisicion_Insert_Result> rrhh_tbCompetenciasRequisicion_Insert(Nullable<int> req_Id, Nullable<int> comp_Id, Nullable<int> creq_UsuarioCrea, Nullable<System.DateTime> creq_FechaCrea)
        {
            var req_IdParameter = req_Id.HasValue ?
                new ObjectParameter("req_Id", req_Id) :
                new ObjectParameter("req_Id", typeof(int));
    
            var comp_IdParameter = comp_Id.HasValue ?
                new ObjectParameter("comp_Id", comp_Id) :
                new ObjectParameter("comp_Id", typeof(int));
    
            var creq_UsuarioCreaParameter = creq_UsuarioCrea.HasValue ?
                new ObjectParameter("creq_UsuarioCrea", creq_UsuarioCrea) :
                new ObjectParameter("creq_UsuarioCrea", typeof(int));
    
            var creq_FechaCreaParameter = creq_FechaCrea.HasValue ?
                new ObjectParameter("creq_FechaCrea", creq_FechaCrea) :
                new ObjectParameter("creq_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rrhh_tbCompetenciasRequisicion_Insert_Result>("rrhh_tbCompetenciasRequisicion_Insert", req_IdParameter, comp_IdParameter, creq_UsuarioCreaParameter, creq_FechaCreaParameter);
        }
    
        public virtual ObjectResult<rrhh_tbCompetenciasRequisicion_Restore_Result> rrhh_tbCompetenciasRequisicion_Restore(Nullable<int> creq_Id, Nullable<int> creq_UsuarioModifica, Nullable<System.DateTime> creq_FechaModifica)
        {
            var creq_IdParameter = creq_Id.HasValue ?
                new ObjectParameter("creq_Id", creq_Id) :
                new ObjectParameter("creq_Id", typeof(int));
    
            var creq_UsuarioModificaParameter = creq_UsuarioModifica.HasValue ?
                new ObjectParameter("creq_UsuarioModifica", creq_UsuarioModifica) :
                new ObjectParameter("creq_UsuarioModifica", typeof(int));
    
            var creq_FechaModificaParameter = creq_FechaModifica.HasValue ?
                new ObjectParameter("creq_FechaModifica", creq_FechaModifica) :
                new ObjectParameter("creq_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rrhh_tbCompetenciasRequisicion_Restore_Result>("rrhh_tbCompetenciasRequisicion_Restore", creq_IdParameter, creq_UsuarioModificaParameter, creq_FechaModificaParameter);
        }
    
        public virtual ObjectResult<rrhh_tbHabilidadesRequisicion_Delete_Result> rrhh_tbHabilidadesRequisicion_Delete(Nullable<int> hreq_Id, Nullable<int> hreq_UsuarioModifica, Nullable<System.DateTime> hreq_FechaModifica)
        {
            var hreq_IdParameter = hreq_Id.HasValue ?
                new ObjectParameter("hreq_Id", hreq_Id) :
                new ObjectParameter("hreq_Id", typeof(int));
    
            var hreq_UsuarioModificaParameter = hreq_UsuarioModifica.HasValue ?
                new ObjectParameter("hreq_UsuarioModifica", hreq_UsuarioModifica) :
                new ObjectParameter("hreq_UsuarioModifica", typeof(int));
    
            var hreq_FechaModificaParameter = hreq_FechaModifica.HasValue ?
                new ObjectParameter("hreq_FechaModifica", hreq_FechaModifica) :
                new ObjectParameter("hreq_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rrhh_tbHabilidadesRequisicion_Delete_Result>("rrhh_tbHabilidadesRequisicion_Delete", hreq_IdParameter, hreq_UsuarioModificaParameter, hreq_FechaModificaParameter);
        }
    
        public virtual ObjectResult<rrhh_tbHabilidadesRequisicion_Insert_Result> rrhh_tbHabilidadesRequisicion_Insert(Nullable<int> req_Id, Nullable<int> habi_Id, Nullable<int> hreq_UsuarioCrea, Nullable<System.DateTime> hreq_FechaCrea)
        {
            var req_IdParameter = req_Id.HasValue ?
                new ObjectParameter("req_Id", req_Id) :
                new ObjectParameter("req_Id", typeof(int));
    
            var habi_IdParameter = habi_Id.HasValue ?
                new ObjectParameter("habi_Id", habi_Id) :
                new ObjectParameter("habi_Id", typeof(int));
    
            var hreq_UsuarioCreaParameter = hreq_UsuarioCrea.HasValue ?
                new ObjectParameter("hreq_UsuarioCrea", hreq_UsuarioCrea) :
                new ObjectParameter("hreq_UsuarioCrea", typeof(int));
    
            var hreq_FechaCreaParameter = hreq_FechaCrea.HasValue ?
                new ObjectParameter("hreq_FechaCrea", hreq_FechaCrea) :
                new ObjectParameter("hreq_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rrhh_tbHabilidadesRequisicion_Insert_Result>("rrhh_tbHabilidadesRequisicion_Insert", req_IdParameter, habi_IdParameter, hreq_UsuarioCreaParameter, hreq_FechaCreaParameter);
        }
    
        public virtual ObjectResult<rrhh_tbHabilidadesRequisicion_Restore_Result> rrhh_tbHabilidadesRequisicion_Restore(Nullable<int> hreq_Id, Nullable<int> hreq_UsuarioModifica, Nullable<System.DateTime> hreq_FechaModifica)
        {
            var hreq_IdParameter = hreq_Id.HasValue ?
                new ObjectParameter("hreq_Id", hreq_Id) :
                new ObjectParameter("hreq_Id", typeof(int));
    
            var hreq_UsuarioModificaParameter = hreq_UsuarioModifica.HasValue ?
                new ObjectParameter("hreq_UsuarioModifica", hreq_UsuarioModifica) :
                new ObjectParameter("hreq_UsuarioModifica", typeof(int));
    
            var hreq_FechaModificaParameter = hreq_FechaModifica.HasValue ?
                new ObjectParameter("hreq_FechaModifica", hreq_FechaModifica) :
                new ObjectParameter("hreq_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rrhh_tbHabilidadesRequisicion_Restore_Result>("rrhh_tbHabilidadesRequisicion_Restore", hreq_IdParameter, hreq_UsuarioModificaParameter, hreq_FechaModificaParameter);
        }
    
        public virtual ObjectResult<rrhh_tbIdiomasRequisicion_Delete_Result> rrhh_tbIdiomasRequisicion_Delete(Nullable<int> ireq_Id, Nullable<int> ireq_UsuarioModifica, Nullable<System.DateTime> ireq_FechaModifica)
        {
            var ireq_IdParameter = ireq_Id.HasValue ?
                new ObjectParameter("ireq_Id", ireq_Id) :
                new ObjectParameter("ireq_Id", typeof(int));
    
            var ireq_UsuarioModificaParameter = ireq_UsuarioModifica.HasValue ?
                new ObjectParameter("ireq_UsuarioModifica", ireq_UsuarioModifica) :
                new ObjectParameter("ireq_UsuarioModifica", typeof(int));
    
            var ireq_FechaModificaParameter = ireq_FechaModifica.HasValue ?
                new ObjectParameter("ireq_FechaModifica", ireq_FechaModifica) :
                new ObjectParameter("ireq_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rrhh_tbIdiomasRequisicion_Delete_Result>("rrhh_tbIdiomasRequisicion_Delete", ireq_IdParameter, ireq_UsuarioModificaParameter, ireq_FechaModificaParameter);
        }
    
        public virtual ObjectResult<rrhh_tbIdiomasRequisicion_Insert_Result> rrhh_tbIdiomasRequisicion_Insert(Nullable<int> req_Id, Nullable<int> idi_Id, Nullable<int> ireq_UsuarioCrea, Nullable<System.DateTime> ireq_FechaCrea)
        {
            var req_IdParameter = req_Id.HasValue ?
                new ObjectParameter("req_Id", req_Id) :
                new ObjectParameter("req_Id", typeof(int));
    
            var idi_IdParameter = idi_Id.HasValue ?
                new ObjectParameter("idi_Id", idi_Id) :
                new ObjectParameter("idi_Id", typeof(int));
    
            var ireq_UsuarioCreaParameter = ireq_UsuarioCrea.HasValue ?
                new ObjectParameter("ireq_UsuarioCrea", ireq_UsuarioCrea) :
                new ObjectParameter("ireq_UsuarioCrea", typeof(int));
    
            var ireq_FechaCreaParameter = ireq_FechaCrea.HasValue ?
                new ObjectParameter("ireq_FechaCrea", ireq_FechaCrea) :
                new ObjectParameter("ireq_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rrhh_tbIdiomasRequisicion_Insert_Result>("rrhh_tbIdiomasRequisicion_Insert", req_IdParameter, idi_IdParameter, ireq_UsuarioCreaParameter, ireq_FechaCreaParameter);
        }
    
        public virtual ObjectResult<rrhh_tbIdiomasRequisicion_Restore_Result> rrhh_tbIdiomasRequisicion_Restore(Nullable<int> ireq_Id, Nullable<int> ireq_UsuarioModifica, Nullable<System.DateTime> ireq_FechaModifica)
        {
            var ireq_IdParameter = ireq_Id.HasValue ?
                new ObjectParameter("ireq_Id", ireq_Id) :
                new ObjectParameter("ireq_Id", typeof(int));
    
            var ireq_UsuarioModificaParameter = ireq_UsuarioModifica.HasValue ?
                new ObjectParameter("ireq_UsuarioModifica", ireq_UsuarioModifica) :
                new ObjectParameter("ireq_UsuarioModifica", typeof(int));
    
            var ireq_FechaModificaParameter = ireq_FechaModifica.HasValue ?
                new ObjectParameter("ireq_FechaModifica", ireq_FechaModifica) :
                new ObjectParameter("ireq_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rrhh_tbIdiomasRequisicion_Restore_Result>("rrhh_tbIdiomasRequisicion_Restore", ireq_IdParameter, ireq_UsuarioModificaParameter, ireq_FechaModificaParameter);
        }
    
        public virtual ObjectResult<rrhh_tbRequerimientosEspecialesRequisicion_Delete_Result> rrhh_tbRequerimientosEspecialesRequisicion_Delete(Nullable<int> rer_Id, Nullable<int> rer_UsuarioModifica, Nullable<System.DateTime> rer_FechaModifica)
        {
            var rer_IdParameter = rer_Id.HasValue ?
                new ObjectParameter("rer_Id", rer_Id) :
                new ObjectParameter("rer_Id", typeof(int));
    
            var rer_UsuarioModificaParameter = rer_UsuarioModifica.HasValue ?
                new ObjectParameter("rer_UsuarioModifica", rer_UsuarioModifica) :
                new ObjectParameter("rer_UsuarioModifica", typeof(int));
    
            var rer_FechaModificaParameter = rer_FechaModifica.HasValue ?
                new ObjectParameter("rer_FechaModifica", rer_FechaModifica) :
                new ObjectParameter("rer_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rrhh_tbRequerimientosEspecialesRequisicion_Delete_Result>("rrhh_tbRequerimientosEspecialesRequisicion_Delete", rer_IdParameter, rer_UsuarioModificaParameter, rer_FechaModificaParameter);
        }
    
        public virtual ObjectResult<rrhh_tbRequerimientosEspecialesRequisicion_Insert_Result> rrhh_tbRequerimientosEspecialesRequisicion_Insert(Nullable<int> req_Id, Nullable<int> resp_Id, Nullable<int> rer_UsuarioCrea, Nullable<System.DateTime> rer_FechaCrea)
        {
            var req_IdParameter = req_Id.HasValue ?
                new ObjectParameter("req_Id", req_Id) :
                new ObjectParameter("req_Id", typeof(int));
    
            var resp_IdParameter = resp_Id.HasValue ?
                new ObjectParameter("resp_Id", resp_Id) :
                new ObjectParameter("resp_Id", typeof(int));
    
            var rer_UsuarioCreaParameter = rer_UsuarioCrea.HasValue ?
                new ObjectParameter("rer_UsuarioCrea", rer_UsuarioCrea) :
                new ObjectParameter("rer_UsuarioCrea", typeof(int));
    
            var rer_FechaCreaParameter = rer_FechaCrea.HasValue ?
                new ObjectParameter("rer_FechaCrea", rer_FechaCrea) :
                new ObjectParameter("rer_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rrhh_tbRequerimientosEspecialesRequisicion_Insert_Result>("rrhh_tbRequerimientosEspecialesRequisicion_Insert", req_IdParameter, resp_IdParameter, rer_UsuarioCreaParameter, rer_FechaCreaParameter);
        }
    
        public virtual ObjectResult<rrhh_tbRequerimientosEspecialesRequisicion_Restore_Result> rrhh_tbRequerimientosEspecialesRequisicion_Restore(Nullable<int> rer_Id, Nullable<int> rer_UsuarioModifica, Nullable<System.DateTime> rer_FechaModifica)
        {
            var rer_IdParameter = rer_Id.HasValue ?
                new ObjectParameter("rer_Id", rer_Id) :
                new ObjectParameter("rer_Id", typeof(int));
    
            var rer_UsuarioModificaParameter = rer_UsuarioModifica.HasValue ?
                new ObjectParameter("rer_UsuarioModifica", rer_UsuarioModifica) :
                new ObjectParameter("rer_UsuarioModifica", typeof(int));
    
            var rer_FechaModificaParameter = rer_FechaModifica.HasValue ?
                new ObjectParameter("rer_FechaModifica", rer_FechaModifica) :
                new ObjectParameter("rer_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rrhh_tbRequerimientosEspecialesRequisicion_Restore_Result>("rrhh_tbRequerimientosEspecialesRequisicion_Restore", rer_IdParameter, rer_UsuarioModificaParameter, rer_FechaModificaParameter);
        }
    
        public virtual ObjectResult<rrhh_tbTitulosRequisicion_Delete_Result> rrhh_tbTitulosRequisicion_Delete(Nullable<int> treq_Id, Nullable<int> tipe_UsuarioModifica, Nullable<System.DateTime> tipe_FechaModifica)
        {
            var treq_IdParameter = treq_Id.HasValue ?
                new ObjectParameter("treq_Id", treq_Id) :
                new ObjectParameter("treq_Id", typeof(int));
    
            var tipe_UsuarioModificaParameter = tipe_UsuarioModifica.HasValue ?
                new ObjectParameter("tipe_UsuarioModifica", tipe_UsuarioModifica) :
                new ObjectParameter("tipe_UsuarioModifica", typeof(int));
    
            var tipe_FechaModificaParameter = tipe_FechaModifica.HasValue ?
                new ObjectParameter("tipe_FechaModifica", tipe_FechaModifica) :
                new ObjectParameter("tipe_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rrhh_tbTitulosRequisicion_Delete_Result>("rrhh_tbTitulosRequisicion_Delete", treq_IdParameter, tipe_UsuarioModificaParameter, tipe_FechaModificaParameter);
        }
    
        public virtual ObjectResult<rrhh_tbTitulosRequisicion_Insert_Result> rrhh_tbTitulosRequisicion_Insert(Nullable<int> req_Id, Nullable<int> titu_Id, Nullable<int> treq_UsuarioCrea, Nullable<System.DateTime> treq_FechaCrea)
        {
            var req_IdParameter = req_Id.HasValue ?
                new ObjectParameter("req_Id", req_Id) :
                new ObjectParameter("req_Id", typeof(int));
    
            var titu_IdParameter = titu_Id.HasValue ?
                new ObjectParameter("titu_Id", titu_Id) :
                new ObjectParameter("titu_Id", typeof(int));
    
            var treq_UsuarioCreaParameter = treq_UsuarioCrea.HasValue ?
                new ObjectParameter("treq_UsuarioCrea", treq_UsuarioCrea) :
                new ObjectParameter("treq_UsuarioCrea", typeof(int));
    
            var treq_FechaCreaParameter = treq_FechaCrea.HasValue ?
                new ObjectParameter("treq_FechaCrea", treq_FechaCrea) :
                new ObjectParameter("treq_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rrhh_tbTitulosRequisicion_Insert_Result>("rrhh_tbTitulosRequisicion_Insert", req_IdParameter, titu_IdParameter, treq_UsuarioCreaParameter, treq_FechaCreaParameter);
        }
    
        public virtual ObjectResult<rrhh_tbTitulosRequisicion_Restore_Result> rrhh_tbTitulosRequisicion_Restore(Nullable<int> treq_Id, Nullable<int> tipe_UsuarioModifica, Nullable<System.DateTime> tipe_FechaModifica)
        {
            var treq_IdParameter = treq_Id.HasValue ?
                new ObjectParameter("treq_Id", treq_Id) :
                new ObjectParameter("treq_Id", typeof(int));
    
            var tipe_UsuarioModificaParameter = tipe_UsuarioModifica.HasValue ?
                new ObjectParameter("tipe_UsuarioModifica", tipe_UsuarioModifica) :
                new ObjectParameter("tipe_UsuarioModifica", typeof(int));
    
            var tipe_FechaModificaParameter = tipe_FechaModifica.HasValue ?
                new ObjectParameter("tipe_FechaModifica", tipe_FechaModifica) :
                new ObjectParameter("tipe_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rrhh_tbTitulosRequisicion_Restore_Result>("rrhh_tbTitulosRequisicion_Restore", treq_IdParameter, tipe_UsuarioModificaParameter, tipe_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbAreas_Delete_Result> UDP_RRHH_tbAreas_Delete(Nullable<int> area_Id, string area_Razoninactivo, Nullable<int> area_Usuariomodifica, Nullable<System.DateTime> area_Fechamodifica)
        {
            var area_IdParameter = area_Id.HasValue ?
                new ObjectParameter("area_Id", area_Id) :
                new ObjectParameter("area_Id", typeof(int));
    
            var area_RazoninactivoParameter = area_Razoninactivo != null ?
                new ObjectParameter("area_Razoninactivo", area_Razoninactivo) :
                new ObjectParameter("area_Razoninactivo", typeof(string));
    
            var area_UsuariomodificaParameter = area_Usuariomodifica.HasValue ?
                new ObjectParameter("area_Usuariomodifica", area_Usuariomodifica) :
                new ObjectParameter("area_Usuariomodifica", typeof(int));
    
            var area_FechamodificaParameter = area_Fechamodifica.HasValue ?
                new ObjectParameter("area_Fechamodifica", area_Fechamodifica) :
                new ObjectParameter("area_Fechamodifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbAreas_Delete_Result>("UDP_RRHH_tbAreas_Delete", area_IdParameter, area_RazoninactivoParameter, area_UsuariomodificaParameter, area_FechamodificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbAreas_Insert_Result> UDP_RRHH_tbAreas_Insert(Nullable<int> suc_Id, string area_Descripcion, Nullable<int> car_Id, Nullable<int> area_Usuariocrea, Nullable<System.DateTime> area_Fechacrea)
        {
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var area_DescripcionParameter = area_Descripcion != null ?
                new ObjectParameter("area_Descripcion", area_Descripcion) :
                new ObjectParameter("area_Descripcion", typeof(string));
    
            var car_IdParameter = car_Id.HasValue ?
                new ObjectParameter("car_Id", car_Id) :
                new ObjectParameter("car_Id", typeof(int));
    
            var area_UsuariocreaParameter = area_Usuariocrea.HasValue ?
                new ObjectParameter("area_Usuariocrea", area_Usuariocrea) :
                new ObjectParameter("area_Usuariocrea", typeof(int));
    
            var area_FechacreaParameter = area_Fechacrea.HasValue ?
                new ObjectParameter("area_Fechacrea", area_Fechacrea) :
                new ObjectParameter("area_Fechacrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbAreas_Insert_Result>("UDP_RRHH_tbAreas_Insert", suc_IdParameter, area_DescripcionParameter, car_IdParameter, area_UsuariocreaParameter, area_FechacreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbAreas_Restore_Result> UDP_RRHH_tbAreas_Restore(Nullable<int> area_Id, Nullable<int> area_Usuariomodifica, Nullable<System.DateTime> area_Fechamodifica)
        {
            var area_IdParameter = area_Id.HasValue ?
                new ObjectParameter("area_Id", area_Id) :
                new ObjectParameter("area_Id", typeof(int));
    
            var area_UsuariomodificaParameter = area_Usuariomodifica.HasValue ?
                new ObjectParameter("area_Usuariomodifica", area_Usuariomodifica) :
                new ObjectParameter("area_Usuariomodifica", typeof(int));
    
            var area_FechamodificaParameter = area_Fechamodifica.HasValue ?
                new ObjectParameter("area_Fechamodifica", area_Fechamodifica) :
                new ObjectParameter("area_Fechamodifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbAreas_Restore_Result>("UDP_RRHH_tbAreas_Restore", area_IdParameter, area_UsuariomodificaParameter, area_FechamodificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbAreas_Update_Result> UDP_RRHH_tbAreas_Update(Nullable<int> area_Id, Nullable<int> suc_Id, string area_Descripcion, Nullable<int> area_Usuariomodifica, Nullable<System.DateTime> area_Fechamodifica)
        {
            var area_IdParameter = area_Id.HasValue ?
                new ObjectParameter("area_Id", area_Id) :
                new ObjectParameter("area_Id", typeof(int));
    
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var area_DescripcionParameter = area_Descripcion != null ?
                new ObjectParameter("area_Descripcion", area_Descripcion) :
                new ObjectParameter("area_Descripcion", typeof(string));
    
            var area_UsuariomodificaParameter = area_Usuariomodifica.HasValue ?
                new ObjectParameter("area_Usuariomodifica", area_Usuariomodifica) :
                new ObjectParameter("area_Usuariomodifica", typeof(int));
    
            var area_FechamodificaParameter = area_Fechamodifica.HasValue ?
                new ObjectParameter("area_Fechamodifica", area_Fechamodifica) :
                new ObjectParameter("area_Fechamodifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbAreas_Update_Result>("UDP_RRHH_tbAreas_Update", area_IdParameter, suc_IdParameter, area_DescripcionParameter, area_UsuariomodificaParameter, area_FechamodificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbCargos_Delete_Result> UDP_RRHH_tbCargos_Delete(Nullable<int> car_Id, string car_razon_Inactivo, Nullable<int> car_UsuarioModifica, Nullable<System.DateTime> car_FechaModifica)
        {
            var car_IdParameter = car_Id.HasValue ?
                new ObjectParameter("car_Id", car_Id) :
                new ObjectParameter("car_Id", typeof(int));
    
            var car_razon_InactivoParameter = car_razon_Inactivo != null ?
                new ObjectParameter("car_razon_Inactivo", car_razon_Inactivo) :
                new ObjectParameter("car_razon_Inactivo", typeof(string));
    
            var car_UsuarioModificaParameter = car_UsuarioModifica.HasValue ?
                new ObjectParameter("car_UsuarioModifica", car_UsuarioModifica) :
                new ObjectParameter("car_UsuarioModifica", typeof(int));
    
            var car_FechaModificaParameter = car_FechaModifica.HasValue ?
                new ObjectParameter("car_FechaModifica", car_FechaModifica) :
                new ObjectParameter("car_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbCargos_Delete_Result>("UDP_RRHH_tbCargos_Delete", car_IdParameter, car_razon_InactivoParameter, car_UsuarioModificaParameter, car_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbCargos_Insert_Result> UDP_RRHH_tbCargos_Insert(string car_Descripcion, Nullable<decimal> car_SueldoMinimo, Nullable<decimal> car_SueldoMaximo, Nullable<int> car_UsuarioCrea, Nullable<System.DateTime> car_FechaCrea)
        {
            var car_DescripcionParameter = car_Descripcion != null ?
                new ObjectParameter("car_Descripcion", car_Descripcion) :
                new ObjectParameter("car_Descripcion", typeof(string));
    
            var car_SueldoMinimoParameter = car_SueldoMinimo.HasValue ?
                new ObjectParameter("car_SueldoMinimo", car_SueldoMinimo) :
                new ObjectParameter("car_SueldoMinimo", typeof(decimal));
    
            var car_SueldoMaximoParameter = car_SueldoMaximo.HasValue ?
                new ObjectParameter("car_SueldoMaximo", car_SueldoMaximo) :
                new ObjectParameter("car_SueldoMaximo", typeof(decimal));
    
            var car_UsuarioCreaParameter = car_UsuarioCrea.HasValue ?
                new ObjectParameter("car_UsuarioCrea", car_UsuarioCrea) :
                new ObjectParameter("car_UsuarioCrea", typeof(int));
    
            var car_FechaCreaParameter = car_FechaCrea.HasValue ?
                new ObjectParameter("car_FechaCrea", car_FechaCrea) :
                new ObjectParameter("car_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbCargos_Insert_Result>("UDP_RRHH_tbCargos_Insert", car_DescripcionParameter, car_SueldoMinimoParameter, car_SueldoMaximoParameter, car_UsuarioCreaParameter, car_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbCargos_Restore_Result> UDP_RRHH_tbCargos_Restore(Nullable<int> car_Id, Nullable<int> car_UsuarioModifica, Nullable<System.DateTime> car_FechaModifica)
        {
            var car_IdParameter = car_Id.HasValue ?
                new ObjectParameter("car_Id", car_Id) :
                new ObjectParameter("car_Id", typeof(int));
    
            var car_UsuarioModificaParameter = car_UsuarioModifica.HasValue ?
                new ObjectParameter("car_UsuarioModifica", car_UsuarioModifica) :
                new ObjectParameter("car_UsuarioModifica", typeof(int));
    
            var car_FechaModificaParameter = car_FechaModifica.HasValue ?
                new ObjectParameter("car_FechaModifica", car_FechaModifica) :
                new ObjectParameter("car_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbCargos_Restore_Result>("UDP_RRHH_tbCargos_Restore", car_IdParameter, car_UsuarioModificaParameter, car_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbCargos_tbEmpleados_Select_Result> UDP_RRHH_tbCargos_tbEmpleados_Select()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbCargos_tbEmpleados_Select_Result>("UDP_RRHH_tbCargos_tbEmpleados_Select");
        }
    
        public virtual ObjectResult<UDP_RRHH_tbCargos_Update_Result> UDP_RRHH_tbCargos_Update(Nullable<int> car_Id, string car_Descripcion, Nullable<decimal> car_SueldoMinimo, Nullable<decimal> car_SueldoMaximo, Nullable<int> car_UsuarioModifica, Nullable<System.DateTime> car_FechaModifica)
        {
            var car_IdParameter = car_Id.HasValue ?
                new ObjectParameter("car_Id", car_Id) :
                new ObjectParameter("car_Id", typeof(int));
    
            var car_DescripcionParameter = car_Descripcion != null ?
                new ObjectParameter("car_Descripcion", car_Descripcion) :
                new ObjectParameter("car_Descripcion", typeof(string));
    
            var car_SueldoMinimoParameter = car_SueldoMinimo.HasValue ?
                new ObjectParameter("car_SueldoMinimo", car_SueldoMinimo) :
                new ObjectParameter("car_SueldoMinimo", typeof(decimal));
    
            var car_SueldoMaximoParameter = car_SueldoMaximo.HasValue ?
                new ObjectParameter("car_SueldoMaximo", car_SueldoMaximo) :
                new ObjectParameter("car_SueldoMaximo", typeof(decimal));
    
            var car_UsuarioModificaParameter = car_UsuarioModifica.HasValue ?
                new ObjectParameter("car_UsuarioModifica", car_UsuarioModifica) :
                new ObjectParameter("car_UsuarioModifica", typeof(int));
    
            var car_FechaModificaParameter = car_FechaModifica.HasValue ?
                new ObjectParameter("car_FechaModifica", car_FechaModifica) :
                new ObjectParameter("car_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbCargos_Update_Result>("UDP_RRHH_tbCargos_Update", car_IdParameter, car_DescripcionParameter, car_SueldoMinimoParameter, car_SueldoMaximoParameter, car_UsuarioModificaParameter, car_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbCompetencias_Delete_Result> UDP_RRHH_tbCompetencias_Delete(Nullable<int> comp_Id, string comp_razon_Inactivo, Nullable<int> comp_UsuarioModifica, Nullable<System.DateTime> comp_FechaModifica)
        {
            var comp_IdParameter = comp_Id.HasValue ?
                new ObjectParameter("comp_Id", comp_Id) :
                new ObjectParameter("comp_Id", typeof(int));
    
            var comp_razon_InactivoParameter = comp_razon_Inactivo != null ?
                new ObjectParameter("comp_razon_Inactivo", comp_razon_Inactivo) :
                new ObjectParameter("comp_razon_Inactivo", typeof(string));
    
            var comp_UsuarioModificaParameter = comp_UsuarioModifica.HasValue ?
                new ObjectParameter("comp_UsuarioModifica", comp_UsuarioModifica) :
                new ObjectParameter("comp_UsuarioModifica", typeof(int));
    
            var comp_FechaModificaParameter = comp_FechaModifica.HasValue ?
                new ObjectParameter("comp_FechaModifica", comp_FechaModifica) :
                new ObjectParameter("comp_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbCompetencias_Delete_Result>("UDP_RRHH_tbCompetencias_Delete", comp_IdParameter, comp_razon_InactivoParameter, comp_UsuarioModificaParameter, comp_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbCompetencias_Insert_Result> UDP_RRHH_tbCompetencias_Insert(string comp_Descripcion, Nullable<int> comp_UsuarioCrea, Nullable<System.DateTime> comp_FechaCrea)
        {
            var comp_DescripcionParameter = comp_Descripcion != null ?
                new ObjectParameter("comp_Descripcion", comp_Descripcion) :
                new ObjectParameter("comp_Descripcion", typeof(string));
    
            var comp_UsuarioCreaParameter = comp_UsuarioCrea.HasValue ?
                new ObjectParameter("comp_UsuarioCrea", comp_UsuarioCrea) :
                new ObjectParameter("comp_UsuarioCrea", typeof(int));
    
            var comp_FechaCreaParameter = comp_FechaCrea.HasValue ?
                new ObjectParameter("comp_FechaCrea", comp_FechaCrea) :
                new ObjectParameter("comp_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbCompetencias_Insert_Result>("UDP_RRHH_tbCompetencias_Insert", comp_DescripcionParameter, comp_UsuarioCreaParameter, comp_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbCompetencias_Restore_Result> UDP_RRHH_tbCompetencias_Restore(Nullable<int> comp_Id, Nullable<int> comp_UsuarioModifica, Nullable<System.DateTime> comp_FechaModifica)
        {
            var comp_IdParameter = comp_Id.HasValue ?
                new ObjectParameter("comp_Id", comp_Id) :
                new ObjectParameter("comp_Id", typeof(int));
    
            var comp_UsuarioModificaParameter = comp_UsuarioModifica.HasValue ?
                new ObjectParameter("comp_UsuarioModifica", comp_UsuarioModifica) :
                new ObjectParameter("comp_UsuarioModifica", typeof(int));
    
            var comp_FechaModificaParameter = comp_FechaModifica.HasValue ?
                new ObjectParameter("comp_FechaModifica", comp_FechaModifica) :
                new ObjectParameter("comp_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbCompetencias_Restore_Result>("UDP_RRHH_tbCompetencias_Restore", comp_IdParameter, comp_UsuarioModificaParameter, comp_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbCompetencias_Update_Result> UDP_RRHH_tbCompetencias_Update(Nullable<int> comp_Id, string comp_Descripcion, Nullable<int> comp_UsuarioModifica, Nullable<System.DateTime> comp_FechaModifica)
        {
            var comp_IdParameter = comp_Id.HasValue ?
                new ObjectParameter("comp_Id", comp_Id) :
                new ObjectParameter("comp_Id", typeof(int));
    
            var comp_DescripcionParameter = comp_Descripcion != null ?
                new ObjectParameter("comp_Descripcion", comp_Descripcion) :
                new ObjectParameter("comp_Descripcion", typeof(string));
    
            var comp_UsuarioModificaParameter = comp_UsuarioModifica.HasValue ?
                new ObjectParameter("comp_UsuarioModifica", comp_UsuarioModifica) :
                new ObjectParameter("comp_UsuarioModifica", typeof(int));
    
            var comp_FechaModificaParameter = comp_FechaModifica.HasValue ?
                new ObjectParameter("comp_FechaModifica", comp_FechaModifica) :
                new ObjectParameter("comp_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbCompetencias_Update_Result>("UDP_RRHH_tbCompetencias_Update", comp_IdParameter, comp_DescripcionParameter, comp_UsuarioModificaParameter, comp_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbCompetenciasPersona_Inactivar_Result> UDP_RRHH_tbCompetenciasPersona_Inactivar(Nullable<int> cope_Id, string cope_RazonInactivo, Nullable<int> cope_UsuarioModifica, Nullable<System.DateTime> cope_FechaModifica)
        {
            var cope_IdParameter = cope_Id.HasValue ?
                new ObjectParameter("cope_Id", cope_Id) :
                new ObjectParameter("cope_Id", typeof(int));
    
            var cope_RazonInactivoParameter = cope_RazonInactivo != null ?
                new ObjectParameter("cope_RazonInactivo", cope_RazonInactivo) :
                new ObjectParameter("cope_RazonInactivo", typeof(string));
    
            var cope_UsuarioModificaParameter = cope_UsuarioModifica.HasValue ?
                new ObjectParameter("cope_UsuarioModifica", cope_UsuarioModifica) :
                new ObjectParameter("cope_UsuarioModifica", typeof(int));
    
            var cope_FechaModificaParameter = cope_FechaModifica.HasValue ?
                new ObjectParameter("cope_FechaModifica", cope_FechaModifica) :
                new ObjectParameter("cope_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbCompetenciasPersona_Inactivar_Result>("UDP_RRHH_tbCompetenciasPersona_Inactivar", cope_IdParameter, cope_RazonInactivoParameter, cope_UsuarioModificaParameter, cope_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbCompetenciasPersona_Insert_Result> UDP_RRHH_tbCompetenciasPersona_Insert(Nullable<int> per_Id, Nullable<int> comp_Id, Nullable<int> cope_UsuarioCrea, Nullable<System.DateTime> cope_FechaCrea)
        {
            var per_IdParameter = per_Id.HasValue ?
                new ObjectParameter("per_Id", per_Id) :
                new ObjectParameter("per_Id", typeof(int));
    
            var comp_IdParameter = comp_Id.HasValue ?
                new ObjectParameter("comp_Id", comp_Id) :
                new ObjectParameter("comp_Id", typeof(int));
    
            var cope_UsuarioCreaParameter = cope_UsuarioCrea.HasValue ?
                new ObjectParameter("cope_UsuarioCrea", cope_UsuarioCrea) :
                new ObjectParameter("cope_UsuarioCrea", typeof(int));
    
            var cope_FechaCreaParameter = cope_FechaCrea.HasValue ?
                new ObjectParameter("cope_FechaCrea", cope_FechaCrea) :
                new ObjectParameter("cope_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbCompetenciasPersona_Insert_Result>("UDP_RRHH_tbCompetenciasPersona_Insert", per_IdParameter, comp_IdParameter, cope_UsuarioCreaParameter, cope_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbCompetenciasPersona_Restore_Result> UDP_RRHH_tbCompetenciasPersona_Restore(Nullable<int> cope_Id, Nullable<int> cope_UsuarioModifica, Nullable<System.DateTime> cope_FechaModifica)
        {
            var cope_IdParameter = cope_Id.HasValue ?
                new ObjectParameter("cope_Id", cope_Id) :
                new ObjectParameter("cope_Id", typeof(int));
    
            var cope_UsuarioModificaParameter = cope_UsuarioModifica.HasValue ?
                new ObjectParameter("cope_UsuarioModifica", cope_UsuarioModifica) :
                new ObjectParameter("cope_UsuarioModifica", typeof(int));
    
            var cope_FechaModificaParameter = cope_FechaModifica.HasValue ?
                new ObjectParameter("cope_FechaModifica", cope_FechaModifica) :
                new ObjectParameter("cope_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbCompetenciasPersona_Restore_Result>("UDP_RRHH_tbCompetenciasPersona_Restore", cope_IdParameter, cope_UsuarioModificaParameter, cope_FechaModificaParameter);
        }
    
        public virtual int UDP_RRHH_tbConceptosArchivos_Delete(Nullable<int> cona_Id, string cona_RazonInactivo, Nullable<int> cona_UsuarioModifica, Nullable<System.DateTime> cona_FechaModifica)
        {
            var cona_IdParameter = cona_Id.HasValue ?
                new ObjectParameter("cona_Id", cona_Id) :
                new ObjectParameter("cona_Id", typeof(int));
    
            var cona_RazonInactivoParameter = cona_RazonInactivo != null ?
                new ObjectParameter("cona_RazonInactivo", cona_RazonInactivo) :
                new ObjectParameter("cona_RazonInactivo", typeof(string));
    
            var cona_UsuarioModificaParameter = cona_UsuarioModifica.HasValue ?
                new ObjectParameter("cona_UsuarioModifica", cona_UsuarioModifica) :
                new ObjectParameter("cona_UsuarioModifica", typeof(int));
    
            var cona_FechaModificaParameter = cona_FechaModifica.HasValue ?
                new ObjectParameter("cona_FechaModifica", cona_FechaModifica) :
                new ObjectParameter("cona_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UDP_RRHH_tbConceptosArchivos_Delete", cona_IdParameter, cona_RazonInactivoParameter, cona_UsuarioModificaParameter, cona_FechaModificaParameter);
        }
    
        public virtual int UDP_RRHH_tbConceptosArchivos_Insert(string cona_Descripcion, Nullable<int> cona_UsuarioCrea, Nullable<System.DateTime> cona_FechaCrea)
        {
            var cona_DescripcionParameter = cona_Descripcion != null ?
                new ObjectParameter("cona_Descripcion", cona_Descripcion) :
                new ObjectParameter("cona_Descripcion", typeof(string));
    
            var cona_UsuarioCreaParameter = cona_UsuarioCrea.HasValue ?
                new ObjectParameter("cona_UsuarioCrea", cona_UsuarioCrea) :
                new ObjectParameter("cona_UsuarioCrea", typeof(int));
    
            var cona_FechaCreaParameter = cona_FechaCrea.HasValue ?
                new ObjectParameter("cona_FechaCrea", cona_FechaCrea) :
                new ObjectParameter("cona_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UDP_RRHH_tbConceptosArchivos_Insert", cona_DescripcionParameter, cona_UsuarioCreaParameter, cona_FechaCreaParameter);
        }
    
        public virtual int UDP_RRHH_tbConceptosArchivos_Restore(Nullable<int> cona_Id, Nullable<int> cona_UsuarioModifica, Nullable<System.DateTime> cona_FechaModifica)
        {
            var cona_IdParameter = cona_Id.HasValue ?
                new ObjectParameter("cona_Id", cona_Id) :
                new ObjectParameter("cona_Id", typeof(int));
    
            var cona_UsuarioModificaParameter = cona_UsuarioModifica.HasValue ?
                new ObjectParameter("cona_UsuarioModifica", cona_UsuarioModifica) :
                new ObjectParameter("cona_UsuarioModifica", typeof(int));
    
            var cona_FechaModificaParameter = cona_FechaModifica.HasValue ?
                new ObjectParameter("cona_FechaModifica", cona_FechaModifica) :
                new ObjectParameter("cona_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UDP_RRHH_tbConceptosArchivos_Restore", cona_IdParameter, cona_UsuarioModificaParameter, cona_FechaModificaParameter);
        }
    
        public virtual int UDP_RRHH_tbConceptosArchivos_Update(Nullable<int> cona_Id, string cona_Descripcion, Nullable<int> cona_UsuarioModifica, Nullable<System.DateTime> cona_FechaModifica)
        {
            var cona_IdParameter = cona_Id.HasValue ?
                new ObjectParameter("cona_Id", cona_Id) :
                new ObjectParameter("cona_Id", typeof(int));
    
            var cona_DescripcionParameter = cona_Descripcion != null ?
                new ObjectParameter("cona_Descripcion", cona_Descripcion) :
                new ObjectParameter("cona_Descripcion", typeof(string));
    
            var cona_UsuarioModificaParameter = cona_UsuarioModifica.HasValue ?
                new ObjectParameter("cona_UsuarioModifica", cona_UsuarioModifica) :
                new ObjectParameter("cona_UsuarioModifica", typeof(int));
    
            var cona_FechaModificaParameter = cona_FechaModifica.HasValue ?
                new ObjectParameter("cona_FechaModifica", cona_FechaModifica) :
                new ObjectParameter("cona_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UDP_RRHH_tbConceptosArchivos_Update", cona_IdParameter, cona_DescripcionParameter, cona_UsuarioModificaParameter, cona_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbDepartamentos_Delete_Result> UDP_RRHH_tbDepartamentos_Delete(Nullable<int> depto_Id, string depto_razon_Inactivo, Nullable<int> depto_UsuarioModifica, Nullable<System.DateTime> depto_FechaModifica)
        {
            var depto_IdParameter = depto_Id.HasValue ?
                new ObjectParameter("depto_Id", depto_Id) :
                new ObjectParameter("depto_Id", typeof(int));
    
            var depto_razon_InactivoParameter = depto_razon_Inactivo != null ?
                new ObjectParameter("depto_razon_Inactivo", depto_razon_Inactivo) :
                new ObjectParameter("depto_razon_Inactivo", typeof(string));
    
            var depto_UsuarioModificaParameter = depto_UsuarioModifica.HasValue ?
                new ObjectParameter("depto_UsuarioModifica", depto_UsuarioModifica) :
                new ObjectParameter("depto_UsuarioModifica", typeof(int));
    
            var depto_FechaModificaParameter = depto_FechaModifica.HasValue ?
                new ObjectParameter("depto_FechaModifica", depto_FechaModifica) :
                new ObjectParameter("depto_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbDepartamentos_Delete_Result>("UDP_RRHH_tbDepartamentos_Delete", depto_IdParameter, depto_razon_InactivoParameter, depto_UsuarioModificaParameter, depto_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbDepartamentos_Insert_Result> UDP_RRHH_tbDepartamentos_Insert(Nullable<int> area_Id, string depto_Descripcion, Nullable<int> car_Id, Nullable<int> depto_Usuariocrea, Nullable<System.DateTime> depto_FechaCrea)
        {
            var area_IdParameter = area_Id.HasValue ?
                new ObjectParameter("area_Id", area_Id) :
                new ObjectParameter("area_Id", typeof(int));
    
            var depto_DescripcionParameter = depto_Descripcion != null ?
                new ObjectParameter("depto_Descripcion", depto_Descripcion) :
                new ObjectParameter("depto_Descripcion", typeof(string));
    
            var car_IdParameter = car_Id.HasValue ?
                new ObjectParameter("car_Id", car_Id) :
                new ObjectParameter("car_Id", typeof(int));
    
            var depto_UsuariocreaParameter = depto_Usuariocrea.HasValue ?
                new ObjectParameter("depto_Usuariocrea", depto_Usuariocrea) :
                new ObjectParameter("depto_Usuariocrea", typeof(int));
    
            var depto_FechaCreaParameter = depto_FechaCrea.HasValue ?
                new ObjectParameter("depto_FechaCrea", depto_FechaCrea) :
                new ObjectParameter("depto_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbDepartamentos_Insert_Result>("UDP_RRHH_tbDepartamentos_Insert", area_IdParameter, depto_DescripcionParameter, car_IdParameter, depto_UsuariocreaParameter, depto_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbDepartamentos_Restore_Result> UDP_RRHH_tbDepartamentos_Restore(Nullable<int> depto_Id, Nullable<int> depto_Usuariomodifica, Nullable<System.DateTime> depto_Fechamodifica)
        {
            var depto_IdParameter = depto_Id.HasValue ?
                new ObjectParameter("depto_Id", depto_Id) :
                new ObjectParameter("depto_Id", typeof(int));
    
            var depto_UsuariomodificaParameter = depto_Usuariomodifica.HasValue ?
                new ObjectParameter("depto_Usuariomodifica", depto_Usuariomodifica) :
                new ObjectParameter("depto_Usuariomodifica", typeof(int));
    
            var depto_FechamodificaParameter = depto_Fechamodifica.HasValue ?
                new ObjectParameter("depto_Fechamodifica", depto_Fechamodifica) :
                new ObjectParameter("depto_Fechamodifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbDepartamentos_Restore_Result>("UDP_RRHH_tbDepartamentos_Restore", depto_IdParameter, depto_UsuariomodificaParameter, depto_FechamodificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbDepartamentos_Update_Result> UDP_RRHH_tbDepartamentos_Update(Nullable<int> depto_Id, Nullable<int> area_Id, string depto_Descripcion, Nullable<int> depto_UsuarioModifica, Nullable<System.DateTime> depto_FechaModifica)
        {
            var depto_IdParameter = depto_Id.HasValue ?
                new ObjectParameter("depto_Id", depto_Id) :
                new ObjectParameter("depto_Id", typeof(int));
    
            var area_IdParameter = area_Id.HasValue ?
                new ObjectParameter("area_Id", area_Id) :
                new ObjectParameter("area_Id", typeof(int));
    
            var depto_DescripcionParameter = depto_Descripcion != null ?
                new ObjectParameter("depto_Descripcion", depto_Descripcion) :
                new ObjectParameter("depto_Descripcion", typeof(string));
    
            var depto_UsuarioModificaParameter = depto_UsuarioModifica.HasValue ?
                new ObjectParameter("depto_UsuarioModifica", depto_UsuarioModifica) :
                new ObjectParameter("depto_UsuarioModifica", typeof(int));
    
            var depto_FechaModificaParameter = depto_FechaModifica.HasValue ?
                new ObjectParameter("depto_FechaModifica", depto_FechaModifica) :
                new ObjectParameter("depto_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbDepartamentos_Update_Result>("UDP_RRHH_tbDepartamentos_Update", depto_IdParameter, area_IdParameter, depto_DescripcionParameter, depto_UsuarioModificaParameter, depto_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbDirectoriosEmpleados_Delete_Result> UDP_RRHH_tbDirectoriosEmpleados_Delete(Nullable<int> direm_Id, string direm_RazonInactivo, Nullable<int> direm_UsuarioModifica, Nullable<System.DateTime> direm_FechaModifica)
        {
            var direm_IdParameter = direm_Id.HasValue ?
                new ObjectParameter("direm_Id", direm_Id) :
                new ObjectParameter("direm_Id", typeof(int));
    
            var direm_RazonInactivoParameter = direm_RazonInactivo != null ?
                new ObjectParameter("direm_RazonInactivo", direm_RazonInactivo) :
                new ObjectParameter("direm_RazonInactivo", typeof(string));
    
            var direm_UsuarioModificaParameter = direm_UsuarioModifica.HasValue ?
                new ObjectParameter("direm_UsuarioModifica", direm_UsuarioModifica) :
                new ObjectParameter("direm_UsuarioModifica", typeof(int));
    
            var direm_FechaModificaParameter = direm_FechaModifica.HasValue ?
                new ObjectParameter("direm_FechaModifica", direm_FechaModifica) :
                new ObjectParameter("direm_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbDirectoriosEmpleados_Delete_Result>("UDP_RRHH_tbDirectoriosEmpleados_Delete", direm_IdParameter, direm_RazonInactivoParameter, direm_UsuarioModificaParameter, direm_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbDirectoriosEmpleados_Insert_Result> UDP_RRHH_tbDirectoriosEmpleados_Insert(string direm_Carpeta, string direm_NombreArchivo, Nullable<int> emp_Id, Nullable<int> direm_UsuarioCrea, Nullable<System.DateTime> direm_FechaCrea)
        {
            var direm_CarpetaParameter = direm_Carpeta != null ?
                new ObjectParameter("direm_Carpeta", direm_Carpeta) :
                new ObjectParameter("direm_Carpeta", typeof(string));
    
            var direm_NombreArchivoParameter = direm_NombreArchivo != null ?
                new ObjectParameter("direm_NombreArchivo", direm_NombreArchivo) :
                new ObjectParameter("direm_NombreArchivo", typeof(string));
    
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var direm_UsuarioCreaParameter = direm_UsuarioCrea.HasValue ?
                new ObjectParameter("direm_UsuarioCrea", direm_UsuarioCrea) :
                new ObjectParameter("direm_UsuarioCrea", typeof(int));
    
            var direm_FechaCreaParameter = direm_FechaCrea.HasValue ?
                new ObjectParameter("direm_FechaCrea", direm_FechaCrea) :
                new ObjectParameter("direm_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbDirectoriosEmpleados_Insert_Result>("UDP_RRHH_tbDirectoriosEmpleados_Insert", direm_CarpetaParameter, direm_NombreArchivoParameter, emp_IdParameter, direm_UsuarioCreaParameter, direm_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbEmpleados_Contratar_Result> UDP_RRHH_tbEmpleados_Contratar(Nullable<int> scan_Id, Nullable<int> car_Id, Nullable<int> area_Id, Nullable<int> depto_Id, Nullable<int> jor_Id, Nullable<int> cpla_IdPlanilla, Nullable<int> fpa_IdFormaPago, string emp_CuentaBancaria, Nullable<bool> emp_Temporal, Nullable<bool> emp_Reingreso, Nullable<int> req_Id, Nullable<int> tmon_Id, Nullable<decimal> sue_Cantidad, Nullable<System.DateTime> emp_Fechaingreso, Nullable<int> emp_UsuarioCrea, Nullable<System.DateTime> emp_FechaCrea)
        {
            var scan_IdParameter = scan_Id.HasValue ?
                new ObjectParameter("scan_Id", scan_Id) :
                new ObjectParameter("scan_Id", typeof(int));
    
            var car_IdParameter = car_Id.HasValue ?
                new ObjectParameter("car_Id", car_Id) :
                new ObjectParameter("car_Id", typeof(int));
    
            var area_IdParameter = area_Id.HasValue ?
                new ObjectParameter("area_Id", area_Id) :
                new ObjectParameter("area_Id", typeof(int));
    
            var depto_IdParameter = depto_Id.HasValue ?
                new ObjectParameter("depto_Id", depto_Id) :
                new ObjectParameter("depto_Id", typeof(int));
    
            var jor_IdParameter = jor_Id.HasValue ?
                new ObjectParameter("jor_Id", jor_Id) :
                new ObjectParameter("jor_Id", typeof(int));
    
            var cpla_IdPlanillaParameter = cpla_IdPlanilla.HasValue ?
                new ObjectParameter("cpla_IdPlanilla", cpla_IdPlanilla) :
                new ObjectParameter("cpla_IdPlanilla", typeof(int));
    
            var fpa_IdFormaPagoParameter = fpa_IdFormaPago.HasValue ?
                new ObjectParameter("fpa_IdFormaPago", fpa_IdFormaPago) :
                new ObjectParameter("fpa_IdFormaPago", typeof(int));
    
            var emp_CuentaBancariaParameter = emp_CuentaBancaria != null ?
                new ObjectParameter("emp_CuentaBancaria", emp_CuentaBancaria) :
                new ObjectParameter("emp_CuentaBancaria", typeof(string));
    
            var emp_TemporalParameter = emp_Temporal.HasValue ?
                new ObjectParameter("emp_Temporal", emp_Temporal) :
                new ObjectParameter("emp_Temporal", typeof(bool));
    
            var emp_ReingresoParameter = emp_Reingreso.HasValue ?
                new ObjectParameter("emp_Reingreso", emp_Reingreso) :
                new ObjectParameter("emp_Reingreso", typeof(bool));
    
            var req_IdParameter = req_Id.HasValue ?
                new ObjectParameter("req_Id", req_Id) :
                new ObjectParameter("req_Id", typeof(int));
    
            var tmon_IdParameter = tmon_Id.HasValue ?
                new ObjectParameter("tmon_Id", tmon_Id) :
                new ObjectParameter("tmon_Id", typeof(int));
    
            var sue_CantidadParameter = sue_Cantidad.HasValue ?
                new ObjectParameter("sue_Cantidad", sue_Cantidad) :
                new ObjectParameter("sue_Cantidad", typeof(decimal));
    
            var emp_FechaingresoParameter = emp_Fechaingreso.HasValue ?
                new ObjectParameter("emp_Fechaingreso", emp_Fechaingreso) :
                new ObjectParameter("emp_Fechaingreso", typeof(System.DateTime));
    
            var emp_UsuarioCreaParameter = emp_UsuarioCrea.HasValue ?
                new ObjectParameter("emp_UsuarioCrea", emp_UsuarioCrea) :
                new ObjectParameter("emp_UsuarioCrea", typeof(int));
    
            var emp_FechaCreaParameter = emp_FechaCrea.HasValue ?
                new ObjectParameter("emp_FechaCrea", emp_FechaCrea) :
                new ObjectParameter("emp_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbEmpleados_Contratar_Result>("UDP_RRHH_tbEmpleados_Contratar", scan_IdParameter, car_IdParameter, area_IdParameter, depto_IdParameter, jor_IdParameter, cpla_IdPlanillaParameter, fpa_IdFormaPagoParameter, emp_CuentaBancariaParameter, emp_TemporalParameter, emp_ReingresoParameter, req_IdParameter, tmon_IdParameter, sue_CantidadParameter, emp_FechaingresoParameter, emp_UsuarioCreaParameter, emp_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbEmpleados_Insert_Result> UDP_RRHH_tbEmpleados_Insert(string per_Identidad, string per_Nombres, string per_Apellidos, Nullable<System.DateTime> per_FechaNacimiento, Nullable<int> per_Edad, string per_Sexo, Nullable<int> nac_Id, string per_Direccion, string per_Telefono, string per_CorreoElectronico, string per_EstadoCivil, string per_TipoSangre, Nullable<int> per_UsuarioCrea, Nullable<System.DateTime> per_FechaCrea, Nullable<int> car_Id, Nullable<int> area_Id, Nullable<int> depto_Id, Nullable<int> jor_Id, Nullable<int> cpla_IdPlanilla, Nullable<int> fpa_IdFormaPago, Nullable<int> emp_UsuarioCrea, Nullable<System.DateTime> emp_FechaCrea, Nullable<System.DateTime> emp_FechaIngreso, string emp_CuentaBancaria, Nullable<decimal> sue_Cantidad, Nullable<int> tmon_Id, Nullable<int> sue_UsuarioCrea, Nullable<System.DateTime> sue_FechaCrea)
        {
            var per_IdentidadParameter = per_Identidad != null ?
                new ObjectParameter("per_Identidad", per_Identidad) :
                new ObjectParameter("per_Identidad", typeof(string));
    
            var per_NombresParameter = per_Nombres != null ?
                new ObjectParameter("per_Nombres", per_Nombres) :
                new ObjectParameter("per_Nombres", typeof(string));
    
            var per_ApellidosParameter = per_Apellidos != null ?
                new ObjectParameter("per_Apellidos", per_Apellidos) :
                new ObjectParameter("per_Apellidos", typeof(string));
    
            var per_FechaNacimientoParameter = per_FechaNacimiento.HasValue ?
                new ObjectParameter("per_FechaNacimiento", per_FechaNacimiento) :
                new ObjectParameter("per_FechaNacimiento", typeof(System.DateTime));
    
            var per_EdadParameter = per_Edad.HasValue ?
                new ObjectParameter("per_Edad", per_Edad) :
                new ObjectParameter("per_Edad", typeof(int));
    
            var per_SexoParameter = per_Sexo != null ?
                new ObjectParameter("per_Sexo", per_Sexo) :
                new ObjectParameter("per_Sexo", typeof(string));
    
            var nac_IdParameter = nac_Id.HasValue ?
                new ObjectParameter("nac_Id", nac_Id) :
                new ObjectParameter("nac_Id", typeof(int));
    
            var per_DireccionParameter = per_Direccion != null ?
                new ObjectParameter("per_Direccion", per_Direccion) :
                new ObjectParameter("per_Direccion", typeof(string));
    
            var per_TelefonoParameter = per_Telefono != null ?
                new ObjectParameter("per_Telefono", per_Telefono) :
                new ObjectParameter("per_Telefono", typeof(string));
    
            var per_CorreoElectronicoParameter = per_CorreoElectronico != null ?
                new ObjectParameter("per_CorreoElectronico", per_CorreoElectronico) :
                new ObjectParameter("per_CorreoElectronico", typeof(string));
    
            var per_EstadoCivilParameter = per_EstadoCivil != null ?
                new ObjectParameter("per_EstadoCivil", per_EstadoCivil) :
                new ObjectParameter("per_EstadoCivil", typeof(string));
    
            var per_TipoSangreParameter = per_TipoSangre != null ?
                new ObjectParameter("per_TipoSangre", per_TipoSangre) :
                new ObjectParameter("per_TipoSangre", typeof(string));
    
            var per_UsuarioCreaParameter = per_UsuarioCrea.HasValue ?
                new ObjectParameter("per_UsuarioCrea", per_UsuarioCrea) :
                new ObjectParameter("per_UsuarioCrea", typeof(int));
    
            var per_FechaCreaParameter = per_FechaCrea.HasValue ?
                new ObjectParameter("per_FechaCrea", per_FechaCrea) :
                new ObjectParameter("per_FechaCrea", typeof(System.DateTime));
    
            var car_IdParameter = car_Id.HasValue ?
                new ObjectParameter("car_Id", car_Id) :
                new ObjectParameter("car_Id", typeof(int));
    
            var area_IdParameter = area_Id.HasValue ?
                new ObjectParameter("area_Id", area_Id) :
                new ObjectParameter("area_Id", typeof(int));
    
            var depto_IdParameter = depto_Id.HasValue ?
                new ObjectParameter("depto_Id", depto_Id) :
                new ObjectParameter("depto_Id", typeof(int));
    
            var jor_IdParameter = jor_Id.HasValue ?
                new ObjectParameter("jor_Id", jor_Id) :
                new ObjectParameter("jor_Id", typeof(int));
    
            var cpla_IdPlanillaParameter = cpla_IdPlanilla.HasValue ?
                new ObjectParameter("cpla_IdPlanilla", cpla_IdPlanilla) :
                new ObjectParameter("cpla_IdPlanilla", typeof(int));
    
            var fpa_IdFormaPagoParameter = fpa_IdFormaPago.HasValue ?
                new ObjectParameter("fpa_IdFormaPago", fpa_IdFormaPago) :
                new ObjectParameter("fpa_IdFormaPago", typeof(int));
    
            var emp_UsuarioCreaParameter = emp_UsuarioCrea.HasValue ?
                new ObjectParameter("emp_UsuarioCrea", emp_UsuarioCrea) :
                new ObjectParameter("emp_UsuarioCrea", typeof(int));
    
            var emp_FechaCreaParameter = emp_FechaCrea.HasValue ?
                new ObjectParameter("emp_FechaCrea", emp_FechaCrea) :
                new ObjectParameter("emp_FechaCrea", typeof(System.DateTime));
    
            var emp_FechaIngresoParameter = emp_FechaIngreso.HasValue ?
                new ObjectParameter("emp_FechaIngreso", emp_FechaIngreso) :
                new ObjectParameter("emp_FechaIngreso", typeof(System.DateTime));
    
            var emp_CuentaBancariaParameter = emp_CuentaBancaria != null ?
                new ObjectParameter("emp_CuentaBancaria", emp_CuentaBancaria) :
                new ObjectParameter("emp_CuentaBancaria", typeof(string));
    
            var sue_CantidadParameter = sue_Cantidad.HasValue ?
                new ObjectParameter("sue_Cantidad", sue_Cantidad) :
                new ObjectParameter("sue_Cantidad", typeof(decimal));
    
            var tmon_IdParameter = tmon_Id.HasValue ?
                new ObjectParameter("tmon_Id", tmon_Id) :
                new ObjectParameter("tmon_Id", typeof(int));
    
            var sue_UsuarioCreaParameter = sue_UsuarioCrea.HasValue ?
                new ObjectParameter("sue_UsuarioCrea", sue_UsuarioCrea) :
                new ObjectParameter("sue_UsuarioCrea", typeof(int));
    
            var sue_FechaCreaParameter = sue_FechaCrea.HasValue ?
                new ObjectParameter("sue_FechaCrea", sue_FechaCrea) :
                new ObjectParameter("sue_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbEmpleados_Insert_Result>("UDP_RRHH_tbEmpleados_Insert", per_IdentidadParameter, per_NombresParameter, per_ApellidosParameter, per_FechaNacimientoParameter, per_EdadParameter, per_SexoParameter, nac_IdParameter, per_DireccionParameter, per_TelefonoParameter, per_CorreoElectronicoParameter, per_EstadoCivilParameter, per_TipoSangreParameter, per_UsuarioCreaParameter, per_FechaCreaParameter, car_IdParameter, area_IdParameter, depto_IdParameter, jor_IdParameter, cpla_IdPlanillaParameter, fpa_IdFormaPagoParameter, emp_UsuarioCreaParameter, emp_FechaCreaParameter, emp_FechaIngresoParameter, emp_CuentaBancariaParameter, sue_CantidadParameter, tmon_IdParameter, sue_UsuarioCreaParameter, sue_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbEmpleados_Recontratar_Result> UDP_RRHH_tbEmpleados_Recontratar(Nullable<int> scan_Id, Nullable<int> car_Id, Nullable<int> area_Id, Nullable<int> depto_Id, Nullable<int> jor_Id, Nullable<int> cpla_IdPlanilla, Nullable<int> fpa_IdFormaPago, string emp_CuentaBancaria, Nullable<bool> emp_Temporal, Nullable<bool> emp_Reingreso, Nullable<int> req_Id, Nullable<int> tmon_Id, Nullable<decimal> sue_Cantidad, Nullable<System.DateTime> emp_Fechareingreso, Nullable<int> emp_UsuarioCrea, Nullable<System.DateTime> emp_FechaCrea)
        {
            var scan_IdParameter = scan_Id.HasValue ?
                new ObjectParameter("scan_Id", scan_Id) :
                new ObjectParameter("scan_Id", typeof(int));
    
            var car_IdParameter = car_Id.HasValue ?
                new ObjectParameter("car_Id", car_Id) :
                new ObjectParameter("car_Id", typeof(int));
    
            var area_IdParameter = area_Id.HasValue ?
                new ObjectParameter("area_Id", area_Id) :
                new ObjectParameter("area_Id", typeof(int));
    
            var depto_IdParameter = depto_Id.HasValue ?
                new ObjectParameter("depto_Id", depto_Id) :
                new ObjectParameter("depto_Id", typeof(int));
    
            var jor_IdParameter = jor_Id.HasValue ?
                new ObjectParameter("jor_Id", jor_Id) :
                new ObjectParameter("jor_Id", typeof(int));
    
            var cpla_IdPlanillaParameter = cpla_IdPlanilla.HasValue ?
                new ObjectParameter("cpla_IdPlanilla", cpla_IdPlanilla) :
                new ObjectParameter("cpla_IdPlanilla", typeof(int));
    
            var fpa_IdFormaPagoParameter = fpa_IdFormaPago.HasValue ?
                new ObjectParameter("fpa_IdFormaPago", fpa_IdFormaPago) :
                new ObjectParameter("fpa_IdFormaPago", typeof(int));
    
            var emp_CuentaBancariaParameter = emp_CuentaBancaria != null ?
                new ObjectParameter("emp_CuentaBancaria", emp_CuentaBancaria) :
                new ObjectParameter("emp_CuentaBancaria", typeof(string));
    
            var emp_TemporalParameter = emp_Temporal.HasValue ?
                new ObjectParameter("emp_Temporal", emp_Temporal) :
                new ObjectParameter("emp_Temporal", typeof(bool));
    
            var emp_ReingresoParameter = emp_Reingreso.HasValue ?
                new ObjectParameter("emp_Reingreso", emp_Reingreso) :
                new ObjectParameter("emp_Reingreso", typeof(bool));
    
            var req_IdParameter = req_Id.HasValue ?
                new ObjectParameter("req_Id", req_Id) :
                new ObjectParameter("req_Id", typeof(int));
    
            var tmon_IdParameter = tmon_Id.HasValue ?
                new ObjectParameter("tmon_Id", tmon_Id) :
                new ObjectParameter("tmon_Id", typeof(int));
    
            var sue_CantidadParameter = sue_Cantidad.HasValue ?
                new ObjectParameter("sue_Cantidad", sue_Cantidad) :
                new ObjectParameter("sue_Cantidad", typeof(decimal));
    
            var emp_FechareingresoParameter = emp_Fechareingreso.HasValue ?
                new ObjectParameter("emp_Fechareingreso", emp_Fechareingreso) :
                new ObjectParameter("emp_Fechareingreso", typeof(System.DateTime));
    
            var emp_UsuarioCreaParameter = emp_UsuarioCrea.HasValue ?
                new ObjectParameter("emp_UsuarioCrea", emp_UsuarioCrea) :
                new ObjectParameter("emp_UsuarioCrea", typeof(int));
    
            var emp_FechaCreaParameter = emp_FechaCrea.HasValue ?
                new ObjectParameter("emp_FechaCrea", emp_FechaCrea) :
                new ObjectParameter("emp_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbEmpleados_Recontratar_Result>("UDP_RRHH_tbEmpleados_Recontratar", scan_IdParameter, car_IdParameter, area_IdParameter, depto_IdParameter, jor_IdParameter, cpla_IdPlanillaParameter, fpa_IdFormaPagoParameter, emp_CuentaBancariaParameter, emp_TemporalParameter, emp_ReingresoParameter, req_IdParameter, tmon_IdParameter, sue_CantidadParameter, emp_FechareingresoParameter, emp_UsuarioCreaParameter, emp_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbEmpleados_Restore_Result> UDP_RRHH_tbEmpleados_Restore(Nullable<int> emp_Id, Nullable<int> emp_Usuariomodifica, Nullable<System.DateTime> emp_Fechamodifica)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var emp_UsuariomodificaParameter = emp_Usuariomodifica.HasValue ?
                new ObjectParameter("emp_Usuariomodifica", emp_Usuariomodifica) :
                new ObjectParameter("emp_Usuariomodifica", typeof(int));
    
            var emp_FechamodificaParameter = emp_Fechamodifica.HasValue ?
                new ObjectParameter("emp_Fechamodifica", emp_Fechamodifica) :
                new ObjectParameter("emp_Fechamodifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbEmpleados_Restore_Result>("UDP_RRHH_tbEmpleados_Restore", emp_IdParameter, emp_UsuariomodificaParameter, emp_FechamodificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbEmpresas_Delete_Result> UDP_RRHH_tbEmpresas_Delete(Nullable<int> empr_Id, string empr_razon_Inactivo, Nullable<int> empr_UsuarioModifica, Nullable<System.DateTime> empr_FechaModifica)
        {
            var empr_IdParameter = empr_Id.HasValue ?
                new ObjectParameter("empr_Id", empr_Id) :
                new ObjectParameter("empr_Id", typeof(int));
    
            var empr_razon_InactivoParameter = empr_razon_Inactivo != null ?
                new ObjectParameter("empr_razon_Inactivo", empr_razon_Inactivo) :
                new ObjectParameter("empr_razon_Inactivo", typeof(string));
    
            var empr_UsuarioModificaParameter = empr_UsuarioModifica.HasValue ?
                new ObjectParameter("empr_UsuarioModifica", empr_UsuarioModifica) :
                new ObjectParameter("empr_UsuarioModifica", typeof(int));
    
            var empr_FechaModificaParameter = empr_FechaModifica.HasValue ?
                new ObjectParameter("empr_FechaModifica", empr_FechaModifica) :
                new ObjectParameter("empr_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbEmpresas_Delete_Result>("UDP_RRHH_tbEmpresas_Delete", empr_IdParameter, empr_razon_InactivoParameter, empr_UsuarioModificaParameter, empr_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbEmpresas_Insert_Result> UDP_RRHH_tbEmpresas_Insert(string empr_Nombre, string empr_Logo, Nullable<int> per_Id, string empr_RTN, Nullable<int> empr_usuarioCrea, Nullable<System.DateTime> empr_FechaCrea)
        {
            var empr_NombreParameter = empr_Nombre != null ?
                new ObjectParameter("empr_Nombre", empr_Nombre) :
                new ObjectParameter("empr_Nombre", typeof(string));
    
            var empr_LogoParameter = empr_Logo != null ?
                new ObjectParameter("empr_Logo", empr_Logo) :
                new ObjectParameter("empr_Logo", typeof(string));
    
            var per_IdParameter = per_Id.HasValue ?
                new ObjectParameter("per_Id", per_Id) :
                new ObjectParameter("per_Id", typeof(int));
    
            var empr_RTNParameter = empr_RTN != null ?
                new ObjectParameter("empr_RTN", empr_RTN) :
                new ObjectParameter("empr_RTN", typeof(string));
    
            var empr_usuarioCreaParameter = empr_usuarioCrea.HasValue ?
                new ObjectParameter("empr_usuarioCrea", empr_usuarioCrea) :
                new ObjectParameter("empr_usuarioCrea", typeof(int));
    
            var empr_FechaCreaParameter = empr_FechaCrea.HasValue ?
                new ObjectParameter("empr_FechaCrea", empr_FechaCrea) :
                new ObjectParameter("empr_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbEmpresas_Insert_Result>("UDP_RRHH_tbEmpresas_Insert", empr_NombreParameter, empr_LogoParameter, per_IdParameter, empr_RTNParameter, empr_usuarioCreaParameter, empr_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbEmpresas_Restore_Result> UDP_RRHH_tbEmpresas_Restore(Nullable<int> empr_Id, Nullable<int> empr_UsuarioModifica, Nullable<System.DateTime> empr_FechaModifica)
        {
            var empr_IdParameter = empr_Id.HasValue ?
                new ObjectParameter("empr_Id", empr_Id) :
                new ObjectParameter("empr_Id", typeof(int));
    
            var empr_UsuarioModificaParameter = empr_UsuarioModifica.HasValue ?
                new ObjectParameter("empr_UsuarioModifica", empr_UsuarioModifica) :
                new ObjectParameter("empr_UsuarioModifica", typeof(int));
    
            var empr_FechaModificaParameter = empr_FechaModifica.HasValue ?
                new ObjectParameter("empr_FechaModifica", empr_FechaModifica) :
                new ObjectParameter("empr_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbEmpresas_Restore_Result>("UDP_RRHH_tbEmpresas_Restore", empr_IdParameter, empr_UsuarioModificaParameter, empr_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbEmpresas_Select_Result> UDP_RRHH_tbEmpresas_Select(Nullable<int> empr_Id)
        {
            var empr_IdParameter = empr_Id.HasValue ?
                new ObjectParameter("empr_Id", empr_Id) :
                new ObjectParameter("empr_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbEmpresas_Select_Result>("UDP_RRHH_tbEmpresas_Select", empr_IdParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbEmpresas_Update_Result> UDP_RRHH_tbEmpresas_Update(Nullable<int> empr_Id, string empr_Nombre, Nullable<int> per_Id, string empr_RTN, string empr_Logo, Nullable<int> empr_usuarioModifica, Nullable<System.DateTime> empr_FechaModifica)
        {
            var empr_IdParameter = empr_Id.HasValue ?
                new ObjectParameter("empr_Id", empr_Id) :
                new ObjectParameter("empr_Id", typeof(int));
    
            var empr_NombreParameter = empr_Nombre != null ?
                new ObjectParameter("empr_Nombre", empr_Nombre) :
                new ObjectParameter("empr_Nombre", typeof(string));
    
            var per_IdParameter = per_Id.HasValue ?
                new ObjectParameter("per_Id", per_Id) :
                new ObjectParameter("per_Id", typeof(int));
    
            var empr_RTNParameter = empr_RTN != null ?
                new ObjectParameter("empr_RTN", empr_RTN) :
                new ObjectParameter("empr_RTN", typeof(string));
    
            var empr_LogoParameter = empr_Logo != null ?
                new ObjectParameter("empr_Logo", empr_Logo) :
                new ObjectParameter("empr_Logo", typeof(string));
    
            var empr_usuarioModificaParameter = empr_usuarioModifica.HasValue ?
                new ObjectParameter("empr_usuarioModifica", empr_usuarioModifica) :
                new ObjectParameter("empr_usuarioModifica", typeof(int));
    
            var empr_FechaModificaParameter = empr_FechaModifica.HasValue ?
                new ObjectParameter("empr_FechaModifica", empr_FechaModifica) :
                new ObjectParameter("empr_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbEmpresas_Update_Result>("UDP_RRHH_tbEmpresas_Update", empr_IdParameter, empr_NombreParameter, per_IdParameter, empr_RTNParameter, empr_LogoParameter, empr_usuarioModificaParameter, empr_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbEquipoEmpleado_Inactivar_Result> UDP_RRHH_tbEquipoEmpleado_Inactivar(Nullable<int> eqem_Id, Nullable<int> eqem_UsuarioModifica, Nullable<System.DateTime> eqem_FechaModifica)
        {
            var eqem_IdParameter = eqem_Id.HasValue ?
                new ObjectParameter("eqem_Id", eqem_Id) :
                new ObjectParameter("eqem_Id", typeof(int));
    
            var eqem_UsuarioModificaParameter = eqem_UsuarioModifica.HasValue ?
                new ObjectParameter("eqem_UsuarioModifica", eqem_UsuarioModifica) :
                new ObjectParameter("eqem_UsuarioModifica", typeof(int));
    
            var eqem_FechaModificaParameter = eqem_FechaModifica.HasValue ?
                new ObjectParameter("eqem_FechaModifica", eqem_FechaModifica) :
                new ObjectParameter("eqem_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbEquipoEmpleado_Inactivar_Result>("UDP_RRHH_tbEquipoEmpleado_Inactivar", eqem_IdParameter, eqem_UsuarioModificaParameter, eqem_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbEquipoEmpleado_Insert_Result> UDP_RRHH_tbEquipoEmpleado_Insert(Nullable<int> emp_Id, Nullable<int> eqtra_Id, Nullable<System.DateTime> eqem_Fecha, Nullable<int> eqem_UsuarioCrea, Nullable<System.DateTime> eqem_FechaCrea)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var eqtra_IdParameter = eqtra_Id.HasValue ?
                new ObjectParameter("eqtra_Id", eqtra_Id) :
                new ObjectParameter("eqtra_Id", typeof(int));
    
            var eqem_FechaParameter = eqem_Fecha.HasValue ?
                new ObjectParameter("eqem_Fecha", eqem_Fecha) :
                new ObjectParameter("eqem_Fecha", typeof(System.DateTime));
    
            var eqem_UsuarioCreaParameter = eqem_UsuarioCrea.HasValue ?
                new ObjectParameter("eqem_UsuarioCrea", eqem_UsuarioCrea) :
                new ObjectParameter("eqem_UsuarioCrea", typeof(int));
    
            var eqem_FechaCreaParameter = eqem_FechaCrea.HasValue ?
                new ObjectParameter("eqem_FechaCrea", eqem_FechaCrea) :
                new ObjectParameter("eqem_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbEquipoEmpleado_Insert_Result>("UDP_RRHH_tbEquipoEmpleado_Insert", emp_IdParameter, eqtra_IdParameter, eqem_FechaParameter, eqem_UsuarioCreaParameter, eqem_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbEquipoEmpleados_Delete_Result> UDP_RRHH_tbEquipoEmpleados_Delete(Nullable<int> eqem_Id, Nullable<int> eqem_UsuarioModifica, Nullable<System.DateTime> eqem_FechaModifica)
        {
            var eqem_IdParameter = eqem_Id.HasValue ?
                new ObjectParameter("eqem_Id", eqem_Id) :
                new ObjectParameter("eqem_Id", typeof(int));
    
            var eqem_UsuarioModificaParameter = eqem_UsuarioModifica.HasValue ?
                new ObjectParameter("eqem_UsuarioModifica", eqem_UsuarioModifica) :
                new ObjectParameter("eqem_UsuarioModifica", typeof(int));
    
            var eqem_FechaModificaParameter = eqem_FechaModifica.HasValue ?
                new ObjectParameter("eqem_FechaModifica", eqem_FechaModifica) :
                new ObjectParameter("eqem_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbEquipoEmpleados_Delete_Result>("UDP_RRHH_tbEquipoEmpleados_Delete", eqem_IdParameter, eqem_UsuarioModificaParameter, eqem_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbEquipoEmpleados_Insert_Result> UDP_RRHH_tbEquipoEmpleados_Insert(Nullable<int> emp_Id, Nullable<int> eqtra_Id, Nullable<System.DateTime> eqem_Fecha, Nullable<int> eqem_UsuarioCrea, Nullable<System.DateTime> eqem_FechaCrea)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var eqtra_IdParameter = eqtra_Id.HasValue ?
                new ObjectParameter("eqtra_Id", eqtra_Id) :
                new ObjectParameter("eqtra_Id", typeof(int));
    
            var eqem_FechaParameter = eqem_Fecha.HasValue ?
                new ObjectParameter("eqem_Fecha", eqem_Fecha) :
                new ObjectParameter("eqem_Fecha", typeof(System.DateTime));
    
            var eqem_UsuarioCreaParameter = eqem_UsuarioCrea.HasValue ?
                new ObjectParameter("eqem_UsuarioCrea", eqem_UsuarioCrea) :
                new ObjectParameter("eqem_UsuarioCrea", typeof(int));
    
            var eqem_FechaCreaParameter = eqem_FechaCrea.HasValue ?
                new ObjectParameter("eqem_FechaCrea", eqem_FechaCrea) :
                new ObjectParameter("eqem_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbEquipoEmpleados_Insert_Result>("UDP_RRHH_tbEquipoEmpleados_Insert", emp_IdParameter, eqtra_IdParameter, eqem_FechaParameter, eqem_UsuarioCreaParameter, eqem_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbEquipoEmpleados_Update_Result> UDP_RRHH_tbEquipoEmpleados_Update(Nullable<int> eqem_Id, Nullable<int> eqtra_Id, Nullable<System.DateTime> eqem_Fecha)
        {
            var eqem_IdParameter = eqem_Id.HasValue ?
                new ObjectParameter("eqem_Id", eqem_Id) :
                new ObjectParameter("eqem_Id", typeof(int));
    
            var eqtra_IdParameter = eqtra_Id.HasValue ?
                new ObjectParameter("eqtra_Id", eqtra_Id) :
                new ObjectParameter("eqtra_Id", typeof(int));
    
            var eqem_FechaParameter = eqem_Fecha.HasValue ?
                new ObjectParameter("eqem_Fecha", eqem_Fecha) :
                new ObjectParameter("eqem_Fecha", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbEquipoEmpleados_Update_Result>("UDP_RRHH_tbEquipoEmpleados_Update", eqem_IdParameter, eqtra_IdParameter, eqem_FechaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbEquipoTrabajo_Inactivar_Result> UDP_RRHH_tbEquipoTrabajo_Inactivar(Nullable<int> eqtra_Id, Nullable<int> eqtra_UsuarioModifica, Nullable<System.DateTime> eqtra_FechaModifica)
        {
            var eqtra_IdParameter = eqtra_Id.HasValue ?
                new ObjectParameter("eqtra_Id", eqtra_Id) :
                new ObjectParameter("eqtra_Id", typeof(int));
    
            var eqtra_UsuarioModificaParameter = eqtra_UsuarioModifica.HasValue ?
                new ObjectParameter("eqtra_UsuarioModifica", eqtra_UsuarioModifica) :
                new ObjectParameter("eqtra_UsuarioModifica", typeof(int));
    
            var eqtra_FechaModificaParameter = eqtra_FechaModifica.HasValue ?
                new ObjectParameter("eqtra_FechaModifica", eqtra_FechaModifica) :
                new ObjectParameter("eqtra_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbEquipoTrabajo_Inactivar_Result>("UDP_RRHH_tbEquipoTrabajo_Inactivar", eqtra_IdParameter, eqtra_UsuarioModificaParameter, eqtra_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbEquipoTrabajo_Insert_Result> UDP_RRHH_tbEquipoTrabajo_Insert(string eqtra_Codigo, string eqtra_Descripcion, string eqtra_Observacion, Nullable<int> eqtra_UsuarioCrea, Nullable<System.DateTime> eqtra_FechaCrea)
        {
            var eqtra_CodigoParameter = eqtra_Codigo != null ?
                new ObjectParameter("eqtra_Codigo", eqtra_Codigo) :
                new ObjectParameter("eqtra_Codigo", typeof(string));
    
            var eqtra_DescripcionParameter = eqtra_Descripcion != null ?
                new ObjectParameter("eqtra_Descripcion", eqtra_Descripcion) :
                new ObjectParameter("eqtra_Descripcion", typeof(string));
    
            var eqtra_ObservacionParameter = eqtra_Observacion != null ?
                new ObjectParameter("eqtra_Observacion", eqtra_Observacion) :
                new ObjectParameter("eqtra_Observacion", typeof(string));
    
            var eqtra_UsuarioCreaParameter = eqtra_UsuarioCrea.HasValue ?
                new ObjectParameter("eqtra_UsuarioCrea", eqtra_UsuarioCrea) :
                new ObjectParameter("eqtra_UsuarioCrea", typeof(int));
    
            var eqtra_FechaCreaParameter = eqtra_FechaCrea.HasValue ?
                new ObjectParameter("eqtra_FechaCrea", eqtra_FechaCrea) :
                new ObjectParameter("eqtra_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbEquipoTrabajo_Insert_Result>("UDP_RRHH_tbEquipoTrabajo_Insert", eqtra_CodigoParameter, eqtra_DescripcionParameter, eqtra_ObservacionParameter, eqtra_UsuarioCreaParameter, eqtra_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbEquipoTrabajo_Restore_Result> UDP_RRHH_tbEquipoTrabajo_Restore(Nullable<int> eqtra_Id, Nullable<int> eqtra_UsuarioModifica, Nullable<System.DateTime> eqtra_FechaModifica)
        {
            var eqtra_IdParameter = eqtra_Id.HasValue ?
                new ObjectParameter("eqtra_Id", eqtra_Id) :
                new ObjectParameter("eqtra_Id", typeof(int));
    
            var eqtra_UsuarioModificaParameter = eqtra_UsuarioModifica.HasValue ?
                new ObjectParameter("eqtra_UsuarioModifica", eqtra_UsuarioModifica) :
                new ObjectParameter("eqtra_UsuarioModifica", typeof(int));
    
            var eqtra_FechaModificaParameter = eqtra_FechaModifica.HasValue ?
                new ObjectParameter("eqtra_FechaModifica", eqtra_FechaModifica) :
                new ObjectParameter("eqtra_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbEquipoTrabajo_Restore_Result>("UDP_RRHH_tbEquipoTrabajo_Restore", eqtra_IdParameter, eqtra_UsuarioModificaParameter, eqtra_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbEquipoTrabajo_Update_Result> UDP_RRHH_tbEquipoTrabajo_Update(Nullable<int> eqtra_Id, string eqtra_Codigo, string eqtra_Descripcion, string eqtra_Observacion, Nullable<int> eqtra_UsuarioModifica, Nullable<System.DateTime> eqtra_FechaModifica)
        {
            var eqtra_IdParameter = eqtra_Id.HasValue ?
                new ObjectParameter("eqtra_Id", eqtra_Id) :
                new ObjectParameter("eqtra_Id", typeof(int));
    
            var eqtra_CodigoParameter = eqtra_Codigo != null ?
                new ObjectParameter("eqtra_Codigo", eqtra_Codigo) :
                new ObjectParameter("eqtra_Codigo", typeof(string));
    
            var eqtra_DescripcionParameter = eqtra_Descripcion != null ?
                new ObjectParameter("eqtra_Descripcion", eqtra_Descripcion) :
                new ObjectParameter("eqtra_Descripcion", typeof(string));
    
            var eqtra_ObservacionParameter = eqtra_Observacion != null ?
                new ObjectParameter("eqtra_Observacion", eqtra_Observacion) :
                new ObjectParameter("eqtra_Observacion", typeof(string));
    
            var eqtra_UsuarioModificaParameter = eqtra_UsuarioModifica.HasValue ?
                new ObjectParameter("eqtra_UsuarioModifica", eqtra_UsuarioModifica) :
                new ObjectParameter("eqtra_UsuarioModifica", typeof(int));
    
            var eqtra_FechaModificaParameter = eqtra_FechaModifica.HasValue ?
                new ObjectParameter("eqtra_FechaModifica", eqtra_FechaModifica) :
                new ObjectParameter("eqtra_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbEquipoTrabajo_Update_Result>("UDP_RRHH_tbEquipoTrabajo_Update", eqtra_IdParameter, eqtra_CodigoParameter, eqtra_DescripcionParameter, eqtra_ObservacionParameter, eqtra_UsuarioModificaParameter, eqtra_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbfasesReclutamiento_Delete_Result> UDP_RRHH_tbfasesReclutamiento_Delete(Nullable<int> fare_Id, string fare_razon_Inactivo, Nullable<int> fare_UsuarioModifica, Nullable<System.DateTime> fare_FechaModifica)
        {
            var fare_IdParameter = fare_Id.HasValue ?
                new ObjectParameter("fare_Id", fare_Id) :
                new ObjectParameter("fare_Id", typeof(int));
    
            var fare_razon_InactivoParameter = fare_razon_Inactivo != null ?
                new ObjectParameter("fare_razon_Inactivo", fare_razon_Inactivo) :
                new ObjectParameter("fare_razon_Inactivo", typeof(string));
    
            var fare_UsuarioModificaParameter = fare_UsuarioModifica.HasValue ?
                new ObjectParameter("fare_UsuarioModifica", fare_UsuarioModifica) :
                new ObjectParameter("fare_UsuarioModifica", typeof(int));
    
            var fare_FechaModificaParameter = fare_FechaModifica.HasValue ?
                new ObjectParameter("fare_FechaModifica", fare_FechaModifica) :
                new ObjectParameter("fare_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbfasesReclutamiento_Delete_Result>("UDP_RRHH_tbfasesReclutamiento_Delete", fare_IdParameter, fare_razon_InactivoParameter, fare_UsuarioModificaParameter, fare_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbFasesReclutamiento_Insert_Result> UDP_RRHH_tbFasesReclutamiento_Insert(string fare_Descripcion, Nullable<int> fare_UsuarioCrea, Nullable<System.DateTime> fare_FechaCrea)
        {
            var fare_DescripcionParameter = fare_Descripcion != null ?
                new ObjectParameter("fare_Descripcion", fare_Descripcion) :
                new ObjectParameter("fare_Descripcion", typeof(string));
    
            var fare_UsuarioCreaParameter = fare_UsuarioCrea.HasValue ?
                new ObjectParameter("fare_UsuarioCrea", fare_UsuarioCrea) :
                new ObjectParameter("fare_UsuarioCrea", typeof(int));
    
            var fare_FechaCreaParameter = fare_FechaCrea.HasValue ?
                new ObjectParameter("fare_FechaCrea", fare_FechaCrea) :
                new ObjectParameter("fare_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbFasesReclutamiento_Insert_Result>("UDP_RRHH_tbFasesReclutamiento_Insert", fare_DescripcionParameter, fare_UsuarioCreaParameter, fare_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbfasesReclutamiento_Restore_Result> UDP_RRHH_tbfasesReclutamiento_Restore(Nullable<int> fare_Id, Nullable<int> fare_UsuarioModifica, Nullable<System.DateTime> fare_FechaModifica)
        {
            var fare_IdParameter = fare_Id.HasValue ?
                new ObjectParameter("fare_Id", fare_Id) :
                new ObjectParameter("fare_Id", typeof(int));
    
            var fare_UsuarioModificaParameter = fare_UsuarioModifica.HasValue ?
                new ObjectParameter("fare_UsuarioModifica", fare_UsuarioModifica) :
                new ObjectParameter("fare_UsuarioModifica", typeof(int));
    
            var fare_FechaModificaParameter = fare_FechaModifica.HasValue ?
                new ObjectParameter("fare_FechaModifica", fare_FechaModifica) :
                new ObjectParameter("fare_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbfasesReclutamiento_Restore_Result>("UDP_RRHH_tbfasesReclutamiento_Restore", fare_IdParameter, fare_UsuarioModificaParameter, fare_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbFasesReclutamiento_Update_Result> UDP_RRHH_tbFasesReclutamiento_Update(Nullable<int> fare_Id, string fare_Descripcion, Nullable<int> fare_UsuarioModifica, Nullable<System.DateTime> fare_FechaModifica)
        {
            var fare_IdParameter = fare_Id.HasValue ?
                new ObjectParameter("fare_Id", fare_Id) :
                new ObjectParameter("fare_Id", typeof(int));
    
            var fare_DescripcionParameter = fare_Descripcion != null ?
                new ObjectParameter("fare_Descripcion", fare_Descripcion) :
                new ObjectParameter("fare_Descripcion", typeof(string));
    
            var fare_UsuarioModificaParameter = fare_UsuarioModifica.HasValue ?
                new ObjectParameter("fare_UsuarioModifica", fare_UsuarioModifica) :
                new ObjectParameter("fare_UsuarioModifica", typeof(int));
    
            var fare_FechaModificaParameter = fare_FechaModifica.HasValue ?
                new ObjectParameter("fare_FechaModifica", fare_FechaModifica) :
                new ObjectParameter("fare_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbFasesReclutamiento_Update_Result>("UDP_RRHH_tbFasesReclutamiento_Update", fare_IdParameter, fare_DescripcionParameter, fare_UsuarioModificaParameter, fare_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHabilidades_Delete_Result> UDP_RRHH_tbHabilidades_Delete(Nullable<int> habi_id, string habi_razon_Inactivo, Nullable<int> habi_UsuarioModifica, Nullable<System.DateTime> habi_FechaModifica)
        {
            var habi_idParameter = habi_id.HasValue ?
                new ObjectParameter("habi_id", habi_id) :
                new ObjectParameter("habi_id", typeof(int));
    
            var habi_razon_InactivoParameter = habi_razon_Inactivo != null ?
                new ObjectParameter("habi_razon_Inactivo", habi_razon_Inactivo) :
                new ObjectParameter("habi_razon_Inactivo", typeof(string));
    
            var habi_UsuarioModificaParameter = habi_UsuarioModifica.HasValue ?
                new ObjectParameter("habi_UsuarioModifica", habi_UsuarioModifica) :
                new ObjectParameter("habi_UsuarioModifica", typeof(int));
    
            var habi_FechaModificaParameter = habi_FechaModifica.HasValue ?
                new ObjectParameter("habi_FechaModifica", habi_FechaModifica) :
                new ObjectParameter("habi_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHabilidades_Delete_Result>("UDP_RRHH_tbHabilidades_Delete", habi_idParameter, habi_razon_InactivoParameter, habi_UsuarioModificaParameter, habi_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHabilidades_Insert_Result> UDP_RRHH_tbHabilidades_Insert(string habi_Descripcion, Nullable<int> habi_UsuarioCrea, Nullable<System.DateTime> habi_FechaCrea)
        {
            var habi_DescripcionParameter = habi_Descripcion != null ?
                new ObjectParameter("habi_Descripcion", habi_Descripcion) :
                new ObjectParameter("habi_Descripcion", typeof(string));
    
            var habi_UsuarioCreaParameter = habi_UsuarioCrea.HasValue ?
                new ObjectParameter("habi_UsuarioCrea", habi_UsuarioCrea) :
                new ObjectParameter("habi_UsuarioCrea", typeof(int));
    
            var habi_FechaCreaParameter = habi_FechaCrea.HasValue ?
                new ObjectParameter("habi_FechaCrea", habi_FechaCrea) :
                new ObjectParameter("habi_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHabilidades_Insert_Result>("UDP_RRHH_tbHabilidades_Insert", habi_DescripcionParameter, habi_UsuarioCreaParameter, habi_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHabilidades_Restore_Result> UDP_RRHH_tbHabilidades_Restore(Nullable<int> habi_id, Nullable<int> habi_UsuarioModifica, Nullable<System.DateTime> habi_FechaModifica)
        {
            var habi_idParameter = habi_id.HasValue ?
                new ObjectParameter("habi_id", habi_id) :
                new ObjectParameter("habi_id", typeof(int));
    
            var habi_UsuarioModificaParameter = habi_UsuarioModifica.HasValue ?
                new ObjectParameter("habi_UsuarioModifica", habi_UsuarioModifica) :
                new ObjectParameter("habi_UsuarioModifica", typeof(int));
    
            var habi_FechaModificaParameter = habi_FechaModifica.HasValue ?
                new ObjectParameter("habi_FechaModifica", habi_FechaModifica) :
                new ObjectParameter("habi_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHabilidades_Restore_Result>("UDP_RRHH_tbHabilidades_Restore", habi_idParameter, habi_UsuarioModificaParameter, habi_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHabilidades_Update_Result> UDP_RRHH_tbHabilidades_Update(Nullable<int> habi_Id, string habi_Descripcion, Nullable<int> habi_UsuarioModifica, Nullable<System.DateTime> habi_FechaModifica)
        {
            var habi_IdParameter = habi_Id.HasValue ?
                new ObjectParameter("habi_Id", habi_Id) :
                new ObjectParameter("habi_Id", typeof(int));
    
            var habi_DescripcionParameter = habi_Descripcion != null ?
                new ObjectParameter("habi_Descripcion", habi_Descripcion) :
                new ObjectParameter("habi_Descripcion", typeof(string));
    
            var habi_UsuarioModificaParameter = habi_UsuarioModifica.HasValue ?
                new ObjectParameter("habi_UsuarioModifica", habi_UsuarioModifica) :
                new ObjectParameter("habi_UsuarioModifica", typeof(int));
    
            var habi_FechaModificaParameter = habi_FechaModifica.HasValue ?
                new ObjectParameter("habi_FechaModifica", habi_FechaModifica) :
                new ObjectParameter("habi_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHabilidades_Update_Result>("UDP_RRHH_tbHabilidades_Update", habi_IdParameter, habi_DescripcionParameter, habi_UsuarioModificaParameter, habi_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHabilidadesPersona_Inactivar_Result> UDP_RRHH_tbHabilidadesPersona_Inactivar(Nullable<int> hape_Id, string hape_RazonInactivo, Nullable<int> hape_UsuarioModifica, Nullable<System.DateTime> hape_FechaModifica)
        {
            var hape_IdParameter = hape_Id.HasValue ?
                new ObjectParameter("hape_Id", hape_Id) :
                new ObjectParameter("hape_Id", typeof(int));
    
            var hape_RazonInactivoParameter = hape_RazonInactivo != null ?
                new ObjectParameter("hape_RazonInactivo", hape_RazonInactivo) :
                new ObjectParameter("hape_RazonInactivo", typeof(string));
    
            var hape_UsuarioModificaParameter = hape_UsuarioModifica.HasValue ?
                new ObjectParameter("hape_UsuarioModifica", hape_UsuarioModifica) :
                new ObjectParameter("hape_UsuarioModifica", typeof(int));
    
            var hape_FechaModificaParameter = hape_FechaModifica.HasValue ?
                new ObjectParameter("hape_FechaModifica", hape_FechaModifica) :
                new ObjectParameter("hape_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHabilidadesPersona_Inactivar_Result>("UDP_RRHH_tbHabilidadesPersona_Inactivar", hape_IdParameter, hape_RazonInactivoParameter, hape_UsuarioModificaParameter, hape_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHabilidadesPersona_Insert_Result> UDP_RRHH_tbHabilidadesPersona_Insert(Nullable<int> per_Id, Nullable<int> habi_Id, Nullable<int> hape_UsuarioCrea, Nullable<System.DateTime> hape_FechaCrea)
        {
            var per_IdParameter = per_Id.HasValue ?
                new ObjectParameter("per_Id", per_Id) :
                new ObjectParameter("per_Id", typeof(int));
    
            var habi_IdParameter = habi_Id.HasValue ?
                new ObjectParameter("habi_Id", habi_Id) :
                new ObjectParameter("habi_Id", typeof(int));
    
            var hape_UsuarioCreaParameter = hape_UsuarioCrea.HasValue ?
                new ObjectParameter("hape_UsuarioCrea", hape_UsuarioCrea) :
                new ObjectParameter("hape_UsuarioCrea", typeof(int));
    
            var hape_FechaCreaParameter = hape_FechaCrea.HasValue ?
                new ObjectParameter("hape_FechaCrea", hape_FechaCrea) :
                new ObjectParameter("hape_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHabilidadesPersona_Insert_Result>("UDP_RRHH_tbHabilidadesPersona_Insert", per_IdParameter, habi_IdParameter, hape_UsuarioCreaParameter, hape_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHabilidadesPersona_Restore_Result> UDP_RRHH_tbHabilidadesPersona_Restore(Nullable<int> hape_Id, Nullable<int> hape_UsuarioModifica, Nullable<System.DateTime> hape_FechaModifica)
        {
            var hape_IdParameter = hape_Id.HasValue ?
                new ObjectParameter("hape_Id", hape_Id) :
                new ObjectParameter("hape_Id", typeof(int));
    
            var hape_UsuarioModificaParameter = hape_UsuarioModifica.HasValue ?
                new ObjectParameter("hape_UsuarioModifica", hape_UsuarioModifica) :
                new ObjectParameter("hape_UsuarioModifica", typeof(int));
    
            var hape_FechaModificaParameter = hape_FechaModifica.HasValue ?
                new ObjectParameter("hape_FechaModifica", hape_FechaModifica) :
                new ObjectParameter("hape_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHabilidadesPersona_Restore_Result>("UDP_RRHH_tbHabilidadesPersona_Restore", hape_IdParameter, hape_UsuarioModificaParameter, hape_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialAmonestaciones_Delete_Result> UDP_RRHH_tbHistorialAmonestaciones_Delete(Nullable<int> hamo_Id, string hamo_RazonInactivo, Nullable<int> hamo_UsuarioModifica, Nullable<System.DateTime> hamo_FechaModifica)
        {
            var hamo_IdParameter = hamo_Id.HasValue ?
                new ObjectParameter("hamo_Id", hamo_Id) :
                new ObjectParameter("hamo_Id", typeof(int));
    
            var hamo_RazonInactivoParameter = hamo_RazonInactivo != null ?
                new ObjectParameter("hamo_RazonInactivo", hamo_RazonInactivo) :
                new ObjectParameter("hamo_RazonInactivo", typeof(string));
    
            var hamo_UsuarioModificaParameter = hamo_UsuarioModifica.HasValue ?
                new ObjectParameter("hamo_UsuarioModifica", hamo_UsuarioModifica) :
                new ObjectParameter("hamo_UsuarioModifica", typeof(int));
    
            var hamo_FechaModificaParameter = hamo_FechaModifica.HasValue ?
                new ObjectParameter("hamo_FechaModifica", hamo_FechaModifica) :
                new ObjectParameter("hamo_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialAmonestaciones_Delete_Result>("UDP_RRHH_tbHistorialAmonestaciones_Delete", hamo_IdParameter, hamo_RazonInactivoParameter, hamo_UsuarioModificaParameter, hamo_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialAmonestaciones_Insert_Result> UDP_RRHH_tbHistorialAmonestaciones_Insert(Nullable<int> emp_Id, Nullable<int> tamo_Id, Nullable<System.DateTime> hamo_Fecha, string hamo_Observacion, Nullable<int> hamo_UsuarioCrea, Nullable<System.DateTime> hamo_FechaCrea)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var tamo_IdParameter = tamo_Id.HasValue ?
                new ObjectParameter("tamo_Id", tamo_Id) :
                new ObjectParameter("tamo_Id", typeof(int));
    
            var hamo_FechaParameter = hamo_Fecha.HasValue ?
                new ObjectParameter("hamo_Fecha", hamo_Fecha) :
                new ObjectParameter("hamo_Fecha", typeof(System.DateTime));
    
            var hamo_ObservacionParameter = hamo_Observacion != null ?
                new ObjectParameter("hamo_Observacion", hamo_Observacion) :
                new ObjectParameter("hamo_Observacion", typeof(string));
    
            var hamo_UsuarioCreaParameter = hamo_UsuarioCrea.HasValue ?
                new ObjectParameter("hamo_UsuarioCrea", hamo_UsuarioCrea) :
                new ObjectParameter("hamo_UsuarioCrea", typeof(int));
    
            var hamo_FechaCreaParameter = hamo_FechaCrea.HasValue ?
                new ObjectParameter("hamo_FechaCrea", hamo_FechaCrea) :
                new ObjectParameter("hamo_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialAmonestaciones_Insert_Result>("UDP_RRHH_tbHistorialAmonestaciones_Insert", emp_IdParameter, tamo_IdParameter, hamo_FechaParameter, hamo_ObservacionParameter, hamo_UsuarioCreaParameter, hamo_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialAmonestaciones_Restore_Result> UDP_RRHH_tbHistorialAmonestaciones_Restore(Nullable<int> hamo_Id, Nullable<int> hamo_UsuarioModifica, Nullable<System.DateTime> hamo_FechaModifica)
        {
            var hamo_IdParameter = hamo_Id.HasValue ?
                new ObjectParameter("hamo_Id", hamo_Id) :
                new ObjectParameter("hamo_Id", typeof(int));
    
            var hamo_UsuarioModificaParameter = hamo_UsuarioModifica.HasValue ?
                new ObjectParameter("hamo_UsuarioModifica", hamo_UsuarioModifica) :
                new ObjectParameter("hamo_UsuarioModifica", typeof(int));
    
            var hamo_FechaModificaParameter = hamo_FechaModifica.HasValue ?
                new ObjectParameter("hamo_FechaModifica", hamo_FechaModifica) :
                new ObjectParameter("hamo_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialAmonestaciones_Restore_Result>("UDP_RRHH_tbHistorialAmonestaciones_Restore", hamo_IdParameter, hamo_UsuarioModificaParameter, hamo_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialAudienciaDescargo_Delete_Result> UDP_RRHH_tbHistorialAudienciaDescargo_Delete(Nullable<int> aude_Id, string aude_RazonInactivo, Nullable<int> aude_UsuarioModifica, Nullable<System.DateTime> aude_FechaModifica)
        {
            var aude_IdParameter = aude_Id.HasValue ?
                new ObjectParameter("aude_Id", aude_Id) :
                new ObjectParameter("aude_Id", typeof(int));
    
            var aude_RazonInactivoParameter = aude_RazonInactivo != null ?
                new ObjectParameter("aude_RazonInactivo", aude_RazonInactivo) :
                new ObjectParameter("aude_RazonInactivo", typeof(string));
    
            var aude_UsuarioModificaParameter = aude_UsuarioModifica.HasValue ?
                new ObjectParameter("aude_UsuarioModifica", aude_UsuarioModifica) :
                new ObjectParameter("aude_UsuarioModifica", typeof(int));
    
            var aude_FechaModificaParameter = aude_FechaModifica.HasValue ?
                new ObjectParameter("aude_FechaModifica", aude_FechaModifica) :
                new ObjectParameter("aude_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialAudienciaDescargo_Delete_Result>("UDP_RRHH_tbHistorialAudienciaDescargo_Delete", aude_IdParameter, aude_RazonInactivoParameter, aude_UsuarioModificaParameter, aude_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialAudienciaDescargo_Insert_Result> UDP_RRHH_tbHistorialAudienciaDescargo_Insert(Nullable<int> emp_Id, string aude_Descripcion, Nullable<System.DateTime> aude_FechaAudiencia, Nullable<bool> aude_Testigo, string aude_DireccionArchivo, Nullable<int> aude_UsuarioCrea, Nullable<System.DateTime> aude_FechaCrea)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var aude_DescripcionParameter = aude_Descripcion != null ?
                new ObjectParameter("aude_Descripcion", aude_Descripcion) :
                new ObjectParameter("aude_Descripcion", typeof(string));
    
            var aude_FechaAudienciaParameter = aude_FechaAudiencia.HasValue ?
                new ObjectParameter("aude_FechaAudiencia", aude_FechaAudiencia) :
                new ObjectParameter("aude_FechaAudiencia", typeof(System.DateTime));
    
            var aude_TestigoParameter = aude_Testigo.HasValue ?
                new ObjectParameter("aude_Testigo", aude_Testigo) :
                new ObjectParameter("aude_Testigo", typeof(bool));
    
            var aude_DireccionArchivoParameter = aude_DireccionArchivo != null ?
                new ObjectParameter("aude_DireccionArchivo", aude_DireccionArchivo) :
                new ObjectParameter("aude_DireccionArchivo", typeof(string));
    
            var aude_UsuarioCreaParameter = aude_UsuarioCrea.HasValue ?
                new ObjectParameter("aude_UsuarioCrea", aude_UsuarioCrea) :
                new ObjectParameter("aude_UsuarioCrea", typeof(int));
    
            var aude_FechaCreaParameter = aude_FechaCrea.HasValue ?
                new ObjectParameter("aude_FechaCrea", aude_FechaCrea) :
                new ObjectParameter("aude_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialAudienciaDescargo_Insert_Result>("UDP_RRHH_tbHistorialAudienciaDescargo_Insert", emp_IdParameter, aude_DescripcionParameter, aude_FechaAudienciaParameter, aude_TestigoParameter, aude_DireccionArchivoParameter, aude_UsuarioCreaParameter, aude_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialAudienciaDescargo_Restore_Result> UDP_RRHH_tbHistorialAudienciaDescargo_Restore(Nullable<int> aude_Id, Nullable<int> aude_UsuarioModifica, Nullable<System.DateTime> aude_FechaModifica)
        {
            var aude_IdParameter = aude_Id.HasValue ?
                new ObjectParameter("aude_Id", aude_Id) :
                new ObjectParameter("aude_Id", typeof(int));
    
            var aude_UsuarioModificaParameter = aude_UsuarioModifica.HasValue ?
                new ObjectParameter("aude_UsuarioModifica", aude_UsuarioModifica) :
                new ObjectParameter("aude_UsuarioModifica", typeof(int));
    
            var aude_FechaModificaParameter = aude_FechaModifica.HasValue ?
                new ObjectParameter("aude_FechaModifica", aude_FechaModifica) :
                new ObjectParameter("aude_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialAudienciaDescargo_Restore_Result>("UDP_RRHH_tbHistorialAudienciaDescargo_Restore", aude_IdParameter, aude_UsuarioModificaParameter, aude_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialAudienciaDescargo_Update_Result> UDP_RRHH_tbHistorialAudienciaDescargo_Update(Nullable<int> aude_Id, Nullable<System.DateTime> aude_FechaAudiencia, Nullable<int> aude_UsuarioModifica, Nullable<System.DateTime> aude_FechaModifica)
        {
            var aude_IdParameter = aude_Id.HasValue ?
                new ObjectParameter("aude_Id", aude_Id) :
                new ObjectParameter("aude_Id", typeof(int));
    
            var aude_FechaAudienciaParameter = aude_FechaAudiencia.HasValue ?
                new ObjectParameter("aude_FechaAudiencia", aude_FechaAudiencia) :
                new ObjectParameter("aude_FechaAudiencia", typeof(System.DateTime));
    
            var aude_UsuarioModificaParameter = aude_UsuarioModifica.HasValue ?
                new ObjectParameter("aude_UsuarioModifica", aude_UsuarioModifica) :
                new ObjectParameter("aude_UsuarioModifica", typeof(int));
    
            var aude_FechaModificaParameter = aude_FechaModifica.HasValue ?
                new ObjectParameter("aude_FechaModifica", aude_FechaModifica) :
                new ObjectParameter("aude_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialAudienciaDescargo_Update_Result>("UDP_RRHH_tbHistorialAudienciaDescargo_Update", aude_IdParameter, aude_FechaAudienciaParameter, aude_UsuarioModificaParameter, aude_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialCargos_Degradar_Result> UDP_RRHH_tbHistorialCargos_Degradar(Nullable<int> hcar_Id, string hcar_RazonPromocion, Nullable<System.DateTime> hcar_Fecha, Nullable<int> hcar_UsuarioModifica, Nullable<System.DateTime> hcar_FechaModifica)
        {
            var hcar_IdParameter = hcar_Id.HasValue ?
                new ObjectParameter("hcar_Id", hcar_Id) :
                new ObjectParameter("hcar_Id", typeof(int));
    
            var hcar_RazonPromocionParameter = hcar_RazonPromocion != null ?
                new ObjectParameter("hcar_RazonPromocion", hcar_RazonPromocion) :
                new ObjectParameter("hcar_RazonPromocion", typeof(string));
    
            var hcar_FechaParameter = hcar_Fecha.HasValue ?
                new ObjectParameter("hcar_Fecha", hcar_Fecha) :
                new ObjectParameter("hcar_Fecha", typeof(System.DateTime));
    
            var hcar_UsuarioModificaParameter = hcar_UsuarioModifica.HasValue ?
                new ObjectParameter("hcar_UsuarioModifica", hcar_UsuarioModifica) :
                new ObjectParameter("hcar_UsuarioModifica", typeof(int));
    
            var hcar_FechaModificaParameter = hcar_FechaModifica.HasValue ?
                new ObjectParameter("hcar_FechaModifica", hcar_FechaModifica) :
                new ObjectParameter("hcar_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialCargos_Degradar_Result>("UDP_RRHH_tbHistorialCargos_Degradar", hcar_IdParameter, hcar_RazonPromocionParameter, hcar_FechaParameter, hcar_UsuarioModificaParameter, hcar_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialCargos_Insert_Result> UDP_RRHH_tbHistorialCargos_Insert(Nullable<int> emp_Id, Nullable<int> car_Id, Nullable<int> area_Id, Nullable<int> depto_Id, Nullable<int> jor_Id, Nullable<decimal> sue_Cantidad, string hcar_RazonPromocion, Nullable<System.DateTime> hcar_Fecha, Nullable<int> req_Id, Nullable<int> emp_UsuarioModifica, Nullable<System.DateTime> emp_FechaModifica)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var car_IdParameter = car_Id.HasValue ?
                new ObjectParameter("car_Id", car_Id) :
                new ObjectParameter("car_Id", typeof(int));
    
            var area_IdParameter = area_Id.HasValue ?
                new ObjectParameter("area_Id", area_Id) :
                new ObjectParameter("area_Id", typeof(int));
    
            var depto_IdParameter = depto_Id.HasValue ?
                new ObjectParameter("depto_Id", depto_Id) :
                new ObjectParameter("depto_Id", typeof(int));
    
            var jor_IdParameter = jor_Id.HasValue ?
                new ObjectParameter("jor_Id", jor_Id) :
                new ObjectParameter("jor_Id", typeof(int));
    
            var sue_CantidadParameter = sue_Cantidad.HasValue ?
                new ObjectParameter("sue_Cantidad", sue_Cantidad) :
                new ObjectParameter("sue_Cantidad", typeof(decimal));
    
            var hcar_RazonPromocionParameter = hcar_RazonPromocion != null ?
                new ObjectParameter("hcar_RazonPromocion", hcar_RazonPromocion) :
                new ObjectParameter("hcar_RazonPromocion", typeof(string));
    
            var hcar_FechaParameter = hcar_Fecha.HasValue ?
                new ObjectParameter("hcar_Fecha", hcar_Fecha) :
                new ObjectParameter("hcar_Fecha", typeof(System.DateTime));
    
            var req_IdParameter = req_Id.HasValue ?
                new ObjectParameter("req_Id", req_Id) :
                new ObjectParameter("req_Id", typeof(int));
    
            var emp_UsuarioModificaParameter = emp_UsuarioModifica.HasValue ?
                new ObjectParameter("emp_UsuarioModifica", emp_UsuarioModifica) :
                new ObjectParameter("emp_UsuarioModifica", typeof(int));
    
            var emp_FechaModificaParameter = emp_FechaModifica.HasValue ?
                new ObjectParameter("emp_FechaModifica", emp_FechaModifica) :
                new ObjectParameter("emp_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialCargos_Insert_Result>("UDP_RRHH_tbHistorialCargos_Insert", emp_IdParameter, car_IdParameter, area_IdParameter, depto_IdParameter, jor_IdParameter, sue_CantidadParameter, hcar_RazonPromocionParameter, hcar_FechaParameter, req_IdParameter, emp_UsuarioModificaParameter, emp_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialContrataciones_Delete_Result> UDP_RRHH_tbHistorialContrataciones_Delete(Nullable<int> hcon_Id, string hcon_RazonInactivo, Nullable<int> hcon_UsuarioModifica, Nullable<System.DateTime> hcon_FechaModifica)
        {
            var hcon_IdParameter = hcon_Id.HasValue ?
                new ObjectParameter("hcon_Id", hcon_Id) :
                new ObjectParameter("hcon_Id", typeof(int));
    
            var hcon_RazonInactivoParameter = hcon_RazonInactivo != null ?
                new ObjectParameter("hcon_RazonInactivo", hcon_RazonInactivo) :
                new ObjectParameter("hcon_RazonInactivo", typeof(string));
    
            var hcon_UsuarioModificaParameter = hcon_UsuarioModifica.HasValue ?
                new ObjectParameter("hcon_UsuarioModifica", hcon_UsuarioModifica) :
                new ObjectParameter("hcon_UsuarioModifica", typeof(int));
    
            var hcon_FechaModificaParameter = hcon_FechaModifica.HasValue ?
                new ObjectParameter("hcon_FechaModifica", hcon_FechaModifica) :
                new ObjectParameter("hcon_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialContrataciones_Delete_Result>("UDP_RRHH_tbHistorialContrataciones_Delete", hcon_IdParameter, hcon_RazonInactivoParameter, hcon_UsuarioModificaParameter, hcon_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialContrataciones_Insert_Result> UDP_RRHH_tbHistorialContrataciones_Insert(Nullable<int> scan_Id, Nullable<int> depto_Id, Nullable<System.DateTime> hcon_FechaContratado, Nullable<int> hcon_UsuarioCrea, Nullable<System.DateTime> hcon_FechaCrea)
        {
            var scan_IdParameter = scan_Id.HasValue ?
                new ObjectParameter("scan_Id", scan_Id) :
                new ObjectParameter("scan_Id", typeof(int));
    
            var depto_IdParameter = depto_Id.HasValue ?
                new ObjectParameter("depto_Id", depto_Id) :
                new ObjectParameter("depto_Id", typeof(int));
    
            var hcon_FechaContratadoParameter = hcon_FechaContratado.HasValue ?
                new ObjectParameter("hcon_FechaContratado", hcon_FechaContratado) :
                new ObjectParameter("hcon_FechaContratado", typeof(System.DateTime));
    
            var hcon_UsuarioCreaParameter = hcon_UsuarioCrea.HasValue ?
                new ObjectParameter("hcon_UsuarioCrea", hcon_UsuarioCrea) :
                new ObjectParameter("hcon_UsuarioCrea", typeof(int));
    
            var hcon_FechaCreaParameter = hcon_FechaCrea.HasValue ?
                new ObjectParameter("hcon_FechaCrea", hcon_FechaCrea) :
                new ObjectParameter("hcon_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialContrataciones_Insert_Result>("UDP_RRHH_tbHistorialContrataciones_Insert", scan_IdParameter, depto_IdParameter, hcon_FechaContratadoParameter, hcon_UsuarioCreaParameter, hcon_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialContrataciones_Restore_Result> UDP_RRHH_tbHistorialContrataciones_Restore(Nullable<int> hcon_Id, Nullable<int> hcon_UsuarioModifica, Nullable<System.DateTime> hcon_FechaModifica)
        {
            var hcon_IdParameter = hcon_Id.HasValue ?
                new ObjectParameter("hcon_Id", hcon_Id) :
                new ObjectParameter("hcon_Id", typeof(int));
    
            var hcon_UsuarioModificaParameter = hcon_UsuarioModifica.HasValue ?
                new ObjectParameter("hcon_UsuarioModifica", hcon_UsuarioModifica) :
                new ObjectParameter("hcon_UsuarioModifica", typeof(int));
    
            var hcon_FechaModificaParameter = hcon_FechaModifica.HasValue ?
                new ObjectParameter("hcon_FechaModifica", hcon_FechaModifica) :
                new ObjectParameter("hcon_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialContrataciones_Restore_Result>("UDP_RRHH_tbHistorialContrataciones_Restore", hcon_IdParameter, hcon_UsuarioModificaParameter, hcon_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialContrataciones_Update_Result> UDP_RRHH_tbHistorialContrataciones_Update(Nullable<int> hcon_Id, Nullable<int> scan_Id, Nullable<int> depto_Id, Nullable<System.DateTime> hcon_FechaContratado, Nullable<int> hcon_UsuarioModifica, Nullable<System.DateTime> hcon_FechaModifica)
        {
            var hcon_IdParameter = hcon_Id.HasValue ?
                new ObjectParameter("hcon_Id", hcon_Id) :
                new ObjectParameter("hcon_Id", typeof(int));
    
            var scan_IdParameter = scan_Id.HasValue ?
                new ObjectParameter("scan_Id", scan_Id) :
                new ObjectParameter("scan_Id", typeof(int));
    
            var depto_IdParameter = depto_Id.HasValue ?
                new ObjectParameter("depto_Id", depto_Id) :
                new ObjectParameter("depto_Id", typeof(int));
    
            var hcon_FechaContratadoParameter = hcon_FechaContratado.HasValue ?
                new ObjectParameter("hcon_FechaContratado", hcon_FechaContratado) :
                new ObjectParameter("hcon_FechaContratado", typeof(System.DateTime));
    
            var hcon_UsuarioModificaParameter = hcon_UsuarioModifica.HasValue ?
                new ObjectParameter("hcon_UsuarioModifica", hcon_UsuarioModifica) :
                new ObjectParameter("hcon_UsuarioModifica", typeof(int));
    
            var hcon_FechaModificaParameter = hcon_FechaModifica.HasValue ?
                new ObjectParameter("hcon_FechaModifica", hcon_FechaModifica) :
                new ObjectParameter("hcon_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialContrataciones_Update_Result>("UDP_RRHH_tbHistorialContrataciones_Update", hcon_IdParameter, scan_IdParameter, depto_IdParameter, hcon_FechaContratadoParameter, hcon_UsuarioModificaParameter, hcon_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialIncapacidades_Delete_Result> UDP_RRHH_tbHistorialIncapacidades_Delete(Nullable<int> hinc_Id, string hinc_RazonInactivo, Nullable<int> hinc_UsuarioModifica, Nullable<System.DateTime> hinc_FechaModifica)
        {
            var hinc_IdParameter = hinc_Id.HasValue ?
                new ObjectParameter("hinc_Id", hinc_Id) :
                new ObjectParameter("hinc_Id", typeof(int));
    
            var hinc_RazonInactivoParameter = hinc_RazonInactivo != null ?
                new ObjectParameter("hinc_RazonInactivo", hinc_RazonInactivo) :
                new ObjectParameter("hinc_RazonInactivo", typeof(string));
    
            var hinc_UsuarioModificaParameter = hinc_UsuarioModifica.HasValue ?
                new ObjectParameter("hinc_UsuarioModifica", hinc_UsuarioModifica) :
                new ObjectParameter("hinc_UsuarioModifica", typeof(int));
    
            var hinc_FechaModificaParameter = hinc_FechaModifica.HasValue ?
                new ObjectParameter("hinc_FechaModifica", hinc_FechaModifica) :
                new ObjectParameter("hinc_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialIncapacidades_Delete_Result>("UDP_RRHH_tbHistorialIncapacidades_Delete", hinc_IdParameter, hinc_RazonInactivoParameter, hinc_UsuarioModificaParameter, hinc_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialIncapacidades_Insert_Result> UDP_RRHH_tbHistorialIncapacidades_Insert(Nullable<int> emp_Id, Nullable<int> ticn_Id, string hinc_CentroMedico, string hinc_Doctor, string hinc_Diagnostico, Nullable<System.DateTime> hinc_FechaInicio, Nullable<System.DateTime> hinc_FechaFin, Nullable<int> hinc_UsuarioCrea, Nullable<System.DateTime> hinc_FechaCrea, Nullable<bool> hinc_Espermanente)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("Emp_Id", emp_Id) :
                new ObjectParameter("Emp_Id", typeof(int));
    
            var ticn_IdParameter = ticn_Id.HasValue ?
                new ObjectParameter("ticn_Id", ticn_Id) :
                new ObjectParameter("ticn_Id", typeof(int));
    
            var hinc_CentroMedicoParameter = hinc_CentroMedico != null ?
                new ObjectParameter("hinc_CentroMedico", hinc_CentroMedico) :
                new ObjectParameter("hinc_CentroMedico", typeof(string));
    
            var hinc_DoctorParameter = hinc_Doctor != null ?
                new ObjectParameter("hinc_Doctor", hinc_Doctor) :
                new ObjectParameter("hinc_Doctor", typeof(string));
    
            var hinc_DiagnosticoParameter = hinc_Diagnostico != null ?
                new ObjectParameter("hinc_Diagnostico", hinc_Diagnostico) :
                new ObjectParameter("hinc_Diagnostico", typeof(string));
    
            var hinc_FechaInicioParameter = hinc_FechaInicio.HasValue ?
                new ObjectParameter("hinc_FechaInicio", hinc_FechaInicio) :
                new ObjectParameter("hinc_FechaInicio", typeof(System.DateTime));
    
            var hinc_FechaFinParameter = hinc_FechaFin.HasValue ?
                new ObjectParameter("hinc_FechaFin", hinc_FechaFin) :
                new ObjectParameter("hinc_FechaFin", typeof(System.DateTime));
    
            var hinc_UsuarioCreaParameter = hinc_UsuarioCrea.HasValue ?
                new ObjectParameter("hinc_UsuarioCrea", hinc_UsuarioCrea) :
                new ObjectParameter("hinc_UsuarioCrea", typeof(int));
    
            var hinc_FechaCreaParameter = hinc_FechaCrea.HasValue ?
                new ObjectParameter("hinc_FechaCrea", hinc_FechaCrea) :
                new ObjectParameter("hinc_FechaCrea", typeof(System.DateTime));
    
            var hinc_EspermanenteParameter = hinc_Espermanente.HasValue ?
                new ObjectParameter("hinc_Espermanente", hinc_Espermanente) :
                new ObjectParameter("hinc_Espermanente", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialIncapacidades_Insert_Result>("UDP_RRHH_tbHistorialIncapacidades_Insert", emp_IdParameter, ticn_IdParameter, hinc_CentroMedicoParameter, hinc_DoctorParameter, hinc_DiagnosticoParameter, hinc_FechaInicioParameter, hinc_FechaFinParameter, hinc_UsuarioCreaParameter, hinc_FechaCreaParameter, hinc_EspermanenteParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialIncapacidades_Restore_Result> UDP_RRHH_tbHistorialIncapacidades_Restore(Nullable<int> hinc_Id, Nullable<int> hinc_UsuarioModifica, Nullable<System.DateTime> hinc_FechaModifica)
        {
            var hinc_IdParameter = hinc_Id.HasValue ?
                new ObjectParameter("hinc_Id", hinc_Id) :
                new ObjectParameter("hinc_Id", typeof(int));
    
            var hinc_UsuarioModificaParameter = hinc_UsuarioModifica.HasValue ?
                new ObjectParameter("hinc_UsuarioModifica", hinc_UsuarioModifica) :
                new ObjectParameter("hinc_UsuarioModifica", typeof(int));
    
            var hinc_FechaModificaParameter = hinc_FechaModifica.HasValue ?
                new ObjectParameter("hinc_FechaModifica", hinc_FechaModifica) :
                new ObjectParameter("hinc_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialIncapacidades_Restore_Result>("UDP_RRHH_tbHistorialIncapacidades_Restore", hinc_IdParameter, hinc_UsuarioModificaParameter, hinc_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialPermisos_Delete_Result> UDP_RRHH_tbHistorialPermisos_Delete(Nullable<int> hper_Id, string hper_razon_Inactivo, Nullable<int> hper_UsuarioModifica, Nullable<System.DateTime> hper_FechaModifica)
        {
            var hper_IdParameter = hper_Id.HasValue ?
                new ObjectParameter("hper_Id", hper_Id) :
                new ObjectParameter("hper_Id", typeof(int));
    
            var hper_razon_InactivoParameter = hper_razon_Inactivo != null ?
                new ObjectParameter("hper_razon_Inactivo", hper_razon_Inactivo) :
                new ObjectParameter("hper_razon_Inactivo", typeof(string));
    
            var hper_UsuarioModificaParameter = hper_UsuarioModifica.HasValue ?
                new ObjectParameter("hper_UsuarioModifica", hper_UsuarioModifica) :
                new ObjectParameter("hper_UsuarioModifica", typeof(int));
    
            var hper_FechaModificaParameter = hper_FechaModifica.HasValue ?
                new ObjectParameter("hper_FechaModifica", hper_FechaModifica) :
                new ObjectParameter("hper_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialPermisos_Delete_Result>("UDP_RRHH_tbHistorialPermisos_Delete", hper_IdParameter, hper_razon_InactivoParameter, hper_UsuarioModificaParameter, hper_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialPermisos_Insert_Result> UDP_RRHH_tbHistorialPermisos_Insert(Nullable<int> emp_Id, Nullable<int> tper_Id, Nullable<System.DateTime> hper_fechaInicio, Nullable<System.DateTime> hper_fechaFin, string hper_Observacion, Nullable<bool> hper_Justificado, Nullable<int> hper_PorcentajeIndemnizado, Nullable<int> hper_UsuarioCrea, Nullable<System.DateTime> hper_FechaCrea)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var tper_IdParameter = tper_Id.HasValue ?
                new ObjectParameter("tper_Id", tper_Id) :
                new ObjectParameter("tper_Id", typeof(int));
    
            var hper_fechaInicioParameter = hper_fechaInicio.HasValue ?
                new ObjectParameter("hper_fechaInicio", hper_fechaInicio) :
                new ObjectParameter("hper_fechaInicio", typeof(System.DateTime));
    
            var hper_fechaFinParameter = hper_fechaFin.HasValue ?
                new ObjectParameter("hper_fechaFin", hper_fechaFin) :
                new ObjectParameter("hper_fechaFin", typeof(System.DateTime));
    
            var hper_ObservacionParameter = hper_Observacion != null ?
                new ObjectParameter("hper_Observacion", hper_Observacion) :
                new ObjectParameter("hper_Observacion", typeof(string));
    
            var hper_JustificadoParameter = hper_Justificado.HasValue ?
                new ObjectParameter("hper_Justificado", hper_Justificado) :
                new ObjectParameter("hper_Justificado", typeof(bool));
    
            var hper_PorcentajeIndemnizadoParameter = hper_PorcentajeIndemnizado.HasValue ?
                new ObjectParameter("hper_PorcentajeIndemnizado", hper_PorcentajeIndemnizado) :
                new ObjectParameter("hper_PorcentajeIndemnizado", typeof(int));
    
            var hper_UsuarioCreaParameter = hper_UsuarioCrea.HasValue ?
                new ObjectParameter("hper_UsuarioCrea", hper_UsuarioCrea) :
                new ObjectParameter("hper_UsuarioCrea", typeof(int));
    
            var hper_FechaCreaParameter = hper_FechaCrea.HasValue ?
                new ObjectParameter("hper_FechaCrea", hper_FechaCrea) :
                new ObjectParameter("hper_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialPermisos_Insert_Result>("UDP_RRHH_tbHistorialPermisos_Insert", emp_IdParameter, tper_IdParameter, hper_fechaInicioParameter, hper_fechaFinParameter, hper_ObservacionParameter, hper_JustificadoParameter, hper_PorcentajeIndemnizadoParameter, hper_UsuarioCreaParameter, hper_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialPermisos_Restore_Result> UDP_RRHH_tbHistorialPermisos_Restore(Nullable<int> hper_Id, Nullable<int> hper_UsuarioModifica, Nullable<System.DateTime> hper_FechaModifica)
        {
            var hper_IdParameter = hper_Id.HasValue ?
                new ObjectParameter("hper_Id", hper_Id) :
                new ObjectParameter("hper_Id", typeof(int));
    
            var hper_UsuarioModificaParameter = hper_UsuarioModifica.HasValue ?
                new ObjectParameter("hper_UsuarioModifica", hper_UsuarioModifica) :
                new ObjectParameter("hper_UsuarioModifica", typeof(int));
    
            var hper_FechaModificaParameter = hper_FechaModifica.HasValue ?
                new ObjectParameter("hper_FechaModifica", hper_FechaModifica) :
                new ObjectParameter("hper_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialPermisos_Restore_Result>("UDP_RRHH_tbHistorialPermisos_Restore", hper_IdParameter, hper_UsuarioModificaParameter, hper_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialPermisos_Update_Result> UDP_RRHH_tbHistorialPermisos_Update(Nullable<int> hper_Id, string hper_Observacion, Nullable<int> hper_UsuarioModifica, Nullable<System.DateTime> hper_FechaModifica)
        {
            var hper_IdParameter = hper_Id.HasValue ?
                new ObjectParameter("hper_Id", hper_Id) :
                new ObjectParameter("hper_Id", typeof(int));
    
            var hper_ObservacionParameter = hper_Observacion != null ?
                new ObjectParameter("hper_Observacion", hper_Observacion) :
                new ObjectParameter("hper_Observacion", typeof(string));
    
            var hper_UsuarioModificaParameter = hper_UsuarioModifica.HasValue ?
                new ObjectParameter("hper_UsuarioModifica", hper_UsuarioModifica) :
                new ObjectParameter("hper_UsuarioModifica", typeof(int));
    
            var hper_FechaModificaParameter = hper_FechaModifica.HasValue ?
                new ObjectParameter("hper_FechaModifica", hper_FechaModifica) :
                new ObjectParameter("hper_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialPermisos_Update_Result>("UDP_RRHH_tbHistorialPermisos_Update", hper_IdParameter, hper_ObservacionParameter, hper_UsuarioModificaParameter, hper_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialSalidas_Delete_Result> UDP_RRHH_tbHistorialSalidas_Delete(Nullable<int> hsal_Id, string hsal_RazonInactivo, Nullable<int> hsal_UsuarioModifica, Nullable<System.DateTime> hsal_FechaModifica)
        {
            var hsal_IdParameter = hsal_Id.HasValue ?
                new ObjectParameter("Hsal_Id", hsal_Id) :
                new ObjectParameter("Hsal_Id", typeof(int));
    
            var hsal_RazonInactivoParameter = hsal_RazonInactivo != null ?
                new ObjectParameter("hsal_RazonInactivo", hsal_RazonInactivo) :
                new ObjectParameter("hsal_RazonInactivo", typeof(string));
    
            var hsal_UsuarioModificaParameter = hsal_UsuarioModifica.HasValue ?
                new ObjectParameter("hsal_UsuarioModifica", hsal_UsuarioModifica) :
                new ObjectParameter("hsal_UsuarioModifica", typeof(int));
    
            var hsal_FechaModificaParameter = hsal_FechaModifica.HasValue ?
                new ObjectParameter("hsal_FechaModifica", hsal_FechaModifica) :
                new ObjectParameter("hsal_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialSalidas_Delete_Result>("UDP_RRHH_tbHistorialSalidas_Delete", hsal_IdParameter, hsal_RazonInactivoParameter, hsal_UsuarioModificaParameter, hsal_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialSalidas_Insert_Result> UDP_RRHH_tbHistorialSalidas_Insert(Nullable<int> emp_Id, Nullable<int> tsal_Id, Nullable<int> rsal_Id, Nullable<System.DateTime> hsal_FechaSalida, string hsal_Observacion, string emp_RazonInactivo, Nullable<int> hsal_UsuarioCrea, Nullable<System.DateTime> hsal_FechaCrea)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var tsal_IdParameter = tsal_Id.HasValue ?
                new ObjectParameter("tsal_Id", tsal_Id) :
                new ObjectParameter("tsal_Id", typeof(int));
    
            var rsal_IdParameter = rsal_Id.HasValue ?
                new ObjectParameter("rsal_Id", rsal_Id) :
                new ObjectParameter("rsal_Id", typeof(int));
    
            var hsal_FechaSalidaParameter = hsal_FechaSalida.HasValue ?
                new ObjectParameter("hsal_FechaSalida", hsal_FechaSalida) :
                new ObjectParameter("hsal_FechaSalida", typeof(System.DateTime));
    
            var hsal_ObservacionParameter = hsal_Observacion != null ?
                new ObjectParameter("hsal_Observacion", hsal_Observacion) :
                new ObjectParameter("hsal_Observacion", typeof(string));
    
            var emp_RazonInactivoParameter = emp_RazonInactivo != null ?
                new ObjectParameter("emp_RazonInactivo", emp_RazonInactivo) :
                new ObjectParameter("emp_RazonInactivo", typeof(string));
    
            var hsal_UsuarioCreaParameter = hsal_UsuarioCrea.HasValue ?
                new ObjectParameter("hsal_UsuarioCrea", hsal_UsuarioCrea) :
                new ObjectParameter("hsal_UsuarioCrea", typeof(int));
    
            var hsal_FechaCreaParameter = hsal_FechaCrea.HasValue ?
                new ObjectParameter("hsal_FechaCrea", hsal_FechaCrea) :
                new ObjectParameter("hsal_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialSalidas_Insert_Result>("UDP_RRHH_tbHistorialSalidas_Insert", emp_IdParameter, tsal_IdParameter, rsal_IdParameter, hsal_FechaSalidaParameter, hsal_ObservacionParameter, emp_RazonInactivoParameter, hsal_UsuarioCreaParameter, hsal_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialSalidas_Restore_Result> UDP_RRHH_tbHistorialSalidas_Restore(Nullable<int> hsal_Id, Nullable<int> hsal_UsuarioModifica, Nullable<System.DateTime> hsal_FechaModifica)
        {
            var hsal_IdParameter = hsal_Id.HasValue ?
                new ObjectParameter("Hsal_Id", hsal_Id) :
                new ObjectParameter("Hsal_Id", typeof(int));
    
            var hsal_UsuarioModificaParameter = hsal_UsuarioModifica.HasValue ?
                new ObjectParameter("hsal_UsuarioModifica", hsal_UsuarioModifica) :
                new ObjectParameter("hsal_UsuarioModifica", typeof(int));
    
            var hsal_FechaModificaParameter = hsal_FechaModifica.HasValue ?
                new ObjectParameter("hsal_FechaModifica", hsal_FechaModifica) :
                new ObjectParameter("hsal_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialSalidas_Restore_Result>("UDP_RRHH_tbHistorialSalidas_Restore", hsal_IdParameter, hsal_UsuarioModificaParameter, hsal_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialSalidas_Update_Result> UDP_RRHH_tbHistorialSalidas_Update(Nullable<int> hsal_Id, string hsal_Observacion, Nullable<int> hsal_UsuarioModifica, Nullable<System.DateTime> fare_FechaModifica)
        {
            var hsal_IdParameter = hsal_Id.HasValue ?
                new ObjectParameter("hsal_Id", hsal_Id) :
                new ObjectParameter("hsal_Id", typeof(int));
    
            var hsal_ObservacionParameter = hsal_Observacion != null ?
                new ObjectParameter("hsal_Observacion", hsal_Observacion) :
                new ObjectParameter("hsal_Observacion", typeof(string));
    
            var hsal_UsuarioModificaParameter = hsal_UsuarioModifica.HasValue ?
                new ObjectParameter("hsal_UsuarioModifica", hsal_UsuarioModifica) :
                new ObjectParameter("hsal_UsuarioModifica", typeof(int));
    
            var fare_FechaModificaParameter = fare_FechaModifica.HasValue ?
                new ObjectParameter("fare_FechaModifica", fare_FechaModifica) :
                new ObjectParameter("fare_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialSalidas_Update_Result>("UDP_RRHH_tbHistorialSalidas_Update", hsal_IdParameter, hsal_ObservacionParameter, hsal_UsuarioModificaParameter, fare_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialVacaciones_Delete_Result> UDP_RRHH_tbHistorialVacaciones_Delete(Nullable<int> hvac_Id, string hvac_RazonInactivo, Nullable<int> hvac_UsuarioModifica, Nullable<System.DateTime> hvac_FechaModifica)
        {
            var hvac_IdParameter = hvac_Id.HasValue ?
                new ObjectParameter("hvac_Id", hvac_Id) :
                new ObjectParameter("hvac_Id", typeof(int));
    
            var hvac_RazonInactivoParameter = hvac_RazonInactivo != null ?
                new ObjectParameter("hvac_RazonInactivo", hvac_RazonInactivo) :
                new ObjectParameter("hvac_RazonInactivo", typeof(string));
    
            var hvac_UsuarioModificaParameter = hvac_UsuarioModifica.HasValue ?
                new ObjectParameter("hvac_UsuarioModifica", hvac_UsuarioModifica) :
                new ObjectParameter("hvac_UsuarioModifica", typeof(int));
    
            var hvac_FechaModificaParameter = hvac_FechaModifica.HasValue ?
                new ObjectParameter("hvac_FechaModifica", hvac_FechaModifica) :
                new ObjectParameter("hvac_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialVacaciones_Delete_Result>("UDP_RRHH_tbHistorialVacaciones_Delete", hvac_IdParameter, hvac_RazonInactivoParameter, hvac_UsuarioModificaParameter, hvac_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialVacaciones_Insert_Result> UDP_RRHH_tbHistorialVacaciones_Insert(Nullable<int> emp_Id, Nullable<System.DateTime> hvac_FechaInicio, Nullable<System.DateTime> hvac_FechaFin, Nullable<int> hvac_UsuarioCrea, Nullable<System.DateTime> hvac_FechaCrea)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var hvac_FechaInicioParameter = hvac_FechaInicio.HasValue ?
                new ObjectParameter("hvac_FechaInicio", hvac_FechaInicio) :
                new ObjectParameter("hvac_FechaInicio", typeof(System.DateTime));
    
            var hvac_FechaFinParameter = hvac_FechaFin.HasValue ?
                new ObjectParameter("hvac_FechaFin", hvac_FechaFin) :
                new ObjectParameter("hvac_FechaFin", typeof(System.DateTime));
    
            var hvac_UsuarioCreaParameter = hvac_UsuarioCrea.HasValue ?
                new ObjectParameter("hvac_UsuarioCrea", hvac_UsuarioCrea) :
                new ObjectParameter("hvac_UsuarioCrea", typeof(int));
    
            var hvac_FechaCreaParameter = hvac_FechaCrea.HasValue ?
                new ObjectParameter("hvac_FechaCrea", hvac_FechaCrea) :
                new ObjectParameter("hvac_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialVacaciones_Insert_Result>("UDP_RRHH_tbHistorialVacaciones_Insert", emp_IdParameter, hvac_FechaInicioParameter, hvac_FechaFinParameter, hvac_UsuarioCreaParameter, hvac_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistorialVacaciones_Restore_Result> UDP_RRHH_tbHistorialVacaciones_Restore(Nullable<int> hvac_Id, Nullable<int> hvac_UsuarioModifica, Nullable<System.DateTime> hvac_FechaModifica)
        {
            var hvac_IdParameter = hvac_Id.HasValue ?
                new ObjectParameter("hvac_Id", hvac_Id) :
                new ObjectParameter("hvac_Id", typeof(int));
    
            var hvac_UsuarioModificaParameter = hvac_UsuarioModifica.HasValue ?
                new ObjectParameter("hvac_UsuarioModifica", hvac_UsuarioModifica) :
                new ObjectParameter("hvac_UsuarioModifica", typeof(int));
    
            var hvac_FechaModificaParameter = hvac_FechaModifica.HasValue ?
                new ObjectParameter("hvac_FechaModifica", hvac_FechaModifica) :
                new ObjectParameter("hvac_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistorialVacaciones_Restore_Result>("UDP_RRHH_tbHistorialVacaciones_Restore", hvac_IdParameter, hvac_UsuarioModificaParameter, hvac_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHistrialVacaciones_Periodos_Result> UDP_RRHH_tbHistrialVacaciones_Periodos(Nullable<int> emp_Id)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHistrialVacaciones_Periodos_Result>("UDP_RRHH_tbHistrialVacaciones_Periodos", emp_IdParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHorarios_Delete_Result> UDP_RRHH_tbHorarios_Delete(Nullable<int> hor_id, string hor_razonInactivo, Nullable<int> hor_UsuarioModifica, Nullable<System.DateTime> hor_FechaModifica)
        {
            var hor_idParameter = hor_id.HasValue ?
                new ObjectParameter("hor_id", hor_id) :
                new ObjectParameter("hor_id", typeof(int));
    
            var hor_razonInactivoParameter = hor_razonInactivo != null ?
                new ObjectParameter("hor_razonInactivo", hor_razonInactivo) :
                new ObjectParameter("hor_razonInactivo", typeof(string));
    
            var hor_UsuarioModificaParameter = hor_UsuarioModifica.HasValue ?
                new ObjectParameter("hor_UsuarioModifica", hor_UsuarioModifica) :
                new ObjectParameter("hor_UsuarioModifica", typeof(int));
    
            var hor_FechaModificaParameter = hor_FechaModifica.HasValue ?
                new ObjectParameter("hor_FechaModifica", hor_FechaModifica) :
                new ObjectParameter("hor_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHorarios_Delete_Result>("UDP_RRHH_tbHorarios_Delete", hor_idParameter, hor_razonInactivoParameter, hor_UsuarioModificaParameter, hor_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHorarios_Insert_Result> UDP_RRHH_tbHorarios_Insert(Nullable<int> jor_Id, string hor_Descripcion, Nullable<System.TimeSpan> hor_HoraIncio, Nullable<System.TimeSpan> hor_HoraFin, Nullable<int> hor_usuarioCrea, Nullable<System.DateTime> hor_FechaCrea)
        {
            var jor_IdParameter = jor_Id.HasValue ?
                new ObjectParameter("jor_Id", jor_Id) :
                new ObjectParameter("jor_Id", typeof(int));
    
            var hor_DescripcionParameter = hor_Descripcion != null ?
                new ObjectParameter("hor_Descripcion", hor_Descripcion) :
                new ObjectParameter("hor_Descripcion", typeof(string));
    
            var hor_HoraIncioParameter = hor_HoraIncio.HasValue ?
                new ObjectParameter("hor_HoraIncio", hor_HoraIncio) :
                new ObjectParameter("hor_HoraIncio", typeof(System.TimeSpan));
    
            var hor_HoraFinParameter = hor_HoraFin.HasValue ?
                new ObjectParameter("hor_HoraFin", hor_HoraFin) :
                new ObjectParameter("hor_HoraFin", typeof(System.TimeSpan));
    
            var hor_usuarioCreaParameter = hor_usuarioCrea.HasValue ?
                new ObjectParameter("hor_usuarioCrea", hor_usuarioCrea) :
                new ObjectParameter("hor_usuarioCrea", typeof(int));
    
            var hor_FechaCreaParameter = hor_FechaCrea.HasValue ?
                new ObjectParameter("hor_FechaCrea", hor_FechaCrea) :
                new ObjectParameter("hor_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHorarios_Insert_Result>("UDP_RRHH_tbHorarios_Insert", jor_IdParameter, hor_DescripcionParameter, hor_HoraIncioParameter, hor_HoraFinParameter, hor_usuarioCreaParameter, hor_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHorarios_Restore_Result> UDP_RRHH_tbHorarios_Restore(Nullable<int> hor_Id, string hor_RazonInactivo, Nullable<int> hor_UsuarioModifica, Nullable<System.DateTime> hor_FechaModifica)
        {
            var hor_IdParameter = hor_Id.HasValue ?
                new ObjectParameter("hor_Id", hor_Id) :
                new ObjectParameter("hor_Id", typeof(int));
    
            var hor_RazonInactivoParameter = hor_RazonInactivo != null ?
                new ObjectParameter("hor_RazonInactivo", hor_RazonInactivo) :
                new ObjectParameter("hor_RazonInactivo", typeof(string));
    
            var hor_UsuarioModificaParameter = hor_UsuarioModifica.HasValue ?
                new ObjectParameter("hor_UsuarioModifica", hor_UsuarioModifica) :
                new ObjectParameter("hor_UsuarioModifica", typeof(int));
    
            var hor_FechaModificaParameter = hor_FechaModifica.HasValue ?
                new ObjectParameter("hor_FechaModifica", hor_FechaModifica) :
                new ObjectParameter("hor_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHorarios_Restore_Result>("UDP_RRHH_tbHorarios_Restore", hor_IdParameter, hor_RazonInactivoParameter, hor_UsuarioModificaParameter, hor_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbHorarios_Update_Result> UDP_RRHH_tbHorarios_Update(Nullable<int> hor_id, string hor_Descripcion, Nullable<System.TimeSpan> hor_HoraIncio, Nullable<System.TimeSpan> hor_HoraFin, Nullable<int> hor_UsuarioModifica, Nullable<System.DateTime> hor_FechaModifica)
        {
            var hor_idParameter = hor_id.HasValue ?
                new ObjectParameter("hor_id", hor_id) :
                new ObjectParameter("hor_id", typeof(int));
    
            var hor_DescripcionParameter = hor_Descripcion != null ?
                new ObjectParameter("hor_Descripcion", hor_Descripcion) :
                new ObjectParameter("hor_Descripcion", typeof(string));
    
            var hor_HoraIncioParameter = hor_HoraIncio.HasValue ?
                new ObjectParameter("hor_HoraIncio", hor_HoraIncio) :
                new ObjectParameter("hor_HoraIncio", typeof(System.TimeSpan));
    
            var hor_HoraFinParameter = hor_HoraFin.HasValue ?
                new ObjectParameter("hor_HoraFin", hor_HoraFin) :
                new ObjectParameter("hor_HoraFin", typeof(System.TimeSpan));
    
            var hor_UsuarioModificaParameter = hor_UsuarioModifica.HasValue ?
                new ObjectParameter("hor_UsuarioModifica", hor_UsuarioModifica) :
                new ObjectParameter("hor_UsuarioModifica", typeof(int));
    
            var hor_FechaModificaParameter = hor_FechaModifica.HasValue ?
                new ObjectParameter("hor_FechaModifica", hor_FechaModifica) :
                new ObjectParameter("hor_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbHorarios_Update_Result>("UDP_RRHH_tbHorarios_Update", hor_idParameter, hor_DescripcionParameter, hor_HoraIncioParameter, hor_HoraFinParameter, hor_UsuarioModificaParameter, hor_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbIdiomas_Delete_Result> UDP_RRHH_tbIdiomas_Delete(Nullable<int> idi_Id, string idi_razon_Inactivo, Nullable<int> idi_UsuarioModifica, Nullable<System.DateTime> idi_FechaModifica)
        {
            var idi_IdParameter = idi_Id.HasValue ?
                new ObjectParameter("idi_Id", idi_Id) :
                new ObjectParameter("idi_Id", typeof(int));
    
            var idi_razon_InactivoParameter = idi_razon_Inactivo != null ?
                new ObjectParameter("idi_razon_Inactivo", idi_razon_Inactivo) :
                new ObjectParameter("idi_razon_Inactivo", typeof(string));
    
            var idi_UsuarioModificaParameter = idi_UsuarioModifica.HasValue ?
                new ObjectParameter("idi_UsuarioModifica", idi_UsuarioModifica) :
                new ObjectParameter("idi_UsuarioModifica", typeof(int));
    
            var idi_FechaModificaParameter = idi_FechaModifica.HasValue ?
                new ObjectParameter("idi_FechaModifica", idi_FechaModifica) :
                new ObjectParameter("idi_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbIdiomas_Delete_Result>("UDP_RRHH_tbIdiomas_Delete", idi_IdParameter, idi_razon_InactivoParameter, idi_UsuarioModificaParameter, idi_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbIdiomas_Insert_Result> UDP_RRHH_tbIdiomas_Insert(string idi_Descripcion, Nullable<int> idi_UsuarioCrea, Nullable<System.DateTime> idi_FechaCrea)
        {
            var idi_DescripcionParameter = idi_Descripcion != null ?
                new ObjectParameter("idi_Descripcion", idi_Descripcion) :
                new ObjectParameter("idi_Descripcion", typeof(string));
    
            var idi_UsuarioCreaParameter = idi_UsuarioCrea.HasValue ?
                new ObjectParameter("idi_UsuarioCrea", idi_UsuarioCrea) :
                new ObjectParameter("idi_UsuarioCrea", typeof(int));
    
            var idi_FechaCreaParameter = idi_FechaCrea.HasValue ?
                new ObjectParameter("idi_FechaCrea", idi_FechaCrea) :
                new ObjectParameter("idi_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbIdiomas_Insert_Result>("UDP_RRHH_tbIdiomas_Insert", idi_DescripcionParameter, idi_UsuarioCreaParameter, idi_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbIdiomas_Restore_Result> UDP_RRHH_tbIdiomas_Restore(Nullable<int> idi_Id, Nullable<int> idi_UsuarioModifica, Nullable<System.DateTime> idi_FechaModifica)
        {
            var idi_IdParameter = idi_Id.HasValue ?
                new ObjectParameter("idi_Id", idi_Id) :
                new ObjectParameter("idi_Id", typeof(int));
    
            var idi_UsuarioModificaParameter = idi_UsuarioModifica.HasValue ?
                new ObjectParameter("idi_UsuarioModifica", idi_UsuarioModifica) :
                new ObjectParameter("idi_UsuarioModifica", typeof(int));
    
            var idi_FechaModificaParameter = idi_FechaModifica.HasValue ?
                new ObjectParameter("idi_FechaModifica", idi_FechaModifica) :
                new ObjectParameter("idi_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbIdiomas_Restore_Result>("UDP_RRHH_tbIdiomas_Restore", idi_IdParameter, idi_UsuarioModificaParameter, idi_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbIdiomas_Update_Result> UDP_RRHH_tbIdiomas_Update(Nullable<int> idi_Id, string idi_Descripcion, Nullable<int> idi_UsuarioModifica, Nullable<System.DateTime> idi_FechaModifica)
        {
            var idi_IdParameter = idi_Id.HasValue ?
                new ObjectParameter("idi_Id", idi_Id) :
                new ObjectParameter("idi_Id", typeof(int));
    
            var idi_DescripcionParameter = idi_Descripcion != null ?
                new ObjectParameter("idi_Descripcion", idi_Descripcion) :
                new ObjectParameter("idi_Descripcion", typeof(string));
    
            var idi_UsuarioModificaParameter = idi_UsuarioModifica.HasValue ?
                new ObjectParameter("idi_UsuarioModifica", idi_UsuarioModifica) :
                new ObjectParameter("idi_UsuarioModifica", typeof(int));
    
            var idi_FechaModificaParameter = idi_FechaModifica.HasValue ?
                new ObjectParameter("idi_FechaModifica", idi_FechaModifica) :
                new ObjectParameter("idi_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbIdiomas_Update_Result>("UDP_RRHH_tbIdiomas_Update", idi_IdParameter, idi_DescripcionParameter, idi_UsuarioModificaParameter, idi_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbIdiomasPersona_Inactivar_Result> UDP_RRHH_tbIdiomasPersona_Inactivar(Nullable<int> idpe_Id, string idpe_RazonInactivo, Nullable<int> idpe_UsuarioModifica, Nullable<System.DateTime> idpe_FechaModifica)
        {
            var idpe_IdParameter = idpe_Id.HasValue ?
                new ObjectParameter("idpe_Id", idpe_Id) :
                new ObjectParameter("idpe_Id", typeof(int));
    
            var idpe_RazonInactivoParameter = idpe_RazonInactivo != null ?
                new ObjectParameter("idpe_RazonInactivo", idpe_RazonInactivo) :
                new ObjectParameter("idpe_RazonInactivo", typeof(string));
    
            var idpe_UsuarioModificaParameter = idpe_UsuarioModifica.HasValue ?
                new ObjectParameter("idpe_UsuarioModifica", idpe_UsuarioModifica) :
                new ObjectParameter("idpe_UsuarioModifica", typeof(int));
    
            var idpe_FechaModificaParameter = idpe_FechaModifica.HasValue ?
                new ObjectParameter("idpe_FechaModifica", idpe_FechaModifica) :
                new ObjectParameter("idpe_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbIdiomasPersona_Inactivar_Result>("UDP_RRHH_tbIdiomasPersona_Inactivar", idpe_IdParameter, idpe_RazonInactivoParameter, idpe_UsuarioModificaParameter, idpe_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbIdiomasPersona_Insert_Result> UDP_RRHH_tbIdiomasPersona_Insert(Nullable<int> per_Id, Nullable<int> idi_Id, Nullable<int> idpe_UsuarioCrea, Nullable<System.DateTime> idpe_FechaCrea)
        {
            var per_IdParameter = per_Id.HasValue ?
                new ObjectParameter("per_Id", per_Id) :
                new ObjectParameter("per_Id", typeof(int));
    
            var idi_IdParameter = idi_Id.HasValue ?
                new ObjectParameter("idi_Id", idi_Id) :
                new ObjectParameter("idi_Id", typeof(int));
    
            var idpe_UsuarioCreaParameter = idpe_UsuarioCrea.HasValue ?
                new ObjectParameter("idpe_UsuarioCrea", idpe_UsuarioCrea) :
                new ObjectParameter("idpe_UsuarioCrea", typeof(int));
    
            var idpe_FechaCreaParameter = idpe_FechaCrea.HasValue ?
                new ObjectParameter("idpe_FechaCrea", idpe_FechaCrea) :
                new ObjectParameter("idpe_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbIdiomasPersona_Insert_Result>("UDP_RRHH_tbIdiomasPersona_Insert", per_IdParameter, idi_IdParameter, idpe_UsuarioCreaParameter, idpe_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbIdiomasPersona_Restore_Result> UDP_RRHH_tbIdiomasPersona_Restore(Nullable<int> idpe_Id, Nullable<int> idpe_UsuarioModifica, Nullable<System.DateTime> idpe_FechaModifica)
        {
            var idpe_IdParameter = idpe_Id.HasValue ?
                new ObjectParameter("idpe_Id", idpe_Id) :
                new ObjectParameter("idpe_Id", typeof(int));
    
            var idpe_UsuarioModificaParameter = idpe_UsuarioModifica.HasValue ?
                new ObjectParameter("idpe_UsuarioModifica", idpe_UsuarioModifica) :
                new ObjectParameter("idpe_UsuarioModifica", typeof(int));
    
            var idpe_FechaModificaParameter = idpe_FechaModifica.HasValue ?
                new ObjectParameter("idpe_FechaModifica", idpe_FechaModifica) :
                new ObjectParameter("idpe_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbIdiomasPersona_Restore_Result>("UDP_RRHH_tbIdiomasPersona_Restore", idpe_IdParameter, idpe_UsuarioModificaParameter, idpe_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbJornadas_Delete_Result> UDP_RRHH_tbJornadas_Delete(Nullable<int> jor_Id, string jor_razon_Inactivo, Nullable<int> jor_UsuarioModifica, Nullable<System.DateTime> jor_FechaModifica)
        {
            var jor_IdParameter = jor_Id.HasValue ?
                new ObjectParameter("jor_Id", jor_Id) :
                new ObjectParameter("jor_Id", typeof(int));
    
            var jor_razon_InactivoParameter = jor_razon_Inactivo != null ?
                new ObjectParameter("jor_razon_Inactivo", jor_razon_Inactivo) :
                new ObjectParameter("jor_razon_Inactivo", typeof(string));
    
            var jor_UsuarioModificaParameter = jor_UsuarioModifica.HasValue ?
                new ObjectParameter("jor_UsuarioModifica", jor_UsuarioModifica) :
                new ObjectParameter("jor_UsuarioModifica", typeof(int));
    
            var jor_FechaModificaParameter = jor_FechaModifica.HasValue ?
                new ObjectParameter("jor_FechaModifica", jor_FechaModifica) :
                new ObjectParameter("jor_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbJornadas_Delete_Result>("UDP_RRHH_tbJornadas_Delete", jor_IdParameter, jor_razon_InactivoParameter, jor_UsuarioModificaParameter, jor_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbJornadas_Insert_Result> UDP_RRHH_tbJornadas_Insert(string jor_Descripcion, Nullable<int> jor_UsuarioCrea, Nullable<System.DateTime> jor_FechaCrea)
        {
            var jor_DescripcionParameter = jor_Descripcion != null ?
                new ObjectParameter("jor_Descripcion", jor_Descripcion) :
                new ObjectParameter("jor_Descripcion", typeof(string));
    
            var jor_UsuarioCreaParameter = jor_UsuarioCrea.HasValue ?
                new ObjectParameter("jor_UsuarioCrea", jor_UsuarioCrea) :
                new ObjectParameter("jor_UsuarioCrea", typeof(int));
    
            var jor_FechaCreaParameter = jor_FechaCrea.HasValue ?
                new ObjectParameter("jor_FechaCrea", jor_FechaCrea) :
                new ObjectParameter("jor_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbJornadas_Insert_Result>("UDP_RRHH_tbJornadas_Insert", jor_DescripcionParameter, jor_UsuarioCreaParameter, jor_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbJornadas_Restore_Result> UDP_RRHH_tbJornadas_Restore(Nullable<int> jor_Id, Nullable<int> jor_UsuarioModifica, Nullable<System.DateTime> jor_FechaModifica)
        {
            var jor_IdParameter = jor_Id.HasValue ?
                new ObjectParameter("jor_Id", jor_Id) :
                new ObjectParameter("jor_Id", typeof(int));
    
            var jor_UsuarioModificaParameter = jor_UsuarioModifica.HasValue ?
                new ObjectParameter("jor_UsuarioModifica", jor_UsuarioModifica) :
                new ObjectParameter("jor_UsuarioModifica", typeof(int));
    
            var jor_FechaModificaParameter = jor_FechaModifica.HasValue ?
                new ObjectParameter("jor_FechaModifica", jor_FechaModifica) :
                new ObjectParameter("jor_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbJornadas_Restore_Result>("UDP_RRHH_tbJornadas_Restore", jor_IdParameter, jor_UsuarioModificaParameter, jor_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbJornadas_Update_Result> UDP_RRHH_tbJornadas_Update(Nullable<int> jor_Id, string jor_Descripcion, Nullable<int> jor_UsuarioModifica, Nullable<System.DateTime> jor_FechaModifica)
        {
            var jor_IdParameter = jor_Id.HasValue ?
                new ObjectParameter("jor_Id", jor_Id) :
                new ObjectParameter("jor_Id", typeof(int));
    
            var jor_DescripcionParameter = jor_Descripcion != null ?
                new ObjectParameter("jor_Descripcion", jor_Descripcion) :
                new ObjectParameter("jor_Descripcion", typeof(string));
    
            var jor_UsuarioModificaParameter = jor_UsuarioModifica.HasValue ?
                new ObjectParameter("jor_UsuarioModifica", jor_UsuarioModifica) :
                new ObjectParameter("jor_UsuarioModifica", typeof(int));
    
            var jor_FechaModificaParameter = jor_FechaModifica.HasValue ?
                new ObjectParameter("jor_FechaModifica", jor_FechaModifica) :
                new ObjectParameter("jor_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbJornadas_Update_Result>("UDP_RRHH_tbJornadas_Update", jor_IdParameter, jor_DescripcionParameter, jor_UsuarioModificaParameter, jor_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbNacionalidades_Delete_Result> UDP_RRHH_tbNacionalidades_Delete(Nullable<int> nac_id, string nac_razon_Inactivo, Nullable<int> nac_UsuarioModifica, Nullable<System.DateTime> nac_FechaModifica)
        {
            var nac_idParameter = nac_id.HasValue ?
                new ObjectParameter("nac_id", nac_id) :
                new ObjectParameter("nac_id", typeof(int));
    
            var nac_razon_InactivoParameter = nac_razon_Inactivo != null ?
                new ObjectParameter("nac_razon_Inactivo", nac_razon_Inactivo) :
                new ObjectParameter("nac_razon_Inactivo", typeof(string));
    
            var nac_UsuarioModificaParameter = nac_UsuarioModifica.HasValue ?
                new ObjectParameter("nac_UsuarioModifica", nac_UsuarioModifica) :
                new ObjectParameter("nac_UsuarioModifica", typeof(int));
    
            var nac_FechaModificaParameter = nac_FechaModifica.HasValue ?
                new ObjectParameter("nac_FechaModifica", nac_FechaModifica) :
                new ObjectParameter("nac_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbNacionalidades_Delete_Result>("UDP_RRHH_tbNacionalidades_Delete", nac_idParameter, nac_razon_InactivoParameter, nac_UsuarioModificaParameter, nac_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbNacionalidades_Insert_Result> UDP_RRHH_tbNacionalidades_Insert(string nac_Descripcion, Nullable<int> nac_UsuarioCrea, Nullable<System.DateTime> nac_FechaCrea)
        {
            var nac_DescripcionParameter = nac_Descripcion != null ?
                new ObjectParameter("nac_Descripcion", nac_Descripcion) :
                new ObjectParameter("nac_Descripcion", typeof(string));
    
            var nac_UsuarioCreaParameter = nac_UsuarioCrea.HasValue ?
                new ObjectParameter("nac_UsuarioCrea", nac_UsuarioCrea) :
                new ObjectParameter("nac_UsuarioCrea", typeof(int));
    
            var nac_FechaCreaParameter = nac_FechaCrea.HasValue ?
                new ObjectParameter("nac_FechaCrea", nac_FechaCrea) :
                new ObjectParameter("nac_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbNacionalidades_Insert_Result>("UDP_RRHH_tbNacionalidades_Insert", nac_DescripcionParameter, nac_UsuarioCreaParameter, nac_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbNacionalidades_Restore_Result> UDP_RRHH_tbNacionalidades_Restore(Nullable<int> nac_id, Nullable<int> nac_UsuarioModifica, Nullable<System.DateTime> nac_FechaModifica)
        {
            var nac_idParameter = nac_id.HasValue ?
                new ObjectParameter("nac_id", nac_id) :
                new ObjectParameter("nac_id", typeof(int));
    
            var nac_UsuarioModificaParameter = nac_UsuarioModifica.HasValue ?
                new ObjectParameter("nac_UsuarioModifica", nac_UsuarioModifica) :
                new ObjectParameter("nac_UsuarioModifica", typeof(int));
    
            var nac_FechaModificaParameter = nac_FechaModifica.HasValue ?
                new ObjectParameter("nac_FechaModifica", nac_FechaModifica) :
                new ObjectParameter("nac_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbNacionalidades_Restore_Result>("UDP_RRHH_tbNacionalidades_Restore", nac_idParameter, nac_UsuarioModificaParameter, nac_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbNacionalidades_Update_Result> UDP_RRHH_tbNacionalidades_Update(Nullable<int> nac_Id, string nac_Descripcion, Nullable<int> nac_UsuarioModifica, Nullable<System.DateTime> nac_FechaModifica)
        {
            var nac_IdParameter = nac_Id.HasValue ?
                new ObjectParameter("nac_Id", nac_Id) :
                new ObjectParameter("nac_Id", typeof(int));
    
            var nac_DescripcionParameter = nac_Descripcion != null ?
                new ObjectParameter("nac_Descripcion", nac_Descripcion) :
                new ObjectParameter("nac_Descripcion", typeof(string));
    
            var nac_UsuarioModificaParameter = nac_UsuarioModifica.HasValue ?
                new ObjectParameter("nac_UsuarioModifica", nac_UsuarioModifica) :
                new ObjectParameter("nac_UsuarioModifica", typeof(int));
    
            var nac_FechaModificaParameter = nac_FechaModifica.HasValue ?
                new ObjectParameter("nac_FechaModifica", nac_FechaModifica) :
                new ObjectParameter("nac_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbNacionalidades_Update_Result>("UDP_RRHH_tbNacionalidades_Update", nac_IdParameter, nac_DescripcionParameter, nac_UsuarioModificaParameter, nac_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbPermisos_Update_Result> UDP_RRHH_tbPermisos_Update(Nullable<int> tper_Id, string tper_Descripcion, Nullable<int> tper_UsuarioModifica, Nullable<System.DateTime> tper_FechaModifica)
        {
            var tper_IdParameter = tper_Id.HasValue ?
                new ObjectParameter("tper_Id", tper_Id) :
                new ObjectParameter("tper_Id", typeof(int));
    
            var tper_DescripcionParameter = tper_Descripcion != null ?
                new ObjectParameter("tper_Descripcion", tper_Descripcion) :
                new ObjectParameter("tper_Descripcion", typeof(string));
    
            var tper_UsuarioModificaParameter = tper_UsuarioModifica.HasValue ?
                new ObjectParameter("tper_UsuarioModifica", tper_UsuarioModifica) :
                new ObjectParameter("tper_UsuarioModifica", typeof(int));
    
            var tper_FechaModificaParameter = tper_FechaModifica.HasValue ?
                new ObjectParameter("tper_FechaModifica", tper_FechaModifica) :
                new ObjectParameter("tper_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbPermisos_Update_Result>("UDP_RRHH_tbPermisos_Update", tper_IdParameter, tper_DescripcionParameter, tper_UsuarioModificaParameter, tper_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbPersonas_Habilitar_Result> UDP_RRHH_tbPersonas_Habilitar(Nullable<int> per_Id, Nullable<int> per_UsuarioModifica, Nullable<System.DateTime> per_FechaModifica)
        {
            var per_IdParameter = per_Id.HasValue ?
                new ObjectParameter("per_Id", per_Id) :
                new ObjectParameter("per_Id", typeof(int));
    
            var per_UsuarioModificaParameter = per_UsuarioModifica.HasValue ?
                new ObjectParameter("per_UsuarioModifica", per_UsuarioModifica) :
                new ObjectParameter("per_UsuarioModifica", typeof(int));
    
            var per_FechaModificaParameter = per_FechaModifica.HasValue ?
                new ObjectParameter("per_FechaModifica", per_FechaModifica) :
                new ObjectParameter("per_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbPersonas_Habilitar_Result>("UDP_RRHH_tbPersonas_Habilitar", per_IdParameter, per_UsuarioModificaParameter, per_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbPersonas_Inactivar_Result> UDP_RRHH_tbPersonas_Inactivar(Nullable<int> per_Id, string per_RazonInactivo, Nullable<int> per_UsuarioModifica, Nullable<System.DateTime> per_FechaModifica)
        {
            var per_IdParameter = per_Id.HasValue ?
                new ObjectParameter("per_Id", per_Id) :
                new ObjectParameter("per_Id", typeof(int));
    
            var per_RazonInactivoParameter = per_RazonInactivo != null ?
                new ObjectParameter("per_RazonInactivo", per_RazonInactivo) :
                new ObjectParameter("per_RazonInactivo", typeof(string));
    
            var per_UsuarioModificaParameter = per_UsuarioModifica.HasValue ?
                new ObjectParameter("per_UsuarioModifica", per_UsuarioModifica) :
                new ObjectParameter("per_UsuarioModifica", typeof(int));
    
            var per_FechaModificaParameter = per_FechaModifica.HasValue ?
                new ObjectParameter("per_FechaModifica", per_FechaModifica) :
                new ObjectParameter("per_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbPersonas_Inactivar_Result>("UDP_RRHH_tbPersonas_Inactivar", per_IdParameter, per_RazonInactivoParameter, per_UsuarioModificaParameter, per_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbPersonas_Insert_Result> UDP_RRHH_tbPersonas_Insert(string per_Identidad, string per_Nombres, string per_Apellidos, Nullable<System.DateTime> per_FechaNacimiento, string per_Sexo, Nullable<int> nac_Id, string per_Direccion, string per_Telefono, string per_CorreoElectronico, string per_EstadoCivil, string per_TipoSangre, Nullable<int> per_UsuarioCrea, Nullable<System.DateTime> per_FechaCrea)
        {
            var per_IdentidadParameter = per_Identidad != null ?
                new ObjectParameter("per_Identidad", per_Identidad) :
                new ObjectParameter("per_Identidad", typeof(string));
    
            var per_NombresParameter = per_Nombres != null ?
                new ObjectParameter("per_Nombres", per_Nombres) :
                new ObjectParameter("per_Nombres", typeof(string));
    
            var per_ApellidosParameter = per_Apellidos != null ?
                new ObjectParameter("per_Apellidos", per_Apellidos) :
                new ObjectParameter("per_Apellidos", typeof(string));
    
            var per_FechaNacimientoParameter = per_FechaNacimiento.HasValue ?
                new ObjectParameter("per_FechaNacimiento", per_FechaNacimiento) :
                new ObjectParameter("per_FechaNacimiento", typeof(System.DateTime));
    
            var per_SexoParameter = per_Sexo != null ?
                new ObjectParameter("per_Sexo", per_Sexo) :
                new ObjectParameter("per_Sexo", typeof(string));
    
            var nac_IdParameter = nac_Id.HasValue ?
                new ObjectParameter("nac_Id", nac_Id) :
                new ObjectParameter("nac_Id", typeof(int));
    
            var per_DireccionParameter = per_Direccion != null ?
                new ObjectParameter("per_Direccion", per_Direccion) :
                new ObjectParameter("per_Direccion", typeof(string));
    
            var per_TelefonoParameter = per_Telefono != null ?
                new ObjectParameter("per_Telefono", per_Telefono) :
                new ObjectParameter("per_Telefono", typeof(string));
    
            var per_CorreoElectronicoParameter = per_CorreoElectronico != null ?
                new ObjectParameter("per_CorreoElectronico", per_CorreoElectronico) :
                new ObjectParameter("per_CorreoElectronico", typeof(string));
    
            var per_EstadoCivilParameter = per_EstadoCivil != null ?
                new ObjectParameter("per_EstadoCivil", per_EstadoCivil) :
                new ObjectParameter("per_EstadoCivil", typeof(string));
    
            var per_TipoSangreParameter = per_TipoSangre != null ?
                new ObjectParameter("per_TipoSangre", per_TipoSangre) :
                new ObjectParameter("per_TipoSangre", typeof(string));
    
            var per_UsuarioCreaParameter = per_UsuarioCrea.HasValue ?
                new ObjectParameter("per_UsuarioCrea", per_UsuarioCrea) :
                new ObjectParameter("per_UsuarioCrea", typeof(int));
    
            var per_FechaCreaParameter = per_FechaCrea.HasValue ?
                new ObjectParameter("per_FechaCrea", per_FechaCrea) :
                new ObjectParameter("per_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbPersonas_Insert_Result>("UDP_RRHH_tbPersonas_Insert", per_IdentidadParameter, per_NombresParameter, per_ApellidosParameter, per_FechaNacimientoParameter, per_SexoParameter, nac_IdParameter, per_DireccionParameter, per_TelefonoParameter, per_CorreoElectronicoParameter, per_EstadoCivilParameter, per_TipoSangreParameter, per_UsuarioCreaParameter, per_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbPersonas_Update_Result> UDP_RRHH_tbPersonas_Update(Nullable<int> per_Id, string per_Identidad, string per_Nombres, string per_Apellidos, Nullable<System.DateTime> per_FechaNacimiento, string per_Sexo, Nullable<int> nac_Id, string per_Direccion, string per_Telefono, string per_CorreoElectronico, string per_EstadoCivil, string per_TipoSangre, Nullable<int> per_UsuarioModifica, Nullable<System.DateTime> per_FechaModifica)
        {
            var per_IdParameter = per_Id.HasValue ?
                new ObjectParameter("per_Id", per_Id) :
                new ObjectParameter("per_Id", typeof(int));
    
            var per_IdentidadParameter = per_Identidad != null ?
                new ObjectParameter("per_Identidad", per_Identidad) :
                new ObjectParameter("per_Identidad", typeof(string));
    
            var per_NombresParameter = per_Nombres != null ?
                new ObjectParameter("per_Nombres", per_Nombres) :
                new ObjectParameter("per_Nombres", typeof(string));
    
            var per_ApellidosParameter = per_Apellidos != null ?
                new ObjectParameter("per_Apellidos", per_Apellidos) :
                new ObjectParameter("per_Apellidos", typeof(string));
    
            var per_FechaNacimientoParameter = per_FechaNacimiento.HasValue ?
                new ObjectParameter("per_FechaNacimiento", per_FechaNacimiento) :
                new ObjectParameter("per_FechaNacimiento", typeof(System.DateTime));
    
            var per_SexoParameter = per_Sexo != null ?
                new ObjectParameter("per_Sexo", per_Sexo) :
                new ObjectParameter("per_Sexo", typeof(string));
    
            var nac_IdParameter = nac_Id.HasValue ?
                new ObjectParameter("nac_Id", nac_Id) :
                new ObjectParameter("nac_Id", typeof(int));
    
            var per_DireccionParameter = per_Direccion != null ?
                new ObjectParameter("per_Direccion", per_Direccion) :
                new ObjectParameter("per_Direccion", typeof(string));
    
            var per_TelefonoParameter = per_Telefono != null ?
                new ObjectParameter("per_Telefono", per_Telefono) :
                new ObjectParameter("per_Telefono", typeof(string));
    
            var per_CorreoElectronicoParameter = per_CorreoElectronico != null ?
                new ObjectParameter("per_CorreoElectronico", per_CorreoElectronico) :
                new ObjectParameter("per_CorreoElectronico", typeof(string));
    
            var per_EstadoCivilParameter = per_EstadoCivil != null ?
                new ObjectParameter("per_EstadoCivil", per_EstadoCivil) :
                new ObjectParameter("per_EstadoCivil", typeof(string));
    
            var per_TipoSangreParameter = per_TipoSangre != null ?
                new ObjectParameter("per_TipoSangre", per_TipoSangre) :
                new ObjectParameter("per_TipoSangre", typeof(string));
    
            var per_UsuarioModificaParameter = per_UsuarioModifica.HasValue ?
                new ObjectParameter("per_UsuarioModifica", per_UsuarioModifica) :
                new ObjectParameter("per_UsuarioModifica", typeof(int));
    
            var per_FechaModificaParameter = per_FechaModifica.HasValue ?
                new ObjectParameter("per_FechaModifica", per_FechaModifica) :
                new ObjectParameter("per_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbPersonas_Update_Result>("UDP_RRHH_tbPersonas_Update", per_IdParameter, per_IdentidadParameter, per_NombresParameter, per_ApellidosParameter, per_FechaNacimientoParameter, per_SexoParameter, nac_IdParameter, per_DireccionParameter, per_TelefonoParameter, per_CorreoElectronicoParameter, per_EstadoCivilParameter, per_TipoSangreParameter, per_UsuarioModificaParameter, per_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbRazonSalida_Update_Result> UDP_RRHH_tbRazonSalida_Update(Nullable<int> rsal_Id, string rsal_Descripcion, Nullable<int> rsal_UsuarioModifica, Nullable<System.DateTime> rsal_FechaModifica)
        {
            var rsal_IdParameter = rsal_Id.HasValue ?
                new ObjectParameter("rsal_Id", rsal_Id) :
                new ObjectParameter("rsal_Id", typeof(int));
    
            var rsal_DescripcionParameter = rsal_Descripcion != null ?
                new ObjectParameter("rsal_Descripcion", rsal_Descripcion) :
                new ObjectParameter("rsal_Descripcion", typeof(string));
    
            var rsal_UsuarioModificaParameter = rsal_UsuarioModifica.HasValue ?
                new ObjectParameter("rsal_UsuarioModifica", rsal_UsuarioModifica) :
                new ObjectParameter("rsal_UsuarioModifica", typeof(int));
    
            var rsal_FechaModificaParameter = rsal_FechaModifica.HasValue ?
                new ObjectParameter("rsal_FechaModifica", rsal_FechaModifica) :
                new ObjectParameter("rsal_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbRazonSalida_Update_Result>("UDP_RRHH_tbRazonSalida_Update", rsal_IdParameter, rsal_DescripcionParameter, rsal_UsuarioModificaParameter, rsal_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbRazonSalidas_Delete_Result> UDP_RRHH_tbRazonSalidas_Delete(Nullable<int> rsal_Id, string rsal_razon_Inactivo, Nullable<int> rsal_UsuarioModifica, Nullable<System.DateTime> rsal_FechaModifica)
        {
            var rsal_IdParameter = rsal_Id.HasValue ?
                new ObjectParameter("rsal_Id", rsal_Id) :
                new ObjectParameter("rsal_Id", typeof(int));
    
            var rsal_razon_InactivoParameter = rsal_razon_Inactivo != null ?
                new ObjectParameter("rsal_razon_Inactivo", rsal_razon_Inactivo) :
                new ObjectParameter("rsal_razon_Inactivo", typeof(string));
    
            var rsal_UsuarioModificaParameter = rsal_UsuarioModifica.HasValue ?
                new ObjectParameter("rsal_UsuarioModifica", rsal_UsuarioModifica) :
                new ObjectParameter("rsal_UsuarioModifica", typeof(int));
    
            var rsal_FechaModificaParameter = rsal_FechaModifica.HasValue ?
                new ObjectParameter("rsal_FechaModifica", rsal_FechaModifica) :
                new ObjectParameter("rsal_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbRazonSalidas_Delete_Result>("UDP_RRHH_tbRazonSalidas_Delete", rsal_IdParameter, rsal_razon_InactivoParameter, rsal_UsuarioModificaParameter, rsal_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbRazonSalidas_Insert_Result> UDP_RRHH_tbRazonSalidas_Insert(string rsal_Descripcion, Nullable<int> rsal_Usuariocrea, Nullable<System.DateTime> rsal_FechaCrea)
        {
            var rsal_DescripcionParameter = rsal_Descripcion != null ?
                new ObjectParameter("rsal_Descripcion", rsal_Descripcion) :
                new ObjectParameter("rsal_Descripcion", typeof(string));
    
            var rsal_UsuariocreaParameter = rsal_Usuariocrea.HasValue ?
                new ObjectParameter("rsal_Usuariocrea", rsal_Usuariocrea) :
                new ObjectParameter("rsal_Usuariocrea", typeof(int));
    
            var rsal_FechaCreaParameter = rsal_FechaCrea.HasValue ?
                new ObjectParameter("rsal_FechaCrea", rsal_FechaCrea) :
                new ObjectParameter("rsal_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbRazonSalidas_Insert_Result>("UDP_RRHH_tbRazonSalidas_Insert", rsal_DescripcionParameter, rsal_UsuariocreaParameter, rsal_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbRazonSalidas_Restore_Result> UDP_RRHH_tbRazonSalidas_Restore(Nullable<int> rsal_Id, Nullable<int> rsal_UsuarioModifica, Nullable<System.DateTime> rsal_FechaModifica)
        {
            var rsal_IdParameter = rsal_Id.HasValue ?
                new ObjectParameter("rsal_Id", rsal_Id) :
                new ObjectParameter("rsal_Id", typeof(int));
    
            var rsal_UsuarioModificaParameter = rsal_UsuarioModifica.HasValue ?
                new ObjectParameter("rsal_UsuarioModifica", rsal_UsuarioModifica) :
                new ObjectParameter("rsal_UsuarioModifica", typeof(int));
    
            var rsal_FechaModificaParameter = rsal_FechaModifica.HasValue ?
                new ObjectParameter("rsal_FechaModifica", rsal_FechaModifica) :
                new ObjectParameter("rsal_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbRazonSalidas_Restore_Result>("UDP_RRHH_tbRazonSalidas_Restore", rsal_IdParameter, rsal_UsuarioModificaParameter, rsal_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbRequerimientosEspeciales_Delete_Result> UDP_RRHH_tbRequerimientosEspeciales_Delete(Nullable<int> resp_id, string resp_razon_Inactivo, Nullable<int> resp_UsuarioModifica, Nullable<System.DateTime> resp_FechaModifica)
        {
            var resp_idParameter = resp_id.HasValue ?
                new ObjectParameter("resp_id", resp_id) :
                new ObjectParameter("resp_id", typeof(int));
    
            var resp_razon_InactivoParameter = resp_razon_Inactivo != null ?
                new ObjectParameter("resp_razon_Inactivo", resp_razon_Inactivo) :
                new ObjectParameter("resp_razon_Inactivo", typeof(string));
    
            var resp_UsuarioModificaParameter = resp_UsuarioModifica.HasValue ?
                new ObjectParameter("resp_UsuarioModifica", resp_UsuarioModifica) :
                new ObjectParameter("resp_UsuarioModifica", typeof(int));
    
            var resp_FechaModificaParameter = resp_FechaModifica.HasValue ?
                new ObjectParameter("resp_FechaModifica", resp_FechaModifica) :
                new ObjectParameter("resp_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbRequerimientosEspeciales_Delete_Result>("UDP_RRHH_tbRequerimientosEspeciales_Delete", resp_idParameter, resp_razon_InactivoParameter, resp_UsuarioModificaParameter, resp_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbRequerimientosEspeciales_Insert_Result> UDP_RRHH_tbRequerimientosEspeciales_Insert(string resp_Descripcion, Nullable<int> resp_UsuarioCrea, Nullable<System.DateTime> resp_FechaCrea)
        {
            var resp_DescripcionParameter = resp_Descripcion != null ?
                new ObjectParameter("resp_Descripcion", resp_Descripcion) :
                new ObjectParameter("resp_Descripcion", typeof(string));
    
            var resp_UsuarioCreaParameter = resp_UsuarioCrea.HasValue ?
                new ObjectParameter("resp_UsuarioCrea", resp_UsuarioCrea) :
                new ObjectParameter("resp_UsuarioCrea", typeof(int));
    
            var resp_FechaCreaParameter = resp_FechaCrea.HasValue ?
                new ObjectParameter("resp_FechaCrea", resp_FechaCrea) :
                new ObjectParameter("resp_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbRequerimientosEspeciales_Insert_Result>("UDP_RRHH_tbRequerimientosEspeciales_Insert", resp_DescripcionParameter, resp_UsuarioCreaParameter, resp_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbRequerimientosEspeciales_Restore_Result> UDP_RRHH_tbRequerimientosEspeciales_Restore(Nullable<int> resp_id, Nullable<int> resp_UsuarioModifica, Nullable<System.DateTime> resp_FechaModifica)
        {
            var resp_idParameter = resp_id.HasValue ?
                new ObjectParameter("resp_id", resp_id) :
                new ObjectParameter("resp_id", typeof(int));
    
            var resp_UsuarioModificaParameter = resp_UsuarioModifica.HasValue ?
                new ObjectParameter("resp_UsuarioModifica", resp_UsuarioModifica) :
                new ObjectParameter("resp_UsuarioModifica", typeof(int));
    
            var resp_FechaModificaParameter = resp_FechaModifica.HasValue ?
                new ObjectParameter("resp_FechaModifica", resp_FechaModifica) :
                new ObjectParameter("resp_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbRequerimientosEspeciales_Restore_Result>("UDP_RRHH_tbRequerimientosEspeciales_Restore", resp_idParameter, resp_UsuarioModificaParameter, resp_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbRequerimientosEspeciales_Update_Result> UDP_RRHH_tbRequerimientosEspeciales_Update(Nullable<int> resp_Id, string resp_Descripcion, Nullable<int> resp_UsuarioModifica, Nullable<System.DateTime> resp_FechaModifica)
        {
            var resp_IdParameter = resp_Id.HasValue ?
                new ObjectParameter("resp_Id", resp_Id) :
                new ObjectParameter("resp_Id", typeof(int));
    
            var resp_DescripcionParameter = resp_Descripcion != null ?
                new ObjectParameter("resp_Descripcion", resp_Descripcion) :
                new ObjectParameter("resp_Descripcion", typeof(string));
    
            var resp_UsuarioModificaParameter = resp_UsuarioModifica.HasValue ?
                new ObjectParameter("resp_UsuarioModifica", resp_UsuarioModifica) :
                new ObjectParameter("resp_UsuarioModifica", typeof(int));
    
            var resp_FechaModificaParameter = resp_FechaModifica.HasValue ?
                new ObjectParameter("resp_FechaModifica", resp_FechaModifica) :
                new ObjectParameter("resp_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbRequerimientosEspeciales_Update_Result>("UDP_RRHH_tbRequerimientosEspeciales_Update", resp_IdParameter, resp_DescripcionParameter, resp_UsuarioModificaParameter, resp_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbRequerimientosEspecialesPersona_Inactivar_Result> UDP_RRHH_tbRequerimientosEspecialesPersona_Inactivar(Nullable<int> rep_Id, string rep_RazonInactivo, Nullable<int> rep_UsuarioModifica, Nullable<System.DateTime> rep_FechaModifica)
        {
            var rep_IdParameter = rep_Id.HasValue ?
                new ObjectParameter("rep_Id", rep_Id) :
                new ObjectParameter("rep_Id", typeof(int));
    
            var rep_RazonInactivoParameter = rep_RazonInactivo != null ?
                new ObjectParameter("rep_RazonInactivo", rep_RazonInactivo) :
                new ObjectParameter("rep_RazonInactivo", typeof(string));
    
            var rep_UsuarioModificaParameter = rep_UsuarioModifica.HasValue ?
                new ObjectParameter("rep_UsuarioModifica", rep_UsuarioModifica) :
                new ObjectParameter("rep_UsuarioModifica", typeof(int));
    
            var rep_FechaModificaParameter = rep_FechaModifica.HasValue ?
                new ObjectParameter("rep_FechaModifica", rep_FechaModifica) :
                new ObjectParameter("rep_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbRequerimientosEspecialesPersona_Inactivar_Result>("UDP_RRHH_tbRequerimientosEspecialesPersona_Inactivar", rep_IdParameter, rep_RazonInactivoParameter, rep_UsuarioModificaParameter, rep_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbRequerimientosEspecialesPersona_Insert_Result> UDP_RRHH_tbRequerimientosEspecialesPersona_Insert(Nullable<int> per_Id, Nullable<int> resp_Id, Nullable<int> rep_UsuarioCrea, Nullable<System.DateTime> rep_FechaCrea)
        {
            var per_IdParameter = per_Id.HasValue ?
                new ObjectParameter("per_Id", per_Id) :
                new ObjectParameter("per_Id", typeof(int));
    
            var resp_IdParameter = resp_Id.HasValue ?
                new ObjectParameter("resp_Id", resp_Id) :
                new ObjectParameter("resp_Id", typeof(int));
    
            var rep_UsuarioCreaParameter = rep_UsuarioCrea.HasValue ?
                new ObjectParameter("rep_UsuarioCrea", rep_UsuarioCrea) :
                new ObjectParameter("rep_UsuarioCrea", typeof(int));
    
            var rep_FechaCreaParameter = rep_FechaCrea.HasValue ?
                new ObjectParameter("rep_FechaCrea", rep_FechaCrea) :
                new ObjectParameter("rep_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbRequerimientosEspecialesPersona_Insert_Result>("UDP_RRHH_tbRequerimientosEspecialesPersona_Insert", per_IdParameter, resp_IdParameter, rep_UsuarioCreaParameter, rep_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbRequerimientosEspecialesPersona_Restore_Result> UDP_RRHH_tbRequerimientosEspecialesPersona_Restore(Nullable<int> rep_Id, Nullable<int> rep_UsuarioModifica, Nullable<System.DateTime> rep_FechaModifica)
        {
            var rep_IdParameter = rep_Id.HasValue ?
                new ObjectParameter("rep_Id", rep_Id) :
                new ObjectParameter("rep_Id", typeof(int));
    
            var rep_UsuarioModificaParameter = rep_UsuarioModifica.HasValue ?
                new ObjectParameter("rep_UsuarioModifica", rep_UsuarioModifica) :
                new ObjectParameter("rep_UsuarioModifica", typeof(int));
    
            var rep_FechaModificaParameter = rep_FechaModifica.HasValue ?
                new ObjectParameter("rep_FechaModifica", rep_FechaModifica) :
                new ObjectParameter("rep_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbRequerimientosEspecialesPersona_Restore_Result>("UDP_RRHH_tbRequerimientosEspecialesPersona_Restore", rep_IdParameter, rep_UsuarioModificaParameter, rep_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbRequisiciones_Delete_Result> UDP_RRHH_tbRequisiciones_Delete(Nullable<int> req_id, string req_razonInactivo, Nullable<int> req_UsuarioModifica, Nullable<System.DateTime> req_FechaModifica)
        {
            var req_idParameter = req_id.HasValue ?
                new ObjectParameter("req_id", req_id) :
                new ObjectParameter("req_id", typeof(int));
    
            var req_razonInactivoParameter = req_razonInactivo != null ?
                new ObjectParameter("req_razonInactivo", req_razonInactivo) :
                new ObjectParameter("req_razonInactivo", typeof(string));
    
            var req_UsuarioModificaParameter = req_UsuarioModifica.HasValue ?
                new ObjectParameter("req_UsuarioModifica", req_UsuarioModifica) :
                new ObjectParameter("req_UsuarioModifica", typeof(int));
    
            var req_FechaModificaParameter = req_FechaModifica.HasValue ?
                new ObjectParameter("req_FechaModifica", req_FechaModifica) :
                new ObjectParameter("req_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbRequisiciones_Delete_Result>("UDP_RRHH_tbRequisiciones_Delete", req_idParameter, req_razonInactivoParameter, req_UsuarioModificaParameter, req_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbRequisiciones_Insert_Result> UDP_RRHH_tbRequisiciones_Insert(string req_Experiencia, string req_Sexo, string req_Descripcion, Nullable<int> req_EdadMinima, Nullable<int> req_EdadMaxima, string req_EstadoCivil, Nullable<bool> req_EducacionSuperior, Nullable<bool> req_Permanente, string req_Duracion, string req_Vacantes, string req_NivelEducativo, Nullable<System.DateTime> req_FechaRequisicion, Nullable<System.DateTime> req_FechaContratacion, Nullable<int> req_UsuarioCrea, Nullable<System.DateTime> req_FechaCrea)
        {
            var req_ExperienciaParameter = req_Experiencia != null ?
                new ObjectParameter("req_Experiencia", req_Experiencia) :
                new ObjectParameter("req_Experiencia", typeof(string));
    
            var req_SexoParameter = req_Sexo != null ?
                new ObjectParameter("req_Sexo", req_Sexo) :
                new ObjectParameter("req_Sexo", typeof(string));
    
            var req_DescripcionParameter = req_Descripcion != null ?
                new ObjectParameter("req_Descripcion", req_Descripcion) :
                new ObjectParameter("req_Descripcion", typeof(string));
    
            var req_EdadMinimaParameter = req_EdadMinima.HasValue ?
                new ObjectParameter("req_EdadMinima", req_EdadMinima) :
                new ObjectParameter("req_EdadMinima", typeof(int));
    
            var req_EdadMaximaParameter = req_EdadMaxima.HasValue ?
                new ObjectParameter("req_EdadMaxima", req_EdadMaxima) :
                new ObjectParameter("req_EdadMaxima", typeof(int));
    
            var req_EstadoCivilParameter = req_EstadoCivil != null ?
                new ObjectParameter("req_EstadoCivil", req_EstadoCivil) :
                new ObjectParameter("req_EstadoCivil", typeof(string));
    
            var req_EducacionSuperiorParameter = req_EducacionSuperior.HasValue ?
                new ObjectParameter("req_EducacionSuperior", req_EducacionSuperior) :
                new ObjectParameter("req_EducacionSuperior", typeof(bool));
    
            var req_PermanenteParameter = req_Permanente.HasValue ?
                new ObjectParameter("req_Permanente", req_Permanente) :
                new ObjectParameter("req_Permanente", typeof(bool));
    
            var req_DuracionParameter = req_Duracion != null ?
                new ObjectParameter("req_Duracion", req_Duracion) :
                new ObjectParameter("req_Duracion", typeof(string));
    
            var req_VacantesParameter = req_Vacantes != null ?
                new ObjectParameter("req_Vacantes", req_Vacantes) :
                new ObjectParameter("req_Vacantes", typeof(string));
    
            var req_NivelEducativoParameter = req_NivelEducativo != null ?
                new ObjectParameter("req_NivelEducativo", req_NivelEducativo) :
                new ObjectParameter("req_NivelEducativo", typeof(string));
    
            var req_FechaRequisicionParameter = req_FechaRequisicion.HasValue ?
                new ObjectParameter("req_FechaRequisicion", req_FechaRequisicion) :
                new ObjectParameter("req_FechaRequisicion", typeof(System.DateTime));
    
            var req_FechaContratacionParameter = req_FechaContratacion.HasValue ?
                new ObjectParameter("req_FechaContratacion", req_FechaContratacion) :
                new ObjectParameter("req_FechaContratacion", typeof(System.DateTime));
    
            var req_UsuarioCreaParameter = req_UsuarioCrea.HasValue ?
                new ObjectParameter("req_UsuarioCrea", req_UsuarioCrea) :
                new ObjectParameter("req_UsuarioCrea", typeof(int));
    
            var req_FechaCreaParameter = req_FechaCrea.HasValue ?
                new ObjectParameter("req_FechaCrea", req_FechaCrea) :
                new ObjectParameter("req_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbRequisiciones_Insert_Result>("UDP_RRHH_tbRequisiciones_Insert", req_ExperienciaParameter, req_SexoParameter, req_DescripcionParameter, req_EdadMinimaParameter, req_EdadMaximaParameter, req_EstadoCivilParameter, req_EducacionSuperiorParameter, req_PermanenteParameter, req_DuracionParameter, req_VacantesParameter, req_NivelEducativoParameter, req_FechaRequisicionParameter, req_FechaContratacionParameter, req_UsuarioCreaParameter, req_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbRequisiciones_Restore_Result> UDP_RRHH_tbRequisiciones_Restore(Nullable<int> req_id, Nullable<int> req_UsuarioModifica, Nullable<System.DateTime> req_FechaModifica)
        {
            var req_idParameter = req_id.HasValue ?
                new ObjectParameter("req_id", req_id) :
                new ObjectParameter("req_id", typeof(int));
    
            var req_UsuarioModificaParameter = req_UsuarioModifica.HasValue ?
                new ObjectParameter("req_UsuarioModifica", req_UsuarioModifica) :
                new ObjectParameter("req_UsuarioModifica", typeof(int));
    
            var req_FechaModificaParameter = req_FechaModifica.HasValue ?
                new ObjectParameter("req_FechaModifica", req_FechaModifica) :
                new ObjectParameter("req_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbRequisiciones_Restore_Result>("UDP_RRHH_tbRequisiciones_Restore", req_idParameter, req_UsuarioModificaParameter, req_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbRequisiciones_Update_Result> UDP_RRHH_tbRequisiciones_Update(Nullable<int> req_Id, string req_Experiencia, string req_Sexo, string req_Descripcion, Nullable<int> req_EdadMinima, Nullable<int> req_EdadMaxima, string req_EstadoCivil, Nullable<bool> req_EducacionSuperior, Nullable<bool> req_Permanente, string req_Duracion, string req_Vacantes, string req_NivelEducativo, Nullable<System.DateTime> req_FechaRequisicion, Nullable<System.DateTime> req_FechaContratacion, Nullable<int> req_UsuarioModifica, Nullable<System.DateTime> req_FechaModifica)
        {
            var req_IdParameter = req_Id.HasValue ?
                new ObjectParameter("req_Id", req_Id) :
                new ObjectParameter("req_Id", typeof(int));
    
            var req_ExperienciaParameter = req_Experiencia != null ?
                new ObjectParameter("req_Experiencia", req_Experiencia) :
                new ObjectParameter("req_Experiencia", typeof(string));
    
            var req_SexoParameter = req_Sexo != null ?
                new ObjectParameter("req_Sexo", req_Sexo) :
                new ObjectParameter("req_Sexo", typeof(string));
    
            var req_DescripcionParameter = req_Descripcion != null ?
                new ObjectParameter("req_Descripcion", req_Descripcion) :
                new ObjectParameter("req_Descripcion", typeof(string));
    
            var req_EdadMinimaParameter = req_EdadMinima.HasValue ?
                new ObjectParameter("req_EdadMinima", req_EdadMinima) :
                new ObjectParameter("req_EdadMinima", typeof(int));
    
            var req_EdadMaximaParameter = req_EdadMaxima.HasValue ?
                new ObjectParameter("req_EdadMaxima", req_EdadMaxima) :
                new ObjectParameter("req_EdadMaxima", typeof(int));
    
            var req_EstadoCivilParameter = req_EstadoCivil != null ?
                new ObjectParameter("req_EstadoCivil", req_EstadoCivil) :
                new ObjectParameter("req_EstadoCivil", typeof(string));
    
            var req_EducacionSuperiorParameter = req_EducacionSuperior.HasValue ?
                new ObjectParameter("req_EducacionSuperior", req_EducacionSuperior) :
                new ObjectParameter("req_EducacionSuperior", typeof(bool));
    
            var req_PermanenteParameter = req_Permanente.HasValue ?
                new ObjectParameter("req_Permanente", req_Permanente) :
                new ObjectParameter("req_Permanente", typeof(bool));
    
            var req_DuracionParameter = req_Duracion != null ?
                new ObjectParameter("req_Duracion", req_Duracion) :
                new ObjectParameter("req_Duracion", typeof(string));
    
            var req_VacantesParameter = req_Vacantes != null ?
                new ObjectParameter("req_Vacantes", req_Vacantes) :
                new ObjectParameter("req_Vacantes", typeof(string));
    
            var req_NivelEducativoParameter = req_NivelEducativo != null ?
                new ObjectParameter("req_NivelEducativo", req_NivelEducativo) :
                new ObjectParameter("req_NivelEducativo", typeof(string));
    
            var req_FechaRequisicionParameter = req_FechaRequisicion.HasValue ?
                new ObjectParameter("req_FechaRequisicion", req_FechaRequisicion) :
                new ObjectParameter("req_FechaRequisicion", typeof(System.DateTime));
    
            var req_FechaContratacionParameter = req_FechaContratacion.HasValue ?
                new ObjectParameter("req_FechaContratacion", req_FechaContratacion) :
                new ObjectParameter("req_FechaContratacion", typeof(System.DateTime));
    
            var req_UsuarioModificaParameter = req_UsuarioModifica.HasValue ?
                new ObjectParameter("req_UsuarioModifica", req_UsuarioModifica) :
                new ObjectParameter("req_UsuarioModifica", typeof(int));
    
            var req_FechaModificaParameter = req_FechaModifica.HasValue ?
                new ObjectParameter("req_FechaModifica", req_FechaModifica) :
                new ObjectParameter("req_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbRequisiciones_Update_Result>("UDP_RRHH_tbRequisiciones_Update", req_IdParameter, req_ExperienciaParameter, req_SexoParameter, req_DescripcionParameter, req_EdadMinimaParameter, req_EdadMaximaParameter, req_EstadoCivilParameter, req_EducacionSuperiorParameter, req_PermanenteParameter, req_DuracionParameter, req_VacantesParameter, req_NivelEducativoParameter, req_FechaRequisicionParameter, req_FechaContratacionParameter, req_UsuarioModificaParameter, req_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbSeleccionCandidatos_Delete_Result> UDP_RRHH_tbSeleccionCandidatos_Delete(Nullable<int> scan_Id, string scan_RazonInactivo, Nullable<int> scan_UsuarioModifica, Nullable<System.DateTime> scan_FechaModifica)
        {
            var scan_IdParameter = scan_Id.HasValue ?
                new ObjectParameter("scan_Id", scan_Id) :
                new ObjectParameter("scan_Id", typeof(int));
    
            var scan_RazonInactivoParameter = scan_RazonInactivo != null ?
                new ObjectParameter("scan_RazonInactivo", scan_RazonInactivo) :
                new ObjectParameter("scan_RazonInactivo", typeof(string));
    
            var scan_UsuarioModificaParameter = scan_UsuarioModifica.HasValue ?
                new ObjectParameter("scan_UsuarioModifica", scan_UsuarioModifica) :
                new ObjectParameter("scan_UsuarioModifica", typeof(int));
    
            var scan_FechaModificaParameter = scan_FechaModifica.HasValue ?
                new ObjectParameter("scan_FechaModifica", scan_FechaModifica) :
                new ObjectParameter("scan_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbSeleccionCandidatos_Delete_Result>("UDP_RRHH_tbSeleccionCandidatos_Delete", scan_IdParameter, scan_RazonInactivoParameter, scan_UsuarioModificaParameter, scan_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbSeleccionCandidatos_Insert_Result> UDP_RRHH_tbSeleccionCandidatos_Insert(Nullable<int> per_Id, Nullable<int> fare_Id, Nullable<System.DateTime> scan_Fecha, Nullable<int> req_Id, Nullable<int> scan_UsuarioCrea, Nullable<System.DateTime> scan_FechaCrea)
        {
            var per_IdParameter = per_Id.HasValue ?
                new ObjectParameter("per_Id", per_Id) :
                new ObjectParameter("per_Id", typeof(int));
    
            var fare_IdParameter = fare_Id.HasValue ?
                new ObjectParameter("fare_Id", fare_Id) :
                new ObjectParameter("fare_Id", typeof(int));
    
            var scan_FechaParameter = scan_Fecha.HasValue ?
                new ObjectParameter("scan_Fecha", scan_Fecha) :
                new ObjectParameter("scan_Fecha", typeof(System.DateTime));
    
            var req_IdParameter = req_Id.HasValue ?
                new ObjectParameter("req_Id", req_Id) :
                new ObjectParameter("req_Id", typeof(int));
    
            var scan_UsuarioCreaParameter = scan_UsuarioCrea.HasValue ?
                new ObjectParameter("scan_UsuarioCrea", scan_UsuarioCrea) :
                new ObjectParameter("scan_UsuarioCrea", typeof(int));
    
            var scan_FechaCreaParameter = scan_FechaCrea.HasValue ?
                new ObjectParameter("scan_FechaCrea", scan_FechaCrea) :
                new ObjectParameter("scan_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbSeleccionCandidatos_Insert_Result>("UDP_RRHH_tbSeleccionCandidatos_Insert", per_IdParameter, fare_IdParameter, scan_FechaParameter, req_IdParameter, scan_UsuarioCreaParameter, scan_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbSeleccionCandidatos_Restore_Result> UDP_RRHH_tbSeleccionCandidatos_Restore(Nullable<int> scan_Id, Nullable<int> scan_UsuarioModifica, Nullable<System.DateTime> scan_FechaModifica)
        {
            var scan_IdParameter = scan_Id.HasValue ?
                new ObjectParameter("scan_Id", scan_Id) :
                new ObjectParameter("scan_Id", typeof(int));
    
            var scan_UsuarioModificaParameter = scan_UsuarioModifica.HasValue ?
                new ObjectParameter("scan_UsuarioModifica", scan_UsuarioModifica) :
                new ObjectParameter("scan_UsuarioModifica", typeof(int));
    
            var scan_FechaModificaParameter = scan_FechaModifica.HasValue ?
                new ObjectParameter("scan_FechaModifica", scan_FechaModifica) :
                new ObjectParameter("scan_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbSeleccionCandidatos_Restore_Result>("UDP_RRHH_tbSeleccionCandidatos_Restore", scan_IdParameter, scan_UsuarioModificaParameter, scan_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbSeleccionCandidatos_Update_Result> UDP_RRHH_tbSeleccionCandidatos_Update(Nullable<int> scan_Id, Nullable<int> fare_Id, Nullable<System.DateTime> scan_Fecha, Nullable<int> req_Id, Nullable<int> scan_UsuarioModifica, Nullable<System.DateTime> scan_FechaModifica)
        {
            var scan_IdParameter = scan_Id.HasValue ?
                new ObjectParameter("scan_Id", scan_Id) :
                new ObjectParameter("scan_Id", typeof(int));
    
            var fare_IdParameter = fare_Id.HasValue ?
                new ObjectParameter("fare_Id", fare_Id) :
                new ObjectParameter("fare_Id", typeof(int));
    
            var scan_FechaParameter = scan_Fecha.HasValue ?
                new ObjectParameter("scan_Fecha", scan_Fecha) :
                new ObjectParameter("scan_Fecha", typeof(System.DateTime));
    
            var req_IdParameter = req_Id.HasValue ?
                new ObjectParameter("req_Id", req_Id) :
                new ObjectParameter("req_Id", typeof(int));
    
            var scan_UsuarioModificaParameter = scan_UsuarioModifica.HasValue ?
                new ObjectParameter("scan_UsuarioModifica", scan_UsuarioModifica) :
                new ObjectParameter("scan_UsuarioModifica", typeof(int));
    
            var scan_FechaModificaParameter = scan_FechaModifica.HasValue ?
                new ObjectParameter("scan_FechaModifica", scan_FechaModifica) :
                new ObjectParameter("scan_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbSeleccionCandidatos_Update_Result>("UDP_RRHH_tbSeleccionCandidatos_Update", scan_IdParameter, fare_IdParameter, scan_FechaParameter, req_IdParameter, scan_UsuarioModificaParameter, scan_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbSucursales_Activar_Result> UDP_RRHH_tbSucursales_Activar(Nullable<int> suc_Id, Nullable<int> suc_UsuarioModifica, Nullable<System.DateTime> suc_FechaModifica)
        {
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var suc_UsuarioModificaParameter = suc_UsuarioModifica.HasValue ?
                new ObjectParameter("suc_UsuarioModifica", suc_UsuarioModifica) :
                new ObjectParameter("suc_UsuarioModifica", typeof(int));
    
            var suc_FechaModificaParameter = suc_FechaModifica.HasValue ?
                new ObjectParameter("suc_FechaModifica", suc_FechaModifica) :
                new ObjectParameter("suc_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbSucursales_Activar_Result>("UDP_RRHH_tbSucursales_Activar", suc_IdParameter, suc_UsuarioModificaParameter, suc_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbSucursales_Inactivar_Result> UDP_RRHH_tbSucursales_Inactivar(Nullable<int> suc_Id, string suc_RazonInactivo, Nullable<int> suc_UsuarioModifica, Nullable<System.DateTime> suc_FechaModifica)
        {
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var suc_RazonInactivoParameter = suc_RazonInactivo != null ?
                new ObjectParameter("suc_RazonInactivo", suc_RazonInactivo) :
                new ObjectParameter("suc_RazonInactivo", typeof(string));
    
            var suc_UsuarioModificaParameter = suc_UsuarioModifica.HasValue ?
                new ObjectParameter("suc_UsuarioModifica", suc_UsuarioModifica) :
                new ObjectParameter("suc_UsuarioModifica", typeof(int));
    
            var suc_FechaModificaParameter = suc_FechaModifica.HasValue ?
                new ObjectParameter("suc_FechaModifica", suc_FechaModifica) :
                new ObjectParameter("suc_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbSucursales_Inactivar_Result>("UDP_RRHH_tbSucursales_Inactivar", suc_IdParameter, suc_RazonInactivoParameter, suc_UsuarioModificaParameter, suc_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbSucursales_Insert_Result> UDP_RRHH_tbSucursales_Insert(Nullable<int> empr_Id, string mun_Codigo, Nullable<int> bod_Id, Nullable<int> pemi_Id, string suc_Descripcion, string suc_Correo, string suc_Direccion, string suc_Telefono, Nullable<int> suc_UsuarioCrea, Nullable<System.DateTime> suc_FechaCrea)
        {
            var empr_IdParameter = empr_Id.HasValue ?
                new ObjectParameter("empr_Id", empr_Id) :
                new ObjectParameter("empr_Id", typeof(int));
    
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var pemi_IdParameter = pemi_Id.HasValue ?
                new ObjectParameter("pemi_Id", pemi_Id) :
                new ObjectParameter("pemi_Id", typeof(int));
    
            var suc_DescripcionParameter = suc_Descripcion != null ?
                new ObjectParameter("suc_Descripcion", suc_Descripcion) :
                new ObjectParameter("suc_Descripcion", typeof(string));
    
            var suc_CorreoParameter = suc_Correo != null ?
                new ObjectParameter("suc_Correo", suc_Correo) :
                new ObjectParameter("suc_Correo", typeof(string));
    
            var suc_DireccionParameter = suc_Direccion != null ?
                new ObjectParameter("suc_Direccion", suc_Direccion) :
                new ObjectParameter("suc_Direccion", typeof(string));
    
            var suc_TelefonoParameter = suc_Telefono != null ?
                new ObjectParameter("suc_Telefono", suc_Telefono) :
                new ObjectParameter("suc_Telefono", typeof(string));
    
            var suc_UsuarioCreaParameter = suc_UsuarioCrea.HasValue ?
                new ObjectParameter("suc_UsuarioCrea", suc_UsuarioCrea) :
                new ObjectParameter("suc_UsuarioCrea", typeof(int));
    
            var suc_FechaCreaParameter = suc_FechaCrea.HasValue ?
                new ObjectParameter("suc_FechaCrea", suc_FechaCrea) :
                new ObjectParameter("suc_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbSucursales_Insert_Result>("UDP_RRHH_tbSucursales_Insert", empr_IdParameter, mun_CodigoParameter, bod_IdParameter, pemi_IdParameter, suc_DescripcionParameter, suc_CorreoParameter, suc_DireccionParameter, suc_TelefonoParameter, suc_UsuarioCreaParameter, suc_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbSucursales_Update_Result> UDP_RRHH_tbSucursales_Update(Nullable<int> suc_Id, Nullable<int> empr_Id, string mun_Codigo, Nullable<int> bod_Id, Nullable<int> pemi_Id, string suc_Descripcion, string suc_Correo, string suc_Direccion, string suc_Telefono, Nullable<int> suc_UsuarioModifica, Nullable<System.DateTime> suc_FechaModifica)
        {
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var empr_IdParameter = empr_Id.HasValue ?
                new ObjectParameter("empr_Id", empr_Id) :
                new ObjectParameter("empr_Id", typeof(int));
    
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var pemi_IdParameter = pemi_Id.HasValue ?
                new ObjectParameter("pemi_Id", pemi_Id) :
                new ObjectParameter("pemi_Id", typeof(int));
    
            var suc_DescripcionParameter = suc_Descripcion != null ?
                new ObjectParameter("suc_Descripcion", suc_Descripcion) :
                new ObjectParameter("suc_Descripcion", typeof(string));
    
            var suc_CorreoParameter = suc_Correo != null ?
                new ObjectParameter("suc_Correo", suc_Correo) :
                new ObjectParameter("suc_Correo", typeof(string));
    
            var suc_DireccionParameter = suc_Direccion != null ?
                new ObjectParameter("suc_Direccion", suc_Direccion) :
                new ObjectParameter("suc_Direccion", typeof(string));
    
            var suc_TelefonoParameter = suc_Telefono != null ?
                new ObjectParameter("suc_Telefono", suc_Telefono) :
                new ObjectParameter("suc_Telefono", typeof(string));
    
            var suc_UsuarioModificaParameter = suc_UsuarioModifica.HasValue ?
                new ObjectParameter("suc_UsuarioModifica", suc_UsuarioModifica) :
                new ObjectParameter("suc_UsuarioModifica", typeof(int));
    
            var suc_FechaModificaParameter = suc_FechaModifica.HasValue ?
                new ObjectParameter("suc_FechaModifica", suc_FechaModifica) :
                new ObjectParameter("suc_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbSucursales_Update_Result>("UDP_RRHH_tbSucursales_Update", suc_IdParameter, empr_IdParameter, mun_CodigoParameter, bod_IdParameter, pemi_IdParameter, suc_DescripcionParameter, suc_CorreoParameter, suc_DireccionParameter, suc_TelefonoParameter, suc_UsuarioModificaParameter, suc_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbSueldos_Insert_Result> UDP_RRHH_tbSueldos_Insert(Nullable<int> sue_Id, Nullable<int> emp_Id, Nullable<int> tmon_Id, Nullable<decimal> sue_Cantidad, Nullable<int> sue_UsuarioCrea, Nullable<int> sue_UsuarioModifica, Nullable<System.DateTime> sue_FechaModifica)
        {
            var sue_IdParameter = sue_Id.HasValue ?
                new ObjectParameter("sue_Id", sue_Id) :
                new ObjectParameter("sue_Id", typeof(int));
    
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var tmon_IdParameter = tmon_Id.HasValue ?
                new ObjectParameter("tmon_Id", tmon_Id) :
                new ObjectParameter("tmon_Id", typeof(int));
    
            var sue_CantidadParameter = sue_Cantidad.HasValue ?
                new ObjectParameter("sue_Cantidad", sue_Cantidad) :
                new ObjectParameter("sue_Cantidad", typeof(decimal));
    
            var sue_UsuarioCreaParameter = sue_UsuarioCrea.HasValue ?
                new ObjectParameter("sue_UsuarioCrea", sue_UsuarioCrea) :
                new ObjectParameter("sue_UsuarioCrea", typeof(int));
    
            var sue_UsuarioModificaParameter = sue_UsuarioModifica.HasValue ?
                new ObjectParameter("sue_UsuarioModifica", sue_UsuarioModifica) :
                new ObjectParameter("sue_UsuarioModifica", typeof(int));
    
            var sue_FechaModificaParameter = sue_FechaModifica.HasValue ?
                new ObjectParameter("sue_FechaModifica", sue_FechaModifica) :
                new ObjectParameter("sue_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbSueldos_Insert_Result>("UDP_RRHH_tbSueldos_Insert", sue_IdParameter, emp_IdParameter, tmon_IdParameter, sue_CantidadParameter, sue_UsuarioCreaParameter, sue_UsuarioModificaParameter, sue_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbSueldos_Restore_Result> UDP_RRHH_tbSueldos_Restore(Nullable<int> sue_id, Nullable<int> sue_UsuarioModifica, Nullable<System.DateTime> sue_FechaModifica)
        {
            var sue_idParameter = sue_id.HasValue ?
                new ObjectParameter("sue_id", sue_id) :
                new ObjectParameter("sue_id", typeof(int));
    
            var sue_UsuarioModificaParameter = sue_UsuarioModifica.HasValue ?
                new ObjectParameter("sue_UsuarioModifica", sue_UsuarioModifica) :
                new ObjectParameter("sue_UsuarioModifica", typeof(int));
    
            var sue_FechaModificaParameter = sue_FechaModifica.HasValue ?
                new ObjectParameter("sue_FechaModifica", sue_FechaModifica) :
                new ObjectParameter("sue_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbSueldos_Restore_Result>("UDP_RRHH_tbSueldos_Restore", sue_idParameter, sue_UsuarioModificaParameter, sue_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoAmonestaciones_Delete_Result> UDP_RRHH_tbTipoAmonestaciones_Delete(Nullable<int> tamo_Id, string tamo_razon_Inactivo, Nullable<int> tamo_UsuarioModifica, Nullable<System.DateTime> tamo_FechaModifica)
        {
            var tamo_IdParameter = tamo_Id.HasValue ?
                new ObjectParameter("tamo_Id", tamo_Id) :
                new ObjectParameter("tamo_Id", typeof(int));
    
            var tamo_razon_InactivoParameter = tamo_razon_Inactivo != null ?
                new ObjectParameter("tamo_razon_Inactivo", tamo_razon_Inactivo) :
                new ObjectParameter("tamo_razon_Inactivo", typeof(string));
    
            var tamo_UsuarioModificaParameter = tamo_UsuarioModifica.HasValue ?
                new ObjectParameter("tamo_UsuarioModifica", tamo_UsuarioModifica) :
                new ObjectParameter("tamo_UsuarioModifica", typeof(int));
    
            var tamo_FechaModificaParameter = tamo_FechaModifica.HasValue ?
                new ObjectParameter("tamo_FechaModifica", tamo_FechaModifica) :
                new ObjectParameter("tamo_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoAmonestaciones_Delete_Result>("UDP_RRHH_tbTipoAmonestaciones_Delete", tamo_IdParameter, tamo_razon_InactivoParameter, tamo_UsuarioModificaParameter, tamo_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoAmonestaciones_Insert_Result> UDP_RRHH_tbTipoAmonestaciones_Insert(string tamo_Descripcion, Nullable<int> tamo_UsuarioCrea, Nullable<System.DateTime> tamo_FechaCrea)
        {
            var tamo_DescripcionParameter = tamo_Descripcion != null ?
                new ObjectParameter("tamo_Descripcion", tamo_Descripcion) :
                new ObjectParameter("tamo_Descripcion", typeof(string));
    
            var tamo_UsuarioCreaParameter = tamo_UsuarioCrea.HasValue ?
                new ObjectParameter("tamo_UsuarioCrea", tamo_UsuarioCrea) :
                new ObjectParameter("tamo_UsuarioCrea", typeof(int));
    
            var tamo_FechaCreaParameter = tamo_FechaCrea.HasValue ?
                new ObjectParameter("tamo_FechaCrea", tamo_FechaCrea) :
                new ObjectParameter("tamo_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoAmonestaciones_Insert_Result>("UDP_RRHH_tbTipoAmonestaciones_Insert", tamo_DescripcionParameter, tamo_UsuarioCreaParameter, tamo_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoAmonestaciones_Restore_Result> UDP_RRHH_tbTipoAmonestaciones_Restore(Nullable<int> tamo_Id, Nullable<int> tamo_UsuarioModifica, Nullable<System.DateTime> tamo_FechaModifica)
        {
            var tamo_IdParameter = tamo_Id.HasValue ?
                new ObjectParameter("tamo_Id", tamo_Id) :
                new ObjectParameter("tamo_Id", typeof(int));
    
            var tamo_UsuarioModificaParameter = tamo_UsuarioModifica.HasValue ?
                new ObjectParameter("tamo_UsuarioModifica", tamo_UsuarioModifica) :
                new ObjectParameter("tamo_UsuarioModifica", typeof(int));
    
            var tamo_FechaModificaParameter = tamo_FechaModifica.HasValue ?
                new ObjectParameter("tamo_FechaModifica", tamo_FechaModifica) :
                new ObjectParameter("tamo_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoAmonestaciones_Restore_Result>("UDP_RRHH_tbTipoAmonestaciones_Restore", tamo_IdParameter, tamo_UsuarioModificaParameter, tamo_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoAmonestaciones_Update_Result> UDP_RRHH_tbTipoAmonestaciones_Update(Nullable<int> tamo_Id, string tamo_Descripcion, Nullable<int> tamo_UsuarioModifica, Nullable<System.DateTime> tamo_FechaModifica)
        {
            var tamo_IdParameter = tamo_Id.HasValue ?
                new ObjectParameter("tamo_Id", tamo_Id) :
                new ObjectParameter("tamo_Id", typeof(int));
    
            var tamo_DescripcionParameter = tamo_Descripcion != null ?
                new ObjectParameter("tamo_Descripcion", tamo_Descripcion) :
                new ObjectParameter("tamo_Descripcion", typeof(string));
    
            var tamo_UsuarioModificaParameter = tamo_UsuarioModifica.HasValue ?
                new ObjectParameter("tamo_UsuarioModifica", tamo_UsuarioModifica) :
                new ObjectParameter("tamo_UsuarioModifica", typeof(int));
    
            var tamo_FechaModificaParameter = tamo_FechaModifica.HasValue ?
                new ObjectParameter("tamo_FechaModifica", tamo_FechaModifica) :
                new ObjectParameter("tamo_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoAmonestaciones_Update_Result>("UDP_RRHH_tbTipoAmonestaciones_Update", tamo_IdParameter, tamo_DescripcionParameter, tamo_UsuarioModificaParameter, tamo_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoHora_Update_Result> UDP_RRHH_tbTipoHora_Update(Nullable<int> tiho_Id, string tiho_Descripcion, Nullable<int> tiho_Recargo, Nullable<int> tiho_UsuarioModifica, Nullable<System.DateTime> tiho_FechaModifica)
        {
            var tiho_IdParameter = tiho_Id.HasValue ?
                new ObjectParameter("tiho_Id", tiho_Id) :
                new ObjectParameter("tiho_Id", typeof(int));
    
            var tiho_DescripcionParameter = tiho_Descripcion != null ?
                new ObjectParameter("tiho_Descripcion", tiho_Descripcion) :
                new ObjectParameter("tiho_Descripcion", typeof(string));
    
            var tiho_RecargoParameter = tiho_Recargo.HasValue ?
                new ObjectParameter("tiho_Recargo", tiho_Recargo) :
                new ObjectParameter("tiho_Recargo", typeof(int));
    
            var tiho_UsuarioModificaParameter = tiho_UsuarioModifica.HasValue ?
                new ObjectParameter("tiho_UsuarioModifica", tiho_UsuarioModifica) :
                new ObjectParameter("tiho_UsuarioModifica", typeof(int));
    
            var tiho_FechaModificaParameter = tiho_FechaModifica.HasValue ?
                new ObjectParameter("tiho_FechaModifica", tiho_FechaModifica) :
                new ObjectParameter("tiho_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoHora_Update_Result>("UDP_RRHH_tbTipoHora_Update", tiho_IdParameter, tiho_DescripcionParameter, tiho_RecargoParameter, tiho_UsuarioModificaParameter, tiho_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoHoras_Delete_Result> UDP_RRHH_tbTipoHoras_Delete(Nullable<int> tiho_Id, string tiho_razon_Inactivo, Nullable<int> tiho_UsuarioModifica, Nullable<System.DateTime> tiho_FechaModifica)
        {
            var tiho_IdParameter = tiho_Id.HasValue ?
                new ObjectParameter("tiho_Id", tiho_Id) :
                new ObjectParameter("tiho_Id", typeof(int));
    
            var tiho_razon_InactivoParameter = tiho_razon_Inactivo != null ?
                new ObjectParameter("tiho_razon_Inactivo", tiho_razon_Inactivo) :
                new ObjectParameter("tiho_razon_Inactivo", typeof(string));
    
            var tiho_UsuarioModificaParameter = tiho_UsuarioModifica.HasValue ?
                new ObjectParameter("tiho_UsuarioModifica", tiho_UsuarioModifica) :
                new ObjectParameter("tiho_UsuarioModifica", typeof(int));
    
            var tiho_FechaModificaParameter = tiho_FechaModifica.HasValue ?
                new ObjectParameter("tiho_FechaModifica", tiho_FechaModifica) :
                new ObjectParameter("tiho_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoHoras_Delete_Result>("UDP_RRHH_tbTipoHoras_Delete", tiho_IdParameter, tiho_razon_InactivoParameter, tiho_UsuarioModificaParameter, tiho_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoHoras_Insert_Result> UDP_RRHH_tbTipoHoras_Insert(string tiho_Descripcion, Nullable<int> tiho_Recargo, Nullable<int> tiho_UsuarioCrea, Nullable<System.DateTime> tiho_FechaCrea)
        {
            var tiho_DescripcionParameter = tiho_Descripcion != null ?
                new ObjectParameter("tiho_Descripcion", tiho_Descripcion) :
                new ObjectParameter("tiho_Descripcion", typeof(string));
    
            var tiho_RecargoParameter = tiho_Recargo.HasValue ?
                new ObjectParameter("tiho_Recargo", tiho_Recargo) :
                new ObjectParameter("tiho_Recargo", typeof(int));
    
            var tiho_UsuarioCreaParameter = tiho_UsuarioCrea.HasValue ?
                new ObjectParameter("tiho_UsuarioCrea", tiho_UsuarioCrea) :
                new ObjectParameter("tiho_UsuarioCrea", typeof(int));
    
            var tiho_FechaCreaParameter = tiho_FechaCrea.HasValue ?
                new ObjectParameter("tiho_FechaCrea", tiho_FechaCrea) :
                new ObjectParameter("tiho_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoHoras_Insert_Result>("UDP_RRHH_tbTipoHoras_Insert", tiho_DescripcionParameter, tiho_RecargoParameter, tiho_UsuarioCreaParameter, tiho_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoHoras_Restore_Result> UDP_RRHH_tbTipoHoras_Restore(Nullable<int> tiho_Id, Nullable<int> tiho_UsuarioModifica, Nullable<System.DateTime> tiho_FechaModifica)
        {
            var tiho_IdParameter = tiho_Id.HasValue ?
                new ObjectParameter("tiho_Id", tiho_Id) :
                new ObjectParameter("tiho_Id", typeof(int));
    
            var tiho_UsuarioModificaParameter = tiho_UsuarioModifica.HasValue ?
                new ObjectParameter("tiho_UsuarioModifica", tiho_UsuarioModifica) :
                new ObjectParameter("tiho_UsuarioModifica", typeof(int));
    
            var tiho_FechaModificaParameter = tiho_FechaModifica.HasValue ?
                new ObjectParameter("tiho_FechaModifica", tiho_FechaModifica) :
                new ObjectParameter("tiho_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoHoras_Restore_Result>("UDP_RRHH_tbTipoHoras_Restore", tiho_IdParameter, tiho_UsuarioModificaParameter, tiho_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoHoras_Select_Result> UDP_RRHH_tbTipoHoras_Select(Nullable<int> tiho_Id)
        {
            var tiho_IdParameter = tiho_Id.HasValue ?
                new ObjectParameter("tiho_Id", tiho_Id) :
                new ObjectParameter("tiho_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoHoras_Select_Result>("UDP_RRHH_tbTipoHoras_Select", tiho_IdParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoIncapacidades_Delete_Result> UDP_RRHH_tbTipoIncapacidades_Delete(Nullable<int> ticn_Id, string ticn_razon_Inactivo, Nullable<int> ticn_UsuarioModifica, Nullable<System.DateTime> ticn_FechaModifica)
        {
            var ticn_IdParameter = ticn_Id.HasValue ?
                new ObjectParameter("ticn_Id", ticn_Id) :
                new ObjectParameter("ticn_Id", typeof(int));
    
            var ticn_razon_InactivoParameter = ticn_razon_Inactivo != null ?
                new ObjectParameter("ticn_razon_Inactivo", ticn_razon_Inactivo) :
                new ObjectParameter("ticn_razon_Inactivo", typeof(string));
    
            var ticn_UsuarioModificaParameter = ticn_UsuarioModifica.HasValue ?
                new ObjectParameter("ticn_UsuarioModifica", ticn_UsuarioModifica) :
                new ObjectParameter("ticn_UsuarioModifica", typeof(int));
    
            var ticn_FechaModificaParameter = ticn_FechaModifica.HasValue ?
                new ObjectParameter("ticn_FechaModifica", ticn_FechaModifica) :
                new ObjectParameter("ticn_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoIncapacidades_Delete_Result>("UDP_RRHH_tbTipoIncapacidades_Delete", ticn_IdParameter, ticn_razon_InactivoParameter, ticn_UsuarioModificaParameter, ticn_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoIncapacidades_Insert_Result> UDP_RRHH_tbTipoIncapacidades_Insert(string ticn_Descripcion, Nullable<int> ticn_Usuariocrea, Nullable<System.DateTime> ticn_FechaCrea)
        {
            var ticn_DescripcionParameter = ticn_Descripcion != null ?
                new ObjectParameter("ticn_Descripcion", ticn_Descripcion) :
                new ObjectParameter("ticn_Descripcion", typeof(string));
    
            var ticn_UsuariocreaParameter = ticn_Usuariocrea.HasValue ?
                new ObjectParameter("ticn_Usuariocrea", ticn_Usuariocrea) :
                new ObjectParameter("ticn_Usuariocrea", typeof(int));
    
            var ticn_FechaCreaParameter = ticn_FechaCrea.HasValue ?
                new ObjectParameter("ticn_FechaCrea", ticn_FechaCrea) :
                new ObjectParameter("ticn_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoIncapacidades_Insert_Result>("UDP_RRHH_tbTipoIncapacidades_Insert", ticn_DescripcionParameter, ticn_UsuariocreaParameter, ticn_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoIncapacidades_Restore_Result> UDP_RRHH_tbTipoIncapacidades_Restore(Nullable<int> ticn_Id, Nullable<int> ticn_UsuarioModifica, Nullable<System.DateTime> ticn_FechaModifica)
        {
            var ticn_IdParameter = ticn_Id.HasValue ?
                new ObjectParameter("ticn_Id", ticn_Id) :
                new ObjectParameter("ticn_Id", typeof(int));
    
            var ticn_UsuarioModificaParameter = ticn_UsuarioModifica.HasValue ?
                new ObjectParameter("ticn_UsuarioModifica", ticn_UsuarioModifica) :
                new ObjectParameter("ticn_UsuarioModifica", typeof(int));
    
            var ticn_FechaModificaParameter = ticn_FechaModifica.HasValue ?
                new ObjectParameter("ticn_FechaModifica", ticn_FechaModifica) :
                new ObjectParameter("ticn_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoIncapacidades_Restore_Result>("UDP_RRHH_tbTipoIncapacidades_Restore", ticn_IdParameter, ticn_UsuarioModificaParameter, ticn_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoIncapacidades_Update_Result> UDP_RRHH_tbTipoIncapacidades_Update(Nullable<int> ticn_Id, string ticn_Descripcion, Nullable<int> ticn_UsuarioModifica, Nullable<System.DateTime> ticn_FechaModifica)
        {
            var ticn_IdParameter = ticn_Id.HasValue ?
                new ObjectParameter("ticn_Id", ticn_Id) :
                new ObjectParameter("ticn_Id", typeof(int));
    
            var ticn_DescripcionParameter = ticn_Descripcion != null ?
                new ObjectParameter("ticn_Descripcion", ticn_Descripcion) :
                new ObjectParameter("ticn_Descripcion", typeof(string));
    
            var ticn_UsuarioModificaParameter = ticn_UsuarioModifica.HasValue ?
                new ObjectParameter("ticn_UsuarioModifica", ticn_UsuarioModifica) :
                new ObjectParameter("ticn_UsuarioModifica", typeof(int));
    
            var ticn_FechaModificaParameter = ticn_FechaModifica.HasValue ?
                new ObjectParameter("ticn_FechaModifica", ticn_FechaModifica) :
                new ObjectParameter("ticn_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoIncapacidades_Update_Result>("UDP_RRHH_tbTipoIncapacidades_Update", ticn_IdParameter, ticn_DescripcionParameter, ticn_UsuarioModificaParameter, ticn_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoMoneda_Update_Result> UDP_RRHH_tbTipoMoneda_Update(Nullable<int> tmon_Id, string tmon_Descripcion, Nullable<int> tmon_UsuarioModifica, Nullable<System.DateTime> tmon_FechaModifica)
        {
            var tmon_IdParameter = tmon_Id.HasValue ?
                new ObjectParameter("tmon_Id", tmon_Id) :
                new ObjectParameter("tmon_Id", typeof(int));
    
            var tmon_DescripcionParameter = tmon_Descripcion != null ?
                new ObjectParameter("tmon_Descripcion", tmon_Descripcion) :
                new ObjectParameter("tmon_Descripcion", typeof(string));
    
            var tmon_UsuarioModificaParameter = tmon_UsuarioModifica.HasValue ?
                new ObjectParameter("tmon_UsuarioModifica", tmon_UsuarioModifica) :
                new ObjectParameter("tmon_UsuarioModifica", typeof(int));
    
            var tmon_FechaModificaParameter = tmon_FechaModifica.HasValue ?
                new ObjectParameter("tmon_FechaModifica", tmon_FechaModifica) :
                new ObjectParameter("tmon_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoMoneda_Update_Result>("UDP_RRHH_tbTipoMoneda_Update", tmon_IdParameter, tmon_DescripcionParameter, tmon_UsuarioModificaParameter, tmon_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoMonedas_Delete_Result> UDP_RRHH_tbTipoMonedas_Delete(Nullable<int> tmon_Id, string tmon_razon_Inactivo, Nullable<int> tmon_UsuarioModifica, Nullable<System.DateTime> tmon_FechaModifica)
        {
            var tmon_IdParameter = tmon_Id.HasValue ?
                new ObjectParameter("tmon_Id", tmon_Id) :
                new ObjectParameter("tmon_Id", typeof(int));
    
            var tmon_razon_InactivoParameter = tmon_razon_Inactivo != null ?
                new ObjectParameter("tmon_razon_Inactivo", tmon_razon_Inactivo) :
                new ObjectParameter("tmon_razon_Inactivo", typeof(string));
    
            var tmon_UsuarioModificaParameter = tmon_UsuarioModifica.HasValue ?
                new ObjectParameter("tmon_UsuarioModifica", tmon_UsuarioModifica) :
                new ObjectParameter("tmon_UsuarioModifica", typeof(int));
    
            var tmon_FechaModificaParameter = tmon_FechaModifica.HasValue ?
                new ObjectParameter("tmon_FechaModifica", tmon_FechaModifica) :
                new ObjectParameter("tmon_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoMonedas_Delete_Result>("UDP_RRHH_tbTipoMonedas_Delete", tmon_IdParameter, tmon_razon_InactivoParameter, tmon_UsuarioModificaParameter, tmon_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoMonedas_Insert_Result> UDP_RRHH_tbTipoMonedas_Insert(string tmon_Descripcion, Nullable<int> tmon_UsuarioCrea, Nullable<System.DateTime> tmon_FechaCrea)
        {
            var tmon_DescripcionParameter = tmon_Descripcion != null ?
                new ObjectParameter("tmon_Descripcion", tmon_Descripcion) :
                new ObjectParameter("tmon_Descripcion", typeof(string));
    
            var tmon_UsuarioCreaParameter = tmon_UsuarioCrea.HasValue ?
                new ObjectParameter("tmon_UsuarioCrea", tmon_UsuarioCrea) :
                new ObjectParameter("tmon_UsuarioCrea", typeof(int));
    
            var tmon_FechaCreaParameter = tmon_FechaCrea.HasValue ?
                new ObjectParameter("tmon_FechaCrea", tmon_FechaCrea) :
                new ObjectParameter("tmon_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoMonedas_Insert_Result>("UDP_RRHH_tbTipoMonedas_Insert", tmon_DescripcionParameter, tmon_UsuarioCreaParameter, tmon_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoMonedas_Restore_Result> UDP_RRHH_tbTipoMonedas_Restore(Nullable<int> tmon_Id, Nullable<int> tmon_UsuarioModifica, Nullable<System.DateTime> tmon_FechaModifica)
        {
            var tmon_IdParameter = tmon_Id.HasValue ?
                new ObjectParameter("tmon_Id", tmon_Id) :
                new ObjectParameter("tmon_Id", typeof(int));
    
            var tmon_UsuarioModificaParameter = tmon_UsuarioModifica.HasValue ?
                new ObjectParameter("tmon_UsuarioModifica", tmon_UsuarioModifica) :
                new ObjectParameter("tmon_UsuarioModifica", typeof(int));
    
            var tmon_FechaModificaParameter = tmon_FechaModifica.HasValue ?
                new ObjectParameter("tmon_FechaModifica", tmon_FechaModifica) :
                new ObjectParameter("tmon_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoMonedas_Restore_Result>("UDP_RRHH_tbTipoMonedas_Restore", tmon_IdParameter, tmon_UsuarioModificaParameter, tmon_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoMonedas_Select_Result> UDP_RRHH_tbTipoMonedas_Select(Nullable<int> tmon_Id)
        {
            var tmon_IdParameter = tmon_Id.HasValue ?
                new ObjectParameter("tmon_Id", tmon_Id) :
                new ObjectParameter("tmon_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoMonedas_Select_Result>("UDP_RRHH_tbTipoMonedas_Select", tmon_IdParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoPermisos_Delete_Result> UDP_RRHH_tbTipoPermisos_Delete(Nullable<int> tper_Id, string tper_razon_Inactivo, Nullable<int> tper_UsuarioModifica, Nullable<System.DateTime> tper_FechaModifica)
        {
            var tper_IdParameter = tper_Id.HasValue ?
                new ObjectParameter("tper_Id", tper_Id) :
                new ObjectParameter("tper_Id", typeof(int));
    
            var tper_razon_InactivoParameter = tper_razon_Inactivo != null ?
                new ObjectParameter("tper_razon_Inactivo", tper_razon_Inactivo) :
                new ObjectParameter("tper_razon_Inactivo", typeof(string));
    
            var tper_UsuarioModificaParameter = tper_UsuarioModifica.HasValue ?
                new ObjectParameter("tper_UsuarioModifica", tper_UsuarioModifica) :
                new ObjectParameter("tper_UsuarioModifica", typeof(int));
    
            var tper_FechaModificaParameter = tper_FechaModifica.HasValue ?
                new ObjectParameter("tper_FechaModifica", tper_FechaModifica) :
                new ObjectParameter("tper_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoPermisos_Delete_Result>("UDP_RRHH_tbTipoPermisos_Delete", tper_IdParameter, tper_razon_InactivoParameter, tper_UsuarioModificaParameter, tper_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoPermisos_Insert_Result> UDP_RRHH_tbTipoPermisos_Insert(string tper_Descripcion, Nullable<int> tper_UsuarioCrea, Nullable<System.DateTime> tper_FechaCrea)
        {
            var tper_DescripcionParameter = tper_Descripcion != null ?
                new ObjectParameter("tper_Descripcion", tper_Descripcion) :
                new ObjectParameter("tper_Descripcion", typeof(string));
    
            var tper_UsuarioCreaParameter = tper_UsuarioCrea.HasValue ?
                new ObjectParameter("tper_UsuarioCrea", tper_UsuarioCrea) :
                new ObjectParameter("tper_UsuarioCrea", typeof(int));
    
            var tper_FechaCreaParameter = tper_FechaCrea.HasValue ?
                new ObjectParameter("tper_FechaCrea", tper_FechaCrea) :
                new ObjectParameter("tper_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoPermisos_Insert_Result>("UDP_RRHH_tbTipoPermisos_Insert", tper_DescripcionParameter, tper_UsuarioCreaParameter, tper_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoPermisos_Restore_Result> UDP_RRHH_tbTipoPermisos_Restore(Nullable<int> tper_Id, Nullable<int> tper_UsuarioModifica, Nullable<System.DateTime> tper_FechaModifica)
        {
            var tper_IdParameter = tper_Id.HasValue ?
                new ObjectParameter("tper_Id", tper_Id) :
                new ObjectParameter("tper_Id", typeof(int));
    
            var tper_UsuarioModificaParameter = tper_UsuarioModifica.HasValue ?
                new ObjectParameter("tper_UsuarioModifica", tper_UsuarioModifica) :
                new ObjectParameter("tper_UsuarioModifica", typeof(int));
    
            var tper_FechaModificaParameter = tper_FechaModifica.HasValue ?
                new ObjectParameter("tper_FechaModifica", tper_FechaModifica) :
                new ObjectParameter("tper_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoPermisos_Restore_Result>("UDP_RRHH_tbTipoPermisos_Restore", tper_IdParameter, tper_UsuarioModificaParameter, tper_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoPermisos_Update_Result> UDP_RRHH_tbTipoPermisos_Update(Nullable<int> tper_Id, string tper_Descripcion, Nullable<int> tper_UsuarioModifica, Nullable<System.DateTime> tper_FechaModifica)
        {
            var tper_IdParameter = tper_Id.HasValue ?
                new ObjectParameter("tper_Id", tper_Id) :
                new ObjectParameter("tper_Id", typeof(int));
    
            var tper_DescripcionParameter = tper_Descripcion != null ?
                new ObjectParameter("tper_Descripcion", tper_Descripcion) :
                new ObjectParameter("tper_Descripcion", typeof(string));
    
            var tper_UsuarioModificaParameter = tper_UsuarioModifica.HasValue ?
                new ObjectParameter("tper_UsuarioModifica", tper_UsuarioModifica) :
                new ObjectParameter("tper_UsuarioModifica", typeof(int));
    
            var tper_FechaModificaParameter = tper_FechaModifica.HasValue ?
                new ObjectParameter("tper_FechaModifica", tper_FechaModifica) :
                new ObjectParameter("tper_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoPermisos_Update_Result>("UDP_RRHH_tbTipoPermisos_Update", tper_IdParameter, tper_DescripcionParameter, tper_UsuarioModificaParameter, tper_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoSalidas_Delete_Result> UDP_RRHH_tbTipoSalidas_Delete(Nullable<int> tsal_id, string tsal_razon_Inactivo, Nullable<int> tsal_UsuarioModifica, Nullable<System.DateTime> tsal_FechaModifica)
        {
            var tsal_idParameter = tsal_id.HasValue ?
                new ObjectParameter("tsal_id", tsal_id) :
                new ObjectParameter("tsal_id", typeof(int));
    
            var tsal_razon_InactivoParameter = tsal_razon_Inactivo != null ?
                new ObjectParameter("tsal_razon_Inactivo", tsal_razon_Inactivo) :
                new ObjectParameter("tsal_razon_Inactivo", typeof(string));
    
            var tsal_UsuarioModificaParameter = tsal_UsuarioModifica.HasValue ?
                new ObjectParameter("tsal_UsuarioModifica", tsal_UsuarioModifica) :
                new ObjectParameter("tsal_UsuarioModifica", typeof(int));
    
            var tsal_FechaModificaParameter = tsal_FechaModifica.HasValue ?
                new ObjectParameter("tsal_FechaModifica", tsal_FechaModifica) :
                new ObjectParameter("tsal_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoSalidas_Delete_Result>("UDP_RRHH_tbTipoSalidas_Delete", tsal_idParameter, tsal_razon_InactivoParameter, tsal_UsuarioModificaParameter, tsal_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoSalidas_Insert_Result> UDP_RRHH_tbTipoSalidas_Insert(string tsal_Descripcion, Nullable<int> tsal_UsuarioCrea, Nullable<System.DateTime> tsal_FechaCrea)
        {
            var tsal_DescripcionParameter = tsal_Descripcion != null ?
                new ObjectParameter("tsal_Descripcion", tsal_Descripcion) :
                new ObjectParameter("tsal_Descripcion", typeof(string));
    
            var tsal_UsuarioCreaParameter = tsal_UsuarioCrea.HasValue ?
                new ObjectParameter("tsal_UsuarioCrea", tsal_UsuarioCrea) :
                new ObjectParameter("tsal_UsuarioCrea", typeof(int));
    
            var tsal_FechaCreaParameter = tsal_FechaCrea.HasValue ?
                new ObjectParameter("tsal_FechaCrea", tsal_FechaCrea) :
                new ObjectParameter("tsal_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoSalidas_Insert_Result>("UDP_RRHH_tbTipoSalidas_Insert", tsal_DescripcionParameter, tsal_UsuarioCreaParameter, tsal_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoSalidas_Restore_Result> UDP_RRHH_tbTipoSalidas_Restore(Nullable<int> tsal_id, Nullable<int> tsal_UsuarioModifica, Nullable<System.DateTime> tsal_FechaModifica)
        {
            var tsal_idParameter = tsal_id.HasValue ?
                new ObjectParameter("tsal_id", tsal_id) :
                new ObjectParameter("tsal_id", typeof(int));
    
            var tsal_UsuarioModificaParameter = tsal_UsuarioModifica.HasValue ?
                new ObjectParameter("tsal_UsuarioModifica", tsal_UsuarioModifica) :
                new ObjectParameter("tsal_UsuarioModifica", typeof(int));
    
            var tsal_FechaModificaParameter = tsal_FechaModifica.HasValue ?
                new ObjectParameter("tsal_FechaModifica", tsal_FechaModifica) :
                new ObjectParameter("tsal_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoSalidas_Restore_Result>("UDP_RRHH_tbTipoSalidas_Restore", tsal_idParameter, tsal_UsuarioModificaParameter, tsal_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTipoSalidas_Update_Result> UDP_RRHH_tbTipoSalidas_Update(Nullable<int> tsal_Id, string tsal_Descripcion, Nullable<int> tsal_UsuarioModifica, Nullable<System.DateTime> tsal_FechaModifica)
        {
            var tsal_IdParameter = tsal_Id.HasValue ?
                new ObjectParameter("tsal_Id", tsal_Id) :
                new ObjectParameter("tsal_Id", typeof(int));
    
            var tsal_DescripcionParameter = tsal_Descripcion != null ?
                new ObjectParameter("tsal_Descripcion", tsal_Descripcion) :
                new ObjectParameter("tsal_Descripcion", typeof(string));
    
            var tsal_UsuarioModificaParameter = tsal_UsuarioModifica.HasValue ?
                new ObjectParameter("tsal_UsuarioModifica", tsal_UsuarioModifica) :
                new ObjectParameter("tsal_UsuarioModifica", typeof(int));
    
            var tsal_FechaModificaParameter = tsal_FechaModifica.HasValue ?
                new ObjectParameter("tsal_FechaModifica", tsal_FechaModifica) :
                new ObjectParameter("tsal_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTipoSalidas_Update_Result>("UDP_RRHH_tbTipoSalidas_Update", tsal_IdParameter, tsal_DescripcionParameter, tsal_UsuarioModificaParameter, tsal_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTitulos_Delete_Result> UDP_RRHH_tbTitulos_Delete(Nullable<int> titu_id, string titu_razon_Inactivo, Nullable<int> titu_UsuarioModifica, Nullable<System.DateTime> titu_FechaModifica)
        {
            var titu_idParameter = titu_id.HasValue ?
                new ObjectParameter("titu_id", titu_id) :
                new ObjectParameter("titu_id", typeof(int));
    
            var titu_razon_InactivoParameter = titu_razon_Inactivo != null ?
                new ObjectParameter("titu_razon_Inactivo", titu_razon_Inactivo) :
                new ObjectParameter("titu_razon_Inactivo", typeof(string));
    
            var titu_UsuarioModificaParameter = titu_UsuarioModifica.HasValue ?
                new ObjectParameter("titu_UsuarioModifica", titu_UsuarioModifica) :
                new ObjectParameter("titu_UsuarioModifica", typeof(int));
    
            var titu_FechaModificaParameter = titu_FechaModifica.HasValue ?
                new ObjectParameter("titu_FechaModifica", titu_FechaModifica) :
                new ObjectParameter("titu_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTitulos_Delete_Result>("UDP_RRHH_tbTitulos_Delete", titu_idParameter, titu_razon_InactivoParameter, titu_UsuarioModificaParameter, titu_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTitulos_Insert_Result> UDP_RRHH_tbTitulos_Insert(string titu_Descripcion, Nullable<int> titu_UsuarioCrea, Nullable<System.DateTime> titu_FechaCrea)
        {
            var titu_DescripcionParameter = titu_Descripcion != null ?
                new ObjectParameter("titu_Descripcion", titu_Descripcion) :
                new ObjectParameter("titu_Descripcion", typeof(string));
    
            var titu_UsuarioCreaParameter = titu_UsuarioCrea.HasValue ?
                new ObjectParameter("titu_UsuarioCrea", titu_UsuarioCrea) :
                new ObjectParameter("titu_UsuarioCrea", typeof(int));
    
            var titu_FechaCreaParameter = titu_FechaCrea.HasValue ?
                new ObjectParameter("titu_FechaCrea", titu_FechaCrea) :
                new ObjectParameter("titu_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTitulos_Insert_Result>("UDP_RRHH_tbTitulos_Insert", titu_DescripcionParameter, titu_UsuarioCreaParameter, titu_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTitulos_Restore_Result> UDP_RRHH_tbTitulos_Restore(Nullable<int> titu_id, Nullable<int> titu_UsuarioModifica, Nullable<System.DateTime> titu_FechaModifica)
        {
            var titu_idParameter = titu_id.HasValue ?
                new ObjectParameter("titu_id", titu_id) :
                new ObjectParameter("titu_id", typeof(int));
    
            var titu_UsuarioModificaParameter = titu_UsuarioModifica.HasValue ?
                new ObjectParameter("titu_UsuarioModifica", titu_UsuarioModifica) :
                new ObjectParameter("titu_UsuarioModifica", typeof(int));
    
            var titu_FechaModificaParameter = titu_FechaModifica.HasValue ?
                new ObjectParameter("titu_FechaModifica", titu_FechaModifica) :
                new ObjectParameter("titu_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTitulos_Restore_Result>("UDP_RRHH_tbTitulos_Restore", titu_idParameter, titu_UsuarioModificaParameter, titu_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTitulos_Update_Result> UDP_RRHH_tbTitulos_Update(Nullable<int> titu_Id, string titu_Descripcion, Nullable<int> titu_UsuarioModifica, Nullable<System.DateTime> titu_FechaModifica)
        {
            var titu_IdParameter = titu_Id.HasValue ?
                new ObjectParameter("titu_Id", titu_Id) :
                new ObjectParameter("titu_Id", typeof(int));
    
            var titu_DescripcionParameter = titu_Descripcion != null ?
                new ObjectParameter("titu_Descripcion", titu_Descripcion) :
                new ObjectParameter("titu_Descripcion", typeof(string));
    
            var titu_UsuarioModificaParameter = titu_UsuarioModifica.HasValue ?
                new ObjectParameter("titu_UsuarioModifica", titu_UsuarioModifica) :
                new ObjectParameter("titu_UsuarioModifica", typeof(int));
    
            var titu_FechaModificaParameter = titu_FechaModifica.HasValue ?
                new ObjectParameter("titu_FechaModifica", titu_FechaModifica) :
                new ObjectParameter("titu_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTitulos_Update_Result>("UDP_RRHH_tbTitulos_Update", titu_IdParameter, titu_DescripcionParameter, titu_UsuarioModificaParameter, titu_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTitulosPersona_Inactivar_Result> UDP_RRHH_tbTitulosPersona_Inactivar(Nullable<int> tipe_Id, string tipe_RazonInactivo, Nullable<int> tipe_UsuarioModifica, Nullable<System.DateTime> tipe_FechaModifica)
        {
            var tipe_IdParameter = tipe_Id.HasValue ?
                new ObjectParameter("tipe_Id", tipe_Id) :
                new ObjectParameter("tipe_Id", typeof(int));
    
            var tipe_RazonInactivoParameter = tipe_RazonInactivo != null ?
                new ObjectParameter("tipe_RazonInactivo", tipe_RazonInactivo) :
                new ObjectParameter("tipe_RazonInactivo", typeof(string));
    
            var tipe_UsuarioModificaParameter = tipe_UsuarioModifica.HasValue ?
                new ObjectParameter("tipe_UsuarioModifica", tipe_UsuarioModifica) :
                new ObjectParameter("tipe_UsuarioModifica", typeof(int));
    
            var tipe_FechaModificaParameter = tipe_FechaModifica.HasValue ?
                new ObjectParameter("tipe_FechaModifica", tipe_FechaModifica) :
                new ObjectParameter("tipe_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTitulosPersona_Inactivar_Result>("UDP_RRHH_tbTitulosPersona_Inactivar", tipe_IdParameter, tipe_RazonInactivoParameter, tipe_UsuarioModificaParameter, tipe_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTitulosPersona_Insert_Result> UDP_RRHH_tbTitulosPersona_Insert(Nullable<int> per_Id, Nullable<int> titu_Id, Nullable<int> titu_Anio, Nullable<int> tipe_UsuarioCrea, Nullable<System.DateTime> tipe_FechaCrea)
        {
            var per_IdParameter = per_Id.HasValue ?
                new ObjectParameter("per_Id", per_Id) :
                new ObjectParameter("per_Id", typeof(int));
    
            var titu_IdParameter = titu_Id.HasValue ?
                new ObjectParameter("titu_Id", titu_Id) :
                new ObjectParameter("titu_Id", typeof(int));
    
            var titu_AnioParameter = titu_Anio.HasValue ?
                new ObjectParameter("titu_Anio", titu_Anio) :
                new ObjectParameter("titu_Anio", typeof(int));
    
            var tipe_UsuarioCreaParameter = tipe_UsuarioCrea.HasValue ?
                new ObjectParameter("tipe_UsuarioCrea", tipe_UsuarioCrea) :
                new ObjectParameter("tipe_UsuarioCrea", typeof(int));
    
            var tipe_FechaCreaParameter = tipe_FechaCrea.HasValue ?
                new ObjectParameter("tipe_FechaCrea", tipe_FechaCrea) :
                new ObjectParameter("tipe_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTitulosPersona_Insert_Result>("UDP_RRHH_tbTitulosPersona_Insert", per_IdParameter, titu_IdParameter, titu_AnioParameter, tipe_UsuarioCreaParameter, tipe_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_RRHH_tbTitulosPersona_Restore_Result> UDP_RRHH_tbTitulosPersona_Restore(Nullable<int> tipe_Id, Nullable<int> tipe_UsuarioModifica, Nullable<System.DateTime> tipe_FechaModifica)
        {
            var tipe_IdParameter = tipe_Id.HasValue ?
                new ObjectParameter("tipe_Id", tipe_Id) :
                new ObjectParameter("tipe_Id", typeof(int));
    
            var tipe_UsuarioModificaParameter = tipe_UsuarioModifica.HasValue ?
                new ObjectParameter("tipe_UsuarioModifica", tipe_UsuarioModifica) :
                new ObjectParameter("tipe_UsuarioModifica", typeof(int));
    
            var tipe_FechaModificaParameter = tipe_FechaModifica.HasValue ?
                new ObjectParameter("tipe_FechaModifica", tipe_FechaModifica) :
                new ObjectParameter("tipe_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_RRHH_tbTitulosPersona_Restore_Result>("UDP_RRHH_tbTitulosPersona_Restore", tipe_IdParameter, tipe_UsuarioModificaParameter, tipe_FechaModificaParameter);
        }
    
        public virtual ObjectResult<string> crud_tbExoneracionInsert(string exo_Documento, Nullable<bool> exo_ExoneracionActiva, Nullable<System.DateTime> exo_FechaInicialVigencia, Nullable<System.DateTime> exo_FechaIFinalVigencia, Nullable<int> clte_Id)
        {
            var exo_DocumentoParameter = exo_Documento != null ?
                new ObjectParameter("exo_Documento", exo_Documento) :
                new ObjectParameter("exo_Documento", typeof(string));
    
            var exo_ExoneracionActivaParameter = exo_ExoneracionActiva.HasValue ?
                new ObjectParameter("exo_ExoneracionActiva", exo_ExoneracionActiva) :
                new ObjectParameter("exo_ExoneracionActiva", typeof(bool));
    
            var exo_FechaInicialVigenciaParameter = exo_FechaInicialVigencia.HasValue ?
                new ObjectParameter("exo_FechaInicialVigencia", exo_FechaInicialVigencia) :
                new ObjectParameter("exo_FechaInicialVigencia", typeof(System.DateTime));
    
            var exo_FechaIFinalVigenciaParameter = exo_FechaIFinalVigencia.HasValue ?
                new ObjectParameter("exo_FechaIFinalVigencia", exo_FechaIFinalVigencia) :
                new ObjectParameter("exo_FechaIFinalVigencia", typeof(System.DateTime));
    
            var clte_IdParameter = clte_Id.HasValue ?
                new ObjectParameter("clte_Id", clte_Id) :
                new ObjectParameter("clte_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("crud_tbExoneracionInsert", exo_DocumentoParameter, exo_ExoneracionActivaParameter, exo_FechaInicialVigenciaParameter, exo_FechaIFinalVigenciaParameter, clte_IdParameter);
        }
    
        public virtual ObjectResult<string> crud_tbSucursalInsert(string mun_Codigo, Nullable<int> bod_Id, Nullable<int> pemi_Id, string suc_Descripcion, string suc_Correo, string suc_Direccion, string suc_Telefono, Nullable<int> suc_UsuarioModifica, Nullable<System.DateTime> suc_FechaModifica)
        {
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var pemi_IdParameter = pemi_Id.HasValue ?
                new ObjectParameter("pemi_Id", pemi_Id) :
                new ObjectParameter("pemi_Id", typeof(int));
    
            var suc_DescripcionParameter = suc_Descripcion != null ?
                new ObjectParameter("suc_Descripcion", suc_Descripcion) :
                new ObjectParameter("suc_Descripcion", typeof(string));
    
            var suc_CorreoParameter = suc_Correo != null ?
                new ObjectParameter("suc_Correo", suc_Correo) :
                new ObjectParameter("suc_Correo", typeof(string));
    
            var suc_DireccionParameter = suc_Direccion != null ?
                new ObjectParameter("suc_Direccion", suc_Direccion) :
                new ObjectParameter("suc_Direccion", typeof(string));
    
            var suc_TelefonoParameter = suc_Telefono != null ?
                new ObjectParameter("suc_Telefono", suc_Telefono) :
                new ObjectParameter("suc_Telefono", typeof(string));
    
            var suc_UsuarioModificaParameter = suc_UsuarioModifica.HasValue ?
                new ObjectParameter("suc_UsuarioModifica", suc_UsuarioModifica) :
                new ObjectParameter("suc_UsuarioModifica", typeof(int));
    
            var suc_FechaModificaParameter = suc_FechaModifica.HasValue ?
                new ObjectParameter("suc_FechaModifica", suc_FechaModifica) :
                new ObjectParameter("suc_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("crud_tbSucursalInsert", mun_CodigoParameter, bod_IdParameter, pemi_IdParameter, suc_DescripcionParameter, suc_CorreoParameter, suc_DireccionParameter, suc_TelefonoParameter, suc_UsuarioModificaParameter, suc_FechaModificaParameter);
        }
    
        public virtual int GetBusquedaClient(string identificacion, string nombre, string telefono)
        {
            var identificacionParameter = identificacion != null ?
                new ObjectParameter("identificacion", identificacion) :
                new ObjectParameter("identificacion", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            var telefonoParameter = telefono != null ?
                new ObjectParameter("telefono", telefono) :
                new ObjectParameter("telefono", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetBusquedaClient", identificacionParameter, nombreParameter, telefonoParameter);
        }
    
        public virtual int GetBusquedaFactura(string cliente, string fecha, string caja)
        {
            var clienteParameter = cliente != null ?
                new ObjectParameter("cliente", cliente) :
                new ObjectParameter("cliente", typeof(string));
    
            var fechaParameter = fecha != null ?
                new ObjectParameter("fecha", fecha) :
                new ObjectParameter("fecha", typeof(string));
    
            var cajaParameter = caja != null ?
                new ObjectParameter("Caja", caja) :
                new ObjectParameter("Caja", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetBusquedaFactura", clienteParameter, fechaParameter, cajaParameter);
        }
    
        public virtual ObjectResult<Listado_CAI_Result> Listado_CAI()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Listado_CAI_Result>("Listado_CAI");
        }
    
        public virtual int NewSelectCommand()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("NewSelectCommand");
        }
    
        public virtual ObjectResult<Prueba_Result> Prueba(Nullable<System.DateTime> pemid_FechaLimite, string dfisc_Descripcion, string pemi_NumeroCAI)
        {
            var pemid_FechaLimiteParameter = pemid_FechaLimite.HasValue ?
                new ObjectParameter("pemid_FechaLimite", pemid_FechaLimite) :
                new ObjectParameter("pemid_FechaLimite", typeof(System.DateTime));
    
            var dfisc_DescripcionParameter = dfisc_Descripcion != null ?
                new ObjectParameter("dfisc_Descripcion", dfisc_Descripcion) :
                new ObjectParameter("dfisc_Descripcion", typeof(string));
    
            var pemi_NumeroCAIParameter = pemi_NumeroCAI != null ?
                new ObjectParameter("pemi_NumeroCAI", pemi_NumeroCAI) :
                new ObjectParameter("pemi_NumeroCAI", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Prueba_Result>("Prueba", pemid_FechaLimiteParameter, dfisc_DescripcionParameter, pemi_NumeroCAIParameter);
        }
    
        public virtual ObjectResult<SDP_tbPedidoDetalle_Select_Result> SDP_tbPedidoDetalle_Select(Nullable<int> pedd_Id)
        {
            var pedd_IdParameter = pedd_Id.HasValue ?
                new ObjectParameter("pedd_Id", pedd_Id) :
                new ObjectParameter("pedd_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_tbPedidoDetalle_Select_Result>("SDP_tbPedidoDetalle_Select", pedd_IdParameter);
        }
    
        public virtual ObjectResult<SDP_Vent_tbCuponDescuentoSelectGet_Result> SDP_Vent_tbCuponDescuentoSelectGet(Nullable<int> cdto_ID)
        {
            var cdto_IDParameter = cdto_ID.HasValue ?
                new ObjectParameter("cdto_ID", cdto_ID) :
                new ObjectParameter("cdto_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Vent_tbCuponDescuentoSelectGet_Result>("SDP_Vent_tbCuponDescuentoSelectGet", cdto_IDParameter);
        }
    
        public virtual ObjectResult<SDP_Vent_tbPedidoDetalle_tbPedido_Select_Result> SDP_Vent_tbPedidoDetalle_tbPedido_Select(Nullable<int> ped_Id)
        {
            var ped_IdParameter = ped_Id.HasValue ?
                new ObjectParameter("ped_Id", ped_Id) :
                new ObjectParameter("ped_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SDP_Vent_tbPedidoDetalle_tbPedido_Select_Result>("SDP_Vent_tbPedidoDetalle_tbPedido_Select", ped_IdParameter);
        }
    
        public virtual ObjectResult<sp_GetDetallePedido_Result> sp_GetDetallePedido(Nullable<int> ped_Id)
        {
            var ped_IdParameter = ped_Id.HasValue ?
                new ObjectParameter("ped_Id", ped_Id) :
                new ObjectParameter("ped_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetDetallePedido_Result>("sp_GetDetallePedido", ped_IdParameter);
        }
    
        public virtual ObjectResult<spGetCaja_Result> spGetCaja(Nullable<int> codUsuario)
        {
            var codUsuarioParameter = codUsuario.HasValue ?
                new ObjectParameter("CodUsuario", codUsuario) :
                new ObjectParameter("CodUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetCaja_Result>("spGetCaja", codUsuarioParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> spGetDenominacion(Nullable<int> idDenominacion)
        {
            var idDenominacionParameter = idDenominacion.HasValue ?
                new ObjectParameter("IdDenominacion", idDenominacion) :
                new ObjectParameter("IdDenominacion", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("spGetDenominacion", idDenominacionParameter);
        }
    
        public virtual ObjectResult<spGetDepartamento_Result> spGetDepartamento(string codMunicipio)
        {
            var codMunicipioParameter = codMunicipio != null ?
                new ObjectParameter("CodMunicipio", codMunicipio) :
                new ObjectParameter("CodMunicipio", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetDepartamento_Result>("spGetDepartamento", codMunicipioParameter);
        }
    
        public virtual ObjectResult<spGetMunicipios_Result> spGetMunicipios(string codDepartamento)
        {
            var codDepartamentoParameter = codDepartamento != null ?
                new ObjectParameter("CodDepartamento", codDepartamento) :
                new ObjectParameter("CodDepartamento", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetMunicipios_Result>("spGetMunicipios", codDepartamentoParameter);
        }
    
        public virtual ObjectResult<spGetMunicipios1_Result> spGetMunicipios1(string codDepartamento)
        {
            var codDepartamentoParameter = codDepartamento != null ?
                new ObjectParameter("CodDepartamento", codDepartamento) :
                new ObjectParameter("CodDepartamento", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetMunicipios1_Result>("spGetMunicipios1", codDepartamentoParameter);
        }
    
        public virtual ObjectResult<spGetParametro_Result> spGetParametro()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetParametro_Result>("spGetParametro");
        }
    
        public virtual ObjectResult<string> spGetProducto1(string prod_Codigo)
        {
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("spGetProducto1", prod_CodigoParameter);
        }
    
        public virtual ObjectResult<spGetTipoIdentificacion_Result> spGetTipoIdentificacion(Nullable<bool> personaNatural)
        {
            var personaNaturalParameter = personaNatural.HasValue ?
                new ObjectParameter("PersonaNatural", personaNatural) :
                new ObjectParameter("PersonaNatural", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetTipoIdentificacion_Result>("spGetTipoIdentificacion", personaNaturalParameter);
        }
    
        public virtual ObjectResult<UDP_tbPedidoDetalle_Select_Result1> UDP_tbPedidoDetalle_Select(Nullable<int> pedd_Id)
        {
            var pedd_IdParameter = pedd_Id.HasValue ?
                new ObjectParameter("pedd_Id", pedd_Id) :
                new ObjectParameter("pedd_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_tbPedidoDetalle_Select_Result1>("UDP_tbPedidoDetalle_Select", pedd_IdParameter);
        }
    
        public virtual ObjectResult<UDP_tbPedidoDetalle_Select_Edit_Result1> UDP_tbPedidoDetalle_Select_Edit(Nullable<int> pedd_Id)
        {
            var pedd_IdParameter = pedd_Id.HasValue ?
                new ObjectParameter("pedd_Id", pedd_Id) :
                new ObjectParameter("pedd_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_tbPedidoDetalle_Select_Edit_Result1>("UDP_tbPedidoDetalle_Select_Edit", pedd_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_DatosConsumidorFinal_Insert_Result> UDP_Vent_DatosConsumidorFinal_Insert(Nullable<long> fact_Id, string confi_Nombres, string confi_Telefono, string confi_Correo)
        {
            var fact_IdParameter = fact_Id.HasValue ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(long));
    
            var confi_NombresParameter = confi_Nombres != null ?
                new ObjectParameter("confi_Nombres", confi_Nombres) :
                new ObjectParameter("confi_Nombres", typeof(string));
    
            var confi_TelefonoParameter = confi_Telefono != null ?
                new ObjectParameter("confi_Telefono", confi_Telefono) :
                new ObjectParameter("confi_Telefono", typeof(string));
    
            var confi_CorreoParameter = confi_Correo != null ?
                new ObjectParameter("confi_Correo", confi_Correo) :
                new ObjectParameter("confi_Correo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_DatosConsumidorFinal_Insert_Result>("UDP_Vent_DatosConsumidorFinal_Insert", fact_IdParameter, confi_NombresParameter, confi_TelefonoParameter, confi_CorreoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_DescuentosporFecha_Result> UDP_Vent_DescuentosporFecha()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_DescuentosporFecha_Result>("UDP_Vent_DescuentosporFecha");
        }
    
        public virtual ObjectResult<UDP_Vent_DescuentosporFecha_Reportes_Result> UDP_Vent_DescuentosporFecha_Reportes(string nombres, string tipo_Factura, Nullable<System.DateTime> fecha_inicio, Nullable<System.DateTime> fecha_final)
        {
            var nombresParameter = nombres != null ?
                new ObjectParameter("Nombres", nombres) :
                new ObjectParameter("Nombres", typeof(string));
    
            var tipo_FacturaParameter = tipo_Factura != null ?
                new ObjectParameter("Tipo_Factura", tipo_Factura) :
                new ObjectParameter("Tipo_Factura", typeof(string));
    
            var fecha_inicioParameter = fecha_inicio.HasValue ?
                new ObjectParameter("Fecha_inicio", fecha_inicio) :
                new ObjectParameter("Fecha_inicio", typeof(System.DateTime));
    
            var fecha_finalParameter = fecha_final.HasValue ?
                new ObjectParameter("Fecha_final", fecha_final) :
                new ObjectParameter("Fecha_final", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_DescuentosporFecha_Reportes_Result>("UDP_Vent_DescuentosporFecha_Reportes", nombresParameter, tipo_FacturaParameter, fecha_inicioParameter, fecha_finalParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_Efectivos_y_Pagos_Result> UDP_Vent_Efectivos_y_Pagos(Nullable<short> caja)
        {
            var cajaParameter = caja.HasValue ?
                new ObjectParameter("caja", caja) :
                new ObjectParameter("caja", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_Efectivos_y_Pagos_Result>("UDP_Vent_Efectivos_y_Pagos", cajaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_Factura_VentasporFecha_Reporte_Result> UDP_Vent_Factura_VentasporFecha_Reporte(string nombres, string tipo_Factura, Nullable<System.DateTime> fecha_inicio, Nullable<System.DateTime> fecha_final)
        {
            var nombresParameter = nombres != null ?
                new ObjectParameter("Nombres", nombres) :
                new ObjectParameter("Nombres", typeof(string));
    
            var tipo_FacturaParameter = tipo_Factura != null ?
                new ObjectParameter("Tipo_Factura", tipo_Factura) :
                new ObjectParameter("Tipo_Factura", typeof(string));
    
            var fecha_inicioParameter = fecha_inicio.HasValue ?
                new ObjectParameter("Fecha_inicio", fecha_inicio) :
                new ObjectParameter("Fecha_inicio", typeof(System.DateTime));
    
            var fecha_finalParameter = fecha_final.HasValue ?
                new ObjectParameter("Fecha_final", fecha_final) :
                new ObjectParameter("Fecha_final", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_Factura_VentasporFecha_Reporte_Result>("UDP_Vent_Factura_VentasporFecha_Reporte", nombresParameter, tipo_FacturaParameter, fecha_inicioParameter, fecha_finalParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_FacturasPagoSelect_Result> UDP_Vent_FacturasPagoSelect(Nullable<int> fact_Id)
        {
            var fact_IdParameter = fact_Id.HasValue ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_FacturasPagoSelect_Result>("UDP_Vent_FacturasPagoSelect", fact_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_FacturasPendientesDePago_Result> UDP_Vent_FacturasPendientesDePago(string clte_Identificacion, Nullable<System.DateTime> fact_Fecha, Nullable<System.DateTime> fact_Fecha2)
        {
            var clte_IdentificacionParameter = clte_Identificacion != null ?
                new ObjectParameter("clte_Identificacion", clte_Identificacion) :
                new ObjectParameter("clte_Identificacion", typeof(string));
    
            var fact_FechaParameter = fact_Fecha.HasValue ?
                new ObjectParameter("fact_Fecha", fact_Fecha) :
                new ObjectParameter("fact_Fecha", typeof(System.DateTime));
    
            var fact_Fecha2Parameter = fact_Fecha2.HasValue ?
                new ObjectParameter("fact_Fecha2", fact_Fecha2) :
                new ObjectParameter("fact_Fecha2", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_FacturasPendientesDePago_Result>("UDP_Vent_FacturasPendientesDePago", clte_IdentificacionParameter, fact_FechaParameter, fact_Fecha2Parameter);
        }
    
        public virtual ObjectResult<UDP_Vent_Pedido_EntreFechas_Result> UDP_Vent_Pedido_EntreFechas(Nullable<System.DateTime> fECHAELABORACION, Nullable<int> cLIENTE, Nullable<bool> tIPOCLIENTE)
        {
            var fECHAELABORACIONParameter = fECHAELABORACION.HasValue ?
                new ObjectParameter("FECHAELABORACION", fECHAELABORACION) :
                new ObjectParameter("FECHAELABORACION", typeof(System.DateTime));
    
            var cLIENTEParameter = cLIENTE.HasValue ?
                new ObjectParameter("CLIENTE", cLIENTE) :
                new ObjectParameter("CLIENTE", typeof(int));
    
            var tIPOCLIENTEParameter = tIPOCLIENTE.HasValue ?
                new ObjectParameter("TIPOCLIENTE", tIPOCLIENTE) :
                new ObjectParameter("TIPOCLIENTE", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_Pedido_EntreFechas_Result>("UDP_Vent_Pedido_EntreFechas", fECHAELABORACIONParameter, cLIENTEParameter, tIPOCLIENTEParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_ReporteConsumidorFinal_Result> UDP_Vent_ReporteConsumidorFinal(Nullable<System.DateTime> fact_Fecha)
        {
            var fact_FechaParameter = fact_Fecha.HasValue ?
                new ObjectParameter("fact_Fecha", fact_Fecha) :
                new ObjectParameter("fact_Fecha", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_ReporteConsumidorFinal_Result>("UDP_Vent_ReporteConsumidorFinal", fact_FechaParameter);
        }
    
        public virtual int UDP_Vent_SolicitudCredito_SolicitudesPorAprobarReporte(Nullable<System.DateTime> fECHADESDE, Nullable<System.DateTime> fECHAHASTA, Nullable<int> cLIENTE, Nullable<bool> tIPOCLIENTE)
        {
            var fECHADESDEParameter = fECHADESDE.HasValue ?
                new ObjectParameter("FECHADESDE", fECHADESDE) :
                new ObjectParameter("FECHADESDE", typeof(System.DateTime));
    
            var fECHAHASTAParameter = fECHAHASTA.HasValue ?
                new ObjectParameter("FECHAHASTA", fECHAHASTA) :
                new ObjectParameter("FECHAHASTA", typeof(System.DateTime));
    
            var cLIENTEParameter = cLIENTE.HasValue ?
                new ObjectParameter("CLIENTE", cLIENTE) :
                new ObjectParameter("CLIENTE", typeof(int));
    
            var tIPOCLIENTEParameter = tIPOCLIENTE.HasValue ?
                new ObjectParameter("TIPOCLIENTE", tIPOCLIENTE) :
                new ObjectParameter("TIPOCLIENTE", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UDP_Vent_SolicitudCredito_SolicitudesPorAprobarReporte", fECHADESDEParameter, fECHAHASTAParameter, cLIENTEParameter, tIPOCLIENTEParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbCaja_Insert_Result> UDP_Vent_tbCaja_Insert(string cja_Descripcion, Nullable<int> suc_Id, Nullable<int> cja_UsuarioCrea, Nullable<System.DateTime> cja_FechaCrea)
        {
            var cja_DescripcionParameter = cja_Descripcion != null ?
                new ObjectParameter("cja_Descripcion", cja_Descripcion) :
                new ObjectParameter("cja_Descripcion", typeof(string));
    
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var cja_UsuarioCreaParameter = cja_UsuarioCrea.HasValue ?
                new ObjectParameter("cja_UsuarioCrea", cja_UsuarioCrea) :
                new ObjectParameter("cja_UsuarioCrea", typeof(int));
    
            var cja_FechaCreaParameter = cja_FechaCrea.HasValue ?
                new ObjectParameter("cja_FechaCrea", cja_FechaCrea) :
                new ObjectParameter("cja_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbCaja_Insert_Result>("UDP_Vent_tbCaja_Insert", cja_DescripcionParameter, suc_IdParameter, cja_UsuarioCreaParameter, cja_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbCaja_Update_Result> UDP_Vent_tbCaja_Update(Nullable<short> cja_Id, string cja_Descripcion, Nullable<int> suc_Id, Nullable<int> cja_UsuarioCrea, Nullable<System.DateTime> cja_FechaCrea, Nullable<int> cja_UsuarioModifica, Nullable<System.DateTime> cja_FechaModifica)
        {
            var cja_IdParameter = cja_Id.HasValue ?
                new ObjectParameter("cja_Id", cja_Id) :
                new ObjectParameter("cja_Id", typeof(short));
    
            var cja_DescripcionParameter = cja_Descripcion != null ?
                new ObjectParameter("cja_Descripcion", cja_Descripcion) :
                new ObjectParameter("cja_Descripcion", typeof(string));
    
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var cja_UsuarioCreaParameter = cja_UsuarioCrea.HasValue ?
                new ObjectParameter("cja_UsuarioCrea", cja_UsuarioCrea) :
                new ObjectParameter("cja_UsuarioCrea", typeof(int));
    
            var cja_FechaCreaParameter = cja_FechaCrea.HasValue ?
                new ObjectParameter("cja_FechaCrea", cja_FechaCrea) :
                new ObjectParameter("cja_FechaCrea", typeof(System.DateTime));
    
            var cja_UsuarioModificaParameter = cja_UsuarioModifica.HasValue ?
                new ObjectParameter("cja_UsuarioModifica", cja_UsuarioModifica) :
                new ObjectParameter("cja_UsuarioModifica", typeof(int));
    
            var cja_FechaModificaParameter = cja_FechaModifica.HasValue ?
                new ObjectParameter("cja_FechaModifica", cja_FechaModifica) :
                new ObjectParameter("cja_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbCaja_Update_Result>("UDP_Vent_tbCaja_Update", cja_IdParameter, cja_DescripcionParameter, suc_IdParameter, cja_UsuarioCreaParameter, cja_FechaCreaParameter, cja_UsuarioModificaParameter, cja_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbCliente_Estado_Result> UDP_Vent_tbCliente_Estado(Nullable<int> clte_Id, Nullable<bool> clte_EsActivo, string clte_RazonInactivo)
        {
            var clte_IdParameter = clte_Id.HasValue ?
                new ObjectParameter("clte_Id", clte_Id) :
                new ObjectParameter("clte_Id", typeof(int));
    
            var clte_EsActivoParameter = clte_EsActivo.HasValue ?
                new ObjectParameter("clte_EsActivo", clte_EsActivo) :
                new ObjectParameter("clte_EsActivo", typeof(bool));
    
            var clte_RazonInactivoParameter = clte_RazonInactivo != null ?
                new ObjectParameter("clte_RazonInactivo", clte_RazonInactivo) :
                new ObjectParameter("clte_RazonInactivo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbCliente_Estado_Result>("UDP_Vent_tbCliente_Estado", clte_IdParameter, clte_EsActivoParameter, clte_RazonInactivoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbCliente_Insert_Result> UDP_Vent_tbCliente_Insert(Nullable<byte> tpi_Id, string clte_Identificacion, Nullable<bool> clte_EsPersonaNatural, string clte_Nombres, string clte_Apellidos, Nullable<System.DateTime> clte_FechaNacimiento, string clte_Nacionalidad, string clte_Sexo, string clte_Telefono, string clte_NombreComercial, string clte_RazonSocial, string clte_ContactoNombre, string clte_ContactoEmail, string clte_ContactoTelefono, Nullable<System.DateTime> clte_FechaConstitucion, string mun_Codigo, string clte_Direccion, string clte_CorreoElectronico, Nullable<bool> clte_EsActivo, string clte_RazonInactivo, Nullable<bool> clte_ConCredito, Nullable<bool> clte_EsMinorista, string clte_Observaciones, Nullable<decimal> clte_MontoCredito, Nullable<int> clte_DiasCredito, Nullable<bool> clte_Exonerado, Nullable<int> clte_UsuarioCrea, Nullable<System.DateTime> clte_FechaCrea)
        {
            var tpi_IdParameter = tpi_Id.HasValue ?
                new ObjectParameter("tpi_Id", tpi_Id) :
                new ObjectParameter("tpi_Id", typeof(byte));
    
            var clte_IdentificacionParameter = clte_Identificacion != null ?
                new ObjectParameter("clte_Identificacion", clte_Identificacion) :
                new ObjectParameter("clte_Identificacion", typeof(string));
    
            var clte_EsPersonaNaturalParameter = clte_EsPersonaNatural.HasValue ?
                new ObjectParameter("clte_EsPersonaNatural", clte_EsPersonaNatural) :
                new ObjectParameter("clte_EsPersonaNatural", typeof(bool));
    
            var clte_NombresParameter = clte_Nombres != null ?
                new ObjectParameter("clte_Nombres", clte_Nombres) :
                new ObjectParameter("clte_Nombres", typeof(string));
    
            var clte_ApellidosParameter = clte_Apellidos != null ?
                new ObjectParameter("clte_Apellidos", clte_Apellidos) :
                new ObjectParameter("clte_Apellidos", typeof(string));
    
            var clte_FechaNacimientoParameter = clte_FechaNacimiento.HasValue ?
                new ObjectParameter("clte_FechaNacimiento", clte_FechaNacimiento) :
                new ObjectParameter("clte_FechaNacimiento", typeof(System.DateTime));
    
            var clte_NacionalidadParameter = clte_Nacionalidad != null ?
                new ObjectParameter("clte_Nacionalidad", clte_Nacionalidad) :
                new ObjectParameter("clte_Nacionalidad", typeof(string));
    
            var clte_SexoParameter = clte_Sexo != null ?
                new ObjectParameter("clte_Sexo", clte_Sexo) :
                new ObjectParameter("clte_Sexo", typeof(string));
    
            var clte_TelefonoParameter = clte_Telefono != null ?
                new ObjectParameter("clte_Telefono", clte_Telefono) :
                new ObjectParameter("clte_Telefono", typeof(string));
    
            var clte_NombreComercialParameter = clte_NombreComercial != null ?
                new ObjectParameter("clte_NombreComercial", clte_NombreComercial) :
                new ObjectParameter("clte_NombreComercial", typeof(string));
    
            var clte_RazonSocialParameter = clte_RazonSocial != null ?
                new ObjectParameter("clte_RazonSocial", clte_RazonSocial) :
                new ObjectParameter("clte_RazonSocial", typeof(string));
    
            var clte_ContactoNombreParameter = clte_ContactoNombre != null ?
                new ObjectParameter("clte_ContactoNombre", clte_ContactoNombre) :
                new ObjectParameter("clte_ContactoNombre", typeof(string));
    
            var clte_ContactoEmailParameter = clte_ContactoEmail != null ?
                new ObjectParameter("clte_ContactoEmail", clte_ContactoEmail) :
                new ObjectParameter("clte_ContactoEmail", typeof(string));
    
            var clte_ContactoTelefonoParameter = clte_ContactoTelefono != null ?
                new ObjectParameter("clte_ContactoTelefono", clte_ContactoTelefono) :
                new ObjectParameter("clte_ContactoTelefono", typeof(string));
    
            var clte_FechaConstitucionParameter = clte_FechaConstitucion.HasValue ?
                new ObjectParameter("clte_FechaConstitucion", clte_FechaConstitucion) :
                new ObjectParameter("clte_FechaConstitucion", typeof(System.DateTime));
    
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            var clte_DireccionParameter = clte_Direccion != null ?
                new ObjectParameter("clte_Direccion", clte_Direccion) :
                new ObjectParameter("clte_Direccion", typeof(string));
    
            var clte_CorreoElectronicoParameter = clte_CorreoElectronico != null ?
                new ObjectParameter("clte_CorreoElectronico", clte_CorreoElectronico) :
                new ObjectParameter("clte_CorreoElectronico", typeof(string));
    
            var clte_EsActivoParameter = clte_EsActivo.HasValue ?
                new ObjectParameter("clte_EsActivo", clte_EsActivo) :
                new ObjectParameter("clte_EsActivo", typeof(bool));
    
            var clte_RazonInactivoParameter = clte_RazonInactivo != null ?
                new ObjectParameter("clte_RazonInactivo", clte_RazonInactivo) :
                new ObjectParameter("clte_RazonInactivo", typeof(string));
    
            var clte_ConCreditoParameter = clte_ConCredito.HasValue ?
                new ObjectParameter("clte_ConCredito", clte_ConCredito) :
                new ObjectParameter("clte_ConCredito", typeof(bool));
    
            var clte_EsMinoristaParameter = clte_EsMinorista.HasValue ?
                new ObjectParameter("clte_EsMinorista", clte_EsMinorista) :
                new ObjectParameter("clte_EsMinorista", typeof(bool));
    
            var clte_ObservacionesParameter = clte_Observaciones != null ?
                new ObjectParameter("clte_Observaciones", clte_Observaciones) :
                new ObjectParameter("clte_Observaciones", typeof(string));
    
            var clte_MontoCreditoParameter = clte_MontoCredito.HasValue ?
                new ObjectParameter("clte_MontoCredito", clte_MontoCredito) :
                new ObjectParameter("clte_MontoCredito", typeof(decimal));
    
            var clte_DiasCreditoParameter = clte_DiasCredito.HasValue ?
                new ObjectParameter("clte_DiasCredito", clte_DiasCredito) :
                new ObjectParameter("clte_DiasCredito", typeof(int));
    
            var clte_ExoneradoParameter = clte_Exonerado.HasValue ?
                new ObjectParameter("clte_Exonerado", clte_Exonerado) :
                new ObjectParameter("clte_Exonerado", typeof(bool));
    
            var clte_UsuarioCreaParameter = clte_UsuarioCrea.HasValue ?
                new ObjectParameter("clte_UsuarioCrea", clte_UsuarioCrea) :
                new ObjectParameter("clte_UsuarioCrea", typeof(int));
    
            var clte_FechaCreaParameter = clte_FechaCrea.HasValue ?
                new ObjectParameter("clte_FechaCrea", clte_FechaCrea) :
                new ObjectParameter("clte_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbCliente_Insert_Result>("UDP_Vent_tbCliente_Insert", tpi_IdParameter, clte_IdentificacionParameter, clte_EsPersonaNaturalParameter, clte_NombresParameter, clte_ApellidosParameter, clte_FechaNacimientoParameter, clte_NacionalidadParameter, clte_SexoParameter, clte_TelefonoParameter, clte_NombreComercialParameter, clte_RazonSocialParameter, clte_ContactoNombreParameter, clte_ContactoEmailParameter, clte_ContactoTelefonoParameter, clte_FechaConstitucionParameter, mun_CodigoParameter, clte_DireccionParameter, clte_CorreoElectronicoParameter, clte_EsActivoParameter, clte_RazonInactivoParameter, clte_ConCreditoParameter, clte_EsMinoristaParameter, clte_ObservacionesParameter, clte_MontoCreditoParameter, clte_DiasCreditoParameter, clte_ExoneradoParameter, clte_UsuarioCreaParameter, clte_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbCliente_Update_Result> UDP_Vent_tbCliente_Update(Nullable<int> clte_Id, Nullable<int> tpi_Id, string clte_Identificacion, Nullable<bool> clte_EsPersonaNatural, string clte_Nombres, string clte_Apellidos, Nullable<System.DateTime> clte_FechaNacimiento, string clte_Nacionalidad, string clte_Sexo, string clte_Telefono, string clte_NombreComercial, string clte_RazonSocial, string clte_ContactoNombre, string clte_ContactoEmail, string clte_ContactoTelefono, Nullable<System.DateTime> clte_FechaConstitucion, string mun_Codigo, string clte_Direccion, string clte_CorreoElectronico, Nullable<bool> clte_EsActivo, string clte_RazonInactivo, Nullable<bool> clte_ConCredito, Nullable<bool> clte_EsMinorista, string clte_Observaciones, Nullable<int> clte_UsuarioCrea, Nullable<System.DateTime> clte_FechaCrea, Nullable<decimal> clte_MontoCredito, Nullable<int> clte_DiasCredito, Nullable<bool> clte_Exonerado, Nullable<int> clte_UsuarioModifica, Nullable<System.DateTime> clte_FechaModifica)
        {
            var clte_IdParameter = clte_Id.HasValue ?
                new ObjectParameter("clte_Id", clte_Id) :
                new ObjectParameter("clte_Id", typeof(int));
    
            var tpi_IdParameter = tpi_Id.HasValue ?
                new ObjectParameter("tpi_Id", tpi_Id) :
                new ObjectParameter("tpi_Id", typeof(int));
    
            var clte_IdentificacionParameter = clte_Identificacion != null ?
                new ObjectParameter("clte_Identificacion", clte_Identificacion) :
                new ObjectParameter("clte_Identificacion", typeof(string));
    
            var clte_EsPersonaNaturalParameter = clte_EsPersonaNatural.HasValue ?
                new ObjectParameter("clte_EsPersonaNatural", clte_EsPersonaNatural) :
                new ObjectParameter("clte_EsPersonaNatural", typeof(bool));
    
            var clte_NombresParameter = clte_Nombres != null ?
                new ObjectParameter("clte_Nombres", clte_Nombres) :
                new ObjectParameter("clte_Nombres", typeof(string));
    
            var clte_ApellidosParameter = clte_Apellidos != null ?
                new ObjectParameter("clte_Apellidos", clte_Apellidos) :
                new ObjectParameter("clte_Apellidos", typeof(string));
    
            var clte_FechaNacimientoParameter = clte_FechaNacimiento.HasValue ?
                new ObjectParameter("clte_FechaNacimiento", clte_FechaNacimiento) :
                new ObjectParameter("clte_FechaNacimiento", typeof(System.DateTime));
    
            var clte_NacionalidadParameter = clte_Nacionalidad != null ?
                new ObjectParameter("clte_Nacionalidad", clte_Nacionalidad) :
                new ObjectParameter("clte_Nacionalidad", typeof(string));
    
            var clte_SexoParameter = clte_Sexo != null ?
                new ObjectParameter("clte_Sexo", clte_Sexo) :
                new ObjectParameter("clte_Sexo", typeof(string));
    
            var clte_TelefonoParameter = clte_Telefono != null ?
                new ObjectParameter("clte_Telefono", clte_Telefono) :
                new ObjectParameter("clte_Telefono", typeof(string));
    
            var clte_NombreComercialParameter = clte_NombreComercial != null ?
                new ObjectParameter("clte_NombreComercial", clte_NombreComercial) :
                new ObjectParameter("clte_NombreComercial", typeof(string));
    
            var clte_RazonSocialParameter = clte_RazonSocial != null ?
                new ObjectParameter("clte_RazonSocial", clte_RazonSocial) :
                new ObjectParameter("clte_RazonSocial", typeof(string));
    
            var clte_ContactoNombreParameter = clte_ContactoNombre != null ?
                new ObjectParameter("clte_ContactoNombre", clte_ContactoNombre) :
                new ObjectParameter("clte_ContactoNombre", typeof(string));
    
            var clte_ContactoEmailParameter = clte_ContactoEmail != null ?
                new ObjectParameter("clte_ContactoEmail", clte_ContactoEmail) :
                new ObjectParameter("clte_ContactoEmail", typeof(string));
    
            var clte_ContactoTelefonoParameter = clte_ContactoTelefono != null ?
                new ObjectParameter("clte_ContactoTelefono", clte_ContactoTelefono) :
                new ObjectParameter("clte_ContactoTelefono", typeof(string));
    
            var clte_FechaConstitucionParameter = clte_FechaConstitucion.HasValue ?
                new ObjectParameter("clte_FechaConstitucion", clte_FechaConstitucion) :
                new ObjectParameter("clte_FechaConstitucion", typeof(System.DateTime));
    
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            var clte_DireccionParameter = clte_Direccion != null ?
                new ObjectParameter("clte_Direccion", clte_Direccion) :
                new ObjectParameter("clte_Direccion", typeof(string));
    
            var clte_CorreoElectronicoParameter = clte_CorreoElectronico != null ?
                new ObjectParameter("clte_CorreoElectronico", clte_CorreoElectronico) :
                new ObjectParameter("clte_CorreoElectronico", typeof(string));
    
            var clte_EsActivoParameter = clte_EsActivo.HasValue ?
                new ObjectParameter("clte_EsActivo", clte_EsActivo) :
                new ObjectParameter("clte_EsActivo", typeof(bool));
    
            var clte_RazonInactivoParameter = clte_RazonInactivo != null ?
                new ObjectParameter("clte_RazonInactivo", clte_RazonInactivo) :
                new ObjectParameter("clte_RazonInactivo", typeof(string));
    
            var clte_ConCreditoParameter = clte_ConCredito.HasValue ?
                new ObjectParameter("clte_ConCredito", clte_ConCredito) :
                new ObjectParameter("clte_ConCredito", typeof(bool));
    
            var clte_EsMinoristaParameter = clte_EsMinorista.HasValue ?
                new ObjectParameter("clte_EsMinorista", clte_EsMinorista) :
                new ObjectParameter("clte_EsMinorista", typeof(bool));
    
            var clte_ObservacionesParameter = clte_Observaciones != null ?
                new ObjectParameter("clte_Observaciones", clte_Observaciones) :
                new ObjectParameter("clte_Observaciones", typeof(string));
    
            var clte_UsuarioCreaParameter = clte_UsuarioCrea.HasValue ?
                new ObjectParameter("clte_UsuarioCrea", clte_UsuarioCrea) :
                new ObjectParameter("clte_UsuarioCrea", typeof(int));
    
            var clte_FechaCreaParameter = clte_FechaCrea.HasValue ?
                new ObjectParameter("clte_FechaCrea", clte_FechaCrea) :
                new ObjectParameter("clte_FechaCrea", typeof(System.DateTime));
    
            var clte_MontoCreditoParameter = clte_MontoCredito.HasValue ?
                new ObjectParameter("clte_MontoCredito", clte_MontoCredito) :
                new ObjectParameter("clte_MontoCredito", typeof(decimal));
    
            var clte_DiasCreditoParameter = clte_DiasCredito.HasValue ?
                new ObjectParameter("clte_DiasCredito", clte_DiasCredito) :
                new ObjectParameter("clte_DiasCredito", typeof(int));
    
            var clte_ExoneradoParameter = clte_Exonerado.HasValue ?
                new ObjectParameter("clte_Exonerado", clte_Exonerado) :
                new ObjectParameter("clte_Exonerado", typeof(bool));
    
            var clte_UsuarioModificaParameter = clte_UsuarioModifica.HasValue ?
                new ObjectParameter("clte_UsuarioModifica", clte_UsuarioModifica) :
                new ObjectParameter("clte_UsuarioModifica", typeof(int));
    
            var clte_FechaModificaParameter = clte_FechaModifica.HasValue ?
                new ObjectParameter("clte_FechaModifica", clte_FechaModifica) :
                new ObjectParameter("clte_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbCliente_Update_Result>("UDP_Vent_tbCliente_Update", clte_IdParameter, tpi_IdParameter, clte_IdentificacionParameter, clte_EsPersonaNaturalParameter, clte_NombresParameter, clte_ApellidosParameter, clte_FechaNacimientoParameter, clte_NacionalidadParameter, clte_SexoParameter, clte_TelefonoParameter, clte_NombreComercialParameter, clte_RazonSocialParameter, clte_ContactoNombreParameter, clte_ContactoEmailParameter, clte_ContactoTelefonoParameter, clte_FechaConstitucionParameter, mun_CodigoParameter, clte_DireccionParameter, clte_CorreoElectronicoParameter, clte_EsActivoParameter, clte_RazonInactivoParameter, clte_ConCreditoParameter, clte_EsMinoristaParameter, clte_ObservacionesParameter, clte_UsuarioCreaParameter, clte_FechaCreaParameter, clte_MontoCreditoParameter, clte_DiasCreditoParameter, clte_ExoneradoParameter, clte_UsuarioModificaParameter, clte_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbCuponDescuento_Anulado_Result> UDP_Vent_tbCuponDescuento_Anulado(Nullable<int> cdto_ID, Nullable<bool> cdto_Anulado, string cdto_RazonAnulado)
        {
            var cdto_IDParameter = cdto_ID.HasValue ?
                new ObjectParameter("cdto_ID", cdto_ID) :
                new ObjectParameter("cdto_ID", typeof(int));
    
            var cdto_AnuladoParameter = cdto_Anulado.HasValue ?
                new ObjectParameter("cdto_Anulado", cdto_Anulado) :
                new ObjectParameter("cdto_Anulado", typeof(bool));
    
            var cdto_RazonAnuladoParameter = cdto_RazonAnulado != null ?
                new ObjectParameter("cdto_RazonAnulado", cdto_RazonAnulado) :
                new ObjectParameter("cdto_RazonAnulado", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbCuponDescuento_Anulado_Result>("UDP_Vent_tbCuponDescuento_Anulado", cdto_IDParameter, cdto_AnuladoParameter, cdto_RazonAnuladoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbCuponDescuento_EsImpreso_Result> UDP_Vent_tbCuponDescuento_EsImpreso(Nullable<int> cdto_ID, Nullable<bool> cdto_EsImpreso)
        {
            var cdto_IDParameter = cdto_ID.HasValue ?
                new ObjectParameter("cdto_ID", cdto_ID) :
                new ObjectParameter("cdto_ID", typeof(int));
    
            var cdto_EsImpresoParameter = cdto_EsImpreso.HasValue ?
                new ObjectParameter("cdto_EsImpreso", cdto_EsImpreso) :
                new ObjectParameter("cdto_EsImpreso", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbCuponDescuento_EsImpreso_Result>("UDP_Vent_tbCuponDescuento_EsImpreso", cdto_IDParameter, cdto_EsImpresoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbCuponDescuento_Imprimir_Result> UDP_Vent_tbCuponDescuento_Imprimir(Nullable<int> cdto_ID)
        {
            var cdto_IDParameter = cdto_ID.HasValue ?
                new ObjectParameter("cdto_ID", cdto_ID) :
                new ObjectParameter("cdto_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbCuponDescuento_Imprimir_Result>("UDP_Vent_tbCuponDescuento_Imprimir", cdto_IDParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbCuponDescuento_Insert_Result> UDP_Vent_tbCuponDescuento_Insert(Nullable<int> suc_Id, Nullable<System.DateTime> cdto_FechaEmision, Nullable<System.DateTime> cdto_FechaVencimiento, Nullable<decimal> cdto_PorcentajeDescuento, Nullable<decimal> cdto_MontoDescuento, Nullable<decimal> cdto_MaximoMontoDescuento, Nullable<decimal> cdto_CantidadCompraMinima, Nullable<bool> cdto_Redimido, Nullable<System.DateTime> cdto_FechaRedencion, Nullable<bool> cdto_Anulado, string cdto_RazonAnulado, Nullable<bool> cdto_EsImpreso, Nullable<int> cdto_UsuarioCrea, Nullable<System.DateTime> cdto_FechaCrea)
        {
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var cdto_FechaEmisionParameter = cdto_FechaEmision.HasValue ?
                new ObjectParameter("cdto_FechaEmision", cdto_FechaEmision) :
                new ObjectParameter("cdto_FechaEmision", typeof(System.DateTime));
    
            var cdto_FechaVencimientoParameter = cdto_FechaVencimiento.HasValue ?
                new ObjectParameter("cdto_FechaVencimiento", cdto_FechaVencimiento) :
                new ObjectParameter("cdto_FechaVencimiento", typeof(System.DateTime));
    
            var cdto_PorcentajeDescuentoParameter = cdto_PorcentajeDescuento.HasValue ?
                new ObjectParameter("cdto_PorcentajeDescuento", cdto_PorcentajeDescuento) :
                new ObjectParameter("cdto_PorcentajeDescuento", typeof(decimal));
    
            var cdto_MontoDescuentoParameter = cdto_MontoDescuento.HasValue ?
                new ObjectParameter("cdto_MontoDescuento", cdto_MontoDescuento) :
                new ObjectParameter("cdto_MontoDescuento", typeof(decimal));
    
            var cdto_MaximoMontoDescuentoParameter = cdto_MaximoMontoDescuento.HasValue ?
                new ObjectParameter("cdto_MaximoMontoDescuento", cdto_MaximoMontoDescuento) :
                new ObjectParameter("cdto_MaximoMontoDescuento", typeof(decimal));
    
            var cdto_CantidadCompraMinimaParameter = cdto_CantidadCompraMinima.HasValue ?
                new ObjectParameter("cdto_CantidadCompraMinima", cdto_CantidadCompraMinima) :
                new ObjectParameter("cdto_CantidadCompraMinima", typeof(decimal));
    
            var cdto_RedimidoParameter = cdto_Redimido.HasValue ?
                new ObjectParameter("cdto_Redimido", cdto_Redimido) :
                new ObjectParameter("cdto_Redimido", typeof(bool));
    
            var cdto_FechaRedencionParameter = cdto_FechaRedencion.HasValue ?
                new ObjectParameter("cdto_FechaRedencion", cdto_FechaRedencion) :
                new ObjectParameter("cdto_FechaRedencion", typeof(System.DateTime));
    
            var cdto_AnuladoParameter = cdto_Anulado.HasValue ?
                new ObjectParameter("cdto_Anulado", cdto_Anulado) :
                new ObjectParameter("cdto_Anulado", typeof(bool));
    
            var cdto_RazonAnuladoParameter = cdto_RazonAnulado != null ?
                new ObjectParameter("cdto_RazonAnulado", cdto_RazonAnulado) :
                new ObjectParameter("cdto_RazonAnulado", typeof(string));
    
            var cdto_EsImpresoParameter = cdto_EsImpreso.HasValue ?
                new ObjectParameter("cdto_EsImpreso", cdto_EsImpreso) :
                new ObjectParameter("cdto_EsImpreso", typeof(bool));
    
            var cdto_UsuarioCreaParameter = cdto_UsuarioCrea.HasValue ?
                new ObjectParameter("cdto_UsuarioCrea", cdto_UsuarioCrea) :
                new ObjectParameter("cdto_UsuarioCrea", typeof(int));
    
            var cdto_FechaCreaParameter = cdto_FechaCrea.HasValue ?
                new ObjectParameter("cdto_FechaCrea", cdto_FechaCrea) :
                new ObjectParameter("cdto_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbCuponDescuento_Insert_Result>("UDP_Vent_tbCuponDescuento_Insert", suc_IdParameter, cdto_FechaEmisionParameter, cdto_FechaVencimientoParameter, cdto_PorcentajeDescuentoParameter, cdto_MontoDescuentoParameter, cdto_MaximoMontoDescuentoParameter, cdto_CantidadCompraMinimaParameter, cdto_RedimidoParameter, cdto_FechaRedencionParameter, cdto_AnuladoParameter, cdto_RazonAnuladoParameter, cdto_EsImpresoParameter, cdto_UsuarioCreaParameter, cdto_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbCuponDescuento_Redimido_Result> UDP_Vent_tbCuponDescuento_Redimido(Nullable<int> cdto_ID, Nullable<bool> cdto_Redimido)
        {
            var cdto_IDParameter = cdto_ID.HasValue ?
                new ObjectParameter("cdto_ID", cdto_ID) :
                new ObjectParameter("cdto_ID", typeof(int));
    
            var cdto_RedimidoParameter = cdto_Redimido.HasValue ?
                new ObjectParameter("cdto_Redimido", cdto_Redimido) :
                new ObjectParameter("cdto_Redimido", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbCuponDescuento_Redimido_Result>("UDP_Vent_tbCuponDescuento_Redimido", cdto_IDParameter, cdto_RedimidoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbCuponDescuento_Reporte_Result> UDP_Vent_tbCuponDescuento_Reporte(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFinal)
        {
            var fechaInicioParameter = fechaInicio.HasValue ?
                new ObjectParameter("FechaInicio", fechaInicio) :
                new ObjectParameter("FechaInicio", typeof(System.DateTime));
    
            var fechaFinalParameter = fechaFinal.HasValue ?
                new ObjectParameter("FechaFinal", fechaFinal) :
                new ObjectParameter("FechaFinal", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbCuponDescuento_Reporte_Result>("UDP_Vent_tbCuponDescuento_Reporte", fechaInicioParameter, fechaFinalParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbCuponDescuento_Update_Result> UDP_Vent_tbCuponDescuento_Update(Nullable<int> cdto_ID, Nullable<int> suc_Id, Nullable<System.DateTime> cdto_FechaEmision, Nullable<System.DateTime> cdto_FechaVencimiento, Nullable<decimal> cdto_PorcentajeDescuento, Nullable<decimal> cdto_MontoDescuento, Nullable<decimal> cdto_MaximoMontoDescuento, Nullable<decimal> cdto_CantidadCompraMinima, Nullable<bool> cdto_Redimido, Nullable<System.DateTime> cdto_FechaRedencion, Nullable<bool> cdto_Anulado, Nullable<bool> cdto_EsImpreso, Nullable<int> cdto_UsuarioCrea, Nullable<System.DateTime> cdto_FechaCrea, Nullable<int> cdto_UsuarioModifica, Nullable<System.DateTime> cdto_FechaModifica)
        {
            var cdto_IDParameter = cdto_ID.HasValue ?
                new ObjectParameter("cdto_ID", cdto_ID) :
                new ObjectParameter("cdto_ID", typeof(int));
    
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var cdto_FechaEmisionParameter = cdto_FechaEmision.HasValue ?
                new ObjectParameter("cdto_FechaEmision", cdto_FechaEmision) :
                new ObjectParameter("cdto_FechaEmision", typeof(System.DateTime));
    
            var cdto_FechaVencimientoParameter = cdto_FechaVencimiento.HasValue ?
                new ObjectParameter("cdto_FechaVencimiento", cdto_FechaVencimiento) :
                new ObjectParameter("cdto_FechaVencimiento", typeof(System.DateTime));
    
            var cdto_PorcentajeDescuentoParameter = cdto_PorcentajeDescuento.HasValue ?
                new ObjectParameter("cdto_PorcentajeDescuento", cdto_PorcentajeDescuento) :
                new ObjectParameter("cdto_PorcentajeDescuento", typeof(decimal));
    
            var cdto_MontoDescuentoParameter = cdto_MontoDescuento.HasValue ?
                new ObjectParameter("cdto_MontoDescuento", cdto_MontoDescuento) :
                new ObjectParameter("cdto_MontoDescuento", typeof(decimal));
    
            var cdto_MaximoMontoDescuentoParameter = cdto_MaximoMontoDescuento.HasValue ?
                new ObjectParameter("cdto_MaximoMontoDescuento", cdto_MaximoMontoDescuento) :
                new ObjectParameter("cdto_MaximoMontoDescuento", typeof(decimal));
    
            var cdto_CantidadCompraMinimaParameter = cdto_CantidadCompraMinima.HasValue ?
                new ObjectParameter("cdto_CantidadCompraMinima", cdto_CantidadCompraMinima) :
                new ObjectParameter("cdto_CantidadCompraMinima", typeof(decimal));
    
            var cdto_RedimidoParameter = cdto_Redimido.HasValue ?
                new ObjectParameter("cdto_Redimido", cdto_Redimido) :
                new ObjectParameter("cdto_Redimido", typeof(bool));
    
            var cdto_FechaRedencionParameter = cdto_FechaRedencion.HasValue ?
                new ObjectParameter("cdto_FechaRedencion", cdto_FechaRedencion) :
                new ObjectParameter("cdto_FechaRedencion", typeof(System.DateTime));
    
            var cdto_AnuladoParameter = cdto_Anulado.HasValue ?
                new ObjectParameter("cdto_Anulado", cdto_Anulado) :
                new ObjectParameter("cdto_Anulado", typeof(bool));
    
            var cdto_EsImpresoParameter = cdto_EsImpreso.HasValue ?
                new ObjectParameter("cdto_EsImpreso", cdto_EsImpreso) :
                new ObjectParameter("cdto_EsImpreso", typeof(bool));
    
            var cdto_UsuarioCreaParameter = cdto_UsuarioCrea.HasValue ?
                new ObjectParameter("cdto_UsuarioCrea", cdto_UsuarioCrea) :
                new ObjectParameter("cdto_UsuarioCrea", typeof(int));
    
            var cdto_FechaCreaParameter = cdto_FechaCrea.HasValue ?
                new ObjectParameter("cdto_FechaCrea", cdto_FechaCrea) :
                new ObjectParameter("cdto_FechaCrea", typeof(System.DateTime));
    
            var cdto_UsuarioModificaParameter = cdto_UsuarioModifica.HasValue ?
                new ObjectParameter("cdto_UsuarioModifica", cdto_UsuarioModifica) :
                new ObjectParameter("cdto_UsuarioModifica", typeof(int));
    
            var cdto_FechaModificaParameter = cdto_FechaModifica.HasValue ?
                new ObjectParameter("cdto_FechaModifica", cdto_FechaModifica) :
                new ObjectParameter("cdto_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbCuponDescuento_Update_Result>("UDP_Vent_tbCuponDescuento_Update", cdto_IDParameter, suc_IdParameter, cdto_FechaEmisionParameter, cdto_FechaVencimientoParameter, cdto_PorcentajeDescuentoParameter, cdto_MontoDescuentoParameter, cdto_MaximoMontoDescuentoParameter, cdto_CantidadCompraMinimaParameter, cdto_RedimidoParameter, cdto_FechaRedencionParameter, cdto_AnuladoParameter, cdto_EsImpresoParameter, cdto_UsuarioCreaParameter, cdto_FechaCreaParameter, cdto_UsuarioModificaParameter, cdto_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbCuponDescuentoSelect_Result> UDP_Vent_tbCuponDescuentoSelect()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbCuponDescuentoSelect_Result>("UDP_Vent_tbCuponDescuentoSelect");
        }
    
        public virtual ObjectResult<UDP_Vent_tbCuponDescuentoSelectGet_Result> UDP_Vent_tbCuponDescuentoSelectGet(Nullable<int> cdto_ID)
        {
            var cdto_IDParameter = cdto_ID.HasValue ?
                new ObjectParameter("cdto_ID", cdto_ID) :
                new ObjectParameter("cdto_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbCuponDescuentoSelectGet_Result>("UDP_Vent_tbCuponDescuentoSelectGet", cdto_IDParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbDenominacionArqueo_Edit_Result> UDP_Vent_tbDenominacionArqueo_Edit(Nullable<int> arqueoID)
        {
            var arqueoIDParameter = arqueoID.HasValue ?
                new ObjectParameter("ArqueoID", arqueoID) :
                new ObjectParameter("ArqueoID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbDenominacionArqueo_Edit_Result>("UDP_Vent_tbDenominacionArqueo_Edit", arqueoIDParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbDenominacionArqueo_Insert_Result> UDP_Vent_tbDenominacionArqueo_Insert(Nullable<int> mocja_Id, Nullable<short> deno_Id, Nullable<short> arqde_CantidadDenominacion, Nullable<decimal> arqde_MontoDenominacion, Nullable<int> arqde_UsuarioCrea, Nullable<System.DateTime> arqde_FechaCrea)
        {
            var mocja_IdParameter = mocja_Id.HasValue ?
                new ObjectParameter("mocja_Id", mocja_Id) :
                new ObjectParameter("mocja_Id", typeof(int));
    
            var deno_IdParameter = deno_Id.HasValue ?
                new ObjectParameter("deno_Id", deno_Id) :
                new ObjectParameter("deno_Id", typeof(short));
    
            var arqde_CantidadDenominacionParameter = arqde_CantidadDenominacion.HasValue ?
                new ObjectParameter("arqde_CantidadDenominacion", arqde_CantidadDenominacion) :
                new ObjectParameter("arqde_CantidadDenominacion", typeof(short));
    
            var arqde_MontoDenominacionParameter = arqde_MontoDenominacion.HasValue ?
                new ObjectParameter("arqde_MontoDenominacion", arqde_MontoDenominacion) :
                new ObjectParameter("arqde_MontoDenominacion", typeof(decimal));
    
            var arqde_UsuarioCreaParameter = arqde_UsuarioCrea.HasValue ?
                new ObjectParameter("arqde_UsuarioCrea", arqde_UsuarioCrea) :
                new ObjectParameter("arqde_UsuarioCrea", typeof(int));
    
            var arqde_FechaCreaParameter = arqde_FechaCrea.HasValue ?
                new ObjectParameter("arqde_FechaCrea", arqde_FechaCrea) :
                new ObjectParameter("arqde_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbDenominacionArqueo_Insert_Result>("UDP_Vent_tbDenominacionArqueo_Insert", mocja_IdParameter, deno_IdParameter, arqde_CantidadDenominacionParameter, arqde_MontoDenominacionParameter, arqde_UsuarioCreaParameter, arqde_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbDenominacionArqueo_Select_Result> UDP_Vent_tbDenominacionArqueo_Select()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbDenominacionArqueo_Select_Result>("UDP_Vent_tbDenominacionArqueo_Select");
        }
    
        public virtual ObjectResult<UDP_Vent_tbDenominacionArqueo_Update_Result> UDP_Vent_tbDenominacionArqueo_Update(Nullable<int> arqde_Id, Nullable<int> mocja_Id, Nullable<short> deno_Id, Nullable<short> arqde_CantidadDenominacion, Nullable<decimal> arqde_MontoDenominacion, Nullable<int> arqde_UsuarioCrea, Nullable<System.DateTime> arqde_FechaCrea, Nullable<int> arqde_UsuarioModifica, Nullable<System.DateTime> arqde_FechaModifica)
        {
            var arqde_IdParameter = arqde_Id.HasValue ?
                new ObjectParameter("arqde_Id", arqde_Id) :
                new ObjectParameter("arqde_Id", typeof(int));
    
            var mocja_IdParameter = mocja_Id.HasValue ?
                new ObjectParameter("mocja_Id", mocja_Id) :
                new ObjectParameter("mocja_Id", typeof(int));
    
            var deno_IdParameter = deno_Id.HasValue ?
                new ObjectParameter("deno_Id", deno_Id) :
                new ObjectParameter("deno_Id", typeof(short));
    
            var arqde_CantidadDenominacionParameter = arqde_CantidadDenominacion.HasValue ?
                new ObjectParameter("arqde_CantidadDenominacion", arqde_CantidadDenominacion) :
                new ObjectParameter("arqde_CantidadDenominacion", typeof(short));
    
            var arqde_MontoDenominacionParameter = arqde_MontoDenominacion.HasValue ?
                new ObjectParameter("arqde_MontoDenominacion", arqde_MontoDenominacion) :
                new ObjectParameter("arqde_MontoDenominacion", typeof(decimal));
    
            var arqde_UsuarioCreaParameter = arqde_UsuarioCrea.HasValue ?
                new ObjectParameter("arqde_UsuarioCrea", arqde_UsuarioCrea) :
                new ObjectParameter("arqde_UsuarioCrea", typeof(int));
    
            var arqde_FechaCreaParameter = arqde_FechaCrea.HasValue ?
                new ObjectParameter("arqde_FechaCrea", arqde_FechaCrea) :
                new ObjectParameter("arqde_FechaCrea", typeof(System.DateTime));
    
            var arqde_UsuarioModificaParameter = arqde_UsuarioModifica.HasValue ?
                new ObjectParameter("arqde_UsuarioModifica", arqde_UsuarioModifica) :
                new ObjectParameter("arqde_UsuarioModifica", typeof(int));
    
            var arqde_FechaModificaParameter = arqde_FechaModifica.HasValue ?
                new ObjectParameter("arqde_FechaModifica", arqde_FechaModifica) :
                new ObjectParameter("arqde_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbDenominacionArqueo_Update_Result>("UDP_Vent_tbDenominacionArqueo_Update", arqde_IdParameter, mocja_IdParameter, deno_IdParameter, arqde_CantidadDenominacionParameter, arqde_MontoDenominacionParameter, arqde_UsuarioCreaParameter, arqde_FechaCreaParameter, arqde_UsuarioModificaParameter, arqde_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbDevolucion_Estado_Result> UDP_Vent_tbDevolucion_Estado(Nullable<int> dev_Id, Nullable<bool> dev_Estado)
        {
            var dev_IdParameter = dev_Id.HasValue ?
                new ObjectParameter("dev_Id", dev_Id) :
                new ObjectParameter("dev_Id", typeof(int));
    
            var dev_EstadoParameter = dev_Estado.HasValue ?
                new ObjectParameter("dev_Estado", dev_Estado) :
                new ObjectParameter("dev_Estado", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbDevolucion_Estado_Result>("UDP_Vent_tbDevolucion_Estado", dev_IdParameter, dev_EstadoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbDevolucion_GetDetalle_Result> UDP_Vent_tbDevolucion_GetDetalle(Nullable<long> dEVOLUCION_ID)
        {
            var dEVOLUCION_IDParameter = dEVOLUCION_ID.HasValue ?
                new ObjectParameter("DEVOLUCION_ID", dEVOLUCION_ID) :
                new ObjectParameter("DEVOLUCION_ID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbDevolucion_GetDetalle_Result>("UDP_Vent_tbDevolucion_GetDetalle", dEVOLUCION_IDParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbDevolucion_GetDetalle_Editar_Result> UDP_Vent_tbDevolucion_GetDetalle_Editar(Nullable<long> dEVOLUCIONDD_ID)
        {
            var dEVOLUCIONDD_IDParameter = dEVOLUCIONDD_ID.HasValue ?
                new ObjectParameter("DEVOLUCIONDD_ID", dEVOLUCIONDD_ID) :
                new ObjectParameter("DEVOLUCIONDD_ID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbDevolucion_GetDetalle_Editar_Result>("UDP_Vent_tbDevolucion_GetDetalle_Editar", dEVOLUCIONDD_IDParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbDevolucion_Insert_Result> UDP_Vent_tbDevolucion_Insert(Nullable<long> fact_Id, Nullable<short> cja_Id, Nullable<System.DateTime> dev_Fecha, Nullable<bool> dev_Estado, Nullable<int> dev_UsuarioCrea, Nullable<System.DateTime> dev_FechaCrea)
        {
            var fact_IdParameter = fact_Id.HasValue ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(long));
    
            var cja_IdParameter = cja_Id.HasValue ?
                new ObjectParameter("cja_Id", cja_Id) :
                new ObjectParameter("cja_Id", typeof(short));
    
            var dev_FechaParameter = dev_Fecha.HasValue ?
                new ObjectParameter("dev_Fecha", dev_Fecha) :
                new ObjectParameter("dev_Fecha", typeof(System.DateTime));
    
            var dev_EstadoParameter = dev_Estado.HasValue ?
                new ObjectParameter("dev_Estado", dev_Estado) :
                new ObjectParameter("dev_Estado", typeof(bool));
    
            var dev_UsuarioCreaParameter = dev_UsuarioCrea.HasValue ?
                new ObjectParameter("dev_UsuarioCrea", dev_UsuarioCrea) :
                new ObjectParameter("dev_UsuarioCrea", typeof(int));
    
            var dev_FechaCreaParameter = dev_FechaCrea.HasValue ?
                new ObjectParameter("dev_FechaCrea", dev_FechaCrea) :
                new ObjectParameter("dev_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbDevolucion_Insert_Result>("UDP_Vent_tbDevolucion_Insert", fact_IdParameter, cja_IdParameter, dev_FechaParameter, dev_EstadoParameter, dev_UsuarioCreaParameter, dev_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbDevolucion_Update_Result> UDP_Vent_tbDevolucion_Update(Nullable<int> dev_Id, Nullable<long> fact_Id, Nullable<short> cja_Id, Nullable<System.DateTime> dev_Fecha, Nullable<bool> dev_Estado, Nullable<int> dev_UsuarioCrea, Nullable<System.DateTime> dev_FechaCrea, Nullable<int> dev_UsuarioModifica, Nullable<System.DateTime> dev_FechaModifica)
        {
            var dev_IdParameter = dev_Id.HasValue ?
                new ObjectParameter("dev_Id", dev_Id) :
                new ObjectParameter("dev_Id", typeof(int));
    
            var fact_IdParameter = fact_Id.HasValue ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(long));
    
            var cja_IdParameter = cja_Id.HasValue ?
                new ObjectParameter("cja_Id", cja_Id) :
                new ObjectParameter("cja_Id", typeof(short));
    
            var dev_FechaParameter = dev_Fecha.HasValue ?
                new ObjectParameter("dev_Fecha", dev_Fecha) :
                new ObjectParameter("dev_Fecha", typeof(System.DateTime));
    
            var dev_EstadoParameter = dev_Estado.HasValue ?
                new ObjectParameter("dev_Estado", dev_Estado) :
                new ObjectParameter("dev_Estado", typeof(bool));
    
            var dev_UsuarioCreaParameter = dev_UsuarioCrea.HasValue ?
                new ObjectParameter("dev_UsuarioCrea", dev_UsuarioCrea) :
                new ObjectParameter("dev_UsuarioCrea", typeof(int));
    
            var dev_FechaCreaParameter = dev_FechaCrea.HasValue ?
                new ObjectParameter("dev_FechaCrea", dev_FechaCrea) :
                new ObjectParameter("dev_FechaCrea", typeof(System.DateTime));
    
            var dev_UsuarioModificaParameter = dev_UsuarioModifica.HasValue ?
                new ObjectParameter("dev_UsuarioModifica", dev_UsuarioModifica) :
                new ObjectParameter("dev_UsuarioModifica", typeof(int));
    
            var dev_FechaModificaParameter = dev_FechaModifica.HasValue ?
                new ObjectParameter("dev_FechaModifica", dev_FechaModifica) :
                new ObjectParameter("dev_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbDevolucion_Update_Result>("UDP_Vent_tbDevolucion_Update", dev_IdParameter, fact_IdParameter, cja_IdParameter, dev_FechaParameter, dev_EstadoParameter, dev_UsuarioCreaParameter, dev_FechaCreaParameter, dev_UsuarioModificaParameter, dev_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbDevolucionDetalle_Insert_Result> UDP_Vent_tbDevolucionDetalle_Insert(Nullable<int> dev_Id, string prod_Codigo, Nullable<decimal> devd_CantidadProducto, string devd_Descripcion, Nullable<decimal> devd_Monto, Nullable<int> devd_UsuarioCrea, Nullable<System.DateTime> devd_FechaCrea)
        {
            var dev_IdParameter = dev_Id.HasValue ?
                new ObjectParameter("dev_Id", dev_Id) :
                new ObjectParameter("dev_Id", typeof(int));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var devd_CantidadProductoParameter = devd_CantidadProducto.HasValue ?
                new ObjectParameter("devd_CantidadProducto", devd_CantidadProducto) :
                new ObjectParameter("devd_CantidadProducto", typeof(decimal));
    
            var devd_DescripcionParameter = devd_Descripcion != null ?
                new ObjectParameter("devd_Descripcion", devd_Descripcion) :
                new ObjectParameter("devd_Descripcion", typeof(string));
    
            var devd_MontoParameter = devd_Monto.HasValue ?
                new ObjectParameter("devd_Monto", devd_Monto) :
                new ObjectParameter("devd_Monto", typeof(decimal));
    
            var devd_UsuarioCreaParameter = devd_UsuarioCrea.HasValue ?
                new ObjectParameter("devd_UsuarioCrea", devd_UsuarioCrea) :
                new ObjectParameter("devd_UsuarioCrea", typeof(int));
    
            var devd_FechaCreaParameter = devd_FechaCrea.HasValue ?
                new ObjectParameter("devd_FechaCrea", devd_FechaCrea) :
                new ObjectParameter("devd_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbDevolucionDetalle_Insert_Result>("UDP_Vent_tbDevolucionDetalle_Insert", dev_IdParameter, prod_CodigoParameter, devd_CantidadProductoParameter, devd_DescripcionParameter, devd_MontoParameter, devd_UsuarioCreaParameter, devd_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbDevolucionDetalle_Update_Result> UDP_Vent_tbDevolucionDetalle_Update(Nullable<int> devd_Id, Nullable<int> dev_Id, string prod_Codigo, Nullable<decimal> devd_CantidadProducto, string devd_Descripcion, Nullable<decimal> devd_Monto, Nullable<int> devd_UsuarioCrea, Nullable<System.DateTime> devd_FechaCrea, Nullable<int> devd_UsuarioModifica, Nullable<System.DateTime> devd_FechaModifica)
        {
            var devd_IdParameter = devd_Id.HasValue ?
                new ObjectParameter("devd_Id", devd_Id) :
                new ObjectParameter("devd_Id", typeof(int));
    
            var dev_IdParameter = dev_Id.HasValue ?
                new ObjectParameter("dev_Id", dev_Id) :
                new ObjectParameter("dev_Id", typeof(int));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var devd_CantidadProductoParameter = devd_CantidadProducto.HasValue ?
                new ObjectParameter("devd_CantidadProducto", devd_CantidadProducto) :
                new ObjectParameter("devd_CantidadProducto", typeof(decimal));
    
            var devd_DescripcionParameter = devd_Descripcion != null ?
                new ObjectParameter("devd_Descripcion", devd_Descripcion) :
                new ObjectParameter("devd_Descripcion", typeof(string));
    
            var devd_MontoParameter = devd_Monto.HasValue ?
                new ObjectParameter("devd_Monto", devd_Monto) :
                new ObjectParameter("devd_Monto", typeof(decimal));
    
            var devd_UsuarioCreaParameter = devd_UsuarioCrea.HasValue ?
                new ObjectParameter("devd_UsuarioCrea", devd_UsuarioCrea) :
                new ObjectParameter("devd_UsuarioCrea", typeof(int));
    
            var devd_FechaCreaParameter = devd_FechaCrea.HasValue ?
                new ObjectParameter("devd_FechaCrea", devd_FechaCrea) :
                new ObjectParameter("devd_FechaCrea", typeof(System.DateTime));
    
            var devd_UsuarioModificaParameter = devd_UsuarioModifica.HasValue ?
                new ObjectParameter("devd_UsuarioModifica", devd_UsuarioModifica) :
                new ObjectParameter("devd_UsuarioModifica", typeof(int));
    
            var devd_FechaModificaParameter = devd_FechaModifica.HasValue ?
                new ObjectParameter("devd_FechaModifica", devd_FechaModifica) :
                new ObjectParameter("devd_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbDevolucionDetalle_Update_Result>("UDP_Vent_tbDevolucionDetalle_Update", devd_IdParameter, dev_IdParameter, prod_CodigoParameter, devd_CantidadProductoParameter, devd_DescripcionParameter, devd_MontoParameter, devd_UsuarioCreaParameter, devd_FechaCreaParameter, devd_UsuarioModificaParameter, devd_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbDocumentoFiscal_Insert_Result> UDP_Vent_tbDocumentoFiscal_Insert(string dfisc_Id, string dfisc_Descripcion, Nullable<int> dfisc_UsuarioCrea, Nullable<System.DateTime> dfisc_FechaCrea)
        {
            var dfisc_IdParameter = dfisc_Id != null ?
                new ObjectParameter("dfisc_Id", dfisc_Id) :
                new ObjectParameter("dfisc_Id", typeof(string));
    
            var dfisc_DescripcionParameter = dfisc_Descripcion != null ?
                new ObjectParameter("dfisc_Descripcion", dfisc_Descripcion) :
                new ObjectParameter("dfisc_Descripcion", typeof(string));
    
            var dfisc_UsuarioCreaParameter = dfisc_UsuarioCrea.HasValue ?
                new ObjectParameter("dfisc_UsuarioCrea", dfisc_UsuarioCrea) :
                new ObjectParameter("dfisc_UsuarioCrea", typeof(int));
    
            var dfisc_FechaCreaParameter = dfisc_FechaCrea.HasValue ?
                new ObjectParameter("dfisc_FechaCrea", dfisc_FechaCrea) :
                new ObjectParameter("dfisc_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbDocumentoFiscal_Insert_Result>("UDP_Vent_tbDocumentoFiscal_Insert", dfisc_IdParameter, dfisc_DescripcionParameter, dfisc_UsuarioCreaParameter, dfisc_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbDocumentoFiscal_Update_Result> UDP_Vent_tbDocumentoFiscal_Update(string dfisc_Id, string dfisc_Descripcion, Nullable<int> dfisc_UsuarioCrea, Nullable<System.DateTime> dfisc_FechaCrea, Nullable<int> dfisc_UsuarioModifica, Nullable<System.DateTime> dfisc_FechaModifica)
        {
            var dfisc_IdParameter = dfisc_Id != null ?
                new ObjectParameter("dfisc_Id", dfisc_Id) :
                new ObjectParameter("dfisc_Id", typeof(string));
    
            var dfisc_DescripcionParameter = dfisc_Descripcion != null ?
                new ObjectParameter("dfisc_Descripcion", dfisc_Descripcion) :
                new ObjectParameter("dfisc_Descripcion", typeof(string));
    
            var dfisc_UsuarioCreaParameter = dfisc_UsuarioCrea.HasValue ?
                new ObjectParameter("dfisc_UsuarioCrea", dfisc_UsuarioCrea) :
                new ObjectParameter("dfisc_UsuarioCrea", typeof(int));
    
            var dfisc_FechaCreaParameter = dfisc_FechaCrea.HasValue ?
                new ObjectParameter("dfisc_FechaCrea", dfisc_FechaCrea) :
                new ObjectParameter("dfisc_FechaCrea", typeof(System.DateTime));
    
            var dfisc_UsuarioModificaParameter = dfisc_UsuarioModifica.HasValue ?
                new ObjectParameter("dfisc_UsuarioModifica", dfisc_UsuarioModifica) :
                new ObjectParameter("dfisc_UsuarioModifica", typeof(int));
    
            var dfisc_FechaModificaParameter = dfisc_FechaModifica.HasValue ?
                new ObjectParameter("dfisc_FechaModifica", dfisc_FechaModifica) :
                new ObjectParameter("dfisc_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbDocumentoFiscal_Update_Result>("UDP_Vent_tbDocumentoFiscal_Update", dfisc_IdParameter, dfisc_DescripcionParameter, dfisc_UsuarioCreaParameter, dfisc_FechaCreaParameter, dfisc_UsuarioModificaParameter, dfisc_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbEstadoFactura_Insert_Result> UDP_Vent_tbEstadoFactura_Insert(string esfac_Descripcion, Nullable<int> esfac_UsuarioCrea, Nullable<System.DateTime> esfac_FechaCrea)
        {
            var esfac_DescripcionParameter = esfac_Descripcion != null ?
                new ObjectParameter("esfac_Descripcion", esfac_Descripcion) :
                new ObjectParameter("esfac_Descripcion", typeof(string));
    
            var esfac_UsuarioCreaParameter = esfac_UsuarioCrea.HasValue ?
                new ObjectParameter("esfac_UsuarioCrea", esfac_UsuarioCrea) :
                new ObjectParameter("esfac_UsuarioCrea", typeof(int));
    
            var esfac_FechaCreaParameter = esfac_FechaCrea.HasValue ?
                new ObjectParameter("esfac_FechaCrea", esfac_FechaCrea) :
                new ObjectParameter("esfac_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbEstadoFactura_Insert_Result>("UDP_Vent_tbEstadoFactura_Insert", esfac_DescripcionParameter, esfac_UsuarioCreaParameter, esfac_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbEstadoFactura_Update_Result> UDP_Vent_tbEstadoFactura_Update(Nullable<byte> esfac_Id, string esfac_Descripcion, Nullable<int> esfac_UsuarioCrea, Nullable<System.DateTime> esfac_FechaCrea, Nullable<int> esfac_UsuarioModifica, Nullable<System.DateTime> esfac_FechaModifica)
        {
            var esfac_IdParameter = esfac_Id.HasValue ?
                new ObjectParameter("esfac_Id", esfac_Id) :
                new ObjectParameter("esfac_Id", typeof(byte));
    
            var esfac_DescripcionParameter = esfac_Descripcion != null ?
                new ObjectParameter("esfac_Descripcion", esfac_Descripcion) :
                new ObjectParameter("esfac_Descripcion", typeof(string));
    
            var esfac_UsuarioCreaParameter = esfac_UsuarioCrea.HasValue ?
                new ObjectParameter("esfac_UsuarioCrea", esfac_UsuarioCrea) :
                new ObjectParameter("esfac_UsuarioCrea", typeof(int));
    
            var esfac_FechaCreaParameter = esfac_FechaCrea.HasValue ?
                new ObjectParameter("esfac_FechaCrea", esfac_FechaCrea) :
                new ObjectParameter("esfac_FechaCrea", typeof(System.DateTime));
    
            var esfac_UsuarioModificaParameter = esfac_UsuarioModifica.HasValue ?
                new ObjectParameter("esfac_UsuarioModifica", esfac_UsuarioModifica) :
                new ObjectParameter("esfac_UsuarioModifica", typeof(int));
    
            var esfac_FechaModificaParameter = esfac_FechaModifica.HasValue ?
                new ObjectParameter("esfac_FechaModifica", esfac_FechaModifica) :
                new ObjectParameter("esfac_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbEstadoFactura_Update_Result>("UDP_Vent_tbEstadoFactura_Update", esfac_IdParameter, esfac_DescripcionParameter, esfac_UsuarioCreaParameter, esfac_FechaCreaParameter, esfac_UsuarioModificaParameter, esfac_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbEstadoPedido_Insert_Result> UDP_Vent_tbEstadoPedido_Insert(string esped_Descripcion, Nullable<int> esped_UsuarioCrea, Nullable<System.DateTime> esped_FechaCrea)
        {
            var esped_DescripcionParameter = esped_Descripcion != null ?
                new ObjectParameter("esped_Descripcion", esped_Descripcion) :
                new ObjectParameter("esped_Descripcion", typeof(string));
    
            var esped_UsuarioCreaParameter = esped_UsuarioCrea.HasValue ?
                new ObjectParameter("esped_UsuarioCrea", esped_UsuarioCrea) :
                new ObjectParameter("esped_UsuarioCrea", typeof(int));
    
            var esped_FechaCreaParameter = esped_FechaCrea.HasValue ?
                new ObjectParameter("esped_FechaCrea", esped_FechaCrea) :
                new ObjectParameter("esped_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbEstadoPedido_Insert_Result>("UDP_Vent_tbEstadoPedido_Insert", esped_DescripcionParameter, esped_UsuarioCreaParameter, esped_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbEstadoPedido_Update_Result> UDP_Vent_tbEstadoPedido_Update(Nullable<byte> esped_Id, string esped_Descripcion, Nullable<int> esped_UsuarioCrea, Nullable<int> esped_UsuarioModifica, Nullable<System.DateTime> esped_FechaAgrego, Nullable<System.DateTime> esped_FechaModifica)
        {
            var esped_IdParameter = esped_Id.HasValue ?
                new ObjectParameter("esped_Id", esped_Id) :
                new ObjectParameter("esped_Id", typeof(byte));
    
            var esped_DescripcionParameter = esped_Descripcion != null ?
                new ObjectParameter("esped_Descripcion", esped_Descripcion) :
                new ObjectParameter("esped_Descripcion", typeof(string));
    
            var esped_UsuarioCreaParameter = esped_UsuarioCrea.HasValue ?
                new ObjectParameter("esped_UsuarioCrea", esped_UsuarioCrea) :
                new ObjectParameter("esped_UsuarioCrea", typeof(int));
    
            var esped_UsuarioModificaParameter = esped_UsuarioModifica.HasValue ?
                new ObjectParameter("esped_UsuarioModifica", esped_UsuarioModifica) :
                new ObjectParameter("esped_UsuarioModifica", typeof(int));
    
            var esped_FechaAgregoParameter = esped_FechaAgrego.HasValue ?
                new ObjectParameter("esped_FechaAgrego", esped_FechaAgrego) :
                new ObjectParameter("esped_FechaAgrego", typeof(System.DateTime));
    
            var esped_FechaModificaParameter = esped_FechaModifica.HasValue ?
                new ObjectParameter("esped_FechaModifica", esped_FechaModifica) :
                new ObjectParameter("esped_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbEstadoPedido_Update_Result>("UDP_Vent_tbEstadoPedido_Update", esped_IdParameter, esped_DescripcionParameter, esped_UsuarioCreaParameter, esped_UsuarioModificaParameter, esped_FechaAgregoParameter, esped_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbEstadoSolicitudCredito_Insert_Result> UDP_Vent_tbEstadoSolicitudCredito_Insert(string escre_Descripcion, Nullable<int> escre_UsuarioCrea, Nullable<System.DateTime> escre_FechaCrea)
        {
            var escre_DescripcionParameter = escre_Descripcion != null ?
                new ObjectParameter("escre_Descripcion", escre_Descripcion) :
                new ObjectParameter("escre_Descripcion", typeof(string));
    
            var escre_UsuarioCreaParameter = escre_UsuarioCrea.HasValue ?
                new ObjectParameter("escre_UsuarioCrea", escre_UsuarioCrea) :
                new ObjectParameter("escre_UsuarioCrea", typeof(int));
    
            var escre_FechaCreaParameter = escre_FechaCrea.HasValue ?
                new ObjectParameter("escre_FechaCrea", escre_FechaCrea) :
                new ObjectParameter("escre_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbEstadoSolicitudCredito_Insert_Result>("UDP_Vent_tbEstadoSolicitudCredito_Insert", escre_DescripcionParameter, escre_UsuarioCreaParameter, escre_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbEstadoSolicitudCredito_Update_Result> UDP_Vent_tbEstadoSolicitudCredito_Update(Nullable<byte> escre_Id, string escre_Descripcion, Nullable<int> escre_UsuarioCrea, Nullable<int> escre_UsuarioModifica, Nullable<System.DateTime> escre_FechaAgrego, Nullable<System.DateTime> escre_FechaModifica)
        {
            var escre_IdParameter = escre_Id.HasValue ?
                new ObjectParameter("escre_Id", escre_Id) :
                new ObjectParameter("escre_Id", typeof(byte));
    
            var escre_DescripcionParameter = escre_Descripcion != null ?
                new ObjectParameter("escre_Descripcion", escre_Descripcion) :
                new ObjectParameter("escre_Descripcion", typeof(string));
    
            var escre_UsuarioCreaParameter = escre_UsuarioCrea.HasValue ?
                new ObjectParameter("escre_UsuarioCrea", escre_UsuarioCrea) :
                new ObjectParameter("escre_UsuarioCrea", typeof(int));
    
            var escre_UsuarioModificaParameter = escre_UsuarioModifica.HasValue ?
                new ObjectParameter("escre_UsuarioModifica", escre_UsuarioModifica) :
                new ObjectParameter("escre_UsuarioModifica", typeof(int));
    
            var escre_FechaAgregoParameter = escre_FechaAgrego.HasValue ?
                new ObjectParameter("escre_FechaAgrego", escre_FechaAgrego) :
                new ObjectParameter("escre_FechaAgrego", typeof(System.DateTime));
    
            var escre_FechaModificaParameter = escre_FechaModifica.HasValue ?
                new ObjectParameter("escre_FechaModifica", escre_FechaModifica) :
                new ObjectParameter("escre_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbEstadoSolicitudCredito_Update_Result>("UDP_Vent_tbEstadoSolicitudCredito_Update", escre_IdParameter, escre_DescripcionParameter, escre_UsuarioCreaParameter, escre_UsuarioModificaParameter, escre_FechaAgregoParameter, escre_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbExoneracion_Estado_Result> UDP_Vent_tbExoneracion_Estado(Nullable<int> exo_Id, Nullable<bool> exo_ExoneracionActiva, Nullable<int> exo_UsuarioModifa, Nullable<System.DateTime> exo_FechaModifica)
        {
            var exo_IdParameter = exo_Id.HasValue ?
                new ObjectParameter("exo_Id", exo_Id) :
                new ObjectParameter("exo_Id", typeof(int));
    
            var exo_ExoneracionActivaParameter = exo_ExoneracionActiva.HasValue ?
                new ObjectParameter("exo_ExoneracionActiva", exo_ExoneracionActiva) :
                new ObjectParameter("exo_ExoneracionActiva", typeof(bool));
    
            var exo_UsuarioModifaParameter = exo_UsuarioModifa.HasValue ?
                new ObjectParameter("exo_UsuarioModifa", exo_UsuarioModifa) :
                new ObjectParameter("exo_UsuarioModifa", typeof(int));
    
            var exo_FechaModificaParameter = exo_FechaModifica.HasValue ?
                new ObjectParameter("exo_FechaModifica", exo_FechaModifica) :
                new ObjectParameter("exo_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbExoneracion_Estado_Result>("UDP_Vent_tbExoneracion_Estado", exo_IdParameter, exo_ExoneracionActivaParameter, exo_UsuarioModifaParameter, exo_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbExoneracion_Insert_Result> UDP_Vent_tbExoneracion_Insert(string exo_Documento, Nullable<bool> exo_ExoneracionActiva, Nullable<System.DateTime> exo_FechaInicialVigencia, Nullable<System.DateTime> exo_FechaIFinalVigencia, Nullable<int> clte_Id, Nullable<int> exo_UsuarioCrea, Nullable<System.DateTime> exo_FechaCrea)
        {
            var exo_DocumentoParameter = exo_Documento != null ?
                new ObjectParameter("exo_Documento", exo_Documento) :
                new ObjectParameter("exo_Documento", typeof(string));
    
            var exo_ExoneracionActivaParameter = exo_ExoneracionActiva.HasValue ?
                new ObjectParameter("exo_ExoneracionActiva", exo_ExoneracionActiva) :
                new ObjectParameter("exo_ExoneracionActiva", typeof(bool));
    
            var exo_FechaInicialVigenciaParameter = exo_FechaInicialVigencia.HasValue ?
                new ObjectParameter("exo_FechaInicialVigencia", exo_FechaInicialVigencia) :
                new ObjectParameter("exo_FechaInicialVigencia", typeof(System.DateTime));
    
            var exo_FechaIFinalVigenciaParameter = exo_FechaIFinalVigencia.HasValue ?
                new ObjectParameter("exo_FechaIFinalVigencia", exo_FechaIFinalVigencia) :
                new ObjectParameter("exo_FechaIFinalVigencia", typeof(System.DateTime));
    
            var clte_IdParameter = clte_Id.HasValue ?
                new ObjectParameter("clte_Id", clte_Id) :
                new ObjectParameter("clte_Id", typeof(int));
    
            var exo_UsuarioCreaParameter = exo_UsuarioCrea.HasValue ?
                new ObjectParameter("exo_UsuarioCrea", exo_UsuarioCrea) :
                new ObjectParameter("exo_UsuarioCrea", typeof(int));
    
            var exo_FechaCreaParameter = exo_FechaCrea.HasValue ?
                new ObjectParameter("exo_FechaCrea", exo_FechaCrea) :
                new ObjectParameter("exo_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbExoneracion_Insert_Result>("UDP_Vent_tbExoneracion_Insert", exo_DocumentoParameter, exo_ExoneracionActivaParameter, exo_FechaInicialVigenciaParameter, exo_FechaIFinalVigenciaParameter, clte_IdParameter, exo_UsuarioCreaParameter, exo_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbExoneracion_Select_Result> UDP_Vent_tbExoneracion_Select()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbExoneracion_Select_Result>("UDP_Vent_tbExoneracion_Select");
        }
    
        public virtual ObjectResult<UDP_Vent_tbExoneracion_Update_Result> UDP_Vent_tbExoneracion_Update(Nullable<int> exo_Id, string exo_Documento, Nullable<bool> exo_ExoneracionActiva, Nullable<System.DateTime> exo_FechaInicialVigencia, Nullable<System.DateTime> exo_FechaIFinalVigencia, Nullable<int> clte_Id, Nullable<int> exo_UsuarioCrea, Nullable<System.DateTime> exo_FechaCrea, Nullable<int> exo_UsuarioModifa, Nullable<System.DateTime> exo_FechaModifica)
        {
            var exo_IdParameter = exo_Id.HasValue ?
                new ObjectParameter("exo_Id", exo_Id) :
                new ObjectParameter("exo_Id", typeof(int));
    
            var exo_DocumentoParameter = exo_Documento != null ?
                new ObjectParameter("exo_Documento", exo_Documento) :
                new ObjectParameter("exo_Documento", typeof(string));
    
            var exo_ExoneracionActivaParameter = exo_ExoneracionActiva.HasValue ?
                new ObjectParameter("exo_ExoneracionActiva", exo_ExoneracionActiva) :
                new ObjectParameter("exo_ExoneracionActiva", typeof(bool));
    
            var exo_FechaInicialVigenciaParameter = exo_FechaInicialVigencia.HasValue ?
                new ObjectParameter("exo_FechaInicialVigencia", exo_FechaInicialVigencia) :
                new ObjectParameter("exo_FechaInicialVigencia", typeof(System.DateTime));
    
            var exo_FechaIFinalVigenciaParameter = exo_FechaIFinalVigencia.HasValue ?
                new ObjectParameter("exo_FechaIFinalVigencia", exo_FechaIFinalVigencia) :
                new ObjectParameter("exo_FechaIFinalVigencia", typeof(System.DateTime));
    
            var clte_IdParameter = clte_Id.HasValue ?
                new ObjectParameter("clte_Id", clte_Id) :
                new ObjectParameter("clte_Id", typeof(int));
    
            var exo_UsuarioCreaParameter = exo_UsuarioCrea.HasValue ?
                new ObjectParameter("exo_UsuarioCrea", exo_UsuarioCrea) :
                new ObjectParameter("exo_UsuarioCrea", typeof(int));
    
            var exo_FechaCreaParameter = exo_FechaCrea.HasValue ?
                new ObjectParameter("exo_FechaCrea", exo_FechaCrea) :
                new ObjectParameter("exo_FechaCrea", typeof(System.DateTime));
    
            var exo_UsuarioModifaParameter = exo_UsuarioModifa.HasValue ?
                new ObjectParameter("exo_UsuarioModifa", exo_UsuarioModifa) :
                new ObjectParameter("exo_UsuarioModifa", typeof(int));
    
            var exo_FechaModificaParameter = exo_FechaModifica.HasValue ?
                new ObjectParameter("exo_FechaModifica", exo_FechaModifica) :
                new ObjectParameter("exo_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbExoneracion_Update_Result>("UDP_Vent_tbExoneracion_Update", exo_IdParameter, exo_DocumentoParameter, exo_ExoneracionActivaParameter, exo_FechaInicialVigenciaParameter, exo_FechaIFinalVigenciaParameter, clte_IdParameter, exo_UsuarioCreaParameter, exo_FechaCreaParameter, exo_UsuarioModifaParameter, exo_FechaModificaParameter);
        }
    
        public virtual int UDP_Vent_tbFactura_BuscarListaPrecio(Nullable<int> iDCLIENTE, string iDPRODUCTO)
        {
            var iDCLIENTEParameter = iDCLIENTE.HasValue ?
                new ObjectParameter("IDCLIENTE", iDCLIENTE) :
                new ObjectParameter("IDCLIENTE", typeof(int));
    
            var iDPRODUCTOParameter = iDPRODUCTO != null ?
                new ObjectParameter("IDPRODUCTO", iDPRODUCTO) :
                new ObjectParameter("IDPRODUCTO", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UDP_Vent_tbFactura_BuscarListaPrecio", iDCLIENTEParameter, iDPRODUCTOParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbFactura_ConsultaBodega_Result> UDP_Vent_tbFactura_ConsultaBodega(Nullable<int> iDSUCURSAL, string cODIGOPRODUCTO)
        {
            var iDSUCURSALParameter = iDSUCURSAL.HasValue ?
                new ObjectParameter("IDSUCURSAL", iDSUCURSAL) :
                new ObjectParameter("IDSUCURSAL", typeof(int));
    
            var cODIGOPRODUCTOParameter = cODIGOPRODUCTO != null ?
                new ObjectParameter("CODIGOPRODUCTO", cODIGOPRODUCTO) :
                new ObjectParameter("CODIGOPRODUCTO", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbFactura_ConsultaBodega_Result>("UDP_Vent_tbFactura_ConsultaBodega", iDSUCURSALParameter, cODIGOPRODUCTOParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbFactura_Estado_Result> UDP_Vent_tbFactura_Estado(Nullable<int> fact_Id, Nullable<bool> fact_EsAnulada, string fact_RazonAnulado)
        {
            var fact_IdParameter = fact_Id.HasValue ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(int));
    
            var fact_EsAnuladaParameter = fact_EsAnulada.HasValue ?
                new ObjectParameter("fact_EsAnulada", fact_EsAnulada) :
                new ObjectParameter("fact_EsAnulada", typeof(bool));
    
            var fact_RazonAnuladoParameter = fact_RazonAnulado != null ?
                new ObjectParameter("fact_RazonAnulado", fact_RazonAnulado) :
                new ObjectParameter("fact_RazonAnulado", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbFactura_Estado_Result>("UDP_Vent_tbFactura_Estado", fact_IdParameter, fact_EsAnuladaParameter, fact_RazonAnuladoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbFactura_EstadoImpreso_Result> UDP_Vent_tbFactura_EstadoImpreso(Nullable<long> fact_Id, Nullable<int> esfac_Id)
        {
            var fact_IdParameter = fact_Id.HasValue ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(long));
    
            var esfac_IdParameter = esfac_Id.HasValue ?
                new ObjectParameter("esfac_Id", esfac_Id) :
                new ObjectParameter("esfac_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbFactura_EstadoImpreso_Result>("UDP_Vent_tbFactura_EstadoImpreso", fact_IdParameter, esfac_IdParameter);
        }
    
        public virtual int UDP_Vent_tbFactura_Filtrado_CodBarra_Sucursal_Cliente(Nullable<int> iDSUCURSAL, string cODIGOBARRAS, Nullable<int> iDCLIENTE)
        {
            var iDSUCURSALParameter = iDSUCURSAL.HasValue ?
                new ObjectParameter("IDSUCURSAL", iDSUCURSAL) :
                new ObjectParameter("IDSUCURSAL", typeof(int));
    
            var cODIGOBARRASParameter = cODIGOBARRAS != null ?
                new ObjectParameter("CODIGOBARRAS", cODIGOBARRAS) :
                new ObjectParameter("CODIGOBARRAS", typeof(string));
    
            var iDCLIENTEParameter = iDCLIENTE.HasValue ?
                new ObjectParameter("IDCLIENTE", iDCLIENTE) :
                new ObjectParameter("IDCLIENTE", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UDP_Vent_tbFactura_Filtrado_CodBarra_Sucursal_Cliente", iDSUCURSALParameter, cODIGOBARRASParameter, iDCLIENTEParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbFactura_GetDetalle_Result> UDP_Vent_tbFactura_GetDetalle(Nullable<long> fACTURA_ID)
        {
            var fACTURA_IDParameter = fACTURA_ID.HasValue ?
                new ObjectParameter("FACTURA_ID", fACTURA_ID) :
                new ObjectParameter("FACTURA_ID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbFactura_GetDetalle_Result>("UDP_Vent_tbFactura_GetDetalle", fACTURA_IDParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbFactura_GetDetalle_Edit_Result> UDP_Vent_tbFactura_GetDetalle_Edit(Nullable<int> fACTURA_IDD)
        {
            var fACTURA_IDDParameter = fACTURA_IDD.HasValue ?
                new ObjectParameter("FACTURA_IDD", fACTURA_IDD) :
                new ObjectParameter("FACTURA_IDD", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbFactura_GetDetalle_Edit_Result>("UDP_Vent_tbFactura_GetDetalle_Edit", fACTURA_IDDParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbFactura_Imprimir_Result> UDP_Vent_tbFactura_Imprimir(Nullable<long> fact_Id)
        {
            var fact_IdParameter = fact_Id.HasValue ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbFactura_Imprimir_Result>("UDP_Vent_tbFactura_Imprimir", fact_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbFactura_Insert_Result> UDP_Vent_tbFactura_Insert(string fact_Codigo, Nullable<System.DateTime> fact_Fecha, Nullable<byte> esfac_Id, Nullable<short> cja_Id, Nullable<int> suc_Id, Nullable<int> clte_Id, string pemi_NumeroCAI, Nullable<bool> fact_AlCredito, Nullable<int> fact_DiasCredito, Nullable<decimal> fact_PorcentajeDescuento, string fact_Vendedor, string clte_Identificacion, string clte_Nombres, string fact_IdentidadTE, string fact_NombresTE, Nullable<System.DateTime> fact_FechaNacimientoTE, Nullable<bool> fact_EsAnulada, string fact_RazonAnulado, Nullable<int> fact_UsuarioCrea, Nullable<System.DateTime> fact_FechaCrea)
        {
            var fact_CodigoParameter = fact_Codigo != null ?
                new ObjectParameter("fact_Codigo", fact_Codigo) :
                new ObjectParameter("fact_Codigo", typeof(string));
    
            var fact_FechaParameter = fact_Fecha.HasValue ?
                new ObjectParameter("fact_Fecha", fact_Fecha) :
                new ObjectParameter("fact_Fecha", typeof(System.DateTime));
    
            var esfac_IdParameter = esfac_Id.HasValue ?
                new ObjectParameter("esfac_Id", esfac_Id) :
                new ObjectParameter("esfac_Id", typeof(byte));
    
            var cja_IdParameter = cja_Id.HasValue ?
                new ObjectParameter("cja_Id", cja_Id) :
                new ObjectParameter("cja_Id", typeof(short));
    
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var clte_IdParameter = clte_Id.HasValue ?
                new ObjectParameter("clte_Id", clte_Id) :
                new ObjectParameter("clte_Id", typeof(int));
    
            var pemi_NumeroCAIParameter = pemi_NumeroCAI != null ?
                new ObjectParameter("pemi_NumeroCAI", pemi_NumeroCAI) :
                new ObjectParameter("pemi_NumeroCAI", typeof(string));
    
            var fact_AlCreditoParameter = fact_AlCredito.HasValue ?
                new ObjectParameter("fact_AlCredito", fact_AlCredito) :
                new ObjectParameter("fact_AlCredito", typeof(bool));
    
            var fact_DiasCreditoParameter = fact_DiasCredito.HasValue ?
                new ObjectParameter("fact_DiasCredito", fact_DiasCredito) :
                new ObjectParameter("fact_DiasCredito", typeof(int));
    
            var fact_PorcentajeDescuentoParameter = fact_PorcentajeDescuento.HasValue ?
                new ObjectParameter("fact_PorcentajeDescuento", fact_PorcentajeDescuento) :
                new ObjectParameter("fact_PorcentajeDescuento", typeof(decimal));
    
            var fact_VendedorParameter = fact_Vendedor != null ?
                new ObjectParameter("fact_Vendedor", fact_Vendedor) :
                new ObjectParameter("fact_Vendedor", typeof(string));
    
            var clte_IdentificacionParameter = clte_Identificacion != null ?
                new ObjectParameter("clte_Identificacion", clte_Identificacion) :
                new ObjectParameter("clte_Identificacion", typeof(string));
    
            var clte_NombresParameter = clte_Nombres != null ?
                new ObjectParameter("clte_Nombres", clte_Nombres) :
                new ObjectParameter("clte_Nombres", typeof(string));
    
            var fact_IdentidadTEParameter = fact_IdentidadTE != null ?
                new ObjectParameter("fact_IdentidadTE", fact_IdentidadTE) :
                new ObjectParameter("fact_IdentidadTE", typeof(string));
    
            var fact_NombresTEParameter = fact_NombresTE != null ?
                new ObjectParameter("fact_NombresTE", fact_NombresTE) :
                new ObjectParameter("fact_NombresTE", typeof(string));
    
            var fact_FechaNacimientoTEParameter = fact_FechaNacimientoTE.HasValue ?
                new ObjectParameter("fact_FechaNacimientoTE", fact_FechaNacimientoTE) :
                new ObjectParameter("fact_FechaNacimientoTE", typeof(System.DateTime));
    
            var fact_EsAnuladaParameter = fact_EsAnulada.HasValue ?
                new ObjectParameter("fact_EsAnulada", fact_EsAnulada) :
                new ObjectParameter("fact_EsAnulada", typeof(bool));
    
            var fact_RazonAnuladoParameter = fact_RazonAnulado != null ?
                new ObjectParameter("fact_RazonAnulado", fact_RazonAnulado) :
                new ObjectParameter("fact_RazonAnulado", typeof(string));
    
            var fact_UsuarioCreaParameter = fact_UsuarioCrea.HasValue ?
                new ObjectParameter("fact_UsuarioCrea", fact_UsuarioCrea) :
                new ObjectParameter("fact_UsuarioCrea", typeof(int));
    
            var fact_FechaCreaParameter = fact_FechaCrea.HasValue ?
                new ObjectParameter("fact_FechaCrea", fact_FechaCrea) :
                new ObjectParameter("fact_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbFactura_Insert_Result>("UDP_Vent_tbFactura_Insert", fact_CodigoParameter, fact_FechaParameter, esfac_IdParameter, cja_IdParameter, suc_IdParameter, clte_IdParameter, pemi_NumeroCAIParameter, fact_AlCreditoParameter, fact_DiasCreditoParameter, fact_PorcentajeDescuentoParameter, fact_VendedorParameter, clte_IdentificacionParameter, clte_NombresParameter, fact_IdentidadTEParameter, fact_NombresTEParameter, fact_FechaNacimientoTEParameter, fact_EsAnuladaParameter, fact_RazonAnuladoParameter, fact_UsuarioCreaParameter, fact_FechaCreaParameter);
        }
    
        public virtual int UDP_Vent_tbFactura_Insert_mejorado(string fact_Codigo, Nullable<System.DateTime> fact_Fecha, Nullable<byte> esfac_Id, Nullable<short> cja_Id, Nullable<int> suc_Id, Nullable<int> clte_Id, string pemi_NumeroCAI, Nullable<bool> fact_AlCredito, Nullable<int> fact_DiasCredito, Nullable<decimal> fact_PorcentajeDescuento, string fact_Vendedor, string clte_Identificacion, string clte_Nombres, string fact_IdentidadTE, string fact_NombresTE, Nullable<System.DateTime> fact_FechaNacimientoTE, Nullable<bool> fact_EsAnulada, string fact_RazonAnulado, Nullable<int> fact_UsuarioCrea, Nullable<System.DateTime> fact_FechaCrea)
        {
            var fact_CodigoParameter = fact_Codigo != null ?
                new ObjectParameter("fact_Codigo", fact_Codigo) :
                new ObjectParameter("fact_Codigo", typeof(string));
    
            var fact_FechaParameter = fact_Fecha.HasValue ?
                new ObjectParameter("fact_Fecha", fact_Fecha) :
                new ObjectParameter("fact_Fecha", typeof(System.DateTime));
    
            var esfac_IdParameter = esfac_Id.HasValue ?
                new ObjectParameter("esfac_Id", esfac_Id) :
                new ObjectParameter("esfac_Id", typeof(byte));
    
            var cja_IdParameter = cja_Id.HasValue ?
                new ObjectParameter("cja_Id", cja_Id) :
                new ObjectParameter("cja_Id", typeof(short));
    
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var clte_IdParameter = clte_Id.HasValue ?
                new ObjectParameter("clte_Id", clte_Id) :
                new ObjectParameter("clte_Id", typeof(int));
    
            var pemi_NumeroCAIParameter = pemi_NumeroCAI != null ?
                new ObjectParameter("pemi_NumeroCAI", pemi_NumeroCAI) :
                new ObjectParameter("pemi_NumeroCAI", typeof(string));
    
            var fact_AlCreditoParameter = fact_AlCredito.HasValue ?
                new ObjectParameter("fact_AlCredito", fact_AlCredito) :
                new ObjectParameter("fact_AlCredito", typeof(bool));
    
            var fact_DiasCreditoParameter = fact_DiasCredito.HasValue ?
                new ObjectParameter("fact_DiasCredito", fact_DiasCredito) :
                new ObjectParameter("fact_DiasCredito", typeof(int));
    
            var fact_PorcentajeDescuentoParameter = fact_PorcentajeDescuento.HasValue ?
                new ObjectParameter("fact_PorcentajeDescuento", fact_PorcentajeDescuento) :
                new ObjectParameter("fact_PorcentajeDescuento", typeof(decimal));
    
            var fact_VendedorParameter = fact_Vendedor != null ?
                new ObjectParameter("fact_Vendedor", fact_Vendedor) :
                new ObjectParameter("fact_Vendedor", typeof(string));
    
            var clte_IdentificacionParameter = clte_Identificacion != null ?
                new ObjectParameter("clte_Identificacion", clte_Identificacion) :
                new ObjectParameter("clte_Identificacion", typeof(string));
    
            var clte_NombresParameter = clte_Nombres != null ?
                new ObjectParameter("clte_Nombres", clte_Nombres) :
                new ObjectParameter("clte_Nombres", typeof(string));
    
            var fact_IdentidadTEParameter = fact_IdentidadTE != null ?
                new ObjectParameter("fact_IdentidadTE", fact_IdentidadTE) :
                new ObjectParameter("fact_IdentidadTE", typeof(string));
    
            var fact_NombresTEParameter = fact_NombresTE != null ?
                new ObjectParameter("fact_NombresTE", fact_NombresTE) :
                new ObjectParameter("fact_NombresTE", typeof(string));
    
            var fact_FechaNacimientoTEParameter = fact_FechaNacimientoTE.HasValue ?
                new ObjectParameter("fact_FechaNacimientoTE", fact_FechaNacimientoTE) :
                new ObjectParameter("fact_FechaNacimientoTE", typeof(System.DateTime));
    
            var fact_EsAnuladaParameter = fact_EsAnulada.HasValue ?
                new ObjectParameter("fact_EsAnulada", fact_EsAnulada) :
                new ObjectParameter("fact_EsAnulada", typeof(bool));
    
            var fact_RazonAnuladoParameter = fact_RazonAnulado != null ?
                new ObjectParameter("fact_RazonAnulado", fact_RazonAnulado) :
                new ObjectParameter("fact_RazonAnulado", typeof(string));
    
            var fact_UsuarioCreaParameter = fact_UsuarioCrea.HasValue ?
                new ObjectParameter("fact_UsuarioCrea", fact_UsuarioCrea) :
                new ObjectParameter("fact_UsuarioCrea", typeof(int));
    
            var fact_FechaCreaParameter = fact_FechaCrea.HasValue ?
                new ObjectParameter("fact_FechaCrea", fact_FechaCrea) :
                new ObjectParameter("fact_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UDP_Vent_tbFactura_Insert_mejorado", fact_CodigoParameter, fact_FechaParameter, esfac_IdParameter, cja_IdParameter, suc_IdParameter, clte_IdParameter, pemi_NumeroCAIParameter, fact_AlCreditoParameter, fact_DiasCreditoParameter, fact_PorcentajeDescuentoParameter, fact_VendedorParameter, clte_IdentificacionParameter, clte_NombresParameter, fact_IdentidadTEParameter, fact_NombresTEParameter, fact_FechaNacimientoTEParameter, fact_EsAnuladaParameter, fact_RazonAnuladoParameter, fact_UsuarioCreaParameter, fact_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbFactura_ObtenerCai_CodigoFactura_Result> UDP_Vent_tbFactura_ObtenerCai_CodigoFactura(Nullable<int> iDSucursal, Nullable<short> iDCAJA)
        {
            var iDSucursalParameter = iDSucursal.HasValue ?
                new ObjectParameter("IDSucursal", iDSucursal) :
                new ObjectParameter("IDSucursal", typeof(int));
    
            var iDCAJAParameter = iDCAJA.HasValue ?
                new ObjectParameter("IDCAJA", iDCAJA) :
                new ObjectParameter("IDCAJA", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbFactura_ObtenerCai_CodigoFactura_Result>("UDP_Vent_tbFactura_ObtenerCai_CodigoFactura", iDSucursalParameter, iDCAJAParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbFactura_ObtenerCai_CodigoFactura_mejora_Result> UDP_Vent_tbFactura_ObtenerCai_CodigoFactura_mejora(Nullable<int> iDSucursal, Nullable<short> iDCAJA)
        {
            var iDSucursalParameter = iDSucursal.HasValue ?
                new ObjectParameter("IDSucursal", iDSucursal) :
                new ObjectParameter("IDSucursal", typeof(int));
    
            var iDCAJAParameter = iDCAJA.HasValue ?
                new ObjectParameter("IDCAJA", iDCAJA) :
                new ObjectParameter("IDCAJA", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbFactura_ObtenerCai_CodigoFactura_mejora_Result>("UDP_Vent_tbFactura_ObtenerCai_CodigoFactura_mejora", iDSucursalParameter, iDCAJAParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbFactura_RolSupervisorCaja_Result> UDP_Vent_tbFactura_RolSupervisorCaja(string usuarioId, string password)
        {
            var usuarioIdParameter = usuarioId != null ?
                new ObjectParameter("UsuarioId", usuarioId) :
                new ObjectParameter("UsuarioId", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbFactura_RolSupervisorCaja_Result>("UDP_Vent_tbFactura_RolSupervisorCaja", usuarioIdParameter, passwordParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbFactura_Update_Result> UDP_Vent_tbFactura_Update(Nullable<long> fact_Id, string fact_Codigo, Nullable<System.DateTime> fact_Fecha, Nullable<byte> esfac_Id, Nullable<int> clte_Id, string pemi_NumeroCAI, Nullable<bool> fact_AlCredito, Nullable<int> fact_DiasCredito, Nullable<decimal> fact_PorcentajeDescuento, string fact_Vendedor, string clte_Identificacion, string clte_Nombres, string fact_IdentidadTE, string fact_NombresTE, Nullable<System.DateTime> fact_FechaNacimientoTE, Nullable<int> fact_UsuarioAutoriza, Nullable<System.DateTime> fact_FechaAutoriza, Nullable<bool> fact_EsAnulada, string fact_RazonAnulado, Nullable<int> fact_UsuarioCrea, Nullable<System.DateTime> fact_FechaCrea, Nullable<int> fact_UsuarioModifica, Nullable<System.DateTime> fact_FechaModifica)
        {
            var fact_IdParameter = fact_Id.HasValue ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(long));
    
            var fact_CodigoParameter = fact_Codigo != null ?
                new ObjectParameter("fact_Codigo", fact_Codigo) :
                new ObjectParameter("fact_Codigo", typeof(string));
    
            var fact_FechaParameter = fact_Fecha.HasValue ?
                new ObjectParameter("fact_Fecha", fact_Fecha) :
                new ObjectParameter("fact_Fecha", typeof(System.DateTime));
    
            var esfac_IdParameter = esfac_Id.HasValue ?
                new ObjectParameter("esfac_Id", esfac_Id) :
                new ObjectParameter("esfac_Id", typeof(byte));
    
            var clte_IdParameter = clte_Id.HasValue ?
                new ObjectParameter("clte_Id", clte_Id) :
                new ObjectParameter("clte_Id", typeof(int));
    
            var pemi_NumeroCAIParameter = pemi_NumeroCAI != null ?
                new ObjectParameter("pemi_NumeroCAI", pemi_NumeroCAI) :
                new ObjectParameter("pemi_NumeroCAI", typeof(string));
    
            var fact_AlCreditoParameter = fact_AlCredito.HasValue ?
                new ObjectParameter("fact_AlCredito", fact_AlCredito) :
                new ObjectParameter("fact_AlCredito", typeof(bool));
    
            var fact_DiasCreditoParameter = fact_DiasCredito.HasValue ?
                new ObjectParameter("fact_DiasCredito", fact_DiasCredito) :
                new ObjectParameter("fact_DiasCredito", typeof(int));
    
            var fact_PorcentajeDescuentoParameter = fact_PorcentajeDescuento.HasValue ?
                new ObjectParameter("fact_PorcentajeDescuento", fact_PorcentajeDescuento) :
                new ObjectParameter("fact_PorcentajeDescuento", typeof(decimal));
    
            var fact_VendedorParameter = fact_Vendedor != null ?
                new ObjectParameter("fact_Vendedor", fact_Vendedor) :
                new ObjectParameter("fact_Vendedor", typeof(string));
    
            var clte_IdentificacionParameter = clte_Identificacion != null ?
                new ObjectParameter("clte_Identificacion", clte_Identificacion) :
                new ObjectParameter("clte_Identificacion", typeof(string));
    
            var clte_NombresParameter = clte_Nombres != null ?
                new ObjectParameter("clte_Nombres", clte_Nombres) :
                new ObjectParameter("clte_Nombres", typeof(string));
    
            var fact_IdentidadTEParameter = fact_IdentidadTE != null ?
                new ObjectParameter("fact_IdentidadTE", fact_IdentidadTE) :
                new ObjectParameter("fact_IdentidadTE", typeof(string));
    
            var fact_NombresTEParameter = fact_NombresTE != null ?
                new ObjectParameter("fact_NombresTE", fact_NombresTE) :
                new ObjectParameter("fact_NombresTE", typeof(string));
    
            var fact_FechaNacimientoTEParameter = fact_FechaNacimientoTE.HasValue ?
                new ObjectParameter("fact_FechaNacimientoTE", fact_FechaNacimientoTE) :
                new ObjectParameter("fact_FechaNacimientoTE", typeof(System.DateTime));
    
            var fact_UsuarioAutorizaParameter = fact_UsuarioAutoriza.HasValue ?
                new ObjectParameter("fact_UsuarioAutoriza", fact_UsuarioAutoriza) :
                new ObjectParameter("fact_UsuarioAutoriza", typeof(int));
    
            var fact_FechaAutorizaParameter = fact_FechaAutoriza.HasValue ?
                new ObjectParameter("fact_FechaAutoriza", fact_FechaAutoriza) :
                new ObjectParameter("fact_FechaAutoriza", typeof(System.DateTime));
    
            var fact_EsAnuladaParameter = fact_EsAnulada.HasValue ?
                new ObjectParameter("fact_EsAnulada", fact_EsAnulada) :
                new ObjectParameter("fact_EsAnulada", typeof(bool));
    
            var fact_RazonAnuladoParameter = fact_RazonAnulado != null ?
                new ObjectParameter("fact_RazonAnulado", fact_RazonAnulado) :
                new ObjectParameter("fact_RazonAnulado", typeof(string));
    
            var fact_UsuarioCreaParameter = fact_UsuarioCrea.HasValue ?
                new ObjectParameter("fact_UsuarioCrea", fact_UsuarioCrea) :
                new ObjectParameter("fact_UsuarioCrea", typeof(int));
    
            var fact_FechaCreaParameter = fact_FechaCrea.HasValue ?
                new ObjectParameter("fact_FechaCrea", fact_FechaCrea) :
                new ObjectParameter("fact_FechaCrea", typeof(System.DateTime));
    
            var fact_UsuarioModificaParameter = fact_UsuarioModifica.HasValue ?
                new ObjectParameter("fact_UsuarioModifica", fact_UsuarioModifica) :
                new ObjectParameter("fact_UsuarioModifica", typeof(int));
    
            var fact_FechaModificaParameter = fact_FechaModifica.HasValue ?
                new ObjectParameter("fact_FechaModifica", fact_FechaModifica) :
                new ObjectParameter("fact_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbFactura_Update_Result>("UDP_Vent_tbFactura_Update", fact_IdParameter, fact_CodigoParameter, fact_FechaParameter, esfac_IdParameter, clte_IdParameter, pemi_NumeroCAIParameter, fact_AlCreditoParameter, fact_DiasCreditoParameter, fact_PorcentajeDescuentoParameter, fact_VendedorParameter, clte_IdentificacionParameter, clte_NombresParameter, fact_IdentidadTEParameter, fact_NombresTEParameter, fact_FechaNacimientoTEParameter, fact_UsuarioAutorizaParameter, fact_FechaAutorizaParameter, fact_EsAnuladaParameter, fact_RazonAnuladoParameter, fact_UsuarioCreaParameter, fact_FechaCreaParameter, fact_UsuarioModificaParameter, fact_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbFacturaDetalle_Insert_Result> UDP_Vent_tbFacturaDetalle_Insert(Nullable<long> fact_Id, string prod_Codigo, Nullable<decimal> factd_Cantidad, Nullable<decimal> factd_MontoDescuento, Nullable<decimal> factd_PorcentajeDescuento, Nullable<decimal> factd_Impuesto, Nullable<decimal> factd_PrecioUnitario, Nullable<int> factd_UsuarioCrea, Nullable<System.DateTime> factd_FechaCrea)
        {
            var fact_IdParameter = fact_Id.HasValue ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(long));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var factd_CantidadParameter = factd_Cantidad.HasValue ?
                new ObjectParameter("factd_Cantidad", factd_Cantidad) :
                new ObjectParameter("factd_Cantidad", typeof(decimal));
    
            var factd_MontoDescuentoParameter = factd_MontoDescuento.HasValue ?
                new ObjectParameter("factd_MontoDescuento", factd_MontoDescuento) :
                new ObjectParameter("factd_MontoDescuento", typeof(decimal));
    
            var factd_PorcentajeDescuentoParameter = factd_PorcentajeDescuento.HasValue ?
                new ObjectParameter("factd_PorcentajeDescuento", factd_PorcentajeDescuento) :
                new ObjectParameter("factd_PorcentajeDescuento", typeof(decimal));
    
            var factd_ImpuestoParameter = factd_Impuesto.HasValue ?
                new ObjectParameter("factd_Impuesto", factd_Impuesto) :
                new ObjectParameter("factd_Impuesto", typeof(decimal));
    
            var factd_PrecioUnitarioParameter = factd_PrecioUnitario.HasValue ?
                new ObjectParameter("factd_PrecioUnitario", factd_PrecioUnitario) :
                new ObjectParameter("factd_PrecioUnitario", typeof(decimal));
    
            var factd_UsuarioCreaParameter = factd_UsuarioCrea.HasValue ?
                new ObjectParameter("factd_UsuarioCrea", factd_UsuarioCrea) :
                new ObjectParameter("factd_UsuarioCrea", typeof(int));
    
            var factd_FechaCreaParameter = factd_FechaCrea.HasValue ?
                new ObjectParameter("factd_FechaCrea", factd_FechaCrea) :
                new ObjectParameter("factd_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbFacturaDetalle_Insert_Result>("UDP_Vent_tbFacturaDetalle_Insert", fact_IdParameter, prod_CodigoParameter, factd_CantidadParameter, factd_MontoDescuentoParameter, factd_PorcentajeDescuentoParameter, factd_ImpuestoParameter, factd_PrecioUnitarioParameter, factd_UsuarioCreaParameter, factd_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbFacturaDetalle_Update_Result> UDP_Vent_tbFacturaDetalle_Update(Nullable<short> factd_Id, string prod_Codigo, Nullable<decimal> factd_Cantidad, Nullable<decimal> factd_MontoDescuento, Nullable<decimal> factd_PorcentajeDescuento, Nullable<decimal> factd_Impuesto, Nullable<decimal> factd_PrecioUnitario, Nullable<int> factd_UsuarioAutoriza, Nullable<System.DateTime> factd_FechaAutoriza, Nullable<int> factd_UsuarioCrea, Nullable<System.DateTime> factd_FechaCrea, Nullable<int> factd_UsuarioModifica, Nullable<System.DateTime> factd_FechaModifica)
        {
            var factd_IdParameter = factd_Id.HasValue ?
                new ObjectParameter("factd_Id", factd_Id) :
                new ObjectParameter("factd_Id", typeof(short));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var factd_CantidadParameter = factd_Cantidad.HasValue ?
                new ObjectParameter("factd_Cantidad", factd_Cantidad) :
                new ObjectParameter("factd_Cantidad", typeof(decimal));
    
            var factd_MontoDescuentoParameter = factd_MontoDescuento.HasValue ?
                new ObjectParameter("factd_MontoDescuento", factd_MontoDescuento) :
                new ObjectParameter("factd_MontoDescuento", typeof(decimal));
    
            var factd_PorcentajeDescuentoParameter = factd_PorcentajeDescuento.HasValue ?
                new ObjectParameter("factd_PorcentajeDescuento", factd_PorcentajeDescuento) :
                new ObjectParameter("factd_PorcentajeDescuento", typeof(decimal));
    
            var factd_ImpuestoParameter = factd_Impuesto.HasValue ?
                new ObjectParameter("factd_Impuesto", factd_Impuesto) :
                new ObjectParameter("factd_Impuesto", typeof(decimal));
    
            var factd_PrecioUnitarioParameter = factd_PrecioUnitario.HasValue ?
                new ObjectParameter("factd_PrecioUnitario", factd_PrecioUnitario) :
                new ObjectParameter("factd_PrecioUnitario", typeof(decimal));
    
            var factd_UsuarioAutorizaParameter = factd_UsuarioAutoriza.HasValue ?
                new ObjectParameter("factd_UsuarioAutoriza", factd_UsuarioAutoriza) :
                new ObjectParameter("factd_UsuarioAutoriza", typeof(int));
    
            var factd_FechaAutorizaParameter = factd_FechaAutoriza.HasValue ?
                new ObjectParameter("factd_FechaAutoriza", factd_FechaAutoriza) :
                new ObjectParameter("factd_FechaAutoriza", typeof(System.DateTime));
    
            var factd_UsuarioCreaParameter = factd_UsuarioCrea.HasValue ?
                new ObjectParameter("factd_UsuarioCrea", factd_UsuarioCrea) :
                new ObjectParameter("factd_UsuarioCrea", typeof(int));
    
            var factd_FechaCreaParameter = factd_FechaCrea.HasValue ?
                new ObjectParameter("factd_FechaCrea", factd_FechaCrea) :
                new ObjectParameter("factd_FechaCrea", typeof(System.DateTime));
    
            var factd_UsuarioModificaParameter = factd_UsuarioModifica.HasValue ?
                new ObjectParameter("factd_UsuarioModifica", factd_UsuarioModifica) :
                new ObjectParameter("factd_UsuarioModifica", typeof(int));
    
            var factd_FechaModificaParameter = factd_FechaModifica.HasValue ?
                new ObjectParameter("factd_FechaModifica", factd_FechaModifica) :
                new ObjectParameter("factd_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbFacturaDetalle_Update_Result>("UDP_Vent_tbFacturaDetalle_Update", factd_IdParameter, prod_CodigoParameter, factd_CantidadParameter, factd_MontoDescuentoParameter, factd_PorcentajeDescuentoParameter, factd_ImpuestoParameter, factd_PrecioUnitarioParameter, factd_UsuarioAutorizaParameter, factd_FechaAutorizaParameter, factd_UsuarioCreaParameter, factd_FechaCreaParameter, factd_UsuarioModificaParameter, factd_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbFacturaHistorica_Insert_Result> UDP_Vent_tbFacturaHistorica_Insert(Nullable<long> fact_Id, Nullable<byte> esfac_Id, Nullable<System.DateTime> facth_Fecha)
        {
            var fact_IdParameter = fact_Id.HasValue ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(long));
    
            var esfac_IdParameter = esfac_Id.HasValue ?
                new ObjectParameter("esfac_Id", esfac_Id) :
                new ObjectParameter("esfac_Id", typeof(byte));
    
            var facth_FechaParameter = facth_Fecha.HasValue ?
                new ObjectParameter("facth_Fecha", facth_Fecha) :
                new ObjectParameter("facth_Fecha", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbFacturaHistorica_Insert_Result>("UDP_Vent_tbFacturaHistorica_Insert", fact_IdParameter, esfac_IdParameter, facth_FechaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbFacturaHistorica_Update_Result> UDP_Vent_tbFacturaHistorica_Update(Nullable<byte> facth_Id, Nullable<System.DateTime> facth_Fecha)
        {
            var facth_IdParameter = facth_Id.HasValue ?
                new ObjectParameter("facth_Id", facth_Id) :
                new ObjectParameter("facth_Id", typeof(byte));
    
            var facth_FechaParameter = facth_Fecha.HasValue ?
                new ObjectParameter("facth_Fecha", facth_Fecha) :
                new ObjectParameter("facth_Fecha", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbFacturaHistorica_Update_Result>("UDP_Vent_tbFacturaHistorica_Update", facth_IdParameter, facth_FechaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbListadoPrecioDetalle_Insert_Result> UDP_Vent_tbListadoPrecioDetalle_Insert(Nullable<int> listp_Id, string prod_Codigo, Nullable<decimal> lispd_PrecioMayorista, Nullable<decimal> lispd_PrecioMinorista, Nullable<decimal> lispd_DescCaja, Nullable<decimal> lispd_DescGerente, Nullable<int> lispd_UsuarioCrea, Nullable<System.DateTime> lispd_FechaCrea)
        {
            var listp_IdParameter = listp_Id.HasValue ?
                new ObjectParameter("listp_Id", listp_Id) :
                new ObjectParameter("listp_Id", typeof(int));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var lispd_PrecioMayoristaParameter = lispd_PrecioMayorista.HasValue ?
                new ObjectParameter("lispd_PrecioMayorista", lispd_PrecioMayorista) :
                new ObjectParameter("lispd_PrecioMayorista", typeof(decimal));
    
            var lispd_PrecioMinoristaParameter = lispd_PrecioMinorista.HasValue ?
                new ObjectParameter("lispd_PrecioMinorista", lispd_PrecioMinorista) :
                new ObjectParameter("lispd_PrecioMinorista", typeof(decimal));
    
            var lispd_DescCajaParameter = lispd_DescCaja.HasValue ?
                new ObjectParameter("lispd_DescCaja", lispd_DescCaja) :
                new ObjectParameter("lispd_DescCaja", typeof(decimal));
    
            var lispd_DescGerenteParameter = lispd_DescGerente.HasValue ?
                new ObjectParameter("lispd_DescGerente", lispd_DescGerente) :
                new ObjectParameter("lispd_DescGerente", typeof(decimal));
    
            var lispd_UsuarioCreaParameter = lispd_UsuarioCrea.HasValue ?
                new ObjectParameter("lispd_UsuarioCrea", lispd_UsuarioCrea) :
                new ObjectParameter("lispd_UsuarioCrea", typeof(int));
    
            var lispd_FechaCreaParameter = lispd_FechaCrea.HasValue ?
                new ObjectParameter("lispd_FechaCrea", lispd_FechaCrea) :
                new ObjectParameter("lispd_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbListadoPrecioDetalle_Insert_Result>("UDP_Vent_tbListadoPrecioDetalle_Insert", listp_IdParameter, prod_CodigoParameter, lispd_PrecioMayoristaParameter, lispd_PrecioMinoristaParameter, lispd_DescCajaParameter, lispd_DescGerenteParameter, lispd_UsuarioCreaParameter, lispd_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbListadoPrecioDetalle_Select_Result> UDP_Vent_tbListadoPrecioDetalle_Select(Nullable<int> iDLISTAPRECIO)
        {
            var iDLISTAPRECIOParameter = iDLISTAPRECIO.HasValue ?
                new ObjectParameter("IDLISTAPRECIO", iDLISTAPRECIO) :
                new ObjectParameter("IDLISTAPRECIO", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbListadoPrecioDetalle_Select_Result>("UDP_Vent_tbListadoPrecioDetalle_Select", iDLISTAPRECIOParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbListadoPrecioDetalle_Update_Result> UDP_Vent_tbListadoPrecioDetalle_Update(Nullable<int> lispd_Id, string prod_Codigo, Nullable<decimal> lispd_PrecioMayorista, Nullable<decimal> lispd_PrecioMinorista, Nullable<decimal> lispd_DescCaja, Nullable<decimal> lispd_DescGerente, Nullable<int> lispd_UsuarioCrea, Nullable<System.DateTime> lispd_FechaCrea, Nullable<int> lispd_UsuarioModifica, Nullable<System.DateTime> lispd_FechaModifica)
        {
            var lispd_IdParameter = lispd_Id.HasValue ?
                new ObjectParameter("lispd_Id", lispd_Id) :
                new ObjectParameter("lispd_Id", typeof(int));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var lispd_PrecioMayoristaParameter = lispd_PrecioMayorista.HasValue ?
                new ObjectParameter("lispd_PrecioMayorista", lispd_PrecioMayorista) :
                new ObjectParameter("lispd_PrecioMayorista", typeof(decimal));
    
            var lispd_PrecioMinoristaParameter = lispd_PrecioMinorista.HasValue ?
                new ObjectParameter("lispd_PrecioMinorista", lispd_PrecioMinorista) :
                new ObjectParameter("lispd_PrecioMinorista", typeof(decimal));
    
            var lispd_DescCajaParameter = lispd_DescCaja.HasValue ?
                new ObjectParameter("lispd_DescCaja", lispd_DescCaja) :
                new ObjectParameter("lispd_DescCaja", typeof(decimal));
    
            var lispd_DescGerenteParameter = lispd_DescGerente.HasValue ?
                new ObjectParameter("lispd_DescGerente", lispd_DescGerente) :
                new ObjectParameter("lispd_DescGerente", typeof(decimal));
    
            var lispd_UsuarioCreaParameter = lispd_UsuarioCrea.HasValue ?
                new ObjectParameter("lispd_UsuarioCrea", lispd_UsuarioCrea) :
                new ObjectParameter("lispd_UsuarioCrea", typeof(int));
    
            var lispd_FechaCreaParameter = lispd_FechaCrea.HasValue ?
                new ObjectParameter("lispd_FechaCrea", lispd_FechaCrea) :
                new ObjectParameter("lispd_FechaCrea", typeof(System.DateTime));
    
            var lispd_UsuarioModificaParameter = lispd_UsuarioModifica.HasValue ?
                new ObjectParameter("lispd_UsuarioModifica", lispd_UsuarioModifica) :
                new ObjectParameter("lispd_UsuarioModifica", typeof(int));
    
            var lispd_FechaModificaParameter = lispd_FechaModifica.HasValue ?
                new ObjectParameter("lispd_FechaModifica", lispd_FechaModifica) :
                new ObjectParameter("lispd_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbListadoPrecioDetalle_Update_Result>("UDP_Vent_tbListadoPrecioDetalle_Update", lispd_IdParameter, prod_CodigoParameter, lispd_PrecioMayoristaParameter, lispd_PrecioMinoristaParameter, lispd_DescCajaParameter, lispd_DescGerenteParameter, lispd_UsuarioCreaParameter, lispd_FechaCreaParameter, lispd_UsuarioModificaParameter, lispd_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbListaPrecio_Estado_Result> UDP_Vent_tbListaPrecio_Estado(Nullable<int> listp_Id, Nullable<bool> listp_EsActivo)
        {
            var listp_IdParameter = listp_Id.HasValue ?
                new ObjectParameter("listp_Id", listp_Id) :
                new ObjectParameter("listp_Id", typeof(int));
    
            var listp_EsActivoParameter = listp_EsActivo.HasValue ?
                new ObjectParameter("listp_EsActivo", listp_EsActivo) :
                new ObjectParameter("listp_EsActivo", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbListaPrecio_Estado_Result>("UDP_Vent_tbListaPrecio_Estado", listp_IdParameter, listp_EsActivoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbListaPrecio_Insert_Result> UDP_Vent_tbListaPrecio_Insert(string listp_Nombre, Nullable<bool> listp_EsActivo, Nullable<System.DateTime> listp_FechaInicioVigencia, Nullable<System.DateTime> listp_FechaFinalVigencia, Nullable<short> listp_Prioridad, Nullable<int> listp_UsuarioCrea, Nullable<System.DateTime> listp_FechaCrea)
        {
            var listp_NombreParameter = listp_Nombre != null ?
                new ObjectParameter("listp_Nombre", listp_Nombre) :
                new ObjectParameter("listp_Nombre", typeof(string));
    
            var listp_EsActivoParameter = listp_EsActivo.HasValue ?
                new ObjectParameter("listp_EsActivo", listp_EsActivo) :
                new ObjectParameter("listp_EsActivo", typeof(bool));
    
            var listp_FechaInicioVigenciaParameter = listp_FechaInicioVigencia.HasValue ?
                new ObjectParameter("listp_FechaInicioVigencia", listp_FechaInicioVigencia) :
                new ObjectParameter("listp_FechaInicioVigencia", typeof(System.DateTime));
    
            var listp_FechaFinalVigenciaParameter = listp_FechaFinalVigencia.HasValue ?
                new ObjectParameter("listp_FechaFinalVigencia", listp_FechaFinalVigencia) :
                new ObjectParameter("listp_FechaFinalVigencia", typeof(System.DateTime));
    
            var listp_PrioridadParameter = listp_Prioridad.HasValue ?
                new ObjectParameter("listp_Prioridad", listp_Prioridad) :
                new ObjectParameter("listp_Prioridad", typeof(short));
    
            var listp_UsuarioCreaParameter = listp_UsuarioCrea.HasValue ?
                new ObjectParameter("listp_UsuarioCrea", listp_UsuarioCrea) :
                new ObjectParameter("listp_UsuarioCrea", typeof(int));
    
            var listp_FechaCreaParameter = listp_FechaCrea.HasValue ?
                new ObjectParameter("listp_FechaCrea", listp_FechaCrea) :
                new ObjectParameter("listp_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbListaPrecio_Insert_Result>("UDP_Vent_tbListaPrecio_Insert", listp_NombreParameter, listp_EsActivoParameter, listp_FechaInicioVigenciaParameter, listp_FechaFinalVigenciaParameter, listp_PrioridadParameter, listp_UsuarioCreaParameter, listp_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbListaPrecio_UltimaFechaVigente_Result> UDP_Vent_tbListaPrecio_UltimaFechaVigente(Nullable<short> pRIORIDAD)
        {
            var pRIORIDADParameter = pRIORIDAD.HasValue ?
                new ObjectParameter("PRIORIDAD", pRIORIDAD) :
                new ObjectParameter("PRIORIDAD", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbListaPrecio_UltimaFechaVigente_Result>("UDP_Vent_tbListaPrecio_UltimaFechaVigente", pRIORIDADParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbListaPrecio_Update_Result> UDP_Vent_tbListaPrecio_Update(Nullable<int> listp_Id, string listp_Nombre, Nullable<bool> listp_EsActivo, Nullable<int> listp_UsuarioCrea, Nullable<System.DateTime> listp_FechaCrea, Nullable<int> listp_UsuarioModifica, Nullable<System.DateTime> listp_FechaModifica, Nullable<System.DateTime> listp_FechaInicioVigencia, Nullable<System.DateTime> listp_FechaFinalVigencia, Nullable<short> listp_Prioridad)
        {
            var listp_IdParameter = listp_Id.HasValue ?
                new ObjectParameter("listp_Id", listp_Id) :
                new ObjectParameter("listp_Id", typeof(int));
    
            var listp_NombreParameter = listp_Nombre != null ?
                new ObjectParameter("listp_Nombre", listp_Nombre) :
                new ObjectParameter("listp_Nombre", typeof(string));
    
            var listp_EsActivoParameter = listp_EsActivo.HasValue ?
                new ObjectParameter("listp_EsActivo", listp_EsActivo) :
                new ObjectParameter("listp_EsActivo", typeof(bool));
    
            var listp_UsuarioCreaParameter = listp_UsuarioCrea.HasValue ?
                new ObjectParameter("listp_UsuarioCrea", listp_UsuarioCrea) :
                new ObjectParameter("listp_UsuarioCrea", typeof(int));
    
            var listp_FechaCreaParameter = listp_FechaCrea.HasValue ?
                new ObjectParameter("listp_FechaCrea", listp_FechaCrea) :
                new ObjectParameter("listp_FechaCrea", typeof(System.DateTime));
    
            var listp_UsuarioModificaParameter = listp_UsuarioModifica.HasValue ?
                new ObjectParameter("listp_UsuarioModifica", listp_UsuarioModifica) :
                new ObjectParameter("listp_UsuarioModifica", typeof(int));
    
            var listp_FechaModificaParameter = listp_FechaModifica.HasValue ?
                new ObjectParameter("listp_FechaModifica", listp_FechaModifica) :
                new ObjectParameter("listp_FechaModifica", typeof(System.DateTime));
    
            var listp_FechaInicioVigenciaParameter = listp_FechaInicioVigencia.HasValue ?
                new ObjectParameter("listp_FechaInicioVigencia", listp_FechaInicioVigencia) :
                new ObjectParameter("listp_FechaInicioVigencia", typeof(System.DateTime));
    
            var listp_FechaFinalVigenciaParameter = listp_FechaFinalVigencia.HasValue ?
                new ObjectParameter("listp_FechaFinalVigencia", listp_FechaFinalVigencia) :
                new ObjectParameter("listp_FechaFinalVigencia", typeof(System.DateTime));
    
            var listp_PrioridadParameter = listp_Prioridad.HasValue ?
                new ObjectParameter("listp_Prioridad", listp_Prioridad) :
                new ObjectParameter("listp_Prioridad", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbListaPrecio_Update_Result>("UDP_Vent_tbListaPrecio_Update", listp_IdParameter, listp_NombreParameter, listp_EsActivoParameter, listp_UsuarioCreaParameter, listp_FechaCreaParameter, listp_UsuarioModificaParameter, listp_FechaModificaParameter, listp_FechaInicioVigenciaParameter, listp_FechaFinalVigenciaParameter, listp_PrioridadParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbMovimientoCaja_Apertura_Details_Result> UDP_Vent_tbMovimientoCaja_Apertura_Details(Nullable<int> idMovimiento)
        {
            var idMovimientoParameter = idMovimiento.HasValue ?
                new ObjectParameter("IdMovimiento", idMovimiento) :
                new ObjectParameter("IdMovimiento", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbMovimientoCaja_Apertura_Details_Result>("UDP_Vent_tbMovimientoCaja_Apertura_Details", idMovimientoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbMovimientoCaja_Apertura_Insert_Result> UDP_Vent_tbMovimientoCaja_Apertura_Insert(Nullable<short> cja_Id, Nullable<System.DateTime> mocja_FechaApertura, Nullable<int> mocja_UsuarioApertura, Nullable<int> usu_Id, Nullable<System.DateTime> mocja_FechaArqueo, Nullable<int> mocja_UsuarioArquea, Nullable<System.DateTime> mocja_FechaAceptacion, Nullable<int> mocja_UsuarioAceptacion, Nullable<int> mocja_UsuarioCrea, Nullable<System.DateTime> mocja_FechaCrea)
        {
            var cja_IdParameter = cja_Id.HasValue ?
                new ObjectParameter("cja_Id", cja_Id) :
                new ObjectParameter("cja_Id", typeof(short));
    
            var mocja_FechaAperturaParameter = mocja_FechaApertura.HasValue ?
                new ObjectParameter("mocja_FechaApertura", mocja_FechaApertura) :
                new ObjectParameter("mocja_FechaApertura", typeof(System.DateTime));
    
            var mocja_UsuarioAperturaParameter = mocja_UsuarioApertura.HasValue ?
                new ObjectParameter("mocja_UsuarioApertura", mocja_UsuarioApertura) :
                new ObjectParameter("mocja_UsuarioApertura", typeof(int));
    
            var usu_IdParameter = usu_Id.HasValue ?
                new ObjectParameter("usu_Id", usu_Id) :
                new ObjectParameter("usu_Id", typeof(int));
    
            var mocja_FechaArqueoParameter = mocja_FechaArqueo.HasValue ?
                new ObjectParameter("mocja_FechaArqueo", mocja_FechaArqueo) :
                new ObjectParameter("mocja_FechaArqueo", typeof(System.DateTime));
    
            var mocja_UsuarioArqueaParameter = mocja_UsuarioArquea.HasValue ?
                new ObjectParameter("mocja_UsuarioArquea", mocja_UsuarioArquea) :
                new ObjectParameter("mocja_UsuarioArquea", typeof(int));
    
            var mocja_FechaAceptacionParameter = mocja_FechaAceptacion.HasValue ?
                new ObjectParameter("mocja_FechaAceptacion", mocja_FechaAceptacion) :
                new ObjectParameter("mocja_FechaAceptacion", typeof(System.DateTime));
    
            var mocja_UsuarioAceptacionParameter = mocja_UsuarioAceptacion.HasValue ?
                new ObjectParameter("mocja_UsuarioAceptacion", mocja_UsuarioAceptacion) :
                new ObjectParameter("mocja_UsuarioAceptacion", typeof(int));
    
            var mocja_UsuarioCreaParameter = mocja_UsuarioCrea.HasValue ?
                new ObjectParameter("mocja_UsuarioCrea", mocja_UsuarioCrea) :
                new ObjectParameter("mocja_UsuarioCrea", typeof(int));
    
            var mocja_FechaCreaParameter = mocja_FechaCrea.HasValue ?
                new ObjectParameter("mocja_FechaCrea", mocja_FechaCrea) :
                new ObjectParameter("mocja_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbMovimientoCaja_Apertura_Insert_Result>("UDP_Vent_tbMovimientoCaja_Apertura_Insert", cja_IdParameter, mocja_FechaAperturaParameter, mocja_UsuarioAperturaParameter, usu_IdParameter, mocja_FechaArqueoParameter, mocja_UsuarioArqueaParameter, mocja_FechaAceptacionParameter, mocja_UsuarioAceptacionParameter, mocja_UsuarioCreaParameter, mocja_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbMovimientoCaja_GetUsuarioApertura_Result> UDP_Vent_tbMovimientoCaja_GetUsuarioApertura(Nullable<int> usuarioApertura)
        {
            var usuarioAperturaParameter = usuarioApertura.HasValue ?
                new ObjectParameter("UsuarioApertura", usuarioApertura) :
                new ObjectParameter("UsuarioApertura", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbMovimientoCaja_GetUsuarioApertura_Result>("UDP_Vent_tbMovimientoCaja_GetUsuarioApertura", usuarioAperturaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbMovimientoCaja_Imprimir_Result> UDP_Vent_tbMovimientoCaja_Imprimir(Nullable<int> mocja_Id)
        {
            var mocja_IdParameter = mocja_Id.HasValue ?
                new ObjectParameter("mocja_Id", mocja_Id) :
                new ObjectParameter("mocja_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbMovimientoCaja_Imprimir_Result>("UDP_Vent_tbMovimientoCaja_Imprimir", mocja_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbMovimientoCaja_Insert_Result> UDP_Vent_tbMovimientoCaja_Insert(Nullable<short> cja_Id, Nullable<int> usu_Id, Nullable<int> mocja_UsuarioApertura, Nullable<System.DateTime> mocja_FechaArqueo, Nullable<int> mocja_UsuarioArquea, Nullable<System.DateTime> mocja_FechaAceptacion, Nullable<int> mocja_UsuarioAceptacion, Nullable<int> mocja_UsuarioCrea, Nullable<System.DateTime> mocja_FechaCrea)
        {
            var cja_IdParameter = cja_Id.HasValue ?
                new ObjectParameter("cja_Id", cja_Id) :
                new ObjectParameter("cja_Id", typeof(short));
    
            var usu_IdParameter = usu_Id.HasValue ?
                new ObjectParameter("usu_Id", usu_Id) :
                new ObjectParameter("usu_Id", typeof(int));
    
            var mocja_UsuarioAperturaParameter = mocja_UsuarioApertura.HasValue ?
                new ObjectParameter("mocja_UsuarioApertura", mocja_UsuarioApertura) :
                new ObjectParameter("mocja_UsuarioApertura", typeof(int));
    
            var mocja_FechaArqueoParameter = mocja_FechaArqueo.HasValue ?
                new ObjectParameter("mocja_FechaArqueo", mocja_FechaArqueo) :
                new ObjectParameter("mocja_FechaArqueo", typeof(System.DateTime));
    
            var mocja_UsuarioArqueaParameter = mocja_UsuarioArquea.HasValue ?
                new ObjectParameter("mocja_UsuarioArquea", mocja_UsuarioArquea) :
                new ObjectParameter("mocja_UsuarioArquea", typeof(int));
    
            var mocja_FechaAceptacionParameter = mocja_FechaAceptacion.HasValue ?
                new ObjectParameter("mocja_FechaAceptacion", mocja_FechaAceptacion) :
                new ObjectParameter("mocja_FechaAceptacion", typeof(System.DateTime));
    
            var mocja_UsuarioAceptacionParameter = mocja_UsuarioAceptacion.HasValue ?
                new ObjectParameter("mocja_UsuarioAceptacion", mocja_UsuarioAceptacion) :
                new ObjectParameter("mocja_UsuarioAceptacion", typeof(int));
    
            var mocja_UsuarioCreaParameter = mocja_UsuarioCrea.HasValue ?
                new ObjectParameter("mocja_UsuarioCrea", mocja_UsuarioCrea) :
                new ObjectParameter("mocja_UsuarioCrea", typeof(int));
    
            var mocja_FechaCreaParameter = mocja_FechaCrea.HasValue ?
                new ObjectParameter("mocja_FechaCrea", mocja_FechaCrea) :
                new ObjectParameter("mocja_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbMovimientoCaja_Insert_Result>("UDP_Vent_tbMovimientoCaja_Insert", cja_IdParameter, usu_IdParameter, mocja_UsuarioAperturaParameter, mocja_FechaArqueoParameter, mocja_UsuarioArqueaParameter, mocja_FechaAceptacionParameter, mocja_UsuarioAceptacionParameter, mocja_UsuarioCreaParameter, mocja_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbMovimientoCaja_Update_Result> UDP_Vent_tbMovimientoCaja_Update(Nullable<int> mocja_Id, Nullable<int> mocja_UsuarioCrea, Nullable<System.DateTime> mocja_FechaCrea, Nullable<int> mocja_UsuarioModifica, Nullable<System.DateTime> mocja_FechaCreaModifica)
        {
            var mocja_IdParameter = mocja_Id.HasValue ?
                new ObjectParameter("mocja_Id", mocja_Id) :
                new ObjectParameter("mocja_Id", typeof(int));
    
            var mocja_UsuarioCreaParameter = mocja_UsuarioCrea.HasValue ?
                new ObjectParameter("mocja_UsuarioCrea", mocja_UsuarioCrea) :
                new ObjectParameter("mocja_UsuarioCrea", typeof(int));
    
            var mocja_FechaCreaParameter = mocja_FechaCrea.HasValue ?
                new ObjectParameter("mocja_FechaCrea", mocja_FechaCrea) :
                new ObjectParameter("mocja_FechaCrea", typeof(System.DateTime));
    
            var mocja_UsuarioModificaParameter = mocja_UsuarioModifica.HasValue ?
                new ObjectParameter("mocja_UsuarioModifica", mocja_UsuarioModifica) :
                new ObjectParameter("mocja_UsuarioModifica", typeof(int));
    
            var mocja_FechaCreaModificaParameter = mocja_FechaCreaModifica.HasValue ?
                new ObjectParameter("mocja_FechaCreaModifica", mocja_FechaCreaModifica) :
                new ObjectParameter("mocja_FechaCreaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbMovimientoCaja_Update_Result>("UDP_Vent_tbMovimientoCaja_Update", mocja_IdParameter, mocja_UsuarioCreaParameter, mocja_FechaCreaParameter, mocja_UsuarioModificaParameter, mocja_FechaCreaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbNotaCredito_Anulado_Result> UDP_Vent_tbNotaCredito_Anulado(Nullable<short> nocre_Id, Nullable<bool> nocre_Anulado, string nocre_RazonAnulado)
        {
            var nocre_IdParameter = nocre_Id.HasValue ?
                new ObjectParameter("nocre_Id", nocre_Id) :
                new ObjectParameter("nocre_Id", typeof(short));
    
            var nocre_AnuladoParameter = nocre_Anulado.HasValue ?
                new ObjectParameter("nocre_Anulado", nocre_Anulado) :
                new ObjectParameter("nocre_Anulado", typeof(bool));
    
            var nocre_RazonAnuladoParameter = nocre_RazonAnulado != null ?
                new ObjectParameter("nocre_RazonAnulado", nocre_RazonAnulado) :
                new ObjectParameter("nocre_RazonAnulado", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbNotaCredito_Anulado_Result>("UDP_Vent_tbNotaCredito_Anulado", nocre_IdParameter, nocre_AnuladoParameter, nocre_RazonAnuladoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbNotaCredito_Caja_Result> UDP_Vent_tbNotaCredito_Caja(Nullable<int> codUsuario)
        {
            var codUsuarioParameter = codUsuario.HasValue ?
                new ObjectParameter("CodUsuario", codUsuario) :
                new ObjectParameter("CodUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbNotaCredito_Caja_Result>("UDP_Vent_tbNotaCredito_Caja", codUsuarioParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbNotaCredito_CodigoNotaCredito_Result> UDP_Vent_tbNotaCredito_CodigoNotaCredito(Nullable<int> iDSucursal, Nullable<short> iDCAJA)
        {
            var iDSucursalParameter = iDSucursal.HasValue ?
                new ObjectParameter("IDSucursal", iDSucursal) :
                new ObjectParameter("IDSucursal", typeof(int));
    
            var iDCAJAParameter = iDCAJA.HasValue ?
                new ObjectParameter("IDCAJA", iDCAJA) :
                new ObjectParameter("IDCAJA", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbNotaCredito_CodigoNotaCredito_Result>("UDP_Vent_tbNotaCredito_CodigoNotaCredito", iDSucursalParameter, iDCAJAParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbNotaCredito_EsImpreso_Result> UDP_Vent_tbNotaCredito_EsImpreso(Nullable<short> nocre_Id, Nullable<bool> nocre_EsImpreso)
        {
            var nocre_IdParameter = nocre_Id.HasValue ?
                new ObjectParameter("nocre_Id", nocre_Id) :
                new ObjectParameter("nocre_Id", typeof(short));
    
            var nocre_EsImpresoParameter = nocre_EsImpreso.HasValue ?
                new ObjectParameter("nocre_EsImpreso", nocre_EsImpreso) :
                new ObjectParameter("nocre_EsImpreso", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbNotaCredito_EsImpreso_Result>("UDP_Vent_tbNotaCredito_EsImpreso", nocre_IdParameter, nocre_EsImpresoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbNotaCredito_Imprimir_Result> UDP_Vent_tbNotaCredito_Imprimir(Nullable<int> nocre_Id)
        {
            var nocre_IdParameter = nocre_Id.HasValue ?
                new ObjectParameter("nocre_Id", nocre_Id) :
                new ObjectParameter("nocre_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbNotaCredito_Imprimir_Result>("UDP_Vent_tbNotaCredito_Imprimir", nocre_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbNotaCredito_Insert_Result> UDP_Vent_tbNotaCredito_Insert(string nocre_Codigo, Nullable<int> dev_Id, Nullable<int> clte_Id, Nullable<int> suc_Id, Nullable<short> cja_Id, Nullable<bool> nocre_Anulado, string nocre_RazonAnulado, Nullable<System.DateTime> nocre_FechaEmision, string nocre_MotivoEmision, Nullable<decimal> nocre_Monto, Nullable<bool> nocre_Redimido, Nullable<System.DateTime> nocre_FechaRedimido, Nullable<bool> nocre_EsImpreso, Nullable<int> nocre_UsuarioCrea, Nullable<System.DateTime> nocre_FechaCrea)
        {
            var nocre_CodigoParameter = nocre_Codigo != null ?
                new ObjectParameter("nocre_Codigo", nocre_Codigo) :
                new ObjectParameter("nocre_Codigo", typeof(string));
    
            var dev_IdParameter = dev_Id.HasValue ?
                new ObjectParameter("dev_Id", dev_Id) :
                new ObjectParameter("dev_Id", typeof(int));
    
            var clte_IdParameter = clte_Id.HasValue ?
                new ObjectParameter("clte_Id", clte_Id) :
                new ObjectParameter("clte_Id", typeof(int));
    
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var cja_IdParameter = cja_Id.HasValue ?
                new ObjectParameter("cja_Id", cja_Id) :
                new ObjectParameter("cja_Id", typeof(short));
    
            var nocre_AnuladoParameter = nocre_Anulado.HasValue ?
                new ObjectParameter("nocre_Anulado", nocre_Anulado) :
                new ObjectParameter("nocre_Anulado", typeof(bool));
    
            var nocre_RazonAnuladoParameter = nocre_RazonAnulado != null ?
                new ObjectParameter("nocre_RazonAnulado", nocre_RazonAnulado) :
                new ObjectParameter("nocre_RazonAnulado", typeof(string));
    
            var nocre_FechaEmisionParameter = nocre_FechaEmision.HasValue ?
                new ObjectParameter("nocre_FechaEmision", nocre_FechaEmision) :
                new ObjectParameter("nocre_FechaEmision", typeof(System.DateTime));
    
            var nocre_MotivoEmisionParameter = nocre_MotivoEmision != null ?
                new ObjectParameter("nocre_MotivoEmision", nocre_MotivoEmision) :
                new ObjectParameter("nocre_MotivoEmision", typeof(string));
    
            var nocre_MontoParameter = nocre_Monto.HasValue ?
                new ObjectParameter("nocre_Monto", nocre_Monto) :
                new ObjectParameter("nocre_Monto", typeof(decimal));
    
            var nocre_RedimidoParameter = nocre_Redimido.HasValue ?
                new ObjectParameter("nocre_Redimido", nocre_Redimido) :
                new ObjectParameter("nocre_Redimido", typeof(bool));
    
            var nocre_FechaRedimidoParameter = nocre_FechaRedimido.HasValue ?
                new ObjectParameter("nocre_FechaRedimido", nocre_FechaRedimido) :
                new ObjectParameter("nocre_FechaRedimido", typeof(System.DateTime));
    
            var nocre_EsImpresoParameter = nocre_EsImpreso.HasValue ?
                new ObjectParameter("nocre_EsImpreso", nocre_EsImpreso) :
                new ObjectParameter("nocre_EsImpreso", typeof(bool));
    
            var nocre_UsuarioCreaParameter = nocre_UsuarioCrea.HasValue ?
                new ObjectParameter("nocre_UsuarioCrea", nocre_UsuarioCrea) :
                new ObjectParameter("nocre_UsuarioCrea", typeof(int));
    
            var nocre_FechaCreaParameter = nocre_FechaCrea.HasValue ?
                new ObjectParameter("nocre_FechaCrea", nocre_FechaCrea) :
                new ObjectParameter("nocre_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbNotaCredito_Insert_Result>("UDP_Vent_tbNotaCredito_Insert", nocre_CodigoParameter, dev_IdParameter, clte_IdParameter, suc_IdParameter, cja_IdParameter, nocre_AnuladoParameter, nocre_RazonAnuladoParameter, nocre_FechaEmisionParameter, nocre_MotivoEmisionParameter, nocre_MontoParameter, nocre_RedimidoParameter, nocre_FechaRedimidoParameter, nocre_EsImpresoParameter, nocre_UsuarioCreaParameter, nocre_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbNotaCredito_Redimido_Result> UDP_Vent_tbNotaCredito_Redimido(Nullable<short> nocre_Id, Nullable<bool> nocre_Redimido)
        {
            var nocre_IdParameter = nocre_Id.HasValue ?
                new ObjectParameter("nocre_Id", nocre_Id) :
                new ObjectParameter("nocre_Id", typeof(short));
    
            var nocre_RedimidoParameter = nocre_Redimido.HasValue ?
                new ObjectParameter("nocre_Redimido", nocre_Redimido) :
                new ObjectParameter("nocre_Redimido", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbNotaCredito_Redimido_Result>("UDP_Vent_tbNotaCredito_Redimido", nocre_IdParameter, nocre_RedimidoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbNotaCredito_Reporte_Result> UDP_Vent_tbNotaCredito_Reporte(Nullable<System.DateTime> fechaDesde, Nullable<System.DateTime> fechaHasta, Nullable<int> clte_Id, Nullable<bool> clte_EsPersonaNatural)
        {
            var fechaDesdeParameter = fechaDesde.HasValue ?
                new ObjectParameter("FechaDesde", fechaDesde) :
                new ObjectParameter("FechaDesde", typeof(System.DateTime));
    
            var fechaHastaParameter = fechaHasta.HasValue ?
                new ObjectParameter("FechaHasta", fechaHasta) :
                new ObjectParameter("FechaHasta", typeof(System.DateTime));
    
            var clte_IdParameter = clte_Id.HasValue ?
                new ObjectParameter("clte_Id", clte_Id) :
                new ObjectParameter("clte_Id", typeof(int));
    
            var clte_EsPersonaNaturalParameter = clte_EsPersonaNatural.HasValue ?
                new ObjectParameter("clte_EsPersonaNatural", clte_EsPersonaNatural) :
                new ObjectParameter("clte_EsPersonaNatural", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbNotaCredito_Reporte_Result>("UDP_Vent_tbNotaCredito_Reporte", fechaDesdeParameter, fechaHastaParameter, clte_IdParameter, clte_EsPersonaNaturalParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbNotaCredito_Select_Result> UDP_Vent_tbNotaCredito_Select()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbNotaCredito_Select_Result>("UDP_Vent_tbNotaCredito_Select");
        }
    
        public virtual ObjectResult<UDP_Vent_tbNotaCredito_Update_Result> UDP_Vent_tbNotaCredito_Update(Nullable<short> nocre_Id, string nocre_Codigo, Nullable<int> dev_Id, Nullable<int> clte_Id, Nullable<int> suc_Id, Nullable<short> cja_Id, Nullable<bool> nocre_Anulado, Nullable<System.DateTime> nocre_FechaEmision, string nocre_MotivoEmision, Nullable<decimal> nocre_Monto, Nullable<bool> nocre_Redimido, Nullable<System.DateTime> nocre_FechaRedimido, Nullable<bool> nocre_EsImpreso, Nullable<int> nocre_UsuarioCrea, Nullable<System.DateTime> nocre_FechaCrea, Nullable<int> nocre_UsuarioModifica, Nullable<System.DateTime> nocre_FechaModifica)
        {
            var nocre_IdParameter = nocre_Id.HasValue ?
                new ObjectParameter("nocre_Id", nocre_Id) :
                new ObjectParameter("nocre_Id", typeof(short));
    
            var nocre_CodigoParameter = nocre_Codigo != null ?
                new ObjectParameter("nocre_Codigo", nocre_Codigo) :
                new ObjectParameter("nocre_Codigo", typeof(string));
    
            var dev_IdParameter = dev_Id.HasValue ?
                new ObjectParameter("dev_Id", dev_Id) :
                new ObjectParameter("dev_Id", typeof(int));
    
            var clte_IdParameter = clte_Id.HasValue ?
                new ObjectParameter("clte_Id", clte_Id) :
                new ObjectParameter("clte_Id", typeof(int));
    
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var cja_IdParameter = cja_Id.HasValue ?
                new ObjectParameter("cja_Id", cja_Id) :
                new ObjectParameter("cja_Id", typeof(short));
    
            var nocre_AnuladoParameter = nocre_Anulado.HasValue ?
                new ObjectParameter("nocre_Anulado", nocre_Anulado) :
                new ObjectParameter("nocre_Anulado", typeof(bool));
    
            var nocre_FechaEmisionParameter = nocre_FechaEmision.HasValue ?
                new ObjectParameter("nocre_FechaEmision", nocre_FechaEmision) :
                new ObjectParameter("nocre_FechaEmision", typeof(System.DateTime));
    
            var nocre_MotivoEmisionParameter = nocre_MotivoEmision != null ?
                new ObjectParameter("nocre_MotivoEmision", nocre_MotivoEmision) :
                new ObjectParameter("nocre_MotivoEmision", typeof(string));
    
            var nocre_MontoParameter = nocre_Monto.HasValue ?
                new ObjectParameter("nocre_Monto", nocre_Monto) :
                new ObjectParameter("nocre_Monto", typeof(decimal));
    
            var nocre_RedimidoParameter = nocre_Redimido.HasValue ?
                new ObjectParameter("nocre_Redimido", nocre_Redimido) :
                new ObjectParameter("nocre_Redimido", typeof(bool));
    
            var nocre_FechaRedimidoParameter = nocre_FechaRedimido.HasValue ?
                new ObjectParameter("nocre_FechaRedimido", nocre_FechaRedimido) :
                new ObjectParameter("nocre_FechaRedimido", typeof(System.DateTime));
    
            var nocre_EsImpresoParameter = nocre_EsImpreso.HasValue ?
                new ObjectParameter("nocre_EsImpreso", nocre_EsImpreso) :
                new ObjectParameter("nocre_EsImpreso", typeof(bool));
    
            var nocre_UsuarioCreaParameter = nocre_UsuarioCrea.HasValue ?
                new ObjectParameter("nocre_UsuarioCrea", nocre_UsuarioCrea) :
                new ObjectParameter("nocre_UsuarioCrea", typeof(int));
    
            var nocre_FechaCreaParameter = nocre_FechaCrea.HasValue ?
                new ObjectParameter("nocre_FechaCrea", nocre_FechaCrea) :
                new ObjectParameter("nocre_FechaCrea", typeof(System.DateTime));
    
            var nocre_UsuarioModificaParameter = nocre_UsuarioModifica.HasValue ?
                new ObjectParameter("nocre_UsuarioModifica", nocre_UsuarioModifica) :
                new ObjectParameter("nocre_UsuarioModifica", typeof(int));
    
            var nocre_FechaModificaParameter = nocre_FechaModifica.HasValue ?
                new ObjectParameter("nocre_FechaModifica", nocre_FechaModifica) :
                new ObjectParameter("nocre_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbNotaCredito_Update_Result>("UDP_Vent_tbNotaCredito_Update", nocre_IdParameter, nocre_CodigoParameter, dev_IdParameter, clte_IdParameter, suc_IdParameter, cja_IdParameter, nocre_AnuladoParameter, nocre_FechaEmisionParameter, nocre_MotivoEmisionParameter, nocre_MontoParameter, nocre_RedimidoParameter, nocre_FechaRedimidoParameter, nocre_EsImpresoParameter, nocre_UsuarioCreaParameter, nocre_FechaCreaParameter, nocre_UsuarioModificaParameter, nocre_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbNotaCreditoSelect_Result> UDP_Vent_tbNotaCreditoSelect(Nullable<int> clte_Id)
        {
            var clte_IdParameter = clte_Id.HasValue ?
                new ObjectParameter("clte_Id", clte_Id) :
                new ObjectParameter("clte_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbNotaCreditoSelect_Result>("UDP_Vent_tbNotaCreditoSelect", clte_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPago_Anulado_Result> UDP_Vent_tbPago_Anulado(Nullable<int> pago_Id, Nullable<bool> pago_EstaAnulado, string pago_RazonAnulado)
        {
            var pago_IdParameter = pago_Id.HasValue ?
                new ObjectParameter("pago_Id", pago_Id) :
                new ObjectParameter("pago_Id", typeof(int));
    
            var pago_EstaAnuladoParameter = pago_EstaAnulado.HasValue ?
                new ObjectParameter("pago_EstaAnulado", pago_EstaAnulado) :
                new ObjectParameter("pago_EstaAnulado", typeof(bool));
    
            var pago_RazonAnuladoParameter = pago_RazonAnulado != null ?
                new ObjectParameter("pago_RazonAnulado", pago_RazonAnulado) :
                new ObjectParameter("pago_RazonAnulado", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPago_Anulado_Result>("UDP_Vent_tbPago_Anulado", pago_IdParameter, pago_EstaAnuladoParameter, pago_RazonAnuladoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPago_Impreso_Result> UDP_Vent_tbPago_Impreso(Nullable<int> pago_Id, Nullable<bool> pago_EstaImpreso)
        {
            var pago_IdParameter = pago_Id.HasValue ?
                new ObjectParameter("pago_Id", pago_Id) :
                new ObjectParameter("pago_Id", typeof(int));
    
            var pago_EstaImpresoParameter = pago_EstaImpreso.HasValue ?
                new ObjectParameter("pago_EstaImpreso", pago_EstaImpreso) :
                new ObjectParameter("pago_EstaImpreso", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPago_Impreso_Result>("UDP_Vent_tbPago_Impreso", pago_IdParameter, pago_EstaImpresoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPago_Imprimir_Result> UDP_Vent_tbPago_Imprimir(Nullable<int> pago_Id)
        {
            var pago_IdParameter = pago_Id.HasValue ?
                new ObjectParameter("pago_Id", pago_Id) :
                new ObjectParameter("pago_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPago_Imprimir_Result>("UDP_Vent_tbPago_Imprimir", pago_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPago_ImprimirEstaImpreso_Result> UDP_Vent_tbPago_ImprimirEstaImpreso(Nullable<int> pago_Id, Nullable<bool> pago_EstaImpreso)
        {
            var pago_IdParameter = pago_Id.HasValue ?
                new ObjectParameter("pago_Id", pago_Id) :
                new ObjectParameter("pago_Id", typeof(int));
    
            var pago_EstaImpresoParameter = pago_EstaImpreso.HasValue ?
                new ObjectParameter("pago_EstaImpreso", pago_EstaImpreso) :
                new ObjectParameter("pago_EstaImpreso", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPago_ImprimirEstaImpreso_Result>("UDP_Vent_tbPago_ImprimirEstaImpreso", pago_IdParameter, pago_EstaImpresoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPago_Insert_Result> UDP_Vent_tbPago_Insert(Nullable<long> fact_Id, Nullable<short> tpa_Id, Nullable<System.DateTime> pago_FechaElaboracion, Nullable<decimal> pago_SaldoAnterior, Nullable<decimal> pago_TotalPago, Nullable<decimal> pago_TotalCambio, string pago_Emisor, Nullable<short> bcta_Id, Nullable<System.DateTime> pago_FechaVencimiento, string pago_Titular, string nocre_Codigo_cdto_Id, Nullable<bool> pago_EstaAnulado, string pago_RazonAnulado, Nullable<bool> pago_EstaImpreso, Nullable<int> pago_UsuarioCrea, Nullable<System.DateTime> pago_FechaCrea)
        {
            var fact_IdParameter = fact_Id.HasValue ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(long));
    
            var tpa_IdParameter = tpa_Id.HasValue ?
                new ObjectParameter("tpa_Id", tpa_Id) :
                new ObjectParameter("tpa_Id", typeof(short));
    
            var pago_FechaElaboracionParameter = pago_FechaElaboracion.HasValue ?
                new ObjectParameter("pago_FechaElaboracion", pago_FechaElaboracion) :
                new ObjectParameter("pago_FechaElaboracion", typeof(System.DateTime));
    
            var pago_SaldoAnteriorParameter = pago_SaldoAnterior.HasValue ?
                new ObjectParameter("pago_SaldoAnterior", pago_SaldoAnterior) :
                new ObjectParameter("pago_SaldoAnterior", typeof(decimal));
    
            var pago_TotalPagoParameter = pago_TotalPago.HasValue ?
                new ObjectParameter("pago_TotalPago", pago_TotalPago) :
                new ObjectParameter("pago_TotalPago", typeof(decimal));
    
            var pago_TotalCambioParameter = pago_TotalCambio.HasValue ?
                new ObjectParameter("pago_TotalCambio", pago_TotalCambio) :
                new ObjectParameter("pago_TotalCambio", typeof(decimal));
    
            var pago_EmisorParameter = pago_Emisor != null ?
                new ObjectParameter("pago_Emisor", pago_Emisor) :
                new ObjectParameter("pago_Emisor", typeof(string));
    
            var bcta_IdParameter = bcta_Id.HasValue ?
                new ObjectParameter("bcta_Id", bcta_Id) :
                new ObjectParameter("bcta_Id", typeof(short));
    
            var pago_FechaVencimientoParameter = pago_FechaVencimiento.HasValue ?
                new ObjectParameter("pago_FechaVencimiento", pago_FechaVencimiento) :
                new ObjectParameter("pago_FechaVencimiento", typeof(System.DateTime));
    
            var pago_TitularParameter = pago_Titular != null ?
                new ObjectParameter("pago_Titular", pago_Titular) :
                new ObjectParameter("pago_Titular", typeof(string));
    
            var nocre_Codigo_cdto_IdParameter = nocre_Codigo_cdto_Id != null ?
                new ObjectParameter("nocre_Codigo_cdto_Id", nocre_Codigo_cdto_Id) :
                new ObjectParameter("nocre_Codigo_cdto_Id", typeof(string));
    
            var pago_EstaAnuladoParameter = pago_EstaAnulado.HasValue ?
                new ObjectParameter("pago_EstaAnulado", pago_EstaAnulado) :
                new ObjectParameter("pago_EstaAnulado", typeof(bool));
    
            var pago_RazonAnuladoParameter = pago_RazonAnulado != null ?
                new ObjectParameter("pago_RazonAnulado", pago_RazonAnulado) :
                new ObjectParameter("pago_RazonAnulado", typeof(string));
    
            var pago_EstaImpresoParameter = pago_EstaImpreso.HasValue ?
                new ObjectParameter("pago_EstaImpreso", pago_EstaImpreso) :
                new ObjectParameter("pago_EstaImpreso", typeof(bool));
    
            var pago_UsuarioCreaParameter = pago_UsuarioCrea.HasValue ?
                new ObjectParameter("pago_UsuarioCrea", pago_UsuarioCrea) :
                new ObjectParameter("pago_UsuarioCrea", typeof(int));
    
            var pago_FechaCreaParameter = pago_FechaCrea.HasValue ?
                new ObjectParameter("pago_FechaCrea", pago_FechaCrea) :
                new ObjectParameter("pago_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPago_Insert_Result>("UDP_Vent_tbPago_Insert", fact_IdParameter, tpa_IdParameter, pago_FechaElaboracionParameter, pago_SaldoAnteriorParameter, pago_TotalPagoParameter, pago_TotalCambioParameter, pago_EmisorParameter, bcta_IdParameter, pago_FechaVencimientoParameter, pago_TitularParameter, nocre_Codigo_cdto_IdParameter, pago_EstaAnuladoParameter, pago_RazonAnuladoParameter, pago_EstaImpresoParameter, pago_UsuarioCreaParameter, pago_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPago_Update_Result> UDP_Vent_tbPago_Update(Nullable<int> pago_Id, Nullable<long> fact_Id, Nullable<short> tpa_Id, Nullable<System.DateTime> pago_FechaElaboracion, Nullable<decimal> pago_SaldoAnterior, Nullable<decimal> pago_TotalPago, Nullable<decimal> pago_TotalCambio, string pago_Emisor, Nullable<short> bcta_Id, Nullable<System.DateTime> pago_FechaVencimiento, string pago_Titular, string nocre_Codigo_cdto_Id, Nullable<int> pago_UsuarioCrea, Nullable<System.DateTime> pago_FechaCrea, Nullable<int> pago_UsuarioModifica, Nullable<System.DateTime> pago_FechaModifica)
        {
            var pago_IdParameter = pago_Id.HasValue ?
                new ObjectParameter("pago_Id", pago_Id) :
                new ObjectParameter("pago_Id", typeof(int));
    
            var fact_IdParameter = fact_Id.HasValue ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(long));
    
            var tpa_IdParameter = tpa_Id.HasValue ?
                new ObjectParameter("tpa_Id", tpa_Id) :
                new ObjectParameter("tpa_Id", typeof(short));
    
            var pago_FechaElaboracionParameter = pago_FechaElaboracion.HasValue ?
                new ObjectParameter("pago_FechaElaboracion", pago_FechaElaboracion) :
                new ObjectParameter("pago_FechaElaboracion", typeof(System.DateTime));
    
            var pago_SaldoAnteriorParameter = pago_SaldoAnterior.HasValue ?
                new ObjectParameter("pago_SaldoAnterior", pago_SaldoAnterior) :
                new ObjectParameter("pago_SaldoAnterior", typeof(decimal));
    
            var pago_TotalPagoParameter = pago_TotalPago.HasValue ?
                new ObjectParameter("pago_TotalPago", pago_TotalPago) :
                new ObjectParameter("pago_TotalPago", typeof(decimal));
    
            var pago_TotalCambioParameter = pago_TotalCambio.HasValue ?
                new ObjectParameter("pago_TotalCambio", pago_TotalCambio) :
                new ObjectParameter("pago_TotalCambio", typeof(decimal));
    
            var pago_EmisorParameter = pago_Emisor != null ?
                new ObjectParameter("pago_Emisor", pago_Emisor) :
                new ObjectParameter("pago_Emisor", typeof(string));
    
            var bcta_IdParameter = bcta_Id.HasValue ?
                new ObjectParameter("bcta_Id", bcta_Id) :
                new ObjectParameter("bcta_Id", typeof(short));
    
            var pago_FechaVencimientoParameter = pago_FechaVencimiento.HasValue ?
                new ObjectParameter("pago_FechaVencimiento", pago_FechaVencimiento) :
                new ObjectParameter("pago_FechaVencimiento", typeof(System.DateTime));
    
            var pago_TitularParameter = pago_Titular != null ?
                new ObjectParameter("pago_Titular", pago_Titular) :
                new ObjectParameter("pago_Titular", typeof(string));
    
            var nocre_Codigo_cdto_IdParameter = nocre_Codigo_cdto_Id != null ?
                new ObjectParameter("nocre_Codigo_cdto_Id", nocre_Codigo_cdto_Id) :
                new ObjectParameter("nocre_Codigo_cdto_Id", typeof(string));
    
            var pago_UsuarioCreaParameter = pago_UsuarioCrea.HasValue ?
                new ObjectParameter("pago_UsuarioCrea", pago_UsuarioCrea) :
                new ObjectParameter("pago_UsuarioCrea", typeof(int));
    
            var pago_FechaCreaParameter = pago_FechaCrea.HasValue ?
                new ObjectParameter("pago_FechaCrea", pago_FechaCrea) :
                new ObjectParameter("pago_FechaCrea", typeof(System.DateTime));
    
            var pago_UsuarioModificaParameter = pago_UsuarioModifica.HasValue ?
                new ObjectParameter("pago_UsuarioModifica", pago_UsuarioModifica) :
                new ObjectParameter("pago_UsuarioModifica", typeof(int));
    
            var pago_FechaModificaParameter = pago_FechaModifica.HasValue ?
                new ObjectParameter("pago_FechaModifica", pago_FechaModifica) :
                new ObjectParameter("pago_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPago_Update_Result>("UDP_Vent_tbPago_Update", pago_IdParameter, fact_IdParameter, tpa_IdParameter, pago_FechaElaboracionParameter, pago_SaldoAnteriorParameter, pago_TotalPagoParameter, pago_TotalCambioParameter, pago_EmisorParameter, bcta_IdParameter, pago_FechaVencimientoParameter, pago_TitularParameter, nocre_Codigo_cdto_IdParameter, pago_UsuarioCreaParameter, pago_FechaCreaParameter, pago_UsuarioModificaParameter, pago_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPagosArqueo_Insert_Result> UDP_Vent_tbPagosArqueo_Insert(Nullable<int> mocja_Id, Nullable<short> tpa_Id, Nullable<decimal> arqpg_PagosSistema, Nullable<decimal> arqpg_PagosConteo, Nullable<int> arqpg_UsuarioCrea, Nullable<System.DateTime> arqpg_FechaCrea)
        {
            var mocja_IdParameter = mocja_Id.HasValue ?
                new ObjectParameter("mocja_Id", mocja_Id) :
                new ObjectParameter("mocja_Id", typeof(int));
    
            var tpa_IdParameter = tpa_Id.HasValue ?
                new ObjectParameter("tpa_Id", tpa_Id) :
                new ObjectParameter("tpa_Id", typeof(short));
    
            var arqpg_PagosSistemaParameter = arqpg_PagosSistema.HasValue ?
                new ObjectParameter("arqpg_PagosSistema", arqpg_PagosSistema) :
                new ObjectParameter("arqpg_PagosSistema", typeof(decimal));
    
            var arqpg_PagosConteoParameter = arqpg_PagosConteo.HasValue ?
                new ObjectParameter("arqpg_PagosConteo", arqpg_PagosConteo) :
                new ObjectParameter("arqpg_PagosConteo", typeof(decimal));
    
            var arqpg_UsuarioCreaParameter = arqpg_UsuarioCrea.HasValue ?
                new ObjectParameter("arqpg_UsuarioCrea", arqpg_UsuarioCrea) :
                new ObjectParameter("arqpg_UsuarioCrea", typeof(int));
    
            var arqpg_FechaCreaParameter = arqpg_FechaCrea.HasValue ?
                new ObjectParameter("arqpg_FechaCrea", arqpg_FechaCrea) :
                new ObjectParameter("arqpg_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPagosArqueo_Insert_Result>("UDP_Vent_tbPagosArqueo_Insert", mocja_IdParameter, tpa_IdParameter, arqpg_PagosSistemaParameter, arqpg_PagosConteoParameter, arqpg_UsuarioCreaParameter, arqpg_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPagosArqueo_Update_Result> UDP_Vent_tbPagosArqueo_Update(Nullable<int> arqpg_Id, Nullable<int> mocja_Id, Nullable<short> tpa_Id, Nullable<decimal> arqpg_PagosSistema, Nullable<decimal> arqpg_PagosConteo, Nullable<int> arqpg_UsuarioCrea, Nullable<System.DateTime> arqpg_FechaCrea, Nullable<int> arqpg_UsuarioModifica, Nullable<System.DateTime> arqpg_FechaModifica)
        {
            var arqpg_IdParameter = arqpg_Id.HasValue ?
                new ObjectParameter("arqpg_Id", arqpg_Id) :
                new ObjectParameter("arqpg_Id", typeof(int));
    
            var mocja_IdParameter = mocja_Id.HasValue ?
                new ObjectParameter("mocja_Id", mocja_Id) :
                new ObjectParameter("mocja_Id", typeof(int));
    
            var tpa_IdParameter = tpa_Id.HasValue ?
                new ObjectParameter("tpa_Id", tpa_Id) :
                new ObjectParameter("tpa_Id", typeof(short));
    
            var arqpg_PagosSistemaParameter = arqpg_PagosSistema.HasValue ?
                new ObjectParameter("arqpg_PagosSistema", arqpg_PagosSistema) :
                new ObjectParameter("arqpg_PagosSistema", typeof(decimal));
    
            var arqpg_PagosConteoParameter = arqpg_PagosConteo.HasValue ?
                new ObjectParameter("arqpg_PagosConteo", arqpg_PagosConteo) :
                new ObjectParameter("arqpg_PagosConteo", typeof(decimal));
    
            var arqpg_UsuarioCreaParameter = arqpg_UsuarioCrea.HasValue ?
                new ObjectParameter("arqpg_UsuarioCrea", arqpg_UsuarioCrea) :
                new ObjectParameter("arqpg_UsuarioCrea", typeof(int));
    
            var arqpg_FechaCreaParameter = arqpg_FechaCrea.HasValue ?
                new ObjectParameter("arqpg_FechaCrea", arqpg_FechaCrea) :
                new ObjectParameter("arqpg_FechaCrea", typeof(System.DateTime));
    
            var arqpg_UsuarioModificaParameter = arqpg_UsuarioModifica.HasValue ?
                new ObjectParameter("arqpg_UsuarioModifica", arqpg_UsuarioModifica) :
                new ObjectParameter("arqpg_UsuarioModifica", typeof(int));
    
            var arqpg_FechaModificaParameter = arqpg_FechaModifica.HasValue ?
                new ObjectParameter("arqpg_FechaModifica", arqpg_FechaModifica) :
                new ObjectParameter("arqpg_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPagosArqueo_Update_Result>("UDP_Vent_tbPagosArqueo_Update", arqpg_IdParameter, mocja_IdParameter, tpa_IdParameter, arqpg_PagosSistemaParameter, arqpg_PagosConteoParameter, arqpg_UsuarioCreaParameter, arqpg_FechaCreaParameter, arqpg_UsuarioModificaParameter, arqpg_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPedido_Estado_Result> UDP_Vent_tbPedido_Estado(Nullable<int> ped_Id, Nullable<bool> ped_EsAnulado, string ped_RazonAnulado)
        {
            var ped_IdParameter = ped_Id.HasValue ?
                new ObjectParameter("ped_Id", ped_Id) :
                new ObjectParameter("ped_Id", typeof(int));
    
            var ped_EsAnuladoParameter = ped_EsAnulado.HasValue ?
                new ObjectParameter("ped_EsAnulado", ped_EsAnulado) :
                new ObjectParameter("ped_EsAnulado", typeof(bool));
    
            var ped_RazonAnuladoParameter = ped_RazonAnulado != null ?
                new ObjectParameter("ped_RazonAnulado", ped_RazonAnulado) :
                new ObjectParameter("ped_RazonAnulado", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPedido_Estado_Result>("UDP_Vent_tbPedido_Estado", ped_IdParameter, ped_EsAnuladoParameter, ped_RazonAnuladoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPedido_Factura_Result> UDP_Vent_tbPedido_Factura(Nullable<int> ped_Id, Nullable<int> fact_Id)
        {
            var ped_IdParameter = ped_Id.HasValue ?
                new ObjectParameter("ped_Id", ped_Id) :
                new ObjectParameter("ped_Id", typeof(int));
    
            var fact_IdParameter = fact_Id.HasValue ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPedido_Factura_Result>("UDP_Vent_tbPedido_Factura", ped_IdParameter, fact_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPedido_Insert_Result> UDP_Vent_tbPedido_Insert(Nullable<byte> esped_Id, Nullable<System.DateTime> ped_FechaElaboracion, Nullable<System.DateTime> ped_FechaEntrega, Nullable<int> clte_Id, Nullable<int> suc_Id, Nullable<long> fact_Id, Nullable<int> ped_UsuarioCrea, Nullable<System.DateTime> ped_FechaCrea, Nullable<bool> ped_EsAnulado, string ped_RazonAnulado)
        {
            var esped_IdParameter = esped_Id.HasValue ?
                new ObjectParameter("esped_Id", esped_Id) :
                new ObjectParameter("esped_Id", typeof(byte));
    
            var ped_FechaElaboracionParameter = ped_FechaElaboracion.HasValue ?
                new ObjectParameter("ped_FechaElaboracion", ped_FechaElaboracion) :
                new ObjectParameter("ped_FechaElaboracion", typeof(System.DateTime));
    
            var ped_FechaEntregaParameter = ped_FechaEntrega.HasValue ?
                new ObjectParameter("ped_FechaEntrega", ped_FechaEntrega) :
                new ObjectParameter("ped_FechaEntrega", typeof(System.DateTime));
    
            var clte_IdParameter = clte_Id.HasValue ?
                new ObjectParameter("clte_Id", clte_Id) :
                new ObjectParameter("clte_Id", typeof(int));
    
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var fact_IdParameter = fact_Id.HasValue ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(long));
    
            var ped_UsuarioCreaParameter = ped_UsuarioCrea.HasValue ?
                new ObjectParameter("ped_UsuarioCrea", ped_UsuarioCrea) :
                new ObjectParameter("ped_UsuarioCrea", typeof(int));
    
            var ped_FechaCreaParameter = ped_FechaCrea.HasValue ?
                new ObjectParameter("ped_FechaCrea", ped_FechaCrea) :
                new ObjectParameter("ped_FechaCrea", typeof(System.DateTime));
    
            var ped_EsAnuladoParameter = ped_EsAnulado.HasValue ?
                new ObjectParameter("ped_EsAnulado", ped_EsAnulado) :
                new ObjectParameter("ped_EsAnulado", typeof(bool));
    
            var ped_RazonAnuladoParameter = ped_RazonAnulado != null ?
                new ObjectParameter("ped_RazonAnulado", ped_RazonAnulado) :
                new ObjectParameter("ped_RazonAnulado", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPedido_Insert_Result>("UDP_Vent_tbPedido_Insert", esped_IdParameter, ped_FechaElaboracionParameter, ped_FechaEntregaParameter, clte_IdParameter, suc_IdParameter, fact_IdParameter, ped_UsuarioCreaParameter, ped_FechaCreaParameter, ped_EsAnuladoParameter, ped_RazonAnuladoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPedido_Select_Result> UDP_Vent_tbPedido_Select(Nullable<int> ped_Id)
        {
            var ped_IdParameter = ped_Id.HasValue ?
                new ObjectParameter("ped_Id", ped_Id) :
                new ObjectParameter("ped_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPedido_Select_Result>("UDP_Vent_tbPedido_Select", ped_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPedido_Update_Result> UDP_Vent_tbPedido_Update(Nullable<int> ped_Id, Nullable<byte> esped_Id, Nullable<System.DateTime> ped_FechaElaboracion, Nullable<System.DateTime> ped_FechaEntrega, Nullable<int> clte_Id, Nullable<int> suc_Id, Nullable<long> fact_Id, Nullable<bool> ped_EsAnulado, string ped_RazonAnulado, Nullable<int> ped_UsuarioCrea, Nullable<System.DateTime> ped_FechaCrea, Nullable<int> ped_UsuarioModifica, Nullable<System.DateTime> ped_FechaModifica)
        {
            var ped_IdParameter = ped_Id.HasValue ?
                new ObjectParameter("ped_Id", ped_Id) :
                new ObjectParameter("ped_Id", typeof(int));
    
            var esped_IdParameter = esped_Id.HasValue ?
                new ObjectParameter("esped_Id", esped_Id) :
                new ObjectParameter("esped_Id", typeof(byte));
    
            var ped_FechaElaboracionParameter = ped_FechaElaboracion.HasValue ?
                new ObjectParameter("ped_FechaElaboracion", ped_FechaElaboracion) :
                new ObjectParameter("ped_FechaElaboracion", typeof(System.DateTime));
    
            var ped_FechaEntregaParameter = ped_FechaEntrega.HasValue ?
                new ObjectParameter("ped_FechaEntrega", ped_FechaEntrega) :
                new ObjectParameter("ped_FechaEntrega", typeof(System.DateTime));
    
            var clte_IdParameter = clte_Id.HasValue ?
                new ObjectParameter("clte_Id", clte_Id) :
                new ObjectParameter("clte_Id", typeof(int));
    
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var fact_IdParameter = fact_Id.HasValue ?
                new ObjectParameter("fact_Id", fact_Id) :
                new ObjectParameter("fact_Id", typeof(long));
    
            var ped_EsAnuladoParameter = ped_EsAnulado.HasValue ?
                new ObjectParameter("ped_EsAnulado", ped_EsAnulado) :
                new ObjectParameter("ped_EsAnulado", typeof(bool));
    
            var ped_RazonAnuladoParameter = ped_RazonAnulado != null ?
                new ObjectParameter("ped_RazonAnulado", ped_RazonAnulado) :
                new ObjectParameter("ped_RazonAnulado", typeof(string));
    
            var ped_UsuarioCreaParameter = ped_UsuarioCrea.HasValue ?
                new ObjectParameter("ped_UsuarioCrea", ped_UsuarioCrea) :
                new ObjectParameter("ped_UsuarioCrea", typeof(int));
    
            var ped_FechaCreaParameter = ped_FechaCrea.HasValue ?
                new ObjectParameter("ped_FechaCrea", ped_FechaCrea) :
                new ObjectParameter("ped_FechaCrea", typeof(System.DateTime));
    
            var ped_UsuarioModificaParameter = ped_UsuarioModifica.HasValue ?
                new ObjectParameter("ped_UsuarioModifica", ped_UsuarioModifica) :
                new ObjectParameter("ped_UsuarioModifica", typeof(int));
    
            var ped_FechaModificaParameter = ped_FechaModifica.HasValue ?
                new ObjectParameter("ped_FechaModifica", ped_FechaModifica) :
                new ObjectParameter("ped_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPedido_Update_Result>("UDP_Vent_tbPedido_Update", ped_IdParameter, esped_IdParameter, ped_FechaElaboracionParameter, ped_FechaEntregaParameter, clte_IdParameter, suc_IdParameter, fact_IdParameter, ped_EsAnuladoParameter, ped_RazonAnuladoParameter, ped_UsuarioCreaParameter, ped_FechaCreaParameter, ped_UsuarioModificaParameter, ped_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPedidoDetalle_Insert_Result> UDP_Vent_tbPedidoDetalle_Insert(Nullable<int> ped_Id, string prod_Codigo, Nullable<decimal> pedd_Cantidad, Nullable<decimal> pedd_CantidadFacturada, Nullable<int> pedd_UsuarioCrea, Nullable<System.DateTime> pedd_FechaCrea)
        {
            var ped_IdParameter = ped_Id.HasValue ?
                new ObjectParameter("ped_Id", ped_Id) :
                new ObjectParameter("ped_Id", typeof(int));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var pedd_CantidadParameter = pedd_Cantidad.HasValue ?
                new ObjectParameter("pedd_Cantidad", pedd_Cantidad) :
                new ObjectParameter("pedd_Cantidad", typeof(decimal));
    
            var pedd_CantidadFacturadaParameter = pedd_CantidadFacturada.HasValue ?
                new ObjectParameter("pedd_CantidadFacturada", pedd_CantidadFacturada) :
                new ObjectParameter("pedd_CantidadFacturada", typeof(decimal));
    
            var pedd_UsuarioCreaParameter = pedd_UsuarioCrea.HasValue ?
                new ObjectParameter("pedd_UsuarioCrea", pedd_UsuarioCrea) :
                new ObjectParameter("pedd_UsuarioCrea", typeof(int));
    
            var pedd_FechaCreaParameter = pedd_FechaCrea.HasValue ?
                new ObjectParameter("pedd_FechaCrea", pedd_FechaCrea) :
                new ObjectParameter("pedd_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPedidoDetalle_Insert_Result>("UDP_Vent_tbPedidoDetalle_Insert", ped_IdParameter, prod_CodigoParameter, pedd_CantidadParameter, pedd_CantidadFacturadaParameter, pedd_UsuarioCreaParameter, pedd_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPedidoDetalle_Update_Result> UDP_Vent_tbPedidoDetalle_Update(Nullable<int> pedd_Id, string prod_Codigo, Nullable<decimal> pedd_Cantidad, Nullable<decimal> pedd_CantidadFacturada, Nullable<int> pedd_UsuarioCrea, Nullable<System.DateTime> pedd_FechaCrea, Nullable<int> pedd_UsuarioModifica, Nullable<System.DateTime> pedd_FechaModifica)
        {
            var pedd_IdParameter = pedd_Id.HasValue ?
                new ObjectParameter("pedd_Id", pedd_Id) :
                new ObjectParameter("pedd_Id", typeof(int));
    
            var prod_CodigoParameter = prod_Codigo != null ?
                new ObjectParameter("prod_Codigo", prod_Codigo) :
                new ObjectParameter("prod_Codigo", typeof(string));
    
            var pedd_CantidadParameter = pedd_Cantidad.HasValue ?
                new ObjectParameter("pedd_Cantidad", pedd_Cantidad) :
                new ObjectParameter("pedd_Cantidad", typeof(decimal));
    
            var pedd_CantidadFacturadaParameter = pedd_CantidadFacturada.HasValue ?
                new ObjectParameter("pedd_CantidadFacturada", pedd_CantidadFacturada) :
                new ObjectParameter("pedd_CantidadFacturada", typeof(decimal));
    
            var pedd_UsuarioCreaParameter = pedd_UsuarioCrea.HasValue ?
                new ObjectParameter("pedd_UsuarioCrea", pedd_UsuarioCrea) :
                new ObjectParameter("pedd_UsuarioCrea", typeof(int));
    
            var pedd_FechaCreaParameter = pedd_FechaCrea.HasValue ?
                new ObjectParameter("pedd_FechaCrea", pedd_FechaCrea) :
                new ObjectParameter("pedd_FechaCrea", typeof(System.DateTime));
    
            var pedd_UsuarioModificaParameter = pedd_UsuarioModifica.HasValue ?
                new ObjectParameter("pedd_UsuarioModifica", pedd_UsuarioModifica) :
                new ObjectParameter("pedd_UsuarioModifica", typeof(int));
    
            var pedd_FechaModificaParameter = pedd_FechaModifica.HasValue ?
                new ObjectParameter("pedd_FechaModifica", pedd_FechaModifica) :
                new ObjectParameter("pedd_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPedidoDetalle_Update_Result>("UDP_Vent_tbPedidoDetalle_Update", pedd_IdParameter, prod_CodigoParameter, pedd_CantidadParameter, pedd_CantidadFacturadaParameter, pedd_UsuarioCreaParameter, pedd_FechaCreaParameter, pedd_UsuarioModificaParameter, pedd_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPuntoEmision_Insert_Result> UDP_Vent_tbPuntoEmision_Insert(string pemi_NumeroCAI, Nullable<int> pemi_UsuarioCrea, Nullable<System.DateTime> pemi_FechaCrea)
        {
            var pemi_NumeroCAIParameter = pemi_NumeroCAI != null ?
                new ObjectParameter("pemi_NumeroCAI", pemi_NumeroCAI) :
                new ObjectParameter("pemi_NumeroCAI", typeof(string));
    
            var pemi_UsuarioCreaParameter = pemi_UsuarioCrea.HasValue ?
                new ObjectParameter("pemi_UsuarioCrea", pemi_UsuarioCrea) :
                new ObjectParameter("pemi_UsuarioCrea", typeof(int));
    
            var pemi_FechaCreaParameter = pemi_FechaCrea.HasValue ?
                new ObjectParameter("pemi_FechaCrea", pemi_FechaCrea) :
                new ObjectParameter("pemi_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPuntoEmision_Insert_Result>("UDP_Vent_tbPuntoEmision_Insert", pemi_NumeroCAIParameter, pemi_UsuarioCreaParameter, pemi_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPuntoEmision_Update_Result> UDP_Vent_tbPuntoEmision_Update(Nullable<int> pemi_Id, string pemi_NumeroCAI, Nullable<int> pemi_UsuarioCrea, Nullable<System.DateTime> pemi_FechaCrea, Nullable<int> pemi_UsuarioModifica, Nullable<System.DateTime> pemi_FechaModifica)
        {
            var pemi_IdParameter = pemi_Id.HasValue ?
                new ObjectParameter("pemi_Id", pemi_Id) :
                new ObjectParameter("pemi_Id", typeof(int));
    
            var pemi_NumeroCAIParameter = pemi_NumeroCAI != null ?
                new ObjectParameter("pemi_NumeroCAI", pemi_NumeroCAI) :
                new ObjectParameter("pemi_NumeroCAI", typeof(string));
    
            var pemi_UsuarioCreaParameter = pemi_UsuarioCrea.HasValue ?
                new ObjectParameter("pemi_UsuarioCrea", pemi_UsuarioCrea) :
                new ObjectParameter("pemi_UsuarioCrea", typeof(int));
    
            var pemi_FechaCreaParameter = pemi_FechaCrea.HasValue ?
                new ObjectParameter("pemi_FechaCrea", pemi_FechaCrea) :
                new ObjectParameter("pemi_FechaCrea", typeof(System.DateTime));
    
            var pemi_UsuarioModificaParameter = pemi_UsuarioModifica.HasValue ?
                new ObjectParameter("pemi_UsuarioModifica", pemi_UsuarioModifica) :
                new ObjectParameter("pemi_UsuarioModifica", typeof(int));
    
            var pemi_FechaModificaParameter = pemi_FechaModifica.HasValue ?
                new ObjectParameter("pemi_FechaModifica", pemi_FechaModifica) :
                new ObjectParameter("pemi_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPuntoEmision_Update_Result>("UDP_Vent_tbPuntoEmision_Update", pemi_IdParameter, pemi_NumeroCAIParameter, pemi_UsuarioCreaParameter, pemi_FechaCreaParameter, pemi_UsuarioModificaParameter, pemi_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPuntoEmisionDetalle_Insert_Result> UDP_Vent_tbPuntoEmisionDetalle_Insert(Nullable<int> pemi_Id, string dfisc_Id, string pemid_RangoInicio, string pemid_RangoFinal, string pemid_NumeroActual, Nullable<System.DateTime> pemid_FechaLimite, Nullable<int> pemid_UsuarioCrea, Nullable<System.DateTime> pemid_FechaCrea)
        {
            var pemi_IdParameter = pemi_Id.HasValue ?
                new ObjectParameter("pemi_Id", pemi_Id) :
                new ObjectParameter("pemi_Id", typeof(int));
    
            var dfisc_IdParameter = dfisc_Id != null ?
                new ObjectParameter("dfisc_Id", dfisc_Id) :
                new ObjectParameter("dfisc_Id", typeof(string));
    
            var pemid_RangoInicioParameter = pemid_RangoInicio != null ?
                new ObjectParameter("pemid_RangoInicio", pemid_RangoInicio) :
                new ObjectParameter("pemid_RangoInicio", typeof(string));
    
            var pemid_RangoFinalParameter = pemid_RangoFinal != null ?
                new ObjectParameter("pemid_RangoFinal", pemid_RangoFinal) :
                new ObjectParameter("pemid_RangoFinal", typeof(string));
    
            var pemid_NumeroActualParameter = pemid_NumeroActual != null ?
                new ObjectParameter("pemid_NumeroActual", pemid_NumeroActual) :
                new ObjectParameter("pemid_NumeroActual", typeof(string));
    
            var pemid_FechaLimiteParameter = pemid_FechaLimite.HasValue ?
                new ObjectParameter("pemid_FechaLimite", pemid_FechaLimite) :
                new ObjectParameter("pemid_FechaLimite", typeof(System.DateTime));
    
            var pemid_UsuarioCreaParameter = pemid_UsuarioCrea.HasValue ?
                new ObjectParameter("pemid_UsuarioCrea", pemid_UsuarioCrea) :
                new ObjectParameter("pemid_UsuarioCrea", typeof(int));
    
            var pemid_FechaCreaParameter = pemid_FechaCrea.HasValue ?
                new ObjectParameter("pemid_FechaCrea", pemid_FechaCrea) :
                new ObjectParameter("pemid_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPuntoEmisionDetalle_Insert_Result>("UDP_Vent_tbPuntoEmisionDetalle_Insert", pemi_IdParameter, dfisc_IdParameter, pemid_RangoInicioParameter, pemid_RangoFinalParameter, pemid_NumeroActualParameter, pemid_FechaLimiteParameter, pemid_UsuarioCreaParameter, pemid_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPuntoEmisionDetalle_Insert_Prueba_Result> UDP_Vent_tbPuntoEmisionDetalle_Insert_Prueba(Nullable<int> pemi_Id, string dfisc_Id, string pemid_RangoInicio, string pemid_RangoFinal, string pemid_NumeroActual, Nullable<System.DateTime> pemid_FechaLimite, Nullable<int> pemid_UsuarioCrea, Nullable<System.DateTime> pemid_FechaCrea)
        {
            var pemi_IdParameter = pemi_Id.HasValue ?
                new ObjectParameter("pemi_Id", pemi_Id) :
                new ObjectParameter("pemi_Id", typeof(int));
    
            var dfisc_IdParameter = dfisc_Id != null ?
                new ObjectParameter("dfisc_Id", dfisc_Id) :
                new ObjectParameter("dfisc_Id", typeof(string));
    
            var pemid_RangoInicioParameter = pemid_RangoInicio != null ?
                new ObjectParameter("pemid_RangoInicio", pemid_RangoInicio) :
                new ObjectParameter("pemid_RangoInicio", typeof(string));
    
            var pemid_RangoFinalParameter = pemid_RangoFinal != null ?
                new ObjectParameter("pemid_RangoFinal", pemid_RangoFinal) :
                new ObjectParameter("pemid_RangoFinal", typeof(string));
    
            var pemid_NumeroActualParameter = pemid_NumeroActual != null ?
                new ObjectParameter("pemid_NumeroActual", pemid_NumeroActual) :
                new ObjectParameter("pemid_NumeroActual", typeof(string));
    
            var pemid_FechaLimiteParameter = pemid_FechaLimite.HasValue ?
                new ObjectParameter("pemid_FechaLimite", pemid_FechaLimite) :
                new ObjectParameter("pemid_FechaLimite", typeof(System.DateTime));
    
            var pemid_UsuarioCreaParameter = pemid_UsuarioCrea.HasValue ?
                new ObjectParameter("pemid_UsuarioCrea", pemid_UsuarioCrea) :
                new ObjectParameter("pemid_UsuarioCrea", typeof(int));
    
            var pemid_FechaCreaParameter = pemid_FechaCrea.HasValue ?
                new ObjectParameter("pemid_FechaCrea", pemid_FechaCrea) :
                new ObjectParameter("pemid_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPuntoEmisionDetalle_Insert_Prueba_Result>("UDP_Vent_tbPuntoEmisionDetalle_Insert_Prueba", pemi_IdParameter, dfisc_IdParameter, pemid_RangoInicioParameter, pemid_RangoFinalParameter, pemid_NumeroActualParameter, pemid_FechaLimiteParameter, pemid_UsuarioCreaParameter, pemid_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbPuntoEmisionDetalle_Update_Result> UDP_Vent_tbPuntoEmisionDetalle_Update(Nullable<int> pemid_Id, string dfisc_Id, string pemid_RangoInicio, string pemid_RangoFinal, string pemid_NumeroActual, Nullable<System.DateTime> pemid_FechaLimite, Nullable<int> pemid_UsuarioCrea, Nullable<System.DateTime> pemid_FechaCrea, Nullable<int> pemid_UsuarioModifica, Nullable<System.DateTime> pemid_FechaModifica)
        {
            var pemid_IdParameter = pemid_Id.HasValue ?
                new ObjectParameter("pemid_Id", pemid_Id) :
                new ObjectParameter("pemid_Id", typeof(int));
    
            var dfisc_IdParameter = dfisc_Id != null ?
                new ObjectParameter("dfisc_Id", dfisc_Id) :
                new ObjectParameter("dfisc_Id", typeof(string));
    
            var pemid_RangoInicioParameter = pemid_RangoInicio != null ?
                new ObjectParameter("pemid_RangoInicio", pemid_RangoInicio) :
                new ObjectParameter("pemid_RangoInicio", typeof(string));
    
            var pemid_RangoFinalParameter = pemid_RangoFinal != null ?
                new ObjectParameter("pemid_RangoFinal", pemid_RangoFinal) :
                new ObjectParameter("pemid_RangoFinal", typeof(string));
    
            var pemid_NumeroActualParameter = pemid_NumeroActual != null ?
                new ObjectParameter("pemid_NumeroActual", pemid_NumeroActual) :
                new ObjectParameter("pemid_NumeroActual", typeof(string));
    
            var pemid_FechaLimiteParameter = pemid_FechaLimite.HasValue ?
                new ObjectParameter("pemid_FechaLimite", pemid_FechaLimite) :
                new ObjectParameter("pemid_FechaLimite", typeof(System.DateTime));
    
            var pemid_UsuarioCreaParameter = pemid_UsuarioCrea.HasValue ?
                new ObjectParameter("pemid_UsuarioCrea", pemid_UsuarioCrea) :
                new ObjectParameter("pemid_UsuarioCrea", typeof(int));
    
            var pemid_FechaCreaParameter = pemid_FechaCrea.HasValue ?
                new ObjectParameter("pemid_FechaCrea", pemid_FechaCrea) :
                new ObjectParameter("pemid_FechaCrea", typeof(System.DateTime));
    
            var pemid_UsuarioModificaParameter = pemid_UsuarioModifica.HasValue ?
                new ObjectParameter("pemid_UsuarioModifica", pemid_UsuarioModifica) :
                new ObjectParameter("pemid_UsuarioModifica", typeof(int));
    
            var pemid_FechaModificaParameter = pemid_FechaModifica.HasValue ?
                new ObjectParameter("pemid_FechaModifica", pemid_FechaModifica) :
                new ObjectParameter("pemid_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbPuntoEmisionDetalle_Update_Result>("UDP_Vent_tbPuntoEmisionDetalle_Update", pemid_IdParameter, dfisc_IdParameter, pemid_RangoInicioParameter, pemid_RangoFinalParameter, pemid_NumeroActualParameter, pemid_FechaLimiteParameter, pemid_UsuarioCreaParameter, pemid_FechaCreaParameter, pemid_UsuarioModificaParameter, pemid_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudCredito_Aprobar_Result> UDP_Vent_tbSolicitudCredito_Aprobar(Nullable<int> cred_Id, Nullable<byte> escre_Id, Nullable<System.DateTime> cred_FechaAprobacion, Nullable<decimal> cred_MontoSolicitado, Nullable<decimal> cred_MontoAprobado, Nullable<int> cred_DiasSolicitado, Nullable<int> cred_DiasAprobado, Nullable<int> cred_UsuarioCrea, Nullable<System.DateTime> cred_FechaCrea, Nullable<int> cred_UsuarioModifica, Nullable<System.DateTime> cred_FechaModifica)
        {
            var cred_IdParameter = cred_Id.HasValue ?
                new ObjectParameter("cred_Id", cred_Id) :
                new ObjectParameter("cred_Id", typeof(int));
    
            var escre_IdParameter = escre_Id.HasValue ?
                new ObjectParameter("escre_Id", escre_Id) :
                new ObjectParameter("escre_Id", typeof(byte));
    
            var cred_FechaAprobacionParameter = cred_FechaAprobacion.HasValue ?
                new ObjectParameter("cred_FechaAprobacion", cred_FechaAprobacion) :
                new ObjectParameter("cred_FechaAprobacion", typeof(System.DateTime));
    
            var cred_MontoSolicitadoParameter = cred_MontoSolicitado.HasValue ?
                new ObjectParameter("cred_MontoSolicitado", cred_MontoSolicitado) :
                new ObjectParameter("cred_MontoSolicitado", typeof(decimal));
    
            var cred_MontoAprobadoParameter = cred_MontoAprobado.HasValue ?
                new ObjectParameter("cred_MontoAprobado", cred_MontoAprobado) :
                new ObjectParameter("cred_MontoAprobado", typeof(decimal));
    
            var cred_DiasSolicitadoParameter = cred_DiasSolicitado.HasValue ?
                new ObjectParameter("cred_DiasSolicitado", cred_DiasSolicitado) :
                new ObjectParameter("cred_DiasSolicitado", typeof(int));
    
            var cred_DiasAprobadoParameter = cred_DiasAprobado.HasValue ?
                new ObjectParameter("cred_DiasAprobado", cred_DiasAprobado) :
                new ObjectParameter("cred_DiasAprobado", typeof(int));
    
            var cred_UsuarioCreaParameter = cred_UsuarioCrea.HasValue ?
                new ObjectParameter("cred_UsuarioCrea", cred_UsuarioCrea) :
                new ObjectParameter("cred_UsuarioCrea", typeof(int));
    
            var cred_FechaCreaParameter = cred_FechaCrea.HasValue ?
                new ObjectParameter("cred_FechaCrea", cred_FechaCrea) :
                new ObjectParameter("cred_FechaCrea", typeof(System.DateTime));
    
            var cred_UsuarioModificaParameter = cred_UsuarioModifica.HasValue ?
                new ObjectParameter("cred_UsuarioModifica", cred_UsuarioModifica) :
                new ObjectParameter("cred_UsuarioModifica", typeof(int));
    
            var cred_FechaModificaParameter = cred_FechaModifica.HasValue ?
                new ObjectParameter("cred_FechaModifica", cred_FechaModifica) :
                new ObjectParameter("cred_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudCredito_Aprobar_Result>("UDP_Vent_tbSolicitudCredito_Aprobar", cred_IdParameter, escre_IdParameter, cred_FechaAprobacionParameter, cred_MontoSolicitadoParameter, cred_MontoAprobadoParameter, cred_DiasSolicitadoParameter, cred_DiasAprobadoParameter, cred_UsuarioCreaParameter, cred_FechaCreaParameter, cred_UsuarioModificaParameter, cred_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudCredito_Denegar_Result> UDP_Vent_tbSolicitudCredito_Denegar(Nullable<int> cred_Id, Nullable<byte> escre_Id, string cred_RazonDenegacion)
        {
            var cred_IdParameter = cred_Id.HasValue ?
                new ObjectParameter("cred_Id", cred_Id) :
                new ObjectParameter("cred_Id", typeof(int));
    
            var escre_IdParameter = escre_Id.HasValue ?
                new ObjectParameter("escre_Id", escre_Id) :
                new ObjectParameter("escre_Id", typeof(byte));
    
            var cred_RazonDenegacionParameter = cred_RazonDenegacion != null ?
                new ObjectParameter("cred_RazonDenegacion", cred_RazonDenegacion) :
                new ObjectParameter("cred_RazonDenegacion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudCredito_Denegar_Result>("UDP_Vent_tbSolicitudCredito_Denegar", cred_IdParameter, escre_IdParameter, cred_RazonDenegacionParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudCredito_Estado_Result> UDP_Vent_tbSolicitudCredito_Estado(Nullable<int> cred_Id, Nullable<int> escre_Id)
        {
            var cred_IdParameter = cred_Id.HasValue ?
                new ObjectParameter("cred_Id", cred_Id) :
                new ObjectParameter("cred_Id", typeof(int));
    
            var escre_IdParameter = escre_Id.HasValue ?
                new ObjectParameter("escre_Id", escre_Id) :
                new ObjectParameter("escre_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudCredito_Estado_Result>("UDP_Vent_tbSolicitudCredito_Estado", cred_IdParameter, escre_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudCredito_Insert_Result> UDP_Vent_tbSolicitudCredito_Insert(Nullable<int> clte_Id, Nullable<byte> escre_Id, Nullable<System.DateTime> cred_FechaSolicitud, Nullable<decimal> cred_MontoSolicitado, Nullable<int> cred_DiasSolicitado, Nullable<int> cred_UsuarioCrea, Nullable<System.DateTime> cred_FechaCrea)
        {
            var clte_IdParameter = clte_Id.HasValue ?
                new ObjectParameter("clte_Id", clte_Id) :
                new ObjectParameter("clte_Id", typeof(int));
    
            var escre_IdParameter = escre_Id.HasValue ?
                new ObjectParameter("escre_Id", escre_Id) :
                new ObjectParameter("escre_Id", typeof(byte));
    
            var cred_FechaSolicitudParameter = cred_FechaSolicitud.HasValue ?
                new ObjectParameter("cred_FechaSolicitud", cred_FechaSolicitud) :
                new ObjectParameter("cred_FechaSolicitud", typeof(System.DateTime));
    
            var cred_MontoSolicitadoParameter = cred_MontoSolicitado.HasValue ?
                new ObjectParameter("cred_MontoSolicitado", cred_MontoSolicitado) :
                new ObjectParameter("cred_MontoSolicitado", typeof(decimal));
    
            var cred_DiasSolicitadoParameter = cred_DiasSolicitado.HasValue ?
                new ObjectParameter("cred_DiasSolicitado", cred_DiasSolicitado) :
                new ObjectParameter("cred_DiasSolicitado", typeof(int));
    
            var cred_UsuarioCreaParameter = cred_UsuarioCrea.HasValue ?
                new ObjectParameter("cred_UsuarioCrea", cred_UsuarioCrea) :
                new ObjectParameter("cred_UsuarioCrea", typeof(int));
    
            var cred_FechaCreaParameter = cred_FechaCrea.HasValue ?
                new ObjectParameter("cred_FechaCrea", cred_FechaCrea) :
                new ObjectParameter("cred_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudCredito_Insert_Result>("UDP_Vent_tbSolicitudCredito_Insert", clte_IdParameter, escre_IdParameter, cred_FechaSolicitudParameter, cred_MontoSolicitadoParameter, cred_DiasSolicitadoParameter, cred_UsuarioCreaParameter, cred_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudCredito_RolGerenteCreditosCobranza_Result> UDP_Vent_tbSolicitudCredito_RolGerenteCreditosCobranza(string usuarioId, string password)
        {
            var usuarioIdParameter = usuarioId != null ?
                new ObjectParameter("UsuarioId", usuarioId) :
                new ObjectParameter("UsuarioId", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudCredito_RolGerenteCreditosCobranza_Result>("UDP_Vent_tbSolicitudCredito_RolGerenteCreditosCobranza", usuarioIdParameter, passwordParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudCredito_Update_Result> UDP_Vent_tbSolicitudCredito_Update(Nullable<int> cred_Id, Nullable<int> clte_Id, Nullable<byte> escre_Id, Nullable<System.DateTime> cred_FechaSolicitud, Nullable<System.DateTime> cred_FechaAprobacion, Nullable<decimal> cred_MontoSolicitado, Nullable<decimal> cred_MontoAprobado, Nullable<int> cred_DiasSolicitado, Nullable<int> cred_DiasAprobado, Nullable<int> cred_UsuarioCrea, Nullable<System.DateTime> cred_FechaCrea, Nullable<int> cred_UsuarioModifica, Nullable<System.DateTime> cred_FechaModifica)
        {
            var cred_IdParameter = cred_Id.HasValue ?
                new ObjectParameter("cred_Id", cred_Id) :
                new ObjectParameter("cred_Id", typeof(int));
    
            var clte_IdParameter = clte_Id.HasValue ?
                new ObjectParameter("clte_Id", clte_Id) :
                new ObjectParameter("clte_Id", typeof(int));
    
            var escre_IdParameter = escre_Id.HasValue ?
                new ObjectParameter("escre_Id", escre_Id) :
                new ObjectParameter("escre_Id", typeof(byte));
    
            var cred_FechaSolicitudParameter = cred_FechaSolicitud.HasValue ?
                new ObjectParameter("cred_FechaSolicitud", cred_FechaSolicitud) :
                new ObjectParameter("cred_FechaSolicitud", typeof(System.DateTime));
    
            var cred_FechaAprobacionParameter = cred_FechaAprobacion.HasValue ?
                new ObjectParameter("cred_FechaAprobacion", cred_FechaAprobacion) :
                new ObjectParameter("cred_FechaAprobacion", typeof(System.DateTime));
    
            var cred_MontoSolicitadoParameter = cred_MontoSolicitado.HasValue ?
                new ObjectParameter("cred_MontoSolicitado", cred_MontoSolicitado) :
                new ObjectParameter("cred_MontoSolicitado", typeof(decimal));
    
            var cred_MontoAprobadoParameter = cred_MontoAprobado.HasValue ?
                new ObjectParameter("cred_MontoAprobado", cred_MontoAprobado) :
                new ObjectParameter("cred_MontoAprobado", typeof(decimal));
    
            var cred_DiasSolicitadoParameter = cred_DiasSolicitado.HasValue ?
                new ObjectParameter("cred_DiasSolicitado", cred_DiasSolicitado) :
                new ObjectParameter("cred_DiasSolicitado", typeof(int));
    
            var cred_DiasAprobadoParameter = cred_DiasAprobado.HasValue ?
                new ObjectParameter("cred_DiasAprobado", cred_DiasAprobado) :
                new ObjectParameter("cred_DiasAprobado", typeof(int));
    
            var cred_UsuarioCreaParameter = cred_UsuarioCrea.HasValue ?
                new ObjectParameter("cred_UsuarioCrea", cred_UsuarioCrea) :
                new ObjectParameter("cred_UsuarioCrea", typeof(int));
    
            var cred_FechaCreaParameter = cred_FechaCrea.HasValue ?
                new ObjectParameter("cred_FechaCrea", cred_FechaCrea) :
                new ObjectParameter("cred_FechaCrea", typeof(System.DateTime));
    
            var cred_UsuarioModificaParameter = cred_UsuarioModifica.HasValue ?
                new ObjectParameter("cred_UsuarioModifica", cred_UsuarioModifica) :
                new ObjectParameter("cred_UsuarioModifica", typeof(int));
    
            var cred_FechaModificaParameter = cred_FechaModifica.HasValue ?
                new ObjectParameter("cred_FechaModifica", cred_FechaModifica) :
                new ObjectParameter("cred_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudCredito_Update_Result>("UDP_Vent_tbSolicitudCredito_Update", cred_IdParameter, clte_IdParameter, escre_IdParameter, cred_FechaSolicitudParameter, cred_FechaAprobacionParameter, cred_MontoSolicitadoParameter, cred_MontoAprobadoParameter, cred_DiasSolicitadoParameter, cred_DiasAprobadoParameter, cred_UsuarioCreaParameter, cred_FechaCreaParameter, cred_UsuarioModificaParameter, cred_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudEfectivo_Apertura_Insert_Result> UDP_Vent_tbSolicitudEfectivo_Apertura_Insert(Nullable<int> mocja_Id, Nullable<bool> solef_EsApertura, Nullable<System.DateTime> solef_FechaEntrega, Nullable<int> solef_UsuarioEntrega, Nullable<short> mnda_Id, Nullable<bool> solef_EsAnulada, Nullable<int> solef_UsuarioCrea, Nullable<System.DateTime> solef_FechaCrea)
        {
            var mocja_IdParameter = mocja_Id.HasValue ?
                new ObjectParameter("mocja_Id", mocja_Id) :
                new ObjectParameter("mocja_Id", typeof(int));
    
            var solef_EsAperturaParameter = solef_EsApertura.HasValue ?
                new ObjectParameter("solef_EsApertura", solef_EsApertura) :
                new ObjectParameter("solef_EsApertura", typeof(bool));
    
            var solef_FechaEntregaParameter = solef_FechaEntrega.HasValue ?
                new ObjectParameter("solef_FechaEntrega", solef_FechaEntrega) :
                new ObjectParameter("solef_FechaEntrega", typeof(System.DateTime));
    
            var solef_UsuarioEntregaParameter = solef_UsuarioEntrega.HasValue ?
                new ObjectParameter("solef_UsuarioEntrega", solef_UsuarioEntrega) :
                new ObjectParameter("solef_UsuarioEntrega", typeof(int));
    
            var mnda_IdParameter = mnda_Id.HasValue ?
                new ObjectParameter("mnda_Id", mnda_Id) :
                new ObjectParameter("mnda_Id", typeof(short));
    
            var solef_EsAnuladaParameter = solef_EsAnulada.HasValue ?
                new ObjectParameter("solef_EsAnulada", solef_EsAnulada) :
                new ObjectParameter("solef_EsAnulada", typeof(bool));
    
            var solef_UsuarioCreaParameter = solef_UsuarioCrea.HasValue ?
                new ObjectParameter("solef_UsuarioCrea", solef_UsuarioCrea) :
                new ObjectParameter("solef_UsuarioCrea", typeof(int));
    
            var solef_FechaCreaParameter = solef_FechaCrea.HasValue ?
                new ObjectParameter("solef_FechaCrea", solef_FechaCrea) :
                new ObjectParameter("solef_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudEfectivo_Apertura_Insert_Result>("UDP_Vent_tbSolicitudEfectivo_Apertura_Insert", mocja_IdParameter, solef_EsAperturaParameter, solef_FechaEntregaParameter, solef_UsuarioEntregaParameter, mnda_IdParameter, solef_EsAnuladaParameter, solef_UsuarioCreaParameter, solef_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudEfectivo_DatosEncabezado_Result> UDP_Vent_tbSolicitudEfectivo_DatosEncabezado(Nullable<int> uSER_ID)
        {
            var uSER_IDParameter = uSER_ID.HasValue ?
                new ObjectParameter("USER_ID", uSER_ID) :
                new ObjectParameter("USER_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudEfectivo_DatosEncabezado_Result>("UDP_Vent_tbSolicitudEfectivo_DatosEncabezado", uSER_IDParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudEfectivo_Details_Result> UDP_Vent_tbSolicitudEfectivo_Details(Nullable<int> iDSOLICITUD)
        {
            var iDSOLICITUDParameter = iDSOLICITUD.HasValue ?
                new ObjectParameter("IDSOLICITUD", iDSOLICITUD) :
                new ObjectParameter("IDSOLICITUD", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudEfectivo_Details_Result>("UDP_Vent_tbSolicitudEfectivo_Details", iDSOLICITUDParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudEfectivo_EsAnulada_Result> UDP_Vent_tbSolicitudEfectivo_EsAnulada(Nullable<int> solef_Id, Nullable<bool> solef_EsAnulada, string motivo)
        {
            var solef_IdParameter = solef_Id.HasValue ?
                new ObjectParameter("solef_Id", solef_Id) :
                new ObjectParameter("solef_Id", typeof(int));
    
            var solef_EsAnuladaParameter = solef_EsAnulada.HasValue ?
                new ObjectParameter("solef_EsAnulada", solef_EsAnulada) :
                new ObjectParameter("solef_EsAnulada", typeof(bool));
    
            var motivoParameter = motivo != null ?
                new ObjectParameter("Motivo", motivo) :
                new ObjectParameter("Motivo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudEfectivo_EsAnulada_Result>("UDP_Vent_tbSolicitudEfectivo_EsAnulada", solef_IdParameter, solef_EsAnuladaParameter, motivoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudEfectivo_EsImpresa_Result> UDP_Vent_tbSolicitudEfectivo_EsImpresa(Nullable<int> solef_Id, Nullable<bool> solef_EsImpresa)
        {
            var solef_IdParameter = solef_Id.HasValue ?
                new ObjectParameter("solef_Id", solef_Id) :
                new ObjectParameter("solef_Id", typeof(int));
    
            var solef_EsImpresaParameter = solef_EsImpresa.HasValue ?
                new ObjectParameter("solef_EsImpresa", solef_EsImpresa) :
                new ObjectParameter("solef_EsImpresa", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudEfectivo_EsImpresa_Result>("UDP_Vent_tbSolicitudEfectivo_EsImpresa", solef_IdParameter, solef_EsImpresaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudEfectivo_Imprimir_Result> UDP_Vent_tbSolicitudEfectivo_Imprimir(Nullable<int> sOLEF_ID)
        {
            var sOLEF_IDParameter = sOLEF_ID.HasValue ?
                new ObjectParameter("SOLEF_ID", sOLEF_ID) :
                new ObjectParameter("SOLEF_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudEfectivo_Imprimir_Result>("UDP_Vent_tbSolicitudEfectivo_Imprimir", sOLEF_IDParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudEfectivo_Insert_Result> UDP_Vent_tbSolicitudEfectivo_Insert(Nullable<int> mocja_Id, Nullable<short> mnda_Id, Nullable<int> solef_UsuarioCrea, Nullable<System.DateTime> solef_FechaCrea)
        {
            var mocja_IdParameter = mocja_Id.HasValue ?
                new ObjectParameter("mocja_Id", mocja_Id) :
                new ObjectParameter("mocja_Id", typeof(int));
    
            var mnda_IdParameter = mnda_Id.HasValue ?
                new ObjectParameter("mnda_Id", mnda_Id) :
                new ObjectParameter("mnda_Id", typeof(short));
    
            var solef_UsuarioCreaParameter = solef_UsuarioCrea.HasValue ?
                new ObjectParameter("solef_UsuarioCrea", solef_UsuarioCrea) :
                new ObjectParameter("solef_UsuarioCrea", typeof(int));
    
            var solef_FechaCreaParameter = solef_FechaCrea.HasValue ?
                new ObjectParameter("solef_FechaCrea", solef_FechaCrea) :
                new ObjectParameter("solef_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudEfectivo_Insert_Result>("UDP_Vent_tbSolicitudEfectivo_Insert", mocja_IdParameter, mnda_IdParameter, solef_UsuarioCreaParameter, solef_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudEfectivo_Recargo_Result> UDP_Vent_tbSolicitudEfectivo_Recargo(Nullable<int> uSER_ID)
        {
            var uSER_IDParameter = uSER_ID.HasValue ?
                new ObjectParameter("USER_ID", uSER_ID) :
                new ObjectParameter("USER_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudEfectivo_Recargo_Result>("UDP_Vent_tbSolicitudEfectivo_Recargo", uSER_IDParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudEfectivo_Update_Result> UDP_Vent_tbSolicitudEfectivo_Update(Nullable<int> solef_Id, Nullable<int> mocja_Id, Nullable<bool> solef_EsApertura, Nullable<short> mnda_Id, Nullable<bool> solef_EsAnulada, Nullable<int> solef_UsuarioCrea, Nullable<System.DateTime> solef_FechaCrea, Nullable<int> solef_UsuarioModifica, Nullable<System.DateTime> solef_FechaModifica)
        {
            var solef_IdParameter = solef_Id.HasValue ?
                new ObjectParameter("solef_Id", solef_Id) :
                new ObjectParameter("solef_Id", typeof(int));
    
            var mocja_IdParameter = mocja_Id.HasValue ?
                new ObjectParameter("mocja_Id", mocja_Id) :
                new ObjectParameter("mocja_Id", typeof(int));
    
            var solef_EsAperturaParameter = solef_EsApertura.HasValue ?
                new ObjectParameter("solef_EsApertura", solef_EsApertura) :
                new ObjectParameter("solef_EsApertura", typeof(bool));
    
            var mnda_IdParameter = mnda_Id.HasValue ?
                new ObjectParameter("mnda_Id", mnda_Id) :
                new ObjectParameter("mnda_Id", typeof(short));
    
            var solef_EsAnuladaParameter = solef_EsAnulada.HasValue ?
                new ObjectParameter("solef_EsAnulada", solef_EsAnulada) :
                new ObjectParameter("solef_EsAnulada", typeof(bool));
    
            var solef_UsuarioCreaParameter = solef_UsuarioCrea.HasValue ?
                new ObjectParameter("solef_UsuarioCrea", solef_UsuarioCrea) :
                new ObjectParameter("solef_UsuarioCrea", typeof(int));
    
            var solef_FechaCreaParameter = solef_FechaCrea.HasValue ?
                new ObjectParameter("solef_FechaCrea", solef_FechaCrea) :
                new ObjectParameter("solef_FechaCrea", typeof(System.DateTime));
    
            var solef_UsuarioModificaParameter = solef_UsuarioModifica.HasValue ?
                new ObjectParameter("solef_UsuarioModifica", solef_UsuarioModifica) :
                new ObjectParameter("solef_UsuarioModifica", typeof(int));
    
            var solef_FechaModificaParameter = solef_FechaModifica.HasValue ?
                new ObjectParameter("solef_FechaModifica", solef_FechaModifica) :
                new ObjectParameter("solef_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudEfectivo_Update_Result>("UDP_Vent_tbSolicitudEfectivo_Update", solef_IdParameter, mocja_IdParameter, solef_EsAperturaParameter, mnda_IdParameter, solef_EsAnuladaParameter, solef_UsuarioCreaParameter, solef_FechaCreaParameter, solef_UsuarioModificaParameter, solef_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudEfectivo_Update_Entrega_Result> UDP_Vent_tbSolicitudEfectivo_Update_Entrega(Nullable<int> solef_Id, Nullable<int> mocja_Id, Nullable<bool> solef_EsApertura, Nullable<System.DateTime> solef_FechaEntrega, Nullable<int> solef_UsuarioEntrega, Nullable<short> mnda_Id, Nullable<bool> solef_EsAnulada)
        {
            var solef_IdParameter = solef_Id.HasValue ?
                new ObjectParameter("solef_Id", solef_Id) :
                new ObjectParameter("solef_Id", typeof(int));
    
            var mocja_IdParameter = mocja_Id.HasValue ?
                new ObjectParameter("mocja_Id", mocja_Id) :
                new ObjectParameter("mocja_Id", typeof(int));
    
            var solef_EsAperturaParameter = solef_EsApertura.HasValue ?
                new ObjectParameter("solef_EsApertura", solef_EsApertura) :
                new ObjectParameter("solef_EsApertura", typeof(bool));
    
            var solef_FechaEntregaParameter = solef_FechaEntrega.HasValue ?
                new ObjectParameter("solef_FechaEntrega", solef_FechaEntrega) :
                new ObjectParameter("solef_FechaEntrega", typeof(System.DateTime));
    
            var solef_UsuarioEntregaParameter = solef_UsuarioEntrega.HasValue ?
                new ObjectParameter("solef_UsuarioEntrega", solef_UsuarioEntrega) :
                new ObjectParameter("solef_UsuarioEntrega", typeof(int));
    
            var mnda_IdParameter = mnda_Id.HasValue ?
                new ObjectParameter("mnda_Id", mnda_Id) :
                new ObjectParameter("mnda_Id", typeof(short));
    
            var solef_EsAnuladaParameter = solef_EsAnulada.HasValue ?
                new ObjectParameter("solef_EsAnulada", solef_EsAnulada) :
                new ObjectParameter("solef_EsAnulada", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudEfectivo_Update_Entrega_Result>("UDP_Vent_tbSolicitudEfectivo_Update_Entrega", solef_IdParameter, mocja_IdParameter, solef_EsAperturaParameter, solef_FechaEntregaParameter, solef_UsuarioEntregaParameter, mnda_IdParameter, solef_EsAnuladaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudEfectivoDetalle_Apertura_Insert_Result> UDP_Vent_tbSolicitudEfectivoDetalle_Apertura_Insert(Nullable<int> solef_Id, Nullable<short> deno_Id, Nullable<short> soled_CantidadSolicitada, Nullable<short> soled_CantidadEntregada, Nullable<decimal> soled_MontoEntregado, Nullable<int> soled_UsuarioCrea, Nullable<System.DateTime> soled_FechaCrea)
        {
            var solef_IdParameter = solef_Id.HasValue ?
                new ObjectParameter("solef_Id", solef_Id) :
                new ObjectParameter("solef_Id", typeof(int));
    
            var deno_IdParameter = deno_Id.HasValue ?
                new ObjectParameter("deno_Id", deno_Id) :
                new ObjectParameter("deno_Id", typeof(short));
    
            var soled_CantidadSolicitadaParameter = soled_CantidadSolicitada.HasValue ?
                new ObjectParameter("soled_CantidadSolicitada", soled_CantidadSolicitada) :
                new ObjectParameter("soled_CantidadSolicitada", typeof(short));
    
            var soled_CantidadEntregadaParameter = soled_CantidadEntregada.HasValue ?
                new ObjectParameter("soled_CantidadEntregada", soled_CantidadEntregada) :
                new ObjectParameter("soled_CantidadEntregada", typeof(short));
    
            var soled_MontoEntregadoParameter = soled_MontoEntregado.HasValue ?
                new ObjectParameter("soled_MontoEntregado", soled_MontoEntregado) :
                new ObjectParameter("soled_MontoEntregado", typeof(decimal));
    
            var soled_UsuarioCreaParameter = soled_UsuarioCrea.HasValue ?
                new ObjectParameter("soled_UsuarioCrea", soled_UsuarioCrea) :
                new ObjectParameter("soled_UsuarioCrea", typeof(int));
    
            var soled_FechaCreaParameter = soled_FechaCrea.HasValue ?
                new ObjectParameter("soled_FechaCrea", soled_FechaCrea) :
                new ObjectParameter("soled_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudEfectivoDetalle_Apertura_Insert_Result>("UDP_Vent_tbSolicitudEfectivoDetalle_Apertura_Insert", solef_IdParameter, deno_IdParameter, soled_CantidadSolicitadaParameter, soled_CantidadEntregadaParameter, soled_MontoEntregadoParameter, soled_UsuarioCreaParameter, soled_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudEfectivoDetalle_CantidadSolicitada_Update_Result> UDP_Vent_tbSolicitudEfectivoDetalle_CantidadSolicitada_Update(Nullable<int> soled_Id, Nullable<short> deno_Id, Nullable<short> soled_CantidadSolicitada, Nullable<short> soled_CantidadEntregada, Nullable<decimal> soled_MontoEntregado)
        {
            var soled_IdParameter = soled_Id.HasValue ?
                new ObjectParameter("soled_Id", soled_Id) :
                new ObjectParameter("soled_Id", typeof(int));
    
            var deno_IdParameter = deno_Id.HasValue ?
                new ObjectParameter("deno_Id", deno_Id) :
                new ObjectParameter("deno_Id", typeof(short));
    
            var soled_CantidadSolicitadaParameter = soled_CantidadSolicitada.HasValue ?
                new ObjectParameter("soled_CantidadSolicitada", soled_CantidadSolicitada) :
                new ObjectParameter("soled_CantidadSolicitada", typeof(short));
    
            var soled_CantidadEntregadaParameter = soled_CantidadEntregada.HasValue ?
                new ObjectParameter("soled_CantidadEntregada", soled_CantidadEntregada) :
                new ObjectParameter("soled_CantidadEntregada", typeof(short));
    
            var soled_MontoEntregadoParameter = soled_MontoEntregado.HasValue ?
                new ObjectParameter("soled_MontoEntregado", soled_MontoEntregado) :
                new ObjectParameter("soled_MontoEntregado", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudEfectivoDetalle_CantidadSolicitada_Update_Result>("UDP_Vent_tbSolicitudEfectivoDetalle_CantidadSolicitada_Update", soled_IdParameter, deno_IdParameter, soled_CantidadSolicitadaParameter, soled_CantidadEntregadaParameter, soled_MontoEntregadoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudEfectivoDetalle_Detalle_Result> UDP_Vent_tbSolicitudEfectivoDetalle_Detalle(Nullable<int> iDSOLICITUD)
        {
            var iDSOLICITUDParameter = iDSOLICITUD.HasValue ?
                new ObjectParameter("IDSOLICITUD", iDSOLICITUD) :
                new ObjectParameter("IDSOLICITUD", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudEfectivoDetalle_Detalle_Result>("UDP_Vent_tbSolicitudEfectivoDetalle_Detalle", iDSOLICITUDParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudEfectivoDetalle_Insert_Result> UDP_Vent_tbSolicitudEfectivoDetalle_Insert(Nullable<int> solef_Id, Nullable<short> deno_Id, Nullable<short> soled_CantidadSolicitada, Nullable<int> soled_UsuarioCrea, Nullable<System.DateTime> soled_FechaCrea)
        {
            var solef_IdParameter = solef_Id.HasValue ?
                new ObjectParameter("solef_Id", solef_Id) :
                new ObjectParameter("solef_Id", typeof(int));
    
            var deno_IdParameter = deno_Id.HasValue ?
                new ObjectParameter("deno_Id", deno_Id) :
                new ObjectParameter("deno_Id", typeof(short));
    
            var soled_CantidadSolicitadaParameter = soled_CantidadSolicitada.HasValue ?
                new ObjectParameter("soled_CantidadSolicitada", soled_CantidadSolicitada) :
                new ObjectParameter("soled_CantidadSolicitada", typeof(short));
    
            var soled_UsuarioCreaParameter = soled_UsuarioCrea.HasValue ?
                new ObjectParameter("soled_UsuarioCrea", soled_UsuarioCrea) :
                new ObjectParameter("soled_UsuarioCrea", typeof(int));
    
            var soled_FechaCreaParameter = soled_FechaCrea.HasValue ?
                new ObjectParameter("soled_FechaCrea", soled_FechaCrea) :
                new ObjectParameter("soled_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudEfectivoDetalle_Insert_Result>("UDP_Vent_tbSolicitudEfectivoDetalle_Insert", solef_IdParameter, deno_IdParameter, soled_CantidadSolicitadaParameter, soled_UsuarioCreaParameter, soled_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudEfectivoDetalle_Select_Result> UDP_Vent_tbSolicitudEfectivoDetalle_Select()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudEfectivoDetalle_Select_Result>("UDP_Vent_tbSolicitudEfectivoDetalle_Select");
        }
    
        public virtual ObjectResult<UDP_Vent_tbSolicitudEfectivoDetalle_Update_Result> UDP_Vent_tbSolicitudEfectivoDetalle_Update(Nullable<int> soled_Id, Nullable<short> deno_Id, Nullable<short> soled_CantidadSolicitada, Nullable<short> soled_CantidadEntregada, Nullable<decimal> soled_MontoEntregado, Nullable<int> soled_UsuarioCrea, Nullable<System.DateTime> soled_FechaCrea, Nullable<int> soled_UsuarioModifica, Nullable<System.DateTime> soled_FechaModifica)
        {
            var soled_IdParameter = soled_Id.HasValue ?
                new ObjectParameter("soled_Id", soled_Id) :
                new ObjectParameter("soled_Id", typeof(int));
    
            var deno_IdParameter = deno_Id.HasValue ?
                new ObjectParameter("deno_Id", deno_Id) :
                new ObjectParameter("deno_Id", typeof(short));
    
            var soled_CantidadSolicitadaParameter = soled_CantidadSolicitada.HasValue ?
                new ObjectParameter("soled_CantidadSolicitada", soled_CantidadSolicitada) :
                new ObjectParameter("soled_CantidadSolicitada", typeof(short));
    
            var soled_CantidadEntregadaParameter = soled_CantidadEntregada.HasValue ?
                new ObjectParameter("soled_CantidadEntregada", soled_CantidadEntregada) :
                new ObjectParameter("soled_CantidadEntregada", typeof(short));
    
            var soled_MontoEntregadoParameter = soled_MontoEntregado.HasValue ?
                new ObjectParameter("soled_MontoEntregado", soled_MontoEntregado) :
                new ObjectParameter("soled_MontoEntregado", typeof(decimal));
    
            var soled_UsuarioCreaParameter = soled_UsuarioCrea.HasValue ?
                new ObjectParameter("soled_UsuarioCrea", soled_UsuarioCrea) :
                new ObjectParameter("soled_UsuarioCrea", typeof(int));
    
            var soled_FechaCreaParameter = soled_FechaCrea.HasValue ?
                new ObjectParameter("soled_FechaCrea", soled_FechaCrea) :
                new ObjectParameter("soled_FechaCrea", typeof(System.DateTime));
    
            var soled_UsuarioModificaParameter = soled_UsuarioModifica.HasValue ?
                new ObjectParameter("soled_UsuarioModifica", soled_UsuarioModifica) :
                new ObjectParameter("soled_UsuarioModifica", typeof(int));
    
            var soled_FechaModificaParameter = soled_FechaModifica.HasValue ?
                new ObjectParameter("soled_FechaModifica", soled_FechaModifica) :
                new ObjectParameter("soled_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSolicitudEfectivoDetalle_Update_Result>("UDP_Vent_tbSolicitudEfectivoDetalle_Update", soled_IdParameter, deno_IdParameter, soled_CantidadSolicitadaParameter, soled_CantidadEntregadaParameter, soled_MontoEntregadoParameter, soled_UsuarioCreaParameter, soled_FechaCreaParameter, soled_UsuarioModificaParameter, soled_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSucursal_Insert_Result> UDP_Vent_tbSucursal_Insert(string mun_Codigo, Nullable<int> bod_Id, Nullable<int> pemi_Id, string suc_Descripcion, string suc_Correo, string suc_Direccion, string suc_Telefono, Nullable<int> suc_UsuarioCrea, Nullable<System.DateTime> suc_FechaCrea)
        {
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var pemi_IdParameter = pemi_Id.HasValue ?
                new ObjectParameter("pemi_Id", pemi_Id) :
                new ObjectParameter("pemi_Id", typeof(int));
    
            var suc_DescripcionParameter = suc_Descripcion != null ?
                new ObjectParameter("suc_Descripcion", suc_Descripcion) :
                new ObjectParameter("suc_Descripcion", typeof(string));
    
            var suc_CorreoParameter = suc_Correo != null ?
                new ObjectParameter("suc_Correo", suc_Correo) :
                new ObjectParameter("suc_Correo", typeof(string));
    
            var suc_DireccionParameter = suc_Direccion != null ?
                new ObjectParameter("suc_Direccion", suc_Direccion) :
                new ObjectParameter("suc_Direccion", typeof(string));
    
            var suc_TelefonoParameter = suc_Telefono != null ?
                new ObjectParameter("suc_Telefono", suc_Telefono) :
                new ObjectParameter("suc_Telefono", typeof(string));
    
            var suc_UsuarioCreaParameter = suc_UsuarioCrea.HasValue ?
                new ObjectParameter("suc_UsuarioCrea", suc_UsuarioCrea) :
                new ObjectParameter("suc_UsuarioCrea", typeof(int));
    
            var suc_FechaCreaParameter = suc_FechaCrea.HasValue ?
                new ObjectParameter("suc_FechaCrea", suc_FechaCrea) :
                new ObjectParameter("suc_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSucursal_Insert_Result>("UDP_Vent_tbSucursal_Insert", mun_CodigoParameter, bod_IdParameter, pemi_IdParameter, suc_DescripcionParameter, suc_CorreoParameter, suc_DireccionParameter, suc_TelefonoParameter, suc_UsuarioCreaParameter, suc_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbSucursal_Update_Result> UDP_Vent_tbSucursal_Update(Nullable<int> suc_Id, string mun_Codigo, Nullable<int> bod_Id, Nullable<int> pemi_Id, string suc_Descripcion, string suc_Correo, string suc_Direccion, string suc_Telefono, Nullable<int> suc_UsuarioCrea, Nullable<System.DateTime> suc_FechaCrea, Nullable<int> suc_UsuarioModifica, Nullable<System.DateTime> suc_FechaModifica)
        {
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            var bod_IdParameter = bod_Id.HasValue ?
                new ObjectParameter("bod_Id", bod_Id) :
                new ObjectParameter("bod_Id", typeof(int));
    
            var pemi_IdParameter = pemi_Id.HasValue ?
                new ObjectParameter("pemi_Id", pemi_Id) :
                new ObjectParameter("pemi_Id", typeof(int));
    
            var suc_DescripcionParameter = suc_Descripcion != null ?
                new ObjectParameter("suc_Descripcion", suc_Descripcion) :
                new ObjectParameter("suc_Descripcion", typeof(string));
    
            var suc_CorreoParameter = suc_Correo != null ?
                new ObjectParameter("suc_Correo", suc_Correo) :
                new ObjectParameter("suc_Correo", typeof(string));
    
            var suc_DireccionParameter = suc_Direccion != null ?
                new ObjectParameter("suc_Direccion", suc_Direccion) :
                new ObjectParameter("suc_Direccion", typeof(string));
    
            var suc_TelefonoParameter = suc_Telefono != null ?
                new ObjectParameter("suc_Telefono", suc_Telefono) :
                new ObjectParameter("suc_Telefono", typeof(string));
    
            var suc_UsuarioCreaParameter = suc_UsuarioCrea.HasValue ?
                new ObjectParameter("suc_UsuarioCrea", suc_UsuarioCrea) :
                new ObjectParameter("suc_UsuarioCrea", typeof(int));
    
            var suc_FechaCreaParameter = suc_FechaCrea.HasValue ?
                new ObjectParameter("suc_FechaCrea", suc_FechaCrea) :
                new ObjectParameter("suc_FechaCrea", typeof(System.DateTime));
    
            var suc_UsuarioModificaParameter = suc_UsuarioModifica.HasValue ?
                new ObjectParameter("suc_UsuarioModifica", suc_UsuarioModifica) :
                new ObjectParameter("suc_UsuarioModifica", typeof(int));
    
            var suc_FechaModificaParameter = suc_FechaModifica.HasValue ?
                new ObjectParameter("suc_FechaModifica", suc_FechaModifica) :
                new ObjectParameter("suc_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbSucursal_Update_Result>("UDP_Vent_tbSucursal_Update", suc_IdParameter, mun_CodigoParameter, bod_IdParameter, pemi_IdParameter, suc_DescripcionParameter, suc_CorreoParameter, suc_DireccionParameter, suc_TelefonoParameter, suc_UsuarioCreaParameter, suc_FechaCreaParameter, suc_UsuarioModificaParameter, suc_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbTipoPago_Insert_Result> UDP_Vent_tbTipoPago_Insert(string tpa_Descripcion, Nullable<bool> tpa_Emisor, Nullable<bool> tpa_Cuenta, Nullable<bool> tpa_FechaVencimiento, Nullable<bool> tpa_Titular, Nullable<int> tpa_UsuarioCrea, Nullable<System.DateTime> tpa_FechaCrea)
        {
            var tpa_DescripcionParameter = tpa_Descripcion != null ?
                new ObjectParameter("tpa_Descripcion", tpa_Descripcion) :
                new ObjectParameter("tpa_Descripcion", typeof(string));
    
            var tpa_EmisorParameter = tpa_Emisor.HasValue ?
                new ObjectParameter("tpa_Emisor", tpa_Emisor) :
                new ObjectParameter("tpa_Emisor", typeof(bool));
    
            var tpa_CuentaParameter = tpa_Cuenta.HasValue ?
                new ObjectParameter("tpa_Cuenta", tpa_Cuenta) :
                new ObjectParameter("tpa_Cuenta", typeof(bool));
    
            var tpa_FechaVencimientoParameter = tpa_FechaVencimiento.HasValue ?
                new ObjectParameter("tpa_FechaVencimiento", tpa_FechaVencimiento) :
                new ObjectParameter("tpa_FechaVencimiento", typeof(bool));
    
            var tpa_TitularParameter = tpa_Titular.HasValue ?
                new ObjectParameter("tpa_Titular", tpa_Titular) :
                new ObjectParameter("tpa_Titular", typeof(bool));
    
            var tpa_UsuarioCreaParameter = tpa_UsuarioCrea.HasValue ?
                new ObjectParameter("tpa_UsuarioCrea", tpa_UsuarioCrea) :
                new ObjectParameter("tpa_UsuarioCrea", typeof(int));
    
            var tpa_FechaCreaParameter = tpa_FechaCrea.HasValue ?
                new ObjectParameter("tpa_FechaCrea", tpa_FechaCrea) :
                new ObjectParameter("tpa_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbTipoPago_Insert_Result>("UDP_Vent_tbTipoPago_Insert", tpa_DescripcionParameter, tpa_EmisorParameter, tpa_CuentaParameter, tpa_FechaVencimientoParameter, tpa_TitularParameter, tpa_UsuarioCreaParameter, tpa_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbTipoPago_Update_Result> UDP_Vent_tbTipoPago_Update(Nullable<short> tpa_Id, string tpa_Descripcion, Nullable<bool> tpa_Emisor, Nullable<bool> tpa_Cuenta, Nullable<bool> tpa_FechaVencimiento, Nullable<bool> tpa_Titular, Nullable<int> tpa_UsuarioCrea, Nullable<System.DateTime> tpa_FechaCrea, Nullable<int> tpa_UsuarioModifica, Nullable<System.DateTime> tpa_FechaModifica)
        {
            var tpa_IdParameter = tpa_Id.HasValue ?
                new ObjectParameter("tpa_Id", tpa_Id) :
                new ObjectParameter("tpa_Id", typeof(short));
    
            var tpa_DescripcionParameter = tpa_Descripcion != null ?
                new ObjectParameter("tpa_Descripcion", tpa_Descripcion) :
                new ObjectParameter("tpa_Descripcion", typeof(string));
    
            var tpa_EmisorParameter = tpa_Emisor.HasValue ?
                new ObjectParameter("tpa_Emisor", tpa_Emisor) :
                new ObjectParameter("tpa_Emisor", typeof(bool));
    
            var tpa_CuentaParameter = tpa_Cuenta.HasValue ?
                new ObjectParameter("tpa_Cuenta", tpa_Cuenta) :
                new ObjectParameter("tpa_Cuenta", typeof(bool));
    
            var tpa_FechaVencimientoParameter = tpa_FechaVencimiento.HasValue ?
                new ObjectParameter("tpa_FechaVencimiento", tpa_FechaVencimiento) :
                new ObjectParameter("tpa_FechaVencimiento", typeof(bool));
    
            var tpa_TitularParameter = tpa_Titular.HasValue ?
                new ObjectParameter("tpa_Titular", tpa_Titular) :
                new ObjectParameter("tpa_Titular", typeof(bool));
    
            var tpa_UsuarioCreaParameter = tpa_UsuarioCrea.HasValue ?
                new ObjectParameter("tpa_UsuarioCrea", tpa_UsuarioCrea) :
                new ObjectParameter("tpa_UsuarioCrea", typeof(int));
    
            var tpa_FechaCreaParameter = tpa_FechaCrea.HasValue ?
                new ObjectParameter("tpa_FechaCrea", tpa_FechaCrea) :
                new ObjectParameter("tpa_FechaCrea", typeof(System.DateTime));
    
            var tpa_UsuarioModificaParameter = tpa_UsuarioModifica.HasValue ?
                new ObjectParameter("tpa_UsuarioModifica", tpa_UsuarioModifica) :
                new ObjectParameter("tpa_UsuarioModifica", typeof(int));
    
            var tpa_FechaModificaParameter = tpa_FechaModifica.HasValue ?
                new ObjectParameter("tpa_FechaModifica", tpa_FechaModifica) :
                new ObjectParameter("tpa_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbTipoPago_Update_Result>("UDP_Vent_tbTipoPago_Update", tpa_IdParameter, tpa_DescripcionParameter, tpa_EmisorParameter, tpa_CuentaParameter, tpa_FechaVencimientoParameter, tpa_TitularParameter, tpa_UsuarioCreaParameter, tpa_FechaCreaParameter, tpa_UsuarioModificaParameter, tpa_FechaModificaParameter);
        }
    
        public virtual int UDP_Vent_tbTipoTaller_Estado(Nullable<int> tptal_Id, Nullable<bool> tptal_Es_Activo)
        {
            var tptal_IdParameter = tptal_Id.HasValue ?
                new ObjectParameter("tptal_Id", tptal_Id) :
                new ObjectParameter("tptal_Id", typeof(int));
    
            var tptal_Es_ActivoParameter = tptal_Es_Activo.HasValue ?
                new ObjectParameter("tptal_Es_Activo", tptal_Es_Activo) :
                new ObjectParameter("tptal_Es_Activo", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UDP_Vent_tbTipoTaller_Estado", tptal_IdParameter, tptal_Es_ActivoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_tbUsuario_Rol_Apertura_Result> UDP_Vent_tbUsuario_Rol_Apertura(Nullable<int> suc_Id, Nullable<int> cAJERO)
        {
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var cAJEROParameter = cAJERO.HasValue ?
                new ObjectParameter("CAJERO", cAJERO) :
                new ObjectParameter("CAJERO", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_tbUsuario_Rol_Apertura_Result>("UDP_Vent_tbUsuario_Rol_Apertura", suc_IdParameter, cAJEROParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_VentasExoneradas_Result> UDP_Vent_VentasExoneradas(string fact_Codigo)
        {
            var fact_CodigoParameter = fact_Codigo != null ?
                new ObjectParameter("fact_Codigo", fact_Codigo) :
                new ObjectParameter("fact_Codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_VentasExoneradas_Result>("UDP_Vent_VentasExoneradas", fact_CodigoParameter);
        }
    
        public virtual ObjectResult<UDP_Vent_vFacturaPago_Result> UDP_Vent_vFacturaPago(Nullable<int> clte_Id)
        {
            var clte_IdParameter = clte_Id.HasValue ?
                new ObjectParameter("clte_Id", clte_Id) :
                new ObjectParameter("clte_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Vent_vFacturaPago_Result>("UDP_Vent_vFacturaPago", clte_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_CatalogoDeduccionesEdit_Select_Result> UDP_Plani_CatalogoDeduccionesEdit_Select(Nullable<int> cpla_IdPlanilla)
        {
            var cpla_IdPlanillaParameter = cpla_IdPlanilla.HasValue ?
                new ObjectParameter("cpla_IdPlanilla", cpla_IdPlanilla) :
                new ObjectParameter("cpla_IdPlanilla", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_CatalogoDeduccionesEdit_Select_Result>("UDP_Plani_CatalogoDeduccionesEdit_Select", cpla_IdPlanillaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_CatalogoDeIngresosEdit_Select_Result> UDP_Plani_CatalogoDeIngresosEdit_Select(Nullable<int> cpla_IdPlanilla)
        {
            var cpla_IdPlanillaParameter = cpla_IdPlanilla.HasValue ?
                new ObjectParameter("cpla_IdPlanilla", cpla_IdPlanilla) :
                new ObjectParameter("cpla_IdPlanilla", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_CatalogoDeIngresosEdit_Select_Result>("UDP_Plani_CatalogoDeIngresosEdit_Select", cpla_IdPlanillaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_EmpleadoComisiones_Activar_Result> UDP_Plani_EmpleadoComisiones_Activar(Nullable<int> cc_Id, Nullable<int> cc_UsuarioModifica, Nullable<System.DateTime> cc_FechaModifcia)
        {
            var cc_IdParameter = cc_Id.HasValue ?
                new ObjectParameter("cc_Id", cc_Id) :
                new ObjectParameter("cc_Id", typeof(int));
    
            var cc_UsuarioModificaParameter = cc_UsuarioModifica.HasValue ?
                new ObjectParameter("cc_UsuarioModifica", cc_UsuarioModifica) :
                new ObjectParameter("cc_UsuarioModifica", typeof(int));
    
            var cc_FechaModifciaParameter = cc_FechaModifcia.HasValue ?
                new ObjectParameter("cc_FechaModifcia", cc_FechaModifcia) :
                new ObjectParameter("cc_FechaModifcia", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_EmpleadoComisiones_Activar_Result>("UDP_Plani_EmpleadoComisiones_Activar", cc_IdParameter, cc_UsuarioModificaParameter, cc_FechaModifciaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_EmpleadoComisiones_Inactivar_Result> UDP_Plani_EmpleadoComisiones_Inactivar(Nullable<int> cc_Id, Nullable<int> cc_UsuarioModifica, Nullable<System.DateTime> cc_FechaModifcia)
        {
            var cc_IdParameter = cc_Id.HasValue ?
                new ObjectParameter("cc_Id", cc_Id) :
                new ObjectParameter("cc_Id", typeof(int));
    
            var cc_UsuarioModificaParameter = cc_UsuarioModifica.HasValue ?
                new ObjectParameter("cc_UsuarioModifica", cc_UsuarioModifica) :
                new ObjectParameter("cc_UsuarioModifica", typeof(int));
    
            var cc_FechaModifciaParameter = cc_FechaModifcia.HasValue ?
                new ObjectParameter("cc_FechaModifcia", cc_FechaModifcia) :
                new ObjectParameter("cc_FechaModifcia", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_EmpleadoComisiones_Inactivar_Result>("UDP_Plani_EmpleadoComisiones_Inactivar", cc_IdParameter, cc_UsuarioModificaParameter, cc_FechaModifciaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_EmpleadoComisiones_Update_Result> UDP_Plani_EmpleadoComisiones_Update(Nullable<int> cc_Id, Nullable<int> emp_Id, Nullable<int> cin_IdIngresos, Nullable<int> cc_UsuarioModifica, Nullable<System.DateTime> cc_FechaModifica, Nullable<decimal> cc_TotalComision, Nullable<decimal> cc_TotalVenta)
        {
            var cc_IdParameter = cc_Id.HasValue ?
                new ObjectParameter("cc_Id", cc_Id) :
                new ObjectParameter("cc_Id", typeof(int));
    
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var cin_IdIngresosParameter = cin_IdIngresos.HasValue ?
                new ObjectParameter("cin_IdIngresos", cin_IdIngresos) :
                new ObjectParameter("cin_IdIngresos", typeof(int));
    
            var cc_UsuarioModificaParameter = cc_UsuarioModifica.HasValue ?
                new ObjectParameter("cc_UsuarioModifica", cc_UsuarioModifica) :
                new ObjectParameter("cc_UsuarioModifica", typeof(int));
    
            var cc_FechaModificaParameter = cc_FechaModifica.HasValue ?
                new ObjectParameter("cc_FechaModifica", cc_FechaModifica) :
                new ObjectParameter("cc_FechaModifica", typeof(System.DateTime));
    
            var cc_TotalComisionParameter = cc_TotalComision.HasValue ?
                new ObjectParameter("cc_TotalComision", cc_TotalComision) :
                new ObjectParameter("cc_TotalComision", typeof(decimal));
    
            var cc_TotalVentaParameter = cc_TotalVenta.HasValue ?
                new ObjectParameter("cc_TotalVenta", cc_TotalVenta) :
                new ObjectParameter("cc_TotalVenta", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_EmpleadoComisiones_Update_Result>("UDP_Plani_EmpleadoComisiones_Update", cc_IdParameter, emp_IdParameter, cin_IdIngresosParameter, cc_UsuarioModificaParameter, cc_FechaModificaParameter, cc_TotalComisionParameter, cc_TotalVentaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_EmpleadosPorAreas_Select_Result> UDP_Plani_EmpleadosPorAreas_Select()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_EmpleadosPorAreas_Select_Result>("UDP_Plani_EmpleadosPorAreas_Select");
        }
    
        public virtual ObjectResult<UDP_Plani_EquipoEmpleadosPorAreas_Select_Result> UDP_Plani_EquipoEmpleadosPorAreas_Select()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_EquipoEmpleadosPorAreas_Select_Result>("UDP_Plani_EquipoEmpleadosPorAreas_Select");
        }
    
        public virtual ObjectResult<UDP_Plani_tbAcumuladosISR_Activar_Result> UDP_Plani_tbAcumuladosISR_Activar(Nullable<int> aisr_Id, Nullable<int> aisr_UsuarioModifica, Nullable<System.DateTime> aisr_FechaModifica)
        {
            var aisr_IdParameter = aisr_Id.HasValue ?
                new ObjectParameter("aisr_Id", aisr_Id) :
                new ObjectParameter("aisr_Id", typeof(int));
    
            var aisr_UsuarioModificaParameter = aisr_UsuarioModifica.HasValue ?
                new ObjectParameter("aisr_UsuarioModifica", aisr_UsuarioModifica) :
                new ObjectParameter("aisr_UsuarioModifica", typeof(int));
    
            var aisr_FechaModificaParameter = aisr_FechaModifica.HasValue ?
                new ObjectParameter("aisr_FechaModifica", aisr_FechaModifica) :
                new ObjectParameter("aisr_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbAcumuladosISR_Activar_Result>("UDP_Plani_tbAcumuladosISR_Activar", aisr_IdParameter, aisr_UsuarioModificaParameter, aisr_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbAcumuladosISR_Inactivar_Result> UDP_Plani_tbAcumuladosISR_Inactivar(Nullable<int> aisr_Id, Nullable<int> aisr_UsuarioModifica, Nullable<System.DateTime> aisr_FechaModifica)
        {
            var aisr_IdParameter = aisr_Id.HasValue ?
                new ObjectParameter("aisr_Id", aisr_Id) :
                new ObjectParameter("aisr_Id", typeof(int));
    
            var aisr_UsuarioModificaParameter = aisr_UsuarioModifica.HasValue ?
                new ObjectParameter("aisr_UsuarioModifica", aisr_UsuarioModifica) :
                new ObjectParameter("aisr_UsuarioModifica", typeof(int));
    
            var aisr_FechaModificaParameter = aisr_FechaModifica.HasValue ?
                new ObjectParameter("aisr_FechaModifica", aisr_FechaModifica) :
                new ObjectParameter("aisr_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbAcumuladosISR_Inactivar_Result>("UDP_Plani_tbAcumuladosISR_Inactivar", aisr_IdParameter, aisr_UsuarioModificaParameter, aisr_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbAcumuladosISR_Insert_Result> UDP_Plani_tbAcumuladosISR_Insert(string aisr_Descripcion, Nullable<decimal> aisr_Monto, Nullable<int> aisr_UsuarioCrea, Nullable<System.DateTime> aisr_FechaCrea, Nullable<bool> aisr_DeducirISR, Nullable<int> emp_Id)
        {
            var aisr_DescripcionParameter = aisr_Descripcion != null ?
                new ObjectParameter("aisr_Descripcion", aisr_Descripcion) :
                new ObjectParameter("aisr_Descripcion", typeof(string));
    
            var aisr_MontoParameter = aisr_Monto.HasValue ?
                new ObjectParameter("aisr_Monto", aisr_Monto) :
                new ObjectParameter("aisr_Monto", typeof(decimal));
    
            var aisr_UsuarioCreaParameter = aisr_UsuarioCrea.HasValue ?
                new ObjectParameter("aisr_UsuarioCrea", aisr_UsuarioCrea) :
                new ObjectParameter("aisr_UsuarioCrea", typeof(int));
    
            var aisr_FechaCreaParameter = aisr_FechaCrea.HasValue ?
                new ObjectParameter("aisr_FechaCrea", aisr_FechaCrea) :
                new ObjectParameter("aisr_FechaCrea", typeof(System.DateTime));
    
            var aisr_DeducirISRParameter = aisr_DeducirISR.HasValue ?
                new ObjectParameter("aisr_DeducirISR", aisr_DeducirISR) :
                new ObjectParameter("aisr_DeducirISR", typeof(bool));
    
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbAcumuladosISR_Insert_Result>("UDP_Plani_tbAcumuladosISR_Insert", aisr_DescripcionParameter, aisr_MontoParameter, aisr_UsuarioCreaParameter, aisr_FechaCreaParameter, aisr_DeducirISRParameter, emp_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbAcumuladosISR_Update_Result> UDP_Plani_tbAcumuladosISR_Update(Nullable<int> aisr_Id, string aisr_Descripcion, Nullable<decimal> aisr_Monto, Nullable<int> aisr_UsuarioModifica, Nullable<System.DateTime> aisr_FechaModifica, Nullable<bool> aisr_DeducirISR, Nullable<int> emp_Id)
        {
            var aisr_IdParameter = aisr_Id.HasValue ?
                new ObjectParameter("aisr_Id", aisr_Id) :
                new ObjectParameter("aisr_Id", typeof(int));
    
            var aisr_DescripcionParameter = aisr_Descripcion != null ?
                new ObjectParameter("aisr_Descripcion", aisr_Descripcion) :
                new ObjectParameter("aisr_Descripcion", typeof(string));
    
            var aisr_MontoParameter = aisr_Monto.HasValue ?
                new ObjectParameter("aisr_Monto", aisr_Monto) :
                new ObjectParameter("aisr_Monto", typeof(decimal));
    
            var aisr_UsuarioModificaParameter = aisr_UsuarioModifica.HasValue ?
                new ObjectParameter("aisr_UsuarioModifica", aisr_UsuarioModifica) :
                new ObjectParameter("aisr_UsuarioModifica", typeof(int));
    
            var aisr_FechaModificaParameter = aisr_FechaModifica.HasValue ?
                new ObjectParameter("aisr_FechaModifica", aisr_FechaModifica) :
                new ObjectParameter("aisr_FechaModifica", typeof(System.DateTime));
    
            var aisr_DeducirISRParameter = aisr_DeducirISR.HasValue ?
                new ObjectParameter("aisr_DeducirISR", aisr_DeducirISR) :
                new ObjectParameter("aisr_DeducirISR", typeof(bool));
    
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbAcumuladosISR_Update_Result>("UDP_Plani_tbAcumuladosISR_Update", aisr_IdParameter, aisr_DescripcionParameter, aisr_MontoParameter, aisr_UsuarioModificaParameter, aisr_FechaModificaParameter, aisr_DeducirISRParameter, emp_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbAdelantoSueldo_Activar_Result> UDP_Plani_tbAdelantoSueldo_Activar(Nullable<int> adsu_IdAdelantoSueldo, Nullable<int> adsu_UsuarioModifica, Nullable<System.DateTime> adsu_FechaModifica)
        {
            var adsu_IdAdelantoSueldoParameter = adsu_IdAdelantoSueldo.HasValue ?
                new ObjectParameter("adsu_IdAdelantoSueldo", adsu_IdAdelantoSueldo) :
                new ObjectParameter("adsu_IdAdelantoSueldo", typeof(int));
    
            var adsu_UsuarioModificaParameter = adsu_UsuarioModifica.HasValue ?
                new ObjectParameter("adsu_UsuarioModifica", adsu_UsuarioModifica) :
                new ObjectParameter("adsu_UsuarioModifica", typeof(int));
    
            var adsu_FechaModificaParameter = adsu_FechaModifica.HasValue ?
                new ObjectParameter("adsu_FechaModifica", adsu_FechaModifica) :
                new ObjectParameter("adsu_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbAdelantoSueldo_Activar_Result>("UDP_Plani_tbAdelantoSueldo_Activar", adsu_IdAdelantoSueldoParameter, adsu_UsuarioModificaParameter, adsu_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbAdelantoSueldo_Inactivar_Result> UDP_Plani_tbAdelantoSueldo_Inactivar(Nullable<int> adsu_IdAdelantoSueldo, Nullable<int> adsu_UsuarioModifica, Nullable<System.DateTime> adsu_FechaModifica)
        {
            var adsu_IdAdelantoSueldoParameter = adsu_IdAdelantoSueldo.HasValue ?
                new ObjectParameter("adsu_IdAdelantoSueldo", adsu_IdAdelantoSueldo) :
                new ObjectParameter("adsu_IdAdelantoSueldo", typeof(int));
    
            var adsu_UsuarioModificaParameter = adsu_UsuarioModifica.HasValue ?
                new ObjectParameter("adsu_UsuarioModifica", adsu_UsuarioModifica) :
                new ObjectParameter("adsu_UsuarioModifica", typeof(int));
    
            var adsu_FechaModificaParameter = adsu_FechaModifica.HasValue ?
                new ObjectParameter("adsu_FechaModifica", adsu_FechaModifica) :
                new ObjectParameter("adsu_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbAdelantoSueldo_Inactivar_Result>("UDP_Plani_tbAdelantoSueldo_Inactivar", adsu_IdAdelantoSueldoParameter, adsu_UsuarioModificaParameter, adsu_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbAdelantoSueldo_Insert_Result> UDP_Plani_tbAdelantoSueldo_Insert(Nullable<int> emp_Id, Nullable<System.DateTime> adsu_FechaAdelanto, string adsu_RazonAdelanto, Nullable<decimal> adsu_Monto, Nullable<int> adsu_UsuarioCrea, Nullable<System.DateTime> adsu_FechaCrea)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var adsu_FechaAdelantoParameter = adsu_FechaAdelanto.HasValue ?
                new ObjectParameter("adsu_FechaAdelanto", adsu_FechaAdelanto) :
                new ObjectParameter("adsu_FechaAdelanto", typeof(System.DateTime));
    
            var adsu_RazonAdelantoParameter = adsu_RazonAdelanto != null ?
                new ObjectParameter("adsu_RazonAdelanto", adsu_RazonAdelanto) :
                new ObjectParameter("adsu_RazonAdelanto", typeof(string));
    
            var adsu_MontoParameter = adsu_Monto.HasValue ?
                new ObjectParameter("adsu_Monto", adsu_Monto) :
                new ObjectParameter("adsu_Monto", typeof(decimal));
    
            var adsu_UsuarioCreaParameter = adsu_UsuarioCrea.HasValue ?
                new ObjectParameter("adsu_UsuarioCrea", adsu_UsuarioCrea) :
                new ObjectParameter("adsu_UsuarioCrea", typeof(int));
    
            var adsu_FechaCreaParameter = adsu_FechaCrea.HasValue ?
                new ObjectParameter("adsu_FechaCrea", adsu_FechaCrea) :
                new ObjectParameter("adsu_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbAdelantoSueldo_Insert_Result>("UDP_Plani_tbAdelantoSueldo_Insert", emp_IdParameter, adsu_FechaAdelantoParameter, adsu_RazonAdelantoParameter, adsu_MontoParameter, adsu_UsuarioCreaParameter, adsu_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbAdelantoSueldo_Insert1_Result> UDP_Plani_tbAdelantoSueldo_Insert1(Nullable<int> emp_Id, Nullable<System.DateTime> adsu_FechaAdelanto, string adsu_RazonAdelanto, Nullable<decimal> adsu_Monto, Nullable<int> adsu_UsuarioCrea, Nullable<System.DateTime> adsu_FechaCrea)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var adsu_FechaAdelantoParameter = adsu_FechaAdelanto.HasValue ?
                new ObjectParameter("adsu_FechaAdelanto", adsu_FechaAdelanto) :
                new ObjectParameter("adsu_FechaAdelanto", typeof(System.DateTime));
    
            var adsu_RazonAdelantoParameter = adsu_RazonAdelanto != null ?
                new ObjectParameter("adsu_RazonAdelanto", adsu_RazonAdelanto) :
                new ObjectParameter("adsu_RazonAdelanto", typeof(string));
    
            var adsu_MontoParameter = adsu_Monto.HasValue ?
                new ObjectParameter("adsu_Monto", adsu_Monto) :
                new ObjectParameter("adsu_Monto", typeof(decimal));
    
            var adsu_UsuarioCreaParameter = adsu_UsuarioCrea.HasValue ?
                new ObjectParameter("adsu_UsuarioCrea", adsu_UsuarioCrea) :
                new ObjectParameter("adsu_UsuarioCrea", typeof(int));
    
            var adsu_FechaCreaParameter = adsu_FechaCrea.HasValue ?
                new ObjectParameter("adsu_FechaCrea", adsu_FechaCrea) :
                new ObjectParameter("adsu_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbAdelantoSueldo_Insert1_Result>("UDP_Plani_tbAdelantoSueldo_Insert1", emp_IdParameter, adsu_FechaAdelantoParameter, adsu_RazonAdelantoParameter, adsu_MontoParameter, adsu_UsuarioCreaParameter, adsu_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbAdelantoSueldo_Update_Result> UDP_Plani_tbAdelantoSueldo_Update(Nullable<int> adsu_IdAdelantoSueldo, Nullable<int> emp_Id, string adsu_RazonAdelanto, Nullable<decimal> adsu_Monto, Nullable<int> adsu_UsuarioModifica, Nullable<System.DateTime> adsu_FechaModifica)
        {
            var adsu_IdAdelantoSueldoParameter = adsu_IdAdelantoSueldo.HasValue ?
                new ObjectParameter("adsu_IdAdelantoSueldo", adsu_IdAdelantoSueldo) :
                new ObjectParameter("adsu_IdAdelantoSueldo", typeof(int));
    
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var adsu_RazonAdelantoParameter = adsu_RazonAdelanto != null ?
                new ObjectParameter("adsu_RazonAdelanto", adsu_RazonAdelanto) :
                new ObjectParameter("adsu_RazonAdelanto", typeof(string));
    
            var adsu_MontoParameter = adsu_Monto.HasValue ?
                new ObjectParameter("adsu_Monto", adsu_Monto) :
                new ObjectParameter("adsu_Monto", typeof(decimal));
    
            var adsu_UsuarioModificaParameter = adsu_UsuarioModifica.HasValue ?
                new ObjectParameter("adsu_UsuarioModifica", adsu_UsuarioModifica) :
                new ObjectParameter("adsu_UsuarioModifica", typeof(int));
    
            var adsu_FechaModificaParameter = adsu_FechaModifica.HasValue ?
                new ObjectParameter("adsu_FechaModifica", adsu_FechaModifica) :
                new ObjectParameter("adsu_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbAdelantoSueldo_Update_Result>("UDP_Plani_tbAdelantoSueldo_Update", adsu_IdAdelantoSueldoParameter, emp_IdParameter, adsu_RazonAdelantoParameter, adsu_MontoParameter, adsu_UsuarioModificaParameter, adsu_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbAFP_Activar_Result> UDP_Plani_tbAFP_Activar(Nullable<int> afp_Id, Nullable<int> afp_UsuarioModifica, Nullable<System.DateTime> afp_FechaModifica)
        {
            var afp_IdParameter = afp_Id.HasValue ?
                new ObjectParameter("afp_Id", afp_Id) :
                new ObjectParameter("afp_Id", typeof(int));
    
            var afp_UsuarioModificaParameter = afp_UsuarioModifica.HasValue ?
                new ObjectParameter("afp_UsuarioModifica", afp_UsuarioModifica) :
                new ObjectParameter("afp_UsuarioModifica", typeof(int));
    
            var afp_FechaModificaParameter = afp_FechaModifica.HasValue ?
                new ObjectParameter("afp_FechaModifica", afp_FechaModifica) :
                new ObjectParameter("afp_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbAFP_Activar_Result>("UDP_Plani_tbAFP_Activar", afp_IdParameter, afp_UsuarioModificaParameter, afp_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbAFP_Inactivar_Result> UDP_Plani_tbAFP_Inactivar(Nullable<int> afp_Id, Nullable<int> afp_UsuarioModifica, Nullable<System.DateTime> afp_FechaModifica)
        {
            var afp_IdParameter = afp_Id.HasValue ?
                new ObjectParameter("afp_Id", afp_Id) :
                new ObjectParameter("afp_Id", typeof(int));
    
            var afp_UsuarioModificaParameter = afp_UsuarioModifica.HasValue ?
                new ObjectParameter("afp_UsuarioModifica", afp_UsuarioModifica) :
                new ObjectParameter("afp_UsuarioModifica", typeof(int));
    
            var afp_FechaModificaParameter = afp_FechaModifica.HasValue ?
                new ObjectParameter("afp_FechaModifica", afp_FechaModifica) :
                new ObjectParameter("afp_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbAFP_Inactivar_Result>("UDP_Plani_tbAFP_Inactivar", afp_IdParameter, afp_UsuarioModificaParameter, afp_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbAFP_Insert_Result> UDP_Plani_tbAFP_Insert(string afp_Descripcion, Nullable<decimal> afp_AporteMinimoLps, Nullable<decimal> afp_InteresAporte, Nullable<decimal> afp_InteresAnual, Nullable<int> tde_IdTipoDedu, Nullable<int> afp_UsuarioCrea, Nullable<System.DateTime> afp_FechaCrea)
        {
            var afp_DescripcionParameter = afp_Descripcion != null ?
                new ObjectParameter("afp_Descripcion", afp_Descripcion) :
                new ObjectParameter("afp_Descripcion", typeof(string));
    
            var afp_AporteMinimoLpsParameter = afp_AporteMinimoLps.HasValue ?
                new ObjectParameter("afp_AporteMinimoLps", afp_AporteMinimoLps) :
                new ObjectParameter("afp_AporteMinimoLps", typeof(decimal));
    
            var afp_InteresAporteParameter = afp_InteresAporte.HasValue ?
                new ObjectParameter("afp_InteresAporte", afp_InteresAporte) :
                new ObjectParameter("afp_InteresAporte", typeof(decimal));
    
            var afp_InteresAnualParameter = afp_InteresAnual.HasValue ?
                new ObjectParameter("afp_InteresAnual", afp_InteresAnual) :
                new ObjectParameter("afp_InteresAnual", typeof(decimal));
    
            var tde_IdTipoDeduParameter = tde_IdTipoDedu.HasValue ?
                new ObjectParameter("tde_IdTipoDedu", tde_IdTipoDedu) :
                new ObjectParameter("tde_IdTipoDedu", typeof(int));
    
            var afp_UsuarioCreaParameter = afp_UsuarioCrea.HasValue ?
                new ObjectParameter("afp_UsuarioCrea", afp_UsuarioCrea) :
                new ObjectParameter("afp_UsuarioCrea", typeof(int));
    
            var afp_FechaCreaParameter = afp_FechaCrea.HasValue ?
                new ObjectParameter("afp_FechaCrea", afp_FechaCrea) :
                new ObjectParameter("afp_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbAFP_Insert_Result>("UDP_Plani_tbAFP_Insert", afp_DescripcionParameter, afp_AporteMinimoLpsParameter, afp_InteresAporteParameter, afp_InteresAnualParameter, tde_IdTipoDeduParameter, afp_UsuarioCreaParameter, afp_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbAFP_Update_Result> UDP_Plani_tbAFP_Update(Nullable<int> afp_Id, string afp_Descripcion, Nullable<decimal> afp_AporteMinimoLps, Nullable<decimal> afp_InteresAporte, Nullable<decimal> afp_InteresAnual, Nullable<int> tde_IdTipoDedu, Nullable<int> afp_UsuarioModifica, Nullable<System.DateTime> afp_FechaModifica)
        {
            var afp_IdParameter = afp_Id.HasValue ?
                new ObjectParameter("afp_Id", afp_Id) :
                new ObjectParameter("afp_Id", typeof(int));
    
            var afp_DescripcionParameter = afp_Descripcion != null ?
                new ObjectParameter("afp_Descripcion", afp_Descripcion) :
                new ObjectParameter("afp_Descripcion", typeof(string));
    
            var afp_AporteMinimoLpsParameter = afp_AporteMinimoLps.HasValue ?
                new ObjectParameter("afp_AporteMinimoLps", afp_AporteMinimoLps) :
                new ObjectParameter("afp_AporteMinimoLps", typeof(decimal));
    
            var afp_InteresAporteParameter = afp_InteresAporte.HasValue ?
                new ObjectParameter("afp_InteresAporte", afp_InteresAporte) :
                new ObjectParameter("afp_InteresAporte", typeof(decimal));
    
            var afp_InteresAnualParameter = afp_InteresAnual.HasValue ?
                new ObjectParameter("afp_InteresAnual", afp_InteresAnual) :
                new ObjectParameter("afp_InteresAnual", typeof(decimal));
    
            var tde_IdTipoDeduParameter = tde_IdTipoDedu.HasValue ?
                new ObjectParameter("tde_IdTipoDedu", tde_IdTipoDedu) :
                new ObjectParameter("tde_IdTipoDedu", typeof(int));
    
            var afp_UsuarioModificaParameter = afp_UsuarioModifica.HasValue ?
                new ObjectParameter("afp_UsuarioModifica", afp_UsuarioModifica) :
                new ObjectParameter("afp_UsuarioModifica", typeof(int));
    
            var afp_FechaModificaParameter = afp_FechaModifica.HasValue ?
                new ObjectParameter("afp_FechaModifica", afp_FechaModifica) :
                new ObjectParameter("afp_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbAFP_Update_Result>("UDP_Plani_tbAFP_Update", afp_IdParameter, afp_DescripcionParameter, afp_AporteMinimoLpsParameter, afp_InteresAporteParameter, afp_InteresAnualParameter, tde_IdTipoDeduParameter, afp_UsuarioModificaParameter, afp_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbAuxilioDeCesantias_Activar_Result> UDP_Plani_tbAuxilioDeCesantias_Activar(Nullable<int> aces_IdAuxilioCesantia, Nullable<int> aces_UsuarioModifica, Nullable<System.DateTime> aces_FechaModifica)
        {
            var aces_IdAuxilioCesantiaParameter = aces_IdAuxilioCesantia.HasValue ?
                new ObjectParameter("aces_IdAuxilioCesantia", aces_IdAuxilioCesantia) :
                new ObjectParameter("aces_IdAuxilioCesantia", typeof(int));
    
            var aces_UsuarioModificaParameter = aces_UsuarioModifica.HasValue ?
                new ObjectParameter("aces_UsuarioModifica", aces_UsuarioModifica) :
                new ObjectParameter("aces_UsuarioModifica", typeof(int));
    
            var aces_FechaModificaParameter = aces_FechaModifica.HasValue ?
                new ObjectParameter("aces_FechaModifica", aces_FechaModifica) :
                new ObjectParameter("aces_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbAuxilioDeCesantias_Activar_Result>("UDP_Plani_tbAuxilioDeCesantias_Activar", aces_IdAuxilioCesantiaParameter, aces_UsuarioModificaParameter, aces_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbAuxilioDeCesantias_Delete_Result> UDP_Plani_tbAuxilioDeCesantias_Delete(Nullable<int> aces_IdAuxilioCesantia, Nullable<int> aces_UsuarioModifica, Nullable<System.DateTime> aces_FechaModifica)
        {
            var aces_IdAuxilioCesantiaParameter = aces_IdAuxilioCesantia.HasValue ?
                new ObjectParameter("aces_IdAuxilioCesantia", aces_IdAuxilioCesantia) :
                new ObjectParameter("aces_IdAuxilioCesantia", typeof(int));
    
            var aces_UsuarioModificaParameter = aces_UsuarioModifica.HasValue ?
                new ObjectParameter("aces_UsuarioModifica", aces_UsuarioModifica) :
                new ObjectParameter("aces_UsuarioModifica", typeof(int));
    
            var aces_FechaModificaParameter = aces_FechaModifica.HasValue ?
                new ObjectParameter("aces_FechaModifica", aces_FechaModifica) :
                new ObjectParameter("aces_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbAuxilioDeCesantias_Delete_Result>("UDP_Plani_tbAuxilioDeCesantias_Delete", aces_IdAuxilioCesantiaParameter, aces_UsuarioModificaParameter, aces_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbAuxilioDeCesantias_Insert_Result> UDP_Plani_tbAuxilioDeCesantias_Insert(Nullable<int> aces_RangoInicioMeses, Nullable<int> aces_RangoFinMeses, Nullable<int> aces_DiasAuxilioCesantia, Nullable<int> aces_UsuarioCrea, Nullable<System.DateTime> aces_FechaCrea, Nullable<bool> aces_Activo)
        {
            var aces_RangoInicioMesesParameter = aces_RangoInicioMeses.HasValue ?
                new ObjectParameter("aces_RangoInicioMeses", aces_RangoInicioMeses) :
                new ObjectParameter("aces_RangoInicioMeses", typeof(int));
    
            var aces_RangoFinMesesParameter = aces_RangoFinMeses.HasValue ?
                new ObjectParameter("aces_RangoFinMeses", aces_RangoFinMeses) :
                new ObjectParameter("aces_RangoFinMeses", typeof(int));
    
            var aces_DiasAuxilioCesantiaParameter = aces_DiasAuxilioCesantia.HasValue ?
                new ObjectParameter("aces_DiasAuxilioCesantia", aces_DiasAuxilioCesantia) :
                new ObjectParameter("aces_DiasAuxilioCesantia", typeof(int));
    
            var aces_UsuarioCreaParameter = aces_UsuarioCrea.HasValue ?
                new ObjectParameter("aces_UsuarioCrea", aces_UsuarioCrea) :
                new ObjectParameter("aces_UsuarioCrea", typeof(int));
    
            var aces_FechaCreaParameter = aces_FechaCrea.HasValue ?
                new ObjectParameter("aces_FechaCrea", aces_FechaCrea) :
                new ObjectParameter("aces_FechaCrea", typeof(System.DateTime));
    
            var aces_ActivoParameter = aces_Activo.HasValue ?
                new ObjectParameter("aces_Activo", aces_Activo) :
                new ObjectParameter("aces_Activo", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbAuxilioDeCesantias_Insert_Result>("UDP_Plani_tbAuxilioDeCesantias_Insert", aces_RangoInicioMesesParameter, aces_RangoFinMesesParameter, aces_DiasAuxilioCesantiaParameter, aces_UsuarioCreaParameter, aces_FechaCreaParameter, aces_ActivoParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbAuxilioDeCesantias_Update_Result> UDP_Plani_tbAuxilioDeCesantias_Update(Nullable<int> aces_IdAuxilioCesantia, Nullable<int> aces_RangoInicioMeses, Nullable<int> aces_RangoFinMeses, Nullable<int> aces_DiasAuxilioCesantia, Nullable<int> aces_UsuarioModifica, Nullable<System.DateTime> aces_FechaModifica)
        {
            var aces_IdAuxilioCesantiaParameter = aces_IdAuxilioCesantia.HasValue ?
                new ObjectParameter("aces_IdAuxilioCesantia", aces_IdAuxilioCesantia) :
                new ObjectParameter("aces_IdAuxilioCesantia", typeof(int));
    
            var aces_RangoInicioMesesParameter = aces_RangoInicioMeses.HasValue ?
                new ObjectParameter("aces_RangoInicioMeses", aces_RangoInicioMeses) :
                new ObjectParameter("aces_RangoInicioMeses", typeof(int));
    
            var aces_RangoFinMesesParameter = aces_RangoFinMeses.HasValue ?
                new ObjectParameter("aces_RangoFinMeses", aces_RangoFinMeses) :
                new ObjectParameter("aces_RangoFinMeses", typeof(int));
    
            var aces_DiasAuxilioCesantiaParameter = aces_DiasAuxilioCesantia.HasValue ?
                new ObjectParameter("aces_DiasAuxilioCesantia", aces_DiasAuxilioCesantia) :
                new ObjectParameter("aces_DiasAuxilioCesantia", typeof(int));
    
            var aces_UsuarioModificaParameter = aces_UsuarioModifica.HasValue ?
                new ObjectParameter("aces_UsuarioModifica", aces_UsuarioModifica) :
                new ObjectParameter("aces_UsuarioModifica", typeof(int));
    
            var aces_FechaModificaParameter = aces_FechaModifica.HasValue ?
                new ObjectParameter("aces_FechaModifica", aces_FechaModifica) :
                new ObjectParameter("aces_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbAuxilioDeCesantias_Update_Result>("UDP_Plani_tbAuxilioDeCesantias_Update", aces_IdAuxilioCesantiaParameter, aces_RangoInicioMesesParameter, aces_RangoFinMesesParameter, aces_DiasAuxilioCesantiaParameter, aces_UsuarioModificaParameter, aces_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbCatalogoDeDeducciones_Activar_Result> UDP_Plani_tbCatalogoDeDeducciones_Activar(Nullable<int> cde_IdDeduccion, Nullable<int> cde_UsuarioModifica, Nullable<System.DateTime> cde_FechaModifica)
        {
            var cde_IdDeduccionParameter = cde_IdDeduccion.HasValue ?
                new ObjectParameter("cde_IdDeduccion", cde_IdDeduccion) :
                new ObjectParameter("cde_IdDeduccion", typeof(int));
    
            var cde_UsuarioModificaParameter = cde_UsuarioModifica.HasValue ?
                new ObjectParameter("cde_UsuarioModifica", cde_UsuarioModifica) :
                new ObjectParameter("cde_UsuarioModifica", typeof(int));
    
            var cde_FechaModificaParameter = cde_FechaModifica.HasValue ?
                new ObjectParameter("cde_FechaModifica", cde_FechaModifica) :
                new ObjectParameter("cde_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbCatalogoDeDeducciones_Activar_Result>("UDP_Plani_tbCatalogoDeDeducciones_Activar", cde_IdDeduccionParameter, cde_UsuarioModificaParameter, cde_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbCatalogoDeDeducciones_Inactivar_Result> UDP_Plani_tbCatalogoDeDeducciones_Inactivar(Nullable<int> cde_IdDeduccion, Nullable<int> cde_UsuarioModifica, Nullable<System.DateTime> cde_FechaModifica)
        {
            var cde_IdDeduccionParameter = cde_IdDeduccion.HasValue ?
                new ObjectParameter("cde_IdDeduccion", cde_IdDeduccion) :
                new ObjectParameter("cde_IdDeduccion", typeof(int));
    
            var cde_UsuarioModificaParameter = cde_UsuarioModifica.HasValue ?
                new ObjectParameter("cde_UsuarioModifica", cde_UsuarioModifica) :
                new ObjectParameter("cde_UsuarioModifica", typeof(int));
    
            var cde_FechaModificaParameter = cde_FechaModifica.HasValue ?
                new ObjectParameter("cde_FechaModifica", cde_FechaModifica) :
                new ObjectParameter("cde_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbCatalogoDeDeducciones_Inactivar_Result>("UDP_Plani_tbCatalogoDeDeducciones_Inactivar", cde_IdDeduccionParameter, cde_UsuarioModificaParameter, cde_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbCatalogoDeDeducciones_Insert_Result> UDP_Plani_tbCatalogoDeDeducciones_Insert(string cde_DescripcionDedu, Nullable<int> tde_IdTipoDedu, Nullable<decimal> cde_PorcentajeColaborador, Nullable<decimal> cde_PorcentajeEmpresa, Nullable<int> cde_UsuarioCrea, Nullable<System.DateTime> cde_FechaCrea)
        {
            var cde_DescripcionDeduParameter = cde_DescripcionDedu != null ?
                new ObjectParameter("cde_DescripcionDedu", cde_DescripcionDedu) :
                new ObjectParameter("cde_DescripcionDedu", typeof(string));
    
            var tde_IdTipoDeduParameter = tde_IdTipoDedu.HasValue ?
                new ObjectParameter("tde_IdTipoDedu", tde_IdTipoDedu) :
                new ObjectParameter("tde_IdTipoDedu", typeof(int));
    
            var cde_PorcentajeColaboradorParameter = cde_PorcentajeColaborador.HasValue ?
                new ObjectParameter("cde_PorcentajeColaborador", cde_PorcentajeColaborador) :
                new ObjectParameter("cde_PorcentajeColaborador", typeof(decimal));
    
            var cde_PorcentajeEmpresaParameter = cde_PorcentajeEmpresa.HasValue ?
                new ObjectParameter("cde_PorcentajeEmpresa", cde_PorcentajeEmpresa) :
                new ObjectParameter("cde_PorcentajeEmpresa", typeof(decimal));
    
            var cde_UsuarioCreaParameter = cde_UsuarioCrea.HasValue ?
                new ObjectParameter("cde_UsuarioCrea", cde_UsuarioCrea) :
                new ObjectParameter("cde_UsuarioCrea", typeof(int));
    
            var cde_FechaCreaParameter = cde_FechaCrea.HasValue ?
                new ObjectParameter("cde_FechaCrea", cde_FechaCrea) :
                new ObjectParameter("cde_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbCatalogoDeDeducciones_Insert_Result>("UDP_Plani_tbCatalogoDeDeducciones_Insert", cde_DescripcionDeduParameter, tde_IdTipoDeduParameter, cde_PorcentajeColaboradorParameter, cde_PorcentajeEmpresaParameter, cde_UsuarioCreaParameter, cde_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbCatalogoDeDeducciones_Update_Result> UDP_Plani_tbCatalogoDeDeducciones_Update(Nullable<int> cde_IdDeduccion, string cde_DescripcionDedu, Nullable<int> tde_IdTipoDedu, Nullable<decimal> cde_PorcentajeColaborador, Nullable<decimal> cde_PorcentajeEmpresa, Nullable<int> cde_UsuarioModifica, Nullable<System.DateTime> cde_FechaModifica)
        {
            var cde_IdDeduccionParameter = cde_IdDeduccion.HasValue ?
                new ObjectParameter("cde_IdDeduccion", cde_IdDeduccion) :
                new ObjectParameter("cde_IdDeduccion", typeof(int));
    
            var cde_DescripcionDeduParameter = cde_DescripcionDedu != null ?
                new ObjectParameter("cde_DescripcionDedu", cde_DescripcionDedu) :
                new ObjectParameter("cde_DescripcionDedu", typeof(string));
    
            var tde_IdTipoDeduParameter = tde_IdTipoDedu.HasValue ?
                new ObjectParameter("tde_IdTipoDedu", tde_IdTipoDedu) :
                new ObjectParameter("tde_IdTipoDedu", typeof(int));
    
            var cde_PorcentajeColaboradorParameter = cde_PorcentajeColaborador.HasValue ?
                new ObjectParameter("cde_PorcentajeColaborador", cde_PorcentajeColaborador) :
                new ObjectParameter("cde_PorcentajeColaborador", typeof(decimal));
    
            var cde_PorcentajeEmpresaParameter = cde_PorcentajeEmpresa.HasValue ?
                new ObjectParameter("cde_PorcentajeEmpresa", cde_PorcentajeEmpresa) :
                new ObjectParameter("cde_PorcentajeEmpresa", typeof(decimal));
    
            var cde_UsuarioModificaParameter = cde_UsuarioModifica.HasValue ?
                new ObjectParameter("cde_UsuarioModifica", cde_UsuarioModifica) :
                new ObjectParameter("cde_UsuarioModifica", typeof(int));
    
            var cde_FechaModificaParameter = cde_FechaModifica.HasValue ?
                new ObjectParameter("cde_FechaModifica", cde_FechaModifica) :
                new ObjectParameter("cde_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbCatalogoDeDeducciones_Update_Result>("UDP_Plani_tbCatalogoDeDeducciones_Update", cde_IdDeduccionParameter, cde_DescripcionDeduParameter, tde_IdTipoDeduParameter, cde_PorcentajeColaboradorParameter, cde_PorcentajeEmpresaParameter, cde_UsuarioModificaParameter, cde_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbCatalogoDePlanillas_Activar_Result> UDP_Plani_tbCatalogoDePlanillas_Activar(Nullable<int> cpla_IdPlanilla, Nullable<int> cpla_UsuarioModifica, Nullable<System.DateTime> cpla_FechaModifica)
        {
            var cpla_IdPlanillaParameter = cpla_IdPlanilla.HasValue ?
                new ObjectParameter("cpla_IdPlanilla", cpla_IdPlanilla) :
                new ObjectParameter("cpla_IdPlanilla", typeof(int));
    
            var cpla_UsuarioModificaParameter = cpla_UsuarioModifica.HasValue ?
                new ObjectParameter("cpla_UsuarioModifica", cpla_UsuarioModifica) :
                new ObjectParameter("cpla_UsuarioModifica", typeof(int));
    
            var cpla_FechaModificaParameter = cpla_FechaModifica.HasValue ?
                new ObjectParameter("cpla_FechaModifica", cpla_FechaModifica) :
                new ObjectParameter("cpla_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbCatalogoDePlanillas_Activar_Result>("UDP_Plani_tbCatalogoDePlanillas_Activar", cpla_IdPlanillaParameter, cpla_UsuarioModificaParameter, cpla_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbCatalogoDePlanillas_Inactivar_Result> UDP_Plani_tbCatalogoDePlanillas_Inactivar(Nullable<int> cpla_IdPlanilla)
        {
            var cpla_IdPlanillaParameter = cpla_IdPlanilla.HasValue ?
                new ObjectParameter("cpla_IdPlanilla", cpla_IdPlanilla) :
                new ObjectParameter("cpla_IdPlanilla", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbCatalogoDePlanillas_Inactivar_Result>("UDP_Plani_tbCatalogoDePlanillas_Inactivar", cpla_IdPlanillaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbCatalogoDePlanillas_Insert_Result> UDP_Plani_tbCatalogoDePlanillas_Insert(string cpla_DescripcionPlanilla, Nullable<int> cpla_FrecuenciaEnDias, Nullable<int> cpla_UsuarioCrea, Nullable<System.DateTime> cpla_FechaCrea, Nullable<bool> cpla_RecibeComision)
        {
            var cpla_DescripcionPlanillaParameter = cpla_DescripcionPlanilla != null ?
                new ObjectParameter("cpla_DescripcionPlanilla", cpla_DescripcionPlanilla) :
                new ObjectParameter("cpla_DescripcionPlanilla", typeof(string));
    
            var cpla_FrecuenciaEnDiasParameter = cpla_FrecuenciaEnDias.HasValue ?
                new ObjectParameter("cpla_FrecuenciaEnDias", cpla_FrecuenciaEnDias) :
                new ObjectParameter("cpla_FrecuenciaEnDias", typeof(int));
    
            var cpla_UsuarioCreaParameter = cpla_UsuarioCrea.HasValue ?
                new ObjectParameter("cpla_UsuarioCrea", cpla_UsuarioCrea) :
                new ObjectParameter("cpla_UsuarioCrea", typeof(int));
    
            var cpla_FechaCreaParameter = cpla_FechaCrea.HasValue ?
                new ObjectParameter("cpla_FechaCrea", cpla_FechaCrea) :
                new ObjectParameter("cpla_FechaCrea", typeof(System.DateTime));
    
            var cpla_RecibeComisionParameter = cpla_RecibeComision.HasValue ?
                new ObjectParameter("cpla_RecibeComision", cpla_RecibeComision) :
                new ObjectParameter("cpla_RecibeComision", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbCatalogoDePlanillas_Insert_Result>("UDP_Plani_tbCatalogoDePlanillas_Insert", cpla_DescripcionPlanillaParameter, cpla_FrecuenciaEnDiasParameter, cpla_UsuarioCreaParameter, cpla_FechaCreaParameter, cpla_RecibeComisionParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbCatalogoDePlanillas_Update_Result> UDP_Plani_tbCatalogoDePlanillas_Update(Nullable<int> cpla_IdPlanilla, string cpla_DescripcionPlanilla, Nullable<int> cpla_FrecuenciaEnDias, Nullable<int> cpla_UsuarioModifica, Nullable<System.DateTime> cpla_FechaModifica, Nullable<bool> cpla_RecibeComision)
        {
            var cpla_IdPlanillaParameter = cpla_IdPlanilla.HasValue ?
                new ObjectParameter("cpla_IdPlanilla", cpla_IdPlanilla) :
                new ObjectParameter("cpla_IdPlanilla", typeof(int));
    
            var cpla_DescripcionPlanillaParameter = cpla_DescripcionPlanilla != null ?
                new ObjectParameter("cpla_DescripcionPlanilla", cpla_DescripcionPlanilla) :
                new ObjectParameter("cpla_DescripcionPlanilla", typeof(string));
    
            var cpla_FrecuenciaEnDiasParameter = cpla_FrecuenciaEnDias.HasValue ?
                new ObjectParameter("cpla_FrecuenciaEnDias", cpla_FrecuenciaEnDias) :
                new ObjectParameter("cpla_FrecuenciaEnDias", typeof(int));
    
            var cpla_UsuarioModificaParameter = cpla_UsuarioModifica.HasValue ?
                new ObjectParameter("cpla_UsuarioModifica", cpla_UsuarioModifica) :
                new ObjectParameter("cpla_UsuarioModifica", typeof(int));
    
            var cpla_FechaModificaParameter = cpla_FechaModifica.HasValue ?
                new ObjectParameter("cpla_FechaModifica", cpla_FechaModifica) :
                new ObjectParameter("cpla_FechaModifica", typeof(System.DateTime));
    
            var cpla_RecibeComisionParameter = cpla_RecibeComision.HasValue ?
                new ObjectParameter("cpla_RecibeComision", cpla_RecibeComision) :
                new ObjectParameter("cpla_RecibeComision", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbCatalogoDePlanillas_Update_Result>("UDP_Plani_tbCatalogoDePlanillas_Update", cpla_IdPlanillaParameter, cpla_DescripcionPlanillaParameter, cpla_FrecuenciaEnDiasParameter, cpla_UsuarioModificaParameter, cpla_FechaModificaParameter, cpla_RecibeComisionParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbDecimoCuartoMes_Insert_Result> UDP_Plani_tbDecimoCuartoMes_Insert(Nullable<int> emp_Id, Nullable<decimal> dtm_DecimoCuartoMonto)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var dtm_DecimoCuartoMontoParameter = dtm_DecimoCuartoMonto.HasValue ?
                new ObjectParameter("dtm_DecimoCuartoMonto", dtm_DecimoCuartoMonto) :
                new ObjectParameter("dtm_DecimoCuartoMonto", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbDecimoCuartoMes_Insert_Result>("UDP_Plani_tbDecimoCuartoMes_Insert", emp_IdParameter, dtm_DecimoCuartoMontoParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbDecimoTercerMes_Insert_Result> UDP_Plani_tbDecimoTercerMes_Insert(Nullable<int> emp_Id, Nullable<decimal> dtm_DecimoTercer)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var dtm_DecimoTercerParameter = dtm_DecimoTercer.HasValue ?
                new ObjectParameter("dtm_DecimoTercer", dtm_DecimoTercer) :
                new ObjectParameter("dtm_DecimoTercer", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbDecimoTercerMes_Insert_Result>("UDP_Plani_tbDecimoTercerMes_Insert", emp_IdParameter, dtm_DecimoTercerParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbDeduccionAFP_Activar_Result> UDP_Plani_tbDeduccionAFP_Activar(Nullable<int> dafp_Id, Nullable<int> dafp_UsuarioModifica, Nullable<System.DateTime> dafp_FechaModifica)
        {
            var dafp_IdParameter = dafp_Id.HasValue ?
                new ObjectParameter("dafp_Id", dafp_Id) :
                new ObjectParameter("dafp_Id", typeof(int));
    
            var dafp_UsuarioModificaParameter = dafp_UsuarioModifica.HasValue ?
                new ObjectParameter("dafp_UsuarioModifica", dafp_UsuarioModifica) :
                new ObjectParameter("dafp_UsuarioModifica", typeof(int));
    
            var dafp_FechaModificaParameter = dafp_FechaModifica.HasValue ?
                new ObjectParameter("dafp_FechaModifica", dafp_FechaModifica) :
                new ObjectParameter("dafp_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbDeduccionAFP_Activar_Result>("UDP_Plani_tbDeduccionAFP_Activar", dafp_IdParameter, dafp_UsuarioModificaParameter, dafp_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbDeduccionAFP_Inactivar_Result> UDP_Plani_tbDeduccionAFP_Inactivar(Nullable<int> dafp_Id, Nullable<int> dafp_UsuarioModifica, Nullable<System.DateTime> dafp_FechaModifica)
        {
            var dafp_IdParameter = dafp_Id.HasValue ?
                new ObjectParameter("dafp_Id", dafp_Id) :
                new ObjectParameter("dafp_Id", typeof(int));
    
            var dafp_UsuarioModificaParameter = dafp_UsuarioModifica.HasValue ?
                new ObjectParameter("dafp_UsuarioModifica", dafp_UsuarioModifica) :
                new ObjectParameter("dafp_UsuarioModifica", typeof(int));
    
            var dafp_FechaModificaParameter = dafp_FechaModifica.HasValue ?
                new ObjectParameter("dafp_FechaModifica", dafp_FechaModifica) :
                new ObjectParameter("dafp_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbDeduccionAFP_Inactivar_Result>("UDP_Plani_tbDeduccionAFP_Inactivar", dafp_IdParameter, dafp_UsuarioModificaParameter, dafp_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbDeduccionAFP_Insert_Result> UDP_Plani_tbDeduccionAFP_Insert(Nullable<decimal> dafp_AporteMinimoLps, Nullable<int> afp_Id, Nullable<int> emp_Id, Nullable<int> dafp_UsuarioCrea, Nullable<System.DateTime> dafp_FechaCrea, Nullable<bool> dafp_DeducirISR)
        {
            var dafp_AporteMinimoLpsParameter = dafp_AporteMinimoLps.HasValue ?
                new ObjectParameter("dafp_AporteMinimoLps", dafp_AporteMinimoLps) :
                new ObjectParameter("dafp_AporteMinimoLps", typeof(decimal));
    
            var afp_IdParameter = afp_Id.HasValue ?
                new ObjectParameter("afp_Id", afp_Id) :
                new ObjectParameter("afp_Id", typeof(int));
    
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var dafp_UsuarioCreaParameter = dafp_UsuarioCrea.HasValue ?
                new ObjectParameter("dafp_UsuarioCrea", dafp_UsuarioCrea) :
                new ObjectParameter("dafp_UsuarioCrea", typeof(int));
    
            var dafp_FechaCreaParameter = dafp_FechaCrea.HasValue ?
                new ObjectParameter("dafp_FechaCrea", dafp_FechaCrea) :
                new ObjectParameter("dafp_FechaCrea", typeof(System.DateTime));
    
            var dafp_DeducirISRParameter = dafp_DeducirISR.HasValue ?
                new ObjectParameter("dafp_DeducirISR", dafp_DeducirISR) :
                new ObjectParameter("dafp_DeducirISR", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbDeduccionAFP_Insert_Result>("UDP_Plani_tbDeduccionAFP_Insert", dafp_AporteMinimoLpsParameter, afp_IdParameter, emp_IdParameter, dafp_UsuarioCreaParameter, dafp_FechaCreaParameter, dafp_DeducirISRParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbDeduccionAFP_Update_Result> UDP_Plani_tbDeduccionAFP_Update(Nullable<int> dafp_Id, Nullable<decimal> dafp_AporteLps, Nullable<int> afp_Id, Nullable<int> emp_Id, Nullable<int> dafp_UsuarioModifica, Nullable<System.DateTime> dafp_FechaModifica, Nullable<bool> dafp_DeducirISR)
        {
            var dafp_IdParameter = dafp_Id.HasValue ?
                new ObjectParameter("dafp_Id", dafp_Id) :
                new ObjectParameter("dafp_Id", typeof(int));
    
            var dafp_AporteLpsParameter = dafp_AporteLps.HasValue ?
                new ObjectParameter("dafp_AporteLps", dafp_AporteLps) :
                new ObjectParameter("dafp_AporteLps", typeof(decimal));
    
            var afp_IdParameter = afp_Id.HasValue ?
                new ObjectParameter("afp_Id", afp_Id) :
                new ObjectParameter("afp_Id", typeof(int));
    
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var dafp_UsuarioModificaParameter = dafp_UsuarioModifica.HasValue ?
                new ObjectParameter("dafp_UsuarioModifica", dafp_UsuarioModifica) :
                new ObjectParameter("dafp_UsuarioModifica", typeof(int));
    
            var dafp_FechaModificaParameter = dafp_FechaModifica.HasValue ?
                new ObjectParameter("dafp_FechaModifica", dafp_FechaModifica) :
                new ObjectParameter("dafp_FechaModifica", typeof(System.DateTime));
    
            var dafp_DeducirISRParameter = dafp_DeducirISR.HasValue ?
                new ObjectParameter("dafp_DeducirISR", dafp_DeducirISR) :
                new ObjectParameter("dafp_DeducirISR", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbDeduccionAFP_Update_Result>("UDP_Plani_tbDeduccionAFP_Update", dafp_IdParameter, dafp_AporteLpsParameter, afp_IdParameter, emp_IdParameter, dafp_UsuarioModificaParameter, dafp_FechaModificaParameter, dafp_DeducirISRParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbDeduccionesExtraordinarias_Activar_Result> UDP_Plani_tbDeduccionesExtraordinarias_Activar(Nullable<int> dex_IdDeduccionesExtra, Nullable<int> dex_UsuarioModifica, Nullable<System.DateTime> dex_FechaModifica)
        {
            var dex_IdDeduccionesExtraParameter = dex_IdDeduccionesExtra.HasValue ?
                new ObjectParameter("dex_IdDeduccionesExtra", dex_IdDeduccionesExtra) :
                new ObjectParameter("dex_IdDeduccionesExtra", typeof(int));
    
            var dex_UsuarioModificaParameter = dex_UsuarioModifica.HasValue ?
                new ObjectParameter("dex_UsuarioModifica", dex_UsuarioModifica) :
                new ObjectParameter("dex_UsuarioModifica", typeof(int));
    
            var dex_FechaModificaParameter = dex_FechaModifica.HasValue ?
                new ObjectParameter("dex_FechaModifica", dex_FechaModifica) :
                new ObjectParameter("dex_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbDeduccionesExtraordinarias_Activar_Result>("UDP_Plani_tbDeduccionesExtraordinarias_Activar", dex_IdDeduccionesExtraParameter, dex_UsuarioModificaParameter, dex_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbDeduccionesExtraordinarias_Inactivar_Result> UDP_Plani_tbDeduccionesExtraordinarias_Inactivar(Nullable<int> dex_IdDeduccionesExtra, Nullable<int> dex_UsuarioModifica, Nullable<System.DateTime> dex_FechaModifica)
        {
            var dex_IdDeduccionesExtraParameter = dex_IdDeduccionesExtra.HasValue ?
                new ObjectParameter("dex_IdDeduccionesExtra", dex_IdDeduccionesExtra) :
                new ObjectParameter("dex_IdDeduccionesExtra", typeof(int));
    
            var dex_UsuarioModificaParameter = dex_UsuarioModifica.HasValue ?
                new ObjectParameter("dex_UsuarioModifica", dex_UsuarioModifica) :
                new ObjectParameter("dex_UsuarioModifica", typeof(int));
    
            var dex_FechaModificaParameter = dex_FechaModifica.HasValue ?
                new ObjectParameter("dex_FechaModifica", dex_FechaModifica) :
                new ObjectParameter("dex_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbDeduccionesExtraordinarias_Inactivar_Result>("UDP_Plani_tbDeduccionesExtraordinarias_Inactivar", dex_IdDeduccionesExtraParameter, dex_UsuarioModificaParameter, dex_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbDeduccionesExtraordinarias_Insert_Result> UDP_Plani_tbDeduccionesExtraordinarias_Insert(Nullable<int> eqem_Id, Nullable<decimal> dex_MontoInicial, Nullable<decimal> dex_MontoRestante, string dex_ObservacionesComentarios, Nullable<int> cde_IdDeducciones, Nullable<decimal> dex_Cuota, Nullable<int> dex_UsuarioCrea, Nullable<System.DateTime> dex_FechaCrea, Nullable<bool> dex_DeducirISR)
        {
            var eqem_IdParameter = eqem_Id.HasValue ?
                new ObjectParameter("eqem_Id", eqem_Id) :
                new ObjectParameter("eqem_Id", typeof(int));
    
            var dex_MontoInicialParameter = dex_MontoInicial.HasValue ?
                new ObjectParameter("dex_MontoInicial", dex_MontoInicial) :
                new ObjectParameter("dex_MontoInicial", typeof(decimal));
    
            var dex_MontoRestanteParameter = dex_MontoRestante.HasValue ?
                new ObjectParameter("dex_MontoRestante", dex_MontoRestante) :
                new ObjectParameter("dex_MontoRestante", typeof(decimal));
    
            var dex_ObservacionesComentariosParameter = dex_ObservacionesComentarios != null ?
                new ObjectParameter("dex_ObservacionesComentarios", dex_ObservacionesComentarios) :
                new ObjectParameter("dex_ObservacionesComentarios", typeof(string));
    
            var cde_IdDeduccionesParameter = cde_IdDeducciones.HasValue ?
                new ObjectParameter("cde_IdDeducciones", cde_IdDeducciones) :
                new ObjectParameter("cde_IdDeducciones", typeof(int));
    
            var dex_CuotaParameter = dex_Cuota.HasValue ?
                new ObjectParameter("dex_Cuota", dex_Cuota) :
                new ObjectParameter("dex_Cuota", typeof(decimal));
    
            var dex_UsuarioCreaParameter = dex_UsuarioCrea.HasValue ?
                new ObjectParameter("dex_UsuarioCrea", dex_UsuarioCrea) :
                new ObjectParameter("dex_UsuarioCrea", typeof(int));
    
            var dex_FechaCreaParameter = dex_FechaCrea.HasValue ?
                new ObjectParameter("dex_FechaCrea", dex_FechaCrea) :
                new ObjectParameter("dex_FechaCrea", typeof(System.DateTime));
    
            var dex_DeducirISRParameter = dex_DeducirISR.HasValue ?
                new ObjectParameter("dex_DeducirISR", dex_DeducirISR) :
                new ObjectParameter("dex_DeducirISR", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbDeduccionesExtraordinarias_Insert_Result>("UDP_Plani_tbDeduccionesExtraordinarias_Insert", eqem_IdParameter, dex_MontoInicialParameter, dex_MontoRestanteParameter, dex_ObservacionesComentariosParameter, cde_IdDeduccionesParameter, dex_CuotaParameter, dex_UsuarioCreaParameter, dex_FechaCreaParameter, dex_DeducirISRParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbDeduccionesExtraordinarias_Update_Result> UDP_Plani_tbDeduccionesExtraordinarias_Update(Nullable<int> dex_IdDeduccionesExtra, Nullable<int> eqem_Id, Nullable<decimal> dex_MontoInicial, Nullable<decimal> dex_MontoRestante, string dex_ObservacionesComentarios, Nullable<int> cde_IdDeducciones, Nullable<decimal> dex_Cuota, Nullable<int> dex_UsuarioModifica, Nullable<System.DateTime> dex_FechaModifica, Nullable<bool> dex_DeducirISR)
        {
            var dex_IdDeduccionesExtraParameter = dex_IdDeduccionesExtra.HasValue ?
                new ObjectParameter("dex_IdDeduccionesExtra", dex_IdDeduccionesExtra) :
                new ObjectParameter("dex_IdDeduccionesExtra", typeof(int));
    
            var eqem_IdParameter = eqem_Id.HasValue ?
                new ObjectParameter("eqem_Id", eqem_Id) :
                new ObjectParameter("eqem_Id", typeof(int));
    
            var dex_MontoInicialParameter = dex_MontoInicial.HasValue ?
                new ObjectParameter("dex_MontoInicial", dex_MontoInicial) :
                new ObjectParameter("dex_MontoInicial", typeof(decimal));
    
            var dex_MontoRestanteParameter = dex_MontoRestante.HasValue ?
                new ObjectParameter("dex_MontoRestante", dex_MontoRestante) :
                new ObjectParameter("dex_MontoRestante", typeof(decimal));
    
            var dex_ObservacionesComentariosParameter = dex_ObservacionesComentarios != null ?
                new ObjectParameter("dex_ObservacionesComentarios", dex_ObservacionesComentarios) :
                new ObjectParameter("dex_ObservacionesComentarios", typeof(string));
    
            var cde_IdDeduccionesParameter = cde_IdDeducciones.HasValue ?
                new ObjectParameter("cde_IdDeducciones", cde_IdDeducciones) :
                new ObjectParameter("cde_IdDeducciones", typeof(int));
    
            var dex_CuotaParameter = dex_Cuota.HasValue ?
                new ObjectParameter("dex_Cuota", dex_Cuota) :
                new ObjectParameter("dex_Cuota", typeof(decimal));
    
            var dex_UsuarioModificaParameter = dex_UsuarioModifica.HasValue ?
                new ObjectParameter("dex_UsuarioModifica", dex_UsuarioModifica) :
                new ObjectParameter("dex_UsuarioModifica", typeof(int));
    
            var dex_FechaModificaParameter = dex_FechaModifica.HasValue ?
                new ObjectParameter("dex_FechaModifica", dex_FechaModifica) :
                new ObjectParameter("dex_FechaModifica", typeof(System.DateTime));
    
            var dex_DeducirISRParameter = dex_DeducirISR.HasValue ?
                new ObjectParameter("dex_DeducirISR", dex_DeducirISR) :
                new ObjectParameter("dex_DeducirISR", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbDeduccionesExtraordinarias_Update_Result>("UDP_Plani_tbDeduccionesExtraordinarias_Update", dex_IdDeduccionesExtraParameter, eqem_IdParameter, dex_MontoInicialParameter, dex_MontoRestanteParameter, dex_ObservacionesComentariosParameter, cde_IdDeduccionesParameter, dex_CuotaParameter, dex_UsuarioModificaParameter, dex_FechaModificaParameter, dex_DeducirISRParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbDeduccionesIndividuales_Activar_Result> UDP_Plani_tbDeduccionesIndividuales_Activar(Nullable<int> dei_IdDeduccionesIndividuales, Nullable<int> dei_UsuarioModifica, Nullable<System.DateTime> dei_FechaModifica)
        {
            var dei_IdDeduccionesIndividualesParameter = dei_IdDeduccionesIndividuales.HasValue ?
                new ObjectParameter("dei_IdDeduccionesIndividuales", dei_IdDeduccionesIndividuales) :
                new ObjectParameter("dei_IdDeduccionesIndividuales", typeof(int));
    
            var dei_UsuarioModificaParameter = dei_UsuarioModifica.HasValue ?
                new ObjectParameter("dei_UsuarioModifica", dei_UsuarioModifica) :
                new ObjectParameter("dei_UsuarioModifica", typeof(int));
    
            var dei_FechaModificaParameter = dei_FechaModifica.HasValue ?
                new ObjectParameter("dei_FechaModifica", dei_FechaModifica) :
                new ObjectParameter("dei_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbDeduccionesIndividuales_Activar_Result>("UDP_Plani_tbDeduccionesIndividuales_Activar", dei_IdDeduccionesIndividualesParameter, dei_UsuarioModificaParameter, dei_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbDeduccionesIndividuales_Inactivar_Result> UDP_Plani_tbDeduccionesIndividuales_Inactivar(Nullable<int> dei_IdDeduccionesIndividuales, Nullable<int> dei_UsuarioModifica, Nullable<System.DateTime> dei_FechaModifica)
        {
            var dei_IdDeduccionesIndividualesParameter = dei_IdDeduccionesIndividuales.HasValue ?
                new ObjectParameter("dei_IdDeduccionesIndividuales", dei_IdDeduccionesIndividuales) :
                new ObjectParameter("dei_IdDeduccionesIndividuales", typeof(int));
    
            var dei_UsuarioModificaParameter = dei_UsuarioModifica.HasValue ?
                new ObjectParameter("dei_UsuarioModifica", dei_UsuarioModifica) :
                new ObjectParameter("dei_UsuarioModifica", typeof(int));
    
            var dei_FechaModificaParameter = dei_FechaModifica.HasValue ?
                new ObjectParameter("dei_FechaModifica", dei_FechaModifica) :
                new ObjectParameter("dei_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbDeduccionesIndividuales_Inactivar_Result>("UDP_Plani_tbDeduccionesIndividuales_Inactivar", dei_IdDeduccionesIndividualesParameter, dei_UsuarioModificaParameter, dei_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbDeduccionesIndividuales_Insert_Result> UDP_Plani_tbDeduccionesIndividuales_Insert(string dei_Motivo, Nullable<int> emp_Id, Nullable<decimal> dei_Monto, Nullable<int> dei_NumeroCuotas, Nullable<decimal> dei_MontoCuotas, Nullable<bool> dei_PagaSiempre, Nullable<bool> dei_Pagado, Nullable<int> dei_UsuarioCrea, Nullable<System.DateTime> dei_FechaCrea, Nullable<bool> dei_DeducirISR)
        {
            var dei_MotivoParameter = dei_Motivo != null ?
                new ObjectParameter("dei_Motivo", dei_Motivo) :
                new ObjectParameter("dei_Motivo", typeof(string));
    
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var dei_MontoParameter = dei_Monto.HasValue ?
                new ObjectParameter("dei_Monto", dei_Monto) :
                new ObjectParameter("dei_Monto", typeof(decimal));
    
            var dei_NumeroCuotasParameter = dei_NumeroCuotas.HasValue ?
                new ObjectParameter("dei_NumeroCuotas", dei_NumeroCuotas) :
                new ObjectParameter("dei_NumeroCuotas", typeof(int));
    
            var dei_MontoCuotasParameter = dei_MontoCuotas.HasValue ?
                new ObjectParameter("dei_MontoCuotas", dei_MontoCuotas) :
                new ObjectParameter("dei_MontoCuotas", typeof(decimal));
    
            var dei_PagaSiempreParameter = dei_PagaSiempre.HasValue ?
                new ObjectParameter("dei_PagaSiempre", dei_PagaSiempre) :
                new ObjectParameter("dei_PagaSiempre", typeof(bool));
    
            var dei_PagadoParameter = dei_Pagado.HasValue ?
                new ObjectParameter("dei_Pagado", dei_Pagado) :
                new ObjectParameter("dei_Pagado", typeof(bool));
    
            var dei_UsuarioCreaParameter = dei_UsuarioCrea.HasValue ?
                new ObjectParameter("dei_UsuarioCrea", dei_UsuarioCrea) :
                new ObjectParameter("dei_UsuarioCrea", typeof(int));
    
            var dei_FechaCreaParameter = dei_FechaCrea.HasValue ?
                new ObjectParameter("dei_FechaCrea", dei_FechaCrea) :
                new ObjectParameter("dei_FechaCrea", typeof(System.DateTime));
    
            var dei_DeducirISRParameter = dei_DeducirISR.HasValue ?
                new ObjectParameter("dei_DeducirISR", dei_DeducirISR) :
                new ObjectParameter("dei_DeducirISR", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbDeduccionesIndividuales_Insert_Result>("UDP_Plani_tbDeduccionesIndividuales_Insert", dei_MotivoParameter, emp_IdParameter, dei_MontoParameter, dei_NumeroCuotasParameter, dei_MontoCuotasParameter, dei_PagaSiempreParameter, dei_PagadoParameter, dei_UsuarioCreaParameter, dei_FechaCreaParameter, dei_DeducirISRParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbDeduccionesIndividuales_Update_Result> UDP_Plani_tbDeduccionesIndividuales_Update(Nullable<int> dei_IdDeduccionesIndividuales, string dei_Motivo, Nullable<int> emp_Id, Nullable<decimal> dei_Monto, Nullable<int> dei_NumeroCuotas, Nullable<decimal> dei_MontoCuota, Nullable<bool> dei_PagaSiempre, Nullable<int> dei_UsuarioModifica, Nullable<System.DateTime> dei_FechaModifica, Nullable<bool> dei_DeducirISR)
        {
            var dei_IdDeduccionesIndividualesParameter = dei_IdDeduccionesIndividuales.HasValue ?
                new ObjectParameter("dei_IdDeduccionesIndividuales", dei_IdDeduccionesIndividuales) :
                new ObjectParameter("dei_IdDeduccionesIndividuales", typeof(int));
    
            var dei_MotivoParameter = dei_Motivo != null ?
                new ObjectParameter("dei_Motivo", dei_Motivo) :
                new ObjectParameter("dei_Motivo", typeof(string));
    
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var dei_MontoParameter = dei_Monto.HasValue ?
                new ObjectParameter("dei_Monto", dei_Monto) :
                new ObjectParameter("dei_Monto", typeof(decimal));
    
            var dei_NumeroCuotasParameter = dei_NumeroCuotas.HasValue ?
                new ObjectParameter("dei_NumeroCuotas", dei_NumeroCuotas) :
                new ObjectParameter("dei_NumeroCuotas", typeof(int));
    
            var dei_MontoCuotaParameter = dei_MontoCuota.HasValue ?
                new ObjectParameter("dei_MontoCuota", dei_MontoCuota) :
                new ObjectParameter("dei_MontoCuota", typeof(decimal));
    
            var dei_PagaSiempreParameter = dei_PagaSiempre.HasValue ?
                new ObjectParameter("dei_PagaSiempre", dei_PagaSiempre) :
                new ObjectParameter("dei_PagaSiempre", typeof(bool));
    
            var dei_UsuarioModificaParameter = dei_UsuarioModifica.HasValue ?
                new ObjectParameter("dei_UsuarioModifica", dei_UsuarioModifica) :
                new ObjectParameter("dei_UsuarioModifica", typeof(int));
    
            var dei_FechaModificaParameter = dei_FechaModifica.HasValue ?
                new ObjectParameter("dei_FechaModifica", dei_FechaModifica) :
                new ObjectParameter("dei_FechaModifica", typeof(System.DateTime));
    
            var dei_DeducirISRParameter = dei_DeducirISR.HasValue ?
                new ObjectParameter("dei_DeducirISR", dei_DeducirISR) :
                new ObjectParameter("dei_DeducirISR", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbDeduccionesIndividuales_Update_Result>("UDP_Plani_tbDeduccionesIndividuales_Update", dei_IdDeduccionesIndividualesParameter, dei_MotivoParameter, emp_IdParameter, dei_MontoParameter, dei_NumeroCuotasParameter, dei_MontoCuotaParameter, dei_PagaSiempreParameter, dei_UsuarioModificaParameter, dei_FechaModificaParameter, dei_DeducirISRParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbDeduccionImpuestoVecinal_Insert_Result> UDP_Plani_tbDeduccionImpuestoVecinal_Insert(Nullable<int> emp_Id, Nullable<decimal> dimv_MontoTotal, Nullable<decimal> dimv_CuotaAPagar, Nullable<int> timv_UsuarioCrea, Nullable<System.DateTime> timv_FechaCrea)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var dimv_MontoTotalParameter = dimv_MontoTotal.HasValue ?
                new ObjectParameter("dimv_MontoTotal", dimv_MontoTotal) :
                new ObjectParameter("dimv_MontoTotal", typeof(decimal));
    
            var dimv_CuotaAPagarParameter = dimv_CuotaAPagar.HasValue ?
                new ObjectParameter("dimv_CuotaAPagar", dimv_CuotaAPagar) :
                new ObjectParameter("dimv_CuotaAPagar", typeof(decimal));
    
            var timv_UsuarioCreaParameter = timv_UsuarioCrea.HasValue ?
                new ObjectParameter("timv_UsuarioCrea", timv_UsuarioCrea) :
                new ObjectParameter("timv_UsuarioCrea", typeof(int));
    
            var timv_FechaCreaParameter = timv_FechaCrea.HasValue ?
                new ObjectParameter("timv_FechaCrea", timv_FechaCrea) :
                new ObjectParameter("timv_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbDeduccionImpuestoVecinal_Insert_Result>("UDP_Plani_tbDeduccionImpuestoVecinal_Insert", emp_IdParameter, dimv_MontoTotalParameter, dimv_CuotaAPagarParameter, timv_UsuarioCreaParameter, timv_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbEmpleadoBonos_Activar_Result> UDP_Plani_tbEmpleadoBonos_Activar(Nullable<int> cb_Id, Nullable<int> cb_UsuarioModifica, Nullable<System.DateTime> cb_FechaModifica)
        {
            var cb_IdParameter = cb_Id.HasValue ?
                new ObjectParameter("cb_Id", cb_Id) :
                new ObjectParameter("cb_Id", typeof(int));
    
            var cb_UsuarioModificaParameter = cb_UsuarioModifica.HasValue ?
                new ObjectParameter("cb_UsuarioModifica", cb_UsuarioModifica) :
                new ObjectParameter("cb_UsuarioModifica", typeof(int));
    
            var cb_FechaModificaParameter = cb_FechaModifica.HasValue ?
                new ObjectParameter("cb_FechaModifica", cb_FechaModifica) :
                new ObjectParameter("cb_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbEmpleadoBonos_Activar_Result>("UDP_Plani_tbEmpleadoBonos_Activar", cb_IdParameter, cb_UsuarioModificaParameter, cb_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbEmpleadoBonos_Inactivar_Result> UDP_Plani_tbEmpleadoBonos_Inactivar(Nullable<int> cb_Id, Nullable<int> cb_UsuarioModifica, Nullable<System.DateTime> cb_FechaModifica)
        {
            var cb_IdParameter = cb_Id.HasValue ?
                new ObjectParameter("cb_Id", cb_Id) :
                new ObjectParameter("cb_Id", typeof(int));
    
            var cb_UsuarioModificaParameter = cb_UsuarioModifica.HasValue ?
                new ObjectParameter("cb_UsuarioModifica", cb_UsuarioModifica) :
                new ObjectParameter("cb_UsuarioModifica", typeof(int));
    
            var cb_FechaModificaParameter = cb_FechaModifica.HasValue ?
                new ObjectParameter("cb_FechaModifica", cb_FechaModifica) :
                new ObjectParameter("cb_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbEmpleadoBonos_Inactivar_Result>("UDP_Plani_tbEmpleadoBonos_Inactivar", cb_IdParameter, cb_UsuarioModificaParameter, cb_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbEmpleadoBonos_Insert_Result> UDP_Plani_tbEmpleadoBonos_Insert(Nullable<int> emp_Id, Nullable<int> cin_IdIngreso, Nullable<decimal> cb_Monto, Nullable<System.DateTime> cb_FechaRegistro, Nullable<bool> cb_Pagado, Nullable<int> cb_UsuarioCrea, Nullable<System.DateTime> cb_FechaCrea)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var cin_IdIngresoParameter = cin_IdIngreso.HasValue ?
                new ObjectParameter("cin_IdIngreso", cin_IdIngreso) :
                new ObjectParameter("cin_IdIngreso", typeof(int));
    
            var cb_MontoParameter = cb_Monto.HasValue ?
                new ObjectParameter("cb_Monto", cb_Monto) :
                new ObjectParameter("cb_Monto", typeof(decimal));
    
            var cb_FechaRegistroParameter = cb_FechaRegistro.HasValue ?
                new ObjectParameter("cb_FechaRegistro", cb_FechaRegistro) :
                new ObjectParameter("cb_FechaRegistro", typeof(System.DateTime));
    
            var cb_PagadoParameter = cb_Pagado.HasValue ?
                new ObjectParameter("cb_Pagado", cb_Pagado) :
                new ObjectParameter("cb_Pagado", typeof(bool));
    
            var cb_UsuarioCreaParameter = cb_UsuarioCrea.HasValue ?
                new ObjectParameter("cb_UsuarioCrea", cb_UsuarioCrea) :
                new ObjectParameter("cb_UsuarioCrea", typeof(int));
    
            var cb_FechaCreaParameter = cb_FechaCrea.HasValue ?
                new ObjectParameter("cb_FechaCrea", cb_FechaCrea) :
                new ObjectParameter("cb_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbEmpleadoBonos_Insert_Result>("UDP_Plani_tbEmpleadoBonos_Insert", emp_IdParameter, cin_IdIngresoParameter, cb_MontoParameter, cb_FechaRegistroParameter, cb_PagadoParameter, cb_UsuarioCreaParameter, cb_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbEmpleadoBonos_Update_Result> UDP_Plani_tbEmpleadoBonos_Update(Nullable<int> cb_Id, Nullable<int> emp_Id, Nullable<int> cin_IdIngreso, Nullable<decimal> cb_Monto, Nullable<System.DateTime> cb_FechaRegistro, Nullable<bool> cb_Pagado, Nullable<int> cb_UsuarioModifica, Nullable<System.DateTime> cb_FechaModifica)
        {
            var cb_IdParameter = cb_Id.HasValue ?
                new ObjectParameter("cb_Id", cb_Id) :
                new ObjectParameter("cb_Id", typeof(int));
    
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var cin_IdIngresoParameter = cin_IdIngreso.HasValue ?
                new ObjectParameter("cin_IdIngreso", cin_IdIngreso) :
                new ObjectParameter("cin_IdIngreso", typeof(int));
    
            var cb_MontoParameter = cb_Monto.HasValue ?
                new ObjectParameter("cb_Monto", cb_Monto) :
                new ObjectParameter("cb_Monto", typeof(decimal));
    
            var cb_FechaRegistroParameter = cb_FechaRegistro.HasValue ?
                new ObjectParameter("cb_FechaRegistro", cb_FechaRegistro) :
                new ObjectParameter("cb_FechaRegistro", typeof(System.DateTime));
    
            var cb_PagadoParameter = cb_Pagado.HasValue ?
                new ObjectParameter("cb_Pagado", cb_Pagado) :
                new ObjectParameter("cb_Pagado", typeof(bool));
    
            var cb_UsuarioModificaParameter = cb_UsuarioModifica.HasValue ?
                new ObjectParameter("cb_UsuarioModifica", cb_UsuarioModifica) :
                new ObjectParameter("cb_UsuarioModifica", typeof(int));
    
            var cb_FechaModificaParameter = cb_FechaModifica.HasValue ?
                new ObjectParameter("cb_FechaModifica", cb_FechaModifica) :
                new ObjectParameter("cb_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbEmpleadoBonos_Update_Result>("UDP_Plani_tbEmpleadoBonos_Update", cb_IdParameter, emp_IdParameter, cin_IdIngresoParameter, cb_MontoParameter, cb_FechaRegistroParameter, cb_PagadoParameter, cb_UsuarioModificaParameter, cb_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbFormaPago_Activar_Result> UDP_Plani_tbFormaPago_Activar(Nullable<int> fpa_IdFormaPago, Nullable<int> fpa_UsuarioModifica, Nullable<System.DateTime> fpa_FechaModifica)
        {
            var fpa_IdFormaPagoParameter = fpa_IdFormaPago.HasValue ?
                new ObjectParameter("fpa_IdFormaPago", fpa_IdFormaPago) :
                new ObjectParameter("fpa_IdFormaPago", typeof(int));
    
            var fpa_UsuarioModificaParameter = fpa_UsuarioModifica.HasValue ?
                new ObjectParameter("fpa_UsuarioModifica", fpa_UsuarioModifica) :
                new ObjectParameter("fpa_UsuarioModifica", typeof(int));
    
            var fpa_FechaModificaParameter = fpa_FechaModifica.HasValue ?
                new ObjectParameter("fpa_FechaModifica", fpa_FechaModifica) :
                new ObjectParameter("fpa_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbFormaPago_Activar_Result>("UDP_Plani_tbFormaPago_Activar", fpa_IdFormaPagoParameter, fpa_UsuarioModificaParameter, fpa_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbFormaPago_Inactivar_Result> UDP_Plani_tbFormaPago_Inactivar(Nullable<int> fpa_IdFormaPago, Nullable<int> fpa_UsuarioModifica, Nullable<System.DateTime> fpa_FechaModifica)
        {
            var fpa_IdFormaPagoParameter = fpa_IdFormaPago.HasValue ?
                new ObjectParameter("fpa_IdFormaPago", fpa_IdFormaPago) :
                new ObjectParameter("fpa_IdFormaPago", typeof(int));
    
            var fpa_UsuarioModificaParameter = fpa_UsuarioModifica.HasValue ?
                new ObjectParameter("fpa_UsuarioModifica", fpa_UsuarioModifica) :
                new ObjectParameter("fpa_UsuarioModifica", typeof(int));
    
            var fpa_FechaModificaParameter = fpa_FechaModifica.HasValue ?
                new ObjectParameter("fpa_FechaModifica", fpa_FechaModifica) :
                new ObjectParameter("fpa_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbFormaPago_Inactivar_Result>("UDP_Plani_tbFormaPago_Inactivar", fpa_IdFormaPagoParameter, fpa_UsuarioModificaParameter, fpa_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbFormaPago_Insert_Result> UDP_Plani_tbFormaPago_Insert(string fpa_Descripcion, Nullable<int> fpa_UsuarioCrea, Nullable<System.DateTime> fpa_FechaCrea)
        {
            var fpa_DescripcionParameter = fpa_Descripcion != null ?
                new ObjectParameter("fpa_Descripcion", fpa_Descripcion) :
                new ObjectParameter("fpa_Descripcion", typeof(string));
    
            var fpa_UsuarioCreaParameter = fpa_UsuarioCrea.HasValue ?
                new ObjectParameter("fpa_UsuarioCrea", fpa_UsuarioCrea) :
                new ObjectParameter("fpa_UsuarioCrea", typeof(int));
    
            var fpa_FechaCreaParameter = fpa_FechaCrea.HasValue ?
                new ObjectParameter("fpa_FechaCrea", fpa_FechaCrea) :
                new ObjectParameter("fpa_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbFormaPago_Insert_Result>("UDP_Plani_tbFormaPago_Insert", fpa_DescripcionParameter, fpa_UsuarioCreaParameter, fpa_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbFormaPago_Update_Result> UDP_Plani_tbFormaPago_Update(Nullable<int> fpa_IdFormaPago, string fpa_Descripcion, Nullable<int> fpa_UsuarioModifica, Nullable<System.DateTime> fpa_FechaModifica)
        {
            var fpa_IdFormaPagoParameter = fpa_IdFormaPago.HasValue ?
                new ObjectParameter("fpa_IdFormaPago", fpa_IdFormaPago) :
                new ObjectParameter("fpa_IdFormaPago", typeof(int));
    
            var fpa_DescripcionParameter = fpa_Descripcion != null ?
                new ObjectParameter("fpa_Descripcion", fpa_Descripcion) :
                new ObjectParameter("fpa_Descripcion", typeof(string));
    
            var fpa_UsuarioModificaParameter = fpa_UsuarioModifica.HasValue ?
                new ObjectParameter("fpa_UsuarioModifica", fpa_UsuarioModifica) :
                new ObjectParameter("fpa_UsuarioModifica", typeof(int));
    
            var fpa_FechaModificaParameter = fpa_FechaModifica.HasValue ?
                new ObjectParameter("fpa_FechaModifica", fpa_FechaModifica) :
                new ObjectParameter("fpa_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbFormaPago_Update_Result>("UDP_Plani_tbFormaPago_Update", fpa_IdFormaPagoParameter, fpa_DescripcionParameter, fpa_UsuarioModificaParameter, fpa_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbHistorialDeLiquidaciones_Insert_Result> UDP_Plani_tbHistorialDeLiquidaciones_Insert(Nullable<int> emp_Id, Nullable<System.DateTime> hdli_FechaLiquidacion, Nullable<decimal> hdli_SalarioOrdinarioMensual_Liq, Nullable<decimal> hdli_SalarioPromedioMensual_Liql, Nullable<decimal> hdli_SalarioOrdinarioDiario_Liq, Nullable<decimal> hdli_SalarioPromedioDiario_Liq, Nullable<decimal> hdli_Preaviso_Liq, Nullable<decimal> hdli_Cesantia_Liq, Nullable<decimal> hdli_DecimoTercerMesProporcional_Liq, Nullable<decimal> hdli_DecimoCuartoMesProporcional_Liq, Nullable<decimal> hdli_VacacionesPendientes_Liq, Nullable<decimal> hdli_SalariosAdeudados, Nullable<decimal> hdli_OtrosPagos, Nullable<decimal> hdli_PagoHEPendiente, Nullable<decimal> hdli_ValorBonoEducativo, Nullable<decimal> hdli_PagoSeptimoDia, Nullable<decimal> hdli_BonoPorVacaciones, Nullable<decimal> hdli_ReajusteSalarial, Nullable<decimal> hdli_DecimoTercerMesAdeudado, Nullable<decimal> hdli_DecimoCuartoMesAdeudado, Nullable<decimal> hdli_BonificacionVacaciones, Nullable<decimal> hdli_PagoPorEmbarazo, Nullable<decimal> hdli_PagoPorLactancia, Nullable<decimal> hdli_PrePosNatal, Nullable<decimal> hdli_PagoPorDiasFeriado, Nullable<decimal> hdli_MontoTotalLiquidacion, Nullable<int> hdli_liqu_UsuarioCrea, Nullable<System.DateTime> hdli_liqu_FechaCrea, Nullable<int> moli_Id)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var hdli_FechaLiquidacionParameter = hdli_FechaLiquidacion.HasValue ?
                new ObjectParameter("hdli_FechaLiquidacion", hdli_FechaLiquidacion) :
                new ObjectParameter("hdli_FechaLiquidacion", typeof(System.DateTime));
    
            var hdli_SalarioOrdinarioMensual_LiqParameter = hdli_SalarioOrdinarioMensual_Liq.HasValue ?
                new ObjectParameter("hdli_SalarioOrdinarioMensual_Liq", hdli_SalarioOrdinarioMensual_Liq) :
                new ObjectParameter("hdli_SalarioOrdinarioMensual_Liq", typeof(decimal));
    
            var hdli_SalarioPromedioMensual_LiqlParameter = hdli_SalarioPromedioMensual_Liql.HasValue ?
                new ObjectParameter("hdli_SalarioPromedioMensual_Liql", hdli_SalarioPromedioMensual_Liql) :
                new ObjectParameter("hdli_SalarioPromedioMensual_Liql", typeof(decimal));
    
            var hdli_SalarioOrdinarioDiario_LiqParameter = hdli_SalarioOrdinarioDiario_Liq.HasValue ?
                new ObjectParameter("hdli_SalarioOrdinarioDiario_Liq", hdli_SalarioOrdinarioDiario_Liq) :
                new ObjectParameter("hdli_SalarioOrdinarioDiario_Liq", typeof(decimal));
    
            var hdli_SalarioPromedioDiario_LiqParameter = hdli_SalarioPromedioDiario_Liq.HasValue ?
                new ObjectParameter("hdli_SalarioPromedioDiario_Liq", hdli_SalarioPromedioDiario_Liq) :
                new ObjectParameter("hdli_SalarioPromedioDiario_Liq", typeof(decimal));
    
            var hdli_Preaviso_LiqParameter = hdli_Preaviso_Liq.HasValue ?
                new ObjectParameter("hdli_Preaviso_Liq", hdli_Preaviso_Liq) :
                new ObjectParameter("hdli_Preaviso_Liq", typeof(decimal));
    
            var hdli_Cesantia_LiqParameter = hdli_Cesantia_Liq.HasValue ?
                new ObjectParameter("hdli_Cesantia_Liq", hdli_Cesantia_Liq) :
                new ObjectParameter("hdli_Cesantia_Liq", typeof(decimal));
    
            var hdli_DecimoTercerMesProporcional_LiqParameter = hdli_DecimoTercerMesProporcional_Liq.HasValue ?
                new ObjectParameter("hdli_DecimoTercerMesProporcional_Liq", hdli_DecimoTercerMesProporcional_Liq) :
                new ObjectParameter("hdli_DecimoTercerMesProporcional_Liq", typeof(decimal));
    
            var hdli_DecimoCuartoMesProporcional_LiqParameter = hdli_DecimoCuartoMesProporcional_Liq.HasValue ?
                new ObjectParameter("hdli_DecimoCuartoMesProporcional_Liq", hdli_DecimoCuartoMesProporcional_Liq) :
                new ObjectParameter("hdli_DecimoCuartoMesProporcional_Liq", typeof(decimal));
    
            var hdli_VacacionesPendientes_LiqParameter = hdli_VacacionesPendientes_Liq.HasValue ?
                new ObjectParameter("hdli_VacacionesPendientes_Liq", hdli_VacacionesPendientes_Liq) :
                new ObjectParameter("hdli_VacacionesPendientes_Liq", typeof(decimal));
    
            var hdli_SalariosAdeudadosParameter = hdli_SalariosAdeudados.HasValue ?
                new ObjectParameter("hdli_SalariosAdeudados", hdli_SalariosAdeudados) :
                new ObjectParameter("hdli_SalariosAdeudados", typeof(decimal));
    
            var hdli_OtrosPagosParameter = hdli_OtrosPagos.HasValue ?
                new ObjectParameter("hdli_OtrosPagos", hdli_OtrosPagos) :
                new ObjectParameter("hdli_OtrosPagos", typeof(decimal));
    
            var hdli_PagoHEPendienteParameter = hdli_PagoHEPendiente.HasValue ?
                new ObjectParameter("hdli_PagoHEPendiente", hdli_PagoHEPendiente) :
                new ObjectParameter("hdli_PagoHEPendiente", typeof(decimal));
    
            var hdli_ValorBonoEducativoParameter = hdli_ValorBonoEducativo.HasValue ?
                new ObjectParameter("hdli_ValorBonoEducativo", hdli_ValorBonoEducativo) :
                new ObjectParameter("hdli_ValorBonoEducativo", typeof(decimal));
    
            var hdli_PagoSeptimoDiaParameter = hdli_PagoSeptimoDia.HasValue ?
                new ObjectParameter("hdli_PagoSeptimoDia", hdli_PagoSeptimoDia) :
                new ObjectParameter("hdli_PagoSeptimoDia", typeof(decimal));
    
            var hdli_BonoPorVacacionesParameter = hdli_BonoPorVacaciones.HasValue ?
                new ObjectParameter("hdli_BonoPorVacaciones", hdli_BonoPorVacaciones) :
                new ObjectParameter("hdli_BonoPorVacaciones", typeof(decimal));
    
            var hdli_ReajusteSalarialParameter = hdli_ReajusteSalarial.HasValue ?
                new ObjectParameter("hdli_ReajusteSalarial", hdli_ReajusteSalarial) :
                new ObjectParameter("hdli_ReajusteSalarial", typeof(decimal));
    
            var hdli_DecimoTercerMesAdeudadoParameter = hdli_DecimoTercerMesAdeudado.HasValue ?
                new ObjectParameter("hdli_DecimoTercerMesAdeudado", hdli_DecimoTercerMesAdeudado) :
                new ObjectParameter("hdli_DecimoTercerMesAdeudado", typeof(decimal));
    
            var hdli_DecimoCuartoMesAdeudadoParameter = hdli_DecimoCuartoMesAdeudado.HasValue ?
                new ObjectParameter("hdli_DecimoCuartoMesAdeudado", hdli_DecimoCuartoMesAdeudado) :
                new ObjectParameter("hdli_DecimoCuartoMesAdeudado", typeof(decimal));
    
            var hdli_BonificacionVacacionesParameter = hdli_BonificacionVacaciones.HasValue ?
                new ObjectParameter("hdli_BonificacionVacaciones", hdli_BonificacionVacaciones) :
                new ObjectParameter("hdli_BonificacionVacaciones", typeof(decimal));
    
            var hdli_PagoPorEmbarazoParameter = hdli_PagoPorEmbarazo.HasValue ?
                new ObjectParameter("hdli_PagoPorEmbarazo", hdli_PagoPorEmbarazo) :
                new ObjectParameter("hdli_PagoPorEmbarazo", typeof(decimal));
    
            var hdli_PagoPorLactanciaParameter = hdli_PagoPorLactancia.HasValue ?
                new ObjectParameter("hdli_PagoPorLactancia", hdli_PagoPorLactancia) :
                new ObjectParameter("hdli_PagoPorLactancia", typeof(decimal));
    
            var hdli_PrePosNatalParameter = hdli_PrePosNatal.HasValue ?
                new ObjectParameter("hdli_PrePosNatal", hdli_PrePosNatal) :
                new ObjectParameter("hdli_PrePosNatal", typeof(decimal));
    
            var hdli_PagoPorDiasFeriadoParameter = hdli_PagoPorDiasFeriado.HasValue ?
                new ObjectParameter("hdli_PagoPorDiasFeriado", hdli_PagoPorDiasFeriado) :
                new ObjectParameter("hdli_PagoPorDiasFeriado", typeof(decimal));
    
            var hdli_MontoTotalLiquidacionParameter = hdli_MontoTotalLiquidacion.HasValue ?
                new ObjectParameter("hdli_MontoTotalLiquidacion", hdli_MontoTotalLiquidacion) :
                new ObjectParameter("hdli_MontoTotalLiquidacion", typeof(decimal));
    
            var hdli_liqu_UsuarioCreaParameter = hdli_liqu_UsuarioCrea.HasValue ?
                new ObjectParameter("hdli_liqu_UsuarioCrea", hdli_liqu_UsuarioCrea) :
                new ObjectParameter("hdli_liqu_UsuarioCrea", typeof(int));
    
            var hdli_liqu_FechaCreaParameter = hdli_liqu_FechaCrea.HasValue ?
                new ObjectParameter("hdli_liqu_FechaCrea", hdli_liqu_FechaCrea) :
                new ObjectParameter("hdli_liqu_FechaCrea", typeof(System.DateTime));
    
            var moli_IdParameter = moli_Id.HasValue ?
                new ObjectParameter("moli_Id", moli_Id) :
                new ObjectParameter("moli_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbHistorialDeLiquidaciones_Insert_Result>("UDP_Plani_tbHistorialDeLiquidaciones_Insert", emp_IdParameter, hdli_FechaLiquidacionParameter, hdli_SalarioOrdinarioMensual_LiqParameter, hdli_SalarioPromedioMensual_LiqlParameter, hdli_SalarioOrdinarioDiario_LiqParameter, hdli_SalarioPromedioDiario_LiqParameter, hdli_Preaviso_LiqParameter, hdli_Cesantia_LiqParameter, hdli_DecimoTercerMesProporcional_LiqParameter, hdli_DecimoCuartoMesProporcional_LiqParameter, hdli_VacacionesPendientes_LiqParameter, hdli_SalariosAdeudadosParameter, hdli_OtrosPagosParameter, hdli_PagoHEPendienteParameter, hdli_ValorBonoEducativoParameter, hdli_PagoSeptimoDiaParameter, hdli_BonoPorVacacionesParameter, hdli_ReajusteSalarialParameter, hdli_DecimoTercerMesAdeudadoParameter, hdli_DecimoCuartoMesAdeudadoParameter, hdli_BonificacionVacacionesParameter, hdli_PagoPorEmbarazoParameter, hdli_PagoPorLactanciaParameter, hdli_PrePosNatalParameter, hdli_PagoPorDiasFeriadoParameter, hdli_MontoTotalLiquidacionParameter, hdli_liqu_UsuarioCreaParameter, hdli_liqu_FechaCreaParameter, moli_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbHistorialDePago_Insert_Result> UDP_Plani_tbHistorialDePago_Insert(Nullable<int> emp_Id, Nullable<decimal> hipa_SueldoNeto, Nullable<System.DateTime> hipa_FechaInicio, Nullable<System.DateTime> hipa_FechaFin, Nullable<System.DateTime> hipa_FechaPago, Nullable<int> hipa_Anio, Nullable<int> hipa_Mes, Nullable<int> peri_IdPeriodo, Nullable<int> hipa_UsuarioCrea, Nullable<System.DateTime> hipa_FechaCrea, Nullable<decimal> hipa_TotalISR, Nullable<bool> hipa_ISRPendiente, Nullable<decimal> hipa_AFP, Nullable<decimal> hipa_TotalHorasConPermisoJustificado, Nullable<decimal> hipa_TotalComisiones, Nullable<decimal> hipa_TotalHorasExtras, Nullable<decimal> hipa_TotalVacaciones, Nullable<decimal> hipa_TotalSeptimoDia, Nullable<decimal> hipa_AdelantoSueldo, Nullable<decimal> hipa_TotalSalario, Nullable<decimal> hipa_TotalDeduccionesIndividuales, Nullable<decimal> hipa_TotalIngresosIndividuales, Nullable<decimal> hipa_TotalSueldoBruto, Nullable<int> hipa_CantidadUnidadesHorasExtras, Nullable<int> hipa_CantidadUnidadesBonos, Nullable<decimal> hipa_TotalBonos, string hipa_CodigoPlanilla)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var hipa_SueldoNetoParameter = hipa_SueldoNeto.HasValue ?
                new ObjectParameter("hipa_SueldoNeto", hipa_SueldoNeto) :
                new ObjectParameter("hipa_SueldoNeto", typeof(decimal));
    
            var hipa_FechaInicioParameter = hipa_FechaInicio.HasValue ?
                new ObjectParameter("hipa_FechaInicio", hipa_FechaInicio) :
                new ObjectParameter("hipa_FechaInicio", typeof(System.DateTime));
    
            var hipa_FechaFinParameter = hipa_FechaFin.HasValue ?
                new ObjectParameter("hipa_FechaFin", hipa_FechaFin) :
                new ObjectParameter("hipa_FechaFin", typeof(System.DateTime));
    
            var hipa_FechaPagoParameter = hipa_FechaPago.HasValue ?
                new ObjectParameter("hipa_FechaPago", hipa_FechaPago) :
                new ObjectParameter("hipa_FechaPago", typeof(System.DateTime));
    
            var hipa_AnioParameter = hipa_Anio.HasValue ?
                new ObjectParameter("hipa_Anio", hipa_Anio) :
                new ObjectParameter("hipa_Anio", typeof(int));
    
            var hipa_MesParameter = hipa_Mes.HasValue ?
                new ObjectParameter("hipa_Mes", hipa_Mes) :
                new ObjectParameter("hipa_Mes", typeof(int));
    
            var peri_IdPeriodoParameter = peri_IdPeriodo.HasValue ?
                new ObjectParameter("peri_IdPeriodo", peri_IdPeriodo) :
                new ObjectParameter("peri_IdPeriodo", typeof(int));
    
            var hipa_UsuarioCreaParameter = hipa_UsuarioCrea.HasValue ?
                new ObjectParameter("hipa_UsuarioCrea", hipa_UsuarioCrea) :
                new ObjectParameter("hipa_UsuarioCrea", typeof(int));
    
            var hipa_FechaCreaParameter = hipa_FechaCrea.HasValue ?
                new ObjectParameter("hipa_FechaCrea", hipa_FechaCrea) :
                new ObjectParameter("hipa_FechaCrea", typeof(System.DateTime));
    
            var hipa_TotalISRParameter = hipa_TotalISR.HasValue ?
                new ObjectParameter("hipa_TotalISR", hipa_TotalISR) :
                new ObjectParameter("hipa_TotalISR", typeof(decimal));
    
            var hipa_ISRPendienteParameter = hipa_ISRPendiente.HasValue ?
                new ObjectParameter("hipa_ISRPendiente", hipa_ISRPendiente) :
                new ObjectParameter("hipa_ISRPendiente", typeof(bool));
    
            var hipa_AFPParameter = hipa_AFP.HasValue ?
                new ObjectParameter("hipa_AFP", hipa_AFP) :
                new ObjectParameter("hipa_AFP", typeof(decimal));
    
            var hipa_TotalHorasConPermisoJustificadoParameter = hipa_TotalHorasConPermisoJustificado.HasValue ?
                new ObjectParameter("hipa_TotalHorasConPermisoJustificado", hipa_TotalHorasConPermisoJustificado) :
                new ObjectParameter("hipa_TotalHorasConPermisoJustificado", typeof(decimal));
    
            var hipa_TotalComisionesParameter = hipa_TotalComisiones.HasValue ?
                new ObjectParameter("hipa_TotalComisiones", hipa_TotalComisiones) :
                new ObjectParameter("hipa_TotalComisiones", typeof(decimal));
    
            var hipa_TotalHorasExtrasParameter = hipa_TotalHorasExtras.HasValue ?
                new ObjectParameter("hipa_TotalHorasExtras", hipa_TotalHorasExtras) :
                new ObjectParameter("hipa_TotalHorasExtras", typeof(decimal));
    
            var hipa_TotalVacacionesParameter = hipa_TotalVacaciones.HasValue ?
                new ObjectParameter("hipa_TotalVacaciones", hipa_TotalVacaciones) :
                new ObjectParameter("hipa_TotalVacaciones", typeof(decimal));
    
            var hipa_TotalSeptimoDiaParameter = hipa_TotalSeptimoDia.HasValue ?
                new ObjectParameter("hipa_TotalSeptimoDia", hipa_TotalSeptimoDia) :
                new ObjectParameter("hipa_TotalSeptimoDia", typeof(decimal));
    
            var hipa_AdelantoSueldoParameter = hipa_AdelantoSueldo.HasValue ?
                new ObjectParameter("hipa_AdelantoSueldo", hipa_AdelantoSueldo) :
                new ObjectParameter("hipa_AdelantoSueldo", typeof(decimal));
    
            var hipa_TotalSalarioParameter = hipa_TotalSalario.HasValue ?
                new ObjectParameter("hipa_TotalSalario", hipa_TotalSalario) :
                new ObjectParameter("hipa_TotalSalario", typeof(decimal));
    
            var hipa_TotalDeduccionesIndividualesParameter = hipa_TotalDeduccionesIndividuales.HasValue ?
                new ObjectParameter("hipa_TotalDeduccionesIndividuales", hipa_TotalDeduccionesIndividuales) :
                new ObjectParameter("hipa_TotalDeduccionesIndividuales", typeof(decimal));
    
            var hipa_TotalIngresosIndividualesParameter = hipa_TotalIngresosIndividuales.HasValue ?
                new ObjectParameter("hipa_TotalIngresosIndividuales", hipa_TotalIngresosIndividuales) :
                new ObjectParameter("hipa_TotalIngresosIndividuales", typeof(decimal));
    
            var hipa_TotalSueldoBrutoParameter = hipa_TotalSueldoBruto.HasValue ?
                new ObjectParameter("hipa_TotalSueldoBruto", hipa_TotalSueldoBruto) :
                new ObjectParameter("hipa_TotalSueldoBruto", typeof(decimal));
    
            var hipa_CantidadUnidadesHorasExtrasParameter = hipa_CantidadUnidadesHorasExtras.HasValue ?
                new ObjectParameter("hipa_CantidadUnidadesHorasExtras", hipa_CantidadUnidadesHorasExtras) :
                new ObjectParameter("hipa_CantidadUnidadesHorasExtras", typeof(int));
    
            var hipa_CantidadUnidadesBonosParameter = hipa_CantidadUnidadesBonos.HasValue ?
                new ObjectParameter("hipa_CantidadUnidadesBonos", hipa_CantidadUnidadesBonos) :
                new ObjectParameter("hipa_CantidadUnidadesBonos", typeof(int));
    
            var hipa_TotalBonosParameter = hipa_TotalBonos.HasValue ?
                new ObjectParameter("hipa_TotalBonos", hipa_TotalBonos) :
                new ObjectParameter("hipa_TotalBonos", typeof(decimal));
    
            var hipa_CodigoPlanillaParameter = hipa_CodigoPlanilla != null ?
                new ObjectParameter("hipa_CodigoPlanilla", hipa_CodigoPlanilla) :
                new ObjectParameter("hipa_CodigoPlanilla", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbHistorialDePago_Insert_Result>("UDP_Plani_tbHistorialDePago_Insert", emp_IdParameter, hipa_SueldoNetoParameter, hipa_FechaInicioParameter, hipa_FechaFinParameter, hipa_FechaPagoParameter, hipa_AnioParameter, hipa_MesParameter, peri_IdPeriodoParameter, hipa_UsuarioCreaParameter, hipa_FechaCreaParameter, hipa_TotalISRParameter, hipa_ISRPendienteParameter, hipa_AFPParameter, hipa_TotalHorasConPermisoJustificadoParameter, hipa_TotalComisionesParameter, hipa_TotalHorasExtrasParameter, hipa_TotalVacacionesParameter, hipa_TotalSeptimoDiaParameter, hipa_AdelantoSueldoParameter, hipa_TotalSalarioParameter, hipa_TotalDeduccionesIndividualesParameter, hipa_TotalIngresosIndividualesParameter, hipa_TotalSueldoBrutoParameter, hipa_CantidadUnidadesHorasExtrasParameter, hipa_CantidadUnidadesBonosParameter, hipa_TotalBonosParameter, hipa_CodigoPlanillaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbIngresosIndividuales_Activar_Result> UDP_Plani_tbIngresosIndividuales_Activar(Nullable<int> ini_IdIngresosIndividuales, Nullable<int> ini_UsuarioModifica, Nullable<System.DateTime> ini_FechaModifica)
        {
            var ini_IdIngresosIndividualesParameter = ini_IdIngresosIndividuales.HasValue ?
                new ObjectParameter("ini_IdIngresosIndividuales", ini_IdIngresosIndividuales) :
                new ObjectParameter("ini_IdIngresosIndividuales", typeof(int));
    
            var ini_UsuarioModificaParameter = ini_UsuarioModifica.HasValue ?
                new ObjectParameter("ini_UsuarioModifica", ini_UsuarioModifica) :
                new ObjectParameter("ini_UsuarioModifica", typeof(int));
    
            var ini_FechaModificaParameter = ini_FechaModifica.HasValue ?
                new ObjectParameter("ini_FechaModifica", ini_FechaModifica) :
                new ObjectParameter("ini_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbIngresosIndividuales_Activar_Result>("UDP_Plani_tbIngresosIndividuales_Activar", ini_IdIngresosIndividualesParameter, ini_UsuarioModificaParameter, ini_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbIngresosIndividuales_Inactivar_Result> UDP_Plani_tbIngresosIndividuales_Inactivar(Nullable<int> ini_IdIngresosIndividuales, Nullable<int> ini_UsuarioModifica, Nullable<System.DateTime> ini_FechaModifica)
        {
            var ini_IdIngresosIndividualesParameter = ini_IdIngresosIndividuales.HasValue ?
                new ObjectParameter("ini_IdIngresosIndividuales", ini_IdIngresosIndividuales) :
                new ObjectParameter("ini_IdIngresosIndividuales", typeof(int));
    
            var ini_UsuarioModificaParameter = ini_UsuarioModifica.HasValue ?
                new ObjectParameter("ini_UsuarioModifica", ini_UsuarioModifica) :
                new ObjectParameter("ini_UsuarioModifica", typeof(int));
    
            var ini_FechaModificaParameter = ini_FechaModifica.HasValue ?
                new ObjectParameter("ini_FechaModifica", ini_FechaModifica) :
                new ObjectParameter("ini_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbIngresosIndividuales_Inactivar_Result>("UDP_Plani_tbIngresosIndividuales_Inactivar", ini_IdIngresosIndividualesParameter, ini_UsuarioModificaParameter, ini_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbIngresosIndividuales_Insert_Result> UDP_Plani_tbIngresosIndividuales_Insert(string ini_Motivo, Nullable<int> emp_Id, Nullable<decimal> ini_Monto, Nullable<bool> ini_Pagado, Nullable<bool> ini_PagaSiempre, string ini_Comentario, Nullable<int> ini_UsuarioCrea, Nullable<System.DateTime> ini_FechaCrea)
        {
            var ini_MotivoParameter = ini_Motivo != null ?
                new ObjectParameter("ini_Motivo", ini_Motivo) :
                new ObjectParameter("ini_Motivo", typeof(string));
    
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var ini_MontoParameter = ini_Monto.HasValue ?
                new ObjectParameter("ini_Monto", ini_Monto) :
                new ObjectParameter("ini_Monto", typeof(decimal));
    
            var ini_PagadoParameter = ini_Pagado.HasValue ?
                new ObjectParameter("ini_Pagado", ini_Pagado) :
                new ObjectParameter("ini_Pagado", typeof(bool));
    
            var ini_PagaSiempreParameter = ini_PagaSiempre.HasValue ?
                new ObjectParameter("ini_PagaSiempre", ini_PagaSiempre) :
                new ObjectParameter("ini_PagaSiempre", typeof(bool));
    
            var ini_ComentarioParameter = ini_Comentario != null ?
                new ObjectParameter("ini_Comentario", ini_Comentario) :
                new ObjectParameter("ini_Comentario", typeof(string));
    
            var ini_UsuarioCreaParameter = ini_UsuarioCrea.HasValue ?
                new ObjectParameter("ini_UsuarioCrea", ini_UsuarioCrea) :
                new ObjectParameter("ini_UsuarioCrea", typeof(int));
    
            var ini_FechaCreaParameter = ini_FechaCrea.HasValue ?
                new ObjectParameter("ini_FechaCrea", ini_FechaCrea) :
                new ObjectParameter("ini_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbIngresosIndividuales_Insert_Result>("UDP_Plani_tbIngresosIndividuales_Insert", ini_MotivoParameter, emp_IdParameter, ini_MontoParameter, ini_PagadoParameter, ini_PagaSiempreParameter, ini_ComentarioParameter, ini_UsuarioCreaParameter, ini_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbIngresosIndividuales_Update_Result> UDP_Plani_tbIngresosIndividuales_Update(Nullable<int> ini_IdIngresosIndividuales, string ini_Motivo, Nullable<int> emp_Id, Nullable<decimal> ini_Monto, Nullable<bool> ini_PagaSiempre, Nullable<int> ini_UsuarioModifica, Nullable<System.DateTime> ini_FechaModifica, string ini_Comentario)
        {
            var ini_IdIngresosIndividualesParameter = ini_IdIngresosIndividuales.HasValue ?
                new ObjectParameter("ini_IdIngresosIndividuales", ini_IdIngresosIndividuales) :
                new ObjectParameter("ini_IdIngresosIndividuales", typeof(int));
    
            var ini_MotivoParameter = ini_Motivo != null ?
                new ObjectParameter("ini_Motivo", ini_Motivo) :
                new ObjectParameter("ini_Motivo", typeof(string));
    
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var ini_MontoParameter = ini_Monto.HasValue ?
                new ObjectParameter("ini_Monto", ini_Monto) :
                new ObjectParameter("ini_Monto", typeof(decimal));
    
            var ini_PagaSiempreParameter = ini_PagaSiempre.HasValue ?
                new ObjectParameter("ini_PagaSiempre", ini_PagaSiempre) :
                new ObjectParameter("ini_PagaSiempre", typeof(bool));
    
            var ini_UsuarioModificaParameter = ini_UsuarioModifica.HasValue ?
                new ObjectParameter("ini_UsuarioModifica", ini_UsuarioModifica) :
                new ObjectParameter("ini_UsuarioModifica", typeof(int));
    
            var ini_FechaModificaParameter = ini_FechaModifica.HasValue ?
                new ObjectParameter("ini_FechaModifica", ini_FechaModifica) :
                new ObjectParameter("ini_FechaModifica", typeof(System.DateTime));
    
            var ini_ComentarioParameter = ini_Comentario != null ?
                new ObjectParameter("ini_Comentario", ini_Comentario) :
                new ObjectParameter("ini_Comentario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbIngresosIndividuales_Update_Result>("UDP_Plani_tbIngresosIndividuales_Update", ini_IdIngresosIndividualesParameter, ini_MotivoParameter, emp_IdParameter, ini_MontoParameter, ini_PagaSiempreParameter, ini_UsuarioModificaParameter, ini_FechaModificaParameter, ini_ComentarioParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbInstitucionesFinancieras_Activar_Result> UDP_Plani_tbInstitucionesFinancieras_Activar(Nullable<int> insf_IdInstitucionFinanciera, Nullable<int> insf_UsuarioModifica, Nullable<System.DateTime> insf_FechaModifica)
        {
            var insf_IdInstitucionFinancieraParameter = insf_IdInstitucionFinanciera.HasValue ?
                new ObjectParameter("insf_IdInstitucionFinanciera", insf_IdInstitucionFinanciera) :
                new ObjectParameter("insf_IdInstitucionFinanciera", typeof(int));
    
            var insf_UsuarioModificaParameter = insf_UsuarioModifica.HasValue ?
                new ObjectParameter("insf_UsuarioModifica", insf_UsuarioModifica) :
                new ObjectParameter("insf_UsuarioModifica", typeof(int));
    
            var insf_FechaModificaParameter = insf_FechaModifica.HasValue ?
                new ObjectParameter("insf_FechaModifica", insf_FechaModifica) :
                new ObjectParameter("insf_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbInstitucionesFinancieras_Activar_Result>("UDP_Plani_tbInstitucionesFinancieras_Activar", insf_IdInstitucionFinancieraParameter, insf_UsuarioModificaParameter, insf_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbInstitucionesFinancieras_Inactivar_Result> UDP_Plani_tbInstitucionesFinancieras_Inactivar(Nullable<int> insf_IdInstitucionFinanciera, Nullable<int> insf_UsuarioModifica, Nullable<System.DateTime> insf_FechaModifica)
        {
            var insf_IdInstitucionFinancieraParameter = insf_IdInstitucionFinanciera.HasValue ?
                new ObjectParameter("insf_IdInstitucionFinanciera", insf_IdInstitucionFinanciera) :
                new ObjectParameter("insf_IdInstitucionFinanciera", typeof(int));
    
            var insf_UsuarioModificaParameter = insf_UsuarioModifica.HasValue ?
                new ObjectParameter("insf_UsuarioModifica", insf_UsuarioModifica) :
                new ObjectParameter("insf_UsuarioModifica", typeof(int));
    
            var insf_FechaModificaParameter = insf_FechaModifica.HasValue ?
                new ObjectParameter("insf_FechaModifica", insf_FechaModifica) :
                new ObjectParameter("insf_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbInstitucionesFinancieras_Inactivar_Result>("UDP_Plani_tbInstitucionesFinancieras_Inactivar", insf_IdInstitucionFinancieraParameter, insf_UsuarioModificaParameter, insf_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbInstitucionesFinancieras_Insert_Result> UDP_Plani_tbInstitucionesFinancieras_Insert(string insf_DescInstitucionFinanc, string insf_Contacto, string insf_Telefono, string insf_Correo, Nullable<int> insf_UsuarioCrea, Nullable<System.DateTime> insf_FechaCrea, Nullable<bool> insf_Activo)
        {
            var insf_DescInstitucionFinancParameter = insf_DescInstitucionFinanc != null ?
                new ObjectParameter("insf_DescInstitucionFinanc", insf_DescInstitucionFinanc) :
                new ObjectParameter("insf_DescInstitucionFinanc", typeof(string));
    
            var insf_ContactoParameter = insf_Contacto != null ?
                new ObjectParameter("insf_Contacto", insf_Contacto) :
                new ObjectParameter("insf_Contacto", typeof(string));
    
            var insf_TelefonoParameter = insf_Telefono != null ?
                new ObjectParameter("insf_Telefono", insf_Telefono) :
                new ObjectParameter("insf_Telefono", typeof(string));
    
            var insf_CorreoParameter = insf_Correo != null ?
                new ObjectParameter("insf_Correo", insf_Correo) :
                new ObjectParameter("insf_Correo", typeof(string));
    
            var insf_UsuarioCreaParameter = insf_UsuarioCrea.HasValue ?
                new ObjectParameter("insf_UsuarioCrea", insf_UsuarioCrea) :
                new ObjectParameter("insf_UsuarioCrea", typeof(int));
    
            var insf_FechaCreaParameter = insf_FechaCrea.HasValue ?
                new ObjectParameter("insf_FechaCrea", insf_FechaCrea) :
                new ObjectParameter("insf_FechaCrea", typeof(System.DateTime));
    
            var insf_ActivoParameter = insf_Activo.HasValue ?
                new ObjectParameter("insf_Activo", insf_Activo) :
                new ObjectParameter("insf_Activo", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbInstitucionesFinancieras_Insert_Result>("UDP_Plani_tbInstitucionesFinancieras_Insert", insf_DescInstitucionFinancParameter, insf_ContactoParameter, insf_TelefonoParameter, insf_CorreoParameter, insf_UsuarioCreaParameter, insf_FechaCreaParameter, insf_ActivoParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbISR_Activar_Result> UDP_Plani_tbISR_Activar(Nullable<int> isr_Id, Nullable<int> isr_UsuarioModifica, Nullable<System.DateTime> isr_FechaModifica)
        {
            var isr_IdParameter = isr_Id.HasValue ?
                new ObjectParameter("isr_Id", isr_Id) :
                new ObjectParameter("isr_Id", typeof(int));
    
            var isr_UsuarioModificaParameter = isr_UsuarioModifica.HasValue ?
                new ObjectParameter("isr_UsuarioModifica", isr_UsuarioModifica) :
                new ObjectParameter("isr_UsuarioModifica", typeof(int));
    
            var isr_FechaModificaParameter = isr_FechaModifica.HasValue ?
                new ObjectParameter("isr_FechaModifica", isr_FechaModifica) :
                new ObjectParameter("isr_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbISR_Activar_Result>("UDP_Plani_tbISR_Activar", isr_IdParameter, isr_UsuarioModificaParameter, isr_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbISR_Inactivar_Result> UDP_Plani_tbISR_Inactivar(Nullable<int> isr_Id, Nullable<int> isr_UsuarioModifica, Nullable<System.DateTime> isr_FechaModifica)
        {
            var isr_IdParameter = isr_Id.HasValue ?
                new ObjectParameter("isr_Id", isr_Id) :
                new ObjectParameter("isr_Id", typeof(int));
    
            var isr_UsuarioModificaParameter = isr_UsuarioModifica.HasValue ?
                new ObjectParameter("isr_UsuarioModifica", isr_UsuarioModifica) :
                new ObjectParameter("isr_UsuarioModifica", typeof(int));
    
            var isr_FechaModificaParameter = isr_FechaModifica.HasValue ?
                new ObjectParameter("isr_FechaModifica", isr_FechaModifica) :
                new ObjectParameter("isr_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbISR_Inactivar_Result>("UDP_Plani_tbISR_Inactivar", isr_IdParameter, isr_UsuarioModificaParameter, isr_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbISR_Insert_Result> UDP_Plani_tbISR_Insert(Nullable<decimal> isr_RangoInicial, Nullable<decimal> isr_RangoFinal, Nullable<decimal> isr_Porcentaje, Nullable<int> tde_IdTipoDedu, Nullable<int> isr_UsuarioCrea, Nullable<System.DateTime> isr_FechaCrea)
        {
            var isr_RangoInicialParameter = isr_RangoInicial.HasValue ?
                new ObjectParameter("isr_RangoInicial", isr_RangoInicial) :
                new ObjectParameter("isr_RangoInicial", typeof(decimal));
    
            var isr_RangoFinalParameter = isr_RangoFinal.HasValue ?
                new ObjectParameter("isr_RangoFinal", isr_RangoFinal) :
                new ObjectParameter("isr_RangoFinal", typeof(decimal));
    
            var isr_PorcentajeParameter = isr_Porcentaje.HasValue ?
                new ObjectParameter("isr_Porcentaje", isr_Porcentaje) :
                new ObjectParameter("isr_Porcentaje", typeof(decimal));
    
            var tde_IdTipoDeduParameter = tde_IdTipoDedu.HasValue ?
                new ObjectParameter("tde_IdTipoDedu", tde_IdTipoDedu) :
                new ObjectParameter("tde_IdTipoDedu", typeof(int));
    
            var isr_UsuarioCreaParameter = isr_UsuarioCrea.HasValue ?
                new ObjectParameter("isr_UsuarioCrea", isr_UsuarioCrea) :
                new ObjectParameter("isr_UsuarioCrea", typeof(int));
    
            var isr_FechaCreaParameter = isr_FechaCrea.HasValue ?
                new ObjectParameter("isr_FechaCrea", isr_FechaCrea) :
                new ObjectParameter("isr_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbISR_Insert_Result>("UDP_Plani_tbISR_Insert", isr_RangoInicialParameter, isr_RangoFinalParameter, isr_PorcentajeParameter, tde_IdTipoDeduParameter, isr_UsuarioCreaParameter, isr_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbISR_Update_Result> UDP_Plani_tbISR_Update(Nullable<int> isr_Id, Nullable<decimal> isr_RangoInicial, Nullable<decimal> isr_RangoFinal, Nullable<decimal> isr_Porcentaje, Nullable<int> tde_IdTipoDedu, Nullable<int> isr_UsuarioModifica, Nullable<System.DateTime> isr_FechaModifica)
        {
            var isr_IdParameter = isr_Id.HasValue ?
                new ObjectParameter("isr_Id", isr_Id) :
                new ObjectParameter("isr_Id", typeof(int));
    
            var isr_RangoInicialParameter = isr_RangoInicial.HasValue ?
                new ObjectParameter("isr_RangoInicial", isr_RangoInicial) :
                new ObjectParameter("isr_RangoInicial", typeof(decimal));
    
            var isr_RangoFinalParameter = isr_RangoFinal.HasValue ?
                new ObjectParameter("isr_RangoFinal", isr_RangoFinal) :
                new ObjectParameter("isr_RangoFinal", typeof(decimal));
    
            var isr_PorcentajeParameter = isr_Porcentaje.HasValue ?
                new ObjectParameter("isr_Porcentaje", isr_Porcentaje) :
                new ObjectParameter("isr_Porcentaje", typeof(decimal));
    
            var tde_IdTipoDeduParameter = tde_IdTipoDedu.HasValue ?
                new ObjectParameter("tde_IdTipoDedu", tde_IdTipoDedu) :
                new ObjectParameter("tde_IdTipoDedu", typeof(int));
    
            var isr_UsuarioModificaParameter = isr_UsuarioModifica.HasValue ?
                new ObjectParameter("isr_UsuarioModifica", isr_UsuarioModifica) :
                new ObjectParameter("isr_UsuarioModifica", typeof(int));
    
            var isr_FechaModificaParameter = isr_FechaModifica.HasValue ?
                new ObjectParameter("isr_FechaModifica", isr_FechaModifica) :
                new ObjectParameter("isr_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbISR_Update_Result>("UDP_Plani_tbISR_Update", isr_IdParameter, isr_RangoInicialParameter, isr_RangoFinalParameter, isr_PorcentajeParameter, tde_IdTipoDeduParameter, isr_UsuarioModificaParameter, isr_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbPeriodos_Activar_Result> UDP_Plani_tbPeriodos_Activar(Nullable<int> peri_IdPeriodo, Nullable<int> peri_UsuarioModifica, Nullable<System.DateTime> peri_FechaModifica)
        {
            var peri_IdPeriodoParameter = peri_IdPeriodo.HasValue ?
                new ObjectParameter("peri_IdPeriodo", peri_IdPeriodo) :
                new ObjectParameter("peri_IdPeriodo", typeof(int));
    
            var peri_UsuarioModificaParameter = peri_UsuarioModifica.HasValue ?
                new ObjectParameter("peri_UsuarioModifica", peri_UsuarioModifica) :
                new ObjectParameter("peri_UsuarioModifica", typeof(int));
    
            var peri_FechaModificaParameter = peri_FechaModifica.HasValue ?
                new ObjectParameter("peri_FechaModifica", peri_FechaModifica) :
                new ObjectParameter("peri_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbPeriodos_Activar_Result>("UDP_Plani_tbPeriodos_Activar", peri_IdPeriodoParameter, peri_UsuarioModificaParameter, peri_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbPeriodos_Inactivar_Result> UDP_Plani_tbPeriodos_Inactivar(Nullable<int> peri_IdPeriodo, Nullable<int> peri_UsuarioModifica, Nullable<System.DateTime> peri_FechaModifica)
        {
            var peri_IdPeriodoParameter = peri_IdPeriodo.HasValue ?
                new ObjectParameter("peri_IdPeriodo", peri_IdPeriodo) :
                new ObjectParameter("peri_IdPeriodo", typeof(int));
    
            var peri_UsuarioModificaParameter = peri_UsuarioModifica.HasValue ?
                new ObjectParameter("peri_UsuarioModifica", peri_UsuarioModifica) :
                new ObjectParameter("peri_UsuarioModifica", typeof(int));
    
            var peri_FechaModificaParameter = peri_FechaModifica.HasValue ?
                new ObjectParameter("peri_FechaModifica", peri_FechaModifica) :
                new ObjectParameter("peri_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbPeriodos_Inactivar_Result>("UDP_Plani_tbPeriodos_Inactivar", peri_IdPeriodoParameter, peri_UsuarioModificaParameter, peri_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbPeriodos_Insert_Result> UDP_Plani_tbPeriodos_Insert(string peri_DescripPeriodo, Nullable<int> peri_UsuarioCrea, Nullable<System.DateTime> peri_FechaCrea, Nullable<bool> peri_RecibeSeptimoDia, Nullable<int> peri_CantidadDias)
        {
            var peri_DescripPeriodoParameter = peri_DescripPeriodo != null ?
                new ObjectParameter("peri_DescripPeriodo", peri_DescripPeriodo) :
                new ObjectParameter("peri_DescripPeriodo", typeof(string));
    
            var peri_UsuarioCreaParameter = peri_UsuarioCrea.HasValue ?
                new ObjectParameter("peri_UsuarioCrea", peri_UsuarioCrea) :
                new ObjectParameter("peri_UsuarioCrea", typeof(int));
    
            var peri_FechaCreaParameter = peri_FechaCrea.HasValue ?
                new ObjectParameter("peri_FechaCrea", peri_FechaCrea) :
                new ObjectParameter("peri_FechaCrea", typeof(System.DateTime));
    
            var peri_RecibeSeptimoDiaParameter = peri_RecibeSeptimoDia.HasValue ?
                new ObjectParameter("peri_RecibeSeptimoDia", peri_RecibeSeptimoDia) :
                new ObjectParameter("peri_RecibeSeptimoDia", typeof(bool));
    
            var peri_CantidadDiasParameter = peri_CantidadDias.HasValue ?
                new ObjectParameter("peri_CantidadDias", peri_CantidadDias) :
                new ObjectParameter("peri_CantidadDias", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbPeriodos_Insert_Result>("UDP_Plani_tbPeriodos_Insert", peri_DescripPeriodoParameter, peri_UsuarioCreaParameter, peri_FechaCreaParameter, peri_RecibeSeptimoDiaParameter, peri_CantidadDiasParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbPeriodos_Update_Result> UDP_Plani_tbPeriodos_Update(Nullable<int> peri_IdPeriodo, string peri_DescripPeriodo, Nullable<int> peri_UsuarioModifica, Nullable<System.DateTime> peri_FechaModifica, Nullable<bool> peri_RecibeSeptimoDia, Nullable<int> peri_CantidadDias)
        {
            var peri_IdPeriodoParameter = peri_IdPeriodo.HasValue ?
                new ObjectParameter("peri_IdPeriodo", peri_IdPeriodo) :
                new ObjectParameter("peri_IdPeriodo", typeof(int));
    
            var peri_DescripPeriodoParameter = peri_DescripPeriodo != null ?
                new ObjectParameter("peri_DescripPeriodo", peri_DescripPeriodo) :
                new ObjectParameter("peri_DescripPeriodo", typeof(string));
    
            var peri_UsuarioModificaParameter = peri_UsuarioModifica.HasValue ?
                new ObjectParameter("peri_UsuarioModifica", peri_UsuarioModifica) :
                new ObjectParameter("peri_UsuarioModifica", typeof(int));
    
            var peri_FechaModificaParameter = peri_FechaModifica.HasValue ?
                new ObjectParameter("peri_FechaModifica", peri_FechaModifica) :
                new ObjectParameter("peri_FechaModifica", typeof(System.DateTime));
    
            var peri_RecibeSeptimoDiaParameter = peri_RecibeSeptimoDia.HasValue ?
                new ObjectParameter("peri_RecibeSeptimoDia", peri_RecibeSeptimoDia) :
                new ObjectParameter("peri_RecibeSeptimoDia", typeof(bool));
    
            var peri_CantidadDiasParameter = peri_CantidadDias.HasValue ?
                new ObjectParameter("peri_CantidadDias", peri_CantidadDias) :
                new ObjectParameter("peri_CantidadDias", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbPeriodos_Update_Result>("UDP_Plani_tbPeriodos_Update", peri_IdPeriodoParameter, peri_DescripPeriodoParameter, peri_UsuarioModificaParameter, peri_FechaModificaParameter, peri_RecibeSeptimoDiaParameter, peri_CantidadDiasParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbPreaviso_Activar_Result> UDP_Plani_tbPreaviso_Activar(Nullable<int> prea_IdPreaviso, Nullable<int> prea_UsuarioModifica, Nullable<System.DateTime> prea_FechaModifica)
        {
            var prea_IdPreavisoParameter = prea_IdPreaviso.HasValue ?
                new ObjectParameter("prea_IdPreaviso", prea_IdPreaviso) :
                new ObjectParameter("prea_IdPreaviso", typeof(int));
    
            var prea_UsuarioModificaParameter = prea_UsuarioModifica.HasValue ?
                new ObjectParameter("prea_UsuarioModifica", prea_UsuarioModifica) :
                new ObjectParameter("prea_UsuarioModifica", typeof(int));
    
            var prea_FechaModificaParameter = prea_FechaModifica.HasValue ?
                new ObjectParameter("prea_FechaModifica", prea_FechaModifica) :
                new ObjectParameter("prea_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbPreaviso_Activar_Result>("UDP_Plani_tbPreaviso_Activar", prea_IdPreavisoParameter, prea_UsuarioModificaParameter, prea_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbPreaviso_Inactivar_Result> UDP_Plani_tbPreaviso_Inactivar(Nullable<int> prea_IdPreaviso, Nullable<int> prea_UsuarioModifica, Nullable<System.DateTime> prea_FechaModifica)
        {
            var prea_IdPreavisoParameter = prea_IdPreaviso.HasValue ?
                new ObjectParameter("prea_IdPreaviso", prea_IdPreaviso) :
                new ObjectParameter("prea_IdPreaviso", typeof(int));
    
            var prea_UsuarioModificaParameter = prea_UsuarioModifica.HasValue ?
                new ObjectParameter("prea_UsuarioModifica", prea_UsuarioModifica) :
                new ObjectParameter("prea_UsuarioModifica", typeof(int));
    
            var prea_FechaModificaParameter = prea_FechaModifica.HasValue ?
                new ObjectParameter("prea_FechaModifica", prea_FechaModifica) :
                new ObjectParameter("prea_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbPreaviso_Inactivar_Result>("UDP_Plani_tbPreaviso_Inactivar", prea_IdPreavisoParameter, prea_UsuarioModificaParameter, prea_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbPreaviso_Insert_Result> UDP_Plani_tbPreaviso_Insert(Nullable<int> prea_RangoInicio, Nullable<int> prea_RangoFin, Nullable<int> prea_DiasPreaviso, Nullable<int> prea_UsuarioCrea, Nullable<System.DateTime> prea_FechaCrea)
        {
            var prea_RangoInicioParameter = prea_RangoInicio.HasValue ?
                new ObjectParameter("prea_RangoInicio", prea_RangoInicio) :
                new ObjectParameter("prea_RangoInicio", typeof(int));
    
            var prea_RangoFinParameter = prea_RangoFin.HasValue ?
                new ObjectParameter("prea_RangoFin", prea_RangoFin) :
                new ObjectParameter("prea_RangoFin", typeof(int));
    
            var prea_DiasPreavisoParameter = prea_DiasPreaviso.HasValue ?
                new ObjectParameter("prea_DiasPreaviso", prea_DiasPreaviso) :
                new ObjectParameter("prea_DiasPreaviso", typeof(int));
    
            var prea_UsuarioCreaParameter = prea_UsuarioCrea.HasValue ?
                new ObjectParameter("prea_UsuarioCrea", prea_UsuarioCrea) :
                new ObjectParameter("prea_UsuarioCrea", typeof(int));
    
            var prea_FechaCreaParameter = prea_FechaCrea.HasValue ?
                new ObjectParameter("prea_FechaCrea", prea_FechaCrea) :
                new ObjectParameter("prea_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbPreaviso_Insert_Result>("UDP_Plani_tbPreaviso_Insert", prea_RangoInicioParameter, prea_RangoFinParameter, prea_DiasPreavisoParameter, prea_UsuarioCreaParameter, prea_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbPreaviso_Update_Result> UDP_Plani_tbPreaviso_Update(Nullable<int> prea_IdPreaviso, Nullable<int> prea_RangoInicio, Nullable<int> prea_RangoFin, Nullable<int> prea_DiasPreaviso, Nullable<int> prea_UsuarioModifica, Nullable<System.DateTime> prea_FechaModifica)
        {
            var prea_IdPreavisoParameter = prea_IdPreaviso.HasValue ?
                new ObjectParameter("prea_IdPreaviso", prea_IdPreaviso) :
                new ObjectParameter("prea_IdPreaviso", typeof(int));
    
            var prea_RangoInicioParameter = prea_RangoInicio.HasValue ?
                new ObjectParameter("prea_RangoInicio", prea_RangoInicio) :
                new ObjectParameter("prea_RangoInicio", typeof(int));
    
            var prea_RangoFinParameter = prea_RangoFin.HasValue ?
                new ObjectParameter("prea_RangoFin", prea_RangoFin) :
                new ObjectParameter("prea_RangoFin", typeof(int));
    
            var prea_DiasPreavisoParameter = prea_DiasPreaviso.HasValue ?
                new ObjectParameter("prea_DiasPreaviso", prea_DiasPreaviso) :
                new ObjectParameter("prea_DiasPreaviso", typeof(int));
    
            var prea_UsuarioModificaParameter = prea_UsuarioModifica.HasValue ?
                new ObjectParameter("prea_UsuarioModifica", prea_UsuarioModifica) :
                new ObjectParameter("prea_UsuarioModifica", typeof(int));
    
            var prea_FechaModificaParameter = prea_FechaModifica.HasValue ?
                new ObjectParameter("prea_FechaModifica", prea_FechaModifica) :
                new ObjectParameter("prea_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbPreaviso_Update_Result>("UDP_Plani_tbPreaviso_Update", prea_IdPreavisoParameter, prea_RangoInicioParameter, prea_RangoFinParameter, prea_DiasPreavisoParameter, prea_UsuarioModificaParameter, prea_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbTechoImpuestoVecinal_Activar_Result> UDP_Plani_tbTechoImpuestoVecinal_Activar(Nullable<int> timv_IdTechoImpuestoVecinal, Nullable<int> timv_UsuarioModifica, Nullable<System.DateTime> timv_FechaModifica)
        {
            var timv_IdTechoImpuestoVecinalParameter = timv_IdTechoImpuestoVecinal.HasValue ?
                new ObjectParameter("timv_IdTechoImpuestoVecinal", timv_IdTechoImpuestoVecinal) :
                new ObjectParameter("timv_IdTechoImpuestoVecinal", typeof(int));
    
            var timv_UsuarioModificaParameter = timv_UsuarioModifica.HasValue ?
                new ObjectParameter("timv_UsuarioModifica", timv_UsuarioModifica) :
                new ObjectParameter("timv_UsuarioModifica", typeof(int));
    
            var timv_FechaModificaParameter = timv_FechaModifica.HasValue ?
                new ObjectParameter("timv_FechaModifica", timv_FechaModifica) :
                new ObjectParameter("timv_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbTechoImpuestoVecinal_Activar_Result>("UDP_Plani_tbTechoImpuestoVecinal_Activar", timv_IdTechoImpuestoVecinalParameter, timv_UsuarioModificaParameter, timv_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbTechoImpuestoVecinal_Inactivar_Result> UDP_Plani_tbTechoImpuestoVecinal_Inactivar(Nullable<int> timv_IdTechoImpuestoVecinal, Nullable<int> timv_UsuarioModifica, Nullable<System.DateTime> timv_FechaModifica)
        {
            var timv_IdTechoImpuestoVecinalParameter = timv_IdTechoImpuestoVecinal.HasValue ?
                new ObjectParameter("timv_IdTechoImpuestoVecinal", timv_IdTechoImpuestoVecinal) :
                new ObjectParameter("timv_IdTechoImpuestoVecinal", typeof(int));
    
            var timv_UsuarioModificaParameter = timv_UsuarioModifica.HasValue ?
                new ObjectParameter("timv_UsuarioModifica", timv_UsuarioModifica) :
                new ObjectParameter("timv_UsuarioModifica", typeof(int));
    
            var timv_FechaModificaParameter = timv_FechaModifica.HasValue ?
                new ObjectParameter("timv_FechaModifica", timv_FechaModifica) :
                new ObjectParameter("timv_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbTechoImpuestoVecinal_Inactivar_Result>("UDP_Plani_tbTechoImpuestoVecinal_Inactivar", timv_IdTechoImpuestoVecinalParameter, timv_UsuarioModificaParameter, timv_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbTechoImpuestoVecinal_Insert_Result> UDP_Plani_tbTechoImpuestoVecinal_Insert(string mun_Codigo, Nullable<int> tde_IdTipoDedu, Nullable<decimal> timv_RangoInicio, Nullable<decimal> timv_RangoFin, Nullable<decimal> timv_Rango, Nullable<decimal> timv_Impuesto, Nullable<int> timv_UsuarioCrea, Nullable<System.DateTime> timv_FechaCrea)
        {
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            var tde_IdTipoDeduParameter = tde_IdTipoDedu.HasValue ?
                new ObjectParameter("tde_IdTipoDedu", tde_IdTipoDedu) :
                new ObjectParameter("tde_IdTipoDedu", typeof(int));
    
            var timv_RangoInicioParameter = timv_RangoInicio.HasValue ?
                new ObjectParameter("timv_RangoInicio", timv_RangoInicio) :
                new ObjectParameter("timv_RangoInicio", typeof(decimal));
    
            var timv_RangoFinParameter = timv_RangoFin.HasValue ?
                new ObjectParameter("timv_RangoFin", timv_RangoFin) :
                new ObjectParameter("timv_RangoFin", typeof(decimal));
    
            var timv_RangoParameter = timv_Rango.HasValue ?
                new ObjectParameter("timv_Rango", timv_Rango) :
                new ObjectParameter("timv_Rango", typeof(decimal));
    
            var timv_ImpuestoParameter = timv_Impuesto.HasValue ?
                new ObjectParameter("timv_Impuesto", timv_Impuesto) :
                new ObjectParameter("timv_Impuesto", typeof(decimal));
    
            var timv_UsuarioCreaParameter = timv_UsuarioCrea.HasValue ?
                new ObjectParameter("timv_UsuarioCrea", timv_UsuarioCrea) :
                new ObjectParameter("timv_UsuarioCrea", typeof(int));
    
            var timv_FechaCreaParameter = timv_FechaCrea.HasValue ?
                new ObjectParameter("timv_FechaCrea", timv_FechaCrea) :
                new ObjectParameter("timv_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbTechoImpuestoVecinal_Insert_Result>("UDP_Plani_tbTechoImpuestoVecinal_Insert", mun_CodigoParameter, tde_IdTipoDeduParameter, timv_RangoInicioParameter, timv_RangoFinParameter, timv_RangoParameter, timv_ImpuestoParameter, timv_UsuarioCreaParameter, timv_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbTechoImpuestoVecinal_Update_Result> UDP_Plani_tbTechoImpuestoVecinal_Update(Nullable<int> timv_IdTechoImpuestoVecinal, string mun_Codigo, Nullable<int> tde_IdTipoDedu, Nullable<decimal> timv_RangoInicio, Nullable<decimal> timv_RangoFin, Nullable<decimal> timv_Rango, Nullable<decimal> timv_Impuesto, Nullable<int> timv_UsuarioModifica, Nullable<System.DateTime> timv_FechaModifica)
        {
            var timv_IdTechoImpuestoVecinalParameter = timv_IdTechoImpuestoVecinal.HasValue ?
                new ObjectParameter("timv_IdTechoImpuestoVecinal", timv_IdTechoImpuestoVecinal) :
                new ObjectParameter("timv_IdTechoImpuestoVecinal", typeof(int));
    
            var mun_CodigoParameter = mun_Codigo != null ?
                new ObjectParameter("mun_Codigo", mun_Codigo) :
                new ObjectParameter("mun_Codigo", typeof(string));
    
            var tde_IdTipoDeduParameter = tde_IdTipoDedu.HasValue ?
                new ObjectParameter("tde_IdTipoDedu", tde_IdTipoDedu) :
                new ObjectParameter("tde_IdTipoDedu", typeof(int));
    
            var timv_RangoInicioParameter = timv_RangoInicio.HasValue ?
                new ObjectParameter("timv_RangoInicio", timv_RangoInicio) :
                new ObjectParameter("timv_RangoInicio", typeof(decimal));
    
            var timv_RangoFinParameter = timv_RangoFin.HasValue ?
                new ObjectParameter("timv_RangoFin", timv_RangoFin) :
                new ObjectParameter("timv_RangoFin", typeof(decimal));
    
            var timv_RangoParameter = timv_Rango.HasValue ?
                new ObjectParameter("timv_Rango", timv_Rango) :
                new ObjectParameter("timv_Rango", typeof(decimal));
    
            var timv_ImpuestoParameter = timv_Impuesto.HasValue ?
                new ObjectParameter("timv_Impuesto", timv_Impuesto) :
                new ObjectParameter("timv_Impuesto", typeof(decimal));
    
            var timv_UsuarioModificaParameter = timv_UsuarioModifica.HasValue ?
                new ObjectParameter("timv_UsuarioModifica", timv_UsuarioModifica) :
                new ObjectParameter("timv_UsuarioModifica", typeof(int));
    
            var timv_FechaModificaParameter = timv_FechaModifica.HasValue ?
                new ObjectParameter("timv_FechaModifica", timv_FechaModifica) :
                new ObjectParameter("timv_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbTechoImpuestoVecinal_Update_Result>("UDP_Plani_tbTechoImpuestoVecinal_Update", timv_IdTechoImpuestoVecinalParameter, mun_CodigoParameter, tde_IdTipoDeduParameter, timv_RangoInicioParameter, timv_RangoFinParameter, timv_RangoParameter, timv_ImpuestoParameter, timv_UsuarioModificaParameter, timv_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbTechosComisiones_Activar_Result> UDP_Plani_tbTechosComisiones_Activar(Nullable<int> tc_Id, Nullable<int> tc_UsuarioModifica, Nullable<System.DateTime> tc_FechaModifica)
        {
            var tc_IdParameter = tc_Id.HasValue ?
                new ObjectParameter("tc_Id", tc_Id) :
                new ObjectParameter("tc_Id", typeof(int));
    
            var tc_UsuarioModificaParameter = tc_UsuarioModifica.HasValue ?
                new ObjectParameter("tc_UsuarioModifica", tc_UsuarioModifica) :
                new ObjectParameter("tc_UsuarioModifica", typeof(int));
    
            var tc_FechaModificaParameter = tc_FechaModifica.HasValue ?
                new ObjectParameter("tc_FechaModifica", tc_FechaModifica) :
                new ObjectParameter("tc_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbTechosComisiones_Activar_Result>("UDP_Plani_tbTechosComisiones_Activar", tc_IdParameter, tc_UsuarioModificaParameter, tc_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbTechosComisiones_Inactivar_Result> UDP_Plani_tbTechosComisiones_Inactivar(Nullable<int> tc_Id, Nullable<int> tc_UsuarioModifica, Nullable<System.DateTime> tc_FechaModifica)
        {
            var tc_IdParameter = tc_Id.HasValue ?
                new ObjectParameter("tc_Id", tc_Id) :
                new ObjectParameter("tc_Id", typeof(int));
    
            var tc_UsuarioModificaParameter = tc_UsuarioModifica.HasValue ?
                new ObjectParameter("tc_UsuarioModifica", tc_UsuarioModifica) :
                new ObjectParameter("tc_UsuarioModifica", typeof(int));
    
            var tc_FechaModificaParameter = tc_FechaModifica.HasValue ?
                new ObjectParameter("tc_FechaModifica", tc_FechaModifica) :
                new ObjectParameter("tc_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbTechosComisiones_Inactivar_Result>("UDP_Plani_tbTechosComisiones_Inactivar", tc_IdParameter, tc_UsuarioModificaParameter, tc_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbTechosComisiones_Insert_Result> UDP_Plani_tbTechosComisiones_Insert(Nullable<int> cin_IdIngreso, Nullable<decimal> tc_RangoInicio, Nullable<decimal> tc_RangoFin, Nullable<decimal> tc_PorcentajeComision, Nullable<int> tc_UsuarioCrea, Nullable<System.DateTime> tc_FechaCrea)
        {
            var cin_IdIngresoParameter = cin_IdIngreso.HasValue ?
                new ObjectParameter("cin_IdIngreso", cin_IdIngreso) :
                new ObjectParameter("cin_IdIngreso", typeof(int));
    
            var tc_RangoInicioParameter = tc_RangoInicio.HasValue ?
                new ObjectParameter("tc_RangoInicio", tc_RangoInicio) :
                new ObjectParameter("tc_RangoInicio", typeof(decimal));
    
            var tc_RangoFinParameter = tc_RangoFin.HasValue ?
                new ObjectParameter("tc_RangoFin", tc_RangoFin) :
                new ObjectParameter("tc_RangoFin", typeof(decimal));
    
            var tc_PorcentajeComisionParameter = tc_PorcentajeComision.HasValue ?
                new ObjectParameter("tc_PorcentajeComision", tc_PorcentajeComision) :
                new ObjectParameter("tc_PorcentajeComision", typeof(decimal));
    
            var tc_UsuarioCreaParameter = tc_UsuarioCrea.HasValue ?
                new ObjectParameter("tc_UsuarioCrea", tc_UsuarioCrea) :
                new ObjectParameter("tc_UsuarioCrea", typeof(int));
    
            var tc_FechaCreaParameter = tc_FechaCrea.HasValue ?
                new ObjectParameter("tc_FechaCrea", tc_FechaCrea) :
                new ObjectParameter("tc_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbTechosComisiones_Insert_Result>("UDP_Plani_tbTechosComisiones_Insert", cin_IdIngresoParameter, tc_RangoInicioParameter, tc_RangoFinParameter, tc_PorcentajeComisionParameter, tc_UsuarioCreaParameter, tc_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbTechosComisiones_Update_Result> UDP_Plani_tbTechosComisiones_Update(Nullable<int> tc_Id, Nullable<int> cin_IdIngreso, Nullable<decimal> tc_RangoInicio, Nullable<decimal> tc_RangoFin, Nullable<decimal> tc_PorcentajeComision, Nullable<int> tc_UsuarioModifica, Nullable<System.DateTime> tc_FechaModifica)
        {
            var tc_IdParameter = tc_Id.HasValue ?
                new ObjectParameter("tc_Id", tc_Id) :
                new ObjectParameter("tc_Id", typeof(int));
    
            var cin_IdIngresoParameter = cin_IdIngreso.HasValue ?
                new ObjectParameter("cin_IdIngreso", cin_IdIngreso) :
                new ObjectParameter("cin_IdIngreso", typeof(int));
    
            var tc_RangoInicioParameter = tc_RangoInicio.HasValue ?
                new ObjectParameter("tc_RangoInicio", tc_RangoInicio) :
                new ObjectParameter("tc_RangoInicio", typeof(decimal));
    
            var tc_RangoFinParameter = tc_RangoFin.HasValue ?
                new ObjectParameter("tc_RangoFin", tc_RangoFin) :
                new ObjectParameter("tc_RangoFin", typeof(decimal));
    
            var tc_PorcentajeComisionParameter = tc_PorcentajeComision.HasValue ?
                new ObjectParameter("tc_PorcentajeComision", tc_PorcentajeComision) :
                new ObjectParameter("tc_PorcentajeComision", typeof(decimal));
    
            var tc_UsuarioModificaParameter = tc_UsuarioModifica.HasValue ?
                new ObjectParameter("tc_UsuarioModifica", tc_UsuarioModifica) :
                new ObjectParameter("tc_UsuarioModifica", typeof(int));
    
            var tc_FechaModificaParameter = tc_FechaModifica.HasValue ?
                new ObjectParameter("tc_FechaModifica", tc_FechaModifica) :
                new ObjectParameter("tc_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbTechosComisiones_Update_Result>("UDP_Plani_tbTechosComisiones_Update", tc_IdParameter, cin_IdIngresoParameter, tc_RangoInicioParameter, tc_RangoFinParameter, tc_PorcentajeComisionParameter, tc_UsuarioModificaParameter, tc_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbTechosDeducciones_Activar_Result> UDP_Plani_tbTechosDeducciones_Activar(Nullable<int> tddu_IdTechosDeducciones, Nullable<int> tddu_UsuarioModifica, Nullable<System.DateTime> tddu_FechaModifica)
        {
            var tddu_IdTechosDeduccionesParameter = tddu_IdTechosDeducciones.HasValue ?
                new ObjectParameter("tddu_IdTechosDeducciones", tddu_IdTechosDeducciones) :
                new ObjectParameter("tddu_IdTechosDeducciones", typeof(int));
    
            var tddu_UsuarioModificaParameter = tddu_UsuarioModifica.HasValue ?
                new ObjectParameter("tddu_UsuarioModifica", tddu_UsuarioModifica) :
                new ObjectParameter("tddu_UsuarioModifica", typeof(int));
    
            var tddu_FechaModificaParameter = tddu_FechaModifica.HasValue ?
                new ObjectParameter("tddu_FechaModifica", tddu_FechaModifica) :
                new ObjectParameter("tddu_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbTechosDeducciones_Activar_Result>("UDP_Plani_tbTechosDeducciones_Activar", tddu_IdTechosDeduccionesParameter, tddu_UsuarioModificaParameter, tddu_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbTechosDeducciones_Inactivar_Result> UDP_Plani_tbTechosDeducciones_Inactivar(Nullable<int> tddu_IdTechosDeducciones, Nullable<int> tddu_UsuarioModifica, Nullable<System.DateTime> tddu_FechaModifica)
        {
            var tddu_IdTechosDeduccionesParameter = tddu_IdTechosDeducciones.HasValue ?
                new ObjectParameter("tddu_IdTechosDeducciones", tddu_IdTechosDeducciones) :
                new ObjectParameter("tddu_IdTechosDeducciones", typeof(int));
    
            var tddu_UsuarioModificaParameter = tddu_UsuarioModifica.HasValue ?
                new ObjectParameter("tddu_UsuarioModifica", tddu_UsuarioModifica) :
                new ObjectParameter("tddu_UsuarioModifica", typeof(int));
    
            var tddu_FechaModificaParameter = tddu_FechaModifica.HasValue ?
                new ObjectParameter("tddu_FechaModifica", tddu_FechaModifica) :
                new ObjectParameter("tddu_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbTechosDeducciones_Inactivar_Result>("UDP_Plani_tbTechosDeducciones_Inactivar", tddu_IdTechosDeduccionesParameter, tddu_UsuarioModificaParameter, tddu_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbTechosDeducciones_Insert_Result> UDP_Plani_tbTechosDeducciones_Insert(Nullable<decimal> tddu_PorcentajeColaboradores, Nullable<decimal> tddu_PorcentajeEmpresa, Nullable<decimal> tddu_Techo, Nullable<int> cde_IdDeducciones, Nullable<int> tddu_UsuarioCrea, Nullable<System.DateTime> tddu_FechaCrea)
        {
            var tddu_PorcentajeColaboradoresParameter = tddu_PorcentajeColaboradores.HasValue ?
                new ObjectParameter("tddu_PorcentajeColaboradores", tddu_PorcentajeColaboradores) :
                new ObjectParameter("tddu_PorcentajeColaboradores", typeof(decimal));
    
            var tddu_PorcentajeEmpresaParameter = tddu_PorcentajeEmpresa.HasValue ?
                new ObjectParameter("tddu_PorcentajeEmpresa", tddu_PorcentajeEmpresa) :
                new ObjectParameter("tddu_PorcentajeEmpresa", typeof(decimal));
    
            var tddu_TechoParameter = tddu_Techo.HasValue ?
                new ObjectParameter("tddu_Techo", tddu_Techo) :
                new ObjectParameter("tddu_Techo", typeof(decimal));
    
            var cde_IdDeduccionesParameter = cde_IdDeducciones.HasValue ?
                new ObjectParameter("cde_IdDeducciones", cde_IdDeducciones) :
                new ObjectParameter("cde_IdDeducciones", typeof(int));
    
            var tddu_UsuarioCreaParameter = tddu_UsuarioCrea.HasValue ?
                new ObjectParameter("tddu_UsuarioCrea", tddu_UsuarioCrea) :
                new ObjectParameter("tddu_UsuarioCrea", typeof(int));
    
            var tddu_FechaCreaParameter = tddu_FechaCrea.HasValue ?
                new ObjectParameter("tddu_FechaCrea", tddu_FechaCrea) :
                new ObjectParameter("tddu_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbTechosDeducciones_Insert_Result>("UDP_Plani_tbTechosDeducciones_Insert", tddu_PorcentajeColaboradoresParameter, tddu_PorcentajeEmpresaParameter, tddu_TechoParameter, cde_IdDeduccionesParameter, tddu_UsuarioCreaParameter, tddu_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbTechosDeducciones_Update_Result> UDP_Plani_tbTechosDeducciones_Update(Nullable<int> tddu_IdTechosDeducciones, Nullable<decimal> tddu_PorcentajeColaboradores, Nullable<decimal> tddu_PorcentajeEmpresa, Nullable<decimal> tddu_Techo, Nullable<int> cde_IdDeducciones, Nullable<int> tddu_UsuarioModifica, Nullable<System.DateTime> tddu_FechaModifica)
        {
            var tddu_IdTechosDeduccionesParameter = tddu_IdTechosDeducciones.HasValue ?
                new ObjectParameter("tddu_IdTechosDeducciones", tddu_IdTechosDeducciones) :
                new ObjectParameter("tddu_IdTechosDeducciones", typeof(int));
    
            var tddu_PorcentajeColaboradoresParameter = tddu_PorcentajeColaboradores.HasValue ?
                new ObjectParameter("tddu_PorcentajeColaboradores", tddu_PorcentajeColaboradores) :
                new ObjectParameter("tddu_PorcentajeColaboradores", typeof(decimal));
    
            var tddu_PorcentajeEmpresaParameter = tddu_PorcentajeEmpresa.HasValue ?
                new ObjectParameter("tddu_PorcentajeEmpresa", tddu_PorcentajeEmpresa) :
                new ObjectParameter("tddu_PorcentajeEmpresa", typeof(decimal));
    
            var tddu_TechoParameter = tddu_Techo.HasValue ?
                new ObjectParameter("tddu_Techo", tddu_Techo) :
                new ObjectParameter("tddu_Techo", typeof(decimal));
    
            var cde_IdDeduccionesParameter = cde_IdDeducciones.HasValue ?
                new ObjectParameter("cde_IdDeducciones", cde_IdDeducciones) :
                new ObjectParameter("cde_IdDeducciones", typeof(int));
    
            var tddu_UsuarioModificaParameter = tddu_UsuarioModifica.HasValue ?
                new ObjectParameter("tddu_UsuarioModifica", tddu_UsuarioModifica) :
                new ObjectParameter("tddu_UsuarioModifica", typeof(int));
    
            var tddu_FechaModificaParameter = tddu_FechaModifica.HasValue ?
                new ObjectParameter("tddu_FechaModifica", tddu_FechaModifica) :
                new ObjectParameter("tddu_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbTechosDeducciones_Update_Result>("UDP_Plani_tbTechosDeducciones_Update", tddu_IdTechosDeduccionesParameter, tddu_PorcentajeColaboradoresParameter, tddu_PorcentajeEmpresaParameter, tddu_TechoParameter, cde_IdDeduccionesParameter, tddu_UsuarioModificaParameter, tddu_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbTipoDeduccion_Activar_Result> UDP_Plani_tbTipoDeduccion_Activar(Nullable<int> tde_IdTipoDedu, Nullable<int> tde_UsuarioModifica, Nullable<System.DateTime> tde_FechaModifica)
        {
            var tde_IdTipoDeduParameter = tde_IdTipoDedu.HasValue ?
                new ObjectParameter("tde_IdTipoDedu", tde_IdTipoDedu) :
                new ObjectParameter("tde_IdTipoDedu", typeof(int));
    
            var tde_UsuarioModificaParameter = tde_UsuarioModifica.HasValue ?
                new ObjectParameter("tde_UsuarioModifica", tde_UsuarioModifica) :
                new ObjectParameter("tde_UsuarioModifica", typeof(int));
    
            var tde_FechaModificaParameter = tde_FechaModifica.HasValue ?
                new ObjectParameter("tde_FechaModifica", tde_FechaModifica) :
                new ObjectParameter("tde_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbTipoDeduccion_Activar_Result>("UDP_Plani_tbTipoDeduccion_Activar", tde_IdTipoDeduParameter, tde_UsuarioModificaParameter, tde_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbTipoDeduccion_Inactivar_Result> UDP_Plani_tbTipoDeduccion_Inactivar(Nullable<int> tde_IdTipoDedu, Nullable<int> tde_UsuarioModifica, Nullable<System.DateTime> tde_FechaModifica)
        {
            var tde_IdTipoDeduParameter = tde_IdTipoDedu.HasValue ?
                new ObjectParameter("tde_IdTipoDedu", tde_IdTipoDedu) :
                new ObjectParameter("tde_IdTipoDedu", typeof(int));
    
            var tde_UsuarioModificaParameter = tde_UsuarioModifica.HasValue ?
                new ObjectParameter("tde_UsuarioModifica", tde_UsuarioModifica) :
                new ObjectParameter("tde_UsuarioModifica", typeof(int));
    
            var tde_FechaModificaParameter = tde_FechaModifica.HasValue ?
                new ObjectParameter("tde_FechaModifica", tde_FechaModifica) :
                new ObjectParameter("tde_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbTipoDeduccion_Inactivar_Result>("UDP_Plani_tbTipoDeduccion_Inactivar", tde_IdTipoDeduParameter, tde_UsuarioModificaParameter, tde_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbTipoDeduccion_Insert_Result> UDP_Plani_tbTipoDeduccion_Insert(string tde_Descripcion, Nullable<int> tde_UsuarioCrea, Nullable<System.DateTime> tde_FechaCrea)
        {
            var tde_DescripcionParameter = tde_Descripcion != null ?
                new ObjectParameter("tde_Descripcion", tde_Descripcion) :
                new ObjectParameter("tde_Descripcion", typeof(string));
    
            var tde_UsuarioCreaParameter = tde_UsuarioCrea.HasValue ?
                new ObjectParameter("tde_UsuarioCrea", tde_UsuarioCrea) :
                new ObjectParameter("tde_UsuarioCrea", typeof(int));
    
            var tde_FechaCreaParameter = tde_FechaCrea.HasValue ?
                new ObjectParameter("tde_FechaCrea", tde_FechaCrea) :
                new ObjectParameter("tde_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbTipoDeduccion_Insert_Result>("UDP_Plani_tbTipoDeduccion_Insert", tde_DescripcionParameter, tde_UsuarioCreaParameter, tde_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbTipoDeduccion_Update_Result> UDP_Plani_tbTipoDeduccion_Update(Nullable<int> tde_IdTipoDedu, string tde_Descripcion, Nullable<int> tde_UsuarioModifica, Nullable<System.DateTime> tde_FechaModifica)
        {
            var tde_IdTipoDeduParameter = tde_IdTipoDedu.HasValue ?
                new ObjectParameter("tde_IdTipoDedu", tde_IdTipoDedu) :
                new ObjectParameter("tde_IdTipoDedu", typeof(int));
    
            var tde_DescripcionParameter = tde_Descripcion != null ?
                new ObjectParameter("tde_Descripcion", tde_Descripcion) :
                new ObjectParameter("tde_Descripcion", typeof(string));
    
            var tde_UsuarioModificaParameter = tde_UsuarioModifica.HasValue ?
                new ObjectParameter("tde_UsuarioModifica", tde_UsuarioModifica) :
                new ObjectParameter("tde_UsuarioModifica", typeof(int));
    
            var tde_FechaModificaParameter = tde_FechaModifica.HasValue ?
                new ObjectParameter("tde_FechaModifica", tde_FechaModifica) :
                new ObjectParameter("tde_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbTipoDeduccion_Update_Result>("UDP_Plani_tbTipoDeduccion_Update", tde_IdTipoDeduParameter, tde_DescripcionParameter, tde_UsuarioModificaParameter, tde_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_EmpleadoComisiones_Insert_Result> UDP_Plani_EmpleadoComisiones_Insert(Nullable<int> emp_Id, Nullable<int> cin_IdIngreso, Nullable<System.DateTime> cc_FechaRegistro, Nullable<bool> cc_Pagado, Nullable<int> cc_UsuarioCrea, Nullable<System.DateTime> cc_FechaCrea, Nullable<decimal> cc_TotalComision, Nullable<decimal> cc_TotalVenta)
        {
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            var cin_IdIngresoParameter = cin_IdIngreso.HasValue ?
                new ObjectParameter("cin_IdIngreso", cin_IdIngreso) :
                new ObjectParameter("cin_IdIngreso", typeof(int));
    
            var cc_FechaRegistroParameter = cc_FechaRegistro.HasValue ?
                new ObjectParameter("cc_FechaRegistro", cc_FechaRegistro) :
                new ObjectParameter("cc_FechaRegistro", typeof(System.DateTime));
    
            var cc_PagadoParameter = cc_Pagado.HasValue ?
                new ObjectParameter("cc_Pagado", cc_Pagado) :
                new ObjectParameter("cc_Pagado", typeof(bool));
    
            var cc_UsuarioCreaParameter = cc_UsuarioCrea.HasValue ?
                new ObjectParameter("cc_UsuarioCrea", cc_UsuarioCrea) :
                new ObjectParameter("cc_UsuarioCrea", typeof(int));
    
            var cc_FechaCreaParameter = cc_FechaCrea.HasValue ?
                new ObjectParameter("cc_FechaCrea", cc_FechaCrea) :
                new ObjectParameter("cc_FechaCrea", typeof(System.DateTime));
    
            var cc_TotalComisionParameter = cc_TotalComision.HasValue ?
                new ObjectParameter("cc_TotalComision", cc_TotalComision) :
                new ObjectParameter("cc_TotalComision", typeof(decimal));
    
            var cc_TotalVentaParameter = cc_TotalVenta.HasValue ?
                new ObjectParameter("cc_TotalVenta", cc_TotalVenta) :
                new ObjectParameter("cc_TotalVenta", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_EmpleadoComisiones_Insert_Result>("UDP_Plani_EmpleadoComisiones_Insert", emp_IdParameter, cin_IdIngresoParameter, cc_FechaRegistroParameter, cc_PagadoParameter, cc_UsuarioCreaParameter, cc_FechaCreaParameter, cc_TotalComisionParameter, cc_TotalVentaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbCatalogoDeIngresos_Activar_Result> UDP_Plani_tbCatalogoDeIngresos_Activar(Nullable<int> cin_IdIngreso, Nullable<int> cin_UsuarioModifica, Nullable<System.DateTime> cin_FechaModifica)
        {
            var cin_IdIngresoParameter = cin_IdIngreso.HasValue ?
                new ObjectParameter("cin_IdIngreso", cin_IdIngreso) :
                new ObjectParameter("cin_IdIngreso", typeof(int));
    
            var cin_UsuarioModificaParameter = cin_UsuarioModifica.HasValue ?
                new ObjectParameter("cin_UsuarioModifica", cin_UsuarioModifica) :
                new ObjectParameter("cin_UsuarioModifica", typeof(int));
    
            var cin_FechaModificaParameter = cin_FechaModifica.HasValue ?
                new ObjectParameter("cin_FechaModifica", cin_FechaModifica) :
                new ObjectParameter("cin_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbCatalogoDeIngresos_Activar_Result>("UDP_Plani_tbCatalogoDeIngresos_Activar", cin_IdIngresoParameter, cin_UsuarioModificaParameter, cin_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbCatalogoDeIngresos_Inactivar_Result> UDP_Plani_tbCatalogoDeIngresos_Inactivar(Nullable<int> cin_IdIngreso, Nullable<int> cin_UsuarioModifica, Nullable<System.DateTime> cin_FechaModifica)
        {
            var cin_IdIngresoParameter = cin_IdIngreso.HasValue ?
                new ObjectParameter("cin_IdIngreso", cin_IdIngreso) :
                new ObjectParameter("cin_IdIngreso", typeof(int));
    
            var cin_UsuarioModificaParameter = cin_UsuarioModifica.HasValue ?
                new ObjectParameter("cin_UsuarioModifica", cin_UsuarioModifica) :
                new ObjectParameter("cin_UsuarioModifica", typeof(int));
    
            var cin_FechaModificaParameter = cin_FechaModifica.HasValue ?
                new ObjectParameter("cin_FechaModifica", cin_FechaModifica) :
                new ObjectParameter("cin_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbCatalogoDeIngresos_Inactivar_Result>("UDP_Plani_tbCatalogoDeIngresos_Inactivar", cin_IdIngresoParameter, cin_UsuarioModificaParameter, cin_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbCatalogoDeIngresos_Insert_Result> UDP_Plani_tbCatalogoDeIngresos_Insert(string cin_DescripcionIngreso, Nullable<int> cin_TipoIngreso, Nullable<int> cin_UsuarioCrea, Nullable<System.DateTime> cin_FechaCrea)
        {
            var cin_DescripcionIngresoParameter = cin_DescripcionIngreso != null ?
                new ObjectParameter("cin_DescripcionIngreso", cin_DescripcionIngreso) :
                new ObjectParameter("cin_DescripcionIngreso", typeof(string));
    
            var cin_TipoIngresoParameter = cin_TipoIngreso.HasValue ?
                new ObjectParameter("cin_TipoIngreso", cin_TipoIngreso) :
                new ObjectParameter("cin_TipoIngreso", typeof(int));
    
            var cin_UsuarioCreaParameter = cin_UsuarioCrea.HasValue ?
                new ObjectParameter("cin_UsuarioCrea", cin_UsuarioCrea) :
                new ObjectParameter("cin_UsuarioCrea", typeof(int));
    
            var cin_FechaCreaParameter = cin_FechaCrea.HasValue ?
                new ObjectParameter("cin_FechaCrea", cin_FechaCrea) :
                new ObjectParameter("cin_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbCatalogoDeIngresos_Insert_Result>("UDP_Plani_tbCatalogoDeIngresos_Insert", cin_DescripcionIngresoParameter, cin_TipoIngresoParameter, cin_UsuarioCreaParameter, cin_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbCatalogoDeIngresos_Update_Result> UDP_Plani_tbCatalogoDeIngresos_Update(Nullable<int> cin_IdIngreso, string cin_DescripcionIngreso, Nullable<int> cin_TipoIngreso, Nullable<int> cin_UsuarioModifica, Nullable<System.DateTime> cin_FechaModifica)
        {
            var cin_IdIngresoParameter = cin_IdIngreso.HasValue ?
                new ObjectParameter("cin_IdIngreso", cin_IdIngreso) :
                new ObjectParameter("cin_IdIngreso", typeof(int));
    
            var cin_DescripcionIngresoParameter = cin_DescripcionIngreso != null ?
                new ObjectParameter("cin_DescripcionIngreso", cin_DescripcionIngreso) :
                new ObjectParameter("cin_DescripcionIngreso", typeof(string));
    
            var cin_TipoIngresoParameter = cin_TipoIngreso.HasValue ?
                new ObjectParameter("cin_TipoIngreso", cin_TipoIngreso) :
                new ObjectParameter("cin_TipoIngreso", typeof(int));
    
            var cin_UsuarioModificaParameter = cin_UsuarioModifica.HasValue ?
                new ObjectParameter("cin_UsuarioModifica", cin_UsuarioModifica) :
                new ObjectParameter("cin_UsuarioModifica", typeof(int));
    
            var cin_FechaModificaParameter = cin_FechaModifica.HasValue ?
                new ObjectParameter("cin_FechaModifica", cin_FechaModifica) :
                new ObjectParameter("cin_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbCatalogoDeIngresos_Update_Result>("UDP_Plani_tbCatalogoDeIngresos_Update", cin_IdIngresoParameter, cin_DescripcionIngresoParameter, cin_TipoIngresoParameter, cin_UsuarioModificaParameter, cin_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_tbTipoPlanillaDetalleIngreso_Update_Result> UDP_tbTipoPlanillaDetalleIngreso_Update(Nullable<int> cpla_IdPlanilla, Nullable<int> cin_IdIngreso)
        {
            var cpla_IdPlanillaParameter = cpla_IdPlanilla.HasValue ?
                new ObjectParameter("cpla_IdPlanilla", cpla_IdPlanilla) :
                new ObjectParameter("cpla_IdPlanilla", typeof(int));
    
            var cin_IdIngresoParameter = cin_IdIngreso.HasValue ?
                new ObjectParameter("cin_IdIngreso", cin_IdIngreso) :
                new ObjectParameter("cin_IdIngreso", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_tbTipoPlanillaDetalleIngreso_Update_Result>("UDP_tbTipoPlanillaDetalleIngreso_Update", cpla_IdPlanillaParameter, cin_IdIngresoParameter);
        }
    
        public virtual ObjectResult<UDP_tbTipoPlanillaDetalleIngreso_Insert_Result> UDP_tbTipoPlanillaDetalleIngreso_Insert(Nullable<int> cin_IdIngreso, Nullable<int> cpla_IdPlanilla, Nullable<int> tpdi_UsuarioCrea, Nullable<System.DateTime> tpdi_FechaCrea)
        {
            var cin_IdIngresoParameter = cin_IdIngreso.HasValue ?
                new ObjectParameter("cin_IdIngreso", cin_IdIngreso) :
                new ObjectParameter("cin_IdIngreso", typeof(int));
    
            var cpla_IdPlanillaParameter = cpla_IdPlanilla.HasValue ?
                new ObjectParameter("cpla_IdPlanilla", cpla_IdPlanilla) :
                new ObjectParameter("cpla_IdPlanilla", typeof(int));
    
            var tpdi_UsuarioCreaParameter = tpdi_UsuarioCrea.HasValue ?
                new ObjectParameter("tpdi_UsuarioCrea", tpdi_UsuarioCrea) :
                new ObjectParameter("tpdi_UsuarioCrea", typeof(int));
    
            var tpdi_FechaCreaParameter = tpdi_FechaCrea.HasValue ?
                new ObjectParameter("tpdi_FechaCrea", tpdi_FechaCrea) :
                new ObjectParameter("tpdi_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_tbTipoPlanillaDetalleIngreso_Insert_Result>("UDP_tbTipoPlanillaDetalleIngreso_Insert", cin_IdIngresoParameter, cpla_IdPlanillaParameter, tpdi_UsuarioCreaParameter, tpdi_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProductoCategoria_Update_Result> UDP_Inv_tbProductoCategoria_Update(Nullable<int> pcat_Id, string pcat_Nombre, Nullable<int> pcat_UsuarioCrea, Nullable<System.DateTime> pcat_FechaCrea, Nullable<int> pcat_UsuarioModifica, Nullable<System.DateTime> pcat_FechaModifica)
        {
            var pcat_IdParameter = pcat_Id.HasValue ?
                new ObjectParameter("pcat_Id", pcat_Id) :
                new ObjectParameter("pcat_Id", typeof(int));
    
            var pcat_NombreParameter = pcat_Nombre != null ?
                new ObjectParameter("pcat_Nombre", pcat_Nombre) :
                new ObjectParameter("pcat_Nombre", typeof(string));
    
            var pcat_UsuarioCreaParameter = pcat_UsuarioCrea.HasValue ?
                new ObjectParameter("pcat_UsuarioCrea", pcat_UsuarioCrea) :
                new ObjectParameter("pcat_UsuarioCrea", typeof(int));
    
            var pcat_FechaCreaParameter = pcat_FechaCrea.HasValue ?
                new ObjectParameter("pcat_FechaCrea", pcat_FechaCrea) :
                new ObjectParameter("pcat_FechaCrea", typeof(System.DateTime));
    
            var pcat_UsuarioModificaParameter = pcat_UsuarioModifica.HasValue ?
                new ObjectParameter("pcat_UsuarioModifica", pcat_UsuarioModifica) :
                new ObjectParameter("pcat_UsuarioModifica", typeof(int));
    
            var pcat_FechaModificaParameter = pcat_FechaModifica.HasValue ?
                new ObjectParameter("pcat_FechaModifica", pcat_FechaModifica) :
                new ObjectParameter("pcat_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProductoCategoria_Update_Result>("UDP_Inv_tbProductoCategoria_Update", pcat_IdParameter, pcat_NombreParameter, pcat_UsuarioCreaParameter, pcat_FechaCreaParameter, pcat_UsuarioModificaParameter, pcat_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProductoCategoria_Update_Estado_Validacion_Result> UDP_Inv_tbProductoCategoria_Update_Estado_Validacion(Nullable<int> pcat_Id, Nullable<int> pcat_UsuarioModifica, Nullable<System.DateTime> pcat_FechaModifica, Nullable<byte> eSTADO)
        {
            var pcat_IdParameter = pcat_Id.HasValue ?
                new ObjectParameter("pcat_Id", pcat_Id) :
                new ObjectParameter("pcat_Id", typeof(int));
    
            var pcat_UsuarioModificaParameter = pcat_UsuarioModifica.HasValue ?
                new ObjectParameter("pcat_UsuarioModifica", pcat_UsuarioModifica) :
                new ObjectParameter("pcat_UsuarioModifica", typeof(int));
    
            var pcat_FechaModificaParameter = pcat_FechaModifica.HasValue ?
                new ObjectParameter("pcat_FechaModifica", pcat_FechaModifica) :
                new ObjectParameter("pcat_FechaModifica", typeof(System.DateTime));
    
            var eSTADOParameter = eSTADO.HasValue ?
                new ObjectParameter("ESTADO", eSTADO) :
                new ObjectParameter("ESTADO", typeof(byte));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProductoCategoria_Update_Estado_Validacion_Result>("UDP_Inv_tbProductoCategoria_Update_Estado_Validacion", pcat_IdParameter, pcat_UsuarioModificaParameter, pcat_FechaModificaParameter, eSTADOParameter);
        }
    
        public virtual ObjectResult<UDP_Inv_tbProductoSubCategoria_Delete_Result> UDP_Inv_tbProductoSubCategoria_Delete(Nullable<int> pscat_Id)
        {
            var pscat_IdParameter = pscat_Id.HasValue ?
                new ObjectParameter("pscat_Id", pscat_Id) :
                new ObjectParameter("pscat_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Inv_tbProductoSubCategoria_Delete_Result>("UDP_Inv_tbProductoSubCategoria_Delete", pscat_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Acce_tbUsuario_PasswordUpdate_Result> UDP_Acce_tbUsuario_PasswordUpdate(Nullable<int> usu_Id, string usu_Password)
        {
            var usu_IdParameter = usu_Id.HasValue ?
                new ObjectParameter("usu_Id", usu_Id) :
                new ObjectParameter("usu_Id", typeof(int));
    
            var usu_PasswordParameter = usu_Password != null ?
                new ObjectParameter("usu_Password", usu_Password) :
                new ObjectParameter("usu_Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Acce_tbUsuario_PasswordUpdate_Result>("UDP_Acce_tbUsuario_PasswordUpdate", usu_IdParameter, usu_PasswordParameter);
        }
    
        public virtual ObjectResult<UDP_Acce_tbUsuario_Insert_Result> UDP_Acce_tbUsuario_Insert(string usu_NombreUsuario, string usu_Password, string usu_Nombres, string usu_Apellidos, string usu_Correo, Nullable<bool> usu_EsActivo, Nullable<bool> usu_EsAdministrador, Nullable<int> suc_Id, Nullable<int> emp_Id)
        {
            var usu_NombreUsuarioParameter = usu_NombreUsuario != null ?
                new ObjectParameter("usu_NombreUsuario", usu_NombreUsuario) :
                new ObjectParameter("usu_NombreUsuario", typeof(string));
    
            var usu_PasswordParameter = usu_Password != null ?
                new ObjectParameter("usu_Password", usu_Password) :
                new ObjectParameter("usu_Password", typeof(string));
    
            var usu_NombresParameter = usu_Nombres != null ?
                new ObjectParameter("usu_Nombres", usu_Nombres) :
                new ObjectParameter("usu_Nombres", typeof(string));
    
            var usu_ApellidosParameter = usu_Apellidos != null ?
                new ObjectParameter("usu_Apellidos", usu_Apellidos) :
                new ObjectParameter("usu_Apellidos", typeof(string));
    
            var usu_CorreoParameter = usu_Correo != null ?
                new ObjectParameter("usu_Correo", usu_Correo) :
                new ObjectParameter("usu_Correo", typeof(string));
    
            var usu_EsActivoParameter = usu_EsActivo.HasValue ?
                new ObjectParameter("usu_EsActivo", usu_EsActivo) :
                new ObjectParameter("usu_EsActivo", typeof(bool));
    
            var usu_EsAdministradorParameter = usu_EsAdministrador.HasValue ?
                new ObjectParameter("usu_EsAdministrador", usu_EsAdministrador) :
                new ObjectParameter("usu_EsAdministrador", typeof(bool));
    
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Acce_tbUsuario_Insert_Result>("UDP_Acce_tbUsuario_Insert", usu_NombreUsuarioParameter, usu_PasswordParameter, usu_NombresParameter, usu_ApellidosParameter, usu_CorreoParameter, usu_EsActivoParameter, usu_EsAdministradorParameter, suc_IdParameter, emp_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Acce_tbUsuario_Update_Result> UDP_Acce_tbUsuario_Update(Nullable<int> usu_Id, string usu_NombreUsuario, string usu_Nombres, string usu_Apellidos, string usu_Correo, Nullable<bool> usu_EsActivo, string usu_RazonInactivo, Nullable<bool> usu_EsAdministrador, Nullable<int> suc_Id, Nullable<int> emp_Id)
        {
            var usu_IdParameter = usu_Id.HasValue ?
                new ObjectParameter("usu_Id", usu_Id) :
                new ObjectParameter("usu_Id", typeof(int));
    
            var usu_NombreUsuarioParameter = usu_NombreUsuario != null ?
                new ObjectParameter("usu_NombreUsuario", usu_NombreUsuario) :
                new ObjectParameter("usu_NombreUsuario", typeof(string));
    
            var usu_NombresParameter = usu_Nombres != null ?
                new ObjectParameter("usu_Nombres", usu_Nombres) :
                new ObjectParameter("usu_Nombres", typeof(string));
    
            var usu_ApellidosParameter = usu_Apellidos != null ?
                new ObjectParameter("usu_Apellidos", usu_Apellidos) :
                new ObjectParameter("usu_Apellidos", typeof(string));
    
            var usu_CorreoParameter = usu_Correo != null ?
                new ObjectParameter("usu_Correo", usu_Correo) :
                new ObjectParameter("usu_Correo", typeof(string));
    
            var usu_EsActivoParameter = usu_EsActivo.HasValue ?
                new ObjectParameter("usu_EsActivo", usu_EsActivo) :
                new ObjectParameter("usu_EsActivo", typeof(bool));
    
            var usu_RazonInactivoParameter = usu_RazonInactivo != null ?
                new ObjectParameter("usu_RazonInactivo", usu_RazonInactivo) :
                new ObjectParameter("usu_RazonInactivo", typeof(string));
    
            var usu_EsAdministradorParameter = usu_EsAdministrador.HasValue ?
                new ObjectParameter("usu_EsAdministrador", usu_EsAdministrador) :
                new ObjectParameter("usu_EsAdministrador", typeof(bool));
    
            var suc_IdParameter = suc_Id.HasValue ?
                new ObjectParameter("suc_Id", suc_Id) :
                new ObjectParameter("suc_Id", typeof(int));
    
            var emp_IdParameter = emp_Id.HasValue ?
                new ObjectParameter("emp_Id", emp_Id) :
                new ObjectParameter("emp_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Acce_tbUsuario_Update_Result>("UDP_Acce_tbUsuario_Update", usu_IdParameter, usu_NombreUsuarioParameter, usu_NombresParameter, usu_ApellidosParameter, usu_CorreoParameter, usu_EsActivoParameter, usu_RazonInactivoParameter, usu_EsAdministradorParameter, suc_IdParameter, emp_IdParameter);
        }
    
        public virtual ObjectResult<UDP_Acce_tbRolesUsuario_Insert_Result> UDP_Acce_tbRolesUsuario_Insert(Nullable<int> usu_Id, Nullable<int> rol_Id, Nullable<int> rolu_UsuarioCrea, Nullable<System.DateTime> rolu_FechaCrea)
        {
            var usu_IdParameter = usu_Id.HasValue ?
                new ObjectParameter("usu_Id", usu_Id) :
                new ObjectParameter("usu_Id", typeof(int));
    
            var rol_IdParameter = rol_Id.HasValue ?
                new ObjectParameter("rol_Id", rol_Id) :
                new ObjectParameter("rol_Id", typeof(int));
    
            var rolu_UsuarioCreaParameter = rolu_UsuarioCrea.HasValue ?
                new ObjectParameter("rolu_UsuarioCrea", rolu_UsuarioCrea) :
                new ObjectParameter("rolu_UsuarioCrea", typeof(int));
    
            var rolu_FechaCreaParameter = rolu_FechaCrea.HasValue ?
                new ObjectParameter("rolu_FechaCrea", rolu_FechaCrea) :
                new ObjectParameter("rolu_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Acce_tbRolesUsuario_Insert_Result>("UDP_Acce_tbRolesUsuario_Insert", usu_IdParameter, rol_IdParameter, rolu_UsuarioCreaParameter, rolu_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Acce_tbUsuario_PasswordRestore_Result> UDP_Acce_tbUsuario_PasswordRestore(Nullable<int> usu_Id, string usu_Password)
        {
            var usu_IdParameter = usu_Id.HasValue ?
                new ObjectParameter("usu_Id", usu_Id) :
                new ObjectParameter("usu_Id", typeof(int));
    
            var usu_PasswordParameter = usu_Password != null ?
                new ObjectParameter("usu_Password", usu_Password) :
                new ObjectParameter("usu_Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Acce_tbUsuario_PasswordRestore_Result>("UDP_Acce_tbUsuario_PasswordRestore", usu_IdParameter, usu_PasswordParameter);
        }
    
        public virtual ObjectResult<UDP_Acce_tbObjeto_Insert_Result> UDP_Acce_tbObjeto_Insert(string obj_Pantalla, string obj_Referencia, Nullable<int> obj_UsuarioCrea, Nullable<System.DateTime> obj_FechaCrea)
        {
            var obj_PantallaParameter = obj_Pantalla != null ?
                new ObjectParameter("obj_Pantalla", obj_Pantalla) :
                new ObjectParameter("obj_Pantalla", typeof(string));
    
            var obj_ReferenciaParameter = obj_Referencia != null ?
                new ObjectParameter("obj_Referencia", obj_Referencia) :
                new ObjectParameter("obj_Referencia", typeof(string));
    
            var obj_UsuarioCreaParameter = obj_UsuarioCrea.HasValue ?
                new ObjectParameter("obj_UsuarioCrea", obj_UsuarioCrea) :
                new ObjectParameter("obj_UsuarioCrea", typeof(int));
    
            var obj_FechaCreaParameter = obj_FechaCrea.HasValue ?
                new ObjectParameter("obj_FechaCrea", obj_FechaCrea) :
                new ObjectParameter("obj_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Acce_tbObjeto_Insert_Result>("UDP_Acce_tbObjeto_Insert", obj_PantallaParameter, obj_ReferenciaParameter, obj_UsuarioCreaParameter, obj_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Acce_tbObjeto_Update_Result> UDP_Acce_tbObjeto_Update(Nullable<int> obj_Id, string obj_Pantalla, string obj_Referencia, Nullable<int> obj_UsuarioCrea, Nullable<System.DateTime> obj_FechaCrea, Nullable<int> obj_UsuarioModifica, Nullable<System.DateTime> obj_FechaModifica)
        {
            var obj_IdParameter = obj_Id.HasValue ?
                new ObjectParameter("obj_Id", obj_Id) :
                new ObjectParameter("obj_Id", typeof(int));
    
            var obj_PantallaParameter = obj_Pantalla != null ?
                new ObjectParameter("obj_Pantalla", obj_Pantalla) :
                new ObjectParameter("obj_Pantalla", typeof(string));
    
            var obj_ReferenciaParameter = obj_Referencia != null ?
                new ObjectParameter("obj_Referencia", obj_Referencia) :
                new ObjectParameter("obj_Referencia", typeof(string));
    
            var obj_UsuarioCreaParameter = obj_UsuarioCrea.HasValue ?
                new ObjectParameter("obj_UsuarioCrea", obj_UsuarioCrea) :
                new ObjectParameter("obj_UsuarioCrea", typeof(int));
    
            var obj_FechaCreaParameter = obj_FechaCrea.HasValue ?
                new ObjectParameter("obj_FechaCrea", obj_FechaCrea) :
                new ObjectParameter("obj_FechaCrea", typeof(System.DateTime));
    
            var obj_UsuarioModificaParameter = obj_UsuarioModifica.HasValue ?
                new ObjectParameter("obj_UsuarioModifica", obj_UsuarioModifica) :
                new ObjectParameter("obj_UsuarioModifica", typeof(int));
    
            var obj_FechaModificaParameter = obj_FechaModifica.HasValue ?
                new ObjectParameter("obj_FechaModifica", obj_FechaModifica) :
                new ObjectParameter("obj_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Acce_tbObjeto_Update_Result>("UDP_Acce_tbObjeto_Update", obj_IdParameter, obj_PantallaParameter, obj_ReferenciaParameter, obj_UsuarioCreaParameter, obj_FechaCreaParameter, obj_UsuarioModificaParameter, obj_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Acce_tbObjeto_Update_Estado_Result> UDP_Acce_tbObjeto_Update_Estado(Nullable<int> obj_Id, Nullable<bool> obj_Estado, Nullable<int> obj_UsuarioModifica, Nullable<System.DateTime> obj_FechaModifica)
        {
            var obj_IdParameter = obj_Id.HasValue ?
                new ObjectParameter("obj_Id", obj_Id) :
                new ObjectParameter("obj_Id", typeof(int));
    
            var obj_EstadoParameter = obj_Estado.HasValue ?
                new ObjectParameter("obj_Estado", obj_Estado) :
                new ObjectParameter("obj_Estado", typeof(bool));
    
            var obj_UsuarioModificaParameter = obj_UsuarioModifica.HasValue ?
                new ObjectParameter("obj_UsuarioModifica", obj_UsuarioModifica) :
                new ObjectParameter("obj_UsuarioModifica", typeof(int));
    
            var obj_FechaModificaParameter = obj_FechaModifica.HasValue ?
                new ObjectParameter("obj_FechaModifica", obj_FechaModifica) :
                new ObjectParameter("obj_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Acce_tbObjeto_Update_Estado_Result>("UDP_Acce_tbObjeto_Update_Estado", obj_IdParameter, obj_EstadoParameter, obj_UsuarioModificaParameter, obj_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Acce_tbRolEstado_Update_Result> UDP_Acce_tbRolEstado_Update(Nullable<int> rol_Id, Nullable<bool> rol_Estado, Nullable<int> rol_UsuarioModifica, Nullable<System.DateTime> rol_FechaModifica)
        {
            var rol_IdParameter = rol_Id.HasValue ?
                new ObjectParameter("rol_Id", rol_Id) :
                new ObjectParameter("rol_Id", typeof(int));
    
            var rol_EstadoParameter = rol_Estado.HasValue ?
                new ObjectParameter("rol_Estado", rol_Estado) :
                new ObjectParameter("rol_Estado", typeof(bool));
    
            var rol_UsuarioModificaParameter = rol_UsuarioModifica.HasValue ?
                new ObjectParameter("rol_UsuarioModifica", rol_UsuarioModifica) :
                new ObjectParameter("rol_UsuarioModifica", typeof(int));
    
            var rol_FechaModificaParameter = rol_FechaModifica.HasValue ?
                new ObjectParameter("rol_FechaModifica", rol_FechaModifica) :
                new ObjectParameter("rol_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Acce_tbRolEstado_Update_Result>("UDP_Acce_tbRolEstado_Update", rol_IdParameter, rol_EstadoParameter, rol_UsuarioModificaParameter, rol_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Acce_tbRol_Inactivar_Result> UDP_Acce_tbRol_Inactivar(Nullable<int> iDRol, Nullable<int> rol_UsuarioModifica, Nullable<System.DateTime> rol_FechaModifica)
        {
            var iDRolParameter = iDRol.HasValue ?
                new ObjectParameter("IDRol", iDRol) :
                new ObjectParameter("IDRol", typeof(int));
    
            var rol_UsuarioModificaParameter = rol_UsuarioModifica.HasValue ?
                new ObjectParameter("rol_UsuarioModifica", rol_UsuarioModifica) :
                new ObjectParameter("rol_UsuarioModifica", typeof(int));
    
            var rol_FechaModificaParameter = rol_FechaModifica.HasValue ?
                new ObjectParameter("rol_FechaModifica", rol_FechaModifica) :
                new ObjectParameter("rol_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Acce_tbRol_Inactivar_Result>("UDP_Acce_tbRol_Inactivar", iDRolParameter, rol_UsuarioModificaParameter, rol_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Acce_tbAccesoRol_Insert_Result> UDP_Acce_tbAccesoRol_Insert(Nullable<int> rol_Id, Nullable<int> obj_Id, Nullable<int> usuarioCrea, Nullable<System.DateTime> fechaCrea)
        {
            var rol_IdParameter = rol_Id.HasValue ?
                new ObjectParameter("rol_Id", rol_Id) :
                new ObjectParameter("rol_Id", typeof(int));
    
            var obj_IdParameter = obj_Id.HasValue ?
                new ObjectParameter("obj_Id", obj_Id) :
                new ObjectParameter("obj_Id", typeof(int));
    
            var usuarioCreaParameter = usuarioCrea.HasValue ?
                new ObjectParameter("UsuarioCrea", usuarioCrea) :
                new ObjectParameter("UsuarioCrea", typeof(int));
    
            var fechaCreaParameter = fechaCrea.HasValue ?
                new ObjectParameter("FechaCrea", fechaCrea) :
                new ObjectParameter("FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Acce_tbAccesoRol_Insert_Result>("UDP_Acce_tbAccesoRol_Insert", rol_IdParameter, obj_IdParameter, usuarioCreaParameter, fechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Acce_tbRol_Insert_Result> UDP_Acce_tbRol_Insert(string rol_Descripcion, Nullable<bool> rol_Estado, Nullable<int> rol_UsuarioCrea, Nullable<System.DateTime> rol_FechaCrea)
        {
            var rol_DescripcionParameter = rol_Descripcion != null ?
                new ObjectParameter("rol_Descripcion", rol_Descripcion) :
                new ObjectParameter("rol_Descripcion", typeof(string));
    
            var rol_EstadoParameter = rol_Estado.HasValue ?
                new ObjectParameter("rol_Estado", rol_Estado) :
                new ObjectParameter("rol_Estado", typeof(bool));
    
            var rol_UsuarioCreaParameter = rol_UsuarioCrea.HasValue ?
                new ObjectParameter("rol_UsuarioCrea", rol_UsuarioCrea) :
                new ObjectParameter("rol_UsuarioCrea", typeof(int));
    
            var rol_FechaCreaParameter = rol_FechaCrea.HasValue ?
                new ObjectParameter("rol_FechaCrea", rol_FechaCrea) :
                new ObjectParameter("rol_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Acce_tbRol_Insert_Result>("UDP_Acce_tbRol_Insert", rol_DescripcionParameter, rol_EstadoParameter, rol_UsuarioCreaParameter, rol_FechaCreaParameter);
        }
    
        public virtual int UDP_Acce_tbAccesoRol_Update(Nullable<int> acrol_Id, Nullable<int> rol_Id, Nullable<int> obj_Id, Nullable<int> acrol_UsuarioCrea, Nullable<System.DateTime> acrol_FechaCrea)
        {
            var acrol_IdParameter = acrol_Id.HasValue ?
                new ObjectParameter("acrol_Id", acrol_Id) :
                new ObjectParameter("acrol_Id", typeof(int));
    
            var rol_IdParameter = rol_Id.HasValue ?
                new ObjectParameter("rol_Id", rol_Id) :
                new ObjectParameter("rol_Id", typeof(int));
    
            var obj_IdParameter = obj_Id.HasValue ?
                new ObjectParameter("obj_Id", obj_Id) :
                new ObjectParameter("obj_Id", typeof(int));
    
            var acrol_UsuarioCreaParameter = acrol_UsuarioCrea.HasValue ?
                new ObjectParameter("acrol_UsuarioCrea", acrol_UsuarioCrea) :
                new ObjectParameter("acrol_UsuarioCrea", typeof(int));
    
            var acrol_FechaCreaParameter = acrol_FechaCrea.HasValue ?
                new ObjectParameter("acrol_FechaCrea", acrol_FechaCrea) :
                new ObjectParameter("acrol_FechaCrea", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UDP_Acce_tbAccesoRol_Update", acrol_IdParameter, rol_IdParameter, obj_IdParameter, acrol_UsuarioCreaParameter, acrol_FechaCreaParameter);
        }
    
        public virtual ObjectResult<UDP_Acce_tbRol_Update_Result> UDP_Acce_tbRol_Update(Nullable<int> rol_Id, string rol_Descripcion, Nullable<int> rol_UsuarioModifica, Nullable<System.DateTime> rol_FechaModifica)
        {
            var rol_IdParameter = rol_Id.HasValue ?
                new ObjectParameter("rol_Id", rol_Id) :
                new ObjectParameter("rol_Id", typeof(int));
    
            var rol_DescripcionParameter = rol_Descripcion != null ?
                new ObjectParameter("rol_Descripcion", rol_Descripcion) :
                new ObjectParameter("rol_Descripcion", typeof(string));
    
            var rol_UsuarioModificaParameter = rol_UsuarioModifica.HasValue ?
                new ObjectParameter("rol_UsuarioModifica", rol_UsuarioModifica) :
                new ObjectParameter("rol_UsuarioModifica", typeof(int));
    
            var rol_FechaModificaParameter = rol_FechaModifica.HasValue ?
                new ObjectParameter("rol_FechaModifica", rol_FechaModifica) :
                new ObjectParameter("rol_FechaModifica", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Acce_tbRol_Update_Result>("UDP_Acce_tbRol_Update", rol_IdParameter, rol_DescripcionParameter, rol_UsuarioModificaParameter, rol_FechaModificaParameter);
        }
    
        public virtual ObjectResult<UDP_Plani_tbInstitucionesFinancieras_Update_Result> UDP_Plani_tbInstitucionesFinancieras_Update(Nullable<int> insf_IdInstitucionFinanciera, string insf_DescInstitucionFinanc, string insf_Contacto, string insf_Telefono, string insf_Correo, Nullable<int> insf_UsuarioModifica, Nullable<System.DateTime> insf_FechaModifica, Nullable<bool> insf_Activo)
        {
            var insf_IdInstitucionFinancieraParameter = insf_IdInstitucionFinanciera.HasValue ?
                new ObjectParameter("insf_IdInstitucionFinanciera", insf_IdInstitucionFinanciera) :
                new ObjectParameter("insf_IdInstitucionFinanciera", typeof(int));
    
            var insf_DescInstitucionFinancParameter = insf_DescInstitucionFinanc != null ?
                new ObjectParameter("insf_DescInstitucionFinanc", insf_DescInstitucionFinanc) :
                new ObjectParameter("insf_DescInstitucionFinanc", typeof(string));
    
            var insf_ContactoParameter = insf_Contacto != null ?
                new ObjectParameter("insf_Contacto", insf_Contacto) :
                new ObjectParameter("insf_Contacto", typeof(string));
    
            var insf_TelefonoParameter = insf_Telefono != null ?
                new ObjectParameter("insf_Telefono", insf_Telefono) :
                new ObjectParameter("insf_Telefono", typeof(string));
    
            var insf_CorreoParameter = insf_Correo != null ?
                new ObjectParameter("insf_Correo", insf_Correo) :
                new ObjectParameter("insf_Correo", typeof(string));
    
            var insf_UsuarioModificaParameter = insf_UsuarioModifica.HasValue ?
                new ObjectParameter("insf_UsuarioModifica", insf_UsuarioModifica) :
                new ObjectParameter("insf_UsuarioModifica", typeof(int));
    
            var insf_FechaModificaParameter = insf_FechaModifica.HasValue ?
                new ObjectParameter("insf_FechaModifica", insf_FechaModifica) :
                new ObjectParameter("insf_FechaModifica", typeof(System.DateTime));
    
            var insf_ActivoParameter = insf_Activo.HasValue ?
                new ObjectParameter("insf_Activo", insf_Activo) :
                new ObjectParameter("insf_Activo", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UDP_Plani_tbInstitucionesFinancieras_Update_Result>("UDP_Plani_tbInstitucionesFinancieras_Update", insf_IdInstitucionFinancieraParameter, insf_DescInstitucionFinancParameter, insf_ContactoParameter, insf_TelefonoParameter, insf_CorreoParameter, insf_UsuarioModificaParameter, insf_FechaModificaParameter, insf_ActivoParameter);
        }
    }
}
