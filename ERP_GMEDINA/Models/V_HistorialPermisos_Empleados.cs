namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_HistorialPermisos_Empleados
    {
        public int Id { get; set; }
        public string Identidad { get; set; }
        public string Nombre { get; set; }
        public string Sexo { get; set; }
        public Nullable<int> Edad { get; set; }
    }
}
