
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_Plani_HistorialPlanilla
    {
        public int hipa_IdHistorialDePago { get; set; }
        public int emp_Id { get; set; }
        public string per_Identidad { get; set; }
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
        public Nullable<decimal> hipa_SueldoNeto { get; set; }
        public Nullable<System.DateTime> hipa_FechaInicio { get; set; }
        public Nullable<System.DateTime> hipa_FechaFin { get; set; }
        public Nullable<System.DateTime> hipa_FechaPago { get; set; }
        public int hipa_Anio { get; set; }
        public int hipa_Mes { get; set; }
        public int peri_IdPeriodo { get; set; }
        public int hipa_UsuarioCrea { get; set; }
        public System.DateTime hipa_FechaCrea { get; set; }
        public Nullable<int> hipa_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> hipa_FechaModifica { get; set; }
        public Nullable<decimal> hipa_TotalISR { get; set; }
        public Nullable<bool> hipa_ISRPendiente { get; set; }
        public Nullable<decimal> hipa_AFP { get; set; }
        public Nullable<decimal> hipa_TotalHorasConPermisoJustificado { get; set; }
        public Nullable<decimal> hipa_TotalComisiones { get; set; }
        public Nullable<decimal> hipa_TotalHorasExtras { get; set; }
        public Nullable<decimal> hipa_TotalVacaciones { get; set; }
        public Nullable<decimal> hipa_TotalSeptimoDia { get; set; }
        public Nullable<decimal> hipa_AdelantoSueldo { get; set; }
        public Nullable<decimal> hipa_TotalSalario { get; set; }
        public Nullable<decimal> hipa_TotalDeduccionesIndividuales { get; set; }
        public Nullable<decimal> hipa_TotalIngresosIndividuales { get; set; }
        public Nullable<decimal> hipa_TotalSueldoBruto { get; set; }
        public Nullable<int> hipa_CantidadUnidadesHorasExtras { get; set; }
        public Nullable<int> hipa_CantidadUnidadesBonos { get; set; }
        public Nullable<decimal> hipa_TotalBonos { get; set; }
        public string hipa_CodigoPlanilla { get; set; }
    }
}