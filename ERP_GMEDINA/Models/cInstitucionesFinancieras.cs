using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using System.ComponentModel.DataAnnotations;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cInstitucionesFinancieras))]
    public partial class tbInstitucionesFinancieras
    {

    }

    public partial class cInstitucionesFinancieras
    {
        [Display(Name = "Codigo")]
        public int insf_IdInstitucionFinanciera { get; set; }
        [Display(Name = "Nombre o Descripcion")]
        [Required(ErrorMessage = "Campo {0} requerido.")]
        public string insf_DescInstitucionFinanc { get; set; }
        [Display(Name = "Nombre Contacto")]
        [Required(ErrorMessage = "Campo {0} requerido.")]
        public string insf_Contacto { get; set; }
        [Display(Name = "Telefono Contacto")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Favor ingresar solamente números.")]
        [Required(ErrorMessage = "Campo {0} requerido.")]
        [MaxLength(15, ErrorMessage = "Numero debe ser inferior a 15 digitos.")]
       
        public string insf_Telefono { get; set; }
        [Display(Name = "Correo Electronico Contacto")]
        [EmailAddress(ErrorMessage = "Correo Electrónico inválido.")]
        [Required(ErrorMessage = "Campo {0} requerido.")]
        public string insf_Correo { get; set; }
        [Display(Name = "Usuario Creacion")]
        public int insf_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Creacion")]
        public System.DateTime insf_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        public Nullable<int> insf_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> insf_FechaModifica { get; set; }
        [Display(Name = "Estado")]
        public bool insf_Activo { get; set; }

        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbDeduccionInstitucionFinanciera> tbDeduccionInstitucionFinanciera { get; set; }
    }
}