using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class ViewModelListaErrores
    {
        public string Identidad { get; set; }
        public string NombreColaborador { get; set; }
        public string Error { get; set; }
        public string PosibleSolucion { get; set; }

    }
}