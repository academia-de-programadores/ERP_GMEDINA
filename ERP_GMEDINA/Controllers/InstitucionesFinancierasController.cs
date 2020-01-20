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
//using LinqToExcel;

namespace ERP_GMEDINA.Controllers
{
    public class InstitucionesFinancierasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: InstitucionesFinancieras
        public ActionResult Index()
        {
            var tbInstitucionesFinancieras = db.tbInstitucionesFinancieras.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbInstitucionesFinancieras.ToList());
        }

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
                                           //.OrderByDescending(x => x.insf_FechaCrea)
                                           /*.Where(x => x.aces_Activo == true)*/.ToList();
            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = tbInstitucionesFinancieras1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: InstitucionesFinancieras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInstitucionesFinancieras tbInstitucionesFinancieras = db.tbInstitucionesFinancieras.Find(id);
            if (tbInstitucionesFinancieras == null)
            {
                return HttpNotFound();
            }
            return View(tbInstitucionesFinancieras);
        }

        // GET: InstitucionesFinancieras/Create
        public ActionResult Create()
        {
            ViewBag.insf_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.insf_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "insf_IdInstitucionFinanciera,insf_DescInstitucionFinanc,insf_Contacto,insf_Telefono,insf_Correo,insf_UsuarioCrea,insf_FechaCrea,insf_UsuarioModifica,insf_FechaModifica,insf_Activo")] tbInstitucionesFinancieras tbInstitucionesFinancieras)
        {
            //LLENAR LA DATA DE AUDITORIA, DE NO HACERLO EL MODELO NO SERÍA VÁLIDO Y SIEMPRE CAERÍA EN EL CATCH
            tbInstitucionesFinancieras.insf_UsuarioCrea = 1;
            tbInstitucionesFinancieras.insf_FechaCrea = DateTime.Now;
            tbInstitucionesFinancieras.insf_Activo = true;
            //VARIABLE PARA ALMACENAR EL RESULTADO DEL PROCESO Y ENVIARLO AL LADO DEL CLIENTE
            string response = String.Empty;
            IEnumerable<object> listInstitucionesFinancieras = null;
            string MensajeError = "";
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listInstitucionesFinancieras = db.UDP_Plani_tbInstitucionesFinancieras_Insert(tbInstitucionesFinancieras.insf_DescInstitucionFinanc,
                                                                                            tbInstitucionesFinancieras.insf_Contacto,
                                                                                            tbInstitucionesFinancieras.insf_Telefono,
                                                                                            tbInstitucionesFinancieras.insf_Correo,
                                                                                            tbInstitucionesFinancieras.insf_UsuarioCrea,
                                                                                            tbInstitucionesFinancieras.insf_FechaCrea,
                                                                                            tbInstitucionesFinancieras.insf_Activo);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbInstitucionesFinancieras_Insert_Result Resultado in listInstitucionesFinancieras)
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
                    response = Ex.Message.ToString();
                }
                //SI LA EJECUCIÓN LLEGA A ESTE PUNTO SIGNIFICA QUE NO OCURRIÓ NINGÚN ERROR Y EL PROCESO FUE EXITOSO
                //IGUALAMOS LA VARIABLE "RESPONSE" A "BIEN" PARA VALIDARLO EN EL CLIENTE
                response = "bien";

            }
            else
            {
                //SI EL MODELO NO ES VÁLIDO, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                response = "error";
            }

            // ViewBag.insf_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbInstitucionesFinancieras.insf_UsuarioCrea);
            // ViewBag.insf_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbInstitucionesFinancieras.insf_UsuarioModifica);
            // return View(tbInstitucionesFinancieras);
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        // GET: InstitucionesFinancieras/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInstitucionesFinancieras tbInstitucionesFinancieras = db.tbInstitucionesFinancieras.Find(id);
            if (tbInstitucionesFinancieras == null)
            {
                return HttpNotFound();
            }
            ViewBag.insf_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbInstitucionesFinancieras.insf_UsuarioCrea);
            ViewBag.insf_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbInstitucionesFinancieras.insf_UsuarioModifica);
            return View(tbInstitucionesFinancieras);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "insf_IdInstitucionFinanciera,insf_DescInstitucionFinanc,insf_Contacto,insf_Telefono,insf_Correo,insf_UsuarioCrea,insf_FechaCrea,insf_UsuarioModifica,insf_FechaModifica,insf_Activo")] tbInstitucionesFinancieras tbInstitucionesFinancieras)
        {
            tbInstitucionesFinancieras.insf_UsuarioModifica = 1;
            tbInstitucionesFinancieras.insf_FechaModifica = DateTime.Now;
            IEnumerable<object> listInFs = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    listInFs = db.UDP_Plani_tbInstitucionesFinancieras_Update(
                                                        tbInstitucionesFinancieras.insf_IdInstitucionFinanciera,
                                                        tbInstitucionesFinancieras.insf_DescInstitucionFinanc,
                                                        tbInstitucionesFinancieras.insf_Contacto,
                                                        tbInstitucionesFinancieras.insf_Telefono,
                                                        tbInstitucionesFinancieras.insf_Correo,
                                                        tbInstitucionesFinancieras.insf_UsuarioModifica,
                                                        tbInstitucionesFinancieras.insf_FechaModifica,
                                                        tbInstitucionesFinancieras.insf_Activo);

                    foreach (UDP_Plani_tbInstitucionesFinancieras_Update_Result Resultado in listInFs)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo mdoficar el registro, contacte al administrador");
                        return View(tbInstitucionesFinancieras);
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
            }
            //ViewBag.insf_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbInstitucionesFinancieras.insf_UsuarioCrea);
            //ViewBag.insf_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbInstitucionesFinancieras.insf_UsuarioModifica);
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        // GET: InstitucionesFinancieras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInstitucionesFinancieras tbInstitucionesFinancieras = db.tbInstitucionesFinancieras.Find(id);
            if (tbInstitucionesFinancieras == null)
            {
                return HttpNotFound();
            }
            return View(tbInstitucionesFinancieras);
        }

        // POST: InstitucionesFinancieras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbInstitucionesFinancieras tbInstitucionesFinancieras = db.tbInstitucionesFinancieras.Find(id);
            db.tbInstitucionesFinancieras.Remove(tbInstitucionesFinancieras);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Inactivar(int ID)
        {
            string response = String.Empty;
            IEnumerable<object> listINFS = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listINFS = db.UDP_Plani_tbInstitucionesFinancieras_Inactivar(ID, 1, DateTime.Now);

                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbInstitucionesFinancieras_Inactivar_Result Resultado in listINFS)
                        MensajeError = Resultado.MensajeError;

                    //RETORNAR MENSAJE DE CONFIRMACIÓN EN CASO QUE NO HAYA CAIDO EN EL CATCH
                    response = "bien";
                }
                catch (Exception)
                {
                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    ModelState.AddModelError("", "No se logró eliminar el registro, contacte al administrador.");
                    response = "error";
                }
            }
            else
            {
                // SI EL MODELO NO ES CORRECTO, RETORNAR ERROR
                ModelState.AddModelError("", "No se logró eliminar el registro, contacte al administrador.");
                response = "error";
            }
            //RETORNAR MENSAJE AL LADO DEL CLIENTE
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Activar(int id)
        {
            IEnumerable<object> listINFS = null;
            string MensajeError = "";
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    listINFS = db.UDP_Plani_tbInstitucionesFinancieras_Activar(id, 1, DateTime.Now);

                    foreach (UDP_Plani_tbInstitucionesFinancieras_Activar1_Result Resultado in listINFS)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo activar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception)
                {
                    response = "error";
                }
                response = "bien";
            }
            else
            {
                //Se devuelve un mensaje de error en caso de que el modelo no sea válido
                response = "error";
            }

            return Json(JsonRequestBehavior.AllowGet);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

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

        [HttpPost]
        public ActionResult _CargaDocumento(HttpPostedFileBase archivoexcel, string cboINFS,string cboIdDeduccion)
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
                    List<int> idsDeducciones = db.tbCatalogoDeDeducciones.Where(x => x.cde_Activo == true).Select(x=> x.cde_IdDeducciones).ToList();
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
                                string identidad = sl.GetCellValueAsString(iRow, 1);
                                decimal monto = sl.GetCellValueAsDecimal(iRow, 2);
                                string comentario = sl.GetCellValueAsString(iRow, 3);


                                var oMiExcel = new tbDeduccionInstitucionFinanciera();

                                var IdEmpleado = (from P in db.tbPersonas
                                             join E in db.tbEmpleados on P.per_Id equals E.per_Id
                                             where
                                             P.per_Identidad == identidad
                                             select new
                                             {
                                                 empleadoID = E.emp_Id,
                                             }).FirstOrDefault();
                                var sql = (from infs in db.tbDeduccionInstitucionFinanciera select infs.deif_IdDeduccionInstFinanciera).Max();
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
                                    oMiExcel.deif_UsuarioCrea = 1;
                                    oMiExcel.deif_FechaCrea = DateTime.Now;
                                    oMiExcel.deif_UsuarioModifica = null;
                                    oMiExcel.deif_FechaModifica = null;
                                    oMiExcel.deif_Activo = true;
                                    oMiExcel.deif_Pagado = false;
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
                catch (Exception)
                {
                    response="error";
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

    }
}
