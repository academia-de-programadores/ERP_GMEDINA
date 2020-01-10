using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class DatosProfessionalesEdit
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string TipoDato { get; set; }
        public int Seleccionado { get; set; }

    }
}