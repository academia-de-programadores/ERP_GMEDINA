//
//$(document).change(() => alert());
$(document).ready(function () {
    if ($('#fechaFin').val()) {
        $('#fechaFin').val('');
    }
    if ($('#fechaInicio').val()) {
        $('#fechaInicio').val('');
    }
});
$('#fechaInicio').focusout(() => {
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

$('#fechaFin').focusout(() => {
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