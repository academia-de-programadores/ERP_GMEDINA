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
	public class SueldosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: /Sueldos/
        public ActionResult Index()        
		{           
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            List<tbSueldos> tbSueldos = new List<tbSueldos> { };
            return View(tbSueldos);
        }

        [HttpPost]
        public ActionResult llenarTabla()
        {
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var tbsueldos = db.V_Sueldos
                        .Select(
                        t => new
                        {
                            Id = t.Id,
                            Identidad = t.Identidad,
                            Nombre = t.Nombre,
                            Sueldo = t.Sueldo,
                            Tipo_Moneda = t.Tipo_Moneda,
                            Cuenta = t.Cuenta,
                            Sueldo_Anterior = t.Sueldo_Anterior,
                            Area = t.Area,
                            Cargo = t.Cargo,
                            Usuario_Nombre = t.Usuario_Nombre,
                            Usuario_Crea = t.Usuario_Crea,
                            Fecha_Crea = t.Fecha_Crea,
                            Usuario_Modifica = t.Usuario_Modifica,
                            Fecha_Modifica = t.Fecha_Modifica

                        }

                        )
                        .ToList();
                    return Json(tbsueldos, JsonRequestBehavior.AllowGet);
                        
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);

            }
        }


        public ActionResult ChildRowData(int? id)
        {
            List<V_Sueldos> lista = new List<V_Sueldos> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    lista = db.V_Sueldos.Where(x => x.Id == id).ToList();
                }
                catch
                {

                }
            }
            return Json(lista, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Details(int? id)
        {
            if (id== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbSueldos tbSueldos = null;
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    tbSueldos = db.tbSueldos.Find(id);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    
                }
            }
            if (tbSueldos == null)
            {
                return HttpNotFound();

            }
            return View(tbSueldos);
        }


        // POST: /Sueldos/Create




        [HttpPost]
        public ActionResult Create(V_Sueldos vsueldos)
        {
            string msj = "";
            using (db = new ERP_GMEDINAEntities())

            {
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbSueldos_Insert(vsueldos.Id_Empleado,
                                                            vsueldos.Id_Amonestacion,
                                                            vsueldos.Sueldo,
                                                            vsueldos.Sueldo_Anterior,
                                                            usuario.usu_Id,
                                                            DateTime.Now);

                    foreach (UDP_RRHH_tbSueldos_Insert_Result item in list)
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

            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }

        // GET: /Sueldos//Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            V_Sueldos VSueldos = null;

            try
            {
                VSueldos = db.V_Sueldos.Find(id);
                if (VSueldos == null || !VSueldos.Estado)
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
            var sueldos = new V_Sueldos
            {
                Id = VSueldos.Id,
                Id_Empleado = VSueldos.Id_Empleado,
                Id_Amonestacion = VSueldos.Id_Amonestacion,
                Identidad = VSueldos.Identidad,
                Nombre = VSueldos.Nombre,
                Sueldo = VSueldos.Sueldo,
                Tipo_Moneda = VSueldos.Tipo_Moneda,
                Cuenta = VSueldos.Cuenta,
                Sueldo_Anterior = VSueldos.Sueldo_Anterior,
                Area = VSueldos.Area,
                Cargo = VSueldos.Cargo,
                Usuario_Nombre = VSueldos.Usuario_Nombre,
                Usuario_Crea = VSueldos.Usuario_Crea,
                Fecha_Crea = VSueldos.Fecha_Crea,
                Usuario_Modifica = VSueldos.Usuario_Modifica,
                Fecha_Modifica = VSueldos.Fecha_Modifica,
                Estado = VSueldos.Estado,
                RazonInactivo = VSueldos.RazonInactivo
            };
            return Json(sueldos, JsonRequestBehavior.AllowGet);
        }
          
        [HttpPost]
        public JsonResult Edit(V_Sueldos vSueldos)
        {
            string msj = "";
            if (vSueldos.Id != 0 && vSueldos.Id_Empleado != 0 && vSueldos.Id_Amonestacion != 0 && vSueldos.Sueldo !=0  && vSueldos.Sueldo_Anterior !=0)            
			{
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbSueldos_Insert(vSueldos.Id_Empleado, vSueldos.Id_Amonestacion, vSueldos.Sueldo, vSueldos.Sueldo_Anterior, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbSueldos_Insert_Result item in list)
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
		// GET: /Sueldos//Delete/5
        [HttpPost]
        public ActionResult Delete(tbSueldos tbSueldos)
        {
            string msj = "";
            if (tbSueldos.sue_Id != 0 && tbSueldos.sue_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbSueldos_Delete(id, tbSueldos.sue_RazonInactivo, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbSueldos_Delete_Result item in list)
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
            return Json(msj.Substring(0, 2),JsonRequestBehavior.AllowGet);
        }
        protected tbUsuario IsNull(tbUsuario valor)
        {
            if (valor!=null)
            {
                return valor;
            }
            else
            {
                return new tbUsuario {usu_NombreUsuario="" };
            }
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
