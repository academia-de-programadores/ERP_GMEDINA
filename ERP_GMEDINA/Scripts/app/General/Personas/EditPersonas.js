function llenarDropDownList() {
    _ajax(null,
       '/Personas/llenarDropDowlistNacionalidades',
       'POST',
       function (result) {
           $.each(result, function (id, Lista) {
               Lista.forEach(function (value, index) {
                   $("#nac_Id").append('<option value="' + value.Id + '">' + value.Descripcion + '</option>');
               });
           });
       });
}
function ListFill(obj) {
    var SlctCompetencias = $(".SlctCompetencias");
    var SlctHabilidades = $(".SlctHabilidades");
    var SlctIdiomas = $(".SlctIdiomas");
    var SlctReqEspeciales = $(".SlctReqEspeciales");
    var SlctTitulos = $(".SlctTitulos");
    obj.forEach(function (index, value) {
        if (index.TipoDato == "C")
        {
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

    SlctCompetencias.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacia', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
    SlctHabilidades.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacia', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
    SlctIdiomas.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacia', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
    SlctReqEspeciales.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacia', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
    SlctTitulos.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacia', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
};
$(document).ready(function () {
    llenarDropDownList();
    id = sessionStorage.getItem("IdPersona");
    $("#per_FechaNacimiento").attr("max", Mayor18());
    _ajax(null,
            '/Personas/DualListBoxData/'+ id,
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    ListFill(obj);
                }
            })

        id = sessionStorage.getItem("IdPersona");
        _ajax(null,
            '/Personas/Edit/' + id,
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    var fecha = FechaFormatoSimple(obj[0].per_FechaNacimiento);
                    console.log(fecha);
                    $("#tbPersonas").find("#per_Id").val(obj[0].per_Id);
                    $("#tbPersonas").find("#per_Identidad").val(obj[0].per_Identidad);
                    $("#tbPersonas").find("#per_Nombres").val(obj[0].per_Nombres);
                    $("#tbPersonas").find("#per_Apellidos").val(obj[0].per_Apellidos);
                    $("#tbPersonas").find("#per_FechaNacimiento").val(FechaFormatoSimple(obj[0].per_FechaNacimiento));
                    $("#tbPersonas").find("#per_Sexo").val(obj[0].per_Sexo);
                    $("#tbPersonas").find("#per_Edad").val(obj[0].per_Edad);
                    $("#tbPersonas").find("#nac_Id").val(obj[0].nac_Id);
                    $("#tbPersonas").find("#per_Direccion").val(obj[0].per_Direccion);
                    $("#tbPersonas").find("#per_Telefono").val(obj[0].per_Telefono);
                    $("#tbPersonas").find("#per_CorreoElectronico").val(obj[0].per_CorreoElectronico);
                    $("#tbPersonas").find("#per_EstadoCivil").val(obj[0].per_EstadoCivil);
                    $("#tbPersonas").find("#per_TipoSangre").val(obj[0].per_TipoSangre);
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
            var Correo = validarEmail($('#per_CorreoElectronico').val());

            var DatosProfesionalesArray = { Competencias: SlctCompetencias.val(), Habilidades: SlctHabilidades.val(), Idiomas: SlctIdiomas.val(), ReqEspeciales: SlctReqEspeciales.val(), Titulos: SlctTitulos.val() };
            var Form = $("#tbPersonas").find("select, textarea, input").serializeArray();
            tbPersonas = serializar(Form);
            data = JSON.stringify({ tbPersonas, DatosProfesionalesArray });

            if (tbPersonas != null)
            {
                    if (Correo != "") {
                        _ajax(data,
                        '/Personas/Edit',
                        'POST',
                        function (obj) {
                            if (obj != "-1" && obj != "-2" && obj != "-3") {
                                MsgSuccess("¡Exito!", "Se ah editado el registro");
                                $("#finish").attr("href", " ");
                                setTimeout(function () { window.location.href = "/Personas/Index"; }, 5000);
                            } else {
                                MsgError("Error", "Codigo:" + obj + ". contacte al administrador.(Verifique si el registro ya existe)");
                            }
                        });
                    }
                    else {
                        MsgError("Error", "Correo Electronico invalido");
                    }
            }
            else {
                MsgError("Error", "por favor llene todos los datos");
            }

        },
    });
});