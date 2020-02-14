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
    public class SucursalController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        Models.Helpers Function = new Models.Helpers();
        // GET: /Sucursal/
        [SessionManager("Sucursal/Index")]
        public ActionResult Index()
        {
            var tbSucursales = db.tbSucursales.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbMunicipio).Include(t => t.tbBodega).Include(t => t.tbPuntoEmision);
            return View(tbSucursales.ToList());
        }

        // GET: /Sucursal/Details/5
        [SessionManager("Sucursal/Details")]
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbSucursales tbSucursales = db.tbSucursales.Find(id);
            if (tbSucursales == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbSucursales);
        }




        public JsonResult Listado_CAI()
        {
            IEnumerable<object> list = null;
            try
            {
                list = db.Listado_CAI().ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToList();
                list = null;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: /Sucursal/Create
        [SessionManager("Sucursal/Create")]
        public ActionResult Create()
        {
            ViewBag.suc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.suc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre");
            var D = db.tbSucursales.Select(x => x.pemi_Id).ToList();
            ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI");
            var Bodegas = db.tbBodega.Select(s => new
            {
                bod_Id = s.bod_Id,
                bod_Nombre = string.Concat(s.mun_Codigo + " - " + s.bod_Nombre)
            }).ToList();

            ViewBag.bod_Id = new SelectList(Bodegas, "bod_Id", "bod_Nombre");

            return View();
        }

        // POST: /Sucursal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Sucursal/Create")]
        public ActionResult Create([Bind(Include="suc_Id,mun_Codigo,bod_Id,pemi_Id,suc_Descripcion,suc_Correo,suc_Direccion,suc_Telefono,suc_UsuarioCrea,suc_FechaCrea,suc_UsuarioModifica,suc_FechaModifica")] tbSucursales tbSucursales)
        {
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre");
            var Bodegas = db.tbBodega.Select(s => new
            {
                bod_Id = s.bod_Id,
                bod_Nombre = string.Concat(s.mun_Codigo + " - " + s.bod_Nombre)
            }).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    string MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbSucursal_Insert( tbSucursales.mun_Codigo,
                                                            tbSucursales.bod_Id,
                                                            tbSucursales.pemi_Id,
                                                            tbSucursales.suc_Descripcion,
                                                            tbSucursales.suc_Correo,
                                                            tbSucursales.suc_Direccion,
                                                            tbSucursales.suc_Telefono,
                                                            Function.GetUser(),
                                                            Function.DatetimeNow());
                    foreach (UDP_Vent_tbSucursal_Insert_Result Exoneracion in list)
                        MensajeError = Exoneracion.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbSucursales.mun_Codigo);
                        ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                        ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSucursales.bod_Id);
                        ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI", tbSucursales.pemi_Id);
                        ViewBag.bod_Id = new SelectList(Bodegas, "bod_Id", "bod_Nombre", tbSucursales.bod_Id);
                        ModelState.AddModelError("", "No se pudo insertar el registro, contacte al administrador");
                        ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                        return View(tbSucursales);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbSucursales.mun_Codigo);
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSucursales.bod_Id);
                ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI", tbSucursales.pemi_Id);
                ViewBag.bod_Id = new SelectList(Bodegas, "bod_Id", "bod_Nombre", tbSucursales.bod_Id);
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                return View(tbSucursales);
            }
            catch (Exception Ex)
            {
                ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "dep_Codigo", tbSucursales.mun_Codigo);
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_ResponsableBodega", tbSucursales.bod_Id);
                ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI", tbSucursales.pemi_Id);
                ViewBag.bod_Id = new SelectList(Bodegas, "bod_Id", "bod_Nombre", tbSucursales.bod_Id);
                ModelState.AddModelError("", "Error al Agregar Registro " + Ex.Message.ToString());
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                return View(tbSucursales);
            }
        }

        // GET: /Sucursal/Edit/5
        [SessionManager("Sucursal/Edit")]
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbSucursales tbSucursales = db.tbSucursales.Find(id);
            if (tbSucursales == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            ViewBag.suc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursales.suc_UsuarioCrea);
            ViewBag.suc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursales.suc_UsuarioModifica);
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbSucursales.tbMunicipio.tbDepartamento.dep_Codigo);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbSucursales.mun_Codigo);
            ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbSucursales.bod_Id);
            var D = db.tbSucursales.Select(x => x.pemi_Id).ToList();
            ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI");
            var Bodegas = db.tbBodega.Select(s => new
            {
                bod_Id = s.bod_Id,
                bod_Nombre = string.Concat(s.mun_Codigo + " - " + s.bod_Nombre)
            }).ToList();

            ViewBag.bod_Id = new SelectList(Bodegas, "bod_Id", "bod_Nombre", tbSucursales.bod_Id);

            return View(tbSucursales);
        }

        // POST: /Sucursal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [SessionManager("Sucursal/Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(short? id, [Bind(Include = "suc_Id,mun_Codigo,bod_Id,pemi_Id,suc_Descripcion,suc_Correo,suc_Direccion,suc_Telefono,suc_UsuarioCrea,suc_FechaCrea")] tbSucursales tbSucursales)
        {
            var Bodegas = db.tbBodega.Select(s => new
            {
                bod_Id = s.bod_Id,
                bod_Nombre = string.Concat(s.mun_Codigo + " - " + s.bod_Nombre)
            }).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    tbSucursales pSucursal = db.tbSucursales.Find(id);
                    string MensajeError = "";
                    IEnumerable<object> list = null;
                    list = db.UDP_Vent_tbSucursal_Update(tbSucursales.suc_Id,
                                                            tbSucursales.mun_Codigo,
                                                            tbSucursales.bod_Id,
                                                            tbSucursales.pemi_Id,
                                                            tbSucursales.suc_Descripcion,
                                                            tbSucursales.suc_Correo,
                                                            tbSucursales.suc_Direccion,
                                                            tbSucursales.suc_Telefono,
                                                            pSucursal.suc_UsuarioCrea,
                                                            pSucursal.suc_FechaCrea, Function.GetUser(), Function.DatetimeNow());
                    foreach (UDP_Vent_tbSucursal_Update_Result Exoneracion in list)
                        MensajeError = Exoneracion.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                        ViewBag.suc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursales.suc_UsuarioCrea);
                        ViewBag.suc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursales.suc_UsuarioModifica);
                        ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbSucursales.tbMunicipio.tbDepartamento.dep_Codigo);
                        ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbSucursales.mun_Codigo);
                        ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbSucursales.bod_Id);
                        ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI", tbSucursales.pemi_Id);
                        ViewBag.bod_Id = new SelectList(Bodegas, "bod_Id", "bod_Nombre", tbSucursales.bod_Id);
                        return View(tbSucursales);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                ViewBag.suc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursales.suc_UsuarioCrea);
                ViewBag.suc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursales.suc_UsuarioModifica);
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbSucursales.tbMunicipio.tbDepartamento.dep_Codigo);
                ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbSucursales.mun_Codigo);
                ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbSucursales.bod_Id);
                ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI", tbSucursales.pemi_Id);
                ViewBag.bod_Id = new SelectList(Bodegas, "bod_Id", "bod_Nombre", tbSucursales.bod_Id);
                return View(tbSucursales);
            }
            catch (Exception Ex)
            {
                ViewBag.suc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursales.suc_UsuarioCrea);
                ViewBag.suc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbSucursales.suc_UsuarioModifica);
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", tbSucursales.tbMunicipio.tbDepartamento.dep_Codigo);
                ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_Codigo", "mun_Nombre", tbSucursales.mun_Codigo);
                ViewBag.bod_Id = new SelectList(db.tbBodega, "bod_Id", "bod_Nombre", tbSucursales.bod_Id);
                ViewBag.pemi_Id = new SelectList(db.tbPuntoEmision, "pemi_Id", "pemi_NumeroCAI", tbSucursales.pemi_Id);
                ViewBag.bod_Id = new SelectList(Bodegas, "bod_Id", "bod_Nombre", tbSucursales.bod_Id);
                ModelState.AddModelError("", "Error al Agregar Registro " + Ex.Message.ToString());
                return View(tbSucursales);
            } 
            
        }

        // GET: /Sucursal/Delete/5
        [SessionManager("Sucursal/Delete")]
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbSucursales tbSucursales = db.tbSucursales.Find(id);
            if (tbSucursales == null)
            {
                return RedirectToAction("NotFound", "Login");
            }
            return View(tbSucursales);
        }

        // POST: /Sucursal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [SessionManager("Sucursal/Delete")]
        public ActionResult DeleteConfirmed(short id)
        {
            tbSucursales tbSucursales = db.tbSucursales.Find(id);
            db.tbSucursales.Remove(tbSucursales);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public JsonResult GetMunicipios(string CodDepartamento)
        {
            var list = db.spGetMunicipios1(CodDepartamento).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


    }
}
