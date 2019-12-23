using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cEmpleados))]
    //public partial class tbAreas
    //{
    //    public string car_Descripcion { get; set; }
    //}
    public class cEmpleados
    {
        [Display(Name = "Cargo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres")]
        public string car_Descripcion { get; set; }

        [Display(Name = "Id empleado")]
        public int emp_Id { get; set; }
        [Display(Name = "Id persona")]
        public int per_Id { get; set; }
        [Display(Name = "Id cargo")]
        public int car_Id { get; set; }
        [Display(Name = "Id area")]
        public int area_Id { get; set; }
        [Display(Name = "Id departamento")]
        public int depto_Id { get; set; }
        [Display(Name = "Id jornada")]
        public int jor_Id { get; set; }
        [Display(Name = "Id planilla")]
        public int cpla_IdPlanilla { get; set; }
        [Display(Name = "Id Forma de pago")]
        public int fpa_IdFormaPago { get; set; }
        [MaxLength(100, ErrorMessage = "Excedió el número máximo de carácteres")]
        [Display(Name = "Cuenta bancaria")]
        public string emp_CuentaBancaria { get; set; }
        [Display(Name = "Reingreso")]
        public bool emp_Reingreso { get; set; }
        [Display(Name = "Fecha ingreso")]
        public System.DateTime emp_Fechaingreso { get; set; }
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres")]
        [Display(Name = "Razon salida")]
        public string emp_RazonSalida { get; set; }
        [Display(Name = "Cargo anterior")]
        public Nullable<int> emp_CargoAnterior { get; set; }
        [Display(Name = "Fecha de salida")]
        public Nullable<System.DateTime> emp_FechaDeSalida { get; set; }
        [Display(Name = "Estado")]
        public bool emp_Estado { get; set; }
        [MaxLength(100, ErrorMessage = "Excedió el número máximo de carácteres")]
        public string emp_RazonInactivo { get; set; }
        [Display(Name = "Ingresado por")]
        public int emp_UsuarioCrea { get; set; }
        [Display(Name = "Fecha ingreso")]
        public System.DateTime emp_FechaCrea { get; set; }
        [Display(Name = "Modificado por")]
        public Nullable<int> emp_UsuarioModifica { get; set; }
        [Display(Name = "Fecha modificación")]
        public Nullable<System.DateTime> emp_FechaModifica { get; set; }
    }
}