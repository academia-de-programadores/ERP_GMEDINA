//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_RPT_HistorialIncapacidad
    {
        public int hinc_Id { get; set; }
        public int ticn_Id { get; set; }
        public string Identidad { get; set; }
        public string NombreCompleto { get; set; }
        public string TipoIncapacidad { get; set; }
        public int Dias { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
    }
}