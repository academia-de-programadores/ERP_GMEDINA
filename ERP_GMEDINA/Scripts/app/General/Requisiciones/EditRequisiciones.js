
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

    SlctCompetencias.bootstrapDualListbox({ selectorMinimalHeight: 160 });
    SlctHabilidades.bootstrapDualListbox({ selectorMinimalHeight: 160 });
    SlctIdiomas.bootstrapDualListbox({ selectorMinimalHeight: 160 });
    SlctReqEspeciales.bootstrapDualListbox({ selectorMinimalHeight: 160 });
    SlctTitulos.bootstrapDualListbox({ selectorMinimalHeight: 160 });
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
    /*
    _ajax(null,
        '/Requisiciones/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                var fecha = FechaFormatoSimple(obj[0].req_FechaNacimiento);
                console.log(fecha);
                $("#tbRequisiciones").find("#req_Id").val(obj[0].req_Id);
                $("#tbRequisiciones").find("#req_Identidad").val(obj[0].req_Identidad);
                $("#tbRequisiciones").find("#req_Nombres").val(obj[0].req_Nombres);
                $("#tbRequisiciones").find("#req_Apellidos").val(obj[0].req_Apellidos);
                $("#tbRequisiciones").find("#req_FechaNacimiento").val(FechaFormatoSimple(obj[0].req_FechaNacimiento));
                $("#tbRequisiciones").find("#req_Sexo").val(obj[0].req_Sexo);
                $("#tbRequisiciones").find("#req_Edad").val(obj[0].req_Edad);
                $("#tbRequisiciones").find("#nac_Id").val(obj[0].nac_Id);
                $("#tbRequisiciones").find("#req_Direccion").val(obj[0].req_Direccion);
                $("#tbRequisiciones").find("#req_Telefono").val(obj[0].req_Telefono);
                $("#tbRequisiciones").find("#req_CorreoElectronico").val(obj[0].req_CorreoElectronico);
                $("#tbRequisiciones").find("#req_EstadoCivil").val(obj[0].req_EstadoCivil);
                $("#tbRequisiciones").find("#req_TipoSangre").val(obj[0].req_TipoSangre);
            }
        });
    */

    _ajax(null,
        '/Requisiciones/Edit/' + id,
        'GET',
        function (obj) {
            if (obj != "-1" && obj != "-2" && obj != "-3") {
                $("#tbRequisiciones").find("#req_Experiencia").val(obj[0].req_Experiencia);
                $("#tbRequisiciones").find("#req_Sexo").val(obj[0].req_Sexo);
                $("#tbRequisiciones").find("#req_Descripcion").val(obj[0].req_Descripcion);
                $("#tbRequisiciones").find("#req_EdadMinima").val(obj[0].req_EdadMinima);
                $("#tbRequisiciones").find("#req_EdadMaxima").val(obj[0].req_EdadMaxima);
                $("#tbRequisiciones").find("#req_EstadoCivil").val(obj[0].req_EstadoCivil);
                $("#tbRequisiciones").find("#req_EducacionSuperior").prop("checked", obj[0].req_EducacionSuperior);
                $("#tbRequisiciones").find("#req_Permanente").prop("checked", obj[0].req_Permanente);
                $("#tbRequisiciones").find("#req_Duracion").val(obj[0].req_Duracion);
                $("#tbRequisiciones").find("#req_Vacantes").val(obj[0].req_Vacantes);
                $("#tbRequisiciones").find("#req_FechaRequisicion").val(FechaFormatoSimple(obj[0].req_FechaRequisicion));
                $("#tbRequisiciones").find("#req_FechaContratacion").val(FechaFormatoSimple(obj[0].req_FechaContratacion));

                if(obj[0].req_Permanente == true)
                {
                    $("#tbRequisiciones").find("#req_Permanente").prop("disabled", false);
                }
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
            tbRequisicion.req_Id = id;
            Form = JSON.stringify({ tbRequisiciones: tbRequisicion, DatosProfesionales: data });
            console.log(Form);

            if (tbRequisicion != null) {
                _ajax(Form,
                '/Requisiciones/Edit',
                'POST',
                function (obj) {
                    if (obj != "-1" && obj != "-2" && obj != "-3") {
                        MsgSuccess("¡Exito!", "Se ah agregado el registro");
                    } else {
                        MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                    }
                });
            }
            else {
                MsgError("Error", "por favor llene todos los campos de texto.");
            }
        },
    });
});