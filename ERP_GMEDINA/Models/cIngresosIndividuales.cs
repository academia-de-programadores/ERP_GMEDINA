using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cIngresosIndividuales))]
    public partial class tbIngresosIndividuales
    {
    }

    public class cIngresosIndividuales
    {
        [Display(Name = "Número")]
        public int ini_IdIngresosIndividuales { get; set; }

        [StringLength(100, ErrorMessage = "No puede ingresar más de 100 caracteres")]
        [Required(ErrorMessage = "Campo Motivo Requerido")]
        [Display(Name = "Motivo")]
        public string ini_Motivo { get; set; }

        [Required(ErrorMessage = "Campo Empleado requerido")]
        [Display(Name = "Empleado")]
        public int emp_Id { get; set; }

        [Required(ErrorMessage = "Campo Monto Requerido")]
        [DataType(DataType.Currency)]
        [Display(Name = "Monto")]
        public decimal ini_Monto { get; set; }

        [Display(Name = "Siempre Recibe")]
        public Nullable<bool> ini_PagaSiempre { get; set; }

        [Display(Name = "Creado por")]
        public int ini_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creacion")]
        public System.DateTime ini_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        public Nullable<int> ini_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificacion")]
        public Nullable<System.DateTime> ini_FechaModifica { get; set; }

        [Display(Name = "Estado")]
        public bool ini_Activo { get; set; }

    }
}