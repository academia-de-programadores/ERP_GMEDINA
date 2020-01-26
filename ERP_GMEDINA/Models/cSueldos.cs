using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cSueldos))]
    public partial class tbSueldos
    {
    }
    public class cSueldos
    {
       
       

        [Display(Name = "ID")]
        public int sue_Id { get; set; }

        [Display(Name = "Empleado Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int emp_Id { get; set; }

        [Display(Name = "Tipo Moneda Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int tmon_Id { get; set; }

        [Display(Name = "Sueldo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string sue_Cantidad { get;set; }

        [Display(Name = "Sueldo Anterior")]
        public Nullable<int> sue_SueldoAnterior { get; set; }
        
        [Display(Name = "Estado")]
        public bool sue_Estado { get; set; }

        [Display(Name = "Razon Inactivo")]
        public string sue_RazonInactivo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int sue_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public Nullable<System.DateTime> sue_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> sue_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> sue_FechaModifica { get; set; }

        [Display(Name = "Usuario")]
        public virtual tbUsuario tbUsuario { get; set; }

        [Display(Name = "Usuario1")]
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}
