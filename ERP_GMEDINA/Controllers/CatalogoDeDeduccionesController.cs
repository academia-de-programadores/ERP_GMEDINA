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
    public class CatalogoDeDeduccionesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
		Models.Helpers Function = new Models.Helpers();

        #region Index Catalogo de Deducciones
        [SessionManager("CatalogoDeDeducciones/Index")]
        // GET: CatalogoDeDeducciones editado
        public ActionResult Index()
        {
            var tbCatalogoDeDeducciones = db.tbCatalogoDeDeducciones.Include(t => t.tbTipoDeduccion).Include( t => t.tbUsuario).OrderByDescending(x => x.cde_Activo);
            return View(tbCatalogoDeDeducciones.ToList());
        }

        // GET: OBTENER LA DATA Y ENVIARLA A LA VISTA EN FORMATO JSON 
        public ActionResult GetData()
        {
            //SI SE LLEGA A DAR PROBLEMAS DE "REFERENCIAS CIRCULARES", OBTENER LA DATA DE ESTA FORMA
            //SELECCIONANDO UNO POR UNO LOS CAMPOS QUE NECESITAREMOS
            //DE LO CONTRARIO, HACERLO DE LA FORMA CONVENCIONAL (EJEMPLO: db.tbCatalogoDeDeducciones.ToList(); )
            var tbCatalogoDeDeducciones1 = db.tbCatalogoDeDeducciones
                        .Select(c => new { tde_Descripcion = c.tbTipoDeduccion.tde_Descripcion, tde_IdTipoDedu = c.tbTipoDeduccion.tde_IdTipoDedu, cde_UsuarioModifica = c.cde_UsuarioModifica, cde_UsuarioCrea = c.cde_UsuarioCrea, cde_PorcentajeEmpresa = c.cde_PorcentajeEmpresa, cde_PorcentajeColaborador = c.cde_PorcentajeColaborador, cde_IdDeducciones = c.cde_IdDeducciones, cde_DescripcionDeduccion = c.cde_DescripcionDeduccion, cde_Activo = c.cde_Activo, cde_FechaCrea = c.cde_FechaCrea, cde_FechaModifica = c.cde_FechaModifica })
                        .OrderByDescending(x => x.cde_Activo)
                        .ToList();
            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = tbCatalogoDeDeducciones1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
#endregion

        #region Crear Catalogo de Deducciones
        // POST: CatalogoDeDeducciones/Create REALIZAR LA INSERCIÓN
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [SessionManager("CatalogoDeDeducciones/Create")]
        public ActionResult Create([Bind(Include = "cde_DescripcionDeduccion,tde_IdTipoDedu,cde_PorcentajeColaborador,cde_PorcentajeEmpresa,cde_UsuarioCrea,cde_FechaCrea")] tbCatalogoDeDeducciones tbCatalogoDeDeducciones)
        {
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbCatalogoDeDeducciones.cde_UsuarioCrea = Function.GetUser();
            tbCatalogoDeDeducciones.cde_FechaCrea = Function.DatetimeNow();
            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
            string response = String.Empty;
            IEnumerable<object> listCatalogoDeDeducciones = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listCatalogoDeDeducciones = db.UDP_Plani_tbCatalogoDeDeducciones_Insert(tbCatalogoDeDeducciones.cde_DescripcionDeduccion, 
                                                                                            tbCatalogoDeDeducciones.tde_IdTipoDedu,
                                                                                            tbCatalogoDeDeducciones.cde_PorcentajeColaborador,
                                                                                            tbCatalogoDeDeducciones.cde_PorcentajeEmpresa, 
                                                                                            tbCatalogoDeDeducciones.cde_UsuarioCrea,
                                                                                            tbCatalogoDeDeducciones.cde_FechaCrea);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbCatalogoDeDeducciones_Insert_Result Resultado in listCatalogoDeDeducciones)
                        MensajeError = Resultado.MensajeError;
                    
                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        response = "error";
                    }

                }catch (Exception Ex)
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
            //RETORNAMOS LA VARIABLE RESPONSE AL CLIENTE PARA EVALUARLA
            ViewBag.cde_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCatalogoDeDeducciones.cde_UsuarioCrea);
            ViewBag.cde_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbCatalogoDeDeducciones.cde_UsuarioModifica);
            ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion", tbCatalogoDeDeducciones.tde_IdTipoDedu);

            object json = new { response = response, data = GetData() };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Editar Catalogo de Deducciones
        // GET: CatalogoDeDeducciones/Edit/5
        [SessionManager("CatalogoDeDeducciones/Edit")]
        public JsonResult Edit(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbCatalogoDeDeducciones tbCatalogoDeDeduccionesJSON = db.tbCatalogoDeDeducciones.Find(ID);
            return Json(tbCatalogoDeDeduccionesJSON, JsonRequestBehavior.AllowGet);
        }

        // POST: CatalogoDeDeducciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [SessionManager("CatalogoDeDeducciones/Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cde_IdDeducciones,cde_DescripcionDeduccion,tde_IdTipoDedu,cde_PorcentajeColaborador,cde_PorcentajeEmpresa,cde_UsuarioCrea,cde_FechaCrea")] tbCatalogoDeDeducciones tbCatalogoDeDeducciones)
        {
            //DATA DE AUDIOTIRIA DE CREACIÓN, PUESTA UNICAMENTE PARA QUE NO CAIGA EN EL CATCH
            //EN EL PROCEDIMIENTO ALMACENADO, ESTOS DOS CAMPOS NO SE DEBEN MODIFICAR
            tbCatalogoDeDeducciones.cde_UsuarioCrea = Function.GetUser();
            tbCatalogoDeDeducciones.cde_FechaCrea = Function.DatetimeNow();


            //LLENAR DATA DE AUDITORIA
            tbCatalogoDeDeducciones.cde_UsuarioModifica = Function.GetUser();
            tbCatalogoDeDeducciones.cde_FechaModifica = Function.DatetimeNow();
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = "bien";
            IEnumerable<object> listCatalogoDeDeducciones = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listCatalogoDeDeducciones = db.UDP_Plani_tbCatalogoDeDeducciones_Update(tbCatalogoDeDeducciones.cde_IdDeducciones,
                                                                                            tbCatalogoDeDeducciones.cde_DescripcionDeduccion,
                                                                                            tbCatalogoDeDeducciones.tde_IdTipoDedu,
                                                                                            tbCatalogoDeDeducciones.cde_PorcentajeColaborador,
                                                                                            tbCatalogoDeDeducciones.cde_PorcentajeEmpresa,
                                                                                            tbCatalogoDeDeducciones.cde_UsuarioModifica,
                                                                                            tbCatalogoDeDeducciones.cde_FechaModifica);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbCatalogoDeDeducciones_Update_Result Resultado in listCatalogoDeDeducciones)
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

            ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion", tbCatalogoDeDeducciones.tde_IdTipoDedu);
            
            //RETORNAR MENSAJE AL LADO DEL CLIENTE
            return Json(response, JsonRequestBehavior.AllowGet);
        }
#endregion

        #region Dropdownlist Tipo Deduccion
        //FUNCIÓN: OBETENER LA DATA PARA LLENAR LOS DROPDOWNLIST DE EDICIÓN Y CREACIÓN
        public JsonResult EditGetDDL()
        {
            //OBTENER LA DATA QUE NECESITAMOS, HACIENDOLO DE ESTA FORMA SE EVITA LA EXCEPCION POR "REFERENCIAS CIRCULARES"
            var DDL =
                from TipoDedu in db.tbTipoDeduccion
                where TipoDedu.tde_Activo == true
                select new { Id = TipoDedu.tde_IdTipoDedu, Descripcion =  TipoDedu.tde_Descripcion };

            //RETORNAR LA DATA EN FORMATO JSON AL CLIENTE 
            return Json(DDL, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Detalles Catalogo de Deducciones
        [SessionManager("CatalogoDeDeducciones/Details")]
        public JsonResult Details(int? ID)
        {
            var tbCatalogoDeDeduccionesJSON = from tbCatDedu in db.tbCatalogoDeDeducciones
                                        where tbCatDedu.cde_IdDeducciones == ID
                                        select new
                                        {
                                            tbCatDedu.cde_IdDeducciones,
                                            tbCatDedu.cde_DescripcionDeduccion,
                                            tbCatDedu.tde_IdTipoDedu,
                                            tbCatDedu.cde_PorcentajeColaborador,
                                            tbCatDedu.cde_PorcentajeEmpresa,
                                            tbCatDedu.cde_Activo,
                                            tbCatDedu.cde_UsuarioCrea,
                                            UsuCrea = tbCatDedu.tbUsuario.usu_NombreUsuario,
                                            tbCatDedu.cde_FechaCrea,
                                            tbCatDedu.cde_UsuarioModifica,
                                            UsuModifica = tbCatDedu.tbUsuario1.usu_NombreUsuario,
                                            tbCatDedu.cde_FechaModifica
                                        };

            db.Configuration.ProxyCreationEnabled = false;
            return Json(tbCatalogoDeDeduccionesJSON, JsonRequestBehavior.AllowGet);
        }
#endregion

        #region Inactivar Catalogo de Deducciones
        [HttpPost]
        [SessionManager("CatalogoDeDeducciones/Inactivar")]
        [ValidateAntiForgeryToken]
        public ActionResult Inactivar(int id)
        {
            IEnumerable<object> listCatalogoDeIngresos = null;
            string MensajeError = "";
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    listCatalogoDeIngresos = db.UDP_Plani_tbCatalogoDeDeducciones_Inactivar(id,
                                                                                            Function.GetUser(),
                                                                                            Function.DatetimeNow());

                    foreach (UDP_Plani_tbCatalogoDeDeducciones_Inactivar_Result Resultado in listCatalogoDeIngresos)
                        MensajeError = Resultado.MensajeError;


                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo actualizar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception)
                {
                    response = "error";
                }
                //SI LA EJECUCIÓN LLEGA A ESTE PUNTO SIGNIFICA QUE NO OCURRIÓ NINGÚN ERROR Y EL PROCESO FUE EXITOSO
                //IGUALAMOS LA VARIABLE "RESPONSE" A "BIEN" PARA VALIDARLO EN EL CLIENTE
                response = "bien";
            }
            else
            {
                //Se devuelve un mensaje de error en caso de que el modelo no sea válido
                response = "error";
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
#endregion

        #region Activar Catalogo de Deducciones
        [HttpPost]
        [SessionManager("CatalogoDeDeducciones/Activar")]
        [ValidateAntiForgeryToken]
        public ActionResult Activar(int id)
        {
            IEnumerable<object> listCatalogoDeIngresos = null;
            string MensajeError = "";
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    listCatalogoDeIngresos = db.UDP_Plani_tbCatalogoDeDeducciones_Activar(id,
                                                                                          Function.GetUser(),
                                                                                          Function.DatetimeNow());
                    foreach (UDP_Plani_tbCatalogoDeDeducciones_Inactivar_Result Resultado in listCatalogoDeIngresos)
                        MensajeError = Resultado.MensajeError;


                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo actualizar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception)
                {
                    response = "error";
                }
                //SI LA EJECUCIÓN LLEGA A ESTE PUNTO SIGNIFICA QUE NO OCURRIÓ NINGÚN ERROR Y EL PROCESO FUE EXITOSO
                //IGUALAMOS LA VARIABLE "RESPONSE" A "BIEN" PARA VALIDARLO EN EL CLIENTE
                response = "bien";
            }
            else
            {
                //Se devuelve un mensaje de error en caso de que el modelo no sea válido
                response = "error";
            }
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
