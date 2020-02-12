using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Attribute;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Controllers
{
    public class AFPController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        private ERP_GMEDINA.Models.Helpers Function = new Models.Helpers();
        #region Index AFP
        // GET: AFP
        [SessionManager("AFP/Index")]
        public ActionResult Index()
        {
            var tbAFP = db.tbAFP.OrderBy(t => t.afp_FechaCrea).Include(a => a.tbTipoDeduccion);
            return View(tbAFP.ToList());
        }


        //GET: Obtener la Data y enviarla en formato Json al JS
        public ActionResult GetData()
        {
            var tbAFP1 = db.tbAFP
                        .Select(a => new
                        {
                            afp_Id = a.afp_Id,
                            afp_Descripcion = a.afp_Descripcion,
                            afp_AporteMinimoLps = a.afp_AporteMinimoLps,
                            afp_InteresAporte = a.afp_InteresAporte,
                            afp_InteresAnual = a.afp_InteresAnual,
                            tde_IdTipoDedu = a.tde_IdTipoDedu,
                            tde_Descripcion = a.tbTipoDeduccion.tde_Descripcion,
                            afp_UsuarioCrea = a.afp_UsuarioCrea,
                            afp_UsuarioModifica = a.afp_UsuarioModifica,
                            afp_FechaCrea = a.afp_FechaCrea,
                            afp_FechaModifica = a.afp_FechaModifica,
                            afp_Activo = a.afp_Activo
                        })
                        .OrderBy(a => a.afp_FechaCrea)
                        .ToList();
            return new JsonResult { Data = tbAFP1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



        #endregion

        #region Crear AFP
        [SessionManager("AFP/Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [SessionManager("AFP/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "afp_Descripcion,afp_AporteMinimoLps,afp_InteresAporte,afp_InteresAnual,tde_IdTipoDedu,afp_UsuarioCrea,afp_FechaCrea")] tbAFP tbAFP)
        {
            // data de auditoria
            //tbAFP.afp_UsuarioCrea = 1;
            //tbAFP.afp_FechaCrea = DateTime.Now;

            // variables para validar resultados
            string response = String.Empty;
            IEnumerable<object> listAFP = null;
            string MensajeError = "";

            // validar modelo
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar procedimientos almacenados
                    listAFP = db.UDP_Plani_tbAFP_Insert(tbAFP.afp_Descripcion,
                                                        tbAFP.afp_AporteMinimoLps,
                                                        tbAFP.afp_InteresAporte,
                                                        tbAFP.afp_InteresAnual,
                                                        tbAFP.tde_IdTipoDedu,
                                                        Function.GetUser(),
                                                        Function.DatetimeNow());

                    // verificar resultado del PA
                    foreach (UDP_Plani_tbAFP_Insert_Result Resultado in listAFP)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el procedimiento almacenado falló
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
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
            
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Dropdownlist
        public JsonResult EditGetTipoDeduccionDDL()
        {
            // obtener data para refrescar datatable
            var DDL =
                from TipoDedu in db.tbTipoDeduccion
                where TipoDedu.tde_Activo == true
                select new { Id = TipoDedu.tde_IdTipoDedu, Descripcion = TipoDedu.tde_Descripcion };

            // retornar data
            return Json(DDL, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Editar AFP
        // GET: AFP/Edit/5
        [SessionManager("AFP/Edit")]
        public ActionResult Edit(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbAFP tbAFPJSON = db.tbAFP.Find(id);
            return Json(tbAFPJSON, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SessionManager("AFP/Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "afp_Id,afp_Descripcion,afp_AporteMinimoLps,afp_InteresAporte,afp_InteresAnual,tde_IdTipoDedu,afp_UsuarioCrea,afp_FechaCrea,afp_UsuarioModifica,afp_FechaModifica")] tbAFP tbAFP)
        {
            //// data de auditoria
            //tbAFP.afp_UsuarioModifica = 1;
            //tbAFP.afp_FechaModifica = DateTime.Now;

            // variables de resultados
            string response = String.Empty;
            IEnumerable<object> listAFP = null;
            string MensajeError = "";

            // validar modelo
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar procedimiento almacenado
                    listAFP = db.UDP_Plani_tbAFP_Update(tbAFP.afp_Id,
                                                        tbAFP.afp_Descripcion,
                                                        tbAFP.afp_AporteMinimoLps,
                                                        tbAFP.afp_InteresAporte,
                                                        tbAFP.afp_InteresAnual,
                                                        tbAFP.tde_IdTipoDedu,
                                                        Function.GetUser(),
                                                        Function.DatetimeNow());

                    // verificar resultado del procedimiento almacenado
                    foreach (UDP_Plani_tbAFP_Update_Result Resultado in listAFP)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // falló el procedimiento almacenado
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        response = "error";
                    }

                }
                catch (Exception Ex)
                {
                    // se generó una excepción
                    response = "error";
                }

                // el resultado fue exitoso
                response = "bien";
            }
            else
            {
                // el modelo no es válido
                ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                response = "error";
            }

            // retornar resultado 
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Detalles AFP
        // GET: AFP/Details/5
        [SessionManager("AFP/Details")]
        public JsonResult Details(int? ID)
        {
            var tbAFPJSON = from tbAFP in db.tbAFP
                           where  tbAFP.afp_Id == ID
                            select new
                            {
                                tbAFP.afp_Id,
                                tbAFP.afp_Descripcion,
                                tbAFP.afp_AporteMinimoLps,
                                tbAFP.afp_InteresAporte,
                                tbAFP.afp_InteresAnual,
                                tbAFP.tde_IdTipoDedu,
                                tbAFP.afp_Activo,
                                tbAFP.afp_UsuarioCrea,
                                UsuCrea = tbAFP.tbUsuario.usu_NombreUsuario,
                                tbAFP.afp_FechaCrea,
                                tbAFP.afp_UsuarioModifica,
                                UsuModifica = tbAFP.tbUsuario1.usu_NombreUsuario,
                                tbAFP.afp_FechaModifica
                            };
            db.Configuration.ProxyCreationEnabled = false;
            return Json(tbAFPJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Inhabilitar AFP
        [HttpPost]
        [SessionManager("AFP/Inactivar")]
        public ActionResult Inactivar(int afp_Id)
        {
            // data de auditoria
            //int afp_UsuarioModifica = 1;
            //DateTime afp_FechaModifica = DateTime.Now;
            
            // variables de resultados
            string response = String.Empty;
            IEnumerable<object> listAFP = null;
            string MensajeError = "";

            // validar modelo
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar procedimiento almacenado
                    listAFP = db.UDP_Plani_tbAFP_Inactivar(afp_Id,
                                                           Function.GetUser(),
                                                           Function.DatetimeNow());

                    // verificar resultado del procedimiento almacenado
                    foreach (UDP_Plani_tbAFP_Inactivar_Result Resultado in listAFP)
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
                    response = response = "error";
                }

                // el proceso fue exitoso
                response = "bien";
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

        #region Activar AFP
        [HttpPost]
        [SessionManager("AFP/Activar")]
        public ActionResult Activar(int id)
        {
            // data de auditoria
            //int afp_UsuarioModifica = 1;
            //DateTime afp_FechaModifica = DateTime.Now;

            //variables de resultados
            string response = String.Empty;
            IEnumerable<object> listAFP = null;
            string MensajeError = "";

            // validar modelo
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar procedimiento almacenado
                    listAFP = db.UDP_Plani_tbAFP_Activar(id,
                                                         Function.GetUser(),
                                                         Function.DatetimeNow());

                    // validar resultado del procedimiento almacenado
                    foreach (UDP_Plani_tbAFP_Activar_Result Resultado in listAFP)
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
                    // se generó una excepció
                    response = "error";
                }

                // el proceso fue exitoso
                response = "bien";
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

        #region Ejecutable AFP
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
