
namespace ERP_GMEDINA.Models
{
    using System;
    
    public partial class SPGetResponsableBodega_Result
    {
        public int bod_Id { get; set; }
        public Nullable<int> bod_ResponsableBodega { get; set; }
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
    }
}
