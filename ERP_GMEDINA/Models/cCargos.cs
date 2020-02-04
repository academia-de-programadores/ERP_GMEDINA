using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cCargos))]
    public partial class tbCargos
    {

    }
    public class cCargos
    {
        [Display(Name = "ID")]
        public int car_Id { get; set; }

        [Display(Name = "Cargo")]

        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string car_Descripcion { get; set; }

        [Display(Name = "Salario minimo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
       
        public decimal car_SalarioMinimo { get; set; }

        [Display(Name = "Salario maximo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        public Nullable<decimal> car_SalarioMaximo { get; set; }


        [Display(Name = "Estado")]
        public bool car_Estado { get; set; }

        [Display(Name = "Razón Inactivo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido.")]
        [MaxLength(100, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string car_RazonInactivo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int car_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime car_FechaCrea { get; set; }

        [Display(Name = "Usuario Modificado")]
        public Nullable<int> car_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> car_FechaModifica { get; set; }

        [Display(Name = "Usuario Crea")]
        public virtual tbUsuario tbUsuario { get; set; }

        [Display(Name = "Usuario Modifica")]
        public virtual tbUsuario tbUsuario1 { get; set; }
    }
}