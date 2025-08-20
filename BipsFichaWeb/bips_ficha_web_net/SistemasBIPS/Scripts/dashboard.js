$(document).ready(function () {
    $("#menuExAnte").addClass('in');
    $("#aMenuDashboardExAnte").dropdown('toggle');
    $("#programasIngresados").empty();
    $("#programasIngresados").text(indicadores.IdPrograma);
    $("#programasEvaluados").empty();
    $("#programasEvaluados").text(indicadores.IdBips);
    $("#programasEvaluacion").empty();
    $("#programasEvaluacion").text(indicadores.IdEtapa);
    $("#programasPromedio").empty();
    $("#programasPromedio").text(indicadores.IdEstado);
    //Datos seleccionados por filtro
    let filtroValores = {
        selectMinisterio: '-1',
        selectServicio: '-1',
        selectPrograma: '-1',
        selectIteracion: '-1',
        selectEvaluadorUno: '-1',
        selectEvaluadorDos: '-1',
    }
    let nuevoTipoProgramas = [];
    if (tipoProgramas.length > 0) {
        var colores = ['#db5c54', '#24446c', '#82CAFF'];
        $.each(tipoProgramas, function (i, item) {
            var tipoPrograma = {};
            tipoPrograma.name = item.Tipo;
            tipoPrograma.color = colores[i];
            tipoPrograma.y = item.IdEtapa;
            tipoPrograma.id = i;
            nuevoTipoProgramas.push(tipoPrograma);
        });
    }
    let nuevoCalificacionProgramas = [];
    if (califProgramas.length > 0) {
        var colores = ['#24446c', '#db5c54', '#82CAFF'];
        $.each(califProgramas, function (i, item) {
            var tipoCalificaciones = {};
            tipoCalificaciones.name = item.Tipo;
            tipoCalificaciones.color = colores[i];
            tipoCalificaciones.y = item.IdEtapa;
            tipoCalificaciones.id = i;
            nuevoCalificacionProgramas.push(tipoCalificaciones);
        });
    }
    let nuevoEstadoProgramas = [];
    if (estadoProgramas.length > 0) {
        var colores = ['#24446c', '#db5c54'];
        var progFuera = 0;
        var progDentro = 0;
        $.each(estadoProgramas, function (i, item) {
            if (item.IdPrograma == "321" || item.IdPrograma == "322") {
                progFuera += item.IdEtapa;
            } else {
                progDentro += item.IdEtapa;;
            }            
        });
        nuevoEstadoProgramas.push({ name: 'Programa dentro', color: '#24446c', y: progDentro });
        nuevoEstadoProgramas.push({ name: 'Programa fuera', color: '#db5c54', y: progFuera });
    }

    //Datos Gauge Chart
    let presupuestoEvaluado = 0;
    let presupuestoEvaluadoMax = 0;
    let promedioPuntaje = 0;
    let promedioPuntajeMax = 0;
    let promedioAtingencia = 0;
    let promedioAtingenciaMax = 0;
    let promedioCoherencia = 0;
    let promedioCoherenciaMax = 0;
    let promedioConsistencia = 0;
    let promedioConsistenciaMax = 0;

    //Datos Column chart
    let numeroIteracion = [];
    progXIteracion.forEach(element => {
        numeroIteracion.push(element.IdBips);
    });
    let cantidadIteraciones = [];
    for (var i = 1; i <= indicadores.IdGrupoFormulario; i++) {
        cantidadIteraciones.push(i);
    }

    $("#selectMinisterio").change(function () {
        var valorObj = $(this).val();
        $("#selectServicio").empty();
        $("#selectServicio").append("<option value='-1'>Seleccione</option>");
        if (valorObj != "-1") {
            if (servicios.length > 0) {
                $.each(servicios, function (i, d) {
                    if (d.Valor != null) {
                        if (valorObj == d.Valor) {
                            $("#selectServicio").append("<option value='" + d.IdParametro + "'>" + d.Descripcion + "</option>");
                        }
                    }
                });
                $("#selectServicio").prop('disabled', false);
            }
        } else {
            $("#selectServicio").prop('disabled', true);
        }        
    });

    $("#selectServicio").change(function () {
        var valorObj = $(this).val();
        $("#selectPrograma").empty();
        $("#selectPrograma").append("<option value='-1'>Seleccione</option>");
        if (valorObj != "-1") {
            if (programas.length > 0) {
                var totalProg = 0;
                $.each(programas, function (i, d) {
                    if (d.IdServicio.IdParametro != null) {
                        if (valorObj == d.IdServicio.IdParametro.toString()) {
                            $("#selectPrograma").append("<option value='" + d.IdPrograma + "'>" + d.Nombre + "</option>");
                            totalProg++;
                        }
                    }
                });
                if (totalProg > 0) {
                    $("#selectPrograma").prop('disabled', false);
                } else {
                    $("#selectPrograma").prop('disabled', true);
                }
            }
        } else {
            $("#selectPrograma").prop('disabled', true);
        }
    });

    let selectIteracion = $("#selectIteracion");
    for (var i = 1; i <= indicadores.IdGrupoFormulario; i++) {
        selectIteracion.append($('<option>', { value: i, text: i }));
    }

    let selectEvaluadorUno = $("#selectEvaluadorUno");
    $.each(evaluadores, function (index, value) {
        selectEvaluadorUno.append($('<option>', {
            value: value.Id,
            text: value.Nombre
        }));
    });

    let selectEvaluadorDos = $("#selectEvaluadorDos");
    $.each(evaluadores, function (index, value) {
        selectEvaluadorDos.append($('<option>', {
            value: value.Id,
            text: value.Nombre
        }));
    });

    //Watch para actualizar el dato seleccionado por filtro
    /*$("select").change(function () {
        var idSelect = $(this).attr('id');
        var valorSeleccionado = $(this).val();
        actualizarFiltro(idSelect, valorSeleccionado);
        if (idSelect === 'selectMinisterio') {
            actualizarServicio();
        }
        if (idSelect === 'selectServicio') {
            actualizarPrograma();
        }
    });*/

    //Watch para obtener los datos específicos de la tabla, variables están estáticas
    $("#tablaEtapa tbody").on( 'click', 'tr', function () {
        // Obtener los datos de la fila clicada
        let fila = $(this).data();
        console.log(fila.numero);
        console.log(fila.etapa);
        actualizarValoresTextos();
        actualizarValoresGraficas();
        actualizarValoresTabla();
    });

    actualizarValoresTextos();
    actualizarValoresGraficas();
    actualizarValoresTabla();

    //Funciones de actualizaciones
    function actualizarFiltro(idSelect, valorSeleccionado) {
        switch (idSelect) {
            case 'selectMinisterio':
                filtroValores.selectMinisterio = valorSeleccionado;
                break;
            case 'selectServicio':
                filtroValores.selectServicio = valorSeleccionado;
                break;
            case 'selectPrograma':
                filtroValores.selectPrograma = valorSeleccionado;
                break;
            case 'selectIteracion':
                filtroValores.selectIteracion = valorSeleccionado;
                break;
            case 'selectEvaluadorUno':
                filtroValores.selectEvaluadorUno = valorSeleccionado;
                break;
            case 'selectEvaluadorDos':
                filtroValores.selectEvaluadorDos = valorSeleccionado;
                break;
        }
        actualizarValoresTextos();
        actualizarValoresGraficas();
        actualizarValoresTabla();
    }

    function actualizarServicio() {
        $("#selectServicio").empty();
        let selectServicio = $("#selectServicio");
        selectServicio.append($('<option>', {
            value: '-1',
            text: 'Seleccione'
        }));
        if (filtroValores.selectMinisterio !== '-1') {
            $("#selectServicio").prop('disabled', false);
            let servicioFiltrado = servicios.filter((servicio) => servicio.ministerio_id.toString() === filtroValores.selectMinisterio);
            $.each(servicioFiltrado, function (index, value) {
                selectServicio.append($('<option>', {
                    value: value.id,
                    text: value.nombre
                }));
            });
        } else {
            $("#selectServicio").prop('disabled', true);
        }
        actualizarFiltro('selectServicio', '-1');
        actualizarPrograma();
    }

    function actualizarPrograma() {
        $("#selectPrograma").empty();
        let selectPrograma = $("#selectPrograma");
        selectPrograma.append($('<option>', { value: '-1', text: 'Seleccione' }));
        if (filtroValores.selectServicio !== '-1') {
            $("#selectPrograma").prop('disabled', false);
            let programaFiltrado = programas.filter((programa) => programa.servicio_id.toString() === filtroValores.selectServicio);
            $.each(programaFiltrado, function (index, value) {
                selectPrograma.append($('<option>', {
                    value: value.id,
                    text: value.nombre
                }));
            });
        } else {
            $("#selectPrograma").prop('disabled', true);
            actualizarFiltro('selectPrograma', '-1')
        }
    }

    function actualizarValoresTextos() {
        //Solo para agregar datos dinámicos a los campos
        //$('#programasIngresados').text(Math.floor(Math.random() * 100) + 1);
        //$('#programasEvaluados').text(Math.floor(Math.random() * 100) + 1);
        //$('#programasPromedio').text(Math.floor(Math.random() * 300) + 1);
        //$('#programasEvaluacion').text(Math.floor(Math.random() * 20) + 1);
    }

    function actualizarValoresTabla() {
        //Solo para agregar datos dinámicos a los campos
        let totalProgramas = 0;
        let datos = [
            { etapa: "Envío a sectorialista", numero: Math.floor(Math.random() * 15) + 1 },
            { etapa: "Fecha dupla", numero: Math.floor(Math.random() * 15) + 1 },
            { etapa: "Comentarios monitoreo", numero: Math.floor(Math.random() * 15) + 1 },
            { etapa: "Evaluación enviada", numero: Math.floor(Math.random() * 15) + 1 },
        ];

        function cargarDatos() {
            let tbody = $('#tablaEtapa tbody');
            tbody.empty();
            $.each(datos, function (index, dato) {
                let row = '<tr data-etapa="' + dato.etapa + '" data-numero="' + dato.numero + '">' +
                    '<td>' + dato.etapa + '</td>' +
                    '<td>' + dato.numero + '</td>' +
                    '</tr>';
                tbody.append(row);
                totalProgramas += dato.numero
            });
            $('#tablaTotal').text(totalProgramas);
        }
        cargarDatos();
    }

    function actualizarValoresGraficas() {
        //Solo para agregar datos dinámicos a los campos
        /*nuevoTipoProgramas = [
                { name: 'Reformulado', color: '#db5c54', y: Math.floor(Math.random() * 50) + 1, id: 2 },
                { name: 'Regularizado', color: '#24446c', y: Math.floor(Math.random() * 50) + 1, id: 3 }
        ]*/
        /*nuevoCalificacionProgramas = [
                { name: 'Recomendado favorablemente', color: '#24446c', y: Math.floor(Math.random() * 50) + 1 },
                { name: 'Objetado técnicamente', color: '#db5c54', y: Math.floor(Math.random() * 50) + 1 }
        ]*/
        /*nuevoEstadoProgramas = [
                { name: 'Programa dentro', color: '#24446c', y: Math.floor(Math.random() * 50) + 1 },
                { name: 'Programa fuera', color: '#db5c54', y: Math.floor(Math.random() * 50) + 1 }
        ]*/

        presupuestoEvaluado = Math.floor(Math.random() * 50) + 1;
        presupuestoEvaluadoMax = presupuestoEvaluado + Math.floor(Math.random() * 20) + 1;

        //promedioPuntaje = Math.floor(Math.random() * 50) + 1;
        //promedioPuntajeMax = promedioPuntaje + Math.floor(Math.random() * 20) + 1;
        //promedioAtingencia = Math.floor(Math.random() * 50) + 1;
        //promedioAtingenciaMax = promedioAtingencia + Math.floor(Math.random() * 20) + 1;
        //promedioCoherencia = Math.floor(Math.random() * 80) + 1;
        //promedioCoherenciaMax = promedioCoherencia + Math.floor(Math.random() * 20) + 1;
        //promedioConsistencia = Math.floor(Math.random() * 80) + 1;
        //promedioConsistenciaMax = promedioConsistencia + Math.floor(Math.random() * 20) + 1;

        /*numeroIteracion = [];
        iteraciones.forEach(element => {
            numeroIteracion.push(Math.floor(Math.random() * 50) + 1);
        });*/

        if ($('#tipoProgramas').highcharts()) {
            $('#tipoProgramas').highcharts().series[0].setData(nuevoTipoProgramas);
            $('#calificacionProgramas').highcharts().series[0].setData(nuevoCalificacionProgramas);
            $('#estadoProgramas').highcharts().series[0].setData(nuevoEstadoProgramas);
        }

        if ($('#presupuestoEvaluado').highcharts()) {
            $('#presupuestoEvaluado').highcharts().series[0].setData([presupuestoEvaluado]);
            $('#presupuestoEvaluado').highcharts().series[0].yAxis.setExtremes(null, presupuestoEvaluadoMax);

            //$('#promedioPuntaje').highcharts().series[0].setData([promedioPuntaje]);
            //$('#promedioPuntaje').highcharts().series[0].yAxis.setExtremes(null, promedioPuntajeMax);

            //$('#promedioAtingencia').highcharts().series[0].setData([promedioAtingencia]);
            //$('#promedioAtingencia').highcharts().series[0].yAxis.setExtremes(null, promedioAtingenciaMax);

            //$('#promedioCoherencia').highcharts().series[0].setData([promedioCoherencia]);
            //$('#promedioCoherencia').highcharts().series[0].yAxis.setExtremes(null, promedioCoherenciaMax);

            //$('#promedioConsistencia').highcharts().series[0].setData([promedioConsistencia]);
            //$('#promedioConsistencia').highcharts().series[0].yAxis.setExtremes(null, promedioConsistenciaMax);
        }

        /*if ($('#programasIteracion').highcharts()) {
            $('#programasIteracion').highcharts().series[0].setData(numeroIteracion);
        }*/
    }

    //Configuraciones Pie charts
    Highcharts.chart('tipoProgramas', {
        credits: {
            enabled: false
        },
        exporting: {
            enabled: false
        },
        chart: {
            type: 'pie',
            height: '300px'
        },
        title: {
            text: 'Tipo de programas',
            style:{
                fontSize: '18px',
                fontWeight: 'normal'
            }
        },
        tooltip: {
            enabled: false
        },
        plotOptions: {
            series: {
                //Permite separar, resaltando la parte del porcentaje seleccionado
                allowPointSelect: false,
                cursor: 'pointer',
                dataLabels: [
                    {
                        enabled: true,
                        distance: 10,
                        format: '{point.name}: {y}',
                        style: {
                            fontSize: '14px'
                        }
                    },
                    {
                        enabled: true,
                        //Distancia del texto de porcentaje del nombre
                        distance: -40,
                        format: '{point.percentage:.1f}%',
                        style: {
                            fontSize: '18px',
                            textOutline: 'none',
                            opacity: 1
                        },
                        filter: {
                            operator: '>',
                            property: 'percentage',
                            value: 20
                        }
                    }
                ],
                point: {
                    events: {
                        click: function (e) {
                            filtroTipoProgramas(e.point);
                        }
                    }
                },
            }
        },
        series: [
            {
                name: 'Percentage',
                colorByPoint: true,
                data: nuevoTipoProgramas
            }
        ]
    });
    Highcharts.chart('calificacionProgramas', {
        credits: {
            enabled: false
        },
        exporting: {
            enabled: false
        },
        chart: {
            type: 'pie',
            height: '300px'
        },
        title: {
            text: 'Calificación programas',
            style: {
                fontSize: '18px',
                fontWeight: 'normal'
            }
        },
        tooltip: {
            enabled: false
        },
        plotOptions: {
            series: {
                //Permite separar, resaltando la parte del porcentaje seleccionado
                allowPointSelect: false,
                cursor: 'pointer',
                dataLabels: [
                    {
                        enabled: true,
                        distance: 10,
                        format: '{point.name}: {y}',
                        style: {
                            fontSize: '14px'
                        }
                    },
                    {
                        enabled: true,
                        //Distancia del texto de porcentaje del nombre
                        distance: -40,
                        format: '{point.percentage:.1f}%',
                        style: {
                            fontSize: '14px',
                            textOutline: 'none',
                            opacity: 1
                        },
                        filter: {
                            operator: '>',
                            property: 'percentage',
                            value: 20
                        }
                    }
                ],
                point: {
                    events: {
                        click: function (e) {
                            filtroCalificacionProgramas(e.point);
                        }
                    }
                },
            }
        },
        series: [
            {
                name: 'Percentage',
                colorByPoint: true,
                data: nuevoCalificacionProgramas
            }
        ]
    });
    Highcharts.chart('estadoProgramas', {
        credits: {
            enabled: false
        },
        exporting: {
            enabled: false
        },
        chart: {
            type: 'pie',
            height: '300px'
        },
        title: {
            text: 'Estado programas',
            style: {
                fontSize: '18px',
                fontWeight: 'normal'
            }
        },
        tooltip: {
            enabled: false
        },
        plotOptions: {
            series: {
                //Permite separar, resaltando la parte del porcentaje seleccionado
                allowPointSelect: false,
                cursor: 'pointer',
                dataLabels: [
                    {
                        enabled: true,
                        distance: 10,
                        format: '{point.name}: {y}',
                        style: {
                            fontSize: '14px'
                        }
                    },
                    {
                        enabled: true,
                        //Distancia del texto de porcentaje del nombre
                        distance: -40,
                        format: '{point.percentage:.1f}%',
                        style: {
                            fontSize: '14px',
                            textOutline: 'none',
                            opacity: 1
                        },
                        filter: {
                            operator: '>',
                            property: 'percentage',
                            value: 20
                        }
                    }
                ],
                point: {
                    events: {
                        click: function (e) {
                            filtroEstadoProgramas(e.point);
                        }
                    }
                },
            }
        },
        series: [
            {
                name: 'Percentage',
                colorByPoint: true,
                data: nuevoEstadoProgramas
            }
        ]
    });

    //Configuraciones Gauge charts
    const puntajesConfig1 = {
        chart: {
            type: 'solidgauge'
        },
        title: null,
        pane: {
            center: ['50%', '55%'],
            size: '100%',
            startAngle: -90,
            endAngle: 90,
            background: {
                backgroundColor:
                Highcharts.defaultOptions.legend.backgroundColor || '#EEE',
                innerRadius: '60%',
                outerRadius: '100%',
                shape: 'arc',
            }
        },
        exporting: {
            enabled: false
        },
        tooltip: {
            enabled: false
        },
        yAxis: {
            stops: [
                [0, '#adcfed'], //Color de la barra, se puede ajustar el color delimitando por porcentaje
            ],
            lineWidth: 0,
            tickWidth: 0,
            minorTickInterval: null,
            tickAmount: 2,
            title: {
                y: -70
            },
            labels: {
                y: 30,
                style: {
                    fontSize: '24px',
                }
            }
        },

        plotOptions: {
            solidgauge: {
                dataLabels: {
                    y: 0.5,
                    borderWidth: 0,
                    useHTML: true
                }
            },
        }
    };
    const puntajesConfig2 = {
        chart: {
            type: 'solidgauge'
        },
        title: null,
        pane: {
            size: '100%',
            startAngle: -90,
            endAngle: 90,
            background: {
                backgroundColor:
                Highcharts.defaultOptions.legend.backgroundColor || '#EEE',
                innerRadius: '60%',
                outerRadius: '100%',
                shape: 'arc',
            }
        },
        exporting: {
            enabled: false
        },
        tooltip: {
            enabled: false
        },
        yAxis: {
            stops: [
                [0, '#24446c'], //Color de la barra, se puede ajustar el color delimitando por porcentaje
            ],
            lineWidth: 0,
            tickWidth: 0,
            minorTickInterval: null,
            tickAmount: 2,
            title: {
                y: -70
            },
            labels: {
                y: 14,
                style: {
                    fontSize: '14px'
                }
            }
        },

        plotOptions: {
            solidgauge: {
                dataLabels: {
                    y: 0.5,
                    borderWidth: 0,
                    useHTML: true
                }
            },
        }
    };
    Highcharts.chart('presupuestoEvaluado', Highcharts.merge(puntajesConfig1, {
        yAxis: {
            //Configuracion minimo y maximo, no modificar tickPositioner
            min: 0,
            max: presupuestoEvaluadoMax,
            tickPositioner: function () {
                return [this.min, this.max];
            },
        },
        credits: {
            enabled: false
        },
        series: [{
            //Puntaje actual
            data: [presupuestoEvaluado],
            dataLabels: {
                format:
                '<div style="text-align:center;position:relative;top:-18px;">' +
                '<span style="font-size:42px;font-weight:100;">{y} mil</span><br/>' +
                '</div>'
            },
        }]
    }));
    Highcharts.chart('promedioPuntaje', Highcharts.merge(puntajesConfig1, {
        yAxis: {
            //Configuracion minimo y maximo, no modificar tickPositioner
            min: 0,
            max: 100,
            tickPositioner: function () {
                return [this.min, this.max];
            },
        },
        credits: {
            enabled: false
        },
        series: [{
            //Puntaje actual
            data: [indicadores.IdExcepcion],
            dataLabels: {
                format:
                '<div style="text-align:center;position:relative;top:-18px;">' +
                '<span style="font-size:42px;font-weight:100;">{y}</span><br/>' +
                '</div>'
            },
        }]
    }));
    Highcharts.chart('promedioAtingencia', Highcharts.merge(puntajesConfig2, {
        yAxis: {
            //Configuracion minimo y maximo, no modificar tickPositioner
            min: 0,
            max: 30,
            tickPositioner: function () {
                return [this.min, this.max];
            },
        },
        credits: {
            enabled: false
        },
        series: [{
            //Puntaje actual
            data: [indicadores.IdPerfil],
            dataLabels: {
                format:
                '<div style="text-align:center;position:relative;top:-33px;">' +
                '<span style="font-size:28px;font-weight:100;">{y}</span><br/>' +
                '</div>'
            },
        }]
    }));
    Highcharts.chart('promedioCoherencia', Highcharts.merge(puntajesConfig2, {
        yAxis: {
            //Configuracion minimo y maximo, no modificar tickPositioner
            min: 0,
            max: 40,
            tickPositioner: function () {
                return [this.min, this.max];
            },
        },
        credits: {
            enabled: false
        },
        series: [{
            //Puntaje actual
            data: [indicadores.IdPlataforma],
            dataLabels: {
                format:
                '<div style="text-align:center;position:relative;top:-33px;">' +
                '<span style="font-size:28px;font-weight:100;">{y}</span><br/>' +
                '</div>'
            },
        }]
    }));
    Highcharts.chart('promedioConsistencia', Highcharts.merge(puntajesConfig2, {
        yAxis: {
            //Configuracion minimo y maximo, no modificar tickPositioner
            min: 0,
            max: 30,
            tickPositioner: function () {
                return [this.min, this.max];
            },
        },
        credits: {
            enabled: false
        },
        series: [{
            //Puntaje actual
            data: [indicadores.IdTipoFormulario],
            dataLabels: {
                format:
                '<div style="text-align:center;position:relative;top:-33px;">' +
                '<span style="font-size:28px;font-weight:100;">{y}</span><br/>' +
                '</div>'
            },
        }]
    }));

    //Configuraciones Column charts
    Highcharts.chart('programasIteracion', {
        colors: ['#24446c'],
        credits: {
            enabled: false
        },
        exporting: {
            enabled: false
        },
        chart: {
            type: 'column'
        },
        title: {
            text: null
        },
        xAxis: {
            categories: cantidadIteraciones,
            crosshair: true,
            accessibility: {
                description: 'Iteraciones'
            },
            labels: {
                style: {
                    fontSize: 16
                }
            }
        },
        yAxis: {
            min: 0,
            //labels: {
            //    enabled: false
            //},
            title: {
                text: null
            },
            labels: {
                style: {
                    fontSize: 16
                }
            }
        },
        tooltip: {
            format:
                '<span style="font-size:16px;">Programas de esta iteración: {y}</span>'
        },
        plotOptions: {
            series: {
                cursor: 'pointer',
                point: {
                    events: {
                        click: function (e) {
                            filtroProgramasIteracion(e.point);
                        }
                    }
                },
                colorByPoint: true
            },
        },
        series: [
            {
                showInLegend: false,
                data: numeroIteracion
            }
        ]
    });

    //Funciones de los gráficos
    function filtroTipoProgramas(datos) {
        console.log(datos);

        actualizarValoresTextos();
        actualizarValoresGraficas();
        actualizarValoresTabla();
    }

    function filtroCalificacionProgramas(datos) {
        console.log(datos);

        actualizarValoresTextos();
        actualizarValoresGraficas();
        actualizarValoresTabla();
    }

    function filtroEstadoProgramas(datos) {
        console.log(datos);

        actualizarValoresTextos();
        actualizarValoresGraficas();
        actualizarValoresTabla();
    }

    function filtroProgramasIteracion(datos) {
        console.log(datos);

        actualizarValoresTextos();
        actualizarValoresGraficas();
        actualizarValoresTabla();
    }
});
