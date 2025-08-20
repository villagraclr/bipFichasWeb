$(document).ready(function () {
    var listaFormuladores;
    var cantidadFormuladores = '';
    var cantidadPermiso = '';
    //$('#popOverEtapas').popover();
    $('ul.nav a').each(function () { if ($(this).hasClass('active')) { $(this).removeClass('active'); } });
    $("#liGores").addClass('active');
    $('#aMenuPerfiles').addClass('active');
    $("#menuGores").addClass('in');
    //Tabla programas
    tablaProgramas(null);
    //Filtro todos los años
    $('#todosAnosFiltro').change(function () {
        //$('#filtroAnos').collapse('show');
        if (this.checked) {
            $('.anosFiltro').prop("checked", true);
        } else {
            $('.anosFiltro').prop("checked", false);
        }
        aplicaFiltros();
    });
    //Filtro años
    $('.anosFiltro').change(function () {
        if (!this.checked) {
            $('#todosAnosFiltro').prop("checked", false);
        } else {
            var todos = 1;
            $('.anosFiltro').each(function () {
                if (!this.checked) {
                    todos = 0;
                }
            });
            if (todos == 1) {
                $('#todosAnosFiltro').prop("checked", true);
            }
        }
        aplicaFiltros();
    });
    //Filtro todos los gores
    $('#todosGoresFiltro').change(function () {
        if (this.checked) {
            $('.goresFiltro').prop("checked", true);
        } else {
            $('.goresFiltro').prop("checked", false);
        }
        aplicaFiltros();
    });    
    //Aplicar filtros
    function aplicaFiltros() {
        tablaProgramas({ filtroAnos: JSON.stringify($('input[name="anosFiltro"]').serializeObject()), filtroGores: JSON.stringify($('input[class="goresFiltro"]').serializeObject()) });
    }
    $.fn.serializeObject = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };
    $('#modalNuevoPerfil').on('show.bs.modal', function (e) {
        $("#cmbGores option:selected").removeAttr("selected");
        $("#txtNombrePerfil").val("");
    });
    $("#btnCreaNuevoPerfil").click(function () {
        if ($.trim($("#txtNombrePerfil").val()) != "" && $("#cmbGores option:selected").val() != "-1") {
            $(this).button('loading');
            var opciones = {
                url: ROOT + "Gores/GuardaNuevoPerfil",
                data: { nombre: $.trim($("#txtNombrePerfil").val()), gore: $("#cmbGores option:selected").val() },
                type: "post",
                datatype: "json",
                beforeSend: function () { $("#cargaPagina").fadeIn(); },
                success: function (result, xhr, status) {
                    $("#btnCreaNuevoPerfil").button('reset')
                    if (result[0] == "ok") {
                        $("#modalNuevoPerfil").modal('hide');
                        //tablaProgramas(-1);
                        modalMsjGore("<p>Perfil creado exitosamente</p>");
                        window.location.href = ROOT + "Formulario/Index" + "?_id=" + encodeURIComponent(result[1]);
                    } else {
                        modalMsjGore("<p>" + result + "</p>");
                        console.log(result[1]);
                    }
                },
                error: function (xhr, status, error) {
                    modalMsjGore("<p>" + error + "</p>");
                    console.log(error);
                }
            };
            $.ajax(opciones);
        } else {
            modalMsjGore("<p>Ingrese nombre perfil</p>");
        }
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
    $('#modalNuevoUsers').on('hidden.bs.modal', function (e) {
        limpiaModelUsers();
    });
    $("#btnGuardaUser").click(function () {
        if (!validaDatosUsuarios()) {
            //$(this).button('loading');
            var datos = {};
            datos.IdUser = $("#txtIdUser").val();
            datos.Email = $("#txtEmail").val();
            datos.Nombre = $("#txtNombre").val();
            datos.Ministerio = $("#cmbMinisterio option:selected").val();
            datos.Servicio = $("#cmbServicio option:selected").val();
            datos.Perfil = $("#cmbPerfil option:selected").val();
            datos.IdGore = idGoreUsuario;
            datos.IdPerfilGore = 7; //Perfil Formulador
            console.log(datos);
            var opciones = {
                url: ROOT + "Mantenedores/GuardaUsuario",
                data: { data: datos },
                type: "post",
                datatype: "json",
                success: function (result, xhr, status) {
                    $("#btnGuardaUser").button('reset')
                    if (result == "ok") {
                        $("#modalNuevoUsers").modal('hide');
                        modalMsjUsers("<p>Usuario creado exitosamente</p>");
                        modalPermisos2();
                        obtenerFormuladoresGore();
                    } else {
                        modalMsjUsers("<p>" + result + "</p>");
                        console.log(result);
                    }
                },
                error: function (xhr, status, error) {
                    modalMsjUsers("<p>" + error + "</p>");
                    console.log(error);
                }
            };
            $.ajax(opciones);
        } else {
            modalMsjUsers("<p>Ingrese campos obligatorios <b>(*)</b></p>");
        }
    });
    $("#btnGuardaPermisos").click(function () {
        if (true) {
            modalMsjPermisos("Confirmar permisos", "<p>¿Está seguro de asignar permisos a este usuario?</p>");
        } else {
            modalMsjPermisos("Falta información", "<p>Seleccione al menos un permiso</p>");
        }
    });
});
function modalMsjPermisos(idFormulario) {
    $('#txtIdFormuladores').val($('#cmbFormuladores' + idFormulario).val());
    $('#txtIdPermiso').val($('#cmbPermiso' + idFormulario).val());
    $('#txtIdPrograma').val(idFormulario);
    $('#bodyMsjAsignarPermisos').empty();
    var titulo = 'Confirmar selección';
    cantidadFormuladores = $('#cmbFormuladores' + idFormulario).val() == null ? 0 : $('#cmbFormuladores' + idFormulario).val().length;
    cantidadPermiso = $('#cmbPermiso' + idFormulario).val(); 
    var mensaje = cantidadFormuladores > 1 ? '<p>¿Asignar permiso a los formuladores seleccionados?</p>' : '<p>¿Asignar permiso al formulador seleccionado?</p>';
    if (cantidadPermiso == -1) { mensaje = 'Selecciona un permiso'; titulo = 'Error'; }
    if (cantidadFormuladores == 0) { mensaje = 'Selecciona al menos un evaluador'; titulo = 'Error'; }
    $('#tituloMsjAsignarPermisos').text(titulo);
    $('#bodyMsjAsignarPermisos').html(mensaje);
    $('#modalMsjAsignarPermisos').modal('show');
}

function asignarFormuladores() {
    if (cantidadPermiso > 0 && cantidadFormuladores > 0) {
        $("#btnMsjConfirm").button('loading');
        var idFormuladores = $('#txtIdFormuladores').val().split(',');
        var idPermiso = $('#txtIdPermiso').val();
        var idPrograma = $('#txtIdPrograma').val();
        var opciones = {
            url: ROOT + "Gores/GuardaFormuladores",
            data: { idFormuladores: idFormuladores, idPermiso: idPermiso, idPrograma: idPrograma },
            type: "post",
            datatype: "json",
            success: function (result, xhr, status) {
                $("#btnMsjConfirm").button('reset')
                $("#modalPermisos2").modal('hide');
                $('#modalMsjAsignarPermisos').modal('hide');
                if (result == "ok") {
                    //iniciaTablaPermisos(usuario);
                    //iniciaTablaUsuarios();
                    modalMsjUsers("<p>Permiso asignado exitosamente</p>");
                    modalPermisos2();
                    $('#btnModalAsignarFormuladores').prop('disabled', false);;
                } else {
                    modalMsjUsers("<p>" + result + "</p>");
                    console.log(result);
                }
            },
            error: function (xhr, status, error) {
                modalMsjUsers("<p>" + error + "</p>");
                console.log(error);
            },
            complete: function () {  }
        };
        $.ajax(opciones);
    } else {
        $('#modalMsjAsignarPermisos').modal('hide');
    }
}

function tablaProgramas(filtros) {
    if (filtros != null) {
        var tablaProgramas = $('#tblProgramas').DataTable();
        tablaProgramas.destroy();
    }
    $('#tblProgramas').dataTable({
        "processing": true,
        "ajax": {
            "url": MyApp.rootPath + "Gores/GetPerfilesGores",
            "dataSrc": "",
            "data": filtros,
            "type": "POST"
        },
        "columns": [
            { "data": 'IdBips' },
            { "data": 'Ano' },
            { "data": 'Gore' },
            {
                "data": 'Tipo',
                "render": function (data, type, full, meta) {
                    var tipo = full.Tipo;
                    if (full.IdTipoFormulario == 336) {
                        tipo = "Social";
                    } else if (full.IdTipoFormulario == 337) {
                        tipo = "No social";
                    }
                    return tipo;
                }
            },
            {
                "data": 'Nombre',
                "render": function (data, type, full, meta) {
                    if (full.Acceso > 0) {
                        var link = ROOT + "Formulario/Index" + "?_id=" + encodeURIComponent(full.IdEncriptado);
                        var title = "Acceso de sólo lectura";
                        var color = "d9534f";
                        for (var i = 0; i < arrayFT.length; i++) {
                            if (full.IdPrograma == arrayFT[i]) {
                                title = "Programa en uso";
                                color = "00DC21";
                                break;
                            }
                        }
                        return '<a href="' + link + '" onclick="muestraLoad();" data-toggle="tooltip" title="' + title + '" style="color:#' + color + '">' + data + '</a>';
                    } else {
                        var link = ROOT + "Formulario/Index" + "?_id=" + encodeURIComponent(full.IdEncriptado);
                        return '<a href="' + link + '" onclick="muestraLoad();" data-toggle="tooltip" title="Click para abrir formulario">' + data + '</a>';
                    }
                }
            },
            {
                "data": 'EtapaDesc', "orderable": false,
                "render": function (data, type, full, meta) {
                    return '<label data-toggle="tooltip" title="En esta etapa..." style="font-weight: normal">' + data + '</label>';
                }
            },
            {
                "data": null, "orderable": false, "width": "2%",
                "render": function (data, type, full, meta) {
                    if (anosFichas.indexOf(full.Ano) >= 0) {
                        var seguimiento = "#";
                        var detalle = "#";
                        $.each(tiposProgramas, function (i, item) {
                            if (item.Id == full.IdTipoFormulario) {
                                seguimiento = item.rutaSeguimiento + full.IdPrograma + '/' + full.Ano + "/" + (item.IdPadre == "302" ? "" : item.IdPadre);
                                detalle = item.rutaDetalle + full.IdPrograma + '/' + full.Ano + "/" + (item.IdPadre == "302" ? "" : item.IdPadre);
                            }
                        });
                        fichas = "";
                        if (permisoInfoEval == "True") {
                            fichas = '<a href="' + seguimiento + '" data-toggle="tooltip" data-placement="left" title="Informe de seguimiento" target="_blank"><i class="fa fa-file-pdf-o" aria-hidden="true"></i></a>';
                        }
                        fichas += '<a href="' + detalle + '" data-toggle="tooltip" data-placement="left" title="Informe de detalle" target="_blank" style="margin-left: 7px;"><i class="fa fa-file-pdf-o" aria-hidden="true"></i></a>';
                    } else {
                        fichas = '<span class="glyphicon glyphicon-remove" aria-hidden="true" data-toggle="tooltip" data-placement="left" title="Sin informe"></span>';
                    }
                    return fichas;
                }
            },
            {
                "data": null, "orderable": false, "width": "2%", "visible": (permisoLibera === "True"),
                "render": function (data, type, full, meta) {
                    var botar = '';
                    if (permisoLibera == "True") {
                        if (full.Tomado > 0) {
                            botar = '<button id="btnLiberaPrograma" class="btn btn-primary btn-xs" type="button" data-toggle="tooltip" data-placement="left" title="Liberar programa" onclick="liberaPrograma(' + full.IdPrograma + ');"><i class="fa fa-sign-out"></i></button>';
                        }
                    }
                    return botar;
                }
            },
            {
                "data": null, "width": "5%", "orderable": false,
                "render": function (data, type, full, meta) {
                    var claseCierre = ((full.IdEtapa == etapaPerfilCalificado) ? "" : "disabled");
                    var accion = ''
                    accion += '<button id="btnConvertirPerfil" class="btn btn-primary btn-xs" ' + claseCierre + ' type="button" data-toggle="tooltip" data-placement="left" title="Crear programa" onclick="modalCrearPrograma(' + full.IdPrograma + ',' + full.IdBips + ');"><i class="fa fa-sign-out"></i></button>';
                    return accion;
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            $('#tblProgramas_spFiltro').append('<button type="button" class="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="Crear nuevo perfil" onclick="modalCreaNuevoPerfil();" style="margin-left: 5px; border-top-left-radius: 4px !important; border-bottom-left-radius: 4px !important"><i class="fa fa-plus" aria-hidden="true"></i></button>');
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) {
            $("[data-toggle=tooltip]").tooltip({ html: true });
        }
    });
}

function muestraLoad() {
    $("#cargaPagina").fadeIn();
}

function liberaPrograma(id) {
    $(document).ready(function () {
        var opciones = {
            url: ROOT + "Formulario/Post",
            data: { _id: id },
            type: "post",
            datatype: "json",
            beforeSend: function () { $("#cargaPagina").fadeIn(); },
            success: function (result, xhr, status) {
                if (result == "ok") {
                    $("#btnLiberaPrograma").prop('disabled', true);
                    $("#txtMensajesFormulario").empty();
                    $("#txtMensajesFormulario").html("<p>Programa liberado con éxito.</p>");
                    $('#modalMensajesFormularios').modal('show');
                }
            },
            error: function (xhr, status, error) {
                console.log(error);
            },
            complete: function () { $("#cargaPagina").fadeOut(); }
        };
        $.ajax(opciones);
        /*
        debugger;        
        //Boton botar programa
        var notificacion = $.connection.notificacionesHub;
        $.connection.hub.start().done(function () {
            notificacion.server.notificacion(id);
        });*/
    });
}

function modalCreaNuevoPerfil() {
    $('#modalNuevoPerfil').modal('show');
}

function modalMsjGore(mensaje) {
    $('#msjInfoGore').empty();
    $('#msjInfoGore').html(mensaje);
    $('#modalMsjGore').modal('show');
}

function modalCrearPrograma(idPrograma, idBips) {
    $("#txtIdProgramaCrear").val(idPrograma);
    $("#txtIdBipsCrear").val(idBips);
    mensaje = "<p>Se procederá con la creación del Programa GORE:</p>";
    mensaje += "<p>En la siguiente ventana se podrá <b>crear formuladores y/o asignarlos al Programa.</b></p>";
    mensaje += "<p>¿Crear programa? <b>Esta acción no podrá ser revertida.</b></p>";
    $('#txtModalCrearPrograma').empty();
    $('#txtModalCrearPrograma').html(mensaje);
    $('#modalCrearPrograma').modal('show');
}

function crearPrograma() {
    var idPrograma = $("#txtIdProgramaCrear").val();
    var idBips = $("#txtIdBipsCrear").val();
    //$(document).ready(function () {
    //    var opciones = {
    //        url: ROOT + "Gores/CrearPrograma",
    //        data: { idPrograma: idPrograma, idBips: idBips },
    //        type: "post",
    //        datatype: "json",
    //        beforeSend: function () { $("#cargaPagina").fadeIn(); },
    //        success: function (result, xhr, status) {
    //            if (result == "ok") {
    //                $('#modalCrearPrograma').modal('hide');
    //                $("#txtModalAsignarFormuladores").empty();
    //                mensaje = "<p><b>Se ha creado el Programa GORE con éxito, éste estará en el submenú Programas, menú GORES </b>.</p>";
    //                mensaje += "<p>A continuación, puedes crear o asignar formuladores para el Programa, la asignación solo puede ser realizada en esta sección:</p>";
    //                $("#txtModalAsignarFormuladores").html(mensaje);
    //                $('#modalAsignarFormuladores').modal('show');
    //                modalPermisos2();
    //                obtenerFormuladoresGore();
    //            }
    //        },
    //        error: function (xhr, status, error) {
    //            console.log(error);
    //        },
    //        complete: function () { $("#cargaPagina").fadeOut(); }
    //    };
    //    $.ajax(opciones);
    //});
    $('#modalCrearPrograma').modal('hide');
    $("#txtModalAsignarFormuladores").empty();
    mensaje = "<p><b>Se ha creado el Programa GORE con éxito, éste estará en el submenú Programas, menú GORES </b>.</p>";
    mensaje += "<p>A continuación, puedes crear o asignar formuladores para el Programa, la asignación solo puede ser realizada en esta sección:</p>";
    $("#txtModalAsignarFormuladores").html(mensaje);
    $('#modalAsignarFormuladores').modal('show');
    modalPermisos2();
    obtenerFormuladoresGore();
}

function modalNuevoUsers() {
    $("#modalNuevoUsers").modal('show');
}

function validaDatosUsuarios() {
    var arrayCampOblig = ["#txtEmail", "#txtNombre", "#cmbMinisterio", "#cmbServicio", "#cmbPerfil"];
    var validacion = false;
    for (x = 0; x < arrayCampOblig.length; x++) {
        if ($($.trim(arrayCampOblig[x])).val() == "" || $(arrayCampOblig[x]).val() == "-1") {
            validacion = true;
            break;
        }
    }
    return validacion;
}

function modalMsjUsers(mensaje) {
    $('#msjInfoUsers').empty();
    $('#msjInfoUsers').html(mensaje);
    $('#modalMsjUsers').modal('show');
}

function limpiaModelUsers() {
    $("#txtIdUser").val("");
    $("#txtEmail").prop("readonly", false);
    $("#txtEmail").val("");
    $("#txtNombre").val("");
    $("#cmbMinisterio option:selected").removeAttr("selected");
    $("#cmbServicio").empty();
    $("#cmbServicio").append("<option value='-1'>Seleccione</option>");
    $("#cmbPerfil option:selected").removeAttr("selected");
    //$("#cmbEstados option:selected").removeAttr("selected");
}

function modalPermisos2() {
    var tablaPermisosUsers2 = $('#tblPermisosUsers2').DataTable();
    tablaPermisosUsers2.destroy();
    var countTabla = 0;
    $('#tblPermisosUsers2').dataTable({
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetFormulariosUser",
            "dataSrc": "",
            "type": "POST",
            "data": { "id": idUsuario, "tipoFormulario": 348 }
        },
        "columns": [
            { "data": 'Ano', "width": "5%" },
            { "data": 'Ministerio' },
            { "data": 'Servicio' },
            { "data": 'Nombre' },
            { "data": 'Tipo' },
            {
                "data": null, "orderable": false, "width": "15%",
                "render": function (data, type, full, meta) {
                    var select = '<select class="form-control input-sm cmbPP" id="cmbFormuladores' + full.IdPrograma + '" multiple="multiple" style="width: 100%;">';
                    //select += '<option value="-1">Seleccione</option>';
                    if(listaFormuladores != null){
                        for (var i = 0; i < listaFormuladores.length; i++) {
                            select += '<option value="' + listaFormuladores[i].Id + '">' + listaFormuladores[i].Nombre + '</option>';
                        }
                    } else {
                        select += '<option value="-1" disabled>No hay formuladores</option>';
                    }
                    select += '</select>'
                    $('#cmbFormuladores' + full.IdPrograma).select2({
                        placeholder: "Selecciona",
                        maximumSelectionLength: 10, // Limitar a 10 selecciones
                        language: {
                            maximumSelected: function (e) {
                                return "Solo puedes seleccionar " + e.maximum + " opciones"; // Personaliza el mensaje
                            }
                        }
                    });
                    return select;
                }
            },
            {
                "data": null, "orderable": false, "width": "15%",
                "render": function (data, type, full, meta) {
                    var select = '<select class="form-control input-sm cmbPP" id="cmbPermiso' + full.IdPrograma + '">';
                    select += '<option value="-1">Seleccione</option>';
                    $.each(aPlantillas, function (i, item) {
                        if (item.TipoFormulario == full.IdTipoFormulario) {
                            //if (perfil != 1 && full.IdTipoFormulario == 342) {
                            //    if (item.Nombre.split('_')[1] == full.IdPrograma) {
                            //        select += '<option value="' + item.IdExcepcionPlantilla + '">' + item.Nombre + '</option>';
                            //    }
                            //} else {
                            if (item.IdExcepcionPlantilla == 4483) {
                                select += '<option value="' + item.IdExcepcionPlantilla + '">' + item.Nombre + '</option>';
                            }
                            //}
                        }
                    });
                    select += '</select>'
                    return select;
                }
            },
            {
                "data": null, "orderable": false,
                "render": function (data, type, full, meta) {
                    var acciones = '<button type="button" class="btn btn-primary" onclick="modalMsjPermisos(' + full.IdPrograma + ')">Asignar</button>';
                    return acciones;
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            if (json.length == 0) {
                bloqueaPermisosGral(1);
            }
            $('#tblPermisosUsers2_spFiltro').append('<button class="btn btn-primary" type="button" onclick="modalNuevoUsers();"><i class="fa fa-plus"></i></button>');
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
    $("#cmbGrupoPermisos").empty();
    $("#cmbGrupoPermisos").append('<option value="-1">Seleccione</option>');
    var opciones = {
        url: ROOT + "Mantenedores/GetGruposUsers",
        data: { id: idUsuario },
        type: "post",
        datatype: "json",
        success: function (result, xhr, status) {
            var datos = $.parseJSON(result);
            $.each(datos, function (i, item) {
                $("#cmbGrupoPermisos").append('<option value="' + item.IdGrupoFormulario + '">' + item.Nombre + '</option>');
            });
        },
        error: function (xhr, status, error) {
            modalMsjUsers("<p>" + error + "</p>");
            console.log(error);
        }
    };
    $.ajax(opciones);
    //$('#modalPermisos2').modal('show');
}

function obtenerFormuladoresGore() {
    var opciones = {
        url: ROOT + "Mantenedores/GetUsuarios",
        data: { idPerfil: 7, idGore: idGoreUsuario },
        type: "post",
        datatype: "json",
        success: function (result, xhr, status) {
            listaFormuladores = JSON.parse(result);
        },
        error: function (xhr, status, error) {
            modalMsjUsers("<p>" + error + "</p>");
            console.log(error);
        }
    };
    $.ajax(opciones);
}