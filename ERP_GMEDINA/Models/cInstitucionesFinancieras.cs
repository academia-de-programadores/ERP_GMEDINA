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
        [Display(Name = "Número")]
        public int insf_IdInstitucionFinanciera { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Campo {0} requerido.")]
        public string insf_DescInstitucionFinanc { get; set; }

        [Display(Name = "Nombre Contacto")]
        [Required(ErrorMessage = "Campo {0} requerido.")]
        public string insf_Contacto { get; set; }

        [Display(Name = "Telefono Contacto")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{4})[-. ]?([0-9]{4})$", ErrorMessage = "Número de Teléfono Inválido.")]
        [Required(ErrorMessage = "Campo {0} requerido.")]
        [MaxLength(15, ErrorMessage = "Numero debe ser inferior a 15 digitos.")]
        public string insf_Telefono { get; set; }

        [Display(Name = "Correo Electrónico")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Correo Electrónico Inválido.")]
        [Required(ErrorMessage = "Campo {0} requerido.")]
        public string insf_Correo { get; set; }

        [Display(Name = "Usuario Creación")]
        public int insf_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Creación")]
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