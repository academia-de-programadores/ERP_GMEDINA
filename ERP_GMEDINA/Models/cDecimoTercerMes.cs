using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cDecimoTercerMes))]
    public partial class V_DecimoTercerMes
    {

    }

    public class cDecimoTercerMes
    {
        [Display(Name = "Codigo Empleado")]
        public int emp_Id { get; set; }
        [Display(Name = "Nombre")]
        public string per_Nombres { get; set; }
        [Display(Name = "Apellido")]
        public string per_Apellidos { get; set; }
        [Display(Name = "Cargo")]
        public string car_Descripcion { get; set; }
        [Display(Name = "Tipo de Planilla")]
        public string cpla_DescripcionPlanilla { get; set; }
        [Display(Name = "Cuenta Bancaria")]
        public string emp_CuentaBancaria { get; set; }
        [Display(Name = "Decimo Tercer Mes")]
        public Nullable<decimal> dtm_Monto { get; set; }
    }
}