using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class ViewModelDecimoTercerMes
    {
        public int emp_Id { get; set; }
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
        public string car_Descripcion { get; set; }
        public string cpla_DescripcionPlanilla { get; set; }
        public string emp_CuentaBancaria { get; set; }
        public Nullable<decimal> dtm_Monto { get; set; }
    }
}