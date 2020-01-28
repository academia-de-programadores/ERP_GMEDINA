
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbDeduccionInstitucionFinanciera
    {
        public int deif_IdDeduccionInstFinanciera { get; set; }
        public int emp_Id { get; set; }
        public int insf_IdInstitucionFinanciera { get; set; }
        public Nullable<decimal> deif_Monto { get; set; }
        public string deif_Comentarios { get; set; }
        public int cde_IdDeducciones { get; set; }
        public int deif_UsuarioCrea { get; set; }
        public System.DateTime deif_FechaCrea { get; set; }
        public Nullable<int> deif_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> deif_FechaModifica { get; set; }
        public bool deif_Activo { get; set; }
        public Nullable<bool> deif_Pagado { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbCatalogoDeDeducciones tbCatalogoDeDeducciones { get; set; }
        public virtual tbInstitucionesFinancieras tbInstitucionesFinancieras { get; set; }
        public virtual tbEmpleados tbEmpleados { get; set; }
    }
}
