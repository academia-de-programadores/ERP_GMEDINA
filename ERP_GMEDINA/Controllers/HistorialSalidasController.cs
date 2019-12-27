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
    public class HistorialSalidasController : Controller
    {
        private ERP_GMEDINAEntities db = null;
        // GET: Areas
        public ActionResult Index()
        {
            //ViewBag.tsal_Id = new SelectList(db.tbTipoSalidas, "tsal_Id", "tsal_Descripcion");
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            var tbHistorialSalidas = new List<tbHistorialSalidas> { };
            return View(tbHistorialSalidas);
        }
        public ActionResult llenarTabla()
        {
            //string estado = 
            try
            {
                //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                //posteriormente es destruida.
                using (db = new ERP_GMEDINAEntities())
                {
                   

                  var V_tbHistorialSalidas_completa = db.V_tbHistorialSalidas_completa
                        .Select(
                        t => new
                        {
                            // p => (p.Date.Value == null ? p.Date.Value : p.Date.Value.Date) == SelectedDate.Date

                            hsal_Id = t.hsal_Id,
                            tsal_Id = t.tsal_Id,
                            tsal_Descripcion = t.tsal_Descripcion,
                            rsal_Id = t.rsal_Id,
                            rsal_Descripcion = t.rsal_Descripcion,
                            per_Nombres = t.per_Nombres + " " + t.per_Apellidos,
                            per_CorreoElectronico = t.per_CorreoElectronico,
                            per_Telefono = t.per_Telefono,
                            per_Direccion = t.per_Direccion,
                            per_Edad = t.per_Edad,
                            per_EstadoCivil = t.per_EstadoCivil,
                            hsal_Observacion = t.hsal_Observacion,
                            hsal_FechaSalida = t.hsal_FechaSalida
                        }
                        )
                        .ToList();
                    return Json(V_tbHistorialSalidas_completa, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {

                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ChildRowData(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<V_tbHistorialSalidas_completa> lista = new List<V_tbHistorialSalidas_completa> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    lista = db.V_tbHistorialSalidas_completa.Where(x => x.hsal_Id == id).ToList();
                }
                catch
                {
                }
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        //--------------------------------------------DESPLEGABLES--------------------------------------------
        //Tipo salidas
        public ActionResult llenarDropDowlistTipoSalida()
        {
            var TipoSalidas = new List<object> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    TipoSalidas.Add(new
                    {
                        Id = 0,
                        Descripcion = "**Seleccione una opción**"
                    });
                    TipoSalidas.AddRange(db.tbTipoSalidas
                    .Select(tabla => new {
                        Id = tabla.tsal_Id,
                        Descripcion = tabla.tsal_Descripcion
                    })
                    .ToList());
                }
                catch
                {
                    return Json("-2", 0);
                }

            }
            var result = new Dictionary<string, object>();
            result.Add("TipoSalidas", TipoSalidas);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //Razon salidas
        public ActionResult llenarDropDowlistRazonSalida()
        {
            var RazonSalidas = new List<object> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    RazonSalidas.Add(new
                    {
                        Id = 0,
                        Descripcion = "**Seleccione una opción**"
                    });
                    RazonSalidas.AddRange(db.tbRazonSalidas
                    .Select(tabla => new {
                        Id = tabla.rsal_Id,
                        Descripcion = tabla.rsal_Descripcion
                    })
                    .ToList());
                }
                catch(Exception ex)
                {
                    return Json("-2", 0);
                }

            }
            var result = new Dictionary<string, object>();
            result.Add("RazonSalidas", RazonSalidas);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //Empleados
        public ActionResult llenarDropDowlistEmpleados()
        {
            var Empleados = new List<object> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    Empleados.Add(new
                    {
                        Id = 0,
                        Descripcion = "**Seleccione una opción**"
                    });
                    Empleados.AddRange(db.V_HistorialSalidas_Empleados
                    .Select(tabla => new {
                        Id = tabla.Id,
                        Descripcion = tabla.Nombre
                    })
                    .ToList());
                }
                catch (Exception ex)
                {
                    return Json("-2", 0);
                }

            }
            var result = new Dictionary<string, object>();
            result.Add("Empleados", Empleados);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //--------------------------------------------cerrarDESPLEGABLES--------------------------------------------

        // GET: Areas/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    //declaramos la variable de coneccion solo para recuperar los datos necesarios.
        //    //posteriormente es destruida.
        //    tbAreas tbAreas = null;
        //    using (db = new ERP_GMEDINAEntities())
        //    {
        //        try
        //        {
        //            tbAreas = db.tbAreas.Find(id);
        //        }
        //        catch
        //        {

        //        }
        //    }
        //    if (tbAreas == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tbAreas);
        //}
        // GET: Areas/Create
        public ActionResult Create()
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<tbTipoSalidas> TipoSalidas = new List<tbTipoSalidas> { };
            ViewBag.Tsal_Id = new SelectList(TipoSalidas, "Tsal_Id", "Tsal_Descripcion");
            List<tbRazonSalidas> RazonSalidas = new List<tbRazonSalidas> { };
            ViewBag.rsal_Id = new SelectList(RazonSalidas, "rsal_Id", "rsal_Descripcion");
            List<V_HistorialSalidas_Empleados> Empleados = new List<V_HistorialSalidas_Empleados> { };
            ViewBag.Id = new SelectList(Empleados, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public ActionResult Create(tbHistorialSalidas tbHistorialSalidas, tbEmpleados[] tbEmpleados)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            //en esta area ingresamos el registro con el procedimiento almacenado
            try
            {
                db = new ERP_GMEDINAEntities();
                using (var transaction = db.Database.BeginTransaction())
                {
                    foreach (tbEmpleados em in tbEmpleados)
                    {
                        var observacion = tbHistorialSalidas.hsal_Observacion == null ? "Ninguna" : tbHistorialSalidas.hsal_Observacion;
                        //var razon = em.emp_RazonInactivo == null ? "Ninguna" : em.emp_RazonInactivo;
                        var emp = db.UDP_RRHH_tbHistorialSalidas_Insert(
                        em.emp_Id,
                        tbHistorialSalidas.tsal_Id,
                        tbHistorialSalidas.rsal_Id,
                        tbHistorialSalidas.hsal_FechaSalida, 
                        observacion,
                        em.emp_RazonInactivo,
                        Usuario.usu_Id,
                        DateTime.Now);
                        string mensajeDB = "";
                        foreach (UDP_RRHH_tbHistorialSalidas_Insert_Result i in emp)
                        {
                            mensajeDB = i.MensajeError.ToString();
                        }
                        if (mensajeDB == "-1")
                        {
                            return Json("-2", JsonRequestBehavior.AllowGet);
                        }
                    }
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("-2", JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            string result = "";
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    //en esta area Inavilitamos el registro con el procedimiento almacenado
                }
                catch
                {
                    result = "-2";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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