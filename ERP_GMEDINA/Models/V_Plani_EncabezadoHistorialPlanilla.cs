
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_Plani_EncabezadoHistorialPlanilla
    {
        public int emp_Id { get; set; }
        public string per_Identidad { get; set; }
        public int hipa_IdHistorialDePago { get; set; }
        public string NombreColaborador { get; set; }
        public string car_Descripcion { get; set; }
        public string area_Descripcion { get; set; }
        public int cpla_IdPlanilla { get; set; }
        public string cpla_DescripcionPlanilla { get; set; }
        public string fpa_Descripcion { get; set; }
        public Nullable<System.DateTime> hipa_FechaPago { get; set; }
        public Nullable<decimal> hipa_SueldoNeto { get; set; }
        public string peri_DescripPeriodo { get; set; }
    }
}
