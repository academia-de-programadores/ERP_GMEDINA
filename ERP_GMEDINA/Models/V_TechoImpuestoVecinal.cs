
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_TechoImpuestoVecinal
    {
        public int timv_IdTechoImpuestoVecinal { get; set; }
        public string mun_Codigo { get; set; }
        public string mun_Nombre { get; set; }
        public Nullable<int> tde_IdTipoDedu { get; set; }
        public string tde_Descripcion { get; set; }
        public Nullable<decimal> timv_RangoInicio { get; set; }
        public Nullable<decimal> timv_RangoFin { get; set; }
        public decimal timv_Rango { get; set; }
        public decimal timv_Impuesto { get; set; }
        public int timv_UsuarioCrea { get; set; }
        public System.DateTime timv_FechaCrea { get; set; }
        public Nullable<int> timv_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> timv_FechaModifica { get; set; }
        public bool timv_Activo { get; set; }
    }
}
