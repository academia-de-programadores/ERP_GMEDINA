using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class CatalogoDeIngresosDeduccionesViewModel
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public bool check { get; set; }
        public CheckId checkId { get; set; }
    }

    public class CatalogoDePlanillasViewModel
    {
        public int idPlanilla { get; set; }
        public string descripcionPlanilla { get; set; }
        public int frecuenciaDias { get; set; }
        public string descripcionPeriodo { get; set; }
        public string recibeComision { get; set; }
		public ActivoAdmin activoAdmin { get; set; }
        public bool esAdmin { get; set; }
    }

    public class ActivoAdmin
    {
        public bool esAdmin { get; set; }
        public bool activo { get; set; }
    }

    public class CheckId
    {
        public int id { get; set; }
        public bool check { get; set; }
    }
}