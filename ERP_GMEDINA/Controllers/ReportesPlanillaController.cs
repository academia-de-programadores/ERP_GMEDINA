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
			conx.Close();

			ViewBag.ReportViewer = reportViewer;
			ViewBag.Titulo = db.tbCatalogoDeDeducciones.Where(x => x.cde_IdDeducciones == cde_IdDeducciones).Select(x => x.cde_DescripcionDeduccion).FirstOrDefault();
			//Cargar DDL del modal (Tipo de planilla a seleccionar)
			ViewBag.Deducciones = new SelectList(db.tbCatalogoDeDeducciones.Where(o => o.cde_Activo == true), "cde_IdDeducciones", "cde_DescripcionDeduccion");
			ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
			return View();
		}	

		#endregion

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
			conx.Close();

			return View();
		}		
		#endregion

		//Pendiente
		#region reportes varios dinamico 

		//parametros del reporte
		public ActionResult ReportesVariosParametros()
		{
			//Cargar DDL del modal (Tipo de planilla a seleccionar)
			ViewBag.Deducciones = new SelectList(db.tbCatalogoDeDeducciones.Where(o => o.cde_Activo == true), "cde_IdDeducciones", "cde_DescripcionDeduccion");
			ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
			return View();
		}
		
		#endregion

	}
}
