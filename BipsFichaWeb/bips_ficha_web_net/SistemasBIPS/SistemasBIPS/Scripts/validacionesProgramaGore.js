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
    //PROGRAMA GORE

    //Diagnóstico - Diagnóstico - Causas
    for (var i = 1; i <= $("#cmb9368 option:selected").val() ; i++) {
        if ($.trim($("#Tabtxt9370" + i.toString()).val()) == "" || $.trim($("#txtTab9371" + i.toString()).val()) == "" || $.trim($("#txtTab9372" + i.toString()).val()) == "") {
            agregarVacio(915, 916, 'Diagnóstico - Diagnóstico - Causas');
        }
    }

    //Propósito y Focalización - Población beneficiaria - Cuantifique la población
    if ($.trim($("#txt1190").val()) == "" || $.trim($("#txt2956").val()) == "" || $.trim($("#txt7187").val()) == "") {
        agregarVacio(917, 921, 'Propósito y Focalización - Población beneficiaria - Cuantifique la población');
    }

    //Estrategia - Estrategia - Componentes
    for (var i = 1; i <= $("#cmb9388 option:selected").val() ; i++) {
        if ($.trim($("#Tabtxt9390" + i.toString()).val()) == "" || $.trim($("#Tabcmb9391" + i.toString()).val()) == "-1" || $.trim($("#Tabcmb9392" + i.toString()).val()) == "-1" || $.trim($("#Tabtxt9393" + i.toString()).val()) == "" || $.trim($("#Tabtxt9394" + i.toString()).val()) == "" || $.trim($("#Tabtxt9395" + i.toString()).val()) == "" || $.trim($("#Tabtxt9396" + i.toString()).val()) == "" || $.trim($("#Tabtxt9397" + i.toString()).val()) == "") {
            agregarVacio(922, 923, 'Estrategia - Estrategia - Componentes');
        }
    }

    //Ejecutores y complementariedades - Ejecutores - Instituciones
    if ($("#cmb9403 option:selected").val() == "902") {
        for (var i = 1; i <= $("#cmb9404 option:selected").val() ; i++) {
            if ($.trim($("#Tabtxt9406" + i.toString()).val()) == "" || $.trim($("#Tabcmb9407" + i.toString()).val()) == "-1" || $.trim($("#Tabcmb9409" + i.toString()).val()) == "-1" || $.trim($("#txtTab9410" + i.toString()).val()) == "" || $.trim($("#txtTab9411" + i.toString()).val()) == "") {
                agregarVacio(924, 925, 'ejecutores y complementariedades - Ejecutores - Instituciones');
            }
        }
    }

    //Ejecutores y complementariedades - Complementariedades - Programas
    if ($("#cmb9412 option:selected").val() == "902") {
        for (var i = 1; i <= $("#cmb9413 option:selected").val() ; i++) {
            if ($.trim($("#Tabtxt9415" + i.toString()).val()) == "" || $.trim($("#Tabtxt9416" + i.toString()).val()) == "" || $.trim($("#txtTab9417" + i.toString()).val()) == "") {
                agregarVacio(924, 926, 'Ejecutores y complementariedades - Ejecutores - Instituciones');
            }
        }
    }

    //Enfoque y/o perspectivas de derechos humanos - Enfoque y/o perspectivas de derechos humanos
    if ($("#cmb9418 option:selected").val() == "902") {
        if ($.trim($("#txt9419").val()) == "") {
            agregarVacio(927, 928, 'Enfoque y/o perspectivas de derechos humanos - Enfoque y/o perspectivas de derechos humanos - Describa medidas concretas');
        }
    } else if ($("#cmb9418 option:selected").val() == "901") {
        if ($("#cmb9420 option:selected").val() == "-1") {
            agregarVacio(927, 928, 'Enfoque y/o perspectivas de derechos humanos - Enfoque y/o perspectivas de derechos humanos - Igualdad mujeres y niñas');
        } else {
            if ($("#cmb9420 option:selected").val() == "902") {
                if ($.trim($("#txt9421").val()) == "") {
                    agregarVacio(927, 928, 'Enfoque y/o perspectivas de derechos humanos - Enfoque y/o perspectivas de derechos humanos - Describa medidas concretas');
                }
            } else {
                if ($.trim($("#txt9422").val()) == "") {
                    agregarVacio(927, 928, 'Enfoque y/o perspectivas de derechos humanos - Enfoque y/o perspectivas de derechos humanos - Justifique por qué');
                }
            }
        }
    }

    //Indicadores - Indicadores de propósito - Indicadores
    if ($.trim($("#Tabtxt1161").val()) == "" || $.trim($("#txtTab1171").val()) == "" || $.trim($("#Tabcmb1181").val()) == "-1" || $.trim($("#Tabcmb1211").val()) == "-1" || $.trim($("#Tabtxt29431").val()) == "" || $.trim($("#Tabtxt29441").val()) == "" || $.trim($("#Tabtxt94331").val()) == "") {
        agregarVacio(929, 930, 'Indicadores - Indicadores de propósito - Indicador');
    }

    //Indicadores - Indicadores Complementarios - Indicadores 
    if ($.trim($("#Tabtxt7861").val()) == "" || $.trim($("#txtTab7871").val()) == "" || $.trim($("#Tabcmb7881").val()) == "-1" || $.trim($("#Tabcmb7911").val()) == "-1" || $.trim($("#Tabtxt29451").val()) == "" || $.trim($("#Tabtxt29461").val()) == "" || $.trim($("#Tabtxt94391").val()) == "") {
        agregarVacio(929, 931, 'Indicadores - Indicadores complementarios - Indicador');
    }

    //Indicadores - Sistemas de información del programa - Describa sistema
    if ($("#cmb9441 option:selected").val() == "902") {
        if ($.trim($("#txt9442").val()) == "") {
            agregarVacio(929, 932, 'Indicadores - Sistemas de información del programa - Describa sistema');
        }
    }

    //Gastos - Gastos por componente y Gasto Administrativo - Tabla gastos
    for (var i = 0; i < $("#cmb9388 option:selected").val() ; i++) {
        var txtGasto = 2459 + i;
        var txtDescripcion = 2483 + i;
        if ($.trim($("#txt" + txtGasto).val()) == "" || $.trim($("#txt" + txtDescripcion).val()) == "") {
            agregarVacio(933, 934, 'Gastos - Gastos por componente y Gasto Administrativo - Tabla gastos');
        }
    }

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

    var cumplePuntajeObligatorio = true;
    var preguntasEvaluacion = [
        {
            //Diagnóstico
            resumen: { pregunta: 8207, puntaje: 0, pestaña: 1, dimension: 'atingencia', puntajeMinimo: 9 },
            preguntas: [
                { pregunta: 9502, puntaje: 9503, puntajeRequerido: 3, puntajeMaximo: 3, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: true },
                { pregunta: 9507, puntaje: 9508, puntajeRequerido: 3, puntajeMaximo: 3, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: true },
                { pregunta: 9512, puntaje: 9513, puntajeRequerido: 0, puntajeMaximo: 3, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: true },
                { pregunta: 9517, puntaje: 9518, puntajeRequerido: 3, puntajeMaximo: 3, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: true },
            ]
        },
        {
            //Objetivo
            resumen: { pregunta: 8208, puntaje: 0, pestaña: 1, dimension: 'coherencia', puntajeMinimo: 13 },
            preguntas: [
                { pregunta: 9524, puntaje: 9525, puntajeRequerido: 3, puntajeMaximo: 3, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: true },
                { pregunta: 9529, puntaje: 9530, puntajeRequerido: 3, puntajeMaximo: 3, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: true },
                { pregunta: 9534, puntaje: 9535, puntajeRequerido: 1, puntajeMaximo: 1, dosAlternativas: true, cuartaAlternativaCero: false, sumaPuntaje: true },
                { pregunta: 9539, puntaje: 9540, puntajeRequerido: 0, puntajeMaximo: 1, dosAlternativas: true, cuartaAlternativaCero: false, sumaPuntaje: true },
                { pregunta: 9544, puntaje: 9545, puntajeRequerido: 3, puntajeMaximo: 3, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: true },
                { pregunta: 9549, puntaje: 9550, puntajeRequerido: 0, puntajeMaximo: 1, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: true },
                { pregunta: 9554, puntaje: 9555, puntajeRequerido: 3, puntajeMaximo: 3, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: true },
                { pregunta: 9559, puntaje: 9560, puntajeRequerido: 0, puntajeMaximo: 1, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: true },
            ]
        },
        {
            //Estrategia
            resumen: { pregunta: 8209, puntaje: 0, pestaña: 2, dimension: 'coherencia', puntajeMinimo: 6 },
            preguntas: [
                { pregunta: 9565, puntaje: 9566, puntajeRequerido: 1.5, puntajeMaximo: 3, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: true },
                { pregunta: 9570, puntaje: 9571, puntajeRequerido: 1.5, puntajeMaximo: 3, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: true },
                { pregunta: 9575, puntaje: 9576, puntajeRequerido: 1.5, puntajeMaximo: 3, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: true },
                { pregunta: 9580, puntaje: 9581, puntajeRequerido: 1.5, puntajeMaximo: 3, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: true },
                { pregunta: 9585, puntaje: 9586, puntajeRequerido: 0, puntajeMaximo: 0, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: false },
            ]
        },
        {
            //Complementariedades
            resumen: { pregunta: 9594, puntaje: 0, pestaña: 3, dimension: 'coherencia', puntajeMinimo: 0 },
            preguntas: [
                { pregunta: 9591, puntaje: 9592, puntajeRequerido: 0, puntajeMaximo: 0, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: false },
            ]
        },
        {
            //Enfoques de derechos humanos
            resumen: { pregunta: 8211, puntaje: 0, pestaña: 4, dimension: 'coherencia', puntajeMinimo: 0 },
            preguntas: [
                { pregunta: 9599, puntaje: 9600, puntajeRequerido: 0, puntajeMaximo: 0, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: false },
                { pregunta: 9604, puntaje: 9605, puntajeRequerido: 0, puntajeMaximo: 0, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: false },
            ]
        },
        {
            //Indicadores de Propósito
            resumen: { pregunta: 9644, puntaje: 0, pestaña: 1, dimension: 'consistencia', puntajeMinimo: 4.5 },
            preguntas: [
                { pregunta: 9611, puntaje: 9612, puntajeRequerido: 3, puntajeMaximo: 3, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: true },
                { pregunta: 9616, puntaje: 9617, puntajeRequerido: 1.5, puntajeMaximo: 3, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: true },
                { pregunta: 9621, puntaje: 9622, puntajeRequerido: 0, puntajeMaximo: 3, dosAlternativas: false, cuartaAlternativaCero: false, sumaPuntaje: true },
                { pregunta: 9626, puntaje: 9627, puntajeRequerido: 0, puntajeMaximo: 0, dosAlternativas: true, cuartaAlternativaCero: false, sumaPuntaje: false },
            ]
        },
        {
            //Indicadores Complementarios
            resumen: { pregunta: 9753, puntaje: 0, pestaña: 2, dimension: 'consistencia', puntajeMinimo: 0 },
            preguntas: [
                { pregunta: 9631, puntaje: 9632, puntajeRequerido: 0, puntajeMaximo: 1, dosAlternativas: false, cuartaAlternativaCero: true, sumaPuntaje: true },
                { pregunta: 9636, puntaje: 9637, puntajeRequerido: 0, puntajeMaximo: 1, dosAlternativas: false, cuartaAlternativaCero: true, sumaPuntaje: true },
                { pregunta: 9641, puntaje: 9642, puntajeRequerido: 0, puntajeMaximo: 1, dosAlternativas: false, cuartaAlternativaCero: true, sumaPuntaje: true },
            ]
        },
        {
            //Sistemas de información
            resumen: { pregunta: 8214, puntaje: 0, pestaña: 3, dimension: 'consistencia', puntajeMinimo: 0 },
            preguntas: [
                { pregunta: 9649, puntaje: 9650, puntajeRequerido: 0, puntajeMaximo: 0, dosAlternativas: true, cuartaAlternativaCero: false, sumaPuntaje: false },
            ]
        },
        {
            //Gastos Programa
            resumen: { pregunta: 8215, puntaje: 0, pestaña: 4, dimension: 'consistencia', puntajeMinimo: 0 },
            preguntas: [
                { pregunta: 9655, puntaje: 9656, puntajeRequerido: 0, puntajeMaximo: 2, dosAlternativas: true, cuartaAlternativaCero: false, sumaPuntaje: true },
                { pregunta: 9660, puntaje: 9661, puntajeRequerido: 0, puntajeMaximo: 2, dosAlternativas: true, cuartaAlternativaCero: false, sumaPuntaje: true },
            ]
        }
    ];

    var dimensiones = [
        { nombre: 'atingencia', puntajeObtenido: 9124, puntajeMaximo: 9127, resultado: 9126 },
        { nombre: 'coherencia', puntajeObtenido: 9129, puntajeMaximo: 9130, resultado: 9133 },
        { nombre: 'consistencia', puntajeObtenido: 9134, puntajeMaximo: 9135, resultado: 9138 }
    ];

    var preguntasFaltaInformacion = [7912, 7913, 7914, 7915, 7916];

    //Valida la cantidad de alternativas, sus puntajes y totales de forma automática, las preguntas sin puntajes no son consideradas
    function calcularPuntaje(preguntas, resumen) {
        for (var i = 0; i < preguntas.length ; i++) {
            var alternativa = $("#Tabcmb" + preguntas[i].pregunta.toString() + resumen.pestaña.toString()).prop('selectedIndex');
            var puntaje = 0;
            if (preguntas[i].sumaPuntaje) {
                switch (alternativa) {
                    case 1:
                        puntaje = preguntas[i].puntajeMaximo;
                        break;
                    case 2:
                        if (preguntas[i].dosAlternativas) {
                            puntaje = 0;
                        } else {
                            puntaje = preguntas[i].puntajeMaximo / 2;
                        }
                        break;
                    case 3:
                        puntaje = 0;
                        break;
                    case 4:
                        puntaje = preguntas[i].cuartaAlternativaCero ? 0 : preguntas[i].puntajeMaximo;
                        break;
                }
            }
            //Puntaje por pregunta
            $("#Tabtxt" + preguntas[i].puntaje.toString() + resumen.pestaña.toString()).val(puntaje);
            resumen.puntaje += puntaje;
            if (puntaje < preguntas[i].puntajeRequerido) { console.log(resumen.dimension, ": ", puntaje, " - ", preguntas[i].puntajeMaximo); cumplePuntajeObligatorio = false; }
        }
        //Resumen el total por sub pestaña
        $("#txt" + resumen.pregunta).val(resumen.puntaje);
    }

    function resumenPorDimension() {
        var puntajeTotal = 0;
        const puntajeMaximo = 56;
        const puntajeMinimoAprobacion = 39.5;
        dimensiones.forEach(function (dimension) {
            var puntajeObtenido = 0;
            var puntajeMaximo = 0;
            var resumenEvaluacion = preguntasEvaluacion.filter(element => element.resumen.dimension == dimension.nombre);
            puntajeObtenido = resumenEvaluacion.reduce((acc, curr) => acc + curr.resumen.puntaje, 0);
            const totalPuntajeMaximo = resumenEvaluacion.reduce((acc, current) => {
                return acc + current.preguntas.reduce((innerAcc, question) => innerAcc + question.puntajeMaximo, 0);
            }, 0);
            $("#txt" + dimension.puntajeObtenido).val(puntajeObtenido);
            $("#txt" + dimension.puntajeMaximo).val(totalPuntajeMaximo);
            puntajeTotal += puntajeObtenido;
        });
        //Puntaje final
        $("#txt7908").val(puntajeTotal);
        //Porcentaje de logro
        $("#txt9100").val(((puntajeTotal / puntajeMaximo) * 100).toFixed(0) + "%");
        //Calificación
        if (cumplePuntajeObligatorio) {
            $("#cmb9750").val(puntajeTotal > puntajeMinimoAprobacion ? 30522 : 30521);
        } else {
            $("#cmb9750").val(30521);
        }
    }

    function validarFaltaInformacion() {
        //Validación falta información - Si algunas de estas preguntas se contesta "Si" la calificación será "Falta información"
        preguntasFaltaInformacion.forEach(function (element) {
            if ($("#cmb" + element + " option:selected").val() == 902) { $("#cmb9750").val(30520); }
        });
    }



    function validarFaltanPreguntasEvaluacion() {
        var faltanPreguntas = false
        preguntasEvaluacion.forEach(function (subMenu) {
            subMenu.preguntas.forEach(function (preguntas) {
                if ($("#Tabcmb" + preguntas.pregunta.toString() + subMenu.resumen.pestaña.toString()).val() == "-1") {
                    faltanPreguntas = true;
                }
            });
        });
        preguntasFaltaInformacion.forEach(function (element) {
            if ($("#cmb" + element).val() == "-1") { faltanPreguntas = true; }
        });
        return faltanPreguntas ? $('#btnEnviarEvaluacionGore').hide() : $('#btnEnviarEvaluacionGore').show();
    }

    preguntasEvaluacion.forEach(function (element) {
        calcularPuntaje(element.preguntas, element.resumen);
    });

    //VALIDACIÓN EVALUACIÓN PROGRAMA GORE
    //30520 - Falta información
    //30521 - Objetado técnicamente
    //30522 - Recomendado favorablemente

    resumenPorDimension();
    validarFaltaInformacion();
    validarFaltanPreguntasEvaluacion();
}

$(document).ready(function () {
    //Envio evaluacion GORE
    $("#btnEnviarEvaluacionGore").click(function () {
        validaBlancos();
        if ($('#btnEnviarEvaluacionGore').is(':hidden')) {
            $('#txtModalEnviarEvaluacionGore').html('<p style="margin: 0;">Faltan alternativas por seleccionar en <b>Evaluación Final</b><br/><br/>Una vez completado <b>Guardar</b> el formulario</p>');
            $('#btnEnviarJefaturaGore').prop('disabled', true);

        } else {
            $('#txtModalEnviarEvaluacionGore').html('<p><b>Antes de enviar se recomienda guardar los cambios realizados</b></p><p>Se enviará la evaluación a Jefatura para su revisión </p><p style="margin: 0;"><b>¿Continuar?</b></p>');
            $('#btnEnviarJefaturaGore').prop('disabled', false);
        }
        $('#modalEnviarEvaluacionGore').modal('show');
    });
    $("#btnEnviarJefaturaGore").click(function () {
        $('#modalEnviarEvaluacionGore').modal('hide');
        var calificacion = $('#cmb9750').val();
        $(this).button('loading');
        var opciones = {
            url: ROOT + "Gores/EnviarEvaluacionJefatura",
            data: { _id: queryString('_id'), calificacion: calificacion },
            type: "post",
            datatype: "json",
            async: false,
            beforeSend: function () { $("#cargaPagina").fadeIn(); },
            success: function (result, xhr, status) {
                $("#btnEnviarJefaturaGore").button('reset');
                $("#modalEnvioEvaluacion").modal('hide');
                if (result == "ok") {
                    $("#txtMensajesFormulario").empty();
                    $("#txtMensajesFormulario").html("<p>Evaluación enviada exitosamente</p>");
                    $('#modalMensajesFormularios').modal('show');
                    window.location.reload();
                } else {
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
})
