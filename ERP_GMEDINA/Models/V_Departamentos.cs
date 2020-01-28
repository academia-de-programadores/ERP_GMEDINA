namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_Departamentos
    {
        public int depto_Id { get; set; }
        public int area_Id { get; set; }
        public string depto_Descripcion { get; set; }
        public int depto_UsuarioCrea { get; set; }
        public System.DateTime depto_Fechacrea { get; set; }
        public Nullable<int> depto_Usuariomodifica { get; set; }
        public Nullable<System.DateTime> depto_Fechamodifica { get; set; }
        public int car_Id { get; set; }
        public string car_Descripcion { get; set; }
        public Nullable<int> per_Id { get; set; }
        public string per_NombreCompleto { get; set; }
        public string per_Telefono { get; set; }
        public string per_CorreoElectronico { get; set; }
    }
}
