namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbIdiomas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbIdiomas()
        {
            this.tbIdiomaPersona = new HashSet<tbIdiomaPersona>();
            this.tbIdiomasRequisicion = new HashSet<tbIdiomasRequisicion>();
        }
    
        public int idi_Id { get; set; }
        public string idi_Descripcion { get; set; }
        public bool idi_Estado { get; set; }
        public string idi_RazonInactivo { get; set; }
        public int idi_UsuarioCrea { get; set; }
        public System.DateTime idi_FechaCrea { get; set; }
        public Nullable<int> idi_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> idi_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbIdiomaPersona> tbIdiomaPersona { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbIdiomasRequisicion> tbIdiomasRequisicion { get; set; }
    }
}
