
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbDecimoCuartoMes
    {
        public System.DateTime dcm_FechaPago { get; set; }
        public int dcm_UsuarioCrea { get; set; }
        public System.DateTime dcm_FechaCrea { get; set; }
        public Nullable<int> dcm_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> dcm_FechaModifica { get; set; }
        public int emp_Id { get; set; }
        public Nullable<decimal> dcm_Monto { get; set; }
        public string dcm_CodigoPago { get; set; }
        public int dcm_IdDecimoCuartoMes { get; set; }
        public int cpla_IdPlanilla { get; set; }
    
        public virtual tbEmpleados tbEmpleados { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbCatalogoDePlanillas tbCatalogoDePlanillas { get; set; }
    }
}
