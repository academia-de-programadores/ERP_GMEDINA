using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Rotativa;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.Configuration;
using ERP_GMEDINA.DataSets;

namespace ERP_GMEDINA.Controllers
{
    public class FechaPlanillaController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        ReportesPlanillaDS ds = new ReportesPlanillaDS();

        public ActionResult Index()
        {
            Session["HistorialDePago"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult ComprobanteEmpleadoEncabezado(int ID = 1, int anio = 2019)
        {
            Session["HistorialDePago"] = new ComprobantePagoSessionViewModel { Id = ID, Anio = anio };
            var V_Plani_EncabezadoHistorialPlanilla = db.V_Plani_EncabezadoHistorialPlanilla.Where(x => x.cpla_IdPlanilla == ID && (x.hipa_FechaPago.Value).Year == anio);

            foreach (var item in V_Plani_EncabezadoHistorialPlanilla)
            {
                var value = item;
            }

            return View(V_Plani_EncabezadoHistorialPlanilla);

        }

        [HttpGet]
        public JsonResult getFechaPlanilla()
        {
            var oV_Plani_FechaPlani = db.V_Plani_AnioPlanilla
            .Select(x => new FechaPlanilla { Anio = x.hipa_Anio }).ToList();


            object json = new { data = oV_Plani_FechaPlani };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getTipoPlanilla(int anio)
        {

            var tbHistorialPlanilla = db.V_Plani_DesplegableHistorialPlanilla
               .Where(x => x.fecha == anio)
               .Select(x => new FechaPlanillaViewModel { DescripcionPlanilla = x.cpla_DescripcionPlanilla, idPlanilla = x.cpla_IdPlanilla, anioPlanilla = (x.fecha ?? 0) });


            object json = new { data = tbHistorialPlanilla };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Report()
        {
            return View();
        }

        [Route("FechaPlanilla/{id}")]
        public ActionResult DetailsEmpleadoEncablezado(int id)
        {
            //Obtener de la sesion el id de la planilla y el año para hacer la filtracion
            ComprobantePagoSessionViewModel sessionComprobantePago = Session["HistorialDePago"] as ComprobantePagoSessionViewModel;

            #region Configuracion de Reportes
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = false;
            reportViewer.Width = Unit.Pixel(1050);
            reportViewer.Height = Unit.Pixel(500);
            reportViewer.BackColor = System.Drawing.Color.White;
            var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            #endregion

            if (sessionComprobantePago != null)
            {
                int anio = sessionComprobantePago.Anio;
                int planilla = sessionComprobantePago.Id;

                //comando para el dataAdapter
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM [Plani].[V_Plani_HistorialIngreso] WHERE emp_Id = @id AND cpla_IdPlanilla = @planilla AND YEAR(hipa_FechaPago) = @anio";

                command.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                command.Parameters.AddWithValue("@planilla", SqlDbType.Int).Value = planilla;
                command.Parameters.AddWithValue("@anio", SqlDbType.Int).Value = anio;
                command.Connection = conx;
                SqlDataAdapter adp = new SqlDataAdapter(command);
                //adp.Fill(ds, ds.V_Ingresos_RPT.TableName);
                adp.Fill(ds, "V_Plani_HistorialIngreso");
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"ReportesPlanilla\HistorialIngresosRPT.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesPlanillaDS", ds.Tables["V_Plani_HistorialIngreso"]));
                ViewBag.ReportViewer = reportViewer;
                conx.Close();
            }

            return View();


        }
    }
}