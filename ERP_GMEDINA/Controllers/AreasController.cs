using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Transactions;

namespace ERP_GMEDINA.Controllers
{
    public class AreasController : Controller
    {
        private ERP_GMEDINAEntities db = null;

        // GET: Areas
        public ActionResult Index()
        {
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            var tbAreas = new List<tbAreas> { };
            return View(tbAreas);
        }
        public ActionResult llenarTabla()
        {
            try
            {
                //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                //posteriormente es destruida.
                using (db = new ERP_GMEDINAEntities())
                {
                    var tbAreas = db.tbAreas
                        .Select(
                        t => new
                        {
                            area_Id = t.area_Id,
                            area_Descripcion = t.area_Descripcion,
                            Encargado = t.tbCargos.tbEmpleados
                                .Select(p => p.tbPersonas.per_Nombres + " " + p.tbPersonas.per_Apellidos)
                        }
                        )
                        .ToList();
                    return Json(tbAreas, JsonRequestBehavior.AllowGet);
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
            List<V_Departamentos> lista = new List<V_Departamentos> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    lista = db.V_Departamentos.Where(x => x.area_Id == id).ToList();
                }
                catch
                {
                }
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public ActionResult llenarDropDowlist()
        {
            var Sucursales = new List<object> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    Sucursales.Add(new
                    {
                        Id = 0,
                        Descripcion = "**Seleccione una opción**"
                    });
                    Sucursales.AddRange(db.tbSucursales
                    .Select(tabla => new { Id = tabla.suc_Id, Descripcion = tabla.suc_Descripcion })
                    .ToList());
                }
                catch
                {
                    return Json("-2", 0);
                }

            }
            var result = new Dictionary<string, object>();
            result.Add("Sucursales", Sucursales);
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
            tbAreas tbAreas = null;
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    tbAreas = db.tbAreas.Find(id);
                }
                catch
                {

                }
            }
            if (tbAreas == null)
            {
                return HttpNotFound();
            }
            return View(tbAreas);
        }
        // GET: Areas/Create
        public ActionResult Create()
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<tbSucursales> Sucursales = new List<tbSucursales> { };
            ViewBag.suc_Id = new SelectList(Sucursales, "suc_Id", "suc_Descripcion");
            return View();
        }
        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(tbAreas tbAreas, tbDepartamentos[] tbDepartamentos)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            string result = "";
            //en esta area ingresamos el registro con el procedimiento almacenado
            try
            {
                if (tbAreas.suc_Id == 0 || tbAreas.tbCargos.car_Descripcion == "" || tbAreas.area_Descripcion == "")
                {
                    return Json("-2", JsonRequestBehavior.AllowGet);
                }
                foreach (var item in tbDepartamentos)
                {
                    if (item.depto_Descripcion == "" || item.tbCargos.car_Descripcion=="")
                    {
                        return Json("-2", JsonRequestBehavior.AllowGet);
                    }
                }   
                var Usuario = (tbUsuario)Session["Usuario"];
                using (var scope = new TransactionScope())
                {
                    using (db = new ERP_GMEDINAEntities())
                    {
                        var list = db.UDP_RRHH_tbAreas_Insert(
                                                                tbAreas.suc_Id,
                                                                tbAreas.tbCargos.car_Descripcion,
                                                                tbAreas.area_Descripcion,
                                                                Usuario.usu_Id,
                                                                DateTime.Now);
                        foreach (UDP_RRHH_tbAreas_Insert_Result item in list)
                        {
                            tbAreas.area_Id = int.Parse(item.MensajeError.ToString());
                        }
                        if (tbAreas.area_Id==-2)
                        {
                            return Json("-2", JsonRequestBehavior.AllowGet);
                        }
                        foreach (var item in tbDepartamentos)
                        {
                        
                        }

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
        // GET: Areas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            tbAreas tbAreas = null;
            using (db = new ERP_GMEDINAEntities())
            {
                List<tbSucursales> Sucursales = null;
                try
                {
                    tbAreas = db.tbAreas.Find(id);
                    Sucursales = db.tbSucursales.ToList();
                    ViewBag.suc_Id = new SelectList(Sucursales, "suc_Id", "suc_Descripcion");
                }
                catch
                {
                }
            }
            if (tbAreas == null)
            {
                return HttpNotFound();
            }
            return View(new cAreas{
                suc_Id =tbAreas.suc_Id,
                area_Descripcion =tbAreas.area_Descripcion });
        }
        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "area_Id,car_Id,suc_Id,area_Descripcion,area_Estado,area_Razoninactivo,area_Usuariocrea,area_Fechacrea,area_Usuariomodifica,area_Fechamodifica")] tbAreas tbAreas)
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
