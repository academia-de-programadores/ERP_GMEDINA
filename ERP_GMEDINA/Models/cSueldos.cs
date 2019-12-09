using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cSueldos))]
    public partial class tbSueldos
    {
    }
    public class cSueldos
    {
        [Display(Name = "Id")]
        public int sue_Id { get; set; }
        [Display(Name = "Id Empleado")]
        public int emp_Id { get; set; }
        [Display(Name = "Id Tipo Moneda")]
        public int tmon_Id { get; set; }
        [Display(Name = "Sueldo")]
        public decimal sue_Cantidad { get; set; }
        [Display(Name = "Sueldo Anterior")]
        public Nullable<int> sue_SueldoAnterior { get; set; }
        [Display(Name = "Estado")]
        public bool sue_Estado { get; set; }
        [Display(Name = "Razon Inactivo")]
        public string sue_RazonInactivo { get; set; }
        [Display(Name = "Usuario Crea")]
        public int sue_UsuarioCrea { get; set; }



    }
}