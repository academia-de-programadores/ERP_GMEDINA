using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class DatosProfesionales
    {
        public List<tbCompetencias> Competencias { get; set; }
        public List<tbHabilidades>Habilidades { get; set; }
        public List<tbIdiomas> Idiomas { get; set; }
        public List<tbRequerimientosEspeciales> ReqEspeciales { get; set; }
        public List<tbTitulos> Titulos { get; set; }
        public int req_Id { get; set; }
        public DatosProfesionales()
        {
            this.Competencias = new List<tbCompetencias>();
            this.Habilidades = new List<tbHabilidades>();
            this.Idiomas = new List<tbIdiomas>();
            this.ReqEspeciales = new List<tbRequerimientosEspeciales>();
            this.Titulos = new List<tbTitulos>();
        }
    }
}