
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

    SlctCompetencias.bootstrapDualListbox({ selectorMinimalHeight: 160 });
    SlctHabilidades.bootstrapDualListbox({ selectorMinimalHeight: 160 });
    SlctIdiomas.bootstrapDualListbox({ selectorMinimalHeight: 160 });
    SlctReqEspeciales.bootstrapDualListbox({ selectorMinimalHeight: 160 });
    SlctTitulos.bootstrapDualListbox({ selectorMinimalHeight: 160 });
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
                        MsgSuccess("¡Exito!", "Se ah agregado el registro");
                    } else {
                        MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
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