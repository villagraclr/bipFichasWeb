//Valida archivos subidos
function validaArchivo(objeto){
    var archivo = objeto.value;
    var ext = archivo.split('.').pop();
    if(ext != "pdf" ){
        $("#txtMensajesFormulario").empty();
        $("#txtMensajesFormulario").html("<p>El tipo de archivo seleccionado no es válido</p>");
        $('#modalMensajesFormularios').modal('show');
        return false;
    }
}
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
//Funcion solo numeros
function soloNumeros(e, caractEspec) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letrasEspeciales = caractEspec == true ? "," : "";
    letras = "0123456789-" + letrasEspeciales;
    especiales = [8, 37, 39, 9];
    tecla_especial = false;
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}
function retornaValorFormateado(valor) {
    var toNum;
    toNum = valor == "" ? 0 : quitaPuntos(valor).replace(",", ".");
    return (parseFloat(toNum));
}
function quitaPuntos(numero) {
    var numeroFormateado = numero + '';
    for (var a = 0; a < numeroFormateado.length; a++) {
        if (numeroFormateado.indexOf('.') > 0)
            numeroFormateado = numeroFormateado.replace('.', '');

        if (numeroFormateado.length == a - 1) break;
    }
    return numeroFormateado;
}
$("#formularios").submit(function (event) {
    var inputID = $("<input>").attr("type", "hidden").attr("name", "_id").val(queryString('_id'));
    var idMenuPadre = $("#aMenuForm li.active").attr('id');
    var inputTAB = $("<input>").attr("type", "hidden").attr("name", "_tab").val(idMenuPadre);
    var inputTABFORM = $("<input>").attr("type", "hidden").attr("name", "_tabForm").val($("#ulForm" + idMenuPadre + " li.active").attr('id'));    
    $("#formularios").append($(inputID));
    $("#formularios").append($(inputTAB));
    $("#formularios").append($(inputTABFORM));
    if (btnEnviarEval == 1) {
        var inputENVIAR = $("<input>").attr("type", "hidden").attr("name", "_enviar").val(btnEnviarEval);
        $("#formularios").append($(inputENVIAR));
    }
    if (btnEnviarObs == 1 || btnEnviarObs == 2 || btnEnviarObs == 3 || btnEnviarObs == 4 || btnEnviarObs == 5) {
        var inputENVIAROBS = $("<input>").attr("type", "hidden").attr("name", "_enviarObs").val(btnEnviarObs);
        $("#formularios").append($(inputENVIAROBS));
    }
    $("#cargaPagina").fadeIn();
    //event.preventDefault();
});
$("#salirForm").submit(function (event) {
    var inputID = $("<input>").attr("type", "hidden").attr("name", "_idSalida").val(queryString('_id'));
    $("#salirForm").append($(inputID));
    //event.preventDefault();
});
$("#guardar").click(function () {
    $(this).button('loading');
    btnEnviarEval = 0;
    btnEnviarObs = 0;
    $("#formularios").submit();
});
$("#btnMensjAceptar").click(function () {
    $("#salirForm").submit();
});
$("#btnSalir").click(function () {
    var inputID = $("<input>").attr("type", "hidden").attr("name", "_idSalida").val(queryString('_id'));
    $("#salirForm").append($(inputID));
    if (tipoAcceso == accesoFormGuardado) {
        $('#modalMensajesSalir').modal('show');
    } else {
        $("#salirForm").submit();
    }
});
$("#btnEnviar").click(function () {
    /*if (!validaDatos('validaDatos')) {*/
        if (tipoAcceso == accesoFormGuardado) {
            $('#modalMensajesEnviar').modal('show');
        }
    /*} else {
        $("#txtMensajesFormulario").empty();
        $("#txtMensajesFormulario").html("<p>Para poder enviar el formulario a evaluación deben estar todos los campos completos.</p>");
        $('#modalMensajesFormularios').modal('show');
    }*/
});
$("#btnMsjEnviarAcep").click(function () {
    $("#btnEnviar").button('loading');
    btnEnviarEval = 1;
    $("#formularios").submit();
});
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
function ocultaTabs(idObjetoDep, numTab, partida) {
    var tabs = $("#ul" + idObjetoDep + " li");
    tabs.removeClass("active");
    tabs.css('display', 'block');
    for (i = numTab; i < tabs.length; i++) {
        tabs[i].style.display = 'none';
    }
    if (partida) {
        $("#ul" + idObjetoDep + " li:eq(0) a").trigger('click');
    } else {
        $("#ul" + idObjetoDep + " li:eq(" + parseInt(numTab - 1) + ") a").trigger('click');
    }
}

function validaDatos(clase) {
    var validacion = 0;
    $("." + clase).each(function (i, item) {
        if ($.trim($(this).val()) == "") {
            validacion = 1;
            return false;
        }
    });
    return validacion;
}
$(document).ready(function () {
    btnEnviarEval = 0;
    btnEnviarObs = 0;
    ocultaMenu();
    $("#aMenuGeneral").removeAttr("href").css("cursor", "pointer");
    $("#aMenuGeneral").click(ocultaMenu);
    ocultaTabs();
    //Agrego tabs
    var tabs = '<ul id="aMenuForm" class="nav nav-pills nav-stacked">';
    $.each(menuPadres, function (i, item) {
        tabs += '<li role="presentation" id="' + item.IdMenu + '"' + ((tab == item.IdMenu || tab == 0 && item.Orden == 1) ? "class=active" : "") + '><a href="#tabForm' + (item.Orden) + '" data-toggle="pill">' + item.TipoMenu + '</a></li>';        
    });
    $(".sidebar-nav").append('<ul id="aMenuForm" class="nav nav-pills nav-stacked">' + tabs + '</ul>');
    //funcionesForm();
    $("#btnFinalizarRevision").click(function () {
        $('#modalMensajeFinalizarRevision').modal('show');
    });
    $("#btnMsjGuardParcial").click(function () {
        $(this).button('loading');
        btnEnviarObs = 3;
        $("#formularios").submit();
    });
    $("#btnMsjFinalizarRev").click(function () {
        $("#modalMensajeFinalizarRevision").modal('hide');
        $("#cmbOpcObservaciones option[value='-1']").prop('selected', true);
        $('#modalFinRevision').modal('show');
    });
    $("#btnFinRevAcep").click(function () {
        if ($("#cmbOpcObservaciones").val() != "-1") {
            if ($("#cmbOpcObservaciones").val() == "1") {
                $("#btnFinRevAcep").button('loading');
                btnEnviarObs = 1;
                $("#formularios").submit();
            } else if ($("#cmbOpcObservaciones").val() == "2") {
                $("#btnFinRevAcep").button('loading');
                btnEnviarObs = 2;
                $("#formularios").submit();
            } else if ($("#cmbOpcObservaciones").val() == "3") {
                $("#btnFinRevAcep").button('loading');
                btnEnviarObs = 4;
                $("#formularios").submit();
            }
        } else {
            $("#txtMensajesFormulario").empty();
            $("#txtMensajesFormulario").html("<p>Seleccione una opción</p>");
            $('#modalMensajesFormularios').modal('show');
        }
    });
    $("#btnEnviarEvaluar").click(function () {
        $('#modalMensajesEvaluar').modal('show');
    });
    $("#btnMsjEvalAcep").click(function () {
        $(this).button('loading');
        $("#cargaPagina").fadeIn();
        var opciones = {
            url: ROOT + "Formulario/EtapaEvaluacion",
            data: { _id: queryString('_id') },
            type: "post",
            datatype: "json",
            success: function (result, xhr, status) {
                if (result == "ok") {
                    //window.location.href = ROOT + "Programa/Programas";
                    location.reload();
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
    //Textos evaluacion
    if (preguntasEval.length > 0) {
        $.each(preguntasEval, function (i, item) {
            if (item.IdParametro != item.idCategoria) {
                $("#" + item.IdPreguntaDesc).prop('readonly', false).prop('disabled', false);
            }
        });
    }
    $("#btnGuardarEvaluacion").click(function () {
        $(this).button('loading');
        var id = queryString('_id');
        var datos = [];
        //Peguntas evaluación
        if (preguntasEval.length > 0) {
            $.each(preguntasEval, function (i, item) {
                if (item.IdParametro != item.idCategoria) {
                    dato = {};
                    dato.IdPregunta = item.IdPregunta;
                    dato.IdTab = item.IdTab;
                    if (item.IdPreguntaDesc.substring(3, 6) == "cmb"){
                        dato.Respuesta = $("#" + item.IdPreguntaDesc + " option:selected").val();
                    }else if (item.IdPreguntaDesc.substring(3, 6) == "cbx"){
                        dato.Respuesta = ($("#" + item.IdPreguntaDesc).is(":checked") ? "on" : "");
                    }else{
                        dato.Respuesta = $("#" + item.IdPreguntaDesc).val();
                    }
                    datos.push(dato);
                }
            });
            var opciones = {
                url: ROOT + "Formulario/GuardaEvaluacion",
                data: { id: id, data: datos },
                type: "post",
                datatype: "json",
                success: function (result, xhr, status) {
                    $("#btnGuardarEvaluacion").button('reset');
                    if (result == "ok") {
                        $("#txtMensajesFormulario").empty();
                        $("#txtMensajesFormulario").html("<p>Guardado exitoso</p>");
                        $('#modalMensajesFormularios').modal('show');
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
        }
    });
});