using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(cAFPRPT))]
    public partial class V_AFP_RPT
    {

    }
    public class cAFPRPT
    {

        [Display(Name = "Codigo Empleado")]
        public int emp_Id { get; set; }

        [Display(Name = "Identidad")]
        public string per_Identidad { get; set; }

        [Display(Name = "Nombre Completo")]
        public string per_Empleado { get; set; }

        [Display(Name = "Departamento")]
        public string depto_descripcion { get; set; }

        [Display(Name = "Área")]
        public string area_Descripcion { get; set; }

        [Display(Name = "Codigo Planilla")]
        public int cpla_IdPlanilla { get; set; }

        [Display(Name = "Tipo Planilla")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]
        public string cpla_DescripcionPlanilla { get; set; }

       
        [Display(Name = "Total")]
        public Nullable<decimal> hipa_AFP{ get; set; }

        [Display(Name = "Fecha Pago")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]
        public System.DateTime hipa_FechaPago { get; set; }
    }
}