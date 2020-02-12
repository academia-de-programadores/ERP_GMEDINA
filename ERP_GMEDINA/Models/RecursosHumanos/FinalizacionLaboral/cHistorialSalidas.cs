using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cHistorialSalidas))]
    public partial class tbHistorialSalidas
    {
        public string emp_RazonInactivo { get; set; }
    }
    public class cHistorialSalidas
    {
        [Display(Name = "Id historial salida")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int hsal_Id { get; set; }
        [Display(Name = "Id empleado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int emp_Id { get; set; }

                [Display(Name = "Tipo de salida")]
                //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
                public int tsal_Id { get; set; }
                [Display(Name = "Razon de la salida")]
                //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
                public int rsal_Id { get; set; }

        [Display(Name = "Fecha salida")]
        public System.DateTime hsal_FechaSalida { get; set; }
        [Display(Name = "Observaciones")]
        [MaxLength(25, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string hsal_Observacion { get; set; }
        [Display(Name = "Estado")]
        public bool hsal_Estado { get; set; }
        [Display(Name = "Razón inactivo")]
        [MaxLength(100, ErrorMessage = "Excedió el número máximo de caracteres.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string hsal_RazonInactivo { get; set; }
        [Display(Name = "Agregado por")]
        public int hsal_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        public System.DateTime hsal_FechaCrea { get; set; }
        [Display(Name = "Modificado por")]
        public Nullable<int> hsal_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> hsal_FechaModifica { get; set; }
    }
}