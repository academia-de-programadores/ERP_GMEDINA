using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace ERP_GMEDINA.Helpers
{
    public class General
    {

        public bool SendEmail(ComprobantePagoModel Model)
        {
            #region Declaracion de variables
            bool response = true;
            string EmailOrigen = "lateos.info@gmail.com";
            string Contraseña = "Admin2305";
            string body = String.Empty;
            StringBuilder trDeduccionesIngresosTemplate = new StringBuilder();
            #endregion

            try
            {
                #region CREACIÓN DEL BODY DEL CORREO ELECTRÓNICO
                int totalIngresos = Model.Ingresos.Count();
                int totalDeducciones = Model.Deducciones.Count();
                int contador = totalDeducciones >= totalIngresos ? totalDeducciones : totalIngresos;
                List<IngresosDeduccionesVoucher> oDeduccionesColaborador = Model.Deducciones;
                List<IngresosDeduccionesVoucher> oIngresosColaborador = Model.Ingresos;
                string conceptoIngreso = String.Empty;
                string montoIngreso = String.Empty;
                string conceptoDeduccion = String.Empty;
                string montoDeduccion = String.Empty;

                for (int i = 0; i <= contador; i++)
                {
                    if (totalIngresos > i)
                    {
                        conceptoIngreso = oIngresosColaborador[i].concepto != null ? Convert.ToString(oIngresosColaborador[i].concepto) : "";
                        montoIngreso = oIngresosColaborador[i].monto == 0 ? "0" : $"L. {Convert.ToString(oIngresosColaborador[i].monto)}";
                    }
                    if (totalDeducciones > i)
                    {
                        conceptoDeduccion = oDeduccionesColaborador[i].concepto != null ? Convert.ToString(oDeduccionesColaborador[i].concepto) : "";
                        montoDeduccion = oDeduccionesColaborador[i].monto == 0 ? "0" : $"L. {Convert.ToString(oDeduccionesColaborador[i].monto)}";
                    }

                    trDeduccionesIngresosTemplate.Append(
                        "<tr>" +
                            "<td class='col1'>&nbsp;&nbsp;&nbsp;&nbsp;" + conceptoIngreso + "</td>" +
                            "<td class='col2'>&nbsp;&nbsp;&nbsp;&nbsp;" + montoIngreso + "</td>" +
                            "<td class='col3'>&nbsp;&nbsp;&nbsp;&nbsp;" + conceptoDeduccion + "</td>" +
                            "<td class='col4'>&nbsp;&nbsp;&nbsp;&nbsp;" + montoDeduccion + "</td>" +
                        "</tr>");
                    conceptoIngreso = string.Empty;
                    montoIngreso = string.Empty;
                    conceptoDeduccion = string.Empty;
                    montoDeduccion = string.Empty;
                }
                 
                using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/app/Voucher/voucher.html")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{idColaborador}", Model.idColaborador.ToString());
                body = body.Replace("{EmailAsunto}", Model.EmailAsunto);
                body = body.Replace("{NombreColaborador}", Model.NombreColaborador);
                body = body.Replace("{PeriodoPago}", Model.PeriodoPago);
                body = body.Replace("{trDeduccionesIngresos}", trDeduccionesIngresosTemplate.ToString());
                body = body.Replace("{totalIngresos}", Model.totalIngresos.ToString());
                body = body.Replace("{TotalDeucciones}", Model.totalDeducciones.ToString());
                body = body.Replace("{TotalPagar}", Model.NetoPagar.ToString());
                #endregion

                MailMessage oMailMessage = new MailMessage(EmailOrigen, Model.EmailDestino, Model.EmailAsunto, body);
                oMailMessage.IsBodyHtml = true;
                SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
                oSmtpClient.EnableSsl = true;
                oSmtpClient.UseDefaultCredentials = false;
                oSmtpClient.Port = 587;
                oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);
                oSmtpClient.Send(oMailMessage);
                oSmtpClient.Dispose();

            }
            catch
            {
                response = false;
            }
            return response;
        }
    }
}