using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cInstitucionesFinancierasRPT))]
    public partial class V_ReporteInstitucionesFinancieras_RPT
    {

    }
    public class cInstitucionesFinancierasRPT
    {        
        public int deif_IdDeduccionInstFinanciera { get; set; }
        public int emp_Id { get; set; }
        public int per_Id { get; set; }
        public string per_Nombres { get; set; }
        [Display(Name = "Código Institución Financiera")]
        [Required(ErrorMessage = "Campo {0} requerido.")]
        public int insf_IdInstitucionFinanciera { get; set; }
        public string insf_DescInstitucionFinanc { get; set; }
        public Nullable<decimal> deif_Monto { get; set; }
        public string deif_Comentarios { get; set; }
        public string deif_Pagado { get; set; }
        public string per_Apellidos { get; set; }
        [Display(Name = "Código Planilla")]
        [Required(ErrorMessage = "Campo {0} requerido.")]
        public int cpla_IdPlanilla { get; set; }
        public string cpla_DescripcionPlanilla { get; set; }
        [Display(Name = "Fecha Crea")]
        [Required(ErrorMessage = "Campo {0} requerido.")]
        public Nullable<System.DateTime> deif_FechaCrea { get; set; }
    }
}