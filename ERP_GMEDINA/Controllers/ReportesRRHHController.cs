﻿using System;
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
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class ReportesRRHHController : Controller
    {
        // GET: ReportesRRHH
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        Models.Helpers Function = new Models.Helpers();
        ReportesRRHH ds = new ReportesRRHH();

        public ActionResult HistorialIncapacidadesRPT()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            //ViewBag.Turno2 = new SelectList(db.tbHistorialHorasTrabajadas.Where(o => o.htra_Estado == true), "htra_Id");
            try
            {
                ViewBag.incapacidades = new SelectList(db.tbTipoIncapacidades.Where(o => o.ticn_Estado == true).OrderBy(x => x.ticn_Descripcion), "ticn_Id", "ticn_Descripcion");
                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        [HttpPost]
        [SessionManager("ReportesRRHH/HistorialIncapacidadesRPT")]
        public ActionResult HistorialIncapacidadesRPT(int? ticn_Id, DateTime? FechaInicio, DateTime? FechaFin)
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
            command.CommandText = "select * from rrhh.V_RPT_HistorialIncapacidad where 1 = 1";

            if (ticn_Id != null)
            {
                command.CommandText += " and ticn_Id = @ticn_Id ";
                command.Parameters.AddWithValue("@ticn_Id", SqlDbType.Int).Value = ticn_Id;
            }

            if (FechaFin != null && FechaInicio != null)
            {
                command.CommandText += " and FechaInicio between @FechaInicio and @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = FechaFin;
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = FechaInicio;
            }
            else if (FechaFin != null && FechaInicio == null)
            {
                command.CommandText += " and FechaInicio <= @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = FechaFin;
            }
            else if (FechaInicio != null && FechaFin == null)
            {
                command.CommandText += " and FechaInicio >= @FechaInicio ";
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = FechaInicio;
            }
            /**/

            try
            {
                SqlConnection conx = new SqlConnection(connectionString);
                command.Connection = conx;
                SqlDataAdapter adp = new SqlDataAdapter(command);
                adp.Fill(ds, ds.V_RPT_HistorialIncapacidad.TableName);

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
            catch (Exception ex)
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        public ActionResult HorasTrabajadas()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            //ViewBag.Turno2 = new SelectList(db.tbHistorialHorasTrabajadas.Where(o => o.htra_Estado == true), "htra_Id");
            try
            {
                ViewBag.Turno = new SelectList(db.tbTipoHoras.Where(o => o.tiho_Estado == true).OrderBy(x => x.tiho_Descripcion), "tiho_Id", "tiho_Descripcion");
                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        [HttpPost]
        [SessionManager("ReportesRRHH/HorasTrabajadas")]
        public ActionResult HorasTrabajadas(int? tiho_Id, DateTime? Fecha, DateTime? FechaFin)

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
            command.CommandText = "SELECT * from rrhh.V_RPT_HorasTrabajadas where 1=1 ";
            if (tiho_Id != null)
            {
                command.CommandText += " and tiho_Id = @tiho_Id ";
                command.Parameters.AddWithValue("@tiho_Id", SqlDbType.Int).Value = tiho_Id;
            }
            if (FechaFin != null && Fecha != null)
            {
                command.CommandText += " and Fecha between @FechaInicio and @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = FechaFin;
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = Fecha;
            }
            else if (FechaFin != null && Fecha == null)
            {
                command.CommandText += " and Fecha <= @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = FechaFin;
            }
            else if (Fecha != null && FechaFin == null)
            {
                command.CommandText += " and Fecha >= @FechaInicio ";
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = Fecha;
            }
            try
            {
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
            catch (Exception ex)
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        public ActionResult HistorialContratacionesRPT()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            try
            {
                ViewBag.Cargo = new SelectList(db.tbCargos.Where(o => o.car_Estado == true).OrderBy(x => x.car_Descripcion), "car_Id", "car_Descripcion");
                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        public ActionResult HistorialCargosRPT()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            try
            {
                ViewBag.empleados = new SelectList(db.V_Empleados.Where(o => o.emp_Estado == true).OrderBy(x => x.per_NombreCompleto), "emp_Id", "per_NombreCompleto");
                ViewBag.Cargo = new SelectList(db.tbCargos.Where(o => o.car_Estado == true).OrderBy(x => x.car_Descripcion), "car_Id", "car_Descripcion");
                //Codigo que hace que todo truene
                //int Cero = 0;
                //int Uno = 1;
                //int UnoEntreCero = Uno / Cero;
                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }

        }
        [HttpPost]
        [SessionManager("ReportesRRHH/HistorialCargosRPT")]
        public ActionResult HistorialCargosRPT(int? car_Id, int? emp_Id, DateTime? Fecha, DateTime? FechaFin)
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
            command.CommandText = "select * from rrhh.V_RPT_HistorialCargos where 1 = 1 ";
            if (emp_Id != null)
            {
                command.CommandText += "and emp_Id = @emp_Id";
                command.Parameters.AddWithValue("@emp_Id", SqlDbType.Int).Value = emp_Id;
            }
            if (car_Id != null)
            {
                command.CommandText += " and car_Id = @car_Id";
                command.Parameters.AddWithValue("@car_Id", SqlDbType.Int).Value = car_Id;
            }
            if (Fecha != null && FechaFin != null)
            {
                command.CommandText += " and Fecha between @Fecha and @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = FechaFin;
                command.Parameters.AddWithValue("@Fecha", SqlDbType.DateTime).Value = Fecha;
            }
            else if (FechaFin != null && Fecha == null)
            {
                command.CommandText += " and Fecha <= @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = FechaFin;
            }
            else if (Fecha != null && FechaFin == null)
            {
                command.CommandText += " and Fecha >= @Fecha ";
                command.Parameters.AddWithValue("@Fecha", SqlDbType.DateTime).Value = Fecha;
            }

            try
            {
                SqlConnection conx = new SqlConnection(connectionString);

                command.Connection = conx;
                SqlDataAdapter adp = new SqlDataAdapter(command);
                adp.Fill(ds, ds.V_RPT_HistorialCargos.TableName);
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\HistorialCargosRPT.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_HistorialCargos"]));
                conx.Close();

                ViewBag.ReportViewer = reportViewer;

                ViewBag.empleados = new SelectList(db.V_Empleados.Where(o => o.emp_Estado == true), "emp_Id", "per_NombreCompleto");
                ViewBag.Cargo = new SelectList(db.tbCargos.Where(o => o.car_Estado == true), "car_Id", "car_Descripcion");

                return View();
            }
            catch (Exception ex)
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        [HttpPost]
        [SessionManager("ReportesRRHH/HistorialContratacionesRPT")]
        public ActionResult HistorialContratacionesRPT(int? car_Id, DateTime? FechaContratacion, DateTime? FechaFin)
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
            command.CommandText = "SELECT * FROM rrhh.V_RPT_HistorialContrataciones where 1=1";

            if (car_Id != null)
            {
                command.CommandText += " and car_Id = @car_Id";
                command.Parameters.AddWithValue("@car_Id", SqlDbType.Int).Value = car_Id;
            }
            if (FechaFin != null && FechaContratacion != null)
            {
                command.CommandText += " and FechaContratacion between @FechaInicio and @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = FechaFin;
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = FechaContratacion;
            }
            else if (FechaFin != null && FechaContratacion == null)
            {
                command.CommandText += " and FechaContratacion <= @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = FechaFin;
            }
            else if (FechaContratacion != null && FechaFin == null)
            {
                command.CommandText += " and FechaContratacion >= @FechaInicio ";
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = FechaContratacion;
            }
            try
            {
                SqlConnection conx = new SqlConnection(connectionString);
                command.Connection = conx;
                SqlDataAdapter adp = new SqlDataAdapter(command);
                adp.Fill(ds, ds.V_RPT_HistorialContrataciones.TableName);

                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\HistorialContratacionesRPT.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_HistorialContrataciones"]));
                conx.Close();

                ViewBag.ReportViewer = reportViewer;

                ViewBag.Cargo = new SelectList(db.tbCargos.Where(o => o.car_Estado == true), "car_Id", "car_Descripcion");

                return View();
            }
            catch (Exception ex)
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        public ActionResult HistorialSalidasRPT()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            //ViewBag.Turno2 = new SelectList(db.tbHistorialHorasTrabajadas.Where(o => o.htra_Estado == true), "htra_Id");
            try
            {
                ViewBag.TipoSalida = new SelectList(db.tbTipoSalidas.Where(o => o.tsal_Estado == true).OrderBy(x => x.tsal_Descripcion), "tsal_Id", "tsal_Descripcion");
                ViewBag.Empleados = new SelectList(db.V_EmpleadoIncapacidades.Where(o => o.emp_Estado == true).OrderBy(x => x.emp_NombreCompleto), "emp_Id", "emp_NombreCompleto");
                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        [HttpPost]
        [SessionManager("ReportesRRHH/HistorialSalidasRPT")]
        public ActionResult HistorialSalidasRPT(int? tsal_Id, DateTime? FechaSalida, DateTime? fechaFin)
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

            command.CommandText = "select * from rrhh.V_RPT_HistorialSalidas where 1 = 1 ";

            if (tsal_Id != null)
            {
                command.CommandText += " and tsal_Id = @tsal_Id";
                command.Parameters.AddWithValue("@tsal_Id", SqlDbType.Int).Value = tsal_Id;
            }
            if (FechaSalida != null && fechaFin != null)
            {
                command.CommandText += " and FechaSalida between @FechaSalida and @FechaFin ";
                command.Parameters.AddWithValue("@fechaFin", SqlDbType.DateTime).Value = fechaFin;
                command.Parameters.AddWithValue("@FechaSalida", SqlDbType.DateTime).Value = FechaSalida;
            }
            else if (fechaFin != null && FechaSalida == null)
            {
                command.CommandText += " and FechaSalida <= @FechaFin ";
                command.Parameters.AddWithValue("@fechaFin", SqlDbType.DateTime).Value = fechaFin;
            }
            else if (FechaSalida != null && fechaFin == null)
            {
                command.CommandText += " and FechaSalida >= @FechaSalida ";
                command.Parameters.AddWithValue("@FechaSalida", SqlDbType.DateTime).Value = FechaSalida;
            }

            try
            {
                SqlConnection conx = new SqlConnection(connectionString);
                command.Connection = conx;
                SqlDataAdapter adp = new SqlDataAdapter(command);
                adp.Fill(ds, ds.V_RPT_HistorialSalidas.TableName);

                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\HistorialSalidasRPT.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_HistorialSalidas"]));
                conx.Close();


                ViewBag.ReportViewer = reportViewer;
                ViewBag.TipoSalida = new SelectList(db.tbTipoSalidas.Where(o => o.tsal_Estado == true), "tsal_Id", "tsal_Descripcion");

                ViewBag.TipoSalida = new SelectList(db.tbTipoSalidas.Where(o => o.tsal_Estado == true), "tsal_Id", "tsal_Descripcion");
                ////ViewBag.Titulos = db.tbHistorialSalidas.Where(x => x.hsal_Id == hsal_Id).Select(x => x.).FirstOrDefault();
                //////Cargar DDL del modal (Tipo de planilla a seleccionar)
                //ViewBag.Tiposalida = new SelectList(db.tbTipoSalidas.Where(o => o.tsal_Estado == true), "tsal_Id", "tsal_Descripcion");

                //ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        public ActionResult HistorialVacacionesRPT()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            //ViewBag.Turno2 = new SelectList(db.tbHistorialHorasTrabajadas.Where(o => o.htra_Estado == true), "htra_Id");

            try
            {
                ViewBag.Empleados = new SelectList(db.V_Empleados.Where(o => o.emp_Estado == true).OrderBy(x => x.per_NombreCompleto), "emp_Id", "Per_NombreCompleto");
                ViewBag.Anios = new SelectList(db.tbHistorialVacaciones.Where(o => o.hvac_Estado == true).OrderBy(x => x.hvac_AnioVacaciones), "hvac_AnioVacaciones", "hvac_AnioVacaciones");
                ViewBag.Mes = new SelectList(db.tbHistorialVacaciones.Where(o => o.hvac_Estado == true).OrderBy(x => x.hvac_MesVacaciones), "hvac_MesVacaciones", "hvac_MesVacaciones");

                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        [HttpPost]
        [SessionManager("ReportesRRHH/HistorialVacacionesRPT")]
        public ActionResult HistorialVacacionesRPT(int? emp_Id, DateTime? hvac_FechaInicio, DateTime? hvac_FechaFin /*,int hvac_AnioVacaciones, int hvac_MesVacaciones*/)
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
            command.CommandText = "select * from rrhh.V_RPT_HistorialVacaciones where 1 = 1 ";
            if (emp_Id != null)
            {
                command.CommandText += " and emp_Id = @emp_Id";
                command.Parameters.AddWithValue("@emp_Id", SqlDbType.Int).Value = emp_Id;
            }
            if (hvac_FechaInicio != null && hvac_FechaFin != null)
            {
                command.CommandText += " and hvac_FechaInicio between @hvac_FechaInicio and @FechaFin ";
                command.Parameters.AddWithValue("@hvac_FechaFin", SqlDbType.DateTime).Value = hvac_FechaFin;
                command.Parameters.AddWithValue("@hvac_FechaInicio", SqlDbType.DateTime).Value = hvac_FechaInicio;
            }
            else if (hvac_FechaFin != null && hvac_FechaInicio == null)
            {
                command.CommandText += " and hvac_FechaInicio <= @FechaFin ";
                command.Parameters.AddWithValue("@hvac_FechaFin", SqlDbType.DateTime).Value = hvac_FechaFin;
            }
            else if (hvac_FechaInicio != null && hvac_FechaFin == null)
            {
                command.CommandText += " and hvac_FechaInicio >= @hvac_FechaInicio ";
                command.Parameters.AddWithValue("@hvac_FechaInicio", SqlDbType.DateTime).Value = hvac_FechaInicio;
            }

            try
            {
                SqlConnection conx = new SqlConnection(connectionString);
                command.Connection = conx;
                SqlDataAdapter adp = new SqlDataAdapter(command);
                adp.Fill(ds, ds.V_RPT_HistorialVacaciones.TableName);

                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\HistorialVacacionesRPT.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_HistorialVacaciones"]));
                conx.Close();


                ViewBag.ReportViewer = reportViewer;
                ViewBag.Empleados = new SelectList(db.V_Empleados.Where(o => o.emp_Estado == true), "emp_Id", "Per_NombreCompleto");

                ViewBag.Anios = new SelectList(db.tbHistorialVacaciones.Where(o => o.hvac_Estado == true), "hvac_AnioVacaciones", "hvac_AnioVacaciones");

                ViewBag.Mes = new SelectList(db.tbHistorialVacaciones.Where(o => o.hvac_Estado == true), "hvac_MesVacaciones", "hvac_MesVacaciones");
                ////ViewBag.Titulos = db.tbHistorialSalidas.Where(x => x.hsal_Id == hsal_Id).Select(x => x.).FirstOrDefault();
                //////Cargar DDL del modal (Tipo de planilla a seleccionar)
                //ViewBag.Tiposalida = new SelectList(db.tbTipoSalidas.Where(o => o.tsal_Estado == true), "tsal_Id", "tsal_Descripcion");

                //ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        public ActionResult SueldosRPT()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            //ViewBag.Turno2 = new SelectList(db.tbHistorialHorasTrabajadas.Where(o => o.htra_Estado == true), "htra_Id");
            //ViewBag.Sueldos = new SelectList(db.tbSueldos.Where(o => o.sue_Estado == true), "sue_Id", "sue_Cantidad");
            try
            {
                ViewBag.Empleados = new SelectList(db.V_Empleados.Where(o => o.emp_Estado == true), "emp_Id", "per_NombreCompleto");
                ViewBag.Cargos = new SelectList(db.tbCargos.Where(o => o.car_Estado == true), "car_Id", "car_Descripcion");
                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        [HttpPost]
        [SessionManager("ReportesRRHH/SueldosRPT")]
        public ActionResult SueldosRPT(int? emp_Id, DateTime? fechaInicio, DateTime? fechafin, int? Car_Id)
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
            command.CommandText = "select * from rrhh.V_RPT_HistorialSueldos where 1 = 1 ";
            if (emp_Id != null)
            {
                command.CommandText += "and emp_Id = @emp_Id";
                command.Parameters.AddWithValue("@emp_Id", SqlDbType.Int).Value = emp_Id;
            }
            if (Car_Id != null)
            {
                command.CommandText += " and Car_Id = @Car_Id";
                command.Parameters.AddWithValue("@Car_Id", SqlDbType.Int).Value = Car_Id;
            }
            if (fechaInicio != null && fechafin != null)
            {
                command.CommandText += " and fechaInicio between @fechaInicio and @fechafin ";
                command.Parameters.AddWithValue("@fechafin", SqlDbType.DateTime).Value = fechafin;
                command.Parameters.AddWithValue("@fechaInicio", SqlDbType.DateTime).Value = fechaInicio;
            }
            else if (fechafin != null && fechaInicio == null)
            {
                command.CommandText += " and fechainicio <= @fechafin ";
                command.Parameters.AddWithValue("@fechafin", SqlDbType.DateTime).Value = fechafin;
            }
            else if (fechaInicio != null && fechafin == null)
            {
                command.CommandText += " and fechaInicio >= @FechaInicio ";
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio;
            }
            try
            {
                SqlConnection conx = new SqlConnection(connectionString);
                command.Connection = conx;
                SqlDataAdapter adp = new SqlDataAdapter(command);
                adp.Fill(ds, ds.V_RPT_HistorialSueldos.TableName);

                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\SueldosRPT.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_HistorialSueldos"]));
                conx.Close();

                ViewBag.ReportViewer = reportViewer;
                ViewBag.Empleados = new SelectList(db.V_Empleados.Where(o => o.emp_Estado == true), "emp_Id", "per_NombreCompleto");
                ViewBag.Cargos = new SelectList(db.tbCargos.Where(o => o.car_Estado == true), "car_Id", "car_Descripcion");
                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        public ActionResult Permisos()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            //ViewBag.Turno2 = new SelectList(db.tbHistorialHorasTrabajadas.Where(o => o.htra_Estado == true), "htra_Id");
            try
            {
                ViewBag.Permiso = new SelectList(db.tbTipoPermisos.Where(o => o.tper_Estado == true).OrderBy(x => x.tper_Descripcion), "tper_Id", "tper_Descripcion");
                ViewBag.Empleados = new SelectList(db.V_Empleados.Where(o => o.emp_Estado == true).OrderBy(x => x.per_NombreCompleto), "emp_Id", "per_NombreCompleto");
                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        [HttpPost]
        [SessionManager("ReportesRRHH/Permisos")]
        public ActionResult Permisos(int? emp_Id, DateTime? FechaInicio, DateTime? FechaFin, int? tper_Id)
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
            command.CommandText = "select * from rrhh.V_RPT_HistorialPermisos where 1 = 1 ";
            if (emp_Id != null)
            {
                command.CommandText += "and emp_Id = @emp_Id";
                command.Parameters.AddWithValue("@emp_Id", SqlDbType.Int).Value = emp_Id;
            }
            if (tper_Id != null)
            {
                command.CommandText += " and tper_Id = @tper_Id";
                command.Parameters.AddWithValue("@tper_Id", SqlDbType.Int).Value = tper_Id;
            }
            if (FechaInicio != null && FechaFin != null)
            {
                command.CommandText += " and FechaInicio between @FechaInicio and @FechaInicio ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = FechaFin;
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = FechaInicio;
            }
            else if (FechaFin != null && FechaInicio == null)
            {
                command.CommandText += " and fechainicio <= @fechafin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = FechaFin;
            }
            else if (FechaInicio != null && FechaFin == null)
            {
                command.CommandText += " and FechaInicio >= @FechaInicio ";
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = FechaInicio;
            }

            try
            {
                SqlConnection conx = new SqlConnection(connectionString);
                command.Connection = conx;
                SqlDataAdapter adp = new SqlDataAdapter(command);
                adp.Fill(ds, ds.V_RPT_HistorialPermisos.TableName);

                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\PermisosRPT.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesPermisosDS", ds.Tables["V_RPT_HistorialPermisos"]));
                conx.Close();

                ViewBag.ReportViewer = reportViewer;
                //ViewBag.TipoHora = db.tbHistorialHorasTrabajadas.Where(x => x.htra_Id == htra_Id);

                //ViewBag.Titulos = db.tbTipoHoras.Where(x => x.tiho_Id == tiho_Id).Select(x => x.tiho_Descripcion).FirstOrDefault();
                ////Cargar DDL del modal (Tipo de planilla a seleccionar)
                //ViewBag.Turno = new SelectList(db.tbTipoHoras.Where(o => o.tiho_Estado == true), "tiho_Id", "tiho_Descripcion");
                //ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
                ViewBag.Permiso = new SelectList(db.tbTipoPermisos.Where(o => o.tper_Estado == true), "tper_Id", "tper_Descripcion");
                ViewBag.Empleados = new SelectList(db.V_Empleados.Where(o => o.emp_Estado == true), "emp_Id", "per_NombreCompleto");
                return View();
            }
            catch (Exception ex)
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        public ActionResult FaseSeleccionRPT()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            try
            {
                ViewBag.FaseReclutamiento = new SelectList(db.tbFasesReclutamiento.Where(o => o.fare_Estado == true).OrderBy(x => x.fare_Descripcion), "fare_Id", "fare_Descripcion");
                // ViewBag.Requisiciones = new SelectList(db.tbRequisiciones.Where(o => o.req_Estado == true), "req_Id", "req_Descripcion");
                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        [HttpPost]
        [SessionManager("ReportesRRHH/FaseSeleccionRPT")]
        public ActionResult FaseSeleccionRPT(int? fare_Id, DateTime? Fecha, DateTime? FechaFin)
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
            command.CommandText = "SELECT * FROM rrhh.V_RPT_FaseSeleccion  where 1 = 1";
            if (fare_Id != null)
            {
                command.CommandText += " and fare_Id = @fare_Id ";
                command.Parameters.AddWithValue("@fare_Id", SqlDbType.Int).Value = fare_Id;
            }
            if (FechaFin != null && Fecha != null)
            {
                command.CommandText += " and Fecha between @FechaInicio and @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = FechaFin;
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = Fecha;
            }
            else if (FechaFin != null && Fecha == null)
            {
                command.CommandText += " and Fecha <= @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = FechaFin;
            }
            else if (Fecha != null && FechaFin == null)
            {
                command.CommandText += " and Fecha >= @FechaInicio ";
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = Fecha;
            }

            try
            {
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
            catch (Exception ex)
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }

        public ActionResult Requisicion()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            try
            {
                ViewBag.Requisicion = new SelectList(db.tbRequisiciones.Where(o => o.req_Estado == true), "req_Id", "req_Descripcion");
                //ViewBag.Persona = new SelectList(db.tbPersonas.Where(o => o.per_Estado == true), "per_Id", "req_Descripcion");
                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        [HttpPost]
        [SessionManager("ReportesRRHH/Requisicion")]
        public ActionResult Requisicion(int? req_Id, DateTime? fechaInicio, DateTime? Fechafin)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = false;
            reportViewer.Width = Unit.Pixel(1050);
            reportViewer.Height = Unit.Pixel(500);
            reportViewer.BackColor = System.Drawing.Color.White;
            var connectionString = ConfigurationManager.ConnectionStrings["ERP_GMEDINAConnectionString"].ConnectionString;


            //comando para el dataAdapter
            //comando para el dataAdapter
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM rrhh.V_RPT_Requisiciones where 1 = 1 ";
            if (req_Id != null)
            {
                command.CommandText += " and req_Id = @req_Id";
                command.Parameters.AddWithValue("@req_Id", SqlDbType.Int).Value = req_Id;
            }
            if (Fechafin != null && fechaInicio != null)
            {
                command.CommandText += " and req_FechaContratacion between @FechaInicio and @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = Fechafin;
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio;
            }
            else if (Fechafin != null && fechaInicio == null)
            {
                command.CommandText += " and req_FechaContratacion <= @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = Fechafin;
            }
            else if (fechaInicio != null && Fechafin == null)
            {
                command.CommandText += " and req_FechaContratacion >= @FechaInicio ";
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio;
            }

            try
            {
                SqlConnection conx = new SqlConnection(connectionString);
                SqlDataAdapter adp = new SqlDataAdapter(command);
                command.Connection = conx;
                adp.Fill(ds, ds.V_RPT_Requisiciones.TableName);

                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\RequisicionesRPT.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_Requisiciones"]));
                conx.Close();
                ViewBag.ReportViewer = reportViewer;
                ViewBag.Requisicion = new SelectList(db.tbRequisiciones.Where(o => o.req_Estado == true), "req_Id", "req_Descripcion");
                return View();
            }
            catch (Exception ex)
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }

        public ActionResult Empleado()
        {
            try
            {
                //ViewBag.Habilidad = new SelectList(db.tbHabilidades.Where(o => o.habi_Estado == true), "habi_Id", "habi_Descripcion");
                //ViewBag.Competencias = new SelectList(db.tbCompetencias.Where(o => o.comp_Estado == true), "comp_Id", "comp_Descripcion");

                ViewBag.Jornadas = new SelectList(db.tbJornadas.Where(o => o.jor_Estado == true).OrderBy(x => x.jor_Descripcion), "jor_Id", "jor_Descripcion");
                ViewBag.Departamentos = new SelectList(db.tbDepartamentos.Where(o => o.depto_Estado == true).OrderBy(x => x.depto_Descripcion), "depto_Id", "depto_Descripcion");
                ViewBag.areas = new SelectList(db.tbAreas.Where(o => o.area_Estado == true).OrderBy(x => x.area_Descripcion), "area_Id", "area_Descripcion");
                ViewBag.Sucursales = new SelectList(db.tbSucursales.Where(o => o.suc_Estado == true).OrderBy(x => x.suc_Descripcion), "suc_Id", "suc_Descripcion");
                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        [HttpPost]
        [SessionManager("ReportesRRHH/Empleado")]
        public ActionResult Empleado(int? jor_Id, int? depto_Id, int? area_Id, int? suc_Id, DateTime? fechaInicio, DateTime? Fechafin)
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
            command.CommandText = "SELECT * FROM rrhh.V_RPT_EmpleadoCurriculum where 1 = 1 ";
            if (jor_Id != null)
            {
                command.CommandText += " and jor_Id = @jor_Id";
                command.Parameters.AddWithValue("@jor_Id", SqlDbType.Date).Value = jor_Id;
            }
            if (depto_Id != null)
            {
                command.CommandText += " and depto_Id = @depto_Id";
                command.Parameters.AddWithValue("@depto_Id", SqlDbType.Int).Value = depto_Id;
            }
            if (area_Id != null)
            {
                command.CommandText += " and area_Id = @area_Id";
                command.Parameters.AddWithValue("@area_Id", SqlDbType.Int).Value = area_Id;
            }
            if (suc_Id != null)
            {
                command.CommandText += " and suc_Id = @suc_Id";
                command.Parameters.AddWithValue("@suc_Id", SqlDbType.Int).Value = suc_Id;
            }
            if (Fechafin != null && fechaInicio != null)
            {
                command.CommandText += " and fechaInicio between @FechaInicio and @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = Fechafin;
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio;
            }
            else if (Fechafin != null && fechaInicio == null)
            {
                command.CommandText += " and fechaInicio <= @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = Fechafin;
            }
            else if (fechaInicio != null && Fechafin == null)
            {
                command.CommandText += " and fechaInicio >= @FechaInicio ";
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio;
            }
            try
            {
                SqlConnection conx = new SqlConnection(connectionString);
                command.Connection = conx;
                SqlDataAdapter adp = new SqlDataAdapter(command);
                adp.Fill(ds, ds.V_RPT_EmpleadoCurriculum.TableName);

                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\EmpleadosCurriculumRPT.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_EmpleadoCurriculum"]));
                conx.Close();

                ViewBag.ReportViewer = reportViewer;

                // ViewBag.Titulo1 = db.tbCompetencias.Where(x => x.Comp_Id == Comp_Id).Select(x => x.comp_Descripcion).FirstOrDefault();
                ViewBag.areas = new SelectList(db.tbAreas.Where(o => o.area_Estado == true), "area_Id", "area_Descripcion");
                ViewBag.Departamentos = new SelectList(db.tbDepartamentos.Where(o => o.depto_Estado == true), "depto_Id", "depto_Descripcion");
                ViewBag.Sucursales = new SelectList(db.tbSucursales.Where(o => o.suc_Estado == true), "suc_Id", "suc_Descripcion");
                ViewBag.Jornadas = new SelectList(db.tbJornadas.Where(o => o.jor_Estado == true), "jor_Id", "jor_Descripcion");
                return View();
            }
            catch (Exception ex)
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }


        //mios
        public ActionResult HistorialAmonestacionesRPT()
        {
            try
            {
                ViewBag.TipoAmonesta = new SelectList(db.tbTipoAmonestaciones.Where(o => o.tamo_Estado == true).OrderBy(x => x.tamo_Descripcion), "tamo_Id", "tamo_Descripcion");
                ViewBag.EmpleadoAMON = new SelectList(db.V_RPT_HistorialAmonestaciones_Empleados.OrderBy(x => x.nombre), "per_Identidad", "nombre");
                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        public ActionResult HistorialAudienciaDescargoRPT()
        {
            try
            {
                ViewBag.EmpleadoAUDE = new SelectList(db.V_RPT_HistorialAudienciaDescargo_empleados, "per_Identidad", "nombre");
                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }

        [HttpPost]
        [SessionManager("ReportesRRHH/HistorialAmonestacionesRPT")]
        public ActionResult HistorialAmonestacionesRPT(int? tamo_Id, string Identidad, DateTime? Fecha, DateTime? FechaAnterior)
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
            if (tamo_Id == null)
            {
                command.CommandText = "SELECT * from rrhh.V_RPT_HistorialAmonestaciones WHERE Identidad like '%'+ @Identidad + '%' ";
                command.Parameters.AddWithValue("@Identidad", SqlDbType.NVarChar).Value = Identidad;
            }
            else
            {
                command.CommandText = "SELECT * from rrhh.V_RPT_HistorialAmonestaciones WHERE  tamo_Id = @tamo_Id and Identidad like '%'+ @Identidad + '%' ";
                command.Parameters.AddWithValue("@Identidad", SqlDbType.NVarChar).Value = Identidad;
                command.Parameters.AddWithValue("@tamo_Id", SqlDbType.Int).Value = tamo_Id;
            }
            if (FechaAnterior != null && Fecha != null)
            {
                command.CommandText += " and Fecha between @FechaInicio and @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = FechaAnterior;
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = Fecha;
            }
            else if (FechaAnterior != null && Fecha == null)
            {
                command.CommandText += " and Fecha <= @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = FechaAnterior;
            }
            else if (Fecha != null && FechaAnterior == null)
            {
                command.CommandText += " and Fecha >= @FechaInicio ";
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = Fecha;
            }

            try
            {
                SqlConnection conx = new SqlConnection(connectionString);
                command.Connection = conx;
                SqlDataAdapter adp = new SqlDataAdapter(command);
                adp.Fill(ds, ds.V_RPT_HistorialAmonestaciones.TableName);

                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\AmonestacionesRPT.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesAmonestacionesDS", ds.Tables["V_RPT_HistorialAmonestaciones"]));
                conx.Close();


                ViewBag.ReportViewer = reportViewer;
                //ViewBag.TipoHora = db.tbHistorialHorasTrabajadas.Where(x => x.htra_Id == htra_Id);

                //ViewBag.Titulos = db.tbTipoAmonestaciones.Where(x => x.tamo_Id == tamo_Id).Select(x => x.tamo_Descripcion).FirstOrDefault();
                //Cargar DDL del modal (Tipo de planilla a seleccionar)

                ViewBag.TipoAmonesta = new SelectList(db.tbTipoAmonestaciones.Where(o => o.tamo_Estado == true), "tamo_Id", "tamo_Descripcion");
                ViewBag.EmpleadoAMON = new SelectList(db.V_RPT_HistorialAmonestaciones_Empleados, "per_Identidad", "nombre");
                //ViewBag.Planillas = new SelectList(db.tbCatalogoDePlanillas.Where(o => o.cpla_Activo == true), "cpla_IdPlanilla", "cpla_DescripcionPlanilla");
                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        [HttpPost]
        [SessionManager("ReportesRRHH/HistorialAudienciaDescargoRPT")]
        public ActionResult HistorialAudienciaDescargoRPT(string per_Identidad, DateTime? fechaAudiencia, DateTime? fecha)
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
            command.CommandText = "SELECT * from rrhh.V_RPT_HistorialAudienciaDescargo WHERE per_identidad like '%'+@Identidad+'%'";

            command.Parameters.AddWithValue("@Identidad", SqlDbType.NVarChar).Value = per_Identidad; if (fecha != null && fechaAudiencia != null)
            {
                command.CommandText += " and fechaAudiencia between @FechaInicio and @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = fecha;
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = fechaAudiencia;
            }
            else if (fecha != null && fechaAudiencia == null)
            {
                command.CommandText += " and fechaAudiencia <= @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = fecha;
            }
            else if (fechaAudiencia != null && fecha == null)
            {
                command.CommandText += " and fechaAudiencia >= @FechaInicio ";
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = fechaAudiencia;
            }

            try
            {
                SqlConnection conx = new SqlConnection(connectionString);
                command.Connection = conx;
                SqlDataAdapter adp = new SqlDataAdapter(command);

                ds.EnforceConstraints = false;
                adp.Fill(ds, ds.V_RPT_HistorialAudienciaDescargo.TableName);

                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\AudienciasDescargo.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesAudiencias", ds.Tables["V_RPT_HistorialAudienciaDescargo"]));
                conx.Close();


                ViewBag.ReportViewer = reportViewer;


                ViewBag.EmpleadoAUDE = new SelectList(db.V_RPT_HistorialAudienciaDescargo_empleados, "per_Identidad", "nombre");
                ViewBag.ReportViewer = reportViewer;

                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }

        public ActionResult EquipoEmpleadosRPT()
        {
            //Cargar DDL del modal (Tipo de planilla a seleccionar)
            try
            {
                ViewBag.tbEquipoTrabajo = new SelectList(db.tbEquipoTrabajo.Where(o => o.eqtra_Estado == true).OrderBy(x => x.eqtra_Descripcion), "eqtra_Id", "eqtra_Descripcion");
                ViewBag.Vista_Empleados = new SelectList(db.V_Empleados.Where(o => o.emp_Estado == true).OrderBy(x => x.per_NombreCompleto), "emp_Id", "per_NombreCompleto");
                ViewBag.tbDepartamentos = new SelectList(db.tbDepartamentos.Where(x => x.depto_Estado == true), "depto_Id", "depto_Descripcion");
                ViewBag.tbAreas = new SelectList(db.tbAreas.Where(x => x.area_Estado == true), "area_Id", "area_Descripcion");
                ViewBag.tbCargos = new SelectList(db.tbCargos.Where(x => x.car_Estado == true), "car_Id", "car_Descripcion");
                //ViewBag.Persona = new SelectList(db.tbPersonas.Where(o => o.per_Estado == true), "per_Id", "req_Descripcion");
                return View();
            }
            catch
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
        [HttpPost]
        [SessionManager("ReportesRRHH/EquipoEmpleadosRPT")]
        public ActionResult EquipoEmpleadosRPT(int? eqtra_Id, int? Id_Persona, DateTime? fechaInicio, DateTime? fechaFin, int? area_Id, int? car_Id, int? depto_Id)
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
            command.CommandText = "SELECT * FROM rrhh.V_RPT_EquipoEmpleado where 1 = 1 ";
            if (eqtra_Id != null)
            {
                command.CommandText += " and eqtra_Id = @eqtra_Id";
                command.Parameters.AddWithValue("@eqtra_Id", SqlDbType.Int).Value = eqtra_Id;
            }
            if (area_Id != null)
            {
                command.CommandText += " and area_Id = @area_Id";
                command.Parameters.AddWithValue("@area_Id", SqlDbType.Int).Value = area_Id;
            }
            if (car_Id != null)
            {
                command.CommandText += " and car_Id = @car_Id";
                command.Parameters.AddWithValue("@car_Id", SqlDbType.Int).Value = car_Id;
            }
            if (depto_Id != null)
            {
                command.CommandText += " and depto_Id = @depto_Id";
                command.Parameters.AddWithValue("@depto_Id", SqlDbType.Int).Value = depto_Id;
            }
            if (Id_Persona != null)
            {
                command.CommandText += " and Id_Persona = @Id_Persona ";
                command.Parameters.AddWithValue("@Id_Persona", SqlDbType.Int).Value = Id_Persona;
            }
            if (fechaFin != null && fechaInicio != null)
            {
                command.CommandText += " and Fecha between @FechaInicio and @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = fechaFin;
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio;
            }
            else if (fechaFin != null && fechaInicio == null)
            {
                command.CommandText += " and Fecha <= @FechaFin ";
                command.Parameters.AddWithValue("@FechaFin", SqlDbType.DateTime).Value = fechaFin;
            }
            else if (fechaInicio != null && fechaFin == null)
            {
                command.CommandText += " and Fecha >= @FechaInicio ";
                command.Parameters.AddWithValue("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio;
            }
            try
            {
                SqlConnection conx = new SqlConnection(connectionString);
                command.Connection = conx;
                SqlDataAdapter adp = new SqlDataAdapter(command);
                adp.Fill(ds, ds.V_RPT_EquipoEmpleado.TableName);

                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\EquipoEmpleadosRPT.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportesRRHH", ds.Tables["V_RPT_EquipoEmpleado"]));
                conx.Close();
                ViewBag.ReportViewer = reportViewer;
                ViewBag.tbEquipoTrabajo = new SelectList(db.tbEquipoTrabajo.Where(o => o.eqtra_Estado == true), "eqtra_Id", "eqtra_Descripcion");
                ViewBag.Vista_Empleados = new SelectList(db.V_Empleados.Where(o => o.emp_Estado == true), "emp_Id", "per_NombreCompleto");
                ViewBag.tbDepartamentos = new SelectList(db.tbDepartamentos.Where(x => x.depto_Estado == true), "depto_Id", "depto_Descripcion");
                ViewBag.tbAreas = new SelectList(db.tbAreas.Where(x => x.area_Estado == true), "area_Id", "area_Descripcion");
                ViewBag.tbCargos = new SelectList(db.tbCargos.Where(x => x.car_Estado == true), "car_Id", "car_Descripcion");
                return View();
            }
            catch (Exception ex)
            {
                return View("~/Views/ErrorPages/ErrorConnectionDB.cshtml", null);
            }
        }
    }
}