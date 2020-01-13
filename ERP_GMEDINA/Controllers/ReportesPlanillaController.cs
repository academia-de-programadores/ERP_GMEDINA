using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ERP_GMEDINA.Models;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using Rotativa;

namespace ERP_GMEDINA.Controllers
{
    public class ReportesPlanillaController : Controller
    {
		private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        //ROTATIVA REPORTS
        #region Rotativa

        #region deducciones
        //---------------------------------DEDUCCIONES---------------------------------//
        //vista inicial
        public ActionResult Deducciones()
		{
			//Cargar DDL del modal (Tipo de planilla a seleccionar)
			ViewBag.Deducciones = new SelectList(db.tbCatalogoDeDeducciones.Where(o => o.cde_Activo == true), "cde_IdDeducciones", "cde_DescripcionDeduccion");
			ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");

			return View();
		}

		//vista de previsualización del reporte
		public ActionResult ReportDeduccionesPreview(int cde_IdDeducciones, DateTime hipa_FechaInicio, DateTime hipa_FechaFin, int cpla_IdPlanilla)
		{
			var ReportDeducciones = from d in db.V_Deducciones_RPT
									where
									d.cde_IdDeducciones == cde_IdDeducciones &&
									d.hipa_FechaPago >= hipa_FechaInicio &&
									d.hipa_FechaPago <= hipa_FechaFin &&
									d.cpla_IdPlanilla == cpla_IdPlanilla
									select new ViewModelDeduccionesReport
									{
										emp_Id = d.emp_Id,
										per_Nombres = d.per_Nombres,
										per_Apellidos = d.per_Apellidos,
										cde_IdDeducciones = d.cde_IdDeducciones,
										cde_DescripcionDeduccion = d.cde_DescripcionDeduccion,
										hidp_Total = d.hidp_Total,
										hipa_FechaInicio = d.hipa_FechaInicio,
										hipa_FechaFin = d.hipa_FechaFin,
										cpla_DescripcionPlanilla = d.cpla_DescripcionPlanilla

									};

			ViewBag.ReportDeducciones = ReportDeducciones.ToList();
			ViewBag.Encabezado = db.tbCatalogoDeDeducciones.Where(x => x.cde_IdDeducciones == cde_IdDeducciones).Select(x => x.cde_DescripcionDeduccion).FirstOrDefault();
			//parametros
			ViewBag.cde_IdDeducciones = cde_IdDeducciones;
			ViewBag.hipa_FechaInicio = hipa_FechaInicio;
			ViewBag.hipa_FechaFin = hipa_FechaFin;
			ViewBag.cpla_IdPlanilla = cpla_IdPlanilla;
			return View();
		}
		//vista del reporte
		public ActionResult ReportDeducciones(int cde_IdDeducciones, DateTime hipa_FechaInicio, DateTime hipa_FechaFin, int cpla_IdPlanilla)
		{
			var ReportDeducciones = from d in db.V_Deducciones_RPT
									where
									d.cde_IdDeducciones == cde_IdDeducciones &&
									d.hipa_FechaPago >= hipa_FechaInicio &&
									d.hipa_FechaPago <= hipa_FechaFin &&
									d.cpla_IdPlanilla == cpla_IdPlanilla
									select new ViewModelDeduccionesReport
									{
										emp_Id = d.emp_Id,
										per_Nombres = d.per_Nombres,
										per_Apellidos = d.per_Apellidos,
										cde_IdDeducciones = d.cde_IdDeducciones,
										cde_DescripcionDeduccion = d.cde_DescripcionDeduccion,
										hidp_Total = d.hidp_Total,
										hipa_FechaInicio = d.hipa_FechaInicio,
										hipa_FechaFin = d.hipa_FechaFin,
										cpla_DescripcionPlanilla = d.cpla_DescripcionPlanilla

									};

			ViewBag.ReportDeducciones = ReportDeducciones.ToList();
			ViewBag.Encabezado = ReportDeducciones.Select(x => x.cde_DescripcionDeduccion).FirstOrDefault();
			return View();
		}

		// acción para imprimir
		public ActionResult PrintDeducciones(int cde_IdDeducciones, DateTime hipa_FechaInicio, DateTime hipa_FechaFin, int cpla_IdPlanilla)
		{
			string NombrePdf = db.tbCatalogoDeDeducciones.Where(x => x.cde_IdDeducciones == cde_IdDeducciones).Select(x => x.cde_DescripcionDeduccion).FirstOrDefault();

			return new ActionAsPdf("ReportDeducciones", new { cde_IdDeducciones = cde_IdDeducciones, hipa_FechaInicio = hipa_FechaInicio, hipa_FechaFin = hipa_FechaFin, cpla_IdPlanilla = cpla_IdPlanilla })
			{
				FileName = $"{NombrePdf}.pdf"
			};

		}

		#endregion

		#region ingresos
		//---------------------------------INGRESOS---------------------------------//
		// vista inicial
		public ActionResult Ingresos()
		{
			//Cargar DDL Tipo de ingreso a seleccionar
			ViewBag.Ingresos = new SelectList(db.tbCatalogoDeIngresos.Where(o => o.cin_Activo == true), "cin_IdIngreso", "cin_DescripcionIngreso");
			//Cargar DDL Tipo de planilla a seleccionar
			ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
			return View();
		}

		// vista de previsualización
		public ActionResult ReportIngresosPreview(int cin_IdIngreso, DateTime hipa_FechaInicio, DateTime hipa_FechaFin, int cpla_IdPlanilla)
		{
			var ReportIngresos = from d in db.V_Ingresos_RPT
								 where
								 d.cin_IdIngreso == cin_IdIngreso &&
								 d.hipa_FechaPago >= hipa_FechaInicio &&
								 d.hipa_FechaPago <= hipa_FechaFin &&
								 d.cpla_IdPlanilla == cpla_IdPlanilla
								 select new ViewModelingresosReport
								 {
									 emp_Id = d.emp_Id,
									 per_Nombres = d.per_Nombres,
									 per_Apellidos = d.per_Apellidos,
									 cin_IdIngreso = d.cin_IdIngreso,
									 cin_DescripcionIngreso = d.cin_DescripcionIngreso,
									 hip_TotalPagar = d.hip_TotalPagar,
									 hipa_FechaInicio = d.hipa_FechaInicio,
									 hipa_FechaFin = d.hipa_FechaFin,
									 cpla_DescripcionPlanilla = d.cpla_DescripcionPlanilla

								 };

			ViewBag.ReportIngresos = ReportIngresos.ToList();
			ViewBag.Encabezado = db.tbCatalogoDeIngresos.Where(x => x.cin_IdIngreso == cin_IdIngreso).Select(x => x.cin_DescripcionIngreso).FirstOrDefault();
			//parametros
			ViewBag.cin_IdIngreso = cin_IdIngreso;
			ViewBag.hipa_FechaInicio = hipa_FechaInicio;
			ViewBag.hipa_FechaFin = hipa_FechaFin;
			ViewBag.cpla_IdPlanilla = cpla_IdPlanilla;
			return View();
		}

		// vista del reporte
		public ActionResult ReportIngresos(int cin_IdIngreso, DateTime hipa_FechaInicio, DateTime hipa_FechaFin, int cpla_IdPlanilla)
		{
			var ReportIngresos = from d in db.V_Ingresos_RPT
								 where
								 d.cin_IdIngreso == cin_IdIngreso &&
								 d.hipa_FechaInicio >= hipa_FechaInicio &&
								 d.hipa_FechaInicio <= hipa_FechaFin &&
								 d.hipa_FechaFin >= hipa_FechaInicio &&
								 d.hipa_FechaFin <= hipa_FechaFin &&
								 d.cpla_IdPlanilla == cpla_IdPlanilla
								 select new ViewModelingresosReport
								 {
									 emp_Id = d.emp_Id,
									 per_Nombres = d.per_Nombres,
									 per_Apellidos = d.per_Apellidos,
									 cin_IdIngreso = d.cin_IdIngreso,
									 cin_DescripcionIngreso = d.cin_DescripcionIngreso,
									 hip_TotalPagar = d.hip_TotalPagar,
									 hipa_FechaInicio = d.hipa_FechaInicio,
									 hipa_FechaFin = d.hipa_FechaFin,
									 cpla_DescripcionPlanilla = d.cpla_DescripcionPlanilla

								 };

			ViewBag.ReportIngresos = ReportIngresos.ToList();
			ViewBag.Encabezado = ReportIngresos.Select(x => x.cin_DescripcionIngreso).FirstOrDefault();
			return View();
		}

		// accion para imprimir
		public ActionResult PrintIngresos(int cin_IdIngreso, DateTime hipa_FechaInicio, DateTime hipa_FechaFin, int cpla_IdPlanilla)
		{
			string NombrePdf = db.tbCatalogoDeIngresos.Where(x => x.cin_IdIngreso == cin_IdIngreso).Select(x => x.cin_DescripcionIngreso).FirstOrDefault();

			return new ActionAsPdf("ReportIngresos", new { cin_IdIngreso = cin_IdIngreso, hipa_FechaInicio = hipa_FechaInicio, hipa_FechaFin = hipa_FechaFin, cpla_IdPlanilla = cpla_IdPlanilla })
			{
				FileName = $"{NombrePdf}.pdf"
			};

		}

		#endregion


		//--------------------------------- Reportes varios ---------------------------------//
		//vista inicial
		public ActionResult ReportesVarios()
		{
			//Cargar DDL del modal (Tipo de planilla a seleccionar)
			//ViewBag.Deducciones = new SelectList(db.tbCatalogoDeDeducciones.Where(o => o.cde_Activo == true), "cde_IdDeducciones", "cde_DescripcionDeduccion");
			ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");

			return View();
		}

		//vista de previsualización del reporte
		public ActionResult ReportVariosPreview(string reporte, DateTime hipa_FechaInicio, DateTime hipa_FechaFin, int cpla_IdPlanilla)
		{
			string encabezado = "ISR";
			string tipoReporte = "ISR";
			var ReportVarios = from d in db.V_ReportesVarios
							   where
							   d.hipa_FechaPago >= hipa_FechaInicio &&
							   d.hipa_FechaPago <= hipa_FechaFin &&
							   d.cpla_IdPlanilla == cpla_IdPlanilla
							   && d.hipa_TotalISR > 0
							   select new ViewModelReportesVarios
							   {
								   emp_Id = d.emp_Id,
								   per_Nombres = d.per_Nombres,
								   per_Apellidos = d.per_Apellidos,
								   concepto = "ISR",
								   monto = d.hipa_TotalISR,
								   hipa_FechaInicio = d.hipa_FechaInicio,
								   hipa_FechaFin = d.hipa_FechaFin,
								   cpla_DescripcionPlanilla = d.cpla_DescripcionPlanilla

							   };

			switch (reporte)
			{
				case "AFP":
					ReportVarios = from d in db.V_ReportesVarios
								   where
								   d.hipa_FechaPago >= hipa_FechaInicio &&
								   d.hipa_FechaPago <= hipa_FechaFin &&
								   d.cpla_IdPlanilla == cpla_IdPlanilla
								   && d.hipa_AFP > 0
								   select new ViewModelReportesVarios
								   {
									   emp_Id = d.emp_Id,
									   per_Nombres = d.per_Nombres,
									   per_Apellidos = d.per_Apellidos,
									   concepto = "AFP",
									   monto = d.hipa_AFP,
									   hipa_FechaInicio = d.hipa_FechaInicio,
									   hipa_FechaFin = d.hipa_FechaFin,
									   cpla_DescripcionPlanilla = d.cpla_DescripcionPlanilla

								   };
					encabezado = "AFP";
					tipoReporte = "AFP";
					break;

				case "Comisiones":
					ReportVarios = from d in db.V_ReportesVarios
								   where
								   d.hipa_FechaPago >= hipa_FechaInicio &&
								   d.hipa_FechaPago <= hipa_FechaFin &&
								   d.cpla_IdPlanilla == cpla_IdPlanilla
								   && d.hipa_TotalComisiones > 0
								   select new ViewModelReportesVarios
								   {
									   emp_Id = d.emp_Id,
									   per_Nombres = d.per_Nombres,
									   per_Apellidos = d.per_Apellidos,
									   concepto = "Comisiones",
									   monto = d.hipa_TotalComisiones,
									   hipa_FechaInicio = d.hipa_FechaInicio,
									   hipa_FechaFin = d.hipa_FechaFin,
									   cpla_DescripcionPlanilla = d.cpla_DescripcionPlanilla

								   };
					encabezado = "Comisiones";
					tipoReporte = "Comisiones";
					break;

				case "HorasExtras":
					ReportVarios = from d in db.V_ReportesVarios
								   where
								   d.hipa_FechaPago >= hipa_FechaInicio &&
								   d.hipa_FechaPago <= hipa_FechaFin &&
								   d.cpla_IdPlanilla == cpla_IdPlanilla
								   && d.hipa_TotalHorasExtras > 0
								   select new ViewModelReportesVarios
								   {
									   emp_Id = d.emp_Id,
									   per_Nombres = d.per_Nombres,
									   per_Apellidos = d.per_Apellidos,
									   concepto = "Horas extras",
									   monto = d.hipa_TotalHorasExtras,
									   hipa_FechaInicio = d.hipa_FechaInicio,
									   hipa_FechaFin = d.hipa_FechaFin,
									   cpla_DescripcionPlanilla = d.cpla_DescripcionPlanilla

								   };
					encabezado = "HorasExtras";
					tipoReporte = "HorasExtras";
					break;

				case "Vacaciones":
					ReportVarios = from d in db.V_ReportesVarios
								   where
								   d.hipa_FechaPago >= hipa_FechaInicio &&
								   d.hipa_FechaPago <= hipa_FechaFin &&
								   d.cpla_IdPlanilla == cpla_IdPlanilla
								   && d.hipa_TotalVacaciones > 0
								   select new ViewModelReportesVarios
								   {
									   emp_Id = d.emp_Id,
									   per_Nombres = d.per_Nombres,
									   per_Apellidos = d.per_Apellidos,
									   concepto = "Vacaciones",
									   monto = d.hipa_TotalVacaciones,
									   hipa_FechaInicio = d.hipa_FechaInicio,
									   hipa_FechaFin = d.hipa_FechaFin,
									   cpla_DescripcionPlanilla = d.cpla_DescripcionPlanilla

								   };
					encabezado = "Vacaciones";
					tipoReporte = "Vacaciones";
					break;

				case "SeptimoDia":
					ReportVarios = from d in db.V_ReportesVarios
								   where
								   d.hipa_FechaPago >= hipa_FechaInicio &&
								   d.hipa_FechaPago <= hipa_FechaFin &&
								   d.cpla_IdPlanilla == cpla_IdPlanilla
								   && d.hipa_TotalSeptimoDia > 0
								   select new ViewModelReportesVarios
								   {
									   emp_Id = d.emp_Id,
									   per_Nombres = d.per_Nombres,
									   per_Apellidos = d.per_Apellidos,
									   concepto = "Séptimo día",
									   monto = d.hipa_TotalSeptimoDia,
									   hipa_FechaInicio = d.hipa_FechaInicio,
									   hipa_FechaFin = d.hipa_FechaFin,
									   cpla_DescripcionPlanilla = d.cpla_DescripcionPlanilla

								   };
					encabezado = "SeptimoDia";
					tipoReporte = "SeptimoDia";
					break;

				case "AdelantosSueldo":
					ReportVarios = from d in db.V_ReportesVarios
								   where
								   d.hipa_FechaPago >= hipa_FechaInicio &&
								   d.hipa_FechaPago <= hipa_FechaFin &&
								   d.cpla_IdPlanilla == cpla_IdPlanilla
								   && d.hipa_AdelantoSueldo > 0
								   select new ViewModelReportesVarios
								   {
									   emp_Id = d.emp_Id,
									   per_Nombres = d.per_Nombres,
									   per_Apellidos = d.per_Apellidos,
									   concepto = "Adelantos de sueldo",
									   monto = d.hipa_AdelantoSueldo,
									   hipa_FechaInicio = d.hipa_FechaInicio,
									   hipa_FechaFin = d.hipa_FechaFin,
									   cpla_DescripcionPlanilla = d.cpla_DescripcionPlanilla

								   };
					encabezado = "Adelanto de sueldo";
					tipoReporte = "AdelantosSueldo";
					break;
			}



			ViewBag.ReportVarios = ReportVarios.ToList();
			ViewBag.Encabezado = encabezado;
			//parametros
			ViewBag.tipoReporte = tipoReporte;
			ViewBag.hipa_FechaInicio = hipa_FechaInicio;
			ViewBag.hipa_FechaFin = hipa_FechaFin;
			ViewBag.cpla_IdPlanilla = cpla_IdPlanilla;
			return View();
		}

		//vista del reporte
		public ActionResult ReportVarios(string reporte, DateTime hipa_FechaInicio, DateTime hipa_FechaFin, int cpla_IdPlanilla)
		{
			string encabezado = "ISR";
			var ReportVarios = from d in db.V_ReportesVarios
							   where
							   d.hipa_FechaPago >= hipa_FechaInicio &&
							   d.hipa_FechaPago <= hipa_FechaFin &&
							   d.cpla_IdPlanilla == cpla_IdPlanilla
							   && d.hipa_TotalISR > 0
							   select new ViewModelReportesVarios
							   {
								   emp_Id = d.emp_Id,
								   per_Nombres = d.per_Nombres,
								   per_Apellidos = d.per_Apellidos,
								   concepto = "ISR",
								   monto = d.hipa_TotalISR,
								   hipa_FechaInicio = d.hipa_FechaInicio,
								   hipa_FechaFin = d.hipa_FechaFin,
								   cpla_DescripcionPlanilla = d.cpla_DescripcionPlanilla

							   };

			switch (reporte)
			{
				case "AFP":
					ReportVarios = from d in db.V_ReportesVarios
								   where
								   d.hipa_FechaPago >= hipa_FechaInicio &&
								   d.hipa_FechaPago <= hipa_FechaFin &&
								   d.cpla_IdPlanilla == cpla_IdPlanilla
								   && d.hipa_AFP > 0
								   select new ViewModelReportesVarios
								   {
									   emp_Id = d.emp_Id,
									   per_Nombres = d.per_Nombres,
									   per_Apellidos = d.per_Apellidos,
									   concepto = "AFP",
									   monto = d.hipa_AFP,
									   hipa_FechaInicio = d.hipa_FechaInicio,
									   hipa_FechaFin = d.hipa_FechaFin,
									   cpla_DescripcionPlanilla = d.cpla_DescripcionPlanilla

								   };
					encabezado = "AFP";

					break;

				case "Comisiones":
					ReportVarios = from d in db.V_ReportesVarios
								   where
								   d.hipa_FechaPago >= hipa_FechaInicio &&
								   d.hipa_FechaPago <= hipa_FechaFin &&
								   d.cpla_IdPlanilla == cpla_IdPlanilla
								   && d.hipa_TotalComisiones > 0
								   select new ViewModelReportesVarios
								   {
									   emp_Id = d.emp_Id,
									   per_Nombres = d.per_Nombres,
									   per_Apellidos = d.per_Apellidos,
									   concepto = "Comisiones",
									   monto = d.hipa_TotalComisiones,
									   hipa_FechaInicio = d.hipa_FechaInicio,
									   hipa_FechaFin = d.hipa_FechaFin,
									   cpla_DescripcionPlanilla = d.cpla_DescripcionPlanilla

								   };
					encabezado = "Comisiones";

					break;

				case "HorasExtras":
					ReportVarios = from d in db.V_ReportesVarios
								   where
								   d.hipa_FechaPago >= hipa_FechaInicio &&
								   d.hipa_FechaPago <= hipa_FechaFin &&
								   d.cpla_IdPlanilla == cpla_IdPlanilla
								   && d.hipa_TotalHorasExtras > 0
								   select new ViewModelReportesVarios
								   {
									   emp_Id = d.emp_Id,
									   per_Nombres = d.per_Nombres,
									   per_Apellidos = d.per_Apellidos,
									   concepto = "Horas extras",
									   monto = d.hipa_TotalHorasExtras,
									   hipa_FechaInicio = d.hipa_FechaInicio,
									   hipa_FechaFin = d.hipa_FechaFin,
									   cpla_DescripcionPlanilla = d.cpla_DescripcionPlanilla

								   };
					encabezado = "HorasExtras";

					break;

				case "Vacaciones":
					ReportVarios = from d in db.V_ReportesVarios
								   where
								   d.hipa_FechaPago >= hipa_FechaInicio &&
								   d.hipa_FechaPago <= hipa_FechaFin &&
								   d.cpla_IdPlanilla == cpla_IdPlanilla
								   && d.hipa_TotalVacaciones > 0
								   select new ViewModelReportesVarios
								   {
									   emp_Id = d.emp_Id,
									   per_Nombres = d.per_Nombres,
									   per_Apellidos = d.per_Apellidos,
									   concepto = "Vacaciones",
									   monto = d.hipa_TotalVacaciones,
									   hipa_FechaInicio = d.hipa_FechaInicio,
									   hipa_FechaFin = d.hipa_FechaFin,
									   cpla_DescripcionPlanilla = d.cpla_DescripcionPlanilla

								   };
					encabezado = "Vacaciones";
					break;

				case "SeptimoDia":
					ReportVarios = from d in db.V_ReportesVarios
								   where
								   d.hipa_FechaPago >= hipa_FechaInicio &&
								   d.hipa_FechaPago <= hipa_FechaFin &&
								   d.cpla_IdPlanilla == cpla_IdPlanilla
								   && d.hipa_TotalSeptimoDia > 0
								   select new ViewModelReportesVarios
								   {
									   emp_Id = d.emp_Id,
									   per_Nombres = d.per_Nombres,
									   per_Apellidos = d.per_Apellidos,
									   concepto = "Séptimo día",
									   monto = d.hipa_TotalSeptimoDia,
									   hipa_FechaInicio = d.hipa_FechaInicio,
									   hipa_FechaFin = d.hipa_FechaFin,
									   cpla_DescripcionPlanilla = d.cpla_DescripcionPlanilla

								   };
					encabezado = "SeptimoDia";
					break;

				case "AdelantosSueldo":
					ReportVarios = from d in db.V_ReportesVarios
								   where
								   d.hipa_FechaPago >= hipa_FechaInicio &&
								   d.hipa_FechaPago <= hipa_FechaFin &&
								   d.cpla_IdPlanilla == cpla_IdPlanilla
								   && d.hipa_AdelantoSueldo > 0
								   select new ViewModelReportesVarios
								   {
									   emp_Id = d.emp_Id,
									   per_Nombres = d.per_Nombres,
									   per_Apellidos = d.per_Apellidos,
									   concepto = "Adelantos de sueldo",
									   monto = d.hipa_AdelantoSueldo,
									   hipa_FechaInicio = d.hipa_FechaInicio,
									   hipa_FechaFin = d.hipa_FechaFin,
									   cpla_DescripcionPlanilla = d.cpla_DescripcionPlanilla

								   };
					encabezado = "AdelantosSueldo";
					break;
			}



			ViewBag.ReportVarios = ReportVarios.ToList();
			ViewBag.Encabezado = encabezado;
			return View();
		}

		// acción para imprimir
		public ActionResult PrintReportesVarios(string nombrePdf, string reporte, DateTime hipa_FechaInicio, DateTime hipa_FechaFin, int cpla_IdPlanilla)
		{
			string NombrePdf = nombrePdf;

			return new ActionAsPdf("ReportVarios", new { reporte = reporte, hipa_FechaInicio = hipa_FechaInicio, hipa_FechaFin = hipa_FechaFin, cpla_IdPlanilla = cpla_IdPlanilla })
			{
				FileName = $"{NombrePdf}.pdf"
			};

		}

		#endregion


	}
}
