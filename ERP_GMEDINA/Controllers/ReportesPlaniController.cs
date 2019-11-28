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
		

	}
}