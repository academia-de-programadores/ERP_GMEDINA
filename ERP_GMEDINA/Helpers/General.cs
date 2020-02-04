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
            ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
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
                        montoIngreso = oIngresosColaborador[i].monto == 0 ? "0" : oIngresosColaborador[i].concepto == "Horas extras" ? $"{Convert.ToString(oIngresosColaborador[i].monto)}" : $"{Convert.ToString(oIngresosColaborador[i].monto)} {Model.moneda}";
                    }
                    if (totalDeducciones > i)
                    {
                        conceptoDeduccion = oDeduccionesColaborador[i].concepto != null ? Convert.ToString(oDeduccionesColaborador[i].concepto) : "";
                        montoDeduccion = oDeduccionesColaborador[i].monto == 0 ? "0" : $"{Convert.ToString(oDeduccionesColaborador[i].monto)} {Model.moneda}";
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
                body = body.Replace("{totalIngresos}", Math.Round(Model.totalIngresos.Value, 2).ToString());
                body = body.Replace("{TotalDeucciones}", Model.totalDeducciones.ToString());
                body = body.Replace("{TotalPagar}",Math.Round(Model.NetoPagar.Value,2).ToString());
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
            catch (Exception ex)
            {

                response = false;                

                // ejecutar el procedimiento almacenado
                db.UDP_Acce_tbBitacoraErrores_Insert("sendEmailPlanilla","Error",DateTime.Now, ex.Message.ToString(), "SendMail");

                
            }
            return response;
        }

        public static string ObtenerEmpleados()
        {
            using (Models.ERP_GMEDINAEntities db = new Models.ERP_GMEDINAEntities())
            {
                var json = "";
                try
                {
                    var jsonAreasEmpleados = db.UDP_Plani_EmpleadosPorAreas_Select();
                    foreach (UDP_Plani_EmpleadosPorAreas_Select_Result result in jsonAreasEmpleados)
                        json = result.json;
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    return "Error";
                }
                return json;
            }
        }

       public class iziToast
        {
            public string Response { get; set; }
            public string Encabezado { get; set; }
            public string Tipo { get; set; }
        }
    }
}