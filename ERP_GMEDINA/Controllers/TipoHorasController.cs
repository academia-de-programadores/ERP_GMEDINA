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
    public class TipoHorasController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        // GET: TipoHoras
        public ActionResult Index()
        {
            tbUsuario Usuario = new tbUsuario();
            Usuario.usu_Id = 1;
            Session["Usuario"] = Usuario;
            var tbTipoHoras = db.tbTipoHoras.Include(t => t.tbUsuario).Include(t => t.tbUsuario1).Where(x => x.tiho_Estado == true).OrderByDescending(x => x.tiho_FechaCrea );
            return View(tbTipoHoras.ToList());
        }

        [HttpPost]
        public JsonResult llenarTabla()
        {
            List<tbTipoHoras> tbTipoHoras =
                new List<Models.tbTipoHoras> { };
            foreach (tbTipoHoras x in db.tbTipoHoras.ToList().Where(x => x.tiho_Estado == true))
            {
                tbTipoHoras.Add(new tbTipoHoras
                {
                    tiho_Id = x.tiho_Id,
                    tiho_Descripcion = x.tiho_Descripcion,
                    tiho_Recargo=x.tiho_Recargo,
                    tiho_Estado = x.tiho_Estado,
                    tiho_RazonInactivo = x.tiho_RazonInactivo,
                    tiho_UsuarioCrea = x.tiho_UsuarioCrea,
                    tiho_FechaCrea = x.tiho_FechaCrea,
                    tiho_UsuarioModifica = x.tiho_UsuarioModifica,
                    tiho_FechaModifica = x.tiho_FechaModifica
                });
            }
            //tbHabilidades Habilidad = new tbHabilidades {habi_Descripcion="hola", habi_Id=1 };
            //List<tbHabilidades> tbHabilidades = new List<Models.tbHabilidades> { };
            ////tbHabilidades.Add(Habilidad);
            return Json(tbTipoHoras, JsonRequestBehavior.AllowGet);
        }


        // GET: TipoHoras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoHoras tbTipoHoras = db.tbTipoHoras.Find(id);
            if (tbTipoHoras == null || !tbTipoHoras.tiho_Estado)
            {
                return HttpNotFound();
            }
            Session["id"] = id;
            var tipohora = new tbTipoHoras
            {
                tiho_Id = tbTipoHoras.tiho_Id,
                tiho_Descripcion = tbTipoHoras.tiho_Descripcion,
                tiho_Recargo = tbTipoHoras.tiho_Recargo,
                tiho_Estado = tbTipoHoras.tiho_Estado,
                tiho_RazonInactivo = tbTipoHoras.tiho_RazonInactivo,
                tiho_UsuarioCrea = tbTipoHoras.tiho_UsuarioCrea,
                tiho_FechaCrea = tbTipoHoras.tiho_FechaCrea,
                tiho_UsuarioModifica = tbTipoHoras.tiho_UsuarioModifica,
                tiho_FechaModifica = tbTipoHoras.tiho_FechaModifica
            };
            if (tbTipoHoras.tbUsuario != null)
            {
                tipohora.tbUsuario = new tbUsuario { usu_NombreUsuario = tbTipoHoras.tbUsuario.usu_NombreUsuario };
            }
            else
            {
                tipohora.tbUsuario = new tbUsuario { usu_NombreUsuario = "" };
            }
            if (tbTipoHoras.tbUsuario1 != null)
            {
                tipohora.tbUsuario1 = new tbUsuario { usu_NombreUsuario = tbTipoHoras.tbUsuario1.usu_NombreUsuario };
            }
            else
            {
                tipohora.tbUsuario1 = new tbUsuario { usu_NombreUsuario = "" };
            }
            return Json(tipohora, JsonRequestBehavior.AllowGet);
        

    }

        // GET: TipoHoras/Create
        public ActionResult Create()
        {
            ViewBag.tiho_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            ViewBag.tiho_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario");
            return View();
        }

        // POST: TipoHoras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(object tbTipoHoras)
        {
            tbTipoHoras TipoHora = new tbTipoHoras();

            var Usuario = (tbUsuario)Session["Usuario"];
            //TipoHora.tiho_Descripcion = tiho_Descripcion;
            //TipoHora.tiho_Recargo = tiho_recargo;
            //TipoHora.tiho_FechaCrea = DateTime.Now;
            if (ModelState.IsValid)
            {
                string MensajeError = "";
                try
                {
                    IEnumerable<object> listTipoHoras = null;
                    listTipoHoras = db.UDP_RRHH_tbTipoHoras_Insert(TipoHora.tiho_Descripcion,
                                                                    TipoHora.tiho_Recargo,
                                                                    Usuario.usu_Id,
                                                                    DateTime.Now);
                    foreach (UDP_RRHH_tbTipoHoras_Insert_Result RES in listTipoHoras)
                    {
                        MensajeError = RES.MensajeError;

                    }
                    if (!string.IsNullOrEmpty(MensajeError))
                    {
                        if (MensajeError.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "1.No se pudo agregar el Registro");
                            return Json(MensajeError.Substring(0, 2));
                        }
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    ModelState.AddModelError("", "2.No se pudo agregar el registro");
                    return Json(MensajeError.Substring(0, 1));
                }

            }

            return Json(TipoHora, JsonRequestBehavior.AllowGet);
        }

        // GET: TipoHoras/Edit/5
        public ActionResult Edit(int? id)
        {
            Session["id"] = id;
            var List = db.UDP_RRHH_tbTipoHoras_Select(id).ToList();

            return Json(List, JsonRequestBehavior.AllowGet);
        }

        // POST: TipoHoras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int tiho_Id, string tiho_Descripcion,int tiho_Recargo)
        {
            tbTipoHoras TipoHora = new tbTipoHoras();
            var Usuario = (tbUsuario)Session["Usuario"];
            TipoHora.tiho_Id = tiho_Id;
            Session["TipoHora"] = TipoHora;
            //var id = (int)Session["id"];
            TipoHora.tiho_Id = tiho_Id;
            TipoHora.tiho_Descripcion = tiho_Descripcion;
            TipoHora.tiho_Recargo = tiho_Recargo;
       
            if (ModelState.IsValid)
            {
                string MensajeError = "";
                try
                {
                    IEnumerable<object> listTipoHoras = null;
                    listTipoHoras = db.UDP_RRHH_tbTipoHora_Update(TipoHora.tiho_Id,
                                                                   TipoHora.tiho_Descripcion,
                                                                   TipoHora.tiho_Recargo,
                                                                    Usuario.usu_Id,
                                                                    DateTime.Now);
                    foreach (UDP_RRHH_tbTipoHora_Update_Result RES in listTipoHoras)
                    {
                        MensajeError = RES.MensajeError;

                    }
                    if (!string.IsNullOrEmpty(MensajeError))
                    {
                        if (MensajeError.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "1.No se pudo editar el Registro");
                            return Json(MensajeError.Substring(0, 2));
                        }
                    }
                    return Json("Exito", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    ModelState.AddModelError("", "2.No se pudo editar el registro");
                    return Json(MensajeError.Substring(0, 1));
                }

            }

            return Json(TipoHora, JsonRequestBehavior.AllowGet);
        }

        // GET: TipoHoras/Delete/5

        //public JsonResult Inactivar(int? ID)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    tbTipoHoras tbTipoHoras = db.tbTipoHoras.Find(ID);
        //    return Json(tbTipoHoras, JsonRequestBehavior.AllowGet);
        //}

        // POST: TipoHoras/Delete/5
        //[HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Inactivar( string  tiho_RazonInactivo)
        {
            var id = (int)Session["id"];
            tbTipoHoras TipoHora = new tbTipoHoras();
            TipoHora.tiho_Id = id;
            TipoHora.tiho_RazonInactivo = tiho_RazonInactivo;
            var Usuario = (tbUsuario)Session["Usuario"];
            if (ModelState.IsValid)
            {
                string MensajeError = "";
                try
                {
                    IEnumerable<object> listTipoHoras = null;
                    listTipoHoras = db.UDP_RRHH_tbTipoHoras_Delete(TipoHora.tiho_Id,
                                                                   TipoHora.tiho_RazonInactivo,
                                                                   Usuario.usu_Id,
                                                                   DateTime.Now);
                    foreach (UDP_RRHH_tbTipoHoras_Delete_Result RES in listTipoHoras)
                    {
                        MensajeError = RES.MensajeError;

                    }
                    if (!string.IsNullOrEmpty(MensajeError))
                    {
                        if (MensajeError.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "1.No se pudo inhabilitar el Registro");
                            return Json(MensajeError.Substring(0, 2));
                        }
                    }
                    return Json("Exito", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    ModelState.AddModelError("", "2.No se pudo inhabilitar el registro");
                    return Json(MensajeError.Substring(0, 1));
                }

            }

            return Json(TipoHora, JsonRequestBehavior.AllowGet);


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
