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
    public class SucursalesController : Controller
    {
        private ERP_GMEDINAEntities db = null;
        Models.Helpers Function = new Models.Helpers();

        // GET: Sucursales
        [SessionManager("Sucursales/Index")]
        public ActionResult Index()
        {
            if (Session["Admin"] == null && Session["Usuario"] == null)
            {
                Response.Redirect("~/Inicio/index");
                return null;
            }
            tbSucursales tbSucursales = new tbSucursales { suc_Estado = true };
            return View(tbSucursales);
        }
        //Llenar Tabla
        [SessionManager("Sucursales/Index")]
        [HttpPost]
        public JsonResult llenarTabla()
        {
            try
            {
                db = new ERP_GMEDINAEntities();
                var lista = db.tbSucursales
                    .Select(
                        t =>
                        new
                        {
                            suc_Id = t.suc_Id,
                            suc_Descripcion = t.suc_Descripcion,
                            suc_Direccion = t.suc_Direccion,
                            suc_Telefono = t.suc_Telefono,
                            suc_Estado = t.suc_Estado
                        }
                    )
                    .ToList();
                return Json(lista, JsonRequestBehavior.AllowGet);

                //aqui termina llenarTabla
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        // GET: Sucursales/Create
        [SessionManager("Sucursales/Create")]
        public ActionResult Create()
        {
            db = new ERP_GMEDINAEntities();
            ViewBag.suc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.suc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            var Departamentos = new List<object> { };
            Departamentos.Add(new
            { dep_Codigo = 0, dep_Nombre = "**Seleccione una opción**" });
            Departamentos.AddRange(db.tbDepartamento.Select(tabla => new { dep_Codigo = tabla.dep_Codigo, dep_Nombre = tabla.dep_Nombre}).ToList());
            ViewBag.dep_codigo = new SelectList(Departamentos, "dep_Codigo", "dep_Nombre");
            var Municipio = new List<object> { };
            Municipio.Add(new{ mun_Codigo = 0, mun_Nombre = "**Seleccione una opción**" });
            ViewBag.mun_codigo = new SelectList(Municipio, "mun_Codigo", "mun_Nombre");
            var Bodega = new List<object> { };
            Bodega.Add(new { bod_Id = 0, bod_Nombre = "**Seleccione una opción**" });
            Bodega.AddRange(db.tbBodega.Select(tabla => new { bod_Id = tabla.bod_Id, bod_Nombre = tabla.bod_Nombre }).ToList());
            ViewBag.bod_id = new SelectList(Bodega, "bod_Id", "bod_Nombre");
            var PuntoE = new List<object> { };
            PuntoE.Add(new{ pemi_Id = 0, pemi_NumeroCAI = "**Seleccione una opción**" });
            PuntoE.AddRange(db.tbPuntoEmision.Select(tabla => new { pemi_Id = tabla.pemi_Id, pemi_NumeroCAI = tabla.pemi_NumeroCAI}).ToList());
            ViewBag.pemi_Id = new SelectList(PuntoE, "pemi_Id", "pemi_NumeroCAI");
            var Empresas = new List<object> { };
            Empresas.Add(new{ empr_Id = 0, empr_Nombre = "**Seleccione una opción**" });
            Empresas.AddRange(db.tbEmpresas.Select(tabla => new { empr_Id = tabla.empr_Id, empr_Nombre = tabla.empr_Nombre }).ToList());
            ViewBag.empr_Id = new SelectList(Empresas, "empr_Id", "empr_Nombre");
            return View();
        }
        [HttpGet]
        public JsonResult MunicipiosDDl (string id)
        {
            using (db = new ERP_GMEDINAEntities())
            {
                var Municipios = new List<object> { };
                Municipios.AddRange(db.tbMunicipio
                        .Select(tabla => new { mun_Codigo = tabla.mun_Codigo, mun_Nombre = tabla.mun_Nombre , dep_Codigo = tabla.dep_Codigo})
                        .Where(x => x.dep_Codigo == id).ToList());
                return Json(Municipios, JsonRequestBehavior.AllowGet);
            }

        }


        // POST: Sucursales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [SessionManager("Sucursales/Create")]
        [HttpPost]
        public ActionResult Create(tbSucursales tbSucursales)
        {
            string msj = "";
            if (tbSucursales != null)
            {
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbSucursales_Insert(tbSucursales.empr_Id,tbSucursales.mun_Codigo, tbSucursales.bod_Id, tbSucursales.pemi_Id, tbSucursales.suc_Descripcion, tbSucursales.suc_Correo, tbSucursales.suc_Direccion, tbSucursales.suc_Telefono, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbSucursales_Insert_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                    return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
                } catch(Exception ex)
                {
                    ex.Message.ToString();
                    msj = "-2";
                    return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0,2), JsonRequestBehavior.AllowGet);
        }
        //Detalles
        [SessionManager("Sucursales/Detalles")]
        public ActionResult Detalles(int? id)
        {
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var lista = db.tbSucursales
                        .Select(
                            t =>
                            new
                            {
                                suc_Id = t.suc_Id,
                                mun_Codigo = t.mun_Codigo,
                                bod_Id = t.bod_Id,
                                pemi_Id = t.pemi_Id,
                                empr_Nombre = t.tbEmpresas.empr_Nombre,
                                suc_Correo = t.suc_Correo,
                                suc_Descripcion = t.suc_Descripcion,
                                suc_Direccion = t.suc_Direccion,
                                suc_Telefono = t.suc_Telefono,
                                suc_FechaCrea = t.suc_FechaCrea,
                                suc_FechaModifica = t.suc_FechaModifica,
                                suc_UsuarioCrea = t.tbUsuario1.usu_Nombres + " " + t.tbUsuario1.usu_Apellidos,
                                suc_UsuarioModifica = t.tbUsuario2.usu_Nombres + " " + t.tbUsuario2.usu_Apellidos
                            }
                        )
                        .Where(x => x.suc_Id == id).ToList();
                    return Json(lista, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        //Llenar DDL
        public ActionResult llenarDropDowlist()
        {
            var Empresas = new List<object> { };
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    Empresas.Add(new
                    {
                        empr_Id = 0,
                        empr_Nombre = "**Seleccione una opción**"
                    });
                    Empresas.AddRange(db.tbEmpresas
                    .Select(tabla => new { empr_Id = tabla.empr_Id, empr_Nombre = tabla.empr_Nombre })
                    .ToList());
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
            var result = new Dictionary<string, object>();
            result.Add("Empresas", Empresas);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // GET: Sucursales/Edit/5
        [SessionManager("Sucursales/Edit")]
        public ActionResult Edit(int? id)
        {
            db = new ERP_GMEDINAEntities();
            ViewBag.suc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.suc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            var Departamentos = new List<object> { };
            Departamentos.Add(new
            { dep_Codigo = 0, dep_Nombre = "**Seleccione una opción**" });
            Departamentos.AddRange(db.tbDepartamento.Select(tabla => new { dep_Codigo = tabla.dep_Codigo, dep_Nombre = tabla.dep_Nombre }).ToList());
            ViewBag.dep_codigo = new SelectList(Departamentos, "dep_Codigo", "dep_Nombre");
            var Municipio = new List<object> { };
            Municipio.Add(new { mun_Codigo = 0, mun_Nombre = "**Seleccione una opción**" });
            ViewBag.mun_codigo = new SelectList(Municipio, "mun_Codigo", "mun_Nombre");
            var Bodega = new List<object> { };
            Bodega.Add(new { bod_Id = 0, bod_Nombre = "**Seleccione una opción**" });
            Bodega.AddRange(db.tbBodega.Select(tabla => new { bod_Id = tabla.bod_Id, bod_Nombre = tabla.bod_Nombre }).ToList());
            ViewBag.bod_id = new SelectList(Bodega, "bod_Id", "bod_Nombre");
            var PuntoE = new List<object> { };
            PuntoE.Add(new { pemi_Id = 0, pemi_NumeroCAI = "**Seleccione una opción**" });
            PuntoE.AddRange(db.tbPuntoEmision.Select(tabla => new { pemi_Id = tabla.pemi_Id, pemi_NumeroCAI = tabla.pemi_NumeroCAI }).ToList());
            ViewBag.pemi_Id = new SelectList(PuntoE, "pemi_Id", "pemi_NumeroCAI");
            var Empresas = new List<object> { };
            Empresas.Add(new { empr_Id = 0, empr_Nombre = "**Seleccione una opción**" });
            Empresas.AddRange(db.tbEmpresas.Select(tabla => new { empr_Id = tabla.empr_Id, empr_Nombre = tabla.empr_Nombre }).ToList());
            ViewBag.empr_Id = new SelectList(Empresas, "empr_Id", "empr_Nombre");

            if (id == null)
            {
                return View("Edit");
            }
            else
            {
                var tbSucursales = db.tbSucursales
                .Select(
                s => new
                {
                    suc_Id = s.suc_Id,
                    empr_Id = s.empr_Id,
                    dep_Codigo = s.tbMunicipio.tbDepartamento.dep_Codigo,
                    mun_Codigo = s.mun_Codigo,
                    bod_Id = s.bod_Id,
                    pemi_Id = s.pemi_Id,
                    suc_Descripcion = s.suc_Descripcion,
                    suc_Correo = s.suc_Correo,
                    suc_Direccion = s.suc_Direccion,
                    suc_Telefono = s.suc_Telefono,

                })
                .Where(x => x.suc_Id == id).ToList();
                return Json(tbSucursales, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Sucursales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [SessionManager("Sucursales/Edit")]
        [HttpPost]
        public ActionResult Edit( tbSucursales tbSucursales)
        {
            string msj = "";
            if (tbSucursales.suc_Id != null)
            {
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbSucursales_Update(tbSucursales.suc_Id,tbSucursales.empr_Id,tbSucursales.mun_Codigo, tbSucursales.bod_Id, tbSucursales.pemi_Id, tbSucursales.suc_Descripcion, tbSucursales.suc_Correo, tbSucursales.suc_Direccion, tbSucursales.suc_Telefono, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbSucursales_Update_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                    return Json(msj.Substring(0,2), JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    msj = "-2";
                    return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }

        // GET: Sucursales/Delete/5
        [SessionManager("Sucursales/Delete")]
        [HttpPost]
        public ActionResult Delete(tbSucursales tbSucursales)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var list = db.UDP_RRHH_tbSucursales_Inactivar(tbSucursales.suc_Id,"", (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbSucursales_Inactivar_Result item in list)
                    {
                        result = item.MensajeError + " ";
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                result = "-2";
            }
            return Json(result.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }
        //Activar
        [SessionManager("Sucursales/habilitar")]
        [HttpPost]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var list = db.UDP_RRHH_tbSucursales_Activar(id, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbSucursales_Activar_Result item in list)
                    {
                        result = item.MensajeError + " ";
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                result = "-2";
            }
            return Json(result.Substring(0,2), JsonRequestBehavior.AllowGet);
        }

        // POST: Sucursales/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tbSucursales tbSucursales = db.tbSucursales.Find(id);
        //    db.tbSucursales.Remove(tbSucursales);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
