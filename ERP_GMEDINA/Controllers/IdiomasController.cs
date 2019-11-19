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
    public class IdiomasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: Idiomas
        public ActionResult Index()
        {
            var tbIdiomas = db.tbIdiomas.Where(t => t.idi_Estado == true);
            return View(tbIdiomas.ToList());
        }
        public ActionResult GetData()
        {
            //SI SE LLEGA A DAR PROBLEMAS DE "REFERENCIAS CIRCULARES", OBTENER LA DATA DE ESTA FORMA
            //SELECCIONANDO UNO POR UNO LOS CAMPOS QUE NECESITAREMOS
            //DE LO CONTRARIO, HACERLO DE LA FORMA CONVENCIONAL (EJEMPLO: db.tbCatalogoDeDeducciones.ToList(); )
            var tbIdiomas1 = db.tbIdiomas
                        .Select(c => new {
                            idi_Id = c.idi_Id,
                            idi_Descripcionn = c.idi_Descripcion,
                            idi_Estado = c.idi_Estado,
                            idi_RazonInactivo = c.idi_RazonInactivo,
                            idi_UsuarioModifica = c.idi_UsuarioModifica,
                            idi_UsuarioCrea = c.idi_UsuarioCrea,
                            idi_FechaCrea = c.idi_FechaCrea,
                            idi_FechaModifica = c.idi_FechaModifica
                        }).Where(c => c.idi_Estado == true)
                        .ToList();
            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = tbIdiomas1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // GET: Idiomas/Details/5

        public JsonResult Details(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbIdiomas tbJSON = db.tbIdiomas.Find(ID);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }

        // GET: Idiomas/Create
         public ActionResult Create()
        {
            return View();
        }

        // POST: Idiomas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idi_Id,idi_Descripcion,idi_Estado,idi_RazonInactivo,idi_UsuarioCrea,idi_FechaCrea,idi_UsuarioModifica,idi_FechaModifica")] tbIdiomas tbIdiomas)
        {
            tbIdiomas.idi_FechaCrea = DateTime.Now;
            tbIdiomas.idi_UsuarioCrea = 2;
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> listidioma = null;
                    string MensajeError = "";
                    listidioma = db.UDP_RRHH_tbIdiomas_Insert(tbIdiomas.idi_Descripcion,
                                                               tbIdiomas.idi_UsuarioCrea,
                                                               tbIdiomas.idi_FechaCrea);
                    foreach(UDP_RRHH_tbIdiomas_Insert_Result Res in listidioma)
                    {
                        MensajeError = Res.MensajeError;
                    }
                    if(!string.IsNullOrEmpty(MensajeError))
                    {
                        if(MensajeError.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "1.No se pudo ingresar el registro");
                            return View(tbIdiomas);
                        }
                    }
                    return RedirectToAction("Index");
                }
                 
                catch(Exception ex)
                {
                    ex.Message.ToString();
                    ModelState.AddModelError("", "2.No se pudo insertar el registro");
                    return View(tbIdiomas);
                }
            }
          //  ViewBag.idi_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIdiomas.idi_UsuarioCrea);
           // ViewBag.idi_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIdiomas.idi_UsuarioModifica);
            return View(tbIdiomas);
        }

        public ActionResult Edit(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbIdiomas tbJSON = db.tbIdiomas.Find(ID);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }
        // POST: Idiomas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idi_Id,idi_Descripcion,idi_Estado,idi_RazonInactivo,idi_UsuarioCrea,idi_FechaCrea,idi_UsuarioModifica,idi_FechaModifica")] tbIdiomas tbIdiomas)
        {
            tbIdiomas.idi_FechaModifica = DateTime.Now;
            tbIdiomas.idi_UsuarioModifica = 7;
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> listIdiomas = null;
                    string MensajeError = "";
                    listIdiomas = db.UDP_RRHH_tbIdiomas_Update(tbIdiomas.idi_Id,
                                                                tbIdiomas.idi_Descripcion,
                                                                tbIdiomas.idi_UsuarioModifica,
                                                                tbIdiomas.idi_FechaModifica);
                    foreach(UDP_RRHH_tbIdiomas_Update_Result Res in listIdiomas)
                    {
                        MensajeError = Res.MensajeError;
                    }
                    if(!string.IsNullOrEmpty(MensajeError))
                    {
                        if(MensajeError.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "1. No se pudo Editar el registo");
                            return View(tbIdiomas);
                        }
                    }
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ex.Message.ToString();
                    ModelState.AddModelError("", "2. No se pudo insertar el registro");
                    return View(tbIdiomas);
                }
               
            }
         //   ViewBag.idi_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIdiomas.idi_UsuarioCrea);
           // ViewBag.idi_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbIdiomas.idi_UsuarioModifica);
            return View(tbIdiomas);
        }


        public JsonResult Inactivar(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbIdiomas tbJSON = db.tbIdiomas.Find(ID);
            return Json(tbJSON, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inactivar([Bind(Include = "idi_Id,idi_UsuarioModifica,idi_FechaModifica")] tbIdiomas tbIdiomas)
        {
           
            tbIdiomas.idi_UsuarioModifica = 1;
            tbIdiomas.idi_FechaModifica = DateTime.Now;
            tbIdiomas.idi_RazonInactivo = "Porque ya no exite";
            string response = String.Empty;
            IEnumerable<object> listIdiomas = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listIdiomas = db.UDP_RRHH_tbIdiomas_Delete(tbIdiomas.idi_Id,
                                                              tbIdiomas.idi_RazonInactivo,
                                                              tbIdiomas.idi_UsuarioModifica,
                                                              tbIdiomas.idi_FechaModifica);
                    foreach (UDP_RRHH_tbIdiomas_Delete_Result Resultado in listIdiomas)
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
