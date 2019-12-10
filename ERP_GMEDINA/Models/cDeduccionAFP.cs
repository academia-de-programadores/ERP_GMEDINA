using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cDeduccionAFP))]
    public partial class tbDeduccionAFP
    {
    }

    public class cDeduccionAFP
    {
        [Display(Name = "Id Deducción AFP")]
        public int dafp_Id { get; set; }

        [Range(0.00, 9999999999999999.99, ErrorMessage = "El Aporte no puede ser menor de 0 dígitos, ni mayor que 10 dígitos")]
        [Required(ErrorMessage = "Campo Requerido")]
        [Display(Name = "Aporte")]
        public decimal dafp_AporteLps { get; set; }

        [Required]
        [Display(Name = "AFP")]
        public int afp_Id { get; set; }

        [Required]
        [Display(Name = "Empleado")]
        public int emp_Id { get; set; }

        [Display(Name = "Pagado")]
        public Nullable<bool> dafp_Pagado { get; set; }

        [Display(Name = "Creado por")]
        public int dafp_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creación")]
        public System.DateTime dafp_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        public Nullable<int> dafp_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public Nullable<System.DateTime> dafp_FechaModifica { get; set; }

        [Display(Name = "Activo")]
        public bool dafp_Activo { get; set; }
    }

}