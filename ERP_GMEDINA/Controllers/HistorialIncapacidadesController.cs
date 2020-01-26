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
    public class HistorialIncapacidadesController : Controller
    {
        private ERP_GMEDINAEntities db = null;

        // GET: HistorialIncapacidades
        public ActionResult Index()
        {
            if (Session["Admin"] == null && Session["Usuario"] == null)
            {
                Response.Redirect("~/Inicio/index");
                return null;
            }
            try
            {
                db = new ERP_GMEDINAEntities();
                ViewBag.ticn_Id = new SelectList(db.tbTipoIncapacidades, "ticn_Id", "ticn_Descripcion");
                bool Admin = (bool)Session["Admin"];
                tbHistorialIncapacidades tbHistorialIncapacidades = new tbHistorialIncapacidades { hinc_Estado = true };
                return View(tbHistorialIncapacidades);
            }
            catch
            {
                return View();
            }
           

            //bool Admin = (bool)Session["Admin"];
            //tbHistorialIncapacidades tbHistorialIncapacidades = new tbHistorialIncapacidades { hinc_Estado = true };
            //return View(tbHistorialIncapacidades);


        }



        public ActionResult Create()
        {
            db = new ERP_GMEDINAEntities();
            ViewBag.hinc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.hinc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria");
            ViewBag.ticn_Id = new SelectList(db.tbTipoIncapacidades, "ticn_Id", "ticn_Descripcion");

            return View();
        }

        // POST: tbHistorialIncapacidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create(tbHistorialIncapacidades tbHistorialIncapacidades)
        {
            string msj = "";
            if (tbHistorialIncapacidades.hinc_Diagnostico != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbHistorialIncapacidades_Insert(tbHistorialIncapacidades.emp_Id, tbHistorialIncapacidades.ticn_Id, tbHistorialIncapacidades.hinc_CentroMedico, tbHistorialIncapacidades.hinc_Doctor, tbHistorialIncapacidades.hinc_Diagnostico, tbHistorialIncapacidades.hinc_FechaInicio, tbHistorialIncapacidades.hinc_FechaFin, Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbHistorialIncapacidades_Insert_Result item in list)
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


        public ActionResult llenarTabla()
        {
            try
            {
                db = new ERP_GMEDINAEntities();
                using (db = new ERP_GMEDINAEntities())
                {
                    var Empleados = db.V_EmpleadoIncapacidades.Where(t => t.emp_Estado == true)
                        .Select(
                        t => new
                        {
                            emp_Id = t.emp_Id,
                            Empleado = t.emp_NombreCompleto,
                            Cargo = t.car_Descripcion,
                            Departamento = t.depto_Descripcion
                        }).ToList();
                    return Json(Empleados, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }





        public ActionResult ChildRowData(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            List<V_HistorialIncapacidades> lista = new List<V_HistorialIncapacidades> { };
            try
            {
                db = new ERP_GMEDINAEntities();
                using (db = new ERP_GMEDINAEntities())
                {
                    lista = db.V_HistorialIncapacidades.Where(x => x.emp_Id == id).ToList();
                }
            }
            catch
            {

            }
                return Json(lista, JsonRequestBehavior.AllowGet);
        }
        // GET: HistorialIncapacidades/Details/5
        public ActionResult Details(int? id)
        {
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                tbHistorialIncapacidades tbincapacidades = null;
               
                try
                {
                    tbincapacidades = db.tbHistorialIncapacidades.Find(id);
                    if (tbincapacidades == null || !tbincapacidades.hinc_Estado)
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
                var incapacidades = new tbHistorialIncapacidades
              
                {
                    hinc_Id = tbincapacidades.hinc_Id,
                   tbTipoIncapacidades= new tbTipoIncapacidades { ticn_Descripcion = tbincapacidades.tbTipoIncapacidades.ticn_Descripcion },
                    hinc_Dias = tbincapacidades.hinc_Dias,
                    hinc_CentroMedico = tbincapacidades.hinc_CentroMedico,
                    hinc_Doctor = tbincapacidades.hinc_Doctor,
                    hinc_Diagnostico = tbincapacidades.hinc_Diagnostico,
                    hinc_FechaInicio = tbincapacidades.hinc_FechaInicio,
                    hinc_FechaFin = tbincapacidades.hinc_FechaFin,
                    tbUsuario = new tbUsuario { usu_NombreUsuario = tbincapacidades.tbUsuario.usu_NombreUsuario },
                    tbUsuario1 = new tbUsuario { usu_NombreUsuario = tbincapacidades.tbUsuario1.usu_NombreUsuario }
                };

                return Json(incapacidades, JsonRequestBehavior.AllowGet);
            }
        }



        // GET: HistorialIncapacidades/Details/5
        public ActionResult Detallesempleados(int? id)
        {
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                tbEmpleados tbEmpleados1 = null;

                try
                {
                    db = new ERP_GMEDINAEntities();
                    tbEmpleados1 = db.tbEmpleados.Find(id);
                    if (tbEmpleados1 == null || !tbEmpleados1.emp_Estado)
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
                var empleados = new tbEmpleados

                {
                    tbPersonas = new tbPersonas { per_Identidad = IsNull(tbEmpleados1.tbPersonas).per_Identidad, per_Nombres = IsNull(tbEmpleados1.tbPersonas).per_Nombres + " " + IsNull(tbEmpleados1.tbPersonas).per_Apellidos }
                 
                
                };
                return Json(empleados, JsonRequestBehavior.AllowGet);
            }
        }


        // GET: HistorialIncapacidades/Create
        //public ActionResult Create()
        //{
        //    ViewBag.hinc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
        //    ViewBag.hinc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
        //    ViewBag.emp_Id = new SelectList(db.tbEmpleados, "emp_Id", "emp_CuentaBancaria");
        //    ViewBag.ticn_Id = new SelectList(db.tbTipoIncapacidades, "ticn_Id", "ticn_Descripcion");
        //    return View();
        //}

        // POST: HistorialIncapacidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //public JsonResult Create(tbHistorialIncapacidades tbHistorialIncapacidades)
        //{
        //    string msj = "";
        //    if (tbHistorialIncapacidades.hinc_Diagnostico != "")
        //    {
        //        var Usuario = (tbUsuario)Session["Usuario"];
        //        try
        //        {
        //            var list = db.UDP_RRHH_tbHistorialIncapacidades_Insert(tbHistorialIncapacidades.emp_Id,
        //                                                                    tbHistorialIncapacidades.ticn_Id,
        //                                                                    tbHistorialIncapacidades.hinc_Dias,
        //                                                                    tbHistorialIncapacidades.hinc_CentroMedico,
        //                                                                    tbHistorialIncapacidades.hinc_Doctor,
        //                                                                    tbHistorialIncapacidades.hinc_Diagnostico,
        //                                                                    tbHistorialIncapacidades.hinc_FechaInicio,
        //                                                                    tbHistorialIncapacidades.hinc_FechaFin,
        //                                                                    1,
        //                                                                    DateTime.Now);
        //            foreach (UDP_RRHH_tbHistorialIncapacidades_Insert_Result item in list)
        //            {
        //                msj = item.MensajeError + " ";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            msj = "-2";
        //            ex.Message.ToString();
        //        }
        //    }
        //    else
        //    {
        //        msj = "-3";
        //    }
        //    return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        //}


        // GET: HistorialIncapacidades/Edit/5

        public ActionResult Edit(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbHistorialIncapacidades tbHistIncapacidades = null;
            try
            {
                db = new ERP_GMEDINAEntities();
                tbHistIncapacidades = db.tbHistorialIncapacidades.Find(ID);
                if (tbHistIncapacidades == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return HttpNotFound();
            }
            Session["id"] = ID;
            var Incapacidades = new tbHistorialIncapacidades
            {
                hinc_Dias = tbHistIncapacidades.hinc_Dias,
                tbTipoIncapacidades = new tbTipoIncapacidades { ticn_Descripcion = IsNull(tbHistIncapacidades.tbTipoIncapacidades).ticn_Descripcion },
                hinc_CentroMedico = tbHistIncapacidades.hinc_CentroMedico,
                hinc_Diagnostico = tbHistIncapacidades.hinc_Diagnostico,
                hinc_FechaInicio = tbHistIncapacidades.hinc_FechaInicio,
                hinc_FechaFin = tbHistIncapacidades.hinc_FechaFin,
                tbUsuario = new tbUsuario { usu_NombreUsuario = IsNull(tbHistIncapacidades.tbUsuario).usu_NombreUsuario },
                hinc_FechaCrea = tbHistIncapacidades.hinc_FechaCrea,
                
            };

            return Json(Incapacidades, JsonRequestBehavior.AllowGet);
        }


        private tbPersonas IsNull(tbPersonas valor)
        {
            if (valor != null)
            {
                return valor;
            }
            else
            {
                return new tbPersonas { per_Identidad = "" };
            }
        }

        private tbPersonas IsNullnombre(tbPersonas valor)
        {
            if (valor != null)
            {
                return valor;
            }
            else
            {
                return new tbPersonas { per_Nombres = "" };
            }
        }

        private tbTipoIncapacidades IsNull(tbTipoIncapacidades valor)
        {
           if(valor != null)
            {
                return valor;
            }
           else
            {
                return new tbTipoIncapacidades { ticn_Descripcion = "" };
            }
        }

        private tbUsuario IsNull(tbUsuario valor)
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




        // GET: HistorialIncapacidades/Delete/5
        [HttpPost]
        public ActionResult Delete(tbHistorialIncapacidades tbHistorialIncapacidades)
        {
            string msj = "";
            if (tbHistorialIncapacidades.hinc_Id != 0 && tbHistorialIncapacidades.hinc_RazonInactivo != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    db = new ERP_GMEDINAEntities();
                    var list = db.UDP_RRHH_tbHistorialIncapacidades_Delete(tbHistorialIncapacidades.hinc_Id,"Predeterminado", Usuario.usu_Id, DateTime.Now);
                    foreach (UDP_RRHH_tbHistorialIncapacidades_Delete_Result item in list)
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
        // POST: HistorialIncapacidades/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tbHistorialIncapacidades tbHistorialIncapacidades = db.tbHistorialIncapacidades.Find(id);
        //    db.tbHistorialIncapacidades.Remove(tbHistorialIncapacidades);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}




        [HttpPost]
        public JsonResult habilitar(tbHistorialIncapacidades tbHistorialIncapacidades)
        {
            string result = "";
            var Usuario = (tbUsuario)Session["Usuario"];
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var list = db.UDP_RRHH_tbHistorialIncapacidades_Restore(tbHistorialIncapacidades.hinc_Id, 1, DateTime.Now);
                    foreach (UDP_RRHH_tbHistorialIncapacidades_Restore_Result item in list)
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
