
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbPersonas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbPersonas()
        {
            this.tbCompetenciasPersona = new HashSet<tbCompetenciasPersona>();
            this.tbEmpleados = new HashSet<tbEmpleados>();
            this.tbHabilidadesPersona = new HashSet<tbHabilidadesPersona>();
            this.tbIdiomaPersona = new HashSet<tbIdiomaPersona>();
            this.tbRequerimientosEspecialesPersona = new HashSet<tbRequerimientosEspecialesPersona>();
            this.tbSeleccionCandidatos = new HashSet<tbSeleccionCandidatos>();
            this.tbTitulosPersona = new HashSet<tbTitulosPersona>();
            this.tbEmpresas = new HashSet<tbEmpresas>();
        }
    
        public int per_Id { get; set; }
        public string per_Identidad { get; set; }
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
        public Nullable<System.DateTime> per_FechaNacimiento { get; set; }
        public string per_Sexo { get; set; }
        public Nullable<int> per_Edad { get; set; }
        public int nac_Id { get; set; }
        public string per_Direccion { get; set; }
        public string per_Telefono { get; set; }
        public string per_CorreoElectronico { get; set; }
        public string per_EstadoCivil { get; set; }
        public string per_TipoSangre { get; set; }
        public bool per_Estado { get; set; }
        public string per_RazonInactivo { get; set; }
        public int per_UsuarioCrea { get; set; }
        public System.DateTime per_FechaCrea { get; set; }
        public Nullable<int> per_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> per_FechaModifica { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCompetenciasPersona> tbCompetenciasPersona { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbEmpleados> tbEmpleados { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbHabilidadesPersona> tbHabilidadesPersona { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbIdiomaPersona> tbIdiomaPersona { get; set; }
        public virtual tbNacionalidades tbNacionalidades { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbRequerimientosEspecialesPersona> tbRequerimientosEspecialesPersona { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSeleccionCandidatos> tbSeleccionCandidatos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbTitulosPersona> tbTitulosPersona { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbEmpresas> tbEmpresas { get; set; }
    }
}
