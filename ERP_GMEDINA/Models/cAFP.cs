using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(cAFP))]
    public partial class tbAFP
    {
    }

    public class cAFP
    {
        [Display(Name = "Número")]
        public int afp_Id { get; set; }

        [StringLength(100, ErrorMessage = "No puede ingresar más de 100 caracteres")]
        [Required(ErrorMessage = "Campo AFP Requerido")]
        [Display(Name = "AFP")]
        public string afp_Descripcion { get; set; }
        
        [Required(ErrorMessage = "Campo Aporte Mínimo Requerido")]
        [DataType(DataType.Currency)]
        [Display(Name = "Aporte Mínimo")]
        public decimal afp_AporteMinimoLps { get; set; }

        [Required(ErrorMessage = "Campo Interés Aporte Requerido")]
        [DataType(DataType.Currency)]
        [Display(Name = "Interés por Aporte")]
        public decimal afp_InteresAporte { get; set; }

        [Required(ErrorMessage = "Campo Interés Anual Requerido")]
        [DataType(DataType.Currency)]
        [Display(Name = "Interés Anual")]
        public decimal afp_InteresAnual { get; set; }

        [Required(ErrorMessage = "Campo Tipo Deducción Requerido")]
        [Display(Name = "Tipo Deducción")]
        public int tde_IdTipoDedu { get; set; }

        [Display(Name = "Creado por")]
        public int afp_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creación")]
        public System.DateTime afp_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        public Nullable<int> afp_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public Nullable<System.DateTime> afp_FechaModifica { get; set; }

        [Display(Name = "Estado")]
        public bool afp_Activo { get; set; }

    }
}