using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cSeleccionCandidatos))]

    public partial class tbSeleccionCandidatos
    {

    }

    public class cSeleccionCandidatos
    { 

        [Display(Name = "Id")]
        public int scan_Id { get; set; }

        [Display(Name = "Persona")]
        public int per_Id { get; set; }

        [Display(Name = "Fecha")]
        public Nullable<System.DateTime> scan_Fecha { get; set; }

        [Display(Name = "Estado")]
        public bool scan_Estado { get; set; }

        [Display(Name = "Razón Inactivo")]
        public string scan_RazonInactivo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int scan_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime scan_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> scan_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> scan_FechaModifica { get; set; }

        [Display(Name = "Fase Reclutamiento")]
        public int fare_Id { get; set; }

        [Display(Name = "Plaza Solicitada")]
        public int req_Id { get; set; }
    }
}