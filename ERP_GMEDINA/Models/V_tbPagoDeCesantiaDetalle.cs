
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_tbPagoDeCesantiaDetalle
    {
        public int IdCesantia { get; set; }
        public int NoColaborador { get; set; }
        public string NoIdentidad { get; set; }
        public string NombreCompleto { get; set; }
        public int DiasPagados { get; set; }
        public decimal SueldoBrutoDiario { get; set; }
        public decimal TotalCesantiaPRO { get; set; }
        public string NoDeCuenta { get; set; }
    }
}
