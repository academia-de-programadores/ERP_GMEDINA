using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cHorarios))]
    public partial class tbHorarios
    {
    }

    public class cHorarios
    {
        [Display(Name = "Id")]
        public int hor_Id { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de caracteres.")]
        public string hor_Descripcion { get; set; }

        [Display(Name = "Hora Inicio")]
        public System.DateTime hor_HoraInicio { get; set; }

        [Display(Name = "Hora Fin")]
        public System.DateTime hor_HoraFin { get; set; }

        [Display(Name = "Cantidad Horas")]
        public int hor_CantidadHoras { get; set; }

        [Display(Name = "Estado")]
        public bool hor_Estado { get; set; }

        [Display(Name = "Razón Inactivo")]
        public string hor_RazonInactivo { get; set; }

        [Display(Name = "Usuario crea")]
        public int hor_UsuarioCrea { get; set; }

        [Display(Name = "Fecha creación")]
        public System.DateTime hor_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> hor_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> hor_FechaModifica { get; set; }
    }
}