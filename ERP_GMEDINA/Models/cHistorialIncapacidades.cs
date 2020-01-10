using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{

    [MetadataType(typeof(cHistorialIncapacidades))]

    public partial class tbHistorialIncapacidades
    {

    }
    public class cHistorialIncapacidades
    {
        [Display(Name = "ID")]
        public int hinc_Id { get; set; }

        [Display(Name = "Empleado")]
        public int emp_Id { get; set; }

        [Display(Name = "Tipo de Incapacidad")]
        public int ticn_Id { get; set; }


        [Display(Name = "Dias")]
        public int hinc_Dias { get; set; }

        [Display(Name = "Centro Medico")]
        public string hinc_CentroMedico { get; set; }

        [Display(Name = "Doctor")]
        public string hinc_Doctor { get; set; }

        [Display(Name = "Diagnostico")]
        public string hinc_Diagnostico { get; set; }

        [Display(Name = "Fecha Inicio")]
        public Nullable<System.DateTime> hinc_FechaInicio { get; set; }

        [Display(Name = "Fecha Fin")]
        public Nullable<System.DateTime> hinc_FechaFin { get; set; }

        [Display(Name = "Estado")]
        public bool hinc_Estado { get; set; }

        [Display(Name = "Razon Inactivo")]
        public string hinc_RazonInactivo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int hinc_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime hinc_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica ")]
        public Nullable<int> hinc_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> hinc_FechaModifica { get; set; }

        [Display(Name = "Usuario Crea")]
        public virtual tbUsuario tbUsuario { get; set; }

        [Display(Name = "Usuario Modifica")]
        public virtual tbUsuario tbUsuario1 { get; set; }

    }
}