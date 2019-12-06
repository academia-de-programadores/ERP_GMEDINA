using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(cGeneralTotalesRPT))]
    public partial class V_GeneralTotales_RPT
    {

    }

    public class cGeneralTotalesRPT
    {

        [Display(Name = "Fecha Pago")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]
        public System.DateTime hipa_FechaPago { get; set; }

        [Display(Name = "Codigo Planilla")]
        public int cpla_IdPlanilla { get; set; }

        [Display(Name = "Tipo Planilla")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]
        public string cpla_DescripcionPlanilla { get; set; }

        [Display(Name = "Total ISR")]
        public Nullable<decimal> cde_TotalISR { get; set; }

        [Display(Name = "Total AFP")]
        public Nullable<decimal> cde_TotalAFP { get; set; }

        [Display(Name = "Total IHSS")]
        public Nullable<decimal> cde_TotalIHSS { get; set; }

        [Display(Name = "Total RAP")]
        public Nullable<decimal> cde_TotalRAP { get; set; }

        [Display(Name = "Total INFOP")]
        public Nullable<decimal> cde_TotalINFOP { get; set; }

        [Display(Name = "Otras Deducciones")]
        public Nullable<decimal> cde_OtrasDeducciones { get; set; }

    }
}