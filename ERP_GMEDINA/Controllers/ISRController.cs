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
    public class ISRController : Controller
    {

        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        private ERP_GMEDINA.Models.Helpers Function = new ERP_GMEDINA.Models.Helpers();

        [SessionManager("ISR/Index")]
        #region Index
        public ActionResult Index()
        {
            var tbISR = db.tbISR.OrderBy(t => t.isr_FechaCrea).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbTipoDeduccion);
            return View(tbISR.ToList());
        }
        #endregion

        #region Get data
        [HttpGet]
        // GET: data para refrescar datatable
        public ActionResult GetData()
        {
            var otbISR = db.tbISR
                        .Select(c => new { tde_Descripcion = c.tbTipoDeduccion.tde_Descripcion, isr_Id = c.isr_Id, isr_RangoInicial = c.isr_RangoInicial, isr_RangoFinal = c.isr_RangoFinal, isr_Porcentaje = c.isr_Porcentaje, isr_Activo = c.isr_Activo, isr_FechaCrea = c.isr_FechaCrea }).OrderByDescending(c => c.isr_FechaCrea)
                        .ToList();

            // retornar data
            return new JsonResult { Data = otbISR, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region GET: Details
        [SessionManager("ISR/Details")]
        public JsonResult Details(int? ID)
        {
            // validar si se obtuvo un ID
            var response = String.Empty;
            if (ID == null) {

                response = "Error";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            // obtener registro con el ID recibido
            var tbISRJSON = from tbISR in db.tbISR
                            where tbISR.isr_Id == ID
                            select new
                            {
                                tbISR.isr_Id,
                                tbISR.isr_RangoInicial,
                                tbISR.isr_RangoFinal,
                                tbISR.isr_Porcentaje,
                                tbISR.tde_IdTipoDedu,
                                tde_Descripcion =  tbISR.tbTipoDeduccion.tde_Descripcion,

                                tbISR.isr_UsuarioCrea,
                                UsuCrea = tbISR.tbUsuario.usu_NombreUsuario,
                                tbISR.isr_FechaCrea,

                                tbISR.isr_UsuarioModifica,
                                UsuModifica = tbISR.tbUsuario1.usu_NombreUsuario,
                                tbISR.isr_FechaModifica
                            };

            // evitar referencias circulares
            db.Configuration.ProxyCreationEnabled = false;

            // retornar objeto 
            return Json(tbISRJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GET: Create
        [SessionManager("ISR/Create")]
        public ActionResult Create()
        {
            ViewBag.isr_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.isr_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion");
            return View();
        }
        #endregion

        #region POST: Create
        [SessionManager("ISR/Create")]
        [HttpPost]
        public ActionResult Create([Bind(Include = "isr_RangoInicial,isr_RangoFinal,isr_Porcentaje,tde_IdTipoDedu,isr_UsuarioCrea,isr_FechaCrea")] tbISR tbISR)
        {
            // data de auditoria
            //tbISR.isr_UsuarioCrea = 1;
            //tbISR.isr_FechaCrea = DateTime.Now;
            
            // variables de resultado del proceso
            string response = String.Empty;
            IEnumerable<object> listISR = null;
            string MensajeError = "";
            
            // validar si el modelo es válid
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar procedimiento almacenado
                    listISR = db.UDP_Plani_tbISR_Insert(tbISR.isr_RangoInicial,
                                                        tbISR.isr_RangoFinal,
                                                        tbISR.isr_Porcentaje,
                                                        tbISR.tde_IdTipoDedu,
                                                        Function.GetUser(),
                                                        Function.DatetimeNow());

                    // resultado del procedimiento almacenado
                    foreach (UDP_Plani_tbISR_Insert_Result Resultado in listISR)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el procedimiento almacenado falló
                        ModelState.AddModelError("", "No se pudo ingresar el registro. Contacte al administrador.");
                        response = "error";
                    }

                    // el proceso fue exitoso
                    response = "bien";
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
                response = "error";
            }

            // retornar resultado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GET: Edit
        [SessionManager("ISR/Edit")]
        public JsonResult Edit(int? id)
        {
            // evitar referencias circulares
            db.Configuration.ProxyCreationEnabled = false;

            // validar si se recibió algún ID
            if (id == null)
            {
                string response = String.Empty;
                response = "error";
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            // encontrar el objeto con el ID recibido
            tbISR tbISRJSON = db.tbISR.Find(id);

            // retornar objeto
            return Json(tbISRJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: Edit
        [SessionManager("ISR/Edit")]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "isr_Id,isr_RangoInicial,isr_RangoFinal,isr_Porcentaje,tde_IdTipoDedu,isr_UsuarioCrea,isr_FechaCrea")] tbISR tbISR)
        {
            // variables de auditoria
            //tbISR.isr_UsuarioModifica = 1;
            //tbISR.isr_FechaModifica = DateTime.Now;

            // variables de resultado del proceso
            IEnumerable<object> listISR = null;
            string MensajeError = "";
            string response = String.Empty;

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listISR = db.UDP_Plani_tbISR_Update(tbISR.isr_Id,
                                                        tbISR.isr_RangoInicial,
                                                        tbISR.isr_RangoFinal,
                                                        tbISR.isr_Porcentaje,
                                                        tbISR.tde_IdTipoDedu, //ID del tipo de la deducción
                                                        Function.GetUser(),
                                                        Function.DatetimeNow());

                    // obtener resultado del procedimiento almacenado
                    foreach (UDP_Plani_tbISR_Update_Result Resultado in listISR.ToList())
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
                    response = Ex.Message.ToString();
                }

                // el proceso fue exitoso
                response = "bien";
            }
            else
            {
                // el modelo no es válido
                response = "error";
            }

            // reotrnar resultado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region EditGetDDL
        public JsonResult EditGetDDL()
        {
            // obtener data
            var DDL =
            from TipoDeduc in db.tbTipoDeduccion
            join ISR in db.tbISR on TipoDeduc.tde_IdTipoDedu equals ISR.tde_IdTipoDedu into prodGroup
            select new { Id = TipoDeduc.tde_IdTipoDedu, Descripcion = TipoDeduc.tde_Descripcion };
            
            // retornar data
            return Json(DDL, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Inactivar
        [SessionManager("ISR/Inactivar")]
        public ActionResult Inactivar(int id)
        {
            // variables de resultado
            IEnumerable<object> listISR = null;
            string MensajeError = "";
            string response = String.Empty;

            // validar si el model es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listISR = db.UDP_Plani_tbISR_Inactivar(id,
                                                           Function.GetUser(),
                                                           Function.DatetimeNow());

                    // obtener resultado del PA
                    foreach (UDP_Plani_tbISR_Inactivar_Result Resultado in listISR)
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
                // el modelo es inválido
                response = "error";
            }

            // retornar resultado del proceso
            return Json(response,JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Activar
        [SessionManager("ISR/Activar")]
        public ActionResult Activar(int id)
        {
            // variables de resultado
            IEnumerable<object> listISR = null;
            string MensajeError = "";
            string response = String.Empty;

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listISR = db.UDP_Plani_tbISR_Activar(id,
                                                         Function.GetUser(),
                                                         Function.DatetimeNow());

                    // obtener resultado del PA
                    foreach (UDP_Plani_tbISR_Activar_Result Resultado in listISR)
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
                // el modelo es inválido
                response = "error";
            }

            // retornar resultado del proceso
            return Json(JsonRequestBehavior.AllowGet);
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
