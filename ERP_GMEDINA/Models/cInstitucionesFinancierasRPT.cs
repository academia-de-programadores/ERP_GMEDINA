using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cInstitucionesFinancierasRPT))]
    public partial class V_InstitucionesFinancieras_RPT
    {

    }
    public class cInstitucionesFinancierasRPT
    {

        public int emp_Id { get; set; }


        public string per_Identidad { get; set; }


        public string per_Nombres { get; set; }


        public string per_Apellidos { get; set; }


        public string depto_descripcion { get; set; }


        public string area_Descripcion { get; set; }



        public int cpla_IdPlanilla { get; set; }

        [Display(Name = "Tipo Planilla")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]
        public string cpla_DescripcionPlanilla { get; set; }


        public int cde_IdDeducciones { get; set; }



        public string cde_DescripcionDeduccion { get; set; }


        public Nullable<decimal> hidp_Total { get; set; }


        [Display(Name = "Fecha Pago")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]
        public System.DateTime hipa_FechaPago { get; set; }
    }
}