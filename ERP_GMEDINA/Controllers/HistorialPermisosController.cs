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
    public class HistorialPermisosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: HistorialPermisos
        public ActionResult Index()
        {
            //ViewBag.tsal_Id = new SelectList(db.tbTipoSalidas, "tsal_Id", "tsal_Descripcion");
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            var tbHistorialPermisos = new List<tbHistorialPermisos> { };
            return View(tbHistorialPermisos);
        }
        public ActionResult llenarTabla()
        {
            //string estado = 
            try
            {
                //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                //posteriormente es destruida.
                using (db = new ERP_GMEDINAEntities())
                {
                    var V_tbHistorialPermisos_completa = db.V_tbHistorialPermisos_completa
                          .Select(
                          t => new
                          {
                              // p => (p.Date.Value == null ? p.Date.Value : p.Date.Value.Date) == SelectedDate.Date
                              hper_Id = t.hper_Id,
                              hper_Justificado = t.hper_Justificado,
                              per_Nombres = t.per_Nombres + " " + t.per_Apellidos,
                              per_CorreoElectronico = t.per_CorreoElectronico,
                              per_Telefono = t.per_Telefono,
                              per_Direccion = t.per_Direccion,
                              per_Edad = t.per_Edad,
                              per_EstadoCivil = t.per_EstadoCivil,
                              hper_Observacion = t.hper_Observacion,
                              hper_fechaInicio = t.hper_fechaInicio,
                              hper_fechaFin = t.hper_fechaFin,
                              hper_Duracion = t.hper_Duracion,
                              hper_PorcentajeIndemnizado = t.hper_PorcentajeIndemnizado,
                              tper_Id = t.tper_Id,
                              tper_Descripcion = t.tper_Descripcion
                          }
                          )
                          .ToList();
                    return Json(V_tbHistorialPermisos_completa, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ChildRowData(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<V_tbHistorialPermisos_completa> lista = new List<V_tbHistorialPermisos_completa> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    lista = db.V_tbHistorialPermisos_completa.Where(x => x.hper_Id == id).ToList();
                }
                catch
                {
                }
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        //--------------------------------------------DESPLEGABLES--------------------------------------------
        //Tipo salidas
        public ActionResult llenarDropDowlistTipoPermisos()
        {
            var TipoPermisos = new List<object> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    TipoPermisos.Add(new
                    {
                        Id = 0,
                        Descripcion = "**Seleccione una opción**"
                    });
                    TipoPermisos.AddRange(db.tbTipoPermisos
                    .Select(tabla => new {
                        Id = tabla.tper_Id,
                        Descripcion = tabla.tper_Descripcion
                    })
                    .ToList());
                }
                catch
                {
                    return Json("-2", 0);
                }

            }
            var result = new Dictionary<string, object>();
            result.Add("TipoPermisos", TipoPermisos);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //Empleados
        public ActionResult llenarDropDowlistEmpleados()
        {
            var Empleados = new List<object> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    Empleados.Add(new
                    {
                        Id = 0,
                        Descripcion = "**Seleccione una opción**"
                    });
                    Empleados.AddRange(db.V_HistorialPermisos_Empleados
                    .Select(tabla => new {
                        Id = tabla.Id,
                        Descripcion = tabla.Nombre
                    })
                    .ToList());
                }
                catch (Exception ex)
                {
                    return Json("-2", 0);
                }

            }
            var result = new Dictionary<string, object>();
            result.Add("Empleados", Empleados);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<tbTipoPermisos> TipoPermisos = new List<tbTipoPermisos> { };
            ViewBag.tper_Id = new SelectList(TipoPermisos, "tper_Id", "tper_Descripcion");
            List<V_HistorialPermisos_Empleados> Empleados = new List<V_HistorialPermisos_Empleados> { };
            ViewBag.Id = new SelectList(Empleados, "Id", "Nombre");
            return View();
        }
        [HttpPost]
        public ActionResult Create(tbHistorialPermisos tbHistorialPermisos, tbEmpleados[] tbEmpleados)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            //en esta area ingresamos el registro con el procedimiento almacenado
            try
            {
                db = new ERP_GMEDINAEntities();
                using (var transaction = db.Database.BeginTransaction())
                {
                    foreach (tbEmpleados em in tbEmpleados)
                    {
                        var observacion = tbHistorialPermisos.hper_Observacion == null ? "Ninguna" : tbHistorialPermisos.hper_Observacion;
                        //var razon = em.emp_RazonInactivo == null ? "Ninguna" : em.emp_RazonInactivo;
                        var emp = db.UDP_RRHH_tbHistorialPermisos_Insert(
                        em.emp_Id,
                        tbHistorialPermisos.tper_Id,
                        tbHistorialPermisos.hper_fechaInicio,
                        tbHistorialPermisos.hper_fechaFin,
                        //tbHistorialPermisos.hper_Duracion,
                        observacion,
                        tbHistorialPermisos.hper_Justificado,
                        tbHistorialPermisos.hper_PorcentajeIndemnizado,
                        1,
                        DateTime.Now);
                        string mensajeDB = "";
                        foreach (UDP_RRHH_tbHistorialPermisos_Insert_Result i in emp)
                        {
                            mensajeDB = i.MensajeError.ToString();
                        }
                        if (mensajeDB == "-1")
                        {
                            return Json("-2", JsonRequestBehavior.AllowGet);
                        }
                    }
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("-2", JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        ///EDIT Y UPDATE
        ///
        public ActionResult Edit(int? id)
        {
            db = new ERP_GMEDINAEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHistorialPermisos tbHistorialPermisos = null;
            try
            {
                tbHistorialPermisos = db.tbHistorialPermisos.Find(id);
                if (tbHistorialPermisos == null || !tbHistorialPermisos.hper_Estado)
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
            var HistorialPermisos = new tbHistorialPermisos
            {
                hper_Id = tbHistorialPermisos.hper_Id,
                hper_Observacion = tbHistorialPermisos.hper_Observacion,
                hper_Estado = tbHistorialPermisos.hper_Estado,
                hper_RazonInactivo = tbHistorialPermisos.hper_RazonInactivo,
                hper_fechaInicio = tbHistorialPermisos.hper_fechaInicio,
                hper_fechaFin = tbHistorialPermisos.hper_fechaFin,
                hper_Duracion=tbHistorialPermisos.hper_Duracion,

                hper_UsuarioCrea = tbHistorialPermisos.hper_UsuarioCrea,
                hper_FechaCrea = tbHistorialPermisos.hper_FechaCrea,
                hper_UsuarioModifica = tbHistorialPermisos.hper_UsuarioModifica,
                hper_FechaModifica = tbHistorialPermisos.hper_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbHistorialPermisos.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbHistorialPermisos.tbUsuario1).usu_NombreUsuario }
            };
            return Json(HistorialPermisos, JsonRequestBehavior.AllowGet);
        }
        // POST: Habilidades/Edit/5
        [HttpPost]
        public JsonResult Edit(string hsal_Observacion)
        {
            string msj = "";
            db = new ERP_GMEDINAEntities();
            tbHistorialPermisos tbHistorialPermisos = new tbHistorialPermisos();
            //tbTipoHoras.tiho_Id = id;
            tbHistorialPermisos.hper_Observacion = hsal_Observacion;
            if (tbHistorialPermisos.hper_Observacion != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                var id = (int)Session["id"];
                try
                {
                    var list = db.UDP_RRHH_tbHistorialPermisos_Update(id, tbHistorialPermisos.hper_Observacion, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbHistorialPermisos_Update_Result item in list)
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
        // GET: Habilidades/Delete/5
        [HttpPost]
        public ActionResult Delete(string hper_RazonInactivo)
        {
            string msj = "";
            db = new ERP_GMEDINAEntities();
            tbHistorialPermisos tbHistorialPermisos = new tbHistorialPermisos();
            //tbTipoHoras.tiho_Id = id;
            tbHistorialPermisos.hper_RazonInactivo = hper_RazonInactivo;

            if (tbHistorialPermisos.hper_RazonInactivo != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                var id = (int)Session["id"];
                try
                {
                    var list = db.UDP_RRHH_tbHistorialPermisos_Delete(id, tbHistorialPermisos.hper_RazonInactivo, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbHistorialPermisos_Delete_Result item in list)
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
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}