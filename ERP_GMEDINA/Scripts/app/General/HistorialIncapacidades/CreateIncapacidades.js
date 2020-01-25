
    id = sessionStorage.getItem("IdPersona");
    $("#MeterId").find("#emp_idd").val(id)


    function Fecha() {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!
        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd
        }
        if (mm < 10) {
            mm = '0' + mm
        }
        return today = yyyy + '-' + mm + '-' + dd;
    }



 
   
   
  


    //$("#hinc_FechaInicio").attr("min", Fecha());
    //$("#hinc_FechaFin").attr("min", Fecha());
 

    
    var fecha1 = $("#hinc_FechaInicio").val;

    var fecha2 = $("#hinc_FechaFin").val;



    function compare_dates() {
        
        var fecha1 = $("#hinc_FechaInicio").val();
        var fecha2 = $("#hinc_FechaFin").val();
   
        var fechalimite = '01/01/1900';
        var fechamaxima = '12/31/2199';

        if ($("#ticn_Id").val() == 0 && fecha1 == "" && fecha2 == "")
        {
            MsgError("Error", "por favor llene todas las cajas de texto.")
        }

        else if ($("#ticn_Id").val() == 0) {
            MsgError("Error", "Es nesesario seleccionar el tipo de incapacidad.")
        }
        else if (fecha1 == "" && fecha2 == "") {
            MsgError("Error", "Fecha inicio y fecha fin son requeridas.")
        }

        else if (fecha1 == "") {
            MsgError("Error", "Fecha inicio es requerida.")

        }
        else if (fecha2 == "") {
            MsgError("Error", "Fecha fin es requerida.")

        }



        else if ( Date.parse(fecha1) > Date.parse(fecha2) ) {
            if (Date.parse(fecha1) < Date.parse(fechalimite) || Date.parse(fecha1) > Date.parse(fechamaxima))
            {
                
            }
            else if(Date.parse(fecha1) > Date.parse(fechalimite) && Date.parse(fecha2) < Date.parse(fechalimite)  )
            {

            }
            else {
                MsgError("Error", "La fecha fin debe ser mayor ");
            }
        }

        else if (Date.parse(fecha1) == Date.parse(fecha2)) {
           
            if (Date.parse(fecha1) < Date.parse(fechalimite) && Date.parse(fecha2) < Date.parse(fechalimite) || Date.parse(fecha1) > Date.parse(fechamaxima) && Date.parse(fecha2) > Date.parse(fechamaxima)) {

            }
            else {
                MsgError("Error", "La fecha inicio y la fecha fin no pueden ser iguales");
            }
        } 


        else if (Date.parse(fecha1) < Date.parse(fechalimite))
        {

        }
       
        else if (Date.parse(fecha2) > Date.parse(fechamaxima)) {

        }

        else {

            return true;
        }
    }


    $("#btnGuardar").click(function () {
    
            var data = $("#FormNuevo").serializeArray();
            data = serializar(data);
            if (compare_dates())
                if (data != null) {
                    data = JSON.stringify({ tbHistorialIncapacidades: data });
                    if (compare_dates()) {
                        _ajax(data,
                            '/HistorialIncapacidades/Create',
                            'POST',
                            function (obj) {
                                if (obj != "-1" && obj != "-2" && obj != "-3") {
                                    MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.");

                                    $("#btnGuardar").attr("disabled", "disabled");
                                    setTimeout(function () { window.location.href = "Index"; }, 4000);
                                } else {
                                    MsgError("Error", "No se agregó el registro, contacte al administrador.");
                                }
                            })
                    };
                } else {
                    MsgError("Error", "por favor llene todas las cajas de texto");
                }
        } );

    
    
    _ajax({ ID: parseInt(id) },
            '/HistorialIncapacidades/Detallesempleados/',
            'GET',
            function (obj) {

                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    //$("#ModalDetalles").find("#emp_Id")["0"].innerText = obj.NombreCompleto;
                    $("#llamardata").find("#nombre")["0"].innerText = obj.tbPersonas.per_Nombres;
                    $("#llamardataa").find("#identidad")["0"].innerText = obj.tbPersonas.per_Identidad;
                    //$("#ModalDetalles").find("#hinc_CentroMedico")["0"].innerText = obj.hinc_CentroMedico;

                 
                }
            });

    
