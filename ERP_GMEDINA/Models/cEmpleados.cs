using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cEmpleados))]
    public partial class tbEmpleados
    {

    }
    public class cEmpleados
    {
        [Display(Name = "Id")]
        public int emp_Id { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\" es requerido")]
        //[Display(Name = "Nombres")]
        //[MaxLength(50, ErrorMessage = "Excedio el numero maximo de caracteres")]
        //public int per_Nombres { get; set; }

        [Display (Name = "Cargo")]
        public int car_Id { get; set; }

        [Display(Name = "Area")]
        public int area_Id { get; set; }

        [Display(Name = "Departamento")]
        public int depto_Id { get; set; }

        [Display(Name = "Jornada")]
        public int jor_Id { get; set; }

        public int cpla_IdPlanilla { get; set; }
        public int fpa_IdFormaPago { get; set; }
        public string emp_CuentaBancaria { get; set; }
        public bool emp_Reingreso { get; set; }
        public System.DateTime emp_Fechaingreso { get; set; }
        public string emp_RazonSalida { get; set; }
        public Nullable<int> emp_CargoAnterior { get; set; }
        public Nullable<System.DateTime> emp_FechaDeSalida { get; set; }
        public bool emp_Estado { get; set; }
        public string emp_RazonInactivo { get; set; }
        public int emp_UsuarioCrea { get; set; }
        public System.DateTime emp_FechaCrea { get; set; }
        public Nullable<int> emp_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> emp_FechaModifica { get; set; }

        public virtual tbUsuario tbUsuario { get; set; }
        public virtual tbUsuario tbUsuario1 { get; set; }
        public virtual tbAreas tbAreas { get; set; }
        public virtual tbCargos tbCargos { get; set; }
        public virtual tbPersonas tbPersonas { get; set; }
        public virtual tbDepartamentos tbDepartamentos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSueldos> tbSueldos { get; set; }
        public virtual tbJornadas tbJornadas { get; set; }
    }

}