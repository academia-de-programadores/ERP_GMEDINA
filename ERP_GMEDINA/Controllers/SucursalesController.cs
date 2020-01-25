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
    public class SucursalesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Sucursales
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
        public ActionResult Create()
        {
            ViewBag.suc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.suc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            var Empresas = new List<object> { };
            Empresas.Add(new
            {
                empr_Id = 0,
                empr_Nombre = "**Seleccione una opción**"
            });
            Empresas.AddRange(db.tbEmpresas
                    .Select(tabla => new { empr_Id = tabla.empr_Id, empr_Nombre = tabla.empr_Nombre })
                    .ToList());
            ViewBag.empr_Id = new SelectList(Empresas, "empr_Id", "empr_Nombre");
            return View();
        }

        // POST: Sucursales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(tbSucursales tbSucursales)
        {
            string msj = "";
            if (tbSucursales != null)
            {
                try
                {
                    var list = db.UDP_RRHH_tbSucursales_Insert(tbSucursales.empr_Id, "0501", tbSucursales.bod_Id, 1, tbSucursales.suc_Descripcion, tbSucursales.suc_Correo, tbSucursales.suc_Direccion, tbSucursales.suc_Telefono, 1, DateTime.Now);
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
                                suc_UsuarioCrea = t.tbUsuario.usu_Nombres + " " + t.tbUsuario.usu_Apellidos,
                                suc_UsuarioModifica = t.tbUsuario1.usu_Nombres + " " + t.tbUsuario1.usu_Apellidos
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
        public ActionResult Edit(int? id)
        {
            db = new ERP_GMEDINAEntities();
            //Nacionalidades
            List<tbEmpresas> Empresas = new List<tbEmpresas> { };
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
        [HttpPost]
        public ActionResult Edit( tbSucursales tbSucursales)
        {
            string msj = "";
            if (tbSucursales.suc_Id != null)
            {
                try
                {
                    var list = db.UDP_RRHH_tbSucursales_Update(tbSucursales.suc_Id,tbSucursales.empr_Id, tbSucursales.mun_Codigo, tbSucursales.bod_Id, tbSucursales.pemi_Id, tbSucursales.suc_Descripcion, tbSucursales.suc_Correo, tbSucursales.suc_Direccion, tbSucursales.suc_Telefono, 1, DateTime.Now);
                    foreach (UDP_RRHH_tbSucursales_Update_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                    return Json(msj, JsonRequestBehavior.AllowGet);
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
        [HttpPost]
        public ActionResult Delete(tbSucursales tbSucursales)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var list = db.UDP_RRHH_tbSucursales_Inactivar(tbSucursales.suc_Id,"", 1, DateTime.Now);
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
        [HttpPost]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var list = db.UDP_RRHH_tbSucursales_Activar(id,1, DateTime.Now);
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
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
