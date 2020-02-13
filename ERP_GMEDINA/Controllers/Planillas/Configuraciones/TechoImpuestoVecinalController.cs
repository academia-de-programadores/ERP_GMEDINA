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
    public class TechoImpuestoVecinalController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        Models.Helpers Function = new Models.Helpers();

        #region Index
        [SessionManager("TechoImpuestoVecinal/Index")]
        public ActionResult Index()
        {
            var tbTechoImpuestoVecinal = db.tbTechoImpuestoVecinal.OrderBy(t => t.timv_FechaCrea).Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Include(t => t.tbTipoDeduccion);
            return View(tbTechoImpuestoVecinal.ToList());
        }
        #endregion

        #region Get data
        [HttpGet]
        // GET: data para refrescar datatable
        public ActionResult GetData()
        {
            var otbTechoImpuestoVecinal = db.tbTechoImpuestoVecinal
                        .Select(c => new { timv_IdTechoImpuestoVecinal = c.timv_IdTechoImpuestoVecinal,
                                           mun_Nombre = c.tbMunicipio.mun_Nombre,
                                           tde_Descripcion = c.tbTipoDeduccion.tde_Descripcion, 
                                           timv_RangoInicio = c.timv_RangoInicio,
                                           timv_RangoFin = c.timv_RangoFin,
                                           timv_Rango = c.timv_Rango,
                                           timv_Impuesto = c.timv_Impuesto,
                                           timv_Activo = c.timv_Activo}).OrderByDescending(c => c.timv_IdTechoImpuestoVecinal)
                        .ToList();

            // retornar data
            return new JsonResult { Data = otbTechoImpuestoVecinal, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region GET: Details
        [SessionManager("TechoImpuestoVecinal/Details")]
        public JsonResult Details(int? ID)
        {
            // validar si se obtuvo un ID
            var response = String.Empty;
            if (ID == null)
            {

                response = "Error";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            // obtener registro con el ID recibido
            var tbTechoImpuestoVecinalJSON = from tbTechoImpuestoVecinal in db.tbTechoImpuestoVecinal
                           where tbTechoImpuestoVecinal.timv_IdTechoImpuestoVecinal == ID
                            select new
                            {
                                tbTechoImpuestoVecinal.timv_IdTechoImpuestoVecinal,
                                tbTechoImpuestoVecinal.mun_Codigo,
                                mun_Nombre = tbTechoImpuestoVecinal.tbMunicipio.mun_Nombre,
                                tbTechoImpuestoVecinal.timv_RangoInicio,
                                tbTechoImpuestoVecinal.timv_RangoFin,
                                tbTechoImpuestoVecinal.timv_Rango,
                                tbTechoImpuestoVecinal.timv_Impuesto,
                                tbTechoImpuestoVecinal.tde_IdTipoDedu,
                                tde_Descripcion = tbTechoImpuestoVecinal.tbTipoDeduccion.tde_Descripcion,

                                tbTechoImpuestoVecinal.timv_UsuarioCrea,
                                UsuCrea = tbTechoImpuestoVecinal.tbUsuario.usu_NombreUsuario,
                                tbTechoImpuestoVecinal.timv_FechaCrea,

                                tbTechoImpuestoVecinal.timv_UsuarioModifica,
                                UsuModifica = tbTechoImpuestoVecinal.tbUsuario1.usu_NombreUsuario,
                                tbTechoImpuestoVecinal.timv_FechaModifica
                            };

            // evitar referencias circulares
            db.Configuration.ProxyCreationEnabled = false;

            // retornar objeto 
            return Json(tbTechoImpuestoVecinalJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //EDITAR ESTE FALTAN VIEWBAGS
        #region GET: Create
        public ActionResult Create()
        {
            ViewBag.timv_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.timv_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.tde_IdTipoDedu = new SelectList(db.tbTipoDeduccion, "tde_IdTipoDedu", "tde_Descripcion");
            return View();
        }
        #endregion

        #region POST: Create
        [HttpPost]
        [SessionManager("TechoImpuestoVecinal/Create")]
        public ActionResult Create([Bind(Include = "mun_Codigo,tde_IdTipoDedu,timv_RangoInicio,timv_RangoFin,timv_Rango,timv_Impuesto,timv_UsuarioCrea,timv_FechaCrea")] tbTechoImpuestoVecinal tbTechoImpuestoVecinal)
        {
            // data de auditoria
            tbTechoImpuestoVecinal.timv_UsuarioCrea = Function.GetUser();
            tbTechoImpuestoVecinal.timv_FechaCrea = Function.DatetimeNow();

            // variables de resultado del proceso
            string response = "bien";
            IEnumerable<object> listTechoImpuestoVecinal = null;
            string MensajeError = "";

            // validar si el modelo es v�lid
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar procedimiento almacenado
                    listTechoImpuestoVecinal = db.UDP_Plani_tbTechoImpuestoVecinal_Insert(tbTechoImpuestoVecinal.mun_Codigo,
                                                                                          tbTechoImpuestoVecinal.tde_IdTipoDedu,
                                                                                          tbTechoImpuestoVecinal.timv_RangoInicio,
                                                                                          tbTechoImpuestoVecinal.timv_RangoFin,
                                                                                          tbTechoImpuestoVecinal.timv_Rango,
                                                                                          tbTechoImpuestoVecinal.timv_Impuesto,
                                                                                          tbTechoImpuestoVecinal.timv_UsuarioCrea,
                                                                                          tbTechoImpuestoVecinal.timv_FechaCrea);

                    // resultado del procedimiento almacenado
                    foreach (UDP_Plani_tbTechoImpuestoVecinal_Insert_Result Resultado in listTechoImpuestoVecinal)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el procedimiento almacenado fall�
                        ModelState.AddModelError("", "No se pudo ingresar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    // se gener� una excepci�n 
                    response = "error";
                }

            }
            else
            {
                // el modelo no es v�lido
                response = "error";
            }

            // retornar resultado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GET: Edit
        [SessionManager("TechoImpuestoVecinal/Edit")]
        public JsonResult Edit(int? id)
        {
            // evitar referencias circulares
            db.Configuration.ProxyCreationEnabled = false;

            // validar si se recibi� alg�n ID
            if (id == null)
            {
                string response = String.Empty;
                response = "error";
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            // encontrar el objeto con el ID recibido
            tbTechoImpuestoVecinal tbTechoImpuestoVecinalJSON = db.tbTechoImpuestoVecinal.Find(id);

            // retornar objeto
            return Json(tbTechoImpuestoVecinalJSON, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POST: Edit
        [HttpPost]
        [SessionManager("TechoImpuestoVecinal/Edit")]
        public ActionResult Edit([Bind(Include = "timv_IdTechoImpuestoVecinal, timv_Rango, mun_Codigo,tde_IdTipoDedu,timv_RangoInicio,timv_RangoFin,timv_UsuarioCrea,timv_FechaCrea")] tbTechoImpuestoVecinal tbTechoImpuestoVecinal, string Impuesto)
        {
            // variables de auditoria
            tbTechoImpuestoVecinal.timv_UsuarioModifica = Function.GetUser();
            tbTechoImpuestoVecinal.timv_FechaModifica = Function.DatetimeNow();
            tbTechoImpuestoVecinal.timv_Impuesto = (decimal)tbTechoImpuestoVecinal.timv_Impuesto;
            // variables de resultado del proceso
            IEnumerable<object> listTechoImpuestoVecinal = null;
            string MensajeError = "";
            string response = String.Empty;

            // validar si el modelo es v�lido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listTechoImpuestoVecinal = db.UDP_Plani_tbTechoImpuestoVecinal_Update(tbTechoImpuestoVecinal.timv_IdTechoImpuestoVecinal,
                                                                                          tbTechoImpuestoVecinal.mun_Codigo,
                                                                                          tbTechoImpuestoVecinal.tde_IdTipoDedu,
                                                                                          tbTechoImpuestoVecinal.timv_RangoInicio,
                                                                                          tbTechoImpuestoVecinal.timv_RangoFin,
                                                                                          tbTechoImpuestoVecinal.timv_Rango,
                                                                                          Convert.ToDecimal(Impuesto),
                                                                                          tbTechoImpuestoVecinal.timv_UsuarioModifica,
                                                                                          tbTechoImpuestoVecinal.timv_FechaModifica);

                    // obtener resultado del procedimiento almacenado
                    foreach (UDP_Plani_tbTechoImpuestoVecinal_Update_Result Resultado in listTechoImpuestoVecinal.ToList())
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el PA fall�
                        ModelState.AddModelError("", "No se pudo actualizar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception Ex)
                {
                    // se gener� una excepci�n
                    response = Ex.Message.ToString();
                }

                // el proceso fue exitoso
                response = "bien";
            }
            else
            {
                // el modelo no es v�lido
                response = "error";
            }

            // reotrnar resultado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region EditGetDDLTipoDedu
        public JsonResult EditGetDDLTipoDedu()
        {
            // obtener data
            var DDL =
            from TipoDeduc in db.tbTipoDeduccion
            join TechoImpuestoVecinal in db.tbTechoImpuestoVecinal on TipoDeduc.tde_IdTipoDedu equals TechoImpuestoVecinal.tde_IdTipoDedu into prodGroup
            select new { Id = TipoDeduc.tde_IdTipoDedu, Descripcion = TipoDeduc.tde_Descripcion };

            // retornar data
            return Json(DDL, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region EditGetDDLMuni
        public JsonResult EditGetDDLMuni()
        {
            // obtener data
            var DDL =
            from Muni in db.tbMunicipio
            join TechoImpuestoVecinal in db.tbTechoImpuestoVecinal on Muni.mun_Codigo equals TechoImpuestoVecinal.mun_Codigo into prodGroup
            select new { Id = Muni.mun_Codigo, Descripcion = Muni.mun_Nombre };

            // retornar data
            return Json(DDL, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Inactivar 
        [SessionManager("TechoImpuestoVecinal/Inactivar")]
        public ActionResult Inactivar(int id)
        {
            // variables de resultado
            IEnumerable<object> listTechoImpuestoVecinal = null;
            string MensajeError = "";
            string response = String.Empty;

            // validar si el model es v�lido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listTechoImpuestoVecinal = db.UDP_Plani_tbTechoImpuestoVecinal_Inactivar(id,
                                                                                             Function.GetUser(),
                                                                                             Function.DatetimeNow());

                    // obtener resultado del PA
                    foreach (UDP_Plani_tbTechoImpuestoVecinal_Inactivar_Result Resultado in listTechoImpuestoVecinal)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el PA fall�
                        ModelState.AddModelError("", "No se pudo actualizar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception)
                {
                    // se gener� una excepci�n
                    response = "error";
                }

                // el proceso fue exitoso
                response = "bien";
            }
            else
            {
                // el modelo es inv�lido
                response = "error";
            }

            // retornar resultado del proceso
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Activar
        [SessionManager("TechoImpuestoVecinal/Activar")]
        public ActionResult Activar(int id)
        {
            // variables de resultado
            IEnumerable<object> listTechoImpuestoVecinal = null;
            string MensajeError = "";
            string response = String.Empty;

            // validar si el modelo es v�lido
            if (ModelState.IsValid)
            {
                try
                {
                    // ejecutar PA
                    listTechoImpuestoVecinal = db.UDP_Plani_tbTechoImpuestoVecinal_Activar(id,
                                                                                           Function.GetUser(),
                                                                                           Function.DatetimeNow());

                    // obtener resultado del PA
                    foreach (UDP_Plani_tbTechoImpuestoVecinal_Activar_Result Resultado in listTechoImpuestoVecinal)
                        MensajeError = Resultado.MensajeError;

                    if (MensajeError.StartsWith("-1"))
                    {
                        // el PA fall�
                        ModelState.AddModelError("", "No se pudo actualizar el registro. Contacte al administrador.");
                        response = "error";
                    }
                }
                catch (Exception)
                {
                    // se gener� una excepci�n
                    response = "error";
                }

                // el proceso fue exitoso
                response = "bien";
            }
            else
            {
                // el modelo es inv�lido
                response = "error";
            }

            // retornar resultado del proceso
            return Json(JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Impuesto Vecinal
        //public JsonResult Calculo()
        //{

           
          
        //    //Variable para guardar el calculo del impuesto vecinal

        //    //Ejemplo

        //    decimal? TotalImpuestoVecinal = 0;



        //    //Total Salario Devengado

        //    decimal? TotalSalarioDevengado = Liquidacion.Calculo_SalarioBrutoMasAlto(item.emp_Id);



        //    //Calculo del Impuesto Vecinal

        //    //Variable para validar que entre en cierto momento en cada uno de los rangos

        //    string IV = "PrimerRango";



        //    //Variable para validar como aparece en la formula del Excel

        //    decimal? RangoInicio = 0;



        //    //Variable para validar como aparece en la formula del Excel

        //    decimal? RangoFin = 0;



        //    //Variable para validar que entre justo en el ultimo rango sin importar la cantidad de registros

        //    int iteradorA = 0;



        //    //Variable de tipo lista para traer los registros de la base de datos de menor a mayor

        //    List<tbTechoImpuestoVecinal> objDeduccionIV = db.tbTechoImpuestoVecinal.Where(x => x.timv_Activo == true)

        //                                                    .OrderBy(x => x.timv_RangoInicio)

        //                                                    .ToList();



        //    //Comienzo de la formula del calculo del Impuesto Vecinal

        //    foreach (var oIV in objDeduccionIV)

        //    {

        //        //Cada vez que pase de nuevo si sume uno mas, para luego validarlo para que entre en el ultimo registro 

        //        iteradorA = iteradorA + 1;



        //        //Si trae datos o siquiera uno entre a hacer el calculo 

        //        if (objDeduccionIV.Count() > 0)

        //        {

        //            //Entrada del Primer Rango

        //            if (IV == "PrimerRango")

        //            {

        //                TotalImpuestoVecinal = TotalImpuestoVecinal + (oIV.timv_RangoFin * oIV.timv_Impuesto) / 1000;

        //                IV = "SegundoRango";

        //            }

        //            //Entrada del segundo rango

        //            else if (IV == "SegundoRango")

        //            {

        //                RangoInicio = oIV.timv_RangoInicio;

        //                RangoFin = oIV.timv_RangoFin;

        //                if (TotalSalarioDevengado > RangoFin)

        //                {

        //                    //Formula del Excel

        //                    TotalImpuestoVecinal = TotalImpuestoVecinal + ((oIV.timv_RangoFin - (oIV.timv_RangoInicio - 1)) * (oIV.timv_Impuesto / 1000));

        //                }

        //                else

        //                {

        //                    //Formula del Excel

        //                    TotalImpuestoVecinal = TotalImpuestoVecinal + ((TotalSalarioDevengado - (oIV.timv_RangoInicio - 1)) * (oIV.timv_Impuesto / 1000));

        //                }

        //                IV = "TercerRango";

        //            }

        //            //Entrada de los siguientes rangos hasta el ultimo

        //            else if (IV == "TercerRango")

        //            {

        //                if (TotalSalarioDevengado < RangoInicio)

        //                {

        //                    TotalImpuestoVecinal = 0;

        //                }

        //                //Entrada en especifico del ultimo registro

        //                else if (objDeduccionIV.Count() == iteradorA)

        //                {

        //                    if (TotalSalarioDevengado < (oIV.timv_RangoInicio - 1))

        //                    {

        //                        TotalImpuestoVecinal = TotalImpuestoVecinal + 0;

        //                    }

        //                    else

        //                    {

        //                        //Formula del Excel

        //                        TotalImpuestoVecinal = TotalImpuestoVecinal + ((TotalSalarioDevengado - (oIV.timv_RangoInicio - 1)) * (oIV.timv_Impuesto / 1000));

        //                    }

        //                }

        //                else if (TotalSalarioDevengado > RangoFin)

        //                {

        //                    //Formula del Excel

        //                    TotalImpuestoVecinal = TotalImpuestoVecinal + ((oIV.timv_RangoFin - (oIV.timv_RangoInicio - 1)) * (oIV.timv_Impuesto / 1000));

        //                }



        //            }



        //        }

        //    }

            

        //    //Variable para colocar punto de interrupci�n si desean ver el resultado del Impuesto Vecinal de un Empleado

        //    //Verificar con el Excel, y colocar el valor de la variable TotalSalarioDevengado en el excel "Ingreso Devengado 2019"

        //    TotalImpuestoVecinal = TotalImpuestoVecinal + 0;

        //    return Json(JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}

