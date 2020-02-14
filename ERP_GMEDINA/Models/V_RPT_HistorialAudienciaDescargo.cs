
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_RPT_HistorialAudienciaDescargo
    {
        public int aude_Id { get; set; }
        public string nombre { get; set; }
        public string per_Identidad { get; set; }
        public string aude_Descripcion { get; set; }
        public System.DateTime fechaAudiencia { get; set; }
        public System.DateTime fecha { get; set; }
    }
}
