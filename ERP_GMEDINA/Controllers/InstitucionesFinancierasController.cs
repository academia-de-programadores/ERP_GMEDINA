using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.IO;
using SpreadsheetLight;
using ERP_GMEDINA.Attribute;
//using LinqToExcel;

namespace ERP_GMEDINA.Controllers
{
    public class InstitucionesFinancierasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        private ERP_GMEDINA.Models.Helpers Function = new ERP_GMEDINA.Models.Helpers();



        #region GET: INDEX
        [SessionManager("InstitucionesFinancieras/Index")]
        public ActionResult Index()
        {
            var tbInstitucionesFinancieras = db.tbInstitucionesFinancieras.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbInstitucionesFinancieras.ToList());
        }
        #endregion

        #region GET: GETDATA

        // data para refrescar el datatable
        public ActionResult GetData()
        {
            var tbInstitucionesFinancieras1 = db.tbInstitucionesFinancieras
                        .Select(c => new {
                            insf_IdInstitucionFinanciera = c.insf_IdInstitucionFinanciera,
                            insf_DescInstitucionFinanc = c.insf_DescInstitucionFinanc,
                            insf_Contacto = c.insf_Contacto,
                            insf_Telefono = c.insf_Telefono,
                            insf_Correo = c.insf_Correo,
                            insf_UsuarioCrea = c.insf_UsuarioCrea,
                            insf_FechaCrea = c.insf_FechaCrea,
                            insf_UsuarioModifica = c.insf_UsuarioModifica,
                            insf_FechaModifica = c.insf_FechaModifica,
                            insf_Activo = c.insf_Activo
                        })
                        .ToList();

            // retornar data obtenida 
            return new JsonResult { Data = tbInstitucionesFinancieras1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region POST: CREATE
        [SessionManager("InstitucionesFinancieras/Create")]
        [HttpPost]
        public JsonResult Create([Bind(Include = "insf_DescInstitucionFinanc,insf_Contacto,insf_Telefono,insf_Correo,insf_UsuarioCrea,insf_FechaCrea")] tbInstitucionesFinancieras tbInstitucionesFinancieras)
        {
            // data de auditoria
            //tbInstitucionesFinancieras.insf_UsuarioCrea = 1;
            //tbInstitucionesFinancieras.insf_FechaCrea = DateTime.Now;
            tbInstitucionesFinancieras.insf_Activo = true;

            // variables de resultados
            string response = "bien";
            IEnumerable<object> listInstitucionesFinancieras = null;
            string MensajeError = "";

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar procedimiento almacenado
                    listInstitucionesFinancieras = db.UDP_Plani_tbInstitucionesFinancieras_Insert(tbInstitucionesFinancieras.insf_DescInstitucionFinanc,
                                                                                                  tbInstitucionesFinancieras.insf_Contacto,
                                                                                                  tbInstitucionesFinancieras.insf_Telefono,
                                                                                                  tbInstitucionesFinancieras.insf_Correo,
                                                                                                  Function.GetUser(),
                                                                                                  Function.DatetimeNow(),
                                                                                                  tbInstitucionesFinancieras.insf_Activo);

                    // obtener resultado del procedimiento almacendo
                    foreach (UDP_Plani_tbInstitucionesFinancieras_Insert_Result Resultado in listInstitucionesFinancieras)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el procedimiento almacenado falló
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        response = "error";
                    }

                }
                catch (Exception Ex)
                {
                    // se generó una excepción
                    response = "error";
                }
            }
            else
            {
                // el modelo no es válido
                response = "error";
            }

            // retornar resultado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GET: EDIT
        [SessionManager("InstitucionesFinancieras/Edit")]
        public JsonResult Edit(int? id)
        {
            // validar si se recibió algún ID
            if (id == null)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

            // obtener objeto con el ID recibido
            var tbInstitucionesFinancieras = db.tbInstitucionesFinancieras
                                                .Where(d => d.insf_IdInstitucionFinanciera == id)
                                                .Select(c => new {
                                                    insf_IdInstitucionFinanciera = c.insf_IdInstitucionFinanciera,
                                                    insf_Descripcion = c.insf_DescInstitucionFinanc,
                                                    insf_Contacto = c.insf_Contacto,
                                                    insf_Telefono = c.insf_Telefono,
                                                    insf_Correo = c.insf_Correo,
                                                    insf_UsuarioCrea = c.insf_UsuarioCrea,
                                                    insf_FechaCrea = c.insf_FechaCrea,
                                                    insf_UsuarioCrea_Nombres = c.tbUsuario.usu_NombreUsuario,
                                                    insf_UsuarioModifica = c.insf_UsuarioModifica,
                                                    insf_FechaModifica = c.insf_FechaModifica,
                                                    insf_UsuarioModifica_Nombres = c.tbUsuario1.usu_NombreUsuario,
                                                    insf_Activo = c.insf_Activo
                                                })
                                                .ToList();

            // si no existe un registro con ese ID, retornar error
            if (tbInstitucionesFinancieras == null)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }

            // retornar objeto
            return Json(tbInstitucionesFinancieras, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: EDIT
        [SessionManager("InstitucionesFinancieras/Edit")]
        [HttpPost]
        public JsonResult Edit([Bind(Include = "insf_IdInstitucionFinanciera,insf_DescInstitucionFinanc,insf_Contacto,insf_Telefono,insf_Correo")] tbInstitucionesFinancieras tbInstitucionesFinancieras)
        {
            // variables de auditoria
            //tbInstitucionesFinancieras.insf_UsuarioModifica = 1;
            //tbInstitucionesFinancieras.insf_FechaModifica = DateTime.Now;
            tbInstitucionesFinancieras.insf_Activo = true;

            // variables de resultado
            IEnumerable<object> listInFs = null;
            string response = "bien";
            string MensajeError = "";

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar procedimiento almacenado
                    listInFs = db.UDP_Plani_tbInstitucionesFinancieras_Update(
                                                        tbInstitucionesFinancieras.insf_IdInstitucionFinanciera,
                                                        tbInstitucionesFinancieras.insf_DescInstitucionFinanc,
                                                        tbInstitucionesFinancieras.insf_Contacto,
                                                        tbInstitucionesFinancieras.insf_Telefono,
                                                        tbInstitucionesFinancieras.insf_Correo,
                                                        Function.GetUser(),
                                                        Function.DatetimeNow(),
                                                        tbInstitucionesFinancieras.insf_Activo);

                    // resultado del procedimiento almacenado
                    foreach (UDP_Plani_tbInstitucionesFinancieras_Update_Result Resultado in listInFs)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el PA falló
                        ModelState.AddModelError("", "No se pudo mdoficar el registro, contacte al administrador");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    // se generó una excepción
                    response = "error";
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                // el modelo no es válido
                response = "error";
            }

            // retornar resultado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GET: DETAILS
        [SessionManager("InstitucionesFinancieras/Details")]
        public JsonResult Details(int? id)
        {
            // validar si se recibió el ID
            if (id == null)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

            // obtener objeto con el ID recibido
            var tbInstitucionesFinancieras = db.tbInstitucionesFinancieras
                                                .Where(d => d.insf_IdInstitucionFinanciera == id)
                                                .Select(c => new {
                                                    insf_IdInstitucionFinanciera = c.insf_IdInstitucionFinanciera,
                                                    insf_Descripcion = c.insf_DescInstitucionFinanc,
                                                    insf_Contacto = c.insf_Contacto,
                                                    insf_Telefono = c.insf_Telefono,
                                                    insf_Correo = c.insf_Correo,
                                                    insf_UsuarioCrea = c.insf_UsuarioCrea,
                                                    insf_FechaCrea = c.insf_FechaCrea,
                                                    insf_UsuarioCrea_Nombres = c.tbUsuario.usu_NombreUsuario,
                                                    insf_UsuarioModifica = c.insf_UsuarioModifica,
                                                    insf_FechaModifica = c.insf_FechaModifica,
                                                    insf_UsuarioModifica_Nombres = c.tbUsuario1.usu_NombreUsuario,
                                                    insf_Activo = c.insf_Activo
                                                }).ToList();

            // retornar objeto
            return Json(tbInstitucionesFinancieras, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: INACTIVAR
        [SessionManager("InstitucionesFinancieras/Inactivar")]
        [HttpPost]
        public ActionResult Inactivar(int ID)
        {
            // variables de resultado
            string response = String.Empty;
            IEnumerable<object> listINFS = null;
            string MensajeError = "";

            // validar si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar procedimiento almacenad
                    listINFS = db.UDP_Plani_tbInstitucionesFinancieras_Inactivar(ID, Function.GetUser(),Function.DatetimeNow());

                    // resultado del procedimiento almacenado
                    foreach (UDP_Plani_tbInstitucionesFinancieras_Inactivar_Result Resultado in listINFS)
                        MensajeError = Resultado.MensajeError;

                    // el proceso fue exitoso
                    response = "bien";
                }
                catch (Exception)
                {
                    // se generó una excepción
                    ModelState.AddModelError("", "No se logró eliminar el registro, contacte al administrador.");
                    response = "error";
                }
            }
            else
            {
                // el modelo no es válido
                ModelState.AddModelError("", "No se logró eliminar el registro, contacte al administrador.");
                response = "error";
            }

            // retornar resultado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: ACTIVAR
        [SessionManager("InstitucionesFinancieras/Activar")]
        public ActionResult Activar(int id)
        {
            // variables de resultado
            IEnumerable<object> listINFS = null;
            string MensajeError = "";
            string response = "bien";

            // validar si el modelo es valido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar procedimiento almacenado
                    listINFS = db.UDP_Plani_tbInstitucionesFinancieras_Activar(id, Function.GetUser(), Function.DatetimeNow());

                    // validar respuesta del procedimiento almacenado
                    foreach (UDP_Plani_tbInstitucionesFinancieras_Activar_Result Resultado in listINFS)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el procedimiento almacenado falló
                        ModelState.AddModelError("", "No se pudo activar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception)
                {
                    // se generó una excepción
                    response = "error";
                }
            }
            else
            {
                // el modelo no es válido
                response = "error";
            }

            // retornar resultado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region DISPOSE
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region GET: CARGAR DOCUMENTO
        [SessionManager("InstitucionesFinancieras/CargaDocumento")]
        public ActionResult CargaDocumento()
        {
            try
            {
                List<tbCatalogoDeDeducciones> OCatalogoDeducciones = db.tbCatalogoDeDeducciones.Where(x => x.cde_Activo == true).ToList();

                ViewBag.Deducciones = OCatalogoDeducciones;
                var listaINFS = from INFS in db.tbInstitucionesFinancieras
                                select new
                                {
                                    idinfs = INFS.insf_IdInstitucionFinanciera,
                                    descinfs = INFS.insf_DescInstitucionFinanc
                                };
                var list = new SelectList(listaINFS, "idinfs", "descinfs");
                ViewData["INFS"] = list;
            }
            catch (Exception ex)
            {
                return Content("Error de Conexion" + ex.ToString());
            }

            return View("CargaDocumento");
        }
        #endregion

        #region POST: CARGAR DOCUMENTO
        [SessionManager("InstitucionesFinancieras/_CargaDocumento")]
        [HttpPost]
        public ActionResult _CargaDocumento(HttpPostedFileBase archivoexcel, string cboINFS, string cboIdDeduccion)
        {
            string response = String.Empty;
            string MensajeError = "";

            //Verificacion del objetto recibido (archivo excel), si esta vacio retornara un error, de lo contrario continuara con el proceso.
            if (archivoexcel != null && archivoexcel.ContentLength > 0)
            {
                //Guardado del archivo en el servidor
                string path = Path.Combine(Server.MapPath("~/Content/PlanillasInstitucionesFinancieras"),
                                      Path.GetFileName(archivoexcel.FileName));
                archivoexcel.SaveAs(path);

                try
                {
                    List<int> idsDeducciones = db.tbCatalogoDeDeducciones.Where(x => x.cde_Activo == true).Select(x => x.cde_IdDeducciones).ToList();
                    List<int> idsINFS = db.tbInstitucionesFinancieras.Where(x => x.insf_Activo == true).Select(x => x.insf_IdInstitucionFinanciera).ToList();

                    int idCatDeduc = Convert.ToInt16(cboIdDeduccion);
                    int IdInsF = Convert.ToInt16(cboINFS);

                    if (!idsDeducciones.Contains(idCatDeduc))
                    {
                        response = "error";

                    }
                    else if (!idsDeducciones.Contains(IdInsF))
                    {
                        response = "error";
                    }
                    else
                    {
                        //Deserealizacion del archivo excel cargado al sistema.
                        SLDocument sl = new SLDocument(path);

                        //Recorremos el archivo para obtener la informaicon.
                        using (var db = new ERP_GMEDINAEntities())
                        {

                            int iRow = 2;
                            while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
                            {
                                bool DeducirISR = false;
                                string identidad = sl.GetCellValueAsString(iRow, 1);
                                decimal monto = sl.GetCellValueAsDecimal(iRow, 2);
                                string comentario = sl.GetCellValueAsString(iRow, 3);
                                string deducirisr = sl.GetCellValueAsString(iRow, 6);

                                if (deducirisr == "Si")
                                {
                                    DeducirISR = true;
                                }
                                else if (deducirisr == "No" || deducirisr == "")
                                {
                                    DeducirISR = false;
                                }
                                else
                                {
                                    DeducirISR = false;
                                }

                                var oMiExcel = new tbDeduccionInstitucionFinanciera();

                                var IdEmpleado = (from P in db.tbPersonas
                                                  join E in db.tbEmpleados on P.per_Id equals E.per_Id
                                                  where
                                                  P.per_Identidad == identidad
                                                  select new
                                                  {
                                                      empleadoID = E.emp_Id,
                                                  }).FirstOrDefault();
                                var sql = (from infs in db.tbDeduccionInstitucionFinanciera select infs.deif_IdDeduccionInstFinanciera).DefaultIfEmpty(0).Max();
                                var iddeducfin = sql + 1;

                                //Validamos si encontro empleados que correspondan a los numeros de identidad proporcionados, de lo contrario mostrara error.
                                if (IdEmpleado != null)
                                {
                                    var IdEmple = IdEmpleado.empleadoID;
                                    oMiExcel.deif_IdDeduccionInstFinanciera = iddeducfin;
                                    oMiExcel.emp_Id = IdEmple;
                                    oMiExcel.insf_IdInstitucionFinanciera = IdInsF;
                                    oMiExcel.deif_Monto = monto;
                                    oMiExcel.deif_Comentarios = comentario;
                                    oMiExcel.cde_IdDeducciones = idCatDeduc;
                                    oMiExcel.deif_UsuarioCrea = Function.GetUser();
                                    oMiExcel.deif_FechaCrea = Function.DatetimeNow();
                                    oMiExcel.deif_UsuarioModifica = null;
                                    oMiExcel.deif_FechaModifica = null;
                                    oMiExcel.deif_Activo = true;
                                    oMiExcel.deif_Pagado = false;
                                    oMiExcel.deif_DeducirISR = DeducirISR;
                                    db.tbDeduccionInstitucionFinanciera.Add(oMiExcel);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    return RedirectToAction("CargaDocumento");
                                }
                                iRow++;
                            }
                        }
                        response = "bien";
                    }
                }
                catch (Exception Ex)
                {
                    response = "error";
                }
            }
            else
            {
                MensajeError = "Error: Debe seleccionar un archivo para poder cargarlo al sistema.";
            }

            List<tbCatalogoDeDeducciones> OCatalogoDeducciones = db.tbCatalogoDeDeducciones.Where(x => x.cde_Activo == true).ToList();
            ViewBag.Deducciones = OCatalogoDeducciones;
            var listaINFS = from INFS in db.tbInstitucionesFinancieras
                            select new
                            {
                                idinfs = INFS.insf_IdInstitucionFinanciera,
                                descinfs = INFS.insf_DescInstitucionFinanc
                            };
            var list = new SelectList(listaINFS, "idinfs", "descinfs");
            ViewData["INFS"] = list;

            if (response == "error")
            {
                ViewBag.MensajeError = "error";
            }
            else
            {
                ViewBag.MensajeError = "bien";
            }

            return View("CargaDocumento");
        }
        #endregion

    }
}
