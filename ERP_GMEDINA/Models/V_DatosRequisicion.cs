
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_DatosRequisicion
    {
        public string Descripcion { get; set; }
        public string TipoDato { get; set; }
        public int Data_Id { get; set; }
        public int req_Id { get; set; }
    }
}
