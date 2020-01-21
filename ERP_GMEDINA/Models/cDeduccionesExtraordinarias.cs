using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(cDeduccionesExtraordinarias))]
    public partial class tbDeduccionesExtraordinarias
    {
    }

    public class cDeduccionesExtraordinarias
    {
        [Display(Name = "Número")]
        public int dex_IdDeduccionesExtra { get; set; }

        [Required(ErrorMessage = "Campo Equipo Empleado Requerido")]
        [Display(Name = "Equipo Empleado")]
        public int eqem_Id { get; set; }

        [Display(Name = "Deducción")]
        public int cde_IdDeducciones { get; set; }

        [Required(ErrorMessage = "Campo Monto Inicial Requerido")]
        [DataType(DataType.Currency)]
        [Display(Name = "Monto Inicial")]
        public decimal dex_MontoInicial { get; set; }
        
        [Required(ErrorMessage = "Campo Monto Restante Requerido")]
        [DataType(DataType.Currency)]
        [Display(Name = "Monto Restante")]
        public decimal dex_MontoRestante { get; set; }

        [StringLength (100, ErrorMessage = "No puede ingresar más de 100 caracteres")]
        [Required(ErrorMessage = "Campo Observaciones Requerido")]
        [Display(Name = "Observaciones")]
        public string dex_ObservacionesComentarios { get; set; }

        
        [Required(ErrorMessage = "Campo Cuota Requerido")]
        [DataType(DataType.Currency)]
        [Display(Name = "Cuota")]
        public decimal dex_Cuota { get; set; }

        [Display(Name = "Creado por")]
        public int dex_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creacion")]
        public System.DateTime dex_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        public Nullable<int> dex_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificacion")]
        public Nullable<System.DateTime> dex_FechaModifica { get; set; }

        [Display(Name = "Estado")]
        public bool dex_Activo { get; set; }

    }
}