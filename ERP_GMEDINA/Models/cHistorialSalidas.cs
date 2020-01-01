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
        public string tsal_Descripcion { get; set; }
        public string rsal_Descripcion { get; set; }
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
        public string per_CorreoElectronico { get; set; }
        public int per_Edad { get; set; }
        public string per_Direccion { get; set; }
        public string per_Telefono { get; set; }
        public string per_EstadoCivil { get; set; }
    }
    public class cHistorialSalidas
    {
        [Display(Name = "Id historial salida")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int hsal_Id { get; set; }
        [Display(Name = "Id empleado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int emp_Id { get; set; }

                [Display(Name = "Seleccione el tipo de salida")]
                //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
                public int tsal_Id { get; set; }
                [Display(Name = "Seleccione la razon de la salida")]
                //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
                public int rsal_Id { get; set; }

        [Display(Name = "Fecha salida")]
        public System.DateTime hsal_FechaSalida { get; set; }
        [Display(Name = "Observaciones")]
        [MaxLength(25, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string hsal_Observacion { get; set; }
        [Display(Name = "Estado")]
        public bool hsal_Estado { get; set; }
        [Display(Name = "Razon inactivo")]
        public string hsal_RazonInactivo { get; set; }
        [Display(Name = "Agregado por")]
        public int hsal_UsuarioCrea { get; set; }
        [Display(Name = "Fecha agregación")]
        public System.DateTime hsal_FechaCrea { get; set; }
        [Display(Name = "Modificado por")]
        public Nullable<int> hsal_UsuarioModifica { get; set; }
        [Display(Name = "Fecha modificadción")]
        public Nullable<System.DateTime> hsal_FechaModifica { get; set; }





        [MaxLength(100, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string emp_RazonInactivo { get; set; }
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string tsal_Descripcion { get; set; }
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string rsal_Descripcion { get; set; }
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string per_Nombres { get; set; }
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string per_Apellidos { get; set; }
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string per_CorreoElectronico { get; set; }
        public int per_Edad { get; set; }
        [MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string per_Direccion { get; set; }
        [MaxLength(20, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string per_Telefono { get; set; }
        [MaxLength(1, ErrorMessage = "Exedio el numero maximo de caracteres")]
        public string per_EstadoCivil { get; set; }
    }
}