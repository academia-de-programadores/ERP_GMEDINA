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


        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>

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

        ////////////////////////////////////////////////////////////////////////////////////////////////



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

        //////////////////////////////////////////////////////////////////////////////////////////


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

        ///////////////////////////////////////////////////////////////////////////////////
        public ActionResult Empleado()
        {

            ViewBag.FechaNacimiento = new SelectList(db.tbPersonas.Where(o => o.per_Estado == true), "per_Id", "per_FechaNacimiento");
            ViewBag.Habilidad = new SelectList(db.tbHabilidades.Where(o => o.habi_Estado == true), "habi_Id", "habi_Descripcion");
            ViewBag.Competencias = new SelectList(db.tbCompetencias.Where(o => o.comp_Estado == true), "comp_Id", "comp_Descripcion");
            return View();
        }

        [HttpPost]
        public ActionResult Empleado(int per_Id, int habi_Id, int comp_Id, DateTime Fecha_Nacimiento)
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
            command.CommandText = "SELECT * from [rrhh].[V_RPT_Empleado_Curriculum] where per_Id = @per_Id and habi_Id = @habi_Id  and comp_Id = @comp_Id and Fecha_Nacimiento = @Fecha_Nacimiento";
            command.Parameters.AddWithValue("@per_Id", SqlDbType.Int).Value = per_Id;
            command.Parameters.AddWithValue("@habi_Id", SqlDbType.Int).Value = habi_Id;
            command.Parameters.AddWithValue("@comp_Id", SqlDbType.Int).Value = comp_Id;
            command.Parameters.AddWithValue("@Fecha_Nacimiento", SqlDbType.DateTime).Value = Fecha_Nacimiento;

            SqlConnection conx = new SqlConnection(connectionString);
            command.Connection = conx;
            SqlDataAdapter adp = new SqlDataAdapter(command);
            adp.Fill(ds, ds.V_RPT_Empleado_Curriculum.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reporte\Empleado.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("REPORTE", ds.Tables["V_RPT_Empleado_Curriculum"]));
            conx.Close();

            ViewBag.ReportViewer = reportViewer;
            ViewBag.FechaNacimiento = new SelectList(db.tbPersonas.Where(o => o.per_Estado == true), "per_Id", "per_FechaNacimiento");
            ViewBag.Habilidad = new SelectList(db.tbHabilidades.Where(o => o.habi_Estado == true), "habi_Id", "habi_Descripcion");
            ViewBag.Competencias = new SelectList(db.tbCompetencias.Where(o => o.comp_Estado == true), "comp_Id", "comp_Descripcion");
            return View();
        }
    }
}


