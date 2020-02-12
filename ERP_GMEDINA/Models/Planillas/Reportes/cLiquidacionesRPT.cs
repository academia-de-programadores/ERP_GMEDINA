using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cLiquidacionesRPT))]
    public partial class V_Liquidaciones_RPT
    {

    }
    public class cLiquidacionesRPT
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

        [Display(Name = "Fecha Ingreso")]
        public System.DateTime hliq_fechaIngreso { get; set; }

        [Display(Name = "Fecha Liquidacion")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]
        public System.DateTime hliq_fechaLiquidacion { get; set; }

        [Display(Name = "Porcentaje Liquidacion")]
        public int hliq_PorcentajeLiquidacion { get; set; }

        [Display(Name = "Observaciones")]
        public string hliq_Observaciones { get; set; }
    }
}