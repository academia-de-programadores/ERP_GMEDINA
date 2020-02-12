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
    public class PeriodosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        Models.Helpers Function = new Models.Helpers();

        #region INDEX
        [SessionManager("Periodos/Index")]
        public ActionResult Index()
        {
            var tbPeriodos = db.tbPeriodos.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).OrderBy(x => x.peri_IdPeriodo);
            return View(tbPeriodos.ToList());
        }
        #endregion

        #region DATA PARA RECARGAR EL DATATABLE
        public ActionResult GetData()
        {
            // evitar referencias circulares
            db.Configuration.ProxyCreationEnabled = false;

            // obtener data para refrescar datatable
            var tbPeriodos = db.tbPeriodos
                .Select(c => new {
                    peri_IdPeriodo = c.peri_IdPeriodo,
                    peri_DescripPeriodo = c.peri_DescripPeriodo,
                    peri_CantidadDias = c.peri_CantidadDias,
                    peri_RecibeSeptimoDia = c.peri_RecibeSeptimoDia,
                    fpa_UsuarioCrea = c.peri_UsuarioCrea,
                    NombreUsuarioCrea = c.tbUsuario.usu_NombreUsuario,
                    peri_FechaCrea = c.peri_FechaCrea,
                    peri_UsuarioModifica = c.peri_UsuarioModifica,
                    NombreUsuarioModifica = c.tbUsuario1.usu_NombreUsuario,
                    peri_FechaModifica = c.peri_FechaModifica,
                    peri_Activo = c.peri_Activo
                }).OrderBy(x => x.peri_IdPeriodo).ToList();

            // retornar data
            return new JsonResult { Data = tbPeriodos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region POST: CREATE
        [HttpPost]
        [SessionManager("Periodos/Create")]
        public ActionResult Create([Bind(Include = "peri_DescripPeriodo, peri_CantidadDias, peri_RecibeSeptimoDia")] tbPeriodos tbPeriodos)
        {
            // data de auditoria
            tbPeriodos.peri_UsuarioCrea = Function.GetUser();
            tbPeriodos.peri_FechaCrea = Function.DatetimeNow();
            tbPeriodos.peri_Activo = true;

            // variables de resultado
            string response = "bien";
            IEnumerable<object> listPeriodo = null;
            String MessageError = "";

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listPeriodo = db.UDP_Plani_tbPeriodos_Insert(tbPeriodos.peri_DescripPeriodo,
                                                                            tbPeriodos.peri_UsuarioCrea,
                                                                            tbPeriodos.peri_FechaCrea,
                                                                            tbPeriodos.peri_RecibeSeptimoDia,
                                                                            tbPeriodos.peri_CantidadDias);

                    // obtener resultado del PA
                    foreach (UDP_Plani_tbPeriodos_Insert_Result resultado in listPeriodo)
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

        #region GET: EDIT
        [SessionManager("Periodos/Edit")]
        public JsonResult Edit(int? id)
        {
            // validar si se recibió el ID
            if (id == null)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

            // obtener registro con ese ID
            var tbPeriodo = db.tbPeriodos.Where(d => d.peri_IdPeriodo == id)
                        .Select(c => new { peri_DescripPeriodo = c.peri_DescripPeriodo, peri_CantidadDias = c.peri_CantidadDias, peri_RecibeSeptimoDia = c.peri_RecibeSeptimoDia, peri_IdPeriodo = c.peri_IdPeriodo, peri_UsuarioCrea = c.peri_UsuarioCrea, peri_FechaCrea = c.peri_FechaCrea, peri_UsuarioModifica = c.peri_UsuarioModifica, peri_FechaModifica = c.peri_FechaModifica, peri_Activo = c.peri_Activo })
                        .ToList();

            // si no hay ningún objeto con ese ID, retornar error
            if (tbPeriodo == null)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

            // retornar objeto
            return Json(tbPeriodo, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: EDITAR    
        [HttpPost]
        [SessionManager("Periodos/Edit")]
        public JsonResult Editar([Bind(Include = "peri_IdPeriodo,peri_DescripPeriodo, peri_CantidadDias, peri_RecibeSeptimoDia")] tbPeriodos tbPeriodos)
        {
            // data de auditoria
            tbPeriodos.peri_UsuarioModifica = Function.GetUser();
            tbPeriodos.peri_FechaModifica = Function.DatetimeNow();
            tbPeriodos.peri_Activo = true;

            // variables de resultado
            string response = "bien";
            IEnumerable<object> listPeriodo = null;
            String MessageError = "";

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listPeriodo = db.UDP_Plani_tbPeriodos_Update(tbPeriodos.peri_IdPeriodo,
                                                                    tbPeriodos.peri_DescripPeriodo,
                                                                    tbPeriodos.peri_UsuarioModifica,
                                                                    tbPeriodos.peri_FechaModifica,
                                                                    tbPeriodos.peri_RecibeSeptimoDia,
                                                                    tbPeriodos.peri_CantidadDias);

                    // obtener resultado del PA
                    foreach (UDP_Plani_tbPeriodos_Update_Result resultado in listPeriodo)
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

        #region GET: DETAILS
        [SessionManager("Periodos/Details")]
        public JsonResult Details(int? id)
        {
            // validar si se recibió el id
            if (id == null)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

            // obtener objeto con el ID recibido
            var tbCatalogoPeriodoJSON = from tbPeriod in db.tbPeriodos
                                        where tbPeriod.peri_IdPeriodo == id
                                        select new
                                        {
                                            tbPeriod.peri_IdPeriodo,
                                            tbPeriod.peri_DescripPeriodo,
                                            tbPeriod.peri_CantidadDias,
                                            tbPeriod.peri_RecibeSeptimoDia,
                                            tbPeriod.peri_Activo,
                                            tbPeriod.peri_UsuarioCrea,
                                            UsuCrea = tbPeriod.tbUsuario.usu_NombreUsuario,
                                            tbPeriod.peri_FechaCrea,
                                            tbPeriod.peri_UsuarioModifica,
                                            UsuModifica = tbPeriod.tbUsuario1.usu_NombreUsuario,
                                            tbPeriod.peri_FechaModifica
                                        };


            // evitar referencias circulares
            db.Configuration.ProxyCreationEnabled = false;
            return Json(tbCatalogoPeriodoJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: INACTIVAR
        [HttpPost]
        [SessionManager("Periodos/Inactivar")]
        public ActionResult Inactivar(int? id)
        {
            // validar si se recibió el id 
            if (id == null)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

            // vairables de resultado 
            string response = "bien";
            IEnumerable<object> listPeriodo = null;
            string MensajeError = "";

            // instancia del objeto
            tbPeriodos tbPeriodos = new tbPeriodos();

            // data del objeto
            tbPeriodos.peri_IdPeriodo = (int)id;
            tbPeriodos.peri_UsuarioModifica = Function.GetUser();
            tbPeriodos.peri_FechaModifica = Function.DatetimeNow();

            // validar que el id sea válido
            if (tbPeriodos.peri_IdPeriodo > 0)
            {
                try
                {
                    // ejecutar PA
                    listPeriodo = db.UDP_Plani_tbPeriodos_Inactivar(tbPeriodos.peri_IdPeriodo,
                                                                    tbPeriodos.peri_UsuarioModifica,
                                                                    tbPeriodos.peri_FechaModifica);

                    // obtener resultado del PA
                    foreach (UDP_Plani_tbPeriodos_Inactivar_Result Resultado in listPeriodo)
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
                // el id no es válido
                ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador.");
                response = "error";
            }

            // retornar resulado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: ACTIVAR      
        [HttpPost]
        [SessionManager("Periodos/Activar")]
        public ActionResult Activar(int? id)
        {
            // variables de resultaod
            string response = "bien";
            IEnumerable<object> listPeriodo = null;
            string MensajeError = "";

            // validar si se recibió el id
            if (id == null)
            {
                response = "error";
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            // instancias del objeto
            tbPeriodos tbPeriodos = new tbPeriodos();

            // data del objeto
            tbPeriodos.peri_IdPeriodo = (int)id;
            tbPeriodos.peri_UsuarioModifica = Function.GetUser();
            tbPeriodos.peri_FechaModifica = Function.DatetimeNow();

            // validar si el ID es válido
            if (tbPeriodos.peri_IdPeriodo > 0)
            {
                try
                {
                    // ejecutar PA
                    listPeriodo = db.UDP_Plani_tbPeriodos_Activar(tbPeriodos.peri_IdPeriodo,
                                                                  tbPeriodos.peri_UsuarioModifica,
                                                                  tbPeriodos.peri_FechaModifica);

                    // obetener resultado del PA
                    foreach (UDP_Plani_tbPeriodos_Activar_Result Resultado in listPeriodo)
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
                // el ID es válido
                ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador.");
                response = "error";
            }

            // retornar el resultado del proceso
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
