using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using ERP_GMEDINA.Reports;
using System.Configuration;
using System.Drawing;

namespace ERP_GMEDINA.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }


		ReportesDS ds = new ReportesDS();
		public ActionResult EmpleadosRPT()
		{

			ReportViewer reportViewer = new ReportViewer();
			reportViewer.ProcessingMode = ProcessingMode.Local;
			reportViewer.SizeToReportContent = false;
			reportViewer.Width =  Unit.Pixel(1050);
			reportViewer.Height = Unit.Pixel(500);
			reportViewer.BackColor = System.Drawing.Color.White;	

			var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;


			SqlConnection conx = new SqlConnection(connectionString);
			SqlDataAdapter adp = new SqlDataAdapter("SELECT * FROM rrhh.tbEmpleados", conx);

			adp.Fill(ds, ds.tbEmpleados.TableName);

			reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\EmpleadosRPT.rdlc";
			reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesDS", ds.Tables[0]));


			ViewBag.ReportViewer = reportViewer;

			return View();
		}





	}
}