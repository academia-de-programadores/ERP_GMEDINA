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
    public class AFPController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        #region Index AFP
        // GET: AFP
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
        // GET: AFP/Create
        public ActionResult Create()
        {
            /*
            ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion");
            */
            return View();
        }

        // POST: AFP/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "afp_Descripcion,afp_AporteMinimoLps,afp_InteresAporte,afp_InteresAnual,tde_IdTipoDedu,afp_UsuarioCrea,afp_FechaCrea")] tbAFP tbAFP)
        {
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbAFP.afp_UsuarioCrea = 1;
            tbAFP.afp_FechaCrea = DateTime.Now;
            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
            string response = String.Empty;
            IEnumerable<object> listAFP = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listAFP = db.UDP_Plani_tbAFP_Insert(tbAFP.afp_Descripcion,
                                                        tbAFP.afp_AporteMinimoLps,
                                                        tbAFP.afp_InteresAporte,
                                                        tbAFP.afp_InteresAnual,
                                                        tbAFP.tde_IdTipoDedu,
                                                        tbAFP.afp_UsuarioCrea,
                                                        tbAFP.afp_FechaCrea);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbAFP_Insert_Result Resultado in listAFP)
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
                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    response = Ex.Message.ToString();
                }
                //SI LA EJECUCIÓN LLEGA A ESTE PUNTO SIGNIFICA QUE NO OCURRIÓ NINGÚN ERROR Y EL PROCESO FUE EXITOSO
                //IGUALAMOS LA VARIABLE "RESPONSE" A "BIEN" PARA VALIDARLO EN EL CLIENTE
                response = "bien";
            }
            else
            {
                //SI EL MODELO NO ES VÁLIDO, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                response = "error";
            }

            ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion", tbAFP.tde_IdTipoDedu);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Dropdownlist
        //Función: Para llenar los Dropdownlist para Agregar y Editar
        public JsonResult EditGetTipoDeduccionDDL()
        {
            var DDL =
                from TipoDedu in db.tbTipoDeduccion
                where TipoDedu.tde_Activo == true
                select new { Id = TipoDedu.tde_IdTipoDedu, Descripcion = TipoDedu.tde_Descripcion };

            return Json(DDL, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Editar AFP
        // GET: AFP/Edit/5
        public ActionResult Edit(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbAFP tbAFPJSON = db.tbAFP.Find(id);
            return Json(tbAFPJSON, JsonRequestBehavior.AllowGet);
        }

        // POST: AFP/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "afp_Id,afp_Descripcion,afp_AporteMinimoLps,afp_InteresAporte,afp_InteresAnual,tde_IdTipoDedu,afp_UsuarioCrea,afp_FechaCrea,afp_UsuarioModifica,afp_FechaModifica")] tbAFP tbAFP)
        {
            //DATA DE AUDIOTIRIA DE CREACIÓN, PUESTA UNICAMENTE PARA QUE NO CAIGA EN EL CATCH
            //EN EL PROCEDIMIENTO ALMACENADO, ESTOS DOS CAMPOS NO SE DEBEN MODIFICAR
            /*
            tbDeduccionAFP.cde_UsuarioCrea = 1;
            tbDeduccionAFP.cde_FechaCrea = DateTime.Now;
            */
            //LLENAR DATA DE AUDITORIA
            tbAFP.afp_UsuarioModifica = 1;
            tbAFP.afp_FechaModifica = DateTime.Now;
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            IEnumerable<object> listAFP = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listAFP = db.UDP_Plani_tbAFP_Update(tbAFP.afp_Id,
                                                        tbAFP.afp_Descripcion,
                                                        tbAFP.afp_AporteMinimoLps,
                                                        tbAFP.afp_InteresAporte,
                                                        tbAFP.afp_InteresAnual,
                                                        tbAFP.tde_IdTipoDedu,
                                                        tbAFP.afp_UsuarioModifica,
                                                        tbAFP.afp_FechaModifica);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbAFP_Update_Result Resultado in listAFP)
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
                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    response = Ex.Message.ToString();
                }
                //SI LA EJECUCIÓN LLEGA A ESTE PUNTO SIGNIFICA QUE NO OCURRIÓ NINGÚN ERROR Y EL PROCESO FUE EXITOSO
                //IGUALAMOS LA VARIABLE "RESPONSE" A "BIEN" PARA VALIDARLO EN EL CLIENTE
                response = "bien";
            }
            else
            {
                // SI EL MODELO NO ES CORRECTO, RETORNAR ERROR
                ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                response = "error";
            }

            //RETORNAR MENSAJE AL LADO DEL CLIENTE
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Detalles AFP
        // GET: AFP/Details/5
        public JsonResult Details(int? ID)
        {
            var tbAFPJSON = from tbAFP in db.tbAFP
                            where tbAFP.afp_Activo == true && tbAFP.afp_Id == ID
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
        public ActionResult Inactivar(int afp_Id)
        {
            //DATA DE AUDIOTIRIA DE CREACIÓN, PUESTA UNICAMENTE PARA QUE NO CAIGA EN EL CATCH
            //EN EL PROCEDIMIENTO ALMACENADO, ESTOS DOS CAMPOS NO SE DEBEN MODIFICAR
            //tbCatalogoDeDeducciones.cde_UsuarioCrea = 1;
            //tbCatalogoDeDeducciones.cde_FechaCrea = DateTime.Now;


            //LLENAR DATA DE AUDITORIA
            int afp_UsuarioModifica = 1;
            DateTime afp_FechaModifica = DateTime.Now;
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            IEnumerable<object> listAFP = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listAFP = db.UDP_Plani_tbAFP_Inactivar(afp_Id,
                                                           afp_UsuarioModifica,
                                                           afp_FechaModifica);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbAFP_Inactivar_Result Resultado in listAFP)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador");
                        response = "error";
                    }

                }
                catch (Exception Ex)
                {
                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    response = Ex.Message.ToString();
                }
                //SI LA EJECUCIÓN LLEGA A ESTE PUNTO SIGNIFICA QUE NO OCURRIÓ NINGÚN ERROR Y EL PROCESO FUE EXITOSO
                //IGUALAMOS LA VARIABLE "RESPONSE" A "BIEN" PARA VALIDARLO EN EL CLIENTE
                response = "bien";
            }
            else
            {
                // SI EL MODELO NO ES CORRECTO, RETORNAR ERROR
                ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador.");
                response = "error";
            }

            //RETORNAR MENSAJE AL LADO DEL CLIENTE
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Activar AFP
        [HttpPost]
        public ActionResult Activar(int id)
        {
            //LLENAR DATA DE AUDITORIA
            int afp_UsuarioModifica = 1;
            DateTime afp_FechaModifica = DateTime.Now;
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            IEnumerable<object> listAFP = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listAFP = db.UDP_Plani_tbAFP_Activar(id,
                                                         afp_UsuarioModifica,
                                                         afp_FechaModifica);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbAFP_Activar_Result Resultado in listAFP)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador");
                        response = "error";
                    }

                }
                catch (Exception Ex)
                {
                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    response = Ex.Message.ToString();
                }
                //SI LA EJECUCIÓN LLEGA A ESTE PUNTO SIGNIFICA QUE NO OCURRIÓ NINGÚN ERROR Y EL PROCESO FUE EXITOSO
                //IGUALAMOS LA VARIABLE "RESPONSE" A "BIEN" PARA VALIDARLO EN EL CLIENTE
                response = "bien";
            }
            else
            {
                // SI EL MODELO NO ES CORRECTO, RETORNAR ERROR
                ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador.");
                response = "error";
            }

            //RETORNAR MENSAJE AL LADO DEL CLIENTE
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
