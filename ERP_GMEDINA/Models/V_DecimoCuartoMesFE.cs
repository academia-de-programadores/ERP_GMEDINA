
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_DecimoCuartoMesFE
    {
        public int emp_Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cargo { get; set; }
        public string Planilla { get; set; }
        public string CuentaBancaria { get; set; }
        public Nullable<decimal> Monto { get; set; }
        public int hipa_Anio { get; set; }
    }
}
