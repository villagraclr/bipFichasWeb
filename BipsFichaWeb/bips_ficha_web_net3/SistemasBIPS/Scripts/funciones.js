//Funciones
const funcChange = 1101;
const funcTab = 1102;
const funcKeyUp = 1103;
const funcCheck = 1104;
const funcValSec = 1105;
const funcOculPreg = 1106;
const funcCheckEnable = 1107;
const funcSumas = 1108;
const funcSumaTotal = 1109;
const funcChangeTab = 1110;
const funcCambiarTabla = 1111;
const funcCopiaTexto = 1112;
const funcKeyUpTab = 1114;
const funcCopiaTextoInput = 1115;
const funcOcultaConSi = 1116;
const funcEtiquetaEspejo = 1117;
const funcMuestraOcultaSegResp = 1118;
const funcOcultaAnidado = 1119;
const funcSumaTotalValidacion = 1120;
const funcValidaDatosCss = 1121;
const funcCopiaSumaTotal = 1122;
const funcChangeHijoPadre = 1123;
const funcSumasTab = 1124;
const funcMarcarRespuesta = 1125;
const funcOcultaCheckbox = 1126;
const funcOculPregTab = 1127;
const funcSumaTotalAnidada = 1128;
const funcLlenaComboConOtro = 1129;
const funcValidaSelecUnica = 1130;
const funcCalculoDivision = 1131;
const funcComparaTotales = 1132;
const funcComparaTotalDepend = 1133;
const funcFiltraSelect = 1134;
const funcLimpiaSelect = 1135;
const funcFiltraComboValor = 1136;
const funcHabilitaTexto = 1137;
const funcValidaComboXCombo = 1138;
const funcValidaErroresMayorQue = 1139;
const funcValidaErroresIgualQue = 1140;
const funcValidaErroresNoCont = 1141;
const funcValidaErroresNoCont2 = 1142;
const funcValidaErroresNoCont3 = 1143;
const funcCalculoEvaluacion = 1144;
const funcCopiaTextoTabla = 1145;
const funcCalculoEvalEfic = 1146;
const funcSumaCamposVarios = 1147;
const funcOcultaTabAsig = 1148;
const funcDivisiones = 1149;
const funcOtrasOpciones = 1150;
const funcOculPregTabNormal = 1151;
const funcSelectOtroSelect = 1152;
const funcRegistroExcepcionesEval = 1153;
const funcBloqueaCopyPaste = 1154;
const funcCopiaTextoNormal = 1155;
const funcColorFondo = 1156;
const funcCambiaTablaMultiple = 1157;
const funcCopiaTextoTabla2 = 1158;
const funcCheckSelecUnica = 1159;
const funcDesbloqueaPregEval = 1160;
const funcModuloEvaluacion = 1161;
const funcMuestOcultaVarResp = 1162;
const funcModuloEvalExAnte2023 = 1163;
const funcOtrasOpciones2 = 1164;
const funcDivisionesPorcentaje = 1165;
const funcModuloEvalMonitoreo2023 = 1166;
const funcCargaFraseInput = 1167;
const funcEjecutaCalEficiencia = 1168;
const funcCalculoDivisionTabs = 1169;
const funcMuestOcultaVarRespTabs = 1170;
const funcLlenaCmbTabs = 1171;
const funcOcultaSegunResp = 1172;
const funcDivisionesTasasPorcentaje = 1173;

const tipoPregCombo = 402;
const tipoPregTab = 405;
const tipoPregTxtGrande = 406;
const tipoPregCheck = 409;
const tipoPregInputCheck = 417;
const tipoPregTotal = 421;
const tipoPregTabla = 413;
const tipoPregTexto = 401;
const tipoPregNumerico = 418;
const tipoPregTextoReadonly = 422;
const tipoPregEtiquetaEspejo = 423;
const tipoPregTotalValidacion = 427;
const tipoPregComboDeshabilitado = 428;
const tipoPregCheckHijo = 426;
const tipoPregCheckVacio = 429;
const tipoPregCheckInline = 433;

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
async function funcionesFormularios(objResultado, respuestas) {
    $.each(objResultado, function (i, item) {
        var idObjeto = "";
        var idObjetoDep = "";
        if (item.IdEvento == funcChange) {
            if (item.ValorPregunta != "" && item.ValorPregunta != null) {
                for (var i = 1; i <= parseInt(item.ValorPregunta) ; i++) {
                    if (item.CategoriaPregunta == tipoPregCombo) {
                        idObjeto = "Tabcmb" + item.IdPregunta + i.toString();
                    }
                    if (item.CategoriaPreguntaDependiente == tipoPregCombo) {
                        idObjetoDep = "Tabcmb" + item.IdPreguntaDependiente + i.toString();
                    }
                    $("#" + idObjetoDep).append("<option value='-1'>Seleccione</option>");
                    if (item.Datos.length > 0) {
                        var resp = "";
                        if (respuestas.length > 0) {
                            $.each(respuestas, function (ii, r) {
                                if (r.IdPregunta == item.IdPreguntaDependiente && r.IdTab == i) {
                                    resp = r.Respuesta;
                                    return false;
                                }
                            });
                        }
                        $.each(item.Datos, function (ii, d) {
                            if (d.Valor != null) {
                                var selec = (resp == d.IdParametro.toString() ? "selected" : "");
                                if ($("#" + idObjeto).val() == d.Valor) {
                                    $("#" + idObjetoDep).append("<option value='" + d.IdParametro + "' " + selec + ">" + d.Descripcion + "</option>");
                                }
                            }
                        });
                    }
                    $("#" + idObjeto).change(function () {
                        var arrayDatos = [];
                        var valorObj = $(this).val();
                        var nTab = $(this).attr('class').split(/\s+/)[2].split('-')[1];
                        if (nTab != null || nTab != 0) {
                            idObjetoDep = ("Tabcmb" + item.IdPreguntaDependiente + nTab);
                        }
                        $("#" + idObjetoDep).empty();
                        $("#" + idObjetoDep).append("<option value='-1'>Seleccione</option>");
                        if (item.Datos.length > 0) {
                            $.each(item.Datos, function (ii, d) {
                                if (d.Valor != null) {
                                    if (valorObj == d.Valor) {
                                        $("#" + idObjetoDep).append("<option value='" + d.IdParametro + "'>" + d.Descripcion + "</option>");
                                    }
                                }
                            });
                        }
                    });
                }
            } else {
                if (item.CategoriaPregunta == tipoPregCombo) {
                    idObjeto = "cmb" + item.IdPregunta;
                }
                if (item.CategoriaPreguntaDependiente == tipoPregCombo) {
                    idObjetoDep = "cmb" + item.IdPreguntaDependiente;
                }
                $("#" + idObjetoDep).append("<option value='-1'>Seleccione</option>");
                if (item.Datos.length > 0) {
                    var resp = "";
                    if (respuestas.length > 0) {
                        $.each(respuestas, function (i, r) {
                            if (r.IdPregunta == item.IdPreguntaDependiente) {
                                resp = r.Respuesta;
                                return false;
                            }
                        });
                    }
                    $.each(item.Datos, function (i, d) {
                        if (d.Valor != null) {
                            var selec = (resp == d.IdParametro.toString() ? "selected" : "");
                            if ($("#" + idObjeto).val() == d.Valor) {
                                $("#" + idObjetoDep).append("<option value='" + d.IdParametro + "' " + selec + ">" + d.Descripcion + "</option>");
                            }
                        }
                    });
                }
                $("#" + idObjeto).change(function () {
                    var arrayDatos = [];
                    var valorObj = $(this).val();
                    $("#" + idObjetoDep).empty();
                    $("#" + idObjetoDep).append("<option value='-1'>Seleccione</option>");
                    if (item.Datos.length > 0) {
                        $.each(item.Datos, function (i, d) {
                            if (d.Valor != null) {
                                if (valorObj == d.Valor) {
                                    $("#" + idObjetoDep).append("<option value='" + d.IdParametro + "'>" + d.Descripcion + "</option>");
                                }
                            }
                        });
                    }
                });
            }
        } else if (item.IdEvento == funcTab) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregTab) {
                idObjetoDep = "tab" + item.IdPreguntaDependiente;
            }
            ocultaTabs(idObjetoDep, $("#" + idObjeto).val(), true);
            $("#" + idObjeto).change(function () {
                ocultaTabs(idObjetoDep, $(this).val(), true);
            });
        } else if (item.IdEvento == funcKeyUp) {
            if (item.TipoPregunta == tipoPregTxtGrande) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (typeof $("#" + idObjeto).val() != "undefined") {
                if ($("#" + idObjeto).val().length > 0) {
                    var totalCaracteres = item.ValorPregunta;
                    var restantes = (totalCaracteres - $("#" + idObjeto).val().length) < 0 ? 0 : (totalCaracteres - $("#" + idObjeto).val().length);
                    $("#txtCount" + item.IdPregunta).text("Caracteres restantes=" + restantes);
                }
                $("#" + idObjeto).keyup(function () {
                    var totalCaracteres = item.ValorPregunta;
                    var restantes = (totalCaracteres - $(this).val().length) < 0 ? 0 : (totalCaracteres - $(this).val().length);
                    $("#txtCount" + item.IdPregunta).text("Caracteres restantes=" + restantes);
                });
            }
        } else if (item.IdEvento == funcCheck) {
            if (item.TipoPregunta == tipoPregCheck || item.TipoPregunta == tipoPregCheckVacio) {
                idObjeto = "cbx" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregCombo) {
                idObjetoDep = "cmb" + item.IdPreguntaDependiente;
            }
            if ($("#" + idObjeto).is(":checked")) {
                $("#" + idObjetoDep).prop('disabled', true);
                $("#" + idObjetoDep + " option[value='-1']").prop('selected', true);
            } else {
                $("#" + idObjetoDep).prop('disabled', false);
            }
            $("#" + idObjeto).click(function () {
                if ($(this).is(":checked")) {
                    $("#" + idObjetoDep).prop('disabled', true);
                    $("#" + idObjetoDep + " option[value='-1']").prop('selected', true);
                } else {
                    $("#" + idObjetoDep).prop('disabled', false);
                }
            });
        } else if (item.IdEvento == funcValSec) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregCombo) {
                idObjetoDep = "cmb" + item.IdPreguntaDependiente;
            }
            $("#" + idObjeto).change(function () {
                if ($(this).val() != "-1") {
                    if (item.ValorPregunta != 1 && $("#" + idObjetoDep).val() == "-1") {
                        $("#" + idObjeto + " option[value='-1']").prop('selected', true);
                        alert("Seleccione un valor principal");
                    } else {
                        if ($(this).val() == $("#" + idObjetoDep).val()) {
                            if (item.ValorPregunta != 1) {
                                $("#" + idObjeto + " option[value='-1']").prop('selected', true);
                            } else {
                                $("#" + idObjetoDep + " option[value='-1']").prop('selected', true);
                            }
                            alert("El valor ya se encuentra seleccionado como " + (item.ValorPreguntaDependiente == 1 ? "principal" : "secundario"));
                        }
                    }
                } else {
                    if (item.ValorPregunta == 1) {
                        $("#" + idObjetoDep + " option[value='-1']").prop('selected', true);
                    }
                }
            });
        } else if (item.IdEvento == funcOculPreg) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.ValorPregunta != "" && item.ValorPregunta != null) {
                for (var i = 1; i <= parseInt(item.ValorPregunta) ; i++) {
                    if ($("#Tab" + idObjeto + i.toString() + " option:selected").text().toUpperCase() == "NO" || $("#Tab" + idObjeto + i.toString() + " option:selected").val() == "-1") {
                        $("#divPregTab" + item.IdPreguntaDependiente + i.toString()).hide();
                    } else {
                        $("#divPregTab" + item.IdPreguntaDependiente + i.toString()).show();
                    }
                    $("#Tab" + idObjeto + i.toString()).change(function () {
                        var tab = this.id.substring(6, this.id.length);
                        var numTab = $("#numTab" + tab).val();
                        if ($("#Tab" + idObjeto + numTab + " option:selected").text().toUpperCase() == "NO" || $("#Tab" + idObjeto + numTab +  " option:selected").val() == "-1") {
                            $("#divPregTab" + item.IdPreguntaDependiente + numTab).hide();                            
                        } else {
                            $("#divPregTab" + item.IdPreguntaDependiente + numTab).show();
                        }
                    });
                }
            }else {
                if ($("#" + idObjeto + " option:selected").text().toUpperCase() == "NO" || $("#" + idObjeto + " option:selected").val() == "-1") {
                    $("#divPreg" + item.IdPreguntaDependiente).hide();
                } else {
                    $("#divPreg" + item.IdPreguntaDependiente).show();
                }
                $("#" + idObjeto).change(function () {
                    if ($("#" + idObjeto + " option:selected").text().toUpperCase() == "NO" || $("#" + idObjeto + " option:selected").val() == "-1") {
                        $("#divPreg" + item.IdPreguntaDependiente).hide();
                        $("#txt" + item.IdPreguntaDependiente).val("");
                        $(".limpiaTxt" + item.IdPreguntaDependiente).val("");
                    } else {
                        $("#divPreg" + item.IdPreguntaDependiente).show();
                    }
                });
            }            
        } else if (item.IdEvento == funcCheckEnable) {
            if (item.TipoPregunta == tipoPregInputCheck) {
                idObjeto = "cbx" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregInputCheck) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            if ($("#" + idObjetoDep).val() != "") {
                $("#" + idObjeto).prop('checked', true);
                $("#" + idObjetoDep).prop('readonly', false);
            }
            $("#" + idObjeto).click(function () {
                if ($(this).is(":checked")) {
                    $("#" + idObjetoDep).prop('readonly', false);
                } else {
                    $("#" + idObjetoDep).prop('readonly', true);
                    $("#" + idObjetoDep).val("");
                }
            });
        } else if (item.IdEvento == funcSumas) {
            if (item.TipoPregunta == tipoPregTotal) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.ValorPreguntaDependiente.indexOf(',') > 0) {
                for (var i = 1; i <= parseInt(item.ValorPreguntaDependiente.split(',')[1]) ; i++) {
                    $(".suma" + (item.ValorPreguntaDependiente.split(',')[0] + i)).keyup(function () {
                        var nTab = $(this).attr('class').split(/\s+/)[4].split('-')[1];
                        var total = 0;
                        var blancos = false;
                        $(".suma" + (item.ValorPreguntaDependiente.split(',')[0] + nTab)).each(function () {
                            if ($.trim($(this).val()) != "") {
                                blancos = true;
                                total += retornaValorFormateado($(this).val());
                            }
                        });
                        if (blancos) {
                            $("#" + (idObjeto + nTab)).val(total.toString().replace('.', ',')).trigger('change');
                        } else {
                            $("#" + (idObjeto + nTab)).val('').trigger('change');
                        }
                    });
                }
            } else {
                $(".suma" + item.ValorPreguntaDependiente).keyup(function () {
                    var total = 0;
                    var blancos = false;
                    $(".suma" + item.ValorPreguntaDependiente).each(function () {
                        if ($.trim($(this).val()) != "") {
                            blancos = true;
                            total += retornaValorFormateado($(this).val());
                        }
                    });
                    if (blancos) {
                        if (item.Datos.length > 0) {
                            var totalCompuesto = 0;
                            var idTotal = "";
                            $.each(item.Datos, function (i, d) {
                                if (d.Valor != null && d.Valor != "") {
                                    if (d.Valor2 != "1") {
                                        if ($("#txt" + d.Valor).val() != "") {
                                            totalCompuesto += retornaValorFormateado($("#txt" + d.Valor).val());
                                        } else {
                                            totalCompuesto += 0;
                                        }
                                    } else {
                                        idTotal = d.Valor;
                                    }
                                }
                            });
                            var TotaFinal = total + totalCompuesto;
                            $("#txt" + idTotal).val(TotaFinal.toString().replace('.', ','));
                        }
                        $("#" + idObjeto).val(total.toString().replace('.', ','));
                    } else {
                        $("#" + idObjeto).val('');
                    }
                });
            }
        } else if (item.IdEvento == funcSumaTotal) {
            if (item.TipoPregunta == tipoPregTotal) {
                idObjeto = "txt" + item.IdPregunta;
            }
            $(".total" + item.IdPreguntaDependiente).change(function () {
                var total = 0;
                var blancos = false;
                $(".total" + item.IdPreguntaDependiente).each(function () {
                    if ($.trim($(this).val()) != "") {
                        blancos = true;
                        total += retornaValorFormateado($(this).val());
                    }
                });
                if (blancos) {
                    $("#" + idObjeto).val(total.toString().replace('.', ',')).trigger('change');
                } else {
                    $("#" + idObjeto).val('').trigger('change');
                }
            });
        } else if (item.IdEvento == funcChangeTab) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregCombo) {
                idObjetoDep = "cmb" + item.IdPreguntaDependiente;
            }
            if (item.Datos.length > 0) {
                var countResp = 0;
                if (respuestas.length > 0) {
                    $.each(respuestas, function (i, r) {
                        if (r.IdPregunta == item.IdPreguntaDependiente) {
                            countResp++;
                        }
                    });
                }
                if (countResp > 0) {
                    for (var i = 1; i <= countResp; i++) {
                        var resp = "";
                        if (respuestas.length > 0) {
                            $.each(respuestas, function (ii, r) {
                                if (r.IdPregunta == item.IdPreguntaDependiente && r.IdTab == i) {
                                    resp = r.Respuesta;
                                    return false;
                                }
                            });
                        }
                        if (item.Datos.length > 0) {
                            $.each(item.Datos, function (ii, d) {
                                if (d.Valor != null) {
                                    var selec = (resp == d.IdParametro.toString() ? "selected" : "");
                                    if ($("#" + (idObjeto + i)).val() == d.Valor) {
                                        $("#" + (idObjetoDep + i)).append("<option value='" + d.IdParametro + "' " + selec + ">" + d.Descripcion + "</option>");
                                    }
                                }
                            });
                        }
                    }
                }
            }
            for (var i = 1; i < parseInt(item.ValorPreguntaDependiente) ; i++) {
                $("#" + (idObjeto + i)).change(function () {
                    var valorObj = $(this).val();
                    $("#" + (idObjetoDep + i)).empty();
                    if (item.Datos.length > 0) {
                        $.each(item.Datos, function (ii, d) {
                            if (d.Valor != null) {
                                if (valorObj == d.Valor) {
                                    $("#" + (idObjetoDep + i)).append("<option value='" + d.IdParametro + "'>" + d.Descripcion + "</option>");
                                }
                            }
                        });
                    }
                });
            }
        } else if (item.IdEvento == funcCambiarTabla) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregTabla) {
                idObjetoDep = "tabla" + item.IdPreguntaDependiente;
            }
            if (item.ValorPreguntaDependiente != "" && item.ValorPreguntaDependiente != null) {
                idObjetoDep = "tablaTab" + item.IdPreguntaDependiente;
                for (var z = 1; z <= parseInt(item.ValorPreguntaDependiente) ; z++) {
                    $("#" + idObjetoDep + z.toString() + " tbody tr").each(function (i) {
                        if ((i + 1) > $("#" + idObjeto + z.toString()).val()) {
                            $(this).css("display", "none");
                        }
                    });
                    $("#" + idObjeto + z.toString()).change(function () {
                        var tab = this.id.substring(3, this.id.length);
                        var numTab = $("#numTab" + tab).val();
                        $("#" + idObjetoDep + numTab + " tbody tr").each(function (i) {
                            if ((i + 1) > $("#" + idObjeto + numTab).val()) {
                                $(this).css("display", "none");
                            } else {
                                $(this).css("display", "");
                            }
                        });
                    });
                }
            } else {
                $("#" + idObjetoDep + " tbody tr").each(function (i) {
                    if ((i + 1) > $("#" + idObjeto).val()) {
                        $(this).css("display", "none");
                    }
                });
                $("#" + idObjeto).change(function () {
                    $("#" + idObjetoDep + " tbody tr").each(function (i) {
                        if ((i + 1) > $("#" + idObjeto).val()) {
                            $(this).css("display", "none");
                        } else {
                            $(this).css("display", "");
                        }
                    });
                });
            }
        } else if (item.IdEvento == funcCopiaTexto) {
            if (item.TipoPregunta == tipoPregTexto || item.CategoriaPregunta == tipoPregNumerico) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregTabla) {
                idObjetoDep = "tabla" + item.IdPreguntaDependiente;
            } else if (item.TipoPreguntaDependiente == tipoPregTextoReadonly || item.CategoriaPregunta == tipoPregNumerico) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            if (item.ValorPregunta != "") {
                for (var i = 1; i <= parseInt(item.ValorPregunta) ; i++) {
                    if ($("#Tab" + (idObjeto + i.toString())).val() != "") {
                        if (item.TipoPreguntaDependiente == tipoPregTabla) {
                            $("#" + idObjetoDep + " tbody tr").each(function (i) {
                                if ((i + 1) == i) {
                                    $(this).children("td").each(function (i2) {
                                        if (i2 == 0) {
                                            $(this).text($("#" + (idObjeto + i)).val());
                                        }
                                    });
                                }
                            });
                        } else {
                            if (item.Datos.length > 0) {
                                $.each(item.Datos, function (ii, d) {
                                    if (i == parseInt(d.Valor2)) {
                                        $("#txt" + d.Valor).val($("#Tab" + (idObjeto + i)).val());
                                    }
                                });
                            }
                        }
                    }
                    $("#Tab" + (idObjeto + i)).keyup(function () {
                        var texto = $(this).val();
                        var tab = this.id.substring(6, this.id.length);
                        var numTab = $("#numTab" + tab).val();
                        if (item.TipoPreguntaDependiente == tipoPregTabla) {
                            $("#" + idObjetoDep + " tbody tr").each(function (i) {
                                if ((i + 1) == i) {
                                    $(this).children("td").each(function (i2) {
                                        if (i2 == 0) {
                                            $(this).text(texto);
                                        }
                                    });
                                }
                            });
                        } else {
                            if (item.Datos.length > 0) {
                                $.each(item.Datos, function (ii, d) {
                                    if (numTab == parseInt(d.Valor2)) {
                                        $("#txt" + d.Valor).val($("#Tab" + (idObjeto + numTab)).val());
                                    }
                                });
                            }
                        }
                    });
                }
            }
        } else if (item.IdEvento == funcKeyUpTab) {
            if (item.TipoPregunta == tipoPregTxtGrande) {
                idObjeto = "txtTab" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregCombo) {
                idObjetoDep = "cmb" + item.IdPreguntaDependiente;
            }
            var totalCaracteres = item.ValorPregunta;
            $("#" + idObjetoDep + " option").each(function (i) {
                if (typeof $("#" + idObjeto + (i + 1)).val() != "undefined") {
                    if ($("#" + idObjeto + (i + 1)).val().length > 0) {
                        var restantes = (totalCaracteres - $("#" + idObjeto + (i + 1)).val().length) < 0 ? 0 : (totalCaracteres - $("#" + idObjeto + (i + 1)).val().length);
                        $("#txtCount" + item.IdPregunta + (i + 1)).text("Caracteres restantes=" + restantes);
                    }
                    $("#" + idObjeto + (i + 1)).keyup(function () {
                        var restantes = (totalCaracteres - $(this).val().length) < 0 ? 0 : (totalCaracteres - $(this).val().length);
                        $("#txtCount" + item.IdPregunta + (i + 1)).text("Caracteres restantes=" + restantes);
                    });
                }
            });
        } else if (item.IdEvento == funcCopiaTextoInput) {
            if (item.CategoriaPregunta == tipoPregNumerico) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregTextoReadonly) {
                idObjetoDep = "txtTab" + item.IdPreguntaDependiente;
            }
            if (item.Valor2Evento != "") {
                for (var i = 1; i <= parseInt(item.Valor2Evento) ; i++) {
                    if ($("#Tab" + (idObjeto + i)).val() != "") {
                        $("#" + (idObjetoDep + i)).val($("#Tab" + (idObjeto + i)).val());
                    }
                    $("#Tab" + (idObjeto + i)).keyup(function () {
                        var tab = this.id.substring(6, this.id.length);
                        var numTab = $("#numTab" + tab).val();
                        $("#" + (idObjetoDep + numTab)).val($(this).val());
                    });
                }
            }
        } else if (item.IdEvento == funcOcultaConSi) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if ($("#" + idObjeto + " option:selected").text().toUpperCase() == "SI" || $("#" + idObjeto + " option:selected").val() == "-1") {
                $("#divPreg" + item.IdPreguntaDependiente).hide();
            } else {
                $("#divPreg" + item.IdPreguntaDependiente).show();
            }
            $("#" + idObjeto).change(function () {
                if ($("#" + idObjeto + " option:selected").text().toUpperCase() == "SI" || $("#" + idObjeto + " option:selected").val() == "-1") {
                    $("#divPreg" + item.IdPreguntaDependiente).hide();
                    $("#txt" + item.IdPreguntaDependiente).val("");
                } else {
                    $("#divPreg" + item.IdPreguntaDependiente).show();
                }
            });
        } else if (item.IdEvento == funcEtiquetaEspejo) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregEtiquetaEspejo) {
                idObjetoDep = "lblEspejo" + item.IdPreguntaDependiente;
            }
            if ($("#" + idObjeto + " option:selected").val() != "-1") {
                $("." + idObjetoDep).text($("#" + idObjeto + " option:selected").text());
            }
            $("#" + idObjeto).change(function () {
                if ($("#" + idObjeto + " option:selected").val() != "-1") {
                    $("." + idObjetoDep).text($("#" + idObjeto + " option:selected").text());
                } else {
                    $("." + idObjetoDep).text("");
                }
            });
        } else if (item.IdEvento == funcMuestraOcultaSegResp) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregTexto || item.TipoPreguntaDependiente == tipoPregTxtGrande) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            if (item.ValorPregunta != "" && item.ValorPregunta != null) {
                for (var i = 1; i <= parseInt(item.ValorPregunta) ; i++) {
                    var valorObj = parseInt($("#Tab" + idObjeto + i.toString() + " option:selected").val());
                    if (item.Datos.length > 0) {
                        respuesta = $.grep(item.Datos, function (n, i) { return n.Valor === valorObj; });
                        if (respuesta.length > 0){
                            $("#divPregTab" + item.IdPreguntaDependiente + i.toString()).show();
                        }else{
                            $("#divPregTab" + item.IdPreguntaDependiente + i.toString()).hide();
                        }
                    }
                    $("#Tab" + idObjeto + i.toString()).change(function () {
                        valorObj = parseInt($(this).val());
                        var tab = this.id.substring(6, this.id.length);
                        var numTab = $("#numTab" + tab).val();
                        if (item.Datos.length > 0) {
                            respuesta = $.grep(item.Datos, function (n, i) { return n.Valor === valorObj; });
                            if (respuesta.length > 0){
                                $("#divPregTab" + item.IdPreguntaDependiente.toString() + numTab).show();
                            }else{
                                $("#divPregTab" + item.IdPreguntaDependiente.toString() + numTab).hide();
                            }
                        }
                    });
                }                
            } else {
                var valorObj = $("#" + idObjeto + " option:selected").val();
                if (item.Datos.length > 0) {
                    $.each(item.Datos, function (ii, d) {                        
                        if (d.Valor != null && d.Valor != "") {
                            if (valorObj == d.Valor.toString()) {
                                $("#divPreg" + item.IdPreguntaDependiente).show();
                            } else {
                                $("#divPreg" + item.IdPreguntaDependiente).hide();
                            }
                        }
                    });
                }
                $("#" + idObjeto).change(function () {
                    valorObj = $(this).val();
                    if (item.Datos.length > 0) {
                        $.each(item.Datos, function (ii, d) {
                            if (d.Valor != null && d.Valor != "") {
                                if (valorObj == d.Valor.toString()) {
                                    $("#divPreg" + item.IdPreguntaDependiente).show();
                                } else {
                                    $("#divPreg" + item.IdPreguntaDependiente).hide();
                                }
                            }
                        });
                    }
                });
            }
        } else if (item.IdEvento == funcOcultaAnidado) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            $.each(item.Datos, function (ii, d) {
                if (d.Valor != null && d.Valor != "") {
                    if ($("#" + idObjeto + " option:selected").text().toUpperCase() == "NO" || $("#" + idObjeto + " option:selected").val() == "-1") {
                        $("#divPreg" + d.Descripcion).hide();
                    } else {
                        if (d.Valor == "1") {
                            $("#divPreg" + d.Descripcion).show();
                        } else {
                            $("#divPreg" + d.Descripcion).hide();
                        }
                    }
                }
            });
            $("#" + idObjeto).change(function () {
                $.each(item.Datos, function (ii, d) {
                    if (d.Valor != null && d.Valor != "") {
                        if ($("#" + idObjeto + " option:selected").text().toUpperCase() == "NO" || $("#" + idObjeto + " option:selected").val() == "-1") {
                            $("#divPreg" + d.Descripcion).hide();
                            $("#txt" + d.Descripcion).val("");
                            $("#cmb" + d.Descripcion + " option[value='-1']").prop('selected', true);
                        } else {
                            if (d.Valor == "1") {
                                $("#divPreg" + d.Descripcion).show();
                            } else {
                                $("#divPreg" + d.Descripcion).hide();
                                $("#txt" + d.Descripcion).val("");
                                $("#cmb" + d.Descripcion + " option[value='-1']").prop('selected', true);
                            }
                        }
                    }
                });
            });
        } else if (item.IdEvento == funcSumaTotalValidacion) {
            if (item.TipoPregunta == tipoPregTotalValidacion) {
                idObjeto = "txt" + item.IdPregunta;
                $("#divTotVal" + item.IdPregunta).removeClass();
                $("#imgValError" + item.IdPregunta).css("display", "none");
                $("#valEstadoError" + item.IdPregunta).css("display", "none");
                $("#imgVal" + item.IdPregunta).css("display", "none");
                $("#valEstado" + item.IdPregunta).css("display", "none");
            } else if (item.TipoPregunta == tipoPregTotal) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregNumerico || item.TipoPreguntaDependiente == tipoPregTotal) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            if (item.Datos.length > 0) {
                var total = 0;
                var blancos = false;
                $.each(item.Datos, function (x, y) {
                    if (y.Valor != null && y.Valor != "") {
                        $(".suma" + y.Valor).each(function () {
                            if ($.trim($(this).val()) != "") {
                                blancos = true;
                                total += retornaValorFormateado($(this).val());
                            }
                        });
                    }
                });
                if (blancos) {
                    $("#" + idObjeto).val(total.toString().replace('.', ',')).trigger('change');
                } else {
                    $("#" + idObjeto).val('').trigger('change');
                }
                if (item.TipoPregunta == tipoPregTotalValidacion) {
                    if ($("#" + idObjeto).val() != $("#" + idObjetoDep).val()) {
                        $("#divTotVal" + item.IdPregunta).addClass("has-error has-feedback");
                        $("#imgValError" + item.IdPregunta).css("display", "");
                        $("#valEstadoError" + item.IdPregunta).css("display", "");
                    } else {
                        $("#divTotVal" + item.IdPregunta).addClass("has-success has-feedback");
                        $("#imgVal" + item.IdPregunta).css("display", "");
                        $("#valEstado" + item.IdPregunta).css("display", "");
                    }
                }
                $.each(item.Datos, function (ii, d) {
                    if (d.Valor != null && d.Valor != "") {
                        $(".suma" + d.Valor).keyup(function () {
                            var total = 0;
                            var blancos = false;
                            $.each(item.Datos, function (iii, dd) {
                                if (dd.Valor != null && dd.Valor != "") {
                                    $(".suma" + dd.Valor).each(function () {
                                        if ($.trim($(this).val()) != "") {
                                            blancos = true;
                                            total += retornaValorFormateado($(this).val());
                                        }
                                    });
                                }
                            });
                            if (blancos) {
                                $("#" + idObjeto).val(total.toString().replace('.', ',')).trigger('change');
                            } else {
                                $("#" + idObjeto).val('').trigger('change');
                            }
                            if (item.TipoPregunta == tipoPregTotalValidacion) {
                                $("#divTotVal" + item.IdPregunta).removeClass();
                                $("#imgValError" + item.IdPregunta).css("display", "none");
                                $("#valEstadoError" + item.IdPregunta).css("display", "none");
                                $("#imgVal" + item.IdPregunta).css("display", "none");
                                $("#valEstado" + item.IdPregunta).css("display", "none");
                                if ($("#" + idObjeto).val() != $("#" + idObjetoDep).val()) {
                                    $("#divTotVal" + item.IdPregunta).addClass("has-error has-feedback");
                                    $("#imgValError" + item.IdPregunta).css("display", "");
                                    $("#valEstadoError" + item.IdPregunta).css("display", "");
                                } else {
                                    $("#divTotVal" + item.IdPregunta).addClass("has-success has-feedback");
                                    $("#imgVal" + item.IdPregunta).css("display", "");
                                    $("#valEstado" + item.IdPregunta).css("display", "");
                                }
                            }
                        });
                    }
                });
            } else {
                if (item.TipoPregunta == tipoPregTotalValidacion) {
                    if ($("#" + idObjeto).val() != $("#" + idObjetoDep).val()) {
                        $("#divTotVal" + item.IdPregunta).addClass("has-error has-feedback");
                        $("#imgValError" + item.IdPregunta).css("display", "");
                        $("#valEstadoError" + item.IdPregunta).css("display", "");
                    } else {
                        $("#divTotVal" + item.IdPregunta).addClass("has-success has-feedback");
                        $("#imgVal" + item.IdPregunta).css("display", "");
                        $("#valEstado" + item.IdPregunta).css("display", "");
                    }
                }
                $(".suma" + item.ValorPregunta).keyup(function () {
                    var total = 0;
                    var blancos = false;
                    $(".suma" + item.ValorPregunta).each(function () {
                        if ($.trim($(this).val()) != "") {
                            blancos = true;
                            total += retornaValorFormateado($(this).val());
                        }
                    });
                    if (blancos) {
                        $("#" + idObjeto).val(total.toString().replace('.', ',')).trigger('change');
                    } else {
                        $("#" + idObjeto).val('').trigger('change');
                    }
                    if (item.TipoPregunta == tipoPregTotalValidacion) {
                        $("#divTotVal" + item.IdPregunta).removeClass();
                        $("#imgValError" + item.IdPregunta).css("display", "none");
                        $("#valEstadoError" + item.IdPregunta).css("display", "none");
                        $("#imgVal" + item.IdPregunta).css("display", "none");
                        $("#valEstado" + item.IdPregunta).css("display", "none");
                        if ($("#" + idObjeto).val() != $("#" + idObjetoDep).val()) {
                            $("#divTotVal" + item.IdPregunta).addClass("has-error has-feedback");
                            $("#imgValError" + item.IdPregunta).css("display", "");
                            $("#valEstadoError" + item.IdPregunta).css("display", "");
                        } else {
                            $("#divTotVal" + item.IdPregunta).addClass("has-success has-feedback");
                            $("#imgVal" + item.IdPregunta).css("display", "");
                            $("#valEstado" + item.IdPregunta).css("display", "");
                        }
                    }
                });
            }
        } else if (item.IdEvento == funcValidaDatosCss) {
            if (item.CategoriaPregunta == tipoPregNumerico) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregTotalValidacion) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            $("#" + idObjeto).keyup(function () {
                $("#divTotVal" + item.IdPreguntaDependiente).removeClass();
                $("#imgValError" + item.IdPreguntaDependiente).css("display", "none");
                $("#valEstadoError" + item.IdPreguntaDependiente).css("display", "none");
                $("#imgVal" + item.IdPreguntaDependiente).css("display", "none");
                $("#valEstado" + item.IdPreguntaDependiente).css("display", "none");
                if ($(this).val() != $("#" + idObjetoDep).val()) {
                    $("#divTotVal" + item.IdPreguntaDependiente).addClass("has-error has-feedback");
                    $("#imgValError" + item.IdPreguntaDependiente).css("display", "");
                    $("#valEstadoError" + item.IdPreguntaDependiente).css("display", "");
                } else {
                    $("#divTotVal" + item.IdPreguntaDependiente).addClass("has-success has-feedback");
                    $("#imgVal" + item.IdPreguntaDependiente).css("display", "");
                    $("#valEstado" + item.IdPreguntaDependiente).css("display", "");
                }
            });
        } else if (item.IdEvento == funcCopiaSumaTotal) {
            if (item.TipoPregunta == tipoPregTotal) {
                idObjeto = "txt" + item.IdPregunta;
            }
            idPreguntaDependiente = 0;
            if (item.Datos.length > 0) {
                $.each(item.Datos, function (i, d) {
                    if (d.Valor != null && d.Valor != "") {
                        idPreguntaDependiente = d.Valor;
                    }
                });
            }            
            $(".suma" + item.ValorPreguntaDependiente + ", .suma" + idPreguntaDependiente).bind("keyup", function () {
                var total = 0;
                var blancos = false;
                $(".suma" + item.ValorPreguntaDependiente).each(function () {
                    if ($.trim($(this).val()) != "") {
                        blancos = true;
                        total += retornaValorFormateado($(this).val());
                    }
                });
                if (item.Datos.length > 0) {
                    $.each(item.Datos, function (i, d) {
                        if (d.Valor != null && d.Valor != "") {
                            if ($.trim($("#txt" + d.Valor).val()) != "") {
                                blancos = true;
                                total += retornaValorFormateado($("#txt" + d.Valor).val());
                            }
                        }
                    });
                }
                if (blancos) {
                    $("#" + idObjeto).val(total.toString().replace('.', ',')).trigger('change');
                } else {
                    $("#" + idObjeto).val('').trigger('change');
                }
            });
        } else if (item.IdEvento == funcChangeHijoPadre) {
            if (item.ValorPregunta != "") {
                for (var i = 1; i <= parseInt(item.ValorPregunta) ; i++) {
                    if (item.CategoriaPregunta == tipoPregCombo) {
                        idObjeto = "Tabcmb" + item.IdPregunta + i.toString();
                    }
                    if (item.CategoriaPreguntaDependiente == tipoPregCombo) {
                        idObjetoDep = "Tabcmb" + item.IdPreguntaDependiente + i.toString();
                    }
                    $("#" + idObjetoDep).append("<option value='-1'>Seleccione</option>");
                    if (item.Datos.length > 0) {
                        var resp = "";
                        if (respuestas.length > 0) {
                            $.each(respuestas, function (ii, r) {
                                if (r.IdPregunta == item.IdPreguntaDependiente && r.IdTab == i) {
                                    resp = r.Respuesta;
                                    return false;
                                }
                            });
                        }
                        $.each(item.Datos, function (ii, d) {
                            if (d.Valor != null) {
                                var selec = (resp == d.IdParametro.toString() ? "selected" : "");
                                if ($("#" + idObjeto).val() == d.Valor) {
                                    $("#" + idObjetoDep).append("<option value='" + d.IdParametro + "' " + selec + ">" + d.Descripcion + "</option>");
                                }
                            }
                        });
                    }
                    $("#" + idObjeto).change(function () {
                        var arrayDatos = [];
                        var valorObj = $(this).val();
                        var nTab = $(this).attr('class').split(/\s+/)[2].split('-')[1];
                        if (nTab != null || nTab != 0) {
                            idObjetoDep = ("cmb" + item.IdPreguntaDependiente + nTab);
                        }
                        $("#" + idObjetoDep).empty();
                        $("#" + idObjetoDep).append("<option value='-1'>Seleccione</option>");
                        if (item.Datos.length > 0) {
                            $.each(item.Datos, function (ii, d) {
                                if (d.Valor != null) {
                                    if (valorObj == d.Valor) {
                                        $("#" + idObjetoDep).append("<option value='" + d.IdParametro + "'>" + d.Descripcion + "</option>");
                                    }
                                }
                            });
                        }
                    });
                }
            } else {
                if (item.CategoriaPregunta == tipoPregCombo) {
                    idObjeto = "cmb" + item.IdPregunta;
                }
                if (item.CategoriaPreguntaDependiente == tipoPregCombo) {
                    idObjetoDep = "cmb" + item.IdPreguntaDependiente;
                }
                $("#" + idObjetoDep).append("<option value='-1'>Seleccione</option>");
                if (item.Datos.length > 0) {
                    var resp = "";
                    if (respuestas.length > 0) {
                        $.each(respuestas, function (i, r) {
                            if (r.IdPregunta == item.IdPreguntaDependiente) {
                                resp = r.Respuesta;
                                return false;
                            }
                        });
                    }
                    $.each(item.Datos, function (i, d) {
                        if (d.Valor != null) {
                            var selec = (resp == d.IdParametro.toString() ? "selected" : "");
                            if ($("#" + idObjeto).val() == d.Valor) {
                                $("#" + idObjetoDep).append("<option value='" + d.IdParametro + "' " + selec + ">" + d.Descripcion + "</option>");
                            }
                        }
                    });
                }
                $("#" + idObjeto).change(function () {
                    var arrayDatos = [];
                    var valorObj = $(this).val();
                    $("#" + idObjetoDep).empty();
                    //$("#" + idObjetoDep).append("<option value='-1'>Seleccione</option>");
                    if (valorObj != "-1") {
                        if (item.Datos.length > 0) {
                            $.each(item.Datos, function (i, d) {
                                if (d.Valor != null) {
                                    if (valorObj == d.Valor) {
                                        $("#" + idObjetoDep).append("<option value='" + d.IdParametro + "'>" + d.Descripcion + "</option>");
                                    }
                                }
                            });
                        }
                    } else {
                        $("#" + idObjetoDep).append("<option value='-1'>Seleccione</option>");
                    }
                });
            }
        } else if (item.IdEvento == funcSumasTab) {
            if (item.TipoPregunta == tipoPregTotal) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregTotalValidacion) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            $(".total" + item.IdPregunta).change(function () {
                $("#divTotVal" + item.IdPreguntaDependiente).removeClass();
                $("#imgValError" + item.IdPreguntaDependiente).css("display", "none");
                $("#valEstadoError" + item.IdPreguntaDependiente).css("display", "none");
                $("#imgVal" + item.IdPreguntaDependiente).css("display", "none");
                $("#valEstado" + item.IdPreguntaDependiente).css("display", "none");
                if ($("#" + idObjeto).val() != $("#" + idObjetoDep).val()) {
                    $("#divTotVal" + item.IdPreguntaDependiente).addClass("has-error has-feedback");
                    $("#imgValError" + item.IdPreguntaDependiente).css("display", "");
                    $("#valEstadoError" + item.IdPreguntaDependiente).css("display", "");
                } else {
                    $("#divTotVal" + item.IdPreguntaDependiente).addClass("has-success has-feedback");
                    $("#imgVal" + item.IdPreguntaDependiente).css("display", "");
                    $("#valEstado" + item.IdPreguntaDependiente).css("display", "");
                }
            });
        } else if (item.IdEvento == funcMarcarRespuesta) {
            if (item.TipoPregunta == tipoPregCheckHijo) {
                idObjeto = "cbx" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregCheckHijo) {
                idObjetoDep = "cbx" + item.IdPreguntaDependiente;
            }
            if ($("#" + idObjeto).is(':checked')) {
                $("#" + idObjetoDep).prop('checked', true);
            } else {
                $("#" + idObjetoDep).prop('checked', false);
            }
            $("#" + idObjeto).click(function () {
                if ($(this).is(':checked')) {
                    $("#" + idObjetoDep).prop('checked', true);
                } else {
                    $("#" + idObjetoDep).prop('checked', false);
                }
            });
        } else if (item.IdEvento == funcOcultaCheckbox) {
            if (item.TipoPregunta == tipoPregCheckHijo || item.TipoPregunta == tipoPregCheckVacio || item.TipoPregunta == tipoPregCheck) {
                idObjeto = "cbx" + item.IdPregunta;
            }
            if (item.ValorPregunta != "" && item.ValorPregunta != null) {
                for (var i = 1; i <= parseInt(item.ValorPregunta) ; i++) {
                    if ($("#Tab" + idObjeto + i.toString()).is(':checked')) {
                        $("#divPregTab" + item.IdPreguntaDependiente + i.toString()).show();
                    } else {
                        $("#divPregTab" + item.IdPreguntaDependiente + i.toString()).hide();
                    }
                    $("#Tab" + idObjeto + i.toString()).click(function () {
                        var nTab = $(this).attr('class').split(/\s+/)[0].split('-')[1];
                        if (nTab != null || nTab != 0) {
                            if ($(this).is(':checked')) {
                                $("#divPregTab" + item.IdPreguntaDependiente + nTab).show();
                            } else {
                                $("#divPregTab" + item.IdPreguntaDependiente + nTab).hide();
                            }
                        }
                    });
                }
            } else {
                if ($("#" + idObjeto).is(':checked')) {
                    $("#divPreg" + item.IdPreguntaDependiente).show();
                } else {
                    $("#divPreg" + item.IdPreguntaDependiente).hide();
                }
                $("#" + idObjeto).click(function () {
                    if ($(this).is(':checked')) {
                        $("#divPreg" + item.IdPreguntaDependiente).show();
                    } else {
                        $("#divPreg" + item.IdPreguntaDependiente).hide();
                    }
                });
            }
        } else if (item.IdEvento == funcOculPregTab) {
            if (item.ValorPregunta != "") {
                for (var i = 1; i <= parseInt(item.ValorPregunta) ; i++) {
                    if (item.CategoriaPregunta == tipoPregCombo) {
                        idObjeto = "Tabcmb" + item.IdPregunta + i.toString();
                    }
                    if (item.TipoPreguntaDependiente == tipoPregTexto || item.TipoPreguntaDependiente == tipoPregTxtGrande) {
                        idObjetoDep = "txtTab" + item.IdPreguntaDependiente + i.toString();
                    }
                    if (item.Valor2Evento != "") {
                        if ($("#" + idObjeto + " option:selected").val() == item.Valor2Evento) {
                            $("#divPregTab" + item.IdPreguntaDependiente + i.toString()).show();
                        } else {
                            $("#divPregTab" + item.IdPreguntaDependiente + i.toString()).hide();
                        }
                    }
                    $("#" + idObjeto).change(function () {
                        var nTab = $(this).attr('class').split(/\s+/)[2].split('-')[1];
                        if (nTab != null || nTab != 0) {
                            idObjeto = ("Tabcmb" + item.IdPregunta + nTab);
                            idObjetoDep = ("txtTab" + item.IdPreguntaDependiente + nTab);
                        }
                        if (item.Valor2Evento != "") {
                            if ($("#" + idObjeto + " option:selected").val() == item.Valor2Evento) {
                                $("#divPregTab" + item.IdPreguntaDependiente + nTab).show();
                            } else {
                                $("#divPregTab" + item.IdPreguntaDependiente + nTab).hide();
                            }
                        }
                    });
                }
            }
        } else if (item.IdEvento == funcSumaTotalAnidada) {
            if (item.CategoriaPregunta == tipoPregNumerico) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregTotal) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            $("#" + idObjeto).keyup(function () {
                var total = 0;
                var valor1 = 0;
                var valor2 = 0;
                var blancos = false;
                if ($.trim($(this).val()) != "") {
                    blancos = true;
                    valor1 = retornaValorFormateado($(this).val());
                }
                if ($.trim($("#" + idObjetoDep).val()) != "") {
                    blancos = true;
                    valor2 = retornaValorFormateado($("#" + idObjetoDep).val());
                }
                if (blancos) {
                    total = valor1 + valor2;
                    $("#txt" + item.ValorPregunta).val(total.toString().replace('.', ','));
                } else {
                    $("#txt" + item.ValorPregunta).val('');
                }
            });
        } else if (item.IdEvento == funcLlenaComboConOtro) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregCombo) {
                idObjetoDep = "cmb" + item.IdPreguntaDependiente;
            }
            if (item.Datos.length > 0) {
                $.each(item.Datos, function (i, d) {
                    if (d.Valor != null) {
                        for (var y = 1; y <= parseInt(d.Valor2) ; y++) {
                            $("#Tab" + idObjeto + y).change(function () {
                                var totalCausas = $("#cmb" + d.Valor).val();
                                var valorObj = $(this).val();
                                var arrayDatos = [];
                                for (var a = 1; a <= parseInt(totalCausas) ; a++) {
                                    causa = $("#Tab" + idObjetoDep + a + " option:selected").val();
                                    if (causa != "-1") {
                                        if (arrayDatos.indexOf(causa) < 0) {
                                            arrayDatos.push(causa);
                                        }
                                    }
                                }
                                var nTab = $(this).attr('class').split(/\s+/)[2].split('-')[1];
                                if (arrayDatos.length > 0) {
                                    if (arrayDatos.indexOf(valorObj) < 0) {
                                        if (nTab != null || nTab != 0) {
                                            $("#Tab" + idObjeto + nTab + " option[value='-1']").prop('selected', true);
                                        }
                                        $("#txtMensajesFormulario").empty();
                                        $("#txtMensajesFormulario").html("<p>Causa seleccionada no definida en el diagnóstico del programa</p>");
                                        $('#modalMensajesFormularios').modal('show');
                                    }
                                } else {
                                    if (nTab != null || nTab != 0) {
                                        $("#Tab" + idObjeto + nTab + " option[value='-1']").prop('selected', true);
                                    }
                                    $("#txtMensajesFormulario").empty();
                                    $("#txtMensajesFormulario").html("<p>Seleccione al menos una causa en el diagnóstico del programa</p>");
                                    $('#modalMensajesFormularios').modal('show');
                                }
                            });
                        }
                    }
                });
            }
        } else if (item.IdEvento == funcValidaSelecUnica) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregCombo) {
                idObjetoDep = "cmb" + item.IdPreguntaDependiente;
            }
            for (var y = 1; y <= parseInt(item.ValorPregunta) ; y++) {
                $("#Tab" + idObjeto + y).change(function () {
                    var valorObj = $(this).val();
                    var nTab = $(this).attr('class').split(/\s+/)[2].split('-')[1];
                    for (var a = 1; a <= parseInt(item.ValorPregunta) ; a++) {
                        if (nTab != a) {
                            causa = $("#Tab" + idObjeto + a + " option:selected").val();
                            if (causa != "-1") {
                                if (causa == valorObj) {
                                    if (nTab != null || nTab != 0) {
                                        $("#Tab" + idObjeto + nTab + " option[value='-1']").prop('selected', true);
                                        $("#txtMensajesFormulario").empty();
                                        $("#txtMensajesFormulario").html("<p>Valor ya seleccionado, seleccione uno distinto</p>");
                                        $('#modalMensajesFormularios').modal('show');
                                    }
                                }
                            }
                        }
                    }
                });
            }
        } else if (item.IdEvento == funcCalculoDivision) {
            if (item.CategoriaPregunta == tipoPregNumerico || item.TipoPregunta == tipoPregCheckInline) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregNumerico || item.TipoPregunta == tipoPregCheckInline) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            if (item.ValorPregunta != "" && item.ValorPregunta != null) {
                for (var y = 1; y <= parseInt(item.ValorPregunta) ; y++) {
                    if (item.Datos.length > 0) {
                        $.each(item.Datos, function (i, d) {
                            if (d.Valor != "" && d.Valor != null) {
                                var blancos = false;
                                var total = 0;
                                var valor1 = 0;
                                var valor2 = 0;
                                if ($.trim($("#Tab" + idObjeto + y).val()) != "") {
                                    blancos = true;
                                    valor1 = retornaValorFormateado($("#Tab" + idObjeto + y).val());
                                }
                                if ($.trim($("#Tab" + idObjetoDep + y).val()) != "") {
                                    blancos = true;
                                    valor2 = retornaValorFormateado($("#Tab" + idObjetoDep + y).val());
                                }
                                if (blancos) {
                                    if (valor2 > 0) {
                                        total = Math.round((valor1 / valor2) * 100) / 100;
                                        $("#Tabtxt" + d.Valor + y).val(total.toString().replace('.', ','));
                                    }
                                } else {
                                    $("#Tabtxt" + d.Valor + y).val('');
                                }
                            }                    
                        });
                    }
                    $("#Tab" + idObjeto + y).keyup(function () {
                        var nTab = $(this).attr('class').split(/\s+/)[2].split('-')[1];
                        if (item.Datos.length > 0) {
                            $.each(item.Datos, function (i, d) {
                                if (d.Valor != "" && d.Valor != null) {
                                    var blancos = false;
                                    var total = 0;
                                    var valor1 = 0;
                                    var valor2 = 0;                                    
                                    if ($.trim($("#Tab" + idObjeto + nTab).val()) != "") {
                                        blancos = true;
                                        valor1 = retornaValorFormateado($("#Tab" + idObjeto + nTab).val());
                                    }
                                    if ($.trim($("#Tab" + idObjetoDep + nTab).val()) != "") {
                                        blancos = true;
                                        valor2 = retornaValorFormateado($("#Tab" + idObjetoDep + nTab).val());
                                    }
                                    if (blancos) {
                                        if (valor2 > 0) {
                                            total = Math.round((valor1 / valor2) * 100) / 100;
                                            $("#Tabtxt" + d.Valor + nTab).val(total.toString().replace('.', ','));
                                        }
                                    } else {
                                        $("#Tabtxt" + d.Valor + nTab).val('');
                                    }
                                }
                            });
                        }
                    });
                    $("#Tab" + idObjetoDep + y).keyup(function () {
                        var nTab = $(this).attr('class').split(/\s+/)[2].split('-')[1];
                        if (item.Datos.length > 0) {
                            $.each(item.Datos, function (i, d) {
                                if (d.Valor != "" && d.Valor != null) {
                                    var blancos = false;
                                    var total = 0;
                                    var valor1 = 0;
                                    var valor2 = 0;                                    
                                    if ($.trim($("#Tab" + idObjeto + nTab).val()) != "") {
                                        blancos = true;
                                        valor1 = retornaValorFormateado($("#Tab" + idObjeto + nTab).val());
                                    }
                                    if ($.trim($("#Tab" + idObjetoDep + nTab).val()) != "") {
                                        blancos = true;
                                        valor2 = retornaValorFormateado($("#Tab" + idObjetoDep + nTab).val());
                                    }
                                    if (blancos) {
                                        if (valor2 > 0) {
                                            total = Math.round((valor1 / valor2) * 100) / 100;
                                            $("#Tabtxt" + d.Valor + nTab).val(total.toString().replace('.', ','));
                                        }
                                    } else {
                                        $("#Tabtxt" + d.Valor + nTab).val('');
                                    }
                                }
                            });
                        }
                    });
                }
            }else{
                if (item.Datos.length > 0) {
                    $.each(item.Datos, function (i, d) {
                        if (d.Valor != "" && d.Valor != null) {
                            var blancos = false;
                            var total = 0;
                            var valor1 = 0;
                            var valor2 = 0;
                            if ($.trim($("#" + idObjeto).val()) != "") {
                                blancos = true;
                                valor1 = retornaValorFormateado($("#" + idObjeto).val());
                            }
                            if ($.trim($("#" + idObjetoDep).val()) != "") {
                                blancos = true;
                                valor2 = retornaValorFormateado($("#" + idObjetoDep).val());
                            }
                            if (blancos) {
                                if (valor2 > 0) {
                                    total = Math.round((valor1 / valor2) * 100) / 100;
                                    $("#txt" + d.Valor).val(total.toString().replace('.', ','));
                                }
                            } else {
                                $("#txt" + d.Valor).val('');
                            }
                        }                    
                    });
                }
                $("#" + idObjeto).keyup(function () {
                    if (item.Datos.length > 0) {
                        $.each(item.Datos, function (i, d) {
                            if (d.Valor != "" && d.Valor != null) {
                                var blancos = false;
                                var total = 0;
                                var valor1 = 0;
                                var valor2 = 0;
                                if ($.trim($("#" + idObjeto).val()) != "") {
                                    blancos = true;
                                    valor1 = retornaValorFormateado($("#" + idObjeto).val());
                                }
                                if ($.trim($("#" + idObjetoDep).val()) != "") {
                                    blancos = true;
                                    valor2 = retornaValorFormateado($("#" + idObjetoDep).val());
                                }
                                if (blancos) {
                                    if (valor2 > 0) {
                                        total = Math.round((valor1 / valor2) * 100) / 100;
                                        $("#txt" + d.Valor).val(total.toString().replace('.', ','));
                                    }
                                } else {
                                    $("#txt" + d.Valor).val('');
                                }
                            }
                        });
                    }
                });
                $("#" + idObjetoDep).keyup(function () {
                    if (item.Datos.length > 0) {
                        $.each(item.Datos, function (i, d) {
                            if (d.Valor != "" && d.Valor != null) {
                                var blancos = false;
                                var total = 0;
                                var valor1 = 0;
                                var valor2 = 0;
                                if ($.trim($("#" + idObjeto).val()) != "") {
                                    blancos = true;
                                    valor1 = retornaValorFormateado($("#" + idObjeto).val());
                                }
                                if ($.trim($("#" + idObjetoDep).val()) != "") {
                                    blancos = true;
                                    valor2 = retornaValorFormateado($("#" + idObjetoDep).val());
                                }
                                if (blancos) {
                                    if (valor2 > 0) {
                                        total = Math.round((valor1 / valor2) * 100) / 100;
                                        $("#txt" + d.Valor).val(total.toString().replace('.', ','));
                                    }
                                } else {
                                    $("#txt" + d.Valor).val('');
                                }
                            }
                        });                            
                    }
                });
            }            
        } else if (item.IdEvento == funcComparaTotales) {
            if (item.TipoPregunta == tipoPregTotal) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregTotal) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            $("#divTotVal" + item.IdPregunta).removeClass();
            $("#imgValError" + item.IdPregunta).css("display", "none");
            $("#valEstadoError" + item.IdPregunta).css("display", "none");
            $("#imgVal" + item.IdPregunta).css("display", "none");
            $("#valEstado" + item.IdPregunta).css("display", "none");
            if ($("#" + idObjeto).val() != $("#" + idObjetoDep).val()) {
                $("#divTotVal" + item.IdPregunta).addClass("has-error has-feedback");
                $("#imgValError" + item.IdPregunta).css("display", "");
                $("#valEstadoError" + item.IdPregunta).css("display", "");
            } else {
                $("#divTotVal" + item.IdPregunta).addClass("has-success has-feedback");
                $("#imgVal" + item.IdPregunta).css("display", "");
                $("#valEstado" + item.IdPregunta).css("display", "");
            }
            $("#" + idObjeto).change(function () {
                $("#divTotVal" + item.IdPregunta).removeClass();
                $("#imgValError" + item.IdPregunta).css("display", "none");
                $("#valEstadoError" + item.IdPregunta).css("display", "none");
                $("#imgVal" + item.IdPregunta).css("display", "none");
                $("#valEstado" + item.IdPregunta).css("display", "none");
                if ($("#" + idObjeto).val() != $("#" + idObjetoDep).val()) {
                    $("#divTotVal" + item.IdPregunta).addClass("has-error has-feedback");
                    $("#imgValError" + item.IdPregunta).css("display", "");
                    $("#valEstadoError" + item.IdPregunta).css("display", "");
                } else {
                    $("#divTotVal" + item.IdPregunta).addClass("has-success has-feedback");
                    $("#imgVal" + item.IdPregunta).css("display", "");
                    $("#valEstado" + item.IdPregunta).css("display", "");
                }
            });
        } else if (item.IdEvento == funcComparaTotalDepend) {
            if (item.TipoPregunta == tipoPregTotal || item.CategoriaPregunta == tipoPregNumerico || item.TipoPregunta == tipoPregTotalValidacion) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregTotal || item.CategoriaPregunta == tipoPregNumerico || item.TipoPregunta == tipoPregTotalValidacion) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            $("#divTotVal" + item.IdPreguntaDependiente).removeClass();
            $("#imgValError" + item.IdPreguntaDependiente).css("display", "none");
            $("#valEstadoError" + item.IdPreguntaDependiente).css("display", "none");
            $("#imgVal" + item.IdPreguntaDependiente).css("display", "none");
            $("#valEstado" + item.IdPreguntaDependiente).css("display", "none");
            if ($("#" + idObjeto).val() != $("#" + idObjetoDep).val()) {
                $("#divTotVal" + item.IdPreguntaDependiente).addClass("has-error has-feedback");
                $("#imgValError" + item.IdPreguntaDependiente).css("display", "");
                $("#valEstadoError" + item.IdPreguntaDependiente).css("display", "");
            } else {
                $("#divTotVal" + item.IdPreguntaDependiente).addClass("has-success has-feedback");
                $("#imgVal" + item.IdPreguntaDependiente).css("display", "");
                $("#valEstado" + item.IdPreguntaDependiente).css("display", "");
            }
            $("#" + idObjeto + ", #" + idObjetoDep).bind("keyup change", function () {
                $("#divTotVal" + item.IdPreguntaDependiente).removeClass();
                $("#imgValError" + item.IdPreguntaDependiente).css("display", "none");
                $("#valEstadoError" + item.IdPreguntaDependiente).css("display", "none");
                $("#imgVal" + item.IdPreguntaDependiente).css("display", "none");
                $("#valEstado" + item.IdPreguntaDependiente).css("display", "none");
                if ($("#" + idObjeto).val() != $("#" + idObjetoDep).val()) {
                    $("#divTotVal" + item.IdPreguntaDependiente).addClass("has-error has-feedback");
                    $("#imgValError" + item.IdPreguntaDependiente).css("display", "");
                    $("#valEstadoError" + item.IdPreguntaDependiente).css("display", "");
                } else {
                    $("#divTotVal" + item.IdPreguntaDependiente).addClass("has-success has-feedback");
                    $("#imgVal" + item.IdPreguntaDependiente).css("display", "");
                    $("#valEstado" + item.IdPreguntaDependiente).css("display", "");
                }
            });
        } else if (item.IdEvento == funcFiltraSelect) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregCombo) {
                idObjetoDep = "cmb" + item.IdPreguntaDependiente;
            }
            if (item.ValorPregunta != "" && item.ValorPregunta != null) {
                for (var y = 1; y <= parseInt(item.ValorPregunta) ; y++) {
                    if (item.Datos.length > 0) {
                        var valorOriginal = $("#Tab" + idObjetoDep + y + " option:selected").val();
                        valorSelec = $("#Tab" + idObjeto + y + " option:selected").val();
                        $("#Tab" + idObjetoDep + y).empty();
                        $("#Tab" + idObjetoDep + y).append("<option value='-1'>Seleccione</option>");
                        if (valorSelec != "-1") {
                            $.each(item.Datos, function (i, d) {
                                if (d.Valor2 != null && d.Valor2 != "") {
                                    if (d.Valor2.toString().indexOf('.') > 0) {
                                        var origen = parseFloat(d.Valor2.toString().replace(',', '.'));
                                        var s = parseInt(origen.toString().split('.')[1]);
                                        var compTemp = parseFloat("1." + valorSelec);
                                        var comp = parseInt(compTemp.toString().split('.')[1]);
                                        if (comp == s) {
                                            var sel = (valorOriginal == d.IdParametro.toString() ? "selected" : "");
                                            $("#Tab" + idObjetoDep + y).append("<option value='" + d.IdParametro + "' " + sel + ">" + d.Descripcion + "</option>");
                                        }
                                    }
                                }
                            });
                        }
                    }
                    $("#Tab" + idObjeto + y).change(function () {
                        var nTab = $(this).attr('class').split(/\s+/)[2].split('-')[1];
                        if (nTab != null || nTab != 0) {
                            $("#Tab" + idObjetoDep + nTab).empty();
                            $("#Tab" + idObjetoDep + nTab).append("<option value='-1'>Seleccione</option>");
                            if (item.Datos.length > 0) {
                                valorSelec = $("#Tab" + idObjeto + nTab + " option:selected").val();
                                if (valorSelec != "-1") {
                                    $.each(item.Datos, function (i, d) {
                                        if (d.Valor2 != null && d.Valor2 != "") {
                                            if (d.Valor2.toString().indexOf('.') > 0) {
                                                var origen = parseFloat(d.Valor2.toString().replace(',', '.'));
                                                var s = parseInt(origen.toString().split('.')[1]);
                                                var compTemp = parseFloat("1." + valorSelec);
                                                var comp = parseInt(compTemp.toString().split('.')[1]);
                                                if (comp == s) {
                                                    $("#Tab" + idObjetoDep + nTab).append("<option value='" + d.IdParametro + "'>" + d.Descripcion + "</option>");
                                                }
                                            }
                                        }
                                    });
                                }
                            }
                        }
                    });
                }
            }else{
                if (item.Datos.length > 0) {                    
                    var valorOriginal = $("#" + idObjetoDep + " option:selected").val();
                    valorSelec = $("#" + idObjeto + " option:selected").val();
                    $("#" + idObjetoDep).empty();
                    $("#" + idObjetoDep).append("<option value='-1'>Seleccione</option>");
                    if (valorSelec != "-1") {
                        $.each(item.Datos, function (i, d) {
                            if (d.Valor2 != null && d.Valor2 != "") {
                                if (d.Valor2.toString().indexOf('.') > 0) {
                                    var origen = parseFloat(d.Valor2.toString().replace(',', '.'));
                                    var s = parseInt(origen.toString().split('.')[1]);
                                    var compTemp = parseFloat("1." + valorSelec);
                                    var comp = parseInt(compTemp.toString().split('.')[1]);
                                    if (comp == s) {
                                        var sel = (valorOriginal == d.IdParametro.toString() ? "selected" : "");
                                        $("#" + idObjetoDep).append("<option value='" + d.IdParametro + "' " + sel + ">" + d.Descripcion + "</option>");
                                    }
                                }
                            }
                        });
                    }
                }
                $("#" + idObjeto).change(function () {
                    $("#" + idObjetoDep).empty();
                    $("#" + idObjetoDep).append("<option value='-1'>Seleccione</option>");
                    if (item.Datos.length > 0) {
                        valorSelec = $("#" + idObjeto + " option:selected").val();
                        if (valorSelec != "-1") {
                            $.each(item.Datos, function (i, d) {
                                if (d.Valor2 != null && d.Valor2 != "") {
                                    if (d.Valor2.toString().indexOf('.') > 0) {
                                        var origen = parseFloat(d.Valor2.toString().replace(',', '.'));
                                        var s = parseInt(origen.toString().split('.')[1]);
                                        var compTemp = parseFloat("1." + valorSelec);
                                        var comp = parseInt(compTemp.toString().split('.')[1]);
                                        if (comp == s) {
                                            $("#" + idObjetoDep).append("<option value='" + d.IdParametro + "'>" + d.Descripcion + "</option>");
                                        }
                                    }
                                }
                            });
                        }
                    }
                });
            }
        } else if (item.IdEvento == funcLimpiaSelect) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregCombo) {
                idObjetoDep = "cmb" + item.IdPreguntaDependiente;
            }
            if (item.ValorPregunta != "") {
                for (var y = 1; y <= parseInt(item.ValorPregunta) ; y++) {
                    $("#Tab" + idObjeto + y).change(function () {
                        var nTab = $(this).attr('class').split(/\s+/)[2].split('-')[1];
                        if (nTab != null || nTab != 0) {
                            $("#Tab" + idObjetoDep + nTab).empty();
                            $("#Tab" + idObjetoDep + nTab).append("<option value='-1'>Seleccione</option>");
                        }
                    });
                }
            }
        } else if (item.IdEvento == funcFiltraComboValor) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregCombo) {
                idObjetoDep = "cmb" + item.IdPreguntaDependiente;
            }
            if (item.ValorPregunta != "") {
                for (var y = 1; y <= parseInt(item.ValorPregunta) ; y++) {
                    if (item.Datos.length > 0) {
                        var valorSelec = $("#Tab" + idObjeto + y + " option:selected").val();
                        var valorSelecDepen = $("#" + idObjetoDep + " option:selected").val();
                        $("#Tab" + idObjeto + y).empty();
                        $("#Tab" + idObjeto + y).append("<option value='-1'>Seleccione</option>");
                        if (valorSelecDepen != "-1") {
                            $.each(item.Datos, function (i, d) {
                                if (d.Valor2 != null) {
                                    if (d.Valor2.toString().indexOf('.') > 0) {
                                        var origen = parseFloat(d.Valor2.toString().replace(',', '.'));
                                        var s = parseInt(origen.toString().split('.')[1]);
                                        var compTemp = parseFloat("1." + valorSelecDepen);
                                        var comp = parseInt(compTemp.toString().split('.')[1]);
                                        if (comp == s) {
                                            var sel = (valorSelec == d.IdParametro.toString() ? "selected" : "");
                                            $("#Tab" + idObjeto + y).append("<option value='" + d.IdParametro + "' " + sel + ">" + d.Descripcion + "</option>");
                                        }
                                    }
                                }
                            });
                        }
                    }
                }
            } else {
                if (item.Datos.length > 0) {
                    var valorSelec = $("#" + idObjeto + " option:selected").val();
                    var valorSelecDepen = $("#" + idObjetoDep + " option:selected").val();
                    $("#" + idObjeto).empty();
                    $("#" + idObjeto).append("<option value='-1'>Seleccione</option>");
                    if (valorSelecDepen != "-1") {
                        $.each(item.Datos, function (i, d) {
                            if (d.Valor2 != null) {
                                if (d.Valor2.toString().indexOf('.') > 0) {
                                    var origen = parseFloat(d.Valor2.toString().replace(',', '.'));
                                    var s = parseInt(origen.toString().split('.')[1]);
                                    var compTemp = parseFloat("1." + valorSelecDepen);
                                    var comp = parseInt(compTemp.toString().split('.')[1]);
                                    if (comp == s) {
                                        var sel = (valorSelec == d.IdParametro.toString() ? "selected" : "");
                                        $("#" + idObjeto).append("<option value='" + d.IdParametro + "' " + sel + ">" + d.Descripcion + "</option>");
                                    }
                                }
                            }
                        });
                    }
                }
            }
            $("#" + idObjetoDep).change(function () {
                if (item.ValorPregunta != "") {
                    for (var y = 1; y <= parseInt(item.ValorPregunta) ; y++) {
                        if (item.Datos.length > 0) {
                            var valorSelec = $("#Tab" + idObjeto + y + " option:selected").val();
                            var valorSelecDepen = $("#" + idObjetoDep + " option:selected").val();
                            $("#Tab" + idObjeto + y).empty();
                            $("#Tab" + idObjeto + y).append("<option value='-1'>Seleccione</option>");
                            if (valorSelecDepen != "-1") {
                                $.each(item.Datos, function (i, d) {
                                    if (d.Valor2 != null) {
                                        if (d.Valor2.toString().indexOf('.') > 0) {
                                            var origen = parseFloat(d.Valor2.toString().replace(',', '.'));
                                            var s = parseInt(origen.toString().split('.')[1]);
                                            var compTemp = parseFloat("1." + valorSelecDepen);
                                            var comp = parseInt(compTemp.toString().split('.')[1]);
                                            if (comp == s) {
                                                $("#Tab" + idObjeto + y).append("<option value='" + d.IdParametro + "'>" + d.Descripcion + "</option>");
                                            }
                                        }
                                    }
                                });
                            }
                        }
                    }
                } else {
                    if (item.Datos.length > 0) {
                        var valorSelec = $("#" + idObjeto + " option:selected").val();
                        var valorSelecDepen = $("#" + idObjetoDep + " option:selected").val();
                        $("#" + idObjeto).empty();
                        $("#" + idObjeto).append("<option value='-1'>Seleccione</option>");
                        if (valorSelecDepen != "-1") {
                            $.each(item.Datos, function (i, d) {
                                if (d.Valor2 != null) {
                                    if (d.Valor2.toString().indexOf('.') > 0) {
                                        var origen = parseFloat(d.Valor2.toString().replace(',', '.'));
                                        var s = parseInt(origen.toString().split('.')[1]);
                                        var compTemp = parseFloat("1." + valorSelecDepen);
                                        var comp = parseInt(compTemp.toString().split('.')[1]);
                                        if (comp == s) {
                                            $("#" + idObjeto).append("<option value='" + d.IdParametro + "'>" + d.Descripcion + "</option>");
                                        }
                                    }
                                }
                            });
                        }
                    }
                }
            });
        } else if (item.IdEvento == funcHabilitaTexto) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregCombo) {
                idObjetoDep = "cmb" + item.IdPreguntaDependiente;
            }
            if (item.ValorPregunta != "") {                
                for (var y = 1; y <= parseInt(item.ValorPregunta) ; y++) {
                    if (item.Datos.length > 0) {
                        var valorSelec = $("#Tab" + idObjeto + y + " option:selected").val();
                        var comparar = 0;
                        $.each(item.Datos, function (i, d) {
                            if (d.Valor2 != null && d.Valor2 != "") {
                                if (d.Valor2 == "-1") {
                                    comparar = d.Valor;
                                }
                            }
                        });
                        $.each(item.Datos, function (i, d) {
                            if (d.Valor2 != null) {
                                if (d.Valor2.toString() == "0") {
                                    if (d.Valor != null && d.Valor != "") {
                                        if (valorSelec == comparar) {
                                            $("#divPregTab" + d.Valor + y).css("display", "none");
                                        } else {
                                            $("#divPregTab" + d.Valor + y).css("display", "");
                                        }
                                    }
                                } else {
                                    if (d.Valor != null && d.Valor != "") {
                                        if (valorSelec != comparar) {
                                            $("#divPregTab" + d.Valor + y).css("display", "none");
                                        }
                                    }
                                }
                            }
                        });
                    }
                    $("#Tab" + idObjeto + y).change(function () {
                        var nTab = $(this).attr('class').split(/\s+/)[2].split('-')[1];
                        if (nTab != null || nTab != 0) {
                            if (item.Datos.length > 0) {
                                var valorSelec = $("#Tab" + idObjeto + nTab + " option:selected").val();
                                var comparar = 0;
                                $.each(item.Datos, function (i, d) {
                                    if (d.Valor2 != null && d.Valor2 != "") {
                                        if (d.Valor2 == "-1") {
                                            comparar = d.Valor;
                                        }
                                    }
                                });
                                $.each(item.Datos, function (i, d) {
                                    if (d.Valor2 != null && d.Valor2 != "") {
                                        if (d.Valor2 == "0") {
                                            if (d.Valor != null && d.Valor != "") {
                                                if (valorSelec == comparar) {
                                                    $("#divPregTab" + d.Valor + nTab).css("display", "none");
                                                } else {
                                                    $("#divPregTab" + d.Valor + nTab).css("display", "");
                                                }
                                            }
                                        } else {
                                            if (d.Valor != null && d.Valor != "") {
                                                if (valorSelec != comparar) {
                                                    $("#divPregTab" + d.Valor + nTab).css("display", "none");
                                                } else {
                                                    $("#divPregTab" + d.Valor + nTab).css("display", "");
                                                }
                                            }
                                        }
                                    }
                                });
                            }
                        }
                    });
                }
            }
        } else if (item.IdEvento == funcValidaComboXCombo) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregCombo) {
                idObjetoDep = "cmb" + item.IdPreguntaDependiente;
            }
            $("#" + idObjeto).change(function () {
                var valorSelec = $("#" + idObjeto + " option:selected").val();
                var valorComp = $("#" + idObjetoDep + " option:selected").val();
                if (valorSelec < valorComp) {
                    $("#txtMensajesFormulario").empty();
                    $("#txtMensajesFormulario").html("<p>Cantidad de componentes menor a cantidad de causas del problema planteado</p>");
                    $('#modalMensajesFormularios').modal('show');
                }
            });
        } else if (item.IdEvento == funcValidaErroresMayorQue) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            } else if (item.CategoriaPregunta == tipoPregTotal || item.CategoriaPregunta == tipoPregNumerico) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.Datos.length > 0) {
                var valorSelec = $("#" + idObjeto + " option:selected").val();
                $.each(item.Datos, function (i, d) {
                    if (d.Valor != null && d.Valor != "") {
                        var valorComp = $("#cmb" + d.Valor + " option:selected").val();
                        if (parseInt(valorSelec) > parseInt(valorComp)) {
                            $("#divErrores").css("display", "");
                            $("#ulListaErrores").append('<li>' + d.Descripcion + '</li>');
                        }
                    }
                });
            }
        } else if (item.IdEvento == funcValidaErroresIgualQue) {
            if (item.TipoPregunta == tipoPregTotal || item.TipoPregunta == tipoPregNumerico) {
                idObjeto = "txt" + item.IdPregunta;
            } else if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.Datos.length > 0) {
                var valorSelec = $("#" + idObjeto).val();
                $.each(item.Datos, function (i, d) {
                    if (d.Valor != null && d.Valor != "") {
                        var valorComp = $("#txt" + d.Valor).val();
                        if (valorSelec != "" || valorComp != "") {
                            if (parseInt(valorSelec) != parseInt(valorComp)) {
                                $("#divErrores").css("display", "");
                                $("#ulListaErrores").append('<li>' + d.Descripcion + '</li>');
                            }
                        }
                    }
                });
            }
        } else if (item.IdEvento == funcValidaErroresNoCont) {
            if (item.TipoPregunta == tipoPregTotal || item.TipoPregunta == tipoPregNumerico) {
                idObjeto = "txt" + item.IdPregunta;
            } else if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregCombo) {
                idObjetoDep = "cmb" + item.IdPreguntaDependiente;
            } else if (item.TipoPregunta == tipoPregTotal || item.TipoPregunta == tipoPregNumerico) {
                idObjetoDep = "txt" + item.IdPregunta;
            }
            if (item.Datos.length > 0) {
                var valorSelec = $("#" + idObjeto + " option:selected").val();
                var valorSelec2 = $("#" + idObjetoDep + " option:selected").val();
                $.each(item.Datos, function (i, d) {
                    if (d.Valor != null && d.Valor != "") {
                        if (valorSelec == "902") {
                            for (var y = 1; y <= parseInt(valorSelec2) ; y++) {
                                var textoData = "";
                                if (d.Valor2 == "-1") {
                                    if ($.trim($("#Tabtxt" + d.Valor + y).val()) == "") {
                                        textoData = "-1";
                                    }
                                } else if (d.Valor2 == "-2") {
                                    if ($("#Tabcmb" + d.Valor + y + " option:selected").val() == "-1") {
                                        textoData = "-1";
                                    }
                                } else if (d.Valor2 == "-3") {
                                    if ($.trim($("#txtTab" + d.Valor + y).val()) == "") {
                                        textoData = "-1";
                                    }
                                }
                            }
                            if (textoData == "-1") {
                                $("#divErrores").css("display", "");
                                $("#ulListaErrores").append('<li>' + d.Descripcion + '</li>');
                            }
                        }
                    }
                });
            }
        } else if (item.IdEvento == funcValidaErroresNoCont2) {
            if (item.Datos.length > 0) {
                var check = 0;
                var tipo = 0;
                $.each(item.Datos, function (i, d) {
                    if (d.Valor != null && d.Valor != "") {
                        var textoData = "";
                        var descCheck = d.Descripcion;
                        if (d.Valor2 == "-1") {
                            if ($.trim($("#txt" + d.Valor).val()) == "") {
                                textoData = "-1";
                            }
                        } else if (d.Valor2 == "-2") {
                            if ($("#cmb" + d.Valor + " option:selected").val() == "-1") {
                                textoData = "-1";
                            }
                        } else if (d.Valor2 == "-3") {
                            if ($.trim($("#txtTab" + d.Valor).val()) == "") {
                                textoData = "-1";
                            }
                        } else if (d.Valor2 == "-4") {
                            if ($("#cbx" + d.Valor2).is(':checked')) {
                                check++;
                                tipo = 4;
                            }
                        }
                        if (textoData == "-1") {
                            $("#divErrores").css("display", "");
                            $("#ulListaErrores").append('<li>' + d.Descripcion + '</li>');
                        }
                    }
                });
                if (check == 0 && tipo == 4) {
                    $("#divErrores").css("display", "");
                    $("#ulListaErrores").append('<li>' + descCheck + '</li>');
                }
            }
        } else if (item.IdEvento == funcValidaErroresNoCont3) {
            if (item.TipoPregunta == tipoPregTotal || item.TipoPregunta == tipoPregNumerico) {
                idObjeto = "txt" + item.IdPregunta;
            } else if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.Datos.length > 0) {
                var valorSelec = $("#" + idObjeto + " option:selected").val();
                $.each(item.Datos, function (i, d) {
                    if (d.Valor != null && d.Valor != "") {
                        if (valorSelec != "-1") {
                            for (var y = 1; y <= parseInt(valorSelec) ; y++) {
                                var textoData = "";
                                if (d.Valor2 == "-1") {
                                    if ($.trim($("#Tabtxt" + d.Valor + y).val()) == "") {
                                        textoData = "-1";
                                    }
                                } else if (d.Valor2 == "-2") {
                                    if ($("#Tabcmb" + d.Valor + y + " option:selected").val() == "-1") {
                                        textoData = "-1";
                                    }
                                } else if (d.Valor2 == "-3") {
                                    if ($.trim($("#txtTab" + d.Valor + y).val()) == "") {
                                        textoData = "-1";
                                    }
                                }
                            }
                            if (textoData == "-1") {
                                $("#divErrores").css("display", "");
                                $("#ulListaErrores").append('<li>' + d.Descripcion + '</li>');
                            }
                        }
                    }
                });
            }
        } else if (item.IdEvento == funcCalculoEvaluacion) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregTotal) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            $("#txt" + item.ValorPreguntaDependiente).prop('readonly', true);
            $("#" + idObjeto).change(function () {
                var valorSelec = 0;
                var valorSelec2 = 0;
                var ponderadores = [];
                var total = 0;
                var total2 = 0;
                if (item.Datos.length > 0) {
                    $.each(item.Datos, function (i, d) {
                        if (d.Valor2 != "-1" && d.Valor2 != "") {
                            var p = {};
                            p.id = d.Valor;
                            p.ponderador = d.Valor2;
                            ponderadores.push(p);
                        }
                    });
                    $.each(item.Datos, function (i, d) {
                        if (d.Valor2 == "-1") {
                            if (ponderadores.length > 0) {
                                $.each(ponderadores, function (x, y) {
                                    if ($("#cmb" + d.Valor + " option:selected").val() == y.id) {
                                        valorSelec += parseInt(y.ponderador);
                                    }
                                });
                            }
                            total++;
                        }
                    });
                    $.each(item.Datos, function (i, d) {
                        if (d.Valor2 == "-2") {
                            if (ponderadores.length > 0) {
                                $.each(ponderadores, function (x, y) {
                                    if ($("#cmb" + d.Valor + " option:selected").val() == y.id) {
                                        valorSelec2 += parseInt(y.ponderador);
                                    }
                                });
                            }
                            total2++;
                        }
                    });
                }
                var resultadoParcial = "";
                if (valorSelec == total) {
                    resultadoParcial = "Cumple";
                } else {
                    resultadoParcial = "No cumple";
                }
                var resultadoFinal = "";
                $("#" + idObjetoDep).val(resultadoParcial);
                $("#txt" + item.ValorPregunta).val(valorSelec);
                var objPertinencia = [2202, 2203, 2204];
                var objSeleccion = [2207, 2208];
                var fraseFoca1 = "El programa cuenta con criterios focalización pertinentes y un adecuado método de selección de sus beneficiarios";
                var fraseFoca2 = "El programa cuenta con criterios focalización pertinentes. Sin embargo, no cuenta con un método adecuado de selección de sus beneficiarios";
                var fraseFoca3 = "El programa no cuenta con criterios focalización pertinentes. Sin embargo, cuenta con un adecuado método de selección de sus beneficiarios.";
                var fraseFoca4 = "El programa no cuenta con criterios de focalización pertinentes ni un adecuado método de selección de sus beneficiarios.";
                if (objPertinencia.indexOf(parseInt(this.id.substring(3, this.id.length))) >= 0) {
                    if (valorSelec == 3) {
                        if (valorSelec2 == 2) {
                            resultadoFinal = fraseFoca1;
                        } else {
                            resultadoFinal = fraseFoca2;
                        }
                    } else {
                        if (valorSelec2 == 2) {
                            resultadoFinal = fraseFoca3;
                        } else {
                            resultadoFinal = fraseFoca4;
                        }
                    }
                } else if (objSeleccion.indexOf(parseInt(this.id.substring(3, this.id.length))) >= 0) {
                    if (valorSelec == 2) {
                        if (valorSelec2 == 3) {
                            resultadoFinal = fraseFoca1;
                        } else {
                            resultadoFinal = fraseFoca3;
                        }
                    } else {
                        if (valorSelec2 == 3) {
                            resultadoFinal = fraseFoca2;
                        } else {
                            resultadoFinal = fraseFoca4;
                        }
                    }
                }
                $("#txt" + item.ValorPreguntaDependiente).val(resultadoFinal);
            });
        } else if (item.IdEvento == funcCopiaTextoTabla) {
            if (item.TipoPregunta == tipoPregTexto) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregEtiquetaEspejo) {
                idObjetoDep = "lblEspejo" + item.IdPreguntaDependiente;
            }
            if ($.trim($("#Tab" + idObjeto + parseInt(item.ValorPreguntaDependiente)).val()) != "") {
                $("." + idObjetoDep).text($.trim($("#Tab" + idObjeto + parseInt(item.ValorPreguntaDependiente)).val()));
            }
            $("#Tab" + idObjeto + parseInt(item.ValorPreguntaDependiente)).keyup(function () {
                $("." + idObjetoDep).text($.trim($(this).val()));
            });
        } else if (item.IdEvento == funcCalculoEvalEfic) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregCombo) {
                idObjetoDep = "cmb" + item.IdPreguntaDependiente;
            }
            $("#txt" + item.ValorPregunta).prop('readonly', true);
            $("#txt2313").prop('readonly', true);
            $("#txt2314").prop('readonly', true);
            $("#txt2306").prop('readonly', true);
            $("#txt2307").prop('readonly', true);
            $("#txt2896").prop('readonly', true);
            $("#txt2897").prop('readonly', true);
            $("#txt2898").prop('readonly', true);
            $("#txt2899").prop('readonly', true);
            $("#txt2900").prop('readonly', true);
            $("#txt2901").prop('readonly', true);
            var ano = (new Date).getFullYear();
            $("#" + idObjeto + ", #" + idObjetoDep).change(function () {
                if (item.Datos.length > 0) {
                    var valorSelec = 0;
                    var valorSelec2 = 0;
                    var resultadoFinal = "";
                    $.each(item.Datos, function (i, d) {
                        if (d.Valor != "") {
                            if ($("#" + idObjeto + " option:selected").val() == d.Valor) {
                                valorSelec += parseInt(d.Valor2);
                            } else if ($("#" + idObjetoDep + " option:selected").val() == d.Valor) {
                                valorSelec += parseInt(d.Valor2);
                            }
                        }
                    });
                    var indicProp = [2222, 2223];
                    var resulFinal = [2286, 2287, 2288, 2289, 2290, 2291, 2292, 2293, 2294, 2295, 2296, 2297, 2298, 2299, 2300, 2301, 2302, 2303, 2304, 2305];
                    var meta1 = [2218, 2220];
                    var meta2 = [2219, 2221];
                    var meta3 = [2246, 2247, 2248, 2249, 2250, 2251, 2252, 2253, 2254, 2255, 2256, 2257, 2258, 2259, 2260, 2261, 2262, 2263, 2264, 2265];
                    var meta4 = [2266, 2267, 2268, 2269, 2270, 2271, 2272, 2273, 2274, 2275, 2276, 2277, 2278, 2279, 2280, 2281, 2282, 2283, 2284, 2285];
                    var totalIndicComp = 0;
                    if ($("#cmb2065").length) {
                        totalIndicComp = $("#cmb2065 option:selected").val();
                    } else if ($("#cmb1923").length) {
                        totalIndicComp = $("#cmb1923 option:selected").val();
                    } else if ($("#cmb2163").length) {
                        totalIndicComp = $("#cmb2163 option:selected").val();
                    }
                    var lectura = 0;
                    //Frases Meta
                    var fraseMeta1 = "El indicador cumple/sobrepasa la meta propuesta para el " + (ano - 1).toString();
                    var fraseMeta2 = "El indicador no cumple con la meta propuesta para el " + (ano - 1).toString();
                    var fraseMeta3 = "Debido a que el indicador no reporta valor efectivo y/o meta " + (ano - 1).toString() + ", no es posible evaluar el resultado del indicador";
                    var fraseMeta4 = "Debido a que el indicador no cumple en términos de calidad y/o pertinencia, no es posible evaluar el cumplimiento en relación a la meta " + (ano - 1).toString();
                    //Cumple calidad y pertinencia (incluyendo proxy)
                    if (valorSelec >= 2) {
                        var valorEfectivoComp = [2322, 2323, 2324, 2325, 2326, 2327, 2328, 2329, 2330, 2331, 2332, 2333, 2334, 2335, 2336, 2337, 2338, 2339, 2340, 2341];
                        var metaEfectivoComp = [2342, 2343, 2344, 2345, 2346, 2347, 2348, 2349, 2350, 2351, 2352, 2353, 2354, 2355, 2356, 2357, 2358, 2359, 2360, 2361];
                        var val1 = null;
                        var val2 = null;
                        //Se evalúan las metas
                        if (meta1.indexOf(parseInt(this.id.substring(3, this.id.length))) >= 0) {
                            if ($("#txt2316").val() != "" && $("#txt2318").val() != "") {
                                val1 = retornaValorFormateado($("#txt2316").val());
                                val2 = retornaValorFormateado($("#txt2318").val());
                            }
                            lectura = $("#cmb1211 option:selected").val();
                        } else if (meta2.indexOf(parseInt(this.id.substring(3, this.id.length))) >= 0) {
                            if ($("#txt2317").val() != "" && $("#txt2319").val() != "") {
                                val1 = retornaValorFormateado($("#txt2317").val());
                                val2 = retornaValorFormateado($("#txt2319").val());
                            }
                            lectura = $("#cmb1212 option:selected").val();
                        } else if (meta3.indexOf(parseInt(this.id.substring(3, this.id.length))) >= 0) {
                            if ($("#txt" + valorEfectivoComp[meta3.indexOf(parseInt(this.id.substring(3, this.id.length)))]).val() != "" && $("#txt" + metaEfectivoComp[meta3.indexOf(parseInt(this.id.substring(3, this.id.length)))]).val() != "") {
                                val1 = retornaValorFormateado($("#txt" + valorEfectivoComp[meta3.indexOf(parseInt(this.id.substring(3, this.id.length)))]).val());
                                val2 = retornaValorFormateado($("#txt" + metaEfectivoComp[meta3.indexOf(parseInt(this.id.substring(3, this.id.length)))]).val());
                            }
                            lectura = $("#cmb791" + (meta3.indexOf(parseInt(this.id.substring(3, this.id.length))) + 1) + " option:selected").val();
                        } else if (meta4.indexOf(parseInt(this.id.substring(3, this.id.length))) >= 0) {
                            if ($("#txt" + valorEfectivoComp[meta4.indexOf(parseInt(this.id.substring(3, this.id.length)))]).val() != "" && $("#txt" + metaEfectivoComp[meta4.indexOf(parseInt(this.id.substring(3, this.id.length)))]).val() != "") {
                                val1 = retornaValorFormateado($("#txt" + valorEfectivoComp[meta4.indexOf(parseInt(this.id.substring(3, this.id.length)))]).val());
                                val2 = retornaValorFormateado($("#txt" + metaEfectivoComp[meta4.indexOf(parseInt(this.id.substring(3, this.id.length)))]).val());
                            }
                            lectura = $("#cmb791" + (meta4.indexOf(parseInt(this.id.substring(3, this.id.length))) + 1) + " option:selected").val();
                        }
                        //Se verifica lectura del indicador (ascendente o descendente)
                        if (val1 != null && val2 != null) {
                            valorSelec2 = val2 - val1;
                            if (lectura == "1037") {
                                if (valorSelec2 > 0) {
                                    valorSelec2 = valorSelec2 * (-1);
                                } else {
                                    valorSelec2 = Math.abs(valorSelec2);
                                }
                            }
                        } else {
                            valorSelec2 = null;
                        }
                        //Meta cumple o sobrepasa
                        if (valorSelec2 <= 0 && valorSelec2 != null) {
                            resultadoFinal = fraseMeta1;
                            //Meta no cumple
                        } else if (valorSelec2 != null) {
                            resultadoFinal = fraseMeta2;
                            //Metas nulas
                        } else if (valorSelec2 == null) {
                            resultadoFinal = fraseMeta3;
                        }
                        //No cumple calidad o pertinencia
                    } else if (valorSelec < 2) {
                        resultadoFinal = fraseMeta4;
                    }
                    //Se asigna resultado
                    $("#txt" + item.ValorPregunta).val(resultadoFinal);
                    //Resultados finales
                    //Calidad
                    var calidadProposito = [2218, 2219];
                    var calidadComponentes = [2246, 2247, 2248, 2249, 2250, 2251, 2252, 2253, 2254, 2255, 2256, 2257, 2258, 2259, 2260, 2261, 2262, 2263, 2264, 2265];
                    var cumpleCalidad = "3701";
                    var noCumpleCalidad = "3702";
                    var countVaciosCalidad = 0;
                    if (calidadProposito.indexOf(parseInt(this.id.substring(3, this.id.length))) >= 0) {
                        var fraseCalidad1 = "El programa/iniciativa cuenta con indicadores de propósito de calidad";
                        var fraseCalidad2 = "El programa/iniciativa no cuenta con indicadores de propósito de calidad";
                        $("#txt2896").val(fraseCalidad2);
                        $.each(calidadProposito, function (i, d) {
                            if ($("#cmb" + d + " option:selected").val() != "-1") {
                                if ($("#cmb" + d + " option:selected").val() == cumpleCalidad) {
                                    $("#txt2896").val(fraseCalidad1);
                                    return false;
                                }
                            } else {
                                countVaciosCalidad++;
                            }
                        });
                    }
                    if (countVaciosCalidad == calidadProposito.length) {
                        $("#txt2896").val("");
                    }
                    countVaciosCalidad = 0;
                    if (calidadComponentes.indexOf(parseInt(this.id.substring(3, this.id.length))) >= 0) {
                        var fraseCalidad3 = "El programa/iniciativa cuenta con indicadores de componentes de calidad";
                        var fraseCalidad4 = "El programa/iniciativa no cuenta con indicadores de componentes de calidad";
                        $("#txt2899").val(fraseCalidad3);
                        for (var z = 0; z < totalIndicComp; z++) {
                            if ($("#cmb" + calidadComponentes[z] + " option:selected").val() != "-1") {
                                if ($("#cmb" + calidadComponentes[z] + " option:selected").val() == noCumpleCalidad) {
                                    $("#txt2899").val(fraseCalidad4);
                                    break;
                                }
                            } else {
                                countVaciosCalidad++;
                            }
                        }
                    }
                    if (countVaciosCalidad == totalIndicComp) {
                        $("#txt2899").val("");
                    }
                    //Pertinencia
                    var pertinenciaProposito = [2220, 2221];
                    var pertinenciaComponentes = [2266, 2267, 2268, 2269, 2270, 2271, 2272, 2273, 2274, 2275, 2276, 2277, 2278, 2279, 2280, 2281, 2282, 2283, 2284, 2285];
                    var cumplePertinencia = "3751";
                    var proxyPertinencia = "3752";
                    var noCumplePertinencia = "3753";
                    var countVaciosPertinencia = 0;
                    if (pertinenciaProposito.indexOf(parseInt(this.id.substring(3, this.id.length))) >= 0) {
                        var frasePertinencia1 = "El programa/iniciativa cuenta con indicadores pertinentes para medir su propósito.";
                        var frasePertinencia2 = "El programa/iniciativa no cuenta con indicadores pertinentes para medir su propósito.";
                        $("#txt2897").val(frasePertinencia2);
                        $.each(pertinenciaProposito, function (i, d) {
                            if ($("#cmb" + d + " option:selected").val() != "-1") {
                                if ($("#cmb" + d + " option:selected").val() != noCumplePertinencia) {
                                    $("#txt2897").val(frasePertinencia1);
                                    return false;
                                }
                            } else {
                                countVaciosPertinencia++;
                            }
                        });
                    }
                    if (countVaciosPertinencia == pertinenciaProposito.length) {
                        $("#txt2897").val("");
                    }
                    countVaciosPertinencia = 0;
                    if (pertinenciaComponentes.indexOf(parseInt(this.id.substring(3, this.id.length))) >= 0) {
                        var frasePertinencia3 = "El programa/iniciativa cuenta con indicadores pertinentes para medir sus componentes.";
                        var frasePertinencia4 = "El programa/iniciativa no cuenta con indicadores pertinentes para medir sus componentes.";
                        $("#txt2900").val(frasePertinencia3);
                        for (var z = 0; z < totalIndicComp; z++) {
                            if ($("#cmb" + pertinenciaComponentes[z] + " option:selected").val() != "-1") {
                                if ($("#cmb" + pertinenciaComponentes[z] + " option:selected").val() == noCumplePertinencia) {
                                    $("#txt2900").val(frasePertinencia4);
                                    break;
                                }
                            } else {
                                countVaciosPertinencia++;
                            }
                        }
                    }
                    if (countVaciosPertinencia == totalIndicComp) {
                        $("#txt2900").val("");
                    }
                    //Meta
                    var fraseFinalMetaCumple = "Los indicadores de propósito cumplen o sobrepasan las metas propuestas para el " + (ano - 1).toString();
                    var fraseFinalMetaNoCumple = "Los indicadores de propósito no cumplen con las metas propuestas para el " + (ano - 1).toString();
                    var fraseMetaFinalNula = "Debido a que los indicadores no reportan valor efectivo y/o meta " + (ano - 1).toString() + ", no es posible evaluar el resultado";
                    var fraseFinalNoCumpleCP = "Debido a que el/los indicador/es no cumple en términos de calidad y/o pertinencia, no es posible evaluar el cumplimiento en relación a la meta " + (ano - 1).toString();
                    var countMeta2 = 0;
                    var countMeta3 = 0;
                    var countMeta4 = 0;
                    //Proposito
                    if (indicProp.indexOf(parseInt(item.ValorPregunta)) >= 0) {
                        $.each(indicProp, function (i, d) {
                            if ($("#txt" + d).val() != "") {
                                if ($("#txt" + d).val() == fraseMeta1) {
                                    $("#txt2898").val(fraseFinalMetaCumple);
                                    countMeta2 = 0;
                                    countMeta3 = 0;
                                    countMeta4 = 0;
                                    return false;
                                } else if ($("#txt" + d).val() == fraseMeta4) {
                                    countMeta4++;
                                } else if ($("#txt" + d).val() == fraseMeta2) {
                                    countMeta2++;
                                } else if ($("#txt" + d).val() == fraseMeta3) {
                                    countMeta3++;
                                }
                            }
                        });
                        if (countMeta4 > 0) {
                            $("#txt2898").val(fraseFinalNoCumpleCP);
                        } else if (countMeta2 > 0) {
                            $("#txt2898").val(fraseFinalMetaNoCumple);
                        } else if (countMeta3 > 0) {
                            $("#txt2898").val(fraseMetaFinalNula);
                        }
                    }
                    //Componentes
                    if (resulFinal.indexOf(parseInt(item.ValorPregunta)) >= 0) {
                        var totalFinal1 = 1;
                        var validaMayoria = 1;
                        var fraseFinalMetaCumple2 = "Los indicadores de componentes cumplen o sobrepasan las metas propuestas para el " + (ano - 1).toString();
                        var fraseFinalMetaNoCumple2 = "Los indicadores de componentes no cumplen con las metas propuestas para el " + (ano - 1).toString();
                        for (var z = 0; z < totalIndicComp; z++) {
                            if ($("#txt" + resulFinal[z]).val() == fraseMeta4) {
                                $("#txt2901").val(fraseFinalNoCumpleCP);
                                validaMayoria = 0;
                                break;
                            } else if ($("#txt" + resulFinal[z]).val() == fraseMeta2) {
                                $("#txt2901").val(fraseFinalMetaNoCumple2);
                                validaMayoria = 0;
                                break;
                            } else if ($("#txt" + resulFinal[z]).val() == fraseMeta1) {
                                totalFinal1++;
                            }
                        }
                        //Se calcula mayoria (50 + 1)
                        if (validaMayoria == 1) {
                            var mayoriaIndicCompTemp = (totalFinal1 / totalIndicComp);
                            var mayoriaFinal = mayoriaIndicCompTemp * 100;
                            if (mayoriaFinal >= 50) {
                                $("#txt2901").val(fraseFinalMetaCumple2);
                            } else {
                                $("#txt2901").val(fraseMetaFinalNula);
                            }
                        }
                    }

                    if ($("#" + idObjeto + " option:selected").val() == "-1" || $("#" + idObjetoDep + " option:selected").val() == "-1") {
                        if (meta1.indexOf(parseInt(this.id.substring(3, this.id.length))) >= 0) {
                            $("#txt2222").val("");
                            $("#txt2898").val("");
                        } else if (meta2.indexOf(parseInt(this.id.substring(3, this.id.length))) >= 0) {
                            $("#txt2223").val("");
                            $("#txt2898").val("");
                        } else if (meta3.indexOf(parseInt(this.id.substring(3, this.id.length))) >= 0) {
                            $("#txt" + resulFinal[meta3.indexOf(parseInt(this.id.substring(3, this.id.length)))]).val("");
                            $("#txt2901").val("");
                        } else if (meta4.indexOf(parseInt(this.id.substring(3, this.id.length))) >= 0) {
                            $("#txt" + resulFinal[meta4.indexOf(parseInt(this.id.substring(3, this.id.length)))]).val("");
                            $("#txt2901").val("");
                        }
                    }
                }
            });
        } else if (item.IdEvento == funcSumaCamposVarios) {
            if (item.TipoPregunta == tipoPregTexto || item.TipoPregunta == tipoPregTotal || item.CategoriaPregunta == tipoPregNumerico || item.TipoPregunta == tipoPregTotalValidacion) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.Datos.length > 0) {
                var total = 0;
                var blancos = false;
                $.each(item.Datos, function (i, d) {
                    if (d.Valor != null && d.Valor != "") {
                        if ($.trim($("#txt" + d.Valor).val()) != "") {
                            blancos = true;
                            total += retornaValorFormateado($("#txt" + d.Valor).val());
                        }
                    }
                });
                if (blancos) {
                    $("#" + idObjeto).val(total.toString().replace('.', ','));
                } else {
                    $("#" + idObjeto).val('');
                }
                $.each(item.Datos, function (i, d) {
                    if (d.Valor != null && d.Valor != "") {
                        $("#txt" + d.Valor).bind("keyup change", function () {
                            var total = ($.trim($(this).val()) != "" ? retornaValorFormateado($(this).val()) : 0);
                            var blancos = (total != 0 && total != "" ? true : false);
                            $.each(item.Datos, function (y, dat) {
                                if (dat.Valor != null && dat.Valor != "") {
                                    if (dat.Valor != d.Valor) {
                                        if ($.trim($("#txt" + dat.Valor).val()) != "") {
                                            blancos = true;
                                            total += retornaValorFormateado($("#txt" + dat.Valor).val());
                                        }
                                    }
                                }
                            });
                            if (blancos) {
                                $("#" + idObjeto).val(total.toString().replace('.', ',')).trigger('change');;
                            } else {
                                $("#" + idObjeto).val('').trigger('change');;
                            }
                        });
                    }
                });
            }
        } else if (item.IdEvento == funcOcultaTabAsig) {            
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregTab) {
                idObjetoDep = item.IdPreguntaDependiente;
            }
            for (var i = 1; i <= 20; i++) {
                for (var x = 21; x <= 33; x++) {
                    var ga = (idObjeto == "cmb6861" ? ((x == 21 || x == 22 || x == 24 || x == 29) ? true : false) : false);
                    if ((x == 24 || x == 31 || x == 32 || x == 33) || ga) {
                        var idLlave = "idLlave" + item.IdPregunta + i.toString() + x.toString();
                        var nuevoidObjetoDep = "tabNieto" + i.toString() + x.toString() + idObjetoDep + $("#" + idLlave).val();
                        var nuevoidObjeto = idObjeto + i.toString() + x.toString();
                        ocultaTabs(nuevoidObjetoDep, $("#" + nuevoidObjeto).val(), true);
                        $("#" + nuevoidObjeto).change(function () {                            
                            var tab = this.id.substring(3, this.id.length);
                            var numTab = $("#numTab" + tab).val();
                            var idLlave = "idLlave" + item.IdPregunta + numTab;
                            var nuevoidObjetoDep = "tabNieto" + numTab + idObjetoDep + $("#" + idLlave).val();
                            ocultaTabs(nuevoidObjetoDep, $(this).val(), true);
                        });
                    }
                }
            }
        } else if (item.IdEvento == funcDivisiones) {
            if (item.CategoriaPregunta == tipoPregNumerico || item.TipoPregunta == tipoPregTotal) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregNumerico || item.TipoPregunta == tipoPregTotal) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            if (item.Datos.length > 0) {
                $.each(item.Datos, function (i, d) {
                    if (d.Valor != null && d.Valor != "") {
                        var blancos = false;
                        var total = 0;
                        var valor1 = 0;
                        var valor2 = 0;
                        if ($.trim($("#" + idObjeto).val()) != "") {
                            blancos = true;
                            valor1 = retornaValorFormateado($("#" + idObjeto).val());
                        }
                        if ($.trim($("#" + idObjetoDep).val()) != "") {
                            blancos = true;
                            valor2 = retornaValorFormateado($("#" + idObjetoDep).val());
                        }
                        if (blancos) {
                            if (valor2 > 0) {
                                total = Math.round((valor1 / valor2) * 100) / 100;
                                $("#txt" + d.Valor).val(total.toString().replace('.', ','));
                            }
                        } else {
                            $("#txt" + d.Valor).val('');
                        }
                    }
                });
            }
            $("#" + idObjeto).bind("keyup change", function () {
                if (item.Datos.length > 0) {
                    $.each(item.Datos, function (i, d) {
                        if (d.Valor != null && d.Valor != "") {
                            var blancos = false;
                            var total = 0;
                            var valor1 = 0;
                            var valor2 = 0;
                            if ($.trim($("#" + idObjeto).val()) != "") {
                                blancos = true;
                                valor1 = retornaValorFormateado($("#" + idObjeto).val());
                            }
                            if ($.trim($("#" + idObjetoDep).val()) != "") {
                                blancos = true;
                                valor2 = retornaValorFormateado($("#" + idObjetoDep).val());
                            }
                            if (blancos) {
                                if (valor2 > 0) {
                                    total = Math.round((valor1 / valor2) * 100) / 100;
                                    $("#txt" + d.Valor).val(total.toString().replace('.', ','));
                                }
                            } else {
                                $("#txt" + d.Valor).val('');
                            }
                        }
                    });
                }
            });
            $("#" + idObjetoDep).bind("keyup change", function () {
                if (item.Datos.length > 0) {
                    $.each(item.Datos, function (i, d) {
                        if (d.Valor != null && d.Valor != "") {
                            var blancos = false;
                            var total = 0;
                            var valor1 = 0;
                            var valor2 = 0;
                            if ($.trim($("#" + idObjeto).val()) != "") {
                                blancos = true;
                                valor1 = retornaValorFormateado($("#" + idObjeto).val());
                            }
                            if ($.trim($("#" + idObjetoDep).val()) != "") {
                                blancos = true;
                                valor2 = retornaValorFormateado($("#" + idObjetoDep).val());
                            }
                            if (blancos) {
                                if (valor2 > 0) {
                                    total = Math.round((valor1 / valor2) * 100) / 100;
                                    $("#txt" + d.Valor).val(total.toString().replace('.', ','));
                                }
                            } else {
                                $("#txt" + d.Valor).val('');
                            }
                        }
                    });
                }
            });
        } else if (item.IdEvento == funcOtrasOpciones) {
            if (item.CategoriaPregunta == tipoPregNumerico || item.TipoPregunta == tipoPregCheckInline) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregNumerico || item.TipoPregunta == tipoPregCheckInline) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            if (item.ValorPregunta != "") {                
                for (var y = 1; y <= parseInt(item.ValorPregunta) ; y++) {
                    if ($.trim($("#Tab" + idObjeto + y).val()) != "") {
                        if ($.trim($("#Tab" + idObjeto + y).val()) == "S/I" || $.trim($("#Tab" + idObjeto + y).val()) == "N/A") {
                            $("#Tab" + idObjeto + y.toString()).prop('readonly', true);
                            $("#divPregTab" + item.IdPreguntaDependiente + y.toString()).show();
                        } else if ($("#Tab" + idObjeto + y.toString()).val() == "0"){
                            $("#divPregTab" + item.IdPreguntaDependiente + y.toString()).show();
                        }else{
                            $("#divPregTab" + item.IdPreguntaDependiente + y.toString()).hide();
                        }
                    }else{
                        $("#divPregTab" + item.IdPreguntaDependiente + y.toString()).hide();
                    }
                    $("#Tab" + idObjeto + y.toString()).bind("keyup change", function () {                        
                        var nTab = $(this).attr('class').split(/\s+/)[2].split('-')[1];
                        if ($(this).val() == "0") {
                            $("#divPregTab" + item.IdPreguntaDependiente + nTab).show();
                        }else{
                            $("#divPregTab" + item.IdPreguntaDependiente + nTab).hide();
                        }
                    });
                }
            }
            $(".otrasOpciones" + item.IdPregunta).click(function () {
                nTab = this.id.split('-')[2];                
                if ($(this).val() == "-1") {
                    if ($("#Tab" + idObjeto + nTab).val() == "S/I"){
                        $("#Tab" + idObjeto + nTab).val('');
                        $("#Tab" + idObjeto + nTab).prop('readonly', false);
                        $("#divPregTab" + item.IdPreguntaDependiente + nTab).hide();
                        $("#txtTab" + item.IdPreguntaDependiente + nTab).val("");
                    }else{
                        if (item.Datos.length > 0) {
                            $("#txt" + item.Datos[nTab].Valor).val('S/I');
                        }
                        $("#Tab" + idObjeto + nTab).val('S/I');
                        $("#Tab" + idObjeto + nTab).prop('readonly', true);
                        $("#divPregTab" + item.IdPreguntaDependiente + nTab).show();
                    }                    
                } else if ($(this).val() == "-2") {
                    if ($("#Tab" + idObjeto + nTab).val() == "N/A"){
                        $("#Tab" + idObjeto + nTab).val('');
                        $("#Tab" + idObjeto + nTab).prop('readonly', false);
                        $("#divPregTab" + item.IdPreguntaDependiente + nTab).hide();
                        $("#txtTab" + item.IdPreguntaDependiente + nTab).val("");
                    }else{
                        if (item.Datos.length > 0) {
                            $("#txt" + item.Datos[nTab].Valor).val('N/A');
                        }
                        $("#Tab" + idObjeto + nTab).val('N/A');
                        $("#Tab" + idObjeto + nTab).prop('readonly', true);
                        $("#divPregTab" + item.IdPreguntaDependiente + nTab).show();
                    }                    
                }
            });
        } else if (item.IdEvento == funcOculPregTabNormal) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregTexto || item.TipoPreguntaDependiente == tipoPregTxtGrande) {
                idObjetoDep = "txtTab" + item.IdPreguntaDependiente;
            }
            if (item.ValorPregunta != "" && item.ValorPregunta != null) {
                for (var i = 1; i <= parseInt(item.ValorPregunta) ; i++) {                    
                    if ($("#Tab" + idObjeto + i.toString() + " option:selected").text().toUpperCase() == "NO" || $("#Tab" + idObjeto + i.toString() + " option:selected").val() == "-1") {
                        $("#divPregTab" + item.IdPreguntaDependiente + i.toString()).hide();
                    } else {
                        $("#divPregTab" + item.IdPreguntaDependiente + i.toString()).show();
                    }
                    $("#Tab" + idObjeto + i.toString()).change(function () {
                        var nTab = $(this).attr('class').split(/\s+/)[2].split('-')[1];
                        if (nTab != null || nTab != 0) {
                            idObjeto = ("Tabcmb" + item.IdPregunta + nTab);
                        }
                        if ($("#" + idObjeto + " option:selected").text().toUpperCase() == "NO" || $("#" + idObjeto + " option:selected").val() == "-1") {
                            $("#divPregTab" + item.IdPreguntaDependiente + nTab).hide();
                        } else {
                            $("#divPregTab" + item.IdPreguntaDependiente + nTab).show();
                        }
                    });
                }
            }
        } else if (item.IdEvento == funcSelectOtroSelect) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjetoDep = "cmb" + item.IdPreguntaDependiente;
            }
            if (item.ValorPregunta != "" && item.ValorPregunta != null) {
                for (var i = 1; i <= parseInt(item.ValorPregunta) ; i++) {
                    if ($("#Tab" + idObjeto + i.toString() + " option:selected").val() != "-1") {
                        if (item.Datos.length > 0) {
                            $.each(item.Datos, function (z, d) {
                                if (d.Valor != null && d.Valor != "") {
                                    if ($("#Tab" + idObjeto + i.toString() + " option:selected").val() == d.Valor) {
                                        $("#Tab" + idObjetoDep + i.toString() + " option[value='" + d.Valor2 + "']").prop('selected', true).trigger('change');;
                                        return false;
                                    }
                                }
                            });
                        }
                    }
                    $("#Tab" + idObjeto + i.toString()).change(function () {
                        var nTab = $(this).attr('class').split(/\s+/)[2].split('-')[1];
                        if (nTab != null || nTab != 0) {
                            if ($(this).val() != "-1") {
                                if (item.Datos.length > 0) {
                                    $.each(item.Datos, function (z, d) {
                                        if (d.Valor != null && d.Valor != "") {
                                            if ($("#Tab" + idObjeto + nTab + " option:selected").val() == d.Valor) {
                                                $("#Tab" + idObjetoDep + nTab + " option[value='" + d.Valor2 + "']").prop('selected', true).trigger('change');;
                                                return false;
                                            } else {
                                                $("#Tab" + idObjetoDep + nTab + " option[value='-1']").prop('selected', true).trigger('change');;
                                            }
                                        }
                                    });
                                }
                            } else {
                                $("#Tab" + idObjetoDep + nTab + " option[value='-1']").prop('selected', true).trigger('change');;
                            }
                        }
                    });
                }
            } else {
                if ($("#" + idObjeto + " option:selected").val() != "-1") {
                    if (item.Datos.length > 0) {
                        $.each(item.Datos, function (z, d) {
                            if (d.Valor != null && d.Valor != "") {
                                if ($("#" + idObjeto + " option:selected").val() == d.Valor) {
                                    $("#" + idObjetoDep + " option[value='" + d.Valor2 + "']").prop('selected', true).trigger('change');;
                                    return false;
                                }
                            }
                        });
                    }
                }
                $("#" + idObjeto).change(function () {
                    if ($(this).val() != "-1") {
                        if (item.Datos.length > 0) {
                            $.each(item.Datos, function (z, d) {
                                if (d.Valor != null && d.Valor != "") {
                                    if ($("#" + idObjeto + " option:selected").val() == d.Valor) {
                                        $("#" + idObjetoDep + " option[value='" + d.Valor2 + "']").prop('selected', true).trigger('change');;
                                        return false;
                                    } else {
                                        $("#" + idObjetoDep + " option[value='-1']").prop('selected', true).trigger('change');;
                                    }
                                }
                            });
                        }
                    } else {
                        $("#" + idObjetoDep + " option[value='-1']").prop('selected', true).trigger('change');;
                    }
                });
            }
        } else if (item.IdEvento == funcRegistroExcepcionesEval) {
            if ($("#Tabcmb71182 option:selected").val() == "902") {
                $("#Tabcmb70082 option[value='10135']").prop('selected', true);
                $("#Tabcmb70092 option[value='10142']").prop('selected', true);
                $("#Tabcmb70152 option[value='10164']").prop('selected', true);
                $("#Tabcmb70162 option[value='10172']").prop('selected', true);
                $("#Tabcmb70381 option[value='10943']").prop('selected', true);
                $("#Tabcmb70451 option[value='10262']").prop('selected', true);
                $("#Tabcmb70411 option[value='10226']").prop('selected', true);
                $("#Tabcmb70441 option[value='10254']").prop('selected', true);
                $("#Tabcmb70411").prop('disabled', true);
            }
            $("#Tabcmb71182").change(function () {
                if ($(this).val() == "902") {
                    $("#Tabcmb70082 option[value='10135']").prop('selected', true);
                    $("#Tabcmb70092 option[value='10142']").prop('selected', true);
                    $("#Tabcmb70152 option[value='10164']").prop('selected', true);
                    $("#Tabcmb70162 option[value='10172']").prop('selected', true);
                    $("#Tabcmb70381 option[value='10943']").prop('selected', true);
                    $("#Tabcmb70451 option[value='10262']").prop('selected', true);
                    $("#Tabcmb70411 option[value='10226']").prop('selected', true);
                    $("#Tabcmb70441 option[value='10254']").prop('selected', true);
                    $("#Tabcmb70411").prop('disabled', true);
                } else {
                    $("#Tabcmb70082 option[value='-1']").prop('selected', true);
                    $("#Tabcmb70092 option[value='-1']").prop('selected', true);
                    $("#Tabcmb70152 option[value='-1']").prop('selected', true);
                    $("#Tabcmb70162 option[value='-1']").prop('selected', true);
                    $("#Tabcmb70381 option[value='-1']").prop('selected', true);
                    $("#Tabcmb70451 option[value='-1']").prop('selected', true);
                    $("#Tabcmb70411 option[value='-1']").prop('selected', true);
                    $("#Tabcmb70441 option[value='-1']").prop('selected', true);
                    $("#Tabcmb70411").prop('disabled', false);
                }
            });
        } else if (item.IdEvento == funcBloqueaCopyPaste) {
            if (item.CategoriaPregunta == tipoPregNumerico) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.ValorPregunta != "" && item.ValorPregunta != null) {
                for (var i = 1; i <= parseInt(item.ValorPregunta) ; i++) {
                    $("#Tab" + idObjeto + i.toString()).on('paste', function (e) { e.preventDefault(); });
                    $("#Tab" + idObjeto + i.toString()).on('copy', function (e) { e.preventDefault(); });
                    $("#Tab" + idObjeto + i.toString()).on('drop', function (e) { e.preventDefault(); });
                }
            } else {
                $("#" + idObjeto).on('paste', function (e) { e.preventDefault(); });
                $("#" + idObjeto).on('copy', function (e) { e.preventDefault(); });
                $("#" + idObjeto).on('drop', function (e) { e.preventDefault(); });
            }
        } else if (item.IdEvento == funcCopiaTextoNormal) {
            if (item.TipoPregunta == tipoPregTexto || item.CategoriaPregunta == tipoPregNumerico || item.TipoPregunta == tipoPregTxtGrande) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregTextoReadonly || item.CategoriaPregunta == tipoPregNumerico || item.TipoPreguntaDependiente == tipoPregTexto || item.TipoPregunta == tipoPregTxtGrande) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            if ($("#" + idObjeto).val() != "") {
                $("#" + idObjetoDep).val($("#" + idObjeto).val());
            }
            $("#" + idObjeto).keyup(function () { $("#" + idObjetoDep).val($(this).val()); });
        } else if (item.IdEvento == funcColorFondo) {
            if (item.Datos.length > 0) {
                $.each(item.Datos, function (i, d) {
                    if (d.IdParametro != d.IdCategoria) {
                        $("#divPreg" + item.IdPregunta).css("background-color", d.Descripcion);
                    }
                });
            }
        } else if (item.IdEvento == funcCambiaTablaMultiple) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregTabla) {
                idObjetoDep = "tabla" + item.IdPreguntaDependiente;
            }
            var totalFilas = 0;
            var idPadre = 0;
            var idHijo = 0;
            if (item.Datos.length > 0) {
                $.each(item.Datos, function (i, d) {
                    if (d.IdParametro != d.IdCategoria) {
                        totalFilas += parseInt($("#cmb" + d.Valor).val());
                    }
                    if (d.Valor2 == "1") {
                        idPadre = d.Valor;
                    } else {
                        idHijo = d.Valor;
                    }
                });
            }
            if ($("#" + idObjeto).val() != "902") {
                totalFilas = parseInt($("#cmb" + idPadre).val());
            }
            $("#" + idObjetoDep + " tbody tr").each(function (i) {
                if ((i + 1) > totalFilas) {
                    $(this).css("display", "none");
                } else {
                    $(this).css("display", "");
                }
            });
            $("#" + idObjeto + ", #cmb" + idPadre + ", #cmb" + idHijo).bind("change", function () {
                var totalFilas = 0;
                var idPadre = 0;
                if (item.Datos.length > 0) {
                    $.each(item.Datos, function (i, d) {
                        if (d.IdParametro != d.IdCategoria) {
                            totalFilas += parseInt($("#cmb" + d.Valor).val());
                        }
                        if (d.Valor2 == "1") {
                            idPadre = d.Valor;
                        }
                    });
                }
                if ($("#" + idObjeto).val() != "902") {
                    totalFilas = parseInt($("#cmb" + idPadre).val());
                }
                $("#" + idObjetoDep + " tbody tr").each(function (i) {
                    if ((i + 1) > totalFilas) {
                        $(this).css("display", "none");
                    } else {
                        $(this).css("display", "");
                    }
                });
            });
        } else if (item.IdEvento == funcCopiaTextoTabla2) {
            var idTextoComp = [0, 6843, 6844, 6845, 6846, 6847, 6848, 6849, 6850, 6851, 6852, 6853, 6854, 6855, 6856];
            var totalComp = parseInt($("#cmb6831").val()) + parseInt($("#cmb6953").val());
            for (var i = 1; i <= parseInt($("#cmb6953").val()) ; i++) {
                if ($.trim($("#Tabtxt6955" + i).val()) != "") {
                    $(".lblEspejo" + idTextoComp[totalComp - 1]).text($.trim($("#Tabtxt6955" + i).val()));
                }
            }
            for (var i = 1; i <= 5 ; i++) {
                $("#Tabtxt6955" + i).keyup(function () {
                    var tab = this.id.substring(6, this.id.length);
                    var numTab = $("#numTab" + tab).val();
                    var totalComp = parseInt($("#cmb6831").val()) + parseInt(numTab);
                    $(".lblEspejo" + idTextoComp[totalComp - 1]).text($.trim($(this).val()));
                });
            }
        } else if (item.IdEvento == funcCheckSelecUnica) {
            $(".cbxunico").click(function () {
                $('input.cbxunico:checkbox').not(this).prop('checked', false);
            });
        } else if (item.IdEvento == funcDesbloqueaPregEval) {
            if (item.Datos.length > 0) {
                $.each(item.Datos, function (i, d) {
                    if (d.IdParametro != d.IdCategoria) {
                        $("#" + d.Descripcion).prop('disabled', true);
                    }
                });
            }
        } else if (item.IdEvento == funcModuloEvaluacion){   
            //Criterios de focalizacion
            $("#cmb7431").prop('disabled', true);
            $("#cmb7432").prop('disabled', true);
            if ($("#cmb7433 option:selected").val() != "21936") {
                $("#divPregTab70891").hide();
                $("#divPregTab70901").hide();
            }else{
                $("#divPregTab70891").show();
                $("#divPregTab70901").show();
            }
            $("#cmb7433").change(function () {
                if ($(this).val() == "21936"){
                    $("#divPregTab70891").show();
                    $("#divPregTab70901").show();            
                }else{
                    $("#divPregTab70891").hide();
                    $("#divPregTab70901").hide();
                }
            });
            //Criterios de priorizacion
            $("#cmb7435").prop('disabled', true);
            $("#cmb7436").prop('disabled', true);
            if ($("#cmb7437 option:selected").val() != "21936") {
                $("#divPregTab70962").hide();
                $("#divPregTab70972").hide();
            }else{
                $("#divPregTab70962").show();
                $("#divPregTab70972").show();
            }
            $("#cmb7437").change(function () {
                if ($(this).val() == "21936"){
                    $("#divPregTab70962").show();
                    $("#divPregTab70972").show();            
                }else{
                    $("#divPregTab70962").hide();
                    $("#divPregTab70972").hide();
                }
            });
            //Ejecución presupuestaria inicial
            if ($("#cmb7442 option:selected").val() != "21936") {
                $("#divPregTab74431").hide();
                $("#divPregTab69841").hide();
            }else{
                $("#divPregTab74431").show();
                $("#divPregTab69841").show();
            }
            $("#cmb7442").change(function () {
                if ($(this).val() == "21936"){
                    $("#divPregTab74431").show();
                    $("#divPregTab69841").show();            
                }else{
                    $("#divPregTab74431").hide();
                    $("#divPregTab69841").hide();
                }
            });
            //Ejecución presupuestaria final
            if ($("#cmb7462 option:selected").val() != "21936") {
                $("#divPregTab74471").hide();
                $("#divPregTab69931").hide();
            }else{
                $("#divPregTab74471").show();
                $("#divPregTab69931").show();
            }
            $("#cmb7462").change(function () {
                if ($(this).val() == "21936"){
                    $("#divPregTab74471").show();
                    $("#divPregTab69931").show();            
                }else{
                    $("#divPregTab74471").hide();
                    $("#divPregTab69931").hide();
                }
            });
            //Persistencia subejecucion presupuestaria inicial
            if ($("#cmb7463 option:selected").val() != "21936") {
                $("#divPregTab74511").hide();
                $("#divPregTab70031").hide();
            }else{
                $("#divPregTab74511").show();
                $("#divPregTab70031").show();
            }
            $("#cmb7463").change(function () {
                if ($(this).val() == "21936"){
                    $("#divPregTab74511").show();
                    $("#divPregTab70031").show();            
                }else{
                    $("#divPregTab74511").hide();
                    $("#divPregTab70031").hide();
                }
            });
            //Gasto por beneficiarios
            if ($("#cmb7464 option:selected").val() != "21936") {
                $("#divPregTab74552").hide();
                $("#divPregTab70122").hide();
            }else{
                $("#divPregTab74552").show();
                $("#divPregTab70122").show();
            }
            $("#cmb7464").change(function () {
                if ($(this).val() == "21936"){
                    $("#divPregTab74552").show();
                    $("#divPregTab70122").show();            
                }else{
                    $("#divPregTab74552").hide();
                    $("#divPregTab70122").hide();
                }
            });
            //Gasto administrativo
            $("#cmb7457").prop('disabled', true);
            $("#cmb7458").prop('disabled', true);
            if ($("#cmb7465 option:selected").val() != "21936") {
                $("#divPregTab74593").hide();
                $("#divPregTab70263").hide();
            }else{
                $("#divPregTab74593").show();
                $("#divPregTab70263").show();
            }
            $("#cmb7465").change(function () {
                if ($(this).val() == "21936"){
                    $("#divPregTab74593").show();
                    $("#divPregTab70263").show();            
                }else{
                    $("#divPregTab74593").hide();
                    $("#divPregTab70263").hide();
                }
            });
            //Persistencia al no reporte del gasto administrativo
            $("#cmb7803").prop('disabled', true);
            $("#cmb7804").prop('disabled', true);
            if ($("#cmb7805 option:selected").val() != "21936") {
                $("#divPregTab78063").hide();
                $("#divPregTab78073").hide();
            }else{
                $("#divPregTab78063").show();
                $("#divPregTab78073").show();
            }
            $("#cmb7805").change(function () {
                if ($(this).val() == "21936"){
                    $("#divPregTab78063").show();
                    $("#divPregTab78073").show();            
                }else{
                    $("#divPregTab78063").hide();
                    $("#divPregTab78073").hide();
                }
            });
            //Indicadores proposito
            for (var i = 1; i <= 2; i++) {
                $("#Tabcmb7042" + i.toString()).prop('disabled', true);
                $("#Tabcmb7477" + i.toString()).prop('disabled', true);
                $("#Tabcmb7043" + i.toString()).prop('disabled', true);
                $("#Tabcmb7478" + i.toString()).prop('disabled', true);
                $("#Tabcmb7044" + i.toString()).prop('disabled', true);
                $("#Tabcmb7479" + i.toString()).prop('disabled', true);
            }
            //Indicadores complementarios
            for (var i = 1; i <= 15; i++) {
                $("#Tabcmb7071" + i.toString()).prop('disabled', true);
                $("#Tabcmb7072" + i.toString()).prop('disabled', true);
                $("#Tabcmb7073" + i.toString()).prop('disabled', true);
            }
        }else if (item.IdEvento == funcMuestOcultaVarResp){
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            var valorObj = $("#" + idObjeto + " option:selected").val();
            var todas = [];
            if (item.Datos.length > 0) {
                $.each(item.Datos, function (ii, d) {                        
                    if (d.Valor != null && d.Valor != "") {
                        if (todas.indexOf(d.Valor.toString()) < 0) {
                            todas.push(d.Valor.toString());
                        }                        
                    }
                });
                if (todas.length > 0){
                    if (todas.indexOf(valorObj) >= 0){
                        $("#divPreg" + item.IdPreguntaDependiente).show();                        
                    }else{
                        $("#divPreg" + item.IdPreguntaDependiente).hide();
                    }
                }
            }
            $("#" + idObjeto).change(function () {
                valorObj = $(this).val();
                var todas = [];
                if (item.Datos.length > 0) {
                    $.each(item.Datos, function (ii, d) {
                        if (d.Valor != null && d.Valor != "") {
                            if (todas.indexOf(d.Valor.toString()) < 0) {
                                todas.push(d.Valor.toString());
                            }                        
                        }
                    });
                    if (todas.length > 0){
                        if (todas.indexOf(valorObj) >= 0){
                            $("#divPreg" + item.IdPreguntaDependiente).show();                        
                        }else{
                            $("#divPreg" + item.IdPreguntaDependiente).hide();
                        }
                    }
                }
            });
        } else if (item.IdEvento == funcModuloEvalExAnte2023){
            //Bloqueo calificacion
            $("#cmb458").prop('disabled', true);
            //Falta información
            var idSelectFI = [7912, 7913, 7914, 7915, 7916];
            var esFI = 0;
            $.each(idSelectFI, function (i, d) {                
                esFI = $("#cmb" + d + " option:selected").val() == "902" ? esFI++ : esFI;                
                $("#cmb" + d).bind("change", function () {
                    var esFI2 = 0;
                    if ($(this).val() == "902"){
                        esFI2++;
                    }else{
                        if (idSelectFI.indexOf(this.id.substring(3, this.id.length)) < 0){
                            $.each(idSelectFI, function (x, y) { 
                                if ($("#cmb" + y + " option:selected").val() == "902"){ 
                                    esFI2++; 
                                }
                            });
                        }
                    }
                    if (esFI2 > 0){
                        $("#cmb458 option[value='1062']").prop('selected', true);
                    }else{
                        $("#cmb458 option[value='-1']").prop('selected', true);
                    }
                });
            });
            if (esFI > 0){
                $("#cmb458 option[value='1062']").prop('selected', true);
            }
            //Minimos
            $("#txt9127").prop('disabled', true);
            $("#txt9128").prop('disabled', true);
            $("#txt9127").val(30);
            $("#txt9128").val($("#cmb6778 option:selected").val() == "7304" ? 21 : 21.5);            
            var minimoAtingencia = 0;
            //Atingencia - Antecedentes
            $("#txt7935").prop('disabled', true);
            $("#Tabcmb79301").bind("change", function () {
                var puntajeFinalAtingencia = 0;
                if ($(this).val() == "24431"){
                    puntajeFinalAtingencia = ($("#cmb6778 option:selected").val() == "7304" ? 3 : 4);
                    $("#txt79321").val(puntajeFinalAtingencia);
                    if ($("#Tabcmb79582 option:selected").val() == "24451" && $("#Tabcmb79602 option:selected").val() == "24461" && $("#Tabcmb79622 option:selected").val() == "24471" && ($("#Tabcmb79642 option:selected").val() == "24481" || $("#Tabcmb79642 option:selected").val() == "24483")){
                        minimoAtingencia = 1;
                    }else{
                        minimoAtingencia = 0;
                    }
                }else{                    
                    puntajeFinalAtingencia = 0;
                    $("#txt79321").val(puntajeFinalAtingencia);
                    minimoAtingencia = 0;
                }
                var otroPuntaje = ($("#txt79331").val() == null || $("#txt79331").val() == "" ? 0 : $("#txt79331").val());
                $("#txt7935").val(puntajeFinalAtingencia + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb79311").bind("change", function () {
                var puntajeFinalAtingencia2 = 0;
                if ($(this).val() == "24441"){
                    puntajeFinalAtingencia2 = ($("#cmb6778 option:selected").val() == "7304" ? 2 : 3);
                    $("#txt79331").val(puntajeFinalAtingencia2);
                }else if ($(this).val() == "24442"){
                    puntajeFinalAtingencia2 = ($("#cmb6778 option:selected").val() == "7304" ? 1 : 1.5);
                    $("#txt79331").val(puntajeFinalAtingencia2);
                }else{
                    puntajeFinalAtingencia2 = 0;
                    $("#txt79331").val(puntajeFinalAtingencia2);
                }
                var otroPuntaje = ($("#txt79321").val() == null || $("#txt79321").val() == "" ? 0 : $("#txt79321").val());
                $("#txt7935").val(puntajeFinalAtingencia2 + parseFloat(otroPuntaje)).trigger('change');
            });
            //Atingencia - Diagnóstico
            $("#txt8207").prop('disabled', true);
            var puntTotalesAting = [79652,79662,79672,79682,79692,79702,82052,82062,79712];
            if ($("#cmb6778 option:selected").val() != "7304"){
                $("#Tabcmb81992").prop('disabled', true);
                $("#Tabcmb82012").prop('disabled', true);
                $("#Tabcmb82032").prop('disabled', true);
                $("#Tabcmb82002").prop('disabled', true);
                $("#Tabcmb82022").prop('disabled', true);
                $("#Tabcmb82042").prop('disabled', true);
            }
            $("#Tabcmb79582").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24451"){
                    puntFinalAtingenciaDiag = ($("#cmb6778 option:selected").val() == "7304" ? 3 : 4);
                    $("#txt79652").val(puntFinalAtingenciaDiag);
                    if ($("#Tabcmb79301 option:selected").val() == "24431" && $("#Tabcmb79602 option:selected").val() == "24461" && $("#Tabcmb79622 option:selected").val() == "24471" && ($("#Tabcmb79642 option:selected").val() == "24481" || $("#Tabcmb79642 option:selected").val() == "24483")){
                        minimoAtingencia = 1;
                    }else{
                        minimoAtingencia = 0;
                    }
                }else if ($(this).val() == "24452"){
                    puntFinalAtingenciaDiag = ($("#cmb6778 option:selected").val() == "7304" ? 1.5 : 2);
                    $("#txt79652").val(puntFinalAtingenciaDiag);
                    minimoAtingencia = 0;
                }else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt79652").val(puntFinalAtingenciaDiag);
                    minimoAtingencia = 0;
                }
                var otroPuntaje = 0;
                $.each(puntTotalesAting, function (x, y) { 
                    if (y != 79652){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });             
                $("#txt8207").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb79592").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24456"){
                    puntFinalAtingenciaDiag = 3;
                    $("#txt79662").val(puntFinalAtingenciaDiag);
                }else if ($(this).val() == "24457"){
                    puntFinalAtingenciaDiag = 1.5;
                    $("#txt79662").val(puntFinalAtingenciaDiag);
                }else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt79662").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesAting, function (x, y) { 
                    if (y != 79662){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8207").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb79602").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24461"){
                    puntFinalAtingenciaDiag = ($("#cmb6778 option:selected").val() == "7304" ? 3 : 4);
                    $("#txt79672").val(puntFinalAtingenciaDiag);
                    if ($("#Tabcmb79301 option:selected").val() == "24431" && $("#Tabcmb79582 option:selected").val() == "24451" && $("#Tabcmb79622 option:selected").val() == "24471" && ($("#Tabcmb79642 option:selected").val() == "24481" || $("#Tabcmb79642 option:selected").val() == "24483")){
                        minimoAtingencia = 1;
                    }else{
                        minimoAtingencia = 0;
                    }
                } else if ($(this).val() == "24463"){
                    puntFinalAtingenciaDiag = ($("#cmb6778 option:selected").val() == "7304" ? 1.5 : 2);
                    $("#txt79672").val(puntFinalAtingenciaDiag);
                    minimoAtingencia = 0;
                }else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt79672").val(puntFinalAtingenciaDiag);
                    minimoAtingencia = 0;
                }
                var otroPuntaje = 0;
                $.each(puntTotalesAting, function (x, y) { 
                    if (y != 79672){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8207").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb79612").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24466"){
                    puntFinalAtingenciaDiag = 3;
                    $("#txt79682").val(puntFinalAtingenciaDiag);
                }else if ($(this).val() == "24467"){
                    puntFinalAtingenciaDiag = 1.5;
                    $("#txt79682").val(puntFinalAtingenciaDiag);
                }else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt79682").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesAting, function (x, y) { 
                    if (y != 79682){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8207").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb79622").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24471"){
                    puntFinalAtingenciaDiag = 3;
                    $("#txt79692").val(puntFinalAtingenciaDiag);
                    if ($("#Tabcmb79301 option:selected").val() == "24431" && $("#Tabcmb79582 option:selected").val() == "24451" && $("#Tabcmb79602 option:selected").val() == "24461" && ($("#Tabcmb79642 option:selected").val() == "24481" || $("#Tabcmb79642 option:selected").val() == "24483")){
                        minimoAtingencia = 1;
                    }else{
                        minimoAtingencia = 0;
                    }
                } else if ($(this).val() == "24473"){
                    puntFinalAtingenciaDiag = 1.5;
                    $("#txt79692").val(puntFinalAtingenciaDiag);
                    minimoAtingencia = 0;
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt79692").val(puntFinalAtingenciaDiag);
                    minimoAtingencia = 0;
                }
                var otroPuntaje = 0;
                $.each(puntTotalesAting, function (x, y) { 
                    if (y != 79692){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8207").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb79632").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24476"){
                    puntFinalAtingenciaDiag = 2;
                    $("#txt79702").val(puntFinalAtingenciaDiag);
                }else if ($(this).val() == "24477"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt79702").val(puntFinalAtingenciaDiag);
                }else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt79702").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesAting, function (x, y) { 
                    if (y != 79702){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8207").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            //Solo reformulados
            if ($("#cmb6778 option:selected").val() == "7304"){
                $("#Tabcmb82032").bind("change", function () {
                    var puntFinalAtingenciaDiag = 0;
                    if ($(this).val() == "24746"){
                        puntFinalAtingenciaDiag = ($("#cmb6778 option:selected").val() == "7304" ? 3 : 4);
                        $("#txt82052").val(puntFinalAtingenciaDiag);
                    }else if ($(this).val() == "24748"){
                        puntFinalAtingenciaDiag = ($("#cmb6778 option:selected").val() == "7304" ? 1.5 : 2);
                        $("#txt82052").val(puntFinalAtingenciaDiag);
                    }else{
                        puntFinalAtingenciaDiag = 0;
                        $("#txt82052").val(puntFinalAtingenciaDiag);
                    }
                    var otroPuntaje = 0;
                    $.each(puntTotalesAting, function (x, y) { 
                        if (y != 82052){
                            if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                                otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                            }
                        }                        
                    });
                    $("#txt8207").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
                });
                $("#Tabcmb82042").bind("change", function () {
                    var puntFinalAtingenciaDiag = 0;
                    if ($(this).val() == "24751"){
                        puntFinalAtingenciaDiag = 2;
                        $("#txt82062").val(puntFinalAtingenciaDiag);
                    }else if ($(this).val() == "24753"){
                        puntFinalAtingenciaDiag = 1;
                        $("#txt82062").val(puntFinalAtingenciaDiag);
                    }else{
                        puntFinalAtingenciaDiag = 0;
                        $("#txt82062").val(puntFinalAtingenciaDiag);
                    }
                    var otroPuntaje = 0;
                    $.each(puntTotalesAting, function (x, y) { 
                        if (y != 82062){
                            if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                                otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                            }
                        }                        
                    });
                    $("#txt8207").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
                });
                //Fin solo reformulados
            }
            $("#Tabcmb79642").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24481"){
                    puntFinalAtingenciaDiag = ($("#cmb6778 option:selected").val() == "7304" ? 3 : 4);
                    $("#txt79712").val(puntFinalAtingenciaDiag);
                    if ($("#Tabcmb79301 option:selected").val() == "24431" && $("#Tabcmb79582 option:selected").val() == "24451" && $("#Tabcmb79602 option:selected").val() == "24461" && $("#Tabcmb79622 option:selected").val() == "24471"){
                        minimoAtingencia = 1;
                    }else{
                        minimoAtingencia = 0;
                    }
                }else if ($(this).val() == "24483"){
                    puntFinalAtingenciaDiag = ($("#cmb6778 option:selected").val() == "7304" ? 1.5 : 2);
                    $("#txt79712").val(puntFinalAtingenciaDiag);
                    if ($("#Tabcmb79301 option:selected").val() == "24431" && $("#Tabcmb79582 option:selected").val() == "24451" && $("#Tabcmb79602 option:selected").val() == "24461" && $("#Tabcmb79622 option:selected").val() == "24471"){
                        minimoAtingencia = 1;
                    }else{
                        minimoAtingencia = 0;
                    }
                }else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt79712").val(puntFinalAtingenciaDiag);
                    minimoAtingencia = 0;
                }
                var otroPuntaje = 0;
                $.each(puntTotalesAting, function (x, y) { 
                    if (y != 79712){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8207").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            //Cohrencia - Objetivo y Poblaciones
            $("#txt8208").prop('disabled', true);
            var puntTotalesCohe = [80231,80241,80251,80261,80271,80281,80291,80301,80311,80321,80331,80341];
            var minimoCoherencia = 0;
            //Minimos
            $("#txt9130").prop('disabled', true);
            $("#txt9131").prop('disabled', true);
            $("#cmb9132").prop('disabled', true);
            $("#cmb9133").prop('disabled', true);
            $("#txt9130").val(40);
            $("#txt9131").val(28.5);
            $("#Tabcmb80111").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24496"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt80231").val(puntFinalAtingenciaDiag);
                }else if ($(this).val() == "24498"){
                    puntFinalAtingenciaDiag = 0.5;
                    $("#txt80231").val(puntFinalAtingenciaDiag);
                }else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80231").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe, function (x, y) { 
                    if (y != 80231){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8208").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb80121").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24501"){
                    puntFinalAtingenciaDiag = 3;
                    $("#txt80241").val(puntFinalAtingenciaDiag);
                    if ($("#Tabcmb80141 option:selected").val() == "24511" && ($("#Tabcmb80171 option:selected").val() == "24526" || $("#Tabcmb80181 option:selected").val() == "24531") && $("#Tabcmb80201 option:selected").val() == "24541" && ($("#Tabcmb80211 option:selected").val() == "24546" || $("#Tabcmb80211 option:selected").val() == "24548")){
                        minimoCoherencia = 1;
                    }else{
                        minimoCoherencia = 0;
                    }
                }else if ($(this).val() == "24503"){
                    puntFinalAtingenciaDiag = 1.5;
                    $("#txt80241").val(puntFinalAtingenciaDiag);
                    minimoCoherencia = 0;
                }else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80241").val(puntFinalAtingenciaDiag);
                    minimoCoherencia = 0;
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe, function (x, y) { 
                    if (y != 80241){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8208").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb80131").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24506"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt80251").val(puntFinalAtingenciaDiag);
                }else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80251").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe, function (x, y) { 
                    if (y != 80251){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8208").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb80141").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24511"){
                    puntFinalAtingenciaDiag = 2;
                    $("#txt80261").val(puntFinalAtingenciaDiag);
                    if ($("#Tabcmb80121 option:selected").val() == "24501" && ($("#Tabcmb80171 option:selected").val() == "24526" || $("#Tabcmb80181 option:selected").val() == "24531") && $("#Tabcmb80201 option:selected").val() == "24541" && ($("#Tabcmb80211 option:selected").val() == "24546" || $("#Tabcmb80211 option:selected").val() == "24548")){
                        minimoCoherencia = 1;
                    }else{
                        minimoCoherencia = 0;
                    }
                }else if ($(this).val() == "24513"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt80261").val(puntFinalAtingenciaDiag);
                    minimoCoherencia = 0;
                }else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80261").val(puntFinalAtingenciaDiag);
                    minimoCoherencia = 0;
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe, function (x, y) { 
                    if (y != 80261){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8208").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb80151").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24516"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt80271").val(puntFinalAtingenciaDiag);
                }else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80271").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe, function (x, y) { 
                    if (y != 80271){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8208").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb80161").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24521"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt80281").val(puntFinalAtingenciaDiag);
                }else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80281").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe, function (x, y) { 
                    if (y != 80281){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8208").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            if ($("#cmb55 option:selected").val() == "902"){
                $("#Tabcmb79941").prop('disabled', true);
                $("#Tabcmb80061").prop('disabled', true);
                $("#Tabcmb80181").prop('disabled', true);
                $("#Tabcmb79951").prop('disabled', true);
                $("#Tabcmb80071").prop('disabled', true);
                $("#Tabcmb80191").prop('disabled', true);
                $("#Tabcmb80171").bind("change", function () {
                    var puntFinalAtingenciaDiag = 0;
                    if ($(this).val() == "24526"){
                        puntFinalAtingenciaDiag = 4;
                        $("#txt80291").val(puntFinalAtingenciaDiag);
                        if ($("#Tabcmb80121 option:selected").val() == "24501" && $("#Tabcmb80141 option:selected").val() == "24511" && $("#Tabcmb80201 option:selected").val() == "24541" && ($("#Tabcmb80211 option:selected").val() == "24546" || $("#Tabcmb80211 option:selected").val() == "24548")){
                            minimoCoherencia = 1;
                        }else{
                            minimoCoherencia = 0;
                        }
                    } else{
                        puntFinalAtingenciaDiag = 0;
                        $("#txt80291").val(puntFinalAtingenciaDiag);
                    }
                    var otroPuntaje = 0;
                    $.each(puntTotalesCohe, function (x, y) { 
                        if (y != 80291){
                            if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                                otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                            }
                        }                        
                    });
                    $("#txt8208").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
                });
            }else{
                $("#Tabcmb79931").prop('disabled', true);
                $("#Tabcmb80051").prop('disabled', true);
                $("#Tabcmb80171").prop('disabled', true);
                $("#Tabcmb80181").bind("change", function () {                    
                    var puntFinalAtingenciaDiag = 0;
                    if ($(this).val() == "24531"){
                        puntFinalAtingenciaDiag = 3;
                        $("#txt80301").val(puntFinalAtingenciaDiag);
                        if ($("#Tabcmb80121 option:selected").val() == "24501" && $("#Tabcmb80141 option:selected").val() == "24511" && $("#Tabcmb80201 option:selected").val() == "24541" && ($("#Tabcmb80211 option:selected").val() == "24546" || $("#Tabcmb80211 option:selected").val() == "24548")){
                            minimoCoherencia = 1;
                        }else{
                            minimoCoherencia = 0;
                        }
                    }else if ($(this).val() == "24533"){
                        puntFinalAtingenciaDiag = 1.5;
                        $("#txt80301").val(puntFinalAtingenciaDiag);
                    } else{
                        puntFinalAtingenciaDiag = 0;
                        $("#txt80301").val(puntFinalAtingenciaDiag);
                    }
                    var otroPuntaje = 0;
                    $.each(puntTotalesCohe, function (x, y) { 
                        if (y != 80301){
                            if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                                otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                            }
                        }                        
                    });
                    $("#txt8208").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
                });
                $("#Tabcmb80191").bind("change", function () {
                    var puntFinalAtingenciaDiag = 0;
                    if ($(this).val() == "24536"){
                        puntFinalAtingenciaDiag = 1;
                        $("#txt80311").val(puntFinalAtingenciaDiag);
                    } else{
                        puntFinalAtingenciaDiag = 0;
                        $("#txt80311").val(puntFinalAtingenciaDiag);
                    }
                    var otroPuntaje = 0;
                    $.each(puntTotalesCohe, function (x, y) { 
                        if (y != 80311){
                            if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                                otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                            }
                        }                        
                    });
                    $("#txt8208").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
                });
            }
            $("#Tabcmb80201").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24541"){
                    puntFinalAtingenciaDiag = 2;
                    $("#txt80321").val(puntFinalAtingenciaDiag);
                    if ($("#Tabcmb80121 option:selected").val() == "24501" && $("#Tabcmb80141 option:selected").val() == "24511" && ($("#Tabcmb80171 option:selected").val() == "24526" || $("#Tabcmb80181 option:selected").val() == "24531") && ($("#Tabcmb80211 option:selected").val() == "24546" || $("#Tabcmb80211 option:selected").val() == "24548")){
                        minimoCoherencia = 1;
                    }else{
                        minimoCoherencia = 0;
                    }
                }else if ($(this).val() == "24543"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt80321").val(puntFinalAtingenciaDiag);
                    minimoCoherencia = 0;
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80321").val(puntFinalAtingenciaDiag);
                    minimoCoherencia = 0;
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe, function (x, y) { 
                    if (y != 80321){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8208").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb80211").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24546"){
                    puntFinalAtingenciaDiag = 2;
                    $("#txt80331").val(puntFinalAtingenciaDiag);
                    if ($("#Tabcmb80121 option:selected").val() == "24501" && $("#Tabcmb80141 option:selected").val() == "24511" && ($("#Tabcmb80171 option:selected").val() == "24526" || $("#Tabcmb80181 option:selected").val() == "24531")){
                        minimoCoherencia = 1;
                    }else{
                        minimoCoherencia = 0;
                    }
                }else if ($(this).val() == "24548"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt80331").val(puntFinalAtingenciaDiag);
                    if ($("#Tabcmb80121 option:selected").val() == "24501" && $("#Tabcmb80141 option:selected").val() == "24511" && ($("#Tabcmb80171 option:selected").val() == "24526" || $("#Tabcmb80181 option:selected").val() == "24531")){
                        minimoCoherencia = 1;
                    }else{
                        minimoCoherencia = 0;
                    }
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80331").val(puntFinalAtingenciaDiag);
                    minimoCoherencia = 0;
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe, function (x, y) { 
                    if (y != 80331){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8208").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb80221").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24551"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt80341").val(puntFinalAtingenciaDiag);
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80341").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe, function (x, y) { 
                    if (y != 80341){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8208").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            //Cohrencia - Estrategia de Intervencion
            var puntTotalesCohe2 = [80652,80662,80672,80682,80692,80702,80712];
            $("#txt8209").prop('disabled', true);
            $("#Tabcmb80582").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24561"){
                    puntFinalAtingenciaDiag = 2;
                    $("#txt80652").val(puntFinalAtingenciaDiag);
                    if ($("#Tabcmb80612 option:selected").val() == "24576" && $("#Tabcmb80622 option:selected").val() == "24581"){
                        minimoCoherencia = 1;
                    }else{
                        minimoCoherencia = 0;
                    }
                } else if ($(this).val() == "24563"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt80652").val(puntFinalAtingenciaDiag);
                    minimoCoherencia = 0;
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80652").val(puntFinalAtingenciaDiag);
                    minimoCoherencia = 0;
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe2, function (x, y) { 
                    if (y != 80652){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8209").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb80592").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24566"){
                    puntFinalAtingenciaDiag = 2;
                    $("#txt80662").val(puntFinalAtingenciaDiag);
                } else if ($(this).val() == "24568"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt80662").val(puntFinalAtingenciaDiag);
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80662").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe2, function (x, y) { 
                    if (y != 80662){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8209").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb80602").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24571"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt80672").val(puntFinalAtingenciaDiag);
                } else if ($(this).val() == "24573"){
                    puntFinalAtingenciaDiag = 0.5;
                    $("#txt80672").val(puntFinalAtingenciaDiag);
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80672").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe2, function (x, y) { 
                    if (y != 80672){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8209").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb80612").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24576"){
                    puntFinalAtingenciaDiag = 2;
                    $("#txt80682").val(puntFinalAtingenciaDiag);
                    if ($("#Tabcmb80582 option:selected").val() == "24561" && $("#Tabcmb80622 option:selected").val() == "24581"){
                        minimoCoherencia = 1;
                    }else{
                        minimoCoherencia = 0;
                    }
                } else if ($(this).val() == "24578"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt80682").val(puntFinalAtingenciaDiag);
                    minimoCoherencia = 0;
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80682").val(puntFinalAtingenciaDiag);
                    minimoCoherencia = 0;
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe2, function (x, y) { 
                    if (y != 80682){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8209").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb80622").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24581"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt80692").val(puntFinalAtingenciaDiag);
                    if ($("#Tabcmb80582 option:selected").val() == "24561" && $("#Tabcmb80612 option:selected").val() == "24576"){
                        minimoCoherencia = 1;
                    }else{
                        minimoCoherencia = 0;
                    }
                } else if ($(this).val() == "24583"){
                    puntFinalAtingenciaDiag = 0.5;
                    $("#txt80692").val(puntFinalAtingenciaDiag);
                    minimoCoherencia = 0;
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80692").val(puntFinalAtingenciaDiag);
                    minimoCoherencia = 0;
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe2, function (x, y) { 
                    if (y != 80692){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8209").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb80632").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24586"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt80702").val(puntFinalAtingenciaDiag);
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80702").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe2, function (x, y) { 
                    if (y != 80702){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8209").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb80642").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24591"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt80712").val(puntFinalAtingenciaDiag);
                } else if ($(this).val() == "24593"){
                    puntFinalAtingenciaDiag = 0.5;
                    $("#txt80712").val(puntFinalAtingenciaDiag);
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80712").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe2, function (x, y) { 
                    if (y != 80712){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8209").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            //Cohrencia - Articulaciones y Complementariedades            
            var puntTotalesCohe3 = [80903,80913,80923,80933];
            $("#txt8210").prop('disabled', true);
            if ($("#cmb9065 option:selected").val() == "902"){
                $("#Tabcmb80783").prop('disabled', true);
                $("#Tabcmb80823").prop('disabled', true);
                $("#Tabcmb80863").prop('disabled', true);
            } else {
                $("#Tabcmb80793").prop('disabled', true);
                $("#Tabcmb80833").prop('disabled', true);
                $("#Tabcmb80873").prop('disabled', true);
            }
            $("#Tabcmb80863").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24601"){
                    puntFinalAtingenciaDiag = 3;
                    $("#txt80903").val(puntFinalAtingenciaDiag);
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80903").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe3, function (x, y) { 
                    if (y != 80903){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8210").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb80873").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24606"){
                    puntFinalAtingenciaDiag = 3;
                    $("#txt80913").val(puntFinalAtingenciaDiag);
                } else if ($(this).val() == "24607"){
                    puntFinalAtingenciaDiag = 1.5;
                    $("#txt80913").val(puntFinalAtingenciaDiag);
                }else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80913").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe3, function (x, y) { 
                    if (y != 80913){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8210").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            if ($("#cmb2426 option:selected").val() == "902"){
                $("#Tabcmb80803").prop('disabled', true);
                $("#Tabcmb80843").prop('disabled', true);
                $("#Tabcmb80883").prop('disabled', true);
            } else {
                $("#Tabcmb80813").prop('disabled', true);
                $("#Tabcmb80853").prop('disabled', true);
                $("#Tabcmb80893").prop('disabled', true);
            }
            $("#Tabcmb80883").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24611"){
                    puntFinalAtingenciaDiag = 3;
                    $("#txt80923").val(puntFinalAtingenciaDiag);
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80923").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe3, function (x, y) { 
                    if (y != 80923){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8210").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb80893").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24616"){
                    puntFinalAtingenciaDiag = 3;
                    $("#txt80933").val(puntFinalAtingenciaDiag);
                } else if ($(this).val() == "24617"){
                    puntFinalAtingenciaDiag = 1.5;
                    $("#txt80933").val(puntFinalAtingenciaDiag);
                }else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt80933").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe3, function (x, y) { 
                    if (y != 80933){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8210").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            //Cohrencia - Enfoques de DDHH
            var puntTotalesCohe4 = [81124,81134,81144,81154];
            $("#txt8211").prop('disabled', true);
            if ($("#cmb7691 option:selected").val() == "902" || $("#cmb9284 option:selected").val() == "902"){
                $("#Tabcmb81004").prop('disabled', true);
                $("#Tabcmb81044").prop('disabled', true);
                $("#Tabcmb81084").prop('disabled', true);
            } else {
                $("#Tabcmb81014").prop('disabled', true);
                $("#Tabcmb81054").prop('disabled', true);
                $("#Tabcmb81094").prop('disabled', true);
            }
            $("#Tabcmb81084").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24621"){
                    puntFinalAtingenciaDiag = 3;
                    $("#txt81124").val(puntFinalAtingenciaDiag);
                } else if ($(this).val() == "24623"){
                    puntFinalAtingenciaDiag = 1.5;
                    $("#txt81124").val(puntFinalAtingenciaDiag);
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt81124").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe4, function (x, y) { 
                    if (y != 81124){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8211").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb81094").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24631"){
                    puntFinalAtingenciaDiag = 3;
                    $("#txt81134").val(puntFinalAtingenciaDiag);
                } else if ($(this).val() == "24633"){
                    puntFinalAtingenciaDiag = 1.5;
                    $("#txt81134").val(puntFinalAtingenciaDiag);
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt81134").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe4, function (x, y) { 
                    if (y != 81134){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8211").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            if ($("#cmb9066 option:selected").val() == "902"){
                $("#Tabcmb81024").prop('disabled', true);
                $("#Tabcmb81064").prop('disabled', true);
                $("#Tabcmb81104").prop('disabled', true);
            } else {
                $("#Tabcmb81034").prop('disabled', true);
                $("#Tabcmb81074").prop('disabled', true);
                $("#Tabcmb81114").prop('disabled', true);
            }
            $("#Tabcmb81104").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24636"){
                    puntFinalAtingenciaDiag = 3;
                    $("#txt81144").val(puntFinalAtingenciaDiag);
                } else if ($(this).val() == "24638"){
                    puntFinalAtingenciaDiag = 1.5;
                    $("#txt81144").val(puntFinalAtingenciaDiag);
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt81144").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe4, function (x, y) { 
                    if (y != 81144){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8211").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb81114").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24641"){
                    puntFinalAtingenciaDiag = 3;
                    $("#txt81154").val(puntFinalAtingenciaDiag);
                } else if ($(this).val() == "24642"){
                    puntFinalAtingenciaDiag = 1.5;
                    $("#txt81154").val(puntFinalAtingenciaDiag);
                }else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt81154").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe4, function (x, y) { 
                    if (y != 81154){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8211").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            //Consistencia - Indicadores de propósito
            //Minimos
            $("#txt9135").prop('disabled', true);
            $("#txt9136").prop('disabled', true);
            $("#cmb9137").prop('disabled', true);
            $("#cmb9138").prop('disabled', true);
            $("#txt9135").val(30);
            $("#txt9136").val(22);
            var puntTotalesCohe5 = [81431,81441,81451,91431];
            $("#txt8212").prop('disabled', true);
            minimoConsistencia = 0;
            $("#Tabcmb81371").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24661"){
                    puntFinalAtingenciaDiag = 4;
                    $("#txt81431").val(puntFinalAtingenciaDiag);
                    if ($("#Tabcmb81381 option:selected").val() == "24666"){
                        minimoConsistencia = 1;
                    }else{
                        minimoConsistencia = 0;
                    }
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt81431").val(puntFinalAtingenciaDiag);
                    minimoConsistencia = 0;
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe5, function (x, y) { 
                    if (y != 81431){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8212").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb81381").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24666"){
                    puntFinalAtingenciaDiag = 3;
                    $("#txt81441").val(puntFinalAtingenciaDiag);
                    if ($("#Tabcmb81371 option:selected").val() == "24661"){
                        minimoConsistencia = 1;
                    }else{
                        minimoConsistencia = 0;
                    }
                }else if ($(this).val() == "24668"){
                    puntFinalAtingenciaDiag = 1.5;
                    $("#txt81441").val(puntFinalAtingenciaDiag);
                    minimoConsistencia = 0;
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt81441").val(puntFinalAtingenciaDiag);
                    minimoConsistencia = 0;
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe5, function (x, y) { 
                    if (y != 81441){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8212").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb81391").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24671"){
                    puntFinalAtingenciaDiag = 2;
                    $("#txt81451").val(puntFinalAtingenciaDiag);
                } else if ($(this).val() == "24672"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt81451").val(puntFinalAtingenciaDiag);
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt81451").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe5, function (x, y) { 
                    if (y != 81451){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8212").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb91421").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "27368"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt91431").val(puntFinalAtingenciaDiag);
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt91431").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe5, function (x, y) { 
                    if (y != 91431){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }                        
                });
                $("#txt8212").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });            
            //Consistencia - Indicadores complementarios
            var puntTotalesCohe6 = [81672,81682,81692];
            $("#txt8213").prop('disabled', true);            
            $("#Tabcmb81632").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24706"){
                    puntFinalAtingenciaDiag = 3;
                    $("#txt81672").val(puntFinalAtingenciaDiag);
                    if ($("#Tabcmb81642 option:selected").val() == "24711"){
                        minimoConsistencia = 1;
                    }else{
                        minimoConsistencia = 0;
                    }
                }else if ($(this).val() == "24707"){
                    puntFinalAtingenciaDiag = 1.5;
                    $("#txt81672").val(puntFinalAtingenciaDiag);
                    minimoConsistencia = 0;
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt81672").val(puntFinalAtingenciaDiag);
                    minimoConsistencia = 0;
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe6, function (x, y) { 
                    if (y != 81672){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }
                });
                $("#txt8213").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb81642").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24711"){
                    puntFinalAtingenciaDiag = 3;
                    $("#txt81682").val(puntFinalAtingenciaDiag);
                    if ($("#Tabcmb81632 option:selected").val() == "24706"){
                        minimoConsistencia = 1;
                    }else{
                        minimoConsistencia = 0;
                    }
                }else if ($(this).val() == "24713"){
                    puntFinalAtingenciaDiag = 1.5;
                    $("#txt81682").val(puntFinalAtingenciaDiag);
                    minimoConsistencia = 0;
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt81682").val(puntFinalAtingenciaDiag);
                    minimoConsistencia = 0;
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe6, function (x, y) { 
                    if (y != 81682){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }
                });
                $("#txt8213").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb81652").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24716"){
                    puntFinalAtingenciaDiag = 2;
                    $("#txt81692").val(puntFinalAtingenciaDiag);
                } else if ($(this).val() == "24717"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt81692").val(puntFinalAtingenciaDiag);
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt81692").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe6, function (x, y) { 
                    if (y != 81692){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }
                });
                $("#txt8213").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            //Consistencia - Sistemas de información
            $("#txt8214").prop('disabled', true);
            $("#Tabcmb81763").bind("change", function () {
                var puntajeFinalAtingencia = 0;
                if ($(this).val() == "24721"){
                    puntajeFinalAtingencia = 3;
                    $("#txt81783").val(puntajeFinalAtingencia);
                } else if ($(this).val() == "24722"){
                    puntajeFinalAtingencia = 1.5;
                    $("#txt81783").val(puntajeFinalAtingencia);
                } else{
                    puntajeFinalAtingencia = 0;
                    $("#txt81783").val(puntajeFinalAtingencia);
                }
                var otroPuntaje = ($("#txt81793").val() == null || $("#txt81793").val() == "" ? 0 : $("#txt81793").val());
                $("#txt8214").val(puntajeFinalAtingencia + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb81773").bind("change", function () {
                var puntajeFinalAtingencia2 = 0;
                if ($(this).val() == "24726"){
                    puntajeFinalAtingencia2 = 2;
                    $("#txt81793").val(puntajeFinalAtingencia2);
                }else if ($(this).val() == "24727"){
                    puntajeFinalAtingencia2 = 1;
                    $("#txt81793").val(puntajeFinalAtingencia2);
                }else{
                    puntajeFinalAtingencia2 = 0;
                    $("#txt81793").val(puntajeFinalAtingencia2);
                }
                var otroPuntaje = ($("#txt81783").val() == null || $("#txt81783").val() == "" ? 0 : $("#txt81783").val());
                $("#txt8214").val(puntajeFinalAtingencia2 + parseFloat(otroPuntaje)).trigger('change');
            });
            //Consistencia - Gastos del programa
            $("#txt8215").prop('disabled', true);
            var puntTotalesCohe7 = [81944,81954,81964];
            $("#Tabcmb81914").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24731"){
                    puntFinalAtingenciaDiag = 3;
                    $("#txt81944").val(puntFinalAtingenciaDiag);
                } else if ($(this).val() == "24733"){
                    puntFinalAtingenciaDiag = 1.5;
                    $("#txt81944").val(puntFinalAtingenciaDiag);
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt81944").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe7, function (x, y) { 
                    if (y != 81944){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }
                });
                $("#txt8215").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb81924").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24736"){
                    puntFinalAtingenciaDiag = 3;
                    $("#txt81954").val(puntFinalAtingenciaDiag);                    
                    minimoConsistencia = 1;
                }else if ($(this).val() == "24738"){
                    puntFinalAtingenciaDiag = 1.5;
                    $("#txt81954").val(puntFinalAtingenciaDiag);
                    minimoConsistencia = 0;
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt81954").val(puntFinalAtingenciaDiag);
                    minimoConsistencia = 0;
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe7, function (x, y) { 
                    if (y != 81954){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }
                });
                $("#txt8215").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            $("#Tabcmb81934").bind("change", function () {
                var puntFinalAtingenciaDiag = 0;
                if ($(this).val() == "24741"){
                    puntFinalAtingenciaDiag = 1;
                    $("#txt81964").val(puntFinalAtingenciaDiag);
                } else{
                    puntFinalAtingenciaDiag = 0;
                    $("#txt81964").val(puntFinalAtingenciaDiag);
                }
                var otroPuntaje = 0;
                $.each(puntTotalesCohe7, function (x, y) { 
                    if (y != 81964){
                        if ($("#txt" + y).val() != null && $("#txt" + y).val() != ""){
                            otroPuntaje = otroPuntaje + parseFloat($("#txt" + y).val());
                        }
                    }
                });
                $("#txt8215").val(puntFinalAtingenciaDiag + parseFloat(otroPuntaje)).trigger('change');
            });
            //Puntaje final atingencia
            var pfAtingencia = 0;
            var puntajeAtingencia = ($("#cmb6778 option:selected").val() == "7304" ? 21 : 21.5);
            $("#cmb9125").prop('disabled', true);
            $("#cmb9126").prop('disabled', true);
            $("#txt7935, #txt8207").bind("change", function () {
                pfAtingencia = (($("#txt7935").val() != "" ? parseFloat($("#txt7935").val()) : 0) + ($("#txt8207").val() != "" ? parseFloat($("#txt8207").val()) : 0));
                minimoAtingencia = (($("#Tabcmb79301 option:selected").val() == "24431" && $("#Tabcmb79582 option:selected").val() == "24451" && $("#Tabcmb79602 option:selected").val() == "24461" && $("#Tabcmb79622 option:selected").val() == "24471" && ($("#Tabcmb79642 option:selected").val() == "24481" || $("#Tabcmb79642 option:selected").val() == "24483")) ? 1 : 0);
                if (pfAtingencia >= puntajeAtingencia && minimoAtingencia == 1){
                    $("#cmb7902 option[value='24411']").prop('selected', true);
                    $("#cmb9125 option[value='902']").prop('selected', true);
                    $("#cmb9126 option[value='27337']").prop('selected', true);
                }else{
                    $("#cmb7902 option[value='24412']").prop('selected', true);
                    $("#cmb9125 option[value='901']").prop('selected', true);
                    $("#cmb9126 option[value='27338']").prop('selected', true);
                }
                $("#txt9124").val(pfAtingencia).trigger('change');
            });
            //Puntaje final coherencia
            var pfCoherencia = 0;
            $("#txt8208, #txt8209, #txt8210, #txt8211").bind("change", function () {
                pfCoherencia = (($("#txt8208").val() != "" ? parseFloat($("#txt8208").val()) : 0) + ($("#txt8209").val() != "" ? parseFloat($("#txt8209").val()) : 0) + ($("#txt8210").val() != "" ? parseFloat($("#txt8210").val()) : 0) + ($("#txt8211").val() != "" ? parseFloat($("#txt8211").val()) : 0));
                minimoCoherencia = (($("#Tabcmb80121 option:selected").val() == "24501" && $("#Tabcmb80141 option:selected").val() == "24511" && ($("#Tabcmb80171 option:selected").val() == "24526" || $("#Tabcmb80181 option:selected").val() == "24531") && $("#Tabcmb80201 option:selected").val() == "24541" && ($("#Tabcmb80211 option:selected").val() == "24546" || $("#Tabcmb80211 option:selected").val() == "24548") && $("#Tabcmb80612 option:selected").val() == "24576" && $("#Tabcmb80582 option:selected").val() == "24561" && $("#Tabcmb80622 option:selected").val() == "24581") ? 1 : 0);
                if (pfCoherencia >= 28.5 && minimoCoherencia == 1){
                    $("#cmb7903 option[value='24411']").prop('selected', true);
                    $("#cmb9132 option[value='902']").prop('selected', true);
                    $("#cmb9133 option[value='27356']").prop('selected', true);
                }else{
                    $("#cmb7903 option[value='24412']").prop('selected', true);
                    $("#cmb9132 option[value='901']").prop('selected', true);
                    $("#cmb9133 option[value='27357']").prop('selected', true);
                }
                $("#txt9129").val(pfCoherencia).trigger('change');
            });           
            //Puntaje final consistencia
            var pfConsistencia = 0;
            $("#txt8212, #txt8213, #txt8214, #txt8215").bind("change", function () {
                pfConsistencia = ($("#txt8212").val() != "" ? parseFloat($("#txt8212").val()) : 0) + ($("#txt8213").val() != "" ? parseFloat($("#txt8213").val()) : 0) + ($("#txt8214").val() != "" ? parseFloat($("#txt8214").val()) : 0) + ($("#txt8215").val() != "" ? parseFloat($("#txt8215").val()) : 0);
                minimoConsistencia = (($("#Tabcmb81371 option:selected").val() == "24661" && $("#Tabcmb81381 option:selected").val() == "24666" && $("#Tabcmb81642 option:selected").val() == "24711" && $("#Tabcmb81632 option:selected").val() == "24706" && $("#Tabcmb81924 option:selected").val() == "24736") ? 1 : 0);
                if (pfConsistencia >= 22 && minimoConsistencia == 1){
                    $("#cmb7904 option[value='24411']").prop('selected', true);
                    $("#cmb9137 option[value='902']").prop('selected', true);
                    $("#cmb9138 option[value='27362']").prop('selected', true);
                }else{
                    $("#cmb7904 option[value='24412']").prop('selected', true);
                    $("#cmb9137 option[value='901']").prop('selected', true);
                    $("#cmb9138 option[value='27363']").prop('selected', true);
                }
                $("#txt9134").val(pfConsistencia).trigger('change');
            });            
            //Puntaje final
            var pfFinal = 0;
            $("#txt9124, #txt9129, #txt9134").bind("change", function () {
                if ($("#txt9124").val() != null && $("#txt9124").val() != "") {
                    pfAtingencia = $("#txt9124").val();
                }
                if ($("#txt9129").val() != null && $("#txt9129").val() != "") {
                    pfCoherencia = $("#txt9129").val();
                }
                if ($("#txt9134").val() != null && $("#txt9134").val() != "") {
                    pfConsistencia = $("#txt9134").val();
                }
                pfFinal = parseFloat(pfAtingencia) + parseFloat(pfCoherencia) + parseFloat(pfConsistencia);
                $("#txt7908").val(pfFinal).trigger('change');
            });
            //Calificación
            $("#cmb7902").prop('disabled', true);
            $("#cmb7903").prop('disabled', true);
            $("#cmb7904").prop('disabled', true);
            $("#txt7908").bind("change", function () { 
                if ($("#txt9124").val() >= puntajeAtingencia && $("#txt9129").val() >= 28.5 && $("#txt9134").val() >= 22 && $("#cmb7902 option:selected").val() == "24411" && $("#cmb7903 option:selected").val() == "24411" && $("#cmb7904 option:selected").val() == "24411"){
                    $("#cmb458 option[value='1064']").prop('selected', true);
                }else{
                    $("#cmb458 option[value='1063']").prop('selected', true);
                }
            });
        }else if (item.IdEvento == funcOtrasOpciones2) {
            if (item.CategoriaPregunta == tipoPregNumerico || item.TipoPregunta == tipoPregCheckInline) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregNumerico || item.TipoPregunta == tipoPregCheckInline) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            if ($.trim($("#" + idObjeto).val()) != "") {
                if ($.trim($("#" + idObjeto).val()) == "S/I" || $.trim($("#" + idObjeto).val()) == "N/A") {
                    $("#" + idObjeto).prop('readonly', true);
                    if (item.Datos.length > 0) {
                        $.each(item.Datos, function (i, d) {
                            if (d.Valor != null){
                                $("#txt" + d.Valor).prop('readonly', true);
                            }                            
                        });                        
                    }
                    $("#txt" + item.IdPreguntaDependiente).css("display", "");
                    $("#txtCount" + item.IdPreguntaDependiente).css("display", "");
                } else if ($("#" + idObjeto).val() == "0"){
                    $("#txt" + item.IdPreguntaDependiente).css("display", "");
                    $("#txtCount" + item.IdPreguntaDependiente).css("display", "");
                }else{
                    $("#txt" + item.IdPreguntaDependiente).css("display", "none");
                    $("#txtCount" + item.IdPreguntaDependiente).css("display", "none");
                }
            }else{
                $("#txt" + item.IdPreguntaDependiente).css("display", "none");
                $("#txtCount" + item.IdPreguntaDependiente).css("display", "none");
            }
            $("#" + idObjeto).bind("keyup change", function () {
                if ($(this).val() == "0") {
                    $("#txt" + item.IdPreguntaDependiente).css("display", "");
                    $("#txtCount" + item.IdPreguntaDependiente).css("display", "");
                }else{
                    $("#txt" + item.IdPreguntaDependiente).css("display", "none");
                    $("#txtCount" + item.IdPreguntaDependiente).css("display", "none");
                }
            });
            $(".otrasOpciones" + item.IdPregunta).click(function () {
                if ($(this).val() == "-1") {
                    if ($("#" + idObjeto).val() == "S/I"){
                        $("#" + idObjeto).val('');
                        $("#" + idObjeto).prop('readonly', false);
                        if (item.Datos.length > 0) {
                            $.each(item.Datos, function (i, d) {
                                if (d.Valor != null){
                                    $("#txt" + d.Valor).val('');
                                    if (d.Valor2 != "1"){
                                        $("#txt" + d.Valor).prop('readonly', false);
                                    }                                    
                                }
                            });
                        }
                        $("#txt" + item.IdPreguntaDependiente).css("display", "none");
                        $("#txtCount" + item.IdPreguntaDependiente).css("display", "none");
                        $("#txt" + item.IdPreguntaDependiente).val("");
                    }else{                        
                        $("#" + idObjeto).val('S/I');
                        $("#" + idObjeto).prop('readonly', true);
                        if (item.Datos.length > 0) {
                            $.each(item.Datos, function (i, d) {
                                if (d.Valor != null){
                                    $("#txt" + d.Valor).val('S/I');
                                    if (d.Valor2 != "1"){
                                        $("#txt" + d.Valor).prop('readonly', true);
                                    }
                                }
                            });
                        }
                        $("#txt" + item.IdPreguntaDependiente).css("display", "");
                        $("#txtCount" + item.IdPreguntaDependiente).css("display", "");
                    }                    
                } else if ($(this).val() == "-2") {
                    if ($("#" + idObjeto).val() == "N/A"){
                        $("#" + idObjeto).val('');
                        $("#" + idObjeto ).prop('readonly', false);
                        if (item.Datos.length > 0) {
                            $.each(item.Datos, function (i, d) {
                                if (d.Valor != null){
                                    $("#txt" + d.Valor).val('');
                                    if (d.Valor2 != "1"){
                                        $("#txt" + d.Valor).prop('readonly', false);
                                    }
                                }
                            });                        
                        }
                        $("#txt" + item.IdPreguntaDependiente).css("display", "none");
                        $("#txtCount" + item.IdPreguntaDependiente).css("display", "none");
                        $("#txt" + item.IdPreguntaDependiente).val("");
                    }else{                        
                        $("#" + idObjeto).val('N/A');
                        $("#" + idObjeto).prop('readonly', true);
                        if (item.Datos.length > 0) {
                            $.each(item.Datos, function (i, d) {
                                if (d.Valor != null){
                                    $("#txt" + d.Valor).val('N/A');
                                    if (d.Valor2 != "1"){
                                        $("#txt" + d.Valor).prop('readonly', true);
                                    }
                                }
                            });
                        }
                        $("#txt" + item.IdPreguntaDependiente).css("display", "");
                        $("#txtCount" + item.IdPreguntaDependiente).css("display", "");
                    }                    
                }
            });
        } else if (item.IdEvento == funcDivisionesPorcentaje) {
            if (item.CategoriaPregunta == tipoPregNumerico || item.TipoPregunta == tipoPregTotal) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregNumerico || item.TipoPreguntaDependiente == tipoPregTotal) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            if (item.ValorPregunta != "" && item.ValorPregunta != null) {
                for (var y = 1; y <= parseInt(item.ValorPregunta) ; y++) {
                    if (item.Datos.length > 0) {
                        $.each(item.Datos, function (i, d) {
                            if (d.Valor != null && d.Valor != "") {
                                var blancos = false;
                                var total = 0;
                                var valor1 = 0;
                                var valor2 = 0;
                                if ($.trim($("#Tab" + idObjeto + y).val()) != "") {
                                    blancos = true;
                                    valor1 = retornaValorFormateado($("#Tab" + idObjeto + y).val());
                                }
                                if ($.trim($("#Tab" + idObjetoDep + y).val()) != "") {
                                    blancos = true;
                                    valor2 = retornaValorFormateado($("#Tab" + idObjetoDep + y).val());
                                }
                                if (blancos) {
                                    if (valor2 > 0) {
                                        total = (Math.round((valor1 / valor2) * 100) / 100) * 100;
                                        $("#Tabtxt" + d.Valor + y).val(total.toString().replace('.', ',') + "%");
                                    }
                                } else {
                                    $("#Tabtxt" + d.Valor + y).val('');
                                }
                            }
                        });
                    }
                    $("#Tab" + idObjeto + y).bind("keyup change", function () {
                        var nTab = $(this).attr('class').split(/\s+/)[2].split('-')[1];
                        if (item.Datos.length > 0) {
                            $.each(item.Datos, function (i, d) {
                                if (d.Valor != null && d.Valor != "") {
                                    var blancos = false;
                                    var total = 0;
                                    var valor1 = 0;
                                    var valor2 = 0;
                                    if ($.trim($("#Tab" + idObjeto + nTab).val()) != "") {
                                        blancos = true;
                                        valor1 = retornaValorFormateado($("#Tab" + idObjeto + nTab).val());
                                    }
                                    if ($.trim($("#Tab" + idObjetoDep + nTab).val()) != "") {
                                        blancos = true;
                                        valor2 = retornaValorFormateado($("#Tab" + idObjetoDep + nTab).val());
                                    }
                                    if (blancos) {
                                        if (valor2 > 0) {
                                            total = (Math.round((valor1 / valor2) * 100) / 100) * 100;
                                            $("#Tabtxt" + d.Valor + nTab).val(total.toString().replace('.', ',') + "%");
                                        }
                                    } else {
                                        $("#Tabtxt" + d.Valor + nTab).val('');
                                    }
                                }
                            });
                        }
                    });
                    $("#Tab" + idObjetoDep + y).bind("keyup change", function () {
                        var nTab = $(this).attr('class').split(/\s+/)[2].split('-')[1];
                        if (item.Datos.length > 0) {
                            $.each(item.Datos, function (i, d) {
                                if (d.Valor != null && d.Valor != "") {
                                    var blancos = false;
                                    var total = 0;
                                    var valor1 = 0;
                                    var valor2 = 0;
                                    if ($.trim($("#Tab" + idObjeto + nTab).val()) != "") {
                                        blancos = true;
                                        valor1 = retornaValorFormateado($("#Tab" + idObjeto + nTab).val());
                                    }
                                    if ($.trim($("#Tab" + idObjetoDep + nTab).val()) != "") {
                                        blancos = true;
                                        valor2 = retornaValorFormateado($("#Tab" + idObjetoDep + nTab).val());
                                    }
                                    if (blancos) {
                                        if (valor2 > 0) {
                                            total = (Math.round((valor1 / valor2) * 100) / 100) * 100;
                                            $("#Tabtxt" + d.Valor + nTab).val(total.toString().replace('.', ',') + "%");
                                        }
                                    } else {
                                        $("#Tabtxt" + d.Valor + nTab).val('');
                                    }
                                }
                            });
                        }
                    });
                }
            }else{
                if (item.Datos.length > 0) {
                    $.each(item.Datos, function (i, d) {
                        if (d.Valor != null && d.Valor != "") {
                            var blancos = false;
                            var total = 0;
                            var valor1 = 0;
                            var valor2 = 0;
                            if ($.trim($("#" + idObjeto).val()) != "") {
                                blancos = true;
                                valor1 = retornaValorFormateado($("#" + idObjeto).val());
                            }
                            if ($.trim($("#" + idObjetoDep).val()) != "") {
                                blancos = true;
                                valor2 = retornaValorFormateado($("#" + idObjetoDep).val());
                            }
                            if (blancos) {
                                if (valor2 > 0) {
                                    total = (Math.round((valor1 / valor2) * 100) / 100) * 100;
                                    $("#txt" + d.Valor).val(total.toString().replace('.', ',') + "%");
                                }
                            } else {
                                $("#txt" + d.Valor).val('');
                            }
                        }
                    });
                }
                $("#" + idObjeto).bind("keyup change", function () {
                    if (item.Datos.length > 0) {
                        $.each(item.Datos, function (i, d) {
                            if (d.Valor != null && d.Valor != "") {
                                var blancos = false;
                                var total = 0;
                                var valor1 = 0;
                                var valor2 = 0;
                                if ($.trim($("#" + idObjeto).val()) != "") {
                                    blancos = true;
                                    valor1 = retornaValorFormateado($("#" + idObjeto).val());
                                }
                                if ($.trim($("#" + idObjetoDep).val()) != "") {
                                    blancos = true;
                                    valor2 = retornaValorFormateado($("#" + idObjetoDep).val());
                                }
                                if (blancos) {
                                    if (valor2 > 0) {
                                        total = (Math.round((valor1 / valor2) * 100) / 100) * 100;
                                        $("#txt" + d.Valor).val(total.toString().replace('.', ',') + "%");
                                    }
                                } else {
                                    $("#txt" + d.Valor).val('');
                                }
                            }
                        });
                    }
                });
                $("#" + idObjetoDep).bind("keyup change", function () {
                    if (item.Datos.length > 0) {
                        $.each(item.Datos, function (i, d) {
                            if (d.Valor != null && d.Valor != "") {
                                var blancos = false;
                                var total = 0;
                                var valor1 = 0;
                                var valor2 = 0;
                                if ($.trim($("#" + idObjeto).val()) != "") {
                                    blancos = true;
                                    valor1 = retornaValorFormateado($("#" + idObjeto).val());
                                }
                                if ($.trim($("#" + idObjetoDep).val()) != "") {
                                    blancos = true;
                                    valor2 = retornaValorFormateado($("#" + idObjetoDep).val());
                                }
                                if (blancos) {
                                    if (valor2 > 0) {
                                        total = (Math.round((valor1 / valor2) * 100) / 100) * 100;
                                        $("#txt" + d.Valor).val(total.toString().replace('.', ',') + "%");
                                    }
                                } else {
                                    $("#txt" + d.Valor).val('');
                                }
                            }
                        });
                    }
                });
            }
        } else if (item.IdEvento == funcModuloEvalMonitoreo2023) {
            var nuevaEvaluacion = [{idPadre: 'cmb8908', hijos: [89101,89111]},{idPadre: 'cmb8913', hijos: [89142,89152]},{idPadre: 'cmb8961', hijos: [89891,90031]},{idPadre: 'cmb8962', hijos: [89901,90041]},{idPadre: 'cmb8963', hijos: [89911,90051]},{idPadre: 'cmb8964', hijos: [89922,90062]},{idPadre: 'cmb8965', hijos: [89933,90073]},{idPadre: 'cmb8966', hijos: [89943,90083]},{idPadre: 'cmb8967', hijos: [89953,90093]},{idPadre: 'Tabcmb89681', hijos: [89961,90101]},{idPadre: 'Tabcmb89682', hijos: [89962,90102]},{idPadre: 'Tabcmb89691', hijos: [89971,90111]},{idPadre: 'Tabcmb89692', hijos: [89972,90112]},{idPadre: 'Tabcmb89701', hijos: [89981,90121]},{idPadre: 'Tabcmb89702', hijos: [89982,90122]},{idPadre: 'Tabcmb89721', hijos: [89991,90131]},{idPadre: 'Tabcmb89722', hijos: [89992,90132]}];
            var nuevaEvalIndic = {};
            for (var i = 1; i <= 15; i++){
                nuevaEvalIndic = {};
                nuevaEvalIndic.idPadre = 'Tabcmb8971' + i;
                nuevaEvalIndic.hijos = ['9000' + i,'9014' + i];
                nuevaEvaluacion.push(nuevaEvalIndic);
                nuevaEvalIndic = {};
                nuevaEvalIndic.idPadre = 'Tabcmb8973' + i;
                nuevaEvalIndic.hijos = ['9001' + i,'9015' + i];
                nuevaEvaluacion.push(nuevaEvalIndic);
                nuevaEvalIndic = {};
                nuevaEvalIndic.idPadre = 'Tabcmb8974' + i;
                nuevaEvalIndic.hijos = ['9002' + i,'9016' + i];
                nuevaEvaluacion.push(nuevaEvalIndic);
            }
            $.each(nuevaEvaluacion, function (x, y) {
                if ($("#" + y.idPadre + " option:selected").text().toUpperCase() == "NO" || $("#" + y.idPadre + " option:selected").val() == "-1") {
                    $.each(y.hijos, function (a, b) {
                        $("#divPregTab" + b).hide();
                    });
                } else {
                    $.each(y.hijos, function (a, b) {
                        $("#divPregTab" + b).show();
                    });                    
                }
                $("#" + y.idPadre).bind("change", function () {                    
                    if ($("#" + y.idPadre + " option:selected").text().toUpperCase() == "NO" || $("#" + y.idPadre + " option:selected").val() == "-1") {
                        $.each(y.hijos, function (a, b) {
                            $("#divPregTab" + b).hide();
                        });
                    } else {
                        $.each(y.hijos, function (a, b) {
                            $("#divPregTab" + b).show();
                        });                    
                    }
                });
            });            
        } else if (item.IdEvento == funcCargaFraseInput) {
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregTexto || item.TipoPreguntaDependiente == tipoPregTxtGrande) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            if (item.ValorPregunta != "" && item.ValorPregunta != null) {
                for (var i = 1; i <= parseInt(item.ValorPregunta) ; i++) {
                    $("#Tab" + idObjeto + i.toString()).change(function () {
                        var tab = this.id.substring(6, this.id.length);
                        var numTab = $("#numTab" + tab).val();
                        if ($("#Tab" + idObjeto + numTab +  " option:selected").val() != "-1"){
                            $("#txtTab" + item.IdPreguntaDependiente + numTab).val("");
                            $("#txtTab" + item.IdPreguntaDependiente + numTab).val($("#Tab" + idObjeto + numTab + " option:selected").text().substring(3,$("#Tab" + idObjeto + numTab + " option:selected").text().length));
                        }
                    });
                }
            }else{
                $("#" + idObjeto).change(function () {
                    if ($(this).val() != "-1"){
                        $("#" + idObjetoDep).val("");
                        $("#" + idObjetoDep).val($("#" + idObjeto + " option:selected").text().substring(3,$("#" + idObjeto + " option:selected").text().length));
                    }
                });
            }
        } else if (item.IdEvento == funcEjecutaCalEficiencia) {
            //Ejecucion presupuestaria inicial
            let ejecPresupIncial = $("#txt7600").val();
            $("#cmb8924").prop('disabled', true);
            $("#cmb9021").prop('disabled', true);
            if (ejecPresupIncial != ""){
                if (parseInt(ejecPresupIncial.split('%')[0]) < 85){
                    $("#cmb8924 option[value='26186']").prop('selected', true).trigger('change');
                    $("#cmb9021").prop('disabled', false);
                } else if (parseInt(ejecPresupIncial.split('%')[0]) >= 85 && parseInt(ejecPresupIncial.split('%')[0]) <= 110){
                    $("#cmb8924 option[value='26187']").prop('selected', true).trigger('change');                    
                } else if (parseInt(ejecPresupIncial.split('%')[0]) > 110){
                    $("#cmb8924 option[value='26189']").prop('selected', true).trigger('change');
                    $("#cmb9021").prop('disabled', false);
                }                
            }else{
                $("#cmb8924 option[value='26190']").prop('selected', true).trigger('change');
            }
            $("#cmb9021").change(function () {
                if ($(this).val() == "902"){
                    if (parseInt(ejecPresupIncial.split('%')[0]) < 85){
                        $("#cmb8924 option[value='26191']").prop('selected', true).trigger('change');
                    } else if (parseInt(ejecPresupIncial.split('%')[0]) > 110){
                        $("#cmb8924 option[value='26188']").prop('selected', true).trigger('change');
                    }
                }else{
                    if (parseInt(ejecPresupIncial.split('%')[0]) < 85){
                        $("#cmb8924 option[value='26186']").prop('selected', true).trigger('change');
                        $("#cmb9021").prop('disabled', false);
                    } else if (parseInt(ejecPresupIncial.split('%')[0]) >= 85 && parseInt(ejecPresupIncial.split('%')[0]) <= 110){
                        $("#cmb8924 option[value='26187']").prop('selected', true).trigger('change');                    
                    } else if (parseInt(ejecPresupIncial.split('%')[0]) > 110){
                        $("#cmb8924 option[value='26189']").prop('selected', true).trigger('change');
                        $("#cmb9021").prop('disabled', false);
                    }
                }
            });
            //Ejecucion presupuestaria final
            $("#cmb8928").prop('disabled', true);
            $("#cmb9022").prop('disabled', true);
            let ejecPresupFinal =  $("#txt7601").val();
            if (ejecPresupFinal != ""){
                if (parseInt(ejecPresupFinal.split('%')[0]) < 90){
                    $("#cmb8928 option[value='26197']").prop('selected', true).trigger('change');
                    $("#cmb9022").prop('disabled', false);
                } else if (parseInt(ejecPresupFinal.split('%')[0]) >= 90 && parseInt(ejecPresupFinal.split('%')[0]) <= 110){
                    $("#cmb8928 option[value='26198']").prop('selected', true).trigger('change');
                } else if (parseInt(ejecPresupFinal.split('%')[0]) > 110){
                    $("#cmb8928 option[value='26200']").prop('selected', true).trigger('change');
                    $("#cmb9022").prop('disabled', false);
                }
            }else{
                $("#cmb8928 option[value='26201']").prop('selected', true).trigger('change');
            }
            $("#cmb9022").change(function () {
                if ($(this).val() == "902"){
                    if (parseInt(ejecPresupFinal.split('%')[0]) < 90){
                        $("#cmb8928 option[value='26196']").prop('selected', true).trigger('change');
                        $("#cmb9022").prop('disabled', false);
                    } else if (parseInt(ejecPresupFinal.split('%')[0]) > 110){
                        $("#cmb8928 option[value='26199']").prop('selected', true).trigger('change');
                        $("#cmb9022").prop('disabled', false);
                    }
                }else{
                    if (parseInt(ejecPresupFinal.split('%')[0]) < 90){
                        $("#cmb8928 option[value='26197']").prop('selected', true).trigger('change');
                        $("#cmb9022").prop('disabled', false);
                    } else if (parseInt(ejecPresupFinal.split('%')[0]) >= 90 && parseInt(ejecPresupFinal.split('%')[0]) <= 110){
                        $("#cmb8928 option[value='26198']").prop('selected', true).trigger('change');
                    } else if (parseInt(ejecPresupFinal.split('%')[0]) > 110){
                        $("#cmb8928 option[value='26200']").prop('selected', true).trigger('change');
                        $("#cmb9022").prop('disabled', false);
                    }
                }
            });
            //Subejecucion presupuestaria inicial
            $("#cmb8929").prop('disabled', true);            
            let ejecPresupIncial2021 = $("#txt7598").val();
            let ejecPresupIncial2020 = $("#txt7596").val();
            if (ejecPresupIncial != "" && ejecPresupIncial2021 != "" && ejecPresupIncial2020 != ""){
                if (parseInt(ejecPresupIncial.split('%')[0]) < 85 && parseInt(ejecPresupIncial2021.split('%')[0]) < 85 && parseInt(ejecPresupIncial2020.split('%')[0]) < 85){
                    $("#cmb8929 option[value='26206']").prop('selected', true).trigger('change');
                }else{
                    $("#cmb8929 option[value='26207']").prop('selected', true).trigger('change');
                }
            }else{
                $("#cmb8929 option[value='26208']").prop('selected', true).trigger('change');
            }
            //Gasto por beneficiario
            $("#cmb8930").prop('disabled', true);
            $("#cmb9030").prop('disabled', true);
            const valorIPC2023 = 1.035;
            let promedioGB = $("#txt7277").val();
            let totalEjecutado = ($("#txt3069").val() != "" ? retornaValorFormateado($("#txt3069").val()) * valorIPC2023 : 0);
            let totalBenef = retornaValorFormateado($("#txt6974").val());
            let gastoXBenef = (totalEjecutado != "" ? (totalEjecutado / totalBenef) : 0);
            if (promedioGB != ""){
                let variacionGB = ((gastoXBenef / parseFloat(promedioGB.toString().replace(',', '.'))) * 100) - 100;
                if (variacionGB <= 20){
                    $("#cmb8930 option[value='26211']").prop('selected', true).trigger('change');
                } else if (variacionGB > 20){
                    $("#cmb8930 option[value='26213']").prop('selected', true).trigger('change');
                    $("#cmb9030").prop('disabled', false);
                }
            }else{
                if ($("#txt7280").val() != ""){
                    $("#cmb8930 option[value='26216']").prop('selected', true).trigger('change');
                }else if ($("#txt7278").val() != "" || $("#txt7279").val() != "" || $("#txt7277").val() != ""){
                    $("#cmb8930 option[value='26217']").prop('selected', true).trigger('change');
                }
            }
            $("#cmb9030").change(function () {
                if ($(this).val() == "902"){
                    if (promedioGB != ""){
                        let variacionGB = Math.round((gastoXBenef / parseFloat(promedioGB.toString().replace(',', '.'))) * 100) - 100;
                        if (variacionGB > 20){
                            $("#cmb8930 option[value='26212']").prop('selected', true).trigger('change');
                        }
                    }else{
                        if ($("#txt7277").val() != "" || $("#txt7280").val() != ""){
                            $("#cmb8930 option[value='26216']").prop('selected', true).trigger('change');
                        }else if ($("#txt7278").val() != "" || $("#txt7279").val() != ""){
                            $("#cmb8930 option[value='26217']").prop('selected', true).trigger('change');
                        }
                    }
                }else{
                    if (promedioGB != ""){
                        let variacionGB = Math.round((gastoXBenef / parseFloat(promedioGB.toString().replace(',', '.'))) * 100) - 100;
                        if (variacionGB <= 20){
                            $("#cmb8930 option[value='26211']").prop('selected', true).trigger('change');
                        } else if (variacionGB > 20){
                            $("#cmb8930 option[value='26213']").prop('selected', true).trigger('change');
                            $("#cmb9030").prop('disabled', false);
                        }
                    }else{
                        if ($("#txt7277").val() != "" || $("#txt7280").val() != ""){
                            $("#cmb8930 option[value='26216']").prop('selected', true).trigger('change');
                        }else if ($("#txt7278").val() != "" || $("#txt7279").val() != ""){
                            $("#cmb8930 option[value='26217']").prop('selected', true).trigger('change');
                        }
                    }
                }
            });
            //Porcentaje Gasto Administrativo
            $("#cmb8936").prop('disabled', true);
            let porcGastAdmin2021 = $("#txt9017").val();
            let porcGastAdmin2022 = $("#txt9018").val();
            let porcGastAdmin2023 = $("#txt9019").val();
            if ((porcGastAdmin2021 == "" || porcGastAdmin2021 == "0%") && (porcGastAdmin2022 == "" || porcGastAdmin2022 == "0%") && (porcGastAdmin2023 == "" || porcGastAdmin2023 == "0%")){
                $("#cmb8936 option[value='26241']").prop('selected', true).trigger('change');
            }else{
                $("#cmb8936 option[value='26242']").prop('selected', true).trigger('change');
            }
            //Resultados de los indicadores
            $("#Tabcmb89471").prop('disabled', true);
            $("#Tabcmb89472").prop('disabled', true);
            $("#Tabcmb89491").prop('disabled', true);
            $("#Tabcmb89492").prop('disabled', true);
            $("#Tabcmb90311").prop('disabled', true);
            $("#Tabcmb90312").prop('disabled', true);
            let valoresNoPermitidos = ["S/I", "N/A", ""];
            let valoresPersistencia = [""];
            let numerador1 = ($.inArray($("#txt7621").val(),valoresNoPermitidos) === -1 ? parseFloat($("#txt7621").val().toString().replace(',', '.')) : "");
            let numerador2 = ($.inArray($("#txt8279").val(),valoresNoPermitidos) === -1 ? parseFloat($("#txt8279").val().toString().replace(',', '.')) : "");
            let denominador1 = ($.inArray($("#txt7622").val(),valoresNoPermitidos) === -1 ? parseFloat($("#txt7622").val().toString().replace(',', '.')) : "");
            let denominador2 = ($.inArray($("#txt8280").val(),valoresNoPermitidos) === -1 ? parseFloat($("#txt8280").val().toString().replace(',', '.')) : "");
            let sentidoMedicion1 = $("#Tabcmb1211 option:selected").val();
            let sentidoMedicion2 = $("#Tabcmb1212 option:selected").val();
            let periodicidad1 = $("#Tabcmb1191 option:selected").val();
            let periodicidad2 = $("#Tabcmb1192 option:selected").val();
            let efectivo12021 = ($.inArray($("#txt7611").val(),valoresNoPermitidos) === -1 ? parseFloat($("#txt7611").val().toString().replace(',', '.')) : "");
            let efectivo12022 = ($.inArray($("#txt7613").val(),valoresNoPermitidos) === -1 ? parseFloat($("#txt7613").val().toString().replace(',', '.')) : "");
            let efectivo22021 = ($.inArray($("#txt7612").val(),valoresNoPermitidos) === -1 ? parseFloat($("#txt7612").val().toString().replace(',', '.')) : "");
            let efectivo22022 = ($.inArray($("#txt7614").val(),valoresNoPermitidos) === -1 ? parseFloat($("#txt7614").val().toString().replace(',', '.')) : "");
            var resultado12023 = "";
            //Indicador de proposito 1
            $("#Tabcmb89431, #Tabcmb89451").bind("change", function () {
                if ($("#Tabcmb89431").val() == "26247" || $("#Tabcmb89451").val() == "26252"){
                    $("#Tabcmb89471 option[value='26267']").prop('selected', true).trigger('change');
                }else if ($("#Tabcmb89431").val() != "26248" && $("#Tabcmb89451").val() != "26253"){
                    if ($.inArray(numerador1,valoresNoPermitidos) === -1 && $.inArray(denominador1,valoresNoPermitidos) === -1){
                        resultado12023 = Math.round((numerador1 / denominador1) * 100) / 100;
                        if (periodicidad1 == 1021){
                            if ($.inArray(efectivo12022,valoresNoPermitidos) === -1){
                                if (sentidoMedicion1 == 1036){
                                    if (resultado12023 > efectivo12022){
                                        $("#Tabcmb89471 option[value='26256']").prop('selected', true).trigger('change');
                                    }else if (resultado12023 < efectivo12022){
                                        $("#Tabcmb89471 option[value='26258']").prop('selected', true).trigger('change');
                                        $("#Tabcmb90311").prop('disabled', false);
                                    }else if (resultado12023 == efectivo12022){
                                        $("#Tabcmb89471 option[value='26262']").prop('selected', true).trigger('change');
                                    }
                                }else if (sentidoMedicion1 == 1037){
                                    if (resultado12023 > efectivo12022){
                                        $("#Tabcmb89471 option[value='26260']").prop('selected', true).trigger('change');
                                        $("#Tabcmb90311").prop('disabled', false);
                                    }else if (resultado12023 < efectivo12022){
                                        $("#Tabcmb89471 option[value='26261']").prop('selected', true).trigger('change');                                        
                                    }else if (resultado12023 == efectivo12022){
                                        $("#Tabcmb89471 option[value='26262']").prop('selected', true).trigger('change');
                                    }
                                }
                            }else {
                                if (efectivo12022 != "N/A"){
                                    $("#Tabcmb89471 option[value='26266']").prop('selected', true).trigger('change');
                                }else{
                                    $("#Tabcmb89471 option[value='26264']").prop('selected', true).trigger('change');
                                }
                            }
                        }
                    }else{
                        if (numerador1 != "N/A"){
                            $("#Tabcmb89471 option[value='26263']").prop('selected', true).trigger('change');
                        }else{
                            $("#Tabcmb89471 option[value='26264']").prop('selected', true).trigger('change');
                        }
                    }
                }else{
                    $("#Tabcmb89471 option[value='26268']").prop('selected', true).trigger('change');
                }
            });
            $("#Tabcmb90311").change(function () {
                if ($(this).val() == "902"){
                    if ($("#Tabcmb89471 option:selected").val() == "26258"){
                        $("#Tabcmb89471 option[value='26257']").prop('selected', true).trigger('change');
                    }else if ($("#Tabcmb89471 option:selected").val() == "26260"){
                        $("#Tabcmb89471 option[value='26259']").prop('selected', true).trigger('change');
                    }
                }else{
                    if ($("#Tabcmb89471 option:selected").val() == "26257"){
                        $("#Tabcmb89471 option[value='26258']").prop('selected', true).trigger('change');
                    }else if ($("#Tabcmb89471 option:selected").val() == "26259"){
                        $("#Tabcmb89471 option[value='26260']").prop('selected', true).trigger('change');
                    }
                }
            });
            //Persistencia
            if ($("#txt7611").val() == "" && $("#txt7613").val() == "" && resultado12023 == ""){
                $("#Tabcmb89491 option[value='26271']").prop('selected', true).trigger('change');
            }else{
                $("#Tabcmb89491 option[value='26272']").prop('selected', true).trigger('change');
            }
            //Indicador de proposito 2
            var resultado22023 = "";
            $("#Tabcmb89432, #Tabcmb89452").bind("change", function () {
                if ($("#Tabcmb89432").val() == "26247" || $("#Tabcmb89452").val() == "26252"){
                    $("#Tabcmb89472 option[value='26267']").prop('selected', true).trigger('change');
                }else if ($("#Tabcmb89432").val() != "26248" && $("#Tabcmb89452").val() != "26253"){
                    if ($.inArray(numerador2,valoresNoPermitidos) === -1 && $.inArray(denominador2,valoresNoPermitidos) === -1){
                        resultado22023 = Math.round((numerador2 / denominador2) * 100) / 100;
                        if (periodicidad2 == 1021){
                            if ($.inArray(efectivo22022,valoresNoPermitidos) === -1){
                                if (sentidoMedicion2 == 1036){
                                    if (resultado22023 > efectivo22022){
                                        $("#Tabcmb89472 option[value='26256']").prop('selected', true).trigger('change');                                
                                    }else if (resultado22023 < efectivo22022){
                                        $("#Tabcmb89472 option[value='26258']").prop('selected', true).trigger('change');
                                        $("#Tabcmb90312").prop('disabled', false);
                                    }else if (resultado22023 == efectivo22022){
                                        $("#Tabcmb89472 option[value='26262']").prop('selected', true).trigger('change');
                                    }
                                }else if (sentidoMedicion2 == 1037){
                                    if (resultado22023 > efectivo22022){
                                        $("#Tabcmb89472 option[value='26260']").prop('selected', true).trigger('change');
                                        $("#Tabcmb90312").prop('disabled', false);
                                    }else if (resultado22023 < efectivo22022){
                                        $("#Tabcmb89472 option[value='26261']").prop('selected', true).trigger('change');                                        
                                    }else if (resultado22023 == efectivo22022){
                                        $("#Tabcmb89472 option[value='26262']").prop('selected', true).trigger('change');
                                    }
                                }
                            }else {
                                if (efectivo22022 != "N/A"){
                                    $("#Tabcmb89472 option[value='26266']").prop('selected', true).trigger('change');
                                }else{
                                    $("#Tabcmb89472 option[value='26264']").prop('selected', true).trigger('change');
                                }
                            }
                        }
                    }else{
                        if (numerador2 != "N/A"){
                            $("#Tabcmb89472 option[value='26263']").prop('selected', true).trigger('change');
                        }else{
                            $("#Tabcmb89472 option[value='26264']").prop('selected', true).trigger('change');
                        }
                    }
                }else{
                    $("#Tabcmb89472 option[value='26268']").prop('selected', true).trigger('change');
                }
            });                        
            $("#Tabcmb90312").change(function () {
                if ($(this).val() == "902"){
                    if ($("#Tabcmb89472 option:selected").val() == "26258"){
                        $("#Tabcmb89472 option[value='26257']").prop('selected', true).trigger('change');
                    }else if ($("#Tabcmb89472 option:selected").val() == "26260"){
                        $("#Tabcmb89472 option[value='26259']").prop('selected', true).trigger('change');
                    }
                }else{
                    if ($("#Tabcmb89472 option:selected").val() == "26257"){
                        $("#Tabcmb89472 option[value='26258']").prop('selected', true).trigger('change');
                    }else if ($("#Tabcmb89472 option:selected").val() == "26259"){
                        $("#Tabcmb89472 option[value='26260']").prop('selected', true).trigger('change');
                    }
                }
            });
            //Persistencia
            if ($("#txt7612").val() == "" && $("#txt7614").val() == "" && resultado22023 == ""){
                $("#Tabcmb89492 option[value='26271']").prop('selected', true).trigger('change');
            }else{                
                $("#Tabcmb89492 option[value='26272']").prop('selected', true).trigger('change');
            }
            //Indicadores complementarios            
            var resultado2023 = "";
            var numerador = ["txt7629", "txt8285", "txt8286", "txt8287", "txt8288", "txt8289", "txt8290", "txt8291", "txt8292", "txt8293", "txt8294", "txt8295", "txt8296", "txt8297", "txt8298"];
            var denominador = ["txt7630", "txt8299", "txt8300", "txt8301", "txt8302", "txt8303", "txt8304", "txt8305", "txt8306", "txt8307" , "txt8308", "txt8309", "txt8310", "txt8311", "txt8312"];
            var efectivo2022 = ["txt7648", "txt7649", "txt7650", "txt7651", "txt7652", "txt7653", "txt7654", "txt7655", "txt7656", "txt7657" , "txt7658", "txt7659", "txt7660", "txt7661", "txt7662"];
            for (var x = 1; x <= $("#cmb3036 option:selected").val(); x++){                
                $("#Tabcmb8959" + x).prop('disabled', true);
                $("#Tabcmb9032" + x).prop('disabled', true);
                $("#Tabcmb8955" + x + ", #Tabcmb8957" + x).bind("change", function () {
                    var tab = this.id.substring(6, this.id.length);
                    var numTab = parseInt($("#numTab" + tab).val());
                    resultado2023 = "";
                    var numeradorComp = ($.inArray($("#" + numerador[numTab-1]).val(),valoresNoPermitidos) === -1 ? parseFloat($("#" + numerador[numTab-1]).val().toString().replace(',', '.')) : "");
                    var denominadorComp = ($.inArray($("#" + denominador[numTab-1]).val(),valoresNoPermitidos) === -1 ? parseFloat($("#" + denominador[numTab-1]).val().toString().replace(',', '.')) : "");
                    if ($("#Tabcmb8955" + numTab + " option:selected").val() == "26277" || $("#Tabcmb8957" + numTab + " option:selected").val() == "26282"){
                        $("#Tabcmb8959" + numTab + " option[value='26297']").prop('selected', true).trigger('change');
                    }else if ($("#Tabcmb8955" + numTab + " option:selected").val() != "26278" && $("#Tabcmb8957" + numTab + " option:selected").val() != "26283"){
                        if ($.inArray(numeradorComp,valoresNoPermitidos) === -1 && $.inArray(denominadorComp,valoresNoPermitidos) === -1){
                            resultado2023 = Math.round((numeradorComp / denominadorComp) * 100) / 100;
                            if ($("#Tabcmb3046" + numTab + " option:selected").val() == 1021){
                                if ($.inArray($("#" + efectivo2022[numTab-1]).val(),valoresNoPermitidos) === -1){
                                    if ($("#Tabcmb3048" + numTab + " option:selected").val() == 1036){
                                        if (resultado2023 > parseFloat($("#" + efectivo2022[numTab-1]).val().toString().replace(',', '.'))){
                                            $("#Tabcmb8959" + numTab + " option[value='26286']").prop('selected', true).trigger('change');
                                        }else if (resultado2023 < parseFloat($("#" + efectivo2022[numTab-1]).val().toString().replace(',', '.'))){
                                            $("#Tabcmb8959" + numTab + " option[value='26288']").prop('selected', true).trigger('change');
                                            $("#Tabcmb9032" + numTab).prop('disabled', false);
                                        }else if (resultado2023 == parseFloat($("#" + efectivo2022[numTab-1]).val().toString().replace(',', '.'))){
                                            $("#Tabcmb8959" + numTab + " option[value='26292']").prop('selected', true).trigger('change');
                                        }
                                    }else if ($("#Tabcmb3048" + numTab + " option:selected").val() == 1037){
                                        if (resultado2023 > parseFloat($("#" + efectivo2022[numTab-1]).val().toString().replace(',', '.'))){
                                            $("#Tabcmb8959" + numTab + " option[value='26290']").prop('selected', true).trigger('change');
                                            $("#Tabcmb9032" + numTab).prop('disabled', false);
                                        }else if (resultado2023 < parseFloat($("#" + efectivo2022[numTab-1]).val().toString().replace(',', '.'))){
                                            $("#Tabcmb8959" + numTab + " option[value='26291']").prop('selected', true).trigger('change');                                            
                                        }else if (resultado2023 == parseFloat($("#" + efectivo2022[numTab-1]).val().toString().replace(',', '.'))){
                                            $("#Tabcmb8959" + numTab + " option[value='26292']").prop('selected', true).trigger('change');
                                        }
                                    }
                                }else {
                                    if ($("#" + efectivo2022[numTab-1]).val() != "N/A"){
                                        $("#Tabcmb8959" + numTab + " option[value='26296']").prop('selected', true).trigger('change');
                                    }else{
                                        $("#Tabcmb8959" + numTab + " option[value='26294']").prop('selected', true).trigger('change');
                                    }
                                }
                            }
                        }else{
                            if (numeradorComp != "N/A"){
                                $("#Tabcmb8959" + numTab + " option[value='26293']").prop('selected', true).trigger('change');
                            }else{
                                $("#Tabcmb8959" + numTab + " option[value='26294']").prop('selected', true).trigger('change');
                            }
                        }
                    }else{
                        $("#Tabcmb8959" + numTab + " option[value='26298']").prop('selected', true).trigger('change');
                    }
                });
                $("#Tabcmb9032" + x).change(function () {
                    var tab = this.id.substring(6, this.id.length);
                    var numTab = $("#numTab" + tab).val();
                    if ($("#Tabcmb9032" + numTab + " option:selected").val() == "902") {
                        if ($("#Tabcmb8959" + numTab + " option:selected").val() == "26288"){
                            $("#Tabcmb8959" + numTab + " option[value='26287']").prop('selected', true).trigger('change');
                        }else if ($("#Tabcmb8959" + numTab + " option:selected").val() == "26290"){
                            $("#Tabcmb8959" + numTab + " option[value='26289']").prop('selected', true).trigger('change');
                        }
                    }else{
                        if ($("#Tabcmb8959" + numTab + " option:selected").val() == "26287"){
                            $("#Tabcmb8959" + numTab + " option[value='26288']").prop('selected', true).trigger('change');
                        }else if ($("#Tabcmb8959" + numTab + " option:selected").val() == "26289"){
                            $("#Tabcmb8959" + numTab + " option[value='26290']").prop('selected', true).trigger('change');
                        }
                    }
                });
            }
        }else if (item.IdEvento == funcCalculoDivisionTabs) {
            if (item.CategoriaPregunta == tipoPregNumerico || item.TipoPregunta == tipoPregCheckInline) {
                idObjeto = "Tabtxt" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregNumerico || item.TipoPregunta == tipoPregCheckInline) {
                idObjetoDep = "Tabtxt" + item.IdPreguntaDependiente;
            }
            if (item.ValorPregunta != "" && item.ValorPregunta != null) {
                for (var i = 1; i <= parseInt(item.ValorPregunta) ; i++) {
                    $("#" + idObjeto + i.toString()).keyup(function () {
                        if (item.Datos.length > 0) {
                            $.each(item.Datos, function (i, d) {
                                if (d.Valor != "" && d.Valor != null) {
                                    var blancos = false;
                                    var total = 0;
                                    var valor1 = 0;
                                    var valor2 = 0;
                                    if ($.trim($("#" + idObjeto + i.toString()).val()) != "") {
                                        blancos = true;
                                        valor1 = retornaValorFormateado($("#" + idObjeto + i.toString()).val());
                                    }
                                    if ($.trim($("#" + idObjetoDep + i.toString()).val()) != "") {
                                        blancos = true;
                                        valor2 = retornaValorFormateado($("#" + idObjetoDep + i.toString()).val());
                                    }
                                    if (blancos) {
                                        if (valor2 > 0) {
                                            total = Math.round((valor1 / valor2) * 100) / 100;
                                            $("#txt" + d.Valor).val(total.toString().replace('.', ','));
                                        }
                                    } else {
                                        $("#txt" + d.Valor).val('');
                                    }
                                }
                            });
                        }
                    });
                }
            }
            if (item.ValorPregunta != "" && item.ValorPregunta != null) {
                for (var i = 1; i <= parseInt(item.ValorPregunta) ; i++) {
                    $("#" + idObjetoDep + i.toString()).keyup(function () {
                        if (item.Datos.length > 0) {
                            $.each(item.Datos, function (i, d) {
                                if (d.Valor != "" && d.Valor != null) {
                                    var blancos = false;
                                    var total = 0;
                                    var valor1 = 0;
                                    var valor2 = 0;
                                    if ($.trim($("#" + idObjeto + i.toString()).val()) != "") {
                                        blancos = true;
                                        valor1 = retornaValorFormateado($("#" + idObjeto + i.toString()).val());
                                    }
                                    if ($.trim($("#" + idObjetoDep + i.toString()).val()) != "") {
                                        blancos = true;
                                        valor2 = retornaValorFormateado($("#" + idObjetoDep + i.toString()).val());
                                    }
                                    if (blancos) {
                                        if (valor2 > 0) {
                                            total = Math.round((valor1 / valor2) * 100) / 100;
                                            $("#txt" + d.Valor).val(total.toString().replace('.', ','));
                                        }
                                    } else {
                                        $("#txt" + d.Valor).val('');
                                    }
                                }
                            });                            
                        }
                    });
                }
            }            
        } else if (item.IdEvento == funcMuestOcultaVarRespTabs){
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            var valorObj = $("#" + idObjeto + " option:selected").val();            
            if (item.Datos.length > 0) {
                $.each(item.Datos, function (ii, d) {
                    if (d.Valor != null && d.Valor != "") {
                        if (valorObj != d.Valor2.toString()){
                            $("#divPregTab" + d.Valor.toString()).hide();
                        }else{
                            $("#divPregTab" + d.Valor.toString()).show();
                        }
                    }
                });
            }
            $("#" + idObjeto).change(function () {
                valorObj = $(this).val();
                if (item.Datos.length > 0) {
                    $.each(item.Datos, function (ii, d) {
                        if (d.Valor != null && d.Valor != "") {
                            if (valorObj != d.Valor2.toString()){
                                $("#divPregTab" + d.Valor.toString()).hide();
                            }else{
                                $("#divPregTab" + d.Valor.toString()).show();
                            }                      
                        }
                    });
                }
            });
        }else if (item.IdEvento == funcLlenaCmbTabs){
            if (item.TipoPregunta == tipoPregTexto) {
                idObjeto = "Tabtxt" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregCombo) {
                idObjetoDep = "Tabcmb" + item.IdPreguntaDependiente;
            }
            if (item.ValorPregunta != "" && item.ValorPregunta != null) {                                
                for (var i = 1; i <= parseInt(item.ValorPregunta) ; i++) {
                    $("#" + idObjetoDep + i.toString()).empty();
                    $("#" + idObjetoDep + i.toString()).append("<option value='-1'>Seleccione</option>");
                    if ($.trim($("#" + idObjeto + i.toString()).val()) != "" && $.trim($("#" + idObjeto + i.toString()).val()) != null){
                        for (var z = 1; z <= parseInt(item.ValorPregunta) ; z++) {
                            if ($("#" + idObjetoDep + z.toString() + " option[value='" + $.trim($("#" + idObjeto + i.toString()).val()) + "']").length == 0){
                                $("#" + idObjetoDep + z.toString()).append("<option value='" + $.trim($("#" + idObjeto + i.toString()).val()) + "'>" + $.trim($("#" + idObjeto + i.toString()).val()) + "</option>");
                            }
                        }
                    }
                    $("#" + idObjeto + i.toString()).bind("change", function () {
                        var tab = this.id.substring(6, this.id.length);
                        var numTab = $("#numTab" + tab).val();
                        if ($.trim($("#" + idObjeto + numTab).val()) != "" && $.trim($("#" + idObjeto + numTab).val()) != null){
                            for (var z = 1; z <= parseInt(item.ValorPregunta) ; z++) {
                                if ($("#" + idObjetoDep + z.toString() + " option[value='" + $.trim($("#" + idObjeto + numTab).val()) + "']").length == 0){
                                    $("#" + idObjetoDep + z.toString()).append("<option value='" + $.trim($("#" + idObjeto + numTab).val()) + "'>" + $.trim($("#" + idObjeto + numTab).val()) + "</option>");
                                }
                            }
                        }
                    });
                }
            }
        } else if (item.IdEvento == funcOcultaSegunResp){
            if (item.CategoriaPregunta == tipoPregCombo) {
                idObjeto = "cmb" + item.IdPregunta;
            }
            if (item.TipoPreguntaDependiente == tipoPregTexto || item.TipoPreguntaDependiente == tipoPregTxtGrande) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            var valorObj = $("#" + idObjeto + " option:selected").val();
            if (item.Datos.length > 0) {
                $.each(item.Datos, function (ii, d) {                        
                    if (d.Valor != null && d.Valor != "") {
                        if (valorObj == d.Valor.toString()) {
                            $("#divPreg" + item.IdPreguntaDependiente).hide();
                        } else {
                            $("#divPreg" + item.IdPreguntaDependiente).show();
                        }
                    }
                });
            }
            $("#" + idObjeto).change(function () {
                valorObj = $(this).val();
                if (item.Datos.length > 0) {
                    $.each(item.Datos, function (ii, d) {
                        if (d.Valor != null && d.Valor != "") {
                            if (valorObj == d.Valor.toString()) {
                                $("#divPreg" + item.IdPreguntaDependiente).hide();
                            } else {
                                $("#divPreg" + item.IdPreguntaDependiente).show();
                            }
                        }
                    });
                }
            });
        } else if (item.IdEvento == funcDivisionesTasasPorcentaje) {
            if (item.CategoriaPregunta == tipoPregNumerico || item.TipoPregunta == tipoPregTotal || item.TipoPregunta == tipoPregCheckInline) {
                idObjeto = "txt" + item.IdPregunta;
            }
            if (item.CategoriaPreguntaDependiente == tipoPregNumerico || item.TipoPreguntaDependiente == tipoPregTotal) {
                idObjetoDep = "txt" + item.IdPreguntaDependiente;
            }
            if (item.ValorPregunta != "" && item.ValorPregunta != null) {
                for (var y = 1; y <= parseInt(item.ValorPregunta) ; y++) {
                    if (item.Datos.length > 0) {
                        $.each(item.Datos, function (i, d) {
                            if (d.Valor != null && d.Valor != "") {
                                var blancos = false;
                                var total = 0;
                                var valor1 = 0;
                                var valor2 = 0;
                                if ($.trim($("#Tab" + idObjeto + y).val()) != "") {
                                    blancos = true;
                                    valor1 = retornaValorFormateado($("#Tab" + idObjeto + y).val());
                                }
                                if ($.trim($("#Tab" + idObjetoDep + y).val()) != "") {
                                    blancos = true;
                                    valor2 = retornaValorFormateado($("#Tab" + idObjetoDep + y).val());
                                }
                                if (blancos) {
                                    if (valor2 > 0) {
                                        total = (Math.round((valor1 / valor2) * 100));
                                        $("#Tabtxt" + d.Valor + y).val(total.toString().replace('.', ',') + "%");
                                    }
                                } else {
                                    $("#Tabtxt" + d.Valor + y).val('');
                                }
                            }
                        });
                    }
                    $("#Tab" + idObjeto + y).bind("keyup change", function () {
                        var nTab = $(this).attr('class').split(/\s+/)[2].split('-')[1];
                        if (item.Datos.length > 0) {
                            $.each(item.Datos, function (i, d) {
                                if (d.Valor != null && d.Valor != "") {
                                    var blancos = false;
                                    var total = 0;
                                    var valor1 = 0;
                                    var valor2 = 0;
                                    if ($.trim($("#Tab" + idObjeto + nTab).val()) != "") {
                                        blancos = true;
                                        valor1 = retornaValorFormateado($("#Tab" + idObjeto + nTab).val());
                                    }
                                    if ($.trim($("#Tab" + idObjetoDep + nTab).val()) != "") {
                                        blancos = true;
                                        valor2 = retornaValorFormateado($("#Tab" + idObjetoDep + nTab).val());
                                    }
                                    if (blancos) {
                                        if (valor2 > 0) {
                                            total = (Math.round((valor1 / valor2) * 100));
                                            $("#Tabtxt" + d.Valor + nTab).val(total.toString().replace('.', ',') + "%");
                                        }
                                    } else {
                                        $("#Tabtxt" + d.Valor + nTab).val('');
                                    }
                                }
                            });
                        }
                    });
                    $("#Tab" + idObjetoDep + y).bind("keyup change", function () {
                        var nTab = $(this).attr('class').split(/\s+/)[2].split('-')[1];
                        if (item.Datos.length > 0) {
                            $.each(item.Datos, function (i, d) {
                                if (d.Valor != null && d.Valor != "") {
                                    var blancos = false;
                                    var total = 0;
                                    var valor1 = 0;
                                    var valor2 = 0;
                                    if ($.trim($("#Tab" + idObjeto + nTab).val()) != "") {
                                        blancos = true;
                                        valor1 = retornaValorFormateado($("#Tab" + idObjeto + nTab).val());
                                    }
                                    if ($.trim($("#Tab" + idObjetoDep + nTab).val()) != "") {
                                        blancos = true;
                                        valor2 = retornaValorFormateado($("#Tab" + idObjetoDep + nTab).val());
                                    }
                                    if (blancos) {
                                        if (valor2 > 0) {
                                            total = (Math.round((valor1 / valor2) * 100));
                                            $("#Tabtxt" + d.Valor + nTab).val(total.toString().replace('.', ',') + "%");
                                        }
                                    } else {
                                        $("#Tabtxt" + d.Valor + nTab).val('');
                                    }
                                }
                            });
                        }
                    });
                }
            }else{
                if (item.Datos.length > 0) {
                    $.each(item.Datos, function (i, d) {
                        if (d.Valor != null && d.Valor != "") {
                            var blancos = false;
                            var total = 0;
                            var valor1 = 0;
                            var valor2 = 0;
                            if ($.trim($("#" + idObjeto).val()) != "") {
                                blancos = true;
                                valor1 = retornaValorFormateado($("#" + idObjeto).val());
                            }
                            if ($.trim($("#" + idObjetoDep).val()) != "") {
                                blancos = true;
                                valor2 = retornaValorFormateado($("#" + idObjetoDep).val());
                            }
                            if (blancos) {
                                if (valor2 > 0) {
                                    total = (Math.round((valor1 / valor2) * 100));
                                    $("#txt" + d.Valor).val(total.toString().replace('.', ',') + "%");
                                }
                            } else {
                                $("#txt" + d.Valor).val('');
                            }
                        }
                    });
                }
                $("#" + idObjeto).bind("keyup change", function () {
                    if (item.Datos.length > 0) {
                        $.each(item.Datos, function (i, d) {
                            if (d.Valor != null && d.Valor != "") {
                                var blancos = false;
                                var total = 0;
                                var valor1 = 0;
                                var valor2 = 0;
                                if ($.trim($("#" + idObjeto).val()) != "") {
                                    blancos = true;
                                    valor1 = retornaValorFormateado($("#" + idObjeto).val());
                                }
                                if ($.trim($("#" + idObjetoDep).val()) != "") {
                                    blancos = true;
                                    valor2 = retornaValorFormateado($("#" + idObjetoDep).val());
                                }
                                if (blancos) {
                                    if (valor2 > 0) {
                                        total = (Math.round((valor1 / valor2) * 100));
                                        $("#txt" + d.Valor).val(total.toString().replace('.', ',') + "%");
                                    }
                                } else {
                                    $("#txt" + d.Valor).val('');
                                }
                            }
                        });
                    }
                });
                $("#" + idObjetoDep).bind("keyup change", function () {
                    if (item.Datos.length > 0) {
                        $.each(item.Datos, function (i, d) {
                            if (d.Valor != null && d.Valor != "") {
                                var blancos = false;
                                var total = 0;
                                var valor1 = 0;
                                var valor2 = 0;
                                if ($.trim($("#" + idObjeto).val()) != "") {
                                    blancos = true;
                                    valor1 = retornaValorFormateado($("#" + idObjeto).val());
                                }
                                if ($.trim($("#" + idObjetoDep).val()) != "") {
                                    blancos = true;
                                    valor2 = retornaValorFormateado($("#" + idObjetoDep).val());
                                }
                                if (blancos) {
                                    if (valor2 > 0) {
                                        total = (Math.round((valor1 / valor2) * 100));
                                        $("#txt" + d.Valor).val(total.toString().replace('.', ',') + "%");
                                    }
                                } else {
                                    $("#txt" + d.Valor).val('');
                                }
                            }
                        });
                    }
                });
            }
        }
    });
}