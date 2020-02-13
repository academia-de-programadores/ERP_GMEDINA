
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbPagoDeCesantiaDetalle
    {
        public int pdcd_IdCesantiaDetalle { get; set; }
        public int emp_Id { get; set; }
        public decimal pdcd_TotalCesantiaColaborador { get; set; }
        public int pdce_IdCesantiaEncabezado { get; set; }
        public int pdcd_DiasPagados { get; set; }
        public decimal pdcd_ConSueldoBruto { get; set; }
        public int pdcd_UsuarioCrea { get; set; }
        public System.DateTime pdcd_FechaCrea { get; set; }
        public Nullable<int> pdcd_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> pdcd_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbPagoDeCesantiaEncabezado tbPagoDeCesantiaEncabezado { get; set; }
    }
}
