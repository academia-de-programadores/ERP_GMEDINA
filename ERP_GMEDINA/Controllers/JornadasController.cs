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
    public class JornadasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        public ActionResult ChildRowData(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            //List<tbHorarios> lista = new List<tbHorarios> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    var lista = db.V_HorariosDetalles.Where(x => x.jor_Id == id && x.hor_Estado == true)
                        .Select(tabla => new { hor_HoraInicio = tabla.hor_HoraInicio, hor_HoraFin = tabla.hor_HoraFin, hor_descripcion = tabla.hor_Descripcion }).ToList();
                    return Json(lista, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                }
            }
            return Json("-2", JsonRequestBehavior.AllowGet);
        }

        public ActionResult llenarTabla()
        {
            try
            {
                //declaramos la variable de coneccion solo para recuperar los datos necesarios.
                //posteriormente es destruida.
                using (db = new ERP_GMEDINAEntities())
                {
                    var tbJornadas =
                        db.tbJornadas
                        .Select(t => new { jor_Id = t.jor_Id, jor_Descripcion = t.jor_Descripcion, jor_Estado = t.jor_Estado })
                        .Where(x => x.jor_Estado == true).ToList();
                    return Json(tbJornadas, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult llenarDropDownList()
        {
            var tbHorarios = new List<object> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    tbHorarios.Add(new { Id = 0, Descripcion = "** Seleccione una opcion **" });
                    tbHorarios.AddRange(db.tbHorarios.Select(t => new { Id = t.hor_Id, Descripcion = t.hor_Descripcion, t.hor_Estado }).Where(t => t.hor_Estado == true).ToList());
                }
                catch
                {
                    return Json("-2", 0);
                }
            }
            var result = new Dictionary<string, object>();
            result.Add("tbHorarios", tbHorarios);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Jornadas
        public ActionResult Index()
        {
            //var tbJornadas = db.tbJornadas.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            //return View(new List<tbJornadas> { });
            List<tbJornadas> tbJornadas = new List<Models.tbJornadas> { };
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            try
            {
                tbJornadas = db.tbJornadas.Where(x => x.jor_Estado == true).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).ToList();
                return View(tbJornadas);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                tbJornadas.Add(new tbJornadas { jor_Id = 0, jor_Descripcion= "Fallo la conexión" });

            }
            return View(tbJornadas);
        }

        //// GET: Jornadas/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tbJornadas tbJornadas = db.tbJornadas.Find(id);
        //    if (tbJornadas == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tbJornadas);
        //}

        // GET: Jornadas/Create

        public ActionResult Create()
        {
            ViewBag.jor_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.jor_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }        

        // POST: Jornadas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]        
        public ActionResult Create(tbJornadas tbJornadas)
        {
            string msj = "...";
            if (tbJornadas.jor_Descripcion != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbJornadas_Insert(tbJornadas.jor_Descripcion, Usuario.usu_Id, DateTime.Now);
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

        //public ActionResult ChildRowData(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
            
        //    try
        //    {
        //        var Horarios = db.tbHorarios.Where(x => x.jor_Id == id).ToList();
        //        List<tbHorarios> ListHorarios = new List<tbHorarios> { };
        //        foreach (var item in Horarios)
        //        {
        //            ListHorarios.Add(new tbHorarios {
        //                hor_Descripcion = item.hor_Descripcion,
        //                hor_HoraInicio = item.hor_HoraInicio,
        //                hor_HoraFin = item.hor_HoraFin,
        //                hor_CantidadHoras = item.hor_CantidadHoras
        //            });
        //        }

        //        return Json(ListHorarios, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //        return HttpNotFound();
        //    }
        //}

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
                tbJornadas = db.tbJornadas.Find(id);
                if (tbJornadas == null || !tbJornadas.jor_Estado)
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

        // POST: Jornadas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Edit(tbJornadas tbJornadas)
        {
            string msj = "";
            if (tbJornadas.jor_Id != 0 && tbJornadas.jor_Descripcion!= "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbJornadas_Update(id, tbJornadas.jor_Descripcion, Usuario.usu_Id, DateTime.Now);
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
                Session.Remove("id");
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }

        // GET: Jornadas/Delete/5
        public ActionResult Delete(tbJornadas tbJornadas)
        {
            string msj = "...";
            if (tbJornadas.jor_Id != 0 && tbJornadas.jor_RazonInactivo != "")
            {
                var id = (int)Session["id"];
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    var list = db.UDP_RRHH_tbJornadas_Delete(id, tbJornadas.jor_RazonInactivo, Usuario.usu_Id, DateTime.Now);
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
                Session.Remove("id");
            }
            else
            {
                msj = "-3";
            }
            return Json(msj.Substring(0, 2), JsonRequestBehavior.AllowGet);
        }
      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
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
