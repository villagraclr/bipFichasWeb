$(document).ready(function () {
    $("#liNivelCentral").addClass('active');
    $("#liBiblioteca").addClass('active');
    $("#menuNivelCentral").addClass('in');
    //$("#aMenuNivelCentral").dropdown('toggle');
    $("#menuBiblioteca").addClass('in');
    $("#aMenuBaseDatos").dropdown('toggle');

    $("#cmbAno").change(function () {
        //tipo de formulario
        $("#cmbFormularios").empty();
        $("#cmbFormularios").append("<option value='-1'>Seleccione</option>");
        if (tipoForm.length > 0) {
            $.each(tipoForm, function (i, item) {
                if (item.IdParametro != item.idCategoria && item.Valor2 == $("#cmbAno").val()) {
                    $("#cmbFormularios").append("<option value='" + item.Valor + "'>" + item.Descripcion + "</option>");
                }
            });
        }
    });
});

$("#descargaBD").submit(function (event) {
    var cat = 0;
    $("input:checkbox[name='categorias']:checked").each(function (index, obj) {
        cat = 1;
        return false;
    });
    if ($("select[name='ano']").val() != "-1" && cat == 1 && $("#cmbFormularios").val() != "-1") {
        $("#btnDescargaBD").button('loading');
        $("#cargaPagina").fadeIn();
        event.preventDefault();
        var form = $(this);
        var url = form.attr('action');
        $.ajax({
            type: "POST",
            url: url,
            data: form.serialize(),
            datatype: "json",
            success: function (data) {
                var response = JSON.parse(data);
                window.location = ROOT + 'Biblioteca/Download?fileGuid=' + response.FileGuid
                                  + '&filename=' + response.FileName;
            },
            complete: function () {
                $("#btnDescargaBD").button('reset');
                $("#cargaPagina").fadeOut();
            }
        });
    } else {
        event.preventDefault();
        $("#msjInfoDescargaBD").empty();
        $("#msjInfoDescargaBD").html("<p>Seleccione al menos un año, una categoría y un tipo de formulario</p>");
        $('#modalMsjDescargaBD').modal('show');
    }
});