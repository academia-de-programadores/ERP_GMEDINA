//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP_GMEDINA.Models.Inventario
{
    using System;
    using System.Collections.Generic;
    
    public partial class UDV_Vent_AnalisisDeMora
    {
        public long fact_Id { get; set; }
        public string RTN { get; set; }
        public string Nombres { get; set; }
        public int Máximo_Días_Crédito { get; set; }
        public decimal Máximo_Monto_Crédito { get; set; }
        public System.DateTime fact_Fecha { get; set; }
        public Nullable<System.DateTime> FechaVencimiento { get; set; }
        public Nullable<decimal> SaldoActual { get; set; }
        public Nullable<decimal> MORADE30 { get; set; }
        public Nullable<decimal> MORADE60 { get; set; }
        public Nullable<decimal> MORADE90 { get; set; }
    }
}