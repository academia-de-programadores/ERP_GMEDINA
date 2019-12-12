using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class RequisicionesData
    {
        public List<tbCompetenciasRequisicion> Competencias { get; set; }
        public List<tbHabilidadesRequisicion>Habilidaes { get; set; }
        public List<tbIdiomasRequisicion> Idiomas { get; set; }
        public List<tbRequerimientosEspecialesRequisicion> ReqEspeciales { get; set; }
        public List<tbTitulosRequisicion> Titulos { get; set; }
    }
}