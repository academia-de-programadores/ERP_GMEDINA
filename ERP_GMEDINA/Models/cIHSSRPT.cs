using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cIHSSRPT))]
    public partial class V_IHSS_RPT
    {

    }
    public class cIHSSRPT
    {
        [Display(Name = "Codigo Empleado")]
        public int emp_Id { get; set; }

        [Display(Name = "Identidad")]
        public string per_Identidad { get; set; }

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }

        [Display(Name = "Departamento")]
        public string depto_descripcion { get; set; }

        [Display(Name = "Area")]
        public string area_Descripcion { get; set; }

        [Display(Name = "Codigo Planilla")]
        public int cpla_IdPlanilla { get; set; }

        [Display(Name = "Planilla")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]
        public string cpla_DescripcionPlanilla { get; set; }

        [Display(Name = "Codigo Deducciones")]
        public int cde_IdDeducciones { get; set; }

        [Display(Name = "Deducción")]
        public string cde_DescripcionDeduccion { get; set; }

        [Display(Name = "Total IHSS")]
        public Nullable<decimal> hidp_Total { get; set; }

        [Display(Name = "Fecha Pago")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]
        public System.DateTime hipa_FechaPago { get; set; }
    }
}