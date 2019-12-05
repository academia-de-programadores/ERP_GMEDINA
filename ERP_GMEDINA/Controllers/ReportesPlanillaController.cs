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
using ERP_GMEDINA.DataSets;

namespace ERP_GMEDINA.Controllers
{
    public class ReportesPlanillaController : Controller
    {
		private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
		
		#region Index Reportes
		// Index de todos los reportes
		public ActionResult Index()
        {
            return View();
        }
		#endregion


		#region Reporte Decimo Tercer Mes
		//-------------------------------------------------------------------------------------------------------------------------------
		//Reporte Decimo Tercer Mes - INICIO

		//Index 
		ReportesPlanillaDS ds = new ReportesPlanillaDS();
		public ActionResult DecimoTercerMesIndexRPT()
		{
			//Cargar DDL del modal (Tipo de planilla a seleccionar)
			ViewBag.TipoPlanillaDDL = new SelectList(db.tbCatalogoDePlanillas, "cpla_IdPlanilla", "cpla_DescripcionPlanilla");

			return View(db.V_DecimoTercerMes_RPT.ToList());
		}

		//Reporte con parametros
		public ActionResult DecimoTercerMesParametrosRPT(DateTime dtm_FechaPago, int cpla_DescripcionPlanilla, string id)
		{
			LocalReport lr = new LocalReport();
			string path = Path.Combine(Server.MapPath("~/ReportesPlanilla"), "DecimoTercerMesRPT.rdlc");
			if (System.IO.File.Exists(path))
			{
				lr.ReportPath = path;
			}
			else
			{
				return View("Index");
			}
			List<V_DecimoTercerMes_RPT> cm = new List<V_DecimoTercerMes_RPT>();

			cm = db.V_DecimoTercerMes_RPT.Where(x => dtm_FechaPago == x.dtm_FechaPago && cpla_DescripcionPlanilla == x.cpla_IdPlanilla).ToList();

			ReportDataSource rd = new ReportDataSource("ReportesPlanillaDS", cm);
			lr.DataSources.Add(rd);
			string reportType = id;
			string mimeType;
			string encoding;
			string fileNameExtension;
			string deviceInfo =

			"<DeviceInfo>" +
			"  <OutputFormat>" + id + "</OutputFormat>" +
			"  <PageWidth>11in</PageWidth>" +
			"  <PageHeight>8.5in</PageHeight>" +
			"  <MarginTop>0.1in</MarginTop>" +
			"  <MarginLeft>0.1in</MarginLeft>" +
			"  <MarginRight>0.1in</MarginRight>" +
			"  <MarginBottom>0.1in</MarginBottom>" +
			"</DeviceInfo>";

			Warning[] warnings;
			string[] streams;
			byte[] renderedBytes;

			renderedBytes = lr.Render(
				reportType,
				deviceInfo,
				out mimeType,
				out encoding,
				out fileNameExtension,
				out streams,
				out warnings);

			return File(renderedBytes, mimeType);
		}
		//Reporte Decimo Tercer Mes - FIN
		//-------------------------------------------------------------------------------------------------------------------------------
		#endregion


		#region Reporte Decimo Cuarto Mes
		//-------------------------------------------------------------------------------------------------------------------------------
		//Reporte Decimo Cuarto Mes - INICIO

		//Index 
		public ActionResult DecimoCuartoMesIndexRPT()
		{
			//Cargar DDL del modal (Tipo de planilla a seleccionar)

			ViewBag.TipoPlanillaDDL = new SelectList(db.tbCatalogoDePlanillas, "cpla_IdPlanilla", "cpla_DescripcionPlanilla");

			ReportViewer reportViewer = new ReportViewer();
			reportViewer.ProcessingMode = ProcessingMode.Local;
			reportViewer.SizeToReportContent = true;
			reportViewer.Width = Unit.Pixel(1050);
			reportViewer.Height = Unit.Pixel(450);
			reportViewer.BackColor = System.Drawing.Color.White;

			var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;


			SqlConnection conx = new SqlConnection(connectionString);
			SqlDataAdapter adp = new SqlDataAdapter("SELECT * FROM Plani.V_DecimoCuartoMes_RPT", conx);

			adp.Fill(ds, ds.V_DecimoCuartoMes_RPT.TableName);

			reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"ReportesPlanilla\DecimoCuartoMesRPT.rdlc";
			reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesPlanillaDS", ds.Tables[1]));


			ViewBag.ReportViewerDecimoCuartoMesRPT = reportViewer;
			return View();
		}

		//Reporte con parametros
		public ActionResult DecimoCuartoMesParametrosRPT(DateTime dcm_FechaPago, int cpla_DescripcionPlanilla, string id)
		{
			LocalReport lr = new LocalReport();
			string path = Path.Combine(Server.MapPath("~/ReportesPlanilla"), "DecimoCuartoMesRPT.rdlc");
			if (System.IO.File.Exists(path))
			{
				lr.ReportPath = path;
			}
			else
			{
				return View("Index");
			}
			List<V_DecimoCuartoMes_RPT> cm = new List<V_DecimoCuartoMes_RPT>();

			cm = db.V_DecimoCuartoMes_RPT.Where(x => dcm_FechaPago == x.dcm_FechaPago && cpla_DescripcionPlanilla == x.cpla_IdPlanilla).ToList();

			ReportDataSource rd = new ReportDataSource("ReportesPlanillaDS", cm);
			lr.DataSources.Add(rd);
			string reportType = id;
			string mimeType;
			string encoding;
			string fileNameExtension;
			string deviceInfo =

			"<DeviceInfo>" +
			"  <OutputFormat>" + id + "</OutputFormat>" +
			"  <PageWidth>11in</PageWidth>" +
			"  <PageHeight>8.5in</PageHeight>" +
			"  <MarginTop>0.1in</MarginTop>" +
			"  <MarginLeft>0.1in</MarginLeft>" +
			"  <MarginRight>0.1in</MarginRight>" +
			"  <MarginBottom>0.1in</MarginBottom>" +
			"</DeviceInfo>";

			Warning[] warnings;
			string[] streams;
			byte[] renderedBytes;

			renderedBytes = lr.Render(
				reportType,
				deviceInfo,
				out mimeType,
				out encoding,
				out fileNameExtension,
				out streams,
				out warnings);

			return File(renderedBytes, mimeType);
		}
        //Reporte Decimo Cuarto Mes - FIN
        //-------------------------------------------------------------------------------------------------------------------------------
        #endregion


        #region INFOP
        //-------------------------------------------------------------------------------------------------------------------------------
        //Reporte INFOP - INICIO

        //Index 
        public ActionResult INFOPIndexRPT()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)

            ViewBag.TipoPlanillaDDL = new SelectList(db.tbCatalogoDePlanillas, "cpla_IdPlanilla", "cpla_DescripcionPlanilla");

            return View(db.V_INFOP_RPT.ToList());
        }

        //Reporte con parametros
        public ActionResult INFOPParametrosRPT(DateTime hipa_FechaPago, int cpla_DescripcionPlanilla, string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/ReportesPlanilla"), "INFOPRPT.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<V_INFOP_RPT> cm = new List<V_INFOP_RPT>();

            cm = db.V_INFOP_RPT.Where(x => hipa_FechaPago == x.hipa_FechaPago && cpla_DescripcionPlanilla == x.cpla_IdPlanilla).ToList();

            ReportDataSource rd = new ReportDataSource("ReportesPlanillaDS", cm);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>11in</PageWidth>" +
            "  <PageHeight>8.5in</PageHeight>" +
            "  <MarginTop>0.1in</MarginTop>" +
            "  <MarginLeft>0.1in</MarginLeft>" +
            "  <MarginRight>0.1in</MarginRight>" +
            "  <MarginBottom>0.1in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }
        //Reporte INFOP - FIN
        //-------------------------------------------------------------------------------------------------------------------------------
        #endregion

        #region Instituciones Financieras
        //-------------------------------------------------------------------------------------------------------------------------------
        //Reporte Instituciones Financiras- INICIO

        //Index 
        public ActionResult InstitucionesFinancierasIndexRPT()
        { 
            //Cargar DDL del modal (Tipo de planilla a seleccionar)

            ViewBag.TipoPlanillaDDL = new SelectList(db.tbCatalogoDePlanillas, "cpla_IdPlanilla", "cpla_DescripcionPlanilla");

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Pixel(1050);
            reportViewer.Height = Unit.Pixel(500);
            reportViewer.BackColor = System.Drawing.Color.White;
            var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;


            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT * FROM Plani.V_InstitucionesFinancieras_RPT", conx);

            adp.Fill(ds, ds.V_InstitucionesFinancieras_RPT.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"ReportesPlanilla\InstitucionesFinancierasRPT.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesPlanillaDS", ds.Tables[3]));


            ViewBag.ReportViewerInstitucionesFinancierasRPT = reportViewer;
            return View();


        }

        //Reporte con parametros
        public ActionResult InstitucionesFinancierasParametrosRPT(DateTime hipa_FechaPago, int cpla_DescripcionPlanilla, string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/ReportesPlanilla"), "InstitucionesFinancierasRPT.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
         
            List<V_InstitucionesFinancieras_RPT> cm = new List<V_InstitucionesFinancieras_RPT>();

            cm = db.V_InstitucionesFinancieras_RPT.Where(x => hipa_FechaPago == x.hipa_FechaPago && cpla_DescripcionPlanilla == x.cpla_IdPlanilla).ToList();

            ReportDataSource rd = new ReportDataSource("ReportesPlanillaDS", cm);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>11in</PageWidth>" +
            "  <PageHeight>8.5in</PageHeight>" +
            "  <MarginTop>0.1in</MarginTop>" +
            "  <MarginLeft>0.1in</MarginLeft>" +
            "  <MarginRight>0.1in</MarginRight>" +
            "  <MarginBottom>0.1in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }
        //Reporte InstitucionesFinancieras - FIN
        //-------------------------------------------------------------------------------------------------------------------------------
        #endregion
    }

}
