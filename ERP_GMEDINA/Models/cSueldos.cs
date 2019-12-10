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
       /* [Display(Name = "Id")]
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
        public int sue_UsuarioCrea { get; set; }*/


        public int Id { get; set; }
        public string Identidad { get; set; }
        public string Nombre { get; set; }
        public decimal Sueldo { get; set; }
        public string Tipo_Moneda { get; set; }
        public string Cuenta { get; set; }
        public Nullable<int> Sueldo_Anterior { get; set; }
        public string Area { get; set; }
        public string Cargo { get; set; }
        public string Usuario_Nombre { get; set; }
        public int Usuario_Crea { get; set; }
        public Nullable<System.DateTime> Usuario_Fecha { get; set; }
        public Nullable<System.DateTime> Usuario_Modifica { get; set; }
        public Nullable<System.DateTime> Fecha_Modifica { get; set; }





    }
}