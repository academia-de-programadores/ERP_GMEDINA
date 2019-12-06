using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(cEmpleadoComisiones))]
    public partial class tbEmpleadoComisiones
    {

    }

    public class cEmpleadoComisiones
    {

        [Display(Name = "ID Empleado Comision")]
        public int cc_Id { get; set; }


        [Display(Name = "Empleado")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]
        public int emp_Id { get; set; }

        [Display(Name = "Ingreso")]
        public int cin_IdIngreso { get; set; }


        [Display(Name = "Fecha Registro")]
        public System.DateTime cc_FechaRegistro { get; set; }

        [Display(Name = "Pagado")]
        public bool cc_Pagado { get; set; }

        [Display(Name = "Creado por")]
        public int cc_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creacion")]
        public System.DateTime cc_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        public Nullable<int> cc_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificacion")]
        public Nullable<System.DateTime> cc_FechaModifica { get; set; }

        [Display(Name = "Activo")]
        public bool cc_Activo { get; set; }


        [Display(Name = "Porcentaje Comision")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]
        [Range(1, 1000000000,ErrorMessage = "El monto {0} debe estar entre {1} y {2}.")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Número decimal válido con un máximo de 2 decimales.")]
        public decimal cc_PorcentajeComision { get; set; }


        [Display(Name = "Total Venta")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]
        [Range(1, 1000000000, ErrorMessage = "El monto {0} debe estar entre {1} y {2}.")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Número decimal válido con un máximo de 2 decimales.")]
        public decimal cc_TotalVenta { get; set; }

        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbCatalogoDeIngresos tbCatalogoDeIngresos { get; set; }
        public virtual tbEmpleados tbEmpleados { get; set; }
    }
}