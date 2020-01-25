using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cAreas))]
    public partial class tbAreas
    {
        public string car_Descripcion { get; set; }
    }
    public class cAreas
    {
        [Display(Name = "Id")]
        public int area_Id { get; set; }
        public int car_Id { get; set; }
        [Display(Name = "Sucursal")]
        public int suc_Id { get; set; }
        [Display(Name = "Área")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string area_Descripcion { get; set; }
        [Display(Name = "Estado")]
        public bool area_Estado { get; set; }
        [Display(Name = "Razon para inactivar")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string area_Razoninactivo { get; set; }
        public int area_Usuariocrea { get; set; }
        public System.DateTime area_Fechacrea { get; set; }
        public Nullable<int> area_Usuariomodifica { get; set; }
        public Nullable<System.DateTime> area_Fechamodifica { get; set; }


        [Display(Name = "Cargo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string car_Descripcion { get; set; }
    }
}
