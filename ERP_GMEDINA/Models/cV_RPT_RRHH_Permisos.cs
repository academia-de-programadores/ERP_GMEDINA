using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cV_RPT_RRHH_Permisos))]
    public partial class V_RPT_HistorialPermisos
    {

    }

    public class cV_RPT_RRHH_Permisos
    {
        public int hper_Id { get; set; }
        [Display(Name ="Empleado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        public int emp_Id { get; set; }
        public string Identidad { get; set; }
        public string NombreCompleto { get; set; }
        public int tper_Id { get; set; }
        public string TipoPermiso { get; set; }
        public bool emp_Estado { get; set; }
        [Display(Name = "Fecha Inicio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        public Nullable<System.DateTime> FechaInicio { get; set; }
        [Display(Name = "Fecha Fin")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        public Nullable<System.DateTime> FechaFin { get; set; }
    }
}