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
    public class CargosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Cargos
        public ActionResult Index()
        {
            var tbCargos = db.tbCargos.Where(t => t.car_Estado==true);
            return View(tbCargos.ToList());
        }

        public ActionResult GetData()
        {
            //SI SE LLEGA A DAR PROBLEMAS DE "REFERENCIAS CIRCULARES", OBTENER LA DATA DE ESTA FORMA
            //SELECCIONANDO UNO POR UNO LOS CAMPOS QUE NECESITAREMOS
            //DE LO CONTRARIO, HACERLO DE LA FORMA CONVENCIONAL (EJEMPLO: db.tbCatalogoDeDeducciones.ToList(); )
            var tbCargos1 = db.tbCargos
                        .Select(c => new {
                            car_Id = c.car_Id,
                            car_Descripcionn = c.car_Descripcion,
                            car_Estado = c.car_Estado,
                            car_RazonInactivo = c.car_RazonInactivo,
                            car_UsuarioModifica = c.car_UsuarioModifica,
                            car_UsuarioCrea = c.car_UsuarioCrea,
                            car_FechaCrea = c.car_FechaCrea,
                            car_FechaModifica = c.car_FechaModifica
                        }).Where(c => c.car_Estado == true)
                        .ToList();
            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = tbCargos1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult Details(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbCargos tbJSON = db.tbCargos.Find(ID);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }

        // GET: Idiomas/Create
        public ActionResult Create()
        {
            return View();
        }
        // GET: Cargos/Create


        // POST: Cargos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "car_Id,car_Descripcion,car_Estado,car_RazonInactivo,car_UsuarioCrea,car_FechaCrea,car_UsuarioModifica,car_FechaModifica")] tbCargos tbCargos)
        {
            tbCargos.car_UsuarioCrea = 1;
            tbCargos.car_FechaCrea = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> listcargos = null;
                    string MensajeError = "";
                    listcargos = db.UDP_RRHH_tbCargos_Insert(tbCargos.car_Descripcion,
                                                           tbCargos.car_UsuarioCrea,
                                                           tbCargos.car_FechaCrea);
                    foreach (UDP_RRHH_tbCargos_Insert_Result car in listcargos)
                    {
                        MensajeError = car.MensajeError;
                    }
                    if (!string.IsNullOrEmpty(MensajeError))
                    {
                        if (MensajeError.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "1. No se pudo insertar el registro");
                            return View(tbCargos);
                        }
                    }
               
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ex.Message.ToString();
                    ModelState.AddModelError("", "2. No se pudo insertar el registro");
                    return View(tbCargos);
                }
            }

            //ViewBag.car_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCargos.car_UsuarioCrea);
            //ViewBag.car_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCargos.car_UsuarioModifica);
            return View(tbCargos);
        }

        // GET: Cargos/Edit/5
        public ActionResult Edit(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbCargos tbJSON = db.tbCargos.Find(ID);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }

        // POST: Cargos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "car_Id,car_Descripcion,car_Estado,car_RazonInactivo,car_UsuarioCrea,car_FechaCrea,car_UsuarioModifica,car_FechaModifica")] tbCargos tbCargos)
        {
            tbCargos.car_UsuarioModifica = 2;
            tbCargos.car_FechaModifica = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> listcargos = null;
                    string MensajeError = "";
                    listcargos = db.UDP_RRHH_tbCargos_Update(tbCargos.car_Id,
                                                             tbCargos.car_Descripcion,
                                                             tbCargos.car_UsuarioModifica,
                                                             tbCargos.car_FechaModifica);

                    foreach(UDP_RRHH_tbCargos_Update_Result car in listcargos)
                    {
                        MensajeError = car.MensajeError;
                    }
                    if (!string.IsNullOrEmpty(MensajeError))
                    {
                        if (MensajeError.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "1. No se pudo editar el registro");
                            return View(tbCargos);
                        }
                    }
                    return RedirectToAction("Index");

                }
                catch(Exception ex)
                {
                    ex.Message.ToString();
                    ModelState.AddModelError("", "2. No se pudo insertar el registro");
                    return View(tbCargos);
                }

                //db.Entry(tbCargos).State = EntityState.Modified;
                //db.SaveChanges();
                
            }
            //ViewBag.car_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCargos.car_UsuarioCrea);
            //ViewBag.car_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCargos.car_UsuarioModifica);
            return View(tbCargos);
        }

        // GET: Cargos/Delete/5
        public JsonResult Inactivar(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbCargos tbJSON = db.tbCargos.Find(ID);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }
        // POST: Cargos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]


        public ActionResult Inactivar([Bind(Include = "car_Id,car_UsuarioModifica,car_FechaModifica")] tbCargos tbCargos)
        {

            tbCargos.car_UsuarioModifica = 1;
            tbCargos.car_FechaModifica = DateTime.Now;
            tbCargos.car_RazonInactivo = "Inactivo";
            string response = String.Empty;
            IEnumerable<object> listCargos = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listCargos = db.UDP_RRHH_tbCargos_Delete(tbCargos.car_Id,
                                                              tbCargos.car_RazonInactivo,
                                                              tbCargos.car_UsuarioModifica,
                                                              tbCargos.car_FechaModifica);
                    foreach (UDP_RRHH_tbCargos_Delete_Result Resultado in listCargos)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador");
                        response = "error";
                    }

                }
                catch (Exception Ex)
                {
                    response = Ex.Message.ToString();
                }
                response = "bien";
            }
            else
            {
                ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador.");
                response = "error";
            }
            //ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion", tbCatalogoDeDeducciones.tde_IdTipoDedu);

            //RETORNAR MENSAJE AL LADO DEL CLIENTE
            return Json(response, JsonRequestBehavior.AllowGet);
        }


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
