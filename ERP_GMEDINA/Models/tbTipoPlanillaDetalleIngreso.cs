
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbTipoPlanillaDetalleIngreso
    {
        public int tpdi_IdDetallePlanillaIngreso { get; set; }
        public int cin_IdIngreso { get; set; }
        public int cpla_IdPlanilla { get; set; }
        public int tpdi_UsuarioCrea { get; set; }
        public System.DateTime tpdi_FechaCrea { get; set; }
        public Nullable<int> tpdi_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tpdi_FechaModifica { get; set; }
        public bool tpdi_Activo { get; set; }
    
        public virtual tbCatalogoDeIngresos tbCatalogoDeIngresos { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbCatalogoDePlanillas tbCatalogoDePlanillas { get; set; }
    }
}
