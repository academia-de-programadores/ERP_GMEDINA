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
    public class AreasController : Controller
    {
        private ERP_GMEDINAEntities db = null;

        // GET: Areas
        public ActionResult Index()
        {
            var tbAreas = new List<tbAreas> { };
            return View(tbAreas);
        }
        public ActionResult llenarTabla()
        {
            var tbAreas = new List<tbAreas> { };
            List<tbAreas> lista = new List<tbAreas> { };
            try
            {
                //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                //posteriormente es destruida.
                using (db = new ERP_GMEDINAEntities())
                {
                     lista= db.tbAreas.ToList();
                }
            }
            catch 
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
            foreach (tbAreas item in lista)
            {
                tbAreas.Add(new tbAreas {
                    area_Id =item.area_Id,
                    area_Descripcion = item.area_Descripcion});
            }
            return Json(tbAreas, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChildRowData(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<V_Departamentos> lista =new List<V_Departamentos> { };
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
            List<tbSucursales> Sucursales = null;
            using (db = new ERP_GMEDINAEntities())
            {
                Sucursales = db.tbSucursales.ToList();
            }
            ViewBag.suc_Id = new SelectList(Sucursales, "suc_Id", "suc_Descripcion");
            return View();
        }

        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create( [Bind(Include = "suc_Id,area_Descripcion")] tbAreas tbAreas)
        {

            tbAreas.tbCargos = new tbCargos {car_Descripcion= Request["tbCargos.car_Descripcion"] };
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            string result = "";
            using (db = new ERP_GMEDINAEntities())
            {
                //en esta area ingresamos el registro con el procedimiento almacenado
                try
                {

                }
                catch 
                {
                    result = "-2";
                }
            }
            return Json(result,JsonRequestBehavior.AllowGet);
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
                    tbAreas= db.tbAreas.Find(id);
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
            return View(tbAreas);
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
            return Json(result,JsonRequestBehavior.AllowGet);
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
            if (disposing && db!=null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
