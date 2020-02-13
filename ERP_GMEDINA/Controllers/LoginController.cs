using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using System.Threading.Tasks;

namespace ERP_GMEDINA.Controllers
{
    public class LoginController : Controller
    {
        ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        // GET: Login
        public ActionResult Index()
        {
                Session["UserLogin"] = null;
                return View();
        }

        [HttpPost]
        public ActionResult Index(tbUsuario Login, string txtPassword)
        {
            try
            {
                var Usuario = db.UDP_Acce_Login(Login.usu_NombreUsuario, txtPassword).ToList();
                if (Usuario.Count > 0)
                {
                    foreach (UDP_Acce_Login_Result UserLogin in Usuario)
                    {
                        var Listado = db.SDP_Acce_GetUserRols(UserLogin.usu_Id, "").ToList();
                        var ListadoRol = db.SDP_Acce_GetRolesAsignados(UserLogin.usu_Id).ToList();
                        Session["UserRol"] = ListadoRol.Count();
                        Session["UserLogin"] = UserLogin.usu_Id;
                        Session["UserName"] = UserLogin.usu_NombreUsuario;
                        Session["UserLoginRols"] = Listado;
                        Session["sesionUsuario"] = UserLogin;
                        Session["UserLoginEsAdmin"] = UserLogin.usu_EsAdministrador;
                        Session["UserLoginSesion"] = UserLogin.usu_SesionesValidas;
                        if (!UserLogin.usu_EsActivo)
                        {
                            ModelState.AddModelError("usu_NombreUsuario", "Usuario inactivo, contacte al Administrador");
                            return View(Login);
                        }
                        if (UserLogin.usu_SesionesValidas == 0)
                        {
                            ModelState.AddModelError("usu_NombreUsuario", "Su contraseña expiró, contacte al Administrador");
                            return View(Login);
                        }
                        if (UserLogin.usu_SesionesValidas == 1)
                        {
                            return RedirectToAction("ModificarPass/" + Session["UserLogin"], "Usuario");
                        }
                    }
                    return RedirectToAction("MenuPrincipal", "Menu");
                }
                else
                {
                    ModelState.AddModelError("usu_NombreUsuario", "Usuario o Password incorrecto");
                    return View(Login);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return View(Login);
            }
        }

        public ActionResult CerrarSesion()
        {
            Session.Clear();
            Session.Abandon();
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1D);
            Response.Expires = -1500;
            Response.CacheControl = "no-cache";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            AuthenticationManager.SignOut();
            Session["UserLogin"] = null;
            Session["UserLoginRols"] = null;
            Session["UserLoginEsAdmin"] = null;
            Session["UserLoginSesion"] = null;
            return RedirectToAction("Index", "Login");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult SinAcceso()
        {
            //Validar Inicio de Sesión
            Models.Helpers Helpers = new Models.Helpers();
            if (Helpers.GetUserLogin())
                return View();
            else
                return RedirectToAction("Index", "Login");
        }

        public ActionResult NotFound()
        {
            //Validar Inicio de Sesión
            Models.Helpers Helpers = new Models.Helpers();
            if (Helpers.GetUserLogin())
                return View();
            else
                return RedirectToAction("Index", "Login");
        }

		#region GET : LoadUserModelState
        // ESTE ES EL QUE SE ESTÁ USANDO
		public JsonResult LoadUserModelState()
		{
			//INICIALIZACION DEL OBJETC VM_ModelState
			VM_ModelState userModel = new VM_ModelState();
			try
			{
                //ID USUARIO LOGUEADO
                int userId = (int)Session["UserLogin"];
				//GET: ESADMIN
				bool EsAdmin = (bool)Session["UserLoginEsAdmin"];
				//UTILITARIO PARA OBTENER LA DATA DE VM_ModelState
				Helpers.General vm = new Helpers.General();
				//SOBRECARGA DE OBJECT VM_ModelState
				userModel = vm.Cargar_ModelState(userId, EsAdmin);
			}
			catch(Exception Ex)
			{
				Ex.Message.ToString();
				return Json("error", JsonRequestBehavior.AllowGet);
			}

			//RETORNO DEL MODEL STATE
			return Json(userModel, JsonRequestBehavior.AllowGet);
		}
        #endregion


        #region GET : LoadUserModelStateAsync
        //public JsonResult LoadUserModelStateAsync()
        //{
        //	//INICIALIZACION DEL OBJETC VM_ModelState
        //	VM_ModelState userModel = new VM_ModelState();
        //	//Task_ModelState = Task.Run(() =>
        //	//{
        //	//ID USUARIO LOGUEADO
        //	int userId = (int)Session["UserLogin"];
        //	//GET: ESADMIN
        //			bool EsAdmin = (bool)Session["UserLoginEsAdmin"];
        //	//UTILITARIO PARA OBTENER LA DATA DE VM_ModelState
        //	Models.Helpers.General vm = new Models.Helpers.General();
        //	//SOBRECARGA DE OBJECT VM_ModelState
        //	userModel = vm.Cargar_ModelState(userId, EsAdmin);
        //	//});
        //	//RETORNO DEL MODEL STATE
        //	return Json(userModel, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        public ActionResult SinRol()
        {
            //Validar Inicio de Sesión
            Models.Helpers Function = new Models.Helpers();
            if (Function.GetUserLogin())
                return View();
            else
                return RedirectToAction("Index", "Login");
        }
    }
}
