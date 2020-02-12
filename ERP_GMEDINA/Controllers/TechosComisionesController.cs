using ERP_GMEDINA.Attribute;
using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace ERP_GMEDINA.Controllers
{
    public class TechosComisionesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        private ERP_GMEDINA.Models.Helpers Function = new Models.Helpers();

        #region Index Techos Comisiones
        // GET: TechosComisiones
        [SessionManager("TechosComisiones/Index")]
        public ActionResult Index()
        {
            try
            {
                var TechosComisiones = db.tbTechosComisiones.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCatalogoDeIngresos);
                return View(TechosComisiones.ToList());
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return View(db.tbTechosComisiones.ToList());
            }
        }

        public ActionResult GetData()
        {
            //SI SE LLEGA A DAR PROBLEMAS DE "REFERENCIAS CIRCULARES", OBTENER LA DATA DE ESTA FORMA
            //SELECCIONANDO UNO POR UNO LOS CAMPOS QUE NECESITAREMOS
            //DE LO CONTRARIO, HACERLO DE LA FORMA CONVENCIONAL (EJEMPLO: db.tbCatalogoDeDeducciones.ToList(); )
            var tbTechosComisiones = db.tbTechosComisiones
                        .Select(c => new
                        {
                            tc_Id = c.tc_Id,
                            cin_IdIngreso = c.cin_IdIngreso,
                            cin_DescripcionIngreso = c.tbCatalogoDeIngresos.cin_DescripcionIngreso,
                            tc_RangoInicio = c.tc_RangoInicio,
                            tc_RangoFin = c.tc_RangoFin,
                            tc_PorcentajeComision = c.tc_PorcentajeComision,
                            tc_Estado = c.tc_Estado,
                            tc_UsuarioCrea = c.tbUsuario.usu_NombreUsuario,
                            tc_FechaCrea = c.tc_FechaCrea,
                            tc_UsuarioModifica = c.tbUsuario1.usu_NombreUsuario,
                            tc_FechaModifica = c.tc_FechaModifica
                        })
                        .ToList();

            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = tbTechosComisiones, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region Dropdownlist Catalogo De Ingresos
        public JsonResult EditGetDDLIngreso()
        {
            //OBTENER LA DATA QUE NECESITAMOS, HACIENDOLO DE ESTA FORMA SE EVITA LA EXCEPCION POR "REFERENCIAS CIRCULARES"
            var DDL =
            from CatIngreso in db.tbCatalogoDeIngresos.Where(x => x.cin_Activo == true)
                //join EmpBonos in db.tbEmpleadoBonos on CatIngreso.cin_IdIngreso equals EmpBonos.cin_IdIngreso
            select new
            {
                Id = CatIngreso.cin_IdIngreso,
                Descripcion = CatIngreso.cin_DescripcionIngreso
            };
            //RETORNAR LA DATA EN FORMATO JSON AL CLIENTE
            return Json(DDL, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create Techos Comisiones
        //FUNCION: CREAR UN NUEVO REGISTRO
        [HttpPost]
        [SessionManager("TechosComisiones/Create")]
        public ActionResult Create([Bind(Include = "cin_IdIngreso, tc_RangoInicio, tc_RangoFin, tc_PorcentajeComision, tc_UsuarioCrea, tc_FechaCrea")] tbTechosComisiones tbTechosComisiones)
        {
            //Para llenar los campos de auditoría
            //tbTechosComisiones.tc_UsuarioCrea = 1;
            //tbTechosComisiones.tc_FechaCrea = DateTime.Now;

            //Variable para enviarla al lado del Cliente
            string Response = String.Empty;
            IEnumerable<object> listTechosComisiones = null;
            string MensajeError = "";

            if (ModelState.IsValid)
            {
                try
                {
                    //Ejecutar Procedimiento Almacenado
                    listTechosComisiones = db.UDP_Plani_tbTechosComisiones_Insert(tbTechosComisiones.cin_IdIngreso,
                                                                                  tbTechosComisiones.tc_RangoInicio,
                                                                                  tbTechosComisiones.tc_RangoFin,
                                                                                  tbTechosComisiones.tc_PorcentajeComision,
                                                                                  Function.GetUser(),
                                                                                  Function.DatetimeNow());

                    //El tipo complejo del Procedimiento Almacenado
                    foreach (UDP_Plani_tbTechosComisiones_Insert_Result Resultado in listTechosComisiones)
                    {
                        MensajeError = Resultado.MensajeError;
                    }

                    if (MensajeError.StartsWith("-1"))
                    {
                        //En caso de un error igualamos la variable Response a "Error" para validar en el lado del Cliente
                        ModelState.AddModelError("", "No se pudo Registrar. Contacte al Administrador!");
                        Response = "Error";
                    }
                }
                catch (Exception Ex)
                {
                    Response = Ex.Message.ToString();
                }
                //Si llega aqui significa que todo salio correctamente. Solo igualamos Response a "Exito" para validar en el lado del Cliente
                Response = "Exito";
                return RedirectToAction("Index");

            }
            else
            {
                //Si el modelo no es valido. Igualamos Response a "Error" para validar en el lado del Cliente
                Response = "Error";
            }
            return Json(Response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Edit Techos Comisiones
        //EDITAR
        //OBTENER REGISTRO PARA EDITAR
        [SessionManager("TechosComisiones/Edit")]
        public ActionResult Edit(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbTechosComisiones tbTechosComisionesJSON = db.tbTechosComisiones.Find(id);
            return Json(tbTechosComisionesJSON, JsonRequestBehavior.AllowGet);
        }

        //FUNCION: EDITAR UN REGISTRO
        [HttpPost]
        [SessionManager("TechosComisiones/Edit")]
        public ActionResult Edit([Bind(Include = "tc_Id, cin_IdIngreso, tc_RangoInicio, tc_RangoFin, tc_PorcentajeComision, tc_UsuarioModifica, tc_FechaModifica")] tbTechosComisiones tbTechosComisiones)
        {
            //tbTechosComisiones.tc_UsuarioModifica = 1;
            //tbTechosComisiones.tc_FechaModifica = DateTime.Now;

            string response = "bien";
            IEnumerable<object> listTechosComisiones = null;
            string MensajeError = "";


            if (ModelState.IsValid)
            {
                try
                {

                    listTechosComisiones = db.UDP_Plani_tbTechosComisiones_Update(tbTechosComisiones.tc_Id,
                                                                                  tbTechosComisiones.cin_IdIngreso,
                                                                                  tbTechosComisiones.tc_RangoInicio,
                                                                                  tbTechosComisiones.tc_RangoFin,
                                                                                  tbTechosComisiones.tc_PorcentajeComision,
                                                                                  Function.GetUser(),
                                                                                  Function.DatetimeNow());

                    foreach (UDP_Plani_tbTechosComisiones_Update_Result resultado in listTechosComisiones)
                        MensajeError = resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    //MENSAJE DE ERROR
                    Ex.Message.ToString();
                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                    response = "error";
                }
            }

            else
            {
                // SI EL MODELO NO ES CORRECTO, RETORNAR ERROR
                ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador");
                response = "error";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Details Techos Comisiones
        //DETALLES
        [SessionManager("TechosComisiones/Details")]
        public JsonResult Details(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            V_tbTechosComisiones V_tbTechosComisionesJSON = db.V_tbTechosComisiones.SingleOrDefault(m => m.tc_Id == id);
            return Json(V_tbTechosComisionesJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Inactivar Techos Comisiones
        //INACTIVAR
        [HttpPost]
        [SessionManager("TechosComisiones/Inactivar")]
        public ActionResult Inactivar(int? Id)
        {
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = "bien";
            IEnumerable<object> listTechosComisiones = null;
            string MensajeError = "";
            //Validar que el Id no sea null
            if (Id == null)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            //LLENAR DATA DE AUDITORIA
            tbTechosComisiones tbTechosComisiones = new tbTechosComisiones();
            tbTechosComisiones.tc_Id = (int)Id;
            //tbTechosComisiones.tc_UsuarioModifica = 1;
            //tbTechosComisiones.tc_FechaModifica = DateTime.Now;
            try
            {
                //EJECUTAR PROCEDIMIENTO ALMACENADO
                listTechosComisiones = db.UDP_Plani_tbTechosComisiones_Inactivar(tbTechosComisiones.tc_Id,
                                                                                 Function.GetUser(),
                                                                                 Function.DatetimeNow());

                //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                foreach (UDP_Plani_tbTechosComisiones_Inactivar_Result Resultado in listTechosComisiones)
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
            //RETORNAR MENSAJE AL LADO DEL CLIENTE
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Activar Techos Comisiones
        //ACTIVAR
        [HttpPost]
        [SessionManager("TechosComisiones/Activar")]
        public ActionResult Activar(int? Id)
        {
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = "bien";
            IEnumerable<object> listTechosComisiones = null;
            string MensajeError = "";
            //Validar que el Id no sea null
            if (Id == null)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            //LLENAR DATA DE AUDITORIA
            tbTechosComisiones tbTechosComisiones = new tbTechosComisiones();
            tbTechosComisiones.tc_Id = (int)Id;
            //tbTechosComisiones.tc_UsuarioModifica = 1;
            //tbTechosComisiones.tc_FechaModifica = DateTime.Now;
            try
            {
                //EJECUTAR PROCEDIMIENTO ALMACENADO
                listTechosComisiones = db.UDP_Plani_tbTechosComisiones_Activar(tbTechosComisiones.tc_Id,
                                                                               Function.GetUser(),
                                                                               Function.DatetimeNow());

                //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                foreach (UDP_Plani_tbTechosComisiones_Activar_Result Resultado in listTechosComisiones)
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
            //RETORNAR MENSAJE AL LADO DEL CLIENTE
            return Json(response, JsonRequestBehavior.AllowGet);
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
