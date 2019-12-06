using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class LiquidacionViewModel
    {
    }

    public class SelectAreasEmpleadosViewModel
    {
        public string text { get; set; }

        public SelectEmpleadosViewModel children { get; set; }
    }

    public class SelectEmpleadosViewModel
    {
        public int id { get; set; }
        public string text { get; set; }
    }
}