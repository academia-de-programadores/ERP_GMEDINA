
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VistaInventarioHistorico
    {
        public int bodd_Id { get; set; }
        public int bod_Id { get; set; }
        public string bod_Nombre { get; set; }
        public string Encargado { get; set; }
        public string bod_Telefono { get; set; }
        public string prod_Codigo { get; set; }
        public string prod_CodigoBarras { get; set; }
        public string prod_Descripcion { get; set; }
        public string pcat_Nombre { get; set; }
        public string prod_Marca { get; set; }
        public string prod_Modelo { get; set; }
        public string prod_Talla { get; set; }
        public string uni_Descripcion { get; set; }
        public decimal bodd_CantidadExistente { get; set; }
        public Nullable<decimal> cantidad_Por_Entrada { get; set; }
        public int entd_Id { get; set; }
        public int ent_Id { get; set; }
        public Nullable<decimal> Cantidad_por_salida { get; set; }
        public int sald_Id { get; set; }
        public int sal_Id { get; set; }
    }
}
