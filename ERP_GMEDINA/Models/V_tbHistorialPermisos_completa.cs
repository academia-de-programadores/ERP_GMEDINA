namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_tbHistorialPermisos_completa
    {
        public int per_Id { get; set; }
        public string per_Identidad { get; set; }
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
        public System.DateTime per_FechaNacimiento { get; set; }
        public string per_Sexo { get; set; }
        public Nullable<int> per_Edad { get; set; }
        public int nac_Id { get; set; }
        public string per_Direccion { get; set; }
        public string per_Telefono { get; set; }
        public string per_CorreoElectronico { get; set; }
        public string per_EstadoCivil { get; set; }
        public string per_TipoSangre { get; set; }
        public bool per_Estado { get; set; }
        public string per_RazonInactivo { get; set; }
        public int per_UsuarioCrea { get; set; }
        public System.DateTime per_FechaCrea { get; set; }
        public Nullable<int> per_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> per_FechaModifica { get; set; }
        public int hper_Id { get; set; }
        public string tper_Descripcion { get; set; }
        public int emp_Id { get; set; }
        public int tper_Id { get; set; }
        public System.DateTime hper_fechaInicio { get; set; }
        public System.DateTime hper_fechaFin { get; set; }
        public int hper_Duracion { get; set; }
        public string hper_Observacion { get; set; }
        public bool hper_Justificado { get; set; }
        public int hper_PorcentajeIndemnizado { get; set; }
        public bool hper_Estado { get; set; }
        public string hper_RazonInactivo { get; set; }
        public System.DateTime hper_FechaCrea { get; set; }
        public Nullable<int> hper_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> hper_FechaModifica { get; set; }
    }
}
