using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class ExcelEmpleados
    {
        public string per_Identidad { get; set; }
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
        public string per_FechaNacimiento { get; set; }
        public string per_Edad { get; set; }
        public string per_Sexo { get; set; }
        public string nac_Id { get; set; }
        public string per_Direccion { get; set; }
        public string per_Telefono { get; set; }
        public string per_CorreoElectronico { get; set; }
        public string per_EstadoCivil { get; set; }
        public string per_TipoSangre { get; set; }
        public IEnumerable<object> Cargo { get; set; }
        public string area_Id { get; set; }
        public string depto_Id { get; set; }
        public string jor_Id { get; set; }
        public string cpla_IdPlanilla { get; set; }
        public string fpa_IdFormaPago { get; set; }
        public string emp_FechaIngreso { get; set; }
        public string emp_CuentaBancaria { get; set; }
        public string sue_Cantidad { get; set; }
        public string tmon_Id { get; set; }



    }
}