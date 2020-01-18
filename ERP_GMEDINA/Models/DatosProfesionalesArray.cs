using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class DatosProfesionalesArray
    {
        public int[] Competencias { get; set; }
        public int[] Habilidades { get; set; }
        public int[] Idiomas { get; set; }
        public int[] ReqEspeciales { get; set; }
        public int[] Titulos { get; set; }
        public int[] req_Id { get; set; }
        public DatosProfesionalesArray()
        {
            
        }
    }
}