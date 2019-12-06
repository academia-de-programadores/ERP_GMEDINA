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
    public class TipoDeduccionesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        #region GET: INDEX  
            // GET: TipoDeducciones
            public ActionResult Index()
            {
                var tbTipoDeduccion = db.tbTipoDeduccion.Where(x => x.tde_Activo == true).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).OrderByDescending(c => c.tde_FechaCrea);
                return View(tbTipoDeduccion.ToList());
            }
        #endregion

        #region GET: DATA
            public ActionResult GetData()
            {
                db.Configuration.ProxyCreationEnabled = false;
                //var tbTipoDeducciones = db.tbTipoDeduccion.ToList().Where(p => p.tde_Activo == true);

                //var tbPidoDedu = from d in db.tbTipoDeduccion
                //                 where

                var tbTipoDeducciones = db.tbTipoDeduccion
                    .Select(c => new {
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
                    .Where(x => x.tde_Activo == true)
                    .OrderByDescending(c => c.tde_FechaCrea).ToList();

            return new JsonResult { Data = tbTipoDeducciones, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        #endregion

        #region POST: CREATE

        // POST: TipoDeduccion/Create REALIZAR LA INSERCIÓN
        // GET: TipoDeducciones/Create
        [HttpPost]
            public ActionResult Create([Bind(Include = "tde_Descripcion, tde_UsuarioCrea, tde_FechaCrea")] tbTipoDeduccion tbTipoDeduccion)
            {
                //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
                tbTipoDeduccion.tde_UsuarioCrea = 1;
                tbTipoDeduccion.tde_FechaCrea = DateTime.Now;
                //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
                string response = String.Empty;
                IEnumerable<object> listTipoDeduccion = null;
                String MessageError = "";
                //VALIDAR SI EL MODELO ES VÁLIDO
                if (ModelState.IsValid)
                   {
                    try
                    {
                        //EJECUTAR PROCEDIMIENTO ALMACENADO
                        listTipoDeduccion = db.UDP_Plani_tbTipoDeduccion_Insert(tbTipoDeduccion.tde_Descripcion,
                                                                                tbTipoDeduccion.tde_UsuarioCrea,
                                                                                tbTipoDeduccion.tde_FechaCrea);
                        //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP                                                  
                        foreach (UDP_Plani_tbTipoDeduccion_Insert_Result resultado in listTipoDeduccion)
                            MessageError = Convert.ToString(resultado);

                        if(MessageError.StartsWith("-1"))
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
                    //SI LA EJECUCIÓN LLEGA A ESTE PUNTO SIGNIFICA QUE NO OCURRIÓ NINGÚN ERROR Y EL PROCESO FUE EXITOSO
                    //IGUALAMOS LA VARIABLE "RESPONSE" A "BIEN" PARA VALIDARLO EN EL CLIENTE
                    response = "bien";
                   }
                else
                {
                    //SI EL MODELO NO ES VÁLIDO, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    response = "error";
                }
                //RETORNAMOS LA VARIABLE RESPONSE AL CLIENTE PARA EVALUARLA
                ViewBag.tde_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoDeduccion.tde_UsuarioCrea);
                ViewBag.tde_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoDeduccion.tde_UsuarioModifica);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        #endregion

        #region GET: Editar 
        // GET: TipoDeducciones/Edit/5
        public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var tbTipoDeduccion = db.tbTipoDeduccion.Where(d=> d.tde_IdTipoDedu == id)
                            .Select(c => new { tde_Descripcion = c.tde_Descripcion, tde_IdTipoDedu = c.tde_IdTipoDedu, tde_UsuarioCrea = c.tde_UsuarioCrea, tde_FechaCrea = c.tde_FechaCrea, tde_UsuarioModifica = c.tde_UsuarioModifica, tde_FechaModifica = c.tde_FechaModifica, tde_Activo = c.tde_Activo })
                            .ToList();

                //db.Configuration.ProxyCreationEnabled = false;
                //tbTipoDeduccion tbTipoDeduccion = db.tbTipoDeduccion.Find(id);
                //RETORNAR JSON AL LADO DEL CLIENTE              
                if (tbTipoDeduccion == null)
                {
                    return HttpNotFound();
                }
                //ViewBag.tde_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoDeduccion.tde_UsuarioCrea);
                //ViewBag.tde_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoDeduccion.tde_UsuarioModifica);
                return new JsonResult { Data = tbTipoDeduccion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        #endregion

        #region GET: Details
            //public ActionResult Details(int? id)
            //{
            //    if (id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            //    tbTipoDeduccion tbTipoDeduccion = db.tbTipoDeduccion.Find(id);
            //    if (tbTipoDeduccion == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    return View(tbTipoDeduccion);
            //}
        #endregion

        #region POST: Editar
        // POST: TipoDeducciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tde_IdTipoDedu,tde_Descripcion,tde_UsuarioCrea,tde_FechaCrea,tde_UsuarioModifica,tde_FechaModifica,tde_Activo")] tbTipoDeduccion tbTipoDeduccion)
        {
            //DATA DE AUDIOTIRIA DE CREACIÓN, PUESTA UNICAMENTE PARA QUE NO CAIGA EN EL CATCH
            //EN EL PROCEDIMIENTO ALMACENADO, ESTOS DOS CAMPOS NO SE DEBEN MODIFICAR
            tbTipoDeduccion.tde_UsuarioCrea = 1;
            tbTipoDeduccion.tde_FechaCrea = DateTime.Now;

            //LLENAR DATA DE AUDITORIA
            tbTipoDeduccion.tde_UsuarioCrea = 1;
            tbTipoDeduccion.tde_FechaCrea = DateTime.Now;

            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            IEnumerable<object> listTipoDeduccion = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                 try {
                    
                    listTipoDeduccion = db.UDP_Plani_tbTipoDeduccion_Update(tbTipoDeduccion.tde_IdTipoDedu,
                                                                            tbTipoDeduccion.tde_Descripcion, 
                                                                            1,
                                                                            DateTime.Now);

                 foreach (UDP_Plani_tbTipoDeduccion_Update_Result resultado in listTipoDeduccion)
                 MensajeError = resultado.MensajeError;

              if(MensajeError.StartsWith("-1"))
                {
                        ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador");
                        response = "error";
                }
            }
              catch(Exception Ex)
            {
                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                    response = "error";
                }
                //RETORNAR MENSAJE DE CONFIRMACIÓN EN CASO QUE NO HAYA CAIDO EN EL CATCH
                response = "bien";
          }
            else
            {
                // SI EL MODELO NO ES CORRECTO, RETORNAR ERROR
                ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador");
                response = "error";
            }
            ViewBag.tde_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoDeduccion.tde_UsuarioCrea);
            ViewBag.tde_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTipoDeduccion.tde_UsuarioModifica);
            // RETORNAR MENSAJE AL LADO DEL CLIENTE
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Inactivar
        public JsonResult Inactivar(int? ID)
            {
                string response = String.Empty;
                IEnumerable<object> listTipoDeduccion = null;
                string MensajeError = "";
                try
                {
                    listTipoDeduccion = db.UDP_Plani_tbTipoDeduccion_Inactivar(ID, 1, DateTime.Now);

                    foreach (UDP_Plani_tbTipoDeduccion_Inactivar_Result resultado in listTipoDeduccion.ToList())
                        MensajeError = resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                    response = "error";
                }
                db.Configuration.ProxyCreationEnabled = false;
                tbTipoDeduccion tbTipoDeduccionesJSON = db.tbTipoDeduccion.Find(ID);
                return Json(tbTipoDeduccionesJSON, JsonRequestBehavior.AllowGet);
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
