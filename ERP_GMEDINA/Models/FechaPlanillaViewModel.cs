using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class FechaPlanillaViewModel
    {
        public int idPlanilla { get; set; }
        public string DescripcionPlanilla { get; set; }
        public DateTime FechaPlanilla { get; set; }
		public int anioPlanilla { get; set; }
    }
}