using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class ViewModelTasasDeCambio
    {
        public int tmon_Id { get; set; }
        public string tmon_Descripcion { get; set; }
        public decimal tmon_Cambio { get; set; }
    }
}