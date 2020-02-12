using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cTechosDeducciones))]
    public partial class tbTechosDeducciones
    {
    }
    public class cTechosDeducciones
    {

        [Display(Name = "ID Techos Deducciones")]
        [Required(ErrorMessage = "Campo {0} requerido")]
        public int tddu_IdTechosDeducciones { get; set; }

        [Required(ErrorMessage = "Campo {0} requerido")]
        [Display(Name = "Porcentaje colaborador")]
        //[RegularExpression("([0-9][.])", ErrorMessage = "El campo {0} debe ser numérico.")]
        public decimal tddu_PorcentajeColaboradores { get; set; }

        [Required(ErrorMessage = "Campo {0} requerido")]
        [Display(Name = "Porcentaje empresa")]
        //[RegularExpression("([0-9][.])", ErrorMessage = "El campo {0} debe ser numérico.")]
        public decimal tddu_PorcentajeEmpresa { get; set; }

        [Required(ErrorMessage = "Campo {0} requerido")]
        [Display(Name = "Techo")]
        //[RegularExpression("([0-9][.])", ErrorMessage = "El campo {0} debe ser numérico.")]
        public decimal tddu_Techo { get; set; }

        [Required(ErrorMessage = "Campo {0} requerido")]
        [Display(Name = "ID Catálogo de Deducciones")]
        public int cde_IdDeducciones { get; set; }

        [Display(Name = "Creado por")]
        public int tddu_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creación")]
        public System.DateTime tddu_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        public Nullable<int> tddu_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificación")]
        public Nullable<System.DateTime> tddu_FechaModifica { get; set; }

        [Display(Name = "Activo")]
        public bool tddu_Activo { get; set; }
    }
}
