using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cPreviewPlanilla))]
    public partial class V_PreviewPlanilla
    {
    }
    public class cPreviewPlanilla
    {
        [Display(Name = "No. empleado")]
        public int emp_Id { get; set; }
        [Display(Name = "Nombres")]
        public string Nombres { get; set; }
        [Display(Name = "Identidad")]
        public string per_Identidad { get; set; }
        [Display(Name = "Sexo")]
        public string per_Sexo { get; set; }
        [Display(Name = "Edad")]
        public Nullable<int> per_Edad { get; set; }
        [Display(Name = "Dirección")]
        public string per_Direccion { get; set; }
        [Display(Name = "Teléfono")]
        public string per_Telefono { get; set; }
        [Display(Name = "Correo electrónico")]
        public string per_CorreoElectronico { get; set; }
        [Display(Name = "Estado civil")]
        public string per_EstadoCivil { get; set; }
        [Display(Name = "Salario ordinario")]
        public decimal salarioBase { get; set; }
        [Display(Name = "ID tipo de moneda")]
        public int tmon_Id { get; set; }
        [Display(Name = "Tipo de moneda")]
        public string tmon_Descripcion { get; set; }
        [Display(Name = "ID planilla")]
        public int cpla_IdPlanilla { get; set; }
        [Display(Name = "Planilla")]
        public string cpla_DescripcionPlanilla { get; set; }
    }
}
