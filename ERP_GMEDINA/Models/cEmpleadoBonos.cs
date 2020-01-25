using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cEmpleadoBonos))]

    public partial class tbEmpleadoBonos
    {
    }

    public class cEmpleadoBonos
    {
        [Display(Name = "ID Bonos")]
        public int cb_Id { get; set; }

        [Required]
        [Display(Name = "ID Colaborador")]
        public int emp_Id { get; set; }

        [Required]
        [Display(Name = "ID Ingreso")]
        public int cin_IdIngreso { get; set; }

        [Required(ErrorMessage = "Campo monto requerido")]
        [Range(1, 999999.99, ErrorMessage = "El monto {0} debe estar entre {1} y {2}")]
        //[RegularExpression(@"^[1-9]+(\.[0-9]{1,2})$", ErrorMessage = "Número decimal válido con un máximo de 2 decimales.")]
        [Display(Name = "Monto")]

        public decimal cb_Monto { get; set; }

        [Required]
        [Display(Name = "Fecha de Registro")]
        public System.DateTime cb_FechaRegistro { get; set; }

        [Required]
        [Display(Name = "Pagado")]
        public bool cb_Pagado { get; set; }

        [Display(Name = "Creado por")]
        public int cb_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creación")]
        public System.DateTime cb_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        public Nullable<int> cb_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public Nullable<System.DateTime> cb_FechaModifica { get; set; }

        [Display(Name = "Activo")]
        public bool cb_Activo { get; set; }

        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbCatalogoDeIngresos tbCatalogoDeIngresos { get; set; }
        public virtual tbEmpleados tbEmpleados { get; set; }
    }
}