
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class Prueba_Result
    {
        public int pemid_Id { get; set; }
        public string pemi_NumeroCAI { get; set; }
        public string dfisc_Descripcion { get; set; }
        public string pemid_RangoInicio { get; set; }
        public string pemid_RangoFinal { get; set; }
        public System.DateTime pemid_FechaLimite { get; set; }
        public string UsuarioCrea { get; set; }
        public System.DateTime pemid_FechaCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public Nullable<System.DateTime> pemid_FechaModifica { get; set; }
    }
}
