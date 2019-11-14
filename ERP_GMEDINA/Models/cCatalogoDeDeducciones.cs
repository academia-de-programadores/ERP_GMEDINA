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
        public string cde_DescripcionDeduccion { get; set; }

        [Display(Name = "Tipo Deduccion")]
        public int tde_IdTipoDedu { get; set; }

        [Display(Name = "Porcentaje Colaborador")]
        public decimal cde_PorcentajeColaborador { get; set; }

        [Display(Name = "Porcentaje Empresa")]
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