
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbPorcentajeMotivoLiquidacion
    {
        public int pml_IdPorcentajeMotivoLiquidacion { get; set; }
        public Nullable<int> moli_IdMotivo { get; set; }
        public Nullable<decimal> pml_Preaviso { get; set; }
        public Nullable<decimal> pml_Cesantia { get; set; }
        public Nullable<decimal> pml_DecimoTercerProp { get; set; }
        public Nullable<decimal> pml_DecimoCuartoProp { get; set; }
        public Nullable<decimal> pml_Vacaciones { get; set; }
        public Nullable<int> pml_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> pml_FechaCrea { get; set; }
        public Nullable<int> pml_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> pml_FechaModifica { get; set; }
        public Nullable<bool> pml_Activo { get; set; }
        public string pml_RazonInactivo { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbMotivoLiquidacion tbMotivoLiquidacion { get; set; }
    }
}
