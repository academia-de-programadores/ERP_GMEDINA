
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UDV_Vent_ListadodePrecios
    {
        public int listp_Id { get; set; }
        public string listp_Nombre { get; set; }
        public Nullable<System.DateTime> listp_FechaInicioVigencia { get; set; }
        public Nullable<System.DateTime> listp_FechaFinalVigencia { get; set; }
        public Nullable<short> listp_Prioridad { get; set; }
        public string prod_CodigoBarras { get; set; }
        public string prod_Descripcion { get; set; }
        public string prod_Marca { get; set; }
        public string prod_Modelo { get; set; }
        public string prod_Talla { get; set; }
        public int uni_Id { get; set; }
        public decimal lispd_PrecioMayorista { get; set; }
        public decimal lispd_PrecioMinorista { get; set; }
    }
}
