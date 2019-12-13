using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class RequisicionesData
    {
        public List<tbCompetencias> Competencias { get; set; }
        public List<tbHabilidades>Habilidaes { get; set; }
        public List<tbIdiomas> Idiomas { get; set; }
        public List<tbRequerimientosEspeciales> ReqEspeciales { get; set; }
        public List<tbTitulos> Titulos { get; set; }
        public int req_Id { get; set; }
        public RequisicionesData()
        {
            this.Competencias = new List<tbCompetencias>();
            this.Habilidaes = new List<tbHabilidades>();
            this.Idiomas = new List<tbIdiomas>();
            this.ReqEspeciales = new List<tbRequerimientosEspeciales>();
            this.Titulos = new List<tbTitulos>();
        }
    }
}