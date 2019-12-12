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
    public class AuxilioDeCesantiasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // Obtenet: tbAuxilioDeCesantias
        public ActionResult Index()
        {
            var tbAuxilioDeCesantias = db.tbAuxilioDeCesantias.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Where(x => x.aces_Activo == true);
            return View(tbAuxilioDeCesantias.ToList());
        }

        //Metodo para refrescar la tabla(Index)
        public ActionResult GetData()
        {
            var tbAuxilioCesantia1 = db.tbAuxilioDeCesantias
                        .Select(c => new {
                            aces_IdAuxilioCesantia = c.aces_IdAuxilioCesantia,
                            aces_RangoInicioMeses = c.aces_RangoInicioMeses,
                            aces_RangoFinMeses = c.aces_RangoFinMeses,
                            aces_DiasAuxilioCesantia = c.aces_DiasAuxilioCesantia,
                            aces_UsuarioCrea = c.aces_UsuarioCrea,
                            aces_FechaCrea = c.aces_FechaCrea,
                            aces_UsuarioModifica = c.aces_UsuarioModifica,
                            aces_FechaModifica = c.aces_FechaModifica,
                            aces_Activo = c.aces_Activo
                        })
                                           .OrderByDescending(x => x.aces_FechaCrea)
                                           .Where(x => x.aces_Activo == true).ToList();
            //RETORNAR JSON AL LADO DEL CLIENTE
            return new JsonResult { Data = tbAuxilioCesantia1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //Metodo para mostrar los detalles del registro seleccionado
        public JsonResult Details(int? ID)
        {
            var tbAuxCesanJSON = from tbAuxilioDeCesantias in db.tbAuxilioDeCesantias
                                           where tbAuxilioDeCesantias.aces_Activo == true && tbAuxilioDeCesantias.aces_IdAuxilioCesantia == ID
                                           orderby tbAuxilioDeCesantias.aces_FechaCrea descending
                                           select new
                                           {
                                               tbAuxilioDeCesantias.aces_IdAuxilioCesantia,
                                               tbAuxilioDeCesantias.aces_RangoInicioMeses,
                                               tbAuxilioDeCesantias.aces_RangoFinMeses,
                                               tbAuxilioDeCesantias.aces_DiasAuxilioCesantia,
                                               UsuCrea = tbAuxilioDeCesantias.tbUsuario.usu_NombreUsuario,
                                               tbAuxilioDeCesantias.aces_FechaCrea,
                                               tbAuxilioDeCesantias.aces_UsuarioModifica,
                                               UsuModifica = tbAuxilioDeCesantias.tbUsuario1.usu_NombreUsuario,
                                               tbAuxilioDeCesantias.aces_FechaModifica
                                           };


            db.Configuration.ProxyCreationEnabled = false;
            return Json(tbAuxCesanJSON, JsonRequestBehavior.AllowGet);
        }

        //Metodo de Creacion de Nuevo registro para la tabla AuxilioCesantia
        [HttpPost]
        public ActionResult Create(tbAuxilioDeCesantias tbAuxilioDeCesantias)
        {
            #region declaracion de variables
            //Auditoria
            tbAuxilioDeCesantias.aces_UsuarioCrea = 1;
            tbAuxilioDeCesantias.aces_FechaCrea = DateTime.Now;
            tbAuxilioDeCesantias.aces_Activo = true;

            string response = String.Empty;
            IEnumerable<object> listAuxCesantias = null;
            string MensajeError = "";
            #endregion

            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listAuxCesantias = db.UDP_Plani_tbAuxilioDeCesantias_Insert(tbAuxilioDeCesantias.aces_RangoInicioMeses,
                                                                                tbAuxilioDeCesantias.aces_RangoFinMeses,
                                                                                tbAuxilioDeCesantias.aces_DiasAuxilioCesantia,
                                                                                         tbAuxilioDeCesantias.aces_UsuarioCrea,
                                                                                         tbAuxilioDeCesantias.aces_FechaCrea,tbAuxilioDeCesantias.aces_Activo);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbAuxilioDeCesantias_Insert_Result Resultado in listAuxCesantias)
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
                response = "bien";
            }
            else
            {
                //SI EL MODELO NO ES VÁLIDO, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                response = "error";
            }

            ViewBag.aces_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbAuxilioDeCesantias.aces_UsuarioCrea);
            ViewBag.aces_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbAuxilioDeCesantias.aces_UsuarioModifica);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        
        // Obtener: Registro de la tabla AuxilioDeCesantias/Edit
        public JsonResult Edit(int? ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbAuxilioDeCesantias tbAuxilioCesEditJSON = db.tbAuxilioDeCesantias.Find(ID);
            return Json(tbAuxilioCesEditJSON, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "aces_IdAuxilioCesantia,aces_RangoInicioMeses,aces_RangoFinMeses,aces_DiasAuxilioCesantia,aces_UsuarioCrea,aces_FechaCrea,aces_UsuarioModifica,aces_FechaModifica,aces_Activo")] tbAuxilioDeCesantias tbAuxilioDeCesantias)
        {
            //Declaracion de variables 
            //LLENAR DATA DE AUDITORIA
            tbAuxilioDeCesantias.aces_UsuarioModifica = 1;
            tbAuxilioDeCesantias.aces_FechaModifica = DateTime.Now;
            string response = String.Empty;
            IEnumerable<object> listAuxCes = null;
            string MensajeError = "";
           
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listAuxCes = db.UDP_Plani_tbAuxilioDeCesantias_Update(tbAuxilioDeCesantias.aces_IdAuxilioCesantia,tbAuxilioDeCesantias.aces_RangoInicioMeses,
                                                                                            tbAuxilioDeCesantias.aces_RangoFinMeses,
                                                                                            tbAuxilioDeCesantias.aces_DiasAuxilioCesantia,tbAuxilioDeCesantias.aces_UsuarioModifica,tbAuxilioDeCesantias.aces_FechaModifica);
                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbAuxilioDeCesantias_Update_Result Resultado in listAuxCes)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        //EN CASO DE OCURRIR UN ERROR, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                        ModelState.AddModelError("", "No se pudo ingresar el registro, contacte al administrador");
                        response = "error";
                    }
                }
                catch (Exception)
                {
                    //EN CASO DE CAER EN EL CATCH, IGUALAMOS LA VARIABLE "RESPONSE" A ERROR PARA VALIDARLO EN EL CLIENTE
                    ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                    response = "error";
                }
            }
            else
            {
                // SI EL MODELO NO ES CORRECTO, RETORNAR ERROR
                ModelState.AddModelError("", "No se pudo modificar el registro, contacte al administrador.");
                response = "error";
            }
            //RETORNAR MENSAJE AL LADO DEL CLIENTE
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Inactivar(int ID)
        {
            string response = String.Empty;
            IEnumerable<object> listAuxilioCesantia = null;
            string MensajeError = "";
            if (ModelState.IsValid)
            {
                try
                {
                    //EJECUTAR PROCEDIMIENTO ALMACENADO
                    listAuxilioCesantia = db.UDP_Plani_tbAuxilioDeCesantias_Delete(ID);

                    //RECORRER EL TIPO COMPLEJO DEL PROCEDIMIENTO ALMACENADO PARA EVALUAR EL RESULTADO DEL SP
                    foreach (UDP_Plani_tbAuxilioDeCesantias_Delete_Result1 Resultado in listAuxilioCesantia)
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
