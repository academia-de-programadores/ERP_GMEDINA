using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using OfficeOpenXml;
using LinqToExcel;
using Microsoft.Office.Interop.Excel;
using SpreadsheetLight;
using ERP_GMEDINA.Attribute;

//using Excel = Microsoft.Office.Interop.Excel;


namespace ERP_GMEDINA.Controllers
{
    public class EmpleadosController : Controller
    {
        private ERP_GMEDINAEntities db = null;
        Models.Helpers Function = new Models.Helpers();

        // GET: Empleados
        [SessionManager("Empleados/Index")]
        public ActionResult Index()
        {
            if (Session["Admin"] == null || Session["Usuario"] == null)
            {
                Response.Redirect("~/Inicio/index/");
                return null;
            }
            tbEmpleados tbEmpleados = new tbEmpleados { };
            return View(tbEmpleados);
        }
        [SessionManager("Empleados/Index")]
        public ActionResult llenarTabla()
        {
            try
            {
                //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                //posteriormente es destruida.
                using (db = new ERP_GMEDINAEntities())
                {
                    var tbEmpleados = db.tbEmpleados
                        .Select(x => new
                        {
                            Número = x.emp_Id,
                            Id = x.emp_Id,
                            per_Identidad = x.tbPersonas.per_Identidad,
                            Nombre = x.tbPersonas.per_Nombres + " " + x.tbPersonas.per_Apellidos,
                            depto_Descripcion = x.tbDepartamentos.depto_Descripcion,
                            Estado = x.emp_Estado,
                            per_Sexo = x.tbPersonas.per_Sexo,
                            per_Edad = x.tbPersonas.per_Edad,
                            per_Telefono = x.tbPersonas.per_Telefono,
                            per_CorreoElectronico = x.tbPersonas.per_CorreoElectronico
                        })
                        .Where(x => x.Estado == true).ToList();
                    return Json(tbEmpleados, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManager("Empleados/Index")]
        public ActionResult ChildRowData(int id)
        {
            List<V_Datos_Empleado> lista = new List<V_Datos_Empleado> { };
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    lista = db.V_Datos_Empleado.Where(x => x.emp_Id == id).ToList();
                }
            }
            catch
            {
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        [SessionManager("Empleados/ArchivoEmpleados")]
        public void ArchivoEmpleados()
        {
            db = new ERP_GMEDINAEntities();
            List<ExcelEmpleados> ExcelEmpleados = new List<ExcelEmpleados>();
            db = new ERP_GMEDINAEntities();
            ExcelEmpleados.Add(new ExcelEmpleados() { per_Identidad = "", per_Nombres = "", per_Apellidos = "", per_FechaNacimiento = "", per_Edad = "", per_Sexo = "", nac_Id = "", per_Direccion = "", per_Telefono = "", per_CorreoElectronico = "", per_EstadoCivil = "", per_TipoSangre = "", Cargo = db.UDP_RRHH_tbCargos_tbEmpleados_Select().ToList(), area_Id = "", depto_Id = "", jor_Id = "", cpla_IdPlanilla = "", fpa_IdFormaPago = "", emp_FechaIngreso = "", emp_CuentaBancaria = "", sue_Cantidad = "", tmon_Id = "" });
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ArchivoEmpleados");
            Sheet.Cells["A1:V3"].Merge = true;
            Sheet.Cells["A1:V3"].Style.Font.Size = 16;
            Sheet.Cells["A1:V3"].Style.Font.Color.SetColor(System.Drawing.Color.Red);
            Sheet.Cells["A1:V3"].Value = "FAVOR LLENAR UNICAMENTE LA INFORMACION SOLICITADA, NO CAMBIAR NINGUNA CONFIGURACION DE ESTE DOCUMENTO";
            Sheet.Cells["A1:V3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            Sheet.Cells["A1:V3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            Sheet.Cells["A1:V3"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            Sheet.Cells["A1:V3"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            Sheet.Cells["A1:V3"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            Sheet.Cells["A1:V3"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            Sheet.Cells["A1:V3"].Style.Border.Top.Color.SetColor(System.Drawing.Color.Red);
            Sheet.Cells["A1:V3"].Style.Border.Left.Color.SetColor(System.Drawing.Color.Red);
            Sheet.Cells["A1:V3"].Style.Border.Right.Color.SetColor(System.Drawing.Color.Red);
            Sheet.Cells["A1:V3"].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Red);
            Sheet.Cells["A1:V3"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Sheet.Cells["A1:V3"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));
            Sheet.Cells["A4"].Value = "Identidad";
            Sheet.Cells["A5:A10000"].Style.Numberformat.Format = "0000-0000-00000";

            Sheet.Cells["B4"].Value = "Nombres";
            Sheet.Cells["C4"].Value = "Apellidos";
            Sheet.Cells["D4"].Value = "Fecha Nacimiento";
            Sheet.Cells["E4"].Value = "Edad";

            Sheet.Cells["F4"].Value = "Sexo";
            Sheet.Cells["G4"].Value = "Nacionalidad";
            Sheet.Cells["H4"].Value = "Direccion";
            Sheet.Cells["I4"].Value = "Telefono";
            Sheet.Cells["J4"].Value = "Correo Electronico";
            Sheet.Cells["K4"].Value = "Estado Civil";
            Sheet.Cells["L4"].Value = "Tipo de Sangre";
            Sheet.Cells["M4"].Value = "Cargo";
            Sheet.Cells["N4"].Value = "Area";
            Sheet.Cells["O4"].Value = "Departamentos";
            Sheet.Cells["P4"].Value = "Jornadas";
            Sheet.Cells["Q4"].Value = "Planilla";
            Sheet.Cells["R4"].Value = "FormadePago";
            Sheet.Cells["S4"].Value = "Fecha Ingreso";
            Sheet.Cells["T4"].Value = "Cuenta Bancaria";
            Sheet.Cells["U4"].Value = "Sueldo Cantidad";
            Sheet.Cells["V4"].Value = "Tipo Moneda";



            int row = 2;
            foreach (var item in ExcelEmpleados)
            {
                //Sheet.Cells["A5:T1000"].AutoFitColumns();
                //double rowHeight = 15;
                //Sheet.Row(5).Height = rowHeight;
                //Sheet.Column(5).AutoFit();
                Sheet.Cells[string.Format("A{0}", row)].Value = item.per_Identidad;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.per_Nombres;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.per_Apellidos;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.per_FechaNacimiento;
                Sheet.Cells["D5:D1000"].Style.Numberformat.Format = "yyyy-mm-dd";
                //Sheet.Cells["D5"].Formula = "=DATE(2014,10,5)";

                var per_Sexo = Sheet.DataValidations.AddListValidation("F5:F1000");
                per_Sexo.Formula.Values.Add("F");
                per_Sexo.Formula.Values.Add("M");
                //Sheet.Cells[string.Format("E{0}", row)].Value = item.per_Sexo;

                var Nacionalidades = db.tbNacionalidades.Where(tabla => tabla.nac_Estado == true)
                    .Select(tabla => tabla.nac_Descripcion)
                    .ToArray();
                Sheet.Cells["MB1"].LoadFromCollection<string>(Nacionalidades.ToList<string>());
                var Nac_val = Sheet.DataValidations.AddListValidation("G5:G1000");
                Nac_val.Formula.ExcelFormula = "$MB$1:$MB$" + Nacionalidades.Length;
                Sheet.Column(340).Style.Font.Color.SetColor(System.Drawing.Color.White);

                Sheet.Cells[string.Format("H{0}", row)].Value = item.per_Direccion;
                Sheet.Cells[string.Format("I{0}", row)].Value = item.per_Telefono;
                Sheet.Cells[string.Format("J{0}", row)].Value = item.per_CorreoElectronico;

                var per_EstadoCivil = Sheet.DataValidations.AddListValidation("K5:K1000");
                per_EstadoCivil.Formula.Values.Add("S");
                per_EstadoCivil.Formula.Values.Add("C");
                per_EstadoCivil.Formula.Values.Add("D");
                per_EstadoCivil.Formula.Values.Add("V");
                //Sheet.Cells[string.Format("J{0}", row)].Value = item.per_EstadoCivil;
                //Sheet.Cells[string.Format("L{0}", row)].Value = item.per_TipoSangre;
                var per_TipoSangre = Sheet.DataValidations.AddListValidation("L5:L1000");
                per_TipoSangre.Formula.Values.Add("A+");
                per_TipoSangre.Formula.Values.Add("A-");
                per_TipoSangre.Formula.Values.Add("AB+");
                per_TipoSangre.Formula.Values.Add("AB-");
                per_TipoSangre.Formula.Values.Add("B+");
                per_TipoSangre.Formula.Values.Add("B-");
                per_TipoSangre.Formula.Values.Add("O+");
                per_TipoSangre.Formula.Values.Add("O-");

                var Cargos = db.tbCargos.Where(tabla => tabla.car_Estado == true)
                    .Select(tabla => tabla.car_Descripcion)
                    .ToArray();
                Sheet.Cells["MA1"].LoadFromCollection<string>(Cargos.ToList<string>());
                var Car_val = Sheet.DataValidations.AddListValidation("M5:M1000");
                Car_val.Formula.ExcelFormula = "$MA$1:$MA$" + Cargos.Length;
                Sheet.Column(339).Style.Font.Color.SetColor(System.Drawing.Color.White);


                var Areas = db.tbAreas.Where(tabla => tabla.area_Estado == true)
                .Select(tabla => tabla.area_Descripcion)
                .ToArray();
                Sheet.Cells["MC1"].LoadFromCollection<string>(Areas.ToList<string>());
                var area_val = Sheet.DataValidations.AddListValidation("N5:N1000");
                area_val.Formula.ExcelFormula = "$MC$1:$MC$" + Areas.Length;
                Sheet.Column(341).Style.Font.Color.SetColor(System.Drawing.Color.White);


                var Dpto = db.tbDepartamentos.Where(tabla => tabla.depto_Estado == true)
                .Select(tabla => tabla.depto_Descripcion)
                .ToArray();
                Sheet.Cells["MD1"].LoadFromCollection<string>(Dpto.ToList<string>());
                var Dpto_val = Sheet.DataValidations.AddListValidation("O5:O1000");
                Dpto_val.Formula.ExcelFormula = "$MD$1:$MD$" + Dpto.Length;
                Sheet.Column(342).Style.Font.Color.SetColor(System.Drawing.Color.White);

                var Jor = db.tbJornadas.Where(tabla => tabla.jor_Estado == true)
               .Select(tabla => tabla.jor_Descripcion)
               .ToArray();
                Sheet.Cells["ME1"].LoadFromCollection<string>(Jor.ToList<string>());
                var jor_val = Sheet.DataValidations.AddListValidation("P5:P1000");
                jor_val.Formula.ExcelFormula = "$ME$1:$ME$" + Jor.Length;
                Sheet.Column(342).Style.Font.Color.SetColor(System.Drawing.Color.White);

                var Plani = db.tbCatalogoDePlanillas.Where(tabla => tabla.cpla_Activo == true)
               .Select(tabla => tabla.cpla_DescripcionPlanilla)
               .ToArray();
                Sheet.Cells["MF1"].LoadFromCollection<string>(Plani.ToList<string>());
                var Plani_val = Sheet.DataValidations.AddListValidation("Q5:Q1000");
                Plani_val.Formula.ExcelFormula = "$MF$1:$MF$" + Plani.Length;
                Sheet.Column(342).Style.Font.Color.SetColor(System.Drawing.Color.White);

                var Fpago = db.tbFormaPago.Where(tabla => tabla.fpa_Activo == true)
                .Select(tabla => tabla.fpa_Descripcion)
                .ToArray();
                Sheet.Cells["MG1"].LoadFromCollection<string>(Fpago.ToList<string>());
                var Fpago_val = Sheet.DataValidations.AddListValidation("R5:R1000");
                Fpago_val.Formula.ExcelFormula = "$MG$1:$MG$" + Fpago.Length;
                Sheet.Column(342).Style.Font.Color.SetColor(System.Drawing.Color.White);

                //Fecha Ingreso
                Sheet.Cells[string.Format("S{0}", row)].Value = item.emp_FechaIngreso;
                Sheet.Cells["S5:S1000"].Style.Numberformat.Format = "yyyy-mm-dd";

                Sheet.Cells[string.Format("T{0}", row)].Value = item.emp_CuentaBancaria;
                // Sheet.Cells["T5:T10000"].Style.Numberformat.Format = "00000000000000000000";
                Sheet.Cells["T5:T10000"].Style.Numberformat.Format = "@";

                Sheet.Cells[string.Format("U{0}", row)].Value = item.sue_Cantidad;
                Sheet.Cells["U5:U1000"].Style.Numberformat.Format = "0.0000";

                var Moneda = db.tbTipoMonedas.Where(tabla => tabla.tmon_Estado == true)
               .Select(tabla => tabla.tmon_Descripcion)
               .ToArray();
                Sheet.Cells["MH1"].LoadFromCollection<string>(Moneda.ToList<string>());
                var Moneda_val = Sheet.DataValidations.AddListValidation("V5:V1000");
                Moneda_val.Formula.ExcelFormula = "$MH$1:$MH$" + Fpago.Length;
                Sheet.Column(343).Style.Font.Color.SetColor(System.Drawing.Color.White);

                row++;
            }
            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + $"ArchivoEmpleados_{DateTime.Now.Ticks.ToString()}.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        [HttpPost]
        [SessionManager("Empleados/UploadEmpleados")]
        public ActionResult UploadEmpleados(HttpPostedFileBase FileUpload)
        {
            try
            {
                db = new ERP_GMEDINAEntities();
                if ((FileUpload.ContentLength != 0) && (FileUpload.FileName.EndsWith("xls") || FileUpload.FileName.EndsWith("xlsx")))
                {//OPEN IF
                 // string path = Server.MapPath("~/Downloadable files/" + FileUpload.FileName);
                    string path = Path.Combine(Server.MapPath("~/Downloadable files"),
                                       Path.GetFileName(FileUpload.FileName));
                    if (!System.IO.File.Exists(path))
                    {//OPEN IF
                        //db = new ERP_GMEDINAEntities();

                        FileUpload.SaveAs(path);
                        // Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
                        //Microsoft.Office.Interop.Excel.Workbook workbook = application.Workbooks.Open(path);
                        //Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
                        //Microsoft.Office.Interop.Excel.Range range = worksheet.UsedRange;
                        SLDocument Slight = new SLDocument(path);
                        // for (int i = 5; i < range.Rows.Count + 1; i++)
                        //{//OPEN FOR
                        int Row = 5;
                        while (!string.IsNullOrEmpty(Slight.GetCellValueAsString(Row, 1)))
                        {
                            string identidad = Slight.GetCellValueAsString(Row, 1);//((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 1]).Text;
                            string nombre = Slight.GetCellValueAsString(Row, 2);//((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 2]).Text;
                            string apellidos = Slight.GetCellValueAsString(Row, 3); //((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 3]).Text;
                            if (identidad != "" && nombre != "" && apellidos != "")
                            {//open if
                                DateTime fechanacimiento = Slight.GetCellValueAsDateTime(Row, 4); //((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 4]).Text;
                                                                                                  // DateTime FECHANAC = Convert.ToDateTime(fechanacimiento);
                                string EDAD = Slight.GetCellValueAsString(Row, 5);// ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 5]).Text;
                                int Edad = Convert.ToInt32(EDAD);
                                string sexo = Slight.GetCellValueAsString(Row, 6);// ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 6]).Text;
                                string nacionalidad = Slight.GetCellValueAsString(Row, 7); //((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 7]).Text;

                                var nac_id = db.tbNacionalidades.Where(nac => nac.nac_Descripcion == nacionalidad)
                                .Select(nac => nac.nac_Id).ToList()[0];

                                //var nacio = (from b in db.tbNacionalidades where b.nac_Descripcion == nacionalidad select b.nac_Id).ToList().First();

                                //int nacio_id = Convert.ToInt32(nacio);

                                string direccion = Slight.GetCellValueAsString(Row, 8);// ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 8]).Text;
                                string telefono = Slight.GetCellValueAsString(Row, 9); //((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 9]).Text;
                                string correo = Slight.GetCellValueAsString(Row, 10); //((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 10]).Text;
                                string estadocivil = Slight.GetCellValueAsString(Row, 11); //((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 11]).Text;
                                string tiposangre = Slight.GetCellValueAsString(Row, 12); //((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 12]).Text;

                                string cargodescrip = Slight.GetCellValueAsString(Row, 13);// ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 13]).Text;
                                int cargo_id = Convert.ToInt32(db.tbCargos.Where(car => car.car_Descripcion == cargodescrip)
                                    .Select(car => car.car_Id).ToList()[0]);

                                string areadescrip = Slight.GetCellValueAsString(Row, 14);// ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 14]).Text;
                                int areas_id = Convert.ToInt32(db.tbAreas.Where(Areas => Areas.area_Descripcion == areadescrip)
                                    .Select(Areas => Areas.area_Id).ToList()[0]);

                                string dptodescrip = Slight.GetCellValueAsString(Row, 15);// ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 15]).Text;
                                int dpto_id = Convert.ToInt32(db.tbDepartamentos.Where(dpto => dpto.depto_Descripcion == dptodescrip)
                                  .Select(dpto => dpto.depto_Id).ToList()[0]);

                                string jordescrip = Slight.GetCellValueAsString(Row, 16);// ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 16]).Text;
                                int jor_id = Convert.ToInt32(db.tbJornadas.Where(jor => jor.jor_Descripcion == jordescrip)
                                .Select(jor => jor.jor_Id).ToList()[0]);

                                string Planidescrip = Slight.GetCellValueAsString(Row, 17);// ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 17]).Text;
                                int plani_id = Convert.ToInt32(db.tbCatalogoDePlanillas.Where(plani => plani.cpla_DescripcionPlanilla == Planidescrip)
                                .Select(plani => plani.cpla_IdPlanilla).ToList()[0]);

                                string formapagodescrip = Slight.GetCellValueAsString(Row, 18);// ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 18]).Text;
                                int formpago_id = Convert.ToInt32(db.tbFormaPago.Where(formpago => formpago.fpa_Descripcion == formapagodescrip)
                               .Select(formpago => formpago.fpa_IdFormaPago).ToList()[0]);

                                DateTime fechaingreso = Slight.GetCellValueAsDateTime(Row, 19); //((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 19]).Text;
                                //DateTime FECHAINGRESO = Convert.ToDateTime(fechaingreso);

                                string CuentaBancaria = Slight.GetCellValueAsString(Row, 20); //((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 20]).Text;

                                string sueldoCantidad = Slight.GetCellValueAsString(Row, 21); //((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 21]).Text;
                                decimal SUELDO = Convert.ToDecimal(sueldoCantidad);

                                string Monedadescrip = Slight.GetCellValueAsString(Row, 22); //((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 22]).Text;
                                int tmon_id = Convert.ToInt32(db.tbTipoMonedas.Where(mon => mon.tmon_Descripcion == Monedadescrip)
                                .Select(mon => mon.tmon_Id).ToList()[0]);


                               // var Usuario = (tbUsuario)Session["Usuario"];

                                IEnumerable<object> listEmpleados = null;
                                string MensajeError = "";
                                listEmpleados = db.UDP_RRHH_tbEmpleados_Insert(identidad, nombre, apellidos, fechanacimiento, Edad, sexo, nac_id, direccion, telefono, correo, estadocivil, tiposangre, (int)Session["UserLogin"], Function.DatetimeNow(), cargo_id, areas_id, dpto_id, jor_id, plani_id, formpago_id, (int)Session["UserLogin"], Function.DatetimeNow(), fechaingreso, CuentaBancaria, SUELDO, tmon_id, (int)Session["UserLogin"], Function.DatetimeNow());

                                foreach (UDP_RRHH_tbEmpleados_Insert_Result Item in listEmpleados)
                                {
                                    MensajeError = Item.MensajeError;
                                }
                                if (!string.IsNullOrEmpty(MensajeError))
                                {//OPEN IF
                                    if (MensajeError.StartsWith("-1"))
                                    {
                                        ModelState.AddModelError("", "1. No se pudo Agregar el registro");
                                        return Json(-1, JsonRequestBehavior.AllowGet);
                                    }
                                }//CLOSE IF
                                //return RedirectToAction("Index");
                            }//close if
                            else
                            {//OPEN ELSE
                                break;

                            }//CLOSE ELSE
                            Row++;
                        }//CLOSE FOR
                    }//CLOSE IF
                    else
                    {
                        System.IO.File.Delete(path);
                        return Json(-3, JsonRequestBehavior.AllowGet);
                    }
                }//CLOSE IF
                else
                {
                    // System.IO.File.Delete(path);
                    return Json(-4, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                InsertBitacoraErrores("Empleados_Index", "-2 " + ex.Message.ToString(), "Insert");
                return Json(-2, JsonRequestBehavior.AllowGet);
            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        private void InsertBitacoraErrores(string sPantalla, string biteMensajeError, string biteAccion)
        {
            string UserName = "Error";
            try
            {
                db = new ERP_GMEDINAEntities();
                var List = db.UDP_Acce_tbBitacoraErrores_Insert(sPantalla, UserName, DateTime.Now, biteMensajeError, biteAccion);
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
        }

        // GET: Empleados/Details/5
        [SessionManager("Empleados/Details")]
        public ActionResult Details(int? id)
        {
            try
            {
                db = new ERP_GMEDINAEntities();
                //Aqui codigo llenarTabla
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbEmpleados tbEmpleados = db.tbEmpleados.Find(id);
                var fechaingreso = (from a in db.tbEmpleados where a.emp_Id == id select a.emp_Fechaingreso).ToList()[0];
                DateTime fechaactual = DateTime.Now;
                DateTime fechaingresodate = Convert.ToDateTime(fechaingreso);
                int timespan = (fechaactual.Year - fechaingresodate.Year);
                if (timespan == 0)
                {
                    ViewBag.Antiguedad = "Menos de 1 año";
                }
                else
                {
                    ViewBag.Antiguedad = timespan + " " + "Años";
                }
                if (tbEmpleados == null)
                {
                    return HttpNotFound();
                }
                var JefeArea = tbEmpleados.tbAreas.tbCargos.tbEmpleados.
                    Select(p => new { Nombres = p.tbPersonas.per_Nombres + " " + p.tbPersonas.per_Apellidos });
                var JefeDepto = tbEmpleados.tbDepartamentos.tbCargos.tbEmpleados.
                    Select(p => new { Nombres = p.tbPersonas.per_Nombres + " " + p.tbPersonas.per_Apellidos });

                Session["JefeArea"] = JefeArea.Count() == 0 ? "Sin asignar" : JefeArea.First().Nombres;
                Session["JefeDepto"] = JefeDepto.Count() == 0 ? "Sin asignar" : JefeDepto.First().Nombres;
                return View(tbEmpleados);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return HttpNotFound();
            }
        }



        /// <summary>
        /// SUBR DOCUMENTOS AL EXPEDIENTE
        /// </summary>
        /// 
        [HttpPost]
        [SessionManager("Empleados/UploadDocumento")]
        public ActionResult UploadDocumento(string tiposArchivo, int id, HttpPostedFileBase File)
        {

            try
            {
                if ((File.ContentLength != 0))
                {//OPEN IF
                    bool ExtensionAceptada = false;
                    string ExtensionDeArchivo = File.FileName.ToString().Split('.').Last();

                    string[] ExtensionesValidas = { "xls", "xlsx", "jpeg", "jpg", "png", "pdf", "svg", "doc", "docx" };

                    db = new ERP_GMEDINAEntities();

                    foreach (string ext in ExtensionesValidas)
                    {
                        if (ExtensionDeArchivo == ext)
                            ExtensionAceptada = true;
                    }

                    if (ExtensionAceptada == true)
                    {
                        string path = Server.MapPath("~/Expedientes/");
                        string folder = ("Expediente_" + id);
                        string carpetaempleados = Server.MapPath("~/Expedientes/" + "Expediente_" + id);
                        string subcarpetas = (carpetaempleados + "/" + tiposArchivo);
                        string FULLPATH = (subcarpetas + "/" + File.FileName);

                        if (!System.IO.Directory.Exists((carpetaempleados)))
                        {//OPEN IF
                         //db = new ERP_GMEDINAEntities();
                            System.IO.Directory.CreateDirectory(path + "Expediente_" + id);
                        }//CLOSE IF

                        if (!System.IO.Directory.Exists((subcarpetas)))
                        {
                            System.IO.Directory.CreateDirectory(path + "Expediente_" + id + "/" + tiposArchivo);
                        }
                        if (!System.IO.File.Exists((FULLPATH)))
                        {
                            File.SaveAs(FULLPATH);
                        }
                        else
                        {
                            return Json(-3, JsonRequestBehavior.AllowGet);
                        }
                        //File.SaveAs(subcarpetas+"/"+File.FileName);

                        //File.SaveAs(path);
                        var Usuario = (tbUsuario)Session["Usuario"];

                        IEnumerable<object> listdocumentos = null;
                        string MensajeError = "";
                        listdocumentos = db.UDP_RRHH_tbDirectoriosEmpleados_Insert(tiposArchivo, File.FileName, id, (int)Session["UserLogin"], Function.DatetimeNow());

                        foreach (UDP_RRHH_tbDirectoriosEmpleados_Insert_Result Item in listdocumentos)
                        {
                            MensajeError = Item.MensajeError;
                        }
                        if (!string.IsNullOrEmpty(MensajeError))
                        {//OPEN IF
                            if (MensajeError.StartsWith("-1"))
                            {
                                ModelState.AddModelError("", "1. No se pudo Agregar el registro");
                                return Json(-1, JsonRequestBehavior.AllowGet);
                            }
                        }//CLOSE IF
                        return Json(1, JsonRequestBehavior.AllowGet);

                    }//CLOSE IF
                    else
                    {
                        return Json(-4, JsonRequestBehavior.AllowGet);
                    }
                }//CLOSE IF
                else
                {
                    return Json(-4, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return Json(-2, JsonRequestBehavior.AllowGet);
            }


        }
        //  [HttpPost]
        [SessionManager("Empleados/CargarArchivos")]
        public ActionResult CargarArchivos(string Id)
        {
            int id = int.Parse(Id);
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var tbDirectoriosEmpleados = db.tbDirectoriosEmpleados.Include(t => t.tbEmpleados)
                    .Select(
                    x => new
                    {

                        direm_Id = x.direm_Id,
                        direm_Carpeta = x.direm_Carpeta,
                        direm_FechaCrea = x.direm_FechaCrea,
                        direm_NombreArchivo = x.direm_NombreArchivo,
                        direm_Estado = x.direm_Estado,
                        emp_Id = x.emp_Id,
                        emp_FechaIngreso = x.tbEmpleados.emp_Fechaingreso
                    }
                    ).Where(x => x.direm_Estado == true && x.emp_Id == id && x.emp_FechaIngreso <= x.direm_FechaCrea).ToList();
                    return Json(tbDirectoriosEmpleados, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }
        [SessionManager("Empleados/CargarArchivosExpedienteViejo")]
        public ActionResult CargarArchivosExpedienteViejo(string Id)
        {
            int id= int.Parse(Id);
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var tbDirectoriosEmpleados = db.tbDirectoriosEmpleados.Include(t => t.tbEmpleados)
                    .Select(
                    x => new
                    {

                        direm_Id = x.direm_Id,
                        direm_Carpeta = x.direm_Carpeta,
                        direm_FechaCrea = x.direm_FechaCrea,
                        direm_NombreArchivo = x.direm_NombreArchivo,
                        direm_Estado = x.direm_Estado,
                        emp_Id = x.emp_Id,
                        emp_FechaIngreso = x.tbEmpleados.emp_Fechaingreso
                    }
                    ).Where(x => x.direm_Estado == true && x.emp_Id == id && x.emp_FechaIngreso > x.direm_FechaCrea).ToList();
                    return Json(tbDirectoriosEmpleados, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }
        [SessionManager("Empleados/DetallesExpediente")]
        public ActionResult DetallesExpediente(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbDirectoriosEmpleados tbDirectoriosEmpleados = null;
            try
            {
                db = new ERP_GMEDINAEntities();
                tbDirectoriosEmpleados = db.tbDirectoriosEmpleados.Find(id);
                if (tbDirectoriosEmpleados == null || !tbDirectoriosEmpleados.direm_Estado)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return HttpNotFound();
            }
            Session["id"] = id;
            var Lista = new tbDirectoriosEmpleados
            {
                direm_Id = tbDirectoriosEmpleados.direm_Id,
                direm_NombreArchivo = tbDirectoriosEmpleados.direm_NombreArchivo,
                direm_Carpeta = tbDirectoriosEmpleados.direm_Carpeta,
                direm_Estado = tbDirectoriosEmpleados.direm_Estado,
                direm_UsuarioCrea = tbDirectoriosEmpleados.direm_UsuarioCrea,
                direm_FechaCrea = tbDirectoriosEmpleados.direm_FechaCrea,
                direm_UsuarioModifica = tbDirectoriosEmpleados.direm_UsuarioModifica,
                direm_FechaModifica = tbDirectoriosEmpleados.direm_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbDirectoriosEmpleados.tbUsuario).usu_NombreUsuario },
            };
            return Json(Lista, JsonRequestBehavior.AllowGet);
        }
        [SessionManager("Empleados/Delete")]
        public ActionResult Delete(tbDirectoriosEmpleados tbDirectoriosEmpleados)
        {
            string msj = "";

            string RazonInactivo = "Se ha Inhabilitado este Registro";
            if (tbDirectoriosEmpleados.direm_Id != 0)
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbDirectoriosEmpleados_Delete(tbDirectoriosEmpleados.direm_Id, RazonInactivo, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbDirectoriosEmpleados_Delete_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }



                    if (msj.Substring(0, 1) != "-")
                    {
                        using (db = new ERP_GMEDINAEntities())
                        {
                            var tbDirectoriosEmpleadosDireccion = db.tbDirectoriosEmpleados
                            .Select(
                            x => new
                            {
                                emp_Id = x.emp_Id,
                                direm_Id = x.direm_Id,
                                direm_Carpeta = x.direm_Carpeta,
                                direm_NombreArchivo = x.direm_NombreArchivo,
                            }

                            ).Where(x => x.direm_Id == tbDirectoriosEmpleados.direm_Id).ToList().Last();
                            string direm_Carpeta = tbDirectoriosEmpleadosDireccion.direm_Carpeta;
                            string direm_NombreArchivo = tbDirectoriosEmpleadosDireccion.direm_NombreArchivo;
                            int emp_Id = tbDirectoriosEmpleadosDireccion.emp_Id;

                            string path = Server.MapPath("~/Expedientes/" + "Expediente_" + emp_Id + "/" + direm_Carpeta + "/" + direm_NombreArchivo);

                            if (System.IO.File.Exists(path))
                            {
                                System.IO.File.Delete(path);
                            }

                        }

                        //Eliminar Archivo
                    }


                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }
        protected tbUsuario IsNull(tbUsuario valor)
        {
            if (valor != null)
            {
                return valor;
            }
            else
            {
                return new tbUsuario { usu_NombreUsuario = "" };
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
