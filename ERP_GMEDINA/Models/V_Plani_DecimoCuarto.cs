
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_Plani_DecimoCuarto
    {
        public int dcm_IdDecimoCuartoMes { get; set; }
        public int emp_Id { get; set; }
        public string NombreCompleto { get; set; }
        public string area_Descripcion { get; set; }
        public System.DateTime dcm_FechaPago { get; set; }
        public Nullable<decimal> dcm_Monto { get; set; }
    }
}
