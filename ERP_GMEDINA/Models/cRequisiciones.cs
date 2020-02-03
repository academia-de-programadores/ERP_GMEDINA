using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cRequisiciones))]
    public partial class tbRequisiciones
    {

    }

    public class cRequisiciones
    {

        [Display(Name = "Numero")]
        public int req_Id { get; set; }
        [Display(Name = "Experiencia")]
        public string req_Experiencia { get; set; }
        [Display(Name = "Sexo")]
        public string req_Sexo { get; set; }
        [Display(Name = "Descripción")]
        public string req_Descripcion { get; set; }
        [Display(Name = "Edad mínima")]
        public int req_EdadMinima { get; set; }
        [Display(Name = "Edad máxima")]
        public int req_EdadMaxima { get; set; }
        [Display(Name = "Estado Civil")]
        public string req_EstadoCivil { get; set; }
        [Display(Name = "Educación superior")]
        public bool req_EducacionSuperior { get; set; }
        [Display(Name = "Temporal")]
        public bool req_Permanente { get; set; }
        [Display(Name = "Duración")]
        public string req_Duracion { get; set; }
        [Display(Name = "Estado")]
        public bool req_Estado { get; set; }
        [Display(Name = "Razon Inactivo")]
        public string req_RazonInactivo { get; set; }
        [Display(Name = "Vacantes")]
        public string req_Vacantes { get; set; }
        [Display(Name = "Nivel educativo")]
        public string req_NivelEducativo { get; set; }
        [Display(Name = "Fecha Requisición")]
        public Nullable<System.DateTime> req_FechaRequisicion { get; set; }
        [Display(Name = "Fecha contratación")]
        public Nullable<System.DateTime> req_FechaContratacion { get; set; }
        [Display(Name = "Usuario crea")]
        public int req_UsuarioCrea { get; set; }
        [Display(Name = "Fecha creacion")]
        public System.DateTime req_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        public Nullable<int> req_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> req_FechaModifica { get; set; }

    }
}