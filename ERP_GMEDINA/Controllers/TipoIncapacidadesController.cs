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
    public class TipoIncapacidadesController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();


        public ActionResult Index()
        {
            List<tbTipoIncapacidades> tbTipoIncapacidades = new List<Models.tbTipoIncapacidades> { };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            try
            {
                tbTipoIncapacidades = db.tbTipoIncapacidades.Where(x => x.ticn_Estado == true).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).ToList();
                return View(tbTipoIncapacidades);
            }
            catch (Exception ex)
            {


                ex.Message.ToString();
                tbTipoIncapacidades.Add(new tbTipoIncapacidades { ticn_Id = 0, ticn_Descripcion = "Fallo la conexión" });
            }
            return View(tbTipoIncapacidades);
        }


        [HttpPost]
        public JsonResult llenarTabla()
        {
            List<tbTipoIncapacidades> tbTipoIncapacidades = new List<Models.tbTipoIncapacidades> { };
            foreach (tbTipoIncapacidades x in db.tbTipoIncapacidades.ToList().Where(x => x.ticn_Estado == true))
            {
                tbTipoIncapacidades.Add(new tbTipoIncapacidades
                {
                    ticn_Id = x.ticn_Id,

                    ticn_Descripcion = x.ticn_Descripcion
                });
            }
            return Json(tbTipoIncapacidades, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Create(tbTipoIncapacidades tbTipoIncapacidades)
        {
            string msj = "";
            if (tbTipoIncapacidades.ticn_Descripcion != "")
            {
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbTipoIncapacidades_Insert(tbTipoIncapacidades.ticn_Descripcion, usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbTipoIncapacidades_Insert_Result item in list)
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


        // GET: Habilidades/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoIncapacidades tbTipoIncapacidades = null;


            try
            {
                tbTipoIncapacidades= db.tbTipoIncapacidades.Find(id);
                if (tbTipoIncapacidades== null || !tbTipoIncapacidades.ticn_Estado)
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
            var TipoIncapacidades = new tbTipoIncapacidades
            {
                ticn_Id = tbTipoIncapacidades.ticn_Id,
                ticn_Descripcion = tbTipoIncapacidades.ticn_Descripcion,
                ticn_Estado = tbTipoIncapacidades.ticn_Estado,
                ticn_RazonInactivo = tbTipoIncapacidades.ticn_RazonInactivo,
                ticn_UsuarioCrea = tbTipoIncapacidades.ticn_UsuarioCrea,
                ticn_FechaCrea = tbTipoIncapacidades.ticn_FechaCrea,
                ticn_UsuarioModifica = tbTipoIncapacidades.ticn_UsuarioModifica,
                ticn_FechaModifica = tbTipoIncapacidades.ticn_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoIncapacidades.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbTipoIncapacidades.tbUsuario).usu_NombreUsuario }
            };
            return Json(TipoIncapacidades, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Edit(tbTipoIncapacidades tbTipoIncapacidades)
        {
            string msj = "";
            if (tbTipoIncapacidades.ticn_Descripcion != "")
            {
                var id = (int)Session["id"];
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbTipoIncapacidades_Update(id, tbTipoIncapacidades.ticn_Descripcion, usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbTipoIncapacidades_Update_Result item in list)
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


        [HttpPost]
        public ActionResult Delete(tbTipoIncapacidades tbTipoIncapacidades)
        {
            string msj = "";
            if (tbTipoIncapacidades.ticn_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbTipoIncapacidades_Delete(id, tbTipoIncapacidades.ticn_RazonInactivo, usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbTipoIncapacidades_Delete_Result item in list)
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
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


















