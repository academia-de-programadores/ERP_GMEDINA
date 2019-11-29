
//----------------------------------------------
//----------ACTUALIZAR AL MODELO ACTUAL---------
//----------------------------------------------

//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using ERP_GMEDINA.Models;

//namespace ERP_GMEDINA.Controllers
//{
//    public class EmpleadoComisionesController : Controller
//    {
//        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

//        // GET: EmpleadoComisiones
//        public ActionResult Index()
//        {
//            var tbEmpleadoComisiones = db.tbEmpleadoComisiones.Where(d => d.cc_Activo == true).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbCatalogoDeIngresos).Include(t => t.tbEmpleados).Include(t => t.tbEmpleados.tbPersonas).OrderByDescending(x => x.cc_FechaCrea);
//            return View(tbEmpleadoComisiones.ToList());
//        }

//        public ActionResult GetData()
//        {
//            //SI SE LLEGA A DAR PROBLEMAS DE "REFERENCIAS CIRCULARES", OBTENER LA DATA DE ESTA FORMA
//            //SELECCIONANDO UNO POR UNO LOS CAMPOS QUE NECESITAREMOS
//            //DE LO CONTRARIO, HACERLO DE LA FORMA CONVENCIONAL (EJEMPLO: db.tbCatalogoDeDeducciones.ToList(); )
//            var tbEmpleadoComisiones = db.tbEmpleadoComisiones
//                        .Select(c => new { cc_Id = c.cc_Id, emp_Id = c.emp_Id, per_Nombres = c.tbEmpleados.tbPersonas.per_Nombres, per_Apellidos = c.tbEmpleados.tbPersonas.per_Apellidos, cin_IdIngreso = c.cin_IdIngreso, cin_DescripcionIngreso = c.tbCatalogoDeIngresos.cin_DescripcionIngreso, cc_Monto = c.cc_Monto, cc_FechaRegistro = c.cc_FechaRegistro, cc_Pagado = c.cc_Pagado, cc_UsuarioCrea = c.cc_UsuarioCrea, cc_FechaCrea = c.cc_FechaCrea, cc_UsuarioModifica = c.cc_UsuarioModifica, cc_FechaModifica = c.cc_FechaModifica,cc_Activo = c.cc_Activo})
//                        .Where(c => c.cc_Activo == true)
//                        .OrderByDescending(x => x.cc_FechaCrea)
//                        .ToList();
//            //RETORNAR JSON AL LADO DEL CLIENTE
//            return new JsonResult { Data = tbEmpleadoComisiones, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
//        }

//        public JsonResult EditGetDDLEmpleado()
//        {
//            //OBTENER LA DATA QUE NECESITAMOS, HACIENDOLO DE ESTA FORMA SE EVITA LA EXCEPCION POR "REFERENCIAS CIRCULARES"
//            var DDL =
//            from Personas in db.tbPersonas
//            join Empleados in db.tbEmpleados on Personas.per_Id equals Empleados.per_Id
//            //join EmpBonos in db.tbEmpleadoBonos on Empleados.emp_Id equals EmpBonos.emp_Id 
//            select new
//            {
//                Id = Empleados.emp_Id,
//                Descripcion = Personas.per_Nombres + " " + Personas.per_Apellidos
//                //+ " " + Personas.per_Apellidos
//            };
//            //RETORNAR LA DATA EN FORMATO JSON AL CLIENTE 
//            return Json(DDL, JsonRequestBehavior.AllowGet);
//        }

//        public JsonResult EditGetDDLIngreso()
//        {
//            //OBTENER LA DATA QUE NECESITAMOS, HACIENDOLO DE ESTA FORMA SE EVITA LA EXCEPCION POR "REFERENCIAS CIRCULARES"
//            var DDL =
//            from CatIngreso in  db.tbCatalogoDeIngresos.Where(x => x.cin_DescripcionIngreso == "Comisiones")
//                //join EmpBonos in db.tbEmpleadoBonos on CatIngreso.cin_IdIngreso equals EmpBonos.cin_IdIngreso
//            select new
//            {
//                Id = CatIngreso.cin_IdIngreso,
//                Descripcion = CatIngreso.cin_DescripcionIngreso
//            };
//            //RETORNAR LA DATA EN FORMATO JSON AL CLIENTE 
//            return Json(DDL, JsonRequestBehavior.AllowGet);
//        }

//        // GET: EmpleadoBonos/Details/5
//        public JsonResult Details(int? ID)
//        {
//            var tbEmpleadoComisionesJSON = from tbEmplComisiones in db.tbEmpleadoComisiones
//                                               //join tbUsuCrea in db.tbUsuario on tbCatIngreso.cin_UsuarioCrea equals tbUsuCrea.usu_Id
//                                               //join tbUsuModi in db.tbUsuario on tbCatIngreso.cin_UsuarioModifica equals tbUsuModi.usu_Id
//                                           where tbEmplComisiones.cc_Activo == true && tbEmplComisiones.cc_Id == ID
//                                           select new
//                                           {
//                                               tbEmplComisiones.cc_Id,
//                                               tbEmplComisiones.emp_Id,
//                                               NombreEmpleado = tbEmplComisiones.tbEmpleados.tbPersonas.per_Nombres,
//                                               ApellidosEmpleado = tbEmplComisiones.tbEmpleados.tbPersonas.per_Apellidos,
//                                               tbEmplComisiones.cin_IdIngreso,
//                                               Ingreso =tbEmplComisiones.tbCatalogoDeIngresos.cin_DescripcionIngreso,
//                                               tbEmplComisiones.cc_Monto,
//                                               tbEmplComisiones.cc_FechaRegistro,
//                                               tbEmplComisiones.cc_Pagado,
//                                               tbEmplComisiones.cc_Activo,
//                                               tbEmplComisiones.cc_UsuarioCrea,
//                                               UsuCrea = tbEmplComisiones.tbUsuario.usu_NombreUsuario,
//                                               tbEmplComisiones.cc_FechaCrea,
//                                               tbEmplComisiones.cc_UsuarioModifica,
//                                               UsuModifica = tbEmplComisiones.tbUsuario1.usu_NombreUsuario,
//                                               tbEmplComisiones.cc_FechaModifica
//                                           };




//            db.Configuration.ProxyCreationEnabled = false;
//            //tbCatalogoDeIngresos tbCatalogoDeIngresosJSON = db.tbCatalogoDeIngresos.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Find(ID);
//            return Json(tbEmpleadoComisionesJSON, JsonRequestBehavior.AllowGet);
//        }



//        // GET: EmpleadoBonos/Create
//        [HttpPost]
//        public ActionResult Create([Bind(Include = "emp_Id, cin_IdIngreso, cc_Monto, cc_FechaRegistro, cc_Pagado, cc_UsuarioCrea, cb_FechaCrea")] tbEmpleadoComisiones tbEmpleadoComisiones)
//        {
//            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
//            tbEmpleadoComisiones.cc_FechaRegistro = DateTime.Now;
//            tbEmpleadoComisiones.cc_UsuarioCrea = 1;
//            tbEmpleadoComisiones.cc_FechaCrea = DateTime.Now;

//            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
//            string response = String.Empty;
//            IEnumerable<object> listEmpleadoComisiones = null;
//            string MensajeError = "";
//            //VALIDAR SI EL MODELO ES VÁLIDO
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    //EJECUTAR PROCEDIMIENTO ALMACENADO
//                    listEmpleadoComisiones = db.UDP_Plani_EmpleadoComisiones_Insert(tbEmpleadoComisiones.emp_Id,
//                                                                                            tbEmpleadoComisiones.cin_IdIngreso,
//                                                                                            tbEmpleadoComisiones.cc_Monto,
//                                                                                            tbEmpleadoComisiones.cc_FechaRegistro,
//                                                                                            tbEmpleadoComisiones.cc_Pagado,
//                                                                                            tbEmpleadoComisiones.cc_UsuarioCrea,
//                                                                                            tbEmpleadoComisiones.cc_FechaCrea);
//                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
//                    foreach (UDP_Plani_EmpleadoComisiones_Insert_Result Resultado in listEmpleadoComisiones)
//                        MensajeError = Resultado.MensajeError;

//                    if (MensajeError.StartsWith("-1"))
//                    {
//                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
//                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
//                        response = "error";
//                    }

//                }
//                catch (Exception Ex)
//                {
//                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
//                    response = Ex.Message.ToString();
//                }
//                //SI LA EJECUCIÓN LLEGA A ESTE PUNTO SIGNIFICA QUE NO OCURRIÓ NINGÚN ERROR Y EL PROCESO FUE EXITOSO
//                //IGUALAMOS LA VARIABLE "RESPONSE" A "BIEN" PARA VALIDARLO EN EL CLIENTE
//                response = "bien";
//            }
//            else
//            {
//                //SI EL MODELO NO ES VÁLIDO, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
//                response = "error";
//            }
//            ViewBag.cc_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpleadoComisiones.cc_UsuarioCrea);
//            ViewBag.cc_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbEmpleadoComisiones.cc_UsuarioModifica);
//            ViewBag.cc_IdIngreso = new SelectList(db.tbCatalogoDeIngresos, "cin_IdIngreso", "cin_DescripcionIngreso", tbEmpleadoComisiones.cin_IdIngreso);
//            return Json(response, JsonRequestBehavior.AllowGet);
//        }


//        // GET: EmpleadoBonos/Edit/5
//        public JsonResult Edit(int? ID)
//        {
//            db.Configuration.ProxyCreationEnabled = false;
//            tbEmpleadoComisiones tbEmpleadoComisionesJSON = db.tbEmpleadoComisiones.Find(ID);
//            return Json(tbEmpleadoComisionesJSON, JsonRequestBehavior.AllowGet);
//        }


//        // POST: EmpleadoBonos/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "cc_Id, emp_Id, cin_IdIngreso, cc_Monto, cc_FechaRegistro, cc_Pagado, cc_UsuarioModifica, cc_FechaModifica")] tbEmpleadoComisiones tbEmpleadoComisiones)
//        {
//            tbEmpleadoComisiones.cc_UsuarioCrea = 1;
//            tbEmpleadoComisiones.cc_FechaCrea = DateTime.Now;
//            tbEmpleadoComisiones.cc_UsuarioModifica = 1;
//            tbEmpleadoComisiones.cc_FechaModifica = DateTime.Now;

//            IEnumerable<object> listEmpleadoComisiones = null;

//            string MensajeError = "";

//            string response = String.Empty;


//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    //EJECUTAR PROCEDIMIENTO ALMACENADO
//                    listEmpleadoComisiones = db.UDP_Plani_EmpleadoComisiones_Update(tbEmpleadoComisiones.cc_Id,
//                                                                                            tbEmpleadoComisiones.emp_Id,
//                                                                                            tbEmpleadoComisiones.cin_IdIngreso,
//                                                                                            tbEmpleadoComisiones.cc_Monto,
//                                                                                            tbEmpleadoComisiones.cc_FechaRegistro,
//                                                                                            tbEmpleadoComisiones.cc_Pagado,
//                                                                                            tbEmpleadoComisiones.cc_UsuarioModifica,
//                                                                                            tbEmpleadoComisiones.cc_FechaModifica
//                                                                                            );
//                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
//                    foreach (UDP_Plani_EmpleadoComisiones_Update_Result Resultado in listEmpleadoComisiones)
//                        MensajeError = Resultado.MensajeError;

//                    if (MensajeError.StartsWith("-1"))
//                    {
//                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
//                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
//                        response = "error";
//                    }

//                }
//                catch (Exception Ex)
//                {
//                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
//                    ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
//                    response = "error";
//                }
//            }
//            else
//            {
//                // SI EL MODELO NO ES CORRECTO, RETORNAR ERROR
//                ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
//                response = "error";
//            }
//            //ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion", tbCatalogoDeDeducciones.tde_IdTipoDedu);

//            //RETORNAR MENSAJE AL LADO DEL CLIENTE
//            return Json(response, JsonRequestBehavior.AllowGet);
//        }


//        // GET: EmpleadoComisiones/Inactivar

//        public JsonResult Inactivar(int? ID)
//        {
//            db.Configuration.ProxyCreationEnabled = false;
//            tbEmpleadoComisiones tbEmpleadoComisionesJSON = db.tbEmpleadoComisiones.Find(ID);
//            return Json(tbEmpleadoComisionesJSON, JsonRequestBehavior.AllowGet);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Inactivar(int id)
//        {

//            string response = String.Empty;

//            IEnumerable<object> listEmpleadoComisiones = null;
//            string MensajeError = "";
//            //VALIDAR SI EL MODELO ES VÁLIDO
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    //EJECUTAR PROCEDIMIENTO ALMACENADO
//                    listEmpleadoComisiones = db.UDP_Plani_EmpleadoComisiones_Inactivar(id,
//                                                                                            1,
//                                                                                            DateTime.Now);
//                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
//                    foreach (UDP_Plani_EmpleadoComisiones_Inactivar_Result Resultado in listEmpleadoComisiones)
//                        MensajeError = Resultado.MensajeError;

//                    if (MensajeError.StartsWith("-1"))
//                    {
//                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
//                        ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador");
//                        response = "error";
//                    }

//                }
//                catch (Exception Ex)
//                {
//                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
//                    response = Ex.Message.ToString();
//                }
//                //SI LA EJECUCIÓN LLEGA A ESTE PUNTO SIGNIFICA QUE NO OCURRIÓ NINGÚN ERROR Y EL PROCESO FUE EXITOSO
//                //IGUALAMOS LA VARIABLE "RESPONSE" A "BIEN" PARA VALIDARLO EN EL CLIENTE
//                response = "bien";
//            }
//            else
//            {
//                // SI EL MODELO NO ES CORRECTO, RETORNAR ERROR
//                ModelState.AddModelError("", "No se pudo inactivar el registro, contacte al administrador.");
//                response = "error";
//            }
//            //ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion", tbCatalogoDeDeducciones.tde_IdTipoDedu);

//            //RETORNAR MENSAJE AL LADO DEL CLIENTE
//            return Json(response, JsonRequestBehavior.AllowGet);
//        }



//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}

