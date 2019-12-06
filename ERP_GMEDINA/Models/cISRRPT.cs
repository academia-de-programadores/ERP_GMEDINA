using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(cISRRPT))]
    public partial class V_ISR_RPT
    {

    }
    public class cISRRPT
    {
        [Display(Name = "Codigo Empleado")]
        public int emp_Id { get; set; }

        public string per_Identidad { get; set; }

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }

        public int depto_Id { get; set; }

        [Display(Name = "Departamento")]
        public string depto_descripcion { get; set; }

        public int area_Id { get; set; }

        [Display(Name = "Area")]
        public string area_Descripcion { get; set; }

        public int cpla_IdPlanilla { get; set; }

        [Display(Name = "Planilla")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]
        public string cpla_DescripcionPlanilla { get; set; }

        [Display(Name = "Total ISR")]
        public decimal hipa_TotalISR { get; set; }

        [Display(Name = "Sueldo Neto")]
        public Nullable<decimal> hipa_SueldoNeto { get; set; }

        [Display(Name = "Fecha Pago")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]
        public System.DateTime hipa_FechaPago { get; set; }
    }
}