
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_RPT_EmpleadoCurriculum
    {
        public string per_Identidad { get; set; }
        public string NombreCompleto { get; set; }
        public string per_Telefono { get; set; }
        public string per_CorreoElectronico { get; set; }
        public Nullable<int> car_Id { get; set; }
        public string car_Descripcion { get; set; }
        public Nullable<int> depto_Id { get; set; }
        public string depto_Descripcion { get; set; }
        public Nullable<int> area_Id { get; set; }
        public string area_Descripcion { get; set; }
        public Nullable<int> suc_Id { get; set; }
        public string suc_Descripcion { get; set; }
        public string jor_Descripcion { get; set; }
        public Nullable<int> jor_Id { get; set; }
        public System.DateTime fechaInicio { get; set; }
        public System.DateTime Fechafin { get; set; }
    }
}
