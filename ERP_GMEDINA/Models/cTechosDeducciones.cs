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
        [Required]
        public int tddu_IdTechosDeducciones { get; set; }

        [Required]
        [Display(Name = "Porcentaje colaborador")]
        public decimal tddu_PorcentajeColaboradores { get; set; }

        [Required]
        [Display(Name = "Porcentaje empresa")]
        public decimal tddu_PorcentajeEmpresa { get; set; }

        [Required]
        [Display(Name = "Techo")]
        public decimal tddu_Techo { get; set; }

        [Required]
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