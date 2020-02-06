using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class ViewModel_PlanillaImpuestoVecinal
    {
        public int No { get; set; }
        public int emp_Id { get; set; }
        public string NoIdentidad { get; set; }
        public int NombreCompleto { get; set; }
        public string TotalImpuestoVecinal { get; set; }
        public string DeduccionMensual { get; set; }
        public string NoDeCuenta { get; set; }
    }
}