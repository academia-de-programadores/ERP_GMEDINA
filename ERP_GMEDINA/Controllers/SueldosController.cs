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
	public class SueldosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: /Sueldos/
        public ActionResult Index()        
		{           
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            List<tbSueldos> tbSueldos = new List<tbSueldos> { };
            return View(tbSueldos);
        }

        public ActionResult llenarTabla()
        {
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var tbsueldos = db.V_Sueldos
                        .Select(
                        t => new
                        {
                            Id = t.Id,
                            Identidad = t.Identidad,
                            Nombre = t.Nombre,
                            Sueldo = t.Sueldo,
                            Tipo_Moneda = t.Tipo_Moneda,
                            Cuenta = t.Cuenta,
                            Sueldo_Anterior = t.Sueldo_Anterior,
                            Area = t.Area,
                            Cargo = t.Cargo,
                            Usuario_Nombre = t.Usuario_Nombre,
                            Usuario_Crea = t.Usuario_Crea,
                            Fecha_Crea = t.Fecha_Crea,
                            Usuario_Modifica = t.Usuario_Modifica,
                            Fecha_Modifica = t.Fecha_Modifica

                        }

                        )
                        .ToList();
                    return Json(tbsueldos, JsonRequestBehavior.AllowGet);
                        
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);

            }
        }


        public ActionResult ChildRowData(int? id)
        {
            List<V_Sueldos> lista = new List<V_Sueldos> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    lista = db.V_Sueldos.Where(x => x.Id == id).ToList();
                }
                catch
                {

                }
            }
            return Json(lista, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Details(int? id)
        {
            if (id== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbSueldos tbSueldos = null;
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    tbSueldos = db.tbSueldos.Find(id);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    
                }
            }
            if (tbSueldos == null)
            {
                return HttpNotFound();

            }
            return View(tbSueldos);
        }


        // POST: /Sueldos/Create


        public ActionResult Create()
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<tbSueldos> sueldos = new List<tbSueldos> { };
            ViewBag.sue_Id = new SelectList(sueldos, "sue_Id", "sue_cantidad");
            return View();
        }

        [HttpPost]
        public ActionResult Create(V_Sueldos vsueldos)
        {
            string msj = "";
            using (db = new ERP_GMEDINAEntities())

            {
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    //var list = db.UDP_RRHH_tbSueldos_Insert(vsueldos.Id_Empleado,
                    //                                        vsueldos.Id_Amonestacion,
                    //                                        vsueldos.Sueldo,
                    //                                        vsueldos.Sueldo_Anterior,
                    //                                        usuario.usu_Id,
                    //                                        DateTime.Now);

                    //foreach (UDP_RRHH_tbSueldos_Insert_Result item in list)
                    //{
                    //    msj = item.MensajeError + " ";
                    //}
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
            }

            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }

        // GET: /Sueldos//Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbSueldos VSueldos = null;

            try
            {
                VSueldos = db.tbSueldos.Find(id);
                if (VSueldos == null || !VSueldos.sue_Estado)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return HttpNotFound();
            }
            Session["id"] = id;
            var sueldos = new tbSueldos
            {
               
                sue_Id = VSueldos.sue_Id,
                emp_Id = VSueldos.emp_Id,
                tmon_Id = VSueldos.tmon_Id,
                sue_Cantidad = VSueldos.sue_Cantidad,
                sue_SueldoAnterior = VSueldos.sue_SueldoAnterior,
                sue_Estado = VSueldos.sue_Estado,
                sue_RazonInactivo = VSueldos.sue_RazonInactivo,
                sue_UsuarioCrea = VSueldos.sue_UsuarioCrea,
                sue_FechaCrea = VSueldos.sue_FechaCrea,
                sue_UsuarioModifica = VSueldos.sue_UsuarioModifica,
                sue_FechaModifica = VSueldos.sue_FechaModifica,
           

            };
            return Json(sueldos, JsonRequestBehavior.AllowGet);
        }
          
        [HttpPost]
        public JsonResult Edit(tbSueldos tbsueldos)
        {
            string msj = "";
            if (tbsueldos.sue_Id != 0 && tbsueldos.emp_Id != 0 && tbsueldos.tmon_Id != 0 && tbsueldos.sue_Cantidad !=0  && tbsueldos.sue_SueldoAnterior !=0)            
			{
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbSueldos_Insert(id, tbsueldos.emp_Id, tbsueldos.tmon_Id, tbsueldos.sue_Cantidad, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbSueldos_Insert_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
                Session.Remove("id");
            }
            else
            {
                msj = "-3";
            }            
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }
		// GET: /Sueldos//Delete/5
        [HttpPost]
        public ActionResult Delete(tbSueldos tbSueldos)
        {
            string msj = "";
            if (tbSueldos.sue_Id != 0 && tbSueldos.sue_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbSueldos_Delete(id, tbSueldos.sue_RazonInactivo, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbSueldos_Delete_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
                Session.Remove("id");
            }
            else
            {
                msj = "-3";
            }            
            return Json(msj.Substring(0, 2),JsonRequestBehavior.AllowGet);
        }
        protected tbUsuario IsNull(tbUsuario valor)
        {
            if (valor!=null)
            {
                return valor;
            }
            else
            {
                return new tbUsuario {usu_NombreUsuario="" };
            }
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
