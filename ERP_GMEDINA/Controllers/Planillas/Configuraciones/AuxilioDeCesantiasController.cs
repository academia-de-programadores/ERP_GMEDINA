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
    public class AuxilioDeCesantiasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        private ERP_GMEDINA.Models.Helpers Function = new Models.Helpers();

        #region index
        [SessionManager("AuxilioDeCesantias/Index")]
        public ActionResult Index()
        {
            var tbAuxilioDeCesantias = db.tbAuxilioDeCesantias.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbAuxilioDeCesantias.ToList());
        }
        #endregion

        #region Get data

        public ActionResult GetData()
        {
            var tbAuxilioCesantia1 = db.tbAuxilioDeCesantias
                                        .Select(c => new
                                        {
                                            aces_IdAuxilioCesantia = c.aces_IdAuxilioCesantia,
                                            aces_RangoInicioMeses = c.aces_RangoInicioMeses,
                                            aces_RangoFinMeses = c.aces_RangoFinMeses,
                                            aces_DiasAuxilioCesantia = c.aces_DiasAuxilioCesantia,
                                            aces_UsuarioCrea = c.aces_UsuarioCrea,
                                            aces_FechaCrea = c.aces_FechaCrea,
                                            aces_UsuarioModifica = c.aces_UsuarioModifica,
                                            aces_FechaModifica = c.aces_FechaModifica,
                                            aces_Activo = c.aces_Activo
                                        })
                                        .OrderByDescending(x => x.aces_FechaCrea)
                                        .ToList();
            // retornar json
            return new JsonResult { Data = tbAuxilioCesantia1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region Details
        [SessionManager("AuxilioDeCesantias/Details")]
        public JsonResult Details(int? ID)
        {
            var tbAuxCesanJSON = from tbAuxilioDeCesantias in db.tbAuxilioDeCesantias
                                 where tbAuxilioDeCesantias.aces_IdAuxilioCesantia == ID
                                 orderby tbAuxilioDeCesantias.aces_FechaCrea descending
                                 select new
                                 {
                                     tbAuxilioDeCesantias.aces_IdAuxilioCesantia,
                                     tbAuxilioDeCesantias.aces_RangoInicioMeses,
                                     tbAuxilioDeCesantias.aces_RangoFinMeses,
                                     tbAuxilioDeCesantias.aces_DiasAuxilioCesantia,
                                     UsuCrea = tbAuxilioDeCesantias.tbUsuario.usu_NombreUsuario,
                                     tbAuxilioDeCesantias.aces_FechaCrea,
                                     tbAuxilioDeCesantias.aces_UsuarioModifica,
                                     UsuModifica = tbAuxilioDeCesantias.tbUsuario1.usu_NombreUsuario,
                                     tbAuxilioDeCesantias.aces_FechaModifica
                                 };


            db.Configuration.ProxyCreationEnabled = false;
            return Json(tbAuxCesanJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create
        [HttpPost]
        [SessionManager("AuxilioDeCesantias/Create")]
        public ActionResult Create(tbAuxilioDeCesantias tbAuxilioDeCesantias)
        {
            
            // Auditoria
            //tbAuxilioDeCesantias.aces_UsuarioCrea = 1;
            //tbAuxilioDeCesantias.aces_FechaCrea = DateTime.Now;
            tbAuxilioDeCesantias.aces_Activo = true;

            // variables de resultados
            string response = String.Empty;
            IEnumerable<object> listAuxCesantias = null;
            string MensajeError = "";

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar procedimiento almacenado
                    listAuxCesantias = db.UDP_Plani_tbAuxilioDeCesantias_Insert(tbAuxilioDeCesantias.aces_RangoInicioMeses,
                                                                                tbAuxilioDeCesantias.aces_RangoFinMeses,
                                                                                tbAuxilioDeCesantias.aces_DiasAuxilioCesantia,
                                                                                Function.GetUser(),
                                                                                Function.DatetimeNow(),
                                                                                tbAuxilioDeCesantias.aces_Activo);
                    // resultado 
                    foreach (UDP_Plani_tbAuxilioDeCesantias_Insert_Result Resultado in listAuxCesantias)
                        MensajeError = Resultado.MensajeError;
                    
                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    // se generó una excepción
                    response = Ex.Message.ToString();
                }
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

        #region GET: Edit
        [SessionManager("AuxilioDeCesantias/Edit")]
        public JsonResult Edit(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbAuxilioDeCesantias tbAuxilioCesEditJSON = db.tbAuxilioDeCesantias.Find(ID);
            return Json(tbAuxilioCesEditJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: Edit
        [HttpPost]
        [SessionManager("AuxilioDeCesantias/Edit")]
        public ActionResult Edit([Bind(Include = "aces_IdAuxilioCesantia,aces_RangoInicioMeses,aces_RangoFinMeses,aces_DiasAuxilioCesantia,aces_UsuarioCrea,aces_FechaCrea,aces_UsuarioModifica,aces_FechaModifica,aces_Activo")] tbAuxilioDeCesantias tbAuxilioDeCesantias)
        {
            // auditoria
            //tbAuxilioDeCesantias.aces_UsuarioModifica = 1;
            //tbAuxilioDeCesantias.aces_FechaModifica = DateTime.Now;

            // variables de resultado
            string response = String.Empty;
            IEnumerable<object> listAuxCes = null;
            string MensajeError = "";

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar procedimiento almacenad
                    listAuxCes = db.UDP_Plani_tbAuxilioDeCesantias_Update(tbAuxilioDeCesantias.aces_IdAuxilioCesantia, 
                                                                          tbAuxilioDeCesantias.aces_RangoInicioMeses,
                                                                          tbAuxilioDeCesantias.aces_RangoFinMeses,
                                                                          tbAuxilioDeCesantias.aces_DiasAuxilioCesantia, 
                                                                          Function.GetUser(),
                                                                          Function.DatetimeNow());
                    
                    // validar resultado del procedimiento almacenado
                    foreach (UDP_Plani_tbAuxilioDeCesantias_Update_Result Resultado in listAuxCes)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el procedimiento almacenado falló
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        response = "error";
                    }
                }
                catch (Exception)
                {
                    // se generó una excepción
                    ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                    response = "error";
                }
            }
            else
            {
                // el modelo no es válido
                ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                response = "error";
            }

            // retornar resultado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Inactivar
        [HttpPost]
        [SessionManager("AuxilioDeCesantias/Inactivar")]
        public ActionResult Inactivar(int ID)
        {
            // variables de resulado
            string response = String.Empty;
            IEnumerable<object> listAuxilioCesantia = null;
            string MensajeError = "";

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar el procedimiento almacenado
                    listAuxilioCesantia = db.UDP_Plani_tbAuxilioDeCesantias_Delete(ID, Function.GetUser(), Function.DatetimeNow());

                    // resultado del procedimiento almacenado
                    foreach (UDP_Plani_tbAuxilioDeCesantias_Delete_Result Resultado in listAuxilioCesantia)
                        MensajeError = Resultado.MensajeError;


                    // el proceso fue exitoso
                    response = "bien";
                }
                catch (Exception)
                {
                    // se generó una excepción
                    ModelState.AddModelError("", "No se logró eliminar el registro, contacte al administrador.");
                    response = "error";
                }
            }
            else
            {
                // el modelo no es válido
                ModelState.AddModelError("", "No se logró eliminar el registro, contacte al administrador.");
                response = "error";
            }

            // retorna resultado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Activar
        [SessionManager("AuxilioDeCesantias/Activar")]
        public ActionResult Activar(int id)
        {
            // variables de resultado
            IEnumerable<object> listAuxCes = null;
            string MensajeError = "";
            string response = String.Empty;

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar procedimiento almacenado
                    listAuxCes = db.UDP_Plani_tbAuxilioDeCesantias_Activar(id, Function.GetUser(), Function.DatetimeNow());

                    // resultado del procedimiento almacenado
                    foreach (UDP_Plani_tbAuxilioDeCesantias_Activar_Result Resultado in listAuxCes)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el procedimiento almacenado falló
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
