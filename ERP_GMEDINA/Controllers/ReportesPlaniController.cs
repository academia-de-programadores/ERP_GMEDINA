using ERP_GMEDINA.DataSet;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ERP_GMEDINA.Controllers
{
    public class ReportesPlaniController : Controller
    {
        // GET: ReportesPlani
        public ActionResult Index()
        {
            return View();
        }

		ReportesPlaniDS ds = new ReportesPlaniDS();
		public ActionResult DecimoTercerMesRPT()
		{

			ReportViewer reportViewer = new ReportViewer();
			reportViewer.ProcessingMode = ProcessingMode.Local;
			reportViewer.SizeToReportContent = false;
			reportViewer.Width = Unit.Pixel(1050);
			reportViewer.Height = Unit.Pixel(500);
			reportViewer.BackColor = System.Drawing.Color.White;

			var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;


			SqlConnection conx = new SqlConnection(connectionString);
			SqlDataAdapter adp = new SqlDataAdapter("SELECT * FROM rrhh.tbEmpleados", conx);

			adp.Fill(ds, ds.UDP_Plani_DecimoTercerMes_RPT.TableName);

			reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"ReportesPlani\DecimoTercerMesRPT.rdlc";
			reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesPlaniDS", ds.Tables[0]));


			ViewBag.ReportViewer = reportViewer;

			return View();
		}







	}
}