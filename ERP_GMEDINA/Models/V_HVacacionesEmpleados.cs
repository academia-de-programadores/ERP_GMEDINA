namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_HVacacionesEmpleados
    {
        public int emp_Id { get; set; }
        public string emp_NombreCompleto { get; set; }
        public int car_Id { get; set; }
        public string car_Descripcion { get; set; }
        public int depto_Id { get; set; }
        public string depto_Descripcion { get; set; }
        public bool emp_Estado { get; set; }
        public System.DateTime emp_Fechaingreso { get; set; }
        public Nullable<int> DiasMax { get; set; }
        public int DiasTomados { get; set; }
        public Nullable<int> DiasTotales { get; set; }
        public Nullable<int> AnnioIngreso { get; set; }
        public Nullable<int> Annio { get; set; }
    }
}
