using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cAdelantoSueldo))]

    public partial class tbAdelantoSueldo
    {
    }
    public class cAdelantoSueldo
    {
        [Display(Name = "ID Adelantos")]
        public int adsu_IdAdelantoSueldo { get; set; }

        [Display(Name = "ID Colaborador")]
        public int emp_Id { get; set; }

        [Display(Name = "Fecha Adelanto")]
        public System.DateTime adsu_FechaAdelanto { get; set; }

        [Display(Name = "Razón")]
        public string adsu_RazonAdelanto { get; set; }

        [Display(Name = "Monto")]
        public decimal adsu_Monto { get; set; }
       
        [Display(Name = "Deducido")]
        public bool adsu_Deducido { get; set; }

        [Display(Name = "Creado por")]
        public int adsu_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creación")]
        public System.DateTime adsu_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        public Nullable<int> adsu_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public Nullable<System.DateTime> adsu_FechaModifica { get; set; }

        [Display(Name = "Activo")]
        public bool adsu_Activo { get; set; }
    }
}