
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbMunicipio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbMunicipio()
        {
            this.tbCliente_copy1 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy11 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy12 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy13 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy14 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy15 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy16 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy17 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy18 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy19 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy110 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy111 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy112 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy113 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy114 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy115 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy116 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy117 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy118 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy119 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy120 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy121 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy122 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy123 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy124 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy125 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy126 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy127 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy128 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy129 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy130 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy131 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy132 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy133 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy134 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy135 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy136 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy137 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy138 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy139 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy140 = new HashSet<tbCliente_copy1>();
            this.tbCliente_copy141 = new HashSet<tbCliente_copy1>();
            this.tbTechoImpuestoVecinal = new HashSet<tbTechoImpuestoVecinal>();
            this.tbBodega = new HashSet<tbBodega>();
            this.tbSucursales = new HashSet<tbSucursales>();
            this.tbCliente = new HashSet<tbCliente>();
            this.tbSucursal = new HashSet<tbSucursal>();
        }
    
        public string mun_Codigo { get; set; }
        public string dep_Codigo { get; set; }
        public string mun_Nombre { get; set; }
        public int mun_UsuarioCrea { get; set; }
        public System.DateTime mun_FechaCrea { get; set; }
        public Nullable<int> mun_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> mun_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbDepartamento tbDepartamento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy11 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy12 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy13 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy14 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy15 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy16 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy17 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy18 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy19 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy110 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy111 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy112 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy113 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy114 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy115 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy116 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy117 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy118 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy119 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy120 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy121 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy122 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy123 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy124 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy125 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy126 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy127 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy128 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy129 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy130 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy131 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy132 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy133 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy134 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy135 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy136 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy137 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy138 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy139 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy140 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente_copy1> tbCliente_copy141 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbTechoImpuestoVecinal> tbTechoImpuestoVecinal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbBodega> tbBodega { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSucursales> tbSucursales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCliente> tbCliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSucursal> tbSucursal { get; set; }
    }
}
