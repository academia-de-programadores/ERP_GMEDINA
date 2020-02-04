
function ListFill(obj) {
    var SlctCompetencias = $(".SlctCompetencias");
    var SlctHabilidades = $(".SlctHabilidades");
    var SlctIdiomas = $(".SlctIdiomas");
    var SlctReqEspeciales = $(".SlctReqEspeciales");
    var SlctTitulos = $(".SlctTitulos");
    obj.forEach(function (index, value) {
        if (index.TipoDato == "C") {
            if (index.Seleccionado == 0)
                SlctCompetencias.append('<option value="' + index.Id + '">' + index.Descripcion + '</option>');
            else
                SlctCompetencias.append('<option value="' + index.Id + '" selected="selected">' + index.Descripcion + '</option>');
        }
        if (index.TipoDato == "H") {
            if (index.Seleccionado == 0)
                SlctHabilidades.append('<option value="' + index.Id + '">' + index.Descripcion + '</option>');
            else
                SlctHabilidades.append('<option value="' + index.Id + '" selected="selected">' + index.Descripcion + '</option>');
        }
        if (index.TipoDato == "I") {
            if (index.Seleccionado == 0)
                SlctIdiomas.append('<option value="' + index.Id + '">' + index.Descripcion + '</option>');
            else
                SlctIdiomas.append('<option value="' + index.Id + '" selected="selected">' + index.Descripcion + '</option>');
        }
        if (index.TipoDato == "R") {
            if (index.Seleccionado == 0)
                SlctReqEspeciales.append('<option value="' + index.Id + '">' + index.Descripcion + '</option>');
            else
                SlctReqEspeciales.append('<option value="' + index.Id + '" selected="selected">' + index.Descripcion + '</option>');
        }
        if (index.TipoDato == "T") {
            if (index.Seleccionado == 0)
                SlctTitulos.append('<option value="' + index.Id + '">' + index.Descripcion + '</option>');
            else
                SlctTitulos.append('<option value="' + index.Id + '" selected="selected">' + index.Descripcion + '</option>');
        }

    });
    SlctCompetencias.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacía', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
    SlctHabilidades.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacía', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
    SlctIdiomas.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacía', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
    SlctReqEspeciales.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacía', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
    SlctTitulos.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacía', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
};

function Req_check() {
    var ischecked = $("#req_Permanente").is(':checked');
    var req_Duracion = $("#req_Duracion");
    if (ischecked) {
        req_Duracion.prop("disabled", false);
        req_Duracion.val("");
    }
    else {
        req_Duracion.prop("disabled", true);
        req_Duracion.val("N/A");
    }
};


$(document).ready(function () {

   

    id = sessionStorage.getItem("IdRequisicion");
    _ajax(null,
            '/Requisiciones/DualListBoxData/' + id,
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    ListFill(obj);
                }
            })

    id = sessionStorage.getItem("IdRequisicion");

    //_ajax(null,
    //    '/Requisiciones/Edit/' + id,
    //    'GET',
    //    function (obj) {
    //        var req_Duracion = $("#req_Duracion");
    //        if (obj != "-1" && obj != "-2" && obj != "-3") {
    //            $("#tbRequisiciones").find("#req_Experiencia").val(obj[0].req_Experiencia);
    //            $("#tbRequisiciones").find("#req_Sexo").val($.trim(obj[0].req_Sexo));
    //            $("#tbRequisiciones").find("#req_Descripcion").val(obj[0].req_Descripcion);
    //            $("#tbRequisiciones").find("#req_EdadMinima").val(obj[0].req_EdadMinima);
    //            $("#tbRequisiciones").find("#req_EdadMaxima").val(obj[0].req_EdadMaxima);
    //            $("#tbRequisiciones").find("#req_EstadoCivil").val($.trim(obj[0].req_EstadoCivil));
    //            $("#tbRequisiciones").find("#req_EducacionSuperior").prop("checked", obj[0].req_EducacionSuperior);
    //            $("#tbRequisiciones").find("#req_Permanente").prop("checked", obj[0].req_Permanente);
    //            $("#tbRequisiciones").find("#req_Duracion").val(obj[0].req_Duracion);
    //            $("#tbRequisiciones").find("#req_Vacantes").val(obj[0].req_Vacantes);
    //            $("#tbRequisiciones").find("#req_FechaRequisicion").val(FechaFormatoSimple(obj[0].req_FechaRequisicion));
    //            $("#tbRequisiciones").find("#req_FechaContratacion").val(FechaFormatoSimple(obj[0].req_FechaContratacion));

    //            if(obj[0].req_Permanente == true)
    //            {
    //                req_Duracion.prop("disabled", false);
    //            }
    //        }
    //    });

    var wizard = $("#Wizard").steps({
        enableCancelButton: false,
        onFinished: function () {
            var SlctCompetencias = $(".SlctCompetencias");
            var SlctHabilidades = $(".SlctHabilidades");
            var SlctIdiomas = $(".SlctIdiomas");
            var SlctReqEspeciales = $(".SlctReqEspeciales");
            var SlctTitulos = $(".SlctTitulos");

            var data = { Competencias: SlctCompetencias.val(), Habilidades: SlctHabilidades.val(), Idiomas: SlctIdiomas.val(), ReqEspeciales: SlctReqEspeciales.val(), Titulos: SlctTitulos.val() };
            var Form = $("#tbRequisiciones").find("select, textarea, input").not("input[type='hidden']").serializeArray();
            tbRequisicion = serializar(Form);
            tbRequisicion.req_Id = id;
            Form = JSON.stringify({ tbRequisiciones: tbRequisicion, DatosProfesionales: data });
            

            if (tbRequisicion != null) {
                _ajax(Form,
                '/Requisiciones/Edit',
                'POST',
                function (obj) {
                    if (obj != "-1" && obj != "-2" && obj != "-3") {
                        MsgSuccess("¡Éxito!", "El registro se editó de forma exitosa.");
                        $("#finish").attr("href", " ");
                        setTimeout(function () { window.location.href = "/Requisiciones/Index"; }, 3000);
                    } else {
                        MsgError("Error", "No se editó el registro, contacte al administrador.");
                    }
                });
            }
            else {
                MsgError("Error", "Por favor llene todas las cajas de texto.");
            }
        },
    });
    var sexo = $("#SexVal").val();
    var EstCivil = $("#CiVal").val();
    var Duration = $("#Duration").val();
    var ReqDate = $("#ReqDate").val();
    var ConDate = $("#ConDate").val();
    var Nivel = $("#Nivel").val();

    var _ReqDate = moment(ReqDate).format();
    var _ConDate = moment(ConDate).format();

    var _ReqDate = _ReqDate.substring(0,10);
    var _ConDate = _ConDate.substring(0,10);


    $("#req_Sexo").val(sexo).change();
    $("#req_EstadoCivil").val(EstCivil).change();
    $("#req_FechaRequisicion").val(_ReqDate);
    $("#req_FechaContratacion").val(_ConDate);
    $("#req_niveleducativo").val(Nivel);

    var ischecked = $("#req_Permanente").is(':checked');
    var req_Duracion = $("#req_Duracion");
    if (ischecked) {
        req_Duracion.prop("disabled", false);
        req_Duracion.val(Duration);
    }
    else {
        req_Duracion.prop("disabled", true);
        req_Duracion.val("N/A");
    }
});