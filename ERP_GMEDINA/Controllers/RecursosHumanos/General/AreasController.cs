using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Attribute;
using ERP_GMEDINA.Models;
using ERP_GMEDINA.Attribute;

namespace ERP_GMEDINA.Controllers
{
    public class AreasController : Controller
    {
        private ERP_GMEDINAEntities db = null;
        Models.Helpers Function = new Models.Helpers();

        //GET: Areas
        [SessionManager("Areas/Index")]
        public ActionResult Index(){
            tbAreas tbAreas = new tbAreas { };
            return View(tbAreas);
        }

        [SessionManager("Areas/Index")]
        [HttpPost]
        public ActionResult llenarTabla()
        {
            try
            {
                db = new ERP_GMEDINAEntities();

                //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                //posteriormente es destruida.
                var tbAreas = db.tbAreas
                    .Select(
                    t => new
                    {
                        area_Id = t.area_Id,
                        area_Descripcion = t.area_Descripcion,
                        Sucursales = t.tbSucursales.suc_Descripcion,
                        Encargado = t.tbCargos.tbEmpleados
                            .Select(p => p.tbPersonas.per_Nombres + " " + p.tbPersonas.per_Apellidos),
                            Empleados=t.tbEmpleados.Count,
                        area_Estado = t.area_Estado
                    }
                    )
                    .ToList();
                return Json(tbAreas, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManager("Areas/Index")]
        [HttpPost]
        public ActionResult ChildRowData(int id)
        {
            try
            {
                //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                //posteriormente es destruida.
                using (db = new ERP_GMEDINAEntities())
                {
                    var lista = db.tbDepartamentos
                        .Where(x => x.area_Id == id && x.depto_Estado == true)
                        .Select(depto =>
                        new
                        {
                            car_Descripcion = depto.tbCargos.car_Descripcion,
                            depto_Descripcion = depto.depto_Descripcion,
                            Empleados = depto.tbEmpleados.Count,
                            persona = new
                            {
                                per_NombreCompleto = depto.tbCargos.tbEmpleados.Select(persona => persona.tbPersonas.per_Nombres + " " + persona.tbPersonas.per_Apellidos),
                                per_Telefono = depto.tbCargos.tbEmpleados.Select(persona => persona.tbPersonas.per_Telefono),
                                per_CorreoElectronico = depto.tbCargos.tbEmpleados.Select(persona => persona.tbPersonas.per_CorreoElectronico),
                            }
                        }).ToList();
                    return Json(lista, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json(new List<tbDepartamentos> { }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult cargarChild(int id)
        {
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var lista = db.tbDepartamentos
                        .Where(x => x.area_Id == id)
                        .Select(depto =>
                        new
                        {
                            depto_Id = depto.depto_Id,
                            car_Id = depto.tbCargos.car_Id,
                            car_Descripcion = depto.tbCargos.car_Descripcion,
                            depto_Descripcion = depto.depto_Descripcion,
                            depto_Estado = depto.depto_Estado
                        }).ToList();
                    return Json(lista, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult llenarDropDowlist()
        {
            var Sucursales = new List<object> { };
            try
            {
                using (db = new ERP_GMEDINAEntities())
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
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
            var result = new Dictionary<string, object>();
            result.Add("suc_Id", Sucursales);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //GET: Areas/Details/5
        [SessionManager("Areas/Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAreas tbAreas = null;
            try
            {
                //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                //posteriormente es destruida.
                db = new ERP_GMEDINAEntities();
                tbAreas = db.tbAreas.Find(id);
            }
            catch
            {
                return HttpNotFound();
            }
            return View(tbAreas);
        }
        //GET: Areas/Create
        [SessionManager("Areas/Create")]
        public ActionResult Create()
        {

            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<tbSucursales> Sucursales = new List<tbSucursales> { };
            ViewBag.suc_Id = new SelectList(Sucursales, "suc_Id", "suc_Descripcion");
            return View();
        }
         //POST: Areas/Create
         //To protect from overposting attacks, please enable the specific properties you want to bind to, for
         //more details see http:go.microsoft.com/fwlink/?LinkId=317598.
         [SessionManager("Areas/Create")]
        [HttpPost]
        public ActionResult Create(tbAreas tbAreas, tbDepartamentos[] tbDepartamentos)
        {
           // declaramos la variable de coneccion solo para recuperar los datos necesarios.
           // posteriormente es destruida.
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
           // en esta area ingresamos el registro con el procedimiento almacenado
            try
            {
                tbDepartamentos = tbDepartamentos == null ? new tbDepartamentos[] { } : tbDepartamentos;
                db = new ERP_GMEDINAEntities();
                using (var transaction = db.Database.BeginTransaction())
                {
                    var cargo = db.UDP_RRHH_tbCargos_Insert(
                        tbAreas.car_Descripcion,
                        tbAreas.car_SalarioMinimo,
                        tbAreas.car_SalarioMaximo,
                         (int)Session["UserLogin"],
                         Function.DatetimeNow()
                        );
                    foreach (UDP_RRHH_tbCargos_Insert_Result item in cargo)
                    {
                        var resultado = item.MensajeError + "  ";
                        if (resultado.Substring(0, 2) == "-1")
                        {
                            return Json(new { codigo = "-3", input = "car_Descripcion", result = tbAreas.car_Descripcion }, JsonRequestBehavior.AllowGet);
                        }
                        tbAreas.car_Id = int.Parse(item.MensajeError);
                    }
                    var list = db.UDP_RRHH_tbAreas_Insert(
                        tbAreas.suc_Id,
                        tbAreas.area_Descripcion,
                        tbAreas.car_Id,
                        (int)Session["UserLogin"],
                        Function.DatetimeNow());
                    foreach (UDP_RRHH_tbAreas_Insert_Result item in list)
                    {
                        var resultado = item.MensajeError + "  ";
                        if (resultado.Substring(0, 2) == "-1")
                        {
                            return Json(new { codigo = "-2", input = "area_Descripcion", result = tbAreas.area_Descripcion }, JsonRequestBehavior.AllowGet);
                        }
                        tbAreas.area_Id = int.Parse(item.MensajeError);
                    }
                    foreach (tbDepartamentos item in tbDepartamentos)
                    {
                        var deptocargo = db.UDP_RRHH_tbCargos_Insert(
                                                               item.tbCargos.car_Descripcion,
                                                               item.tbCargos.car_SueldoMinimo,
                                                               item.tbCargos.car_SueldoMaximo,
                                                                (int)Session["UserLogin"],
                                                                Function.DatetimeNow()
                                                             );
                        foreach (UDP_RRHH_tbCargos_Insert_Result i in deptocargo)
                        {
                            var resultadod = i.MensajeError + "  ";
                            if (resultadod.Substring(0, 2) == "-1")
                            {
                                return Json(new { codigo = "-4", input = "car_Descripcion", result = item.tbCargos.car_Descripcion }, JsonRequestBehavior.AllowGet);
                            }
                            item.tbCargos.car_Id = int.Parse(i.MensajeError);
                        }
                        var depto = db.UDP_RRHH_tbDepartamentos_Insert(
                            tbAreas.area_Id,
                            item.depto_Descripcion,
                            item.tbCargos.car_Id,
                            (int)Session["UserLogin"],
                            Function.DatetimeNow());
                        string mensajeDB = "";
                        foreach (UDP_RRHH_tbDepartamentos_Insert_Result i in depto)
                        {
                            mensajeDB = i.MensajeError.ToString();
                        }
                        var resultado = mensajeDB + "  ";
                        if (resultado.Substring(0, 2) == "-1")
                        {
                            return Json("-4", JsonRequestBehavior.AllowGet);
                        }
                    }
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json(new { codigo = "-2" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { codigo = result }, JsonRequestBehavior.AllowGet);
        }
        [SessionManager("Areas/Edit")]
        //GET: Areas/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<tbSucursales> Sucursales = null;
            try
            {
                //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                //posteriormente es destruida.
                using (db = new ERP_GMEDINAEntities())
                {
                    Session["area_Id"] = id;
                    var tbAreas = db.tbAreas
                        .Select(tabla => new cAreas
                        {
                            area_Id = tabla.area_Id,
                            suc_Id = tabla.suc_Id,
                            area_Descripcion = tabla.area_Descripcion,
                            car_Descripcion = tabla.tbCargos.car_Descripcion,
                            area_Estado = tabla.area_Estado
                        }).ToList()
                        .Where(x => x.area_Id == id)
                        .First();
                    Sucursales = db.tbSucursales.Where(t=>t.suc_Estado==true).ToList();
                    ViewBag.suc_Id = new SelectList(Sucursales, "suc_Id", "suc_Descripcion");
                    if (tbAreas.area_Estado == false)
                    {
                        return HttpNotFound();
                    }
                    if (tbAreas == null)
                    {
                        return HttpNotFound();
                    }
                    return View(tbAreas);
                }
            }
            catch
            {
                return HttpNotFound();
            }
        }
        // POST: Areas/Edit/5
         //To protect from overposting attacks, please enable the specific properties you want to bind to, for
         //more details see http:go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [SessionManager("Areas/Edit")]
        public ActionResult Edit(cAreas cAreas, cDepartamentos[] Departamentos, cDepartamentos[] inactivar)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            string result = "";
            cAreas.area_Id = (int)Session["area_Id"];
            var Usuario = (tbUsuario)Session["Usuario"];
            //en esta area ingresamos el registro con el procedimiento almacenado
            try
            {
                db = new ERP_GMEDINAEntities();
                using (var transaction = db.Database.BeginTransaction())
                {
                    var list = db.UDP_RRHH_tbAreas_Update(
                        cAreas.area_Id,
                        cAreas.suc_Id,
                        cAreas.area_Descripcion,
                        (int)Session["UserLogin"],
                        Function.DatetimeNow());
                    foreach (UDP_RRHH_tbAreas_Update_Result item in list)
                    {
                        if (item.MensajeError == "-1")
                        {
                            return Json(new { codigo = "-1" }, JsonRequestBehavior.AllowGet);
                        }
                        cAreas.area_Id = int.Parse(item.MensajeError);
                    }
                    inactivar = inactivar == null ? new cDepartamentos[] { } : inactivar;
                    Departamentos = Departamentos == null ? new cDepartamentos[] { } : Departamentos;
                    foreach (cDepartamentos item in inactivar)
                    {
                        var depto = db.UDP_RRHH_tbDepartamentos_Delete(
                                item.depto_Id,
                                item.depto_RazonInactivo,
                                (int)Session["UserLogin"],
                                Function.DatetimeNow());
                        foreach (UDP_RRHH_tbDepartamentos_Delete_Result dep in depto)
                        {
                            dep.ToString();
                        }
                    }
                    foreach (cDepartamentos item in Departamentos)
                    {
                        if (item.Accion == "i")
                        {
                            var deptocargo = db.UDP_RRHH_tbCargos_Insert(
                                                               item.car_Descripcion,
                                                               item.car_SalarioMinimo,
                                                               item.car_SalarioMaximo,
                                                                (int)Session["UserLogin"],
                                                                Function.DatetimeNow()
                                                             );
                            foreach (UDP_RRHH_tbCargos_Insert_Result i in deptocargo)
                            {
                                var resultadod = i.MensajeError + "  ";
                                if (resultadod.Substring(0, 2) == "-1")
                                {
                                    return Json(new { codigo = "-3", Accion = "i" }, JsonRequestBehavior.AllowGet);
                                }
                                item.car_Id = int.Parse(i.MensajeError);
                            }
                            var depto = db.UDP_RRHH_tbDepartamentos_Insert(
                                cAreas.area_Id,
                                item.depto_Descripcion,
                                item.car_Id,
                                 (int)Session["UserLogin"],
                                 Function.DatetimeNow());
                            string mensajeDB = "";
                            foreach (UDP_RRHH_tbDepartamentos_Insert_Result i in depto)
                            {
                                mensajeDB = i.MensajeError.ToString();
                            }
                            if (mensajeDB == "-1")
                            {
                                return Json(new { codigo = "-4", Accion = "i" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else if (item.Accion == "e")
                        {
                            var depto = db.UDP_RRHH_tbDepartamentos_Update(
                                item.depto_Id,
                                cAreas.area_Id,
                                item.depto_Descripcion,
                                 (int)Session["UserLogin"],
                                 Function.DatetimeNow());
                            string mensajeDB = "";
                            foreach (UDP_RRHH_tbDepartamentos_Update_Result i in depto)
                            {
                                mensajeDB = i.MensajeError.ToString();
                            }
                            if (mensajeDB == "-1")
                            {
                                return Json(new { codigo = "-4", Accion = "e" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else if (item.Accion == "a")
                        {
                            var depto = db.UDP_RRHH_tbDepartamentos_Restore(item.depto_Id,
                                                                             (int)Session["UserLogin"],
                                                                             Function.DatetimeNow());
                            foreach (UDP_RRHH_tbDepartamentos_Restore_Result dep in depto)
                            {
                                dep.ToString();
                            }
                        }
                    }
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json(new { codigo = "-2" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { codigo = result }, JsonRequestBehavior.AllowGet);
        }
         //POST: Areas/Delete/5
        [HttpPost]
        [SessionManager("Areas/Delete")]
        public ActionResult Delete(string area_Razoninactivo)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            string result = "";
            var cAreas =
                new cAreas
                {
                    area_Id = (int)Session["area_Id"],
                    area_Razoninactivo = area_Razoninactivo
                };
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var list = db.UDP_RRHH_tbAreas_Delete(cAreas.area_Id, cAreas.area_Razoninactivo,
                                                                         (int)Session["UserLogin"],
                                                                         Function.DatetimeNow());
                    foreach (UDP_RRHH_tbAreas_Delete_Result item in list)
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

         //POST: Areas/Delete/5
        [HttpPost]
        [SessionManager("Areas/hablilitar")]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var list = db.UDP_RRHH_tbAreas_Restore(id,
                                                         (int)Session["UserLogin"],
                                                         Function.DatetimeNow());
                    foreach (UDP_RRHH_tbAreas_Restore_Result item in list)
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

        [HttpPost]
        public JsonResult Validar(string Descripcion, string Cargo)
        {
            List<object> Resultado = new List<object> { };
            try
            {
                db = new ERP_GMEDINAEntities();
                List<tbDepartamentos> departamento = db.tbDepartamentos
                    .Where(t => t.depto_Descripcion == Descripcion)
                    .ToList();
                if (departamento.Count > 0)
                {
                    Resultado.Add(new { input = "depto_Descripcion", Descripcion = Descripcion });
                }
                List<tbCargos> cargo = db.tbCargos
                    .Where(t => t.car_Descripcion == Cargo)
                    .ToList();
                if (cargo.Count > 0)
                {
                    Resultado.Add(new { input = "car_Descripcion", Descripcion = Cargo });
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
            //new { codigo = "-3", input = "car_Descripcion", result = tbAreas.car_Descripcion }
            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ValidarDepto(string Descripcion)
        {
            List<object> Resultado = new List<object> { };
            try
            {
                db = new ERP_GMEDINAEntities();
                List<tbDepartamentos> departamento = db.tbDepartamentos
                    .Where(t => t.depto_Descripcion == Descripcion)
                    .ToList();
                if (departamento.Count > 0)
                {
                    Resultado.Add(new { input = "depto_Descripcion", Descripcion = Descripcion });
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
            return Json(Resultado, JsonRequestBehavior.AllowGet);
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
