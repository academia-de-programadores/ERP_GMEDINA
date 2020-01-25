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

//using Excel = Microsoft.Office.Interop.Excel;


namespace ERP_GMEDINA.Controllers
{
    public class EmpleadosController : Controller
    {
        private ERP_GMEDINAEntities db = null;

        // GET: Empleados
        public ActionResult Index()
        {
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            var tbEmpleados = new List<tbEmpleados> { };
            return View(tbEmpleados);
        }
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
                        .Where(x=>x.Estado==true).ToList();
                    return Json(tbEmpleados, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ChildRowData(int id)
        {
            List<V_Datos_Empleado> lista = new List<V_Datos_Empleado> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    lista = db.V_Datos_Empleado.Where(x => x.emp_Id == id).ToList();
                }
                catch
                {
                }
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public void ArchivoEmpleados()
        {
            db = new ERP_GMEDINAEntities();
            List<ExcelEmpleados> ExcelEmpleados = new List<ExcelEmpleados>();
            db = new ERP_GMEDINAEntities();
            ExcelEmpleados.Add(new ExcelEmpleados() { per_Identidad = "", per_Nombres = "", per_Apellidos = "", per_FechaNacimiento = "", per_Edad = "", per_Sexo = "", nac_Id = "", per_Direccion = "", per_Telefono = "", per_CorreoElectronico = "", per_EstadoCivil = "", per_TipoSangre = "", Cargo = db.UDP_RRHH_tbCargos_tbEmpleados_Select().ToList(), area_Id = "", depto_Id = "", jor_Id = "", cpla_IdPlanilla = "", fpa_IdFormaPago = "", emp_FechaIngreso = "", emp_CuentaBancaria = "", sue_Cantidad="", tmon_Id="" });
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

                var Nacionalidades = db.tbNacionalidades.Where(tabla=>tabla.nac_Estado==true)
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
        public ActionResult UploadEmpleados(HttpPostedFileBase FileUpload)
        {
            string path = Server.MapPath("~/Downloadable files/" + FileUpload.FileName);
            try
            {
                db = new ERP_GMEDINAEntities();
                if ((FileUpload.ContentLength != 0) && (FileUpload.FileName.EndsWith("xls") || FileUpload.FileName.EndsWith("xlsx")))
                {//OPEN IF
                    if (!System.IO.File.Exists(path))
                    {//OPEN IF
                        db = new ERP_GMEDINAEntities();

                        FileUpload.SaveAs(path);
                        Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel.Workbook workbook = application.Workbooks.Open(path);
                        Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
                        Microsoft.Office.Interop.Excel.Range range = worksheet.UsedRange;

                        for (int i = 5; i < range.Rows.Count + 1; i++)
                        {//OPEN FOR
                            string identidad = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 1]).Text;
                            string nombre = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 2]).Text;
                            string apellidos = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 3]).Text;
                            if (identidad != "" && nombre != "" && apellidos != "")
                            {//open if
                                string fechanacimiento = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 4]).Text;
                                DateTime FECHANAC = Convert.ToDateTime(fechanacimiento);
                                string EDAD = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 5]).Text;
                                int Edad = Convert.ToInt32(EDAD);
                                string sexo = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 6]).Text;
                                string nacionalidad = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 7]).Text;

                                int nac_id = Convert.ToInt32(db.tbNacionalidades.Where(nac => nac.nac_Descripcion == nacionalidad)
                                    .Select(nac => nac.nac_Id).ToList()[0]);

                                string direccion = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 8]).Text;
                                string telefono = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 9]).Text;
                                string correo = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 10]).Text;
                                string estadocivil = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 11]).Text;
                                string tiposangre = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 12]).Text;

                                string cargodescrip = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 13]).Text;
                                int cargo_id = Convert.ToInt32(db.tbCargos.Where(car => car.car_Descripcion == cargodescrip)
                                    .Select(car => car.car_Id).ToList()[0]);

                                string areadescrip = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 14]).Text;
                                int areas_id = Convert.ToInt32(db.tbAreas.Where(Areas => Areas.area_Descripcion == areadescrip)
                                    .Select(Areas => Areas.area_Id).ToList()[0]);

                                string dptodescrip = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 15]).Text;
                                int dpto_id = Convert.ToInt32(db.tbDepartamentos.Where(dpto => dpto.depto_Descripcion == dptodescrip)
                                  .Select(dpto => dpto.depto_Id).ToList()[0]);

                                string jordescrip = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 16]).Text;
                                int jor_id = Convert.ToInt32(db.tbJornadas.Where(jor => jor.jor_Descripcion == jordescrip)
                                .Select(jor => jor.jor_Id).ToList()[0]);

                                string Planidescrip = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 17]).Text;
                                int plani_id = Convert.ToInt32(db.tbCatalogoDePlanillas.Where(plani => plani.cpla_DescripcionPlanilla == Planidescrip)
                                .Select(plani => plani.cpla_IdPlanilla).ToList()[0]);

                                string formapagodescrip = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 18]).Text;
                                int formpago_id = Convert.ToInt32(db.tbFormaPago.Where(formpago => formpago.fpa_Descripcion == formapagodescrip)
                               .Select(formpago => formpago.fpa_IdFormaPago).ToList()[0]);

                                string fechaingreso = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 19]).Text;
                                DateTime FECHAINGRESO = Convert.ToDateTime(fechaingreso);

                                string CuentaBancaria = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 20]).Text;

                                string sueldoCantidad = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 21]).Text;
                                decimal SUELDO = Convert.ToDecimal(sueldoCantidad);

                                string Monedadescrip = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 22]).Text;
                                int tmon_id = Convert.ToInt32(db.tbTipoMonedas.Where(mon => mon.tmon_Descripcion == Monedadescrip)
                                .Select(mon => mon.tmon_Id).ToList()[0]);


                                var Usuario = (tbUsuario)Session["Usuario"];

                                IEnumerable<object> listEmpleados = null;
                                string MensajeError = "";
                                listEmpleados = db.UDP_RRHH_tbEmpleados_Insert(identidad, nombre, apellidos, FECHANAC, Edad, sexo, nac_id, direccion, telefono, correo, estadocivil, tiposangre, Usuario.usu_Id, DateTime.Now, cargo_id, areas_id, dpto_id, jor_id, plani_id, formpago_id, Usuario.usu_Id, DateTime.Now, FECHAINGRESO, CuentaBancaria, SUELDO, tmon_id,Usuario.usu_Id,DateTime.Now);

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
                    System.IO.File.Delete(path);
                    return Json(-4, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return Json(-2, JsonRequestBehavior.AllowGet);
            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        // GET: Empleados/Details/5
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
                if (tbEmpleados == null)
                {
                    return HttpNotFound();
                }
                return View(tbEmpleados);
                //aqui termina llenarTabla
            }
            catch
            {
                return HttpNotFound();
            }
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            ViewBag.emp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.emp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.area_Id = new SelectList(db.tbAreas, "area_Id", "area_Descripcion");
            ViewBag.car_Id = new SelectList(db.tbCargos, "car_Id", "car_Descripcion");
            ViewBag.per_Id = new SelectList(db.tbPersonas, "per_Id", "per_Identidad");
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "emp_Id,per_Id,car_Id,area_Id,depto_Id,jor_Id,cpla_IdPlanilla,fpa_IdFormaPago,emp_CuentaBancaria,emp_Reingreso,emp_Fechaingreso,emp_RazonSalida,emp_CargoAnterior,emp_FechaDeSalida,emp_Estado,emp_RazonInactivo,emp_UsuarioCrea,emp_FechaCrea,emp_UsuarioModifica,emp_FechaModifica")] tbEmpleados tbEmpleados)
        {
            if (ModelState.IsValid)
            {
                db.tbEmpleados.Add(tbEmpleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.emp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpleados.emp_UsuarioCrea);
            ViewBag.emp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpleados.emp_UsuarioModifica);
            ViewBag.area_Id = new SelectList(db.tbAreas, "area_Id", "area_Descripcion", tbEmpleados.area_Id);
            ViewBag.car_Id = new SelectList(db.tbCargos, "car_Id", "car_Descripcion", tbEmpleados.car_Id);
            ViewBag.per_Id = new SelectList(db.tbPersonas, "per_Id", "per_Identidad", tbEmpleados.per_Id);
            return View(tbEmpleados);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleados tbEmpleados = db.tbEmpleados.Find(id);
            if (tbEmpleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.emp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpleados.emp_UsuarioCrea);
            ViewBag.emp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpleados.emp_UsuarioModifica);
            ViewBag.area_Id = new SelectList(db.tbAreas, "area_Id", "area_Descripcion", tbEmpleados.area_Id);
            ViewBag.car_Id = new SelectList(db.tbCargos, "car_Id", "car_Descripcion", tbEmpleados.car_Id);
            ViewBag.per_Id = new SelectList(db.tbPersonas, "per_Id", "per_Identidad", tbEmpleados.per_Id);
            return View(tbEmpleados);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "emp_Id,per_Id,car_Id,area_Id,depto_Id,jor_Id,cpla_IdPlanilla,fpa_IdFormaPago,emp_CuentaBancaria,emp_Reingreso,emp_Fechaingreso,emp_RazonSalida,emp_CargoAnterior,emp_FechaDeSalida,emp_Estado,emp_RazonInactivo,emp_UsuarioCrea,emp_FechaCrea,emp_UsuarioModifica,emp_FechaModifica")] tbEmpleados tbEmpleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbEmpleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.emp_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpleados.emp_UsuarioCrea);
            ViewBag.emp_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpleados.emp_UsuarioModifica);
            ViewBag.area_Id = new SelectList(db.tbAreas, "area_Id", "area_Descripcion", tbEmpleados.area_Id);
            ViewBag.car_Id = new SelectList(db.tbCargos, "car_Id", "car_Descripcion", tbEmpleados.car_Id);
            ViewBag.per_Id = new SelectList(db.tbPersonas, "per_Id", "per_Identidad", tbEmpleados.per_Id);
            return View(tbEmpleados);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleados tbEmpleados = db.tbEmpleados.Find(id);
            if (tbEmpleados == null)
            {
                return HttpNotFound();
            }
            return View(tbEmpleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbEmpleados tbEmpleados = db.tbEmpleados.Find(id);
            db.tbEmpleados.Remove(tbEmpleados);
            db.SaveChanges();
            return RedirectToAction("Index");
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
