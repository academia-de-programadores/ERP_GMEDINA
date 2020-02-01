using System;
using System.ComponentModel.DataAnnotations;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cV_tbAdelantoSueldo))]
    public partial class V_tbAdelantoSueldo
    {
    }

    public class cV_tbAdelantoSueldo
    {
        [Display(Name = "ID Adelantos")]
        public int adsu_IdAdelantoSueldo { get; set; }

        [Display(Name = "ID Colaborador")]
        public int emp_Id { get; set; }

        [Display(Name = "Colaborador")]
        public string per_Nombres { get; set; }

        [Display(Name = "Fecha Adelanto")]
        public System.DateTime adsu_FechaAdelanto { get; set; }

        [Display(Name = "Razón")]
        public string adsu_RazonAdelanto { get; set; }

        [Display(Name = "Monto")]
        public Nullable<decimal> adsu_Monto { get; set; }

        [Display(Name = "Deducido")]
        public bool adsu_Deducido { get; set; }

        [Display(Name = "ID Usuario Crea")]
        public int adsu_UsuarioCrea { get; set; }

        [Display(Name = "Nombre Usuario Crea")]
        public string NombreUsuarioCrea { get; set; }

        [Display(Name = "Creado por")]
        public string UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creación")]
        public System.DateTime adsu_FechaCrea { get; set; }

        [Display(Name = "ID Usuario Modifica")]
        public Nullable<int> adsu_UsuarioModifica { get; set; }

        [Display(Name = "Nombre Usuario Modifca")]
        public string NombreUsuarioModifica { get; set; }

        [Display(Name = "Modificado por")]
        public string UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public Nullable<System.DateTime> adsu_FechaModifica { get; set; }

        [Display(Name = "Activo")]
        public bool adsu_Activo { get; set; }
    }
}