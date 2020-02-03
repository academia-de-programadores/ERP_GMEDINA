using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(cTechoImpuestoVecinal))]
    public partial class tbTechoImpuestoVecinal { }

    public class cTechoImpuestoVecinal
    {
        [Display(Name = "Número")]
        public int timv_IdTechoImpuestoVecinal { get; set; }

        [Display(Name = "Código Municipio")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string mun_Codigo { get; set; }

        [Display(Name = "Tipo deducción")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Nullable<int> tde_IdTipoDedu { get; set; }

        [Display(Name = "Rango inicio")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Nullable<decimal> timv_RangoInicio { get; set; }

        [Display(Name = "Rango fin")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Nullable<decimal> timv_RangoFin { get; set; }

        [Display(Name = "Rango")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal timv_Rango { get; set; }

        [Display(Name = "Impuesto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal timv_Impuesto { get; set; }

        [Display(Name = "Creado por")]
        public int timv_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Creación")]
        public System.DateTime timv_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        public Nullable<int> timv_UsuarioModifica { get; set; }

        [Display(Name = "Fecha modificación")]
        public Nullable<System.DateTime> timv_FechaModifica { get; set; }

        [Display(Name = "Activo")]
        public bool timv_Activo { get; set; }

        [Display(Name = "Creado por")]
        public virtual tbUsuario tbUsuario { get; set; }

        [Display(Name = "Modificado por")]
        public virtual tbUsuario tbUsuario1 { get; set; }

        [Display(Name = "Municipio")]
        public virtual tbMunicipio tbMunicipio { get; set; }

        [Display(Name = "Tipo Deducción")]
        public virtual tbTipoDeduccion tbTipoDeduccion { get; set; }
    }
}