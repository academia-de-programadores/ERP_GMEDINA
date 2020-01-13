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
    public class PreavisoController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        #region INDEX
        // GET: Preaviso
        public ActionResult Index()
        {
            var tbPreaviso = db.tbPreaviso.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).OrderBy(x => x.prea_IdPreaviso);
            return View(tbPreaviso.ToList());
        }
        #endregion

        #region DATA PARA RECARGAR EL DATATABLE
        public ActionResult GetData()
        {
            db.Configuration.ProxyCreationEnabled = false;


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

            return new JsonResult { Data = tbPreaviso, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region GET: DETAILS
        // GET: Preaviso/Details/5
        public JsonResult Details(int? id)
        {
            var tbCatalogoPreavisoJSON = from tbPreavi in db.tbPreaviso
                                             //join tbUsuCrea in db.tbUsuario on tbCatIngreso.cin_UsuarioCrea equals tbUsuCrea.usu_Id
                                             //join tbUsuModi in db.tbUsuario on tbCatIngreso.cin_UsuarioModifica equals tbUsuModi.usu_Id
                                         where tbPreavi.prea_Activo == true && tbPreavi.prea_IdPreaviso == id
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



            db.Configuration.ProxyCreationEnabled = false;
            //tbCatalogoDeIngresos tbCatalogoDeIngresosJSON = db.tbCatalogoDeIngresos.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Find(ID);
            return Json(tbCatalogoPreavisoJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "prea_IdPreaviso,prea_RangoInicioMeses,prea_RangoFinMeses,prea_DiasPreaviso,prea_UsuarioCrea,prea_FechaCrea,prea_UsuarioModifica,prea_FechaModifica,prea_Activo")] tbPreaviso tbPreaviso)
        {
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbPreaviso.prea_UsuarioCrea = 1;
            tbPreaviso.prea_FechaCrea = DateTime.Now;
            tbPreaviso.prea_Activo = true;
            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
            string response = "bien";
            IEnumerable<object> listPreaviso = null;
            String MessageError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listPreaviso = db.UDP_Plani_tbPreaviso_Insert(tbPreaviso.prea_RangoInicioMeses,
                                                                            tbPreaviso.prea_RangoFinMeses,
                                                                            tbPreaviso.prea_DiasPreaviso,
                                                                            tbPreaviso.prea_UsuarioCrea,
                                                                            tbPreaviso.prea_FechaCrea);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO                                                
                    foreach (UDP_Plani_tbPreaviso_Insert_Result resultado in listPreaviso)
                        MessageError = Convert.ToString(resultado);

                    if (MessageError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                    response = "error" + Ex.Message.ToString();
                }
            }
            else
            {
                //SI EL MODELO NO ES VÁLIDO, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                response = "error";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GET: EDIT
        // GET: Periodos/Edit/5
        public JsonResult Edit(int? id)
        {
            if (id == null)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
            var tbPreaviso = db.tbPreaviso.Where(d => d.prea_IdPreaviso == id)
                        .Select(c => new { prea_IdPreaviso = c.prea_IdPreaviso, prea_RangoInicioMeses = c.prea_RangoInicioMeses, prea_RangoFinMeses = c.prea_RangoFinMeses, prea_DiasPreaviso = c.prea_DiasPreaviso, prea_UsuarioCrea = c.prea_UsuarioCrea, prea_FechaCrea = c.prea_FechaCrea, prea_UsuarioModifica = c.prea_UsuarioModifica, prea_FechaModifica = c.prea_FechaModifica, prea_Activo = c.prea_Activo })
                        .ToList();

            if (tbPreaviso == null)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
            return Json(tbPreaviso, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: EDITAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "prea_IdPreaviso,prea_RangoInicioMeses,prea_RangoFinMeses,prea_DiasPreaviso")] tbPreaviso tbPreaviso)
        {
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbPreaviso.prea_UsuarioModifica = 1;
            tbPreaviso.prea_FechaModifica = DateTime.Now;
            tbPreaviso.prea_Activo = true;
            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
            string response = "bien";
            IEnumerable<object> listPeriodo = null;
            String MessageError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listPeriodo = db.UDP_Plani_tbPreaviso_Update(tbPreaviso.prea_IdPreaviso,
                                                                    tbPreaviso.prea_RangoInicioMeses,
                                                                    tbPreaviso.prea_RangoFinMeses,
                                                                    tbPreaviso.prea_DiasPreaviso,
                                                                    tbPreaviso.prea_UsuarioModifica,
                                                                    tbPreaviso.prea_FechaModifica);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO                                               
                    foreach (UDP_Plani_tbPreaviso_Update_Result resultado in listPeriodo)
                        MessageError = Convert.ToString(resultado);

                    if (MessageError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                    response = "error" + Ex.Message.ToString();
                }
            }
            else
            {
                //SI EL MODELO NO ES VÁLIDO, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                response = "error";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: INACTIVAR
        // POST: Periodos/Delete/5
        [HttpPost]
        public ActionResult Inactivar(int? id)
        {
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = "bien";
            IEnumerable<object> listPreaviso = null;
            string MensajeError = "";
            //VALIDAR QUE EL ID NO LLEGUE NULL
            if (id == null)
            {
                response = "error";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            //INSTANCIA DEL MODELO
            tbPreaviso tbPeaviso = new tbPreaviso();
            //LLENAR DATA DE AUDITORIA
            tbPeaviso.prea_IdPreaviso = (int)id;
            tbPeaviso.prea_UsuarioModifica = 1;
            tbPeaviso.prea_FechaModifica = DateTime.Now;
            //VALIDAR SI EL ID ES VÁLIDO
            if (tbPeaviso.prea_IdPreaviso > 0)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listPreaviso = db.UDP_Plani_tbPreaviso_Inactivar(tbPeaviso.prea_IdPreaviso,
                                                                               tbPeaviso.prea_UsuarioModifica,
                                                                               tbPeaviso.prea_FechaModifica);

                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO
                    foreach (UDP_Plani_tbPreaviso_Inactivar_Result Resultado in listPreaviso)
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

        #region POST: ACTIVAR
        [HttpPost]
        public ActionResult Activar(int? Id)
        {
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = "bien";
            IEnumerable<object> listPreaviso = null;
            string MensajeError = "";
            //VALIDAR QUE EL ID NO LLEGUE NULL
            if (Id == null)
            {
                response = "error";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            //INSTANCIA DEL MODELO
            tbPreaviso tbPreaviso = new tbPreaviso();
            //LLENAR DATA DE AUDITORIA
            tbPreaviso.prea_IdPreaviso = (int)Id;
            tbPreaviso.prea_UsuarioModifica = 1;
            tbPreaviso.prea_FechaModifica = DateTime.Now;
            //VALIDAR SI EL ID ES VÁLIDO
            if (tbPreaviso.prea_IdPreaviso > 0)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listPreaviso = db.UDP_Plani_tbPreaviso_Activar(tbPreaviso.prea_IdPreaviso,
                                                                     tbPreaviso.prea_UsuarioModifica,
                                                                     tbPreaviso.prea_FechaModifica);

                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbPreaviso_Activar_Result Resultado in listPreaviso)
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
            }
            else
            {
                // SI EL MODELO NO ES CORRECTO, RETORNAR ERROR
                ModelState.AddModelError("", "No se pudo Activar el registro, contacte al administrador.");
                response = "error";
            }

            //RETORNAR MENSAJE AL LADO DEL CLIENTE
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