using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using OfficeOpenXml;

//using Excel = Microsoft.Office.Interop.Excel;


namespace ERP_GMEDINA.Controllers
{
    public class EmpleadosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Empleados
        public ActionResult Index()
        {
            var tbEmpleados = new List < tbEmpleados >{ };
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
                        .Select(x=>new
                        {
                            Id=x.emp_Id,
                            per_Identidad = x.tbPersonas.per_Identidad,
                            Nombre = x.tbPersonas.per_Nombres+" "+x.tbPersonas.per_Apellidos,
                            depto_Descripcion = x.tbDepartamentos.depto_Descripcion,
                            per_Sexo = x.tbPersonas.per_Sexo,
                            per_Edad = x.tbPersonas.per_Edad,
                            per_Telefono = x.tbPersonas.per_Telefono,
                            per_CorreoElectronico = x.tbPersonas.per_CorreoElectronico
                        })
                        .ToList();
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

        public ActionResult DescargarArchivo()
        {
            string carpeta = AppDomain.CurrentDomain.BaseDirectory + "Downloadable files/";
            byte[] ArchivoBytes = System.IO.File.ReadAllBytes(carpeta + "AgregarEmpleados.xlsx");
            string NombreArchivo = "AgregarEmpleados.xlsx";
            return File(ArchivoBytes, System.Net.Mime.MediaTypeNames.Application.Octet, NombreArchivo);
        }

        public void ArchivoEmpleados()
        {
            List<ExcelEmpleados> ExcelEmpleados = new List<ExcelEmpleados>();
            ExcelEmpleados.Add(new ExcelEmpleados() { per_Identidad = "", per_Nombres = "5", per_Apellidos = "Jan", per_FechaNacimiento = "2019", per_Sexo = "", nac_Id = "", per_Direccion = "", per_Telefono = "", per_CorreoElectronico = "", per_EstadoCivil = "", per_TipoSangre = "", Cargo = db.UDP_RRHH_tbCargos_tbEmpleados_Select().ToList(), area_Id = "", depto_Id = "", jor_Id = "" });
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ArchivoEmpleados");
            Sheet.Cells["A1"].Value = "Identidad";
            Sheet.Cells["B1"].Value = "Nombres";
            Sheet.Cells["C1"].Value = "Apellidos";
            Sheet.Cells["D1"].Value = "Fecha Nacimiento";
            Sheet.Cells["E1"].Value = "Sexo";
            Sheet.Cells["F1"].Value = "Nacionalidad";
            Sheet.Cells["G1"].Value = "Direccion";
            Sheet.Cells["H1"].Value = "Telefono";
            Sheet.Cells["I1"].Value = "Correo Electronico";
            Sheet.Cells["J1"].Value = "Estado Civil";
            Sheet.Cells["K1"].Value = "Tipo de Sangre";
            Sheet.Cells["L1"].Value = "Cargo";
            Sheet.Cells["LL1"].Value = "Area";
            Sheet.Cells["M1"].Value = "Departamentos";
            Sheet.Cells["N1"].Value = "Jornadas";



            int row = 2;
            foreach (var item in ExcelEmpleados)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.per_Identidad;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.per_Nombres;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.per_Apellidos;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.per_FechaNacimiento;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.per_Sexo;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.nac_Id;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.per_Direccion;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.per_Telefono;
                Sheet.Cells[string.Format("I{0}", row)].Value = item.per_CorreoElectronico;
                Sheet.Cells[string.Format("J{0}", row)].Value = item.per_EstadoCivil;
                Sheet.Cells[string.Format("K{0}", row)].Value = item.per_TipoSangre;

                var lol = db.tbCargos
                    .Select(tabla=>tabla.car_Descripcion)
                    .ToArray();
                Sheet.Cells["MA1"].LoadFromCollection<string>(lol.ToList<string>());
                var val = Sheet.DataValidations.AddListValidation("L2");
                val.Formula.ExcelFormula = "$MA$1:$MA$"+lol.Length;
                Sheet.Column(339).Style.Font.Color.SetColor(System.Drawing.Color.White);
                //Sheet.Column(15).Hidden = true;
                //Sheet.Cells.AutoFitColumns(8.43, 100);

                //agregar parte de :"FAVOR LLENAR UNICAMENTE LA INFORMACION SOLICITADA, NO CAMBIAR NINGUNA CONFIGURACION DE ESTE DOCUMENTO"
                Sheet.Cells[string.Format("M{0}", row)].Value = item.area_Id;
                Sheet.Cells[string.Format("N{0}", row)].Value = item.depto_Id;
                Sheet.Cells[string.Format("O{0}", row)].Value = item.jor_Id;

                row++;
            }
            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + $"ArchivoEmpleados_{DateTime.Now.Ticks.ToString()}.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }


        public void ArchveEmpleados() {


        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
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
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
