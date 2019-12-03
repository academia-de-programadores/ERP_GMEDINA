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
    public class TipoAmonestacionesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: TipoAmonestaciones
        public ActionResult Index()
        {
            var tbTipoAmonestaciones = db.tbTipoAmonestaciones.Where(t => t.tamo_Estado == true);
            return View(tbTipoAmonestaciones.ToList());
        }
        //LO AGREGUE
        public ActionResult GetData()
        {
            //SI SE LLEGA A DAR PROBLEMAS DE "REFERENCIAS CIRCULARES", OBTENER LA DATA DE ESTA FORMA
            //SELECCIONANDO UNO POR UNO LOS CAMPOS QUE NECESITAREMOS
            //DE LO CONTRARIO, HACERLO DE LA FORMA CONVENCIONAL (EJEMPLO: db.tbCatalogoDeDeducciones.ToList(); )
            var tbTipoAmonestaciones1 = db.tbTipoAmonestaciones
                        .Select(c => new {
                            tamo_Id = c.tamo_Id,
                            tamo_Descripcionn = c.tamo_Descripcion,
                            tamo_Estado = c.tamo_Estado,
                            tamo_RazonInactivo = c.tamo_RazonInactivo,
                            tamo_UsuarioModifica = c.tamo_UsuarioModifica,
                            tamo_UsuarioCrea = c.tamo_UsuarioCrea,
                            tamo_FechaCrea = c.tamo_FechaCrea,
                            tamo_FechaModifica = c.tamo_FechaModifica
                        }).Where(c => c.tamo_Estado == true)
                        .ToList();
            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = tbTipoAmonestaciones1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: TipoAmonestaciones/Details/5
        public JsonResult Details(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbTipoAmonestaciones tbJSON = db.tbTipoAmonestaciones.Find(ID);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }

        // GET: TipoAmonestaciones/Create
        //LO AGREGE
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoAmonestaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        //LO AGREGE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tamo_Id,tamo_Descripcion,tamo_Estado,tamo_RazonInactivo,tamo_UsuarioCrea,tamo_FechaCrea,tamo_UsuarioModifica,tamo_FechaModifica")] tbTipoAmonestaciones tbTipoAmonestaciones)
        {
            tbTipoAmonestaciones.tamo_UsuarioCrea = 1;
            tbTipoAmonestaciones.tamo_FechaCrea = DateTime.Now;
            IEnumerable<object> listTipoAmonestaciones = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                   
                    listTipoAmonestaciones = db.UDP_RRHH_tbTipoAmonestaciones_Insert(tbTipoAmonestaciones.tamo_Descripcion,
                                                                                     tbTipoAmonestaciones.tamo_UsuarioCrea,
                                                                                     tbTipoAmonestaciones.tamo_FechaCrea);

                    foreach (UDP_RRHH_tbTipoAmonestaciones_Insert_Result Resultado in listTipoAmonestaciones)
                    {
                        MensajeError = Resultado.MensajeError;
                    }
                    if (!string.IsNullOrEmpty(MensajeError))
                    {
                        if (MensajeError.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "1.No se pudo ingresar el registro");
                            return View(tbTipoAmonestaciones);
                        }
                    }
                    return RedirectToAction("Index");
                }

                catch (Exception ex)
                {
                    ex.Message.ToString();
                    ModelState.AddModelError("", "2.No se pudo insertar el registro");
                    return View(tbTipoAmonestaciones);
                }
            }
            //  ViewBag.idi_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIdiomas.idi_UsuarioCrea);
            // ViewBag.idi_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIdiomas.idi_UsuarioModifica);
            return View(tbTipoAmonestaciones);
        }


        // GET: TipoAmonestaciones/Edit/5
        //LO AGREGE
        public ActionResult Edit(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbTipoAmonestaciones tbJSON = db.tbTipoAmonestaciones.Find(ID);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }
        // POST: TipoAmonestaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //PROCESO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tamo_Id,tamo_Descripcion,tamo_Estado,tamo_RazonInactivo,tamo_UsuarioCrea,tamo_FechaCrea,tamo_UsuarioModifica,tamo_FechaModifica")] tbTipoAmonestaciones tbTipoAmonestaciones)
        {
           
            //Llenar los campo de auditoria
            tbTipoAmonestaciones.tamo_UsuarioModifica = 1;
            tbTipoAmonestaciones.tamo_FechaModifica = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> listTipoAmonestacion1 = null;
                    string MensajeError = "";
                    listTipoAmonestacion1 = db.UDP_RRHH_tbTipoAmonestaciones_Update(tbTipoAmonestaciones.tamo_Id,
                                                                           tbTipoAmonestaciones.tamo_Descripcion,
                                                                           tbTipoAmonestaciones.tamo_UsuarioModifica,
                                                                           tbTipoAmonestaciones.tamo_FechaModifica);

                    foreach (UDP_RRHH_tbTipoAmonestaciones_Update_Result Res in listTipoAmonestacion1)
                    {
                        MensajeError = Res.MensajeError;
                    }
                    if (!string.IsNullOrEmpty(MensajeError))
                    {
                        if (MensajeError.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "1. No se pudo Editar el registo");
                            return View(tbTipoAmonestaciones);
                        }
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    ModelState.AddModelError("", "2. No se pudo insertar el registro");
                    return View(tbTipoAmonestaciones);
                }

            }
            //   ViewBag.idi_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIdiomas.idi_UsuarioCrea);
            // ViewBag.idi_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIdiomas.idi_UsuarioModifica);
            return View(tbTipoAmonestaciones);
        }

        // GET: TipoAmonestaciones/Delete/5
        public JsonResult Inactivar(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbTipoAmonestaciones tbJSON = db.tbTipoAmonestaciones.Find(ID);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }

        // POST: TipoAmonestaciones/Delete/5
        public ActionResult Inactivar([Bind(Include = "tamo_Id,tamo_UsuarioModifica,tamo_FechaModifica")] tbTipoAmonestaciones tbTipoAmonestaciones)
        {

            tbTipoAmonestaciones.tamo_UsuarioModifica = 1;
            tbTipoAmonestaciones.tamo_FechaModifica = DateTime.Now;
            tbTipoAmonestaciones.tamo_RazonInactivo = "Porque ya no exite";
            string response = String.Empty;
            IEnumerable<object> listTipo = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listTipo = db.UDP_RRHH_tbTipoAmonestaciones_Delete(tbTipoAmonestaciones.tamo_Id,
                                                              tbTipoAmonestaciones.tamo_RazonInactivo,
                                                              tbTipoAmonestaciones.tamo_UsuarioModifica,
                                                              tbTipoAmonestaciones.tamo_FechaModifica);
                    foreach (UDP_RRHH_tbTipoAmonestaciones_Delete_Result Resultado in listTipo)
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