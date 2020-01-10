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
    public class PeriodosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        #region INDEX
        // GET: Periodos
        public ActionResult Index()
        {
            var tbPeriodos = db.tbPeriodos.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbPeriodos.ToList());
        }
        #endregion

        #region DATA PARA RECARGAR EL DATATABLE
        public ActionResult GetData()
        {
            db.Configuration.ProxyCreationEnabled = false;


            var tbPeriodos = db.tbPeriodos
                .Select(c => new {
                    peri_IdPeriodo = c.peri_IdPeriodo,
                    peri_DescripPeriodo = c.peri_DescripPeriodo,
                    fpa_UsuarioCrea = c.peri_UsuarioCrea,
                    NombreUsuarioCrea = c.tbUsuario.usu_NombreUsuario,
                    peri_FechaCrea = c.peri_FechaCrea,
                    peri_UsuarioModifica = c.peri_UsuarioModifica,
                    NombreUsuarioModifica = c.tbUsuario1.usu_NombreUsuario,
                    peri_FechaModifica = c.peri_FechaModifica,
                    peri_Activo = c.peri_Activo
                }).OrderByDescending(x => x.peri_FechaCrea).ToList();

            return new JsonResult { Data = tbPeriodos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region POST: CREATE
        // GET: Periodos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "peri_IdPeriodo,peri_DescripPeriodo,peri_UsuarioCrea,peri_FechaCrea,peri_Activo")] tbPeriodos tbPeriodos)
        {
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbPeriodos.peri_UsuarioCrea = 1;
            tbPeriodos.peri_FechaCrea = DateTime.Now;
            tbPeriodos.peri_Activo = true;
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
                    listPeriodo = db.UDP_Plani_tbPeriodos_Insert(tbPeriodos.peri_DescripPeriodo,
                                                                            tbPeriodos.peri_UsuarioCrea,
                                                                            tbPeriodos.peri_FechaCrea);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO                                                
                    foreach (UDP_Plani_tbPeriodos_Insert_Result resultado in listPeriodo)
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
            var tbPeriodo = db.tbPeriodos.Where(d => d.peri_IdPeriodo == id)
                        .Select(c => new { peri_DescripPeriodo = c.peri_DescripPeriodo , peri_IdPeriodo = c.peri_IdPeriodo, peri_UsuarioCrea = c.peri_UsuarioCrea, peri_FechaCrea = c.peri_FechaCrea, peri_UsuarioModifica = c.peri_UsuarioModifica, peri_FechaModifica = c.peri_FechaModifica, peri_Activo = c.peri_Activo })
                        .ToList();
            
            //RETORNAR JSON AL LADO DEL CLIENTE              
            if (tbPeriodo == null)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
            return Json(tbPeriodo, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: EDITAR
        // POST: Periodos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Editar([Bind(Include = "peri_IdPeriodo,peri_DescripPeriodo")] tbPeriodos tbPeriodos)
        {
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbPeriodos.peri_UsuarioModifica = 1;
            tbPeriodos.peri_FechaModifica = DateTime.Now;
            tbPeriodos.peri_Activo = true;
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
                    listPeriodo = db.UDP_Plani_tbPeriodos_Update(tbPeriodos.peri_IdPeriodo,
                                                                    tbPeriodos.peri_DescripPeriodo,
                                                                    tbPeriodos.peri_UsuarioModifica,
                                                                    tbPeriodos.peri_FechaModifica);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO                                               
                    foreach (UDP_Plani_tbPeriodos_Update_Result resultado in listPeriodo)
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

        #region GET: DETAILS
        public JsonResult Details(int? id)
        {
            var tbCatalogoPeriodoJSON = from tbPeriod in db.tbPeriodos
                                                  //join tbUsuCrea in db.tbUsuario on tbCatIngreso.cin_UsuarioCrea equals tbUsuCrea.usu_Id
                                                  //join tbUsuModi in db.tbUsuario on tbCatIngreso.cin_UsuarioModifica equals tbUsuModi.usu_Id
                                              where tbPeriod.peri_Activo == true && tbPeriod.peri_IdPeriodo == id
                                              select new
                                              {
                                                  tbPeriod.peri_IdPeriodo,
                                                  tbPeriod.peri_DescripPeriodo,
                                                  tbPeriod.peri_Activo,
                                                  tbPeriod.peri_UsuarioCrea,
                                                  UsuCrea = tbPeriod.tbUsuario.usu_NombreUsuario,
                                                  tbPeriod.peri_FechaCrea,
                                                  tbPeriod.peri_UsuarioModifica,
                                                  UsuModifica = tbPeriod.tbUsuario1.usu_NombreUsuario,
                                                  tbPeriod.peri_FechaModifica
                                              };



            db.Configuration.ProxyCreationEnabled = false;
            //tbCatalogoDeIngresos tbCatalogoDeIngresosJSON = db.tbCatalogoDeIngresos.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Find(ID);
            return Json(tbCatalogoPeriodoJSON, JsonRequestBehavior.AllowGet);

            //if (id == null)
            //{
            //    return Json("Error", JsonRequestBehavior.AllowGet);
            //}
            //var tbPeriodo = db.tbPeriodos.Where(d => d.peri_IdPeriodo == id)
            //            .Select(c => new { peri_DescripPeriodo = c.peri_DescripPeriodo, peri_IdPeriodo = c.peri_IdPeriodo, peri_UsuarioCrea = c.peri_UsuarioCrea, peri_FechaCrea = c.peri_FechaCrea, peri_UsuarioModifica = c.peri_UsuarioModifica, peri_FechaModifica = c.peri_FechaModifica, peri_Activo = c.peri_Activo })
            //            .ToList();

            ////RETORNAR JSON AL LADO DEL CLIENTE              
            //if (tbPeriodo == null)
            //{
            //    return Json("Error", JsonRequestBehavior.AllowGet);
            //}
            //return Json(tbPeriodo, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: INACTIVAR
        // POST: Periodos/Delete/5
        [HttpPost]
        public ActionResult Inactivar(int? id)
        {
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = "bien";
            IEnumerable<object> listPeriodo = null;
            string MensajeError = "";
            //VALIDAR QUE EL ID NO LLEGUE NULL
            if (id == null)
            {
                response = "error";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            //INSTANCIA DEL MODELO
            tbPeriodos tbPeriodos = new tbPeriodos();
            //LLENAR DATA DE AUDITORIA
            tbPeriodos.peri_IdPeriodo = (int)id;
            tbPeriodos.peri_UsuarioModifica = 1;
            tbPeriodos.peri_FechaModifica = DateTime.Now;
            //VALIDAR SI EL ID ES VÁLIDO
            if (tbPeriodos.peri_IdPeriodo > 0)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listPeriodo = db.UDP_Plani_tbPeriodos_Inactivar(tbPeriodos.peri_IdPeriodo,
                                                                               tbPeriodos.peri_UsuarioModifica,
                                                                               tbPeriodos.peri_FechaModifica);

                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO
                    foreach (UDP_Plani_tbPeriodos_Inactivar_Result Resultado in listPeriodo)
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
