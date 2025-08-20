$(document).ready(function () {
    $('ul.nav a').each(function () { if ($(this).hasClass('active')) { $(this).removeClass('active'); } });
    //'#aMenuEvalExAnte').addClass('active');
    $("#menuExAnte").addClass('in');
    $("#aMenuEvalExAnte").dropdown('toggle');
    iniciaTablaEvalExAnte();
    $("#btnMsjEvalAcep").click(function () {
        $(this).button('loading');
        var idPrograma = $("#txtIdPrograma").val();
        var evaluador = $("#txtIdEvaluador").val();
        var numEvaluador = $("#txtNumEvaluador").val();
        var opciones = {
            url: ROOT + "EvaluacionExAnte/AsignaEvaluador",
            data: { idPrograma: idPrograma, idEvaluador: evaluador, numEvaluador: numEvaluador },
            type: "post",
            datatype: "json",
            success: function (result, xhr, status) {
                $("#btnMsjEvalAcep").button('reset')
                if (result == "ok") {
                    $("#modalMsjEvaluadores").modal('hide');                    
                    modalMsjEvaluaciones("<p>Evaluador asignado con éxito</p>");
                } else {
                    modalMsjEvaluaciones("<p>" + result + "</p>");
                    console.log(result);
                }
            },
            error: function (xhr, status, error) {
                modalMsjEvaluaciones("<p>" + error + "</p>");
                console.log(error);
            }
        };
        $.ajax(opciones);
    });
    $("#btnModalEvalCancel").click(function () {
        var id = $("#txtIdPrograma").val();
        var numEval = $("#txtNumEvaluador").val();
        $(".cssEval" + id + numEval + " option[value='-1']").prop('selected', true);             
    });
    caracteresPregEval(1);
    $("#btnGuardarEvaluacion").click(function () {
        if (validaBlancosEvaluacion('validaEval')) {
            $(this).button('loading');
            var datos = {};
            datos.idPrograma = $("#txtIdProgramaEvaluacion").val();
            datos.calificacion = $("#cmbCalificacion option:selected").val();
            datos.comentSegpres = $("#txtComentSegpres").val();
            datos.comentGeneral = $("#txtComentGeneral").val();
            datos.atingencia = $("#txtAtingencia").val();
            datos.coherencia = $("#txtCoherencia").val();
            datos.consistencia = $("#txtConsistencia").val();
            datos.antecPrograma = $("#txtAntPrograma").val();
            datos.diagPrograma = $("#txtDiagPrograma").val();
            datos.objPoblPrograma = $("#txtObjPobPrograma").val();
            datos.estrategiaPrograma = $("#txtEstraPrograma").val();
            datos.indicadoresPrograma = $("#txtIndicadores").val();
            datos.gastosPrograma = $("#txtGastos").val();
            var opciones = {
                url: ROOT + "EvaluacionExAnte/GuardaEvaluacion",
                data: { data: datos },
                type: "post",
                datatype: "json",
                success: function (result, xhr, status) {
                    $("#btnGuardarEvaluacion").button('reset')
                    if (result == "ok") {
                        iniciaTablaEvalExAnte();
                        //$("#modalEvaluacion").modal('hide');
                        modalMsjEvaluaciones("<p>Evaluación guardada con éxito</p>");
                    } else {
                        modalMsjEvaluaciones("<p>" + result + "</p>");
                        console.log(result);
                    }
                },
                error: function (xhr, status, error) {
                    modalMsjEvaluaciones("<p>" + error + "</p>");
                    console.log(error);
                }
            };
            $.ajax(opciones);
        } else {
            modalMsjEvaluaciones("<p>Ingrese al menos un valor</p>");
        }
    });
    $("#btnEnviarEvaluacion").click(function () {        
        $("#modalMsjEnviaEvaluacion").modal('show');
    });
    $("#btnMsjEnviaEvaluacionAcep").click(function () {
        $("#btnEnviarEvaluacion").button('loading');
        var idPrograma = $("#txtIdProgramaEvaluacion").val();
        var datos = {};
        datos.idPrograma = $("#txtIdProgramaEvaluacion").val();
        datos.calificacion = $("#cmbCalificacion option:selected").val();
        datos.comentSegpres = $("#txtComentSegpres").val();
        datos.comentGeneral = $("#txtComentGeneral").val();
        datos.atingencia = $("#txtAtingencia").val();
        datos.coherencia = $("#txtCoherencia").val();
        datos.consistencia = $("#txtConsistencia").val();
        datos.antecPrograma = $("#txtAntPrograma").val();
        datos.diagPrograma = $("#txtDiagPrograma").val();
        datos.objPoblPrograma = $("#txtObjPobPrograma").val();
        datos.estrategiaPrograma = $("#txtEstraPrograma").val();
        datos.indicadoresPrograma = $("#txtIndicadores").val();
        datos.gastosPrograma = $("#txtGastos").val();
        var opciones = {
            url: ROOT + "EvaluacionExAnte/EnviaEvaluacion",
            data: { idPrograma: idPrograma, data: datos },
            type: "post",
            datatype: "json",
            success: function (result, xhr, status) {
                $("#btnEnviarEvaluacion").button('reset')
                if (result == "ok") {
                    iniciaTablaEvalExAnte();
                    $("#modalMsjEnviaEvaluacion").modal('hide');
                    $("#modalEvaluacion").modal('hide');
                    modalMsjEvaluaciones("<p>Evaluación enviada con éxito</p>");
                } else {
                    modalMsjEvaluaciones("<p>" + result + "</p>");
                    console.log(result);
                }
            },
            error: function (xhr, status, error) {
                modalMsjEvaluaciones("<p>" + error + "</p>");
                console.log(error);
            }
        };
        $.ajax(opciones);
    });
    $("#btnMsjCierraEvalAcep").click(function () {
        $(this).button('loading');
        var idPrograma = $("#txtIdProgCierraEval").val();
        var opciones = {
            url: ROOT + "EvaluacionExAnte/CierraEvaluacion",
            data: { idPrograma: idPrograma },
            type: "post",
            datatype: "json",
            success: function (result, xhr, status) {
                $("#btnMsjCierraEvalAcep").button('reset')
                if (result == "ok") {
                    $("#modalMsjCerrarEvaluacion").modal('hide');
                    //$('#btnEditarEval').attr('disabled', true);
                    //$('#cmbEval1').attr('disabled', true);
                    //$('#cmbEval2').attr('disabled', true);
                    //$("#btnCerrarEval").removeClass("btn-primary");
                    //$("#btnCerrarEval").addClass("btn-success");
                    //$('#btnCerrarEval').attr('disabled', true);
                    //iniciaTablaEvalExAnte();
                    modalMsjEvaluaciones("<p>Evaluación cerrada con éxito</p>");
                    window.location.reload();
                } else {
                    modalMsjEvaluaciones("<p>" + result + "</p>");
                    console.log(result);
                }
            },
            error: function (xhr, status, error) {
                modalMsjEvaluaciones("<p>" + error + "</p>");
                console.log(error);
            }
        };
        $.ajax(opciones);
    });
    $("#btnMsjNuevaIteracionAcep").click(function () {
        $(this).button('loading');
        var idPrograma = $("#txtIdProgNuevaIteracion").val();
        var versPrograma = $("#txtVersionProg").val();
        var opciones = {
            url: ROOT + "EvaluacionExAnte/NuevaIteracion",
            data: { idPrograma: idPrograma, versionPrograma: versPrograma },
            type: "post",
            datatype: "json",
            success: function (result, xhr, status) {
                $("#btnMsjNuevaIteracionAcep").button('reset')
                if (result == "ok") {
                    iniciaTablaEvalExAnte();
                    $("#modalMsjCrearNuevaIteracion").modal('hide');                    
                    modalMsjEvaluaciones("<p>Nueva iteración creada con éxito</p>");
                } else {
                    modalMsjEvaluaciones("<p>" + result + "</p>");
                    console.log(result);
                }
            },
            error: function (xhr, status, error) {
                modalMsjEvaluaciones("<p>" + error + "</p>");
                console.log(error);
            }
        };
        $.ajax(opciones);
    });
    if ($("#txtComentMonitoreo").val().length > 0) {
        var totalCaracteres = 8000;
        var restantes = (totalCaracteres - $("#txtComentMonitoreo").val().length) < 0 ? 0 : (totalCaracteres - $("#txtComentMonitoreo").val().length);
        $("#txtCountComentMonitoreo").text("Caracteres restantes=" + restantes);
    }
    $("#txtComentMonitoreo").keyup(function () {
        var totalCaracteres = 8000;
        var restantes = (totalCaracteres - $(this).val().length) < 0 ? 0 : (totalCaracteres - $(this).val().length);
        $("#txtCountComentMonitoreo").text("Caracteres restantes=" + restantes);
    });
    if ($("#txtComentEstudio").val().length > 0) {
        var totalCaracteres = 8000;
        var restantes = (totalCaracteres - $("#txtComentEstudio").val().length) < 0 ? 0 : (totalCaracteres - $("#txtComentEstudio").val().length);
        $("#txtCountComentEstudio").text("Caracteres restantes=" + restantes);
    }
    $("#txtComentEstudio").keyup(function () {
        var totalCaracteres = 8000;
        var restantes = (totalCaracteres - $(this).val().length) < 0 ? 0 : (totalCaracteres - $(this).val().length);
        $("#txtCountComentEstudio").text("Caracteres restantes=" + restantes);
    });
    if ($("#cmbTieneComentMonitoreo option:selected").text().toUpperCase() == "NO" || $("#cmbTieneComentMonitoreo option:selected").val() == "-1") {
        $(".divPregComentMonitoreo").hide();
    } else {
        $(".divPregComentMonitoreo").show();
    }
    $("#cmbTieneComentMonitoreo").change(function () {
        if ($("#cmbTieneComentMonitoreo option:selected").text().toUpperCase() == "NO" || $("#cmbTieneComentMonitoreo option:selected").val() == "-1") {
            $(".divPregComentMonitoreo").hide();
            $("#txtComentMonitoreo").val("");
        } else {
            $(".divPregComentMonitoreo").show();
        }
    });
    if ($("#cmbTieneComentEstudio option:selected").text().toUpperCase() == "NO" || $("#cmbTieneComentEstudio option:selected").val() == "-1") {
        $(".divPregComentEstudio").hide();
    } else {
        $(".divPregComentEstudio").show();
    }
    $("#cmbTieneComentEstudio").change(function () {
        if ($("#cmbTieneComentEstudio option:selected").text().toUpperCase() == "NO" || $("#cmbTieneComentEstudio option:selected").val() == "-1") {
            $(".divPregComentEstudio").hide();
            $("#txtComentEstudio").val("");
        } else {
            $(".divPregComentEstudio").show();
        }
    });

    if ($("#cmbTieneComentJefSect1 option:selected").text().toUpperCase() == "NO" || $("#cmbTieneComentJefSect1 option:selected").val() == "-1") {
        $(".divPregComentJefSect1").hide();
    } else {
        $(".divPregComentJefSect1").show();
    }
    $("#cmbTieneComentJefSect1").change(function () {
        if ($("#cmbTieneComentJefSect1 option:selected").text().toUpperCase() == "NO" || $("#cmbTieneComentJefSect1 option:selected").val() == "-1") {
            $(".divPregComentJefSect1").hide();
            $("#txtComentJefSect1").val("");
        } else {
            $(".divPregComentJefSect1").show();
        }
    });
    if ($("#cmbTieneComentJefSect2 option:selected").text().toUpperCase() == "NO" || $("#cmbTieneComentJefSect2 option:selected").val() == "-1") {
        $(".divPregComentJefSect2").hide();
    } else {
        $(".divPregComentJefSect2").show();
    }
    $("#cmbTieneComentJefSect2").change(function () {
        if ($("#cmbTieneComentJefSect2 option:selected").text().toUpperCase() == "NO" || $("#cmbTieneComentJefSect2 option:selected").val() == "-1") {
            $(".divPregComentJefSect2").hide();
            $("#txtComentJefSect2").val("");
        } else {
            $(".divPregComentJefSect2").show();
        }
    });
    if ($("#cmbTieneComentariosSect option:selected").text().toUpperCase() == "NO" || $("#cmbTieneComentariosSect option:selected").val() == "-1") {
        $(".divComentariosSect").hide();
    } else {
        $(".divComentariosSect").show();
    }
    $("#cmbTieneComentariosSect").change(function () {
        if ($("#cmbTieneComentariosSect option:selected").text().toUpperCase() == "NO" || $("#cmbTieneComentariosSect option:selected").val() == "-1") {
            $(".divComentariosSect").hide();
        } else {
            $(".divComentariosSect").show();
        }
    });

    $("#btnEnviarRevJefatura").click(function () {
        $("#modalMsjRevJef").modal('show');        
    });
    $("#btnMsjEnviaRevJefAcep").click(function () {
        $(this).button('loading');
        var fd = new FormData();
        var file = document.getElementById("idInformePDF").files[0];
        fd.append('informePDF', file);
        fd.append('idPrograma', $("#txtIdProgramaRevJef").val());
        fd.append('tieneComentMonitoreo', $("#cmbTieneComentMonitoreo").val());
        fd.append('comentMonitoreo', $("#txtComentMonitoreo").val());
        fd.append('tieneComentEstudio', $("#cmbTieneComentEstudio").val());
        fd.append('comentEstudio', $("#txtComentEstudio").val());
        fd.append('evaluador1', $("#cmbEvaluador1").val());
        fd.append('evaluador2', $("#cmbEvaluador2").val());
        var opciones = {
            url: ROOT + "EvaluacionExAnte/revisarEvaluacion",
            data: fd,
            type: "post",
            contentType: false,
            processData: false,
            success: function (result, xhr, status) {
                $("#btnMsjEnviaRevJefAcep").button('reset');
                if (result == "ok") {
                    $("#modalRevisionJefaturas").modal('hide');
                    $("#modalMsjRevJef").modal('hide');
                    modalMsjEvaluaciones("<p>Comentarios enviados con éxito</p>");
                } else {
                    modalMsjEvaluaciones("<p>" + result + "</p>");
                    console.log(result);
                }
            },
            error: function (xhr, status, error) {
                modalMsjEvaluaciones("<p>" + error + "</p>");
                console.log(error);
            }
        };
        $.ajax(opciones);
    });
    /*$("#btnFinalizarRevJefatura").click(function () {
        var file = document.getElementById("idInformePDF").files[0];
        if (file != null && file != "") {
            $("#modalMsjFinRevJef").modal('show');
        } else {
            modalMsjEvaluaciones("<p>Seleccione informe a enviar</p>");
        }
    });*/
    $("#btnMsjEnviaFinRevJefAcep").click(function () {
        var fd = new FormData();
        var file = document.getElementById("idInformePDF").files[0];
        if (file != null && file != "") {
            $(this).button('loading');
            fd.append('informePDF', file);
            fd.append('idPrograma', $("#txtIdProgramaFinRevJef").val());
            var opciones = {
                url: ROOT + "EvaluacionExAnte/GetFinalizarRevisionExAnte",
                data: fd,
                type: "post",
                contentType: false,
                processData: false,
                success: function (result, xhr, status) {
                    $("#btnMsjEnviaFinRevJefAcep").button('reset');
                    if (result == "ok") {
                        $("#modalRevisionJefaturas").modal('hide');
                        $("#modalMsjFinRevJef").modal('hide');
                        iniciaTablaEvalExAnte();
                        modalMsjEvaluaciones("<p>Informe enviado con éxito</p>");
                    } else {
                        modalMsjEvaluaciones("<p>" + result + "</p>");
                        console.log(result);
                    }
                },
                error: function (xhr, status, error) {
                    modalMsjEvaluaciones("<p>" + error + "</p>");
                    console.log(error);
                }
            };
            $.ajax(opciones);
        }else{
            modalMsjEvaluaciones("<p>Seleccione informe a enviar</p>");
        }
    });
    $("#btnEnviarCS").click(function () {
        var fd = new FormData();
        var file = document.getElementById("idInformeSectPDF").files[0];
        $(this).button('loading');
        fd.append('informePDF', file);
        fd.append('idPrograma', $("#txtIdProgramaCS").val());
        fd.append('evaluador1', $("#cmbEvaluador1CS").val());
        fd.append('evaluador2', $("#cmbEvaluador2CS").val());
        fd.append('comentarios', $("#cmbTieneComentariosSect option:selected").val());
        var opciones = {
            url: ROOT + "EvaluacionExAnte/GetEnviarComentariosSect",
            data: fd,
            type: "post",
            contentType: false,
            processData: false,
            success: function (result, xhr, status) {
                $("#btnEnviarCS").button('reset');
                if (result == "ok") {
                    $("#modalComentariosSectorialistas").modal('hide');
                    iniciaTablaEvalExAnte();
                    modalMsjEvaluaciones("<p>Comentarios enviados con éxito</p>");
                } else {
                    modalMsjEvaluaciones("<p>" + result + "</p>");
                    console.log(result);
                }
            },
            error: function (xhr, status, error) {
                modalMsjEvaluaciones("<p>" + error + "</p>");
                console.log(error);
            }
        };
        $.ajax(opciones);
    });
    $("#btnEnviarCJS1").click(function () {
        var fd = new FormData();
        var file = document.getElementById("idInformeJefSectPDF1").files[0];        
        $(this).button('loading');
        fd.append('informePDF', file);
        fd.append('idPrograma', $("#txtIdProgramaCJS1").val());
        fd.append('evaluador1', $("#cmbEvaluador1CJS1").val());
        fd.append('evaluador2', $("#cmbEvaluador2CJS1").val());            
        fd.append('tipoJefatura', 1);
        fd.append('comentarios', $("#txtComentJefSect1").val());
        var opciones = {
            url: ROOT + "EvaluacionExAnte/GetEnviarComentJefSect",
            data: fd,
            type: "post",
            contentType: false,
            processData: false,
            success: function (result, xhr, status) {
                $("#btnEnviarCJS1").button('reset');
                if (result == "ok") {
                    $("#modalComentJefesSectorialistas1").modal('hide');
                    iniciaTablaEvalExAnte();
                    modalMsjEvaluaciones("<p>Comentarios enviados con éxito</p>");
                } else {
                    modalMsjEvaluaciones("<p>" + result + "</p>");
                    console.log(result);
                }
            },
            error: function (xhr, status, error) {
                modalMsjEvaluaciones("<p>" + error + "</p>");
                console.log(error);
            }
        };
        $.ajax(opciones);
    });
    $("#btnEnviarCJS2").click(function () {
        var fd = new FormData();
        var file = document.getElementById("idInformeJefSectPDF2").files[0];
        $(this).button('loading');
        fd.append('informePDF', file);
        fd.append('idPrograma', $("#txtIdProgramaCJS2").val());
        fd.append('evaluador1', $("#cmbEvaluador1CJS2").val());
        fd.append('evaluador2', $("#cmbEvaluador2CJS2").val());
        fd.append('tipoJefatura', 2);
        fd.append('comentarios', $("#txtComentJefSect2").val());
        var opciones = {
            url: ROOT + "EvaluacionExAnte/GetEnviarComentJefSect",
            data: fd,
            type: "post",
            contentType: false,
            processData: false,
            success: function (result, xhr, status) {
                $("#btnEnviarCJS1").button('reset');
                if (result == "ok") {
                    $("#modalComentJefesSectorialistas2").modal('hide');
                    iniciaTablaEvalExAnte();
                    modalMsjEvaluaciones("<p>Comentarios enviados con éxito</p>");
                } else {
                    modalMsjEvaluaciones("<p>" + result + "</p>");
                    console.log(result);
                }
            },
            error: function (xhr, status, error) {
                modalMsjEvaluaciones("<p>" + error + "</p>");
                console.log(error);
            }
        };
        $.ajax(opciones);
    });
});
$(document).on('change', 'input[type="file"]', function () {
    var fileName = this.files[0].name;
    var fileSize = this.files[0].size;
    var ext = fileName.split('.');    
    ext = ext[ext.length - 1];
    if (ext != "pdf") {
        modalMsjEvaluaciones("<p>El tipo de archivo seleccionado no es válido</p>");
        this.value = '';
    }
});
function iniciaTablaEvalExAnte() {
    var tablaEvalExAnte = $('#tblEvalExAnte').DataTable();
    tablaEvalExAnte.destroy();
    $('#tblEvalExAnte').dataTable({
        "processing": true,
        "ajax": {
            "url": ROOT + "EvaluacionExAnte/GetEvaluacionesExAnte",
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
                    return '<a href="' + link + '" data-toggle="tooltip" data-placement="right" title="Click para abrir programa" target="_blank">' + full.Nombre + '</a>';
                }
            },
            { "data": 'Version', "width": "2%" },
            { "data": 'PuntajeFinal', "width": "2%" },
            { "data": 'Calificacion', "width": "5%" },
            {
                "data": null, "orderable": false, "width": "3%",
                "render": function (data, type, full, meta) {
                    var claseCierre = ((full.IdEtapa == etapaCierre || perfil != coordinador) ? "disabled" : "");
                    var select = '<select id="cmbEval1" class="form-control input-sm cssEval' + full.IdPrograma + '1" onchange="cmbEvaluadores(' + full.IdPrograma + ',this.value, 1);" ' + claseCierre + '>'
                    select += '<option value="-1">Seleccione</option>';
                    $.each(aEvaluadores, function (i, item) {
                        var sel = (item.Id == full.IdEvaluador1 ? "selected='selected'" : "");
                        select += '<option value="' + item.Id + '" ' + sel + '>' + item.Nombre + '</option>';
                    });
                    select += '</select>'
                    return select;
                }
            },
            {
                "data": null, "orderable": false, "width": "3%",
                "render": function (data, type, full, meta) {
                    var claseCierre = ((full.IdEtapa == etapaCierre || perfil != coordinador) ? "disabled" : "");
                    var select = '<select id="cmbEval2" class="form-control input-sm cssEval' + full.IdPrograma + '2" onchange="cmbEvaluadores(' + full.IdPrograma + ',this.value, 2);" ' + claseCierre + '>'
                    select += '<option value="-1">Seleccione</option>';
                    $.each(aEvaluadores, function (i, item) {
                        var sel = (item.Id == full.IdEvaluador2 ? "selected='selected'" : "");
                        select += '<option value="' + item.Id + '" ' + sel + '>' + item.Nombre + '</option>';
                    });
                    select += '</select>'
                    return select;
                }
            },
            {
                "data": 'Revisión Sectorialista', "orderable": false, "width": "1%", "visible": (tipoJefatura != 1 && tipoJefatura != 2),
                "render": function (data, type, full, meta) {
                    var claseCierre = ((full.IdEtapa == etapaCierre || perfil == soloLectura) ? "disabled" : "");
                    //return '<button id="btnEditarEval" class="btn btn-info btn-xs" type="button" data-toggle="tooltip" data-placement="left" title="Editar Evaluación" onclick="modelEvaluacion(' + full.IdPrograma + ')" ' + claseCierre + '><i class="fa fa-pencil-square-o"></i></button>';
                    return '<button id="btnComentSect" class="btn btn-info btn-xs" type="button" data-toggle="tooltip" data-placement="left" title="Enviar Comentarios" onclick="modelComentSect(' + full.IdPrograma + ')" ' + claseCierre + '><i class="fa fa-pencil-square-o"></i></button>';
                }
            },
            /*{
                "data": null, "orderable": false, "width": "1%",
                "render": function (data, type, full, meta) {
                    var tipo = (listaNuevosExAnte.indexOf(full.IdTipoFormulario) >= 0 ? 1 : (listaReformuladosExAnte.indexOf(full.IdTipoFormulario) >= 0 ? 2 : 0));
                    var inforDeta = '<a class="btn btn-default btn-xs" data-toggle="tooltip" data-placement="left" title="Informe Detalle" href="' + informeDetalle.replace("{0}", tipo).replace("{1}", full.IdPrograma).replace("{2}", full.Ano) + '" target="_blank"><i class="fa fa-file-pdf-o"></i></a>';
                    var inforEval = '<a class="btn btn-default btn-xs" style="margin-left:2px;" data-toggle="tooltip" data-placement="left" title="Informe Evaluación" href="' + informeEvaluacion.replace("{0}", tipo).replace("{1}", full.IdPrograma).replace("{2}", full.Ano) + '" target="_blank"><i class="fa fa-file-pdf-o"></i></a>';
                    return '<div style="width:50px;">' + inforDeta + inforEval + '</div>';
                }
            },*/
            {
                "data": 'Revisión Monitoreo', "orderable": false, "width": "1%", "visible": (tipoJefatura === 1),
                "render": function (data, type, full, meta) {
                    var claseCierre = ((full.IdEtapa == etapaCierre || perfil == soloLectura) ? "disabled" : "");
                    var clase = (full.IdEtapa == etapaCierre ? "success" : "primary");
                    return '<button id="btnMonitoreo" class="btn btn-' + clase + ' btn-xs" type="button" data-toggle="tooltip" data-placement="left" title="Revisión Monitoreo" onclick="modelComentJefEval(' + full.IdPrograma + ',1)" ' + claseCierre + '><i class="fa fa-check-square-o"></i></button>';
                }
            },
            {
                "data": 'Revisión Estudios', "orderable": false, "width": "1%", "visible": (tipoJefatura === 2),
                "render": function (data, type, full, meta) {
                    var claseCierre = ((full.IdEtapa == etapaCierre || perfil == soloLectura) ? "disabled" : "");
                    var clase = (full.IdEtapa == etapaCierre ? "success" : "primary");
                    return '<button id="btnEstudios" class="btn btn-' + clase + ' btn-xs" type="button" data-toggle="tooltip" data-placement="left" title="Revisión Estudios" onclick="modelComentJefEval(' + full.IdPrograma + ',2);" ' + claseCierre + '><i class="fa fa-check-square-o"></i></button>';
                }
            },
            {
                "data": 'Finalizar Evaluación', "orderable": false, "width": "1%", "visible": (tipoJefatura === 1),
                "render": function (data, type, full, meta) {
                    //var claseCierre = ((full.IdEtapa == etapaCierre || perfil != coordinador) ? "disabled" : "");
                    //var titleCierre = (full.IdEtapa == etapaCierre ? "Evaluación cerrada" : "Cerrar evaluación");
                    var claseCierre = ((full.IdEtapa == etapaCierre || perfil == soloLectura) ? "disabled" : "");
                    var clase = (full.IdEtapa == etapaCierre ? "success" : "warning");
                    return '<button id="btnCerrarEval" class="btn btn-' + clase + ' btn-xs" type="button" data-toggle="tooltip" data-placement="left" title="Finalizar Revisión" onclick="cierraEvaluacionJefatura(' + full.IdPrograma + ');" ' + claseCierre + '><i class="fa fa-check-square"></i></button>';
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            var btnCreaForm = '<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>';
            $('#tblEvalExAnte_spFiltro').append(btnCreaForm);
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
}
function modelComentJefEval(id, jefatura) {
    $("#txtIdProgramaCJS" + jefatura).val(id);
    $("#cmbEvaluador1CJS" + jefatura).val($(".cssEval" + id + "1").val());
    $("#cmbEvaluador2CJS" + jefatura).val($(".cssEval" + id + "2").val());
    $("#modalComentJefesSectorialistas" + jefatura).modal('show');
}
function modelComentSect(id) {
    $("#txtIdProgramaCS").val(id);
    $("#cmbEvaluador1CS").val($(".cssEval" + id + "1").val());
    $("#cmbEvaluador2CS").val($(".cssEval" + id + "2").val());
    $("#modalComentariosSectorialistas").modal('show');
}
function modelEvaluacion(id) {
    $("#txtIdProgramaEvaluacion").val(id);
    $("#cargaPagina").fadeIn();
    cargaPreguntasEval(id);    
}
function cmbEvaluadores(idPrograma, evaluador, numEvaluador) {
    var otroEvaluador = (numEvaluador == 1 ? 2 : 1);
    var idOtroEvaluador = $(".cssEval" + idPrograma + otroEvaluador).val();
    if (idOtroEvaluador != evaluador) {
        if (evaluador != "-1") {
            $("#txtIdPrograma").val(idPrograma);
            $("#txtIdEvaluador").val(evaluador);
            $("#txtNumEvaluador").val(numEvaluador);
            $("#modalMsjEvaluadores").modal('show');
        }
    } else {
        $(".cssEval" + idPrograma + numEvaluador + " option[value='-1']").prop('selected', true);
        modalMsjEvaluaciones("<p>Evaluador ya asignado para esta evaluación, por favor seleccione otro</p>");
    }
}
function modalMsjEvaluaciones(mensaje) {
    $('#msjInfoEval').empty();
    $('#msjInfoEval').html(mensaje);
    $('#modalMsjEvaluaciones').modal('show');
}
function caracteresPregEval(inicio) {
    caractPregEvaluacion("txtComentSegpres", 8000, 1, inicio);
    caractPregEvaluacion("txtComentGeneral", 8000, 2, inicio);
    caractPregEvaluacion("txtAtingencia", 8000, 3, inicio);
    caractPregEvaluacion("txtCoherencia", 8000, 4, inicio);
    caractPregEvaluacion("txtConsistencia", 8000, 5, inicio);
    caractPregEvaluacion("txtAntPrograma", 8000, 6, inicio);
    caractPregEvaluacion("txtDiagPrograma", 8000, 7, inicio);
    caractPregEvaluacion("txtObjPobPrograma", 8000, 8, inicio);
    caractPregEvaluacion("txtEstraPrograma", 8000, 9, inicio);
    caractPregEvaluacion("txtIndicadores", 8000, 10, inicio);
    caractPregEvaluacion("txtGastos", 8000, 11, inicio);
}
function caractPregEvaluacion(idObjeto, largoCaracteres, idCount, inicio) {
    if ($("#" + idObjeto).val().length > 0) {
        var totalCaracteres = largoCaracteres;
        var restantes = (totalCaracteres - $("#" + idObjeto).val().length) < 0 ? 0 : (totalCaracteres - $("#" + idObjeto).val().length);
        $("#txtCount" + idCount).text("Caracteres restantes=" + restantes);
    } else {
        $("#txtCount" + idCount).text("Caracteres restantes=" + largoCaracteres);
    }
    if (inicio) {
        $("#" + idObjeto).keyup(function () {
            var totalCaracteres = largoCaracteres;
            var restantes = (totalCaracteres - $(this).val().length) < 0 ? 0 : (totalCaracteres - $(this).val().length);
            $("#txtCount" + idCount).text("Caracteres restantes=" + restantes);
        });
    }    
}
function validaBlancosEvaluacion(clase, valorComparativo) {
    var validacion = 0;
    $("." + clase).each(function (i, item) {
        if ($.trim($(this).val()) != "") {
            validacion = 1;
            return false;
        }
    });
    return validacion;
}
function cargaPreguntasEval(id) {
    var opciones = {
        url: ROOT + "EvaluacionExAnte/GetPreguntasEvaluaciones",
        data: { idPrograma: id },
        type: "post",
        datatype: "json",
        success: function (result, xhr, status) {
            if (result.calificacion != null) {
                $('#cmbCalificacion option[value="' + result.calificacion + '"]').attr('selected', 'selected');
            } else {
                $("#cmbCalificacion option:selected").removeAttr("selected");
            }            
            $("#txtComentSegpres").val(result.comentSegpres);
            $("#txtComentGeneral").val(result.comentGeneral);
            $("#txtAtingencia").val(result.atingencia);
            $("#txtCoherencia").val(result.coherencia);
            $("#txtConsistencia").val(result.consistencia);
            $("#txtAntPrograma").val(result.antecPrograma);
            $("#txtDiagPrograma").val(result.diagPrograma);
            $("#txtObjPobPrograma").val(result.objPoblPrograma);
            $("#txtEstraPrograma").val(result.estrategiaPrograma);
            $("#txtIndicadores").val(result.indicadoresPrograma);
            $("#txtGastos").val(result.gastosPrograma);
            caracteresPregEval(0);
            $("#modalEvaluacion").modal('show');
            $("#cargaPagina").fadeOut();
        },
        error: function (xhr, status, error) {
            modalMsjEvaluaciones("<p>" + error + "</p>");
            console.log(error);
        }
    };
    $.ajax(opciones);
}
function modalCerrarEvaluacion(id) {
    $("#cargaPagina").fadeIn();
    $("#txtIdProgramaRevJef").val(id);
    $("#cmbEvaluador1").val($(".cssEval" + id + "1").val());
    $("#cmbEvaluador2").val($(".cssEval" + id + "2").val());
    $('#cmbTieneComentMonitoreo option[value="-1"]').prop('selected', true).trigger('change');
    $('#cmbTieneComentEstudio option[value="-1"]').prop('selected', true).trigger('change');
    $("#txtComentMonitoreo").val("");
    $("#txtComentEstudio").val("");
    $("#idInformePDF").val("");
    $("#btnFinalizarRevJefatura").button('reset');
    var opciones = {
        url: ROOT + "EvaluacionExAnte/GetComentariosJefaturas",
        data: { idPrograma: id },
        type: "post",
        datatype: "json",
        success: function (result, xhr, status) {
            if (result.tieneComentMonitoreo != null) {
                $('#cmbTieneComentMonitoreo option[value="' + result.tieneComentMonitoreo + '"]').prop('selected', true).trigger('change');
            }
            $("#txtComentMonitoreo").val(result.comentMonitoreo);
            caractPregEvaluacion("txtComentMonitoreo", 8000, "ComentMonitoreo", false);
            if (result.tieneComentEstudios != null) {
                $('#cmbTieneComentEstudio option[value="' + result.tieneComentEstudios + '"]').prop('selected', true).trigger('change');
            }            
            $("#txtComentEstudio").val(result.comentEstudios);
            caractPregEvaluacion("txtComentEstudio", 8000, "ComentEstudio", false);
            $("#modalRevisionJefaturas").modal('show');
            $("#cargaPagina").fadeOut();
        },
        error: function (xhr, status, error) {
            $("#cargaPagina").fadeOut();
            modalMsjEvaluaciones("<p>" + error + "</p>");
            console.log(error);            
        }
    };
    $.ajax(opciones);
}
function modalCrearIteracion(id, version) {
    $("#txtIdProgNuevaIteracion").val(id);
    $("#txtVersionProg").val(version);
    $("#modalMsjCrearNuevaIteracion").modal('show');
}
function cierraEvaluacionJefatura(id) {
    $("#txtIdProgramaFinRevJef").val(id);
    $("#modalMsjFinRevJef").modal('show');
}