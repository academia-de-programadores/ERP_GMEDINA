﻿using System;
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
    public class TechosDeduccionesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        //GET: TechosDeducciones
        public ActionResult Index()
        {
            var tbTechosDeducciones = db.tbTechosDeducciones.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCatalogoDeDeducciones).OrderBy(t => t.tddu_FechaCrea);
            return View(tbTechosDeducciones.ToList());
        }

        [HttpGet]
        // GET: OBTENER LA DATA Y ENVIARLA A LA VISTA EN FORMATO JSON
        public ActionResult GetData()
        {
            var otbTechosDeducciones = db.tbTechosDeducciones
                        .Select(c => new {
                            cde_DescripcionDeduccion = c.tbCatalogoDeDeducciones.cde_DescripcionDeduccion,
                            tddu_IdTechosDeducciones = c.tddu_IdTechosDeducciones,
                            tddu_PorcentajeEmpresa = c.tddu_PorcentajeEmpresa,
                            tddu_PorcentajeColaboradores = c.tddu_PorcentajeColaboradores,
                            tddu_Techo = c.tddu_Techo,
                            tddu_Activo = c.tddu_Activo,
                            tede_FechaCrea = c.tddu_FechaCrea
                        }).OrderByDescending(c => c.tede_FechaCrea)
                        .ToList();

            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = otbTechosDeducciones, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult Details(int? ID)
        {
            var tbTechosDeduccionesJSON = from tbTechosDeducciones in db.tbTechosDeducciones
                                          where tbTechosDeducciones.tddu_IdTechosDeducciones == ID
                                          select new
                                          {
                                              tbTechosDeducciones.tddu_IdTechosDeducciones,
                                              tbTechosDeducciones.tddu_PorcentajeColaboradores,
                                              tbTechosDeducciones.tddu_PorcentajeEmpresa,
                                              tbTechosDeducciones.tddu_Techo,
                                              tbTechosDeducciones.cde_IdDeducciones,

                                              tbTechosDeducciones.tddu_UsuarioCrea,
                                              UsuCrea = tbTechosDeducciones.tbUsuario.usu_NombreUsuario,
                                              tbTechosDeducciones.tddu_FechaCrea,

                                              tbTechosDeducciones.tddu_UsuarioModifica,
                                              UsuModifica = tbTechosDeducciones.tbUsuario1.usu_NombreUsuario,
                                              tbTechosDeducciones.tddu_FechaModifica
                                          };

            db.Configuration.ProxyCreationEnabled = false;

            return Json(tbTechosDeduccionesJSON, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(tbTechosDeducciones tbTechosDeducciones)
        {
            #region declaracion de variables 
            //Llenar los datos de auditoría, de no hacerlo el modelo será inválido y entrará directamente al Catch
            tbTechosDeducciones.tddu_UsuarioCrea = 1;
            tbTechosDeducciones.tddu_FechaCrea = DateTime.Now;
            //Variable para almacenar el resultado del proceso y enviarlo al lado del cliente
            string response = String.Empty;
            IEnumerable<object> listTechosDeducciones = null;
            string MensajeError = "";
            #endregion

            if (ModelState.IsValid)
            {
                try
                {
                    //Ejecutar el procedimiento almacenado
                    listTechosDeducciones = db.UDP_Plani_tbTechosDeducciones_Insert(tbTechosDeducciones.tddu_PorcentajeColaboradores,
                                                                                     tbTechosDeducciones.tddu_PorcentajeEmpresa,
                                                                                     tbTechosDeducciones.tddu_Techo,
                                                                                     tbTechosDeducciones.cde_IdDeducciones,
                                                                                     tbTechosDeducciones.tddu_UsuarioCrea,
                                                                                     tbTechosDeducciones.tddu_FechaCrea);

                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbTechosDeducciones_Insert_Result Resultado in listTechosDeducciones)
                        MensajeError = Resultado.MensajeError;

                    response = "bien";
                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo ingresar el registro. Contacte al administrador.");
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
                //SI EL MODELO NO ES VÁLIDO, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                response = "error";
            }
            //RETORNAMOS LA VARIABLE RESPONSE AL CLIENTE PARA EVALUARLA
            ViewBag.tede_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTechosDeducciones.tddu_UsuarioCrea);
            ViewBag.tede_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTechosDeducciones.tddu_UsuarioModifica);
            ViewBag.cde_IdDeducciones = new SelectList(db.tbCatalogoDeDeducciones, "cde_IdDeducciones", "cde_DescripcionDeduccion", tbTechosDeducciones.cde_IdDeducciones);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //GET: TechosDeducciones/Edit/5
        public JsonResult Edit(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbTechosDeducciones tbTechosDeduccionesJSON = db.tbTechosDeducciones.Find(id);
            return Json(tbTechosDeduccionesJSON, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbTechosDeducciones tbTechosDeducciones)
        {
            tbTechosDeducciones.tddu_UsuarioModifica = 1;
            tbTechosDeducciones.tddu_FechaModifica = DateTime.Now;
            IEnumerable<object> listTechosDeducciones = null;
            string MensajeError = "";
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //Ejecución del procedimiento almacenado
                    listTechosDeducciones = db.UDP_Plani_tbTechosDeducciones_Update(tbTechosDeducciones.tddu_IdTechosDeducciones,
                                                                                    tbTechosDeducciones.tddu_PorcentajeColaboradores,
                                                                                     tbTechosDeducciones.tddu_PorcentajeEmpresa,
                                                                                     tbTechosDeducciones.tddu_Techo,
                                                                                     tbTechosDeducciones.cde_IdDeducciones, //ID del porcentaje de deducción
                                                                                     1,
                                                                                     DateTime.Now);

                    foreach (UDP_Plani_tbTechosDeducciones_Update_Result Resultado in listTechosDeducciones.ToList())
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo actualizar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    response = Ex.Message.ToString();
                }
                response = "bien";
            }
            else
            {
                //Se devuelve un mensaje de error en caso de que el modelo no sea válido
                response = "error";
            }
            ViewBag.tede_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTechosDeducciones.tddu_UsuarioCrea);
            ViewBag.tede_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbTechosDeducciones.tddu_UsuarioModifica);
            ViewBag.cde_IdDeducciones = new SelectList(db.tbCatalogoDeDeducciones, "cde_IdDeducciones", "cde_DescripcionDeduccion", tbTechosDeducciones.cde_IdDeducciones);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditGetDDL()
        {
            //OBTENER LA DATA QUE NECESITAMOS, HACIENDOLO DE ESTA FORMA SE EVITA LA EXCEPCIÓN POR "REFERENCIAS CIRCULARES"
            var DDL =
            from CatDeduc in db.tbCatalogoDeDeducciones
            join TechDeduc in db.tbTechosDeducciones on CatDeduc.cde_IdDeducciones equals TechDeduc.cde_IdDeducciones into prodGroup
            select new { Id = CatDeduc.cde_IdDeducciones, Descripcion = CatDeduc.cde_DescripcionDeduccion };
            //RETORNAR LA DATA EN FORMATO JSON AL CLIENTE 
            return Json(DDL, JsonRequestBehavior.AllowGet);
        }

        //GET: TechosDeducciones/Inactivar/5    
        public ActionResult Inactivar(int id)
        {
            IEnumerable<object> listTechosDeducciones = null;
            string MensajeError = "";
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    listTechosDeducciones = db.UDP_Plani_tbTechosDeducciones_Inactivar(id,
                                                                                        1,
                                                                                        DateTime.Now);

                    foreach (UDP_Plani_tbTechosDeducciones_Inactivar_Result Resultado in listTechosDeducciones)
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
                response = "bien";
            }
            else
            {
                //Se devuelve un mensaje de error en caso de que el modelo no sea válido
                response = "error";
            }

            return Json(JsonRequestBehavior.AllowGet);
        }

        //GET: TechosDeducciones/Inactivar/5    
        public ActionResult Activar(int id)
        {
            IEnumerable<object> listTechosDeducciones = null;
            string MensajeError = "";
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    listTechosDeducciones = db.UDP_Plani_tbTechosDeducciones_Activar(id,
                                                                                        1,
                                                                                        DateTime.Now);

                    foreach (UDP_Plani_tbTechosDeducciones_Activar_Result Resultado in listTechosDeducciones)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo activar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception)
                {
                    response = "error";
                }
                response = "bien";
            }
            else
            {
                //Se devuelve un mensaje de error en caso de que el modelo no sea válido
                response = "error";
            }

            return Json(JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
