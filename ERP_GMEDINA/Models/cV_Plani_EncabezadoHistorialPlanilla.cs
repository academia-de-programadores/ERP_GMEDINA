using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cV_Plani_EncabezadoHistorialPlanilla))]
    public partial class V_Plani_EncabezadoHistorialPlanilla
    {
    }

    public class cV_Plani_EncabezadoHistorialPlanilla
    {
        [Display(Name = "Codigo Empleado")]
        public int emp_Id { get; set; }

        [Display(Name = "Id Historial de Pago")]
        public int hipa_IdHistorialDePago { get; set; }

        [Display(Name = "Nombre")]
        public string NombreColaborador { get; set; }

        [Display(Name = "Cargo")]
        public string car_Descripcion { get; set; }
        [Display(Name = "Área")]
        public string area_Descripcion { get; set; }
        [Display(Name = "Id Planilla")]
        public int cpla_IdPlanilla { get; set; }

        [Display(Name = "Descripción")]
        public string cpla_DescripcionPlanilla { get; set; }

        [Display(Name = "Forma de Pago")]
        public string fpa_Descripcion { get; set; }
        [Display(Name = "Fecha de Pago")]
        public Nullable<System.DateTime> hipa_FechaPago { get; set; }

        [Display(Name = "Sueldo Neto")]
        public Nullable<decimal> hipa_SueldoNeto { get; set; }

        [Display(Name = "Periodo")]
        public string peri_DescripPeriodo { get; set; }
    }
}