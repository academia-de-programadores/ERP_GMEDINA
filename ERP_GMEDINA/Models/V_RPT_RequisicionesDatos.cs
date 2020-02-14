
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_RPT_RequisicionesDatos
    {
        public int req_Id { get; set; }
        public string req_Experiencia { get; set; }
        public string req_Sexo { get; set; }
        public string req_Descripcion { get; set; }
        public int req_EdadMinima { get; set; }
        public int req_EdadMaxima { get; set; }
        public string req_EstadoCivil { get; set; }
        public bool req_EducacionSuperior { get; set; }
        public bool req_Permanente { get; set; }
        public string req_Duracion { get; set; }
        public string req_Vacantes { get; set; }
        public int req_VacantesOcupadas { get; set; }
        public Nullable<System.DateTime> req_FechaRequisicion { get; set; }
        public Nullable<System.DateTime> req_FechaContratacion { get; set; }
        public Nullable<int> resp_Id { get; set; }
        public string resp_Descripcion { get; set; }
        public Nullable<int> titu_Id { get; set; }
        public string titu_Descripcion { get; set; }
        public Nullable<int> comp_Id { get; set; }
        public string comp_Descripcion { get; set; }
        public Nullable<int> habi_Id { get; set; }
        public string habi_Descripcion { get; set; }
        public Nullable<int> idi_Id { get; set; }
        public string idi_Descripcion { get; set; }
    }
}
