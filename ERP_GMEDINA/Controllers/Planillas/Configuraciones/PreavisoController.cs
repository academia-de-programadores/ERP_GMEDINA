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
    public class PreavisoController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        private ERP_GMEDINA.Models.Helpers Function = new Models.Helpers();

        #region INDEX
        [SessionManager("Preaviso/Index")]
        public ActionResult Index()
        {
            var tbPreaviso = db.tbPreaviso.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).OrderBy(x => x.prea_IdPreaviso);
            return View(tbPreaviso.ToList());
        }
        #endregion

        #region DATA PARA RECARGAR EL DATATABLE
        public ActionResult GetData()
        {
            // evitar referencias circulares
            db.Configuration.ProxyCreationEnabled = false;

            // obtener data para refrescar datatable
            var tbPreaviso = db.tbPreaviso
                .Select(c => new
                {
                    prea_IdPreaviso = c.prea_IdPreaviso,
                    prea_RangoInicioMeses = c.prea_RangoInicioMeses,
                    prea_RangoFinMeses = c.prea_RangoFinMeses,
                    prea_DiasPreaviso = c.prea_DiasPreaviso,
                    prea_UsuarioCrea = c.prea_UsuarioCrea,
                    NombreUsuarioCrea = c.tbUsuario.usu_NombreUsuario,
                    prea_FechaCrea = c.prea_FechaCrea,
                    prea_UsuarioModifica = c.prea_UsuarioModifica,
                    NombreUsuarioModifica = c.tbUsuario1.usu_NombreUsuario,
                    prea_FechaModifica = c.prea_FechaModifica,
                    prea_Activo = c.prea_Activo
                }).OrderBy(x => x.prea_IdPreaviso).ToList();

            // retornar data
            return new JsonResult { Data = tbPreaviso, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region GET: DETAILS
        [SessionManager("Preaviso/Details")]
        public JsonResult Details(int? id)
        {
            // validar si se recibió el ID
            if(id == null)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

            var tbCatalogoPreavisoJSON = from tbPreavi in db.tbPreaviso
                                         where tbPreavi.prea_IdPreaviso == id
                                         select new
                                         {
                                             tbPreavi.prea_IdPreaviso,
                                             tbPreavi.prea_RangoInicioMeses,
                                             tbPreavi.prea_RangoFinMeses,
                                             tbPreavi.prea_DiasPreaviso,
                                             tbPreavi.prea_Activo,
                                             tbPreavi.prea_UsuarioCrea,
                                             UsuCrea = tbPreavi.tbUsuario.usu_NombreUsuario,
                                             tbPreavi.prea_FechaCrea,
                                             tbPreavi.prea_UsuarioModifica,
                                             UsuModifica = tbPreavi.tbUsuario1.usu_NombreUsuario,
                                             tbPreavi.prea_FechaModifica
                                         };

            // evitar referencias circulares
            db.Configuration.ProxyCreationEnabled = false;

            // retornar registro con el ID recibido
            return Json(tbCatalogoPreavisoJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Preaviso/Create")]
        public ActionResult Create([Bind(Include = "prea_RangoInicioMeses,prea_RangoFinMeses,prea_DiasPreaviso,prea_UsuarioCrea,prea_FechaCrea,prea_Activo")] tbPreaviso tbPreaviso)
        {
            // data de auditoria
            //tbPreaviso.prea_UsuarioCrea = 1;
            //tbPreaviso.prea_FechaCrea = DateTime.Now;
            tbPreaviso.prea_Activo = true;

            // variables de resultados 
            string response = "bien";
            IEnumerable<object> listPreaviso = null;
            String MessageError = "";

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listPreaviso = db.UDP_Plani_tbPreaviso_Insert(tbPreaviso.prea_RangoInicioMeses,
                                                                  tbPreaviso.prea_RangoFinMeses,
                                                                  tbPreaviso.prea_DiasPreaviso,
                                                                  Function.GetUser(),
                                                                  Function.DatetimeNow());
                    
                    // obtener resultado del PA
                    foreach (UDP_Plani_tbPreaviso_Insert_Result resultado in listPreaviso)
                        MessageError = Convert.ToString(resultado);

                    if (MessageError.StartsWith("-1"))
                    {
                        // el PA falló
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    // se generó una excepción
                    ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                    response = "error";
                }
            }
            else
            {
                // el modelo no es válido
                response = "error";
            }

            // retornar resultado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GET: EDIT
        [SessionManager("Preaviso/Edit")]
        public JsonResult Edit(int? id)
        {
            // validar si se recibió el ID
            if (id == null)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

            // obtener registro con el ID recibido  
            var tbPreaviso = db.tbPreaviso.Where(d => d.prea_IdPreaviso == id)
                        .Select(c => new { prea_IdPreaviso = c.prea_IdPreaviso, prea_RangoInicioMeses = c.prea_RangoInicioMeses, prea_RangoFinMeses = c.prea_RangoFinMeses, prea_DiasPreaviso = c.prea_DiasPreaviso, prea_UsuarioCrea = c.prea_UsuarioCrea, prea_FechaCrea = c.prea_FechaCrea, prea_UsuarioModifica = c.prea_UsuarioModifica, prea_FechaModifica = c.prea_FechaModifica, prea_Activo = c.prea_Activo })
                        .ToList();

            // validar si habia registro con el ID recibido
            if (tbPreaviso == null)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

            // retornar objeto
            return Json(tbPreaviso, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: EDITAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionManager("Preaviso/Edit")]
        public ActionResult Editar([Bind(Include = "prea_IdPreaviso,prea_RangoInicioMeses,prea_RangoFinMeses,prea_DiasPreaviso")] tbPreaviso tbPreaviso)
        {
            // data de auditoria
            //tbPreaviso.prea_UsuarioModifica = 1;
            //tbPreaviso.prea_FechaModifica = DateTime.Now;
            tbPreaviso.prea_Activo = true;
            
            // variable de resultados
            string response = "bien";
            IEnumerable<object> listPeriodo = null;
            String MessageError = "";

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listPeriodo = db.UDP_Plani_tbPreaviso_Update(tbPreaviso.prea_IdPreaviso,
                                                                 tbPreaviso.prea_RangoInicioMeses,
                                                                 tbPreaviso.prea_RangoFinMeses,
                                                                 tbPreaviso.prea_DiasPreaviso,
                                                                 Function.GetUser(),
                                                                 Function.DatetimeNow());
                    
                    // obtener resultado del PA
                    foreach (UDP_Plani_tbPreaviso_Update_Result resultado in listPeriodo)
                        MessageError = Convert.ToString(resultado);

                    if (MessageError.StartsWith("-1"))
                    {
                        // el PA falló
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    // se generó una excepción
                    ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                    response = "error";
                }
            }
            else
            {
                // el modelo no es válido
                response = "error";
            }

            // retornar resultado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: INACTIVAR
        [HttpPost]
        [SessionManager("Preaviso/Inactivar")]
        public ActionResult Inactivar(int? id)
        {
            // variables de resultado
            string response = "bien";
            IEnumerable<object> listPreaviso = null;
            string MensajeError = "";

            // validar que se recibió el ID
            if (id == null)
            {
                response = "error";
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            // objeto 
            tbPreaviso tbPeaviso = new tbPreaviso();
            tbPeaviso.prea_IdPreaviso = (int)id;
            //tbPeaviso.prea_UsuarioModifica = 1;
            //tbPeaviso.prea_FechaModifica = DateTime.Now;
            
            // validar si el ID es válido
            if (tbPeaviso.prea_IdPreaviso > 0)
            {
                try
                {
                    // ejecutar PA
                    listPreaviso = db.UDP_Plani_tbPreaviso_Inactivar(tbPeaviso.prea_IdPreaviso,
                                                                     Function.GetUser(),
                                                                     Function.DatetimeNow());

                    // obtener resultado del PA
                    foreach (UDP_Plani_tbPreaviso_Inactivar_Result Resultado in listPreaviso)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el PA falló
                        ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador");
                        response = "error";
                    }

                }
                catch (Exception Ex)
                {
                    // se generó una excepción
                    response = "error";
                }
            }
            else
            {
                // el ID no es válido
                ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador.");
                response = "error";
            }

            // retornar resultado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: ACTIVAR
        [HttpPost]
        [SessionManager("Preaviso/Activar")]
        public ActionResult Activar(int? Id)
        {
            // variables de resultado
            string response = "bien";
            IEnumerable<object> listPreaviso = null;
            string MensajeError = "";

            // validar que el id se recibió
            if (Id == null)
            {
                response = "error";
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            // objeto
            tbPreaviso tbPreaviso = new tbPreaviso();
            tbPreaviso.prea_IdPreaviso = (int)Id;
            //tbPreaviso.prea_UsuarioModifica = 1;
            //tbPreaviso.prea_FechaModifica = DateTime.Now;
            
            // validar que el ID es válido
            if (tbPreaviso.prea_IdPreaviso > 0)
            {
                try
                {
                    // ejecutar PA
                    listPreaviso = db.UDP_Plani_tbPreaviso_Activar(tbPreaviso.prea_IdPreaviso,
                                                                   Function.GetUser(),
                                                                   Function.DatetimeNow());

                    // obtener resultado del PA
                    foreach (UDP_Plani_tbPreaviso_Activar_Result Resultado in listPreaviso)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el PA falló
                        ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador");
                        response = "error";
                    }

                }
                catch (Exception Ex)
                {
                    // se generó una excepción
                    response = "error";
                }
            }
            else
            {
                // el ID no es válido
                ModelState.AddModelError("", "No se pudo Activar el registro, contacte al administrador.");
                response = "error";
            }

            // retornar resultado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region DISPOSE
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}