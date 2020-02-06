
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
  
        
         if ( Date.parse(fecha1) > Date.parse(fecha2) ) {
            
                MsgError("Error", "La fecha fin debe ser mayor.");
         }

             //else if ($("#hinc_Espermanente").val() == 1) {
             //    MsgError("Error", "Es nesesario seleccionar la incapacidad.")
             //}
        

        else if (Date.parse(fecha1) == Date.parse(fecha2)) {
           
          
                MsgError("Error", "La fecha inicio y la fecha fin no pueden ser iguales.");
            
        } 


        else {

            return true;
        }
    }


    
   


    function llamarmodaldetalle() {
        

        //var identidad = $("#llamardataa").find("#identidad").val();
        //var empleado = $("#llamardata").find("#nombre").val();
        var hospital = $("#hinc_CentroMedico").val();
        var Diagnostico = $("#hinc_Diagnostico").val();
        var FechaInicio = $("#hinc_FechaInicio").val();
        var Fechafin = $("#hinc_FechaFin").val();
        var incapacidad = "Permanente";

        debugger
        _ajax({ ID: parseInt(id) },
        '/HistorialIncapacidades/Detallesempleados/',
        'GET',
        function (obj) {

            if (obj != "-1" && obj != "-2" && obj != "-3") {
              
                var empleado = $("#llamardata").find("#nombre")["0"].innerText = obj.tbPersonas.per_Nombres;
                var identidad = $("#llamardataa").find("#identidad")["0"].innerText = obj.tbPersonas.per_Identidad;
         

                var modalnuevo = $("#ModalDetalles");
                $("#ModalDetalles").find("#nombre")["0"].innerText = empleado;
                $("#ModalDetalles").find("#identidad")["0"].innerText = identidad;
                $("#ModalDetalles").find("#hinc_CentroMedico")["0"].innerText = hospital;
                $("#ModalDetalles").find("#hinc_Diagnostico")["0"].innerText = Diagnostico;
                $("#ModalDetalles").find("#hinc_FechaInicio")["0"].innerText = FechaInicio;
                $("#ModalDetalles").find("#hinc_FechaFin")["0"].innerText = Fechafin;
                $("#ModalDetalles").find("#tipincapacidad")["0"].innerText = incapacidad;
                modalnuevo.modal('show');
            }
        });
  
    }


    

    $("#btnGuardar").click(function () {
        debugger
        if (compare_dates())
            if ($("#hinc_Espermanente").val() == "true") {

                llamarmodaldetalle()

                $("#btnSi").click(function () {

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
                                            $("#ModalDetalles").modal('hide');
                                            MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.");

                                            $("#btnGuardar").attr("disabled", "disabled");
                                            $("#btncancelar").attr("disabled", "disabled");
                                            setTimeout(function () { window.location.href = "Index"; }, 4000);
                                        } else {
                                            MsgError("Error", "No se agregó el registro, contacte al administrador.");
                                        }
                                    })
                            };
                        } else {
                            MsgError("Error", "Por favor llene todas las cajas de texto.");
                        }

                })
            }
            else {
                var data = $("#FormNuevo").serializeArray();
                data = serializar(data);
                if (compare_dates())
                    if (data != null) {
                        var incapacidad = data.hinc_Espermanente
                        if (incapacidad==1)
                        {
                            MsgError("Error", "Es nesesario seleccionar la incapacidad.");
                        }
                        else{

                        data = JSON.stringify({ tbHistorialIncapacidades: data });
                        if (compare_dates()) {
                            _ajax(data,
                                '/HistorialIncapacidades/Create',
                                'POST',
                                function (obj) {
                                    if (obj != "-1" && obj != "-2" && obj != "-3") {
                                        MsgSuccess("¡Éxito!", "El registro se agregó de forma exitosa.");

                                        $("#btnGuardar").attr("disabled", "disabled");
                                        $("#btncancelar").attr("disabled", "disabled");
                                        setTimeout(function () { window.location.href = "Index"; }, 4000);
                                    } else {
                                        MsgError("Error", "No se agregó el registro, contacte al administrador.");
                                    }
                                })
                        };
                    }
            }
                else {
                    MsgError("Error", "Por favor llene todas las cajas de texto.");
                }
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

    
