
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbDevolucionDetalle
    {
        public int devd_Id { get; set; }
        public int dev_Id { get; set; }
        public string prod_Codigo { get; set; }
        public decimal devd_CantidadProducto { get; set; }
        public string devd_Descripcion { get; set; }
        public decimal devd_Monto { get; set; }
        public int devd_UsuarioCrea { get; set; }
        public System.DateTime devd_FechaCrea { get; set; }
        public Nullable<int> devd_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> devd_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbProducto tbProducto { get; set; }
        public virtual tbDevolucion tbDevolucion { get; set; }
    }
}