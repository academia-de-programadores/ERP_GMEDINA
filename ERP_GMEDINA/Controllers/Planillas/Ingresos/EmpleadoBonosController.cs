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
    public class EmpleadoBonosController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        Models.Helpers Function = new Models.Helpers();

        // GET: EmpleadoBonos
        [SessionManager("EmpleadoBonos/Index")]
        public ActionResult Index()
        {
            var tbEmpleadoBonos = db.tbEmpleadoBonos.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCatalogoDeIngresos).Include(t => t.tbEmpleados).Include(t => t.tbEmpleados.tbPersonas).OrderByDescending(x => x.cb_FechaCrea);
            //throw new Exception();
            //tbEmpleadoBonos = null;
            //.Where(p => p.cb_Activo == true)
            return View(tbEmpleadoBonos.ToList());
        }

        public ActionResult GetData()
        {
            var tbEmpleadoBonos = db.tbEmpleadoBonos
                        .Select(c => new { cb_Id = c.cb_Id, emp_Id = c.emp_Id, per_Nombres = c.tbEmpleados.tbPersonas.per_Nombres, per_Apellidos = c.tbEmpleados.tbPersonas.per_Apellidos, cin_IdIngreso = c.cin_IdIngreso, cin_DescripcionIngreso = c.tbCatalogoDeIngresos.cin_DescripcionIngreso, cb_Monto = c.cb_Monto, cb_FechaRegistro = c.cb_FechaRegistro, cb_Pagado = c.cb_Pagado, NombreUsarioCrea = c.tbUsuario.usu_NombreUsuario, cb_UsuarioCrea = c.cb_UsuarioCrea, cb_FechaCrea = c.cb_FechaCrea, usuarioModifica = c.tbUsuario1.usu_NombreUsuario, cb_UsuarioModifica = c.cb_UsuarioModifica, cb_FechaModifica = c.cb_FechaModifica, cb_Activo = c.cb_Activo })
                        .OrderByDescending(x => x.cb_FechaCrea)
                        .ToList();
            //.Where(p => p.cb_Activo == true);
            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = tbEmpleadoBonos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public string EditGetDDLEmpleado()
        {
            return Helpers.General.ObtenerEmpleados();
        }

        public JsonResult EditGetDDLIngreso()
        {
            //OBTENER LA DATA QUE NECESITAMOS, HACIENDOLO DE ESTA FORMA SE EVITA LA EXCEPCION POR "REFERENCIAS CIRCULARES"
            var DDL =
            from CatIngreso in db.tbCatalogoDeIngresos
            where CatIngreso.cin_Activo == true
            //where CatIngreso.cin_DescripcionIngreso == "Bonos" || CatIngreso.cin_IdIngreso == 5
            //join EmpBonos in db.tbEmpleadoBonos on CatIngreso.cin_IdIngreso equals EmpBonos.cin_IdIngreso
            select new
            {
                Id = CatIngreso.cin_IdIngreso,
                Descripcion = CatIngreso.cin_DescripcionIngreso
            };
            //RETORNAR LA DATA EN FORMATO JSON AL CLIENTE 
            return Json(DDL, JsonRequestBehavior.AllowGet);
        }

        // GET: EmpleadoBonos/Details/5
        [SessionManager("EmpleadoBonos/Details")]
        public JsonResult Details(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbEmpleadoBonos tbEmpleadoBonosJSON = db.tbEmpleadoBonos.Find(id);
            // probando
            var tbEmpleadoBonos = db.tbEmpleadoBonos
                        .Select(c => new { cb_Id = c.cb_Id, emp_Id = c.emp_Id, per_Nombres = c.tbEmpleados.tbPersonas.per_Nombres, per_Apellidos = c.tbEmpleados.tbPersonas.per_Apellidos, cin_IdIngreso = c.cin_IdIngreso, cin_DescripcionIngreso = c.tbCatalogoDeIngresos.cin_DescripcionIngreso, cb_Monto = c.cb_Monto, cb_FechaRegistro = c.cb_FechaRegistro, cb_Pagado = c.cb_Pagado, NombreUsarioCrea = c.tbUsuario.usu_NombreUsuario, cb_UsuarioCrea = c.cb_UsuarioCrea, cb_FechaCrea = c.cb_FechaCrea, usuarioModifica = c.tbUsuario1.usu_NombreUsuario, cb_UsuarioModifica = c.cb_UsuarioModifica, cb_FechaModifica = c.cb_FechaModifica, cb_Activo = c.cb_Activo })
                        .Where(p => p.cb_Id == id).FirstOrDefault();
            return Json(tbEmpleadoBonos, JsonRequestBehavior.AllowGet);

        }

        // GET: EmpleadoBonos/Create
        [SessionManager("EmpleadoBonos/Create")]
        [HttpPost]
        public ActionResult Create([Bind(Include = "emp_Id, cin_IdIngreso, cb_Monto")] tbEmpleadoBonos tbEmpleadoBonos)
        {
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbEmpleadoBonos.cb_FechaRegistro = Function.DatetimeNow();
            tbEmpleadoBonos.cb_Pagado = false;
            tbEmpleadoBonos.cb_UsuarioCrea = Function.GetUser();
            tbEmpleadoBonos.cb_FechaCrea = Function.DatetimeNow();
            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
            string response = "bien";
            IEnumerable<object> listEmpleadoBonos = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listEmpleadoBonos = db.UDP_Plani_tbEmpleadoBonos_Insert(tbEmpleadoBonos.emp_Id,
                                                                                            tbEmpleadoBonos.cin_IdIngreso,
                                                                                            tbEmpleadoBonos.cb_Monto,
                                                                                            tbEmpleadoBonos.cb_FechaRegistro,
                                                                                            tbEmpleadoBonos.cb_Pagado,
                                                                                            tbEmpleadoBonos.cb_UsuarioCrea,
                                                                                            tbEmpleadoBonos.cb_FechaCrea);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbEmpleadoBonos_Insert_Result Resultado in listEmpleadoBonos)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        response = "error";
                    }

                }
                catch (Exception Ex)
                {
                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    Ex.Message.ToString();
                    response = "error";
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                //SI EL MODELO NO ES VÁLIDO, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                response = "error";
            }
            //RETORNAMOS LA VARIABLE RESPONSE AL CLIENTE PARA EVALUARLA
            //ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion", tbEmpleadoBonos.tde_IdTipoDedu);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // GET: EmpleadoBonos/Edit/5
        public JsonResult Edit(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbEmpleadoBonos tbEmpleadoBonosJSON = db.tbEmpleadoBonos.Find(ID);
            return Json(tbEmpleadoBonosJSON, JsonRequestBehavior.AllowGet);
        }

        // POST: EmpleadoBonos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [SessionManager("EmpleadoBonos/Edit")]
        [HttpPost]
        public ActionResult edit([Bind(Include = "cb_Id, emp_Id, cin_IdIngreso, cb_Monto")] tbEmpleadoBonos tbEmpleadoBonos)
        {
            tbEmpleadoBonos.cb_UsuarioModifica = Function.GetUser();
            tbEmpleadoBonos.cb_FechaModifica = Function.DatetimeNow();
            DateTime FechaRegistro = db.tbEmpleadoBonos.Where(x => x.cb_Id == tbEmpleadoBonos.cb_Id).Select(c => c.cb_FechaRegistro).FirstOrDefault();
            tbEmpleadoBonos.cb_FechaRegistro = (FechaRegistro == null) ? DateTime.Now : FechaRegistro;
            IEnumerable<object> listEmpleadoBonos = null;
            string MensajeError = "";
            string response = "bien";

            if (ModelState.IsValid)
            {
                try
                {
                    //ejecutar procedimiento almacenado
                    listEmpleadoBonos = db.UDP_Plani_tbEmpleadoBonos_Update(tbEmpleadoBonos.cb_Id,
                                                                                            tbEmpleadoBonos.emp_Id,
                                                                                            tbEmpleadoBonos.cin_IdIngreso,
                                                                                            tbEmpleadoBonos.cb_Monto,
                                                                                            tbEmpleadoBonos.cb_FechaRegistro,
                                                                                            tbEmpleadoBonos.cb_Pagado,
                                                                                            tbEmpleadoBonos.cb_UsuarioModifica,
                                                                                            tbEmpleadoBonos.cb_FechaModifica);
                    //recorrer el tipo complejo del procedimiento almacenado para evaluar el resultado del sp
                    foreach (UDP_Plani_tbEmpleadoBonos_Update_Result resultado in listEmpleadoBonos)
                        MensajeError = resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        //en caso de ocurrir un error, igualamos la variable "response" a error para validarlo en el cliente
                        ModelState.AddModelError("", "no se pudo ingresar el registro, contacte al administrador");
                        response = "error";
                    }

                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    //en caso de caer en el catch, igualamos la variable "response" a error para validarlo en el cliente
                    ModelState.AddModelError("", "no se pudo modificar el registro, contacte al administrador.");
                    response = "error";
                }
            }
            else
            {
                // si el modelo no es correcto, retornar error
                ModelState.AddModelError("", "no se pudo modificar el registro, contacte al administrador.");
                response = "error";
            }

            //retornar mensaje al lado del cliente
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // GET: EmpleadoBonos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleadoBonos tbEmpleadoBonos = db.tbEmpleadoBonos.Find(id);
            if (tbEmpleadoBonos == null)
            {
                return HttpNotFound();
            }
            return View(tbEmpleadoBonos);
        }

        // POST: EmpleadoBonos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbEmpleadoBonos tbEmpleadoBonos = db.tbEmpleadoBonos.Find(id);
            db.tbEmpleadoBonos.Remove(tbEmpleadoBonos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [SessionManager("EmpleadoBonos/Inactivar")]
        [HttpPost]
        public ActionResult Inactivar(int? Id)
        {
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = "bien";
            IEnumerable<object> listEmpleadoBonos = null;
            string MensajeError = "";
            //Validar que el Id no sea null
            if (Id == null)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            //LLENAR DATA DE AUDITORIA
            tbEmpleadoBonos tbEmpleadoBonos = new tbEmpleadoBonos();
            tbEmpleadoBonos.cb_Id = (int)Id;
            tbEmpleadoBonos.cb_UsuarioModifica = Function.GetUser();
            tbEmpleadoBonos.cb_FechaModifica = Function.DatetimeNow();
            try
            {
                //EJECUTAR PROCEDIMIENTO ALMACENADO
                listEmpleadoBonos = db.UDP_Plani_tbEmpleadoBonos_Inactivar(tbEmpleadoBonos.cb_Id,
                                                                            tbEmpleadoBonos.cb_UsuarioModifica,
                                                                            tbEmpleadoBonos.cb_FechaModifica);

                //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                foreach (UDP_Plani_tbEmpleadoBonos_Inactivar_Result Resultado in listEmpleadoBonos)
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

        [SessionManager("EmpleadoBonos/Activar")]
        [HttpPost]
        public ActionResult Activar(int? Id)
        {
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = "bien";
            IEnumerable<object> listEmpleadoBonos = null;
            string MensajeError = "";
            //Validar que el Id no sea null
            if (Id == null)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            //LLENAR DATA DE AUDITORIA
            tbEmpleadoBonos tbEmpleadoBonos = new tbEmpleadoBonos();
            tbEmpleadoBonos.cb_Id = (int)Id;
            tbEmpleadoBonos.cb_UsuarioModifica = Function.GetUser();
            tbEmpleadoBonos.cb_FechaModifica = Function.DatetimeNow();
            try
            {
                //EJECUTAR PROCEDIMIENTO ALMACENADO
                listEmpleadoBonos = db.UDP_Plani_tbEmpleadoBonos_Activar(tbEmpleadoBonos.cb_Id,
                                                                            tbEmpleadoBonos.cb_UsuarioModifica,
                                                                            tbEmpleadoBonos.cb_FechaModifica);

                //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                foreach (UDP_Plani_tbEmpleadoBonos_Activar_Result Resultado in listEmpleadoBonos)
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
