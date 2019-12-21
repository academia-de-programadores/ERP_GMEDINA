var ChildTable = null;
var list = [];



function llenarDropDownList() {
    _ajax(null,
       '/Personas/llenarDropDowlistNacionalidades',
       'POST',
       function (result) {
           $.each(result, function (id, Lista) {
               Lista.forEach(function (value, index) {
                   $("#"+ id ).append(new Option(value.Descripcion, value.Id));
               });
           });
       });
    function llenarDropDownList() {
        _ajax(null,
          '/Personas/llenarDropDowlistCompetencias',
          'POST',
          function (result) {
              $.each(result, function (id, Lista) {
                  Lista.forEach(function (value, index) {
                      $("#"+id).append(new Option(value.Descripcion, value.Id));
                  });
              });
          });
    }

}


