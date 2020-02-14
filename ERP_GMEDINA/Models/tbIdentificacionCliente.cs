
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbIdentificacionCliente
    {
        public byte tpi_Id { get; set; }
        public bool clte_ConsumidorFinal { get; set; }
    
        public virtual tbTipoIdentificacion tbTipoIdentificacion { get; set; }
    }
}
