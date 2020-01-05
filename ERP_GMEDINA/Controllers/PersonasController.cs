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
        // GET : Personas/Llenar DDL
        public ActionResult llenarDropDowlistNacionalidades()
        {
            var Nacionalidades = new List<object> { };
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    Nacionalidades.Add(new
                    {
                        Id = "",
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
        // GET : Personas/Create
        public ActionResult Create()
        {
            Session["Usuario"] = new tbUsuario { usu_Id = 1 };
            //Ddl Sexo
            var Sexo = new List<object> { };
            Sexo.Add(new
            {
                Id = "",
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
                Id = "", 
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
                Id = "",
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
                Id = "A+",
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
        // GET : Personas/LlenarTabla
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
                            CorreoElectronico = p.per_CorreoElectronico,
                            Estado = p.per_Estado
                        })
                        .Where(p => p.Estado == true)
                        .ToList();
                    return Json(tbPersonas, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        // GET : Personas/ChildRowData
        public ActionResult ChildRowData(int? id)
        {
            
            using (db = new ERP_GMEDINAEntities())
            {
                List<V_tbPersonas> lista = null;
                using (db = new ERP_GMEDINAEntities())
                try
                {
                    lista =new List<V_tbPersonas> { };     
                    var Competencias = db.tbCompetenciasPersona.Select(c => new { cope_Id = c.cope_Id, per_Id = c.per_Id , Descripcion = c.tbCompetencias.comp_Descripcion}).ToList();
                    foreach (var X in Competencias)
                    {
                        if(X.per_Id == id)
                        {
                            lista.Add(new V_tbPersonas { per_Id = X.per_Id, Relacion_Id = X.cope_Id, Descripcion = X.Descripcion, Relacion = "Competencias" });
                        }
                    }
                    var Habilidades = db.tbHabilidadesPersona.Select(h => new { hape_Id = h.hape_Id, per_Id = h.per_Id, Descripcion = h.tbHabilidades.habi_Descripcion }).ToList();
                    foreach (var X in Habilidades)
                    {
                        if(X.per_Id == id)
                        {
                            lista.Add(new V_tbPersonas { per_Id = X.per_Id, Relacion_Id = X.hape_Id, Descripcion = X.Descripcion, Relacion = "Habilidades" });
                        }
                    }
                    var Idiomas = db.tbIdiomaPersona.Select(i => new { idpe_Id = i.idpe_Id, per_Id = i.per_Id, Descripcion = i.tbIdiomas.idi_Descripcion }).ToList();
                    foreach (var X in Idiomas)
                    {
                        if (X.per_Id == id)
                        {
                            lista.Add(new V_tbPersonas { per_Id = X.per_Id.Value, Relacion_Id = X.idpe_Id, Descripcion = X.Descripcion, Relacion = "Idiomas" });
                        }
                    }
                    var ReEspeciales = db.tbRequerimientosEspecialesPersona.Select(rep => new { rep_Id = rep.rep_Id, per_Id = rep.per_Id, Descripcion = rep.tbRequerimientosEspeciales.resp_Descripcion }).ToList();
                    foreach (var X in ReEspeciales)
                    {
                        if (X.per_Id == id)
                        {
                            lista.Add(new V_tbPersonas { per_Id = X.per_Id, Relacion_Id = X.rep_Id, Descripcion = X.Descripcion, Relacion = "Requerimientos_Especiales" });
                        }
                    }
                    var Titulos = db.tbTitulosPersona.Select(t => new { tipe_Id = t.tipe_Id, per_Id = t.per_Id, Descripcion = t.tbTitulos.titu_Descripcion }).ToList();
                    foreach (var X in Titulos)
                    {
                        if (X.per_Id == id)
                        {
                            lista.Add(new V_tbPersonas { per_Id = X.per_Id, Relacion_Id = X.tipe_Id, Descripcion = X.Descripcion, Relacion = "Titulos" });
                        }
                    }
                        if (lista.Count == 0)
                    {
                        lista.Add(new V_tbPersonas { per_Id = id.Value, Relacion_Id = 0, Descripcion = "", Relacion = "" });
                    }

                    return Json(lista, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                return Json(lista, JsonRequestBehavior.AllowGet);
            }

        }
        // GET : Personas/Create
        public ActionResult DualListBoxData()
        {
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

        // POST: Personas/Create
        [HttpPost]
        public ActionResult Create(Personas tbPersonas,DatosProfesionalesArray DatosProfesionalesArray)//,)
        {
            string msj = "...";
            if (tbPersonas != null)
            {
                try
                {
                    using (db = new ERP_GMEDINAEntities())
                    {
                        var List = db.UDP_RRHH_tbPersonas_Insert(tbPersonas.per_Identidad, tbPersonas.per_Nombres, tbPersonas.per_Apellidos, tbPersonas.per_FechaNacimiento, tbPersonas.per_Sexo, tbPersonas.per_Edad, tbPersonas.nac_Id, tbPersonas.per_Direccion, tbPersonas.per_Telefono, tbPersonas.per_CorreoElectronico, tbPersonas.per_EstadoCivil, tbPersonas.per_TipoSangre, 1, DateTime.Now);

                        foreach (UDP_RRHH_tbPersonas_Insert_Result item in List)
                        {
                            msj = item.MensajeError + "";
                            //Competencias
                            if(DatosProfesionalesArray.Competencias != null & msj != "-1")
                            {
                               for( int i =0;i < DatosProfesionalesArray.Competencias.Length;i++)
                                {
                                    var Competencias = db.UDP_RRHH_tbCompetenciasPersona_Insert(Int32.Parse(msj),DatosProfesionalesArray.Competencias[i],1,DateTime.Now);
                                    foreach(UDP_RRHH_tbCompetenciasPersona_Insert_Result comp in Competencias )
                                    {
                                        var result = comp.MensajeError + "";
                                    }
                                }   
                            }
                            //Habilidades
                            if (DatosProfesionalesArray.Habilidades != null & msj != "-1")
                            {
                                for (int i = 0; i < DatosProfesionalesArray.Competencias.Length; i++)
                                {
                                    var Habilidades = db.UDP_RRHH_tbHabilidadesPersona_Insert(Int32.Parse(msj), DatosProfesionalesArray.Habilidades[i], 1, DateTime.Now);
                                    foreach (UDP_RRHH_tbHabilidadesPersona_Insert_Result hab in Habilidades)
                                    {
                                        var result = hab.MensajeError + "";
                                    }
                                }
                            }
                            //Idiomas
                            if (DatosProfesionalesArray.Idiomas != null & msj != "-1")
                            {
                                for (int i = 0; i < DatosProfesionalesArray.Idiomas.Length; i++)
                                {
                                    var Idiomas = db.UDP_RRHH_tbIdiomasPersona_Insert(Int32.Parse(msj), DatosProfesionalesArray.Idiomas[i], 1, DateTime.Now);
                                    foreach (UDP_RRHH_tbIdiomasPersona_Insert_Result idi in Idiomas)
                                    {
                                        var result = idi.MensajeError + "";
                                    }
                                }
                            }
                            //Requerimientos Especiales
                            if (DatosProfesionalesArray.ReqEspeciales != null & msj != "-1")
                            {
                                for (int i = 0; i < DatosProfesionalesArray.ReqEspeciales.Length; i++)
                                {
                                    var ReqEspeciales = db.UDP_RRHH_tbRequerimientosEspecialesPersona_Insert(Int32.Parse(msj), DatosProfesionalesArray.ReqEspeciales[i], 1, DateTime.Now);
                                    foreach (UDP_RRHH_tbRequerimientosEspecialesPersona_Insert_Result rep in ReqEspeciales)
                                    {
                                        var result = rep.MensajeError + "";
                                    }
                                }
                            }
                            //Titulos
                            if (DatosProfesionalesArray.Titulos != null & msj != "-1")
                            {
                                for (int i = 0; i < DatosProfesionalesArray.Titulos.Length; i++)
                                {
                                    var Titulos = db.UDP_RRHH_tbTitulosPersona_Insert(Int32.Parse(msj), DatosProfesionalesArray.Titulos[i],2000, 1, DateTime.Now);
                                    foreach (UDP_RRHH_tbTitulosPersona_Insert_Result rep in Titulos)
                                    {
                                        var result = rep.MensajeError + "";
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
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
        [HttpPost]
        public ActionResult Edit([Bind(Include = "area_Id,car_Id,suc_Id,area_Descripcion,area_Estado,area_Razoninactivo,area_Usuariocrea,area_Fechacrea,area_Usuariomodifica,area_Fechamodifica")] tbAreas tbAreas)
        {
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
        public ActionResult Delete(Personas tbPersonas)
        {
            string msj = "";
            string result = "";
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    var _list = db.UDP_RRHH_tbPersonas_Inactivar(tbPersonas.per_Id,tbPersonas.per_RazonInactivo,1,DateTime.Now);
                    foreach(UDP_RRHH_tbPersonas_Inactivar_Result item in _list)
                    {
                        msj = item.MensajeError + "";
                        if(msj != "-1")
                        {
                            var Competencias = db.tbCompetenciasPersona.Select(c => new { cope_Id = c.cope_Id, per_Id = c.per_Id }).ToList();
                            foreach (var X in Competencias)
                            {
                                if (X.per_Id == tbPersonas.per_Id)
                                    {
                                        var Comp = db.UDP_RRHH_tbCompetenciasPersona_Inactivar(X.cope_Id,"Persona Inactivada",1,DateTime.Now);
                                        foreach(UDP_RRHH_tbCompetenciasPersona_Inactivar_Result cmp in Comp)
                                        {
                                            result = cmp.MensajeError + "";
                                        }
                                    }
                            }
                            var Habilidades = db.tbHabilidadesPersona.Select(h => new { hape_Id = h.hape_Id , per_Id = h.per_Id }).ToList();
                            foreach(var x in Habilidades)
                            {
                                if(x.per_Id == tbPersonas.per_Id)
                                    {
                                        var Habi = db.UDP_RRHH_tbHabilidadesPersona_Inactivar(x.hape_Id,"Personas Inactivada",1,DateTime.Now);
                                        foreach(UDP_RRHH_tbHabilidadesPersona_Inactivar_Result hab in Habi)
                                        {
                                            result = hab.MensajeError + "";
                                        }
                                    }
                            }
                            var Idiomas = db.tbIdiomaPersona.Select(i => new { idpe_Id = i.idpe_Id, per_Id = i.per_Id }).ToList();
                            foreach (var x in Idiomas)
                            {
                                if (x.per_Id == tbPersonas.per_Id)
                                {
                                    var Idim = db.UDP_RRHH_tbIdiomasPersona_Inactivar(x.idpe_Id, "Personas Inactivada", 1, DateTime.Now);
                                    foreach (UDP_RRHH_tbIdiomasPersona_Inactivar_Result idi in Idim)
                                    {
                                        result = idi.MensajeError + "";
                                    }
                                }
                            }
                            var REspeciales = db.tbRequerimientosEspecialesPersona.Select(re => new { rep_Id = re.rep_Id, per_Id = re.per_Id }).ToList();
                            foreach (var x in REspeciales)
                            {
                                if (x.per_Id == tbPersonas.per_Id)
                                {
                                    var ReEs = db.UDP_RRHH_tbRequerimientosEspecialesPersona_Inactivar(x.rep_Id, "Personas Inactivada", 1, DateTime.Now);
                                    foreach (UDP_RRHH_tbRequerimientosEspecialesPersona_Inactivar_Result resp in ReEs)
                                    {
                                        result = resp.MensajeError + "";
                                    }
                                }
                            }
                            var Titulos = db.tbTitulosPersona.Select(t => new { tipe_Id = t.tipe_Id, per_Id = t.per_Id }).ToList();
                            foreach (var x in Titulos)
                            {
                                if (x.per_Id == tbPersonas.per_Id)
                                {
                                    var Titu = db.UDP_RRHH_tbTitulosPersona_Inactivar(x.tipe_Id, "Personas Inactivada", 1, DateTime.Now);
                                    foreach (UDP_RRHH_tbTitulosPersona_Inactivar_Result tit in Titu)
                                    {
                                        result = tit.MensajeError + "";
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    msj = "-2";
                }
            }
            return Json(msj, JsonRequestBehavior.AllowGet);
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
