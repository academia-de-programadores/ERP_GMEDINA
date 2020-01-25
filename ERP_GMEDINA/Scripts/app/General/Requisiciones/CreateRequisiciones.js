


function ListFill(obj) {
    var SlctCompetencias = $(".SlctCompetencias");
    var SlctHabilidades = $(".SlctHabilidades");
    var SlctIdiomas = $(".SlctIdiomas");
    var SlctReqEspeciales = $(".SlctReqEspeciales");
    var SlctTitulos = $(".SlctTitulos");
    obj.Competencias.forEach(function (index, value) {
        SlctCompetencias.append('<option value="' + index.comp_Id + '">' + index.comp_Descripcion + '</option>');
    });

    obj.Habilidades.forEach(function (index, value) {
        SlctHabilidades.append('<option value="' + index.habi_Id + '">' + index.habi_Descripcion + '</option>');
    });

    obj.Idiomas.forEach(function (index, value) {
        SlctIdiomas.append('<option value="' + index.idi_Id + '">' + index.idi_Descripcion + '</option>');
    });

    obj.ReqEspeciales.forEach(function (index, value) {
        SlctReqEspeciales.append('<option value="' + index.resp_Id + '">' + index.resp_Descripcion + '</option>');
    });

    obj.Titulos.forEach(function (index, value) {
        SlctTitulos.append('<option value="' + index.titu_Id + '">' + index.titu_Descripcion + '</option>');
    });

    SlctCompetencias.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacia', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
    SlctHabilidades.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacia', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
    SlctIdiomas.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacia', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
    SlctReqEspeciales.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacia', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
    SlctTitulos.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacia', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
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

function SetMinOn_req_EdadMaxima() {
    var EdadMax = $("#req_EdadMaxima");
    var EdadMin = $("#req_EdadMinima");
    EdadMax.attr({       // substitute your own
        "min": EdadMin.val()        // values (or variables) here
    });

    if (EdadMax.val() < EdadMin.val())
    {
        EdadMax.val(EdadMin.val());
    }
}

$(document).ready(function () {
    _ajax(null,
            '/Requisiciones/DualListBoxData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    ListFill(obj);
                }
            });

    var wizard = $("#Wizard").steps({
        enableCancelButton: false,
        onStepChanging: function (event, currentIndex, newIndex) {
            var Form = $("#tbRequisiciones").find("select, textarea, input").not("input[type='hidden']");
            Form.validate().settings.ignore = ":hidden";
            //if ($("#tbRequisiciones").find("#req_EdadMinima").val > 0 && $("#tbRequisiciones").find("#req_EdadMaxima").val() > 0 && $("#tbRequisiciones").find("#req_Vacantes").val() >= 1 )
            return Form.valid();
        },
        onFinishing: function (event, currentIndex) {
            var Form = $("#tbRequisiciones").find("select, textarea, input").not("input[type='hidden']");
           
            return Form.valid();
        },
        onFinished: function () {
            var SlctCompetencias = $(".SlctCompetencias");
            var SlctHabilidades = $(".SlctHabilidades");
            var SlctIdiomas = $(".SlctIdiomas");
            var SlctReqEspeciales = $(".SlctReqEspeciales");
            var SlctTitulos = $(".SlctTitulos");
            
            var data = { Competencias: SlctCompetencias.val(), Habilidades: SlctHabilidades.val(), Idiomas: SlctIdiomas.val(), ReqEspeciales: SlctReqEspeciales.val(), Titulos: SlctTitulos.val() };
            var Form = $("#tbRequisiciones").find("select, textarea, input").not("input[type='hidden']").serializeArray();
            tbRequisicion = serializar(Form);
            Form = JSON.stringify({ tbRequisiciones: tbRequisicion, DatosProfesionales: data });
            console.log(Form);

            if (tbRequisicion != null)
            {
                _ajax(Form,
                '/Requisiciones/Create',
                'POST',
                function (obj) {
                    if (obj != "-1" && obj != "-2" && obj != "-3") {
                       // $("#finish").attr("href", " ");
                        MsgSuccess("¡Exito!", "El registro se agregó de forma exitosa");
                        $("#finish").attr("href", " ");
                        setTimeout(function () { window.location.href = "/Requisiciones/Index"; }, 3000);
                    } else {
                        MsgError("Error", "No se agrego el registro, contacte al administrador");
                    }
                });
            }
            else
            {
                MsgError("Error", "por favor llene todos los campos de texto.");
            }
        },
    });
});

//, Habilidades: SlctHabilidades.val(), Idiomas: SlctIdiomas, ReqEspeciales: SlctReqEspeciales.val(), Titulos : SlctTitulos.val()