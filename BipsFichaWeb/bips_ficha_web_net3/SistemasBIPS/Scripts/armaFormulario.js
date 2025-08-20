//Constantes
const titulo = 407;
const titulo2 = 434;
const tabla = 413;
const anos = 415;
const etiqueta = 416;
const vacio = 414;
const etiquetaEspejo = 423;
const texto = 401;
const largoTexto = 601;
const textoReadonly = 422;
const numerico = 418;
const numericoSuma = 420;
const total = 421;
const textoGrande = 406;
const combo = 402;
const seleccione = 403;
const seleccioneVacio = 603;
const comboNumerico = 404;
const comboDinamico = 410;
const totalValidacion = 427;
const grupoCheckPadre = 425;
const tabObj = 405;
const tabEvaluacion = 435;
const grupoCheckHijo = 426;
const checkbox = 409;
const inputCheck = 417;
const largoNumerico = 606;
const grupoCheck = 424;
const inputBloquedo = 661;
const checkboxVacio = 429;
const checkboxUnico = 728;
const checkboxInline = 433;
const inputValidaciones = 436;
const etapaRevAnalista = 327;
const tabIndicProp = 7603;
const tabIndicComp = 7624;
const rolesHiloComent = [1,2,3,7];

function creaFormulario(menu, respuestas, excepciones) {
    if (menu.length > 0) {
        var indPreguntas = 0;
        var indObservaciones = 0;
        $.each(menu, function (i, item) {
            $("#principal").append('<div class="tab-pane fade' + ((tab == item.menuPadre.IdMenu || tab == 0 && item.menuPadre.Orden == 1 || tab == 0 && item.Orden == item.menuPadre.Orden) ? " in active" : "") + '" id="tabForm' + item.menuPadre.Orden + '"><div class="row"><ul class="nav nav-tabs" id="ulForm' + item.menuPadre.IdMenu + '"></ul><div class="tab-content" id="content' + item.menuPadre.IdMenu + '">');
            if (item.menuHijo.length > 0) {
                $.each(item.menuHijo, function (i, itemHijo) {
                    let values = item.menuHijo.map(function (v) { return v.menuHijo.Orden; });
                    var minTab = Math.min.apply(null, values);                    
                    if (itemHijo.menuHijo.IdPadre == item.menuPadre.IdTipoMenu) {                        
                        var coment = (comentarios.length > 0 ? $.grep(comentarios, function (n, i) { return n.IdMenuHijo === itemHijo.menuHijo.IdMenu; }) : null);
                        var msj = (coment != null ? (coment.length > 0 ? coment.length : '') : '');
                        var activaTab = ("tabHijos" + itemHijo.menuHijo.IdMenu.toString() == tabForm ? 1 : 0);
                        $("#ulForm" + item.menuPadre.IdMenu).append('<li role="presentation" id="tabHijos' + itemHijo.menuHijo.IdMenu + '"' + (activaTab > 0 ? (tabForm == ("tabHijos" + itemHijo.menuHijo.IdMenu.toString()) ? "class=active" : "") : (itemHijo.menuHijo.Orden == 1 || itemHijo.menuHijo.Orden == minTab ? "class=active" : "")) + '><a href="#subNivel' + (item.menuPadre.Orden.toString() + itemHijo.menuHijo.Orden.toString()) + '" data-toggle="tab" onclick="validaBlancos(false, ' + item.menuPadre.IdMenu + ',' + itemHijo.menuHijo.IdMenu + ');">' + itemHijo.menuHijo.TipoMenu + '<span id="spnHijo' + itemHijo.menuHijo.IdMenu + '" class="badge" style="display: inline; padding: 0px 7px; margin-left: 5px; background-color: #8400CA;"></span><span id="spnTotalComent' + itemHijo.menuHijo.IdMenu + '" class="badge" style="display: ' + (msj == '' ? "none" : "inline") + '; margin-left: 5px; background-color: #337ab7; color:#fff">' + msj + '</span></a></li>');
                        $("#content" + item.menuPadre.IdMenu).append('<div class="tab-pane fade' + (activaTab > 0 ? (tabForm == ("tabHijos" + itemHijo.menuHijo.IdMenu.toString()) ? " in active" : "") : (itemHijo.menuHijo.Orden == 1 || itemHijo.menuHijo.Orden == minTab ? " in active" : "")) + '" id="subNivel' + (item.menuPadre.Orden.toString() + itemHijo.menuHijo.Orden.toString()) + '">');
                        $("#subNivel" + item.menuPadre.Orden.toString() + itemHijo.menuHijo.Orden.toString()).append('<div class="col-lg-12 anti-padding-RL contenido-form"><div class="panel panel-default tab-sin-borde"><div class="panel-body form-horizontal" id="formHorizontal' + item.menuPadre.Orden.toString() + itemHijo.menuHijo.Orden.toString() + '">');                        
                        //Agrego checkbox global
                        if (etapa == etapaRevAnalista) {
                            if (!$('#divCbk' + item.menuPadre.Orden.toString() + itemHijo.menuHijo.Orden.toString()).length) {
                                var checkGlobal = '<div id="divCbk' + item.menuPadre.Orden.toString() + itemHijo.menuHijo.Orden.toString() + '" class="form-group form-group-sm" style="text-align: right;margin-right: 110px;"><div class="checkbox"><label><input type="checkbox" name="cbkMarcaTodos" value="' + item.menuPadre.IdMenu.toString() + itemHijo.menuHijo.IdMenu.toString() + '"> Marcar todos</label></div></div>';
                                $("#formHorizontal" + item.menuPadre.Orden.toString() + itemHijo.menuHijo.Orden.toString()).append(checkGlobal);
                            }
                        }
                        //agrego preguntas
                        if (itemHijo.preguntas.length > 0) {
                            $.each(itemHijo.preguntas, function (i, pregunta) {
                                agregaPregunta(pregunta, (item.menuPadre.Orden.toString() + itemHijo.menuHijo.Orden.toString()), indPreguntas, respuestas, excepciones, item.menuPadre.IdMenu, itemHijo.menuHijo.IdMenu);
                            });
                        }
                        //Seccion de comentarios                        
                        //var comentarios = '<div class="sep"></div><div class="alert alert-success" style="padding:8px;font-size:13px; border-radius: 4px;" role="alert">Hilos de comentarios/consultas</div><button type="button" class="btn btn-primary btn-sm" onclick="modalComentarios(' + itemHijo.menuHijo.IdMenu + ');">Ingresar comentario/consulta</button>';
                        //$("#formHorizontal" + item.menuPadre.Orden.toString() + itemHijo.menuHijo.Orden.toString()).append(comentarios);
                        //Valido si existen comentarios
                        //agregaComentarios(itemHijo.menuHijo.IdMenu, queryString('_id'), item.menuPadre.Orden.toString() + itemHijo.menuHijo.Orden.toString());
                    }
                });
            }
        });
        $("[data-toggle=tooltip]").tooltip({ html: true });
    }
}
function agregaPregunta(pregunta, id, indPreguntas, respuestas, excepciones, menuPadre, menuHijo) {   
    var respuesta = $.grep(respuestas, function (n, i) { return n.IdPregunta === pregunta.idPregunta; });
    if (pregunta.tipoPregunta.IdParametro.toString() == titulo || pregunta.tipoPregunta.IdParametro.toString() == titulo2) {
        var preguntaTexto = pregunta.pregunta;
        var pregDinamica = (preguntaTexto.includes('#') ? preguntaTexto.split('#') : new String());
        if (pregDinamica.length > 0) {
            var anosDinamicos = 0;
            if (pregDinamica[1].indexOf('-') > 0) {
                anosDinamicos = anoCabecera - parseInt(pregDinamica[1].split('-')[1]);
            } else if (pregDinamica[1].indexOf('+') > 0) {
                anosDinamicos = anoCabecera + parseInt(pregDinamica[1].split('+')[1]);
            } else {
                anosDinamicos = anoCabecera;
            }
            preguntaTexto = pregDinamica[0] + anosDinamicos.toString() + pregDinamica[2];
        }
        $("#formHorizontal" + id).append('<div id="divPreg' + pregunta.idPregunta.toString() + '"><p class="' + (pregunta.tipoPregunta.IdParametro.toString() == titulo ? "bg-info" : "bg-primary") + '" style="padding:8px;font-size:12px;">' + preguntaTexto + '</p></div>');
    } else if (pregunta.tipoPregunta.IdParametro.toString() == tabla) {
        if (pregunta.valor_funcion.length > 0) {
            var filas = 0, columnas = 0;
            var cabeceras = 0;
            filas = parseInt(pregunta.valor_funcion[0].Valor);
            columnas = parseInt(pregunta.valor_funcion[0].Valor2);
            cabeceras = (parseFloat(pregunta.valor_funcion[0].Valor.toString()) - parseFloat(filas));
            cabeceras = ((Math.round(cabeceras * 100) / 100) != 0 ? parseFloat((Math.round(cabeceras * 100) / 100).toString().split('.')[1]) : 0);
            filas = (filas - parseInt(cabeceras.toString()));
            var contenido = '';
            if (cabeceras != 0) {
                contenido += '<thead>';
                for (var f = 1; f <= parseInt(cabeceras.toString()) ; f++) {
                    contenido += '<tr style="text-align: center;">';
                    for (var c = 1; c <= columnas; c++) {
                        if (pregunta.preguntasGrupos.length > 0) {
                            var pregTabla = $.grep(pregunta.preguntasGrupos, function (n, i) { return n.fila === f && n.columna === c; });
                            if (pregTabla.length > 0) {
                                contenido += '<td style="width: ' + Math.round(100 / columnas) + '%">';
                                contenido += tiposPreguntas(pregTabla[0], indPreguntas, 1, respuesta, excepciones, menuPadre, menuHijo);
                                contenido += '</td>';
                            }
                        }
                    }
                    contenido += '</tr>';
                }
                contenido += '</thead>';
            }
            if (filas > 0) {
                contenido += '<tbody>';
                for (var f = (parseInt(cabeceras.toString()) + 1) ; f <= (filas + cabeceras) ; f++) {
                    contenido += '<tr>';
                    for (var c = 1; c <= columnas; c++) {
                        if (pregunta.preguntasGrupos.length > 0) {                         
                            var pregTabla = $.grep(pregunta.preguntasGrupos, function (n, i) { return n.fila === f && n.columna === c; });
                            if (pregTabla.length > 0) {                                
                                contenido += '<td>';
                                $.each(pregTabla, function (t, obj) {                                
                                    respuesta = $.grep(respuestas, function (n, i) { return n.IdPregunta === obj.idPregunta; });
                                    contenido += tiposPreguntas(obj, indPreguntas, 1, respuesta, excepciones, menuPadre, menuHijo);
                                });
                                contenido += '</td>';
                            }
                        }
                    }
                    contenido += '</tr>';
                }
                contenido += '</tbody>';
            }
        }
        $("#formHorizontal" + id).append('<div class="table-responsive" id="divPreg' + pregunta.idPregunta.toString() + '"><table id="tabla' + pregunta.idPregunta + '" class="table table-bordered">' + contenido);
    } else {
        var contenido = '';
        var cssGrupoCheckPadres = (pregunta.tipoPregunta.IdParametro.toString() == grupoCheckPadre ? "margin-bottom: -38px;" : "");
        if (pregunta.tipoPregunta.IdCategoria.toString() == tabObj && pregunta.tipoPregunta.IdParametro.toString() != tabEvaluacion) {
            contenido += '<div class="col-sm-12"><ul id="ultab' + pregunta.idPregunta.toString() + '" class="nav nav-tabs">';
            if (pregunta.valor_funcion.length > 0) {
                for (var x = pregunta.valor_funcion[0].Valor; x <= pregunta.valor_funcion[0].Valor2; x++) {
                    contenido += '<li role="presentation"' + (x == 1 ? "class=active" : '') + '><a href="#tab' + (pregunta.id.toString() + x.toString()) + '" data-toggle="tab">' + (countObjeto(pregunta.valores, 1, x) > 0 ? $.grep(pregunta.valores, function (n, i) { return n.Orden === x && n.IdParametro != n.IdCategoria; })[0].Descripcion : x.toString()) + '</a></li>';
                }
            }
            contenido += '</ul><div id="tab' + pregunta.idPregunta.toString() + '" class="tab-content">';
            if (pregunta.valor_funcion.length > 0) {
                for (var x = pregunta.valor_funcion[0].Valor; x <= pregunta.valor_funcion[0].Valor2; x++) {
                    contenido += '<div class="tab-pane fade' + (x == 1 ? " in active" : "") + '" id="tab' + (pregunta.id.toString() + x.toString()) + '"><div class="panel panel-default tab-sin-borde"><div class="panel-body form-horizontal">';
                    if (pregunta.preguntasGrupos.length > 0) {
                        var listaPreguntas = (pregunta.tipoPregunta.IdParametro.toString() == tabObj ? pregunta.preguntasGrupos : $.grep(pregunta.preguntasGrupos, function (n, i) { return n.valores[0].Valor === x; }));
                        for (var i = 0; i < listaPreguntas.length; i++) {
                            var cssGrupoCheckPadresTab = (listaPreguntas[i].tipoPregunta.IdParametro.toString() == grupoCheckPadre ? "margin-bottom: -38px;" : "");
                            if (listaPreguntas[i].tipoPregunta.IdParametro.toString() == tabla) {
                                contenido += '<div class="table-responsive" id="divPregTab' + (listaPreguntas[i].idPregunta.toString() + x.toString()) + '"><table id="tablaTab' + (listaPreguntas[i].idPregunta.toString() + x.toString()) + '" class="table table-bordered">';
                                if (listaPreguntas[i].valor_funcion.length > 0) {
                                    var filas = 0, columnas = 0;
                                    var cabeceras = 0;
                                    filas = parseInt(listaPreguntas[i].valor_funcion[0].Valor);
                                    columnas = parseInt(listaPreguntas[i].valor_funcion[0].Valor2);
                                    cabeceras = (parseFloat(listaPreguntas[i].valor_funcion[0].Valor.toString()) - parseFloat(filas));
                                    cabeceras = ((Math.round(cabeceras * 100) / 100) != 0 ? parseFloat((Math.round(cabeceras * 100) / 100).toString().split('.')[1]) : 0);
                                    filas = (filas - parseInt(cabeceras.toString()));
                                    //var contenido = '';
                                    if (cabeceras != 0) {
                                        contenido += '<thead>';
                                        for (var f = 1; f <= parseInt(cabeceras.toString()) ; f++) {
                                            contenido += '<tr style="text-align: center;">';
                                            for (var c = 1; c <= columnas; c++) {
                                                if (listaPreguntas[i].preguntasTablas.length > 0) {
                                                    var pregTabla = $.grep(listaPreguntas[i].preguntasTablas, function (n, i) { return n.fila === f && n.columna === c; });
                                                    if (pregTabla.length > 0) {
                                                        pregTabla[0].idTab = x;
                                                        contenido += '<td>';
                                                        contenido += tiposPreguntas(pregTabla[0], indPreguntas, 1, respuesta, excepciones, menuPadre, menuHijo);
                                                        contenido += '</td>';
                                                    }
                                                }
                                            }
                                            contenido += '</tr>';
                                        }
                                        contenido += '</thead>';
                                    }
                                    if (filas > 0) {
                                        contenido += '<tbody>';
                                        for (var f = (parseInt(cabeceras.toString()) + 1) ; f <= (filas + cabeceras) ; f++) {
                                            contenido += '<tr>';
                                            for (var c = 1; c <= columnas; c++) {
                                                if (pregunta.preguntasTablas.length > 0) {
                                                    var pregTabla = $.grep(pregunta.preguntasTablas, function (n, i) { return n.fila === f && n.columna === c; });
                                                    if (pregTabla.length > 0) {
                                                        pregTabla[0].idTab = x;
                                                        var idTabPreg = pregTabla[0].idTab;
                                                        respuesta = $.grep(respuestas, function (n, i) { return n.IdPregunta === pregTabla[0].idPregunta && n.IdTab === idTabPreg; });
                                                        contenido += '<td>';
                                                        contenido += tiposPreguntas(pregTabla[0], indPreguntas, 1, respuesta, excepciones, menuPadre, menuHijo);
                                                        contenido += '</td>';
                                                    }
                                                }
                                            }
                                            contenido += '</tr>';
                                        }
                                        contenido += '</tbody>';
                                    }
                                }
                                contenido += '</table></div>';
                            } else if (listaPreguntas[i].tipoPregunta.IdParametro.toString() == titulo) {
                                var preguntaTexto = listaPreguntas[i].pregunta;
                                var pregDinamica = (preguntaTexto.includes('#') ? preguntaTexto.split('#') : new String());
                                if (pregDinamica.length > 0) {
                                    var anosDinamicos = 0;
                                    if (pregDinamica[1].indexOf('-') > 0) {
                                        anosDinamicos = anoCabecera - parseInt(pregDinamica[1].split('-')[1]);
                                    } else if (pregDinamica[1].indexOf('+') > 0) {
                                        anosDinamicos = anoCabecera + parseInt(pregDinamica[1].split('+')[1]);
                                    } else {
                                        anosDinamicos = anoCabecera;
                                    }
                                    preguntaTexto = pregDinamica[0] + anosDinamicos.toString() + pregDinamica[2];
                                }
                                contenido += '<div id="divPreg' + listaPreguntas[i].idPregunta.toString() + '"><p class="' + (listaPreguntas[i].tipoPregunta.IdParametro.toString() == titulo ? "bg-info" : "bg-primary") + '" style="padding:8px;font-size:12px;">' + preguntaTexto + '</p></div>';
                            } else if (listaPreguntas[i].tipoPregunta.IdCategoria.toString() == tabObj) {                          
                                var idPreguntaTab = (listaPreguntas[i].idPregunta == "6757" ? "6772" : "6860");
                                var idCmbAsignaciones = (listaPreguntas[i].idPregunta == "6757" ? "4255" : "6861");
                                var idPreguntaItem = (listaPreguntas[i].idPregunta == "6757" ? "6773" : "6862");
                                var idPreguntaAsig = (listaPreguntas[i].idPregunta == "6757" ? "6774" : "6863");
                                var idPreguntaGasto = (listaPreguntas[i].idPregunta == "6757" ? "6775" : "6864");
                                var indiceUnico = 1;
                                contenido += '<div class="col-sm-12">';
                                contenido += '<ul id="ultabHijo' + (listaPreguntas[i].idPregunta.toString()) + '" class="nav nav-tabs">';
                                for (var z = 21; z <= 35; z++) {
                                    if (z != 26 && z != 27 && z != 28) {
                                        if (idPreguntaTab == "6860" && (z == 21 || z == 22 || z == 24 || z == 29)) {
                                            contenido += '<li role="presentation" ' + (z == 21 ? "class=active" : "") + '><a href="#tabHijo' + ((listaPreguntas[i].idPregunta.toString() + x.toString()) + z.toString() + x.toString()) + '" data-toggle="tab">Sub. ' + z + '</a></li>';
                                        } else if (idPreguntaTab == "6772") {
                                            contenido += '<li role="presentation" ' + (z == 21 ? "class=active" : "") + '><a href="#tabHijo' + ((listaPreguntas[i].idPregunta.toString() + x.toString()) + z.toString() + x.toString()) + '" data-toggle="tab">Sub. ' + z + '</a></li>';
                                        }
                                    }
                                }
                                contenido += '</ul>';
                                contenido += '<div id="tabHijo' + (listaPreguntas[i].idPregunta.toString()) + '" class="tab-content">';
                                for (var z = 21; z <= 35; z++) {
                                    var ga = (idPreguntaTab == "6860" ? ((z == 21 || z == 22 || z == 24 || z == 29) ? true : false) : false);
                                    if (z != 26 && z != 27 && z != 28) {
                                        var totalAsig = (z == 24 && !ga ? 25 : 15);
                                        if (ga || idPreguntaTab != "6860") {
                                            contenido += '<div class="tab-pane fade ' + (z == 21 ? "in active" : "") + '" id="tabHijo' + ((listaPreguntas[i].idPregunta.toString() + x.toString()) + z.toString() + x.toString()) + '"><div class="panel panel-default tab-sin-borde"><div class="panel-body form-horizontal">';
                                            if ((z == 24 || z == 31 || z == 32 || z == 33) || ga) {
                                                contenido += '<div class="form-group form-group-sm" id="divPregTab' + (idCmbAsignaciones + x.toString() + z.toString()) + '">';
                                                contenido += '<label for="cmb' + (idCmbAsignaciones + x.toString() + z.toString()) + '" class="col-sm-4 control-label">' + (idPreguntaItem == "6862" && z != 24 ? "¿Cantidad de items?" : "¿Cantidad de asignaciones?") + '</label>';
                                                contenido += '<div class="col-sm-6">';
                                                if (etapa == etapaRevAnalista) {
                                                    contenido += '<div class="input-group">';
                                                }
                                                var cmbSoloLectura = (countObjeto(excepciones, 2, parseInt(idCmbAsignaciones)) > 0 ? "" : "disabled");
                                                contenido += '<input type="hidden" name="" value="' + idCmbAsignaciones + '" />';
                                                contenido += '<input type="hidden" id="numTab' + (idCmbAsignaciones + x.toString() + z.toString()) + '" name="" value="' + (x.toString() + z.toString()) + '" />';
                                                contenido += '<input type="hidden" id="idLlave' + (idCmbAsignaciones + x.toString() + z.toString()) + '" value="' + indiceUnico + '" />';                                               
                                                contenido += '<select class="form-control input-sm numTab-' + x.toString() + '" name="' + idCmbAsignaciones + "_" + (x.toString() + z.toString()) + '" style="border-radius:3px;" id="cmb' + (idCmbAsignaciones + x.toString() + z.toString()) + '" ' + cmbSoloLectura + '>';
                                                for (var j = 1; j <= totalAsig; j++) {
                                                    var respuestaCmb = (respuestas.length > 0 ? $.grep(respuestas, function (n, i) { return n.IdPregunta === parseInt(idCmbAsignaciones) && n.IdTab === parseInt(x.toString() + z.toString()); }) : "");
                                                    contenido += '<option value="' + j + '"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == j ? " selected=selected" : "") : "") + '>' + j + '</option>';
                                                }
                                                contenido += '</select>';
                                                if (etapa == etapaRevAnalista) {
                                                    contenido += '<span class="input-group-addon" style="border:0;background-color:transparent;"><input type="checkbox" class="cbkTodos' + menuPadre.toString() + menuHijo.toString() + '" name="cbkPregIterar" style="cursor:pointer;" aria-label="..." value="' + idCmbAsignaciones + '"></span></div>';
                                                }
                                                contenido += '</div></div>';
                                                contenido += '<div class="col-sm-12">';
                                                contenido += '<ul id="ultabNieto' + (x.toString() + z.toString() + idPreguntaTab + indiceUnico.toString()) + '" class="nav nav-tabs">';
                                                for (var y = 1; y <= totalAsig; y++) {
                                                    contenido += '<li role="presentation" ' + (y == 1 ? "class=active" : "") + '><a href="#tabNieto' + (x.toString() + z.toString() + y.toString() + idPreguntaTab + indiceUnico.toString()) + '" data-toggle="tab">' + y + '</a></li>';
                                                }
                                                contenido += '</ul>';
                                                contenido += '<div id="tabNieto' + idPreguntaTab + '" class="tab-content">';
                                                for (var y = 1; y <= totalAsig; y++) {
                                                    contenido += '<div class="tab-pane fade ' + (y == 1 ? "in active" : "") + '" id="tabNieto' + (x.toString() + z.toString() + y.toString() + idPreguntaTab + indiceUnico.toString()) + '"><div class="panel panel-default tab-sin-borde"><div class="panel-body form-horizontal"><div class="form-group form-group-sm" id="divPregTab' + (x.toString() + z.toString() + y.toString() + idPreguntaItem + indiceUnico.toString()) + '">';
                                                    contenido += '<label for="txt' + (x.toString() + z.toString() + y.toString() + idPreguntaItem + indiceUnico.toString()) + '" class="col-sm-4 control-label">Item</label>';
                                                    contenido += '<div class="col-sm-6">';
                                                    if (etapa == etapaRevAnalista) {
                                                        contenido += '<div class="input-group">';
                                                    }
                                                    contenido += '<input type="hidden" name="" value="' + idPreguntaItem + '" />';
                                                    contenido += '<input type="hidden" id="numTab' + (x.toString() + z.toString() + idPreguntaItem) + '" name="" value="' + (x.toString() + z.toString() + y.toString() + indiceUnico.toString()) + '" />';
                                                    if (idPreguntaItem == "6862") {
                                                        contenido += '<select class="form-control input-sm numTab-' + x.toString() + '" name="' + idPreguntaItem + "_" + (x.toString() + z.toString() + y.toString() + indiceUnico.toString()) + '" style="border-radius:3px;" id="cmb' + (x.toString() + z.toString() + y.toString() + idPreguntaItem + indiceUnico.toString()) + '" ' + (countObjeto(excepciones, 2, parseInt(idPreguntaItem)) > 0 ? "" : "disabled") + '>';
                                                        var respuestaCmb = (respuestas.length > 0 ? $.grep(respuestas, function (n, i) { return n.IdPregunta === parseInt(idPreguntaItem) && n.IdTab === parseInt(x.toString() + z.toString() + y.toString() + indiceUnico.toString()); }) : "");
                                                        if (z == 21) {
                                                            contenido += '<option value="1"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 1 ? " selected=selected" : "") : "") + '>01-Personal de Planta</option>';
                                                            contenido += '<option value="2"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 2 ? " selected=selected" : "") : "") + '>02-Personal a Contrata</option>';
                                                            contenido += '<option value="3"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 3 ? " selected=selected" : "") : "") + '>03-Otras Remuneraciones</option>';
                                                            contenido += '<option value="4"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 4 ? " selected=selected" : "") : "") + '>04-Otros Gastos en Personal</option>';
                                                        } else if (z == 22) {
                                                            contenido += '<option value="5"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 5 ? " selected=selected" : "") : "") + '>01-Alimentos y Bebidas</option>';
                                                            contenido += '<option value="6"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 6 ? " selected=selected" : "") : "") + '>02-Textiles, Vestuario y Calzado</option>';
                                                            contenido += '<option value="7"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 7 ? " selected=selected" : "") : "") + '>03-Combustibles y Lubricantes</option>';
                                                            contenido += '<option value="8"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 8 ? " selected=selected" : "") : "") + '>04-Materiales de Uso o Consumo</option>';
                                                            contenido += '<option value="9"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 9 ? " selected=selected" : "") : "") + '>05-Servicios Básicos</option>';
                                                            contenido += '<option value="10"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 10 ? " selected=selected" : "") : "") + '>06-Mantenimiento y Reparaciones</option>';
                                                            contenido += '<option value="11"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 11 ? " selected=selected" : "") : "") + '>07-Publicidad y Difusión</option>';
                                                            contenido += '<option value="12"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 12 ? " selected=selected" : "") : "") + '>08-Servicios Generales</option>';
                                                            contenido += '<option value="13"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 13 ? " selected=selected" : "") : "") + '>09-Arriendos</option>';
                                                            contenido += '<option value="14"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 14 ? " selected=selected" : "") : "") + '>10-Servicios Financieros y de Seguros</option>';
                                                            contenido += '<option value="15"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 15 ? " selected=selected" : "") : "") + '>11-Servicios</option>';
                                                            contenido += '<option value="16"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 16 ? " selected=selected" : "") : "") + '>12-Otros Gastos en Bienes y Servicios de Consumo</option>';
                                                        } else if (z == 24) {
                                                            contenido += '<option value="17"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 17 ? " selected=selected" : "") : "") + '>01-Transf. al sector privado (1-3)</option>';
                                                            contenido += '<option value="18"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 18 ? " selected=selected" : "") : "") + '>02-Al Gobierno Central</option>';
                                                            contenido += '<option value="19"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 19 ? " selected=selected" : "") : "") + '>03-Transf. a otras entidades públicas</option>';
                                                        } else if (z == 29) {
                                                            contenido += '<option value="25"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 25 ? " selected=selected" : "") : "") + '>06-Equipos Informáticos</option>';
                                                            contenido += '<option value="26"' + (respuestaCmb.length > 0 ? (parseInt(respuestaCmb[0].Respuesta.toString()) == 26 ? " selected=selected" : "") : "") + '>07-Programas Informáticos</option>';
                                                        }
                                                        contenido += '</select>';
                                                    } else {
                                                        var respuestaCmb = (respuestas.length > 0 ? $.grep(respuestas, function (n, i) { return n.IdPregunta === parseInt(idPreguntaItem) && n.IdTab === parseInt(x.toString() + z.toString() + y.toString() + indiceUnico.toString()); }) : "");
                                                        contenido += '<input type="text" name="' + idPreguntaItem + "_" + (x.toString() + z.toString() + y.toString() + indiceUnico.toString()) + '" class="form-control input-sm numTab-' + x.toString() + '" style="border-radius:3px;" id="txt' + (x.toString() + z.toString() + y.toString() + idPreguntaItem + indiceUnico.toString()) + '" maxlength="15" onkeypress="return soloNumeros(event,true);" placeholder="Ingrese registro" value="' + (respuestaCmb.length > 0 ? respuestaCmb[0].Respuesta.toString() : "") + '" ' + (countObjeto(excepciones, 2, parseInt(idPreguntaItem)) > 0 ? "" : "readonly") + ' />';
                                                    }
                                                    if (etapa == etapaRevAnalista) {
                                                        contenido += '<span class="input-group-addon" style="border:0;background-color:transparent;"><input type="checkbox" class="cbkTodos' + menuPadre.toString() + menuHijo.toString() + '" name="cbkPregIterar" style="cursor:pointer;" aria-label="..." value="' + idPreguntaItem + '"></span></div>';
                                                    }
                                                    contenido += '</div></div>';
                                                    if (idPreguntaAsig != "6863" || z == 24) {
                                                        var respuestaCmb = (respuestas.length > 0 ? $.grep(respuestas, function (n, i) { return n.IdPregunta === parseInt(idPreguntaAsig) && n.IdTab === parseInt(x.toString() + z.toString() + y.toString() + indiceUnico.toString()); }) : "");
                                                        contenido += '<div class="form-group form-group-sm" id="divPregTab' + (x.toString() + z.toString() + y.toString() + idPreguntaAsig + indiceUnico.toString()) + '">';
                                                        contenido += '<label for="txt' + (x.toString() + z.toString() + y.toString() + idPreguntaAsig + indiceUnico.toString()) + '" class="col-sm-4 control-label">Asignación</label>';
                                                        contenido += '<div class="col-sm-6">';
                                                        if (etapa == etapaRevAnalista) {
                                                            contenido += '<div class="input-group">';
                                                        }
                                                        contenido += '<input type="hidden" name="" value="' + idPreguntaAsig + '" />';
                                                        contenido += '<input type="hidden" id="numTab' + (x.toString() + z.toString() + idPreguntaAsig + indiceUnico.toString()) + '" name="" value="' + (x.toString() + z.toString() + y.toString() + indiceUnico.toString()) + '" />';
                                                        contenido += '<input type="text" name="' + idPreguntaAsig + "_" + (x.toString() + z.toString() + y.toString() + indiceUnico.toString()) + '" class="form-control input-sm numTab-' + x.toString() + '" style="border-radius:3px;" id="txt' + (x.toString() + z.toString() + y.toString() + idPreguntaAsig + indiceUnico.toString()) + '" maxlength="15" onkeypress="return soloNumeros(event,true);" placeholder="Ingrese registro" value="' + (respuestaCmb.length > 0 ? respuestaCmb[0].Respuesta.toString() : "") + '" ' + (countObjeto(excepciones, 2, parseInt(idPreguntaAsig)) > 0 ? "" : "readonly") + ' />';
                                                        if (etapa == etapaRevAnalista) {
                                                            contenido += '<span class="input-group-addon" style="border:0;background-color:transparent;"><input type="checkbox" class="cbkTodos' + menuPadre.toString() + menuHijo.toString() + '" name="cbkPregIterar" style="cursor:pointer;" aria-label="..." value="' + idPreguntaAsig + '"></span></div>';
                                                        }
                                                        contenido += '</div></div>';
                                                    }
                                                    var respuestaCmb = (respuestas.length > 0 ? $.grep(respuestas, function (n, i) { return n.IdPregunta === parseInt(idPreguntaGasto) && n.IdTab === parseInt(x.toString() + z.toString() + y.toString() + indiceUnico.toString()); }) : "");
                                                    contenido += '<div class="form-group form-group-sm" id="divPregTab' + (x.toString() + z.toString() + y.toString() + idPreguntaGasto + indiceUnico.toString()) + '">';
                                                    contenido += '<label for="txt' + (x.toString() + z.toString() + y.toString() + idPreguntaGasto + indiceUnico.toString()) + '" class="col-sm-4 control-label">Gasto ($miles)</label>';
                                                    contenido += '<div class="col-sm-6">';
                                                    if (etapa == etapaRevAnalista) {
                                                        contenido += '<div class="input-group">';
                                                    }
                                                    contenido += '<input type="hidden" name="" value=" ' + idPreguntaGasto + '" />';
                                                    contenido += '<input type="hidden" id="numTab' + (x.toString() + z.toString() + idPreguntaGasto + indiceUnico.toString()) + '" name="" value="' + (x.toString() + z.toString() + y.toString() + indiceUnico.toString()) + '" />';
                                                    contenido += '<input type="text" name="' + idPreguntaGasto + "_" + (x.toString() + z.toString() + y.toString() + indiceUnico.toString()) + '" class="form-control input-sm numTab-' + x.toString() + ' suma' + listaPreguntas[i].idPregunta.toString() + '" style="border-radius:3px;" id="txt' + (x.toString() + z.toString() + y.toString() + idPreguntaGasto + indiceUnico.toString()) + '" maxlength="15" onkeypress="return soloNumeros(event,true);" placeholder="Ingrese registro" value="' + (respuestaCmb.length > 0 ? respuestaCmb[0].Respuesta.toString() : "") + '" ' + (countObjeto(excepciones, 2, parseInt(idPreguntaGasto)) > 0 ? "" : "readonly") + ' />';
                                                    if (etapa == etapaRevAnalista) {
                                                        contenido += '<span class="input-group-addon" style="border:0;background-color:transparent;"><input type="checkbox" class="cbkTodos' + menuPadre.toString() + menuHijo.toString() + '" name="cbkPregIterar" style="cursor:pointer;" aria-label="..." value="' + idPreguntaGasto + '"></span></div>';
                                                    }
                                                    contenido += '</div></div></div></div></div>';
                                                }
                                                contenido += '</div></div>';
                                            } else {
                                                //var respuestaCmb = (respuestas.length > 0 ? $.grep(respuestas, function (n, i) { return n.IdPregunta === parseInt(idPreguntaItem) && n.IdTab === parseInt(x.toString() + z.toString() + indiceUnico.toString()); }) : "");
                                                //contenido += '<div class="form-group form-group-sm" id="divPregTab' + (x.toString() + z.toString() + idPreguntaItem + indiceUnico.toString()) + '">';
                                                //contenido += '<label for="txt' + (x.toString() + z.toString() + idPreguntaItem + indiceUnico.toString()) + '" class="col-sm-4 control-label">Item</label>';
                                                //contenido += '<div class="col-sm-6">';
                                                //if (etapa == etapaRevAnalista){
                                                //    contenido += '<div class="input-group">';
                                                //}
                                                //contenido += '<input type="hidden" name="" value="' + idPreguntaItem + '" />';
                                                //contenido += '<input type="hidden" id="numTab' + (x.toString() + z.toString() + idPreguntaItem + indiceUnico.toString()) + '" name="" value="' + (x.toString() + z.toString() + indiceUnico.toString()) + '" />';
                                                //contenido += '<input type="text" name="' + idPreguntaItem + "_" + (x.toString() + z.toString() + indiceUnico.toString()) + '" class="form-control input-sm numTab-' + x.toString() + '" style="border-radius:3px;" id="txt' + (x.toString() + z.toString() + idPreguntaItem + indiceUnico.toString()) + '" maxlength="15" onkeypress="return soloNumeros(event,true);" placeholder="Ingrese registro" value="' + (respuestaCmb.length > 0 ? respuestaCmb[0].Respuesta.toString() : "") + '" ' + (countObjeto(excepciones, 2, parseInt(idPreguntaItem)) > 0 ? "" : "readonly") + ' />';
                                                //if (etapa == etapaRevAnalista) {
                                                //    contenido += '<span class="input-group-addon" style="border:0;background-color:transparent;"><input type="checkbox" name="cbkPregIterar" style="cursor:pointer;" aria-label="..." value="' + idPreguntaItem + '"></span></div>';
                                                //}
                                                //contenido += '</div></div>';
                                                /*var respuestaCmb = (respuestas.length > 0 ? $.grep(respuestas, function (n, i) { return n.IdPregunta === parseInt(idPreguntaAsig) && n.IdTab === parseInt(x.toString() + z.toString() + indiceUnico.toString()); }) : "");
                                                contenido += '<div class="form-group form-group-sm" id="divPregTab' + (x.toString() + z.toString() + idPreguntaAsig + indiceUnico.toString()) + '">';
                                                contenido += '<label for="txt' + (x.toString() + z.toString() + idPreguntaAsig + indiceUnico.toString()) + '" class="col-sm-4 control-label">Asignación</label>';
                                                contenido += '<div class="col-sm-6">';
                                                if (etapa == etapaRevAnalista) {
                                                    contenido += '<div class="input-group">';
                                                }
                                                contenido += '<input type="hidden" name="" value="' + idPreguntaAsig + '" />';
                                                contenido += '<input type="hidden" id="numTab' + (x.toString() + z.toString() + idPreguntaAsig + indiceUnico.toString()) + '" name="" value="' + (x.toString() + z.toString() + indiceUnico.toString()) + '" />';
                                                contenido += '<input type="text" name="' + idPreguntaAsig + "_" + (x.toString() + z.toString() + indiceUnico.toString()) + '" class="form-control input-sm numTab-' + x.toString() + '" style="border-radius:3px;" id="txt' + (x.toString() + z.toString() + idPreguntaAsig + indiceUnico.toString()) + '" maxlength="15" onkeypress="return soloNumeros(event,true);" placeholder="Ingrese registro" value="' + (respuestaCmb.length > 0 ? respuestaCmb[0].Respuesta.toString() : "") + '" ' + (countObjeto(excepciones, 2, parseInt(idPreguntaAsig)) > 0 ? "" : "readonly") + ' />';
                                                if (etapa == etapaRevAnalista) {
                                                    contenido += '<span class="input-group-addon" style="border:0;background-color:transparent;"><input type="checkbox" name="cbkPregIterar" style="cursor:pointer;" aria-label="..." value="' + idPreguntaAsig + '"></span></div>';
                                                }
                                                contenido += '</div></div>';*/
                                                var respuestaCmb = (respuestas.length > 0 ? $.grep(respuestas, function (n, i) { return n.IdPregunta === parseInt(idPreguntaGasto) && n.IdTab === parseInt(x.toString() + z.toString() + indiceUnico.toString()); }) : "");
                                                contenido += '<div class="form-group form-group-sm" id="divPregTab' + (x.toString() + z.toString() + idPreguntaGasto + indiceUnico.toString()) + '">';
                                                contenido += '<label for="txt' + (x.toString() + z.toString() + idPreguntaGasto + indiceUnico.toString()) + '" class="col-sm-4 control-label">Gasto ($miles)</label>';
                                                contenido += '<div class="col-sm-6">';
                                                if (etapa == etapaRevAnalista) {
                                                    contenido += '<div class="input-group">';
                                                }
                                                contenido += '<input type="hidden" name="" value="' + idPreguntaGasto + '" />';
                                                contenido += '<input type="hidden" id="numTab' + (x.toString() + z.toString() + idPreguntaGasto + indiceUnico.toString()) + '" name="" value="' + (x.toString() + z.toString() + indiceUnico.toString()) + '" />';
                                                contenido += '<input type="text" name="' + idPreguntaGasto + "_" + (x.toString() + z.toString() + indiceUnico.toString()) + '" class="form-control input-sm numTab-' + x.toString() + ' suma' + listaPreguntas[i].idPregunta.toString() + '" style="border-radius:3px;" id="txt' + (x.toString() + z.toString() + idPreguntaGasto + indiceUnico.toString()) + '" maxlength="15" onkeypress="return soloNumeros(event,true);" placeholder="Ingrese registro" value="' + (respuestaCmb.length > 0 ? respuestaCmb[0].Respuesta.toString() : "") + '" ' + (countObjeto(excepciones, 2, parseInt(idPreguntaGasto)) > 0 ? "" : "readonly") + ' />';
                                                if (etapa == etapaRevAnalista) {
                                                    contenido += '<span class="input-group-addon" style="border:0;background-color:transparent;"><input type="checkbox" class="cbkTodos' + menuPadre.toString() + menuHijo.toString() + '" name="cbkPregIterar" style="cursor:pointer;" aria-label="..." value="' + idPreguntaGasto + '"></span></div>';
                                                }
                                                contenido += '</div></div>';
                                            }
                                            contenido += '</div></div></div>';
                                            indiceUnico++;
                                        }
                                    }
                                }
                                contenido += '</div></div>';
                            } else {                                
                                if (pregunta.idPregunta == tabIndicProp || pregunta.idPregunta == tabIndicComp) {                                    
                                    if (listaPreguntas[i].idPregunta == 3039 || listaPreguntas[i].idPregunta == 3045 || listaPreguntas[i].idPregunta == 116 || listaPreguntas[i].idPregunta == 118 || listaPreguntas[i].idPregunta == 7621 || listaPreguntas[i].idPregunta == 7629) {
                                        contenido += '<div class="col-xs-4">';
                                    }                                    
                                }
                                listaPreguntas[i].idTab = x;
                                listaPreguntas[i].id = (listaPreguntas[i].idPregunta.toString() + x.toString());
                                var idPreg = listaPreguntas[i].idPregunta;
                                var idTabPreg = listaPreguntas[i].idTab;
                                respuesta = $.grep(respuestas, function (n, i) { return n.IdPregunta === idPreg && n.IdTab === idTabPreg; });                                
                                contenido += '<div class="form-group form-group-sm" id="divPregTab' + (listaPreguntas[i].idPregunta.toString() + x.toString()) + '" style="' + cssGrupoCheckPadresTab + '">';
                                contenido += tiposPreguntas(listaPreguntas[i], indPreguntas, 2, respuesta, excepciones, menuPadre, menuHijo);
                                contenido += '</div>';
                                if (pregunta.idPregunta == tabIndicProp || pregunta.idPregunta == tabIndicComp) {
                                    if (listaPreguntas[i].idPregunta == 3040 || listaPreguntas[i].idPregunta == 3048 || listaPreguntas[i].idPregunta == 117 || listaPreguntas[i].idPregunta == 121 || listaPreguntas[i].idPregunta == 7622 || listaPreguntas[i].idPregunta == 7630) {
                                        contenido += '</div>';
                                        if (listaPreguntas[i].idPregunta == 7622 || listaPreguntas[i].idPregunta == 7630) {
                                            contenido += '<div class="col-xs-12"></div>';
                                        }
                                    }
                                }
                            }
                        }
                    }
                    contenido += '</div></div></div>';
                }
            }
        } else if (pregunta.tipoPregunta.IdParametro.toString() == tabEvaluacion) {
            contenido += '<div class="col-sm-12">';
            contenido += '<ul id="ultab' + pregunta.idPregunta.toString() + '" class="nav nav-tabs">';
            if (pregunta.valor_funcion.length > 0) {
                for (var x = pregunta.valor_funcion[0].Valor; x <= pregunta.valor_funcion[0].Valor2; x++) {
                    contenido += '<li role="presentation" ' + (x == 1 ? "class=active" : "") + '><a href="#tab' + (pregunta.id.toString() + x.toString()) + '" data-toggle="tab">' + (countObjeto(pregunta.valores, 1, x) > 0 ? $.grep(pregunta.valores, function (n, i) { return n.Orden === x && n.IdParametro != n.IdCategoria; })[0].Descripcion : x.toString()) + '</a></li>';
                }
            }
            contenido += '</ul>';
            contenido += '<div id="tab' + (pregunta.idPregunta.toString()) + '" class="tab-content">';
            if (pregunta.valor_funcion.length > 0) {
                for (var x = pregunta.valor_funcion[0].Valor; x <= pregunta.valor_funcion[0].Valor2; x++) {
                    contenido += '<div class="tab-pane fade ' + (x == 1 ? "in active" : "") + '" id="tab' + (pregunta.id.toString() + x.toString()) + '"><div class="panel panel-default tab-sin-borde"><div class="panel-body form-horizontal">';
                    if (pregunta.preguntasGrupos.length > 0) {                        
                        var preguntasGrupos = ((pregunta.idPregunta.toString() == "7028" || pregunta.idPregunta.toString() == "7058") ? pregunta.preguntasGrupos : $.grep(pregunta.preguntasGrupos, function (n, i) { return n.menuGrupo === x; }));
                        if (preguntasGrupos.length > 0) {
                            for (var i = 0; i < preguntasGrupos.length; i++) {

                                if (preguntasGrupos[i].tipoPregunta.IdParametro.toString() == titulo || preguntasGrupos[i].tipoPregunta.IdParametro.toString() == titulo2) {                                    
                                    var preguntaTexto = preguntasGrupos[i].pregunta;
                                    var pregDinamica = (preguntaTexto.includes('#') ? preguntaTexto.split('#') : new String());
                                    if (pregDinamica.length > 0) {
                                        var anosDinamicos = 0;
                                        if (pregDinamica[1].indexOf('-') > 0) {
                                            anosDinamicos = anoCabecera - parseInt(pregDinamica[1].split('-')[1]);
                                        } else if (pregDinamica[1].indexOf('+') > 0) {
                                            anosDinamicos = anoCabecera + parseInt(pregDinamica[1].split('+')[1]);
                                        } else {
                                            anosDinamicos = anoCabecera;
                                        }
                                        preguntaTexto = pregDinamica[0] + anosDinamicos.toString() + pregDinamica[2];
                                    }
                                    contenido += '<div id="divPreg' + preguntasGrupos[i].idPregunta.toString() + '"><p class="' + (preguntasGrupos[i].tipoPregunta.IdParametro.toString() == titulo ? "bg-info" : "bg-primary") + '" style="padding:8px;font-size:12px;">' + preguntaTexto + '</p></div>';
                                } else if (preguntasGrupos[i].tipoPregunta.IdParametro.toString() == tabla) {
                                    //debugger;
                                    contenido += '<div class="table-responsive" id="divPregTab' + (preguntasGrupos[i].idPregunta.toString() + x.toString()) + '"><table id="tablaTab' + (preguntasGrupos[i].idPregunta.toString() + x.toString()) + '" class="table table-bordered">';
                                    if (preguntasGrupos[i].valor_funcion.length > 0) {
                                        var filas = 0, columnas = 0;
                                        var cabeceras = 0;
                                        filas = parseInt(preguntasGrupos[i].valor_funcion[0].Valor);
                                        columnas = parseInt(preguntasGrupos[i].valor_funcion[0].Valor2);
                                        cabeceras = (parseFloat(preguntasGrupos[i].valor_funcion[0].Valor.toString()) - parseFloat(filas));
                                        cabeceras = ((Math.round(cabeceras * 100) / 100) != 0 ? parseFloat((Math.round(cabeceras * 100) / 100).toString().split('.')[1]) : 0);
                                        filas = (filas - parseInt(cabeceras.toString()));
                                        //var contenido = '';
                                        if (cabeceras != 0) {
                                            contenido += '<thead>';
                                            for (var f = 1; f <= parseInt(cabeceras.toString()) ; f++) {
                                                contenido += '<tr style="text-align: center;">';
                                                for (var c = 1; c <= columnas; c++) {
                                                    if (pregunta.preguntasTablas.length > 0) {
                                                        var pregTabla = $.grep(pregunta.preguntasTablas, function (n, a) { return n.fila === f && n.columna === c && n.IdTabla === preguntasGrupos[i].idPregunta; });
                                                        if (pregTabla.length > 0) {
                                                            pregTabla[0].idTab = x;
                                                            contenido += '<td>';
                                                            contenido += tiposPreguntas(pregTabla[0], indPreguntas, 1, respuesta, excepciones, menuPadre, menuHijo);
                                                            contenido += '</td>';
                                                        }
                                                    }
                                                }
                                                contenido += '</tr>';
                                            }
                                            contenido += '</thead>';
                                        }
                                        if (filas > 0) {
                                            contenido += '<tbody>';
                                            for (var f = (parseInt(cabeceras.toString()) + 1) ; f <= (filas + cabeceras) ; f++) {
                                                contenido += '<tr>';
                                                for (var c = 1; c <= columnas; c++) {
                                                    if (pregunta.preguntasTablas.length > 0) {
                                                        var pregTabla = $.grep(pregunta.preguntasTablas, function (n, a) { return n.fila === f && n.columna === c && n.IdTabla === preguntasGrupos[i].idPregunta; });
                                                        if (pregTabla.length > 0) {
                                                            pregTabla[0].idTab = x;
                                                            var idTabPreg = pregTabla[0].idTab;
                                                            respuesta = $.grep(respuestas, function (n, i) { return n.IdPregunta === pregTabla[0].idPregunta && n.IdTab === idTabPreg; });
                                                            contenido += '<td>';
                                                            contenido += tiposPreguntas(pregTabla[0], indPreguntas, 1, respuesta, excepciones, menuPadre, menuHijo);
                                                            contenido += '</td>';
                                                        }
                                                    }
                                                }
                                                contenido += '</tr>';
                                            }
                                            contenido += '</tbody>';
                                        }
                                    }
                                    contenido += '</table></div>';
                                } else {
                                    var idPreg = preguntasGrupos[i].idPregunta;
                                    respuesta = $.grep(respuestas, function (n, i) { return n.IdPregunta === idPreg; });
                                    contenido += '<div class="form-group form-group-sm" id="divPregTab' + (preguntasGrupos[i].idPregunta.toString() + x.toString()) + '">';
                                    contenido += tiposPreguntas(preguntasGrupos[i], indPreguntas, 2, respuesta, excepciones, menuPadre, menuHijo);
                                    contenido += '</div>';
                                }
                            }
                        }
                    }
                    contenido += '</div></div></div>';
                }
            }
            contenido += '</div></div>';
        } else {
            contenido += tiposPreguntas(pregunta, indPreguntas, 2, respuesta, excepciones, menuPadre, menuHijo);
        }
        $("#formHorizontal" + id).append('<div class="form-group form-group-sm" id="divPreg' + pregunta.idPregunta.toString() + '" style="' + cssGrupoCheckPadres + '">' + contenido);
    }
}
function tiposPreguntas(objeto, indPreguntas, tipo, respuesta, excepciones, menuPadre, menuHijo) {
    var preguntaValidar = {};
    var lectura = countObjeto(excepciones, 2, (tipo == 2 ? objeto.idPregunta : objeto.id)) > 0 ? true : false;
    var contenido = '';
    var cssGrupoCheckPadres = objeto.tipoPregunta.IdParametro.toString() == grupoCheckPadre ? "margin-top: 5px;" : "";
    if (tipo == 2) {
        var preguntaTexto = objeto.pregunta;
        var pregDinamica = (preguntaTexto.includes('#') ? preguntaTexto.split('#') : new String());
        if (pregDinamica.length > 0) {
            var anosDinamicos = 0;
            if (pregDinamica[1].indexOf('-') > 0) {
                anosDinamicos = anoCabecera - parseInt(pregDinamica[1].split('-')[1]);
            } else if (pregDinamica[1].indexOf('+') > 0) {
                anosDinamicos = anoCabecera + parseInt(pregDinamica[1].split('+')[1]);
            } else {
                anosDinamicos = anoCabecera;
            }
            preguntaTexto = pregDinamica[0] + anosDinamicos.toString() + pregDinamica[2];
        }
        contenido += '<label for="txt' + objeto.id + '" class="col-sm-4 control-label" style="' + cssGrupoCheckPadres + '">' + ((objeto.tipoPregunta.IdParametro.toString() == grupoCheckHijo) ? "" : (preguntaTexto)) + '</label>';
        contenido += '<div class="col-sm-6">';
    }
    if (objeto.tipoPregunta.IdParametro.toString() != grupoCheckPadre) {        
        if ((countObjeto(plantillaBase, 2, (tipo == 2 ? objeto.idPregunta : objeto.id)) > 0 && permisoAbreCampos == "True") || ($.inArray(rol, rolesHiloComent) !== -1)) {
            if ((etapa == etapaRevAnalista && objeto.tipoPregunta.IdParametro.toString() != textoReadonly && objeto.tipoPregunta.IdParametro.toString() != anos && objeto.tipoPregunta.IdParametro.toString() != etiqueta && objeto.tipoPregunta.IdParametro.toString() != etiquetaEspejo && objeto.tipoPregunta.IdParametro.toString() != totalValidacion && objeto.tipoPregunta.IdParametro.toString() != vacio) || ($.inArray(rol, rolesHiloComent) !== -1)) {
                contenido += '<div class="input-group-sm">';
            }
        }
        contenido += '<input type="hidden" name="" value="' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" />';
        contenido += '<input type="hidden" id="numTab' + (objeto.idPregunta.toString() + (objeto.idTab != null ? objeto.idTab : "")) + '" name="" value="' + (objeto.idTab != null ? objeto.idTab : "") + '" />';
        if (objeto.tipoPregunta.IdParametro.toString() == texto) {
            preguntaValidar.idPregunta = (tipo == 2 ? objeto.idPregunta : objeto.id);
            preguntaValidar.idObjeto = (objeto.idTab != null ? "Tab" : "") + 'txt' + (tipo == 2 ? objeto.idPregunta : objeto.id) + (objeto.idTab != null ? objeto.idTab : "");
            preguntaValidar.idTab = (objeto.idTab != null ? objeto.idTab : "");
            preguntaValidar.tipo = 'texto';
            preguntaValidar.menuPadre = menuPadre;
            preguntaValidar.menuHijo = menuHijo;
            preguntaValidar.respuesta = (respuesta.length > 0 ? respuesta[0].Respuesta : "");
            if ($.grep(validaciones, function (elem) { return elem.Valor === preguntaValidar.idPregunta; }).length > 0) {
                camposValidar.push(preguntaValidar);
            }
            if ($.inArray(rol, rolesHiloComent) !== -1) {
                contenido += '<div class="input-group">';
            }
            contenido += '<input type="text" name="' + (tipo == 2 ? objeto.idPregunta : objeto.id) + "_" + (objeto.idTab != null ? objeto.idTab : "") + '" class="form-control input-sm" style="border-radius:3px;" id="' + (objeto.idTab != null ? "Tab" : "") + 'txt' + (tipo == 2 ? objeto.idPregunta : objeto.id) + (objeto.idTab != null ? objeto.idTab : "") + '" ' + (objeto.funcion.IdParametro.toString() == largoTexto ? "maxlength=" + objeto.valor_funcion[0].Valor.toString() : "") + ' placeholder="Ingrese registro" value="' + (respuesta.length > 0 ? respuesta[0].Respuesta : "") + '" ' + (lectura ? "" : "readonly") + ' />';
            if ($.inArray(rol, rolesHiloComent) !== -1) {
                var coment = (comentarios.length > 0 ? $.grep(comentarios, function (n, i) { return n.IdPregunta === (tipo == 2 ? objeto.idPregunta : objeto.id); }) : null);
                var claseComent = (coment != null ? (coment.length > 0 ? 'fa-comment' : 'fa-pencil-square-o') : 'fa-pencil-square-o');
                var claseBtnComent = (coment != null ? (coment.length > 0 ? 'btn-success' : 'btn-primary') : 'btn-primary');
                contenido += '<div class="input-group-btn"><button id="btnIngresarComent' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" class="btn ' + claseBtnComent + ' btn-sm" type="button" data-toggle="tooltip" data-placement="right" title="Ingresar comentarios" onclick="ingresarComentarios(' + (tipo == 2 ? objeto.idPregunta : objeto.id) + ',' + (objeto.idTab != 0 ? objeto.idTab : "") + ',' + menuPadre + ',' + menuHijo + ');"><i id="ibtnComent' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" class="fa ' + claseComent + '" aria-hidden="true"></i></button></div></div>';
            }
        } else if (objeto.tipoPregunta.IdParametro.toString() == textoReadonly) {
            contenido += '<input type="text" name="' + (tipo == 2 ? objeto.idPregunta : objeto.id) + "_" + (objeto.idTab != null ? objeto.idTab : "") + '" class="form-control input-sm" style="border-radius:3px;" id="' + ((objeto.idTab == null || objeto.idTab == "") ? "txt" + (tipo == 2 ? objeto.idPregunta : objeto.id) : "txtTab" + (tipo == 2 ? objeto.idPregunta : objeto.id) + objeto.idTab) + '" maxlength="' + objeto.tipoPregunta.Valor2 + '" placeholder="Ingrese registro" value="' + (respuesta.length > 0 ? respuesta[0].Respuesta : "") + '" readonly />';
        } else if (objeto.tipoPregunta.IdCategoria.toString() == numerico) {
            preguntaValidar.idObjeto = ((objeto.idTab != null && objeto.idTab != "") ? "Tab" : "") + 'txt' + (tipo == 2 ? objeto.idPregunta : objeto.id) + ((objeto.idTab != null && objeto.idTab != "") ? objeto.idTab : "")
            preguntaValidar.idTab = (objeto.idTab != null ? objeto.idTab : "");
            preguntaValidar.idPregunta = (tipo == 2 ? objeto.idPregunta : objeto.id);
            preguntaValidar.tipo = 'numerico';
            preguntaValidar.menuPadre = menuPadre;
            preguntaValidar.menuHijo = menuHijo;
            preguntaValidar.respuesta = (respuesta.length > 0 ? respuesta[0].Respuesta : "");
            if ($.grep(validaciones, function (elem) { return elem.Valor === preguntaValidar.idPregunta; }).length > 0) {
                camposValidar.push(preguntaValidar);
            }
            if ($.inArray(rol, rolesHiloComent) !== -1) {
                contenido += '<div class="input-group">';
            }
            contenido += '<input type="text" name="' + (tipo == 2 ? objeto.idPregunta : objeto.id) + "_" + (objeto.idTab != null ? objeto.idTab : "") + '" class="form-control input-sm numTab-' + (objeto.idTab != null ? objeto.idTab : "") + (tipo == 1 ? " limpiaTxt" + objeto.IdTabla : "") + (objeto.tipoPregunta.IdParametro.toString() == numericoSuma ? " suma" + (tipo == 1 ? objeto.IdTabla.toString() + objeto.columna.toString() + (objeto.idTab != null ? objeto.idTab.toString() : "") : objeto.idPregunta.toString() + (objeto.idTab != null ? "" : " validaDatos")) : "") + '" style="border-radius:3px;" id="' + ((objeto.idTab != null && objeto.idTab != "") ? "Tab" : "") + 'txt' + (tipo == 2 ? objeto.idPregunta : objeto.id) + ((objeto.idTab != null && objeto.idTab != "") ? objeto.idTab : "") + '" maxlength="' + objeto.tipoPregunta.Valor2 + '" onkeypress="return soloNumeros(event,true);" placeholder="Ingrese registro" value="' + (respuesta.length > 0 ? respuesta[0].Respuesta : "") + '" ' + (lectura ? '' : "readonly") + ' />';
            if ($.inArray(rol, rolesHiloComent) !== -1) {
                var coment = (comentarios.length > 0 ? $.grep(comentarios, function (n, i) { return n.IdPregunta === (tipo == 2 ? objeto.idPregunta : objeto.id); }) : null);
                var claseComent = (coment != null ? (coment.length > 0 ? 'fa-comment' : 'fa-pencil-square-o') : 'fa-pencil-square-o');
                var claseBtnComent = (coment != null ? (coment.length > 0 ? 'btn-success' : 'btn-primary') : 'btn-primary');
                contenido += '<div class="input-group-btn"><button id="btnIngresarComent' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" class="btn ' + claseBtnComent + ' btn-sm" type="button" data-toggle="tooltip" data-placement="right" title="Ingresar comentarios" onclick="ingresarComentarios(' + (tipo == 2 ? objeto.idPregunta : objeto.id) + ',' + (objeto.idTab != 0 ? objeto.idTab : "") + ',' + menuPadre + ',' + menuHijo + ');"><i id="ibtnComent' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" class="fa ' + claseComent + '" aria-hidden="true"></i></button></div></div>';
            }
        } else if (objeto.tipoPregunta.IdParametro.toString() == total) {
            if ($.inArray(rol, rolesHiloComent) !== -1) {
                contenido += '<div class="input-group">';
            }
            contenido += '<input type="text" name="' + (tipo == 2 ? objeto.idPregunta : objeto.id) + "_" + (objeto.idTab != null ? objeto.idTab : "") + '" class="form-control input-sm total' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" style="border-radius:3px;" id="' + ((objeto.idTab != null && objeto.idTab != "") ? "Tab" : "") + 'txt' + ((tipo == 2 ? objeto.idPregunta : objeto.id) + (objeto.idTab != null ? objeto.idTab.toString() : "")) + '" placeholder="Ingrese registro" value="' + (respuesta.length > 0 ? respuesta[0].Respuesta : "") + '" readonly />';
            if ($.inArray(rol, rolesHiloComent) !== -1) {
                var coment = (comentarios.length > 0 ? $.grep(comentarios, function (n, i) { return n.IdPregunta === (tipo == 2 ? objeto.idPregunta : objeto.id); }) : null);
                var claseComent = (coment != null ? (coment.length > 0 ? 'fa-comment' : 'fa-pencil-square-o') : 'fa-pencil-square-o');
                var claseBtnComent = (coment != null ? (coment.length > 0 ? 'btn-success' : 'btn-primary') : 'btn-primary');
                contenido += '<div class="input-group-btn"><button id="btnIngresarComent' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" class="btn ' + claseBtnComent + ' btn-sm" type="button" data-toggle="tooltip" data-placement="right" title="Ingresar comentarios" onclick="ingresarComentarios(' + (tipo == 2 ? objeto.idPregunta : objeto.id) + ',' + (objeto.idTab != 0 ? objeto.idTab : "") + ',' + menuPadre + ',' + menuHijo + ');"><i id="ibtnComent' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" class="fa ' + claseComent + '" aria-hidden="true"></i></button></div></div>';
            }
        } else if (objeto.tipoPregunta.IdParametro.toString() == textoGrande) {
            preguntaValidar.idPregunta = (tipo == 2 ? objeto.idPregunta : objeto.id);
            preguntaValidar.idObjeto = ((objeto.idTab == null || objeto.idTab == "") ? "txt" + (tipo == 2 ? objeto.idPregunta : objeto.id) : "txtTab" + (tipo == 2 ? objeto.idPregunta : objeto.id) + objeto.idTab);
            preguntaValidar.idTab = (objeto.idTab != null ? objeto.idTab : "");
            preguntaValidar.tipo = 'textoGrande';
            preguntaValidar.menuPadre = menuPadre;
            preguntaValidar.menuHijo = menuHijo;
            preguntaValidar.respuesta = (respuesta.length > 0 ? respuesta[0].Respuesta : "");
            if ($.grep(validaciones, function (elem) { return elem.Valor === preguntaValidar.idPregunta; }).length > 0) {
                camposValidar.push(preguntaValidar);
            }
            if ($.inArray(rol, rolesHiloComent) !== -1) {
                contenido += '<div class="input-group">';
            }
            contenido += '<textarea class="form-control input-sm" style="border-radius:3px;" name="' + (tipo == 2 ? objeto.idPregunta : objeto.id) + "_" + (objeto.idTab != null ? objeto.idTab : "") + '" rows="3" id="' + ((objeto.idTab == null || objeto.idTab == "") ? "txt" + (tipo == 2 ? objeto.idPregunta : objeto.id) : "txtTab" + (tipo == 2 ? objeto.idPregunta : objeto.id) + objeto.idTab) + '"' + (objeto.funcion.IdParametro.toString() == largoTexto ? " maxlength=" + objeto.valor_funcion[0].Valor.toString() : "") + ' placeholder="Ingrese registro" aria-describedby="txtCount' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '"' + (lectura ? '' : " readonly") + '>' + (respuesta.length > 0 ? respuesta[0].Respuesta : "") + '</textarea>';
            contenido += '<span id="txtCount' + (tipo == 2 ? objeto.idPregunta : objeto.id) + (objeto.idTab != null ? objeto.idTab : "") + '" class="help-block" style="font-size:12px;">' + (objeto.funcion.IdParametro.toString() == largoTexto ? "Caracteres restantes=" + objeto.valor_funcion[0].Valor.toString() : "") + '</span>';
            if ($.inArray(rol, rolesHiloComent) !== -1) {
                var coment = (comentarios.length > 0 ? $.grep(comentarios, function (n, i) { return n.IdPregunta === (tipo == 2 ? objeto.idPregunta : objeto.id); }) : null);
                var claseComent = (coment != null ? (coment.length > 0 ? 'fa-comment' : 'fa-pencil-square-o') : 'fa-pencil-square-o');
                var claseBtnComent = (coment != null ? (coment.length > 0 ? 'btn-success' : 'btn-primary') : 'btn-primary');
                contenido += '<div class="input-group-btn"><button id="btnIngresarComent' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" class="btn ' + claseBtnComent + ' btn-sm" type="button" data-toggle="tooltip" data-placement="right" title="Ingresar comentarios" onclick="ingresarComentarios(' + (tipo == 2 ? objeto.idPregunta : objeto.id) + ',' + (objeto.idTab != 0 ? objeto.idTab : "") + ',' + menuPadre + ',' + menuHijo + ');"><i id="ibtnComent' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" class="fa ' + claseComent + '" aria-hidden="true"></i></button></div></div>';
            }
        } else if (objeto.tipoPregunta.IdParametro.toString() == checkbox) {
            preguntaValidar.idPregunta = (tipo == 2 ? objeto.idPregunta : objeto.id);
            preguntaValidar.idObjeto = (objeto.idTab != null ? "Tab" : "") + 'cbx' + (tipo == 2 ? objeto.idPregunta : objeto.id);
            preguntaValidar.idTab = (objeto.idTab != null ? objeto.idTab : "");
            preguntaValidar.tipo = 'checkbox';
            preguntaValidar.menuPadre = menuPadre;
            preguntaValidar.menuHijo = menuHijo;
            preguntaValidar.respuesta = (respuesta.length > 0 ? respuesta[0].Respuesta : "");
            if ($.grep(validaciones, function (elem) { return elem.Valor === preguntaValidar.idPregunta; }).length > 0) {
                camposValidar.push(preguntaValidar);
            }
            contenido += '<div class="checkbox" style="padding-top:6px;font-size:12px;">';
            contenido += '<label><input type="checkbox" name="' + (tipo == 2 ? objeto.idPregunta : objeto.id) + "_" + (objeto.idTab != null ? objeto.idTab : "") + '" id="' + (objeto.idTab != null ? "Tab" : "") + 'cbx' + (tipo == 2 ? objeto.idPregunta : objeto.id) + (objeto.idTab != null ? objeto.idTab.toString() : "") + '" style="margin-top:2px;" ' + (respuesta.length > 0 ? (respuesta[0].Respuesta != null ? "checked" : "") : "") + (lectura ? "" : " disabled") + '>' + objeto.pregunta + '</label>';
            contenido += '</div>';
        } else if (objeto.tipoPregunta.IdCategoria.toString() == combo) {
            preguntaValidar.idPregunta = (tipo == 2 ? objeto.idPregunta : objeto.id);
            preguntaValidar.idObjeto = ((objeto.idTab != null && objeto.idTab != "") ? "Tab" : "") + 'cmb' + (tipo == 2 ? objeto.idPregunta : objeto.id) + ((objeto.idTab != null && objeto.idTab != "") ? objeto.idTab : "");
            preguntaValidar.idTab = (objeto.idTab != null ? objeto.idTab : "");
            preguntaValidar.tipo = 'combo';
            preguntaValidar.menuPadre = menuPadre;
            preguntaValidar.menuHijo = menuHijo;
            preguntaValidar.respuesta = (respuesta.length > 0 ? respuesta[0].Respuesta : "");
            if ($.inArray(rol, rolesHiloComent) !== -1) {
                contenido += '<div class="input-group">';
            }
            if ($.grep(validaciones, function (elem) { return elem.Valor === preguntaValidar.idPregunta; }).length > 0) {
                camposValidar.push(preguntaValidar);
            }
            contenido += '<select class="form-control input-sm numTab-' + (objeto.idTab != null ? objeto.idTab : "") + '" style="border-radius:3px;" name="' + (tipo == 2 ? objeto.idPregunta : objeto.id) + "_" + (objeto.idTab != null ? objeto.idTab : "") + '" id="' + ((objeto.idTab != null && objeto.idTab != "") ? "Tab" : "") + 'cmb' + (tipo == 2 ? objeto.idPregunta : objeto.id) + ((objeto.idTab != null && objeto.idTab != "") ? objeto.idTab : "") + '"' + (lectura ? '' : " disabled") + '>';
            if (objeto.tipoPregunta.IdParametro.toString() == seleccione) {
                contenido += '<option value="-1"' + (respuesta.length > 0 ? (respuesta[0].Respuesta.toString() == "-1" ? " selected=selected" : "") : "") + '>Seleccione</option>';
            }
            if (objeto.valores.length > 0 && objeto.funcion.IdParametro.toString() != seleccioneVacio) {                
                $.each(objeto.valores, function (i, val) {
                    if (val.IdParametro != val.IdCategoria) {
                        contenido += '<option value="' + val.IdParametro + '"' + (respuesta.length > 0 ? (parseInt(respuesta[0].Respuesta.toString()) == val.IdParametro ? " selected=selected" : "") : "") + '>' + val.Descripcion + '</option>';
                    }
                });
            }
            if (objeto.tipoPregunta.IdParametro.toString() == comboNumerico) {
                if (objeto.valor_funcion.length > 0) {
                    for (var x = objeto.valor_funcion[0].Valor; x <= objeto.valor_funcion[0].Valor2; x++) {
                        contenido += '<option value="' + x + '"' + (respuesta.length > 0 ? (parseInt(respuesta[0].Respuesta.toString()) == x ? " selected=selected" : "") : "") + '>' + x + '</option>';
                    }
                }
            }
            if (objeto.tipoPregunta.IdParametro.toString() == comboDinamico) {
                contenido += '<option value="-1"' + (respuesta.length > 0 ? (respuesta[0].Respuesta.toString() == "-1" ? " selected=selected" : "") : "") + '>Seleccione</option>';
                if (objeto.valor_funcion.length > 0) {
                    var valor1 = 0;
                    var valor2 = 0;
                    for (var x = (Number.isInteger(objeto.valor_funcion[0].Valor) ? (anoCabecera + parseInt(objeto.valor_funcion[0].Valor)) : Math.round(parseInt(objeto.valor_funcion[0].Valor.toString()))) ; x <= (Number.isInteger(objeto.valor_funcion[0].Valor2) ? (anoCabecera + parseInt(objeto.valor_funcion[0].Valor2)) : Math.round(parseInt(objeto.valor_funcion[0].Valor2.toString()))) ; x++) {
                        contenido += '<option value="' + x + '"' + (respuesta.length > 0 ? (parseInt(respuesta[0].Respuesta.toString()) == x ? " selected=selected" : "") : "") + '>' + x + '</option>';
                    }
                }
            }
            contenido += '</select>';
            if ($.inArray(rol, rolesHiloComent) !== -1) {
                var coment = (comentarios.length > 0 ? $.grep(comentarios, function (n, i) { return n.IdPregunta === (tipo == 2 ? objeto.idPregunta : objeto.id); }) : null);
                var claseComent = (coment != null ? (coment.length > 0 ? 'fa-comment' : 'fa-pencil-square-o') : 'fa-pencil-square-o');
                var claseBtnComent = (coment != null ? (coment.length > 0 ? 'btn-success' : 'btn-primary') : 'btn-primary');
                contenido += '<div class="input-group-btn"><button id="btnIngresarComent' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" class="btn ' + claseBtnComent + ' btn-sm" type="button" data-toggle="tooltip" data-placement="right" title="Ingresar comentarios" onclick="ingresarComentarios(' + (tipo == 2 ? objeto.idPregunta : objeto.id) + ',' + (objeto.idTab != 0 ? objeto.idTab : "") + ',' + menuPadre + ',' + menuHijo + ');"><i id="ibtnComent' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" class="fa ' + claseComent + '" aria-hidden="true"></i></button></div></div>';
            }
        } else if (objeto.tipoPregunta.IdParametro.toString() == inputCheck) {
            var soloNumKeyPress = (objeto.funcion.IdParametro.toString() == largoNumerico ? "return soloNumeros(event,true);" : "");
            contenido += '<div class="input-group">';
            contenido += '<span class="input-group-addon" style="background-color:#FFF;"><input type="checkbox" aria-label="..." id="' + (objeto.idTab != null ? "Tab" : "") + 'cbx' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" style="cursor:pointer;" ' + (lectura ? "" : "disabled") + '></span>';
            contenido += '<input type="text" name="' + (tipo == 2 ? objeto.idPregunta : objeto.id) + "_" + (objeto.idTab != null ? objeto.idTab : "") + '" class="form-control input-sm" id="' + (objeto.idTab != null ? "Tab" : "") + 'txt' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" onkeypress="' + soloNumKeyPress + '" ' + (objeto.funcion.IdParametro.toString() == largoTexto ? "maxlength=" + objeto.valor_funcion[0].Valor.toString() : "") + ' placeholder="Ingrese registro" aria-label="..." value="' + (respuesta.length > 0 ? respuesta[0].Respuesta : "") + '" readonly />';
            contenido += '</div>';
        } else if (objeto.tipoPregunta.IdCategoria.toString() == grupoCheck) {
            var cbxSoloLectura = (!lectura || objeto.funcion.IdParametro.toString() == inputBloquedo ? "disabled" : "");
            contenido += '<div class="checkbox" style="padding-top:6px;font-size:12px;">';
            contenido += '<label><input type="checkbox" class="numTab-' + (objeto.idTab != null ? objeto.idTab : "") + '" name="' + (tipo == 2 ? objeto.idPregunta : objeto.id) + "_" + (objeto.idTab != null ? objeto.idTab : "") + '" id="' + (objeto.idTab != null ? "Tab" : "") + 'cbx' + (tipo == 2 ? objeto.idPregunta : objeto.id) + (objeto.idTab != null ? objeto.idTab.toString() : "") + '" style="margin-top:2px;" ' + (respuesta.length > 0 ? (respuesta[0].Respuesta != null ? "checked " : "") : "") + cbxSoloLectura + '> ' + objeto.pregunta + '</label>';
            contenido += '</div>';
        } else if (objeto.tipoPregunta.IdParametro.toString() == anos) {
            if (objeto.valor_funcion.length > 0) {
                contenido += (anoCabecera + (objeto.valor_funcion[0].Valor + objeto.valor_funcion[0].Valor2));
            }
        } else if (objeto.tipoPregunta.IdParametro.toString() == etiqueta) {
            var preguntaTexto = objeto.pregunta;
            var pregDinamica = (preguntaTexto.includes('#') ? preguntaTexto.split('#') : new String());
            if (pregDinamica.length > 0) {
                var anosDinamicos = 0;
                if (pregDinamica[1].indexOf('-') > 0) {
                    anosDinamicos = anoCabecera - parseInt(pregDinamica[1].split('-')[1]);
                } else if (pregDinamica[1].indexOf('+') > 0) {
                    anosDinamicos = anoCabecera + parseInt(pregDinamica[1].split('+')[1]);
                } else {
                    anosDinamicos = anoCabecera;
                }
                preguntaTexto = pregDinamica[0] + anosDinamicos.toString() + pregDinamica[2];
            }
            contenido += preguntaTexto;
        } else if (objeto.tipoPregunta.IdParametro.toString() == etiquetaEspejo) {
            contenido += '<label class="lblEspejo' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" style="font-weight:normal;"></label>';
        } else if (objeto.tipoPregunta.IdParametro.toString() == totalValidacion) {
            contenido += '<div id="divTotVal' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '">';
            contenido += '<input type="text" name="' + (tipo == 2 ? objeto.idPregunta : objeto.id) + "_" + (objeto.idTab != null ? objeto.idTab : "") + '" class="form-control input-sm total' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" style="border-radius:3px;" id="txt' + ((tipo == 2 ? objeto.idPregunta : objeto.id) + objeto.idTab) + '" placeholder="Ingrese registro" value="' + (respuesta.length > 0 ? respuesta[0].Respuesta : "") + '" readonly />';
            contenido += '<span id="imgVal' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" class="glyphicon glyphicon-ok form-control-feedback" aria-hidden="true" style="display:none;"></span>';
            contenido += '<span id="valEstado' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" class="sr-only" style="display:none;">(success)</span>';
            contenido += '<span id="imgValError' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" class="glyphicon glyphicon-remove form-control-feedback" aria-hidden="true" style="display:none;"></span>';
            contenido += '<span id="valEstadoError' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" class="sr-only" style="display:none;">(error)</span>';
            contenido += '</div>';
        } else if (objeto.tipoPregunta.IdParametro.toString() == checkboxVacio) {
            preguntaValidar.idPregunta = (tipo == 2 ? objeto.idPregunta : objeto.id);
            preguntaValidar.idObjeto = (objeto.idTab != null ? "Tab" : "") + 'cbx' + (tipo == 2 ? objeto.idPregunta : objeto.id);
            preguntaValidar.idTab = (objeto.idTab != null ? objeto.idTab : "");
            preguntaValidar.tipo = 'checkboxVacio';
            preguntaValidar.menuPadre = menuPadre;
            preguntaValidar.menuHijo = menuHijo;
            preguntaValidar.respuesta = (respuesta.length > 0 ? respuesta[0].Respuesta : "");
            if ($.grep(validaciones, function (elem) { return elem.Valor === preguntaValidar.idPregunta; }).length > 0) {
                camposValidar.push(preguntaValidar);
            }
            contenido += '<div class="checkbox" style="padding-top:6px;font-size:12px;">';
            contenido += '<label><input type="checkbox" class="numTab-' + objeto.idTab + (objeto.funcion.IdParametro.toString() == checkboxUnico ? " cbxunico" : "") + '" name="' + (tipo == 2 ? objeto.idPregunta : objeto.id) + "_" + (objeto.idTab != null ? objeto.idTab : "") + '" id="' + (objeto.idTab != null ? "Tab" : "") + 'cbx' + (tipo == 2 ? objeto.idPregunta : objeto.id) + (objeto.idTab != null ? objeto.idTab.toString() : "") + '" style="cursor:pointer;" ' + (respuesta.length > 0 ? (respuesta[0].Respuesta != null ? "checked " : "") : "") + (lectura ? "" : "disabled") + '></label>';
            contenido += '</div>';
        } else if (objeto.tipoPregunta.IdParametro.toString() == checkboxInline) {
            preguntaValidar.idPregunta = (tipo == 2 ? objeto.idPregunta : objeto.id);
            preguntaValidar.idObjeto = (objeto.idTab != null ? "Tab" : "") + 'txt' + (tipo == 2 ? objeto.idPregunta : objeto.id) + (objeto.idTab != null ? objeto.idTab : "");
            preguntaValidar.idTab = (objeto.idTab != null ? objeto.idTab : "");
            preguntaValidar.tipo = 'checkboxInline';
            preguntaValidar.menuPadre = menuPadre;
            preguntaValidar.menuHijo = menuHijo;
            preguntaValidar.respuesta = (respuesta.length > 0 ? respuesta[0].Respuesta : "");
            if ($.grep(validaciones, function (elem) { return elem.Valor === preguntaValidar.idPregunta; }).length > 0) {
                camposValidar.push(preguntaValidar);
            }
            contenido += '<div class="input-group">';
            contenido += '<input type="text" name="' + (tipo == 2 ? objeto.idPregunta : objeto.id) + "_" + (objeto.idTab != null ? objeto.idTab : "") + '" class="form-control input-sm numTab-' + objeto.idTab + (objeto.tipoPregunta.IdParametro.toString() == numericoSuma ? " suma" + objeto.idPregunta : "") + (objeto.idTab != null ? "" : " validaDatos") + '" id="' + (objeto.idTab != null ? "Tab" : "") + 'txt' + (tipo == 2 ? objeto.idPregunta : objeto.id) + (objeto.idTab != null ? objeto.idTab : "") + '" maxlength="' + (objeto.valor_funcion.length > 0 ? objeto.valor_funcion[0].Valor2 : "") + '" onkeypress="return soloNumeros(event,true);" placeholder="Ingrese registro" value="' + (respuesta.length > 0 ? respuesta[0].Respuesta : "") + '" ' + (lectura ? "" : "readonly") + '/>';
            contenido += '<div class="input-group-btn">';
            contenido += '<button type="button" class="btn btn-default btn-sm otrasOpciones' + (objeto.idPregunta) + '" value="-1" id="numTab-' + (objeto.idPregunta + "-" + objeto.idTab) + '">S/I</button>';
            //contenido += '<button type="button" class="btn btn-default btn-sm otrasOpciones' + (objeto.idPregunta) + '" value="-2" id="numTab-' + (objeto.idPregunta + "-" + objeto.idTab) + '">N/A</button>';
            if ($.inArray(rol, rolesHiloComent) !== -1) {
                var coment = (comentarios.length > 0 ? $.grep(comentarios, function (n, i) { return n.IdPregunta === (tipo == 2 ? objeto.idPregunta : objeto.id); }) : null);
                var claseComent = (coment != null ? (coment.length > 0 ? 'fa-comment' : 'fa-pencil-square-o') : 'fa-pencil-square-o');
                var claseBtnComent = (coment != null ? (coment.length > 0 ? 'btn-success' : 'btn-primary') : 'btn-primary');
                contenido += '<button id="btnIngresarComent' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" class="btn ' + claseBtnComent + ' btn-sm" type="button" data-toggle="tooltip" data-placement="right" title="Ingresar comentarios" onclick="ingresarComentarios(' + (tipo == 2 ? objeto.idPregunta : objeto.id) + ',' + (objeto.idTab != 0 ? objeto.idTab : "") + ',' + menuPadre + ',' + menuHijo + ');"><i id="ibtnComent' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" class="fa ' + claseComent + '" aria-hidden="true"></i></button>';
            }
            contenido += '</div></div>';
            contenido += '<span id="helpBlock" class="help-block" style="font-size:12px;">S/I = Sin información</span>';
        } else if (objeto.tipoPregunta.IdParametro.toString() == inputValidaciones) {
            contenido += '<div id="divVal' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" class="input-group">';
            contenido += '<input type="text" name="' + (tipo == 2 ? objeto.idPregunta : objeto.id) + "_" + (objeto.idTab != null ? objeto.idTab : "") + '" class="form-control input-sm total' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" style="border-radius:3px;" id="txt' + ((tipo == 2 ? objeto.idPregunta : objeto.id) + (objeto.idTab != null ? objeto.idTab.toString() : "")) + '" placeholder="Ingrese registro" value="' + (respuesta.length > 0 ? respuesta[0].Respuesta : "") + '" readonly aria-describedby="helpVal' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" />';
            contenido += '<span id="helpVal' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '" class="help-block"></span>';
            contenido += '</div>';
        } 
        if ((countObjeto(plantillaBase, 2, (tipo == 2 ? objeto.idPregunta : objeto.id)) > 0 && permisoAbreCampos == "True") || ($.inArray(rol, rolesHiloComent) !== -1)) {
            if (etapa == etapaRevAnalista && objeto.tipoPregunta.IdParametro.toString() != textoReadonly && objeto.tipoPregunta.IdParametro.toString() != anos && objeto.tipoPregunta.IdParametro.toString() != etiqueta && objeto.tipoPregunta.IdParametro.toString() != etiquetaEspejo && objeto.tipoPregunta.IdParametro.toString() != totalValidacion && objeto.tipoPregunta.IdParametro.toString() != vacio) {
                contenido += '<span class="input-group-addon" style="border:0;background-color:transparent;"><input type="checkbox" class="cbkTodos' + menuPadre.toString() + menuHijo.toString() + '" name="cbkPregIterar" style="cursor:pointer;" aria-label="..." value="' + (tipo == 2 ? objeto.idPregunta : objeto.id) + '"></span></div>';
            } else if ($.inArray(rol, rolesHiloComent) !== -1) {
                contenido += '</div>';
            }
        }
    }        
    if (tipo == 2) {
        contenido += '</div>';
    }
    contenido += '';
    return contenido;
}
function countObjeto(objeto, tipo, valor) {
    var total = 0;
    if (objeto.length > 0) {
        $.each(objeto, function (i, obj) {
            if (tipo == 1) {
                if (obj.Orden == valor) { total++; }
            } else if (tipo == 2) {
                if (obj.IdPregunta == valor) { total++; }
            }
        });
    }
    return total;
}
function agregaComentarios(idMenu, idPrograma, idFormHorizontal) {
    var opciones = {
        url: ROOT + "Formulario/BuscaComentarios",
        data: { _id: idPrograma, menu: idMenu },
        type: "post",
        datatype: "json",
        async: false,
        success: function (result, xhr, status) {
            var objResultado = JSON.parse(result);
            if (objResultado.length > 0) {
                let values = objResultado.map(function (v) { return v.IdConsulta; });
                var maxTab = Math.max.apply(null, values);
                $.each(objResultado, function (i, obj) {                    
                    var listaComentarios = '<div class="panel-group" id="accordion' + idMenu + obj.IdConsulta + '" role="tablist" aria-multiselectable="true" style="margin-top: 15px;"><div class="panel panel-default"><div class="panel-heading" role="tab" id="headingOne' + idMenu + obj.IdConsulta + '"><h4 class="panel-title" style="font-size:14px;"><a role="button" data-toggle="collapse" data-parent="#accordion' + idMenu + obj.IdConsulta + '" href="#collapseOne' + idMenu + obj.IdConsulta + '" aria-expanded="true" aria-controls="collapseOne">' + obj.Tema + '</a></h4></div><div id="collapseOne' + idMenu + obj.IdConsulta + '" class="panel-collapse collapse ' + (obj.IdConsulta == maxTab ? "in" : "") + '" role="tabpanel" aria-labelledby="headingOne"><div class="panel-body" style="font-size:12px;"><dl id="panelComentConsultas' + idMenu + obj.IdConsulta + '" style="font-size: 13px;"><dt>' + obj.Tipo + ' - ' + obj.Usuario + ' - ' + moment(obj.Fecha).format("DD/MM/YYYY HH:mm:ss") + '</dt><dd>' + obj.Consulta + '</dd></dl><p class="text-right"><button type="button" id="btnRespComent' + idMenu + obj.IdConsulta + '" class="btn btn-primary btn-xs" style="margin-right: 10px;" data-toggle="tooltip" data-placement="right" title="Responder comentario" onclick="respuestasConsultas(' + obj.IdConsulta + ');"><span class="fa fa-pencil-square-o" aria-hidden="true"></span></button></p></div></div>';
                    $("#formHorizontal" + idFormHorizontal).append(listaComentarios);
                    agregaRespuestasComentarios(obj.IdConsulta, idMenu);
                });
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
function agregaRespuestasComentarios(idConsulta, idMenu) {
    var opciones = {
        url: ROOT + "Formulario/BuscaRespuestasComentarios",
        data: { idConsulta: idConsulta },
        type: "post",
        datatype: "json",
        async: false,
        success: function (result, xhr, status) {
            var objResultado = JSON.parse(result);
            if (objResultado.length > 0) {
                $.each(objResultado, function (i, obj) {
                    var listaRespuestas = '<div style="font-size:12px; margin-left: 20px; margin-top: 10px;"><dt>Respuesta - ' + obj.Usuario + ' - ' + moment(obj.Fecha).format("DD/MM/YYYY HH:mm:ss") + '</dt><dd>' + obj.Respuesta + '</dd></div>';
                    $("#panelComentConsultas" + idMenu + idConsulta).append(listaRespuestas);
                });
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
function respuestasConsultas(idConsulta) {
    $("#txtIdConsulta").val(idConsulta);
    $('#modalRespuestasConsultas').modal('show');
}