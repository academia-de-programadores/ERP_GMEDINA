
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_RPT_Requisiciones
    {
        public int req_Id { get; set; }
        public Nullable<System.DateTime> fechaInicio { get; set; }
        public Nullable<System.DateTime> fechaFin { get; set; }
        public string NombreCimpleto { get; set; }
        public string per_Identidad { get; set; }
        public string req_Duracion { get; set; }
        public Nullable<int> req_EdadMaxima { get; set; }
        public Nullable<int> req_EdadMinima { get; set; }
        public Nullable<bool> req_EducacionSuperior { get; set; }
        public string req_Vacantes { get; set; }
        public string req_Experiencia { get; set; }
        public Nullable<System.DateTime> req_FechaContratacion { get; set; }
        public string req_Descripcion { get; set; }
        public string req_Sexo { get; set; }
        public Nullable<bool> req_Permanente { get; set; }
    }
}
