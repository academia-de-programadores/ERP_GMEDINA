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
        [Display(Name= "Id")]
        public int scan_Id { get; set; }

        [Display(Name = "Persona")]
        public int per_Id { get; set; }

        [Display(Name = "Fase Reclutamiento")]
        public int fare_Id { get; set; }

        public Nullable<System.DateTime> scan_Fecha { get; set; }

        public int rper_Id { get; set; }
        public bool scan_Estado { get; set; }
        public string scan_RazonInactivo { get; set; }
        public int scan_UsuarioCrea { get; set; }
        public System.DateTime scan_FechaCrea { get; set; }
        public Nullable<int> scan_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> scan_FechaModifica { get; set; }

    }
}