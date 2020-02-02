
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_DeduccionesExtrasColaboradores
    {
        public int eqtra_Id { get; set; }
        public string eqtra_codigo { get; set; }
        public string eqtra_Descripcion { get; set; }
        public string eqtra_observacion { get; set; }
        public int emp_Id { get; set; }
        public System.DateTime eqem_Fecha { get; set; }
        public Nullable<decimal> dex_MontoInicial { get; set; }
        public Nullable<decimal> dex_MontoRestante { get; set; }
        public string dex_ObservacionesComentarios { get; set; }
        public int cde_IdDeducciones { get; set; }
        public string cde_DescripcionDeduccion { get; set; }
        public Nullable<decimal> dex_Cuota { get; set; }
        public int dex_UsuarioCrea { get; set; }
        public System.DateTime dex_FechaCrea { get; set; }
        public Nullable<int> dex_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> dex_FechaModifica { get; set; }
        public bool dex_Activo { get; set; }
    }
}
