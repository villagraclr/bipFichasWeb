//Rescata valores desde url
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
function ocultaMenu() {
    //Agrupo menu principal
    $("#side-menu li").each(function (index, elemento) {
        var id = elemento.id;
        if (id != "liMenuGeneral") {
            if ($(elemento).is(":visible")) {
                $(elemento).css("display", "none");
            } else {
                $(elemento).css("display", "block");
            }
        } else {
            $(elemento).css("display", "block");
        }
    });
}
function agregaMenuPrincipal() {
    $('#btnModalMensRecargar').hide();
    $('#btnModalMensRedirigir').hide();
    //console.log("Carga Menu Formularios");
    var menu;    
    var opciones = {
        url: ROOT + "Content/ArchivosSubidos/formulario_" + tipoPrograma + ".json",
        contentType: "application/json",
        datatype: "json",
        async: false,
        beforeSend: function () { $("#cargaPagina").fadeIn(); },
        success: function (result, xhr, status) {
            var objResultado = result;
            //Agrego tabs
            const menuSectorialistas = 737;
            const menuSectorialistas2 = 776;
            const menuEvaluacion = [765,796,873,901,936,938,979,490];
            const rolesPermisoSect = [1,2,5,6];
            const etapaEvaluacion = 329;
            const etapaEvaluacion2 = 349;
            const etapaPerfilEnConsulta = 29431;
            menu = ($.inArray(rol,rolesPermisoSect) !== -1 ? objResultado.menu : $.grep(objResultado.menu, function (n, i) { return n.menuPadre.IdMenu != menuSectorialistas && $.inArray(n.menuPadre.IdMenu,menuEvaluacion) == -1 && n.menuPadre.IdMenu != menuSectorialistas2; }));            
            var tabs = '<ul id="aMenuForm" class="nav nav-pills nav-stacked">';
            $.each(menu, function (i, item) {
                var coment = (comentarios.length > 0 ? $.grep(comentarios, function (n, i) { return n.IdMenu === item.menuPadre.IdMenu; }) : null);
                var msj = (coment != null ? (coment.length > 0 ? coment.length : '') : '');
                if ($.inArray(item.menuPadre.IdMenu,menuEvaluacion) !== -1){
                    if (etapa == etapaEvaluacion || etapa == etapaEvaluacion2 || etapa == etapaPerfilEnConsulta){
                        tabs += '<li role="presentation" id="' + item.menuPadre.IdMenu + '"' + ((tab == item.menuPadre.IdMenu || tab == 0 && item.menuPadre.Orden == 1) ? "class=active" : "") + '><a href="#tabForm' + (item.menuPadre.Orden) + '" data-toggle="pill" onclick="validaBlancos(false, ' + item.menuPadre.IdMenu + ', 0);">' + item.menuPadre.TipoMenu + '<span id="spnPadre' + item.menuPadre.IdMenu + '" class="badge" style="display: inline; padding: 0px 7px; margin-left: 10px; background-color: #8400CA;"></span><span id="spnPadreTotalComent' + item.menuPadre.IdMenu + '" class="badge">' + msj + '</span></a></li>';
                    }                    
                }else{
                    tabs += '<li role="presentation" id="' + item.menuPadre.IdMenu + '"' + ((tab == item.menuPadre.IdMenu || tab == 0 && item.menuPadre.Orden == 1) ? "class=active" : "") + '><a href="#tabForm' + (item.menuPadre.Orden) + '" data-toggle="pill" onclick="validaBlancos(false, ' + item.menuPadre.IdMenu + ', 0);">' + item.menuPadre.TipoMenu + '<span id="spnPadre' + item.menuPadre.IdMenu + '" class="badge" style="display: inline; padding: 0px 7px; margin-left: 10px; background-color: #8400CA;"></span><span id="spnPadreTotalComent' + item.menuPadre.IdMenu + '" class="badge">' + msj + '</span></a></li>';
                }
            });
            $(".sidebar-nav").append('<ul id="aMenuForm" class="nav nav-pills nav-stacked">' + tabs + '</ul>');
        },
        error: function (xhr, status, error) {
            $("#txtMensajesFormulario").empty();
            $("#txtMensajesFormulario").html("<p>" + error + "</p>");
            $('#modalMensajesFormularios').modal('show');
            console.log(error);
        },
        complete: function () { $("#cargaPagina").fadeOut(); }
    };
    /*var opciones = {
        url: ROOT + "Formulario/CreaFormulario",
        data: { _id: queryString('_id') },
        type: "post",
        datatype: "json",
        async: false,
        beforeSend: function () { $("#cargaPagina").fadeIn(); },
        success: function (result, xhr, status) {
            var objResultado = JSON.parse(result);
            //Agrego tabs
            menu = objResultado.menu;
            var tabs = '<ul id="aMenuForm" class="nav nav-pills nav-stacked">';
            $.each(menu, function (i, item) {
                tabs += '<li role="presentation" id="' + item.menuPadre.IdMenu + '"' + ((tab == item.menuPadre.IdMenu || tab == 0 && item.menuPadre.Orden == 1) ? "class=active" : "") + '><a href="#tabForm' + (item.menuPadre.Orden) + '" data-toggle="pill">' + item.menuPadre.TipoMenu + '</a></li>';
            });
            $(".sidebar-nav").append('<ul id="aMenuForm" class="nav nav-pills nav-stacked">' + tabs + '</ul>');
        },
        error: function (xhr, status, error) {
            $("#txtMensajesFormulario").empty();
            $("#txtMensajesFormulario").html("<p>" + error + "</p>");
            $('#modalMensajesFormularios').modal('show');
            console.log(error);
        },
        complete: function () { $("#cargaPagina").fadeOut(); }
    };*/
    $.ajax(opciones);
    return menu;
}
/*function buscaRespuestas() {
    var respuestas;
    var opciones = {
        url: ROOT + "Formulario/BuscaRespuestas",
        data: { _id: queryString('_id') },
        type: "post",
        datatype: "json",
        async: false,
        beforeSend: function () { $("#cargaPagina").fadeIn(); },
        success: function (result, xhr, status) {            
            respuestas = JSON.parse(result);
        },
        error: function (xhr, status, error) {
            $("#txtMensajesFormulario").empty();
            $("#txtMensajesFormulario").html("<p>" + error + "</p>");
            $('#modalMensajesFormularios').modal('show');
            console.log(error);
        },
        complete: function () { $("#cargaPagina").fadeOut(); }
    };
    $.ajax(opciones);
    return respuestas;
}*/
function buscaExcepciones() {
    var excepciones;
    /*var opciones = {
        url: ROOT + "Content/ArchivosSubidos/excepciones_" + tipoPrograma + ".json",
        contentType: "application/json",
        datatype: "json",
        async: false,
        beforeSend: function () { $("#cargaPagina").fadeIn(); },
        success: function (result, xhr, status) {
            excepciones = result;
        },
        error: function (xhr, status, error) {
            $("#txtMensajesFormulario").empty();
            $("#txtMensajesFormulario").html("<p>" + error + "</p>");
            $('#modalMensajesFormularios').modal('show');
            console.log(error);
        },
        complete: function () { $("#cargaPagina").fadeOut(); }
    };*/
    var opciones = {
        url: ROOT + "Formulario/BuscaExcepciones",
        data: { _id: queryString('_id') },
        type: "post",
        datatype: "json",
        async: false,
        beforeSend: function () { $("#cargaPagina").fadeIn(); },
        success: function (result, xhr, status) {
            excepciones = JSON.parse(result);
        },
        error: function (xhr, status, error) {
            $("#txtMensajesFormulario").empty();
            $("#txtMensajesFormulario").html("<p>" + error + "</p>");
            $('#modalMensajesFormularios').modal('show');
            console.log(error);
        },
        complete: function () { $("#cargaPagina").fadeOut(); }
    };
    $.ajax(opciones);
    return excepciones;
}
async function funciones(respuestas) {
    var opciones = {
        //async: false,
        url: ROOT + "Content/ArchivosSubidos/funciones_" + tipoPrograma + ".json",        
        contentType: "application/json",
        datatype: "json",
        beforeSend: function () { $("#cargaPagina").fadeIn(); },
        success: function (result, xhr, status) {
            (async() => { await funcionesFormularios(result, respuestas); })();            
        },
        error: function (xhr, status, error) {
            $("#txtMensajesFormulario").empty();
            $("#txtMensajesFormulario").html("<p>" + error + "</p>");
            $('#modalMensajesFormularios').modal('show');
            console.log(error);
        },
        complete: function () { 
            validaBlancos(true, 0, 0);
            $("#cargaPagina").fadeOut(); 
        }
    };
    /*var opciones = {
        url: ROOT + "Formulario/GetFunciones",
        data: { _id: queryString('_id') },
        type: "post",
        datatype: "json",
        async: false,
        beforeSend: function () { $("#cargaPagina").fadeIn(); },
        success: function (result, xhr, status) {
            var objResultado = JSON.parse(result);
            funcionesFormularios(objResultado, respuestas);
        },
        error: function (xhr, status, error) {
            $("#txtMensajesFormulario").empty();
            $("#txtMensajesFormulario").html("<p>" + error + "</p>");
            $('#modalMensajesFormularios').modal('show');
            console.log(error);
        },
        complete: function () { $("#cargaPagina").fadeOut(); }
    };*/
    $.ajax(opciones);
}
function modalComentarios(menuHijo){
    $("#txtTemaComentario").val("");
    $("#cmbOpcObservaciones option[value='-1']").prop('selected', true);
    $("#txtComentarios").val("");
    $("#menuId").val(menuHijo);
    $('#modalComentarios').resizable({        
        minHeight: 300,
        minWidth: 300
    });
    $('#modalComentarios').draggable({
        handle: ".modal-header"
    });
    $('#modalComentarios').modal('show');
}
function abreInformes(link, ano, tipo){
    var idProg = $("#cmbVersionInforEval option:selected").val();
    if (idProg != "-1"){
        window.open(link.replace('{0}',idProg).replace('{1}',ano));
    }else{
        $("#txtMensajesFormulario").empty();
        $("#txtMensajesFormulario").html("<p>Seleccione al menos una versión del informe</p>");
        $('#modalMensajesFormularios').modal('show');
    }   
}
function ingresarComentarios(idPregunta, idTab, idMenuPadre, idMenuHijo){    
    txtComentariosInstancia.setData('');
    $("#idConsulta").val('');
    $("#idPregunta").val(idPregunta);
    $("#idTab").val(idTab);
    $("#idMenuPadre").val(idMenuPadre);
    $("#idMenuHijo").val(idMenuHijo);
    $('#modalComentarios').resizable({
        minHeight: 300,
        minWidth: 300
    });
    $('#modalComentarios').draggable({
        handle: ".modal-header"
    });
    var opciones = {
        url: ROOT + "Formulario/GetComentarios",
        data: { _id: queryString('_id') },
        type: "post",
        datatype: "json",
        async: false,
        beforeSend: function () { $("#cargaPagina").fadeIn(); },
        success: function (result, xhr, status) {
            comentarios = JSON.parse(result);
        },
        error: function (xhr, status, error) {
            $("#txtMensajesFormulario").empty();
            $("#txtMensajesFormulario").html("<p>" + error + "</p>");
            $('#modalMensajesFormularios').modal('show');
            console.log(error);
        },
        complete: function () { $("#cargaPagina").fadeOut(); }
    };
    $.ajax(opciones);
    if (comentarios.length > 0){
        $("#ulComentarios").empty();
        var listaComent = $.grep(comentarios, function (elem) { return elem.IdPregunta === idPregunta; });
        if (listaComent.length > 0) {            
            $("#msjAlert" + listaComent[0].IdPregunta + listaComent[0].IdTab).remove();
            $("#btnIngresarComent" + listaComent[0].IdPregunta).removeClass("btn-warning").addClass("btn-success");
            $("#ibtnComent" + listaComent[0].IdPregunta).removeClass("fa-bell-o").addClass("fa-comment");
            $.each(listaComent, function (i, c) {
                var liCss = ((i+1)%2 !== 0 ? "left" : "right");
                var btnComent = (c.IdUsuario == usuario ? (listaComent.length == (i+1) ? '<button id="btnEditaComentario" type="button" class="btn btn-success btn-xs" data-toggle="tooltip" data-placement="top" title="Editar comentario" style="margin-right: 2px; margin-left: 5px;" onclick="editaComentario(' + c.IdConsulta + ',\'' + c.Consulta + '\');"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></button><button id="btnEliminaComentario" type="button" class="btn btn-danger btn-xs" data-toggle="tooltip" data-placement="top" title="Eliminar comentario" style="margin-right: 5px;" onclick="eliminaComentario(' + c.IdConsulta + ',' + c.IdPrograma + ',' + c.IdMenu + ',' + c.IdMenuHijo + ',' + c.IdPregunta + ',\'' + c.IdUsuario + '\');"><i class="fa fa-trash" aria-hidden="true"></i></button>' : '') : '');
                var ordenComent = ((i+1)%2 !== 0 ? '<strong class="primary-font">' + c.NombreUsuario + '</strong><small class="pull-right text-muted">' + (liCss == "left" ? btnComent : "") + '<span class="glyphicon glyphicon-time"></span>' + moment(c.Fecha).format("DD/MM/YYYY HH:mm:ss") + '</small>' : '<small class="text-muted"><span class="glyphicon glyphicon-time"></span>' + moment(c.Fecha).format("DD/MM/YYYY HH:mm:ss") + '</small>' + (liCss == "left" ? "" : btnComent) + '<strong class="pull-right primary-font">' + c.NombreUsuario + '</strong>');
                $("#ulComentarios").append('<li class="' + liCss + ' clearfix" style="margin-right: 10px;"><span class="chat-img pull-' + liCss + '"><h2 style="margin-top: 0px !important; margin-bottom: 0px !important; color: ' + (liCss == "left" ? "#337ab7" : "#d9534f") + ' !important;">' + c.NombreUsuario.split(' ')[0].substring(0,1).toUpperCase() + c.NombreUsuario.split(' ')[1].substring(0,1).toUpperCase() + '</h2></span><div class="chat-body clearfix"><div class="header" style="margin-bottom: 5px">' + ordenComent + '</div><p id="comentario' + c.IdConsulta + '">' + c.Consulta + '</p></div></li>');
            });
        }        
    }
    $("#divTextoComentarios").css("display", "none");
    $("#btnIngComent").css("display", "none");
    $("#btnNuevoComent").css("display", "");
    $("[data-toggle=tooltip]").tooltip({ html: true });
    $('#modalComentarios').modal('show');
}
function editaComentario(idConsulta, consulta){
    $("#btnEditaComentario").prop('disabled', true);
    txtComentariosInstancia.setData(consulta);
    $("#divTextoComentarios").css("display", "");
    $("#btnIngComent").css("display", "");
    $("#btnNuevoComent").css("display", "none");
    $("#idConsulta").val(idConsulta);
}
function eliminaComentario(idConsulta, idPrograma, idMenuPadre, idMenuHijo, idPregunta, idUsuario){
    modalMsjConfirm("Eliminar comentario", "<p>¿Está seguro que desea eliminar este comentario?</p>", idPrograma, idUsuario, idConsulta, idMenuPadre, idMenuHijo, idPregunta);
}
function modalMsjConfirm(titulo, mensaje, idPrograma, idUsuario, idConsulta, idMenuPadre, idMenuHijo, idPregunta) {
    $('#tituloMsjConfirm').empty();
    $('#tituloMsjConfirm').text(titulo);
    $('#bodyMsjConfirm').html(mensaje);
    $('#borraIdConsulta').val(idConsulta);
    $('#borraIdPrograma').val(idPrograma);
    $('#borraIdMenuPadre').val(idMenuPadre);
    $('#borraIdMenuHijo').val(idMenuHijo);
    $('#borraIdPregunta').val(idPregunta);
    $('#borraIdUsuario').val(idUsuario);
    $('#modalMsjConfirm').modal('show');
}
function guardarAdmisibilidad(){
    //var idPrograma = $('#idProgramaAdmisibilidad').val();
    var ingresoExAnte = $('#cmb9722').val();
    var motivoNoIngreso = $('#cmb9723').find('option:selected').text();
    var otroMotivo = $('#txt9724').val();
    $('#btnModalEnviarAdmisibilidad').button('loading');
    var opciones = {
        url: ROOT + "Gores/EnviarAdmisibilidad",
        data: { _id: queryString('_id'), ingresoExAnte: ingresoExAnte, motivoNoIngreso: motivoNoIngreso, otroMotivo: otroMotivo },
        type: "post",
        datatype: "json",
        async: false,
        beforeSend: function () { $("#cargaPagina").fadeIn(); },
        success: function (result, xhr, status) {
            $("#btnModalEnviarAdmisibilidad").button('reset');
            $("#modalEnviarAdmisibilidad").modal('hide');
            $("#txtMensajesFormulario").empty();
            $('#btnModalMensajes').hide();
            if (result == "ok") {
                if(ingresoExAnte == 902){
                    $('#btnModalMensRecargar').show();
                    $('#btnModalMensajesX').css('display', 'none');
                    $("#txtMensajesFormulario").html("<p>Guardado exitoso, <b>la página se recargará</b></p>");
                }else{
                    $('#btnModalMensRedirigir').show();
                    $('#btnModalMensajesX').css('display', 'none');
                    $("#txtMensajesFormulario").html("<p>Se ha procesado la admisibilidad del perfil</p><p>Al aceptar serás <b>redirigido a la vista de Evaluadores Perfil</b></p>");
                    $('#modalMensajesFormularios').modal('show');
                    $('#btnModalMensRedirigir').attr('href', '/Gores/EvaluadorPerfil');
                }
            }else{
                $("#txtMensajesFormulario").html("<p>" + result + "</p>");
            }
            $('#modalMensajesFormularios').modal('show');

        },
        error: function (xhr, status, error) {
            $("#txtMensajesFormulario").empty();
            $("#txtMensajesFormulario").html("<p>" + error + "</p>");
            $('#modalMensajesFormularios').modal('show');
            console.log(error);
        },
        complete: function () { $("#cargaPagina").fadeOut(); }
    };
    $.ajax(opciones);
}
function guardarCalificacion(){
    var calificacion = $('#respuestaCalificacion').val();
    var idBips = $('#idBipsCalificacion').val();
    $('#btnModalEnviarCalificacion').button('loading');
    var opciones = {
        url: ROOT + "Gores/EnviarCalificacion",
        data: { _id: queryString('_id'), idBips: idBips, calificacion: calificacion },
        type: "post",
        datatype: "json",
        async: false,
        beforeSend: function () { $("#cargaPagina").fadeIn(); },
        success: function (result, xhr, status) {
            $("#btnModalEnviarAdmisibilidad").button('reset');
            $("#modalEnviarCalificacion").modal('hide');
            $('#btnModalMensajes').hide();
            if (result == "ok") {
                $('#btnModalMensRedirigir').show();
                $('#btnModalMensajesX').css('display', 'none');
                $("#txtMensajesFormulario").html("<p>Se ha procesado la calificación</p><p>Al aceptar serás <b>redirigido a la vista de Perfiles GORE</b></p>");
                $('#modalMensajesFormularios').modal('show');
                $('#btnModalMensRedirigir').attr('href', '/Gores/Index');
            }else{
                $("#txtMensajesFormulario").empty();
                $("#txtMensajesFormulario").html("<p>" + result + "</p>");
                $('#modalMensajesFormularios').modal('show');
                $('#btnModalMensajes').show();
            }
        },
        error: function (xhr, status, error) {
            $("#txtMensajesFormulario").empty();
            $("#txtMensajesFormulario").html("<p>" + error + "</p>");
            $('#modalMensajesFormularios').modal('show');
            console.log(error);
        },
        complete: function () { $("#cargaPagina").fadeOut(); }
    };
    $.ajax(opciones);
}
function guardarRespuestas(){
    $('#guardar').button('loading');
    $("#cargaPagina").fadeIn();
    //Habilito inputs disabled
    var formulario = $('#formularios');
    var disabled = formulario.find(':input:disabled').removeAttr('disabled');        
    var fd = new FormData();
    var respuestas = [];
    $.each($('form').serializeArray({ checkboxesAsBools: true }), function (i, field) {
        var respuesta = {};
        respuesta.idPregunta = field.name.split("_")[0];
        respuesta.idTab = field.name.split("_")[1];
        respuesta.respuesta = field.value;
        respuestas.push(respuesta);
    });
    var data = JSON.stringify(respuestas);
    fd.append('id', queryString('_id'));
    fd.append('data', data);
    //Deshabilito inputs enabled
    disabled.attr('disabled','disabled');
    var opciones = {
        url: ROOT + "Formulario/GuardaDatos",
        data: fd,
        type: "post",
        contentType: false,
        processData: false,
        success: function (result, xhr, status) {
            $("#guardar").button('reset');
            if (result == "ok") {
                validaBlancos(false, 0, 0);
                if($('#936').hasClass('active') && $('#tabHijos985').hasClass('active') && $('#cmd9722').is(':disabled') === false){ 
                    guardarAdmisibilidad();
                }
                else if($('#936').hasClass('active') && $('#tabHijos937').hasClass('active') && !($("#cmb9462").val() == "-1")){ 
                    guardarCalificacion();
                }
                else
                {
                    $("#txtMensajesFormulario").empty();
                    $("#txtMensajesFormulario").html("<p>Guardado exitoso</p>");
                    $('#modalMensajesFormularios').modal('show');  
                }
                
            } else {
                $("#txtMensajesFormulario").empty();
                $("#txtMensajesFormulario").html("<p>" + result + "</p>");
                $('#modalMensajesFormularios').modal('show');
                console.log(result);
            }
        },
        error: function (xhr, status, error) {
            $("#txtMensajesFormulario").empty();
            $("#txtMensajesFormulario").html("<p>" + error + "</p>");
            $('#modalMensajesFormularios').modal('show');
            console.log(error);
        },
        //Verifica si la respuesta de admisibilidad es si para habilitar la pestaña Perfil de Evaluación GORE
        complete: function () { $("#cargaPagina").fadeOut(); $("#cmb9722 option:selected").val() == "902" ? $('#tabHijos937').show() : $('#tabHijos937').hide(); }
    };
    $.ajax(opciones);
}
function recargarPagina(){
    $('#btnModalMensRecargar').button('loading');
    location.reload();
}
$(document).ready(function () {    
    btnEnviarEval = 0;
    btnEnviarObs = 0;
    ocultaMenu();
    //$('#btnAyudaMenu').tooltip();    
    //$("#msjAyudaGral").html("<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam aliquet pharetra luctus. Pellentesque ligula risus, consectetur nec vestibulum sed, gravida eu erat. Aenean sodales magna at eleifend ultrices. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Cras ipsum lectus, convallis non pellentesque sit amet, molestie nec tellus. Curabitur vel libero id orci euismod pharetra. In hac habitasse platea dictumst. Sed imperdiet libero nec mi efficitur faucibus. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. In et nisi pretium, mollis enim sed, fermentum tortor. Nunc nec lacinia quam. Curabitur varius dolor at leo tincidunt, vitae facilisis sem congue. Phasellus gravida suscipit dolor at dapibus. Donec pulvinar laoreet libero, vel euismod quam malesuada eu. Morbi consectetur libero id interdum sollicitudin.<br> Mauris condimentum condimentum ex sagittis sagittis. Etiam sed metus vulputate, interdum diam ut, tempus nulla. Nullam volutpat tristique finibus. Aenean consequat maximus rutrum. Nullam semper, nibh suscipit consectetur elementum, mauris lorem mollis massa, ac tincidunt lectus augue nec neque. Nam et eros gravida, rutrum velit id, ultricies arcu. Duis sed felis porttitor justo dictum tempus.<br><br> Quisque ut iaculis erat. Quisque sed orci lectus. Ut eget pretium tellus. Praesent ornare lobortis leo in fringilla. Morbi sed lectus et turpis bibendum semper nec sed libero. Duis vel rutrum nisl. Etiam feugiat, erat ac dictum tempus, lacus ante malesuada lorem, sit amet cursus turpis eros nec mauris. Ut at aliquet lorem. Integer sodales turpis a laoreet ornare. Phasellus faucibus, ante vitae sodales mollis, sapien eros egestas urna, nec sagittis arcu mi at erat. Sed nec ornare turpis. Praesent sed nibh vitae velit egestas pellentesque ac nec ipsum. Sed vestibulum, mi at faucibus sodales, nunc risus ullamcorper neque, consectetur fringilla elit purus nec mauris. Fusce iaculis dapibus nulla ut ultricies. Nam fermentum ornare purus ut egestas. Mauris ac justo justo.</p>");
    $("#aMenuGeneral").removeAttr("href").css("cursor", "pointer");
    $("#aMenuGeneral").click(ocultaMenu);
    var menu = agregaMenuPrincipal();    
    //var respuestas = buscaRespuestas();    
    var excepciones = buscaExcepciones();
    creaFormulario(menu, respuestas, excepciones);
    (async() => { await funciones(respuestas); })();
    //funciones(respuestas);
    //validaBlancos(true);
    $('#btnModalMensRecargar').hide();
    $('#btnModalMensRedirigir').hide();
    $("#guardar").click(function () {
        //Validacion Evaluacion - Admisibilidad Gore
        if ($('#936').hasClass('active') && $('#tabHijos985').hasClass('active') && $('#cmd9722').is(':disabled') === false) {
            var tipoAdmisibilidad = $('#cmb9722').val() == "902" ? '<p>Al responder <b>Si</b>, se habilitarán las preguntas de la pestaña <b>Perfil</b> en el módulo de evaluación.</p><p style="margin-bottom: 0;">¿Continuar?</p>' : '<p>Al responder <b>No</b>, se enviará un correo a DIPRES notificando la respuesta, los campos se bloquearán y el estado del formulario cambiará a <b>Perfil en verificación</b>.</p><p style="margin-bottom: 0;">¿Continuar?</p>';
            $('#respuestaAdmisibilidad').val($('#cmb9722').val());
            $('#txtModalAdmisibilidad').html(tipoAdmisibilidad);
            if ($("#cmb9722").val() == "902") { 
                $('#modalEnviarAdmisibilidad').modal('show');
            } else if($("#cmb9722").val() == "901") { 
                var validador = true;
                if ($("#cmb9723 option:selected").val() == "29416" || $("#cmb9723 option:selected").val() == "-1") { validador = false; }
                if ($("#cmb9723 option:selected").val() == "29416") { $("#txt9724").val() === "" ? validador = false : validador = true ; }
                if(validador){ $('#modalEnviarAdmisibilidad').modal('show'); } else { 
                    $("#txtMensajesFormulario").empty();
                    $("#txtMensajesFormulario").html("<p>Completa todas las preguntas para continuar</p>");
                    $('#modalMensajesFormularios').modal('show');
                }
            }
        }
        //Validacion Evaluacion - Perfil Gore
        else if($('#936').hasClass('active') && $('#tabHijos937').hasClass('active') && $("#cmb9462 option:selected").val() != "-1"){
            let valido = true;
            // Recorre el rango de select desde #cmb9450 hasta #cmb9460
            for (let i = 9450; i <= 9460; i += 2) {
                let combo = $('#cmb' + i);
                if (combo.val() === '-1') { valido = false; break; }
            }
            // Recorre el rango de campos de texto desde #txt9451 hasta #txt9461
            for (let i = 9451; i <= 9461; i += 2) {
                let campoTexto = $('#txt' + i);
                if (campoTexto.val().trim() === '') { valido = false; break; }
            }
            if(valido){
                var textoModalCalificacion = '<p>Al seleccionar <b>"' + $('#cmb9462 option:selected').text() + '"</b>:</p>';
                $('#respuestaCalificacion').val($('#cmb9462').val());
                switch ($('#cmb9462').val()) {
                    case "29420":
                        textoModalCalificacion += '<p>Aceptando iniciará el proceso de creación del <b>Programa GORE</b>.</p><p>Se enviará un correo a la <b>COG</b> con el ID y reporte del perfil, además <b>se asignará el permiso para habilitar a los formuladores</b>.</p>'
                        break;
                    case "29421":
                        textoModalCalificacion += '<p>Se informará a la <b>COG</b> por correo que el perfil volverá a la etapa <b>En corrección del Servicio</b>, para revisar y comprobar el formulario nuevamente.</p>'
                        break;
                    case "29422":
                        textoModalCalificacion += '<p>Se notificará por correo con su respectivo reporte a la <b>COG</b>, posteriormente la plataforma cambiará su etapa a <b>Cierre Perfil</b>.</p>'
                        break;
                    default:
                }
                $('#txtModalCalificacion').html(textoModalCalificacion);
                $('#modalEnviarCalificacion').modal('show');
            }else{
                $("#txtMensajesFormulario").empty();
                $("#txtMensajesFormulario").html("<p>Completa todos los campos de perfil antes de calificar.</p>");
                $('#modalMensajesFormularios').modal('show');
            }
        }
        //else if($('#936').hasClass('active') && $('#tabHijos937').hasClass('active') && $("#cmb9462 option:selected").val() != "-1"){
        //}
        //Flujo regular al guardar
        else
        {
            guardarRespuestas();
        }
    });
    $("#btnModalMensRecargar").click(function () {
        recargarPagina();
    });
    // Boton envio admisibilidad gore
    $("#btnModalEnviarAdmisibilidad").click(function () {
        guardarRespuestas();
    });
    // Boton envio calificación gore
    $("#btnModalEnviarCalificacion").click(function () {
        guardarRespuestas();
    });
    $("#btnEnviarEvaluar").click(function () {
        $('#modalFinRevision').modal('show');
    });
    $('input[name="cbkMarcaTodos"]').click(function () {
        if ($(this).is(':checked')) {
            $('.cbkTodos' + $(this).val()).prop('checked', true);
        }else{
            $('.cbkTodos' + $(this).val()).prop('checked', false);
        }        
    });
    $("#btnFinRevAcep").click(function () {
        var valido = 1;
        if ($("#iterar:radio").is(':checked')) {
            var pregIterar = 0;
            $.each($('input[name = "cbkPregIterar"]'), function (i, item) {
                if (item.checked) {
                    pregIterar = 1;
                    return;
                }
            });
            if (!pregIterar) {
                valido = 0;
                $("#txtMensajesFormulario").empty();
                $("#txtMensajesFormulario").html("<p>Seleccione al menos una pregunta a iterar</p>");
                $('#modalMensajesFormularios').modal('show');
            }            
        }
        if ($("#evaluar:radio").is(':checked')){
            var indicComp = 1;
            for (var i = 0; i <= $("#cmb6834 option:selected").val(); i++){
                if ($("#Tabcmb9675" + i.toString() + " option:selected").val() == "-1"){
                    indicComp = 0;
                }
            }
            for (var i = 0; i <= $("#cmb3036 option:selected").val(); i++){
                if ($("#Tabcmb9676" + i.toString() + " option:selected").val() == "-1"){
                    indicComp = 0;
                }
            }
            if (!indicComp){
                valido = 0;
                $("#txtMensajesFormulario").empty();
                $("#txtMensajesFormulario").html("<p>Recuerde que debe seleccionar si sus indicadores de propósito y complementarios son comparables o no.</p>");
                $('#modalMensajesFormularios').modal('show');
            }
        }
        if (valido) {
            $(this).button('loading');
            var preguntasIterar = [];
            //var preguntasAnidadas = [{idPadre: 7740, hijos: [2402]},{idPadre: 7538, hijos: [3006,3007]}, {idPadre: 7539, hijos: [7540, 7541, 7542, 7543, 7544]}, {idPadre: 50, hijos: [22,23,24,25,7741,1594]}, { idPadre: 55, hijos: [3009,7413,7547]}, { idPadre: 7861, hijos: [8244]}, { idPadre: 1177, hijos: [7554,7735,7736,7738,7743,8803,8804]}, { idPadre: 7739, hijos: [7269]}, { idPadre: 7210, hijos: [7218,7219,7220,7221]}, { idPadre: 7235, hijos: [6953,6955,6956,7238,6957,7581,6958,8247,7582]}, { idPadre: 82, hijos: [1339,1342,7760]}, { idPadre: 7586, hijos: [95,2074,2076,7588]}, { idPadre: 3075, hijos: [8877,3079,3080,3081,3082,3083,3084,3085,3086,3087,3088,3089,3090,3091,3092,3093,3094,3095,8878,8881,8882,8883,8884,8885,8886,8887,8888,8889,8890,8891,8892,8893,8894,8895,8896,8897,8899]}, { idPadre: 3074, hijos : [3097,3098,3100,3101,6970,3102,3104,3108,3110,6969,3103,3105,3109,3111,6968,3112]}, { idPadre: 7762, hijos : [7763,7764,7765,7766,7767,7768,7769,7770]}, { idPadre: 7626, hijos: [7771,7772,7773,7774,7775,7776,7777,7778]}, { idPadre: 8260, hijos: [7685,7686,7689,7690,7751,7752,7753,7754,8770]}, { idPadre: 7324, hijos: [7701,7702,7703,7704,7705,7706,7707,7708,7709,7710,7711,7712,7713,7714,7715,7716,7717,7718,7719,7720,7721,7722,7723,7724,7725,7726,7727,7728] }];
            $.each($('input[name = "cbkPregIterar"]'), function (i, item) {
                if (item.checked) {
                    preguntasIterar.push(item.value);
                }
            });
            var radios = ($("#iterar:radio").is(':checked') ? 1 : 2);            
            var opciones = {
                url: ROOT + "Formulario/IterarEvaluar",
                data: { _id: queryString('_id'), pregIterar: preguntasIterar, opcion: radios, tipoPrograma: tipoPrograma },
                type: "post",
                datatype: "json",
                async: false,
                beforeSend: function () { $("#cargaPagina").fadeIn(); },
                success: function (result, xhr, status) {
                    $("#btnFinRevAcep").button('reset')
                    if (result == "ok") {                        
                        window.location.href = ROOT + "Programa/Programas";
                    } else {
                        $("#txtMensajesFormulario").empty();
                        $("#txtMensajesFormulario").html("<p>" + result + "</p>");
                        $('#modalMensajesFormularios').modal('show');
                        console.log(result);
                    }
                },
                error: function (xhr, status, error) {
                    $("#txtMensajesFormulario").empty();
                    $("#txtMensajesFormulario").html("<p>" + error + "</p>");
                    $('#modalMensajesFormularios').modal('show');
                    console.log(error);
                },
                complete: function () { $("#cargaPagina").fadeOut(); }
            };
            $.ajax(opciones);
        }
    });
    /*$("#btnAyudaMenu").click(function () {
        $('#modalGblAyuda').modal('show');
    });*/
    $("#btnEnviar").click(function () {
        $('#modalMensajesEnviar').modal('show');
    });
    $("#btnMsjEnviarAcep").click(function () {
        $(this).button('loading');
        var opciones = {
            url: ROOT + "Formulario/revisionAnalista",
            data: { _id: queryString('_id') },
            type: "post",
            datatype: "json",
            async: false,
            beforeSend: function () { $("#cargaPagina").fadeIn(); },
            success: function (result, xhr, status) {
                if (result) {   
                    window.location.reload();
                } else {
                    $("#txtMensajesFormulario").empty();
                    $("#txtMensajesFormulario").html("<p>" + result + "</p>");
                    $('#modalMensajesFormularios').modal('show');
                    console.log(result);
                }
            },
            error: function (xhr, status, error) {
                $("#txtMensajesFormulario").empty();
                $("#txtMensajesFormulario").html("<p>" + error + "</p>");
                $('#modalMensajesFormularios').modal('show');
                console.log(error);
            }
        };
        $.ajax(opciones);
    });
    //Ingresar comentarios
    $("#btnNuevoComent").click(function(){
        $("#divTextoComentarios").css("display", "");
        $("#btnIngComent").css("display", "");
        $(this).css("display", "none");
    });
    $("#btnIngComent").click(function () {
        $(this).button('loading');
        var coment = txtComentariosInstancia.getData();
        var menuPadre = $("#idMenuPadre").val();
        var menuHijo = $("#idMenuHijo").val();
        var idpregunta = $("#idPregunta").val();
        var idtab = $("#idTab").val();
        var idConsulta = $("#idConsulta").val();
        var opciones = {
            url: ROOT + "Formulario/ingresaComentario",
            data: { _id: queryString('_id'), idMenuP: menuPadre, idMenuH: menuHijo, idPreg: idpregunta, idT: idtab, idConsulta: idConsulta, comentario: coment },
            type: "post",
            datatype: "json",
            async: false,
            beforeSend: function () { $("#cargaPagina").fadeIn(); },
            success: function (result, xhr, status) {
                $("#btnIngComent").button('reset');
                $("#modalComentarios").modal('hide');
                if (result.IdPrograma > 0) {
                    $("#txtMensajesFormulario").empty();
                    $("#txtMensajesFormulario").html("<p>Comentario creado exitosamente</p>");
                    $('#modalMensajesFormularios').modal('show');
                    comentarioschat.server.comentariosChat(result);
                }else{
                    $("#txtMensajesFormulario").empty();
                    $("#txtMensajesFormulario").html("<p>Error al ingresar comentarios</p>");
                    $('#modalMensajesFormularios').modal('show');
                }
            },
            error: function (xhr, status, error) {
                $("#txtMensajesFormulario").empty();
                $("#txtMensajesFormulario").html("<p>" + error + "</p>");
                $('#modalMensajesFormularios').modal('show');
                console.log(error);
            },
            complete: function () { $("#cargaPagina").fadeOut(); }
        };
        $.ajax(opciones);
    });
    $("#btnSalir").click(function () {
        if (tipoAcceso == accesoFormGuardado) {
            $('#modalMensajesSalir').modal('show');
        } else {
            window.location.href = ROOT + "Programa/Programas";
        }        
    });
    $("#btnMensjAceptar").click(function () {
        $(this).button('loading');
        var opciones = {
            url: ROOT + "Formulario/SalirPrograma",
            data: { _id: queryString('_id') },
            type: "post",
            datatype: "json",
            async: false,
            beforeSend: function () { $("#cargaPagina").fadeIn(); },
            success: function (result, xhr, status) {
                $("#btnMensjAceptar").button('reset');
                window.location.href = ROOT + "Programa/Programas";
            },
            error: function (xhr, status, error) {
                $("#txtMensajesFormulario").empty();
                $("#txtMensajesFormulario").html("<p>" + error + "</p>");
                $('#modalMensajesFormularios').modal('show');
                console.log(error);
            },
            complete: function () { $("#cargaPagina").fadeOut(); }
        };
        $.ajax(opciones);
    });
    //Ingresar respuesta
    $("#txtRespuestas").keyup(function () {
        var totalCaracteres = 1500;
        var restantes = (totalCaracteres - $(this).val().length) < 0 ? 0 : (totalCaracteres - $(this).val().length);
        $("#caractRespuestas").text("Caracteres restantes=" + restantes);
    });
    /*$("#btnIngResp").click(function () {
        $(this).button('loading');
        var opciones = {
            url: ROOT + "Formulario/ingresaRespuesta",
            data: { idConsulta: $("#txtIdConsulta").val(), respuesta: $.trim($("#txtRespuestas").val()) },
            type: "post",
            datatype: "json",
            async: false,
            beforeSend: function () { $("#cargaPagina").fadeIn(); },
            success: function (result, xhr, status) {
                $("#btnIngResp").button('reset');
                $("#modalRespuestasConsultas").modal('hide');
                if (result == "ok") {   
                    $("#txtMensajesFormulario").empty();
                    $("#txtMensajesFormulario").html("<p>Respuesta enviada exitosamente</p>");
                    $('#modalMensajesFormularios').modal('show');
                    window.location.reload();
                }else{
                    $("#txtMensajesFormulario").empty();
                    $("#txtMensajesFormulario").html("<p>" + result + "</p>");
                    $('#modalMensajesFormularios').modal('show');
                }
            },
            error: function (xhr, status, error) {
                $("#txtMensajesFormulario").empty();
                $("#txtMensajesFormulario").html("<p>" + error + "</p>");
                $('#modalMensajesFormularios').modal('show');
                console.log(error);
            }
        };
        $.ajax(opciones);
    });*/
    //Boton informes
    $("#btnDescargaInforme").click(function () { 
        $('#modalDescargaInformes').modal('show');
    });
    //Boton envio evaluacion
    $("#btnEnvioEvaluacion").click(function () { 
        $('#modalEnvioEvaluacion').modal('show');
    });
    $("#btnEnvioSect").click(function () { 
        $(this).button('loading');
        var opciones = {
            url: ROOT + "Formulario/EnvioEvaluacion",
            data: { _id: queryString('_id'), tipo: 2 },
            type: "post",
            datatype: "json",
            async: false,
            beforeSend: function () { $("#cargaPagina").fadeIn(); },
            success: function (result, xhr, status) {
                $("#btnEnvioSect").button('reset');
                $("#modalEnvioEvaluacion").modal('hide');
                if (result == "ok") {   
                    $("#txtMensajesFormulario").empty();
                    $("#txtMensajesFormulario").html("<p>Evaluación enviada exitosamente</p>");
                    $('#modalMensajesFormularios').modal('show');
                }else{
                    $("#txtMensajesFormulario").empty();
                    $("#txtMensajesFormulario").html("<p>" + result + "</p>");
                    $('#modalMensajesFormularios').modal('show');
                }
            },
            error: function (xhr, status, error) {
                $("#txtMensajesFormulario").empty();
                $("#txtMensajesFormulario").html("<p>" + error + "</p>");
                $('#modalMensajesFormularios').modal('show');
                console.log(error);
            },
            complete: function () { $("#cargaPagina").fadeOut(); }
        };
        $.ajax(opciones);
    });
    $("#btnEnvioJef").click(function () { 
        $(this).button('loading');
        var opciones = {
            url: ROOT + "Formulario/EnvioEvaluacion",
            data: { _id: queryString('_id'), tipo: 1, tipoPrograma: tipoPrograma },
            type: "post",
            datatype: "json",
            async: false,
            beforeSend: function () { $("#cargaPagina").fadeIn(); },
            success: function (result, xhr, status) {
                $("#btnEnvioJef").button('reset');
                $("#modalEnvioEvaluacion").modal('hide');
                if (result == "ok") {   
                    $("#txtMensajesFormulario").empty();
                    $("#txtMensajesFormulario").html("<p>Evaluación enviada exitosamente</p>");
                    $('#modalMensajesFormularios').modal('show');
                    window.location.reload();
                }else{
                    $("#txtMensajesFormulario").empty();
                    $("#txtMensajesFormulario").html("<p>" + result + "</p>");
                    $('#modalMensajesFormularios').modal('show');
                }
            },
            error: function (xhr, status, error) {
                $("#txtMensajesFormulario").empty();
                $("#txtMensajesFormulario").html("<p>" + error + "</p>");
                $('#modalMensajesFormularios').modal('show');
                console.log(error);
            },
            complete: function () { $("#cargaPagina").fadeOut(); }
        };
        $.ajax(opciones);
    });
    $("#btnMsjConfirm").click(function () {
        $(this).button('loading');
        var opciones = {
            url: ROOT + "Formulario/BorrarComentario",
            data: { idConsulta: $("#borraIdConsulta").val() },
            type: "post",
            datatype: "json",
            async: false,
            beforeSend: function () { $("#cargaPagina").fadeIn(); },
            success: function (result, xhr, status) {
                $("#btnMsjConfirm").button('reset');
                //$("#modalComentarios").modal('hide');
                $("#modalMsjConfirm").modal('hide');
                if (result == "ok") {
                    $("#txtMensajesFormulario").empty();
                    $("#txtMensajesFormulario").html("<p>Comentario eliminado exitosamente</p>");
                    $('#modalMensajesFormularios').modal('show');
                    comentarioschat.server.borraComentariosChat($("#borraIdPrograma").val(), $("#borraIdUsuario").val(), $("#borraIdMenuHijo").val(), $("#borraIdMenuPadre").val(), $("#borraIdPregunta").val());
                }else{
                    $("#txtMensajesFormulario").empty();
                    $("#txtMensajesFormulario").html("<p>" + result + "</p>");
                    $('#modalMensajesFormularios').modal('show');
                }
            },
            error: function (xhr, status, error) {
                $("#txtMensajesFormulario").empty();
                $("#txtMensajesFormulario").html("<p>" + error + "</p>");
                $('#modalMensajesFormularios').modal('show');
                console.log(error);
            },
            complete: function () { $("#cargaPagina").fadeOut(); }
        };
        $.ajax(opciones);
    });
});