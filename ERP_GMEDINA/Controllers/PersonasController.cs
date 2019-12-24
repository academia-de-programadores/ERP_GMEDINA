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
    public class PersonasController : Controller
    {
        private ERP_GMEDINAEntities db = null;

        // GET: Personas
        public ActionResult Index()
        {
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            var tbPersonas = new List<tbPersonas> { };
            return View(tbPersonas);
        }
        // GET: Personas/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPersonas tbPersonas = db.tbPersonas.Find(id);
            if (tbPersonas == null)
            {
                return HttpNotFound();
            }
            return View(tbPersonas);
        }
        public ActionResult Create()
        {
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            //Ddl Sexo
            var Sexo = new List<object> { };
            Sexo.Add(new
            {
                Id = 0,
                Descripcion = "**Seleccione una opción**"
            });
            Sexo.Add(new
            {
                Id = "F",
                Descripcion = "Femenino"
            });
            Sexo.Add(new
            {
                Id = "M",
                Descripcion = "Masculino"
            });
            //Ddl EstadoCivil
            var EstadoCivil = new List<object> { };
            EstadoCivil.Add(new
            {
                Id = 0,
                Descripcion = "**Seleccione una opción**",
            });
            EstadoCivil.Add(new
            {
                Id = "S",
                Descripcion = "Soltero"
            });
            EstadoCivil.Add(new
            {
                Id = "C",
                Descripcion = "Casado"
            });
            //ddl TipoSangre
            var TipoSangre = new List<object> { };
            TipoSangre.Add(new
            {
                Id = 0,
                Descripcion = "**Seleccione una opción**",
            });
            TipoSangre.Add(new
            {
                Id = "O+",
                Descripcion = "O+"
            });
            TipoSangre.Add(new
            {
                Id = "O-",
                Descripcion = "O-"
            });
            TipoSangre.Add(new
            {
                Id = "A",
                Descripcion = "A+"
            });
            TipoSangre.Add(new
            {
                Id = "A-",
                Descripcion = "A-"
            });
            ViewBag.per_TipoSangre = new SelectList(TipoSangre, "Id", "Descripcion");
            ViewBag.per_EstadoCivil = new SelectList(EstadoCivil, "Id", "Descripcion");
            ViewBag.per_Sexo = new SelectList(Sexo, "Id", "Descripcion");

            //Nacionalidades
            List<tbNacionalidades> Nacionalidades = new List<tbNacionalidades> { };
            ViewBag.nac_Id = new SelectList(Nacionalidades, "nac_Id", "nac_Descripcion");

            return View();
        }
        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(tbPersonas tbPersonas, DatosProfesionalesArray DatosProfesionales)
        {
            string msj = "...";
            if (tbPersonas.per_Identidad != "")
            {
                var Usuario = (tbUsuario)Session["Usuario"];
                try
                {
                    //var PA
                    //recorro el result
                    //{
                    // msj= item.MensajeError + "";
                    //}
                }
                catch(Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
            }
            else
            {
                msj = "-3";
            }
            return Json(msj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult llenarTabla()
        {
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var tbPersonas = db.tbPersonas
                        .Select(
                        p => new
                        {
                            Id = p.per_Id,
                            Identidad = p.per_Identidad,
                            Nombre = p.per_Nombres + " " + p.per_Apellidos,
                            CorreoElectronico = p.per_CorreoElectronico
                        })
                        .ToList();
                    return Json(tbPersonas, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        //Llenar Drop Nacionalidades
        public ActionResult llenarDropDowlistNacionalidades()
        {
            var Nacionalidades = new List<object> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    Nacionalidades.Add(new
                    {
                        Id = 0,
                        Descripcion = "**Seleccione una opción**"
                    });
                    Nacionalidades.AddRange(db.tbNacionalidades
                    .Select(tabla => new { Id = tabla.nac_Id, Descripcion = tabla.nac_Descripcion })
                    .ToList());
                }
                catch
                {
                    return Json("-2", 0);
                }

            }
            var result = new Dictionary<string, object>();
            result.Add("nac_Id", Nacionalidades);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChildRowData(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            
            List<V_tbPersonas> lista = new List<V_tbPersonas> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    lista = db.V_tbPersonas.Where(x => x.per_Id == id).ToList();
                    if(lista.Count == 0)
                    {
                        lista.Add(new V_tbPersonas { per_Id = id,Relacion_Id =0,Descripcion ="",Relacion = "" });
                    }
                }
                catch(Exception ex)
                {
                    ex.Message.ToString();
                }
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DualListBoxData()
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            //List<tbHorarios> lista = new List<tbHorarios> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    var lista = db.V_DatosProfesionalesP.Select(tabla => new { TipoDato = tabla.TipoDato, Id = tabla.Data_Id, Descripcion = tabla.Descripcion }).ToList();
                    DatosProfesionales Data = new DatosProfesionales();
                    foreach (var X in lista)
                    {
                        switch (X.TipoDato)
                        {
                            case "C":
                                tbCompetencias Comp = new tbCompetencias();
                                Comp.comp_Descripcion = X.Descripcion;
                                Comp.comp_Id = X.Id;
                                Data.Competencias.Add(Comp);
                                break;

                            case "H":
                                tbHabilidades Habi = new tbHabilidades();
                                Habi.habi_Descripcion = X.Descripcion;
                                Habi.habi_Id = X.Id;
                                Data.Habilidades.Add(Habi);
                                break;

                            case "I":
                                tbIdiomas Idi = new tbIdiomas();
                                Idi.idi_Descripcion = X.Descripcion;
                                Idi.idi_Id = X.Id;
                                Data.Idiomas.Add(Idi);
                                break;

                            case "T":
                                tbTitulos Titu = new tbTitulos();
                                Titu.titu_Descripcion = X.Descripcion;
                                Titu.titu_Id = X.Id;
                                Data.Titulos.Add(Titu);
                                break;

                            case "R":
                                tbRequerimientosEspeciales Reqs = new tbRequerimientosEspeciales();
                                Reqs.resp_Descripcion = X.Descripcion;
                                Reqs.resp_Id = X.Id;
                                Data.ReqEspeciales.Add(Reqs);
                                break;
                        }
                    }

                    return Json(Data, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
            return Json("-2", JsonRequestBehavior.AllowGet);
        }
        //Detalles
        public ActionResult Detalles(int? id)
        {
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var tbPersonas = db.tbPersonas
                        .Select(
                        p => new
                        {
                            per_Id = p.per_Id,
                            per_Identidad = p.per_Identidad,
                            per_Nombres = p.per_Nombres,
                            per_Apellidos =p.per_Apellidos,
                            nac_Id = p.tbNacionalidades.nac_Descripcion,
                            per_Edad = p.per_Edad,
                            per_TipoSangre = p.per_TipoSangre,
                            per_Direccion = p.per_Direccion,
                            per_Telefono = p.per_Telefono,
                            per_CorreoElectronico = p.per_CorreoElectronico,
                            per_FechaCrea = p.per_FechaCrea,
                            per_FechaModifca = p.per_FechaModifica,
                            per_UsuarioCrea = p.tbUsuario.usu_Nombres,
                            per_UsuarioModifica = p.tbUsuario.usu_Nombres
                        })
                        .Where(x=> x.per_Id == id).ToList();
                    return Json(tbPersonas, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Areas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPersonas tbPersonas  = db.tbPersonas.Find(id);
            if (tbPersonas == null)
            {
                return HttpNotFound();
            }
            ViewBag.per_UsuarioCrea = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPersonas.per_UsuarioCrea);
            ViewBag.per_UsuarioModifica = new SelectList(db.tbUsuario, "usu_Id", "usu_NombreUsuario", tbPersonas.per_UsuarioModifica);
            return View(tbPersonas);
        }
        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "area_Id,car_Id,suc_Id,area_Descripcion,area_Estado,area_Razoninactivo,area_Usuariocrea,area_Fechacrea,area_Usuariomodifica,area_Fechamodifica")] tbAreas tbAreas)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            string result = "";
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    //en esta area actualizamos el registro con el procedimiento almacenado
                }
                catch
                {
                    result = "-2";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // POST: Areas/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            //declaramos la variable de coneccion solo para recuperar los datos necesarios.
            //posteriormente es destruida.
            string result = "";
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    //en esta area Inavilitamos el registro con el procedimiento almacenado
                }
                catch
                {
                    result = "-2";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected tbUsuario IsNull(tbUsuario valor)
        {
            if (valor != null)
            {
                return valor;
            }
            else
            {
                return new tbUsuario { usu_NombreUsuario = "" };
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
