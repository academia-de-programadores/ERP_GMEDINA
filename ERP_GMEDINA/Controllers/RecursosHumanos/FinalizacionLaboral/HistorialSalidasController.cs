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
    public class HistorialSalidasController : Controller
    {
        private ERP_GMEDINAEntities db = null;
        Models.Helpers Function = new Models.Helpers();
        // GET: /EquipoTrabajo/
        [SessionManager("HistorialSalidas/Index")]
        public ActionResult Index()
        {
            //bool Admin = (bool)Session["Admin"];
            tbHistorialSalidas tbHistorialSalidas = new tbHistorialSalidas { hsal_Estado = true };
            return View(tbHistorialSalidas);
        }
        public ActionResult llenarTabla()
        {
            try
            {
                db = new ERP_GMEDINAEntities();
                var V_tbHistorialSalidas_completa = db.V_tbHistorialSalidas_completa
                    .Select(
                        t => new
                        {
                            hsal_Id = t.hsal_Id,
                            tsal_Id = t.tsal_Id,
                            tsal_Descripcion = t.tsal_Descripcion,
                            rsal_Id = t.rsal_Id,
                            rsal_Descripcion = t.rsal_Descripcion,
                            per_Nombres = t.per_Nombres + " " + t.per_Apellidos,
                            per_CorreoElectronico = t.per_CorreoElectronico,
                            per_Telefono = t.per_Telefono,
                            per_Direccion = t.per_Direccion,
                            per_Edad = t.per_Edad,
                            per_EstadoCivil = t.per_EstadoCivil,
                            hsal_Observacion = t.hsal_Observacion,
                            hsal_Estado = t.hsal_Estado,
                            hsal_FechaSalida = t.hsal_FechaSalida
                        }
                        )
                        .ToList();
                    return Json(V_tbHistorialSalidas_completa, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {

                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManager("HistorialSalidas/hablilitar")]
        [HttpPost]
        public JsonResult hablilitar(int id)
        {
            string result = "";
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var list = db.UDP_RRHH_tbHistorialSalidas_Restore(id, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbHistorialSalidas_Restore_Result item in list)
                    {
                        result = item.MensajeError;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                result = "-2";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChildRowData(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<V_tbHistorialSalidas_completa> lista = new List<V_tbHistorialSalidas_completa> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    lista = db.V_tbHistorialSalidas_completa.Where(x => x.hsal_Id == id).ToList();
                }
                catch
                {
                }
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        //--------------------------------------------DESPLEGABLES--------------------------------------------
        //Tipo salidas
        public ActionResult llenarDropDowlistTipoSalida()
        {
            var TipoSalidas = new List<object> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    TipoSalidas.Add(new
                    {
                        Id = 0,
                        Descripcion = "**Seleccione una opción**"
                    });
                    TipoSalidas.AddRange(db.tbTipoSalidas
                    .Select(tabla => new {
                        Id = tabla.tsal_Id,
                        Descripcion = tabla.tsal_Descripcion
                    })
                    .ToList());
                }
                catch
                {
                    return Json("-2", 0);
                }

            }
            var result = new Dictionary<string, object>();
            result.Add("TipoSalidas", TipoSalidas);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //Razon salidas
        public ActionResult llenarDropDowlistRazonSalida()
        {
            var RazonSalidas = new List<object> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    RazonSalidas.Add(new
                    {
                        Id = 0,
                        Descripcion = "**Seleccione una opción**"
                    });
                    RazonSalidas.AddRange(db.tbRazonSalidas
                    .Select(tabla => new {
                        Id = tabla.rsal_Id,
                        Descripcion = tabla.rsal_Descripcion
                    })
                    .ToList());
                }
                catch(Exception ex)
                {
                    return Json("-2", 0);
                }

            }
            var result = new Dictionary<string, object>();
            result.Add("RazonSalidas", RazonSalidas);
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
                    Empleados.AddRange(db.V_Empleados
                    .Select(tabla => new {
                        Id = tabla.emp_Id,
                        Descripcion = tabla.per_NombreCompleto
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


        [SessionManager("HistorialSalidas/Create")]
        public ActionResult Create()
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<tbTipoSalidas> TipoSalidas = new List<tbTipoSalidas> { };
            ViewBag.Tsal_Id = new SelectList(TipoSalidas, "Tsal_Id", "Tsal_Descripcion");
            List<tbRazonSalidas> RazonSalidas = new List<tbRazonSalidas> { };
            ViewBag.rsal_Id = new SelectList(RazonSalidas, "rsal_Id", "rsal_Descripcion");
            List<V_HistorialSalidas_Empleados> Empleados = new List<V_HistorialSalidas_Empleados> { };
            ViewBag.Id = new SelectList(Empleados, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [SessionManager("HistorialSalidas/Create")]
        public ActionResult Create(tbHistorialSalidas tbHistorialSalidas, tbEmpleados[] tbEmpleados)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            string result = "";
            db = new ERP_GMEDINAEntities();
            try
            {
                db = new ERP_GMEDINAEntities();
                using (var transaction = db.Database.BeginTransaction())
                {
                    foreach (tbEmpleados em in tbEmpleados)
                    {
                        var observacion = tbHistorialSalidas.hsal_Observacion == null ? "Ninguna" : tbHistorialSalidas.hsal_Observacion;
                        //var razon = em.emp_RazonInactivo == null ? "Ninguna" : em.emp_RazonInactivo;
                        var emp = db.UDP_RRHH_tbHistorialSalidas_Insert(
                        em.emp_Id,
                        tbHistorialSalidas.tsal_Id,
                        tbHistorialSalidas.rsal_Id,
                        tbHistorialSalidas.hsal_FechaSalida, 
                        observacion,
                        em.emp_RazonInactivo,
                        (int)Session["UserLogin"], 
                        Function.DatetimeNow());
                        string mensajeDB = "";
                        foreach (UDP_RRHH_tbHistorialSalidas_Insert_Result i in emp)
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
        [SessionManager("HistorialSalidas/Edit")]
        public ActionResult Edit(int? id)
        {
            db = new ERP_GMEDINAEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHistorialSalidas tbHistorialSalidas = null;
            try
            {
                tbHistorialSalidas = db.tbHistorialSalidas.Find(id);
                if (tbHistorialSalidas == null)
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
            var HistorialSalidas = new tbHistorialSalidas
            {
                hsal_Id = tbHistorialSalidas.hsal_Id,
                hsal_Observacion = tbHistorialSalidas.hsal_Observacion,
                hsal_Estado = tbHistorialSalidas.hsal_Estado,
                hsal_RazonInactivo = tbHistorialSalidas.hsal_RazonInactivo,
                hsal_UsuarioCrea = tbHistorialSalidas.hsal_UsuarioCrea,
                hsal_FechaCrea = tbHistorialSalidas.hsal_FechaCrea,
                hsal_UsuarioModifica = tbHistorialSalidas.hsal_UsuarioModifica,
                hsal_FechaModifica = tbHistorialSalidas.hsal_FechaModifica,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbHistorialSalidas.tbUsuario).usu_NombreUsuario },
                tbUsuario1 = new tbUsuario { usu_NombreUsuario = IsNull(tbHistorialSalidas.tbUsuario1).usu_NombreUsuario }
            };
            return Json(HistorialSalidas, JsonRequestBehavior.AllowGet);
        }

        // POST: Habilidades/Edit/5
        [HttpPost]
        [SessionManager("HistorialSalidas/Edit")]
        public JsonResult Edit(string hsal_Observacion)
        {
            string msj = "";
            db = new ERP_GMEDINAEntities();
            tbHistorialSalidas tbHistorialSalidas = new tbHistorialSalidas();
            //tbTipoHoras.tiho_Id = id;
            tbHistorialSalidas.hsal_Observacion = hsal_Observacion;
            if (tbHistorialSalidas.hsal_Observacion != "")
            {
                var id = (int)Session["id"];
                try
                {
                    var list = db.UDP_RRHH_tbHistorialSalidas_Update(id, tbHistorialSalidas.hsal_Observacion, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbHistorialSalidas_Update_Result item in list)
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

        // GET: Habilidades/Delete/5
        [HttpPost]
        [SessionManager("HistorialSalidas/Delete")]
        public ActionResult Delete(string hsal_RazonInactivo)
        {
            string msj = "";
            db = new ERP_GMEDINAEntities();
            tbHistorialSalidas tbHistorialSalidas = new tbHistorialSalidas();
            //tbTipoHoras.tiho_Id = id;
            tbHistorialSalidas.hsal_RazonInactivo = hsal_RazonInactivo;

            if (tbHistorialSalidas.hsal_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                try
                {
                    var list = db.UDP_RRHH_tbHistorialSalidas_Delete(id, tbHistorialSalidas.hsal_RazonInactivo, (int)Session["UserLogin"], Function.DatetimeNow());
                    foreach (UDP_RRHH_tbHistorialSalidas_Delete_Result item in list)
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