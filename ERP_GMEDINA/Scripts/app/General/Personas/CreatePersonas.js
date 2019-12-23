
function llenarDropDownList() {
    _ajax(null,
       '/Personas/llenarDropDowlistNacionalidades',
       'POST',
       function (result) {
           $.each(result, function (id, Lista) {
               Lista.forEach(function (value, index) {
                   $("#nac_Id" ).append(new Option(value.Descripcion, value.Id));
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
