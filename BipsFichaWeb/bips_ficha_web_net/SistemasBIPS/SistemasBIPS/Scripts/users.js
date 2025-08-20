function modalNuevoUsers(id, user, nombre, ministerio, servicio, perfil, estado) {
    if (creaUsers) {
        limpiaModelUsers();
        if (id != 0) {            
            $("#txtIdUser").val(id);
            $("#txtEmail").prop("readonly", true);
            $("#txtEmail").val(user);
            $("#txtNombre").val(nombre);
            $("#cmbMinisterio option[value=" + ministerio + "]").prop("selected", true);
            $("#cmbMinisterio").val(ministerio).trigger('change');
            $("#cmbServicio option[value=" + servicio + "]").prop("selected", true);
            $("#cmbPerfil option[value=" + perfil + "]").prop("selected", true);
            $("#cmbEstados option[value=" + estado + "]").prop("selected", true);
        }
        $("#modalTitleNuevoUser").text((id == 0 ? "Crear" : "Editar") + " Usuario");
        $("#modalNuevoUsers").modal('show');
    } else {
        modalMsjUsers("<p>Sin permiso para editar usuario</p>");
    }
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
    $("#cmbEstados option:selected").removeAttr("selected");
}
function modalPerfiles(id,nombre) {
    var tablaPermisos = $('#tblPermisos').DataTable();
    tablaPermisos.destroy();
    $('#tblPermisos').dataTable({
        "autoWidth": false,
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetPermisos",
            "dataSrc": "",
            "type": "POST",
            "data": { "id" : id }
        },
        "columns": [
                { "data": 'Permiso', "width": "30%" },
                { "data": 'DescripcionPermiso', "width": "70%" }
            ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            $('#tblPermisos_spFiltro').append('<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>');
            this.css('width', '100%');
        },
        "drawCallback": function(settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
    $('#modalTitlePerm').text(nombre);
    $('#modalPerfiles').modal('show');
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
function modalGrupos(usuario) {
    iniciaTablaGrupos(usuario);
    $("#txtUserModel").val(usuario);
    $('#modalGrupos').modal('show');
}
function iniciaTablaGrupos(usuario) {
    var tablaGrupos = $('#tblGrupos').DataTable();
    tablaGrupos.destroy();
    $('#tblGrupos').dataTable({
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetGruposUsers",
            "dataSrc": "",
            "type": "POST",
            "data": { "id": usuario }
        },
        "columns": [
            { "data": 'Nombre', "width": "40%" },
            { "data": 'Descripcion', "width": "40%" },
            { "data": 'TipoGrupo', "width": "19%" },
            {
                "data": null, "width": "1%", "orderable": false,
                "render": function (data, type, full, meta) {
                    var titulo = "Eliminar grupo";
                    var mensaje = "<p>¿Está seguro de eliminar este grupo para el usuario seleccionado?</p>";
                    return '<button class="btn btn-danger btn-xs" type="button" data-toggle="tooltip" data-placement="left" title="Eliminar grupo asignado" onclick="modalEliminarGnral(\'' + titulo + '\',\'' + mensaje + '\',\'grupo\',' + full.IdGrupoFormulario + ');"><i class="fa fa-times"></i></button>';
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            var btnAsignaGrupoUsers = '<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>';
            if (asignaGrupoUsers) {
                btnAsignaGrupoUsers = '<button type="button" class="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="Asignar un nuevo grupo" onclick="modalGrupos2(\'' + usuario + '\');"><i class="fa fa-plus" aria-hidden="true"></i></button>';
            }
            $('#tblGrupos_spFiltro').append(btnAsignaGrupoUsers);
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
}
function modalGrupos2(usuario) {
    var tablaGrupos2 = $('#tblGrupos2').DataTable();
    tablaGrupos2.destroy();
    $('#tblGrupos2').dataTable({
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetGruposFormularios",
            "dataSrc": "",
            "type": "POST",
            "data": { "id": usuario }
        },
        "columns": [
            { "data": 'Nombre', "width": "49%" },
            { "data": 'Descripcion', "width": "50%" },
            {
                "data": null, "width": "1%", "orderable": false,
                "render": function (data, type, full, meta) {
                    var select = '<select class="form-control input-sm cmbTG">';
                    select += '<option value="-1/0">Seleccione</option>';
                    $.each(aTiposGrupos, function (i, item) {
                        select += '<option value="' + item.IdParametro + '/' + full.IdGrupoFormulario + '">' + item.Descripcion + '</option>';
                    });
                    select += '</select>'
                    return select;
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {                        
            $('#tblGrupos2_spFiltro').append('<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>');
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
    $('#modalGrupos2').modal('show');
}
function modalPermisos(usuario) {
    iniciaTablaPermisos(usuario);
    $("#txtUserPermisosModel").val(usuario);
    $('#modalPermisos').modal('show');
}
function iniciaTablaPermisos(usuario) {
    var tablaPermisosUsers = $('#tblPermisosUsers').DataTable();
    tablaPermisosUsers.destroy();
    $('#tblPermisosUsers').dataTable({
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetPermisosUser",
            "dataSrc": "",
            "type": "POST",
            "data": { "id": usuario }
        },
        "columns": [
            { "data": 'Ano', "width": "5%" },
            { "data": 'Ministerio', "width": "20%" },
            { "data": 'Servicio', "width": "20%" },
            { "data": 'Nombre', "width": "20%" },
            { "data": 'TipoFormulario.Descripcion', "width": "20%" },
            { "data": 'TipoExcepcion', "width": "10%" },
            {
                "data": null, "width": "5%", "orderable": false,
                "render": function (data, type, full, meta) {
                    var titulo = "Eliminar permiso";
                    var mensaje = "<p>¿Está seguro de eliminar este permiso para el usuario seleccionado?</p>";
                    var tipo = "permisos";
                    return '<button class="btn btn-danger btn-xs" type="button" data-toggle="tooltip" data-placement="left" title="Eliminar permiso asignado" onclick="modalEliminarGnral(\'' + titulo + '\',\'' + mensaje + '\',\'' + tipo + '\',' + full.IdPrograma + ')"><i class="fa fa-times"></i></button>';
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {            
            var btnAsignaPermisosUsers = '<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>';
            if (asignaPermisosUsers) {
                btnAsignaPermisosUsers = '<button type="button" class="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="Asignar permiso" onclick="modalPermisos2(\'' + usuario + '\');"><i class="fa fa-plus" aria-hidden="true"></i></button>';
            }
            $('#tblPermisosUsers_spFiltro').append(btnAsignaPermisosUsers);
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
}
function modalPermisos2(usuario) {
    var tablaPermisosUsers2 = $('#tblPermisosUsers2').DataTable();
    tablaPermisosUsers2.destroy();
    var countTabla = 0;
    $('#tblPermisosUsers2').dataTable({
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetFormulariosUser",
            "dataSrc": "",
            "type": "POST",
            "data": { "id": usuario }
        },
        "columns": [
            { "data": 'Ano', "width": "5%" },
            { "data": 'Ministerio', "width": "20%" },
            { "data": 'Servicio', "width": "20%" },
            { "data": 'Nombre', "width": "30%" },
            { "data": 'Tipo', "width": "20%" },
            {
                "data": null, "width": "5%", "orderable": false,
                "render": function (data, type, full, meta) {
                    var select = '<select class="form-control input-sm cmbPP">';
                    select += '<option value="-1/0">Seleccione</option>';
                    $.each(aPlantillas, function (i, item) {
                        if (item.TipoFormulario == full.IdTipoFormulario) {
                            if (perfil != 1 && full.IdTipoFormulario == 342) {
                                if (item.Nombre.split('_')[1] == full.IdPrograma) {
                                    select += '<option value="' + item.IdExcepcionPlantilla + '/' + full.IdPrograma + '">' + item.Nombre + '</option>';
                                }
                            } else {
                                select += '<option value="' + item.IdExcepcionPlantilla + '/' + full.IdPrograma + '">' + item.Nombre + '</option>';
                            }
                        }
                    });
                    select += '</select>'
                    return select;
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            if (json.length == 0){
                bloqueaPermisosGral(1);
            }            
            $('#tblPermisosUsers2_spFiltro').append('<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>');
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
    $("#cmbGrupoPermisos").empty();
    $("#cmbGrupoPermisos").append('<option value="-1">Seleccione</option>');
    var opciones = {
        url: ROOT + "Mantenedores/GetGruposUsers",
        data: { id: usuario },
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
    $('#modalPermisos2').modal('show');
}
function bloqueaPermisosGral(bloquea) {
    tipo = bloquea ? true : false;
    $(".agregaPermisoMasivo").prop('disabled', tipo);
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
function modalMsjConfirm(titulo, mensaje) {
    $('#tituloMsjConfirm').empty();
    $('#tituloMsjConfirm').text(titulo);
    $('#bodyMsjConfirm').html(mensaje);
    $('#modalMsjConfirm').modal('show');
}
function modalCambioPass(usuario) {
    $("#txtIdUserCambioPass").val(usuario);
    $("#modalCambioPass").modal('show');
}
function iniciaTablaUsuarios() {
    var tablaUsuarios = $('#tblUsuarios').DataTable();
    tablaUsuarios.destroy();
    $('#tblUsuarios').dataTable({
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetUsuarios",
            "dataSrc": "",
            "type": "POST"
        },
        "columns": [
            {
                "data": 'UserName', "width": "25%",
                "render": function (data, type, full, meta) {
                    return '<a href="#" onclick="modalNuevoUsers(\'' + full.Id + '\',\'' + data + '\',\'' + full.Nombre + '\',' + full.IdMinisterio + ',' + full.IdServicio + ',' + full.IdPerfil + ',' + full.IdEstado + ');" data-toggle="tooltip" data-placement="right" title="Click para editar usuario">' + data + '</a>';
                }
            },
            {
                "data": 'Nombre', "width": "25%",
                "render": function (data, type, full, meta) {
                    return '<label data-toggle="tooltip" title="Email confirmado: ' + (full.EmailConfirmed == 0 ? "No" : "Si") + '" style="font-weight:normal;margin-bottom:0px;">' + data + '</label>';
                }
            },
            { "data": 'Ministerio', "width": "20%" },
            { "data": 'Servicio', "width": "17%" },
            {
                "data": 'Perfil', "width": "5%",
                "render": function (data, type, full, meta) {
                    var grupo = "";
                    if (data != null) {
                        grupo = '<label data-toggle="tooltip" title="' + full.DescripcionPerfil + '" style="font-weight:normal;margin-bottom:0px;">' + data + '</label>';;
                    } else {
                        grupo = "";
                    }
                    return grupo;
                }
            },                        
            {
                "data": null, "orderable": false, "width": "2%",
                "render": function (data, type, full, meta) {
                    return '<button type="button" class="btn btn-primary btn-xs ' + (full.TotalGrupos == 0 ? "count0" : "") + '" data-toggle="tooltip" data-placement="left" title="Click para ver los grupos pertenecientes a este usuario" onclick="modalGrupos(\'' + full.Id + '\');"><span class="badge" style="font-size:11px;">' + full.TotalGrupos + '</span></button>';
                }
            },
            {
                "data": null, "orderable": false, "width": "2%",
                "render": function (data, type, full, meta) {
                    return '<button type="button" class="btn btn-primary btn-xs ' + (full.TotalPermisos == 0 ? "count0" : "") + '" data-toggle="tooltip" data-placement="left" title="Click para ver los permisos pertenecientes a este usuario" onclick="modalPermisos(\'' + full.Id + '\');"><span class="badge" style="font-size:11px;">' + full.TotalPermisos + '</span></button>';
                }
            },
            {
                "data": null, "orderable": false, "width": "2%",
                "render": function (data, type, full, meta) {
                    return '<button type="button" class="btn btn-primary btn-xs" data-toggle="tooltip" data-placement="left" title="Click para asignar una contraseña aleatoria a este usuario" onclick="modalCambioPass(\'' + full.Id + '\');"><i class="fa fa-refresh" aria-hidden="true"></i></button>';
                }
            },
            {
                "data": null, "orderable": false, "width": "2%",
                "render": function (data, type, full, meta) {
                    var titulo = "Eliminar usuario";
                    var mensaje = "<p>¿Está seguro de eliminar el usuario seleccionado?</p>";
                    return '<button class="btn btn-danger btn-xs" type="button" data-toggle="tooltip" data-placement="left" title="Eliminar usuario" onclick="modalEliminarGnral(\'' + titulo + '\',\'' + mensaje + '\',\'usuario\',\'' + full.Id + '\');"><i class="fa fa-times"></i></button>';                    
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            var btnCreaUsers = '<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>';
            if (creaUsers) {
                btnCreaUsers = '<button type="button" class="btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="Crear un nuevo usuario" onclick="modalNuevoUsers(0,\'\',\'\',0,0,0);"><i class="fa fa-user-plus" aria-hidden="true"></i></button>';
            }
            $('#tblUsuarios_spFiltro').append(btnCreaUsers);
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
}
function borraGrupo() {
    $("#btnElimGenral").button('loading');
    var usuario = $("#txtUserModel").val();
    var datos = {};
    datos.IdUsuario = usuario;
    datos.IdGrupoFormulario = $("#txtIdEliminar").val();
    var opciones = {
        url: ROOT + "Mantenedores/EliminaGrupoUsuario",
        data: { data: datos },
        type: "post",
        datatype: "json",
        success: function (result, xhr, status) {
            $("#btnElimGenral").button('reset')
            if (result == "ok") {
                $("#modalEliminarGeneral").modal('hide');
                iniciaTablaUsuarios();
                iniciaTablaGrupos(usuario);
                modalMsjUsers("<p>Grupo eliminado exitosamente para el usuario seleccionado</p>");
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
}
function borraPermiso(todos) {
    $("#btnElimGenral").button('loading');
    var usuario = $("#txtUserPermisosModel").val();
    var datos = [];    
    if (todos) {

    } else {
        var dato = {};
        dato.IdUsuario = usuario;
        dato.IdFormulario = $("#txtIdEliminar").val();
        datos.push(dato);
    }    
    var opciones = {
        url: ROOT + "Mantenedores/EliminaPermiso",
        data: { data: datos },
        type: "post",
        datatype: "json",
        success: function (result, xhr, status) {
            $("#btnElimGenral").button('reset')
            $("#modalEliminarGeneral").modal('hide');
            if (result == "ok") {
                iniciaTablaPermisos(usuario);
                iniciaTablaUsuarios();                
                modalMsjUsers("<p>Permiso eliminado exitosamente para el usuario seleccionado</p>");
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
}
function modalMsjConfirm(titulo, mensaje, tipoMsj) {
    $('#tituloMsjConfirm').empty();
    $('#tituloMsjConfirm').text(titulo);
    $('#bodyMsjConfirm').html(mensaje);
    $('#txtTipoMsj').val(tipoMsj);
    $('#modalMsjConfirm').modal('show');
}
function asignaUserGrupo() {
    usuario = $("#txtUserModel").val();
    $("#btnMsjConfirm").button('loading');
    var datos = [];
    $(".cmbTG").each(function (i, item) {
        if ($(this).val().split('/')[0] != "-1") {
            dato = {};
            dato.IdUsuario = usuario;
            dato.IdGrupoFormulario = $(this).val().split('/')[1];
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
                $("#modalGrupos2").modal('hide');
                iniciaTablaGrupos(usuario);
                iniciaTablaUsuarios();
                modalMsjUsers("<p>Grupo asignado exitosamente a este usuario</p>");
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
}
function asignaUserPermisos() {
    usuario = $("#txtUserPermisosModel").val();
    $("#btnMsjConfirm").button('loading');
    var datos = [];
    $(".cmbPP").each(function (i, item) {
        if ($(this).val().split('/')[0] != "-1") {
            dato = {};
            dato.IdUsuario = usuario;
            dato.IdFormulario = $(this).val().split('/')[1];
            dato.IdPermiso = $(this).val().split('/')[0];
            datos.push(dato);
        }
    });
    var opciones = {
        url: ROOT + "Mantenedores/GuardaUsuariosPermisos",
        data: { data: datos },
        type: "post",
        datatype: "json",
        success: function (result, xhr, status) {
            $("#btnMsjConfirm").button('reset')
            $("#modalMsjConfirm").modal('hide');
            if (result == "ok") {                
                $("#modalPermisos2").modal('hide');
                iniciaTablaPermisos(usuario);
                iniciaTablaUsuarios();
                modalMsjUsers("<p>Permiso asignado exitosamente a este usuario</p>");
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
}
function eliminaPermisosGral() {    
    var oTabla = $("#tblPermisosUsers").DataTable();
    var rowCount = oTabla.data().count();
    if (rowCount > 0) {
        $('#modalElimPermisosUsers').modal('show');
    } else {
        modalMsjUsers("<p>Sin datos para eliminar</p>");
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
function agregaPermisosMasivo() {
    usuario = $("#txtUserPermisosModel").val();
    $("#btnGuardaPermisos").button('loading');    
    var datos = {};
    if ($("#cmbAnoPermisos option:selected").val() != "-1") {
        datos.Ano = $("#cmbAnoPermisos option:selected").val();
    }
    if ($("#cmbMinisterioPermisos option:selected").val() != "-1") {
        datos.IdMinisterio = $("#cmbMinisterioPermisos option:selected").val();
    }
    if ($("#cmbServicioPermisos option:selected").val() != "-1") {
        datos.IdServicio = $("#cmbServicioPermisos option:selected").val();
    }
    if ($("#cmbGrupoPermisos option:selected").val() != "-1") {
        datos.IdGrupoFormulario = $("#cmbGrupoPermisos option:selected").val();
    }
    if ($("#cmbTipoPermisos option:selected").val() != "-1") {
        datos.TipoFormulario = $("#cmbTipoPermisos option:selected").val();
    }
    if ($("#cmbPermisoPerm option:selected").val() != "-1") {
        datos.IdExcepcion = $("#cmbPermisoPerm option:selected").val();
    }
    datos.IdUser = usuario;
    var opciones = {
        url: ROOT + "Mantenedores/GuardaPermisosMasivo",
        data: { data: datos },
        type: "post",
        datatype: "json",
        success: function (result, xhr, status) {
            $("#btnGuardaPermisos").button('reset');
            if (result == "ok") {
                $("#modalPermisos2").modal('hide');
                iniciaTablaPermisos(usuario);
                iniciaTablaUsuarios();
                modalMsjUsers("<p>Permiso asignado exitosamente a este usuario</p>");
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
}
function borraUsuario() {
    $("#btnElimGenral").button('loading');
    var usuario = $("#txtIdEliminar").val();
    var opciones = {
        url: ROOT + "Mantenedores/EliminaUsuarioEstado",
        data: { id: usuario },
        type: "post",
        datatype: "json",
        success: function (result, xhr, status) {
            $("#btnElimGenral").button('reset')
            if (result == "ok") {
                $("#modalEliminarGeneral").modal('hide');
                iniciaTablaUsuarios();
                modalMsjUsers("<p>Usuario eliminado exitosamente</p>");
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
}
$(document).ready(function () {
    $("#menuMantenedores").addClass('in');
    $("#aMenuUsuarios").dropdown('toggle');
    $("#liNivelCentral").addClass('active');
    $("#liMantenedores").addClass('active');
    $("#menuNivelCentral").addClass('in');    
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
    $("#btnVerPermisos").click(function () {
        var id = $("#cmbPerfil option:selected").val();
        if (id != "-1") {
            modalPerfiles(id, $("#cmbPerfil option:selected").text());
        }
    });
    iniciaTablaUsuarios();
    $("#btnGuardaUser").click(function () {
        if (!validaDatosUsuarios()) {
            $(this).button('loading');
            var datos = {};
            datos.IdUser = $("#txtIdUser").val();
            datos.Email = $("#txtEmail").val();
            datos.Nombre = $("#txtNombre").val();
            datos.Ministerio = $("#cmbMinisterio option:selected").val();
            datos.Servicio = $("#cmbServicio option:selected").val();
            datos.Perfil = $("#cmbPerfil option:selected").val();
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
                        iniciaTablaUsuarios();
                        modalMsjUsers("<p>Usuario creado exitosamente</p>");
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
    $('#modalNuevoUsers').on('hidden.bs.modal', function (e) {
        limpiaModelUsers();
    });
    $('#modalMsjUsers').on('hidden.bs.modal', function (e) {
        $('#msjInfoUsers').empty();
    });
    $('#modalGrupos').on('hidden.bs.modal', function (e) {
        $("#txtUserModel").val("");
    });
    $('#modalPermisos').on('hidden.bs.modal', function (e) {
        $("#txtUserPermisosModel").val("");
    });
    $('#modalEliminarGeneral').on('hidden.bs.modal', function (e) {        
        $("#txtTipoEliminar").val("");
        $("#txtIdEliminar").val("");
    });
    //$("#btnAcepMsjUser").click(function () {
    //    location.reload();
    //    //window.location.href = ROOT + "Mantenedores/Grupos";
    //});       
    $("#btnElimGenral").click(function () {
        var tipo = $("#txtTipoEliminar").val();
        if (tipo == "grupo") {
            borraGrupo();
        }else if (tipo == "permisos"){
            borraPermiso(0);
        }else if (tipo == "usuario"){
            borraUsuario();
        }
    });
    $("#btnGuardaGrupos").click(function () {
        var countTG = 0;
        $(".cmbTG").each(function (i, item) {
            if ($(this).val().split('/')[0] != "-1") {
                countTG++;
            }
        });
        if (countTG != 0) {
            modalMsjConfirm("Asignar usuarios", "<p>¿Está seguro de asignar " + countTG + " grupos a este usuario?</p>", "usersGrupos");
        } else {
            modalMsjUsers("<p>Seleccione al menos un tipo de grupo</p>");
        }
    });
    $('#modalMsjConfirm').on('hidden.bs.modal', function (e) {
        $("#txtTipoMsj").val("");
    });
    $("#btnMsjConfirm").click(function () {
        var tipo = $("#txtTipoMsj").val();
        if (tipo == "usersGrupos") {
            asignaUserGrupo();
        } else if (tipo == "usersPermisos") {
            asignaUserPermisos();
        }
    });
    $("#btnGuardaPermisos").click(function () {
        if ($("#idTipoAgrePerm").val() == "Individual") {
            var countPP = 0;
            $(".cmbPP").each(function (i, item) {
                if ($(this).val().split('/')[0] != "-1") {
                    countPP++;
                }
            });
            if (countPP != 0) {
                modalMsjConfirm("Asignar permisos", "<p>¿Está seguro de asignar " + countPP + " permisos a este usuario?</p>", "usersPermisos");
            } else {
                modalMsjUsers("<p>Seleccione al menos un permiso</p>");
            }
        } else {
            if (!validaSeleccione2('agregaPermisoMasivo')) {
                agregaPermisosMasivo();                
            } else {
                modalMsjUsers("<p>Seleccione campos obligatorios <b>(*)</b></p>");
            }
        }
    });
    $("#btnElimUsersPerm").click(function () {
        if (validaSeleccione('elimPermUser')) {
            usuario = $("#txtUserPermisosModel").val();
            $(this).button('loading');            
            var datos = {};
            if ($("#cmbAnoElimPerm option:selected").val() != "-1") {
                datos.Ano = $("#cmbAnoElimPerm option:selected").val();
            }
            if ($("#cmbMinisterioElimPerm option:selected").val() != "-1") {
                datos.IdMinisterio = $("#cmbMinisterioElimPerm option:selected").val();
            }
            if ($("#cmbServicioElimPerm option:selected").val() != "-1") {
                datos.IdServicio = $("#cmbServicioElimPerm option:selected").val();
            }
            if ($("#cmbTipoElimPerm option:selected").val() != "-1") {
                datos.TipoFormulario = $("#cmbTipoElimPerm option:selected").val();
            }
            if ($("#cmbPlantillasElimPerm option:selected").val() != "-1") {
                datos.IdExcepcion = $("#cmbPlantillasElimPerm option:selected").val();
            }
            datos.IdUser = usuario;
            var opciones = {
                url: ROOT + "Mantenedores/EliminaPermisosMasivo",
                beforeSend: function (xhr) { $("#cargaPagina").fadeIn(); },
                data: { data: datos },
                type: "post",
                datatype: "json",
                success: function (result, xhr, status) {
                    $("#btnElimUsersPerm").button('reset');                    
                    if (result == "ok") {
                        iniciaTablaPermisos(usuario);
                        $("#modalElimPermisosUsers").modal('hide');
                        iniciaTablaUsuarios();
                        modalMsjUsers("<p>Permisos eliminados exitosamente para este usuario</p>");
                    } else {
                        modalMsjUsers("<p>" + result + "</p>");
                        console.log(result);
                    }
                },
                error: function (xhr, status, error) {
                    modalMsjUsers("<p>" + error + "</p>");
                    console.log(error);
                },
                complete: function (xhr, status) {
                    $("#cargaPagina").fadeOut();
                }
            };
            $.ajax(opciones);
        } else {
            modalMsjUsers("<p>Seleccione al menos una categoría</p>");
        }
    });
    $('#modalElimPermisosUsers').on('hidden.bs.modal', function (e) {
        $("#cmbAnoElimPerm option:selected").removeAttr("selected");
        $("#cmbMinisterioElimPerm option:selected").removeAttr("selected");
        $("#cmbServicioElimPerm").empty();
        $("#cmbServicioElimPerm").append("<option value='-1'>Seleccione</option>");
        $("#cmbTipoElimPerm option:selected").removeAttr("selected");
        $("#cmbPlantillasElimPerm option:selected").removeAttr("selected");
    });
    $('.tipoAgregaPerm li a').click(function (e) {
        $("#idTipoAgrePerm").val($(this).text());
    });
    $('#modalPermisos2').on('hidden.bs.modal', function (e) {
        $("#idTipoAgrePerm").val("");
        $("#cmbAnoPermisos option:selected").removeAttr("selected");
        $("#cmbMinisterioPermisos option:selected").removeAttr("selected");
        $("#cmbServicioPermisos").empty();
        $("#cmbServicioPermisos").append("<option value='-1'>Seleccione</option>");
        $("#cmbGrupoPermisos option:selected").removeAttr("selected");
        $("#cmbTipoPermisos option:selected").removeAttr("selected");
        $("#cmbPermisoPerm option:selected").removeAttr("selected");
        bloqueaPermisosGral(0);
    });
    $('#modalPermisos2').on('show.bs.modal', function (e) {
        $(".tipoAgregaPerm li").removeClass('active');
        $(".tab-pane").removeClass('active');
        $("#liIndivAgrePerm").addClass('active');
        $("#individual").addClass('active');
        $("#idTipoAgrePerm").val("Individual");
    });
    $('#modalCambioPass').on('hidden.bs.modal', function (e) {        
        $("#txtIdUserCambioPass").val("");
    });
    $('#btnReenviarMail').click(function () {
        usuario = $("#txtIdUserCambioPass").val();
        $(this).button('loading');
        var opciones = {
            url: ROOT + "Mantenedores/ReenviaMail",
            data: { id: usuario },
            type: "post",
            datatype: "json",
            success: function (result, xhr, status) {
                $("#btnReenviarMail").button('reset');
                if (result == "ok") {                    
                    $("#modalCambioPass").modal('hide');
                    iniciaTablaUsuarios();
                    modalMsjUsers("<p>Email enviado exitosamente para este usuario</p>");
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
    });
    $('#btnNuevaPass').click(function () {
        usuario = $("#txtIdUserCambioPass").val();
        $(this).button('loading');
        var opciones = {
            url: ROOT + "Mantenedores/NuevaPass",
            data: { id: usuario },
            type: "post",
            datatype: "json",
            success: function (result, xhr, status) {
                $("#btnNuevaPass").button('reset');
                $("#modalCambioPass").modal('hide');
                iniciaTablaUsuarios();
                modalMsjUsers("<p>Nueva contraseña: " + result + "</p>");
            },
            error: function (xhr, status, error) {
                modalMsjUsers("<p>" + error + "</p>");
                console.log(error);
            }
        };
        $.ajax(opciones);
    });
    $('#cmbTipoPermisos').click(function () {
        if (aPlantillas.length > 0) {
            var tipo = $(this).val();
            $("#cmbPermisoPerm").empty();
            $("#cmbPermisoPerm").append("<option value='-1'>Seleccione</option>");
            $.each(aPlantillas, function (i, item) {
                if (item.TipoFormulario == tipo) {
                    $("#cmbPermisoPerm").append("<option value='" + item.IdExcepcionPlantilla + "'>" + item.Nombre + "</option>");
                }
            });
        }
    });
});