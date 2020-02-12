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
    public class HistorialAmonestacionesController : Controller
    {
       
        private ERP_GMEDINAEntities db = null;
        Models.Helpers Function = new Models.Helpers();
        // GET: HistorialAmonestaciones
        [SessionManager("HistorialAmonestaciones/Index")]
        public ActionResult Index()
        {  
            try 
            {
                db = new ERP_GMEDINAEntities();
                ViewBag.tamo_Id = new SelectList(db.tbTipoAmonestaciones, "tamo_Id", "tamo_Descripcion");
                //var tbHistorialAmonestaciones = db.tbHistorialAmonestaciones.Include(t => t.tbEmpleados).Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
                tbHistorialAmonestaciones tbHistorialAmonestaciones = new tbHistorialAmonestaciones { hamo_Estado = true };
                bool Admin = (bool)Session["Admin"];
                return View(tbHistorialAmonestaciones);
            }
            catch
            {
                return View();
            }
            
        }
        [SessionManager("HistorialAmonestaciones/Index")]
        public ActionResult llenarTabla()
        {
            try
            {
                db = new ERP_GMEDINAEntities();
                using (db = new ERP_GMEDINAEntities())
                {
                    var Empleados = db.V_EmpleadoAmonestaciones.Where(t => t.emp_Estado == true)
                        .Select(
                        t => new
                        {
                            emp_Id = t.emp_Id,
                            Empleado= t.emp_NombreCompleto,
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
        [SessionManager("HistorialAmonestaciones/Index")]
        public ActionResult ChildRowData(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<V_HistorialAmonestacion> lista = new List<V_HistorialAmonestacion> { };
            try
            {
                db = new ERP_GMEDINAEntities();
                using (db = new ERP_GMEDINAEntities())
                {

                    lista = db.V_HistorialAmonestacion.Where(x => x.emp_Id == id).ToList();
                }
            }
            catch
            {
            }
            
            return Json(lista, JsonRequestBehavior.AllowGet);
        }



        // GET: HistorialAmonestaciones/Details/5
        //Modal de Detalle 
        [SessionManager("HistorialAmonestaciones/Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHistorialAmonestaciones tbHistorialAmonestaciones = null;
            try
            {
                db = new ERP_GMEDINAEntities();
                tbHistorialAmonestaciones = db.tbHistorialAmonestaciones.Find(id);
                if(tbHistorialAmonestaciones == null)
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
            var amonestaciones = new tbHistorialAmonestaciones
            {
                emp_Id = tbHistorialAmonestaciones.emp_Id,
                hamo_Observacion = tbHistorialAmonestaciones.hamo_Observacion,
                tbTipoAmonestaciones = new tbTipoAmonestaciones { tamo_Descripcion = IsNull(tbHistorialAmonestaciones.tbTipoAmonestaciones).tamo_Descripcion },
                hamo_Fecha = tbHistorialAmonestaciones.hamo_Fecha,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbHistorialAmonestaciones.tbUsuario).usu_NombreUsuario},
                hamo_FechaCrea = tbHistorialAmonestaciones.hamo_FechaCrea
             };
            return Json(amonestaciones, JsonRequestBehavior.AllowGet);
        }

        private  tbTipoAmonestaciones IsNull(tbTipoAmonestaciones valor)
        {
            if (valor != null)
            {
                return valor;
            }
            else
            {
                return new tbTipoAmonestaciones { tamo_Descripcion = "" };
            }
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
        public ActionResult Fecha(int? id)
        {
       
            tbHistorialAmonestaciones tbHistorialAmonestaciones = null;
            try
            {
                db = new ERP_GMEDINAEntities();
                tbHistorialAmonestaciones = db.tbHistorialAmonestaciones.Find(id);
                if (tbHistorialAmonestaciones == null)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch 
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

            db = new ERP_GMEDINAEntities();
            List<V_HistorialAmonestacion> lista = new List<V_HistorialAmonestacion> { };

            using (db = new ERP_GMEDINAEntities())
            {

                lista = db.V_HistorialAmonestacion.Where(x => x.emp_Id == id).ToList();
            }
            bool i = true;
            string Mensaje = "";
            DateTime FechaUltimaAmonestacion = Convert.ToDateTime("1900/01/01");

            foreach (var item in lista)
            {
                DateTime FechaAmonestacion = Convert.ToDateTime(item.hamo_Fecha);
                if (i)
                {
                    FechaUltimaAmonestacion = FechaAmonestacion;
                    i = false;
                }
                else if (FechaUltimaAmonestacion < FechaAmonestacion)
                {
                    FechaUltimaAmonestacion = FechaAmonestacion;
                }
            }
            if(FechaUltimaAmonestacion == Convert.ToDateTime("1900/01/01"))
            {
                Mensaje = "";

            }
            else
            {
                DateTime oldDate = FechaUltimaAmonestacion;
                DateTime newDate = DateTime.Now;

                // Difference in days, hours, and minutes.
                TimeSpan ts = newDate - oldDate;
                int differenceInDays = ts.Days;
                // Difference in days.
                Mensaje = "*Última amonestación hace: " + differenceInDays + " Días";
            }
            return Json(Mensaje, JsonRequestBehavior.AllowGet);
        }
        [SessionManager("HistorialAmonestaciones/Create")]
        // GET: HistorialAmonestaciones/Create
        public JsonResult Create(tbHistorialAmonestaciones tbHistorialAmonestaciones)
        {
            string msj = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
                {
                db = new ERP_GMEDINAEntities();
                var list = db.UDP_RRHH_tbHistorialAmonestaciones_Insert(tbHistorialAmonestaciones.emp_Id,
                                                                            tbHistorialAmonestaciones.tamo_Id,
                                                                            DateTime.Now,
                                                                            tbHistorialAmonestaciones.hamo_Observacion,
                                                                            (int)Session["UserLogin"],
                                                                            Function.DatetimeNow());
                    foreach (UDP_RRHH_tbHistorialAmonestaciones_Insert_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        [SessionManager("HistorialAmonestaciones/Delete")]
        public ActionResult Delete(tbHistorialAmonestaciones tbHistorialAmonestaciones)
        {
            string msj = "";
            if (tbHistorialAmonestaciones.hamo_Id != 0 && tbHistorialAmonestaciones.hamo_RazonInactivo != "")
            {
                //var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbHistorialAmonestaciones_Delete(tbHistorialAmonestaciones.hamo_Id, "Predeterminado", (int)Session["UserLogin"],Function.DatetimeNow());
                    foreach (UDP_RRHH_tbHistorialAmonestaciones_Delete_Result item in list)
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
       
        [HttpPost]
        [SessionManager("HistorialAmonestaciones/habilitar")]
        public JsonResult habilitar(tbHistorialAmonestaciones tbHistorialAmonestaciones)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                db = new ERP_GMEDINAEntities();
                using (db = new ERP_GMEDINAEntities())
                {
                    var list = db.UDP_RRHH_tbHistorialAmonestaciones_Restore(tbHistorialAmonestaciones.hamo_Id, 
                                                                                     (int)Session["UserLogin"],
                                                                                        Function.DatetimeNow());
                    foreach (UDP_RRHH_tbHistorialAmonestaciones_Restore_Result item in list)
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

        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                db = new ERP_GMEDINAEntities();
                tbHistorialAmonestaciones tbHistorialAmonestaciones = db.tbHistorialAmonestaciones.Find(id);
                db.tbHistorialAmonestaciones.Remove(tbHistorialAmonestaciones);
                db.SaveChanges();
            }
            catch
            {

            }
           
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
