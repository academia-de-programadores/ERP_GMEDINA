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
    public class ISRController : Controller
    {

        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: ISR
        public ActionResult Index()
        {
            var tbISR = db.tbISR.OrderByDescending(t => t.isr_FechaCrea).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbTipoDeduccion);
            return View(tbISR.ToList());
        }
        [HttpGet]
        // GET: OBTENER LA DATA Y ENVIARLA A LA VISTA EN FORMATO JSON
        public ActionResult GetData()
        {
            var otbISR = db.tbISR
                        .Select(c => new { tde_Descripcion = c.tbTipoDeduccion.tde_Descripcion, isr_Id = c.isr_Id, isr_RangoInicial = c.isr_RangoInicial, isr_RangoFinal = c.isr_RangoFinal, isr_Porcentaje = c.isr_Porcentaje, isr_Activo = c.isr_Activo, isr_FechaCrea = c.isr_FechaCrea }).OrderByDescending(c => c.isr_FechaCrea)
                        .ToList();

            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = otbISR, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult Details(int? ID)
        {
            var tbISRJSON = from tbISR in db.tbISR
                            where tbISR.isr_Activo == true && tbISR.isr_Id == ID
                            select new
                            {
                                tbISR.isr_Id,
                                tbISR.isr_RangoInicial,
                                tbISR.isr_RangoFinal,
                                tbISR.isr_Porcentaje,
                                tbISR.tde_IdTipoDedu,
                                tde_Descripcion =  tbISR.tbTipoDeduccion.tde_Descripcion,

                                tbISR.isr_UsuarioCrea,
                                UsuCrea = tbISR.tbUsuario.usu_NombreUsuario,
                                tbISR.isr_FechaCrea,

                                tbISR.isr_UsuarioModifica,
                                UsuModifica = tbISR.tbUsuario1.usu_NombreUsuario,
                                tbISR.isr_FechaModifica
                            };

            db.Configuration.ProxyCreationEnabled = false;

            return Json(tbISRJSON, JsonRequestBehavior.AllowGet);
        }

        // GET: ISR/Create
        public ActionResult Create()
        {
            ViewBag.isr_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.isr_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion");
            return View();
        }

        // POST: ISR/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "isr_Id,isr_RangoInicial,isr_RangoFinal,isr_Porcentaje,tde_IdTipoDedu,isr_UsuarioCrea,isr_FechaCrea,isr_UsuarioModifica,isr_FechaModifica,isr_Activo")] tbISR tbISR)
        {
            #region declaracion de variables 
            //Llenar los datos de auditoría, de no hacerlo el modelo será inválido y entrará directamente al Catch
            tbISR.isr_UsuarioCrea = 1;
            tbISR.isr_FechaCrea = DateTime.Now;
            //Variable para almacenar el resultado del proceso y enviarlo al lado del cliente
            string response = String.Empty;
            IEnumerable<object> listISR = null;
            string MensajeError = "";
            #endregion

            if (ModelState.IsValid)
            {
                try
                {
                    //Ejecutar el procedimiento almacenado
                    listISR = db.UDP_Plani_tbISR_Insert(tbISR.isr_RangoInicial,
                                                        tbISR.isr_RangoFinal,
                                                        tbISR.isr_Porcentaje,
                                                        tbISR.tde_IdTipoDedu,
                                                        tbISR.isr_UsuarioCrea,
                                                        tbISR.isr_FechaCrea);

                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbISR_Insert_Result Resultado in listISR)
                        MensajeError = Resultado.MensajeError;

                    response = "bien";
                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo ingresar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    response = Ex.Message.ToString();
                }

            }
            else
            {
                //SI EL MODELO NO ES VÁLIDO, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                response = "error";
            }
            //RETORNAMOS LA VARIABLE RESPONSE AL CLIENTE PARA EVALUARLA
            ViewBag.isr_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbISR.isr_UsuarioCrea);
            ViewBag.isr_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbISR.isr_UsuarioModifica);
            ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion", tbISR.tde_IdTipoDedu);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // GET: ISR/Edit/5
        public JsonResult Edit(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbISR tbISRJSON = db.tbISR.Find(id);
            return Json(tbISRJSON, JsonRequestBehavior.AllowGet);
        }

        // POST: ISR/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "isr_Id,isr_RangoInicial,isr_RangoFinal,isr_Porcentaje,tde_IdTipoDedu,isr_UsuarioCrea,isr_FechaCrea,isr_UsuarioModifica,isr_FechaModifica,isr_Activo")] tbISR tbISR)
        {
            tbISR.isr_UsuarioModifica = 1;
            tbISR.isr_FechaModifica = DateTime.Now;
            IEnumerable<object> listISR = null;
            string MensajeError = "";
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            //VALIDAR SI EL MODELO ES VÁLIDO
            if (ModelState.IsValid)
            {
                try
                {
                    //Ejecución del procedimiento almacenado
                    listISR = db.UDP_Plani_tbISR_Update(tbISR.isr_Id,
                                                        tbISR.isr_RangoInicial,
                                                        tbISR.isr_RangoFinal,
                                                        tbISR.isr_Porcentaje,
                                                        tbISR.tde_IdTipoDedu, //ID del tipo de la deducción
                                                        1,
                                                        DateTime.Now);

                    foreach (UDP_Plani_tbISR_Update_Result Resultado in listISR.ToList())
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo actualizar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    response = Ex.Message.ToString();
                }
                response = "bien";
            }
            else
            {
                //Se devuelve un mensaje de error en caso de que el modelo no sea válido
                response = "error";
            }
            ViewBag.isr_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbISR.isr_UsuarioCrea);
            ViewBag.isr_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbISR.isr_UsuarioModifica);
            ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion", tbISR.tde_IdTipoDedu);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditGetDDL()
        {
            //OBTENER LA DATA QUE NECESITAMOS, HACIENDOLO DE ESTA FORMA SE EVITA LA EXCEPCIÓN POR "REFERENCIAS CIRCULARES"
            var DDL =
            from TipoDeduc in db.tbTipoDeduccion
            join ISR in db.tbISR on TipoDeduc.tde_IdTipoDedu equals ISR.tde_IdTipoDedu into prodGroup
            select new { Id = TipoDeduc.tde_IdTipoDedu, Descripcion = TipoDeduc.tde_Descripcion };
            //RETORNAR LA DATA EN FORMATO JSON AL CLIENTE 
            return Json(DDL, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Inactivar(int id)
        {
            IEnumerable<object> listISR = null;
            string MensajeError = "";
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    listISR = db.UDP_Plani_tbISR_Inactivar(id,
                                                            1,
                                                            DateTime.Now);

                    foreach (UDP_Plani_tbISR_Inactivar_Result Resultado in listISR)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo actualizar el registro. Contacte al administrador.");
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

        public ActionResult Activar(int id)
        {
            IEnumerable<object> listISR = null;
            string MensajeError = "";
            //VARIABLE DONDE SE ALMACENARA EL RESULTADO DEL PROCESO
            string response = String.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    listISR = db.UDP_Plani_tbISR_Activar(id,
                                                          1,
                                                         DateTime.Now);

                    foreach (UDP_Plani_tbISR_Activar_Result Resultado in listISR)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo actualizar el registro. Contacte al administrador.");
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
    }
}
