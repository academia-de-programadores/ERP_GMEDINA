using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cTechosComisiones))]
    public partial class tbTechosComisiones
    {
    }
    public class cTechosComisiones
    {
        [Display(Name = "ID Techo Comisión")]
        public int tc_Id { get; set; }

        [Display(Name = "Ingreso")]
        public Nullable<int> cin_IdIngreso { get; set; }

        [Display(Name = "Rango Inicio")]
        public Nullable<decimal> tc_RangoInicio { get; set; }

        [Display(Name = "Rango Fin")]
        public Nullable<decimal> tc_RangoFin { get; set; }

        [Display(Name = "Porcentaje Comisión")]
        public Nullable<decimal> tc_PorcentajeComision { get; set; }

        [Display(Name = "Estado")]
        public bool tc_Estado { get; set; }

        [Display(Name = "Creado por")]
        public Nullable<int> tc_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creación")]
        public Nullable<System.DateTime> tc_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        public Nullable<int> tc_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public Nullable<System.DateTime> tc_FechaModifica { get; set; }

        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbCatalogoDeIngresos tbCatalogoDeIngresos { get; set; }
    }
}