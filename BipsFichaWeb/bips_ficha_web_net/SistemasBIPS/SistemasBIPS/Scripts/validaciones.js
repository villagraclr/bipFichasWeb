function validaBlancos(inicio, menuPadre, menuHijo) {
    var countNulos = 0;
    var menuActual = 0;
    var menuBlancos = [];    
    $.each(camposValidar, function (i, item) {
        console.log(camposValidar);
        var excepiones = true;
        if (item.tipo == "texto" || item.tipo == "textoGrande" || item.tipo == "numerico") {
            if (item.idTab == "") {
                if ($.trim($("#" + item.idObjeto).val()) == "") {
                    var added = false;
                    if (menuBlancos.length > 0) {
                        $.map(menuBlancos, function (elementOfArray, indexInArray) {
                            if (elementOfArray.menuPadre == item.menuPadre && elementOfArray.menuHijo == item.menuHijo) { added = true; }
                        });
                    }
                    if (!added) {
                        var mph = {};
                        mph.menuPadre = item.menuPadre;
                        mph.menuHijo = item.menuHijo;
                        menuBlancos.push(mph);
                    }
                    countNulos++;
                    console.log("idPregunta:" + item.idPregunta + ";menuPadre:" + item.menuPadre + ";menuHijo:" + item.menuHijo);
                }
            }
        } else if (item.tipo == "combo") {
            if (item.idTab == "") {
                if ($("#" + item.idObjeto + " option:selected").val() == "-1") {
                    //Valido preguntas dependientes
                    //Año de inicio
                    if (item.idPregunta == 1571 && $("#cbx18").is(":checked")) {
                        excepiones = false;
                    }
                    if (excepiones) {
                        var added = false;
                        if (menuBlancos.length > 0) {
                            $.map(menuBlancos, function (elementOfArray, indexInArray) {
                                if (elementOfArray.menuPadre == item.menuPadre && elementOfArray.menuHijo == item.menuHijo) { added = true; }
                            });
                        }
                        if (!added) {
                            var mph = {};
                            mph.menuPadre = item.menuPadre;
                            mph.menuHijo = item.menuHijo;
                            menuBlancos.push(mph);
                        }
                        countNulos++;
                        console.log("idPregunta:" + item.idPregunta + ";menuPadre:" + item.menuPadre + ";menuHijo:" + item.menuHijo);
                    }
                }
            }
        }
    });
    //Valido preguntas dependientes
    //Subsistemas
    if ($("#cmb9705 option:selected").val() == "902") {
        var valida = 0;
        if ($("#cmb7740 option:selected").val() == "-1") {
            valida = 1;
        } else if ($("#cmb7740 option:selected").val() == "23448" && $.trim($("#txt2402").val()) == "") {
            valida = 1;
        }
        if (valida) {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 944 && elementOfArray.menuHijo == 945) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 944;
                mph.menuHijo = 945;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("Otro - Subsistema");
        }
    }
    //Causas
    /*for (var i = 1; i <= $("#cmb9083 option:selected").val() ; i++) {
        if ($.trim($("#Tabtxt9085" + i.toString()).val()) == "" || $.trim($("#txtTab9086" + i.toString()).val()) == "" || $.trim($("#txtTab9087" + i.toString()).val()) == "") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 879 && elementOfArray.menuHijo == 880) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 879;
                mph.menuHijo = 880;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("Causas");
        }
    }*/
    //Evaluaciones Ex Ante
    /*if ($("#cmb7538 option:selected").val() == "902") {
        if ($("#cmb3006 option:selected").val() == "-1" || $("#cmb3007 option:selected").val() == "-1") {            
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 803 && elementOfArray.menuHijo == 811) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 803;
                mph.menuHijo = 811;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("Evaluaciones Ex Ante");
        }
    }*/
    //Evaluaciones Ex Post
    /*if ($("#cmb7539 option:selected").val() == "902") {       
        for (var i = 1; i <= $("#cmb7540 option:selected").val() ; i++) {
            if ($("#Tabcmb7541" + i.toString() + " option:selected").val() == "-1" || $("#Tabcmb7542" + i.toString() + " option:selected").val() == "-1" || $.trim($("#Tabtxt7543" + i.toString()).val()) == "" || $.trim($("#Tabtxt7544" + i.toString()).val()) == "") {
                var added = false;
                if (menuBlancos.length > 0) {
                    $.map(menuBlancos, function (elementOfArray, indexInArray) {
                        if (elementOfArray.menuPadre == 803 && elementOfArray.menuHijo == 811) { added = true; }
                    });
                }
                if (!added) {
                    var mph = {};
                    mph.menuPadre = 803;
                    mph.menuHijo = 811;
                    menuBlancos.push(mph);
                }
                countNulos++;
                console.log("Evaluaciones Ex Post");
            }
        }       
    }*/
    //Evaluaciones Externas
    if ($("#cmb50 option:selected").val() == "902") {
        for (var i = 1; i <= $("#cmb22 option:selected").val() ; i++) {
            if ($("#Tabcmb25" + i.toString() + " option:selected").val() == "-1" || $("#Tabcmb7741" + i.toString() + " option:selected").val() == "-1" || $.trim($("#Tabtxt23" + i.toString()).val()) == "" || $.trim($("#Tabtxt24" + i.toString()).val()) == "" || $.trim($("#Tabtxt1594" + i.toString()).val()) == "") {
                var added = false;
                if (menuBlancos.length > 0) {
                    $.map(menuBlancos, function (elementOfArray, indexInArray) {
                        if (elementOfArray.menuPadre == 944 && elementOfArray.menuHijo == 946) { added = true; }
                    });
                }
                if (!added) {
                    var mph = {};
                    mph.menuPadre = 944;
                    mph.menuHijo = 946;
                    menuBlancos.push(mph);
                }
                countNulos++;
                console.log("Evaluaciones Externas");
            }
        }
    } else if ($("#cmb50 option:selected").val() == "-1") {
        var added = false;
        if (menuBlancos.length > 0) {
            $.map(menuBlancos, function (elementOfArray, indexInArray) {
                if (elementOfArray.menuPadre == 944 && elementOfArray.menuHijo == 946) { added = true; }
            });
        }
        if (!added) {
            var mph = {};
            mph.menuPadre = 944;
            mph.menuHijo = 946;
            menuBlancos.push(mph);
        }
        countNulos++;
        console.log("Evaluaciones Externas (Seleccione)");
    }
    //Poblacion objetivo
    /*if ($("#cmb55 option:selected").val() == "901" && ($.trim($("#txt3009").val()) == "" || $.trim($("#txt7413").val()) == "" || $.trim($("#txt7547").val()) == "")) {
        var added = false;
        if (menuBlancos.length > 0) {
            $.map(menuBlancos, function (elementOfArray, indexInArray) {
                if (elementOfArray.menuPadre == 949 && elementOfArray.menuHijo == 951) { added = true; }
            });
        }
        if (!added) {
            var mph = {};
            mph.menuPadre = 949;
            mph.menuHijo = 951;
            menuBlancos.push(mph);
        }
        countNulos++;
        console.log("población objetivo");
    }*/
    //Cupos población beneficiada
    if ($("#cmb7861 option:selected").val() == "902" && $.trim($("#txt9706").val()) == "" && $.trim($("#txt9670").val()) == "") {
        var added = false;
        if (menuBlancos.length > 0) {
            $.map(menuBlancos, function (elementOfArray, indexInArray) {
                if (elementOfArray.menuPadre == 949 && elementOfArray.menuHijo == 952) { added = true; }
            });
        }
        if (!added) {
            var mph = {};
            mph.menuPadre = 949;
            mph.menuHijo = 952;
            menuBlancos.push(mph);
        }
        countNulos++;
        console.log("cupos");
    }
    //RSH    
    if ($("#cmb1177 option:selected").val() == "902") {
        if ($("#cmb7554 option:selected").val() == "-1") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 949 && elementOfArray.menuHijo == 952) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 949;
                mph.menuHijo = 952;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("quien utiliza rsh");
        }
        if ($("#cmb7735 option:selected").val() == "-1") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 949 && elementOfArray.menuHijo == 952) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 949;
                mph.menuHijo = 952;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("rsh");
        }
        if ($("#cmb7735 option:selected").val() == "23402" && $("#cmb7736 option:selected").val() == "-1") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 949 && elementOfArray.menuHijo == 952) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 949;
                mph.menuHijo = 952;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("pertenencia CSE");
        }
        if ($("#cmb7735 option:selected").val() == "23403") {
            let otrasVariablesRSH = false;
            if ($("#cmb7738 option:selected").val() == "-1") {
                otrasVariablesRSH = true;
            } else if ($("#cmb7738 option:selected").val() == "23425" && $.trim($("#txt7743").val()) == "") {
                otrasVariablesRSH = true;
            }
            if (otrasVariablesRSH) {
                var added = false;
                if (menuBlancos.length > 0) {
                    $.map(menuBlancos, function (elementOfArray, indexInArray) {
                        if (elementOfArray.menuPadre == 949 && elementOfArray.menuHijo == 952) { added = true; }
                    });
                }
                if (!added) {
                    var mph = {};
                    mph.menuPadre = 949;
                    mph.menuHijo = 952;
                    menuBlancos.push(mph);
                }
                countNulos++;
                console.log("uso de variables del RSH");
            }
        }
        if ($("#cmb7735 option:selected").val() == "23404") {
            let otrasVariablesRSH = false;
            if ($("#cmb8803 option:selected").val() == "-1") {
                otrasVariablesRSH = true;
            } else if ($("#cmb8803 option:selected").val() == "25285" && $.trim($("#txt8804").val()) == "") {
                otrasVariablesRSH = true;
            }
            if (otrasVariablesRSH) {
                var added = false;
                if (menuBlancos.length > 0) {
                    $.map(menuBlancos, function (elementOfArray, indexInArray) {
                        if (elementOfArray.menuPadre == 949 && elementOfArray.menuHijo == 952) { added = true; }
                    });
                }
                if (!added) {
                    var mph = {};
                    mph.menuPadre = 949;
                    mph.menuHijo = 952;
                    menuBlancos.push(mph);
                }
                countNulos++;
                console.log("uso del RSH construccion variables");
            }
        }        
    }
    if ($("#cmb7739 option:selected").val() == "902") {
        if ($.trim($("#txt7269").val()) == "") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 949 && elementOfArray.menuHijo == 952) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 949;
                mph.menuHijo = 952;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("otro instrumento RSH");
        }
    }
    //Total territorial
    if ($("#cmb7210 option:selected").val() == "902") {
        if ($.trim($("#txt7221").val()) == "") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 949 && elementOfArray.menuHijo == 952) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 949;
                mph.menuHijo = 952;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("total territorial (beneficiarios)");
        }
    }
    //Total por genero
    if ($("#cmb33 option:selected").val() == "851") {
        if ($.trim($("#txt6829").val()) == "") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 949 && elementOfArray.menuHijo == 952) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 949;
                mph.menuHijo = 952;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("total genero (beneficiarios)");
        }
    }
    //Total por empresa
    if ($("#cmb33 option:selected").val() == "866") {
        if ($.trim($("#txt6830").val()) == "") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 949 && elementOfArray.menuHijo == 952) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 949;
                mph.menuHijo = 952;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("total empresa (beneficiarios)");
        }
    }
    /*if ($("#cmb6778 option:selected").val() == "7304") {
        if ($.trim($("#txt1294").val()) == "" || $.trim($("#txt372").val()) == "" || $.trim($("#txt373").val()) == "" || $.trim($("#txt374").val()) == "" || $.trim($("#txt7180").val()) == "") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 881 && elementOfArray.menuHijo == 885) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 881;
                mph.menuHijo = 885;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("tabla beneficiarios (reformulado)");
        }
    } else if ($("#cmb6778 option:selected").val() == "27400" || $("#cmb6778 option:selected").val() == "7303") {
        if ($.trim($("#txt7186").val()) == "" || $.trim($("#txt7187").val()) == "" || $.trim($("#txt7188").val()) == "" || $.trim($("#txt7189").val()) == "") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 881 && elementOfArray.menuHijo == 885) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 881;
                mph.menuHijo = 885;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("tabla beneficiarios (nuevo)");
        }
    }*/
    //Componentes
    for (var i = 1; i <= $("#cmb6831").val() ; i++) {
        if ($.trim($("#Tabtxt1970" + i).val()) == "" || $("#Tabcmb9680" + i + " option:selected").val() == "-1" || $.trim($("#txtTab7579" + i).val()) == "") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 953 && elementOfArray.menuHijo == 954) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 953;
                mph.menuHijo = 954;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("componentes");
        } else if ($("#Tabcmb9680" + i + " option:selected").val() == "902" && $.trim($("#Tabtxt9681" + i).val()) == "") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 953 && elementOfArray.menuHijo == 954) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 953;
                mph.menuHijo = 954;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("componentes concursables");
        }
    }
    if ($("#cmb7235 option:selected").val() == "-1") {
        var added = false;
        if (menuBlancos.length > 0) {
            $.map(menuBlancos, function (elementOfArray, indexInArray) {
                if (elementOfArray.menuPadre == 953 && elementOfArray.menuHijo == 954) { added = true; }
            });
        }
        if (!added) {
            var mph = {};
            mph.menuPadre = 953;
            mph.menuHijo = 954;
            menuBlancos.push(mph);
        }
        countNulos++;
        console.log("componentes extras");
    } else if ($("#cmb7235 option:selected").val() == "902") {
        for (var i = 1; i <= $("#cmb6953").val() ; i++) {
            if ($.trim($("#Tabtxt6955" + i).val()) == "" || $.trim($("#txtTab6956" + i).val()) == "" || $("#Tabcmb7238" + i + " option:selected").val() == "-1" || $("#Tabcmb6957" + i + " option:selected").val() == "-1" || $("#Tabcmb7581" + i + " option:selected").val() == "-1" || $.trim($("#Tabtxt6958" + i).val()) == "" || $.trim($("#Tabtxt6959" + i).val()) == "" || $.trim($("#txtTab7582" + i).val()) == "" || $("#Tabcmb9711" + i + " option:selected").val() == "-1") {
                var added = false;
                if (menuBlancos.length > 0) {
                    $.map(menuBlancos, function (elementOfArray, indexInArray) {
                        if (elementOfArray.menuPadre == 953 && elementOfArray.menuHijo == 954) { added = true; }
                    });
                }
                if (!added) {
                    var mph = {};
                    mph.menuPadre = 953;
                    mph.menuHijo = 954;
                    menuBlancos.push(mph);
                }
                countNulos++;
                console.log("componentes extras");
            } else if ($("#Tabcmb9711" + i + " option:selected").val() == "902" && $.trim($("#Tabtxt9712" + i).val()) == "") {
                var added = false;
                if (menuBlancos.length > 0) {
                    $.map(menuBlancos, function (elementOfArray, indexInArray) {
                        if (elementOfArray.menuPadre == 953 && elementOfArray.menuHijo == 954) { added = true; }
                    });
                }
                if (!added) {
                    var mph = {};
                    mph.menuPadre = 953;
                    mph.menuHijo = 954;
                    menuBlancos.push(mph);
                }
                countNulos++;
                console.log("componentes extras (concursables)");
            }
        }
    }
    //Acceso población beneficiada
    /*if ($("#cmb9093 option:selected").val() == "902") {
        if ($.trim($("#txt380").val()) == "") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 886 && elementOfArray.menuHijo == 887) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 886;
                mph.menuHijo = 887;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("acceso poblacion beneficiada");
        }
    }*/
    //Ejecutores
    if ($("#cmb82 option:selected").val() == "902") {
        for (var i = 1; i <= $("#cmb1339").val() ; i++) {
            if ($("#Tabcmb1342" + i + " option:selected").val() == "-1" || $.trim($("#Tabtxt7760" + i).val()) == "") {
                var added = false;
                if (menuBlancos.length > 0) {
                    $.map(menuBlancos, function (elementOfArray, indexInArray) {
                        if (elementOfArray.menuPadre == 953 && elementOfArray.menuHijo == 955) { added = true; }
                    });
                }
                if (!added) {
                    var mph = {};
                    mph.menuPadre = 953;
                    mph.menuHijo = 955;
                    menuBlancos.push(mph);
                }
                countNulos++;
                console.log("ejecutores");
            }
        }
    } else if ($("#cmb82 option:selected").val() == "-1") {
        var added = false;
        if (menuBlancos.length > 0) {
            $.map(menuBlancos, function (elementOfArray, indexInArray) {
                if (elementOfArray.menuPadre == 953 && elementOfArray.menuHijo == 955) { added = true; }
            });
        }
        if (!added) {
            var mph = {};
            mph.menuPadre = 953;
            mph.menuHijo = 955;
            menuBlancos.push(mph);
        }
        countNulos++;
        console.log("ejecutores (seleccione)");
    }
    //Articulaciones
    /*if ($("#cmb9065 option:selected").val() == "902") {
        for (var i = 1; i <= $("#cmb1344").val() ; i++) {
            if ($("#Tabcmb2026" + i + " option:selected").val() == "-1" || $.trim($("#Tabtxt1346" + i).val()) == "" || $.trim($("#txtTab93" + i).val()) == "") {
                var added = false;
                if (menuBlancos.length > 0) {
                    $.map(menuBlancos, function (elementOfArray, indexInArray) {
                        if (elementOfArray.menuPadre == 886 && elementOfArray.menuHijo == 888) { added = true; }
                    });
                }
                if (!added) {
                    var mph = {};
                    mph.menuPadre = 886;
                    mph.menuHijo = 888;
                    menuBlancos.push(mph);
                }
                countNulos++;
                console.log("articulaciones");
            }
        }
    }*/
    //Complementariedades    
    if ($("#cmb7586 option:selected").val() == "902") {
        for (var i = 1; i <= $("#cmb95").val() ; i++) {
            if ($("#Tabcmb2074" + i + " option:selected").val() == "-1" || $("#Tabcmb2076" + i + " option:selected").val() == "-1" || $.trim($("#txtTab7588" + i).val()) == "" || (!$("#Tabcbx9688").is(":checked") && !$("#Tabcbx9689").is(":checked") && !$("#Tabcbx9690").is(":checked") && !$("#Tabcbx9691").is(":checked") && !$("#Tabcbx9692").is(":checked") && !$("#Tabcbx9693").is(":checked") && !$("#Tabcbx9694").is(":checked") && !$("#Tabcbx9695").is(":checked"))) {
                var added = false;
                if (menuBlancos.length > 0) {
                    $.map(menuBlancos, function (elementOfArray, indexInArray) {
                        if (elementOfArray.menuPadre == 953 && elementOfArray.menuHijo == 956) { added = true; }
                    });
                }
                if (!added) {
                    var mph = {};
                    mph.menuPadre = 953;
                    mph.menuHijo = 956;
                    menuBlancos.push(mph);
                }
                countNulos++;
                console.log("complementariedades");
            }
        }
    } else if ($("#cmb7586 option:selected").val() == "-1") {
        var added = false;
        if (menuBlancos.length > 0) {
            $.map(menuBlancos, function (elementOfArray, indexInArray) {
                if (elementOfArray.menuPadre == 953 && elementOfArray.menuHijo == 956) { added = true; }
            });
        }
        if (!added) {
            var mph = {};
            mph.menuPadre = 953;
            mph.menuHijo = 956;
            menuBlancos.push(mph);
        }
        countNulos++;
        console.log("complementariedades (seleccione)");
    }
    //Enfoque genero
    if ($("#cmb9718 option:selected").val() == "28191") {
        if ($("#cmb9720 option:selected").val() == "-1") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 969 && elementOfArray.menuHijo == 970) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 969;
                mph.menuHijo = 970;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("perspectiva de genero (a)");
        }
    } else if ($("#cmb9718 option:selected").val() == "28192") {
        if ($("#cmb9720 option:selected").val() == "-1" || $.trim($("#txt9719").val()) == "") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 969 && elementOfArray.menuHijo == 970) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 969;
                mph.menuHijo = 970;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("perspectiva de genero (b)");
        }
    } else if ($("#cmb9718 option:selected").val() == "-1") {
        var added = false;
        if (menuBlancos.length > 0) {
            $.map(menuBlancos, function (elementOfArray, indexInArray) {
                if (elementOfArray.menuPadre == 969 && elementOfArray.menuHijo == 970) { added = true; }
            });
        }
        if (!added) {
            var mph = {};
            mph.menuPadre = 969;
            mph.menuHijo = 970;
            menuBlancos.push(mph);
        }
        countNulos++;
        console.log("perspectiva de genero (seleccione)");
    }
    /*if ($("#cmb9718 option:selected").val() == "902") {
        if ((!$("#cbx9698").is(":checked") && !$("#cbx9699").is(":checked") && !$("#cbx9700").is(":checked") && !$("#cbx9701").is(":checked") && !$("#cbx9702").is(":checked") && !$("#cbx9715").is(":checked")) || $.trim($("#txt9716").val()) == "" || $("#cmb9714 option:selected").val() == "-1") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 969 && elementOfArray.menuHijo == 970) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 969;
                mph.menuHijo = 970;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("enfoque genero");
        }
    } else if ($("#cmb8260 option:selected").val() == "901") {
        if ($.trim($("#txt8770").val()) == "") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 969 && elementOfArray.menuHijo == 970) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 969;
                mph.menuHijo = 970;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("enfoque genero");
        }        
    } else if ($("#cmb8260 option:selected").val() == "-1") {
        var added = false;
        if (menuBlancos.length > 0) {
            $.map(menuBlancos, function (elementOfArray, indexInArray) {
                if (elementOfArray.menuPadre == 969 && elementOfArray.menuHijo == 970) { added = true; }
            });
        }
        if (!added) {
            var mph = {};
            mph.menuPadre = 969;
            mph.menuHijo = 970;
            menuBlancos.push(mph);
        }
        countNulos++;
        console.log("enfoque genero");
    }*/
    //Enfoque DDHH    
    if ($("#cmb7324 option:selected").val() == "902") {
        if ((!$("#cbx7701").is(":checked") && !$("#cbx7708").is(":checked") && !$("#cbx7715").is(":checked") && !$("#cbx7702").is(":checked") && !$("#cbx7709").is(":checked") && !$("#cbx7716").is(":checked") && !$("#cbx7703").is(":checked") && !$("#cbx7710").is(":checked") && !$("#cbx7717").is(":checked") && !$("#cbx7704").is(":checked") && !$("#cbx7711").is(":checked") && !$("#cbx7718").is(":checked") && !$("#cbx7707").is(":checked") && !$("#cbx7712").is(":checked") && !$("#cbx7719").is(":checked") && !$("#cbx7705").is(":checked") && !$("#cbx7713").is(":checked") && !$("#cbx7720").is(":checked") && !$("#cbx7706").is(":checked") && !$("#cbx7714").is(":checked") && !$("#cbx7721").is(":checked")) || ($.trim($("#txt7722").val()) == "" && $.trim($("#txt7723").val()) == "" && $.trim($("#txt7724").val()) == "" && $.trim($("#txt7725").val()) == "" && $.trim($("#txt7726").val()) == "" && $.trim($("#txt7727").val()) == "" && $.trim($("#txt7728").val()) == "")) {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 969 && elementOfArray.menuHijo == 971) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 969;
                mph.menuHijo = 971;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("Tabla medidas enfoque DDHH");
        }
    } else if ($("#cmb7324 option:selected").val() == "901") {
        if ($.trim($("#txt7693").val()) == "") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 969 && elementOfArray.menuHijo == 971) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 969;
                mph.menuHijo = 971;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("Tabla medidas enfoque DDHH (justificacion)");
        }
    } else if ($("#cmb7324 option:selected").val() == "-1") {
        var added = false;
        if (menuBlancos.length > 0) {
            $.map(menuBlancos, function (elementOfArray, indexInArray) {
                if (elementOfArray.menuPadre == 969 && elementOfArray.menuHijo == 971) { added = true; }
            });
        }
        if (!added) {
            var mph = {};
            mph.menuPadre = 969;
            mph.menuHijo = 971;
            menuBlancos.push(mph);
        }
        countNulos++;
        console.log("enfoque DDHH");
    }
    //Indicadores de proposito
    var validoCompProp = true;
    if ($("#cmb7412 option:selected").val() == "13002") {
        if ($("#cmb8902 option:selected").val() == "901") {
            validoCompProp = false;
        } else if ($("#cmb8902 option:selected").val() == "-1") {
            validoCompProp = false;
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 966 && elementOfArray.menuHijo == 967) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 966;
                mph.menuHijo = 967;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("cuenta con indicadores de propósito");
        }
    }
    if (validoCompProp) {
        for (var i = 1; i <= $("#cmb6834").val() ; i++) {
            if ($.trim($("#txtTab3033" + i).val()) == "" || $.trim($("#txtTab7604" + i).val()) == "") {
                var added = false;
                if (menuBlancos.length > 0) {
                    $.map(menuBlancos, function (elementOfArray, indexInArray) {
                        if (elementOfArray.menuPadre == 966 && elementOfArray.menuHijo == 967) { added = true; }
                    });
                }
                if (!added) {
                    var mph = {};
                    mph.menuPadre = 966;
                    mph.menuHijo = 967;
                    menuBlancos.push(mph);
                }
                countNulos++;
                console.log("indicador de proposito");
            }
            if ($("#Tabcmb7762" + i + " option:selected").val() == "902") {
                if (!$("#Tabcbx7763" + i).is(":checked") && !$("#Tabcbx7764" + i).is(":checked") && !$("#Tabcbx7765" + i).is(":checked") && !$("#Tabcbx7766" + i).is(":checked") && !$("#Tabcbx7767" + i).is(":checked") && !$("#Tabcbx7768" + i).is(":checked") && !$("#Tabcbx7769" + i).is(":checked") && !$("#Tabcbx7770" + i).is(":checked")) {
                    var added = false;
                    if (menuBlancos.length > 0) {
                        $.map(menuBlancos, function (elementOfArray, indexInArray) {
                            if (elementOfArray.menuPadre == 966 && elementOfArray.menuHijo == 967) { added = true; }
                        });
                    }
                    if (!added) {
                        var mph = {};
                        mph.menuPadre = 966;
                        mph.menuHijo = 967;
                        menuBlancos.push(mph);
                    }
                    countNulos++;
                    console.log("categorias indicador de proposito");
                }
            } else if ($("#Tabcmb7762" + i + " option:selected").val() == "-1") {
                var added = false;
                if (menuBlancos.length > 0) {
                    $.map(menuBlancos, function (elementOfArray, indexInArray) {
                        if (elementOfArray.menuPadre == 966 && elementOfArray.menuHijo == 967) { added = true; }
                    });
                }
                if (!added) {
                    var mph = {};
                    mph.menuPadre = 966;
                    mph.menuHijo = 967;
                    menuBlancos.push(mph);
                }
                countNulos++;
                console.log("categorias indicador de proposito");
            }
            if (i == 1) {
                if ($.trim($("#txt7621").val()) == "" || $.trim($("#txt7622").val()) == "") {
                    var added = false;
                    if (menuBlancos.length > 0) {
                        $.map(menuBlancos, function (elementOfArray, indexInArray) {
                            if (elementOfArray.menuPadre == 966 && elementOfArray.menuHijo == 967) { added = true; }
                        });
                    }
                    if (!added) {
                        var mph = {};
                        mph.menuPadre = 966;
                        mph.menuHijo = 967;
                        menuBlancos.push(mph);
                    }
                    countNulos++;
                    console.log("numerador/denominador de proposito 1");
                }
            } else {
                if ($.trim($("#txt8279").val()) == "" || $.trim($("#txt8280").val()) == "") {
                    var added = false;
                    if (menuBlancos.length > 0) {
                        $.map(menuBlancos, function (elementOfArray, indexInArray) {
                            if (elementOfArray.menuPadre == 966 && elementOfArray.menuHijo == 967) { added = true; }
                        });
                    }
                    if (!added) {
                        var mph = {};
                        mph.menuPadre = 966;
                        mph.menuHijo = 967;
                        menuBlancos.push(mph);
                    }
                    countNulos++;
                    console.log("numerador/denominador de proposito 2");
                }
            }
        }
    }
    //Indicadores complementarios
    var validoComp = true;
    var numDenoComp = [['txt7629', 'txt7630'], ['txt8285', 'txt8299'], ['txt8286', 'txt8300'], ['txt8287', 'txt8301'], ['txt8288', 'txt8302'], ['txt8289', 'txt8303'], ['txt8290', 'txt8304'], ['txt8291', 'txt8305'], ['txt8292', 'txt8306'], ['txt8293', 'txt8307'], ['txt8294', 'txt8308'], ['txt8295', 'txt8309'], ['txt8296', 'txt8310'], ['txt8297', 'txt8311'], ['txt8298', 'txt8312']];
    if ($("#cmb7412 option:selected").val() == "13002") {
        if ($("#cmb7781 option:selected").val() == "901") {
            validoComp = false;
        } else if ($("#cmb7781 option:selected").val() == "-1") {
            validoComp = false;
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 966 && elementOfArray.menuHijo == 968) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 966;
                mph.menuHijo = 968;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("cuenta con indicadores complementarios");
        }
    }
    if (validoComp) {
        for (var i = 1; i <= $("#cmb3036").val() ; i++) {
            if ($.trim($("#txtTab3050" + i).val()) == "" || $.trim($("#txtTab7625" + i).val()) == "") {
                var added = false;
                if (menuBlancos.length > 0) {
                    $.map(menuBlancos, function (elementOfArray, indexInArray) {
                        if (elementOfArray.menuPadre == 966 && elementOfArray.menuHijo == 968) { added = true; }
                    });
                }
                if (!added) {
                    var mph = {};
                    mph.menuPadre = 966;
                    mph.menuHijo = 968;
                    menuBlancos.push(mph);
                }
                countNulos++;
                console.log("indicadores complementarios");
            }
            if ($("#Tabcmb7626" + i + " option:selected").val() == "902") {
                if (!$("#Tabcbx7771" + i).is(":checked") && !$("#Tabcbx7772" + i).is(":checked") && !$("#Tabcbx7773" + i).is(":checked") && !$("#Tabcbx7774" + i).is(":checked") && !$("#Tabcbx7775" + i).is(":checked") && !$("#Tabcbx7776" + i).is(":checked") && !$("#Tabcbx7777" + i).is(":checked") && !$("#Tabcbx7778" + i).is(":checked")) {
                    var added = false;
                    if (menuBlancos.length > 0) {
                        $.map(menuBlancos, function (elementOfArray, indexInArray) {
                            if (elementOfArray.menuPadre == 966 && elementOfArray.menuHijo == 968) { added = true; }
                        });
                    }
                    if (!added) {
                        var mph = {};
                        mph.menuPadre = 966;
                        mph.menuHijo = 968;
                        menuBlancos.push(mph);
                    }
                    countNulos++;
                    console.log("categorias indicador complementario");
                }
            } else if ($("#Tabcmb7626" + i + " option:selected").val() == "-1") {
                var added = false;
                if (menuBlancos.length > 0) {
                    $.map(menuBlancos, function (elementOfArray, indexInArray) {
                        if (elementOfArray.menuPadre == 966 && elementOfArray.menuHijo == 968) { added = true; }
                    });
                }
                if (!added) {
                    var mph = {};
                    mph.menuPadre = 966;
                    mph.menuHijo = 968;
                    menuBlancos.push(mph);
                }
                countNulos++;
                console.log("categorias indicador complementario");
            }
            if ($.trim($("#" + numDenoComp[i - 1][0]).val()) == "" || $.trim($("#" + numDenoComp[i - 1][1]).val()) == "") {
                var added = false;
                if (menuBlancos.length > 0) {
                    $.map(menuBlancos, function (elementOfArray, indexInArray) {
                        if (elementOfArray.menuPadre == 966 && elementOfArray.menuHijo == 968) { added = true; }
                    });
                }
                if (!added) {
                    var mph = {};
                    mph.menuPadre = 966;
                    mph.menuHijo = 968;
                    menuBlancos.push(mph);
                }
                countNulos++;
                console.log("numerador/denominador complementario " + i);
            }
        }
    }
    //Sistema información
    /*if ($("#cmb7875 option:selected").val() == "901") {
        if ($.trim($("#txt7885").val()) == "") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 890 && elementOfArray.menuHijo == 893) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 890;
                mph.menuHijo = 893;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("sistemas de información");
        }
    }*/
    /*if ($("#cmb2434 option:selected").val() == "902") {
        if ($.trim($("#txt7886").val()) == "" || $.trim($("#txt7887").val()) == "" || $("#cmb7891 option:selected").val() == "-1") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 890 && elementOfArray.menuHijo == 893) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 890;
                mph.menuHijo = 893;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("sistemas de información");
        }
    }*/
    //Presupuesto
    for (var i = 1; i <= $("#cmb10").val() ; i++) {
        if ($.trim($("#Tabtxt12" + i).val()) == "" || $.trim($("#Tabtxt60" + i).val()) == "" || $.trim($("#Tabtxt13" + i).val()) == "") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 957 && elementOfArray.menuHijo == 958) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 957;
                mph.menuHijo = 958;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("partidas presupuestarias");
        }
    }
    //Gastos de inversión
    /*if ($("#cmb2504 option:selected").val() == "902") {
        for (var i = 1; i <= $("#cmb2505").val() ; i++) {
            if ($.trim($("#Tabtxt2507" + i).val()) == "" || $.trim($("#Tabtxt2508" + i).val()) == "" || $.trim($("#Tabtxt2509" + i).val()) == "" || $.trim($("#txtTab2510" + i).val()) == "") {
                var added = false;
                if (menuBlancos.length > 0) {
                    $.map(menuBlancos, function (elementOfArray, indexInArray) {
                        if (elementOfArray.menuPadre == 894 && elementOfArray.menuHijo == 898) { added = true; }
                    });
                }
                if (!added) {
                    var mph = {};
                    mph.menuPadre = 894;
                    mph.menuHijo = 898;
                    menuBlancos.push(mph);
                }
                countNulos++;
                console.log("gasto de inversión");
            }
        }
    }*/
    //Gasto gobiernos regionales
    if ($("#cmb3075 option:selected").val() == "902") {
        if ($("#cmb8877 option:selected").val() == "902" || $("#cmb8877 option:selected").val() == "-1") {
            if ($.trim($("#txt3095").val()) == "") {
                var added = false;
                if (menuBlancos.length > 0) {
                    $.map(menuBlancos, function (elementOfArray, indexInArray) {
                        if (elementOfArray.menuPadre == 957 && elementOfArray.menuHijo == 960) { added = true; }
                    });
                }
                if (!added) {
                    var mph = {};
                    mph.menuPadre = 957;
                    mph.menuHijo = 960;
                    menuBlancos.push(mph);
                }
                countNulos++;
                console.log("gasto FNDR");
            }            
        }
        if ($("#cmb8878 option:selected").val() == "902" || $("#cmb8878 option:selected").val() == "-1") {
            if ($.trim($("#txt8897").val()) == "") {
                var added = false;
                if (menuBlancos.length > 0) {
                    $.map(menuBlancos, function (elementOfArray, indexInArray) {
                        if (elementOfArray.menuPadre == 957 && elementOfArray.menuHijo == 960) { added = true; }
                    });
                }
                if (!added) {
                    var mph = {};
                    mph.menuPadre = 957;
                    mph.menuHijo = 960;
                    menuBlancos.push(mph);
                }
                countNulos++;
                console.log("gasto otros fondos regionales");
            }
        }        
    } else if ($("#cmb3075 option:selected").val() == "-1") {
        var added = false;
        if (menuBlancos.length > 0) {
            $.map(menuBlancos, function (elementOfArray, indexInArray) {
                if (elementOfArray.menuPadre == 957 && elementOfArray.menuHijo == 960) { added = true; }
            });
        }
        if (!added) {
            var mph = {};
            mph.menuPadre = 957;
            mph.menuHijo = 960;
            menuBlancos.push(mph);
        }
        countNulos++;
        console.log("gasto GORES (seleccione)");
    }
    //Gastos extrapresupuestarios
    if ($("#cmb3074 option:selected").val() == "902") {
        if ($("#cmb3097 option:selected").val() == "902") {
            for (var i = 1; i <= $("#cmb3098").val() ; i++) {
                if ($.trim($("#Tabtxt3100" + i).val()) == "" || $.trim($("#Tabtxt3101" + i).val()) == "") {
                    var added = false;
                    if (menuBlancos.length > 0) {
                        $.map(menuBlancos, function (elementOfArray, indexInArray) {
                            if (elementOfArray.menuPadre == 957 && elementOfArray.menuHijo == 961) { added = true; }
                        });
                    }
                    if (!added) {
                        var mph = {};
                        mph.menuPadre = 957;
                        mph.menuHijo = 961;
                        menuBlancos.push(mph);
                    }
                    countNulos++;
                    console.log("gasto instituciones privadas");
                }
            }
        } else if ($("#cmb3097 option:selected").val() == "-1") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 957 && elementOfArray.menuHijo == 961) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 957;
                mph.menuHijo = 961;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("gasto instituciones privadas (seleccione)");
        }
        if ($("#cmb3102 option:selected").val() == "902") {
            for (var i = 1; i <= $("#cmb3104").val() ; i++) {
                if ($.trim($("#Tabtxt3108" + i).val()) == "" || $.trim($("#Tabtxt3110" + i).val()) == "") {
                    var added = false;
                    if (menuBlancos.length > 0) {
                        $.map(menuBlancos, function (elementOfArray, indexInArray) {
                            if (elementOfArray.menuPadre == 957 && elementOfArray.menuHijo == 961) { added = true; }
                        });
                    }
                    if (!added) {
                        var mph = {};
                        mph.menuPadre = 957;
                        mph.menuHijo = 961;
                        menuBlancos.push(mph);
                    }
                    countNulos++;
                    console.log("gasto organismos internacionales");
                }
            }
        } else if ($("#cmb3102 option:selected").val() == "-1") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 957 && elementOfArray.menuHijo == 961) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 957;
                mph.menuHijo = 961;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("gasto organismos internacionales (seleccione)");
        }
        if ($("#cmb3103 option:selected").val() == "902") {
            for (var i = 1; i <= $("#cmb3105").val() ; i++) {
                if ($.trim($("#Tabtxt3109" + i).val()) == "" || $.trim($("#Tabtxt3111" + i).val()) == "") {
                    var added = false;
                    if (menuBlancos.length > 0) {
                        $.map(menuBlancos, function (elementOfArray, indexInArray) {
                            if (elementOfArray.menuPadre == 957 && elementOfArray.menuHijo == 961) { added = true; }
                        });
                    }
                    if (!added) {
                        var mph = {};
                        mph.menuPadre = 957;
                        mph.menuHijo = 961;
                        menuBlancos.push(mph);
                    }
                    countNulos++;
                    console.log("gasto otras fuentes");
                }
            }
        } else if ($("#cmb3103 option:selected").val() == "-1") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 957 && elementOfArray.menuHijo == 961) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 957;
                mph.menuHijo = 961;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("gasto otras fuentes (seleccione)");
        }
    } else if ($("#cmb3074 option:selected").val() == "-1") {
        var added = false;
        if (menuBlancos.length > 0) {
            $.map(menuBlancos, function (elementOfArray, indexInArray) {
                if (elementOfArray.menuPadre == 957 && elementOfArray.menuHijo == 961) { added = true; }
            });
        }
        if (!added) {
            var mph = {};
            mph.menuPadre = 957;
            mph.menuHijo = 961;
            menuBlancos.push(mph);
        }
        countNulos++;
        console.log("gastos extrapresupuestarios (seleccione)");
    }
    //Gastos componentes
    var pregDetComp = ["txt2483", "txt2484", "txt2485", "txt2486", "txt2487", "txt2488", "txt2489", "txt2490", "txt2491", "txt2492", "txt2493", "txt2494", "txt2495", "txt2496", "txt2497"];
    var pregPrespComp = ["txt6865", "txt6866", "txt6867", "txt6868", "txt6869", "txt6870", "txt6871", "txt6872", "txt6873", "txt6874", "txt6875", "txt6876", "txt6877", "txt6878", "txt6879"];
    var totalCompVal = ($("#cmb7235 option:selected").val() == "902" ? parseInt($("#cmb6831 option:selected").val()) + parseInt($("#cmb6953 option:selected").val()) : parseInt($("#cmb6831 option:selected").val()));
    for (var i = 0; i < totalCompVal; i++) {
        if ($.trim($("#" + pregDetComp[i]).val()) == "" || $.trim($("#" + pregPrespComp[i]).val()) == "") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 957 && elementOfArray.menuHijo == 962) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 957;
                mph.menuHijo = 962;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("Gastos componentes");
        }        
    }
    //Detalle regional
    if ($.trim($("#txt7267").val()) == "") {
        var added = false;
        if (menuBlancos.length > 0) {
            $.map(menuBlancos, function (elementOfArray, indexInArray) {
                if (elementOfArray.menuPadre == 957 && elementOfArray.menuHijo == 963) { added = true; }
            });
        }
        if (!added) {
            var mph = {};
            mph.menuPadre = 957;
            mph.menuHijo = 963;
            menuBlancos.push(mph);
        }
        countNulos++;
        console.log("Detalle regional componentes");
    }
    //Observaciones
    if ($("#cmb7730 option:selected").val() == "902") {
        for (var i = 1; i <= $("#cmb7731").val() ; i++) {
            if ($("#Tabcmb7733" + i + " option:selected").val() == "-1" || $.trim($("#txtTab7734" + i).val()) == "") {
                var added = false;
                if (menuBlancos.length > 0) {
                    $.map(menuBlancos, function (elementOfArray, indexInArray) {
                        if (elementOfArray.menuPadre == 977 && elementOfArray.menuHijo == 978) { added = true; }
                    });
                }
                if (!added) {
                    var mph = {};
                    mph.menuPadre = 977;
                    mph.menuHijo = 978;
                    menuBlancos.push(mph);
                }
                countNulos++;
                console.log("observaciones");
            }
        }
    } else if ($("#cmb7730 option:selected").val() == "-1") {
        var added = false;
        if (menuBlancos.length > 0) {
            $.map(menuBlancos, function (elementOfArray, indexInArray) {
                if (elementOfArray.menuPadre == 977 && elementOfArray.menuHijo == 978) { added = true; }
            });
        }
        if (!added) {
            var mph = {};
            mph.menuPadre = 977;
            mph.menuHijo = 978;
            menuBlancos.push(mph);
        }
        countNulos++;
        console.log("observaciones");
    }
    var menuPadreActivo = 0;
    $("#aMenuForm li").each(function (index) {
        if ($(this).hasClass("active")) {
            menuPadreActivo = this.id;
            return false;
        }
    });
    var menuHijoActivo = "";
    if (menuPadreActivo > 0) {
        $("#ulForm" + menuPadreActivo + " li").each(function (index) {
            if ($(this).hasClass("active")) {
                menuHijoActivo = this.id;
                return false;
            }
        });
    }
    //Conteo final de nulos
    if (countNulos > 0) {
        $("#btnEnviar").hide();
        var countErrorMenuActual = 0;
        var countErrorMenuActualPadre = 0;
        if (menuBlancos.length > 0) {
            if (menuHijoActivo != "") {
                menuHijoActivo = menuHijoActivo.substring(8, menuHijoActivo.length);
                $.each(menuBlancos, function (i, d) {
                    if (d.menuPadre == menuPadreActivo && d.menuHijo == menuHijoActivo) {
                        countErrorMenuActual++;
                    }
                    if (d.menuPadre == menuPadreActivo) {
                        countErrorMenuActualPadre++;
                    }
                });
            }            
        }
        if (countErrorMenuActual > 0) {
            $("#spnHijo" + menuHijoActivo).css("background-color", "#EDF92D");
        } else {
            $("#spnHijo" + menuHijoActivo).css("background-color", "#1ABD00");
        }
        if (countErrorMenuActualPadre > 0) {
            $("#spnPadre" + menuPadreActivo).css("background-color", "#EDF92D");
        } else {
            $("#spnPadre" + menuPadreActivo).css("background-color", "#1ABD00");
        }        
    } else {
        $("#btnEnviar").show();
        $('#btnEnviar').prop('disabled', false);
        if (inicio) {
            $("#spnPadre944").css("background-color", "#1ABD00");
            $("#spnHijo945").css("background-color", "#1ABD00");
        } else {
            $("#spnPadre" + menuPadre).css("background-color", "#1ABD00");
            if (menuHijo == 0) {
                $("#spnHijo" + (menuPadre == 944 ? 945 : (menuPadre == 949 ? 950 : (menuPadre == 953 ? 954 : (menuPadre == 957 ? 958 : (menuPadre == 969 ? 970 : (menuPadre == 977 ? 978 : 0))))))).css("background-color", "#1ABD00");
            } else {
                $("#spnHijo" + menuHijo).css("background-color", "#1ABD00");
            }            
        }
    }
    //Validaciones cruzadas
    //Detalle beneficiarios por region
    if ($.trim($("#txt6974").val()) > 0) {
        if ($.trim($("#txt6806").val()) != $.trim($("#txt6974").val())) {
            $("#txt6806").css("background-color", "#FF6F6F");
            $("#spnPadre949").css("background-color", "#FF2B2B");
            $("#spnHijo952").css("background-color", "#FF2B2B");
            if (!$("#alertBeneficiarios5").length) {
                $("#txt6806").after('<p id="alertBeneficiarios5" class="help-block" style="color:#FF6F6F; font-size:12px;">El total de la tabla  de beneficiarios regional no coincide con el total de beneficiarios en el año t registrados.</p>');
            }
            $("#btnEnviar").hide();
        } else {
            $("#txt6806").css("background-color", "transparent ");
            $("#alertBeneficiarios5").remove();
        }
    }
    //Total territorial
    if ($("#cmb7210 option:selected").val() == "902") {
        if ($.trim($("#txt7221").val()) > 0) {
            if ($.trim($("#txt7221").val()) != $.trim($("#txt6974").val())) {
                $("#txt7221").css("background-color", "#FF6F6F");
                $("#spnPadre949").css("background-color", "#FF2B2B");
                $("#spnHijo952").css("background-color", "#FF2B2B");
                if (!$("#alertBeneficiarios1").length) {
                    $("#txt7221").after('<p id="alertBeneficiarios1" class="help-block" style="color:#FF6F6F; font-size:12px;">El total de la tabla de beneficiarios por aspectos territoriales no coincide con el total de beneficiarios en el año t registrados.</p>');                    
                }
                $("#btnEnviar").hide();
            } else {
                $("#txt7221").css("background-color", "transparent ");
                $("#alertBeneficiarios1").remove();
            }
        }
    }
    //Total por empresa
    if ($("#cmb33 option:selected").val() == "866") {
        if ($.trim($("#txt6830").val()) > 0) {
            if ($.trim($("#txt6830").val()) != $.trim($("#txt6974").val())) {
                $("#txt6830").css("background-color", "#FF6F6F");
                $("#spnPadre949").css("background-color", "#FF2B2B");
                $("#spnHijo952").css("background-color", "#FF2B2B");
                if (!$("#alertBeneficiarios2").length) {
                    $("#txt6830").after('<p id="alertBeneficiarios2" class="help-block" style="color:#FF6F6F; font-size:12px;">El total de la tabla de beneficiarios por empresa no coincide con el total de beneficiarios en el año t registrados.</p>');
                }
                $("#btnEnviar").hide();
            } else {
                $("#txt6830").css("background-color", "transparent ");
                $("#alertBeneficiarios2").remove();
            }
        }
    }
    //Total por genero
    if ($("#cmb33 option:selected").val() == "851") {
        if ($.trim($("#txt6829").val()) > 0) {
            if ($.trim($("#txt6829").val()) != $.trim($("#txt6974").val())) {
                $("#txt6829").css("background-color", "#FF6F6F");
                $("#spnPadre949").css("background-color", "#FF2B2B");
                $("#spnHijo952").css("background-color", "#FF2B2B");
                if (!$("#alertBeneficiarios3").length) {
                    $("#txt6829").after('<p id="alertBeneficiarios3" class="help-block" style="color:#FF6F6F; font-size:12px;">El total de la tabla de beneficiarios por sexo registral no coincide con el total de beneficiarios en el año t registrados.</p>');
                }
                $("#btnEnviar").hide();
            } else {
                $("#txt6830").css("background-color", "transparent ");
                $("#alertBeneficiarios3").remove();
            }
        }
    }
    //Detalle regional componentes
    if ($.trim($("#txt7267").val()) > 0) {
        if ($.trim($("#txt7267").val()) != $.trim($("#txt6944").val())) {
            $("#txt7267").css("background-color", "#FF6F6F");
            $("#spnPadre957").css("background-color", "#FF2B2B");
            $("#spnHijo963").css("background-color", "#FF2B2B");
            if (!$("#alertBeneficiarios4").length) {
                $("#txt7267").after('<p id="alertBeneficiarios4" class="help-block" style="color:#FF6F6F; font-size:12px;">El total de los gastos  por componente no coincide con el total del detalle regional de gasto por componente.</p>');
            }
            $("#btnEnviar").hide();
        } else {
            $("#txt7267").css("background-color", "transparent ");
            $("#alertBeneficiarios4").remove();
        }
    }
    //Excepciones
    $("#spnHijo947").remove();
    $("#spnHijo948").remove();
    $("#spnHijo950").remove();
    $("#spnHijo951").remove();
    $("#spnHijo959").remove();
    $("#spnHijo965").remove();
    $("#spnHijo973").remove();
    $("#spnHijo974").remove();
    $("#spnHijo975").remove();
    $("#spnHijo976").remove();    
}