
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbHistorialDeIngresosPago
    {
        public int hip_IdHistorialDeIngresosPago { get; set; }
        public int hipa_IdHistorialDePago { get; set; }
        public System.DateTime hip_FechaInicio { get; set; }
        public System.DateTime hip_FechaFinal { get; set; }
        public int hip_UnidadesPagar { get; set; }
        public int hip_MedidaUnitaria { get; set; }
        public Nullable<decimal> hip_TotalPagar { get; set; }
        public int cin_IdIngreso { get; set; }
        public int hip_UsuarioCrea { get; set; }
        public System.DateTime hip_FechaCrea { get; set; }
        public Nullable<int> hip_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> hip_FechaModifica { get; set; }
    
        public virtual tbCatalogoDeIngresos tbCatalogoDeIngresos { get; set; }
        public virtual tbHistorialDePago tbHistorialDePago { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}
