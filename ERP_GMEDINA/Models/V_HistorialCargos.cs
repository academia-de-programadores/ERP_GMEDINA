
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_HistorialCargos
    {
        public int hcar_Id { get; set; }
        public string Nombre_Completo { get; set; }
        public string CargoAnterior { get; set; }
        public string CargoNuevo { get; set; }
        public Nullable<System.DateTime> Fecha_de_Historial { get; set; }
        public string Usuario_Crea { get; set; }
        public System.DateTime Fecha_Crea { get; set; }
        public string Usuario_Modifica { get; set; }
        public Nullable<System.DateTime> Fecha_Modifica { get; set; }
        public int PuedeDeshacer { get; set; }
    }
}
