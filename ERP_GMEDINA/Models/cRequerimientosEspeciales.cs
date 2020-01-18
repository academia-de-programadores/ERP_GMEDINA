using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cRequerimientosEspeciales))]

    public partial class tbRequerimientosEspeciales
    {
            
    }

    public class cRequerimientosEspeciales
    {
        [Display(Name = "Id")]
        public int resp_Id { get; set; }

        [Display(Name = "Descripción")]
        public string resp_Descripcion { get; set; }

        [Display(Name = "Estado")]
        public bool resp_Estado { get; set; }

        [Display(Name = "Razon Inactivo")]
        public string resp_RazonInactivo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int resp_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creacion")]
        public System.DateTime resp_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> resp_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificacion")]
        public Nullable<System.DateTime> resp_FechaModifica { get; set; }
    }
}