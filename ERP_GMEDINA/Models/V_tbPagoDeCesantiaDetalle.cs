
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_tbPagoDeCesantiaDetalle
    {
        public int IdCesantia { get; set; }
        public int emp_Id { get; set; }
        public int per_Id { get; set; }
        public string NoIdentidad { get; set; }
        public string NombreCompleto { get; set; }
        public int Depto_Id { get; set; }
        public string DeptoDescripcion { get; set; }
        public int Area_Id { get; set; }
        public string Area_Descripcion { get; set; }
        public Nullable<int> DiasPagados { get; set; }
        public Nullable<decimal> ConSueldo { get; set; }
        public Nullable<decimal> TotalCesantiaColaborador { get; set; }
        public string NoDeCuenta { get; set; }
        public Nullable<int> pdcd_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> pdcd_FechaCrea { get; set; }
        public Nullable<int> pdcd_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> pdcd_FechaModifica { get; set; }
    }
}
