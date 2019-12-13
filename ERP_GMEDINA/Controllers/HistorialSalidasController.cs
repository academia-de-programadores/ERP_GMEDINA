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
            ViewBag.rsal_Id = new SelectList(db.tbRazonSalidas, "rsal_Id", "rsal_Descripcion");
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
                            hsal_Observacion = t.hsal_Observacion
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
            return View();
        }
        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //public ActionResult Create(tbAreas tbAreas, tbDepartamentos[] tbDepartamentos)
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
        //public ActionResult Edit(int? id)
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
        //        List<tbSucursales> Sucursales = null;
        //        try
        //        {
        //            tbAreas = db.tbAreas.Find(id);
        //            Sucursales = new List<tbSucursales> { new tbSucursales {suc_Id=tbAreas.suc_Id } }; //db.tbSucursales.ToList();
        //            ViewBag.suc_Id = new SelectList(Sucursales, "suc_Id", "suc_Descripcion");
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
        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //public ActionResult Edit([Bind(Include = "area_Id,car_Id,suc_Id,area_Descripcion,area_Estado,area_Razoninactivo,area_Usuariocrea,area_Fechacrea,area_Usuariomodifica,area_Fechamodifica")] tbAreas tbAreas)
        //{
        //    //declaramos la variable de coneccion solo para recuperar los datos necesarios.
        //    //posteriormente es destruida.
        //    string result = "";
        //    using (db = new ERP_GMEDINAEntities())
        //    {
        //        try
        //        {
        //            //en esta area actualizamos el registro con el procedimiento almacenado
        //        }
        //        catch
        //        {
        //            result = "-2";
        //        }
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
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