using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
//using System.Transactions;


namespace ERP_GMEDINA.Controllers
{
    public class HistorialPermisosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        // GET: Areas
        public ActionResult Index()
        {
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
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
                    var tbHistorialPermisos = db.tbHistorialPermisos
                        .Select(
                        t => new
                        {
                            hper_Id = t.hper_Id,
                            hper_Descripcion = t.hper_Observacion,
                            Encargado = t.tbTipoPermisos.tbHistorialPermisos

                                .Select(p => p.tbEmpleados.tbPersonas.per_Nombres + " " + p.tbEmpleados.tbPersonas.per_Apellidos)
                        }
                        )
                        .ToList();
                    return Json(tbHistorialPermisos, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        //public ActionResult ChildRowData(int? id)
        //{
        //    //declaramos la variable de coneccion solo para recuperar los datos necesarios.
        //    //posteriormente es destruida.
        //    List<V_Departamentos> lista = new List<V_Departamentos> { };
        //    using (db = new ERP_GMEDINAEntities())
        //    {
        //        try
        //        {
        //            lista = db.V_Departamentos.Where(x => x.area_Id == id).ToList();
        //        }
        //        catch
        //        {
        //        }
        //    }
        //    return Json(lista, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult llenarDropDowlist()
        {
            var TipoPermisos = new List<object> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    TipoPermisos.Add(new
                    {
                        Id = 0,
                        Descripcion = "**Seleccione una opción**"
                    });
                    TipoPermisos.AddRange(db.tbTipoPermisos
                    .Select(tabla => new { Id = tabla.tper_Id, Descripcion = tabla.tper_Descripcion })
                    .ToList());
                }
                catch
                {
                    return Json("-2", 0);
                }

            }
            var result = new Dictionary<string, object>();
            result.Add("Tipo Permiso", TipoPermisos);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
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
            List<tbHistorialPermisos> HistorialPermisos = new List<tbHistorialPermisos> { };
            ViewBag.hper_Id = new SelectList(HistorialPermisos, "hper_Id", "hper_Descripcion");
            return View();
        }
        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(tbHistorialPermisos tbHistorialPermisos, tbPersonas[] tbPersonas)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            string result = "";
            //en esta area ingresamos el registro con el procedimiento almacenado
            try
            {
                if (tbHistorialPermisos.hper_Id == 0 || tbHistorialPermisos.tbTipoPermisos.tper_Descripcion == "" || tbHistorialPermisos.hper_Observacion == "")
                {
                    return Json("-2", JsonRequestBehavior.AllowGet);
                }
                foreach (var item in tbPersonas)
                {
                    if (item.per_Nombres == "" || item.per_Apellidos == "")
                    {
                        return Json("-2", JsonRequestBehavior.AllowGet);
                    }
                }
                var Usuario = (tbUsuario)Session["Usuario"];
                //using (var scope = new TransactionScope())
                //{
                //    using (db = new ERP_GMEDINAEntities())
                //    {
                //        var list = db.UDP_RRHH_tbHistorialPermisos_Insert(
                //                                                tbHistorialPermisos.hper_Id,
                //                                                tbHistorialPermisos.tbTipoPermisos.tper_Descripcion,
                //                                                tbHistorialPermisos.hper_fechaInicio,
                //                                                Usuario.usu_Id,
                //                                                DateTime.Now);
                //        foreach (UDP_RRHH_tbAreas_Insert_Result item in list)
                //        {
                //            tbAreas.area_Id = int.Parse(item.MensajeError.ToString());
                //        }
                //        if (tbAreas.area_Id == -2)
                //        {
                //            return Json("-2", JsonRequestBehavior.AllowGet);
                //        }
                //        foreach (var item in tbDepartamentos)
                //        {

                //        }

                //    }
                //}

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                result = "-2";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
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
        //            Sucursales = db.tbSucursales.ToList();
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
        //    return View(new cAreas
        //    {
        //        suc_Id = tbAreas.suc_Id,
        //        area_Descripcion = tbAreas.area_Descripcion
        //    });
        //}
        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "area_Id,car_Id,suc_Id,area_Descripcion,area_Estado,area_Razoninactivo,area_Usuariocrea,area_Fechacrea,area_Usuariomodifica,area_Fechamodifica")] tbHistorialPermisos tbHistorialPermisos)
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

