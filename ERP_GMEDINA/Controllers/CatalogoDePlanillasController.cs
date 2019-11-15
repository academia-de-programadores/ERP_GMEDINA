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
    public class CatalogoDePlanillasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: CatalogoDePlanillas
        public ActionResult Index()
        {
            var tbCatalogoDePlanillas = db.tbCatalogoDePlanillas.Include(t => t.tbUsuario).Include(t => t.tbUsuario1);
            return View(tbCatalogoDePlanillas.ToList());
        }

        // GET: CatalogoDePlanillas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCatalogoDePlanillas tbCatalogoDePlanillas = db.tbCatalogoDePlanillas.Find(id);
            if (tbCatalogoDePlanillas == null)
            {
                return HttpNotFound();
            }
            return View(tbCatalogoDePlanillas);
        }

        // GET: CatalogoDePlanillas/Create
        public ActionResult Create()
        {
            var CatalogoIngresos = (from catalogoIngresos in db.tbCatalogoDeIngresos
                                    where catalogoIngresos.cin_Activo == true
                                    select new CatalogoDeIngresosDeduccionesViewModel
                                    {
                                          id = catalogoIngresos.cin_IdIngreso,
                                          descripcion = catalogoIngresos.cin_DescripcionIngreso
                                    }).ToList();

            var CatalogoDeducciones = (from catalogoDeducciones in db.tbCatalogoDeDeducciones
                                       where catalogoDeducciones.cde_Activo == true
                                    select new CatalogoDeIngresosDeduccionesViewModel
                                    {
                                        id = catalogoDeducciones.cde_IdDeducciones,
                                        descripcion = catalogoDeducciones.cde_DescripcionDeduccion
                                    }).ToList();
            ViewBag.CatalogoIngresos = CatalogoIngresos;
            ViewBag.CatalogoDeducciones = CatalogoDeducciones;
            return View();
        }

        // POST: CatalogoDePlanillas/Create
        [HttpPost]
        public ActionResult Create(string[] catalogoDePlanillas, int[] catalogoIngresos, int[] catalogoDeducciones, int? idPlanillaEdit)
        {
            //La variabele "response" puede contener la palabra: "bien", o "error".
            string response = "";

            /*
             * Verificar que el catalogo de planillas, catalogo de ingresos, y catalogo de deducciones
             * tengan algún valor, si no no dejara insertar en la planillas, la validación también
             * se hace en el lado del cliente
             */
            if(catalogoDePlanillas.Length > 1 && catalogoIngresos.Length > 0 && catalogoDeducciones.Length > 0)
            {
                #region Declaración de Variables
                string MensajeError = "", /*MensajeError es la variable que va a contener el id de la tabla
                                           * maestro, también puede contener el mensaje de error, que el retorno
                                           * es -1 y seguido del mensaje de error detectado en el procedimiento
                                           * almacenado en SQL Server
                                           */

                    MensajeErrorCatalogoDeIngresos = "", /*En esta variable se guardarán todos los id's de los ingresos
                                                          * también con esta variable se detecta si hubo un error.
                                                          */

                    MensajeErrorCatalogoDeDeducciones = ""; /*En esta variable se guardarán todos los id's de las deducciones
                                                             * también con esta variable se detecta si hubo un error.
                                                             */

                //Estas variables se utilizaan para saber el resultado que manda el procedimiento almacenado al insertar
                IEnumerable<object> listCatalogoDePlanillas = null;
                IEnumerable<object> listCatalogoDeIngresos = null;
                IEnumerable<object> listCatalogoDeDeducciones = null;
                int cpla_UsuarioCreaModifica = 1; //TODO: Editar el Usuario Crea del catalogo de planillas
                DateTime cpla_FechaCreaModifica =  DateTime.Now; // Asignarle la fecha actual a la variable cpla_FechaCrea
                string cpla_DescripcionPlanilla;
                int cpla_FrecuenciaEnDias;

                #endregion

                try
                {
                    #region Insertar o editar en el catalogo de Planillas

                    cpla_DescripcionPlanilla = catalogoDePlanillas[0]; //El índice 0 del array catalogoDePlanillas contiene la descripción de la planilla
                    cpla_FrecuenciaEnDias = int.Parse(catalogoDePlanillas[1]); //El índice 1 contiene la frecuencia en días de la planilla

                        /* Guardar el catalogo de Planillas en la base de datos
                         * El procedimiento lo igualo a la variable listCatalogoDePlanillas para luego recorrerla
                         * y ver que datos me trae del procedimiento almacenado
                         */
                        listCatalogoDePlanillas = InsertarPlanilla(cpla_UsuarioCreaModifica, cpla_FechaCreaModifica, cpla_DescripcionPlanilla, cpla_FrecuenciaEnDias);

                        /*Recorrer la variable listCatalogoDePlanillas para obtener el id del campo registro ingresado actualmente.
                         *O obtener un -1 y el mensaje de error enviado desde el procedimieto almacenado
                         */
                        foreach (UDP_Plani_tbCatalogoDePlanillas_Insert_Result Resultado in listCatalogoDePlanillas)
                            MensajeError = Resultado.MensajeError;

                    #endregion

                    //Si hubo un error al insertar en el catalogo de planillas, al inicio de la cadena de texto habrá un -1, seguido del mensaje de error
                    if (MensajeError.StartsWith("-1"))
                    {
                        response = "error";
                    }
                    else
                    {
                        response = "bien";
                        //Verificar que se haya insertado en el catalogo de Planillas
                        if (listCatalogoDePlanillas != null)
                        {
                            #region Insertar en el catalogo de Ingresos

                            // Recorrer el array catalogoIngresos, el catalogoIngresos trae todos los id de los ingresos
                            InsertarCatalogoIngresos(catalogoIngresos, ref response, MensajeError, ref MensajeErrorCatalogoDeIngresos, ref listCatalogoDeIngresos, cpla_UsuarioCreaModifica);
                            #endregion

                            #region Insertar en el catalogo de Deducciones

                            // Recorrer el array catalogoDeducciones
                            InsertarCatalogoDeducciones(catalogoDeducciones, ref response, MensajeError, MensajeErrorCatalogoDeIngresos, ref MensajeErrorCatalogoDeDeducciones, ref listCatalogoDeDeducciones);
                            #endregion
                        }
                    }
                }
                catch (Exception)
                {
                    response = "error";
                }
            }
            if(response == "bien")
                return RedirectToAction("Index");
            else
                return Json(response, JsonRequestBehavior.AllowGet);
        }

        //Insertar una nueva planilla
        private IEnumerable<object> InsertarPlanilla(int cpla_UsuarioCreaModifica, DateTime cpla_FechaCreaModifica, string cpla_DescripcionPlanilla, int cpla_FrecuenciaEnDias)
        {
            return db.UDP_Plani_tbCatalogoDePlanillas_Insert(cpla_DescripcionPlanilla,
                cpla_FrecuenciaEnDias,
                cpla_UsuarioCreaModifica,
                cpla_FechaCreaModifica);
        }

        //Editar una planilla
        private string EditarPlanilla(int? idPlanillaEdit, ref string MensajeError, int cpla_UsuarioCreaModifica, DateTime cpla_FechaCreaModifica, string cpla_DescripcionPlanilla, int cpla_FrecuenciaEnDias)
        {
            IEnumerable<object> listCatalogoDePlanillas = db.UDP_Plani_tbCatalogoDePlanillas_Update(idPlanillaEdit,
                cpla_DescripcionPlanilla,
                cpla_FrecuenciaEnDias,
                cpla_UsuarioCreaModifica,
                cpla_FechaCreaModifica);
            foreach (UDP_Plani_tbCatalogoDePlanillas_Update_Result Resultado in listCatalogoDePlanillas)
                MensajeError = Resultado.MensajeError;
            return MensajeError;
        }

        //Insertar en el catalogo de deducciones
        private void InsertarCatalogoDeducciones(int[] catalogoDeducciones, ref string response, string MensajeError, string MensajeErrorCatalogoDeIngresos, ref string MensajeErrorCatalogoDeDeducciones, ref IEnumerable<object> listCatalogoDeDeducciones)
        {
            foreach (int i in catalogoDeducciones)
            {
                int cde_IdDeducciones = i; // Asignarle el id de la deduccion
                int tpdd_UsuarioCrea = 1; // TODO: Editar el Usuario Crea del Catalogo de Deducciones
                DateTime tpdd_FechaCrea = DateTime.Now; // Asignar la fecha actual

                //Guardar en el catalogo de deducciones
                listCatalogoDeDeducciones = db.UDP_tbTipoPlanillaDetalleDeduccion_Insert(cde_IdDeducciones,
                    int.Parse(MensajeError),
                    tpdd_UsuarioCrea,
                    tpdd_FechaCrea);

                //Ver si se insertaron o no los datos
                foreach (UDP_tbTipoPlanillaDetalleDeduccion_Insert_Result Resultado in listCatalogoDeDeducciones)
                    MensajeErrorCatalogoDeDeducciones = Resultado.MensajeError;

                // Asignarle a la variable response el error si hubo error
                if (MensajeErrorCatalogoDeIngresos.StartsWith("-1"))
                    response = "error";
                else
                    response = "bien";
            }
        }

        //Insertar en el catalogo de ingresos
        private void InsertarCatalogoIngresos(int[] catalogoIngresos, ref string response, string MensajeError, ref string MensajeErrorCatalogoDeIngresos, ref IEnumerable<object> listCatalogoDeIngresos, int cpla_UsuarioCreaModifica)
        {
            foreach (int i in catalogoIngresos)
            {
                int cin_IdIngreso = i; // Asignarle el id del ingreso 
                                       //int tpdi_UsuarioCrea = 1; // TODO: Editar el Usuario Crea del Catalogo de Ingresos 
                DateTime tpdi_FechaCrea = DateTime.Now; //Asignarle la fecha y hora actual a la variable tpdi_FechaCrea

                // Insertar en el Catalogo de Ingresos
                listCatalogoDeIngresos = db.UDP_tbTipoPlanillaDetalleIngreso_Insert(cin_IdIngreso,
                    int.Parse(MensajeError),
                    cpla_UsuarioCreaModifica,
                    tpdi_FechaCrea);

                //Recorrer el IEnumerable listCatalogoDeIngresos para saber si trae el id del registro insertado, o un mensaje de error
                foreach (UDP_tbTipoPlanillaDetalleIngreso_Insert_Result Resultado in listCatalogoDeIngresos)
                    MensajeErrorCatalogoDeIngresos = Resultado.MensajeError;

                // Si hubo un error entonces se le asigna a la variable response el texto "error", caso contrario "bien"
                if (MensajeErrorCatalogoDeIngresos.StartsWith("-1"))
                    response = "error";
                else
                    response = "bien";
            }
        }

        // GET: CatalogoDePlanillas/Edit/5
        public ActionResult Edit(int? id)
        {
            string response = "";
            //Buscar en la 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Obtener los datos del catalogo de planillas filtrando por el id
            IEnumerable<object> listCatalogoDeDeducciones = null;
            IEnumerable<object> listCatalogoDeIngresos = null;

            List<CatalogoDeIngresosDeduccionesViewModel> catalogoIngresos = new List<CatalogoDeIngresosDeduccionesViewModel>();
            tbCatalogoDePlanillas tbCatalogoDePlanillas = db.tbCatalogoDePlanillas.Find(id);
            List<CatalogoDeIngresosDeduccionesViewModel> listaCatalogoIngresos = new List<CatalogoDeIngresosDeduccionesViewModel>();
            List<CatalogoDeIngresosDeduccionesViewModel> listaCatalogoDeducciones = new List<CatalogoDeIngresosDeduccionesViewModel>();

            try
            {
                listCatalogoDeIngresos = db.UDP_Plani_CatalogoDeIngresosEdit_Select(id);
                foreach (UDP_Plani_CatalogoDeIngresosEdit_Select_Result result in listCatalogoDeIngresos)
                {
                    CatalogoDeIngresosDeduccionesViewModel catalogoIngreso = new CatalogoDeIngresosDeduccionesViewModel();
                    catalogoIngreso.id = result.cin_IdIngreso;
                    catalogoIngreso.descripcion = result.cin_DescripcionIngreso;
                    if (result.@checked == 1)
                        catalogoIngreso.check = true;
                    else
                        catalogoIngreso.check = false;
                    listaCatalogoIngresos.Add(catalogoIngreso);
                }

                listCatalogoDeDeducciones = db.UDP_Plani_CatalogoDeduccionesEdit_Select(id);

                foreach (UDP_Plani_CatalogoDeduccionesEdit_Select_Result result in listCatalogoDeDeducciones)
                {
                    CatalogoDeIngresosDeduccionesViewModel catalogoDeduccion = new CatalogoDeIngresosDeduccionesViewModel();
                    catalogoDeduccion.id = result.cde_IdDeducciones;
                    catalogoDeduccion.descripcion = result.cde_DescripcionDeduccion;

                    if (result.@checked == 1)
                        catalogoDeduccion.check = true;
                    else
                        catalogoDeduccion.check = false;
                    listaCatalogoDeducciones.Add(catalogoDeduccion);
                }
            }
            catch (Exception ex)
            {
                response = ex.Message.ToString();
            }
            
            ViewBag.CatalogoIngresos = listaCatalogoIngresos;
            ViewBag.CatalogoDeducciones = listaCatalogoDeducciones;
            if (tbCatalogoDePlanillas == null)
            {
                return HttpNotFound();
            }

            return View(tbCatalogoDePlanillas);
        }

        // POST: CatalogoDePlanillas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,string[] catalogoDePlanillas, int[] catalogoIngresos, int[] catalogoDeducciones)
        {
            #region Declaracion de Variables
            IEnumerable<object> borrarIngresos = null,
                borrarDeducciones = null,
                listCatalogoDeIngresos = null,
                listCatalogoDeDeducciones = null;

            string mensajeErrorIngreso = "",
                mensajeErrorDeduccion = "", 
                response = "", 
                MensajeError = "", 
                cpla_DescripcionPlanilla = catalogoDePlanillas[0],
                MensajeErrorCatalogoDeIngresos =""
                , MensajeErrorCatalogoDeDeducciones = "";
            int cpla_UsuarioModifica = 1, cpla_FrecuenciaEnDias = int.Parse(catalogoDePlanillas[1]); //TODO: Editar el usuario modifica
            DateTime cpla_FechaModifica = DateTime.Now;
            #endregion

            try
            {
                #region Declarar los listados

                /*
                 * Obtener listados de los detalles de la base de datos
                 */

                //Lista de deducciones de la planilla que estan en la base de datos
                var listadoDetallePlanillaDeduccionesBaseDeDatos = db.tbTipoPlanillaDetalleDeduccion.Where(x => x.cpla_IdPlanilla == id).Select(x => x.cde_IdDeducciones).ToList();

                //Lista los ingresos de la planilla que estan en la base de datos
                var listadoDetallePlanillaIngresosBaseDeDatos = db.tbTipoPlanillaDetalleIngreso.Where(x => x.cpla_IdPlanilla == id).Select(x => x.cin_IdIngreso).ToList();

                //A este listado despues le elimino los id de las deducciones que no quiero eliminar (ni se insertaran), y los que queden entonces los eliminare
                List<int> listadoDetallePlanillaDeduccionesDelete = new List<int>(listadoDetallePlanillaDeduccionesBaseDeDatos);

                //A este listado despues le elimino los id de las ingresos que no quiero eliminar, y los que queden entonces los eliminare
                List<int> listadoDetallePlanillaIngresosDelete = new List<int>(listadoDetallePlanillaIngresosBaseDeDatos);

                /*
                 * Obtener listados de los detalles del lado del cliente
                 */

                //A este listado le eliminare las deducciones que no deseo insertar, entonces si hay algún id dentro de este listado, se insertara.
                List<int> listadoDetallePlanillaDeduccionesInsert = catalogoDeducciones.ToList();

                //A este listado le eliminare los ingresos que no deseo insertar, entonces si hay algún id dentro de este listado, se insertara.
                List<int> listadoDetallePlanillaIngresosInsert = catalogoIngresos.ToList();
                #endregion

                #region Recorrer los listados

                //Obtener los id de las deducciones que quiero eliminar, o insertar, o a los que no les quiero hacer nada.
                foreach (var i in listadoDetallePlanillaDeduccionesBaseDeDatos)
                {
                    foreach (var j in catalogoDeducciones)
                    {
                        if (i == j)
                        {
                            /*
                             Los items que queden en listadoDetallePlanillaDeduccionDelete seran los que se eliminaran, ya que el cliente no los ha seleccionado.
                             */
                            listadoDetallePlanillaDeduccionesDelete.Remove(j);

                            /*
                             Los items que queden en catalogoDeduccionesInsert seran los que se deben insertar
                             */
                            listadoDetallePlanillaDeduccionesInsert.Remove(j);
                            continue;
                        }
                    }
                }

                //Obtener los id de los ingresos que quiero eliminar, o insertar, o a los que no les quiero hacer nada.
                foreach (var i in listadoDetallePlanillaIngresosBaseDeDatos)
                {
                    foreach (var j in catalogoIngresos)
                    {
                        if (i == j)
                        {
                            /*
                             Los items que queden en listadoDetallePlanillaDeduccionDelete seran los que se eliminaran, ya que el cliente no los ha seleccionado.
                             */
                            listadoDetallePlanillaIngresosDelete.Remove(j);

                            /*
                             Los items que queden en catalogoDeduccionesInsert seran los que se deben insertar
                             */
                            listadoDetallePlanillaIngresosInsert.Remove(j);
                            continue;
                        }
                    }
                }
                #endregion

                //Actualizar el el catalogo de planillas
                MensajeError = EditarPlanilla(id, ref MensajeError, cpla_UsuarioModifica, cpla_FechaModifica, cpla_DescripcionPlanilla, cpla_FrecuenciaEnDias);

                if (MensajeError.StartsWith("-1"))
                {
                    response = "error";
                }
                else
                {
                    #region Eliminaciones
                    //Eliminar los registros de las deducciones que desmarco el cliente
                    if (listadoDetallePlanillaDeduccionesDelete.Count != 0)
                    {
                        foreach (var i in listadoDetallePlanillaDeduccionesDelete)
                        {
                            borrarDeducciones = db.UDP_tbTipoPlanillaDetalleDeduccion_Update(i);

                            foreach (UDP_tbTipoPlanillaDetalleDeduccion_Update_Result result in borrarDeducciones)
                                mensajeErrorDeduccion = result.MensajeError; //TODO: Verificar como detectar si hay errores aqui
                        }
                    }

                    //Eliminar los registros de los ingresos que desmarco el cliente
                    if (listadoDetallePlanillaIngresosDelete.Count != 0)
                    {
                        foreach (var i in listadoDetallePlanillaIngresosDelete)
                        {
                            borrarIngresos = db.UDP_tbTipoPlanillaDetalleIngreso_Update(i);
                            foreach (UDP_tbTipoPlanillaDetalleIngreso_Update_Result result in borrarIngresos)
                                mensajeErrorIngreso = result.MensajeError; //TODO: Verificar como detectar si hay errores aqui
                        }
                    }
                    #endregion

                    #region Inserciones
                    //Insertar los registros en catalogo de deducciones
                    if (listadoDetallePlanillaDeduccionesInsert.Count != 0)
                    {
                        InsertarCatalogoDeducciones(catalogoDeducciones: (from i in listadoDetallePlanillaDeduccionesInsert select i).ToArray(),
                            response: ref response,
                            MensajeError: MensajeError,
                            MensajeErrorCatalogoDeIngresos: MensajeErrorCatalogoDeIngresos,
                            MensajeErrorCatalogoDeDeducciones: ref MensajeErrorCatalogoDeDeducciones,
                            listCatalogoDeDeducciones: ref listCatalogoDeDeducciones);
                    }

                    //Insertar los registros en el catalogo de ingresoso
                    if (listadoDetallePlanillaIngresosInsert.Count != 0)
                    {
                        InsertarCatalogoIngresos(catalogoIngresos: (from i in listadoDetallePlanillaIngresosInsert select i).ToArray(),
                            response: ref response,
                            MensajeError: MensajeError,
                            MensajeErrorCatalogoDeIngresos: ref MensajeErrorCatalogoDeIngresos,
                            listCatalogoDeIngresos: ref listCatalogoDeIngresos,
                            cpla_UsuarioCreaModifica: cpla_UsuarioModifica);
                    }
                    #endregion
                }
            }
            catch (Exception)
            {
                response = "error";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // POST: CatalogoDePlanillas/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            string response = "";
            string mensajeError = "";
            IEnumerable<object> planillaInactivada = null; 
            try
            {
                //Inactivar la planilla
                planillaInactivada = db.UDP_Plani_tbCatalogoDePlanillas_Inactivar(id);

                foreach (UDP_Plani_tbCatalogoDePlanillas_Inactivar_Result result in planillaInactivada)
                    mensajeError = result.MensajeError;

                if (mensajeError.Contains("-1"))
                    response = "error";
                else
                    response = "bien";

            }
            catch (Exception ex)
            {
                response = "error";
            }
            return Json(response,JsonRequestBehavior.AllowGet);
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
