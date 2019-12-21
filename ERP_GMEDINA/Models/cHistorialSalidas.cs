using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cHistorialSalidas))]
    //public partial class tbHistorialSalidas
    //{
    //    public string car_Descripcion { get; set; }
    //}
    public class cHistorialSalidas
    {
        [Display(Name = "Id historial salida")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int hsal_Id { get; set; }
        [Display(Name = "Id empleado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int emp_Id { get; set; }

                [Display(Name = "Id tipo salida")]
                //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
                public int tsal_Id { get; set; }
                [Display(Name = "Id razon salida")]
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
    }
}