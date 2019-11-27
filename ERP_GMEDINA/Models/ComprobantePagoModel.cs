using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP_GMEDINA.Models
{
    public class ComprobantePagoModel
    {
        //parametro que se enviarán en el correo electrónico
        [Display(Name = "Asunto")]
        public string EmailAsunto { get; set; }

        [Display(Name = "Colaborador")]
        public string NombreColaborador { get; set; }

        [Display(Name = "ID colaborador")]
        public int idColaborador { get; set; }

        [Display(Name = "Para")]
        public string EmailDestino { get; set; }

        [Display(Name = "Periodo de pago")]
        public string PeriodoPago { get; set; }

        [Display(Name = "Lista deducciones")]
        public List<IngresosDeduccionesVoucher> Deducciones { get; set; }

        [Display(Name = "Lista de ingresos")]
        public List<IngresosDeduccionesVoucher> Ingresos { get; set; }

        [Display(Name = "Colaborador")]
        public decimal totalDeducciones { get; set; }

        [Display(Name = "Colaborador")]
        public decimal totalIngresos { get; set; }

        [Display(Name = "Colaborador")]
        public decimal NetoPagar { get; set; }
    }

    public class IngresosDeduccionesVoucher
    {
        public decimal monto { get; set; }

        public string concepto { get; set; }
    }
}