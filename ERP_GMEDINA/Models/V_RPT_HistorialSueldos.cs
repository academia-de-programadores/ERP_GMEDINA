//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_RPT_HistorialSueldos
    {
        public int sue_Id { get; set; }
        public string NombreEmp { get; set; }
        public int emp_Id { get; set; }
        public decimal sue_Cantidad { get; set; }
        public string tmon_Descripcion { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<System.DateTime> fechafin { get; set; }
        public string car_Descripcion { get; set; }
        public int car_Id { get; set; }
    }
}
