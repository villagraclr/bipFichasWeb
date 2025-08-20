$(document).ready(function () {
    $('ul.nav a').each(function () { if ($(this).hasClass('active')) { $(this).removeClass('active'); } });
    $("#liGores").addClass('active');
    $("#liEvaluadoresGore").addClass('active');
    $('#aMenuEvaluadorPerfil').addClass('active');
    $("#menuGores").addClass('in');
    $("#menuEvaluadoresGore").addClass('in');
    iniciaTablaEvalPerfil();
    $("#btnModalAceptaEvaluador").click(function () {
        $("#cargaPagina").fadeIn();
        $('#btnModalAceptaEvaluador').button('loading');
        var idPrograma = $("#txtIdPrograma").val();
        var evaluador = $("#txtIdEvaluador").val();
        var evaluadorAnterior = ($("#txtEva" + idPrograma).val() ? $("#txtEva" + idPrograma).val() : null)
        //var numEvaluador = $("#txtNumEvaluador").val();
        var opciones = {
            url: ROOT + "Gores/AsignaEvaluadorPerfil",
            data: { idPrograma: idPrograma, idEvaluador: evaluador, idEvaluadorAnterior: evaluadorAnterior },
            type: "post",
            datatype: "json",
            async: false,
            beforeSend: function () { },
            success: function (result, xhr, status) {
                //$("#btnModalAceptaResultadoEvaluador").button('reset')
                if (result == "ok") {
                    $("#modalAsignaEvaluador").modal('hide');
                    modalResultados("<p>Evaluador asignado con éxito</p>");
                    iniciaTablaEvalPerfil();
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
        if (evaluadorActual != 'null') {
            $("#cmbEval" + idPrograma).val(evaluadorActual).trigger('change');
        } else {
            $("#cmbEval" + idPrograma).val("-1").trigger('change');
        }
    });
    $("#btnModalAceptaTipoOferta").click(function () {
        $("#cargaPagina").fadeIn();
        $('#btnModalAceptaTipoOferta').button('loading');
        var idPrograma = $("#txtIdPrograma").val();
        var tipoOferta = $("#txtTipoOferta").val();
        var evaluadorDipres = tipoOferta == 13002 ? (evaluadores.filter(element => element.IdMinisterio == 30).map(function (e) { return e.Id; })) : [];
        var evaluadorAnterior = tipoOferta == 13002 ? ($("#txtEva" + idPrograma).val() ? $("#txtEva" + idPrograma).val() : []) : (evaluadores.filter(element => element.IdMinisterio == 30).map(function (e) { return e.Id; }));
        var opciones = {
            url: ROOT + "Gores/AsignaTipoOferta",
            data: { idPrograma: idPrograma, tipoOferta: tipoOferta, evaluadorDipres: evaluadorDipres, idEvaluadorAnterior: evaluadorAnterior },
            type: "post",
            datatype: "json",
            async: false,
            beforeSend: function () { },
            success: function (result, xhr, status) {
                if (result == "ok") {
                    $("#modalAsignaTipoOferta").modal('hide');
                    modalResultados("<p>Tipo oferta asignado con éxito</p>");
                    iniciaTablaEvalPerfil();
                } else {
                    modalResultados("<p>" + result + "</p>");
                }
                $('#btnModalAceptaTipoOferta').button('reset');
            },
            error: function (xhr, status, error) {
                modalResultados("<p>" + error + "</p>");
                console.log(error);
                $('#btnModalAceptaTipoOferta').button('reset');
            },
            complete: function () { $("#cargaPagina").fadeOut(); }
        };
        $.ajax(opciones);
    });
    $("#btnModalCancelaTipoOferta").click(function () {
        var idPrograma = $("#txtIdPrograma").val();
        if (tipoOfertaActual != null) {
            $("#cmbOfer" + idPrograma).val(tipoOfertaActual).trigger('change');
        } else {
            $("#cmbOfer" + idPrograma).val("-1").trigger('change');
        }
    });
});
var evaluadorActual = null;
var tipoOfertaActual = null;
function iniciaTablaEvalPerfil() {
    var tablaEvalPerfil = $('#tblEvalPerfil').DataTable();
    tablaEvalPerfil.destroy();
    $('#tblEvalPerfil').dataTable({
        "processing": true,
        "ajax": {
            "url": ROOT + "Gores/GetEvaluacionesPerfil",
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
                "data": 'Calificacion', "width": "5%",
                "render": function (data, type, full, meta) {
                    var calificacion = '<p>' + (data === null ? "Sin calificar" : data) + '</p>'
                    return calificacion;
                }
            },
            {
                "data": null, "orderable": false, "width": "12%",
                "render": function (data, type, full, meta) {
                    var claseCierre = ((full.IdEtapa == etapaCierrePerfilGore || full.IdEtapa == etapaPerfilEnConsulta) ? "disabled" : "");
                    var select = '<select id="cmbOfer' + full.IdPrograma + '" class="form-control input-sm cssEval' + full.IdPrograma + '1" onchange="cmbTipoOferta(' + full.IdPrograma + ',this.value, ' + full.TipoGeneral + ');" ' + claseCierre + ' style="width: 100%;">'
                    if (full.TipoGeneral == null) {
                        select += '<option value="-1">Seleccione</option>';
                    }
                    select += '<option ' + ('13001' == full.TipoGeneral ? "selected='selected'" : "") + ' value="13001">Social</option>';
                    select += '<option ' + ('13002' == full.TipoGeneral ? "selected='selected'" : "") + ' value="13002">No Social</option>';
                    select += '</select>'
                    //$('#cmbOfer' + full.IdPrograma).select2();
                    return select;
                }
            },
            {
                "data": null, "orderable": false, "width": "12%",
                "render": function (data, type, full, meta) {
                    var idEvaluador1 = full.IdEvaluador1 ? encodeURIComponent(full.IdEvaluador1) : null;
                    var claseCierre = ((full.IdEtapa == etapaCierrePerfilGore || full.IdEtapa == etapaPerfilEnConsulta) ? "disabled" : "");
                    if (full.TipoGeneral == '13001' || full.TipoGeneral == null) {
                        var select = '<select id="cmbEval' + full.IdPrograma + '" class="form-control input-sm cssEval' + full.IdPrograma + '1" onchange="cmbEvaluadores(' + full.IdPrograma + ',this.value, 1, \'' + idEvaluador1 + '\');" ' + claseCierre + ' style="width: 100%;"  ' + (full.TipoGeneral == null ? "disabled" : "") + '>'
                        if (full.IdEvaluador1 == null) {
                            select += '<option value="-1">Seleccione</option>';
                        }
                        $.each(evaluadores, function (i, item) {
                            if (item.IdMinisterio != 30) {
                                var sel = (item.Id == full.IdEvaluador1 ? "selected='selected'" : "");
                                select += '<option value="' + item.Id + '" ' + sel + '>' + item.Nombre + '</option>';
                            }
                        });
                        select += '</select>'
                    } else {
                        var select = '<select multiple id="cmbEval' + full.IdPrograma + '" style="width: 100%;" disabled>'
                        $.each(evaluadores, function (i, item) {
                            //Ministerio de hacienda
                            if (item.IdMinisterio == 30) {
                                select += '<option value="' + item.Id + '" selected>' + item.Nombre + '</option>';
                            }
                        });
                        select += '</select>'
                    }
                    //$('#cmbEval' + full.IdPrograma).select2();
                    if (full.IdEvaluador1 != null) {
                        //Evaluador Anterior
                        select += '<input type="hidden" id="txtEva' + full.IdPrograma + '" value="' + full.IdEvaluador1 + '" />'
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
                    var claseCierre = ((full.IdEtapa == etapaPerfilEnConsulta) ? "" : "disabled");
                    var accion = ''
                    accion += '<button id="btnCorrespondeIngreso" class="btn btn-primary btn-xs" ' + claseCierre + ' type="button" data-toggle="tooltip" data-placement="left" title="Corresponde ingreso" onclick="ingresoExAnte(' + full.IdPrograma + ');"><i class="fa fa-sign-out"></i></button>';
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
function cmbEvaluadores(idPrograma, evaluador, numEvaluador, valor) {
    evaluadorActual = valor;
    if (evaluador != "-1") {
        $("#txtIdPrograma").val(idPrograma);
        $("#txtIdEvaluador").val(evaluador);
        $("#txtNumEvaluador").val(numEvaluador);
        $("#modalAsignaEvaluador").modal('show');
    }
}
function cmbTipoOferta(idPrograma, tipoOferta, valor) {
    tipoOfertaActual = valor;
    if (tipoOferta != "-1") {
        $("#txtIdPrograma").val(idPrograma);
        $("#txtTipoOferta").val(tipoOferta);
        if (tipoOferta == 13001) {
            $("#msjSeleccionOferta").html('<p style="margin-bottom: 10px;">Al seleccionar <strong>Social</strong>, se habilitará una lista donde se podrá asignar un evaluador</p><p style="margin-bottom: 5px;">¿Asignar el tipo del oferta seleccionada?</p>');
        } else {
            $("#msjSeleccionOferta").html('<p style="margin-bottom: 10px;">Al seleccionar <strong>No Social</strong>, los evaluadores se asignarán automáticamente y no podrán ser editados hasta volver a cambiar el tipo de oferta</p><p style="margin-bottom: 5px;">¿Asignar el tipo del oferta seleccionada?</p>');
        }
        $("#modalAsignaTipoOferta").modal('show');
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

function ejecutarIngresoExAnte(correspondeIngreso) {
    $("#cargaPagina").fadeIn();
    var idPrograma = $('#txtIngresoIdPrograma').val();
    var idEvaluador = $('#cmbEval'+ idPrograma).val();
    if (correspondeIngreso) {
        $('#btnIngresoExAnte').button('loading');
        //$('#btnNoIngresoExAnte').prop('disabled', true);
    } else {
        $('#btnNoIngresoExAnte').button('loading');
        //$('#btnIngresoExAnte').prop('disabled', true);
    }
    var opciones = {
        url: ROOT + "Gores/correspondeIngresoExAnte",
        data: { idPrograma: idPrograma, idEvaluador: idEvaluador, correspondeIngreso: correspondeIngreso },
        type: "post",
        datatype: "json",
        async: false,
        beforeSend: function () { },
        success: function (result, xhr, status) {
            if (result == "ok") {
                $("#modalIngresoExAnte").modal('hide');
                var mensaje = correspondeIngreso ? "<p>Se ha <b>permitido</b> el ingreso del Perfil</p>" : "<p>Se ha <b>denegado</b> el ingreso del Perfil</p>";
                modalResultados(mensaje);
                iniciaTablaEvalPerfil();
            } else {
                modalResultados("<p>" + result + "</p>");
            }
        },
        error: function (xhr, status, error) {
            modalResultados("<p>" + error + "</p>");
            console.log(error);
            $('#btnNoIngresoExAnte').button('reset');
            $('#btnIngresoExAnte').button('reset');
        },
        complete: function () {
            $('#btnNoIngresoExAnte').button('reset');
            $('#btnIngresoExAnte').button('reset');
            //$('#btnNoIngresoExAnte').prop('disabled', false);
            //$('#btnIngresoExAnte').prop('disabled', false);
            $("#cargaPagina").fadeOut();
        }
    };
    $.ajax(opciones);
}