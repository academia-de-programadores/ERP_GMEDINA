using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class Personas
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
    }
}