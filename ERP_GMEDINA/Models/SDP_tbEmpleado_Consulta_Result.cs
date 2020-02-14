
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class SDP_tbEmpleado_Consulta_Result
    {
        public int emp_Id { get; set; }
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
        public string per_Sexo { get; set; }
        public Nullable<System.DateTime> per_FechaNacimiento { get; set; }
        public Nullable<int> tpi_Id { get; set; }
        public string per_Identidad { get; set; }
        public string per_Telefono { get; set; }
        public string per_Correoelectronico { get; set; }
        public string per_TipoSangre { get; set; }
        public string emp_Puesto { get; set; }
        public System.DateTime emp_FechaIngreso { get; set; }
        public string per_Direccion { get; set; }
        public string emp_RazonInactivacion { get; set; }
        public int emp_UsuarioCrea { get; set; }
        public System.DateTime emp_FechaCrea { get; set; }
        public Nullable<int> emp_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> emp_FechaModifica { get; set; }
        public bool emp_Estado { get; set; }
        public string emp_RazonSalida { get; set; }
        public System.DateTime emp_FechaDeSalida { get; set; }
    }
}
