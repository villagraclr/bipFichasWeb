function validaBlancos(inicio, menuPadre, menuHijo) {
    var countNulos = 0;
    var countNulosAdmisibilidad = 0;
    var menuActual = 0;
    var menuBlancos = [];
    $.each(camposValidar, function (i, item) {
        var excepciones = true;
        if (item.tipo == "texto" || item.tipo == "textoGrande" || item.tipo == "numerico") {
            if (item.idTab == "") {
                //console.log($.trim($("#" + item.idObjeto).val()));
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
                        excepciones = false;
                    }
                    if (excepciones) {
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

    //Perfil del programa
    $('#cmb8248').prop('disabled', true);

    if ($("#cmb9327 option:selected").val() == "27625" || $("#cmb9327 option:selected").val() == "27626" || $("#cmb9327 option:selected").val() == "27627") {
        if ($.trim($("#txt9328").val()) == "") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 911 && elementOfArray.menuHijo == 912) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 911;
                mph.menuHijo = 912;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("Entidad responsable por la ejecución");
        }
    }
    var cmbPadre = $("#cmb9329 option:selected").val();
    var valoresCmbPadre = ["27632", "27633", "27634", "27635", "27636", "27637", "27638", "27640"];
    var index = valoresCmbPadre.indexOf(cmbPadre);
    if (valoresCmbPadre.includes(cmbPadre)) {
        var selectoresHijos = ["#cmb9330", "#cmb9331", "#cmb9332", "#cmb9333", "#cmb9334", "#cmb9335", "#cmb9336", "#cmb9337"];
        if ($(selectoresHijos[index] + " option:selected").val() == "-1") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 911 && elementOfArray.menuHijo == 912) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 911;
                mph.menuHijo = 912;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("Principal competencia (Select adicional)");
        }
    }
    if ($("#cmb9338 option:selected").val() == "902") {
        if ($("#cmb9449 option:selected").val() == "-1") {
            var added = false;
            if (menuBlancos.length > 0) {
                $.map(menuBlancos, function (elementOfArray, indexInArray) {
                    if (elementOfArray.menuPadre == 911 && elementOfArray.menuHijo == 912) { added = true; }
                });
            }
            if (!added) {
                var mph = {};
                mph.menuPadre = 911;
                mph.menuHijo = 912;
                menuBlancos.push(mph);
            }
            countNulos++;
            console.log("Entidad responsable por la ejecución");
        }
    }
    //Evaluacion
    $("#cmb9722").val() == "902" || $("#cmb9725").val() == "902" ? $('#tabHijos937').show() : $('#tabHijos937').hide();
    $("#cmb9725").val() == "901" || $("#cmb9725").val() == "902" ? $("#cmb9722").prop('disabled', true) : '';
    $("#cmb9725").val() == "901" || $("#cmb9725").val() == "902" ? $("#cmb9723").prop('disabled', true) : '';
    $("#cmb9725").val() == "901" || $("#cmb9725").val() == "902" ? $("#txt9724").prop('disabled', true) : '';

    function agregarVacio(padre, hijo, mensaje) {
        var added = false;
        if (menuBlancos.length > 0) {
            $.map(menuBlancos, function (elementOfArray, indexInArray) {
                if (elementOfArray.menuPadre == padre && elementOfArray.menuHijo == hijo) { added = true; }
            });
        }
        if (!added) {
            var mph = {};
            mph.menuPadre = padre;
            mph.menuHijo = hijo;
            menuBlancos.push(mph);
        }
        countNulos++;
        console.log(mensaje);
    }

    //CONTEO FINAL DE NULOS
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
}
