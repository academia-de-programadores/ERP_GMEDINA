
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbDecimoTercerMes
    {
        public int dtm_IdDecimoTercerMes { get; set; }
        public System.DateTime dtm_FechaPago { get; set; }
        public int dtm_UsuarioCrea { get; set; }
        public System.DateTime dtm_FechaCrea { get; set; }
        public Nullable<int> dtm_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> dtm_FechaModifica { get; set; }
        public Nullable<int> emp_Id { get; set; }
        public Nullable<decimal> dtm_Monto { get; set; }
        public string dtm_CodigoPago { get; set; }
        public int cpla_IdPlanilla { get; set; }
    
        public virtual tbEmpleados tbEmpleados { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbCatalogoDePlanillas tbCatalogoDePlanillas { get; set; }
    }
}
