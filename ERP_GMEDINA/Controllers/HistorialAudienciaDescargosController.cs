using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Controllers
{
    public class HistorialAudienciaDescargosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: HistorialAudienciaDescargos
        public ActionResult Index()
        {
            var tbHistorialAudienciaDescargo = db.tbHistorialAudienciaDescargo.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbEmpleados);
            return View(tbHistorialAudienciaDescargo.ToList());
        }



        public ActionResult llenarTabla()
        {
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var Empleados = db.V_EmpleadoIncapacidades.Where(t => t.emp_Estado == true)
                        .Select(
                        t => new
                        {
                            emp_Id = t.emp_Id,
                            Empleado = t.emp_NombreCompleto,
                            Cargo = t.car_Descripcion,
                            Departamento = t.depto_Descripcion
                        }).ToList();
                    return Json(Empleados, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ChildRowData(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<V_HistorialAudienciaDescargo> lista = new List<V_HistorialAudienciaDescargo> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    lista = db.V_HistorialAudienciaDescargo.Where(x => x.emp_Id == id & x.aude_Estado == true).ToList();

                }
                catch
                {

                }

            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Create(tbHistorialAudienciaDescargo tbHistorialAudienciaDescargo)
        {
            string msj = "";
            if (tbHistorialAudienciaDescargo.aude_DireccionArchivo != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbHistorialAudienciaDescargo_Insert(tbHistorialAudienciaDescargo.emp_Id,
                                                                            tbHistorialAudienciaDescargo.aude_Descripcion,
                                                                            tbHistorialAudienciaDescargo.aude_FechaAudiencia,
                                                                            tbHistorialAudienciaDescargo.aude_Testigo,
                                                                            tbHistorialAudienciaDescargo.aude_DireccionArchivo,
                                                                            1,
                                                                            DateTime.Now);
                    foreach (UDP_RRHH_tbHistorialAudienciaDescargo_Insert_Result item in list)
                    {
                        msj = item.MensajeError + " ";
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

        public ActionResult Edit(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbHistorialAudienciaDescargo tbHistaudiencia = null;
            try
            {
                tbHistaudiencia = db.tbHistorialAudienciaDescargo.Find(ID);
                if (tbHistaudiencia == null || !tbHistaudiencia.aude_Estado)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return HttpNotFound();
            }
            Session["id"] = ID;
            var Audiencia = new tbHistorialAudienciaDescargo
            {
                aude_Descripcion = tbHistaudiencia.aude_Descripcion,
                aude_FechaAudiencia = tbHistaudiencia.aude_FechaAudiencia,
                aude_Testigo = tbHistaudiencia.aude_Testigo,
                aude_DireccionArchivo = tbHistaudiencia.aude_DireccionArchivo,
                aude_FechaCrea = tbHistaudiencia.aude_FechaCrea,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbHistaudiencia.tbUsuario).usu_NombreUsuario },
                aude_FechaModifica = tbHistaudiencia.aude_FechaModifica,
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbHistaudiencia.tbUsuario1).usu_NombreUsuario },
          
            };

            return Json(Audiencia, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Edit2(tbHistorialAudienciaDescargo tbHistorialAudienciaDescargo)
        {
            string msj = "";
            if (tbHistorialAudienciaDescargo.aude_Id != 0)
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbHistorialAudienciaDescargo_Update(id, tbHistorialAudienciaDescargo.aude_FechaAudiencia, 1, DateTime.Now);
                    foreach (UDP_RRHH_tbHistorialAudienciaDescargo_Update_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
                Session.Remove("id");
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }

        private tbUsuario IsNull(tbUsuario valor)
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










        // GET: HistorialAudienciaDescargos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHistorialAudienciaDescargo tbHistorialAudienciaDescargo = db.tbHistorialAudienciaDescargo.Find(id);
            if (tbHistorialAudienciaDescargo == null)
            {
                return HttpNotFound();
            }
            return View(tbHistorialAudienciaDescargo);
        }

        // GET: HistorialAudienciaDescargos/Create
        public ActionResult Create()
        {
            ViewBag.aude_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.aude_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria");
            return View();
        }






        [HttpPost]
        public ActionResult Delete(tbHistorialAudienciaDescargo tbHistorialAudienciaDescargo)
        {
            string msj = "";
            if (tbHistorialAudienciaDescargo.aude_Id != 0 && tbHistorialAudienciaDescargo.aude_RazonInactivo != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbHistorialAudienciaDescargo_Delete(tbHistorialAudienciaDescargo.aude_Id, tbHistorialAudienciaDescargo.aude_RazonInactivo, 1, DateTime.Now);
                    foreach (UDP_RRHH_tbHistorialAudienciaDescargo_Delete_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
                Session.Remove("id");
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }
        // POST: HistorialAudienciaDescargos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: HistorialAudienciaDescargos/Edit/5

        // POST: HistorialAudienciaDescargos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.




       

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
