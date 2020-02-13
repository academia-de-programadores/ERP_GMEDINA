using ERP_GMEDINA.Attribute;
using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace ERP_GMEDINA.Controllers
{
    public class CatalogoDePlanillasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        Models.Helpers Function = new Models.Helpers();

        // GET: CatalogoDePlanillas
        [SessionManager("CatalogoDePlanillas/Index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult getPlanilla()
        {
            //Obtener el catalogo de planillas, y los usuarios que la crearon y/o modificaron
            UDP_Acce_Login_Result sesion = Session["sesionUsuario"] as UDP_Acce_Login_Result;

            IQueryable<CatalogoDePlanillasViewModel> tbCatalogoDePlanillas = GetPlanilla(sesion?.usu_EsAdministrador);
            object json = new { data = tbCatalogoDePlanillas };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getPeriodos()
        {
            string response = "ok";
            object json = null;
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var listaPeriodos = db.tbPeriodos
                        .Where(x => x.peri_Activo == true)
                        .Select(x => new { id = x.peri_IdPeriodo, descripcion = x.peri_DescripPeriodo }).ToList();
                    return Json(json = new { data = listaPeriodos, status = response }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

            }
            json = new { status = response };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        private IQueryable<CatalogoDePlanillasViewModel> GetPlanilla(bool? usuario = false)
        {
            return db.tbCatalogoDePlanillas
                            .OrderByDescending(x => x.cpla_FechaCrea)
                            .OrderByDescending(x => x.cpla_Activo)
                            .Select(x => new CatalogoDePlanillasViewModel
                            {
                                idPlanilla = x.cpla_IdPlanilla,
                                descripcionPlanilla = x.cpla_DescripcionPlanilla,
                                descripcionPeriodo = x.tbPeriodos.peri_DescripPeriodo,
                                recibeComision = (x.cpla_RecibeComision == true ? "Si" : "No"),
                                activoAdmin = new ActivoAdmin { activo = x.cpla_Activo, esAdmin = usuario ?? false }
                            });
        }

        [HttpGet]
        //Enviar los ingresos y deducciones de la planilla
        public JsonResult getDeduccionIngresos(int id)
        {
            var deducciones = db.tbTipoPlanillaDetalleDeduccion
                .Where(d => d.cpla_IdPlanilla == id)
                .Where(x => x.tbCatalogoDeDeducciones.cde_Activo == true)
                .Select(x => new { x.tbCatalogoDeDeducciones.cde_DescripcionDeduccion });

            var ingresos = db.tbTipoPlanillaDetalleIngreso
                .Where(d => d.cpla_IdPlanilla == id)
                .Where(x => x.tbCatalogoDeIngresos.cin_Activo == true)
                .Select(x => new { x.tbCatalogoDeIngresos.cin_DescripcionIngreso });

            object json = new { deducciones, ingresos };
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        // GET: CatalogoDePlanillas/Details/5
        [SessionManager("CatalogoDePlanillas/Details")]
        public ActionResult Details(int? id)
        {
            string response = "bien";

            if (id == null)
            {
                return Json(response = "error", JsonRequestBehavior.AllowGet);
            }

            tbCatalogoDePlanillas tbCatalogoDePlanillas; //Planilla 

            //Ingresos
            IEnumerable<object> objListaCatalogoIngresos = null;
            List<CatalogoDeIngresosDeduccionesViewModel> listaCatalogoIngresos = new List<CatalogoDeIngresosDeduccionesViewModel>(); //Detalles de la planilla
            GetIngresosEdit(id, ref listaCatalogoIngresos, out objListaCatalogoIngresos);
            ViewBag.CatalogoIngresos = listaCatalogoIngresos;

            //Deducciones
            IEnumerable<object> objListaCatalogoDeducciones = null;
            List<CatalogoDeIngresosDeduccionesViewModel> listaCatalogoDeducciones = new List<CatalogoDeIngresosDeduccionesViewModel>();
            GetDeduccionesEdit(id, ref listaCatalogoDeducciones, out objListaCatalogoDeducciones);
            ViewBag.CatalogoDeducciones = listaCatalogoDeducciones;

            //Obtener la planilla y sus detalles
            response = ObtenerCatalogoDePlanilla(id, response, out tbCatalogoDePlanillas);

            return View(tbCatalogoDePlanillas);
        }

        // GET: CatalogoDePlanillas/Create
        [SessionManager("CatalogoDePlanillas/Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]

        public async Task<JsonResult> GetIngresosDeducciones(bool esCrear = true, int id = 0, bool esIngreso = true)
        {
            object json = null;


            if (esCrear)
            {
                if (esIngreso)
                {
                    //Obtener la lista de ingresos de la base de datos
                    var ingresos = await (from catalogoIngresos in db.tbCatalogoDeIngresos
                                    where catalogoIngresos.cin_Activo == true
                                    select new CatalogoDeIngresosDeduccionesViewModel //Se crea un nuevo objeto para luego recorrer la lista de estos objetos
                                    {
                                        id = catalogoIngresos.cin_IdIngreso,
                                        descripcion = catalogoIngresos.cin_DescripcionIngreso
                                    }).ToListAsync();

                    json = new { data = ingresos };
                }
                else
                {
                    var deducciones = await (from catalogoDeducciones in db.tbCatalogoDeDeducciones
                                       where catalogoDeducciones.cde_Activo == true
                                       select new CatalogoDeIngresosDeduccionesViewModel
                                       {
                                           id = catalogoDeducciones.cde_IdDeducciones,
                                           descripcion = catalogoDeducciones.cde_DescripcionDeduccion
                                       }).ToListAsync();
                    json = new { data = deducciones };
                }
            }
            else
            {
                if (esIngreso)
                {
                    IEnumerable<object> objListaCatalogoIngresos = null;
                    List<CatalogoDeIngresosDeduccionesViewModel> listaCatalogoIngresos = new List<CatalogoDeIngresosDeduccionesViewModel>(); //Detalles de la planilla
                    GetIngresosEdit(id, ref listaCatalogoIngresos, out objListaCatalogoIngresos);
                    json = new { data = listaCatalogoIngresos };
                }
                else
                {
                    IEnumerable<object> objListaCatalogoDeducciones = null;
                    List<CatalogoDeIngresosDeduccionesViewModel> listaCatalogoDeducciones = new List<CatalogoDeIngresosDeduccionesViewModel>();
                    GetDeduccionesEdit(id, ref listaCatalogoDeducciones, out objListaCatalogoDeducciones);
                    json = new { data = listaCatalogoDeducciones };
                }

            }

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        // POST: CatalogoDePlanillas/Create
        [HttpPost]
        [SessionManager("CatalogoDePlanillas/Create")]
        public ActionResult Create(string[] catalogoDePlanillas, int[] catalogoIngresos, int[] catalogoDeducciones, bool checkRecibeComision)
        {

            //La variabele "response" puede contener la palabra: "bien", o "error".
            string response = "bien";

            /*
             * Verificar que el catalogo de planillas, catalogo de ingresos, y catalogo de deducciones
             * tengan algún valor, si no no dejara insertar en la planillas, la validación también
             * se hace en el lado del cliente
             */
            if (catalogoDeducciones == null)
                catalogoDeducciones = new int[0];

            if (catalogoDePlanillas.Length > 1 && catalogoIngresos.Length > 0)
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

                //Estas variables se utilizan para saber el resultado que manda el procedimiento almacenado al insertar
                IEnumerable<object> listCatalogoDePlanillas = null;
                IEnumerable<object> listCatalogoDeIngresos = null;
                IEnumerable<object> listCatalogoDeDeducciones = null;
                int idUser = (int)Session["UserLogin"];
                int cpla_UsuarioCreaModifica = idUser;
                DateTime cpla_FechaCreaModifica = Function.DatetimeNow(); // Asignarle la fecha actual a la variable cpla_FechaCrea
                string cpla_DescripcionPlanilla;
                int cpla_FrecuenciaEnDias;

                #endregion

                using (var dbContextTransaccion = db.Database.BeginTransaction())
                {
                    try
                    {
                        #region Insertar en el catalogo de Planillas

                        cpla_DescripcionPlanilla = catalogoDePlanillas[0]; //El índice 0 del array catalogoDePlanillas contiene la descripción de la planilla
                        cpla_FrecuenciaEnDias = int.Parse(catalogoDePlanillas[1]); //El índice 1 contiene la frecuencia en días de la planilla

                        /* Guardar el catalogo de Planillas en la base de datos
                         * El procedimiento lo igualo a la variable listCatalogoDePlanillas para luego recorrerla
                         * y ver que datos me trae del procedimiento almacenado
                         */
                        listCatalogoDePlanillas = InsertarPlanilla(cpla_UsuarioCreaModifica, cpla_FechaCreaModifica, cpla_DescripcionPlanilla, cpla_FrecuenciaEnDias, checkRecibeComision);

                        /*Recorrer la variable listCatalogoDePlanillas para obtener el id del campo registro ingresado actualmente.
                         *O obtener un -1 y el mensaje de error enviado desde el procedimieto almacenado
                         */
                        foreach (UDP_Plani_tbCatalogoDePlanillas_Insert_Result Resultado in listCatalogoDePlanillas)
                            MensajeError = Resultado.MensajeError;

                        #endregion}

                        //Si hubo un error al insertar en el catalogo de planillas, al inicio de la cadena de texto habrá un -1, seguido del mensaje de error
                        if (MensajeError.StartsWith("-1"))
                        {
                            response = "error";
                        }
                        else
                        {
                            //Verificar que se haya insertado en el catalogo de Planillas
                            if (listCatalogoDePlanillas != null)
                            {
                                #region Insertar en el catalogo de Ingresos

                                // Recorrer el array catalogoIngresos, el catalogoIngresos trae todos los id de los ingresos
                                InsertarCatalogoIngresos(catalogoIngresos,
                                    ref response,
                                    MensajeError,
                                    ref MensajeErrorCatalogoDeIngresos,
                                    ref listCatalogoDeIngresos,
                                    cpla_UsuarioCreaModifica);

                                #endregion

                                #region Insertar en el catalogo de Deducciones

                                if (catalogoDeducciones.Length != 0)
                                    // Recorrer el array catalogoDeducciones
                                    InsertarCatalogoDeducciones(catalogoDeducciones,
                                        ref response,
                                        MensajeError,
                                        MensajeErrorCatalogoDeIngresos,
                                        ref MensajeErrorCatalogoDeDeducciones,
                                        ref listCatalogoDeDeducciones);

                                #endregion
                            }
                        }
                        dbContextTransaccion.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaccion.Rollback();
                        response = "error";
                    }
                }
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //Insertar una nueva planilla
        private IEnumerable<object> InsertarPlanilla(int cpla_UsuarioCreaModifica, DateTime cpla_FechaCreaModifica, string cpla_DescripcionPlanilla, int cpla_FrecuenciaEnDias, bool checkRecibeComision)
        {
            //Retorna la planilla insertada
            return db.UDP_Plani_tbCatalogoDePlanillas_Insert(cpla_DescripcionPlanilla,
                cpla_FrecuenciaEnDias,
                cpla_UsuarioCreaModifica,
                cpla_FechaCreaModifica,
                checkRecibeComision);
        }

        //Editar una planilla
        private string EditarPlanilla(int? idPlanillaEdit, ref string MensajeError, int cpla_UsuarioCreaModifica, DateTime cpla_FechaCreaModifica, string cpla_DescripcionPlanilla, int cpla_FrecuenciaEnDias, bool checkRecibeComision)
        {
            //Procedimiento almacenado para editar el catalogo de planillas
            IEnumerable<object> listCatalogoDePlanillas = db.UDP_Plani_tbCatalogoDePlanillas_Update(idPlanillaEdit,
                cpla_DescripcionPlanilla,
                cpla_FrecuenciaEnDias,
                cpla_UsuarioCreaModifica,
                cpla_FechaCreaModifica,
                checkRecibeComision);

            //Recorrer el resultado de listCatalogoDePlanillas para saber si hubo un error
            foreach (UDP_Plani_tbCatalogoDePlanillas_Update_Result Resultado in listCatalogoDePlanillas)
                MensajeError = Resultado.MensajeError; /*Si hubo un error se le asignara un string
                                                        *que en los primeros caracteres tendra un -1, caso contrario el id que se edito*/

            return MensajeError;
        }

        //Insertar en el catalogo de deducciones
        private void InsertarCatalogoDeducciones(int[] catalogoDeducciones, ref string response, string MensajeError, string MensajeErrorCatalogoDeIngresos, ref string MensajeErrorCatalogoDeDeducciones, ref IEnumerable<object> listCatalogoDeDeducciones)
        {
            foreach (int i in catalogoDeducciones)
            {
                int idUser = (int)Session["UserLogin"];
                int cde_IdDeducciones = i; // Asignarle el id de la deduccion
                int tpdd_UsuarioCrea = idUser;
                DateTime tpdd_FechaCrea = Function.DatetimeNow(); // Asignar la fecha actual

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
            }
        }

        //Insertar en el catalogo de ingresos
        private void InsertarCatalogoIngresos(int[] catalogoIngresos, ref string response, string MensajeError, ref string MensajeErrorCatalogoDeIngresos, ref IEnumerable<object> listCatalogoDeIngresos, int cpla_UsuarioCreaModifica)
        {
            foreach (int i in catalogoIngresos)
            {
                int cin_IdIngreso = i; // Asignarle el id del ingreso 
                DateTime tpdi_FechaCrea = Function.DatetimeNow(); //Asignarle la fecha y hora actual a la variable tpdi_FechaCrea

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
            }
        }

        // GET: CatalogoDePlanillas/Edit/5
        [SessionManager("CatalogoDePlanillas/Edit")]
        public ActionResult Edit(int? id)
        {
            string response = "bien";

            if (id == null)
            {
                return Json(response = "error", JsonRequestBehavior.AllowGet);
            }

            // Obtener los datos del catalogo de planillas filtrando por el id
            tbCatalogoDePlanillas tbCatalogoDePlanillas; //Planilla

            //Obtener la planilla y sus detalles
            response = ObtenerCatalogoDePlanilla(id, response, out tbCatalogoDePlanillas);

            return View(tbCatalogoDePlanillas);
        }

        /*Obtener el catalogo de planillas, el catalogo de ingresos de la planilla 
        *y el catalogo de deducciones de la planilla, filtrando por el id de la planilla*/
        private string ObtenerCatalogoDePlanilla(int? id, string response, out tbCatalogoDePlanillas tbCatalogoDePlanillas)
        {
            response = "ok";
            tbCatalogoDePlanillas = db.tbCatalogoDePlanillas.Find(id); //Buscar por el id en el catalogo de planillas
            return response;
        }

        private void GetIngresosEdit(int? id, ref List<CatalogoDeIngresosDeduccionesViewModel> listaCatalogoIngresos, out IEnumerable<object> listCatalogoDeIngresos)
        {
            #region Obtener el catalogo de ingresos
            listCatalogoDeIngresos = db.UDP_Plani_CatalogoDeIngresosEdit_Select(id); //Obtener la lista del catalogo de ingresos filtrando por el id de la planilla

            //Recorrer el resultado de la variable listCatalogoDeIngresos
            foreach (UDP_Plani_CatalogoDeIngresosEdit_Select_Result result in listCatalogoDeIngresos)
            {
                CatalogoDeIngresosDeduccionesViewModel catalogoIngresos = new CatalogoDeIngresosDeduccionesViewModel(); //Almacenar los ingresos de la planilla
                catalogoIngresos.id = result.cin_IdIngreso; //Se utilizara para identificar que checkbox ha sido clickeado
                catalogoIngresos.descripcion = result.cin_DescripcionIngreso; //Descripcion del ingreso

                //Si la propiedad checked del resultado es verdadera entonces sera true, caso contrario false, esto para saber cuando marcar el checkbox
                if (result.@checked == 1)
                {
                    catalogoIngresos.check = true;
                    catalogoIngresos.checkId = new CheckId { check = true, id = result.cin_IdIngreso };
                }
                else
                {
                    catalogoIngresos.check = false;
                    catalogoIngresos.checkId = new CheckId { check = false, id = result.cin_IdIngreso };
                }

                //Agregar a la lista del catalogo de ingresos el objeto que se acaba de crear
                listaCatalogoIngresos.Add(catalogoIngresos);
            }
            #endregion
        }

        private void GetDeduccionesEdit(int? id, ref List<CatalogoDeIngresosDeduccionesViewModel> listaCatalogoDeducciones, out IEnumerable<object> listCatalogoDeDeducciones)
        {
            #region Obtener el catalogo de deducciones
            //Obtener la lista del catalogo de deducciones filtrando por el id de la planilla
            listCatalogoDeDeducciones = db.UDP_Plani_CatalogoDeduccionesEdit_Select(id);

            //Recorrer el resultado de la variable listCatalogoDeDeducciones
            foreach (UDP_Plani_CatalogoDeduccionesEdit_Select_Result result in listCatalogoDeDeducciones.ToList())
            {
                CatalogoDeIngresosDeduccionesViewModel catalogoDeduccion = new CatalogoDeIngresosDeduccionesViewModel(); //Almacenar las deducciones de la planilla
                catalogoDeduccion.id = result.cde_IdDeducciones; //Se utilizara para identificar que checkbox ha sido clickeado
                catalogoDeduccion.descripcion = result.cde_DescripcionDeduccion; //Descripcion de la deducción

                //Si la propiedad checked del resultado es verdadera entonces sera true, caso contrario false, esto para saber cuando marcar el checkbox
                if (result.@checked == 1)
                {
                    catalogoDeduccion.check = true;
                    catalogoDeduccion.checkId = new CheckId { check = true, id = result.cde_IdDeducciones };
                }
                else
                {
                    catalogoDeduccion.check = false;
                    catalogoDeduccion.checkId = new CheckId { check = false, id = result.cde_IdDeducciones };
                }

                //Agregar a la lista del catalogo de ingresos el objeto que se acaba de crear
                listaCatalogoDeducciones.Add(catalogoDeduccion);
            }
            #endregion
        }


        // POST: CatalogoDePlanillas/Edit/5/arrayCatalogoPlanillas/arrayCatalogoIngresos/arrayCatalogoDeducciones
        [HttpPost]
        [SessionManager("CatalogoDePlanillas/Edit")]
        public async Task<ActionResult> Edit(int id, /*El id de la planilla*/ string[] catalogoDePlanillas, /*valor 1:string =  Descripcion de la planilla valor 2:int = Frecuencia en días*/ int[] catalogoIngresos, /*Array de enteros con los id de los ingresos para la planilla*/ int[] catalogoDeducciones /*Array de enteros con los id de las deducciones para la planilla*/, bool checkRecibeComision)
        {
            #region Declaracion de Variables
            IEnumerable<object> borrarIngresos = null, //Aquí se almacenara el resultado del procedimiento almacenado para borrar el ingreso
                borrarDeduccion = null, //Aquí se almacenara el resultado del procedimiento almacenado para borrar la deducción
                agregarIngreso = null, //Se almacena el resultado del procedimiento almacenado para agregar el ingreso
                agregarDeduccion = null; //Se almacena el resultado del procedimiento almacenado para agregar la deducción

            int idUser = (int)Session["UserLogin"];
            string mensajeErrorIngreso = "", //Para cuando suceda algún error al amacenar un ingreso
                mensajeErrorDeduccion = "", //Para cuando suceda algún error al amacenar una deducción
                MensajeErrorCatalogoPlanillas = "", //Para cuando suceda algun error en al guardar en el catalogo de planillas
                MensajeErrorCatalogoDeIngresos = "", //Para cuando suceda algún error al amacenar un ingreso
                cpla_DescripcionPlanilla = catalogoDePlanillas[0], //Descripción de la planilla
                response = "bien" //Si no hay nada que falle, entonces recibira un mensaje de que todo se hizo bien el cliente
                , MensajeErrorCatalogoDeDeducciones = ""; //Si hay error al guardar las deduccioenes se le notifica
            int cpla_UsuarioModifica = idUser, 
                cpla_FrecuenciaEnDias = int.Parse(catalogoDePlanillas[1]); //Frecuencia en días para generar la planilla
            DateTime cpla_FechaModifica = Function.DatetimeNow();
            #endregion

            using (var dbContextTransaccion = db.Database.BeginTransaction())
            {
                try
                {
                    #region Declarar los listados

                    /*
                     * Obtener listados de los detalles de la base de datos
                     */

                    //Lista de deducciones de la planilla que estan en la base de datos
                    List<int> listadoDetallePlanillaDeduccionesBaseDeDatos = await db.tbTipoPlanillaDetalleDeduccion.Where(x => x.cpla_IdPlanilla == id).Select(x => x.cde_IdDeducciones).ToListAsync();

                    //Lista los ingresos de la planilla que estan en la base de datos
                    List<int> listadoDetallePlanillaIngresosBaseDeDatos = await db.tbTipoPlanillaDetalleIngreso.Where(x => x.cpla_IdPlanilla == id).Select(x => x.cin_IdIngreso).ToListAsync();

                    //A este listado despues le elimino los id de las deducciones que no quiero eliminar (ni se insertaran), y los que queden entonces los eliminare
                    List<int> listadoDetallePlanillaDeduccionesDelete = new List<int>(listadoDetallePlanillaDeduccionesBaseDeDatos);

                    //A este listado despues le elimino los id de las ingresos que no quiero eliminar, y los que queden entonces los eliminare
                    List<int> listadoDetallePlanillaIngresosDelete = new List<int>(listadoDetallePlanillaIngresosBaseDeDatos);

                    /*
                     * Obtener listados de los detalles del lado del cliente
                     */

                    if (catalogoDeducciones == null)
                        catalogoDeducciones = new int[0];

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
                    MensajeErrorCatalogoPlanillas = EditarPlanilla(id, ref MensajeErrorCatalogoPlanillas, cpla_UsuarioModifica, cpla_FechaModifica, cpla_DescripcionPlanilla, cpla_FrecuenciaEnDias, checkRecibeComision);

                    if (MensajeErrorCatalogoPlanillas.StartsWith("-1"))
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
                                borrarDeduccion = db.UDP_tbTipoPlanillaDetalleDeduccion_Update(id, i); //Eliminar la deducción de la base de datos

                                foreach (UDP_tbTipoPlanillaDetalleDeduccion_Update_Result result in borrarDeduccion)
                                    mensajeErrorDeduccion = result.MensajeError; 
                            }
                        }

                        //Eliminar los registros de los ingresos que desmarco el cliente
                        if (listadoDetallePlanillaIngresosDelete.Count != 0)
                        {
                            foreach (var i in listadoDetallePlanillaIngresosDelete)
                            {
                                borrarIngresos = db.UDP_tbTipoPlanillaDetalleIngreso_Update(id, i);
                                foreach (UDP_tbTipoPlanillaDetalleIngreso_Update_Result result in borrarIngresos)
                                    mensajeErrorIngreso = result.MensajeError; 
                            }
                        }
                        #endregion

                        #region Inserciones
                        //Insertar los registros en catalogo de deducciones
                        if (listadoDetallePlanillaDeduccionesInsert.Count != 0)
                        {
                            InsertarCatalogoDeducciones(catalogoDeducciones: (from i in listadoDetallePlanillaDeduccionesInsert select i).ToArray(),
                                response: ref response,
                                MensajeError: MensajeErrorCatalogoPlanillas,
                                MensajeErrorCatalogoDeIngresos: MensajeErrorCatalogoDeIngresos,
                                MensajeErrorCatalogoDeDeducciones: ref MensajeErrorCatalogoDeDeducciones,
                                listCatalogoDeDeducciones: ref agregarDeduccion);
                        }

                        //Insertar los registros en el catalogo de ingresoso
                        if (listadoDetallePlanillaIngresosInsert.Count != 0)
                        {
                            InsertarCatalogoIngresos(catalogoIngresos: (from i in listadoDetallePlanillaIngresosInsert select i).ToArray(),
                                response: ref response,
                                MensajeError: MensajeErrorCatalogoPlanillas,
                                MensajeErrorCatalogoDeIngresos: ref MensajeErrorCatalogoDeIngresos,
                                listCatalogoDeIngresos: ref agregarIngreso,
                                cpla_UsuarioCreaModifica: cpla_UsuarioModifica);
                        }
                        #endregion
                    }
                    dbContextTransaccion.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaccion.Rollback();
                    response = "error";
                }
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        // POST: CatalogoDePlanillas/Delete/5
        [HttpPost, ActionName("Delete")]
        [SessionManager("CatalogoDePlanillas/DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            string response = "bien";
            string mensajeError = "";
            IEnumerable<object> planillaInactivada = null;
            using (var dbContextTransaccion = db.Database.BeginTransaction())
            {
                try
                {
                    //Inactivar la planilla
                    planillaInactivada = db.UDP_Plani_tbCatalogoDePlanillas_Inactivar(id);

                    foreach (UDP_Plani_tbCatalogoDePlanillas_Inactivar_Result result in planillaInactivada)
                        mensajeError = result.MensajeError;

                    if (mensajeError.Contains("-1"))
                        response = "error";

                    dbContextTransaccion.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaccion.Rollback();
                    response = "error";

                }
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SessionManager("CatalogoDePlanillas/Activar")]
        public JsonResult ActivarPlanilla(int id)
        {
            string response = "bien";
            string mensajeError = "";
            int usuarioModifica = Function.GetUser(); //TOOD: Agregar usuario modifica
            DateTime fechaModifica = Function.DatetimeNow();
            IEnumerable<object> planillaActivada = null;
            IQueryable<CatalogoDePlanillasViewModel> tbCatalogoDePlanillas = null;

            using (var dbContextTransaccion = db.Database.BeginTransaction())
            {
                try
                {
                    planillaActivada = db.UDP_Plani_tbCatalogoDePlanillas_Activar(id, usuarioModifica, fechaModifica);

                    foreach (UDP_Plani_tbCatalogoDePlanillas_Activar_Result result in planillaActivada)
                        mensajeError = result.MensajeError;

                    if (mensajeError.StartsWith("-1"))
                        response = "error";

                    tbCatalogoDePlanillas = GetPlanilla((Session["sesionUsuario"] as tbUsuario)?.usu_EsAdministrador);

                    dbContextTransaccion.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaccion.Rollback();
                    response = "error";
                }
            }

            object json = new { data = tbCatalogoDePlanillas, response = response };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult verificarAutorizacion(string sPantalla)
        {
            int UserID = 0;
            bool EsAdmin = false;
            bool Retorno = false;

            try
            {
                UserID = (int)Session["UserLogin"];
                EsAdmin = (bool)Session["UserLoginEsAdmin"];
                if (EsAdmin)
                {
                    Retorno = true;
                }
                else
                {
                    var list = (IEnumerable<SDP_Acce_GetUserRols_Result>)Session["UserLoginRols"];
                    var BuscarList = list.Where(x => x.obj_Referencia == sPantalla);
                    int Conteo = BuscarList.Count();
                    if (Conteo > 0)
                        Retorno = true;
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                Retorno = false;
            }

            return Json(Retorno, JsonRequestBehavior.AllowGet);

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