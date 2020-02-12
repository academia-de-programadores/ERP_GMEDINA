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
    public class TechosDeduccionesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        Models.Helpers Function = new Models.Helpers();

        #region Index
        [SessionManager("TechosDeducciones/Index")]
        public ActionResult Index()
        {
            var tbTechosDeducciones = db.tbTechosDeducciones.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCatalogoDeDeducciones).OrderBy(t => t.tddu_FechaCrea);
            return View(tbTechosDeducciones.ToList());
        }
        #endregion

        #region Get data
        [HttpGet]
        public ActionResult GetData()
        {
            // obtener data
            var otbTechosDeducciones = db.tbTechosDeducciones
                        .Select(c => new { cde_DescripcionDeduccion = c.tbCatalogoDeDeducciones.cde_DescripcionDeduccion,
                            tddu_IdTechosDeducciones = c.tddu_IdTechosDeducciones,
                            tddu_PorcentajeEmpresa = c.tddu_PorcentajeEmpresa,
                            tddu_PorcentajeColaboradores = c.tddu_PorcentajeColaboradores,
                            tddu_Techo = c.tddu_Techo,
                            tddu_Activo = c.tddu_Activo,
                            tede_FechaCrea = c.tddu_FechaCrea }) 
                            .OrderBy(c => c.tede_FechaCrea)
                        .ToList();

            // retornar data
            return new JsonResult { Data = otbTechosDeducciones, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region GET: Details
        [SessionManager("TechosDeducciones/Details")]
        public JsonResult Details(int? ID)
        {
            // validar que se recibió el ID
            if (ID == null)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
            // obtener registro con el id recibido
            var tbTechosDeduccionesJSON = from tbTechosDeducciones in db.tbTechosDeducciones
                                          where tbTechosDeducciones.tddu_IdTechosDeducciones == ID
                                          select new
                                          {
                                              tbTechosDeducciones.tddu_IdTechosDeducciones,
                                              tbTechosDeducciones.tddu_PorcentajeColaboradores,
                                              tbTechosDeducciones.tddu_PorcentajeEmpresa,
                                              tbTechosDeducciones.tddu_Techo,
                                              tbTechosDeducciones.cde_IdDeducciones,
                                              tbTechosDeducciones.tbCatalogoDeDeducciones.cde_DescripcionDeduccion,

                                              tbTechosDeducciones.tddu_UsuarioCrea,
                                              UsuCrea = tbTechosDeducciones.tbUsuario.usu_NombreUsuario,
                                              tbTechosDeducciones.tddu_FechaCrea,

                                              tbTechosDeducciones.tddu_UsuarioModifica,
                                              UsuModifica = tbTechosDeducciones.tbUsuario1.usu_NombreUsuario,
                                              tbTechosDeducciones.tddu_FechaModifica
                                          };

            // evitar referencias circulares
            db.Configuration.ProxyCreationEnabled = false;

            // validar que el objeto no sea nulo
            if (tbTechosDeduccionesJSON == null)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

            // retornar objeto
            return Json(tbTechosDeduccionesJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: Create
        [HttpPost]
        [SessionManager("TechosDeducciones/Create")]
        public ActionResult Create(tbTechosDeducciones tbTechosDeducciones)
        {
            // data de auditoria
            tbTechosDeducciones.tddu_UsuarioCrea = Function.GetUser();
            tbTechosDeducciones.tddu_FechaCrea = Function.DatetimeNow();
            
            // variables de resultado
            string response = String.Empty;
            IEnumerable<object> listTechosDeducciones = null;
            string MensajeError = "";
            
            // validar que el modelo sea válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listTechosDeducciones = db.UDP_Plani_tbTechosDeducciones_Insert(tbTechosDeducciones.tddu_PorcentajeColaboradores,
                                                                                     tbTechosDeducciones.tddu_PorcentajeEmpresa,
                                                                                     tbTechosDeducciones.tddu_Techo,
                                                                                     tbTechosDeducciones.cde_IdDeducciones,
                                                                                     tbTechosDeducciones.tddu_UsuarioCrea,
                                                                                     tbTechosDeducciones.tddu_FechaCrea);

                    // obtener resultado del PA
                    foreach (UDP_Plani_tbTechosDeducciones_Insert_Result Resultado in listTechosDeducciones)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el PA falló
                        ModelState.AddModelError("", "No se pudo ingresar el registro. Contacte al administrador.");
                        response = "error";
                    }

                    // el proceso fue exitoso
                    response = "bien";
                }
                catch (Exception Ex)
                {
                    // se generó una excepción
                    response = "bien";
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

        #region GET: Edit
        [SessionManager("TechosDeducciones/Edit")]
        public JsonResult Edit(int? id)
        {
            // validar que se recibió el id
            if (id == null)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
            // evitar referencias circulares
            db.Configuration.ProxyCreationEnabled = false;
            
            // obtener registro
            tbTechosDeducciones tbTechosDeduccionesJSON = db.tbTechosDeducciones.Find(id);

            // retornar objeto
            return Json(tbTechosDeduccionesJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: Edit
        [HttpPost]
        [SessionManager("TechosDeducciones/Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbTechosDeducciones tbTechosDeducciones)
        {
            // data de auditoria
            tbTechosDeducciones.tddu_UsuarioModifica = Function.GetUser();
            tbTechosDeducciones.tddu_FechaModifica  = Function.DatetimeNow();
            
            // variables de resultados
            IEnumerable<object> listTechosDeducciones = null;
            string MensajeError = "";
            string response = String.Empty;
            
            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listTechosDeducciones = db.UDP_Plani_tbTechosDeducciones_Update(tbTechosDeducciones.tddu_IdTechosDeducciones,
                                                                                    tbTechosDeducciones.tddu_PorcentajeColaboradores,
                                                                                     tbTechosDeducciones.tddu_PorcentajeEmpresa,
                                                                                     tbTechosDeducciones.tddu_Techo,
                                                                                     tbTechosDeducciones.cde_IdDeducciones, //ID del porcentaje de deducción
                                                                                     tbTechosDeducciones.tddu_UsuarioModifica,
                                                                                     tbTechosDeducciones.tddu_FechaModifica);  
                    // obtener resultado del PA
                    foreach (UDP_Plani_tbTechosDeducciones_Update_Result Resultado in listTechosDeducciones.ToList())
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el PA falló
                        ModelState.AddModelError("", "No se pudo actualizar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    // se generó una excepción
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

        #region EditGetDDL
        public JsonResult EditGetDDL()
        {
            // obtener data
            var DDL =
            from CatDeduc in db.tbCatalogoDeDeducciones
            join TechDeduc in db.tbTechosDeducciones on CatDeduc.cde_IdDeducciones equals TechDeduc.cde_IdDeducciones into prodGroup
            select new { Id = CatDeduc.cde_IdDeducciones, Descripcion = CatDeduc.cde_DescripcionDeduccion };
            
            // retorna data
            return Json(DDL, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Inactivar 
        [SessionManager("TechosDeducciones/Inactivar")]
        public ActionResult Inactivar(int id)
        {
            // validar si se recibió el ID
            if (id == null)
            {
                return Json("Error",JsonRequestBehavior.AllowGet);
            }

            // variables de resultados
            IEnumerable<object> listTechosDeducciones = null;
            string MensajeError = "";
            string response = String.Empty;

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listTechosDeducciones = db.UDP_Plani_tbTechosDeducciones_Inactivar(id,
                                                                                       Function.GetUser(),
                                                                                       Function.DatetimeNow());

                    // obtener resultado del PA
                    foreach (UDP_Plani_tbTechosDeducciones_Inactivar_Result Resultado in listTechosDeducciones)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el PA falló
                        ModelState.AddModelError("", "No se pudo actualizar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception)
                {
                    // se generó una excepción
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
            return Json(response,JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Activar
        [SessionManager("TechosDeducciones/Activar")]
        public ActionResult Activar(int id)
        {
            // vairables de resultado
            IEnumerable<object> listTechosDeducciones = null;
            string MensajeError = "";
            string response = String.Empty;

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listTechosDeducciones = db.UDP_Plani_tbTechosDeducciones_Activar(id,
                                                                                     Function.GetUser(),
                                                                                     Function.DatetimeNow());

                    // obtener resultado del PA
                    foreach (UDP_Plani_tbTechosDeducciones_Activar_Result Resultado in listTechosDeducciones)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el PA falló
                        ModelState.AddModelError("", "No se pudo activar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception)
                {
                    // se generó una excepción
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
            return Json(response,JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Dispose
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
