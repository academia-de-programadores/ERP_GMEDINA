using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cSueldos))]
    public partial class V_Sueldos
    {
    }
    public class cSueldos
    {
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
        public Nullable<System.DateTime> Usuario_Modifica { get; set; }
        public Nullable<System.DateTime> Fecha_Modifica { get; set; }
        public int Id_Empleado { get; set; }
        public int Id_Amonestacion { get; set; }
        public Nullable<System.DateTime> Fecha_Crea { get; set; }
        public bool Estado { get; set; }
        public string RazonInactivo { get; set; }

        public int sue_Id { get; set; }
        public int emp_Id { get; set; }
        public int tmon_Id { get; set; }
        public Nullable<decimal> sue_Cantidad { get; set; }
        public Nullable<int> sue_SueldoAnterior { get; set; }
        public bool sue_Estado { get; set; }
        public string sue_RazonInactivo { get; set; }
        public int sue_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> sue_FechaCrea { get; set; }
        public Nullable<int> sue_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> sue_FechaModifica { get; set; }
    }
}
