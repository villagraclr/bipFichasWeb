$(document).ready(function () {
    $('#aMenuCargaRIS').addClass('active');
    iniciaTablaCargaRIS();
    $("#btnCargaRIS").click(function () {        
        if ($("#cmbSubeBeneficiarios option:selected").val() != "-1") {
            var enviaBenef = true;
            if ($("#cmbSubeBeneficiarios option:selected").val() == "No" && $("#cmbJustificacion option:selected").val() == "-1") {
                enviaBenef = false;
                modalMsjCargaRIS("<p>Seleccione al menos una justificación</p>");
            } else if ($("#cmbSubeBeneficiarios option:selected").val() == "Si" && document.getElementById("idCargaRIS").files.length <= 0) {
                enviaBenef = false;
                modalMsjCargaRIS("<p>Seleccione al menos un archivo</p>");
            } else if ($("#cmbSubeBeneficiarios option:selected").val() == "No" && ($("#cmbJustificacion option:selected").val() == "2" || $("#cmbJustificacion option:selected").val() == "3") && $.trim($("#txtComentSubeBenef").val()) == "") {
                enviaBenef = false;
                modalMsjCargaRIS("<p>Ingrese justificación</p>");
            }
            if (enviaBenef) {
                var fd = new FormData();
                var file = document.getElementById("idCargaRIS").files[0];
                $(this).button('loading');
                fd.append('idPrograma', $("#txtIdPrograma").val());
                fd.append('beneficiarios', file);
                fd.append('justificacion', $("#txtComentSubeBenef").val());
                fd.append('tieneBeneficiarios', $("#cmbSubeBeneficiarios option:selected").val());
                fd.append('tipoJustificacion', $("#cmbJustificacion option:selected").val());
                var opciones = {
                    url: ROOT + "CargaRIS/GetCargaBenefRIS",
                    data: fd,
                    type: "post",
                    contentType: false,
                    processData: false,
                    success: function (result, xhr, status) {
                        $("#btnCargaRIS").button('reset');
                        if (result == "ok") {
                            $("#modalSubirArchivo").modal('hide');
                            iniciaTablaCargaRIS();
                            modalMsjCargaRIS("<p>La carga se ejecutó correctamente. Recuerde que su archivo pasará por un proceso de análisis de calidad para evaluar si la carga cumple con las especificaciones técnicas indicadas en el oficio Nº259/2024 de la Subsecretaría de Evaluación Social.</p>");
                        } else {
                            modalMsjCargaRIS("<p>" + result + "</p>");
                            console.log(result);
                        }
                    },
                    error: function (xhr, status, error) {
                        modalMsjCargaRIS("<p>" + error + "</p>");
                        console.log(error);
                    }
                };
                $.ajax(opciones);
            }
        } else {
            modalMsjCargaRIS("<p>Seleccione al menos una opción</p>");
        }
    });
    if ($("#cmbSubeBeneficiarios option:selected").val() == "Si") {
        $(".divCargaBeneficiarios").css("display", "");
        $(".divJustNoCargaBeneficiarios").css("display", "none");
        $(".divCmbJustificacion").css("display", "none");
        $(".divTextoCargaBeneficiarios").css("display", "");
    } else if ($("#cmbSubeBeneficiarios option:selected").val() == "No") {
        $(".divJustNoCargaBeneficiarios").css("display", "none");
        $(".divCmbJustificacion").css("display", "");
        $(".divTextoNoCargaBeneficiarios").css("display", "");        
        $(".divCargaBeneficiarios").css("display", "none");
        $(".divTextoCargaBeneficiarios").css("display", "none");
    } else if ($("#cmbSubeBeneficiarios option:selected").val() == "-1") {
        $(".divJustNoCargaBeneficiarios").css("display", "none");
        $(".divCargaBeneficiarios").css("display", "none");
        $(".divTextoNoCargaBeneficiarios").css("display", "none");
        $(".divCmbJustificacion").css("display", "none");
        $(".divTextoCargaBeneficiarios").css("display", "none");
    }
    if ($("#txtComentSubeBenef").val().length > 0) {
        var totalCaracteres = 3000;
        var restantes = (totalCaracteres - $("#txtComentSubeBenef").val().length) < 0 ? 0 : (totalCaracteres - $("#txtComentSubeBenef").val().length);
        $("#txtCounttxtComentSubeBenef").text("Caracteres restantes=" + restantes);
    }
    $("#txtComentSubeBenef").keyup(function () {
        var totalCaracteres = 3000;
        var restantes = (totalCaracteres - $(this).val().length) < 0 ? 0 : (totalCaracteres - $(this).val().length);
        $("#txtCounttxtComentSubeBenef").text("Caracteres restantes=" + restantes);
    });
    $("#cmbSubeBeneficiarios").click(function () {
        if ($(this).val() == "No") {
            $(".divJustNoCargaBeneficiarios").css("display", "none");
            $(".divCmbJustificacion").css("display", "");
            $(".divTextoNoCargaBeneficiarios").css("display", "");
            $(".divCargaBeneficiarios").css("display", "none");
            $(".divTextoCargaBeneficiarios").css("display", "none");
        } else if ($(this).val() == "Si") {
            $(".divCargaBeneficiarios").css("display", "");
            $(".divJustNoCargaBeneficiarios").css("display", "none");
            $(".divCmbJustificacion").css("display", "none");
            $(".divTextoNoCargaBeneficiarios").css("display", "none");
            $(".divTextoCargaBeneficiarios").css("display", "");
            $("#txtComentSubeBenef").val("");
            $("#txtCounttxtComentSubeBenef").text("Caracteres restantes=3000");
        } else if ($("#cmbSubeBeneficiarios option:selected").val() == "-1") {
            $(".divJustNoCargaBeneficiarios").css("display", "none");
            $(".divCargaBeneficiarios").css("display", "none");
            $(".divCmbJustificacion").css("display", "none");
            $(".divTextoNoCargaBeneficiarios").css("display", "none");
            $(".divTextoCargaBeneficiarios").css("display", "none");
            $("#txtComentSubeBenef").val("");
            $("#txtCounttxtComentSubeBenef").text("Caracteres restantes=3000");
        }
    });
    $("#cmbJustificacion").click(function () {
        if ($(this).val() == "2" || $(this).val() == "3") {
            $(".divJustNoCargaBeneficiarios").css("display", "");
        } else {
            $(".divJustNoCargaBeneficiarios").css("display", "none");
        }
    });
});
function iniciaTablaCargaRIS() {
    var tablaPanelCargaRIS = $('#tblPanelCargaRIS').DataTable();
    tablaPanelCargaRIS.destroy();
    $('#tblPanelCargaRIS').dataTable({
        dom: 'Bfrtip',
        buttons: [{
            extend: 'excel',
            text: 'Descargar planilla',
            filename: moment().format("YYYY/MM/DD") + '_planilla_beneficiarios',
            title: ''
        }],
        "processing": true,
        "ajax": {
            "url": ROOT + "CargaRIS/GetPanelCargaRIS",
            "dataSrc": "",
            "type": "POST"
        },
        "columns": [
            { "data": 'IdBips', "width": "4%" },
            { "data": 'Ministerio', "width": "20%" },
            { "data": 'Servicio', "width": "20%" },
            {
                "data": 'Nombre', "width": "30%",
                "render": function (data, type, full, meta) {
                    var link = ROOT + "Formulario/Index" + "?_id=" + encodeURIComponent(full.IdEncriptado);
                    return '<a href="' + link + '" data-toggle="tooltip" data-placement="right" title="Click para abrir programa" target="_blank">' + full.Nombre + '</ña>';
                }
            },           
            { "data": 'Version', "width": "2%" },
            {
                "data": 'FecSolicitudEval', "width": "5%"
            },
            {
                "data": 'Cargar beneficiarios', "orderable": false, "width": "1%",
                "render": function (data, type, full, meta) {
                    return '<button id="btnCargarRIS" class="btn btn-primary btn-xs" type="button" data-toggle="tooltip" data-placement="left" title="Cargar beneficiarios" onclick="cargaBenefRIS(' + full.IdPrograma + ',' + full.Tomado + ');"><i class="fa fa-upload"></i></button>';
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });            
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
}
function cargaBenefRIS(id, marcaRIS) {
    $("#txtIdPrograma").val(id);
    $("#cmbJustificacion").empty();
    $("#cmbJustificacion").append("<option value='-1'>Seleccione</option>");
    if (marcaRIS) {        
        $("#cmbJustificacion").append("<option value='1'>Reporte en RIS</option>");
    }
    $("#cmbJustificacion").append("<option value='3'>Información Sensible</option>");
    $("#cmbJustificacion").append("<option value='2'>Otra</option>");
    $("#modalSubirArchivo").modal('show');
}
function modalMsjCargaRIS(mensaje) {
    $('#msjInfoCargaRIS').empty();
    $('#msjInfoCargaRIS').html(mensaje);
    $('#modalMsjCargaBenef').modal('show');
}