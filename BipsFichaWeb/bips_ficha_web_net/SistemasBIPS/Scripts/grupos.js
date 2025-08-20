$(document).ready(function () {
    $("#menuMantenedores").addClass('in');
    $("#aMenuGrupos").dropdown('toggle');
    iniciaTablaGrupos();
    $('#modalNuevosGrupos').on('hidden.bs.modal', function (e) {
        limpiaModelGrupos();
    });
    $("#cmbMinisterioFormGrupMasivo").change(function () {
        $("#cmbServicioFormGrupMasivo").empty();
        $("#cmbServicioFormGrupMasivo").append("<option value='-1'>Seleccione</option>");
        var idMinist = $(this).val();
        $.each(aMinsServ, function (i, item) {
            if (idMinist == item.IdParametro) {
                $.each(item.Servicios, function (i2, item2) {
                    $("#cmbServicioFormGrupMasivo").append("<option value='" + item2.IdParametro + "'>" + item2.Descripcion + "</option>");
                });
            }
        });
    });
    $("#cmbMinisterioElimGrupo").change(function () {
        $("#cmbServicioElimGrupo").empty();
        $("#cmbServicioElimGrupo").append("<option value='-1'>Seleccione</option>");
        var idMinist = $(this).val();
        $.each(aMinsServ, function (i, item) {
            if (idMinist == item.IdParametro) {
                $.each(item.Servicios, function (i2, item2) {
                    $("#cmbServicioElimGrupo").append("<option value='" + item2.IdParametro + "'>" + item2.Descripcion + "</option>");
                });
            }
        });
    });
    $("#btnGuardaGrupos").click(function () {
        var countTG = 0;        
        $(".cmbTG").each(function (i, item) {
            if ($(this).val().split('/')[0] != "-1") {
                countTG++;
            }
        });
        if (countTG != 0) {
            modalMsjConfirm("Asignar usuarios", "<p>¿Está seguro de asignar " + countTG + " usuarios a este grupo?</p>", "usersGrupos");
        } else {
            modalMsjGrupos("<p>Seleccione al menos un tipo de grupo</p>");
        }
    });
    $("#btnGuardaNuevosForm").click(function () {
        if ($("#idTipoAgreForm").val() == "Individual") {
            if (aAgregaForm.length > 0) {
                modalMsjConfirm("Agregar formularios", "<p>¿Está seguro de agregar " + aAgregaForm.length + " formularios a este grupo?</p>", "agregaForm");
            } else {
                modalMsjGrupos("<p>Seleccione al menos un formulario</p>");
            }
        } else {
            if (validaSeleccione('agregaFormGrupMasivo')) {
                agregaFormGrupoMasivo();
            } else {
                modalMsjGrupos("<p>Seleccione al menos una categoría</p>");
            }
        }
    });
    $("[data-toggle=tooltip]").tooltip({ html: true });
    $("#btnGuardaNuevosGrupos").click(function () {
        var datosNulos = ["#txtNombre", "#txtDescripcion"];
        if (!validaDatosNulos(datosNulos)) {
            $(this).button('loading');
            var datos = {};
            datos.IdGrupoFormulario = $("#txtIdGrupo").val();
            datos.Nombre = $("#txtNombre").val();
            datos.Descripcion = $("#txtDescripcion").val();
            var opciones = {
                url: ROOT + "Mantenedores/NuevoGrupo",
                data: { data: datos },
                type: "post",
                datatype: "json",
                success: function (result, xhr, status) {
                    $("#btnGuardaNuevosGrupos").button('reset')
                    if (result == "ok") {
                        $("#modalNuevosGrupos").modal('hide');
                        iniciaTablaGrupos();
                        modalMsjGrupos("<p>Grupo " + ($("#txtNombre").val() == "" ? "creado" : "modificado") + " exitosamente</p>");
                    } else {
                        modalMsjGrupos("<p>" + result + "</p>");
                        console.log(result);
                    }
                },
                error: function (xhr, status, error) {
                    //redireccionar a página de error
                    modalMsjGrupos("<p>" + error + "</p>");
                    console.log(error);
                }
            };
            $.ajax(opciones);
        } else {
            modalMsjGrupos("<p>Ingrese campos obligatorios <b>(*)</b></p>");
        }
    });
    $("#btnElimFormIndiv").click(function () {
        var tipo = $("#txtTipoEliminar").val();
        if (tipo == "grupo") {
            borraGrupo();
        }else if (tipo == "usuario"){
            borraUsuario();
        }else if (tipo == "formulario"){
            borraFormulario();
        }
    });
    $('#modalEliminar').on('hidden.bs.modal', function (e) {
        $("#txtIdEliminar").val("");        
        $("#txtTipoEliminar").val("");
    });
    $('#modalUserGrupos').on('hidden.bs.modal', function (e) {
        $("#txtGrupoModel").val("");
        $("#txtNombreModel").val("");        
    });
    $('#modalMsjConfirm').on('hidden.bs.modal', function (e) {
        $("#txtTipoMsj").val("");
    });
    $('#modalFormGrupos').on('hidden.bs.modal', function (e) {
        $("#txtGrupoFormModel").val("");
        $("#txtNombreFormModel").val("");
    });
    $('#modalElimFormGrupos').on('hidden.bs.modal', function (e) {
        $("#cmbAnoElimGrupo option:selected").removeAttr("selected");
        $("#cmbMinisterioElimGrupo option:selected").removeAttr("selected");
        $("#cmbServicioElimGrupo").empty();
        $("#cmbServicioElimGrupo").append("<option value='-1'>Seleccione</option>");        
        $("#cmbTipoElimGrupo option:selected").removeAttr("selected");
    });
    $('#modalNuevosForm').on('hidden.bs.modal', function (e) {
        aAgregaForm = [];
        $("#idTipoAgreForm").val("");
    });
    $('#modalNuevosForm').on('show.bs.modal', function (e) {
        $(".tipoAgregaForm li").removeClass('active');
        $(".tab-pane").removeClass('active');
        $("#liIndivAgreForm").addClass('active');
        $("#individual").addClass('active');
        $("#idTipoAgreForm").val("Individual");
    });
    $("#btnMsjConfirm").click(function () {
        var tipo = $("#txtTipoMsj").val();
        if (tipo == "usersGrupos") {
            asignaUserGrupo();
        } else if (tipo == "agregaForm") {
            agregraFormulariosGrupo();
        }
    });
    $("#btnElimFormGrup").click(function () {
        if (validaSeleccione('elimFormGrup')) {
            grupo = $("#txtGrupoFormModel").val();
            $(this).button('loading');
            var datos = {};
            if ($("#cmbMinisterioElimGrupo option:selected").val() != "-1") {
                datos.IdMinisterio = $("#cmbMinisterioElimGrupo option:selected").val();
            }
            if ($("#cmbServicioElimGrupo option:selected").val() != "-1") {
                datos.IdServicio = $("#cmbServicioElimGrupo option:selected").val();
            }
            if ($("#cmbAnoElimGrupo option:selected").val() != "-1") {
                datos.Ano = $("#cmbAnoElimGrupo option:selected").val();
            }
            if ($("#cmbTipoElimGrupo option:selected").val() != "-1") {
                datos.TipoFormulario = $("#cmbTipoElimGrupo option:selected").val();
            }            
            var opciones = {
                url: ROOT + "Mantenedores/EliminaFormGrupoMasivo",
                data: { data: datos, idGrupo: grupo },
                type: "post",
                datatype: "json",
                success: function (result, xhr, status) {
                    $("#btnElimFormGrup").button('reset')
                    if (result == "ok") {                        
                        iniciaTablaGrupos();
                        $("#modalElimFormGrupos").modal('hide');
                        iniciaTablaFormGrupos(grupo);
                        modalMsjGrupos("<p>Formularios eliminados exitosamente de este grupo</p>");
                    } else {
                        modalMsjGrupos("<p>" + result + "</p>");
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
            modalMsjGrupos("<p>Seleccione al menos una categoría</p>");
        }
    });
    $('.tipoAgregaForm li a').click(function (e) {
        $("#idTipoAgreForm").val($(this).text());
    });
});
function iniciaTablaGrupos() {
    var tablaGruposForm = $('#tblGruposFormularios').DataTable();
    tablaGruposForm.destroy();
    $('#tblGruposFormularios').dataTable({
        "autoWidth": false,
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetGruposFormularios",
            "dataSrc": "",
            "type": "POST"
        },
        "columns": [
            {
                "data": 'Nombre', "width": "40%",
                "render": function (data, type, full, meta) {
                    return '<a href="#" data-toggle="tooltip" data-placement="right" title="Cilck para editar grupo" id="nomGrupo' + full.IdGrupoFormulario + '" onclick="modalNuevosGrupos(' + full.IdGrupoFormulario + ',\'' + full.Nombre + '\',\'' + full.Descripcion + '\')">' + full.Nombre + '</a>';
                }
            },
            { "data": 'Descripcion', "width": "45%" },
            {
                "data": null, "width": "5%", "orderable": false,
                "render": function (data, type, full, meta) {
                    return '<button type="button" class="btn btn-primary btn-xs ' + (full.Usuarios == 0 ? "count0" : "") + '" data-toggle="tooltip" data-placement="left" title="Ver los usuarios pertenecientes a este grupo" onclick="modalUsers(' + full.IdGrupoFormulario + ',\'' + full.Nombre + '\');"><span class="badge" style="font-size:11px;">' + full.Usuarios + '</span></button>';
                }
            },
            {
                "data": null, "width": "5%", "className": "dt-center", "orderable": false,
                "render": function (data, type, full, meta) {
                    return '<button type="button" class="btn btn-primary btn-xs ' + (full.Formularios == 0 ? "count0" : "") + '" data-toggle="tooltip" data-placement="left" title="Ver formularios pertenecientes a este grupo" onclick="modalFormGrupos(' + full.IdGrupoFormulario + ',\'' + full.Nombre + '\');"><span class="badge" style="font-size:11px;">' + full.Formularios + '</span></button>';
                }
            },
            {
                "data": null, "width": "5%", "className": "dt-center", "orderable": false,
                "render": function (data, type, full, meta) {
                    return '<button type="button" class="btn btn-danger btn-xs" data-toggle="tooltip" data-placement="left" title="Eliminar grupo" onclick="eliminarGrupo(' + full.IdGrupoFormulario + ');"><i class="fa fa-times" aria-hidden="true"></i></button>';
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            var btnCreaGrupo = '<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>';
            if (creaGrupos) {
                btnCreaGrupo = '<button type="button" class="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="Crear un nuevo grupo de formularios" onclick="modalNuevosGrupos(0,\'\',\'\');"><i class="fa fa-plus" aria-hidden="true"></i></button>';
            }
            $('#tblGruposFormularios_spFiltro').append(btnCreaGrupo);
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
}
function modalFormGrupos(id, nombre) {
    $("#txtGrupoFormModel").val(id);
    $("#txtNombreFormModel").val(nombre);
    iniciaTablaFormGrupos(id);
    $('#modalTitleFormGrup').text(nombre);
    $('#modalFormGrupos').modal('show');
}
function iniciaTablaFormGrupos(id) {
    var tablaFormGrupos = $('#tblFormulariosGrupos').DataTable();
    tablaFormGrupos.destroy();
    $('#tblFormulariosGrupos').dataTable({
        "autoWidth": false,
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetFormulariosGrupos",
            "dataSrc": "",
            "type": "POST",
            "data": { "id": id }
        },
        "columns": [
            { "data": 'Ano', "width": "5%" },
            { "data": 'Nombre', "width": "25%" },
            { "data": 'TipoFormulario', "width": "15%" },
            { "data": 'Ministerio', "width": "25%" },
            { "data": 'Servicio', "width": "25%" },
            {
                "data": null, "width": "5%", "orderable": false,
                "render": function (data, type, full, meta) {
                    return '<button type="button" class="btn btn-danger btn-xs" data-toggle="tooltip" data-placement="left" title="Eliminar formulario" onclick="eliminarFormIndiv(' + full.IdFormulario + ');"><i class="fa fa-times" aria-hidden="true"></i></button>';
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            var btnAsignaFormularios = '<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>';
            if (asignaFormularios) {
                btnAsignaFormularios = '<button type="button" class="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="Asignar un nuevo formulario al grupo" onclick="modalNuevosFormularios(' + id + ');"><i class="fa fa-plus" aria-hidden="true"></i></button>';
            }
            $('#tblFormulariosGrupos_spFiltro').append(btnAsignaFormularios);
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
}
function modalUsers(grupo, nombre) {
    $("#txtGrupoModel").val(grupo);
    $("#txtNombreModel").val(nombre);
    iniciaTablaUserGroup(grupo,nombre);
    $('#modalTitleUserGroup').text(nombre);
    $('#modalUserGrupos').modal('show');
}
function iniciaTablaUserGroup(grupo,nombre) {
    var tablaUsersGroup = $('#tblUserGroup').DataTable();
    tablaUsersGroup.destroy();
    $('#tblUserGroup').dataTable({
        "autoWidth": false,
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetUsuariosGrupos",
            "dataSrc": "",
            "type": "POST",
            "data": { "idGrupo": grupo }
        },
        "columns": [
            { "data": 'UserName', "width": "10%" },
            { "data": 'Nombre', "width": "30%" },
            { "data": 'Ministerio', "width": "25%" },
            { "data": 'Servicio', "width": "25%" },
            { "data": 'TipoGrupo', "width": "5%" },
            {
                "data": null, "width": "5%", "className": "dt-center", "orderable": false,
                "render": function (data, type, full, meta) {
                    return '<button type="button" class="btn btn-danger btn-xs" data-toggle="tooltip" data-placement="left" title="Eliminar usuario del grupo" onclick="eliminarUsuario(\'' + full.Id + '\');"><i class="fa fa-times" aria-hidden="true"></i></button>';
                }
            },
            { "data": 'EmailConfirmed', "visible": false },
            { "data": 'Estado', "visible": false }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            var btnAsigUserGroup = '<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>';
            if (asignaUsuarios) {
                btnAsigUserGroup = '<button type="button" class="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="Asignar nuevos usuarios" onclick="modalNuevosUsersGroup(' + grupo + ',\'' + nombre + '\');"><i class="fa fa-plus" aria-hidden="true"></i></button>';
            }
            $('#tblUserGroup_spFiltro').append(btnAsigUserGroup);
            this.css('width', '100%');
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
}
function modalNuevosUsersGroup(grupo, nombre) {
    var tablaFormUsers = $('#tblUsuarios').DataTable();
    tablaFormUsers.destroy();
    $('#tblUsuarios').dataTable({
        "autoWidth": false,
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetUsuariosGruposAsignados",
            "dataSrc": "",
            "type": "POST",
            "data": { "idGrupo": grupo }
        },
        "columns": [
            { "data": 'UserName', "width": "10%" },
            { "data": 'Nombre', "width": "30%" },
            { "data": 'Ministerio', "width": "25%" },
            { "data": 'Servicio', "width": "25%" },
            {
                "data": null, "width": "10%", "orderable": false,
                "render": function (data, type, full, meta) {
                    //var select = $('<select/>', {
                    //    'id': 'cmbTipoGrupo' + grupo,
                    //    'class': 'form-control'
                    //});
                    //var opciones = ["Seleccione", "Propios", "Otros"];
                    //var valores = ["-1", "1", "2"];
                    //for (var i = 0, x = opciones.length; i < x; i++){
                    //    select[0][i] = new Option(opciones[i], valores[i]);
                    //}
                    var select = '<select class="form-control input-sm cmbTG">';
                    select += '<option value="-1/0">Seleccione</option>';
                    $.each(aTiposGrupos, function (i, item) {
                        select += '<option value="' + item.IdParametro + '/' + full.Id + '">' + item.Descripcion + '</option>';
                    });
                    select += '</select>'
                    return select;
                }

            },
            { "data": 'Perfil', "visible": false },
            { "data": 'EmailConfirmed', "visible": false },
            { "data": 'Estado', "visible": false }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            $('#tblUsuarios_spFiltro').append('<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>');
            this.css('width', '100%');
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
    $('#modalTitleFormUsers').text(nombre);
    $('#modalFormUsers').modal('show');
}
function modalNuevosGrupos(id, nombre, descripcion) {
    if (creaGrupos) {
        limpiaModelGrupos();
        if (id != 0) {
            $("#txtIdGrupo").val(id);
            $("#txtNombre").val(nombre);
            $("#txtDescripcion").val(descripcion);
        }
        $('#modalTitleNuevosGrupos').text((id == 0 ? "Nuevo" : "Editar") + " Grupo");
        $('#modalNuevosGrupos').modal('show');
    } else {
        modalMsjGrupos("<p>Sin permiso para editar grupo</p>");
    }
}
function modalMsjGrupos(mensaje) {
    $('#msjInfoGrupos').empty();
    $('#msjInfoGrupos').html(mensaje);
    $('#modalMsjGrupos').modal('show');
}
function limpiaModelGrupos() {
    $("#txtIdGrupo").val("");
    $("#txtNombre").val("");
    $("#txtDescripcion").val("");
}
function modalNuevosFormularios(id) {
    iniciaTablaNuevoForm();
    $('#modalNuevosForm').modal('show');
}
function iniciaTablaNuevoForm() {
    grupo = $("#txtGrupoFormModel").val();
    var tablaNuevosForm = $('#tblNuevosForm').DataTable();
    tablaNuevosForm.destroy();
    $('#tblNuevosForm').dataTable({
        "autoWidth": false,
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetFormularios",
            "dataSrc": "",
            "type": "POST",
            "data": { "idGrupo": grupo }
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
                    return '<input type="checkbox" data-toggle="tooltip" data-placement="left" title="Seleccionar para asignar" style="cursor:pointer;" onclick="agregaFormulario(this);" value="' + full.IdPrograma + '" />';
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            $('#tblNuevosForm_spFiltro').append('<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>');
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
}
function selecTipoNuevosForm(tipo) {
    if (tipo == 1) {
        $("#divTblNuevosForm").css("display", "block");
        $("#divFormSelecc").css("display", "block");
        $("#divFormGrupales").css("display", "none");
        iniciaTablaNuevoForm();
    } else if (tipo == 2) {
        $("#divTblNuevosForm").css("display", "none");
        $("#divFormSelecc").css("display", "none");
        $("#divFormGrupales").css("display", "block");
    }
}
function eliminarFormGrupos() {
    $('#modalElimFormGrupos').modal('show');
}
function eliminarFormIndiv(id) {
    $('#tituloEliminar').empty();
    $('#tituloEliminar').text("Eliminar formulario");
    $('#bodyEliminar').empty();
    $('#bodyEliminar').html("<p>¿Está seguro de querer eliminar este formulario de este grupo?</p>");
    $("#txtIdEliminar").val(id);
    $("#txtTipoEliminar").val("formulario");
    $('#modalEliminar').modal('show');
}
function eliminarGrupo(id) {
    $('#tituloEliminar').empty();
    $('#tituloEliminar').text("Eliminar grupo");
    $('#bodyEliminar').empty();
    $('#bodyEliminar').html("<p>¿Está seguro de querer eliminar este grupo?</p>");
    $("#txtIdEliminar").val(id);
    $("#txtTipoEliminar").val("grupo");
    $('#modalEliminar').modal('show');
}
function eliminarUsuario(id) {
    $('#tituloEliminar').empty();
    $('#tituloEliminar').text("Eliminar usuario");
    $('#bodyEliminar').empty();
    $('#bodyEliminar').html("<p>¿Está seguro de querer eliminar este usuario?</p>");
    $("#txtIdEliminar").val(id);
    $("#txtTipoEliminar").val("usuario");
    $('#modalEliminar').modal('show');
}
function modalMsjConfirm(titulo, mensaje, tipoMsj) {
    $('#tituloMsjConfirm').empty();
    $('#tituloMsjConfirm').text(titulo);
    $('#bodyMsjConfirm').html(mensaje);
    $('#txtTipoMsj').val(tipoMsj);
    $('#modalMsjConfirm').modal('show');
}
function validaDatosNulos(datos) {    
    var validacion = false;
    $.each(datos, function (i, item) {
        if ($($.trim(item)).val() == "" || $(item).val() == "-1") {
            validacion = true;
            return false;
        }
    });    
    return validacion;
}
function borraGrupo() {
    $("#btnElimFormIndiv").button('loading');
    var datos = {};
    datos.IdGrupoFormulario = $("#txtIdEliminar").val();
    var opciones = {
        url: ROOT + "Mantenedores/EliminaGrupo",
        data: { data: datos },
        type: "post",
        datatype: "json",
        success: function (result, xhr, status) {
            $("#btnElimFormIndiv").button('reset')
            if (result == "ok") {
                $("#modalEliminar").modal('hide');
                iniciaTablaGrupos();
                modalMsjGrupos("<p>Grupo eliminado exitosamente</p>");
            } else {
                modalMsjGrupos("<p>" + result + "</p>");
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
function borraUsuario() {
    grupo = $("#txtGrupoModel").val();
    nombre = $("#txtNombreModel").val();
    $("#btnElimFormIndiv").button('loading');
    var datos = {};
    datos.IdGrupoFormulario = grupo;
    datos.IdUsuario = $("#txtIdEliminar").val();
    var opciones = {
        url: ROOT + "Mantenedores/EliminaUsuario",
        data: { data: datos },
        type: "post",
        datatype: "json",
        success: function (result, xhr, status) {
            $("#btnElimFormIndiv").button('reset')
            if (result == "ok") {
                iniciaTablaGrupos();
                $("#modalEliminar").modal('hide');
                iniciaTablaUserGroup(grupo, nombre);
                modalMsjGrupos("<p>Usuario eliminado exitosamente de este grupo</p>");
            } else {
                modalMsjGrupos("<p>" + result + "</p>");
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
function asignaUserGrupo() {
    grupo = $("#txtGrupoModel").val();
    nombre = $("#txtNombreModel").val();
    $("#btnMsjConfirm").button('loading');
    var datos = [];
    $(".cmbTG").each(function (i, item) {
        if ($(this).val().split('/')[0] != "-1") {
            dato = {};
            dato.IdUsuario = $(this).val().split('/')[1];
            dato.IdGrupoFormulario = $("#txtGrupoModel").val();
            dato.TipoGrupo = $(this).val().split('/')[0];
            datos.push(dato);
        }
    });    
    var opciones = {
        url: ROOT + "Mantenedores/GuardaUsuariosGrupos",
        data: { data: datos },
        type: "post",
        datatype: "json",
        success: function (result, xhr, status) {
            $("#btnMsjConfirm").button('reset')
            if (result == "ok") {
                $("#modalMsjConfirm").modal('hide');
                $("#modalFormUsers").modal('hide');
                iniciaTablaGrupos();
                iniciaTablaUserGroup(grupo, nombre);
                modalMsjGrupos("<p>Usuario asignado exitosamente a este grupo</p>");
            } else {
                modalMsjGrupos("<p>" + result + "</p>");
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
function borraFormulario() {
    grupo = $("#txtGrupoFormModel").val();
    $("#btnElimFormIndiv").button('loading');
    var datos = {};
    datos.IdGrupoFormulario = grupo;
    datos.IdFormulario = $("#txtIdEliminar").val();
    var opciones = {
        url: ROOT + "Mantenedores/EliminaFormularioGrupo",
        data: { data: datos },
        type: "post",
        datatype: "json",
        success: function (result, xhr, status) {
            $("#btnElimFormIndiv").button('reset')
            if (result == "ok") {
                iniciaTablaGrupos();
                $("#modalEliminar").modal('hide');
                iniciaTablaFormGrupos(grupo);
                modalMsjGrupos("<p>Formulario eliminado exitosamente de este grupo</p>");
            } else {
                modalMsjGrupos("<p>" + result + "</p>");
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
function agregaFormulario(obj) {
    if (obj.checked) {
        if (aAgregaForm.indexOf(obj.value) < 0) {
            aAgregaForm.push(obj.value);
        }
    } else {
        if (aAgregaForm.indexOf(obj.value) >= 0) {
            aAgregaForm.splice(aAgregaForm.indexOf(obj.value),1);
        }
    }
}
function agregraFormulariosGrupo() {
    grupo = $("#txtGrupoFormModel").val();
    $("#btnMsjConfirm").button('loading');
    var datos = [];
    $.each(aAgregaForm, function (i, item) {
        var dato = {};
        dato.IdGrupoFormulario = grupo;
        dato.IdFormulario = item;
        datos.push(dato);
    });
    var opciones = {
        url: ROOT + "Mantenedores/GuardaFormulariosGrupo",
        data: { data: datos },
        type: "post",
        datatype: "json",
        success: function (result, xhr, status) {
            $("#btnMsjConfirm").button('reset')
            if (result == "ok") {
                $("#modalMsjConfirm").modal('hide');
                $("#modalNuevosForm").modal('hide');
                iniciaTablaGrupos();
                iniciaTablaFormGrupos(grupo);
                modalMsjGrupos("<p>Formularios asignados exitosamente a este grupo</p>");
            } else {
                modalMsjGrupos("<p>" + result + "</p>");
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
function agregaFormGrupoMasivo() {
    grupo = $("#txtGrupoFormModel").val();
    $("#btnGuardaNuevosForm").button('loading');
    var datos = {};
    if ($("#cmbMinisterioFormGrupMasivo option:selected").val() != "-1") {
        datos.IdMinisterio = $("#cmbMinisterioFormGrupMasivo option:selected").val();
    }
    if ($("#cmbServicioFormGrupMasivo option:selected").val() != "-1") {
        datos.IdServicio = $("#cmbServicioFormGrupMasivo option:selected").val();
    }
    if ($("#cmbAnoFormGrupMasivo option:selected").val() != "-1") {
        datos.Ano = $("#cmbAnoFormGrupMasivo option:selected").val();
    }
    if ($("#cmbTipoFormGrupMasivo option:selected").val() != "-1") {
        datos.TipoFormulario = $("#cmbTipoFormGrupMasivo option:selected").val();
    }
    var opciones = {
        url: ROOT + "Mantenedores/GuardaFormulariosGrupoMasivo",
        data: { data: datos, idGrupo: grupo },
        type: "post",
        datatype: "json",
        success: function (result, xhr, status) {
            $("#btnGuardaNuevosForm").button('reset')
            if (result == "ok") {               
                $("#modalNuevosForm").modal('hide');
                iniciaTablaGrupos();
                iniciaTablaFormGrupos(grupo);
                modalMsjGrupos("<p>Formularios agregados exitosamente a este grupo</p>");
            } else {
                modalMsjGrupos("<p>" + result + "</p>");
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