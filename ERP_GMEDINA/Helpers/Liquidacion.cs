using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Helpers
{
    public class Liquidacion
    {
        #region Calcular las fechas, año de 360 días
        public static double Dias360Mes(DateTime fechaFin, int idEmpleado)
        {
            DateTime fechaInicio = DateTime.MinValue;
            using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
            {
                try
                {
                    fechaInicio = db.tbEmpleados.Where(x => x.emp_Id == idEmpleado).Select(x => x.emp_Fechaingreso).FirstOrDefault();
                }
                catch (Exception ex)
                {

                }
            }

            if (fechaInicio > fechaFin)
                return 0;

            int diaInicio = fechaInicio.Day;
            int mesInicio = fechaInicio.Month;
            int anioInicio = fechaInicio.Year;
            int diaFin = fechaFin.Day;
            int mesFin = fechaFin.Month;
            int anioFin = fechaFin.Year;

            if (diaInicio == 31 || EsElUltimoDiaDeFebrero(fechaInicio))
            {
                diaInicio = 30;
            }

            if (diaInicio == 30 && diaFin == 31)
            {
                diaFin = 30;
            }

            return ((anioFin - anioInicio) * 360) + ((mesFin - mesInicio) * 30) + (diaFin - diaInicio);
        }

        private static bool EsElUltimoDiaDeFebrero(DateTime date)
        {
            return date.Month == 2 && date.Day == DateTime.DaysInMonth(date.Year, date.Month);
        }
        #endregion

        #region Hecho, o Haciendo
        public static decimal? Salario(int idEmpleado)
        {
            decimal? salario = 0;
            //Cacular el salario en base a los ultimos 6 mesesde pago
            using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
            {
                var historialSalariosDB = ObtenerSalarios(idEmpleado, db).Take(6).ToList();
                decimal dHistorialSalariosDB = 0;

                if (historialSalariosDB.Count() > 0)
                {
                    //Hacer el promedio de x cantidad de meses meses de pago para el "Salario"
                    foreach (var item in historialSalariosDB)
                    {
                        dHistorialSalariosDB += item;
                    }
                    salario = (dHistorialSalariosDB / historialSalariosDB.Count());
                }
                else
                {
                    //Salario es el sueldo
                    salario = db.tbSueldos.Where(x => x.emp_Id == idEmpleado).Select(x => x.sue_Cantidad).FirstOrDefault();
                }
            }

            return salario;
        }

        public static decimal? SalarioOrdinarioDiario(decimal? salario)
        {
            //Salario Ordinario Diario: salario/30
            return (salario / 30);
        }

        /*
            No puede qeudar ninguno vacio o sin valor en los promedios de los ultimos 6 meses 
        */

        public static decimal? SalarioOrdinarioPromedioDiario(decimal? salario)
        {
            //Salario ordinario promedio diario = (salario * 14)/360
            return ((salario * 14) / 360);
        }

        public decimal SalarioPromedioDiaro(decimal salario, decimal promedioHorasExtras, decimal promedioBonificaciones)
        {
            //SalarioOrdinarioPromedioDiario + Horas Extras + Bonificaciones..

            return 0;
        }

        public decimal? AlimentacionOVicienda(decimal? salario)
        {
            return (salario * 0.20M);
        }
        public decimal? AlimentacionYVivienda(decimal? salario)
        {
            return (salario * 0.30M);
        }
        #endregion

        #region Por hacer
        public decimal Comisiones()
        {
            decimal comision = 0;
            //(Comisiones en los ultimos 6 meses / 6 )
            using (ERP_GMEDINAEntities db = new ERP_GMEDINAEntities())
            {
                //var promedioComisiones = db.tbEmpleadoComisiones.Where(x=> x.cc_Pagado =)

                //if (historialSalariosDB.Count() > 0)
                //{
                //    Hacer el promedio de x cantidad de meses meses de pago para el "Salario"
                //    foreach (var item in historialSalariosDB)
                //    {
                //        dHistorialSalariosDB += item;
                //    }
                //    salario = (dHistorialSalariosDB / historialSalariosDB.Count());
                //}
                //else
                //{
                //    Salario es el sueldo
                //    salario = db.tbSueldos.Where(x => x.emp_Id == idEmpleado).Select(x => x.sue_Cantidad).FirstOrDefault();
                //}
            }
            return comision;
        }

        public decimal HorasExtras()
        {
            //(Horas extras en los últimos 6 meses / 30) / 8
            return 0;
        }

        public decimal Bonificaciones()
        {
            //(Bonificaciones en los últimos 6 meses / 6)
            return 0;
        }

        public bool mas10Empleados()
        {
            //Ya se esta validando eso en la base de datos
            return true;
        }

        public void EsEmpresaDomestica()
        {

        }

        public void Preaviso()
        {
            /*
             (Salario Promedio Diario * 30) 
    	     (DependiendoCantidadTiempoTrabajado: Salario Promedio Diario * 60)         
             */


            /*
              Es la notificación por escrito con que una de las partes da por finalizada la relación laboral.
              Si no se realizó esta notificación deberá pagarse el equivalente del salario y el plazo de preaviso
              depende del tiempo de duración de la relación. En periodo de prueba no se otorga preaviso.
              Menos de 3 meses, se debe considerar 24 horas de preaviso; 
              de 3-6 meses, 1 semana; de 6 meses a 1 año, 2 semanas; de 1-2 años, 1 mes de preaviso; y más de 2 años, 2 meses.             
             */

            //Si esta en el perido de prueba no se le otorga preaviso



            //Menos de 3 meses, 24 horas de salario promedio diario
            //Es un dia de pago


            //De 3 a 6 meses, una semana de pago
            //7 dias de pago

            //De 6 meses a 1 año: una semana de pago
            //14 dias de pago

            //1 a 2 años: 1 mes de preaviso
            //30 dias de pago, en base a salario promedio diario


            //Mas de 2 años: 3 meses de preaviso
            //60 dias de pago.

        }

        public void Cesantia(decimal? salarioPromedioDiario, double dias)
        {
            double anios = 0, meses = 0, salarioCesantiaProporcional = 0;

            decimal? salario30Dias = (salarioPromedioDiario * 30),
                salario20Dias = (salarioPromedioDiario * 20),
                salario10Dias = (salarioPromedioDiario * 10),
                salarioCesantia = 0;

            decimal salario6Meses = 0,
                salario3Meses = 0;

            CalcularAniosMesesDias(ref anios, ref meses, ref dias);

            if (anios >= 1)
            {
                while (anios >= 1)
                {
                    anios -= 1;
                    salarioCesantia += salario30Dias;
                }

                if (meses >= 1)
                {
                    double cantidadDiasMes = 0;
                    double diasAnio = 0;
                    while (meses > 1)
                    {
                        meses -= 1;
                        cantidadDiasMes += 30;
                    }

                    cantidadDiasMes += dias;

                    double calculoDiasMesPor30 = (cantidadDiasMes * 30);

                    diasAnio = (calculoDiasMesPor30 / 360);
                    salarioCesantiaProporcional = (diasAnio * Decimal.ToDouble(salarioPromedioDiario.Value));
                }
            }

            if (meses >= 6 && anios < 1)
            {
                //continuar haciendo las siguientes validaciones...
                salario6Meses = ((decimal)salarioPromedioDiario * 20);
            }

            if (meses >= 3 && meses < 6)
            {
                salario3Meses = ((decimal)salarioPromedioDiario * 10);
            }

            //Tomare en cuenta el salario promedio diario para pagar los dias de salario
            /*
             (tiempo >=3 meses  && tiempo < 6 meses) 10 dias de salario
	         (tiempo >=6 meses  && tiempo < 1 año) 20 dias de salario
	         (tiempo >= 1 año) 30 dias de salario + la parte porporcional por fracciones             
             */

        }

        public static void CalcularAniosMesesDias(ref double anios, ref double meses, ref double dias)
        {
            while (dias >= 360)
            {
                dias -= 360;
                anios += 1;
            }

            while (dias >= 30)
            {
                dias -= 30;
                meses += 1;
            }
        }


        #endregion

        private static IQueryable<decimal> ObtenerSalarios(int Emp_Id, ERP_GMEDINAEntities db)
        {
            return db.tbHistorialDePago.Where(p => p.emp_Id == Emp_Id).Select(x => (decimal)x.hipa_SueldoNeto);
        }

        //Ejecutar calculos
        public static object EjecutarCalculosSalarios(int idEmpleado)
        {
            decimal? salario = Salario(idEmpleado).Value;
            decimal? salarioOrdinarioDiario = SalarioOrdinarioDiario(salario);
            decimal? salarioOrdinarioPromedioDiario = SalarioOrdinarioPromedioDiario(salario);
            decimal? salarioPromedioDiario = salarioOrdinarioPromedioDiario;

            salario = Math.Round((Decimal)salario, 2);
            salarioOrdinarioDiario = Math.Round((Decimal)salarioOrdinarioDiario, 2);
            salarioOrdinarioPromedioDiario = Math.Round((Decimal)salarioOrdinarioPromedioDiario, 2);
            salarioPromedioDiario = Math.Round((Decimal)salarioPromedioDiario, 2);

            return new { salario, salarioOrdinarioDiario, salarioOrdinarioPromedioDiario, salarioPromedioDiario };
        }
    }
}