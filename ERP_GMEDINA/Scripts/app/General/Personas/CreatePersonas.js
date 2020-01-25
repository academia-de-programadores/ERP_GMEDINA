function llenarDropDownList() {
    _ajax(null,
       '/Personas/llenarDropDowlistNacionalidades',
       'POST',
       function (result) {
           $.each(result, function (id, Lista) {
               Lista.forEach(function (value, index) {
                   $("#nac_Id" ).append('<option value="' + value.Id + '">' + value.Descripcion + '</option>');
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
        if (index.TipoDato == "C") {
                SlctCompetencias.append('<option value="' + index.Id + '">' + index.Descripcion + '</option>');
        }
        if (index.TipoDato == "H") {
                SlctHabilidades.append('<option value="' + index.Id + '">' + index.Descripcion + '</option>');
        }
        if (index.TipoDato == "I") {
                SlctIdiomas.append('<option value="' + index.Id + '">' + index.Descripcion + '</option>');
        }
        if (index.TipoDato == "R") {
                SlctReqEspeciales.append('<option value="' + index.Id + '">' + index.Descripcion + '</option>');
        }
        if (index.TipoDato == "T") {
                SlctTitulos.append('<option value="' + index.Id + '">' + index.Descripcion + '</option>');
        }
    });

    SlctCompetencias.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacía', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel : 'Mover todos',removeAllLabel: 'Remover todos'});
    SlctHabilidades.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacía', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
    SlctIdiomas.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacía', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
    SlctReqEspeciales.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacía', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
    SlctTitulos.bootstrapDualListbox({ selectorMinimalHeight: 160, filterPlaceHolder: 'Buscar...', infoText: 'Mostrando {0}', infoTextEmpty: 'Lista vacía', infoTextFiltered: '<span class="label label-warning">Coincidencias</span> {0} de {1}', filterTextClear: 'Mostrar todos', moveAllLabel: 'Mover todos', removeAllLabel: 'Remover todos' });
};
$(document).ready(function () {
    llenarDropDownList();
    $("#per_FechaNacimiento").attr("max", Mayor18());
    _ajax(null,
            '/Personas/DualListBoxData',
            'GET',
            function (obj) {
                if (obj != "-1" && obj != "-2" && obj != "-3") {
                    ListFill(obj);
                }
            })

    var wizard = $("#Wizard").steps({
        enableCancelButton: false,
        //validaciones
        onStepChanging:
            function (event, currentIndex, newIndex) {
                
                    if ($("#per_FechaNacimiento").val() > '1899/01/01/') 
                    {
                        var Form = $("#tbPersonas").find("select, textarea, input");
                        Form.validate().settings.ignore = ":disabled,:hidden";
                        return Form.valid();
                    }
                    else
                    {

                        MsgError("Error", "Fecha de nacimiento inválida");
                        window.location.href = "#Wizard-h-0";
                        //$("#per_FechaNacimiento").val('1900/01/01')

                    }
                    var Form = $("#tbPersonas").find("select, textarea, input");
                    Form.validate().settings.ignore = ":disabled,:hidden";
                    return Form.valid();
                    
        },
        onFinishing: function (event, currentIndex) {
            if ($("#per_FechaNacimiento").val() > '1899/01/01/') {
                var Form = $("#tbPersonas").find("select, textarea, input");
                Form.validate().settings.ignore = ":disabled,:hidden";
                return Form.valid();
            }
            else {

                MsgError("Error", "Fecha de nacimiento inválida");
                window.location.href = "#Wizard-h-0";
                //$("#per_FechaNacimiento").val('1900/01/01')

            }
            var Form = $("#tbPersonas").find("select, textarea, input");
            Form.validate().settings.ignore = ":disabled,:hidden";
            return Form.valid();
        },
        onFinished: function () {
            var SlctCompetencias = $(".SlctCompetencias");
            var SlctHabilidades = $(".SlctHabilidades");
            var SlctIdiomas = $(".SlctIdiomas");
            var SlctReqEspeciales = $(".SlctReqEspeciales");
            var SlctTitulos = $(".SlctTitulos");

            var DatosProfesionalesArray = { Competencias: SlctCompetencias.val(), Habilidades: SlctHabilidades.val(), Idiomas: SlctIdiomas.val(), ReqEspeciales: SlctReqEspeciales.val(), Titulos: SlctTitulos.val() };
            var Form = $("#tbPersonas").find("select, textarea, input").serializeArray();
            tbPersonas = serializarPro(Form);
            data = JSON.stringify({ tbPersonas, DatosProfesionalesArray });
            //
                        _ajax(data,
                        '/Personas/Create',
                        'POST',
                        function (obj) {
                            if (obj != "-1" && obj != "-2" && obj != "-3") {
                                MsgSuccess("¡Exito!", "El registro se agregó de forma exitosa");
                                $("#finish").attr("href", " ");
                                setTimeout(function () { window.location.href = "/Personas/Index"; }, 3000);
                            } else {
                                MsgError("Error", "No se agrego el registro, contacte al administrador");
                            }
                        });
        },
    });
});