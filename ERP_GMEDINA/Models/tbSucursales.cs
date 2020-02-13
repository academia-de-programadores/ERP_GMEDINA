
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbSucursales
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbSucursales()
        {
            this.tbUsuario = new HashSet<tbUsuario>();
            this.tbParametro = new HashSet<tbParametro>();
            this.tbAreas = new HashSet<tbAreas>();
            this.tbCaja = new HashSet<tbCaja>();
            this.tbCuponDescuento = new HashSet<tbCuponDescuento>();
            this.tbNotaCredito = new HashSet<tbNotaCredito>();
            this.tbFactura = new HashSet<tbFactura>();
            this.tbPedido = new HashSet<tbPedido>();
        }
    
        public int suc_Id { get; set; }
        public int empr_Id { get; set; }
        public string mun_Codigo { get; set; }
        public int bod_Id { get; set; }
        public int pemi_Id { get; set; }
        public string suc_Descripcion { get; set; }
        public string suc_Correo { get; set; }
        public string suc_Direccion { get; set; }
        public string suc_Telefono { get; set; }
        public bool suc_Estado { get; set; }
        public string suc_RazonInactivo { get; set; }
        public int suc_UsuarioCrea { get; set; }
        public System.DateTime suc_FechaCrea { get; set; }
        public Nullable<int> suc_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> suc_FechaModifica { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbUsuario> tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbUsuario tbUsuario2 { get; set; }
        public virtual tbMunicipio tbMunicipio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbParametro> tbParametro { get; set; }
        public virtual tbBodega tbBodega { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbAreas> tbAreas { get; set; }
        public virtual tbEmpresas tbEmpresas { get; set; }
        public virtual tbPuntoEmision tbPuntoEmision { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCaja> tbCaja { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCuponDescuento> tbCuponDescuento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbNotaCredito> tbNotaCredito { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbFactura> tbFactura { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPedido> tbPedido { get; set; }
    }
}
