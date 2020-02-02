
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbISR
    {
        public int isr_Id { get; set; }
        public decimal isr_RangoInicial { get; set; }
        public decimal isr_RangoFinal { get; set; }
        public decimal isr_Porcentaje { get; set; }
        public int tde_IdTipoDedu { get; set; }
        public int isr_UsuarioCrea { get; set; }
        public System.DateTime isr_FechaCrea { get; set; }
        public Nullable<int> isr_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> isr_FechaModifica { get; set; }
        public bool isr_Activo { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbTipoDeduccion tbTipoDeduccion { get; set; }
    }
}
