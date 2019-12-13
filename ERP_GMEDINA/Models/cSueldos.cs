using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cSueldos))]
    public partial class tbSueldos
    {
    }
    public class cSueldos
    {
        public int sue_Id { get; set; }
        public int emp_Id { get; set; }
        public int tmon_Id { get; set; }
        public Nullable<decimal> sue_Cantidad { get; set; }
        public Nullable<int> sue_SueldoAnterior { get; set; }
        public bool sue_Estado { get; set; }
        public string sue_RazonInactivo { get; set; }
        public int sue_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> sue_FechaCrea { get; set; }
        public Nullable<int> sue_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> sue_FechaModifica { get; set; }
    }
}