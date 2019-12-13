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
        public ActionResult Create(tbSueldos vsueldos)
        {
            string msj = "";
            using (db = new ERP_GMEDINAEntities())

            {
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                  /*  var list = db.UDP_RRHH_tbSueldos_Insert(vsueldos.Id_Empleado,
                                                            vsueldos.Id_Amonestacion,
                                                            vsueldos.Sueldo,
                                                            vsueldos.Sueldo_Anterior,
                                                            usuario.usu_Id,
                                                            DateTime.Now);

                    foreach (UDP_RRHH_tbSueldos_Insert_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }*/
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

            tbSueldos tbsueldos = null;

            try
            {
                tbsueldos = db.tbSueldos.Find(id);
                if (tbsueldos == null || !tbsueldos.sue_Estado)
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
            var sueldos = new tbSueldos
            {
              sue_Id = tbsueldos.sue_Id,
             emp_Id = tbsueldos.emp_Id,
             tmon_Id = tbsueldos.tmon_Id,
             sue_Cantidad = tbsueldos.sue_Cantidad,
             sue_SueldoAnterior = tbsueldos.sue_SueldoAnterior,
             sue_Estado = tbsueldos.sue_Estado,
             sue_RazonInactivo = tbsueldos.sue_RazonInactivo,
             sue_UsuarioCrea = tbsueldos.sue_UsuarioCrea,
             sue_FechaCrea = tbsueldos.sue_FechaCrea,
             sue_UsuarioModifica = tbsueldos.sue_UsuarioModifica,
             sue_FechaModifica = tbsueldos.sue_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbsueldos.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbsueldos.tbUsuario).usu_NombreUsuario }

            };
            return Json(sueldos, JsonRequestBehavior.AllowGet);
        }
          
        [HttpPost]
        public JsonResult Edit(tbSueldos Sueldos)
        {
            string msj = "";
            if ( Sueldos.sue_Cantidad != 0)
            {
                /* var id = (int)Session["id"];*/
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbSueldos_Insert(Sueldos.emp_Id, Sueldos.tmon_Id, Convert.ToInt16(Sueldos.sue_Cantidad), Sueldos.sue_SueldoAnterior, Usuario.usu_Id, DateTime.Now);
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
