using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(CHistorialVacaciones))]
    public partial class tbHistorialVacaciones
    {
        //public string car_Descripcion { get; set; }
    }
    public class CHistorialVacaciones
    {
        //[Display(Name = "Descripcion")]
        
        [Display(Name = "Id vacaciones")]
        public int hvac_Id { get; set; }
        [Display(Name = "Id empleado")]
        public int emp_Id { get; set; }
        [Display(Name = "fecha de inicio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
      
        public System.DateTime hvac_FechaInicio { get; set; }
        [Display(Name = "Fecha de fin")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime hvac_FechaFin { get; set; }
        [Display(Name = "Cantidad de dias")]
        public Nullable<int> hvac_CantDias { get; set; }
        [Display(Name = "Dias pagados")]
        public bool hvac_DiasPagados { get; set; }
        [Display(Name = "Mes de vacaciones")]
        public int hvac_MesVacaciones { get; set; }
        [Display(Name = "Año de vacaciones")]
        public int hvac_AnioVacaciones { get; set; }
        [Display(Name = "Fecha crea")]
        public System.DateTime hvac_FechaCrea { get; set; }
 
        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> hvac_FechaModifica { get; set; }
    }
}