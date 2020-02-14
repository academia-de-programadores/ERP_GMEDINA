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
    public class TipoDeduccionesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        Models.Helpers Function = new Models.Helpers();

        #region GET: INDEX  
        // GET: TipoDeducciones
        [SessionManager("TipoDeducciones/Index")]
        public ActionResult Index()
        {
            var tbTipoDeduccion = db.tbTipoDeduccion.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).OrderByDescending(c => c.tde_FechaCrea);
            return View(tbTipoDeduccion.ToList());
        }
        #endregion

        #region GET: DATA
        public ActionResult GetData()
        {
            // evitar refernecias circulares
            db.Configuration.ProxyCreationEnabled = false;
            
            // obtener data para refrescar datatable
			var tbTipoDeducciones = db.tbTipoDeduccion
				.Select(c => new
				{
					tde_Descripcion = c.tde_Descripcion,
					tde_UsuarioCrea = c.tde_UsuarioCrea,
					NombreUsuarioCrea = c.tbUsuario.usu_NombreUsuario,
					tde_FechaCrea = c.tde_FechaCrea,

					tde_UsuarioModifica = c.tde_UsuarioModifica,
					NombreUsuarioModifica = c.tbUsuario1.usu_NombreUsuario,
					tde_FechaModifica = c.tde_FechaModifica,
					tde_IdTipoDedu = c.tde_IdTipoDedu,
					tde_Activo = c.tde_Activo
				})
                .OrderBy(c => c.tde_FechaCrea)
                .ToList();

            // retornar data
            return new JsonResult { Data = tbTipoDeducciones, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region POST: CREATE
        [SessionManager("TipoDeducciones/Create")]
        [HttpPost]
        public ActionResult Create([Bind(Include = "tde_Descripcion, tde_UsuarioCrea, tde_FechaCrea")] tbTipoDeduccion tbTipoDeduccion)
        {
            // data de auditoria
            tbTipoDeduccion.tde_UsuarioCrea = 1;
            tbTipoDeduccion.tde_FechaCrea = DateTime.Now;
            
            // vairables de resultados
            string response = String.Empty;
            IEnumerable<object> listTipoDeduccion = null;
            String MessageError = "";
            
            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listTipoDeduccion = db.UDP_Plani_tbTipoDeduccion_Insert(tbTipoDeduccion.tde_Descripcion,
                                                                            tbTipoDeduccion.tde_UsuarioCrea,
                                                                            tbTipoDeduccion.tde_FechaCrea);
                    // obtener resultado del PA
                    foreach (UDP_Plani_tbTipoDeduccion_Insert_Result resultado in listTipoDeduccion)
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

                // el proceso fue exitoso
                response = "bien";
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

        #region GET: Editar 
        [SessionManager("TipoDeducciones/Edit")]
        public ActionResult Edit(int? id)
        {
            // validar si se recibió el id
            if (id == null)
            {
                return new JsonResult { Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

            // obtener registro con el id recibido
            var tbTipoDeduccion = db.tbTipoDeduccion.Where(d => d.tde_IdTipoDedu == id)
                        .Select(c => new { tde_Descripcion = c.tde_Descripcion, tde_IdTipoDedu = c.tde_IdTipoDedu, tde_UsuarioCrea = c.tde_UsuarioCrea, tde_FechaCrea = c.tde_FechaCrea, tde_UsuarioModifica = c.tde_UsuarioModifica, tde_FechaModifica = c.tde_FechaModifica, tde_Activo = c.tde_Activo })
                        .ToList();

            // retornar registro en formato JSON
            return new JsonResult { Data = tbTipoDeduccion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region GET: Details
        [SessionManager("TipoDeducciones/Details")]
        public JsonResult Details(int? ID)
        {
            // obtener registro con el ID recibido
            var tbTechosDeduccionesJSON = from tbTipoDeducciones in db.tbTipoDeduccion
                                          where tbTipoDeducciones.tde_IdTipoDedu == ID
                                          select new
                                          {
                                              tde_IdTipoDedu = tbTipoDeducciones.tde_IdTipoDedu,
                                              tde_Descripcion = tbTipoDeducciones.tde_Descripcion,

                                              tde_UsuarioCrea = tbTipoDeducciones.tde_UsuarioCrea,
                                              UsuCrea = tbTipoDeducciones.tbUsuario.usu_NombreUsuario,
                                              tde_FechaCrea = tbTipoDeducciones.tde_FechaCrea,

                                              tde_UsuarioModifica = tbTipoDeducciones.tde_UsuarioModifica,
                                              UsuModifica = tbTipoDeducciones.tbUsuario1.usu_NombreUsuario,
                                              tde_FechaModifica = tbTipoDeducciones.tde_FechaModifica
                                          };

            // evitar referencias circulares
            db.Configuration.ProxyCreationEnabled = false;

            // retornar registro en formato JSON
            return Json(tbTechosDeduccionesJSON, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region POST: Editar
        [HttpPost]
        [SessionManager("TipoDeducciones/Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tde_IdTipoDedu,tde_Descripcion,tde_UsuarioCrea,tde_FechaCrea,tde_UsuarioModifica,tde_FechaModifica,tde_Activo")] tbTipoDeduccion tbTipoDeduccion)
        {
            // data de auditoria
            tbTipoDeduccion.tde_UsuarioCrea = Function.GetUser();
            tbTipoDeduccion.tde_FechaCrea = Function.DatetimeNow();


            tbTipoDeduccion.tde_UsuarioModifica = Function.GetUser();
            tbTipoDeduccion.tde_FechaModifica = Function.DatetimeNow();
            // vairable de resultados
            string response = String.Empty;
            IEnumerable<object> listTipoDeduccion = null;
            string MensajeError = "";

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listTipoDeduccion = db.UDP_Plani_tbTipoDeduccion_Update(tbTipoDeduccion.tde_IdTipoDedu,
                                                                            tbTipoDeduccion.tde_Descripcion,
                                                                            tbTipoDeduccion.tde_UsuarioModifica,
                                                                            tbTipoDeduccion.tde_FechaModifica);

                    // obtener resultado del PA
                    foreach (UDP_Plani_tbTipoDeduccion_Update_Result resultado in listTipoDeduccion)
                        MensajeError = resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el PA falló
                        ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    // se generó una excepción
                    ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                    response = "error";
                }
                // el proceso fue exitoso
                response = "bien";
            }
            else
            {
                // el modelo no es válido
                ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador");
                response = "error";
            }

            // retornar resultado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Inactivar
        [SessionManager("TipoDeducciones/Inactivar")]
        public JsonResult Inactivar(int? ID)
        {
            // validar si se recibó el ID
            if (ID == null)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

            // variables de resultado 
            string response = String.Empty;
            IEnumerable<object> listTipoDeduccion = null;
            string MensajeError = "";

            try
            {
                // ejecutar PA
                listTipoDeduccion = db.UDP_Plani_tbTipoDeduccion_Inactivar(ID, 1, DateTime.Now);

                // obtener resultado del PA
                foreach (UDP_Plani_tbTipoDeduccion_Inactivar_Result resultado in listTipoDeduccion.ToList())
                    MensajeError = resultado.MensajeError;

                if (MensajeError.StartsWith("-1"))
                {
                    // el PA falló
                    ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador");
                    response = "error";
                }
            }
            catch (Exception Ex)
            {
                // se generó una excepción
                ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                response = "error";
            }

            // retornar resultado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Activar
        [SessionManager("TipoDeducciones/Activar")]
        public JsonResult Activar(int? ID)
        {
            //validar si se recibió el ID 
            if (ID == null)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

            // variables de resultado
            string response = String.Empty;
            IEnumerable<object> listTipoDeduccion = null;
            string MensajeError = "";

            try
            {
                // ejecutar PA
                listTipoDeduccion = db.UDP_Plani_tbTipoDeduccion_Activar(ID, 1, DateTime.Now);

                // obtener resultado del PA
                foreach (UDP_Plani_tbTipoDeduccion_Activar_Result resultado in listTipoDeduccion.ToList())
                    MensajeError = resultado.MensajeError;

                if (MensajeError.StartsWith("-1"))
                {
                    // el PA falló
                    ModelState.AddModelError("", "No se pudo activar el registro, contacte al administrador");
                    response = "error";
                }
            }
            catch (Exception Ex)
            {
                // se generó una excepción
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
