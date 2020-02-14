
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_RPT_EmpleadoCurriculum_Personas
    {
        public Nullable<int> per_Id { get; set; }
        public int emp_Id { get; set; }
        public string Nombre { get; set; }
        public string Identidad { get; set; }
        public Nullable<int> Edad { get; set; }
        public string Direccion { get; set; }
        public Nullable<System.DateTime> Fecha_Nacimiento { get; set; }
        public string Estado_Civil { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string Correo_Electronico { get; set; }
        public string Tipo_Sangre { get; set; }
        public Nullable<int> hape_Id { get; set; }
        public Nullable<int> cope_Id { get; set; }
        public Nullable<int> habi_Id { get; set; }
        public Nullable<int> comp_Id { get; set; }
        public System.DateTime fechaIngreso { get; set; }
        public System.DateTime fechaFin { get; set; }
        public string Habilidades { get; set; }
        public string Competencias { get; set; }
    }
}
