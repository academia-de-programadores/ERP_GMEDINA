﻿using System;
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
    public class SueldosController : Controller
    {
        private ERP_GMEDINAEntities db = null;

        Models.Helpers Function = new Models.Helpers();


        // GET: Titulos
        [SessionManager("Sueldos/Index")]
        public ActionResult Index()
        {

            List<tbSueldos> tbSueldos = new List<tbSueldos> { };
            try
            {
                tbSueldos = db.tbSueldos.Where(x => x.sue_Estado == true).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).ToList();
                return View(tbSueldos);
            }
            catch (Exception ex)
            {

                ex.Message.ToString();
                tbSueldos.Add(new tbSueldos { sue_Id = 0 });
            }
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
                            Id_Empleado = t.Id_Empleado,
                            Id_Amonestacion = t.Id_Amonestacion,
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
                            Fecha_Modifica = t.Fecha_Modifica,
                            Estado = t.Estado,
                            Sueldo_Maximo = t.Sueldo_Maximo,
                            Sueldo_Minimo = t.Sueldo_Minimo,
                            Id_cargo = t.Id_Cargo

                        }

                        )
                        .Where(x => x.Estado == true).ToList();
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
                    lista = db.V_Sueldos.OrderByDescending(x => x.Id).Where(x => x.Id_Empleado == id && x.Estado == false).ToList();
                }
                catch
                {

                }
            }
            return Json(lista, JsonRequestBehavior.AllowGet);

        }



        // POST: /Sueldos/Create


        /*        public ActionResult Create()
                {
                    //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                    //posteriormente es destruida.
                    List<tbSueldos> sueldos = new List<tbSueldos> { };
                    ViewBag.sue_Id = new SelectList(sueldos, "sue_Id", "sue_cantidad");
                    return View();
                }

                [HttpPost]
                public ActionResult Create(tbSueldos vsueldos)
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
                }*/

        // GET: /Sueldos//Edit/5
        [HttpGet]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            V_Sueldos VSueldos = null;
            tbSueldos sueldos1 = null;

            try
            {
                db = new ERP_GMEDINAEntities();
                sueldos1 = db.tbSueldos.Find(id);
                if (sueldos1 == null)
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
            db = new ERP_GMEDINAEntities();
            var sueldos = db.V_Sueldos.Select(x =>
            new
            {

                Id = x.Id,
                Identidad = x.Identidad,
                Id_Empleado = x.Id_Empleado,
                Id_Amonestacion = x.Id_Amonestacion,
                Sueldo = x.Sueldo,
                Sueldo_Anterior = x.Sueldo_Anterior,
                Estado = x.Estado,
                RazonInactivo = x.RazonInactivo,
                Usuario_Nombre = x.Usuario_Nombre,
                Usuario_Crea = x.Usuario_Crea,
                Fecha_Crea = x.Fecha_Crea,
                Usuario_Modifica = x.Usuario_Modifica,
                Fecha_Modifica = x.Fecha_Modifica,
                Sueldo_Maximo = x.Sueldo_Maximo,
                Sueldo_Minimo = x.Sueldo_Minimo,


            }).Where(x => x.Id == id).ToList();

            return Json(sueldos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(cSueldos tbsueldos)
        {
            string msj = "";
            if (tbsueldos.sue_Id != 0 && decimal.Parse(tbsueldos.sue_Cantidad) != 0)
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbSueldos_Insert(tbsueldos.sue_Id, tbsueldos.emp_Id, tbsueldos.tmon_Id, Convert.ToDecimal(tbsueldos.sue_Cantidad), (int)Session["UserLogin"], (int)Session["UserLogin"],Function.DatetimeNow());
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
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }
        // GET: /Sueldos//Delete/5
        [HttpPost]
        [SessionManager("Sueldos/Edit")]

        public ActionResult Delete(tbSueldos tbSueldos)
        {
            string msj = "";

            string RazonInactivo = "Se ha Inhabilitado este Registro";

            if (tbSueldos.sue_Id != 0)
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    /* var list = db.UDP_RRHH_tbSueldos_Delete(id, tbSueldos.sue_RazonInactivo, Usuario.usu_Id, DateTime.Now);
                     foreach (UDP_RRHH_tbSueldos_Delete_Result item in list)
                     {
                         msj = item.MensajeError + " ";
                     }*/
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
                //Session.Remove("id");
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }
        [SessionManager("Sueldos/Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
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

        protected tbUsuario IsNull(tbUsuario valor)
        {
            if (valor != null)
            {
                return valor;
            }
            else
            {
                return new tbUsuario { usu_NombreUsuario = "" };
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
