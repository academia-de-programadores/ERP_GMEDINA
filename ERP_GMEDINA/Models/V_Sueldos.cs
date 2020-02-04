
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_Sueldos
    {
        public int Id { get; set; }
        public int Id_Empleado { get; set; }
        public int Id_Amonestacion { get; set; }
        public int Id_Cargo { get; set; }
        public string Identidad { get; set; }
        public string Nombre { get; set; }
        public decimal Sueldo { get; set; }
        public string Tipo_Moneda { get; set; }
        public string Cuenta { get; set; }
        public Nullable<int> Sueldo_Anterior { get; set; }
        public string Area { get; set; }
        public string Cargo { get; set; }
        public Nullable<decimal> Sueldo_Maximo { get; set; }
        public decimal Sueldo_Minimo { get; set; }
        public string Usuario_Nombre { get; set; }
        public int Usuario_Crea { get; set; }
        public Nullable<System.DateTime> Fecha_Crea { get; set; }
        public Nullable<int> Usuario_Modifica { get; set; }
        public Nullable<System.DateTime> Fecha_Modifica { get; set; }
        public bool Estado { get; set; }
        public string RazonInactivo { get; set; }
    }
}
