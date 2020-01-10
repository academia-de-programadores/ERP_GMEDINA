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
                            per_Apellidos = p.per_Apellidos,
                            nac_Id = p.tbNacionalidades.nac_Descripcion,
                            per_Edad = p.per_Edad,
                            per_TipoSangre = p.per_TipoSangre,
                            per_Direccion = p.per_Direccion,
                            per_Telefono = p.per_Telefono,
                            per_CorreoElectronico = p.per_CorreoElectronico,
                            per_FechaCrea = p.per_FechaCrea,
                            per_FechaModifica = p.per_FechaModifica,
                            per_UsuarioCrea = p.tbUsuario.usu_Nombres,
                            per_UsuarioModifica = p.tbUsuario.usu_Nombres
                        })
                        .Where(x => x.per_Id == id).ToList();
                    return Json(tbPersonas, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
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
            Sexo.Add(new { Id = "", Descripcion = "**Seleccione una opción**" });
            Sexo.Add(new { Id = "F", Descripcion = "Femenino" });
            Sexo.Add(new { Id = "M", Descripcion = "Masculino" });
            //Ddl EstadoCivil
            var EstadoCivil = new List<object> { };
            EstadoCivil.Add(new { Id = "", Descripcion = "**Seleccione una opción**" });
            EstadoCivil.Add(new { Id = "C", Descripcion = "Casado" });
            EstadoCivil.Add(new { Id = "D", Descripcion = "Divorciado" });
            EstadoCivil.Add(new { Id = "S", Descripcion = "Soltero" });
            EstadoCivil.Add(new { Id = "U", Descripcion = "Union Libres" });
            EstadoCivil.Add(new { Id = "V", Descripcion = "Viudo" });
            //ddl TipoSangre
            var TipoSangre = new List<object> { };
            TipoSangre.Add(new { Id = "", Descripcion = "**Seleccione una opción**" });
            TipoSangre.Add(new { Id = "A+", Descripcion = "A+" });
            TipoSangre.Add(new { Id = "A-", Descripcion = "A-" });
            TipoSangre.Add(new { Id = "B+", Descripcion = "B+" });
            TipoSangre.Add(new { Id = "B-", Descripcion = "B-" });
            TipoSangre.Add(new { Id = "O+", Descripcion = "O+" });
            TipoSangre.Add(new { Id = "O-", Descripcion = "O-" });
            TipoSangre.Add(new { Id = "AB+", Descripcion = "AB+" });
            TipoSangre.Add(new { Id = "AB-", Descripcion = "AB-" });

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
                    var Competencias = db.tbCompetenciasPersona.Select(c => new { cope_Id = c.cope_Id, per_Id = c.per_Id , Descripcion = c.tbCompetencias.comp_Descripcion , Estado = c.cope_Estado}).Where(c => c.Estado == true).ToList();
                    foreach (var X in Competencias)
                    {
                        if(X.per_Id == id)
                            lista.Add(new V_tbPersonas { per_Id = X.per_Id, Relacion_Id = X.cope_Id, Descripcion = X.Descripcion, Relacion = "Competencias" });
                    }
                    var Habilidades = db.tbHabilidadesPersona.Select(h => new { hape_Id = h.hape_Id, per_Id = h.per_Id, Descripcion = h.tbHabilidades.habi_Descripcion, Estado = h.hape_Estado }).Where(h => h.Estado == true).ToList();
                    foreach (var X in Habilidades)
                    {
                        if(X.per_Id == id)
                            lista.Add(new V_tbPersonas { per_Id = X.per_Id, Relacion_Id = X.hape_Id, Descripcion = X.Descripcion, Relacion = "Habilidades" });
                    }
                    var Idiomas = db.tbIdiomaPersona.Select(i => new { idpe_Id = i.idpe_Id, per_Id = i.per_Id, Descripcion = i.tbIdiomas.idi_Descripcion, Estado = i.idpe_Estado }).Where(i => i.Estado == true).ToList();
                    foreach (var X in Idiomas)
                    {
                        if (X.per_Id == id)
                            lista.Add(new V_tbPersonas { per_Id = X.per_Id.Value, Relacion_Id = X.idpe_Id, Descripcion = X.Descripcion, Relacion = "Idiomas" });
                    }
                    var ReEspeciales = db.tbRequerimientosEspecialesPersona.Select(rep => new { rep_Id = rep.rep_Id, per_Id = rep.per_Id, Descripcion = rep.tbRequerimientosEspeciales.resp_Descripcion, Estado = rep.rep_Estado }).Where(rep => rep.Estado == true).ToList();
                    foreach (var X in ReEspeciales)
                    {
                        if (X.per_Id == id)
                            lista.Add(new V_tbPersonas { per_Id = X.per_Id, Relacion_Id = X.rep_Id, Descripcion = X.Descripcion, Relacion = "Requerimientos_Especiales" });
                    }
                    var Titulos = db.tbTitulosPersona.Select(t => new { tipe_Id = t.tipe_Id, per_Id = t.per_Id, Descripcion = t.tbTitulos.titu_Descripcion, Estado = t.tipe_Estado }).Where(t => t.Estado == true).ToList();
                    foreach (var X in Titulos)
                    {
                        if (X.per_Id == id)
                            lista.Add(new V_tbPersonas { per_Id = X.per_Id, Relacion_Id = X.tipe_Id, Descripcion = X.Descripcion, Relacion = "Titulos" });
                    }
                        if (lista.Count == 0)
                            lista.Add(new V_tbPersonas { per_Id = id.Value, Relacion_Id = 0, Descripcion = "", Relacion = "" });

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
        public ActionResult DualListBoxData(int? id)
        {
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                        List<DatosProfessionalesEdit> Data = new List<DatosProfessionalesEdit> { };
                        var lista = db.V_DatosProfesionalesP.Select(tabla => new { TipoDato = tabla.TipoDato, Id = tabla.Data_Id, Descripcion = tabla.Descripcion }).ToList();
                        foreach(var X in lista)
                        {
                            switch (X.TipoDato)
                            {
                                case "C":
                                    var Comp = db.tbCompetenciasPersona.Select(c => new { comp_Id = c.comp_Id , Descripcion = c.tbCompetencias.comp_Descripcion , per_Id = c.per_Id , Estado = c.cope_Estado}).Where(c => c.per_Id == id && c.Estado == true && c.comp_Id == X.Id).ToList();
                                    foreach(var cmp in Comp)
                                    {
                                            Data.Add(new DatosProfessionalesEdit { Id = cmp.comp_Id ,Descripcion = cmp.Descripcion,TipoDato = "C",Seleccionado = 1 });
                                    }
                                    if(Comp.Count == 0)
                                        Data.Add(new DatosProfessionalesEdit { Id = X.Id, Descripcion = X.Descripcion, TipoDato = "C", Seleccionado = 0 });
                                    break;

                                case "H":
                                    var Hab = db.tbHabilidadesPersona.Select(h => new { habi_Id = h.habi_Id, Descripcion = h.tbHabilidades.habi_Descripcion, per_Id = h.per_Id , Estado = h.hape_Estado }).Where(h => h.per_Id == id && h.Estado == true && h.habi_Id == X.Id).ToList();
                                    foreach (var habi in Hab)
                                    {
                                        if (X.Id == habi.habi_Id)
                                            Data.Add(new DatosProfessionalesEdit { Id = habi.habi_Id, Descripcion = habi.Descripcion, TipoDato = "H", Seleccionado = 1 });
                                    }
                                    if(Hab.Count == 0)
                                        Data.Add(new DatosProfessionalesEdit { Id = X.Id, Descripcion = X.Descripcion, TipoDato = "H", Seleccionado = 0 });
                                    break;

                                case "I":
                                    var Idi = db.tbIdiomaPersona.Select(i => new { idi_Id = i.idi_Id, Descripcion = i.tbIdiomas.idi_Descripcion, per_Id = i.per_Id , Estado = i.idpe_Estado }).Where(i => i.per_Id == id && i.Estado == true && i.idi_Id == X.Id).ToList();
                                    foreach (var idm in Idi)
                                    {
                                        if (X.Id == idm.idi_Id)
                                            Data.Add(new DatosProfessionalesEdit { Id = idm.idi_Id.Value, Descripcion = idm.Descripcion, TipoDato = "I", Seleccionado = 1 });
                                    }
                                    if(Idi.Count == 0)
                                        Data.Add(new DatosProfessionalesEdit { Id = X.Id, Descripcion = X.Descripcion, TipoDato = "I", Seleccionado = 0 });
                                    break;

                                case "T":
                                    var Tit = db.tbTitulosPersona.Select(t => new { titu_Id = t.titu_Id, Descripcion = t.tbTitulos.titu_Descripcion, per_Id = t.per_Id , Estado = t.tipe_Estado}).Where(t => t.per_Id == id && t.Estado == true && t.titu_Id == X.Id).ToList();
                                    foreach (var Titu in Tit)
                                    {
                                        if (X.Id == Titu.titu_Id)
                                            Data.Add(new DatosProfessionalesEdit { Id = Titu.titu_Id, Descripcion = Titu.Descripcion, TipoDato = "T", Seleccionado = 1 });
                                    }
                                    if(Tit.Count == 0)
                                        Data.Add(new DatosProfessionalesEdit { Id = X.Id, Descripcion = X.Descripcion, TipoDato = "T", Seleccionado = 0 });
                                    break;

                                case "R":
                                    var Reqs = db.tbRequerimientosEspecialesPersona.Select(re => new { resp_Id = re.resp_Id, Descripcion = re.tbRequerimientosEspeciales.resp_Descripcion, per_Id = re.per_Id , Estado = re.rep_Estado}).Where(re => re.per_Id == id && re.Estado == true && re.resp_Id == X.Id).ToList();
                                    foreach (var ReEs in Reqs)
                                    {
                                        if (X.Id == ReEs.resp_Id)
                                            Data.Add(new DatosProfessionalesEdit { Id = ReEs.resp_Id, Descripcion = ReEs.Descripcion, TipoDato = "R", Seleccionado = 1 });
                                    }
                                    if(Reqs.Count == 0)
                                        Data.Add(new DatosProfessionalesEdit { Id = X.Id, Descripcion = X.Descripcion, TipoDato = "R", Seleccionado = 0 });
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
                        var List = db.UDP_RRHH_tbPersonas_Insert1(tbPersonas.per_Identidad, tbPersonas.per_Nombres, tbPersonas.per_Apellidos, tbPersonas.per_FechaNacimiento, tbPersonas.per_Sexo, tbPersonas.nac_Id, tbPersonas.per_Direccion, tbPersonas.per_Telefono, tbPersonas.per_CorreoElectronico, tbPersonas.per_EstadoCivil, tbPersonas.per_TipoSangre, 1, DateTime.Now);

                        foreach (UDP_RRHH_tbPersonas_Insert1_Result item in List)
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
                                for (int i = 0; i < DatosProfesionalesArray.Habilidades.Length; i++)
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
        // GET: Areas/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                using (db = new ERP_GMEDINAEntities())
                {
                    var Sexo = new List<object> { };
                    Sexo.Add(new { Id = "", Descripcion = "**Seleccione una opción**"});
                    Sexo.Add(new { Id = "F", Descripcion = "Femenino"});
                    Sexo.Add(new { Id = "M", Descripcion = "Masculino"});
                    //Ddl EstadoCivil
                    var EstadoCivil = new List<object> { };
                    EstadoCivil.Add(new { Id = "", Descripcion = "**Seleccione una opción**"});
                    EstadoCivil.Add(new { Id = "C", Descripcion = "Casado" });
                    EstadoCivil.Add(new { Id = "D", Descripcion = "Divorciado" });
                    EstadoCivil.Add(new { Id = "S", Descripcion = "Soltero" });
                    EstadoCivil.Add(new { Id = "U", Descripcion = "Union Libres" });
                    EstadoCivil.Add(new { Id = "V", Descripcion = "Viudo" });
                    //ddl TipoSangre
                    var TipoSangre = new List<object> { };
                    TipoSangre.Add(new { Id = "", Descripcion = "**Seleccione una opción**"});
                    TipoSangre.Add(new { Id = "A+", Descripcion = "A+" });
                    TipoSangre.Add(new { Id = "A-", Descripcion = "A-" });
                    TipoSangre.Add(new { Id = "B+", Descripcion = "B+" });
                    TipoSangre.Add(new { Id = "B-", Descripcion = "B-" });
                    TipoSangre.Add(new { Id = "O+", Descripcion = "O+" });
                    TipoSangre.Add(new { Id = "O-", Descripcion = "O-" });
                    TipoSangre.Add(new { Id = "AB+", Descripcion = "AB+" });
                    TipoSangre.Add(new { Id = "AB-", Descripcion = "AB-" });

                    ViewBag.per_TipoSangre = new SelectList(TipoSangre, "Id", "Descripcion");
                    ViewBag.per_EstadoCivil = new SelectList(EstadoCivil, "Id", "Descripcion");
                    ViewBag.per_Sexo = new SelectList(Sexo, "Id", "Descripcion");
                    //Nacionalidades
                    List<tbNacionalidades> Nacionalidades = new List<tbNacionalidades> { };
                    ViewBag.nac_Id = new SelectList(Nacionalidades, "nac_Id", "nac_Descripcion");
                   
                    if (id == null)
                    {
                        return View("Edit");
                    }
                    else
                    {
                        var tbPersonas = db.tbPersonas
                        .Select(
                        p => new
                        {
                            per_Id = p.per_Id,
                            per_Identidad = p.per_Identidad,
                            per_Nombres = p.per_Nombres,
                            per_Apellidos = p.per_Apellidos,
                            per_FechaNacimiento = p.per_FechaNacimiento,
                            per_Sexo = p.per_Sexo,
                            per_Edad = p.per_Edad,
                            nac_Id = p.nac_Id,
                            per_Direccion = p.per_Direccion,
                            per_Telefono = p.per_Telefono,
                            per_CorreoElectronico = p.per_CorreoElectronico,
                            per_EstadoCivil = p.per_EstadoCivil,
                            per_TipoSangre = p.per_TipoSangre,

                        })
                        .Where(x => x.per_Id == id).ToList();
                        return Json(tbPersonas, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                return Json("-2", JsonRequestBehavior.AllowGet);
            }
        }
        // POST: Areas/Edit/5
        [HttpPost]
        public ActionResult Edit(Personas tbPersonas, DatosProfesionalesArray DatosProfesionalesArray)
        {
            var msj = "";
            using (db = new ERP_GMEDINAEntities())
            {
                try
                {
                    string ResultI = "";
                    string ResultE = "";
                    var _list = db.UDP_RRHH_tbPersonas_Update1(tbPersonas.per_Id,tbPersonas.per_Identidad,tbPersonas.per_Nombres,tbPersonas.per_Apellidos,tbPersonas.per_FechaNacimiento,tbPersonas.per_Sexo,tbPersonas.nac_Id,tbPersonas.per_Direccion,tbPersonas.per_Telefono,tbPersonas.per_CorreoElectronico,tbPersonas.per_EstadoCivil,tbPersonas.per_TipoSangre,1,DateTime.Now);
                    foreach(UDP_RRHH_tbPersonas_Update1_Result Update in _list)
                    {
                        msj = Update.MensajeError + "";
                        if(msj != "")
                        {
                            var lista = db.V_DatosProfesionalesP.Select(tabla => new { TipoDato = tabla.TipoDato, Id = tabla.Data_Id, Descripcion = tabla.Descripcion }).ToList();
                            foreach (var X in lista)
                            {
                                string Nuevo = null;
                                switch (X.TipoDato)
                                {
                                    case "C":
                                        var CompV = db.tbCompetenciasPersona.Select(c => new { comp_Id = c.comp_Id, Descripcion = c.tbCompetencias.comp_Descripcion, per_Id = c.per_Id, cope_Id = c.cope_Id ,Estado = c.cope_Estado }).Where(c => c.per_Id == tbPersonas.per_Id && c.Estado == true && c.comp_Id == X.Id).ToList();
                                        if (DatosProfesionalesArray.Competencias != null)
                                        {
                                            foreach (var x in DatosProfesionalesArray.Competencias)
                                            {
                                                if (x == X.Id)
                                                    Nuevo = "1";
                                                else if (Nuevo != "1")
                                                    Nuevo = null;
                                            }
                                        }
                                        if (CompV.Count == 0 && Nuevo == "1")
                                        {
                                            var Competencias = db.UDP_RRHH_tbCompetenciasPersona_Insert(tbPersonas.per_Id, X.Id, 1, DateTime.Now);
                                            foreach (UDP_RRHH_tbCompetenciasPersona_Insert_Result Com in Competencias)
                                            {
                                                ResultI = Com.MensajeError + "";
                                            }
                                        }
                                        if (CompV.Count >= 1 && Nuevo == null)
                                        {
                                            foreach (var c in CompV)
                                            {
                                                var Competencias = db.UDP_RRHH_tbCompetenciasPersona_Inactivar(c.cope_Id, "Persona Editada", 1, DateTime.Now);
                                                foreach (UDP_RRHH_tbCompetenciasPersona_Inactivar_Result Com in Competencias)
                                                {
                                                    ResultE = Com.MensajeError + "";
                                                }
                                            }
                                        }
                                        break;
                                    case "H":
                                        var habV = db.tbHabilidadesPersona.Select(h => new { habi_Id = h.habi_Id, Descripcion = h.tbHabilidades.habi_Descripcion, per_Id = h.per_Id, hape_Id = h.hape_Id , Estado = h.hape_Estado}).Where(h => h.per_Id == tbPersonas.per_Id && h.Estado == true && h.habi_Id == X.Id).ToList();
                                        if (DatosProfesionalesArray.Habilidades != null)
                                        {
                                            foreach (var x in DatosProfesionalesArray.Habilidades)
                                            {
                                                if (x == X.Id)
                                                    Nuevo = "1";
                                                else if (Nuevo != "1")
                                                    Nuevo = null;
                                            }
                                        }
                                        if (habV.Count == 0 && Nuevo == "1")
                                        {
                                            var Habilidades = db.UDP_RRHH_tbHabilidadesPersona_Insert(tbPersonas.per_Id, X.Id, 1, DateTime.Now);
                                            foreach (UDP_RRHH_tbHabilidadesPersona_Insert_Result hab in Habilidades)
                                            {
                                                ResultI = hab.MensajeError + "";
                                            }
                                        }
                                        if (habV.Count >= 1 && Nuevo == null)
                                        {
                                            foreach (var h in habV)
                                            {
                                                var Habilidades = db.UDP_RRHH_tbHabilidadesPersona_Inactivar(h.hape_Id, "Persona Editada", 1, DateTime.Now);
                                                foreach (UDP_RRHH_tbHabilidadesPersona_Inactivar_Result Com in Habilidades)
                                                {
                                                    ResultE = Com.MensajeError + "";
                                                }
                                            }
                                        }
                                        break;
                                    case "I":
                                        var IdiV = db.tbIdiomaPersona.Select(i => new { idi_Id = i.idi_Id, Descripcion = i.tbIdiomas.idi_Descripcion, per_Id = i.per_Id, idpe_Id = i.idpe_Id , Estado = i.idpe_Estado }).Where(i => i.per_Id == tbPersonas.per_Id && i.Estado == true && i.idi_Id == X.Id).ToList();
                                        if (DatosProfesionalesArray.Idiomas != null)
                                        {
                                            foreach (var x in DatosProfesionalesArray.Idiomas)
                                            {
                                                if (x == X.Id)
                                                    Nuevo = "1";
                                                else if (Nuevo != "1")
                                                    Nuevo = null;
                                            }
                                        }
                                        if (IdiV.Count == 0 && Nuevo == "1")
                                        {
                                            var Idiomas = db.UDP_RRHH_tbIdiomasPersona_Insert(tbPersonas.per_Id, X.Id, 1, DateTime.Now);
                                            foreach (UDP_RRHH_tbIdiomasPersona_Insert_Result idi in Idiomas)
                                            {
                                                ResultI = idi.MensajeError + "";
                                            }
                                        }
                                        if (IdiV.Count >= 1 && Nuevo == null)
                                        {
                                            foreach (var i in IdiV)
                                            {
                                                var Idiomas = db.UDP_RRHH_tbIdiomasPersona_Inactivar(i.idpe_Id, "Persona Editada", 1, DateTime.Now);
                                                foreach (UDP_RRHH_tbIdiomasPersona_Inactivar_Result idio in Idiomas)
                                                {
                                                    ResultE = idio.MensajeError + "";
                                                }
                                            }
                                        }
                                        break;
                                    case "T":
                                        var TitV = db.tbTitulosPersona.Select(t => new { titu_Id = t.titu_Id, Descripcion = t.tbTitulos.titu_Descripcion, per_Id = t.per_Id, tipe_Id = t.tipe_Id , Estado = t.tipe_Estado}).Where(t => t.per_Id == tbPersonas.per_Id && t.Estado == true && t.titu_Id == X.Id).ToList();
                                        if (DatosProfesionalesArray.Titulos != null)
                                        {
                                            foreach (var x in DatosProfesionalesArray.Titulos)
                                            {
                                                if (x == X.Id)
                                                    Nuevo = "1";
                                                else if (Nuevo != "1")
                                                    Nuevo = null;
                                            }
                                        }
                                        if (TitV.Count == 0 && Nuevo == "1")
                                        {
                                            var Titulos = db.UDP_RRHH_tbTitulosPersona_Insert(tbPersonas.per_Id, X.Id, 2019, 1, DateTime.Now);
                                            foreach (UDP_RRHH_tbTitulosPersona_Insert_Result titu in Titulos)
                                            {
                                                ResultI = titu.MensajeError + "";
                                            }
                                        }
                                        if (TitV.Count >= 1 && Nuevo == null)
                                        {
                                            foreach (var t in TitV)
                                            {
                                                var Titulos = db.UDP_RRHH_tbTitulosPersona_Inactivar(t.tipe_Id, "Persona Editada", 1, DateTime.Now);
                                                foreach (UDP_RRHH_tbTitulosPersona_Inactivar_Result titu in Titulos)
                                                {
                                                    ResultE = titu.MensajeError + "";
                                                }
                                            }
                                        }
                                        break;
                                    case "R":
                                        var RepV = db.tbRequerimientosEspecialesPersona.Select(r => new { resp_Id = r.resp_Id, Descripcion = r.tbRequerimientosEspeciales.resp_Descripcion, per_Id = r.per_Id, rep_Id = r.rep_Id , Estado = r.rep_Estado }).Where(r => r.per_Id == tbPersonas.per_Id && r.Estado == true && r.resp_Id == X.Id).ToList();
                                        if (DatosProfesionalesArray.ReqEspeciales != null)
                                        {
                                            foreach (var x in DatosProfesionalesArray.Titulos)
                                            {
                                                if (x == X.Id)
                                                    Nuevo = "1";
                                                else if (Nuevo != "1")
                                                    Nuevo = null;
                                            }
                                        }
                                        if (RepV.Count == 0 && Nuevo == "1")
                                        {
                                            var REspeciales = db.UDP_RRHH_tbRequerimientosEspecialesPersona_Insert(tbPersonas.per_Id, X.Id, 1, DateTime.Now);
                                            foreach (UDP_RRHH_tbRequerimientosEspecialesPersona_Insert_Result resp in REspeciales)
                                            {
                                                ResultI = resp.MensajeError + "";
                                            }
                                        }
                                        if (RepV.Count >= 1 && Nuevo == null)
                                        {
                                            foreach (var r in RepV)
                                            {
                                                var ReqEspeciales = db.UDP_RRHH_tbRequerimientosEspecialesPersona_Inactivar(r.rep_Id, "Persona Editada", 1, DateTime.Now);
                                                foreach (UDP_RRHH_tbRequerimientosEspecialesPersona_Inactivar_Result resp in ReqEspeciales)
                                                {
                                                    ResultE = resp.MensajeError + "";
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    
                }
                catch(Exception ex)
                {
                    ex.Message.ToString();
                    msj = "-2";
                }
            }
            return Json(msj, JsonRequestBehavior.AllowGet);
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
                    var _list = db.UDP_RRHH_tbPersonas_Inactivar(tbPersonas.per_Id,"",1,DateTime.Now);
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