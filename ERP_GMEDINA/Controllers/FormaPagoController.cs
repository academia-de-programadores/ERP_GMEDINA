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
    public class FormaPagoController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        Models.Helpers Function = new Models.Helpers();

        #region INDEX
        [SessionManager("FormaPago/Index")]
        // GET: FormaPago
        public ActionResult Index()
        {
            var tbFormaPago = db.tbFormaPago.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).OrderBy(x => x.fpa_IdFormaPago);
            return View(tbFormaPago.ToList());
        }
        #endregion

        #region GET: DATA PARA RECARGAR EL DATATABLE 
        //GET: DATA
        public ActionResult GetData()
        {
            db.Configuration.ProxyCreationEnabled = false;

            var tbFormaPago = db.tbFormaPago
                .Select(c => new {
                    fpa_IdFormaPago = c.fpa_IdFormaPago,
                    fpa_Descripcion = c.fpa_Descripcion,
                    fpa_UsuarioCrea = c.fpa_UsuarioCrea,
                    NombreUsuarioCrea = c.tbUsuario.usu_NombreUsuario,
                    fpa_FechaCrea = c.fpa_FechaCrea,
                    fpa_UsuarioModifica = c.fpa_UsuarioModifica,
                    NombreUsuarioModifica = c.tbUsuario1.usu_NombreUsuario,
                    fpa_FechaModifica = c.fpa_FechaModifica,
                    fpa_Activo = c.fpa_Activo
                }).OrderBy(x => x.fpa_IdFormaPago).ToList();

            return new JsonResult { Data = tbFormaPago, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region POST: CREATE
        [SessionManager("FormaPago/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "fpa_IdFormaPago,fpa_Descripcion,fpa_UsuarioCrea,fpa_FechaCrea")] tbFormaPago tbFormaPago)
        {
            // data de auditoria
            tbFormaPago.fpa_UsuarioCrea = Function.GetUser();
            tbFormaPago.fpa_FechaCrea = Function.DatetimeNow();
            
            // variables de resultado
            string response = "bien";
            IEnumerable<object> listFormaPago = null;
            String MessageError = "";

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar procedimiento almacenado
                    listFormaPago = db.UDP_Plani_tbFormaPago_Insert(tbFormaPago.fpa_Descripcion,
                                                                            tbFormaPago.fpa_UsuarioCrea,
                                                                            tbFormaPago.fpa_FechaCrea);

                    // resultado del procedimiento almacenado                                               
                    foreach (UDP_Plani_tbFormaPago_Insert_Result resultado in listFormaPago)
                        MessageError = Convert.ToString(resultado);

                    if (MessageError.StartsWith("-1"))
                    {
                        // el procedimiento almacenado falló
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                     // se generó una excepción
                    ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                    response = "error" + Ex.Message.ToString();
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

        #region GET: EDITAR 
        [SessionManager("FormaPago/Edit")]
        public JsonResult Edit(int? id)
		{
            // si no se recibió ningún ID
			if (id == null)
			{
				return Json("Error", JsonRequestBehavior.AllowGet);
			}

            // obtener objeto
			var tbFormaPago = db.tbFormaPago
                                .Where(d => d.fpa_IdFormaPago == id)
						        .Select(c => new { fpa_Descripcion = c.fpa_Descripcion, fpa_IdFormaPago = c.fpa_IdFormaPago, fpa_UsuarioCrea = c.fpa_UsuarioCrea, fpa_FechaCrea = c.fpa_FechaCrea, fpa_UsuarioModifica = c.fpa_UsuarioModifica, fpa_FechaModifica = c.fpa_FechaModifica, fpa_Activo = c.fpa_Activo })
						        .ToList();

			// si no se obtuvo ningún objeto
			if (tbFormaPago == null)
			{
				return Json("Error", JsonRequestBehavior.AllowGet);
			}

            // retornar objeto al procedimiento almacenado
            return Json(tbFormaPago, JsonRequestBehavior.AllowGet);
		}
        #endregion

        #region POST: EDITAR
        [SessionManager("Bodega/Editar")]
        [HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult Editar([Bind(Include = "fpa_IdFormaPago,fpa_Descripcion")] tbFormaPago tbFormaPago)
		{
			// variables auditoria
			tbFormaPago.fpa_UsuarioModifica = Function.GetUser();
			tbFormaPago.fpa_FechaModifica = Function.DatetimeNow();
			
            // variables de resultado
			string response = "bien";
			IEnumerable<object> listFormaPago = null;
			String MessageError = "";

			// validar si el modelo es válido
			if (ModelState.IsValid)
			{
				try
				{
					// ejecutar procedimiento almacenado
					listFormaPago = db.UDP_Plani_tbFormaPago_Update(tbFormaPago.fpa_IdFormaPago, 
																	tbFormaPago.fpa_Descripcion,
																	tbFormaPago.fpa_UsuarioModifica,
																	tbFormaPago.fpa_FechaModifica);

					// resultado del procedimiento almacenado                                            
					foreach (UDP_Plani_tbFormaPago_Update_Result resultado in listFormaPago)
						MessageError = Convert.ToString(resultado);

					if (MessageError.StartsWith("-1"))
					{
						// el procedimiento almacenado falló
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

            // retornar el resultado del proceso
			return Json(response, JsonRequestBehavior.AllowGet);
		}
        #endregion

        #region GET: DETAILS
        [SessionManager("FormaPago/Details")]
        public JsonResult Details(int ID)
        {
            // evitar referencias circulares
            db.Configuration.ProxyCreationEnabled = false;

            // obtener data de la bbdd
            var tbFormaPagoJSON = from tbFormaPago in db.tbFormaPago
                                 where tbFormaPago.fpa_IdFormaPago == ID
                                 orderby tbFormaPago.fpa_FechaCrea descending
                                 select new
                                 {
                                     fpa_IdFormaPago = tbFormaPago.fpa_IdFormaPago,
                                     fpa_Descripcion = tbFormaPago.fpa_Descripcion,
                                     fpa_UsuarioCrea = tbFormaPago.fpa_UsuarioCrea,
                                     UsuCrea = tbFormaPago.tbUsuario.usu_NombreUsuario,
                                     fpa_FechaCrea = tbFormaPago.fpa_FechaCrea,
                                     fpa_UsuarioModifica = tbFormaPago.fpa_UsuarioModifica,
                                     UsuModifica = tbFormaPago.tbUsuario1.usu_NombreUsuario,
                                     fpa_FechaModifica = tbFormaPago.fpa_FechaModifica,
                                     fpa_Activo = tbFormaPago.fpa_Activo
                                 };

            // retornar data
            return Json(tbFormaPagoJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: INACTIVAR
        [SessionManager("FormaPago/Inactivar")]
        [HttpPost]
        public ActionResult Inactivar(int? Id)
        {
            // vairables de resultado
            string response = "bien";
            IEnumerable<object> listFormaPago = null;
            string MensajeError = "";

            // validar si no llegó ningún ID
            if (Id == null)
            {
                response = "error";
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            // instancia del objeto 
            tbFormaPago tbFormaPago = new tbFormaPago();

            // data de auditoria
            tbFormaPago.fpa_IdFormaPago = (int)Id;
            tbFormaPago.fpa_UsuarioModifica = Function.GetUser();
            tbFormaPago.fpa_FechaModifica = Function.DatetimeNow();
            
            // validar si el ID es válido
            if (tbFormaPago.fpa_IdFormaPago > 0)
            {
                try
                {
                    // ejecutar procedimiento almacenado
                    listFormaPago = db.UDP_Plani_tbFormaPago_Inactivar(tbFormaPago.fpa_IdFormaPago,
                                                                               tbFormaPago.fpa_UsuarioModifica,
                                                                               tbFormaPago.fpa_FechaModifica);

                    // resultado del procedimiento almacenado
                    foreach (UDP_Plani_tbFormaPago_Inactivar_Result Resultado in listFormaPago)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el procedimiento almacenado falló
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
                // el modelo no es válido
                ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador.");
                response = "error";
            }

            // retornar resultado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: ACTIVAR
        [SessionManager("FormaPago/Activar")]
        [HttpPost]
        public ActionResult Activar(int? Id)
        {
            // variables de resultados del proceso
            string response = "bien";
            IEnumerable<object> listFormaPago = null;
            string MensajeError = "";

            // validar si se recibió algún ID
            if (Id == null)
            {
                response = "error";
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            // instancia del objeto
            tbFormaPago tbFormaPago = new tbFormaPago();
            
            // data de auditoria
            tbFormaPago.fpa_IdFormaPago = (int)Id;
            tbFormaPago.fpa_UsuarioModifica = Function.GetUser();
            tbFormaPago.fpa_FechaModifica = Function.DatetimeNow();
            
            // validar si el ID es válido
            if (tbFormaPago.fpa_IdFormaPago > 0)
            {
                try
                {
                    // ejecutar procedimiento almacenado
                    listFormaPago = db.UDP_Plani_tbFormaPago_Activar(tbFormaPago.fpa_IdFormaPago,
                                                                     tbFormaPago.fpa_UsuarioModifica,
                                                                     tbFormaPago.fpa_FechaModifica);

                    // resultado del procedimiento almacenado
                    foreach (UDP_Plani_tbFormaPago_Activar_Result Resultado in listFormaPago)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el procedimiento almacenado falló
                        ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador");
                        response = "error";
                    }

                }
                catch (Exception Ex)
                {
                    // se generó una excepción
                    response = Ex.Message.ToString();
                }
            }
            else
            {
                // el modelo no es válido
                ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador.");
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
