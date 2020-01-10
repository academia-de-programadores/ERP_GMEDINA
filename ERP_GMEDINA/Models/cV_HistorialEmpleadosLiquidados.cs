using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cV_HistorialEmpleadosLiquidados))]
    public partial class V_HistorialEmpleadosLiquidados
    {

    }

    public partial class cV_HistorialEmpleadosLiquidados
    {
        [Display(Name = "Número")]
        public Nullable<int> hdli_Id { get; set; }

        [Display(Name = "ID de Empleado")]
        public Nullable<int> emp_Id { get; set; }

        [Display(Name = "Nombre")]
        public string per_Nombres { get; set; }

        [Display(Name = "Apellido")]
        public string per_Apellidos { get; set; }

        [Display(Name = "Salario Ordinario Mensual")]
        public Nullable<decimal> hdli_SalarioOrdinarioMensual_Liq { get; set; }

        [Display(Name = "Salario Promedio Mensual")]
        public Nullable<decimal> hdli_SalarioPromedioMensual_Liql { get; set; }

        [Display(Name = "ID Motivo De Liquidación")]
        public int moli_IdMotivo { get; set; }

        [Display(Name = "Razón De Liquidación")]
        public string moli_Descripcion { get; set; }

        [Display(Name = "Total")]
        public Nullable<decimal> hdli_MontoTotalLiquidacion { get; set; }
    }
}