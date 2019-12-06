using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using System.Collections.Generic;

namespace ERP_GMEDINA.Controllers
{
    public class AdelantoSueldoController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        // GET: AdelantoSueldo
        public ActionResult Index()
        {
            var tbAdelantoSueldo = db.tbAdelantoSueldo.Where(t => t.adsu_Activo == true).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbEmpleados).OrderBy(t => t.adsu_FechaCrea).OrderByDescending(t => t.adsu_FechaCrea);
            return View(tbAdelantoSueldo.ToList());
        }

        public ActionResult GetData()
        {
            //SI SE LLEGA A DAR PROBLEMAS DE "REFERENCIAS CIRCULARES", OBTENER LA DATA DE ESTA FORMA
            //SELECCIONANDO UNO POR UNO LOS CAMPOS QUE NECESITAREMOS
            //DE LO CONTRARIO, HACERLO DE LA FORMA CONVENCIONAL (EJEMPLO: db.tbCatalogoDeDeducciones.ToList(); )
            var tbAdelantoSueldo = db.tbAdelantoSueldo
                        .Select(c => new { adsu_IdAdelantoSueldo = c.adsu_IdAdelantoSueldo,
                                           adsu_RazonAdelanto = c.adsu_RazonAdelanto,
                                           adsu_Monto = c.adsu_Monto,
                                           adsu_FechaAdelanto = c.adsu_FechaAdelanto,
                                           adsu_Deducido = c.adsu_Deducido,
                                           adsu_UsuarioCrea = c.tbUsuario.usu_NombreUsuario,
                                           adsu_FechaCrea = c.adsu_FechaCrea,
                                           adsu_UsuarioModifica = c.tbUsuario1.usu_NombreUsuario,
                                           adsu_FechaModifica = c.adsu_FechaModifica,
                                           adsu_Activo = c.adsu_Activo,
                                           empleadoNombre = c.tbEmpleados.tbPersonas.per_Nombres + " " + c.tbEmpleados.tbPersonas.per_Nombres })
                        .OrderByDescending(x => x.adsu_FechaCrea)
                        .ToList().Where(p => p.adsu_Activo == true);
            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = tbAdelantoSueldo, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult EmpleadoGetDDL()
        {
            //LA CONSULTA DEVUELVE LOS REGISTROS QUE NO TENGAN ADELANTOS ACTIVOS
            var DDL =
            from Personas in db.tbPersonas
            join Empleados in db.tbEmpleados on Personas.per_Id equals Empleados.per_Id
            where !(from Adelanto in db.tbAdelantoSueldo where Adelanto.adsu_Deducido == false
                      select Adelanto.emp_Id).Contains(Empleados.emp_Id)
            select new
            {
                Id = Empleados.emp_Id,
                Descripcion = Personas.per_Nombres
                + " " + Personas.per_Apellidos
                + "-" + Personas.per_Identidad
            };
            //RETORNAR LA DATA EN FORMATO JSON AL CLIENTE 
            return Json(DDL, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "emp_Id, adsu_FechaAdelanto, adsu_RazonAdelanto, adsu_Monto")] tbAdelantoSueldo tbAdelantoSueldo)
        {
            //Para llenar los campos de auditoría
            tbAdelantoSueldo.adsu_FechaAdelanto = DateTime.Now;
            tbAdelantoSueldo.adsu_UsuarioCrea = 1;
            tbAdelantoSueldo.adsu_FechaCrea = DateTime.Now;

            //Variable para enviarla al lado del Cliente
            string Response = String.Empty;
            IEnumerable<object> listAdelantoSueldo = null;
            string MensajeError = "";
            
            if (ModelState.IsValid)
            {
                try
                {
                    //Ejecutar Procedimiento Almacenado
                    listAdelantoSueldo = db.UDP_Plani_tbAdelantoSueldo_Insert(tbAdelantoSueldo.emp_Id,
                                                                              tbAdelantoSueldo.adsu_FechaAdelanto,
                                                                              tbAdelantoSueldo.adsu_RazonAdelanto,
                                                                              tbAdelantoSueldo.adsu_Monto,
                                                                              tbAdelantoSueldo.adsu_UsuarioCrea,
                                                                              tbAdelantoSueldo.adsu_FechaCrea);

                    //El tipo complejo del Procedimiento Almacenado
                    foreach (UDP_Plani_tbAdelantoSueldo_Insert_Result Resultado in listAdelantoSueldo)
                    {
                        MensajeError = Resultado.MensajeError;
                    }

                    if (MensajeError.StartsWith("-1"))
                    {
                        //En caso de un error igualamos la variable Response a "Error" para validar en el lado del Cliente
                        ModelState.AddModelError("", "No se pudo Registrar. Contacte al Administrador!");
                        Response = "Error";
                    }
                }
                catch (Exception Ex)
                {
                    Response = Ex.Message.ToString();
                }
                //Si llega aqui significa que todo salio correctamente. Solo igualamos Response a "Exito" para validar en el lado del Cliente
                Response = "Exito";
                return RedirectToAction("Index");

            }
            else
            {
                //Si el modelo no es valido. Igualamos Response a "Error" para validar en el lado del Cliente
                Response = "Error";
            }
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        //DETALLES
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var AdelantoSueldo_Detalles = db.tbAdelantoSueldo
                .Select(c => new {
                    adsu_IdAdelantoSueldo = c.adsu_IdAdelantoSueldo,
                    adsu_RazonAdelanto = c.adsu_RazonAdelanto,
                    adsu_UsuarioCrea = c.adsu_UsuarioCrea,
                    NombreUsuarioCrea = c.tbUsuario.usu_NombreUsuario,
                    adsu_FechaCrea = c.adsu_FechaCrea,
                    adsu_UsuarioModifica = c.adsu_UsuarioModifica,
                    NombreUsuarioModifica = c.tbUsuario1.usu_NombreUsuario,
                    adsu_FechaModificadifica = c.adsu_FechaModifica,
                    adsu_Activo = c.adsu_Activo,
                    adsu_Deducido = c.adsu_Deducido,
                    adsu_FechaAdelanto = c.adsu_FechaAdelanto
                }).Where(x => x.adsu_IdAdelantoSueldo == id)
                .OrderByDescending(x => x.adsu_FechaCrea).ToList();
            if (AdelantoSueldo_Detalles == null)
            {
                return HttpNotFound();
            }
            return View(AdelantoSueldo_Detalles);
        }
    }
}