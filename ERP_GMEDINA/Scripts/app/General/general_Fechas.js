
//@section Styles {
//@Styles.Render("~/plugins/select2Styles")
//}
//@section Scripts {
//@Scripts.Render("~/plugins/select2")
//@Scripts.Render("~/Scripts/app/general_Fechas")
//}
$(document).ready(function () {
    $(".buscable").select2();
    if ($('#fechaFin').val()) {
        $('#fechaFin').val('');
    }
    if ($('#fechaInicio').val()) {
        $('#fechaInicio').val('');
    }
});
$('#fechaInicio').focusout(() => {
    var a = $("#fechaInicio").val();
    a = toDate("#fechaInicio")
    a.getFullYear();
    if ($('#fechaFin').val() != null) {
        $('#fechaInicio').val() < '1900-01-01' || a > 2518 ? $('#fechaInicio').val($('#fechaFin').val()) : '';
    } else {
        $('#fechaInicio').val() < '1900-01-01' || $('#fechaInicio').val() > '9999-01-01' ? $('#fechaInicio').val('') : '';
    }
    if ($('#fechaFin').attr('min')) {
        $('#fechaFin').removeAttr('min');
        $("#fechaFin").attr("min", $('#fechaInicio').val());
    } else {
        if ($('#fechaFin').val()) {
            if ($('#fechaFin').val() > $('#fechaInicio').val()) {
                $('#fechaInicio').val($('#fechaFin').val());
                $("#fechaFin").attr("min", $('#fechaInicio').val());
            } else {
                $("#fechaFin").attr("min", $('#fechaInicio').val());
            }
        } else {
            $("#fechaFin").attr("min", $('#fechaInicio').val());
        }
    }
});
function toDate(selector) {
    var from = $(selector).val().split("-")
    return new Date(from[2], from[1] - 1, from[0])
}
$('#fechaFin').focusout(() => {
    var a = $("#fechaFin").val();
    a = toDate("#fechaFin")
    a.getFullYear();
    if ($('#fechaInicio').val() != null) {
        $('#fechaFin').val() < '1900-01-01' || a > 2518 ? $('#fechaFin').val($('#fechaInicio').val()) : '';
    } else {
        $('#fechaFin').val() < '1900-01-01' || $('#fechaFin').val() > '9999-01-01' ? $('#fechaFin').val('') : '';
    }
    if ($('#fechaInicio').attr('max')) {
        $('#fechaInicio').removeAttr('max');
        $("#fechaInicio").attr("max", $('#fechaFin').val());
    } else {
        if ($('#fechaInicio').val()) {
            if ($('#fechaInicio').val() > $('#fechaFin').val()) {
                $('#fechaFin').val($('#fechaInicio').val());
                $("#fechaInicio").attr("max", $('#fechaFin').val());
            } else {
                $("#fechaInicio").attr("max", $('#fechaFin').val());
            }
        } else {
            $("#fechaInicio").attr("max", $('#fechaFin').val());
        }
    }
});

//$('#fechaInicio').focusout(() => {
//    if (!$('#fechaFin').val()) {
//        $("#fechaFin").attr("min", $('#fechaInicio').val());
//    }
//});


//$('#btnPrevisualizar').click(
//    function () {
//        let a = $('#fechaFin').val();
//        $('#fechaFin').removeAttr('min');
//        $('#fechaInicio').removeAttr('max');
//});
//$('#btnPrevisualizar').submit(function ($) {
//    // set data-after-submit-value on input:submit to disable button after submit
//    $(document).on('submit', 'form', function () {
//        var $form = $(this),
//			$button,
//			label;
//        $form.find(':submit').each(function () {
//            $button = $(this);
//            $('#fechaInicio').val('');
//            $('#fechaFin').val('');
//        });
//    });
//});