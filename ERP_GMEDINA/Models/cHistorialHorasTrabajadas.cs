using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cHistorialHorasTrabajadas))]
    public partial class tbHistorialHorasTrabajadas
    {
        
    }
    public class cHistorialHorasTrabajadas
    {
        [Display(Name = "Id")]
        public int htra_Id { get; set; }

        [Display(Name = "Id")]
        public int emp_Id { get; set; }

        //[Display(Name = "Id")]
        //public int per_Id { get; set; }

        [Display(Name = "Id")]
        public int tiho_Id { get; set; }

        [Display(Name = "Id")]
        public int jor_Id { get; set; }

        [Display(Name = "Id")]
        public int htra_CantidadHoras { get; set; }

        [Display(Name = "Id")]
        public Nullable<System.DateTime> htra_Fecha { get; set; }
       
        public bool htra_Estado { get; set; }
        [Display(Name = "Razon para inactivar")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [MaxLength(100, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string htra_RazonInactivo { get; set; }

        public int htra_UsuarioCrea { get; set; }
        public System.DateTime htra_FechaCrea { get; set; }
        public Nullable<int> htra_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> htra_FechaModifica { get; set; }
    }
}