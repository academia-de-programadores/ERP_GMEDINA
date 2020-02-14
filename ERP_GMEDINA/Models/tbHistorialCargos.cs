
namespace ERP_GMEDINA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbHistorialCargos
    {
        public int hcar_Id { get; set; }
        public int emp_Id { get; set; }
        public Nullable<int> car_IdAnterior { get; set; }
        public int car_IdNuevo { get; set; }
        public Nullable<System.DateTime> hcar_Fecha { get; set; }
        public bool hcar_Estado { get; set; }
        public string hcar_RazonInactivo { get; set; }
        public int hcar_UsuarioCrea { get; set; }
        public System.DateTime hcar_FechaCrea { get; set; }
        public Nullable<int> hcar_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> hcar_FechaModifica { get; set; }
        public string hcar_RazonPromocion { get; set; }
        public int area_IdAnterior { get; set; }
        public int area_IdNuevo { get; set; }
        public int depto_IdAnterior { get; set; }
        public int depto_IdNuevo { get; set; }
        public int jor_IdAnterior { get; set; }
        public int jor_IdNuevo { get; set; }
    
        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbAreas tbAreas { get; set; }
        public virtual tbAreas tbAreas1 { get; set; }
        public virtual tbCargos tbCargos { get; set; }
        public virtual tbCargos tbCargos1 { get; set; }
        public virtual tbDepartamentos tbDepartamentos { get; set; }
        public virtual tbDepartamentos tbDepartamentos1 { get; set; }
        public virtual tbEmpleados tbEmpleados { get; set; }
        public virtual tbJornadas tbJornadas { get; set; }
        public virtual tbJornadas tbJornadas1 { get; set; }
    }
}
