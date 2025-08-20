$(document).ready(function () {
    $('ul.nav a').each(function () { if ($(this).hasClass('active')) { $(this).removeClass('active'); } });
    $("#liGores").addClass('active');
    $("#liEvaluadoresGore").addClass('active');
    $('#aMenuEvaluadorPrograma').addClass('active');
    $("#menuGores").addClass('in');
    $("#menuEvaluadoresGore").addClass('in');
    iniciaTablaEvalPrograma();
    $("#btnModalAceptaEvaluador").click(function () {
        $("#cargaPagina").fadeIn();
        $('#btnModalAceptaEvaluador').button('loading');
        var idPrograma = $("#txtIdPrograma").val();
        var evaluador = $("#txtIdEvaluador").val();
        var evaluadorAnterior = evaActual//($("#txtEva" + idPrograma).val() ? $("#txtEva" + idPrograma).val() : null)
        var numEvaluador = $("#txtNumEvaluador").val();
        var opciones = {
            url: ROOT + "Gores/AsignaEvaluadoresPrograma",
            data: { idPrograma: idPrograma, idEvaluador: evaluador, idEvaluadorAnterior: evaluadorAnterior, numeroEvaluador: numEvaluador },
            type: "post",
            datatype: "json",
            async: false,
            beforeSend: function () { },
            success: function (result, xhr, status) {
                //$("#btnModalAceptaResultadoEvaluador").button('reset')
                if (result == "ok") {
                    $("#modalAsignaEvaluador").modal('hide');
                    modalResultados("<p>Evaluador asignado con éxito</p>");
                    iniciaTablaEvalPrograma();
                } else {
                    modalResultados("<p>" + result + "</p>");
                }
                $('#btnModalAceptaEvaluador').button('reset');
            },
            error: function (xhr, status, error) {
                modalResultados("<p>" + error + "</p>");
                console.log(error);
                $('#btnModalAceptaEvaluador').button('reset');
            },
            complete: function () { $("#cargaPagina").fadeOut(); }
        };
        $.ajax(opciones);
    });
    $("#btnModalCancelaEvaluador").click(function () {
        var idPrograma = $("#txtIdPrograma").val();
        var numEvaluador = $("#txtNumEvaluador").val();
        if (evaActual != 'null') {
            $("#cmb" + numEvaluador + "Eval" + idPrograma).val(evaActual).trigger('change');
        } else {
            $("#cmb" + numEvaluador + "Eval" + idPrograma).val("-1").trigger('change');
        }
    });
    $('#archivoCargaComentarios').change(function () {
        var file = $(this)[0].files[0];
        if (file) {
            var fileExtension = file.name.split('.').pop().toLowerCase();
            if (fileExtension === 'pdf') {
                $('#btnModalEnviarComentarios').prop('disabled', false);
                $('#lblArchivoCargaComentarios span').text(file.name);
                $('#lblArchivoCargaComentarios').css('color', 'green');
            } else {
                $('#btnModalEnviarComentarios').prop('disabled', true);
                $('#lblArchivoCargaComentarios span').text('Por favor, selecciona un archivo PDF válido')
                $('#lblArchivoCargaComentarios').css('color', 'red');
                $('#archivoCargaComentarios').val('');
            }
        } else {
            $('#btnModalEnviarComentarios').prop('disabled', true);
            $('#lblArchivoCargaComentarios span').text('Selecciona un archivo')
            $('#lblArchivoCargaComentarios').css('color', 'black');
        }
    });
    $("#btnModalEnviarComentarios").click(function () {
        var idPrograma = $("#txtComentariosIdPrograma").val();
        enviarComentariosJefatura(idPrograma);
    });
    $('#modalEnviarComentarios').on('hidden.bs.modal', function () {
        $('#archivoCargaComentarios').val('');
        $('#lblArchivoCargaComentarios span').text('Selecciona un archivo')
        $('#lblArchivoCargaComentarios').css('color', 'black');
    });
    $("#btnModalEnviarCalificacion").click(function () {
        var idBips = $("#txtCalificacionIdBips").val();
        var idPrograma = $("#txtCalificacionIdPrograma").val();
        enviarCalificacionJefatura(idBips, idPrograma);
    });
});
function queryString(valor) {
    valor = valor.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + valor + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results == null)
        return "";
    else
        return results[1];
}
var evaActual = null;
function iniciaTablaEvalPrograma() {
    var tablaEvalPerfil = $('#tblEvalPerfil').DataTable();
    tablaEvalPerfil.destroy();
    $('#tblEvalPerfil').dataTable({
        "processing": true,
        "ajax": {
            "url": ROOT + "Gores/GetEvaluacionesPrograma",
            "dataSrc": "",
            "type": "POST"
        },
        "columns": [
            { "data": 'IdBips', "width": "4%" },
            { "data": 'Ministerio', "width": "15%" },
            {
                "data": 'Nombre', "width": "15%",
                "render": function (data, type, full, meta) {
                    var link = ROOT + "Formulario/Index" + "?_id=" + encodeURIComponent(full.IdEncriptado);
                    return '<a href="' + link + '" data-toggle="tooltip" data-placement="right" title="Click para abrir programa" target="_blank">' + full.Nombre + '</a>';
                }
            },
            { "data": 'Version', "width": "1%" },
            {
                "data": 'CalificacionPrograma', "width": "5%",
                "render": function (data, type, full, meta) {
                    var calificacion = '<p>' + (data === null ? "Sin calificar" : data) + '</p>'
                    return calificacion;
                }
            },
            {
                "data": null, "orderable": false, "width": "12%",
                "render": function (data, type, full, meta) {
                    var idEvaluador1 = full.IdEvaluador1 ? encodeURIComponent(full.IdEvaluador1) : null;
                    var claseCierre = ((full.IdEtapa == etapaRevisionJefe || full.IdEtapa == etapaProgramaCalificado) ? "disabled" : "");
                    var select = '<select id="cmb1Eval' + full.IdPrograma + '" class="form-control input-sm cssEval' + full.IdPrograma + '1" onchange="cmbEvaluadores(' + full.IdPrograma + ',this.value, 1, \'' + idEvaluador1 + '\');" ' + claseCierre + ' style="width: 100%;"  ' + (full.TipoGeneral == null ? "disabled" : "") + '>' 
                    if (full.IdEvaluador1 == null) {
                        select += '<option value="-1">Seleccione</option>';
                    }
                    $.each(evaluadores, function (i, item) {
                        if (item.IdMinisterio != 30) {
                            var sel = (item.Id == full.IdEvaluador1 ? "selected='selected'" : "");
                            var dis = (item.Id == full.IdEvaluador2 ? "disabled" : "");
                            select += '<option value="' + item.Id + '" ' + sel + ' ' + dis + '>' + item.Nombre + '</option>';
                        }
                    });
                    select += '</select>'
                    if (full.IdEvaluador1 != null) {
                        //Evaluador Anterior
                        select += '<input type="hidden" id="txtEva' + full.IdPrograma + '" value="' + full.IdEvaluador1 + '" />'
                    }
                    return select;
                },
            },
            {
                "data": null, "orderable": false, "width": "12%",
                "render": function (data, type, full, meta) {
                    var idEvaluador2 = full.IdEvaluador2 ? encodeURIComponent(full.IdEvaluador2) : null;
                    var claseCierre = ((full.IdEtapa == etapaRevisionJefe || full.IdEtapa == etapaProgramaCalificado) ? "disabled" : "");
                    var select = '<select id="cmb2Eval' + full.IdPrograma + '" class="form-control input-sm cssEval' + full.IdPrograma + '2" onchange="cmbEvaluadores(' + full.IdPrograma + ',this.value, 2, \'' + idEvaluador2 + '\');" ' + claseCierre + ' style="width: 100%;"  ' + (full.TipoGeneral == null ? "disabled" : "") + '>'
                    if (full.IdEvaluador2 == null) {
                        select += '<option value="-1">Seleccione</option>';
                    }
                    $.each(evaluadores, function (i, item) {
                        if (item.IdMinisterio != 30) {
                            var sel = (item.Id == full.IdEvaluador2 ? "selected='selected'" : "");
                            var dis = (item.Id == full.IdEvaluador1 ? "disabled" : "");
                            select += '<option value="' + item.Id + '" ' + sel + ' ' + dis + '>' + item.Nombre + '</option>';
                        }
                    });
                    select += '</select>'
                    if (full.IdEvaluador2 != null) {
                        //Evaluador Anterior
                        select += '<input type="hidden" id="txtEva' + full.IdPrograma + '" value="' + full.IdEvaluador2 + '" />'
                    }
                    return select;
                },
            },
            {
                "data": 'EtapaDesc', "width": "10%", "orderable": false,
                "render": function (data, type, full, meta) {
                    return '<label style="font-weight: normal">' + data + '</label>';
                }
            },
            {
                "data": null, "width": "5%", "orderable": false,
                "render": function (data, type, full, meta) {
                    var claseCierre = ((full.IdEtapa == etapaRevisionJefe) ? "" : "disabled");
                    var accion = '<div style=" display: flex; justify-content: space-between;">'
                    accion += '<button id="" class="btn btn-primary btn-xs" ' + claseCierre + ' type="button" data-toggle="tooltip" data-placement="left" title="Enviar calificación" onclick="modalEnviarCalificacionJefatura(' + full.IdPrograma + ', ' + full.IdBips + ');"><i class="fa fa-sign-out"></i></button>';
                    accion += '<button id="" class="btn btn-primary btn-xs" ' + claseCierre + ' type="button" data-toggle="tooltip" data-placement="left" title="Enviar comentarios" onclick="modalEnviarComentariosJefatura(' + full.IdPrograma + ');"><i class="fa fa-comments"></i></button>';
                    accion += '</div>'
                    return accion;
                }
            },
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            var btnCreaForm = '<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>';
            $('#tblEvalPerfil_spFiltro').append(btnCreaForm);
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
}
function cmbEvaluadores(idPrograma, evaluadorNuevo, numEvaluador, evaluadorActual) {
    evaActual = evaluadorActual;
    if (evaluadorNuevo != "-1") {
        $("#txtIdPrograma").val(idPrograma);
        $("#txtIdEvaluador").val(evaluadorNuevo);
        $("#txtNumEvaluador").val(numEvaluador);
        $("#modalAsignaEvaluador").modal('show');
    }
}
function ingresoExAnte(idPrograma) {
    $('#txtIngresoIdPrograma').val(idPrograma);
    $('#msjIngresoExAnte').empty();
    var mensaje = '<p>Luego de revisar la justificación explicando por qué el perfil no corresponde su ingreso a ExAnte (enviado por correo), selecciona una opción:</p>'
    mensaje += '<p><strong>Si Corresponde ingreso:</strong> Se habilitará el módulo <strong>perfil</strong> de evaluación, para que el evaluador complete las preguntas restantes finalizando con la calificación.</p>'
    mensaje += '<p><strong>No corresponde ingreso:</strong> Se notificará a la <strong>COG</strong> a través de correo, luego la plataforma procederá a cerrar el perfil.</p>'
    mensaje += '<p class="text-center"><strong>ESTA ACCIÓN NO PODRÁ SER REVERTIDA</strong></p>'
    $('#msjIngresoExAnte').html(mensaje);
    $('#modalIngresoExAnte').modal('show');
}
function modalResultados(mensaje) {
    $('#msjResultados').empty();
    $('#msjResultados').html(mensaje);
    $('#modalResultados').modal('show');
}
function modalEnviarComentariosJefatura(idPrograma) {
    $('#txtComentariosIdPrograma').val(idPrograma);
    $('#msjEnviarComentarios').empty();
    $('#msjEnviarComentarios').html('<p>Adjuntar PDF con comentarios, al enviar el programa se habilitará el módulo de evaluación con la etapa <b>En evaluación</b>, se asignarán nuevamente los permisos de edición a los evaluadores</p>');
    $('#modalEnviarComentarios').modal('show');
}
function enviarComentariosJefatura(idPrograma) {
    $("#cargaPagina").fadeIn();
    var fd = new FormData();
    var file = $("#archivoCargaComentarios")[0].files[0];
    $('#btnModalEnviarComentarios').button('loading');
    fd.append('idPrograma', idPrograma);
    fd.append('comentarios', file);
    var opciones = {
        url: ROOT + "Gores/EnviarComentariosJefatura",
        data: fd,
        type: "post",
        contentType: false,
        processData: false,
        success: function (result, xhr, status) {
            if (result == "ok") {
                $("#modalEnviarComentarios").modal('hide');
                modalMsjEnviarComentarios("<p>Se ha enviado los comentarios exitosamente</p>");
                iniciaTablaEvalPrograma();
            }
        },
        error: function (xhr, status, error) {
            modalMsjEnviarComentarios("<p>Ha ocurrido un error durante el proceso: <b>" + error + "</b></p>");
            console.log(error);
        },
        complete: function () { $('#btnModalEnviarComentarios').button('reset'); $('#archivoCargaComentarios').val(''); $("#cargaPagina").fadeOut(); }
    };
    $.ajax(opciones);
}
function modalMsjEnviarComentarios(mensaje) {
    $('#txtMsjEnviarComentarios').html(mensaje);
    $('#modalMsjEnviarComentarios').modal('show');
}
function modalEnviarCalificacionJefatura(idPrograma, idBips) {
    $('#txtCalificacionIdPrograma').val(idPrograma);
    $('#txtCalificacionIdBips').val(idBips);
    $('#msjEnviarCalificacion').empty();
    var mensaje = '<p>A continuación se procederá con la calificación del programa en base a la información del <b>Reporte de Evaluación</b> enviado por los evaluadores del programa, el programa cambiará su etapa de acuerdo a su calificación:</p>';
    mensaje += '<p><b>Recomendado Favorablemente</b>: Programa calificado</p>';
    mensaje += '<p><b>Objetado Técnicamente</b>: En corrección de servicio</p>';
    mensaje += '<p><b>Falta Información</b>: En corrección de servicio</p>';
    mensaje += '<p>Se notificará por correo a la COG con su respectivo reporte, en el caso de las calificaciones <b>Objetado Técnicamente</b> y <b>Falta Información</b> se habilitará el formulario para su revisión</p>';
    $('#msjEnviarCalificacion').html(mensaje);
    $('#modalEnviarCalificacion').modal('show');
}
function enviarCalificacionJefatura() {
    $("#cargaPagina").fadeIn();
    $('#btnModalEnviarCalificacion').button('loading');
    var idPrograma = $('#txtCalificacionIdPrograma').val();
    var idBips = $('#txtCalificacionIdBips').val();
    var opciones = {
        url: ROOT + "Gores/EnviarCalificacionPrograma",
        data: { idPrograma: idPrograma, idBips: idBips },
        type: "post",
        async: false,
        success: function (result, xhr, status) {
            if (result == "ok") {
                $("#modalEnviarCalificacion").modal('hide');
                modalMsjEnviarComentarios("<p>Se ha procesado la calificación exitosamente</p>");
                iniciaTablaEvalPrograma();
            }
        },
        error: function (xhr, status, error) {
            modalMsjEnviarComentarios("<p>Ha ocurrido un error durante el proceso: <b>" + error + "</b></p>");
            console.log(error);
        },
        complete: function () { $('#btnModalEnviarCalificacion').button('reset'); $("#cargaPagina").fadeOut(); }
    };
    $.ajax(opciones);
}
function modalMsjEnviarCalificacion(mensaje) {
    $('#txtMsjEnviarCalificacion').html(mensaje);
    $('#modalMsjEnviarCalificacion').modal('show');
}