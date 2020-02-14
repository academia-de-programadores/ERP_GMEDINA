
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbBitacoraErrores
    {
        public int bite_Id { get; set; }
        public string bite_Pantalla { get; set; }
        public string bite_Usuario { get; set; }
        public Nullable<System.DateTime> bite_Fecha { get; set; }
        public string bite_MensajeError { get; set; }
        public string bite_Accion { get; set; }
    }
}
