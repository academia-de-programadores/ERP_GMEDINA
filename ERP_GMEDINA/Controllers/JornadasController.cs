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
    public class JornadasController : Controller
    {
        private ERP_GMEDINAEntities db = null;

        Models.Helpers Fuction = new Models.Helpers();
        [SessionManager("Jornadas/Index")]

        public ActionResult Index()
        {
            try
            {
                db = new ERP_GMEDINAEntities();
                tbJornadas tbJornadas = new tbJornadas { jor_Estado = true };
                bool Admin = (bool)Session["Admin"];
                return View(tbJornadas);

            }
            catch (Exception)
            {
                return View();

            }
        }

        [SessionManager("Jornadas/Index")]
        public ActionResult ChildRowData(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            //List<tbHorarios> lista = new List<tbHorarios> { };
            //using (db = new ERP_GMEDINAEntities())
                
            {
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var lista = db.V_HorariosDetalles.Where(x => x.jor_Id == id)
                        .Select(tabla =>
                        new
                        {
                            jor_Id = tabla.jor_Id,
                            hor_Id = tabla.hor_Id,
                            hor_HoraInicio = tabla.hor_HoraInicio,
                            hor_HoraFin = tabla.hor_HoraFin,
                            hor_descripcion = tabla.hor_Descripcion,
                            hor_Estado = tabla.hor_Estado
                        }).ToList();
                    return Json(lista, JsonRequestBehavior.AllowGet);
                }
                catch (Exception EX)
                {
                    EX.Message.ToString();

                }
            }
            return Json("-2", JsonRequestBehavior.AllowGet);
        }

        [SessionManager("Jornadas/Index")]
        public ActionResult llenarTabla()
        {
            try
            {
                //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                //posteriormente es destruida.
                using (db = new ERP_GMEDINAEntities())
                {
                    var tbJornadas = db.tbJornadas.Select(t => new { jor_Id = t.jor_Id, jor_Descripcion = t.jor_Descripcion, jor_Estado = t.jor_Estado }).ToList();
                    return Json(tbJornadas, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Jornadas/Create        
        public ActionResult Create()
        {
            ViewBag.jor_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.jor_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }
        
        // POST: Jornadas/Create
        [HttpPost]
        [SessionManager("Jornadas/Create")]
        public ActionResult Create(tbJornadas tbJornadas)
        {
            string msj = "...";
            if (tbJornadas.jor_Descripcion != "")
            {
                db = new ERP_GMEDINAEntities();                
                try
                {

                    var list = db.UDP_RRHH_tbJornadas_Insert(tbJornadas.jor_Descripcion, (int)Session["UserLogin"], Fuction.DatetimeNow());
                    foreach (UDP_RRHH_tbJornadas_Insert_Result item in list)
                    {
                        msj = item.MensajeError;
                        return Json(msj, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                    return Json(msj, JsonRequestBehavior.AllowGet);
                }
            }

            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SessionManager("Jornadas/CreateHorario")]
        public ActionResult CreateHorario(tbHorarios tbHorarios)
        {
            string msj = "...";
            if (tbHorarios.hor_Descripcion != "")
            {
                db = new ERP_GMEDINAEntities();                
                try
                {
                    var list = db.UDP_RRHH_tbHorarios_Insert(tbHorarios.jor_Id, tbHorarios.hor_Descripcion, tbHorarios.hor_HoraInicio, tbHorarios.hor_HoraFin, (int)Session["UserLogin"], Fuction.DatetimeNow());
                    foreach (UDP_RRHH_tbHorarios_Insert_Result item in list)
                    {
                        msj = item.MensajeError;
                        return Json(msj, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                    return Json(msj, JsonRequestBehavior.AllowGet);
                }
            }

            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }

        // GET: Jornadas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbJornadas tbJornadas = null;
            try
            {
                db = new ERP_GMEDINAEntities();         
                tbJornadas = db.tbJornadas.Find(id);
                if (tbJornadas == null)
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
            var jornada = new tbJornadas
            {                
                jor_Id = tbJornadas.jor_Id,
                jor_Descripcion = tbJornadas.jor_Descripcion,
                jor_Estado = tbJornadas.jor_Estado,
                jor_RazonInactivo = tbJornadas.jor_RazonInactivo,
                jor_UsuarioCrea = tbJornadas.jor_UsuarioCrea,
                jor_FechaCrea = tbJornadas.jor_FechaCrea,
                jor_UsuarioModifica = tbJornadas.jor_UsuarioModifica,
                jor_FechaModifica = tbJornadas.jor_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbJornadas.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbJornadas.tbUsuario1).usu_NombreUsuario }
            };
            return Json(jornada, JsonRequestBehavior.AllowGet);            
        }

        public ActionResult EditHorario(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbHorarios tbHorarios = null;
            try
            {
                db = new ERP_GMEDINAEntities();
                tbHorarios = db.tbHorarios.Find(id);
                if (tbHorarios == null)
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
            var horario = new tbHorarios
            {
                hor_Id = tbHorarios.hor_Id,
                jor_Id = tbHorarios.jor_Id,
                hor_Descripcion = tbHorarios.hor_Descripcion,
                hor_HoraInicio = tbHorarios.hor_HoraInicio,
                hor_HoraFin = tbHorarios.hor_HoraFin,
                hor_CantidadHoras = tbHorarios.hor_CantidadHoras,
                hor_Estado = tbHorarios.hor_Estado,
                hor_RazonInactivo = tbHorarios.hor_RazonInactivo,
                hor_UsuarioCrea = tbHorarios.hor_UsuarioCrea,
                hor_FechaCrea = tbHorarios.hor_FechaCrea,
                hor_UsuarioModifica = tbHorarios.hor_UsuarioModifica,
                hor_FechaModifica = tbHorarios.hor_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbHorarios.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbHorarios.tbUsuario1).usu_NombreUsuario }
            };
            return Json(horario, JsonRequestBehavior.AllowGet);
        }

        // POST: Jornadas/Edit/5
        [HttpPost]
        [SessionManager("Jornadas/Edit")]
        public JsonResult Edit(tbJornadas tbJornadas)
        {
            string msj = "";
            if (tbJornadas.jor_Id != 0 && tbJornadas.jor_Descripcion != "")
            {
                var id = (int)Session["id"];                
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbJornadas_Update(id, tbJornadas.jor_Descripcion, (int)Session["UserLogin"], Fuction.DatetimeNow());
                    foreach (UDP_RRHH_tbJornadas_Update_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
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

        [HttpPost]
        [SessionManager("Jornadas/EditHorario")]
        public JsonResult EditHorario(tbHorarios tbHorarios)
        {
            string msj = "";
            if (tbHorarios.hor_Id != 0 && tbHorarios.hor_Descripcion != "")
            {
                var id = (int)Session["id"];                
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbHorarios_Update(id, tbHorarios.hor_Descripcion, tbHorarios.hor_HoraInicio, tbHorarios.hor_HoraFin, (int)Session["UserLogin"], Fuction.DatetimeNow());
                    foreach (UDP_RRHH_tbHorarios_Update_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
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


        // GET: Jornadas/Delete/5
        [HttpPost]
        [SessionManager("Jornadas/Delete")]
        public ActionResult Delete(tbJornadas tbJornadas)
        {
            string msj = "...";
            string RazonInactivo = "Se ha Inhabilitado este Registro";
            if (tbJornadas.jor_Id != 0 )
            {
                var id = (int)Session["id"];                
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbJornadas_Delete(id, RazonInactivo, (int)Session["UserLogin"], Fuction.DatetimeNow());
                    foreach (UDP_RRHH_tbJornadas_Delete_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
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

        [HttpPost]
        [SessionManager("Jornadas/DeleteHorario")]
        public ActionResult DeleteHorario(tbHorarios tbHorarios)
        {
            string msj = "...";
            string RazonInactivo = "Se ha Inhabilitado este Registro";
            if (tbHorarios.hor_Id != 0 && tbHorarios.hor_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbHorarios_Delete(id, RazonInactivo, (int)Session["UserLogin"], Fuction.DatetimeNow());
                    foreach (UDP_RRHH_tbHorarios_Delete_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
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

        [HttpPost]
        [SessionManager("Jornadas/habilitar")]
        public JsonResult habilitar(int id)
        {
            string result = "";            
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    //db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbJornadas_Restore(id, (int)Session["UserLogin"], Fuction.DatetimeNow());
                    foreach (UDP_RRHH_tbJornadas_Restore_Result item in list)
                    {
                        result = item.MensajeError;
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    result = "-2";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SessionManager("Jornadas/habilitarHorario")]
        public JsonResult habilitarHorario(int id)
        {
            string result = "";
            string razon = "";            
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    //db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbHorarios_Restore(id, razon, (int)Session["UserLogin"], Fuction.DatetimeNow());
                    foreach (UDP_RRHH_tbHorarios_Restore_Result item in list)
                    {
                        result = item.MensajeError;
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    result = "-2";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && db!= null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
    }
}
