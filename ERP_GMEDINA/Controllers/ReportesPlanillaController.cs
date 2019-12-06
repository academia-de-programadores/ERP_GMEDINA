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

            return View(db.V_DecimoCuartoMes_RPT.ToList());
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

            return View(db.V_InstitucionesFinancieras_RPT.ToList());
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


		#region Reporte IHSS
		//-------------------------------------------------------------------------------------------------------------------------------
		//Reporte Decimo Tercer Mes - INICIO

		//Index 
		public ActionResult IHSSIndexRPT()
		{
			//Cargar DDL del modal (Tipo de planilla a seleccionar)
			ViewBag.TipoPlanillaDDL = new SelectList(db.tbCatalogoDePlanillas, "cpla_IdPlanilla", "cpla_DescripcionPlanilla");

			return View(db.V_IHSS_RPT.ToList());
		}

		//Reporte con parametros
		public ActionResult IHSSParametrosRPT(DateTime hipa_FechaPago, int cpla_DescripcionPlanilla, string id)
		{
			LocalReport lr = new LocalReport();
			string path = Path.Combine(Server.MapPath("~/ReportesPlanilla"), "IHSSRPT.rdlc");
			if (System.IO.File.Exists(path))
			{
				lr.ReportPath = path;
			}
			else
			{
				return View("Index");
			}
			List<V_IHSS_RPT> cm = new List<V_IHSS_RPT>();

			cm = db.V_IHSS_RPT.Where(x => hipa_FechaPago == x.hipa_FechaPago && cpla_DescripcionPlanilla == x.cpla_IdPlanilla).ToList();

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

		
		#region Reporte Impuesto Sobre la Renta(ISR)
		//-------------------------------------------------------------------------------------------------------------------------------
		//Reporte Decimo Cuarto Mes - INICIO

		//Index 
		public ActionResult ISRIndexRPT()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            ViewBag.TipoPlanillaDDL = new SelectList(db.tbCatalogoDePlanillas, "cpla_IdPlanilla", "cpla_DescripcionPlanilla");

            return View(db.V_ISR_RPT.ToList());
        }

        //Reporte con parametros
        public ActionResult ISRParametrosRPT(DateTime hipa_FechaPago, int cpla_DescripcionPlanilla, string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/ReportesPlanilla"), "ISRRPT.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<V_ISR_RPT> cm = new List<V_ISR_RPT>();

            cm = db.V_ISR_RPT.Where(x => hipa_FechaPago == x.hipa_FechaPago && cpla_DescripcionPlanilla == x.cpla_IdPlanilla).ToList();

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

		
		#region Reporte Liquidaciones
		//-------------------------------------------------------------------------------------------------------------------------------
		//Reporte Decimo Cuarto Mes - INICIO

		//Index 
		public ActionResult LiquidacionesIndexRPT()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            ViewBag.TipoPlanillaDDL = new SelectList(db.tbCatalogoDePlanillas, "cpla_IdPlanilla", "cpla_DescripcionPlanilla");

            return View(db.V_Liquidaciones_RPT.ToList());
        }

        //Reporte con parametros
        public ActionResult LiquidacionesParametrosRPT(DateTime hliq_fechaLiquidacion, int cpla_DescripcionPlanilla, string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/ReportesPlanilla"), "LiquidacionesRPT.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<V_Liquidaciones_RPT> cm = new List<V_Liquidaciones_RPT>();

            cm = db.V_Liquidaciones_RPT.Where(x => hliq_fechaLiquidacion == x.hliq_fechaLiquidacion && cpla_DescripcionPlanilla == x.cpla_IdPlanilla).ToList();

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


		#region Reporte RAP

		////-------------------------------------------------------------------------------------------------------------------------------
		////Reporte INFOP - INICIO

		////Index 
		public ActionResult RAPIndexRPT()
		{
			//Cargar DDL del modal (Tipo de planilla a seleccionar)

			ViewBag.TipoPlanillaDDL = new SelectList(db.tbCatalogoDePlanillas, "cpla_IdPlanilla", "cpla_DescripcionPlanilla");

			return View(db.V_RAP_RPT.ToList());
		}

		//Reporte con parametros
		public ActionResult RAPParametrosRPT(DateTime hipa_FechaPago, int cpla_DescripcionPlanilla, string id)
		{
			LocalReport lr = new LocalReport();
			string path = Path.Combine(Server.MapPath("~/ReportesPlanilla"), "RAPRPT.rdlc");
			if (System.IO.File.Exists(path))
			{
				lr.ReportPath = path;
			}
			else
			{
				return View("Index");
			}
			List<V_RAP_RPT> cm = new List<V_RAP_RPT>();

			cm = db.V_RAP_RPT.Where(x => hipa_FechaPago == x.hipa_FechaPago && cpla_DescripcionPlanilla == x.cpla_IdPlanilla).ToList();

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
		////Reporte RAP - FIN
		////-------------------------------------------------------------------------------------------------------------------------------


		#endregion


		#region Reporte AFP

		////-------------------------------------------------------------------------------------------------------------------------------
		////Reporte INFOP - INICIO

		////Index 
		public ActionResult AFPIndexRPT()
		{
			//Cargar DDL del modal (Tipo de planilla a seleccionar)

			ViewBag.TipoPlanillaDDL = new SelectList(db.tbCatalogoDePlanillas, "cpla_IdPlanilla", "cpla_DescripcionPlanilla");

			return View(db.V_AFP_RPT.ToList());
		}

		//Reporte con parametros
		public ActionResult AFPParametrosRPT(DateTime hipa_FechaPago, int cpla_DescripcionPlanilla, string id)
		{
			LocalReport lr = new LocalReport();
			string path = Path.Combine(Server.MapPath("~/ReportesPlanilla"), "AFPRPT.rdlc");
			if (System.IO.File.Exists(path))
			{
				lr.ReportPath = path;
			}
			else
			{
				return View("Index");
			}
			List<V_AFP_RPT> cm = new List<V_AFP_RPT>();

			cm = db.V_AFP_RPT.Where(x => hipa_FechaPago == x.hipa_FechaPago && cpla_DescripcionPlanilla == x.cpla_IdPlanilla).ToList();

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
		////Reporte AFP - FIN
		////-------------------------------------------------------------------------------------------------------------------------------


		#endregion


		#region Reporte General Totales

		//-------------------------------------------------------------------------------------------------------------------------------
		//Reporte General Totales - INICIO

		//Index 
		public ActionResult GeneralTotalesIndexRPT()
		{
			//Cargar DDL del modal (Tipo de planilla a seleccionar)
			ViewBag.TipoPlanillaDDL = new SelectList(db.tbCatalogoDePlanillas, "cpla_IdPlanilla", "cpla_DescripcionPlanilla");

			return View(db.V_GeneralTotales_RPT.ToList());
		}

		//Reporte con parametros
		public ActionResult GeneralTotalesParametrosRPT(DateTime hipa_FechaPago, int cpla_DescripcionPlanilla, string id)
		{
			LocalReport lr = new LocalReport();
			string path = Path.Combine(Server.MapPath("~/ReportesPlanilla"), "GeneralTotalesRPT.rdlc");
			if (System.IO.File.Exists(path))
			{
				lr.ReportPath = path;
			}
			else
			{
				return View("Index");
			}
			List<V_GeneralTotales_RPT> cm = new List<V_GeneralTotales_RPT>();

			cm = db.V_GeneralTotales_RPT.Where(x => hipa_FechaPago == x.hipa_FechaPago && cpla_DescripcionPlanilla == x.cpla_IdPlanilla).ToList();

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
		//Reporte General Totales Mes - FIN
		//-------------------------------------------------------------------------------------------------------------------------------

		#endregion


	}
}
