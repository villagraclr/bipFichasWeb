$(document).ready(function () {    
    var timer = new Date().getTime() + 1200000; //20 minutos
    $('#relojSesion').countdown(timer, function (event) {
        $(this).html(event.strftime('Tiempo restante de sesion: %M:%S'));
    })
    .on('finish.countdown', function (event) {
        $('#modalGblMensajes').modal('show');
    });
    $("#btnMsjGblAceptar").click(function () {
        var opciones = {
            url: ROOT + "Mantenedores/RenuevaSesion",
            type: "post",
            datatype: "json",
            success: function (result, xhr, status) {
                $('#relojSesion').countdown(new Date().getTime() + 1200000); //20 minutos
                window.clearInterval(timerCierraSesionId);
                timerCierraSesionId = window.setInterval("cierraSesionAuto()", 1800000); //30 minutos
                $('#modalGblMensajes').modal('hide');
                console.log(result);
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        };
        $.ajax(opciones);
    });
});