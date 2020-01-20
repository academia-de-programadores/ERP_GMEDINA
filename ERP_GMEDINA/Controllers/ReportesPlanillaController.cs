using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.Reporting.WebForms;
using ERP_GMEDINA.Models;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using Rotativa;
using ERP_GMEDINA.DataSets;
using System.Data;

namespace ERP_GMEDINA.Controllers
{
    public class ReportesPlanillaController : Controller
    {
		private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

		ReportesPlanillaDS ds = new ReportesPlanillaDS();

        //Listo
		#region deducciones dinamico

		//parametros del reporte
		public ActionResult DeduccionesParametros()
		{
			//Cargar DDL del modal (Tipo de planilla a seleccionar)
			ViewBag.Deducciones = new SelectList(db.tbCatalogoDeDeducciones.Where(o => o.cde_Activo == true), "cde_IdDeducciones", "cde_DescripcionDeduccion");
			ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
			return View();
		}

		//parametros del reporte
		[HttpPost]
		public ActionResult DeduccionesParametros(int cde_IdDeducciones, int cpla_IdPlanilla, DateTime hipa_FechaInicio, DateTime hipa_FechaFin)
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
			command.CommandText = "SELECT * FROM Plani.V_Deducciones_RPT where cde_IdDeducciones = @cde_IdDeducciones and cpla_IdPlanilla = @cpla_IdPlanilla and hipa_FechaPago between @hipa_FechaInicio and @hipa_FechaFin";
			command.Parameters.AddWithValue("@cde_IdDeducciones", SqlDbType.Int).Value = cde_IdDeducciones;
			command.Parameters.AddWithValue("@cpla_IdPlanilla", SqlDbType.Int).Value = cpla_IdPlanilla;
			command.Parameters.AddWithValue("@hipa_FechaInicio", SqlDbType.Date).Value = hipa_FechaInicio;
			command.Parameters.AddWithValue("@hipa_FechaFin", SqlDbType.Date).Value = hipa_FechaFin;
			command.Connection = conx;
			SqlDataAdapter adp = new SqlDataAdapter(command);
			adp.Fill(ds, ds.V_Deducciones_RPT.TableName);

			reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"ReportesPlanilla\DeduccionesRPT.rdlc";
			reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesPlanillaDS", ds.Tables["V_Deducciones_RPT"]));

            //Setiar parametros del reporte para asignar logo y usuario crea del reporte.
            reportViewer.LocalReport.EnableExternalImages = true;
            List<ReportParameter> parameters = new List<ReportParameter>();
            //parameters.Add(new ReportParameter("logo", "file:" + @"C:\Users\LAB02\Desktop\Proyecto AHM\GITHUB\Proyecto_ERP_GMEDINA\ERP_GMEDINA\ReportesPlanilla\intel.jpg"));

            var oUsuario = (ERP_GMEDINA.Models.tbUsuario)HttpContext.Session["sesionUsuario"];
            string nombreUsuario = oUsuario.usu_NombreUsuario;
            parameters.Add(new ReportParameter("usuario", nombreUsuario));

            reportViewer.LocalReport.SetParameters(parameters);
            reportViewer.LocalReport.Refresh();


            conx.Close();

			ViewBag.ReportViewer = reportViewer;
			ViewBag.Titulo = db.tbCatalogoDeDeducciones.Where(x => x.cde_IdDeducciones == cde_IdDeducciones).Select(x => x.cde_DescripcionDeduccion).FirstOrDefault();
			//Cargar DDL del modal (Tipo de planilla a seleccionar)
			ViewBag.Deducciones = new SelectList(db.tbCatalogoDeDeducciones.Where(o => o.cde_Activo == true), "cde_IdDeducciones", "cde_DescripcionDeduccion");
			ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
			return View();
		}

		#endregion

        //Listo
		#region ingresos dinamico

		//parametros del reporte
		public ActionResult IngresosParametros()
		{
			//Cargar DDL del modal (Tipo de planilla a seleccionar)
			ViewBag.Ingresos = new SelectList(db.tbCatalogoDeIngresos.Where(o => o.cin_Activo == true), "cin_IdIngreso", "cin_DescripcionIngreso");
			ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
			return View();
		}

		[HttpPost]
		//parametros del reporte
		public ActionResult IngresosParametros(int cin_IdIngreso, int cpla_IdPlanilla, DateTime hipa_FechaInicio, DateTime hipa_FechaFin)
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
			command.CommandText = "SELECT * FROM Plani.V_Ingresos_RPT where cin_IdIngreso = @cin_IdIngreso and cpla_IdPlanilla = @cpla_IdPlanilla and hipa_FechaPago between @hipa_FechaInicio and @hipa_FechaFin";
			command.Parameters.AddWithValue("@cin_IdIngreso", SqlDbType.Int).Value = cin_IdIngreso;
			command.Parameters.AddWithValue("@cpla_IdPlanilla", SqlDbType.Int).Value = cpla_IdPlanilla;
			command.Parameters.AddWithValue("@hipa_FechaInicio", SqlDbType.Date).Value = hipa_FechaInicio;
			command.Parameters.AddWithValue("@hipa_FechaFin", SqlDbType.Date).Value = hipa_FechaFin;
			command.Connection = conx;
			SqlDataAdapter adp = new SqlDataAdapter(command);
			//adp.Fill(ds, ds.V_Ingresos_RPT.TableName);
			adp.Fill(ds, ds.V_Ingresos_RPT.TableName);

			reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"ReportesPlanilla\IngresosRPT.rdlc";
			reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesPlanillaDS", ds.Tables["V_Ingresos_RPT"]));

			ViewBag.ReportViewer = reportViewer;
			//Cargar DDL del modal (Tipo de planilla a seleccionar)
			ViewBag.Ingresos = new SelectList(db.tbCatalogoDeIngresos.Where(o => o.cin_Activo == true), "cin_IdIngreso", "cin_DescripcionIngreso");
			ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
			ViewBag.Titulo = db.tbCatalogoDeIngresos.Where(x => x.cin_IdIngreso == cin_IdIngreso).Select(x => x.cin_DescripcionIngreso).FirstOrDefault();

            //Setiar parametros del reporte para asignar logo y usuario crea del reporte.
            reportViewer.LocalReport.EnableExternalImages = true;
            List<ReportParameter> parameters = new List<ReportParameter>();
            //parameters.Add(new ReportParameter("logo", "file:" + @"C:\Users\LAB02\Desktop\Proyecto AHM\GITHUB\Proyecto_ERP_GMEDINA\ERP_GMEDINA\ReportesPlanilla\intel.jpg"));

            var oUsuario = (ERP_GMEDINA.Models.tbUsuario)HttpContext.Session["sesionUsuario"];
            string nombreUsuario = oUsuario.usu_NombreUsuario;
            parameters.Add(new ReportParameter("usuario", nombreUsuario));

            reportViewer.LocalReport.SetParameters(parameters);
            reportViewer.LocalReport.Refresh();

            conx.Close();

			return View();
		}
		#endregion

		#region reportes varios dinamico

		//parametros del reporte
		public ActionResult ReportesVariosParametros()
		{
			//Cargar DDL del modal (Tipo de planilla a seleccionar)
			ViewBag.Deducciones = new SelectList(db.tbCatalogoDeDeducciones.Where(o => o.cde_Activo == true), "cde_IdDeducciones", "cde_DescripcionDeduccion");
			ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
			return View();
		}

		//Report viewer
		[HttpPost]
		public ActionResult ReportesVariosParametros(string reporte, int cpla_IdPlanilla, DateTime hipa_FechaInicio, DateTime hipa_FechaFin)
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
			command.Parameters.AddWithValue("@cpla_IdPlanilla", SqlDbType.Int).Value = cpla_IdPlanilla;
			command.Parameters.AddWithValue("@hipa_FechaInicio", SqlDbType.Date).Value = hipa_FechaInicio;
			command.Parameters.AddWithValue("@hipa_FechaFin", SqlDbType.Date).Value = hipa_FechaFin;
			command.Connection = conx;




			// seleccionar reporte
			reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"ReportesPlanilla\ReporteISRRPT.rdlc";
			command.CommandText = "SELECT emp_Id,per_Nombres,per_Apellidos,hipa_FechaInicio,hipa_FechaFin, hipa_FechaPago,hipa_TotalISR,cpla_DescripcionPlanilla FROM Plani.V_ReportesVarios where hipa_TotalISR >0 and  cpla_IdPlanilla = @cpla_IdPlanilla and hipa_FechaPago between @hipa_FechaInicio and @hipa_FechaFin";
			string encabezadoReporte = "ISR";

			switch (reporte)
			{
				case "AFP":
					command.CommandText = "SELECT emp_Id,per_Nombres,per_Apellidos,hipa_FechaInicio,hipa_FechaFin, hipa_FechaPago,hipa_AFP,cpla_DescripcionPlanilla FROM Plani.V_ReportesVarios where hipa_AFP > 0 and cpla_IdPlanilla = @cpla_IdPlanilla and hipa_FechaPago between @hipa_FechaInicio and @hipa_FechaFin";
					reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"ReportesPlanilla\ReporteAFPRPT.rdlc";
					encabezadoReporte = "AFP";
					break;

				case "Comisiones":
					command.CommandText = "SELECT emp_Id,per_Nombres,per_Apellidos,hipa_FechaInicio,hipa_FechaFin, hipa_FechaPago,hipa_TotalComisiones,cpla_DescripcionPlanilla FROM Plani.V_ReportesVarios where hipa_TotalComisiones > 0 and cpla_IdPlanilla = @cpla_IdPlanilla and hipa_FechaPago between @hipa_FechaInicio and @hipa_FechaFin";
					reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"ReportesPlanilla\ReporteComisionesRPT.rdlc";
					encabezadoReporte = "Comisiones";
					break;

				case "HorasExtras":
					command.CommandText = "SELECT emp_Id,per_Nombres,per_Apellidos,hipa_FechaInicio,hipa_FechaFin, hipa_FechaPago,hipa_TotalHorasExtras,cpla_DescripcionPlanilla FROM Plani.V_ReportesVarios where hipa_TotalHorasExtras > 0 and cpla_IdPlanilla = @cpla_IdPlanilla and hipa_FechaPago between @hipa_FechaInicio and @hipa_FechaFin";
					reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"ReportesPlanilla\ReporteHorasExtrasRPT.rdlc";
					encabezadoReporte = "Horas extras";
					break;

				case "Vacaciones":
					command.CommandText = "SELECT emp_Id,per_Nombres,per_Apellidos,hipa_FechaInicio,hipa_FechaFin, hipa_FechaPago,hipa_TotalVacaciones,cpla_DescripcionPlanilla FROM Plani.V_ReportesVarios where hipa_TotalVacaciones > 0 and  cpla_IdPlanilla = @cpla_IdPlanilla and hipa_FechaPago between @hipa_FechaInicio and @hipa_FechaFin";
					reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"ReportesPlanilla\ReporteVacacionesRPT.rdlc";
					encabezadoReporte = "Vacaciones";
					break;
				case "SeptimoDia":
					command.CommandText = "SELECT emp_Id,per_Nombres,per_Apellidos,hipa_FechaInicio,hipa_FechaFin, hipa_FechaPago,hipa_TotalSeptimoDia,cpla_DescripcionPlanilla FROM Plani.V_ReportesVarios where hipa_TotalSeptimoDia > 0 and cpla_IdPlanilla = @cpla_IdPlanilla and hipa_FechaPago between @hipa_FechaInicio and @hipa_FechaFin";
					reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"ReportesPlanilla\ReporteSeptimoDiaRPT.rdlc";
					encabezadoReporte = "Séptimo día";
					break;
				case "AdelantosSueldo":
					command.CommandText = "SELECT emp_Id,per_Nombres,per_Apellidos,hipa_FechaInicio,hipa_FechaFin, hipa_FechaPago,hipa_AdelantoSueldo,cpla_DescripcionPlanilla FROM Plani.V_ReportesVarios where hipa_AdelantoSueldo > 0 and cpla_IdPlanilla = @cpla_IdPlanilla and hipa_FechaPago between @hipa_FechaInicio and @hipa_FechaFin";
					reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"ReportesPlanilla\ReporteAdelantoSueldoRPT.rdlc";
					encabezadoReporte = "Adelantos de sueldo";
					break;
			}

			SqlDataAdapter adp = new SqlDataAdapter(command);
			adp.Fill(ds, "tbHistorialDePago");
			reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesPlanillaDS", ds.Tables["tbHistorialDePago"]));
			ViewBag.ReportViewer = reportViewer;
			ViewBag.Encabezado = encabezadoReporte;
			//DDLS
			ViewBag.Deducciones = new SelectList(db.tbCatalogoDeDeducciones.Where(o => o.cde_Activo == true), "cde_IdDeducciones", "cde_DescripcionDeduccion");
			ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");

			conx.Close();

			return View();
		}

		#endregion

        //Listo
		#region decimo tercer mes

		//parametros del reporte
		public ActionResult DecimoTercerParametros()
		{
			//Cargar DDL del modal (Tipo de planilla a seleccionar)
			ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
			return View();
		}

		//parametros del reporte
		[HttpPost]
		public ActionResult DecimoTercerParametros(int cpla_IdPlanilla, DateTime dtm_FechaPago)
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
			command.CommandText = "SELECT * FROM [Plani].[V_DecimoTercerMes_RPT] where cpla_IdPlanilla = @cpla_IdPlanilla and dtm_FechaPago = @dtm_FechaPago";
			command.Parameters.AddWithValue("@cpla_IdPlanilla", SqlDbType.Int).Value = cpla_IdPlanilla;
			command.Parameters.AddWithValue("@dtm_FechaPago", SqlDbType.Date).Value = dtm_FechaPago;
			command.Connection = conx;
			SqlDataAdapter adp = new SqlDataAdapter(command);
			adp.Fill(ds, ds.V_DecimoTercerMes_RPT.TableName);

			reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"ReportesPlanilla\DecimoTercerRPT.rdlc";
			reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesPlanillaDS", ds.Tables["V_DecimoTercerMes_RPT"]));

            //Setiar parametros del reporte para asignar logo y usuario crea del reporte.
            reportViewer.LocalReport.EnableExternalImages = true;
            List<ReportParameter> parameters = new List<ReportParameter>();
            //parameters.Add(new ReportParameter("logo", "file:" + @"C:\Users\LAB02\Desktop\Proyecto AHM\GITHUB\Proyecto_ERP_GMEDINA\ERP_GMEDINA\ReportesPlanilla\intel.jpg"));

            var oUsuario = (ERP_GMEDINA.Models.tbUsuario)HttpContext.Session["sesionUsuario"];
            string nombreUsuario = oUsuario.usu_NombreUsuario;
            parameters.Add(new ReportParameter("usuario", nombreUsuario));

            reportViewer.LocalReport.SetParameters(parameters);
            reportViewer.LocalReport.Refresh();
            conx.Close();

			ViewBag.ReportViewer = reportViewer;
			//Cargar DDL del modal (Tipo de planilla a seleccionar)
			ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
			return View();
		}
		#endregion

        //Listo
		#region decimo cuarto mes

		//parametros del reporte
		public ActionResult DecimoCuartoParametros()
		{
			//Cargar DDL del modal (Tipo de planilla a seleccionar)
			ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
			return View();
		}

		//parametros del reporte
		[HttpPost]
		public ActionResult DecimoCuartoParametros(int cpla_IdPlanilla, DateTime dcm_FechaPago)
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
			command.CommandText = "SELECT * FROM [Plani].[V_DecimoCuartoMes_RPT] where cpla_IdPlanilla = @cpla_IdPlanilla and dcm_FechaPago = @dcm_FechaPago";
			command.Parameters.AddWithValue("@cpla_IdPlanilla", SqlDbType.Int).Value = cpla_IdPlanilla;
			command.Parameters.AddWithValue("@dcm_FechaPago", SqlDbType.Date).Value = dcm_FechaPago;
			command.Connection = conx;
			SqlDataAdapter adp = new SqlDataAdapter(command);
			adp.Fill(ds, ds.V_DecimoCuartoMes_RPT.TableName);

			reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"ReportesPlanilla\DecimoCuartoRPT.rdlc";
			reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesPlanillaDS", ds.Tables["V_DecimoCuartoMes_RPT"]));

            //Setiar parametros del reporte para asignar logo y usuario crea del reporte.
            reportViewer.LocalReport.EnableExternalImages = true;
            List<ReportParameter> parameters = new List<ReportParameter>();
            //parameters.Add(new ReportParameter("logo", "file:" + @"C:\Users\LAB02\Desktop\Proyecto AHM\GITHUB\Proyecto_ERP_GMEDINA\ERP_GMEDINA\ReportesPlanilla\intel.jpg"));

            var oUsuario = (ERP_GMEDINA.Models.tbUsuario)HttpContext.Session["sesionUsuario"];
            string nombreUsuario = oUsuario.usu_NombreUsuario;
            parameters.Add(new ReportParameter("usuario", nombreUsuario));

            reportViewer.LocalReport.SetParameters(parameters);
            reportViewer.LocalReport.Refresh();
            conx.Close();

			ViewBag.ReportViewer = reportViewer;
			//Cargar DDL del modal (Tipo de planilla a seleccionar)
			ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
			return View();
		}
        #endregion

        #region instituciones financieras

        //parametros del reporte
        public ActionResult InstitucionesFinancierasParametros()
        {
            var empleados =
            from Emp in db.tbEmpleados
            join Per in db.tbPersonas on Emp.per_Id equals Per.per_Id
            where Emp.emp_Estado == true
            select new
            {
                emp_Id = Emp.emp_Id,
                Nombres = Per.per_Nombres + " " + Per.per_Apellidos
            };

            ViewBag.Empleados = new SelectList(empleados, "emp_Id", "Nombres");
            ViewBag.Instituciones = new SelectList(db.tbInstitucionesFinancieras.Where(o => o.insf_Activo == true), "insf_IdInstitucionFinanciera", "insf_DescInstitucionFinanc");
            ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
            return View();
        }

        [HttpPost]
        //parametros del reporte
        public ActionResult InstitucionesFinancierasParametros(int? emp_Id, int? insf_IdInstitucionFinanciera, int cpla_IdPlanilla)
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
            command.CommandText = "SELECT * FROM Plani.V_ReporteInstitucionesFinancieras_RPT where emp_Id = @emp_Id and insf_IdInstitucionFinanciera = @insf_IdInstitucionFinanciera and cpla_IdPlanilla = @cpla_IdPlanilla";
            command.Parameters.AddWithValue("@cpla_IdPlanilla", SqlDbType.Int).Value = cpla_IdPlanilla;
            command.Parameters.AddWithValue("@emp_Id", SqlDbType.Int).Value = emp_Id;
            command.Parameters.AddWithValue("@insf_IdInstitucionFinanciera", SqlDbType.Int).Value = insf_IdInstitucionFinanciera;
            command.Connection = conx;
            SqlDataAdapter adp = new SqlDataAdapter(command);
            //adp.Fill(ds, ds.V_Ingresos_RPT.TableName);
            adp.Fill(ds, "tbInstitucionesFinancieras");

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"ReportesPlanilla\InstitucionesFinancierasRPT.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesPlanillaDS", ds.Tables["tbInstitucionesFinancieras"]));

            ViewBag.ReportViewer = reportViewer;            
            conx.Close();

            var empleados =
            from Emp in db.tbEmpleados
            join Per in db.tbPersonas on Emp.per_Id equals Per.per_Id
            where Emp.emp_Estado == true
            select new
            {
                emp_Id = Emp.emp_Id,
                Nombres = Per.per_Nombres + " " + Per.per_Apellidos
            };

            ViewBag.Empleados = new SelectList(empleados, "emp_Id", "Nombres");
            ViewBag.Instituciones = new SelectList(db.tbInstitucionesFinancieras.Where(o => o.insf_Activo == true), "insf_IdInstitucionFinanciera", "insf_DescInstitucionFinanc");
            ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
            return View();
        }
        #endregion
    }
}
