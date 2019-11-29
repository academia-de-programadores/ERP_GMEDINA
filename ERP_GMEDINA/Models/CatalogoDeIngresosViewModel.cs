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
    }

    public class CatalogoDePlanillasViewModel
    {
        public int idPlanilla { get; set; }
        public string descripcionPlanilla { get; set; }
        public int frecuenciaDias { get; set; }
    }
}