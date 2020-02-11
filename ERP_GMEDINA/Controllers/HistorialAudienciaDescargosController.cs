using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class HistorialAudienciaDescargosController : Controller
    {
        private ERP_GMEDINAEntities db = null;
        Models.Helpers Function = new Models.Helpers();

        // GET: HistorialAudienciaDescargos
        [SessionManager("AudienciasDescargo/Index")]
        public ActionResult Index()
        {
            
            try
            {
                db = new ERP_GMEDINAEntities();
                //var tbHistorialAudienciaDescargo = db.tbHistorialAudienciaDescargo.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbEmpleados);
                tbHistorialAudienciaDescargo tbHistorialAudienciaDescargos = new tbHistorialAudienciaDescargo { aude_Estado = true };
                bool Admin = (bool)Session["Admin"];
                return View(tbHistorialAudienciaDescargos);
            }
            catch
            {
                return View();
            }
           
        }
        [SessionManager("AudienciasDescargo/Index")]
        public ActionResult llenarTabla()
        {
            try
            {
                db = new ERP_GMEDINAEntities();
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
        [SessionManager("AudienciasDescargo/Index")]
        public ActionResult ChildRowData(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<V_HistorialAudienciaDescargo> lista = new List<V_HistorialAudienciaDescargo> { };
            try
            {
                db = new ERP_GMEDINAEntities();
                using (db = new ERP_GMEDINAEntities())
                {
                    lista = db.V_HistorialAudienciaDescargo.Where(x => x.emp_Id == id).ToList();
                }
            }
            catch
            {

            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [SessionManager("AudienciasDescargo/Create")]
        public JsonResult Create(tbHistorialAudienciaDescargo tbHistorialAudienciaDescargo)
        {
            string msj = "";
            if (tbHistorialAudienciaDescargo.aude_DireccionArchivo != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbHistorialAudienciaDescargo_Insert(tbHistorialAudienciaDescargo.emp_Id,
                                                                            tbHistorialAudienciaDescargo.aude_Descripcion,
                                                                            tbHistorialAudienciaDescargo.aude_FechaAudiencia,
                                                                            tbHistorialAudienciaDescargo.aude_Testigo,
                                                                            tbHistorialAudienciaDescargo.aude_DireccionArchivo,
                                                                             (int)Session["UserLogin"],
                                                                                Function.DatetimeNow());
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
        [SessionManager("AudienciasDescargo/Edit")]
        public ActionResult Edit(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbHistorialAudienciaDescargo tbHistaudiencia = null;
            try
            {
                db = new ERP_GMEDINAEntities();
                tbHistaudiencia = db.tbHistorialAudienciaDescargo.Find(ID);
                if (tbHistaudiencia == null)
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
        [SessionManager("AudienciasDescargo/Edit2")]
        public JsonResult Edit2(tbHistorialAudienciaDescargo tbHistorialAudienciaDescargo)
        {
            string msj = "";
            if (tbHistorialAudienciaDescargo.aude_Id != 0)
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
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
                //Session.Remove("id");
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
        [SessionManager("AudienciasDescargo/Details")]
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
        [SessionManager("AudienciasDescargo/Create")]
        public ActionResult Create()
        {
            ViewBag.aude_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.aude_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria");
            return View();
        }
        [HttpPost]
        [SessionManager("AudienciasDescargo/Delete")]
        public ActionResult Delete(tbHistorialAudienciaDescargo tbHistorialAudienciaDescargo)
        {
            string msj = "";

            string RazonInactivo = "Se ha Inhabilitado este Registro";

            if (tbHistorialAudienciaDescargo.aude_Id != 0 )
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbHistorialAudienciaDescargo_Delete(tbHistorialAudienciaDescargo.aude_Id, RazonInactivo, 
                                                                               (int)Session["UserLogin"],
                                                                                 Function.DatetimeNow());
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
                //Session.Remove("id");
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [SessionManager("AudienciasDescargo/habilitar")]
        public JsonResult habilitar(tbHistorialAudienciaDescargo tbHistorialAudienciaDescargo)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                db = new ERP_GMEDINAEntities();
                using (db = new ERP_GMEDINAEntities())
                {
                    var list = db.UDP_RRHH_tbHistorialAudienciaDescargo_Restore(tbHistorialAudienciaDescargo.aude_Id, 
                                                                                            (int)Session["UserLogin"],
                                                                                             Function.DatetimeNow());
                    foreach (UDP_RRHH_tbHistorialAudienciaDescargo_Restore_Result item in list)
                    {
                        result = item.MensajeError;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                result = "-2";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
