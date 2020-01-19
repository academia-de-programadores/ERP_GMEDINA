using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using ERP_GMEDINA.DataSets;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data;

namespace ERP_GMEDINA.Controllers
{
    public class ReportesRRHHController : Controller
    {
        // GET: ReportesRRHH
        ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        ReportesRRHH ds = new ReportesRRHH();


        public ActionResult HorasTrabajadas()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            //ViewBag.Turno2 = new SelectList(db.tbHistorialHorasTrabajadas.Where(o => o.htra_Estado == true), "htra_Id");

            ViewBag.Turno = new SelectList(db.tbTipoHoras.Where(o => o.tiho_Estado == true), "tiho_Id", "tiho_Descripcion");
            return View();
        }
        [HttpPost]
        public ActionResult HorasTrabajadas( int tiho_Id,  DateTime Fecha)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = false;
            reportViewer.Width = Unit.Pixel(1050);
            reportViewer.Height = Unit.Pixel(500);
            reportViewer.BackColor = System.Drawing.Color.White;
            var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;


            //comando para el dataAdapter
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * from rrhh.V_RPT_HorasTrabajadas where tiho_Id = @tiho_Id and Fecha = @Fecha";
            command.Parameters.AddWithValue("@tiho_Id", SqlDbType.Int).Value = tiho_Id;
            //command.Parameters.AddWithValue("@tiho_Descripcion", SqlDbType.NVarChar).Value = tiho_Descripcion;
            command.Parameters.AddWithValue("@Fecha", SqlDbType.DateTime).Value = Fecha;

            SqlConnection conx = new SqlConnection(connectionString);
            command.Connection = conx;
            SqlDataAdapter adp = new SqlDataAdapter(command);
            adp.Fill(ds, ds.V_RPT_HorasTrabajadas.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\HorasTrabajadas.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_HorasTrabajadas"]));
            conx.Close();

            ViewBag.ReportViewer = reportViewer;
            //ViewBag.TipoHora = db.tbHistorialHorasTrabajadas.Where(x => x.htra_Id == htra_Id);

            ViewBag.Titulos = db.tbTipoHoras.Where(x => x.tiho_Id == tiho_Id).Select(x => x.tiho_Descripcion).FirstOrDefault();
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            ViewBag.Turno = new SelectList(db.tbTipoHoras.Where(o => o.tiho_Estado == true), "tiho_Id", "tiho_Descripcion");
            //ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
            return View();
        }


        public ActionResult HistorialContratacionesRPT()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            ViewBag.Cargo = new SelectList(db.tbCargos.Where(o => o.car_Estado == true), "car_Id", "car_Descripcion");
            return View();
        }

        public ActionResult HistorialCargosRPT()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            ViewBag.Cargo = new SelectList(db.tbCargos.Where(o => o.car_Estado == true), "car_Id", "car_Descripcion");
            return View();
        }

        //parametros del reporte
        [HttpPost]
        public ActionResult HistorialContratacionesRPT(int car_Id, DateTime FechaContratacion, DateTime fechaFin)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = false;
            reportViewer.Width = Unit.Pixel(1050);
            reportViewer.Height = Unit.Pixel(500);
            reportViewer.BackColor = System.Drawing.Color.White;
            var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            //comando para el dataAdapter
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM rrhh.V_RPT_HistorialContrataciones where car_Id = @car_Id and FechaContratacion between @FechaContratacion and @fechaFin";
            command.Parameters.AddWithValue("@car_Id", SqlDbType.Int).Value = car_Id;
            command.Parameters.AddWithValue("@FechaContratacion", SqlDbType.Date).Value = FechaContratacion;
            command.Parameters.AddWithValue("@fechaFin", SqlDbType.Date).Value = fechaFin;
            command.Connection = conx;
            SqlDataAdapter adp = new SqlDataAdapter(command);
            adp.Fill(ds, ds.V_RPT_HistorialContrataciones.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\HistorialContratacionesRPT.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_HistorialContrataciones"]));
            conx.Close();

            ViewBag.ReportViewer = reportViewer;
            ViewBag.Cargo = new SelectList(db.tbCargos.Where(o => o.car_Estado == true), "car_Id", "car_Descripcion");

            //Cargar DDL del modal (DDL para Cargo)
            ViewBag.Cargo = new SelectList(db.tbCargos.Where(o => o.car_Estado == true), "car_Id", "car_Descripcion");
            return View();
        }

        public ActionResult HistorialSalidasRPT()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            //ViewBag.Turno2 = new SelectList(db.tbHistorialHorasTrabajadas.Where(o => o.htra_Estado == true), "htra_Id");

            ViewBag.TipoSalida = new SelectList(db.tbTipoSalidas.Where(o => o.tsal_Estado == true), "tsal_Id", "tsal_Descripcion");
            ViewBag.Empleados = new SelectList(db.V_EmpleadoIncapacidades.Where(o => o.emp_Estado == true), "emp_Id", "emp_NombreCompleto");
            return View();
        }
        [HttpPost]
        public ActionResult HistorialSalidasRPT(int tsal_Id, DateTime FechaSalida, DateTime fechaFin)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = false;
            reportViewer.Width = Unit.Pixel(1050);
            reportViewer.Height = Unit.Pixel(500);
            reportViewer.BackColor = System.Drawing.Color.White;
            var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;


            //comando para el dataAdapter
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * from rrhh.V_RPT_HistorialSalidas where tsal_Id = @tsal_Id and FechaSalida between @FechaSalida and @fechaFin";
            command.Parameters.AddWithValue("@tsal_Id", SqlDbType.Int).Value = tsal_Id;
            //command.Parameters.AddWithValue("@tiho_Descripcion", SqlDbType.NVarChar).Value = tiho_Descripcion;
            command.Parameters.AddWithValue("@FechaSalida", SqlDbType.DateTime).Value = FechaSalida;
            command.Parameters.AddWithValue("@fechaFin", SqlDbType.DateTime).Value = fechaFin;

            SqlConnection conx = new SqlConnection(connectionString);
            command.Connection = conx;
            SqlDataAdapter adp = new SqlDataAdapter(command);
            adp.Fill(ds, ds.V_RPT_HistorialSalidas.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\HistorialSalidasRPT.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_HistorialSalidas"]));
            conx.Close();


            ViewBag.ReportViewer = reportViewer;
            ViewBag.TipoSalida = new SelectList(db.tbTipoSalidas.Where(o => o.tsal_Estado == true), "tsal_Id", "tsal_Descripcion");

            ViewBag.TipoSalida = new SelectList(db.tbTipoSalidas.Where(o => o.tsal_Estado == true), "tsal_Id", "tsal_Descripcion");
            ////ViewBag.Titulos = db.tbHistorialSalidas.Where(x => x.hsal_Id == hsal_Id).Select(x => x.).FirstOrDefault();
            //////Cargar DDL del modal (Tipo de planilla a seleccionar)
            //ViewBag.Tiposalida = new SelectList(db.tbTipoSalidas.Where(o => o.tsal_Estado == true), "tsal_Id", "tsal_Descripcion");

            //ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
            return View();
        }

        public ActionResult HistorialVacacionesRPT()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            //ViewBag.Turno2 = new SelectList(db.tbHistorialHorasTrabajadas.Where(o => o.htra_Estado == true), "htra_Id");


            ViewBag.Empleados = new SelectList(db.V_Empleados.Where(o => o.emp_Estado == true), "emp_Id", "Per_NombreCompleto");
            ViewBag.Anios = new SelectList(db.tbHistorialVacaciones.Where(o => o.hvac_Estado == true), "hvac_AnioVacaciones", "hvac_AnioVacaciones");
            ViewBag.Mes = new SelectList(db.tbHistorialVacaciones.Where(o => o.hvac_Estado == true), "hvac_MesVacaciones", "hvac_MesVacaciones");

            return View();
        }
        [HttpPost]
        public ActionResult HistorialVacacionesRPT(int emp_Id, DateTime hvac_FechaInicio, DateTime hvac_FechaFin /*,int hvac_AnioVacaciones, int hvac_MesVacaciones*/)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = false;
            reportViewer.Width = Unit.Pixel(1050);
            reportViewer.Height = Unit.Pixel(500);
            reportViewer.BackColor = System.Drawing.Color.White;
            var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;


            //comando para el dataAdapter
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT * from rrhh.V_RPT_HistorialVacaciones where emp_Id = @emp_Id and hvac_FechaInicio between @hvac_FechaInicio and @hvac_FechaFin";
            command.Parameters.AddWithValue("@emp_Id", SqlDbType.Int).Value = emp_Id;
            //command.Parameters.AddWithValue("@tiho_Descripcion", SqlDbType.NVarChar).Value = tiho_Descripcion;
            command.Parameters.AddWithValue("@hvac_FechaInicio", SqlDbType.DateTime).Value = hvac_FechaInicio;
            command.Parameters.AddWithValue("@hvac_FechaFin", SqlDbType.DateTime).Value = hvac_FechaFin;
            //command.Parameters.AddWithValue("@hvac_AnioVacaciones", SqlDbType.DateTime).Value = hvac_AnioVacaciones;
            //command.Parameters.AddWithValue("@hvac_MesVacaciones", SqlDbType.DateTime).Value = hvac_MesVacaciones;

            SqlConnection conx = new SqlConnection(connectionString);
            command.Connection = conx;
            SqlDataAdapter adp = new SqlDataAdapter(command);
            adp.Fill(ds, ds.V_RPT_HistorialVacaciones.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\HistorialVacacionesRPT.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_HistorialVacaciones"]));
            conx.Close();


            ViewBag.ReportViewer = reportViewer;
            ViewBag.Empleados = new SelectList(db.V_Empleados.Where(o => o.emp_Estado == true), "emp_Id", "Per_NombreCompleto");

            ViewBag.Anios = new SelectList(db.tbHistorialVacaciones.Where(o => o.hvac_Estado == true), "hvac_AnioVacaciones", "hvac_AnioVacaciones");

            ViewBag.Mes = new SelectList(db.tbHistorialVacaciones.Where(o => o.hvac_Estado == true), "hvac_MesVacaciones", "hvac_MesVacaciones");
            ////ViewBag.Titulos = db.tbHistorialSalidas.Where(x => x.hsal_Id == hsal_Id).Select(x => x.).FirstOrDefault();
            //////Cargar DDL del modal (Tipo de planilla a seleccionar)
            //ViewBag.Tiposalida = new SelectList(db.tbTipoSalidas.Where(o => o.tsal_Estado == true), "tsal_Id", "tsal_Descripcion");

            //ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
            return View();
        }


        [HttpPost]
        public ActionResult HistorialCargosRPT(int car_Id, DateTime Fecha, DateTime FechaFin)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = false;
            reportViewer.Width = Unit.Pixel(1050);
            reportViewer.Height = Unit.Pixel(500);
            reportViewer.BackColor = System.Drawing.Color.White;
            var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            //comando para el dataAdapter
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM rrhh.V_RPT_HistorialCargos where car_Id = @car_Id and Fecha between @Fecha and @FechaFin";
            command.Parameters.AddWithValue("@car_Id", SqlDbType.Int).Value = car_Id;
            command.Parameters.AddWithValue("@Fecha", SqlDbType.Date).Value = Fecha;
            command.Parameters.AddWithValue("@FechaFin", SqlDbType.Date).Value = FechaFin;
            command.Connection = conx;
            SqlDataAdapter adp = new SqlDataAdapter(command);
            adp.Fill(ds, ds.V_RPT_HistorialCargos.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\HistorialCargosRPT.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_HistorialCargos"]));
            conx.Close();

            ViewBag.ReportViewer = reportViewer;
            ViewBag.Cargo = new SelectList(db.tbCargos.Where(o => o.car_Estado == true), "car_Id", "car_Descripcion");

            //Cargar DDL del modal (DDL para Cargo)
            ViewBag.Cargo = new SelectList(db.tbCargos.Where(o => o.car_Estado == true), "car_Id", "car_Descripcion");
            return View();
        }

        public ActionResult SueldosRPT()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            //ViewBag.Turno2 = new SelectList(db.tbHistorialHorasTrabajadas.Where(o => o.htra_Estado == true), "htra_Id");

            ViewBag.Sueldos = new SelectList(db.tbSueldos.Where(o => o.sue_Estado == true), "sue_Id", "sue_Cantidad");
            ViewBag.Empleados = new SelectList(db.V_Empleados.Where(o => o.emp_Estado == true), "emp_Id", "per_NombreCompleto");

            return View();
        }
        [HttpPost]
        public ActionResult SueldosRPT(int sue_Id, int emp_Id)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = false;
            reportViewer.Width = Unit.Pixel(1050);
            reportViewer.Height = Unit.Pixel(500);
            reportViewer.BackColor = System.Drawing.Color.White;
            var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;


            //comando para el dataAdapter
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * from rrhh.V_RPT_HistorialSueldos where sue_Id = @sue_Id and emp_Id = @emp_Id";
            command.Parameters.AddWithValue("@sue_Id", SqlDbType.Int).Value = sue_Id;
            command.Parameters.AddWithValue("@emp_Id", SqlDbType.Int).Value = emp_Id;
            //command.Parameters.AddWithValue("@tiho_Descripcion", SqlDbType.NVarChar).Value = tiho_Descripcion;
            //command.Parameters.AddWithValue("@tmon_Descripcion", SqlDbType.NVarChar).Value = tmon_Descripcion;




            SqlConnection conx = new SqlConnection(connectionString);
            command.Connection = conx;
            SqlDataAdapter adp = new SqlDataAdapter(command);
            adp.Fill(ds, ds.V_RPT_HistorialSueldos.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\SueldosRPT.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_HistorialSueldos"]));
            conx.Close();

            ViewBag.ReportViewer = reportViewer;
            ViewBag.Sueldos = new SelectList(db.tbSueldos.Where(o => o.sue_Estado == true), "sue_Id", "sue_Cantidad");
            ViewBag.Empleados = new SelectList(db.V_Empleados.Where(o => o.emp_Estado == true), "emp_Id", "per_NombreCompleto");

            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            ViewBag.Sueldos = new SelectList(db.tbSueldos.Where(o => o.sue_Estado == true), "sue_Id", "sue_Cantidad");
            ViewBag.Empleados = new SelectList(db.V_Empleados.Where(o => o.emp_Estado == true), "emp_Id", "per_NombreCompleto");
            //ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
            return View();
        }

        public ActionResult Permisos()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            //ViewBag.Turno2 = new SelectList(db.tbHistorialHorasTrabajadas.Where(o => o.htra_Estado == true), "htra_Id");
            ViewBag.Permiso = new SelectList(db.tbTipoPermisos.Where(o => o.tper_Estado == true), "tper_Id", "tper_Descripcion");
            ViewBag.Empleados = new SelectList(db.V_Empleados.Where(o => o.emp_Estado == true), "emp_Id", "per_NombreCompleto");
            return View();
        }

        [HttpPost]
        public ActionResult Permisos(int tper_Id, int emp_Id, DateTime FechaInicio, DateTime FechaFin)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = false;
            reportViewer.Width = Unit.Pixel(1050);
            reportViewer.Height = Unit.Pixel(500);
            reportViewer.BackColor = System.Drawing.Color.White;
            var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;


            //comando para el dataAdapter
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * from rrhh.V_RPT_HistorialPermisos where tper_Id = @tper_Id and emp_Id = @emp_Id and FechaInicio between @FechaInicio and @FechaFin  and FechaFin between @FechaInicio and @FechaFin  ";
            command.Parameters.AddWithValue("@tper_Id", SqlDbType.Int).Value = tper_Id;
            command.Parameters.AddWithValue("@emp_Id", SqlDbType.Int).Value = emp_Id;
            command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = FechaInicio;
            command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = FechaFin;

            SqlConnection conx = new SqlConnection(connectionString);
            command.Connection = conx;
            SqlDataAdapter adp = new SqlDataAdapter(command);
            adp.Fill(ds, ds.V_RPT_HistorialPermisos.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\PermisosRPT.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesPermisosDS", ds.Tables["V_RPT_HistorialPermisos"]));
            conx.Close();

            ViewBag.ReportViewer = reportViewer;
            //ViewBag.TipoHora = db.tbHistorialHorasTrabajadas.Where(x => x.htra_Id == htra_Id);

            //ViewBag.Titulos = db.tbTipoHoras.Where(x => x.tiho_Id == tiho_Id).Select(x => x.tiho_Descripcion).FirstOrDefault();
            ////Cargar DDL del modal (Tipo de planilla a seleccionar)
            //ViewBag.Turno = new SelectList(db.tbTipoHoras.Where(o => o.tiho_Estado == true), "tiho_Id", "tiho_Descripcion");
            //ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
            ViewBag.Permiso = new SelectList(db.tbTipoPermisos.Where(o => o.tper_Estado == true), "tper_Id", "tper_Descripcion");
            ViewBag.Empleados = new SelectList(db.V_Empleados.Where(o => o.emp_Estado == true), "emp_Id", "per_NombreCompleto");
            return View();
        }

        public ActionResult HistorialIncapacidades()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            //ViewBag.Turno2 = new SelectList(db.tbHistorialHorasTrabajadas.Where(o => o.htra_Estado == true), "htra_Id");

            ViewBag.incapacidades = new SelectList(db.tbTipoIncapacidades.Where(o => o.ticn_Estado == true), "ticn_Id", "ticn_Descripcion");
            return View();
        }
        [HttpPost]
        public ActionResult HistorialIncapacidades(int ticn_Id, DateTime FechaInicio,DateTime FechaFin)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = false;
            reportViewer.Width = Unit.Pixel(1050);
            reportViewer.Height = Unit.Pixel(500);
            reportViewer.BackColor = System.Drawing.Color.White;
            var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;


            //comando para el dataAdapter
            SqlCommand command = new SqlCommand();
            command.CommandText = " select * from rrhh.V_RPT_HistorialIncapacidad where ticn_Id = @ticn_Id and FechaInicio  between  @FechaInicio and @FechaFin ";
            command.Parameters.AddWithValue("@ticn_Id", SqlDbType.Int).Value = ticn_Id;
            command.Parameters.AddWithValue("@FechaInicio", SqlDbType.Date).Value = FechaInicio;
            command.Parameters.AddWithValue("@FechaFin", SqlDbType.Date).Value = FechaFin;


            SqlConnection conx = new SqlConnection(connectionString);
            command.Connection = conx;
            SqlDataAdapter adp = new SqlDataAdapter(command);
            adp.Fill(ds, ds.V_RPT_HorasTrabajadas.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\HistorialIncapacidades.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_HistorialIncapacidad"]));
            conx.Close();

            ViewBag.ReportViewer = reportViewer;
            //ViewBag.TipoHora = db.tbHistorialHorasTrabajadas.Where(x => x.htra_Id == htra_Id);

            ViewBag.Titulos = db.tbTipoIncapacidades.Where(x => x.ticn_Id == ticn_Id).Select(x => x.ticn_Descripcion).FirstOrDefault();
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            ViewBag.incapacidades = new SelectList(db.tbTipoIncapacidades.Where(o => o.ticn_Estado == true), "ticn_Id", "ticn_Descripcion");
            //ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
            return View();
        }
        public ActionResult FaseSeleccion()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            ViewBag.FaseReclutamiento = new SelectList(db.tbFasesReclutamiento.Where(o => o.fare_Estado == true), "fare_Id", "fare_Descripcion");
            // ViewBag.Requisiciones = new SelectList(db.tbRequisiciones.Where(o => o.req_Estado == true), "req_Id", "req_Descripcion");
            return View();
        }
        //parametros del reporte
        [HttpPost]
        public ActionResult FaseSeleccion(int fare_Id, DateTime Fecha)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = false;
            reportViewer.Width = Unit.Pixel(1050);
            reportViewer.Height = Unit.Pixel(500);
            reportViewer.BackColor = System.Drawing.Color.White;
            var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;


            //comando para el dataAdapter
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM rrhh.V_RPT_FaseSeleccion where fare_Id = @fare_Id  and Fecha = @Fecha";
            command.Parameters.AddWithValue("@fare_Id", SqlDbType.Int).Value = fare_Id;
            command.Parameters.AddWithValue("@Fecha", SqlDbType.Date).Value = Fecha;
            SqlConnection conx = new SqlConnection(connectionString);
            command.Connection = conx;
            SqlDataAdapter adp = new SqlDataAdapter(command);
            adp.Fill(ds, ds.V_RPT_FaseSeleccion.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\FaseSeleccion.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_FaseSeleccion"]));
            conx.Close();

            ViewBag.ReportViewer = reportViewer;
            ViewBag.Titulo = db.tbFasesReclutamiento.Where(x => x.fare_Id == fare_Id).Select(x => x.fare_Descripcion).FirstOrDefault();
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            ViewBag.FaseReclutamiento = new SelectList(db.tbFasesReclutamiento.Where(o => o.fare_Estado == true), "fare_Id", "fare_Descripcion");
            // ViewBag.Requisiciones = new SelectList(db.tbRequisiciones.Where(o => o.req_Estado == true), "req_Id", "req_Descripcion");
            return View();
        }
        public ActionResult Requisicion()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            ViewBag.Requisicion = new SelectList(db.tbRequisiciones.Where(o => o.req_Estado == true), "req_Id", "req_Descripcion");
            //ViewBag.Persona = new SelectList(db.tbPersonas.Where(o => o.per_Estado == true), "per_Id", "req_Descripcion");
            return View();
        }
        [HttpPost]
        public ActionResult Requisicion(int Id_Requisicion, DateTime Fecha_Contratacion)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = false;
            reportViewer.Width = Unit.Pixel(1050);
            reportViewer.Height = Unit.Pixel(500);
            reportViewer.BackColor = System.Drawing.Color.White;
            var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;


            //comando para el dataAdapter
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM [rrhh].[V_RPT_Requisiciones] where Id_Requisicion = @Id_Requisicion  and Fecha_Contratacion = @Fecha_Contratacion";

            command.Parameters.AddWithValue("@Id_Requisicion", SqlDbType.Int).Value = Id_Requisicion;
            //command.Parameters.AddWithValue("@tiho_Descripcion", SqlDbType.NVarChar).Value = tiho_Descripcion;
            command.Parameters.AddWithValue("@Fecha_Contratacion", SqlDbType.NVarChar).Value = Fecha_Contratacion;
            
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter(command);
            command.Connection = conx;
            adp.Fill(ds, ds.V_RPT_Requisiciones.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\RequisicionesRPT.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_Requisiciones"]));
            conx.Close();
            ViewBag.ReportViewer = reportViewer;

            ViewBag.ReportViewer = reportViewer;

            ViewBag.aja = db.tbRequisiciones.Where(o => o.req_Id == Id_Requisicion).Select(o => o.req_Descripcion).FirstOrDefault();
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            ViewBag.Requisicion = new SelectList(db.tbRequisiciones.Where(o => o.req_Estado == true), "req_Id", "req_Descripcion");
            // ViewBag.Requisiciones = new SelectList(db.tbRequisiciones.Where(o => o.req_Estado == true), "req_Id", "req_Descripcion");
            return View();
        }
        public ActionResult Empleado()
        {
            ViewBag.Habilidad = new SelectList(db.tbHabilidades.Where(o => o.habi_Estado == true), "habi_Id", "habi_Descripcion");
            ViewBag.Competencias = new SelectList(db.tbCompetencias.Where(o => o.comp_Estado == true), "comp_Id", "comp_Descripcion");
            return View();
        }
        //parametros del reporte
        [HttpPost]
        public ActionResult FaseSeleccion(int? habi_Id, int? comp_Id)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = false;
            reportViewer.Width = Unit.Pixel(1050);
            reportViewer.Height = Unit.Pixel(500);
            reportViewer.BackColor = System.Drawing.Color.White;
            var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;


            //comando para el dataAdapter
            SqlCommand command = new SqlCommand();
            if (comp_Id == null && habi_Id == null)
            {
                command.CommandText = "SELECT * FROM rrhh.V_RPT_Empleado_Curriculum";
            }
            else if (comp_Id == null)
            {
                command.CommandText = "SELECT * FROM rrhh.V_RPT_Empleado_Curriculum where habi_Id = @habi_Id ";
                command.Parameters.AddWithValue("@habi_Id", SqlDbType.Date).Value = habi_Id;
            }
            else if (habi_Id == null)
            {
                command.CommandText = "SELECT * FROM rrhh.V_RPT_Empleado_Curriculum where  comp_Id =@comp_Id ";
                command.Parameters.AddWithValue("@comp_Id", SqlDbType.Int).Value = comp_Id;
            }
            else
            {
                command.CommandText = "SELECT * FROM rrhh.V_RPT_Empleado_Curriculum where habi_Id = @habi_Id and comp_Id = @comp_Id";
                command.Parameters.AddWithValue("@habi_Id", SqlDbType.Date).Value = habi_Id;
                command.Parameters.AddWithValue("@comp_Id", SqlDbType.Date).Value = comp_Id;

            }
            SqlConnection conx = new SqlConnection(connectionString);
            command.Connection = conx;
            SqlDataAdapter adp = new SqlDataAdapter(command);
            adp.Fill(ds, ds.V_RPT_Empleado_Curriculum.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\EmpleadoCurriculum.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_Empleado_Curriculum"]));
            conx.Close();

            ViewBag.ReportViewer = reportViewer;
            ViewBag.Titulo = db.tbHabilidades.Where(x => x.habi_Id == habi_Id).Select(x => x.habi_Descripcion).FirstOrDefault();
            // ViewBag.Titulo1 = db.tbCompetencias.Where(x => x.Comp_Id == Comp_Id).Select(x => x.comp_Descripcion).FirstOrDefault();
            ViewBag.Habilidad = new SelectList(db.tbHabilidades.Where(o => o.habi_Estado == true), "habi_Id", "habi_Descripcion");
            ViewBag.Competencias = new SelectList(db.tbCompetencias.Where(o => o.comp_Estado == true), "comp_Id", "comp_Descripcion");
            return View();
        }
    
    public ActionResult HistorialAmonestaciones()
        {
            ViewBag.TipoAmonesta = new SelectList(db.tbTipoAmonestaciones.Where(o => o.tamo_Estado == true), "tamo_Id", "tamo_Descripcion");
            return View();
        }
        public ActionResult HistorialAudienciaDescargo()
        {
            //ViewBag.TipoAmonesta = new SelectList(db.tbTipoAmonestaciones.Where(o => o.tamo_Estado == true), "tamo_Id", "tamo_Descripcion");
            return View();
        }


        [HttpPost]
        public ActionResult HistorialAmonestaciones(int tamo_Id, DateTime Fecha, DateTime Fecha1, string Colaborador)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = false;
            reportViewer.Width = Unit.Pixel(1050);
            reportViewer.Height = Unit.Pixel(500);
            reportViewer.BackColor = System.Drawing.Color.White;
            var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;


            //comando para el dataAdapter
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * from rrhh.V_RPT_HistorialAmonestaciones WHERE  tamo_Id = @tamo_Id  and Fecha BETWEEN @Fecha AND @Fecha1 and Colaborador like '%@Colaborador%'";
            command.Parameters.AddWithValue("@tamo_Id", SqlDbType.Int).Value = tamo_Id;
            command.Parameters.AddWithValue("@Fecha", SqlDbType.DateTime).Value = Fecha;
            command.Parameters.AddWithValue("@Fecha1", SqlDbType.DateTime).Value = Fecha1;
            command.Parameters.AddWithValue("@Colaborador", SqlDbType.Text).Value = Colaborador;

            SqlConnection conx = new SqlConnection(connectionString);
            command.Connection = conx;
            SqlDataAdapter adp = new SqlDataAdapter(command);
            adp.Fill(ds, ds.V_RPT_HistorialAmonestaciones.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\HistorialAmonestacionesRPT.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_HistorialAmonestaciones"]));
            conx.Close();


            ViewBag.ReportViewer = reportViewer;
            //ViewBag.TipoHora = db.tbHistorialHorasTrabajadas.Where(x => x.htra_Id == htra_Id);

            ViewBag.Titulos = db.tbTipoAmonestaciones.Where(x => x.tamo_Id == tamo_Id).Select(x => x.tamo_Descripcion).FirstOrDefault();
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            ViewBag.Turno = new SelectList(db.tbTipoAmonestaciones.Where(o => o.tamo_Estado == true), "tamo_Id", "tamo_Descripcion");
            //ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
            return View();
        }
        //HistorialAudienciaDescargo
        [HttpPost]
        public ActionResult HistorialAudienciaDescargo(DateTime aude_fechaaudiencia, DateTime aude_fechaaudiencia1, string nombre)
        {

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = false;
            reportViewer.Width = Unit.Pixel(1050);
            reportViewer.Height = Unit.Pixel(500);
            reportViewer.BackColor = System.Drawing.Color.White;
            var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;

            //comando para el dataAdapter
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * from rrhh.V_RPT_HistorialAudienciaDescargo WHERE aude_fechaaudiencia BETWEEN @Fecha AND @Fecha1 and nombre like '%@nombre%'";
            command.Parameters.AddWithValue("@Fecha", SqlDbType.DateTime).Value = aude_fechaaudiencia;
            command.Parameters.AddWithValue("@Fecha1", SqlDbType.DateTime).Value = aude_fechaaudiencia1;
            command.Parameters.AddWithValue("@nombre", SqlDbType.Text).Value = nombre;


            SqlConnection conx = new SqlConnection(connectionString);
            command.Connection = conx;
            SqlDataAdapter adp = new SqlDataAdapter(command);
            adp.Fill(ds, ds.V_RPT_HorasTrabajadas.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\AudienciaDescargo.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_HistorialAudienciaDescargo"]));
            conx.Close();


            ViewBag.ReportViewer = reportViewer;
            //ViewBag.TipoHora = db.tbHistorialHorasTrabajadas.Where(x => x.htra_Id == htra_Id);

            //ViewBag.Titulos = db.tbTipoHoras.Where(x => x.tiho_Id == tiho_Id).Select(x => x.tiho_Descripcion).FirstOrDefault();
            ////Cargar DDL del modal (Tipo de planilla a seleccionar)
            //ViewBag.Turno = new SelectList(db.tbTipoHoras.Where(o => o.tiho_Estado == true), "tiho_Id", "tiho_Descripcion");
            ////ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
            return View();
        }

    }
}


