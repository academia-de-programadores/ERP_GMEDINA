using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cCatalogoDeDeducciones))]
    public partial class tbCatalogoDeDeducciones
    {
    }
    public class cCatalogoDeDeducciones
    {
        [Display(Name = "ID Deducciones")]
        public int cde_IdDeducciones { get; set; }

        [Display(Name = "Descripcion Deduccion")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]

        public string cde_DescripcionDeduccion { get; set; }

        [Display(Name = "Tipo Deduccion")]
        [Required]
        public int tde_IdTipoDedu { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Porcentaje Colaborador")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Número decimal válido con un máximo de 2 decimales.")]
        [Range(1, 999999999999, ErrorMessage = "El monto {0} debe estar entre {1} y {2}.")]
        public decimal cde_PorcentajeColaborador { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Porcentaje Empresa")]
        [Required(ErrorMessage = "No puede dejar campos vacios.")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Número decimal válido con un máximo de 2 decimales.")]
        [Range(1, 999999999999, ErrorMessage = "El monto {0} debe estar entre {1} y {2}.")]
        public decimal cde_PorcentajeEmpresa { get; set; }

        [Display(Name = "Creado por")]
        public int cde_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creacion")]
        public System.DateTime cde_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        public Nullable<int> cde_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modificacion")]
        public Nullable<System.DateTime> cde_FechaModifica { get; set; }

        [Display(Name = "Activo")]
        public bool cde_Activo { get; set; }
    }
}