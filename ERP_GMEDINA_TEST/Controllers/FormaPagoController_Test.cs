using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ERP_GMEDINA.Controllers;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA_TEST.Controllers
{
    [TestClass]
    public class FormaPagoController_Test
    {
        //TEST DE METODOS DE ACCION DEL CONTROLADOR FormaPago

        //APLICACION DE LAS 3 "A" EN EL TESTING 
        //ARRANGE : PREPARAR 
        //ACT     : ACTUAR
        //ASSERT  : AFIRMAR

        [TestMethod]
        public void Create()
        {
            //
            //ARRANGE
            //

            //Instancia del controlador
            FormaPagoController _FormaPagoController = new FormaPagoController();
            //Instancia de la clase
            tbFormaPago tbFormaPago = new tbFormaPago();
            tbFormaPago.fpa_Descripcion = "TestProject";
            tbFormaPago.fpa_UsuarioCrea = 1;
            tbFormaPago.fpa_FechaCrea = DateTime.Now;
            //Variable para capturar el valor de retorno
            string ReturnValue = string.Empty;

            //
            //ACT
            //

            //Seteo de la variable para capturar el valor de retorno
            ReturnValue = (string)(_FormaPagoController.Create(tbFormaPago)).Data;

            //
            //ASSERT
            //
            Assert.IsTrue(ReturnValue == "bien");

        }
    }
}
