using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class LiquidacionViewModel
    {
        public Nullable<int>      emp_Id { get; set; }
        public Nullable<int> moli_Id { get; set; }
        public Nullable<DateTime> FechaLiquidacion { get; set; }
        public Nullable<decimal>  SalarioOrdinarioMensual_Liq { get; set; }
        public Nullable<decimal>  SalarioPromedioMensual_Liq { get; set; }
        public Nullable<decimal>  SalarioOrdinarioDiario_Liq { get; set; }
        public Nullable<decimal>  SalarioPromedioDiario_Liq { get; set; }
        public Nullable<decimal>  Preaviso_Liq { get; set; }
        public Nullable<decimal>  Cesantia_Liq { get; set; }
        public Nullable<decimal>  DecimoTercerMesProporcional_Liq { get; set; }
        public Nullable<decimal>  DecimoCuartoMesProporcional_Liq { get; set; }
        public Nullable<decimal>  VacacionesPendientes_Liq { get; set; }
        public Nullable<decimal>  SalariosAdeudados { get; set; }
        public Nullable<decimal>  OtrosPagos { get; set; }
        public Nullable<decimal>  PagoHEPendiente { get; set; }
        public Nullable<decimal>  ValorBonoEducativo { get; set; }
        public Nullable<decimal>  PagoSeptimoDia { get; set; }
        public Nullable<decimal>  BonoPorVacaciones { get; set; }
        public Nullable<decimal>  ReajusteSalarial { get; set; }
        public Nullable<decimal>  DecimoTercerMesAdeudado { get; set; }
        public Nullable<decimal>  DecimoCuartoMesAdeudado { get; set; }
        public Nullable<decimal>  BonificacionVacaciones { get; set; }
        public Nullable<decimal>  PagoPorEmbarazo { get; set; }
        public Nullable<decimal>  PagoPorLactancia { get; set; }
        public Nullable<decimal>  PrePosNatal { get; set; }
        public Nullable<decimal>  PagoPorDiasFeriado { get; set; }
        public Nullable<decimal>  MontoTotalLiquidacion { get; set; }
        public Nullable<int>      UsuarioCrea { get; set; }
        public Nullable<DateTime> FechaCrea { get; set; }
        public Nullable<int> UsuarioModifica { get; set; }
        public Nullable<DateTime> FechaModifica { get; set; }
    }

    public class SelectAreasEmpleadosViewModel
    {
        public string text { get; set; }

        public SelectEmpleadosViewModel children { get; set; }
    }

    public class SelectEmpleadosViewModel
    {
        public int id { get; set; }
        public string text { get; set; }
    }
}