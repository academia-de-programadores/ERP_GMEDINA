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
    public class HistorialPermisosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Areas
        public ActionResult Index()
        {
            var tbHistorialPermisos = new List<tbHistorialPermisos> { };
            return View(tbHistorialPermisos);
        }
        public ActionResult llenarTabla()
        {

            try
            {
                //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                //posteriormente es destruida.
                using (db = new ERP_GMEDINAEntities())
                {
                    var V_HistorialPermisos = db.V_HistorialPermisos
                        .Select(
                        t => new
                        {
                            hper_Id = t.Id,
                            tper_Id = t.Id_Permiso,
                            tper_Descripcion = t.Descripcion_Permiso,
                            hper_fechaInicio = t.Fecha_Inicial,
                            hper_fechaFin = t.Fecha_Fin,
                            hper_Duracion=t.Duracion,
                            hper_Justificado=t.Justificado,
                            per_Nombres = t.Nombre_Completo,
                        }
                        )
                        .ToList();
                    return Json(V_HistorialPermisos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ChildRowData(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<V_HistorialPermisos> lista = new List<V_HistorialPermisos> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    lista = db.V_HistorialPermisos.Where(x => x.Id == id).ToList();
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
            var tbTipoPermisos = new List<object> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    tbTipoPermisos.Add(new
                    {
                        Id = 0,
                        Descripcion = "**Seleccione una opción**"
                    });
                    tbTipoPermisos.AddRange(db.tbTipoPermisos
                    .Select(tabla => new {
                        Id = tabla.tper_Id,
                        Descripcion = tabla.tper_Descripcion
                    })
                    .ToList());
                }
                catch
                {
                    return Json("-2", 0);
                }

            }
            var result = new Dictionary<string, object>();
            result.Add("tbTipoPermisos", tbTipoPermisos);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //Razon salidas
        //public ActionResult llenarDropDowlistRazonSalida()
        //{
        //    var tbRazonSalidas = new List<object> { };
        //    using (db = new ERP_GMEDINAEntities())
        //    {
        //        try
        //        {
        //            tbRazonSalidas.Add(new
        //            {
        //                Id = 0,
        //                Descripcion = "**Seleccione una opción**"
        //            });
        //            tbRazonSalidas.AddRange(db.tbRazonSalidas
        //            .Select(tabla => new {
        //                Id = tabla.rsal_Id,
        //                Descripcion = tabla.rsal_Descripcion
        //            })
        //            .ToList());
        //        }
        //        catch
        //        {
        //            return Json("-2", 0);
        //        }

        //    }
        //    var result = new Dictionary<string, object>();
        //    result.Add("tbRazonSalidas", tbRazonSalidas);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        //--------------------------------------------cerrarDESPLEGABLES--------------------------------------------

        // GET: Areas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            tbHistorialPermisos tbHistorialPermisos = null;
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    tbHistorialPermisos = db.tbHistorialPermisos.Find(id);
                }
                catch
                {

                }
            }
            if (tbHistorialPermisos == null)
            {
                return HttpNotFound();
            }
            return View(tbHistorialPermisos);
        }
        // GET: Areas/Create
        public ActionResult Create()
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<tbTipoPermisos> TipoPermisos = new List<tbTipoPermisos> { };
            ViewBag.tper_Id = new SelectList(TipoPermisos, "tper_Id", "tper_Descripcion");
            return View();
        }
        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //public ActionResult Create(tbHistorialPermisos tbAreas, tbDepartamentos[] tbDepartamentos)
        //{
        //    //declaramos la variable de coneccion solo para recuperar los datos necesarios.
        //    //posteriormente es destruida.
        //    string result = "";
        //    using (db = new ERP_GMEDINAEntities())
        //    {
        //        //en esta area ingresamos el registro con el procedimiento almacenado
        //        try
        //        {

        //        }
        //        catch
        //        {
        //            result = "-2";
        //        }
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        // GET: Areas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            tbHistorialPermisos tbHistorialPermisos = null;
            using (db = new ERP_GMEDINAEntities())
            {
                List<tbEmpleados> Empleados = null;
                try
                {
                    tbHistorialPermisos = db.tbHistorialPermisos.Find(id);
                    Empleados = new List<tbEmpleados> { new tbEmpleados { emp_Id = tbHistorialPermisos.emp_Id } }; //db.tbSucursales.ToList();
                    ViewBag.emp_Id = new SelectList(Empleados, "emp_Id", "emp_Descripcion");
                }
                catch
                {
                }
            }
            if (tbHistorialPermisos == null)
            {
                return HttpNotFound();
            }
            return View(tbHistorialPermisos);
        }
        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "hper_Id, emp_Id, tper_Id, hper_fechaInicio, hper_fechaFin, hper_Duracion, hper_Observacion, hper_Justificado, hper_PorcentajeIndemnizado, hper_Estado, hper_RazonInactivo, hper_UsuarioCrea, hper_FechaCrea, hper_UsuarioModifica, hper_FechaModifica")] tbHistorialPermisos tbHistorialPermisos)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            string result = "";
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    //en esta area actualizamos el registro con el procedimiento almacenado
                }
                catch
                {
                    result = "-2";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // POST: Areas/Delete/5
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

