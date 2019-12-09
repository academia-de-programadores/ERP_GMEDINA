using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cHistorialHorasTrabajadas))]
    public partial class tbHistorialHorasTrabajadas
    {

    }
    public class cHistorialHorasTrabajadas
    {
        [Display(Name = "ID ")]
        public int htra_Id { get; set; }
        public int emp_Id { get; set; }
        public int tiho_Id { get; set; }
        public int jor_Id { get; set; }
        public int htra_CantidadHoras { get; set; }
        public Nullable<System.DateTime> htra_Fecha { get; set; }
        public bool htra_Estado { get; set; }
        [Display(Name = "Razon Inactivo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string htra_RazonInactivo { get; set; }
        public int htra_UsuarioCrea { get; set; }
        public System.DateTime htra_FechaCrea { get; set; }
        public Nullable<int> htra_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> htra_FechaModifica { get; set; }
    }
}