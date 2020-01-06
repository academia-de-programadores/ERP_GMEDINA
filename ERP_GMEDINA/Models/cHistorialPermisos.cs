using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cHistorialPermisos))]
    //public partial class tbHistorialPermisos
    //{
    //    public string car_Descripcion { get; set; }
    //}
    public class cHistorialPermisos
    {
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        //[MaxLength(50, ErrorMessage = "Exedio el numero maximo de caracteres")]        
        [Display(Name = "Id")]
        public int hper_Id { get; set; }
        [Display(Name = "Empleado")]
        public int emp_Id { get; set; }
        [Display(Name = "Tipo permiso")]
        public int tper_Id { get; set; }
        [Display(Name = "Fecha salida")]
        public System.DateTime hper_fechaInicio { get; set; }
        [Display(Name = "Fecha regreso")]
        public System.DateTime hper_fechaFin { get; set; }
        [Display(Name = "Duración")]
        public int hper_Duracion { get; set; }
        [Display(Name = "Observaciónes")]
        public string hper_Observacion { get; set; }
        [Display(Name = "Justificado")]
        public Nullable<bool> hper_Justificado { get; set; }
        [Display(Name = "Porcentaje derecho indemnización")]
        public int hper_PorcentajeIndemnizado { get; set; }
        [Display(Name = "Estado")]
        public bool hper_Estado { get; set; }
        [Display(Name = "Razon")]
        public string hper_RazonInactivo { get; set; }
        public int hper_UsuarioCrea { get; set; }
        public System.DateTime hper_FechaCrea { get; set; }
        public Nullable<int> hper_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> hper_FechaModifica { get; set; }


        [Display(Name = "Usuario inserta")]
        public virtual tbUsuario tbUsuario { get; set; }
        [Display(Name = "Usuario modifica")]
        public virtual tbUsuario tbUsuario1 { get; set; }
        //public virtual tbEmpleados tbEmpleados { get; set; }
        //public virtual tbTipoPermisos tbTipoPermisos { get; set; }
    }
}