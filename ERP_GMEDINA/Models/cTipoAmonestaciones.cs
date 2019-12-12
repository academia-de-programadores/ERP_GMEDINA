using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cTipoAmonestaciones))]
    public partial class tbTipoAmonestaciones
    {

    }
    public class cTipoAmonestaciones
    {
        [Display(Name = "Tipo Amonestacion")]
        public int tamo_Id { get; set; }
        //[Display(Name = "Tipo Amonestacion")]
        public string tamo_Descripcion { get; set; }
    }
}