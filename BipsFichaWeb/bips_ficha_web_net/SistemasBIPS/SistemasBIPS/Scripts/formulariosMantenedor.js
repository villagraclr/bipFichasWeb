$(document).ready(function () {
    $("#menuMantenedores").addClass('in');
    $("#aMenuNuevosForm").dropdown('toggle');
    iniciaTablaFormularios();
    $("#btnSiguienteCreaForm").click(function () {        
        if ($("#cmbTipoCreaForm").val() != "-1") {
            $("#modalNuevoFormulario").modal('hide');
            if ($("#cmbTipoCreaForm").val() == "1") {
                //$("#modalNuevoFormulario").modal('hide');
                $("#modalFormularioVacio").modal('show');
            } else if ($("#cmbTipoCreaForm").val() == "2") {
                $("#modalOrigenFormulario").modal('show');
            } else if ($("#cmbTipoCreaForm").val() == "3") {                
                iniciaTablaFormRestrucc();
                $("#modalFormRestruc").modal('show');
            }else {
                if ($("#cmbTipoFormulario").val() != "-1") {
                    //$("#modalNuevoFormulario").modal('hide');
                    iniciaTablaFormFiltros($("#cmbTipoFormulario").val(), $("#cmbAnoRestrucc").val());
                    $("#txtFiltro1").val($("#cmbTipoCreaForm").val());
                    $("#txtFiltro2").val($("#cmbTipoFormulario").val());
                    $("#modalFormularios").modal('show');
                } else {
                    modalMsjFormularios("<p>Seleccione al menos un tipo de formulario</p>");
                }
            }            
        } else {
            modalMsjFormularios("<p>Seleccione al menos un tipo de creación</p>");
        }
    });
    $("#btnSiguienteOrigForm").click(function () {        
        if (validaSeleccione('filtraTipFormOrig')) {
            var filtros = filtrosFormOrg();
            iniciaTablaFormFiltros(filtros);
            $("#modalOrigenFormulario").modal('hide');
            $("#modalFormularios").modal('show');
        } else {
            modalMsjFormularios("<p>Seleccione un tipo de formulario</p>");
        }
    });
    $("#btnAtrasOrigForm").click(function () {
        $("#modalNuevoFormulario").modal('show');
    });
    $("#btnGuardaFormularios").click(function () {
        if ($("#idTipoAgreForm").val() == "Individual") {
            if (aAgregaFormOrig.length > 0) {
                $("#txtFiltro31").val($("#txtFiltro2").val());
                $("#modalFormularios").modal('hide');
                $("#modalFormulariosDestino").modal('show');
            } else {
                modalMsjFormularios("<p>Seleccione al menos un formulario</p>");
            }
        } else {
            if (validaSeleccione('agregaFormMasivo')) {
                $("#txtFiltro31").val($("#txtFiltro2").val());
                $("#modalFormularios").modal('hide');
                $("#modalFormulariosDestino").modal('show');
            } else {
                modalMsjFormularios("<p>Seleccione al menos una categoría</p>");
            }
        }
    });
    $("#btnAtrasFormularios").click(function () {
        //$("#modalFormularios").modal('hide');
        //$("#cmbTipoCreaForm option[value=" + $("#txtFiltro1").val() + "]").prop("selected", true);
        //$("#cmbTipoFormulario option[value=" + $("#txtFiltro2").val() + "]").prop("selected", true);
        //$("#modalNuevoFormulario").modal('show');
        $("#modalOrigenFormulario").modal('show');
    });
    $("#btnAtrasFormNuevo").click(function () {        
        if (validaSeleccione('filtraTipFormOrig')) {
            var filtros = filtrosFormOrg();
            iniciaTablaFormFiltros(filtros);
        }        
        $("#modalFormularios").modal('show');
        $("#modalFormulariosDestino").modal('hide');
    });
    $('#modalFormularios').on('hidden.bs.modal', function (e) {

    });
    $('#modalNuevoFormulario').on('hidden.bs.modal', function (e) {
        //$("#divTipoForm").css("display", "none");
        //$("#cmbTipoCreaForm option:selected").removeAttr("selected");
        //$("#cmbTipoFormulario option:selected").removeAttr("selected");        
    });
    $('#modalFormulariosDestino').on('hidden.bs.modal', function (e) {
        
    });
    $('#modalNuevoFormulario').on('show.bs.modal', function (e) {
        $("#txtFiltro1").val("");
        $("#txtFiltro2").val("");
        $("#txtFiltro31").val("");
        //$(".tipoAgregaForm li").removeClass('active');
        //$(".tab-pane").removeClass('active');
        //$("#liIndivAgreForm").addClass('active');
        //$("#individual").addClass('active');
        $("#idTipoAgreForm").val("Individual");        
        $("#cmbAnoFormularios option:selected").removeAttr("selected");
        $("#cmbMinisterioForm option:selected").removeAttr("selected");
        $("#cmbServicioForm").empty();
        $("#cmbServicioForm").append("<option value='-1'>Seleccione</option>");
        $("#cmbGrupoForm option:selected").removeAttr("selected");
        $("#cmbAnoFormDestino option:selected").removeAttr("selected");
        $("#cmbPlantillaDestino option:selected").removeAttr("selected");
        $("#cmbGrupoFormDestino option:selected").removeAttr("selected");
        //$("#anoRestrucc").css("display", "none");
        //$("#divTipoForm").css("display", "none");
        $("#cmbAnoRestrucc option:selected").removeAttr("selected");        
        $("#divPlantRestDest").css("display", "block");
        $("#divMintFormRest").css("display", "none");
        $("#divServFormRest").css("display", "none");
        $("#divTipoFormRest").css("display", "none");
        $("#divNombreFormRest").css("display", "none");
        $("#cmbFormOrig").empty();
        $("#cmbFormOrig").append("<option value='-1'>Seleccione</option>");
        $("#cmbFormOrig").append("<option value='0'>Vacio</option>");
        $("#cmbAnoRestDestino option:selected").removeAttr("selected");
        $("#cmbGrupoRestDestino option:selected").removeAttr("selected");
        $("#cmbAnoOrig option:selected").removeAttr("selected");
        $("#cmbMinisterioOrig option:selected").removeAttr("selected");
        $("#cmbServicioOrig").empty();
        $("#cmbServicioOrig").append("<option value='-1'>Seleccione</option>");
        $("#cmbTipoFormulario option:selected").removeAttr("selected");
        $("#cmbGrupoOrig option:selected").removeAttr("selected");
        aAgregaFormOrig = [];
        aAgregaFormRest = [];
        aAgregaFormOrigTodos = [];
        $("#cmbTipoFormOrigDest option:selected").removeAttr("selected");
    });
    $('.tipoAgregaForm li a').click(function (e) {
        $("#idTipoAgreForm").val($(this).text());
    });
    $(".cmbMinisterio").change(function () {
        $(".cmbServicio").empty();
        $(".cmbServicio").append("<option value='-1'>Seleccione</option>");
        var idMinist = $(this).val();
        $.each(aMinsServ, function (i, item) {
            if (idMinist == item.IdParametro) {
                $.each(item.Servicios, function (i2, item2) {
                    $(".cmbServicio").append("<option value='" + item2.IdParametro + "'>" + item2.Descripcion + "</option>");
                });
            }
        });
    });    
    $('#modalFormularioVacio').on('show.bs.modal', function (e) {
        $("#cmbAnoFormVacio option:selected").removeAttr("selected");
        $("#cmbTipoFormVacio option:selected").removeAttr("selected");
        $("#cmbMinisterioFormVacio option:selected").removeAttr("selected");
        $("#cmbServicioFormVacio").empty();
        $("#cmbServicioFormVacio").append("<option value='-1'>Seleccione</option>");
        $("#cmbGrupoFormVacio option:selected").removeAttr("selected");
        $("#txtNombreFormVacio").val("");
    });
    $("#btnGuardaNuevoForm").click(function () {
        if (!validaSeleccione2('agregaFormVacio') && $("#txtNombreFormVacio").val() != "") {            
            $(this).button('loading');
            var datos = {};
            datos.Ano = $("#cmbAnoFormVacio").val();
            datos.IdTipoFormulario = $("#cmbTipoFormVacio").val();
            datos.Ministerio = $("#cmbMinisterioFormVacio").val();
            datos.Servicio = $("#cmbServicioFormVacio").val();
            datos.IdGrupoFormulario = $("#cmbGrupoFormVacio").val();
            datos.Nombre = $("#txtNombreFormVacio").val();
            var opciones = {
                url: ROOT + "Mantenedores/GuardaFormulario",
                data: { data: datos },
                type: "post",
                datatype: "json",
                success: function (result, xhr, status) {
                    $("#btnGuardaNuevoForm").button('reset')
                    if (result == "ok") {
                        $("#modalFormularioVacio").modal('hide');
                        iniciaTablaFormularios();                        
                        modalMsjFormularios("<p>Formulario creado exitosamente</p>");
                    } else {
                        modalMsjFormularios("<p>" + result + "</p>");
                        console.log(result);
                    }
                },
                error: function (xhr, status, error) {
                    modalMsjGrupos("<p>" + error + "</p>");
                    console.log(error);
                }
            };
            $.ajax(opciones);
        } else {
            modalMsjFormularios("<p>Seleccione campos obligatorios <b>(*)</b></p>");
        }
    });
    //$("#cmbTipoCreaForm").change(function () {
    //    if ($(this).val() == "3") {
    //        //$("#anoRestrucc").css("display", "block");
    //        //$("#divTipoForm").css("display", "none");
    //    } else if ($(this).val() == "1") {
    //        //$("#divTipoForm").css("display", "none");
    //        //$("#anoRestrucc").css("display", "none");
    //    } else if ($(this).val() == "2") {
    //        //$("#anoRestrucc").css("display", "block");
    //        //$("#divTipoForm").css("display", "block");
    //    }else {
    //        //$("#anoRestrucc").css("display", "none");
    //        //$("#divTipoForm").css("display", "none");
    //    }
    //});
    $("#btnAtrasFormRestrucc").click(function () {
        $("#modalFormularios").modal('hide');
        $("#modalNuevoFormulario").modal('show');
    });
    $("#btnGuardaFormRestrucc").click(function () {
        $("#cmbFormOrig").empty();
        $("#cmbFormOrig").append("<option value='-1'>Seleccione</option>");
        $("#cmbFormOrig").append("<option value='0'>Vacio</option>");
        if (aAgregaFormRest.length > 0) {
            $("#modalFormRestruc").modal('hide');
            $.each(aAgregaFormRest, function (i, item) {
                $("#cmbFormOrig").append("<option value='" + item.id + "'>" + item.nombre + "</option>");
            });
            $("#modalFormRestDestino").modal('show');
        } else {
            modalMsjFormularios("<p>Seleccione al menos un formulario</p>");
        }
    });
    $("#btnAtrasFormRestNuevo").click(function () {
        $("#modalFormRestDestino").modal('hide');
        $("#modalFormRestruc").modal('show');
    });
    $("#cmbFormOrig").change(function () {
        if ($(this).val() == "0") {            
            $("#divPlantRestDest").css("display", "none");
            $("#divMintFormRest").css("display", "block");
            $("#divServFormRest").css("display", "block");
            $("#divTipoFormRest").css("display", "block");
            $("#divNombreFormRest").css("display", "block");
        } else {
            $("#divPlantRestDest").css("display", "block");
            $("#divMintFormRest").css("display", "none");
            $("#divServFormRest").css("display", "none");
            $("#divTipoFormRest").css("display", "none");
            $("#divNombreFormRest").css("display", "none");
        }
    });
    $("#btnGuardaFormNuevo").click(function () {
        if (!validaSeleccione2('agregaFormExistente')) {
            $(this).button('loading');
            if (aAgregaFormOrig.length > 0) {
                var datos = [];
                $.each(aAgregaFormOrig, function (i, item) {
                    dato = {};
                    dato.IdPrograma = item;
                    dato.Ano = $("#cmbAnoFormDestino").val();
                    dato.IdGrupoFormulario = $("#cmbGrupoFormDestino").val();
                    dato.IdTipoFormulario = $("#cmbPlantillaDestino").val();
                    datos.push(dato);
                });                               
                var opciones = {
                    url: ROOT + "Mantenedores/GuardaFormulariosRestruc",
                    data: { data: datos },
                    type: "post",
                    datatype: "json",
                    success: function (result, xhr, status) {
                        $("#btnGuardaFormNuevo").button('reset')
                        if (result == "ok") {
                            $("#modalFormulariosDestino").modal('hide');
                            iniciaTablaFormularios();                        
                            modalMsjFormularios("<p>Formularios creados exitosamente</p>");
                        } else {
                            modalMsjFormularios("<p>" + result + "</p>");
                            console.log(result);
                        }
                    },
                    error: function (xhr, status, error) {
                        modalMsjFormularios("<p>" + error + "</p>");
                        console.log(error);
                    }
                };
                $.ajax(opciones);
            }
        } else {
            modalMsjFormularios("<p>Seleccione campos obligatorios <b>(*)</b></p>");
        }
    });
    $("#btnGuardaFormRestNuevo").click(function () {
        var claseValidar = "";
        var nombreValidar = true;
        if ($("#cmbFormOrig").val() == "0") {
            claseValidar = "restruc1";
            nombreValidar = ($("#txtNombreFormRest").val() == "" ? false : true);
        } else {
            claseValidar = "restruc2";
        }
        if (!validaSeleccione2(claseValidar) && nombreValidar) {
            $(this).button('loading');
            if (aAgregaFormRest.length > 0) {
                var datos = [];
                $.each(aAgregaFormRest, function (i, item) {
                    dato = {};
                    dato.IdPrograma = item.id;
                    datos.push(dato);
                });
                var datos2 = {};
                datos2.IdPlataforma = $("#cmbFormOrig").val();
                datos2.Ano = $("#cmbAnoRestDestino").val();
                datos2.IdGrupoFormulario = $("#cmbGrupoRestDestino").val();
                datos2.IdTipoFormulario = $("#cmbTipoFormRest").val();
                datos2.Ministerio = $("#cmbMinistFormRest").val();
                datos2.Servicio = $("#cmbServFormRest").val();
                datos2.Nombre = $("#txtNombreFormRest").val();
                datos2.IdExcepcion = $("#cmbPlantRestDestino").val();
                var opciones = {
                    url: ROOT + "Mantenedores/GuardaFormularioRest",
                    data: { data1: datos, data2: datos2 },
                    type: "post",
                    datatype: "json",
                    success: function (result, xhr, status) {
                        $("#btnGuardaFormRestNuevo").button('reset')
                        if (result == "ok") {
                            $("#modalFormRestDestino").modal('hide');
                            iniciaTablaFormularios();
                            modalMsjFormularios("<p>Formulario creado exitosamente</p>");
                        } else {
                            modalMsjFormularios("<p>" + result + "</p>");
                            console.log(result);
                        }
                    },
                    error: function (xhr, status, error) {
                        modalMsjFormularios("<p>" + error + "</p>");
                        console.log(error);
                    }
                };
                $.ajax(opciones);
            }
        } else {
            modalMsjFormularios("<p>Seleccione campos obligatorios <b>(*)</b></p>");
        }
    });
    $("#btnElimGenral").click(function () {
        borraFormulario();
    });
    $("#cmbTipoFormOrigDest").change(function () {
        $("#cmbPlantillaDestino").empty();
        $("#cmbPlantillaDestino").append("<option value='-1'>Seleccione</option>");        
        if (aPlantillasDestino.length > 0) {
            var origen = $("#cmbTipoFormulario").val();
            var destino = $(this).val();
            $.each(aPlantillasDestino, function (i, item) {
                if (destino == item.TipoFormularioDestino && origen == item.TipoFormularioOrigen) {
                    $("#cmbPlantillaDestino").append("<option value='" + item.IdPlantillaTraspaso + "'>" + item.Nombre + "</option>");
                }
            });
        }
    });
    $('#modalIteraciones').on('show.bs.modal', function (e) {
        aAgregaIteracion = [];
    });
    $("#btnGuardaIteracion").click(function () {
        if (aAgregaIteracion.length > 0) {
            modalMsjConfirm("Agregar programas a iterar", "<p>¿Está seguro que desea iterar " + aAgregaIteracion.length + " programas?</p>", "");
        } else {
            modalMsjFormularios("<p>Seleccione al menos un formulario a iterar</p>");
        }       
    });
    $("#btnMsjConfirm").click(function () {
        $(this).button('loading');
        var datos = [];
        $.each(aAgregaIteracion, function (i, item) {
            var dato = {};
            dato.IdPrograma = item;
            datos.push(dato);
        });
        var opciones = {
            url: ROOT + "Mantenedores/GuardaIteracionesFormularios",
            data: { data: datos },
            type: "post",
            datatype: "json",
            success: function (result, xhr, status) {
                $("#btnMsjConfirm").button('reset')
                if (result == "ok") {
                    $("#modalMsjConfirm").modal('hide');
                    $("#modalIteraciones").modal('hide');
                    iniciaTablaFormularios();
                    modalMsjFormularios("<p>Iteraciones creadas exitosamente</p>");
                } else {
                    modalMsjFormularios("<p>" + result + "</p>");
                    console.log(result);
                }
            },
            error: function (xhr, status, error) {
                modalMsjFormularios("<p>" + error + "</p>");
                console.log(error);
            }
        };
        $.ajax(opciones);
    });
    $("#btnCrearFormJson").click(function () {
        if ($(this).val() != "-1") {
            $(this).button('loading');
            var opciones = {
                url: ROOT + "Mantenedores/CreaArchivoJson",
                data: { tipo: $("#cmbTipoFormJson option:selected").val() },
                type: "post",
                datatype: "json",
                success: function (result, xhr, status) {
                    $("#btnCrearFormJson").button('reset')
                    if (result == "ok") {
                        $("#modalCreaJsonForm").modal('hide');
                        modalMsjFormularios("<p>Archivo JSON creado exitosamente</p>");
                    } else {
                        modalMsjFormularios("<p>" + result + "</p>");
                        console.log(result);
                    }
                },
                error: function (xhr, status, error) {
                    modalMsjGrupos("<p>" + error + "</p>");
                    console.log(error);
                }
            };
            $.ajax(opciones);
        } else {
            modalMsjFormularios("<p>Seleccione un tipo de formulario</p>");
        }
    });
});
function iniciaTablaFormularios() {
    var tablaFormularios = $('#tblFormularios').DataTable();
    tablaFormularios.destroy();
    $('#tblFormularios').dataTable({
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetFormulariosMantenedor",
            "dataSrc": "",
            "type": "POST"
        },
        "columns": [
            { "data": 'IdBips', "width": "5%" },
            { "data": 'Ano', "width": "5%" },
            { "data": 'Nombre', "width": "20%" },
            { "data": 'Tipo', "width": "15%" },
            { "data": 'Ministerio', "width": "20%" },
            { "data": 'Servicio', "width": "20%" },            
            {
                "data": 'TotalUsuarios', "orderable": false, "width": "5%",
                "render": function (data, type, full, meta) {
                    return '<button type="button" class="btn btn-primary btn-xs ' + (data == 0 ? "count0" : "") + '" data-toggle="tooltip" data-placement="left" title="CLick para ver los usuarios que tienen asignado este formulario" onclick="modelUsuarios(' + full.IdPrograma + ');"><span class="badge" style="font-size:11px;">' + data + '</span></button>';
                }
            },
            {
                "data": 'TotalGrupos', "orderable": false, "width": "5%",
                "render": function (data, type, full, meta) {
                    return '<button type="button" class="btn btn-primary btn-xs ' + (data == 0 ? "count0" : "") + '" data-toggle="tooltip" data-placement="left" title="Click para ver los grupos que tienen asignado este formulario" onclick="modelGrupos(' + full.IdPrograma + ');"><span class="badge" style="font-size:11px;">' + data + '</span></button>';
                }
            },
            {
                "data": null, "orderable": false, "width": "5%",
                "render": function (data, type, full, meta) {
                    var titulo = "Eliminar formulario";
                    var mensaje = "<p>¿Está seguro de eliminar el formulario seleccionado?</p>";
                    return '<button class="btn btn-danger btn-xs" type="button" data-toggle="tooltip" data-placement="left" title="Eliminar formulario" onclick="modalEliminarGnral(\'' + titulo + '\',\'' + mensaje + '\',\'formulario\',\'' + full.IdPrograma + '\');"><i class="fa fa-times"></i></button>';
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            var btnCreaForm = '<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>';
            var btnIteracionForm = '';
            var btnCreaJsonForm = '';
            if (creaFormularios) {
                btnCreaForm = '<button type="button" class="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="Crear un nuevo formulario" onclick="modalNuevoFormulario();"><i class="fa fa-plus" aria-hidden="true"></i></button>';
                btnIteracionForm = '<button class="btn btn-danger" type="button" data-toggle="tooltip" data-placement="bottom" title="Crear nueva iteración" onclick="modalCrearIteracion();"><i class="fa fa-refresh"></i></button>';
                btnCreaJsonForm = '<button class="btn btn-info" type="button" data-toggle="tooltip" data-placement="bottom" title="Crear nuevo archivo de formulario (JSON)" onclick="modalCreaJsonForm();"><i class="fa fa-file-code-o"></i></button>';
            }            
            $('#tblFormularios_spFiltro').append(btnCreaForm);
            $('#tblFormularios_spFiltro').append(btnIteracionForm);
            $('#tblFormularios_spFiltro').append(btnCreaJsonForm);
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
}
function modalEliminarGnral(titulo, mensaje, tipo, id) {
    $('#tituloEliminarGnral').empty();
    $('#tituloEliminarGnral').text(titulo);
    $('#bodyEliminarGnral').empty();
    $('#bodyEliminarGnral').html(mensaje);
    $("#txtIdEliminar").val(id);
    $("#txtTipoEliminar").val(tipo);
    $('#modalEliminarGeneral').modal('show');
}
function modalNuevoFormulario() {
    $("#modalNuevoFormulario").modal("show");
}
function modalMsjFormularios(mensaje) {
    $('#msjInfoForm').empty();
    $('#msjInfoForm').html(mensaje);
    $('#modalMsjForm').modal('show');
}
function iniciaTablaFormFiltros(filtros) {
    var tablaFormFiltros = $('#tblFormulariosInd').DataTable();
    tablaFormFiltros.destroy();
    var tablaFormOrig = $('#tblFormulariosInd').dataTable({
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetFormFiltrosMantenedor",
            "dataSrc": "",
            "type": "POST",
            "data": { "filtros": filtros }
        },
        "columns": [
            { "data": 'IdBips', "width": "5%" },
            { "data": 'Ano', "width": "5%" },
            { "data": 'Nombre', "width": "30%" },
            { "data": 'Tipo', "width": "15%" },
            { "data": 'Ministerio', "width": "20%" },
            { "data": 'Servicio', "width": "20%" },
            {
                "data": null, "width": "5%", "orderable": false,
                "render": function (data, type, full, meta) {
                    var check = ""
                    agregaFormularioOrigTodos(full.IdPrograma.toString());
                    if (aAgregaFormOrig.indexOf(full.IdPrograma.toString()) >= 0) {
                        var check = "checked";
                    }
                    return '<input type="checkbox" data-toggle="tooltip" data-placement="left" class="cbxFormOrigen" title="Seleccionar formulario" style="cursor:pointer;" onclick="agregaFormularioOrig(this);" value="' + full.IdPrograma + '" ' + check + ' id="cbxFO' + full.IdPrograma + '" />';
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {            
            $('#tblFormulariosInd_spFiltro').append('<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>');
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) {
            $("[data-toggle=tooltip]").tooltip({ html: true });
            //if ($('#cbxSelecTodosFormOrg').prop('checked')) {                
            //    if (aAgregaFormOrigTodos.length > 0) {
            //        $('.cbxFormOrigen').prop("checked", true);
            //    }
            //} else {
            //    if (aAgregaFormOrig.length == 0) {
            //        $('.cbxFormOrigen').prop("checked", false);
            //    }
            //}
            if (aAgregaFormOrig.length > 0) {
                $.each(aAgregaFormOrig, function (i, item) {
                    $('#cbxFO' + parseInt(item)).prop("checked", true);
                });
            }            
        }
    });
}
function agregaFormularioOrig(obj) {
    if (obj.checked) {
        if (aAgregaFormOrig.indexOf(obj.value) < 0) {
            aAgregaFormOrig.push(obj.value);
        }
    } else {
        if (aAgregaFormOrig.indexOf(obj.value) >= 0) {
            aAgregaFormOrig.splice(aAgregaFormOrig.indexOf(obj.value), 1);
        }
        if ($('#cbxSelecTodosFormOrg').prop('checked')) {
            $('#cbxSelecTodosFormOrg').prop("checked", false);
        }
    }
}
function validaSeleccione(clase) {
    var validacion = 0;
    $("." + clase).each(function (i, item) {
        if ($(this).val() != "-1") {
            validacion = 1;
            return false;
        }
    });
    return validacion;
}
function validaSeleccione2(clase) {
    var validacion = 0;
    $("." + clase).each(function (i, item) {
        if ($(this).val() == "-1") {
            validacion = 1;
            return false;
        }
    });
    return validacion;
}
function iniciaTablaFormRestrucc() {
    var tablaFormRestrucc = $('#tblFormRestrucc').DataTable();
    tablaFormRestrucc.destroy();
    $('#tblFormRestrucc').dataTable({
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetFormRestruccMantenedor",
            "dataSrc": "",
            "type": "POST",
            "data": { "filtro": $("#cmbAnoRestrucc").val() }
        },
        "columns": [
            { "data": 'IdBips', "width": "5%" },
            { "data": 'Ano', "width": "5%" },
            { "data": 'Nombre', "width": "30%" },
            { "data": 'Tipo', "width": "15%" },
            { "data": 'Ministerio', "width": "20%" },
            { "data": 'Servicio', "width": "20%" },
            {
                "data": null, "width": "5%", "orderable": false,
                "render": function (data, type, full, meta) {
                    return '<input type="checkbox" data-toggle="tooltip" data-placement="left" title="Seleccionar formulario" style="cursor:pointer;" onclick="agregaFormRestruc(' + full.IdPrograma + ',\'' + full.Nombre + '\', this);" value="' + full.IdPrograma + '" />';
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            $('#tblFormRestrucc_spFiltro').append('<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>');
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
}
function agregaFormRestruc(id, nombre, obj) {
    if (obj.checked) {
        if (arrayObjectIndexOf(aAgregaFormRest,id,"id") < 0) {
            var dato = {};
            dato.id = id;
            dato.nombre = nombre;
            aAgregaFormRest.push(dato);
        }
    } else {
        if (arrayObjectIndexOf(aAgregaFormRest,id,"id") >= 0) {
            aAgregaFormRest.splice(arrayObjectIndexOf(aAgregaFormRest,id,"id"), 1);
        }
    }
}
function arrayObjectIndexOf(myArray, searchTerm, property) {
    for (var i = 0, len = myArray.length; i < len; i++) {
        if (myArray[i][property] === searchTerm) return i;
    }
    return -1;
}
function borraFormulario() {
    $("#btnElimGenral").button('loading');
    var datos = {};
    datos.IdPrograma = $("#txtIdEliminar").val();
    var opciones = {
        url: ROOT + "Mantenedores/EliminarFormulario",
        data: { data: datos },
        type: "post",
        datatype: "json",
        success: function (result, xhr, status) {
            $("#btnElimGenral").button('reset')
            if (result == "ok") {
                $("#modalEliminarGeneral").modal('hide');
                iniciaTablaFormularios();
                modalMsjFormularios("<p>Formulario eliminado exitosamente</p>");
            } else {
                modalMsjFormularios("<p>" + result + "</p>");
                console.log(result);
            }
        },
        error: function (xhr, status, error) {
            modalMsjGrupos("<p>" + error + "</p>");
            console.log(error);
        }
    };
    $.ajax(opciones);
}
function modelUsuarios(id) {
    var tablaUsuarios = $('#tblUsuarios').DataTable();
    tablaUsuarios.destroy();
    dato = {};
    dato.IdPrograma = id;
    $('#tblUsuarios').dataTable({
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetUserGruposFormularios",
            "dataSrc": "",
            "type": "POST",
            "data": { "data": dato, "tipo": "usuarios" }
        },
        "columns": [
            { "data": 'IdUser', "width": "25%" },
            { "data": 'Nombre', "width": "25%" },
            { "data": 'Ministerio', "width": "25%" },
            { "data": 'Servicio', "width": "25%" }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            $('#tblUsuarios_spFiltro').append('<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>');
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
    $("#modalUsuarios").modal('show');
}
function modelGrupos(id) {
    var tablaGrupos = $('#tblGrupos').DataTable();
    tablaGrupos.destroy();
    dato = {};
    dato.IdPrograma = id;
    $('#tblGrupos').dataTable({
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetUserGruposFormularios",
            "dataSrc": "",
            "type": "POST",
            "data": { "data": dato, "tipo": "grupos" }
        },
        "columns": [
            { "data": 'NombreGrupo', "width": "50%" },
            { "data": 'DescGrupoFormulario', "width": "50%" }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            $('#tblGrupos_spFiltro').append('<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>');
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
    $("#modalGrupos").modal('show');
}
function selecTodosFormOrg(obj) {
    aAgregaFormOrig = [];
    if (obj.checked) {
        if (aAgregaFormOrigTodos.length > 0) {
            aAgregaFormOrig = aAgregaFormOrigTodos;
            $('.cbxFormOrigen').prop("checked", true);
        }
    } else {
        $('.cbxFormOrigen').prop("checked", false);
    }
}
function agregaFormularioOrigTodos(id) {   
    if (aAgregaFormOrigTodos.indexOf(id) < 0) {
        aAgregaFormOrigTodos.push(id);
    }
}
function filtrosFormOrg() {
    var filtros = {};
    if ($("#cmbAnoOrig").val() != "-1") {
        filtros.Ano = $("#cmbAnoOrig").val();
    }
    if ($("#cmbMinisterioOrig").val() != "-1") {
        filtros.IdMinisterio = $("#cmbMinisterioOrig").val();
    }
    if ($("#cmbServicioOrig").val() != "-1") {
        filtros.IdServicio = $("#cmbServicioOrig").val();
    }
    if ($("#cmbGrupoOrig").val() != "-1") {
        filtros.IdGrupoFormulario = $("#cmbGrupoOrig").val();
    }
    filtros.TipoFormulario = $("#cmbTipoFormulario").val();
    return filtros;
}
function modalCrearIteracion() {
    inciaTablaIteraciones();
    $('#modalIteraciones').modal('show');
}
function inciaTablaIteraciones() {
    var tablaIteraciones = $('#tblIteraciones').DataTable();
    tablaIteraciones.destroy();
    $('#tblIteraciones').dataTable({
        "autoWidth": false,
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetFormulariosIteraciones",
            "dataSrc": "",
            "type": "POST"
        },
        "columns": [
            { "data": 'IdBips', "width": "5%" },
            { "data": 'Ano', "width": "5%" },
            { "data": 'Ministerio', "width": "20%" },
            { "data": 'Servicio', "width": "20%" },
            { "data": 'Tipo', "width": "10%" },
            { "data": 'Nombre', "width": "37%" },
            {
                "data": null, "width": "3%", "orderable": false,
                "render": function (data, type, full, meta) {
                    return '<input type="checkbox" data-toggle="tooltip" data-placement="left" title="Seleccionar para iterar" style="cursor:pointer;" onclick="agregaIteracion(this);" value="' + full.IdPrograma + '" />';
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            $('#tblIteraciones_spFiltro').append('<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>');
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
}
function agregaIteracion(obj) {
    if (obj.checked) {
        if (aAgregaIteracion.indexOf(obj.value) < 0) {
            aAgregaIteracion.push(obj.value);
        }
    } else {
        if (aAgregaIteracion.indexOf(obj.value) >= 0) {
            aAgregaIteracion.splice(aAgregaIteracion.indexOf(obj.value), 1);
        }
    }
}
function modalMsjConfirm(titulo, mensaje, tipoMsj) {
    $('#tituloMsjConfirm').empty();
    $('#tituloMsjConfirm').text(titulo);
    $('#bodyMsjConfirm').html(mensaje);
    $('#txtTipoMsj').val(tipoMsj);
    $('#modalMsjConfirm').modal('show');
}
function modalCreaJsonForm() {
    $('#modalCreaJsonForm').modal('show');
}