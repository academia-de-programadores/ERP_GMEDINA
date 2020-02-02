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
    public class TechoImpuestoVecinalController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        #region Index
        public ActionResult Index()
        {
            var tbTechoImpuestoVecinal = db.tbTechoImpuestoVecinal.OrderBy(t => t.timv_FechaCrea).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbTipoDeduccion);
            return View(tbTechoImpuestoVecinal.ToList());
        }
        #endregion

        #region Get data
        [HttpGet]
        // GET: data para refrescar datatable
        public ActionResult GetData()
        {
            var otbTechoImpuestoVecinal = db.tbTechoImpuestoVecinal
                        .Select(c => new { timv_IdTechoImpuestoVecinal = c.timv_IdTechoImpuestoVecinal,
                                           mun_Nombre = c.tbMunicipio.mun_Nombre,
                                           tde_Descripcion = c.tbTipoDeduccion.tde_Descripcion, 
                                           timv_RangoInicio = c.timv_RangoInicio,
                                           timv_RangoFin = c.timv_RangoFin,
                                           timv_Rango = c.timv_Rango,
                                           timv_Impuesto = c.timv_Impuesto,
                                           timv_Activo = c.timv_Activo}).OrderByDescending(c => c.timv_IdTechoImpuestoVecinal)
                        .ToList();

            // retornar data
            return new JsonResult { Data = otbTechoImpuestoVecinal, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region GET: Details
        public JsonResult Details(int? ID)
        {
            // validar si se obtuvo un ID
            var response = String.Empty;
            if (ID == null)
            {

                response = "Error";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            // obtener registro con el ID recibido
            var tbTechoImpuestoVecinalJSON = from tbTechoImpuestoVecinal in db.tbTechoImpuestoVecinal
                           where tbTechoImpuestoVecinal.timv_IdTechoImpuestoVecinal == ID
                            select new
                            {
                                tbTechoImpuestoVecinal.timv_IdTechoImpuestoVecinal,
                                tbTechoImpuestoVecinal.mun_Codigo,
                                mun_Nombre = tbTechoImpuestoVecinal.tbMunicipio.mun_Nombre,
                                tbTechoImpuestoVecinal.timv_RangoInicio,
                                tbTechoImpuestoVecinal.timv_RangoFin,
                                tbTechoImpuestoVecinal.timv_Rango,
                                tbTechoImpuestoVecinal.timv_Impuesto,
                                tbTechoImpuestoVecinal.tde_IdTipoDedu,
                                tde_Descripcion = tbTechoImpuestoVecinal.tbTipoDeduccion.tde_Descripcion,

                                tbTechoImpuestoVecinal.timv_UsuarioCrea,
                                UsuCrea = tbTechoImpuestoVecinal.tbUsuario.usu_NombreUsuario,
                                tbTechoImpuestoVecinal.timv_FechaCrea,

                                tbTechoImpuestoVecinal.timv_UsuarioModifica,
                                UsuModifica = tbTechoImpuestoVecinal.tbUsuario1.usu_NombreUsuario,
                                tbTechoImpuestoVecinal.timv_FechaModifica
                            };

            // evitar referencias circulares
            db.Configuration.ProxyCreationEnabled = false;

            // retornar objeto 
            return Json(tbTechoImpuestoVecinalJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion
        //EDITAR ESTE FALTAN VIEWBAGS
        #region GET: Create
        public ActionResult Create()
        {
            ViewBag.isr_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.isr_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion");
            return View();
        }
        #endregion

        #region POST: Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "mun_Codigo,tde_IdTipoDedu,timv_RangoInicial,timv_RangoFin,timv_Impuesto,timv_UsuarioCrea,timv_FechaCrea")] tbTechoImpuestoVecinal tbTechoImpuestoVecinal)
        {
            // data de auditoria
            tbTechoImpuestoVecinal.timv_UsuarioCrea = 1;
            tbTechoImpuestoVecinal.timv_FechaCrea = DateTime.Now;

            // variables de resultado del proceso
            string response = String.Empty;
            IEnumerable<object> listTechoImpuestoVecinal = null;
            string MensajeError = "";

            // validar si el modelo es válid
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar procedimiento almacenado
                    listTechoImpuestoVecinal = db.UDP_Plani_tbTechoImpuestoVecinal_Insert(tbTechoImpuestoVecinal.mun_Codigo,
                                                                                          tbTechoImpuestoVecinal.tde_IdTipoDedu,
                                                                                          tbTechoImpuestoVecinal.timv_RangoInicio,
                                                                                          tbTechoImpuestoVecinal.timv_RangoFin,
                                                                                          tbTechoImpuestoVecinal.timv_Rango,
                                                                                          tbTechoImpuestoVecinal.timv_Impuesto,
                                                                                          tbTechoImpuestoVecinal.timv_UsuarioCrea,
                                                                                          tbTechoImpuestoVecinal.timv_FechaCrea);

                    // resultado del procedimiento almacenado
                    foreach (UDP_Plani_tbTechoImpuestoVecinal_Insert_Result Resultado in listTechoImpuestoVecinal)
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
            tbTechoImpuestoVecinal tbTechoImpuestoVecinalJSON = db.tbTechoImpuestoVecinal.Find(id);

            // retornar objeto
            return Json(tbTechoImpuestoVecinalJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: Edit
        [HttpPost]
        public ActionResult Edit([Bind(Include = "mun_Codigo,tde_IdTipoDedu,timv_RangoInicial,timv_RangoFin,timv_Impuesto,timv_UsuarioCrea,timv_FechaCrea")] tbTechoImpuestoVecinal tbTechoImpuestoVecinal)
        {
            // variables de auditoria
            tbTechoImpuestoVecinal.timv_UsuarioModifica = 1;
            tbTechoImpuestoVecinal.timv_FechaModifica = DateTime.Now;

            // variables de resultado del proceso
            IEnumerable<object> listTechoImpuestoVecinal = null;
            string MensajeError = "";
            string response = String.Empty;

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listTechoImpuestoVecinal = db.UDP_Plani_tbTechoImpuestoVecinal_Update(tbTechoImpuestoVecinal.timv_IdTechoImpuestoVecinal,
                                                                                          tbTechoImpuestoVecinal.mun_Codigo,
                                                                                          tbTechoImpuestoVecinal.tde_IdTipoDedu,
                                                                                          tbTechoImpuestoVecinal.timv_RangoInicio,
                                                                                          tbTechoImpuestoVecinal.timv_RangoFin,
                                                                                          tbTechoImpuestoVecinal.timv_Rango,
                                                                                          tbTechoImpuestoVecinal.timv_Impuesto,
                                                                                          1,
                                                                                          DateTime.Now);

                    // obtener resultado del procedimiento almacenado
                    foreach (UDP_Plani_tbTechoImpuestoVecinal_Update_Result Resultado in listTechoImpuestoVecinal.ToList())
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

        #region EditGetDDLTipoDedu
        public JsonResult EditGetDDLTipoDedu()
        {
            // obtener data
            var DDL =
            from TipoDeduc in db.tbTipoDeduccion
            join TechoImpuestoVecinal in db.tbTechoImpuestoVecinal on TipoDeduc.tde_IdTipoDedu equals TechoImpuestoVecinal.tde_IdTipoDedu into prodGroup
            select new { Id = TipoDeduc.tde_IdTipoDedu, Descripcion = TipoDeduc.tde_Descripcion };

            // retornar data
            return Json(DDL, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region EditGetDDLMuni
        public JsonResult EditGetDDLMuni()
        {
            // obtener data
            var DDL =
            from Muni in db.tbMunicipio
            join TechoImpuestoVecinal in db.tbTechoImpuestoVecinal on Muni.mun_Codigo equals TechoImpuestoVecinal.mun_Codigo into prodGroup
            select new { Id = Muni.mun_Codigo, Descripcion = Muni.mun_Nombre };

            // retornar data
            return Json(DDL, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Inactivar 
        public ActionResult Inactivar(int id)
        {
            // variables de resultado
            IEnumerable<object> listTechoImpuestoVecinal = null;
            string MensajeError = "";
            string response = String.Empty;

            // validar si el model es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listTechoImpuestoVecinal = db.UDP_Plani_tbTechoImpuestoVecinal_Inactivar(id,
                                                                                             1,
                                                                                             DateTime.Now);

                    // obtener resultado del PA
                    foreach (UDP_Plani_tbTechoImpuestoVecinal_Inactivar_Result Resultado in listTechoImpuestoVecinal)
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
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Activar
        public ActionResult Activar(int id)
        {
            // variables de resultado
            IEnumerable<object> listTechoImpuestoVecinal = null;
            string MensajeError = "";
            string response = String.Empty;

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listTechoImpuestoVecinal = db.UDP_Plani_tbTechoImpuestoVecinal_Activar(id,
                                                                                           1,
                                                                                           DateTime.Now);

                    // obtener resultado del PA
                    foreach (UDP_Plani_tbTechoImpuestoVecinal_Activar_Result Resultado in listTechoImpuestoVecinal)
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

