
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_InformacionColaborador
    {
        public int emp_Id { get; set; }
        public bool emp_Estado { get; set; }
        public Nullable<int> area_Id { get; set; }
        public string area_Descripcion { get; set; }
        public Nullable<int> depto_Id { get; set; }
        public string depto_Descripcion { get; set; }
        public Nullable<int> jor_Id { get; set; }
        public string jor_Descripcion { get; set; }
        public Nullable<int> per_Id { get; set; }
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
        public string per_Identidad { get; set; }
        public string per_Sexo { get; set; }
        public Nullable<int> per_Edad { get; set; }
        public string per_Direccion { get; set; }
        public string per_Telefono { get; set; }
        public string per_CorreoElectronico { get; set; }
        public string per_EstadoCivil { get; set; }
        public Nullable<decimal> SalarioBase { get; set; }
        public int cpla_IdPlanilla { get; set; }
        public string emp_CuentaBancaria { get; set; }
        public Nullable<int> fpa_IdFormaPago { get; set; }
        public string fpa_Descripcion { get; set; }
        public Nullable<int> car_Id { get; set; }
        public string car_Descripcion { get; set; }
    }
}
